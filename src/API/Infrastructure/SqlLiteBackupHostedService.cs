using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class SqlLiteBackupHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BlobStorageSettings _storageSettings;
        private readonly ILogger<SqlLiteBackupHostedService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStorageService _storageService;
        private TimeSpan _interval;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly int _retryCount = 3;

        public SqlLiteBackupHostedService(
            IServiceProvider serviceProvider,
            ILogger<SqlLiteBackupHostedService> logger,
            IOptions<BlobStorageSettings> storageSettings,
            IConfiguration configuration,
            IStorageService storageService,
            IWebHostEnvironment hostEnvironment)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _storageSettings = storageSettings.Value;
            _configuration = configuration;
            _storageService = storageService;

            _interval = TimeSpan.FromDays(_configuration.GetValue<int>("BackupIntervalDays", 7));
            _hostEnvironment = hostEnvironment;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<SqlLiteFarmerDbContext>();

                // Retrieve database file path
                var sourceDbPath = dbContext.Database.GetDbConnection().ConnectionString
                    .Replace("Data Source=", string.Empty).Trim();
                var backupFileName = $"Farmer_{_hostEnvironment.EnvironmentName}_backup_{DateTime.UtcNow:yyyyMMddHHmmss}.db";
                var backupFilePath = Path.Combine(Path.GetTempPath(), backupFileName);

                try
                {
                    // Create a local backup copy
                    File.Copy(sourceDbPath, backupFilePath, overwrite: true);

                    // Attempt to upload with retry logic
                    bool uploadSucceeded = await UploadBackupWithRetryAsync(backupFilePath, backupFileName, stoppingToken);

                    if (uploadSucceeded)
                    {
                        _logger.LogInformation("SQLite backup successfully uploaded to Azure Blob Storage.");
                    }
                    else
                    {
                        _logger.LogWarning("Backup upload failed after multiple retries. Will retry after the interval.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred during backup process.");
                }
                finally
                {
                    // Clean up local backup file
                    if (File.Exists(backupFilePath))
                    {
                        File.Delete(backupFilePath);
                    }
                }

                // Wait for the next interval
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task<bool> UploadBackupWithRetryAsync(string backupFilePath, string backupFileName, CancellationToken cancellationToken)
        {
            for (int attempt = 1; attempt <= _retryCount; attempt++)
            {
                try
                {
                    await using var fileStream = File.OpenRead(backupFilePath);
                    bool result = await _storageService.UploadFileAsync(fileStream, _storageSettings.BackupContainerName, backupFileName);
                    if (result)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex,"Backup upload attempt {Attempt}/{RetryCount} failed.", attempt, _retryCount);
                }

                if (attempt < _retryCount)
                {
                    // Wait briefly before retrying
                    await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);
                }
            }

            return false;
        }
    }
}

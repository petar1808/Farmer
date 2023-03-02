using Application.Services;
using Infrastructure.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Infrastructure.Workers
{
    public class SqlLiteBackupWorker : BackgroundService
    {
        private readonly ILogger<SqlLiteBackupWorker> _logger;
        private readonly IOptions<InfrastructureSettings> _infrastructureSettings;
        private readonly IExternalStorage _externalStorage;
        private readonly IWebHostEnvironment _currentEnvironment;
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        public SqlLiteBackupWorker(
            ILogger<SqlLiteBackupWorker> logger,
            IOptions<InfrastructureSettings> infrastructureSettings,
            IExternalStorage externalStorage,
            IWebHostEnvironment currentEnvironment,
            IOptions<ConnectionStrings> connectionStrings)
        {
            this._logger = logger;
            this._infrastructureSettings = infrastructureSettings;
            this._externalStorage = externalStorage;
            this._currentEnvironment = currentEnvironment;
            this._connectionStrings = connectionStrings;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(SqlLiteBackupWorker)} run at: {DateTime.UtcNow}");

                var directoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

                if (_currentEnvironment.IsEnvironment("Local"))
                {
                    directoryPath = Directory.GetCurrentDirectory();
                }

                string sourceFilePath = $"{directoryPath}/{_connectionStrings.Value.GetSqlLiteDatabaseName()}";
                string destinationFilePath = $"{directoryPath}/Farmer-{DateTime.UtcNow.ToString("MM/dd/yyyy")}.db";


                File.Copy(sourceFilePath, destinationFilePath, true);

                using var file = new FileStream(destinationFilePath!, FileMode.Open, FileAccess.Read);

                string saveToPath = $"{_infrastructureSettings.Value.SqlLiteBackupFolderName}/" +
                    $"{_currentEnvironment.EnvironmentName}/Farmer-{DateTime.UtcNow.ToString("MM/dd/yyyy")}.db";

                var createdFileName = await _externalStorage.UploadFile(saveToPath, file);

                File.Delete(destinationFilePath);

                _logger.LogInformation($"New backup created {createdFileName}");

                await Task.Delay(TimeSpan.FromHours(this._infrastructureSettings.Value.SqlLiteBackupHours), stoppingToken);
            }
        }
    }
}

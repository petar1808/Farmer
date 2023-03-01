using Application.Services;
using Dropbox.Api;
using Dropbox.Api.Files;
using Infrastructure.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Infrastructure.ExternalStorage
{
    public class DropBoxService : IExternalStorage
    {
        private readonly DropBoxSettings _dropBoxSettings;
        private const string separator = "/";
        private readonly ILogger<DropBoxService> _logger;

        public DropBoxService(
            IOptions<DropBoxSettings> settings, 
            ILogger<DropBoxService> logger)
        {
            this._dropBoxSettings = settings.Value;
            _logger = logger;
        }

        public async Task<Stream> DownloadFile(string path)
        {
            try
            {
                using var dropboxClient = new DropboxClient(_dropBoxSettings.AccessToken);
                var fileMetadata = await dropboxClient.Files.DownloadAsync(path);
                return await fileMetadata.GetContentAsStreamAsync();
            }
            catch (DropboxException ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }

        // To do for improvement
        public async Task<Stream> DownloadLastBackUpFile(string path)
        {
            try
            {
                using var dropboxClient = new DropboxClient(_dropBoxSettings.AccessToken);
                var files = await dropboxClient.Files.ListFolderAsync(new ListFolderArg(path));
                var lastBackupFileName = files.Entries.Last().Name;

                return await DownloadFile(path + separator + lastBackupFileName);
            }
            catch (DropboxException ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                throw;
            }
        }
    }
}

using Application.Services;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Infrastructure.Storage
{
    public class BlobStorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<BlobStorageService> _logger;
        public BlobStorageService(
            BlobServiceClient blobServiceClient,
            ILogger<BlobStorageService> logger)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        public async Task<bool> UploadFileAsync(Stream content, string containerName, string fileName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                if (!blobContainerClient.Exists())
                {
                    await blobContainerClient.CreateAsync();
                }

                var blobClient = blobContainerClient.GetBlobClient(fileName);

                var result = await blobClient.UploadAsync(content);

                return result.GetRawResponse().Status == (int)HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while uploading file '{BlobName}': {Message}", containerName, ex.Message);
                return false;
            }
        }
    }
}

namespace Application.Services
{
    public interface IStorageService
    {
        Task<bool> UploadFileAsync(Stream content, string containerName, string fileName);
    }
}

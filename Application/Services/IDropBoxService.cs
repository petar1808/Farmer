namespace Application.Services
{
    public interface IExternalStorage
    {
        Task<Stream> DownloadFile(string path);

        Task<Stream> DownloadLastBackUpFile(string path);

        Task<string> UploadFile(string path, Stream content);
    }
}

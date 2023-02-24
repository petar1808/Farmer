namespace Application.Services
{
    public interface IExternalStorage
    {
        Task<Stream> DownloadFile(string path);
    }
}

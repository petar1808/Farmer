namespace Infrastructure.Storage
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string BackupContainerName { get; set; } = string.Empty;
    }
}

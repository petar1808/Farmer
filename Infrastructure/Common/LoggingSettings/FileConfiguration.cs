namespace Infrastructure.Common.LoggingSettings
{
    public class FileConfiguration
    {
        public bool IsEnabled { get; set; }
        public string MinLogLevel { get; set; } = default!;
        public string FileName { get; set; } = default!;
    }
}
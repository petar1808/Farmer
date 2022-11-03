namespace Infrastructure.Common.LoggingSettings
{
    public class ConsoleConfiguration
    {
        public bool IsEnabled { get; set; }
        public string MinLogLevel { get; set; } = default!;
    }
}
namespace Infrastructure.Common.LoggingSettings
{
    public class SqlConfiguration
    {
        public bool IsEnabled { get; set; }

        public string MinLogLevel { get; set; } = default!;

        public string TableName { get; set; } = default!;
    }
}
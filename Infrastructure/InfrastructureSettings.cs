namespace Infrastructure
{
    public class InfrastructureSettings
    {
        public string Secret { get; set; } = default!;

        public DatabaseProvider DatabaseProvider { get; set; }

        public bool EnableSensitiveDataLogging { get; set; }
    }

    public enum DatabaseProvider
    {
        SqlLite,
        SqlServer
    }
}

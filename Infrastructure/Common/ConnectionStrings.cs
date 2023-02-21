namespace Infrastructure.Common
{
    public class ConnectionStrings
    {
        public string SqlDefaultConnection { get; set; } = default!;

        public string SqlLiteConnection { get; set; } = default!;

        public string MySqlConnection { get; set; } = default!;

        public bool EnableSensitiveDataLogging { get; set; }
    }
}

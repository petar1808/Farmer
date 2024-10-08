﻿namespace Infrastructure
{
    public class InfrastructureSettings
    {
        public string Secret { get; set; } = default!;

        public DatabaseProvider DatabaseProvider { get; set; }
    }

    public enum DatabaseProvider
    {
        SqlLite,
        SqlServer,
        MySql
    }
}

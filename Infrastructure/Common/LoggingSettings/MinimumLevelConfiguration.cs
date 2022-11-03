namespace Infrastructure.Common.LoggingSettings
{
    public class MinimumLevelConfiguration
    {
        public string Default { get; set; } = default!;
        public SerilogOverrides Override { get; set; } = default!;
    }
}
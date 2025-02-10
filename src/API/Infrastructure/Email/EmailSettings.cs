namespace Infrastructure.Email
{
    public class EmailSettings
    {
        public string ConnectionString { get; set; } = default!;

        public string SenderEmail { get; set; } = default!;
    }
}

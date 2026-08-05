namespace Communication.Email.Configurations
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string SenderName { get; set; } = default!;
        public string From { get; set; } = default!;
    }
}

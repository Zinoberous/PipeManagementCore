namespace PipeServiceCore.Models
{
    public class Logging
    {
        public bool Console { get; set; }
        public FileLogging? File { get; set; }
        public HubLogging? Hub { get; set; }
        public MailLogging? Mail { get; set; }
        public TeamsLogging? Teams { get; set; }
    }

    public class FileLogging
    {
        public string? Workfolder { get; set; }
    }

    public class HubLogging
    {
        public string? LogLevel { get; set; }
        public Uri Url { get; set; }
    }

    public class MailLogging
    {
        public string? LogLevel { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string>? Cc { get; set; }
        public IEnumerable<string>? Bcc { get; set; }
        //public string Subject { get; set; }
        //public string Body { get; set; }
    }

    public class TeamsLogging
    {
        public string? LogLevel { get; set; }
        public Uri WebhookUrl { get; set; }
        //public string Title { get; set; }
        //public string Message { get; set; }
    }
}

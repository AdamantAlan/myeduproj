namespace MasstransitEdu.Messaging;

public class RabbitMqOptions
{
    public string Host { get; set; } = "localhost";

    public string VirtualHost { get; set; } = "/";

    public string Username { get; set; } = "guest";

    public string Password { get; set; } = "guest";

    public string DeadLetterExchange { get; set; } = "masstransit-edu.dlx";

    public string DeadLetterQueue { get; set; } = "masstransit-edu.dlq";
}

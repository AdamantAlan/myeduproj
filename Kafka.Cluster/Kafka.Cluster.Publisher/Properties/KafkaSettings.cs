namespace Kafka.Cluster.Publisher.Properties
{
    public class KafkaProducerDefinition
    {
        public string Topic { get; set; } = "";
        public string GroupId { get; set; } = "";
    }

    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = "";
        public List<KafkaProducerDefinition> Producers { get; set; } = new();
    }
}

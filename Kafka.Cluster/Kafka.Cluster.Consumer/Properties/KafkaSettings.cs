namespace Kafka.Cluster.Consumer.Properties
{
    public class KafkaConsumerDefinition
    {
        public string Topic { get; set; } = "";
        public string GroupId { get; set; } = "";
    }

    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = "";
        public List<KafkaConsumerDefinition> Consumers { get; set; } = new();
    }
}

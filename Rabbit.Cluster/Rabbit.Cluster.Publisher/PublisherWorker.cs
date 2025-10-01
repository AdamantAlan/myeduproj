using EasyNetQ;

//namespace Rabbit.Cluster.Publisher
namespace Rabbit.Contracts
{
    public class PublisherWorker(IBus bus) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int counter = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                var msg = new TestMessage(counter);
                await bus.PubSub.PublishAsync(msg, cfg => cfg.WithTopic("TestMessage"), stoppingToken);
                Console.WriteLine($"[Publisher] [✅] Sent: {msg.Text}");

                var msg1 = new TestMessage1(counter);
                await bus.PubSub.PublishAsync(msg1, cfg => cfg.WithTopic("TestMessage1"), cancellationToken: stoppingToken);
                Console.WriteLine($"[Publisher] [✅] Sent: {msg1.Text}");

                var msg2 = new TestMessage2(counter);
                await bus.PubSub.PublishAsync(msg2, cfg => cfg.WithTopic("TestMessage2"), cancellationToken: stoppingToken);
                Console.WriteLine($"[Publisher] [✅] Sent: {msg2.Text}");

                var msg3 = new TestMessage3(counter);
                await bus.PubSub.PublishAsync(msg3, cfg => cfg.WithTopic("TestMessage3"), cancellationToken: stoppingToken);
                Console.WriteLine($"[Publisher] [✅] Sent: {msg3.Text}");

                var msg4 = new TestMessage4(counter);
                await bus.PubSub.PublishAsync(msg4, cfg => cfg.WithTopic("TestMessage4"), cancellationToken: stoppingToken);
                Console.WriteLine($"[Publisher] [✅] Sent: {msg4.Text}");

                var msg5 = new TestMessage5(counter);
                await bus.PubSub.PublishAsync(msg5, cfg => cfg.WithTopic("TestMessage5"), cancellationToken: stoppingToken);
                Console.WriteLine($"[Publisher] [✅] Sent: {msg5.Text}");

                counter++;
                await Task.Delay(500, stoppingToken);
            }
        }
    }

    public class TestMessage
    {
        public TestMessage(int counter) => Text = $"TestMessage {counter}";

        public string Text { get; set; }
    }

    public class TestMessage1
    {
        public TestMessage1(int counter) => Text = $"TestMessage1 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage2
    {
        public TestMessage2(int counter) => Text = $"TestMessage2 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage3
    {
        public TestMessage3(int counter) => Text = $"TestMessage3 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage4
    {
        public TestMessage4(int counter) => Text = $"TestMessage4 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage5
    {
        public TestMessage5(int counter) => Text = $"TestMessage5 {counter}";

        public string Text { get; set; }
    }
}

public class SimpleTypeNameSerializer : ITypeNameSerializer
{
    public string Serialize(Type type)
    {
        return type.FullName ?? type.Name; // "Rabbit.Contracts.TestMessage"
    }

    public Type Deserialize(string typeName)
    {
        // тут нужен резолвинг типа, например через рефлексию из загруженных сборок
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .First(t => t.FullName == typeName);
    }
}

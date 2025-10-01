using EasyNetQ;

//namespace Rabbit.Cluster.Consumer
namespace Rabbit.Contracts
{
    public class ConsumerWorker(IBus bus) : BackgroundService
    {
        private readonly List<IDisposable> _subscriptions = [];

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptions.Add(bus.PubSub.Subscribe<TestMessage>(
                "consumer0", msg => Console.WriteLine($"[✅] {msg.Text}"),
                cfg => cfg.WithTopic("TestMessage")));
            _subscriptions.Add(bus.PubSub.Subscribe<TestMessage1>(
                "consumer1", msg => Console.WriteLine($"[✅] {msg.Text}"),
                cfg => cfg.WithTopic("TestMessage1")));
            _subscriptions.Add(bus.PubSub.Subscribe<TestMessage2>(
                "consumer2", msg => Console.WriteLine($"[✅] {msg.Text}"),
                cfg => cfg.WithTopic("TestMessage2")));
            _subscriptions.Add(bus.PubSub.Subscribe<TestMessage3>(
                "consumer3", msg => Console.WriteLine($"[✅] {msg.Text}"),
                cfg => cfg.WithTopic("TestMessage3")));
            _subscriptions.Add(bus.PubSub.Subscribe<TestMessage4>(
                "consumer4", msg => Console.WriteLine($"[✅] {msg.Text}"),
                cfg => cfg.WithTopic("TestMessage4")));
            _subscriptions.Add(bus.PubSub.Subscribe<TestMessage5>(
                "consumer5", msg => Console.WriteLine($"[✅] {msg.Text}"),
                cfg => cfg.WithTopic("TestMessage5")));

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var sub in _subscriptions)
                sub.Dispose();

            return base.StopAsync(cancellationToken);
        }
    }

    public class TestMessage
    {
        public TestMessage()
        {

        }
        public TestMessage(int counter) => Text = $"TestMessage {counter}";

        public string Text { get; set; }
    }

    public class TestMessage1
    {

        public TestMessage1()
        {

        }
        public TestMessage1(int counter) => Text = $"TestMessage1 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage2
    {

        public TestMessage2()
        {

        }
        public TestMessage2(int counter) => Text = $"TestMessage2 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage3
    {

        public TestMessage3()
        {

        }
        public TestMessage3(int counter) => Text = $"TestMessage3 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage4
    {
        public TestMessage4()
        {

        }
        public TestMessage4(int counter) => Text = $"TestMessage4 {counter}";

        public string Text { get; set; }
    }

    public class TestMessage5
    {
        public TestMessage5()
        {

        }
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
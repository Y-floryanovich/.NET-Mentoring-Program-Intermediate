using Confluent.Kafka;
using Message;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    public class Consumer
    {
        private const string Broker = "localhost:9092";
        private const string Topic = "kafka-image";
        private const string Group = "kafka-image_group";
        private IConsumer<Null, string> _consumer;

        public Consumer()
        {
            var _config = new ConsumerConfig()
            {
                GroupId = Group,
                BootstrapServers = Broker,
            };
            _consumer = new ConsumerBuilder<Null, string>(_config).Build();
            _consumer.Subscribe(Topic);
        }

        public async Task<MessageChunk> Start(CancellationToken cancellationToken)
        {
           var response = _consumer.Consume(cancellationToken);
           return response != null ? JsonConvert.DeserializeObject<MessageChunk>(response.Message.Value) : null;
        }

        public Task Stop(CancellationToken cancellationToken)
        {
            _consumer?.Dispose();
            return Task.CompletedTask;
        }
    }
}

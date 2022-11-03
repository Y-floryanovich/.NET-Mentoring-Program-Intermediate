using Confluent.Kafka;
using Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Message_queues
{
    public class Producer
    {
        private const string Broker = "localhost:9092";
        private const string Topic = "kafka-image";
        private IProducer<Null, string> _producer;

        public Producer()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = Broker
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task Start(CancellationToken cancellationToken, List<MessageChunk> messageChunks)
        {
            try
            {
                for (int i = 0; i < messageChunks.Count; i++)
                {
                    await _producer.ProduceAsync(Topic, new Message<Null, string>()
                    {
                        Value = JsonConvert.SerializeObject(messageChunks[i])
                    }, cancellationToken);
                }

                _producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine("Your message was send.");
            }
            catch(ProduceException<Null, string> exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public Task Stop(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}

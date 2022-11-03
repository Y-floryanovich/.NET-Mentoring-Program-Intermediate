using Common;
using Message;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Message_queues
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter the path:");
            var path = Console.ReadLine();
            while (!File.Exists(path))
            {
                Console.Write("File doesn't exist. Enter new path:");
                path = Console.ReadLine();
            }

            var source = new CancellationTokenSource();
            var producer = new Producer();
            using (var fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[fileStream.Length];
                fileStream.Read(bytes);

                var chunks = MessageService.GetMessageChunks(bytes);

                if (chunks != null)
                {
                    await producer.Start(source.Token, chunks);
                }
            }

            await producer.Stop(source.Token);
            source.Dispose();
            Console.ReadLine();
        }
    }
}

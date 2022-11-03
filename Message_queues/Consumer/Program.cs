using Common;
using Message;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    internal class Program
    {
        private const int Size = 8 * 1024;
        private const string FilePath = @"C:\Users";
        private const string FileName = @"ttt.pdf";
        private readonly static MessageService _messageService = new MessageService();

        static async Task Main(string[] args)
        {
            var source = new CancellationTokenSource();
            var consumer = new Consumer();
            byte[] result;
            while (true)
            {
                var messageChunk = await consumer.Start(source.Token);
                var message = new byte[Size];
                var isMessageWasGet = _messageService.TryGetMessage(messageChunk,out message);
                if(isMessageWasGet)
                {
                    var index = 0;
                    var filePath = $"{FilePath}\\{FileName}";
                    while (File.Exists(FilePath))
                    {
                        index++;
                        var newName = $"{Path.GetFileNameWithoutExtension(FilePath)}_{index}";
                        var extension = Path.GetExtension(FilePath);
                        filePath = $"{FilePath}\\{newName}{extension}";
                    }

                    FileService.WriteToFile(filePath, message);

                    Console.WriteLine($"File was saved.");
                }
            }

            await consumer.Stop(source.Token);
            source.Dispose();
        }
    }
}

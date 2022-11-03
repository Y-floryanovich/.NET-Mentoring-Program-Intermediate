using System.IO;
using System.Threading.Tasks;

namespace Message_queues
{
    public static class FileExtensions
    {
        public static async Task<byte[]> ToArrayAsync(this Stream stream)
        {
            var memoryStream = new MemoryStream();
            await memoryStream.CopyToAsync(stream);
            return memoryStream.ToArray();
        }
    }
}

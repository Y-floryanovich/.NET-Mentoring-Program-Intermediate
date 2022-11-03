using System.IO;

namespace Common
{
    public class FileService
    {
        public static void WriteToFile(string filePath, byte[] file)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fileStream.Write(file);
        }
    }
}

using System;
using System.IO;

namespace chat_server_test
{
    public sealed class TempFile : IDisposable
    {
        public TempFile(string extension) => Path = $"{Guid.NewGuid().ToString()}.{extension}";

        public string Path { get; }

        public void Dispose()
        {
            try
            {
                File.Delete(Path);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
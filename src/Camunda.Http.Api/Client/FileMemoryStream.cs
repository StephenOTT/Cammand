using System.IO;

namespace Camunda.Http.Api.Client
{
    public class FileMemoryStream: MemoryStream
    {
        public string FileName { get; set; }
        public string ContentName { get; set; }

        public FileMemoryStream()
        {
        }

        public FileMemoryStream(byte[] buffer, string fileName, string contentName) : base(buffer)
        {
            FileName = fileName;
            ContentName = contentName;
        }

        public FileMemoryStream(byte[] buffer, bool writable, string fileName, string contentName) : base(buffer, writable)
        {
            FileName = fileName;
            ContentName = contentName;
        }

        public FileMemoryStream(byte[] buffer, int index, int count, string fileName, string contentName) : base(buffer, index, count)
        {
            FileName = fileName;
            ContentName = contentName;
        }

        public FileMemoryStream(byte[] buffer, int index, int count, bool writable, string fileName, string contentName) : base(buffer, index, count, writable)
        {
            FileName = fileName;
            ContentName = contentName;
        }

        public FileMemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible, string fileName, string contentName) : base(buffer, index, count, writable, publiclyVisible)
        {
            FileName = fileName;
            ContentName = contentName;
        }

        public FileMemoryStream(int capacity, string fileName, string contentName) : base(capacity)
        {
            FileName = fileName;
            ContentName = contentName;
        }
        
    }
}
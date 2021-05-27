#nullable enable
using System.IO;

namespace Camunda.Http.Api.Client
{
    public class FileParameter
    {
        public Stream Content { get; set; }
        public string? Name { get; set; } = "data";
        public string? FileName { get; set; } = "no_file_name_provided";
        
        public FileParameter(Stream content, string? name, string? fileName)
        {
            Content = content;
            Name = name;
            FileName = fileName;
        }
    }
}
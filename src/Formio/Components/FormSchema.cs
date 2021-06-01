using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Formio.Components
{
    public class FormSchema
    { 
        [JsonPropertyName("display")]
        public string Display { get; set; }

        [JsonPropertyName("components")] 
        public List<object> Components { get; set; } = new();

    }
}
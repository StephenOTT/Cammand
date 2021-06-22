using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Formio.Components
{
    public class FormSubmission
    { 
        [JsonPropertyName("data")]
        public JsonDocument Data { get; set; }

        [JsonPropertyName("metadata")] 
        public JsonDocument Metadata { get; set; }
    }
}
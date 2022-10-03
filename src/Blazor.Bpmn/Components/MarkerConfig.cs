#nullable enable
using System.Collections.Generic;

namespace Blazor.Bpmn.Components
{
    public class MarkerConfig
    {
        public string? ElementId { get; set; }

        public string State { get; set; } = "INIT";
        
        public string? CssClass { get; set; }

        public IEnumerable<string>? Tags { get; set; }
        
        public string? MarkerId { get; set; }
        
    }
}
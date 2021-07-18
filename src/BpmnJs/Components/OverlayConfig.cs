#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BpmnJs.Components
{
    public class OverlayConfig
    {
        public string? ElementId { get; set; }

        public string State { get; set; } = "INIT";

        public int? PositionTop { get; set; } = null;
        public int? PositionBottom { get; set; } = null;
        public int? PositionLeft { get; set; } = null;
        public int? PositionRight { get; set; } = null;
        
        [JsonIgnore]
        public RenderFragment<BpmnElement>? OverlayRenderFragment { get; set; }
        
        public ElementReference? HtmlElementRef { get; set; }

        public IEnumerable<string>? Tags { get; set; }
        
        public string? OverlayId { get; set; }

        public static async Task RemoveOverlays(
            IEnumerable<string> overlayIds, 
            IJSObjectReference bpmnJsModule,
            IJSObjectReference bpmnJsInstance)
        {
            await bpmnJsModule.InvokeVoidAsync("removeOverlays", overlayIds, bpmnJsInstance);
        }
    }
}
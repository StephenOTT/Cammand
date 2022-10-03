#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace Blazor.Bpmn.Components
{
    public class OverlayConfig
    {
        public OverlayConfig(
            string? elementId, 
            RenderFragment<GenericElement>? overlayRenderFragment, 
            List<string> tags, 
            int? positionTop = null, 
            int? positionBottom = null, 
            int? positionLeft = null, 
            int? positionRight = null)
        {
            ElementId = elementId;
            PositionTop = positionTop;
            PositionBottom = positionBottom;
            PositionLeft = positionLeft;
            PositionRight = positionRight;
            OverlayRenderFragment = overlayRenderFragment;
            Tags = tags;
        }

        public OverlayConfig(string elementId, Func<GenericElement, OverlayConfig> overlayConfig)
        {
            ElementId = elementId;
            _overlayConfigFunc = overlayConfig;
        }

        [JsonIgnore]
        public Func<GenericElement, OverlayConfig>?_overlayConfigFunc = null;
        
        public string? ElementId { get;}

        public string State { get; set; } = "HIDE";

        public int? PositionTop { get; set; }
        public int? PositionBottom { get; set; }
        public int? PositionLeft { get; set; }
        public int? PositionRight { get; set; }

        [JsonIgnore] public RenderFragment<GenericElement>? OverlayRenderFragment { get; set; }

        public List<string> Tags { get; set; } = new();
        
        public ElementReference? HtmlElementRef { get; set; }

        public string? OverlayId { get; set; }

        public void SetupOverlayFunc(GenericElement bpmnElement)
        {
            if (_overlayConfigFunc != null)
            {
                var overlayConfig = _overlayConfigFunc.Invoke(bpmnElement);
                State = overlayConfig.State;
                PositionTop = overlayConfig.PositionTop;
                PositionBottom = overlayConfig.PositionBottom;
                PositionLeft = overlayConfig.PositionLeft;
                PositionRight = overlayConfig.PositionRight;
                Tags = overlayConfig.Tags;
                OverlayRenderFragment = overlayConfig.OverlayRenderFragment; 
            }
            else
            {
                throw new Exception("No Overlay Config Func was found");
            }
        }
        
    }
}
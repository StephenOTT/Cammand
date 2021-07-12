#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace BpmnJs.Components
{

    public abstract class BpmnElementInteropBase
    {
        internal IJSObjectReference BpmnJsInstance;
        internal IJSRuntime JsRuntime;
        internal string InternalId;
        internal string PathPrefix = "";
        
        public Dictionary<string, object>? Attributes => GetProp<Dictionary<string, object>>("$attrs");

        
        internal T? GetProp<T>(string path)
        {
            var rt = (IJSInProcessRuntime)JsRuntime;
            var inst = BpmnJsInstance;
            return rt.Invoke<T>("getBpmnElementProperty", inst, InternalId, PathPrefix + path);
        }
    }
    public class BpmnElement: BpmnElementInteropBase
    {
        public BpmnElement(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string elementId)
        {
            InternalId = elementId;
            BpmnJsInstance = bpmnJsInstance;
            JsRuntime = jsRuntime;
            PathPrefix = "";

            BusinessObject = new BusinessObject(bpmnJsInstance, jsRuntime, elementId);
        }

        public string Id => GetProp<string>("id")!;
        public string Type => GetProp<string>("type")!;
        
        public int X => GetProp<int>("x")!;
        public int Y => GetProp<int>("y")!;
        
        public string? Label => GetProp<string>("label")!;
        public List<string>? Labels => GetProp<List<string>>("labels")!;

        public List<BpmnElement>? Attachers
        {
            get
            {
                var count = GetProp<int>("attachers.length");
                var result = new List<BpmnElement>();
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        result.Add(new BpmnElement(BpmnJsInstance, JsRuntime, GetProp<string>($"attachers[{i.ToString()}].id")!));
                    }
                }

                return result;
            }
        }
        
        public BusinessObject BusinessObject { get; }
        
    }

    public class BusinessObject: BpmnElementInteropBase
    {
        public BusinessObject(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string elementId)
        {
            InternalId = elementId;
            BpmnJsInstance = bpmnJsInstance;
            JsRuntime = jsRuntime;
            PathPrefix = "businessObject.";
        }
        
        public string Id => GetProp<string>("id")!;
        public string Type => GetProp<string>("$type")!;
        public string? Name => GetProp<string>("name")!;
        public List<Documentation>? Documentation => GetProp<List<Documentation>>("documentation");
        
        public ExtensionElements? ExtensionElements => GetProp<ExtensionElements>("extensionElements");

    }
    
    public class Documentation
    {
        [JsonPropertyName("$type")]
        public string Type { get; set; }
        
        public string? Text { get; set; }
        
    }
    
    public class ExtensionElements
    {
        [JsonPropertyName("$type")]
        public string Type { get; set; }  //"bpmn:ExtensionElements"
        
        public List<ExtensionElement>? Values { get; set; }
    }
    
    public class ExtensionElement
    {
        [JsonPropertyName("$type")]
        public string Type { get; set; } //"camunda:properties"
        
        [JsonPropertyName("$children")]
        public List<ExtensionElementProperty>? Children { get; set; }
    }
    
    public class ExtensionElementProperty
    {
        [JsonPropertyName("$type")]
        public string Type { get; set; } //"camunda:property"
        
        public string Name { get; set; }
        
        public string? Value { get; set; } // Example: used for Camunda Extension props
        
        [JsonPropertyName("$body")]
        public string? Body { get; set; } // Example: used for Input/Output mappings
    }
    


}
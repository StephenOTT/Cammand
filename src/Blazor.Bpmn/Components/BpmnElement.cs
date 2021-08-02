#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.JSInterop;

namespace Blazor.Bpmn.Components
{
    public static class ElementExtensions
    {
        public static Documentation AsDocumentation(this GenericObject genericObject)
        {
            return new(genericObject.BpmnJsInstance, genericObject.JsRuntime, genericObject.InternalId,
                genericObject.PathPrefix);
        }

        public static UserTask AsUserTask(this IBusinessObject genericObject)
        {
            return new(genericObject.BpmnJsInstance, genericObject.JsRuntime, genericObject.InternalId,
                genericObject.PathPrefix);
        }
        
        public static ScriptTask AsScriptTask(this IBusinessObject genericObject)
        {
            return new(genericObject.BpmnJsInstance, genericObject.JsRuntime, genericObject.InternalId,
                genericObject.PathPrefix);
        }
        
        public static Participant AsParticipant(this IBusinessObject genericObject)
        {
            return new(genericObject.BpmnJsInstance, genericObject.JsRuntime, genericObject.InternalId,
                genericObject.PathPrefix);
        }
        
        public static Process AsProcess(this IBusinessObject genericObject)
        {
            return new(genericObject.BpmnJsInstance, genericObject.JsRuntime, genericObject.InternalId,
                genericObject.PathPrefix);
        }
        
        public static CommonGateway AsGateway(this IBusinessObject genericObject)
        {
            return new(genericObject.BpmnJsInstance, genericObject.JsRuntime, genericObject.InternalId,
                genericObject.PathPrefix);
        }
        
        public static SequenceFlow AsSequenceFlow(this IBusinessObject genericObject)
        {
            return new(genericObject.BpmnJsInstance, genericObject.JsRuntime, genericObject.InternalId,
                genericObject.PathPrefix);
        }

    }

    public interface ICore
    {
        public IJSObjectReference BpmnJsInstance { get; }
        public IJSRuntime JsRuntime { get; }
        public string InternalId { get; }
        public string PathPrefix { get; }
        public T GetProp<T>(string path);
        public bool HasProp(string path);
        public List<T> GetProps<T>(string listPath, string propertyPath);
        public GenericElement GetGenericElement(string path);
        public GenericObject GetGenericObject(string path);
        public GenericBusinessObject GetGenericBusinessObject(string path);
        public List<GenericObject>? GetGenericObjectList(string path);
        public List<GenericElement>? GetGenericElementList(string path);
    }

    public abstract class Core: ICore
    {
        protected Core(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix)
        {
            BpmnJsInstance = bpmnJsInstance;
            JsRuntime = jsRuntime;
            PathPrefix = pathPrefix;
            InternalId = internalId;
        }

        public IJSObjectReference BpmnJsInstance { get; }
        public IJSRuntime JsRuntime { get; }
        public string InternalId { get; }
        public string PathPrefix { get; }

        public T GetProp<T>(string path)
        {
            var rt = (IJSInProcessRuntime) JsRuntime;
            var returnedObject = rt.Invoke<T>("getBpmnElementProperty", BpmnJsInstance, InternalId, PathPrefix + path);
            return returnedObject;
        }
        
        public bool HasProp(string path)
        {
            var rt = (IJSInProcessRuntime) JsRuntime;
            var returnedObject = rt.Invoke<bool>("hasBpmnElementProperty", BpmnJsInstance, InternalId, PathPrefix + path);
            return returnedObject;
        }
        
        public List<T> GetProps<T>(string listPath, string propertyPath)
        {
            var rt = (IJSInProcessRuntime) JsRuntime;
            var returnedObject = rt.Invoke<List<T>>("getPropertyFromObjectArray", BpmnJsInstance, InternalId, PathPrefix + listPath, propertyPath);
            return returnedObject;
        }
        
        public GenericElement GetGenericElement(string path)
        {
            //@TODO add logic to check if prop exists
            return new(BpmnJsInstance, JsRuntime, InternalId, PathPrefix + path);
        }

        public GenericObject GetGenericObject(string path)
        {
            //@TODO add logic to check if prop exists
            return new(BpmnJsInstance, JsRuntime, InternalId, PathPrefix + path);
        }

        public GenericBusinessObject GetGenericBusinessObject(string path)
        {
            return new(BpmnJsInstance, JsRuntime, InternalId, PathPrefix + path);
        }

        public GenericBusinessObject GetBusinessGenericObject(string path)
        {
            //@TODO add logic to check if prop exists
            return new(BpmnJsInstance, JsRuntime, InternalId, PathPrefix + path);
        }

        public List<GenericObject>? GetGenericObjectList(string path)
        {
            //@TODO add logic to check if prop exists
            var count = GetProp<int?>(path + ".length");
            List<GenericObject>? list = null;
            if (count != null)
            {
                list = new List<GenericObject>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(new GenericObject(BpmnJsInstance, JsRuntime, InternalId, PathPrefix + path + $"[{i}]."));
                }
            }

            return list;
        }

        public List<GenericElement>? GetGenericElementList(string path)
        {
            //@TODO add logic to check if prop exists
            var count = GetProp<int?>(path + ".length");
            List<GenericElement>? list = null;
            if (count != null)
            {
                list = new List<GenericElement>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(new GenericElement(BpmnJsInstance, JsRuntime, InternalId, PathPrefix + path + $"[{i}]"));
                }
            }

            return list;
        }
        
        
    }

    public interface IGenericObject: ICore
    {
        public JsonDocument? Attributes { get; }

    }

    public class GenericObject : Core, IGenericObject
    {
        public GenericObject(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) 
            : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public JsonDocument? Attributes => GetProp<JsonDocument>("$attrs");
    }
    
    public interface IBusinessObject: IGenericObject
        {
            public const string BpmnProcess = "bpmn:Process";
            public const string BpmnParticipant = "bpmn:Participant";
            public const string BpmnSequenceFlow = "bpmn:SequenceFlow";
            public const string BpmnStartEvent = "bpmn:StartEvent";
            public const string BpmnEndEvent = "bpmn:EndEvent";
            public const string BpmnParallelGateway = "bpmn:ParallelGateway";
            public const string BpmnInclusiveGateway = "bpmn:InclusiveGateway";
            public const string BpmnExclusiveGateway = "bpmn:ExclusiveGateway";
            public const string BpmnEventBasedGateway = "bpmn:EventBasedGateway";
            public const string BpmnIntermediateThrowEvent = "bpmn:IntermediateThrowEvent";
            public const string BpmnBoundaryEvent = "bpmn:BoundaryEvent";
            
            public const string BpmnTask = "bpmn:Task";
            public const string BpmnManualTask = "bpmn:ManualTask";
            public const string BpmnUserTask = "bpmn:UserTask";
            public const string BpmnScriptTask = "bpmn:ScriptTask";
            public const string BpmnServiceTask = "bpmn:ServiceTask";
            public const string BpmnSendTask = "bpmn:BpmnSendTask";
            public const string BpmnReceiveTask = "bpmn:ReceiveTask";
            public const string BpmnBusinessRuleTask = "bpmn:BusinessRuleTask";
            public const string BpmnCallActivity = "bpmn:CallActivity";
            
            public const string BpmnSubProcess = "bpmn:SubProcess";

            public static readonly string[] FlowElements = {
                BpmnTask, BpmnManualTask, BpmnUserTask, BpmnScriptTask, 
                BpmnServiceTask, BpmnSendTask,BpmnReceiveTask,BpmnBusinessRuleTask,
                BpmnCallActivity, BpmnStartEvent, BpmnEndEvent, BpmnIntermediateThrowEvent, 
                BpmnBoundaryEvent, BpmnParallelGateway, BpmnExclusiveGateway, BpmnInclusiveGateway, 
                BpmnEventBasedGateway, BpmnSubProcess
            };

            public string Name { get; }

            public string Id { get; }

            public string Type { get; }

            public List<Documentation>? Documentation { get; }

            public bool HasDocumentation();

            public bool IsAsyncCapable();

            public bool HasExtensionElements();

            public bool HasCamundaExtensionProperties();

            public List<GenericObject>? GetCamundaExtensionProperties();

            public List<GenericObject>? ExtensionElements { get; }

            public bool IsAsyncBefore { get; }
            
            public bool IsAsyncAfter { get; }
        }

    
    public class GenericBusinessObject : Core, IBusinessObject
    {
        public GenericBusinessObject(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) 
            : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public JsonDocument? Attributes => GetProp<JsonDocument?>("$attrs");
        
        public string Name => GetProp<string>("name");
            
        public string Id => GetProp<string>("id");


        private string? _type;

        public string Type => _type ??= GetProp<string>("$type");

        public List<Documentation>? Documentation => 
            GetGenericObjectList("documentation")
                .Select(i=>i.AsDocumentation()).ToList();

        public bool HasDocumentation() => HasProp("documentation");
            
        public bool IsAsyncCapable()
        {
            return IBusinessObject.FlowElements.Contains(Type);
        }

        public bool HasExtensionElements()
        {
            return HasProp("extensionElements");
        }
            
        public bool HasCamundaExtensionProperties()
        {
            return ExtensionElements != null && ExtensionElements.Any(el => el.GetProp<string>("$type") == "camunda:properties");
        }

        public List<GenericObject>? GetCamundaExtensionProperties()
        {
            var position = ExtensionElements.FindIndex(el => el.GetProp<string>("$type") == "camunda:properties");
            return GetGenericObjectList($"extensionElements.values[{position}].$children");
        }

        public List<GenericObject>? ExtensionElements => GetGenericObjectList("extensionElements.values");
            

        public bool IsAsyncBefore
        {
            get
            {
                var prop = GetProp<string?>("$attrs.camunda:asyncBefore");
                return prop == "true";
            }
        }

        public bool IsAsyncAfter
        {
            get
            {
                var prop = GetProp<string?>("$attrs.camunda:asyncAfter");
                return prop == "true";
            }
        }
        
    }
    
    
    public interface CamundaExtensionProperty : IGenericObject
    {
        public string Name => GetProp<string>("name");
        public string Value => GetProp<string>("value");
    }
    
    public class Documentation : GenericObject
    {
        public Documentation(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public string Text => GetProp<string>("text");
    }
    
    
    public class UserTask : GenericBusinessObject
    {
        public UserTask(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public string Assignee => GetProp<string>("$attrs.camunda:assignee");
            
        public string CandidateGroups => GetProp<string>("$attrs.camunda:candidateGroups");
            
        public string CandidateUsers => GetProp<string>("$attrs.camunda:candidateUsers");
            
        public string DueDate => GetProp<string>("$attrs.camunda:dueDate");
            
        public string FollowUpDate => GetProp<string>("$attrs.camunda:followUpDate");
            
        public string Priority => GetProp<string>("$attrs.camunda:priority");

        public string? FormKey => GetProp<string?>("$attrs.camunda:formKey");
    }

    public class GenericElement : Core
    {
        public GenericElement(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        //@TODO consider a cache or perf enhancement for getting type as it would be common!
        // BUT would need to consider that in modeler the type could change during the modeling process!
        private string? _type;

        public string Type => _type ??= GetProp<string>("type");
        public string Id => GetProp<string>("id");
        public int X => GetProp<int>("x");
        public int Y => GetProp<int>("y");
        public int Width => GetProp<int>("width");
        public int Height => GetProp<int>("height");
        public string? Label => GetProp<string?>("label")!;
        public List<string>? Labels => GetProp<List<string>?>("labels");

        private IBusinessObject? _businessObject;
        public IBusinessObject BusinessObject => _businessObject ??= GetGenericBusinessObject("businessObject.");
        
    }
    
    public class Participant : GenericBusinessObject
    {
        public Participant(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public Process ProcessRef => new GenericBusinessObject(BpmnJsInstance, JsRuntime, InternalId, PathPrefix + "processRef.").AsProcess();
    }
    
    
    public class ScriptTask : GenericBusinessObject
    {
        public ScriptTask(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public string? ScriptFormat => GetProp<string?>("scriptFormat");
        public string? Script => GetProp<string?>("script");
        public string? ResultVariable => GetProp<string?>("$attrs.camunda:resultVariable");
        public string? Resource => GetProp<string?>("$attrs.camunda:resource");
        public bool IsInlineScript()
        {
            return HasProp("script");
        }
            
        public bool IsExternalScript()
        {
            return HasProp("$attrs.camunda:resource");
        }
    }

    public class ServiceTask : GenericBusinessObject
    {
        public ServiceTask(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public string? ServiceTaskType => GetProp<string?>("$attrs.camunda:type");
    }
        
    public class ExternalServiceTask : ServiceTask
    {
        public ExternalServiceTask(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
        {
        }

        public string? Topic => GetProp<string?>("$attrs.camunda:topic");
    }
    
    
    public class Process : GenericBusinessObject
        {
            public Process(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
            {
            }

            public bool IsExecutable => GetProp<bool>("isExecutable");
            public string? CandidateStarterGroups => GetProp<string?>("$attrs.camunda:candidateStarterGroups");
            public string? CandidateStarterUsers => GetProp<string?>("$attrs.camunda:candidateStarterUsers");
            public string? HistoryTimeToLive => GetProp<string?>("$attrs.camunda:historyTimeToLive");
            public string? JobPriority => GetProp<string?>("$attrs.camunda:jobPriority");
            public string? TaskPriority => GetProp<string?>("$attrs.camunda:taskPriority");
            public string? VersionTag => GetProp<string?>("$attrs.camunda:versionTag");

        }
        

    public class SequenceFlow : GenericBusinessObject
        {
            public SequenceFlow(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
            {
            }

            public string SourceId => GetProp<string>("sourceRef.id");
            public string TargetId => GetProp<string>("targetRef.id");

            public bool IsDefault()
            {
                var sourceDefaultId = GetProp<string?>("sourceRef.default.id");
                return sourceDefaultId == Id;
            }

            public bool HasCondition() => HasProp("conditionExpression");
            public string? ConditionType => GetProp<string?>("conditionExpression.$type");
            public string? ConditionBody => GetProp<string?>("conditionExpression.body");
            public string? ConditionLanguage => GetProp<string?>("conditionExpression.language");
            public string? ConditionXsiType => GetProp<string?>("conditionExpression.$attrs.xsi:type");
        }
        
    public class CommonGateway : GenericBusinessObject
        {
            public CommonGateway(IJSObjectReference bpmnJsInstance, IJSRuntime jsRuntime, string internalId, string pathPrefix) : base(bpmnJsInstance, jsRuntime, internalId, pathPrefix)
            {
            }

            public List<string> IncomingIds => GetProps<string>("incoming", "id");
            public List<string> OutgoingIds => GetProps<string>("outgoing", "id");
            public string? DefaultId => GetProp<string?>("default.id");
        }


}
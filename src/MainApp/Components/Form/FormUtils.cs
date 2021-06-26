using System;
using System.Collections.Generic;
using System.Text.Json;
using Camunda.Http.Api.Model;

namespace MainApp.Components.Form
{
    public static class FormUtils
    {
        
        public static VariableValueDto CreateVariableValue(JsonElement value, bool transient)
        {
            //@TODO REVIEW!

            var typeInfo = new Dictionary<string, object>();
            if (transient)
            {
                typeInfo["transient"] = true;
            }
            
            switch (value.ValueKind)
            {
                case JsonValueKind.Null:
                    return new VariableValueDto(null, "null", typeInfo);
                case JsonValueKind.String:
                    return new VariableValueDto(value.GetString(), "string", typeInfo);
                case JsonValueKind.Number:
                {
                    if (value.GetRawText().Contains("."))
                    {
                        return new VariableValueDto(value.GetDouble(), "double", typeInfo);
                    }

                    return new VariableValueDto(value.GetUInt64(), "long", typeInfo);
                }
                
                case JsonValueKind.True:
                    return new VariableValueDto(value.GetBoolean(), "boolean", typeInfo);
                case JsonValueKind.False:
                    return new VariableValueDto(value.GetBoolean(), "boolean", typeInfo);
                case JsonValueKind.Array:
                    return new VariableValueDto(JsonSerializer.Serialize(value), "json", typeInfo);
                case JsonValueKind.Object:
                    return new VariableValueDto(JsonSerializer.Serialize(value), "json", typeInfo);
                default:
                    throw new Exception("unknown json object type");     
                
                //@TODO Date support?
                //@TODO File support?
                //https://docs.camunda.org/manual/7.15/user-guide/process-engine/variables/#supported-variable-values
                
            }
        
        }
    }
}
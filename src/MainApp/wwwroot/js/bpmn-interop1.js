
// This has to be here because the Sync Downcasting does not work with ES modules
function getBpmnElementProperty(bpmnJsInstance, elementId, propertyPath){
    let element = bpmnJsInstance.get("elementRegistry").get(elementId)
    return _.get(element, propertyPath)
}
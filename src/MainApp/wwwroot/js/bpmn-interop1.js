
// This has to be here because the Sync Downcasting does not work with ES modules
function getBpmnElementProperty(bpmnJsInstance, elementId, propertyPath){
    let element = bpmnJsInstance.get("elementRegistry").get(elementId)
    console.log("Getting " + elementId + " Element path: " + propertyPath)
    return _.get(element, propertyPath)
}

function hasBpmnElementProperty(bpmnJsInstance, elementId, propertyPath){
    let element = bpmnJsInstance.get("elementRegistry").get(elementId)
    return _.has(element, propertyPath)
}

function getBpmnElementPropertyArraySize(bpmnJsInstance, elementId, propertyPath){
    let element = bpmnJsInstance.get("elementRegistry").get(elementId)
    let prop = _.get(element, propertyPath)
    return _.size(prop)
}

function getPropertyFromObjectArray(bpmnJsInstance, elementId, arrayPropPath, propertyPath){
    let elements = _.get(bpmnJsInstance.get("elementRegistry").get(elementId), arrayPropPath)
    return _.map(elements, propertyPath)
}

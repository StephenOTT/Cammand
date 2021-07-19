/// <reference path="./bpmn-navigated-viewer.production.min.js" />

import "./bpmn-navigated-viewer.production.min.js"
import "./lodash.min.js"

export function createBpmnJSInstance(element) {
    return new BpmnJS({
        container: element
    })
}

export function getElementProperty(bpmnJsInstance, elementId, propertyPath) {
    let element = bpmnJsInstance.get("elementRegistry").get(elementId)
    return _.get(element, propertyPath)
}

export function getAllElementPropertyIds(bpmnJsInstance) {
    return Object.getOwnPropertyNames(bpmnJsInstance.get("elementRegistry")._elements)
}

export function addOverlay(overlayConfig, bpmnJsInstance) {
    let overlays = bpmnJsInstance.get('overlays')
    //@TODO add issue to bpmnjs to ask for position allowing all 4 props and can provide null to ignore.

    let position = {}
    if (overlayConfig.positionTop != null) {
        position.top = overlayConfig.positionTop
    }
    if (overlayConfig.positionLeft != null) {
        position.left = overlayConfig.positionLeft
    }
    if (overlayConfig.positionBottom != null) {
        position.bottom = overlayConfig.positionBottom
    }
    if (overlayConfig.positionRight != null) {
        position.right = overlayConfig.positionRight
    }
    overlayConfig.htmlElementRef.style.display = ''

    let id = overlays.add(overlayConfig.elementId,
        {
            position: position,
            html: overlayConfig.htmlElementRef
        }
    )
    
    console.log("created overlay: " + id)
    
    return id
}

export function removeOverlays(overlayIds, bpmnJsInstance) {
    let overlays = bpmnJsInstance.get("overlays")
        overlayIds.forEach(id=>{
            console.log("removing " + id)
            overlays.remove(id)
        })
}


export function generateHeatmap(data, bpmnJsInstance, bpmnRef, heatmapInstance, maxValue) {
    let heatmapData = []

    let useCustomMaxValue = maxValue == null
    if (!useCustomMaxValue) {
        maxValue = 0
    }

    if (heatmapInstance == null) {
        heatmapInstance = h337.create({
            radius: 50,
            blur: .75,
            maxOpacity: .5,
            minOpacity: 0,
            container: document.querySelector(".djs-container")
        })
    } else {
        heatmapInstance.setData({data: []})
    }

    //@TODO come up with another solution so diagram can be moved around while heatmap is on
    // bpmnRef.style.pointerEvents = "none"

    let elementRegistry = bpmnJsInstance.get("elementRegistry")
    let viewbox = bpmnJsInstance.get("canvas").viewbox();

    data.forEach(i => {

        let elementKey = i.key
        let count = i.value
        let element = elementRegistry.get(elementKey)

        const shapeX = element.x * viewbox.scale - viewbox.x * viewbox.scale;
        const shapeY = element.y * viewbox.scale - viewbox.y * viewbox.scale;
        const shapeW = element.width * viewbox.scale;
        const shapeH = element.height * viewbox.scale;

        let radius = element.width / 2

        let xValue = Math.round(Math.abs(shapeX) + Math.floor(shapeW / 2))
        let yValue = Math.round(Math.abs(shapeY) + Math.floor(shapeH / 2))

        if (!useCustomMaxValue) {
            maxValue = Math.max(maxValue, count)
        }

        heatmapData.push({
            x: xValue,
            y: yValue,
            value: count,
            radius: radius
        })
    })

    heatmapInstance.setData({
        min: 0,
        max: maxValue,
        data: heatmapData
    });
    return heatmapInstance
}

export function destroyHeatmap(bpmnRef) {
    bpmnRef.querySelector(".heatmap-canvas").remove();
}

export function recenterDiagram(bpmnJsInstance) {
    bpmnJsInstance.get("canvas").zoom("fit-viewport", "auto")
    bpmnJsInstance.get("canvas").zoom("fit-viewport", "auto") //@TODO review why this is needed. Likely because the instance was not aware the viewport changed sizes.. Low priority
}

export function setupElementSelectionListener(bpmnJsInstance, dotNetRef) {
    console.log("creating listener for element selection")
    bpmnJsInstance.get("eventBus").on('selection.changed', function (context) {
        if (context.newSelection.length === 1) {
            //@TODO Refactor
            let elementId = context.newSelection[0].id
            dotNetRef.invokeMethodAsync('ElementSelectedEvent', elementId);
        } else if (context.newSelection.length === 0) {
            dotNetRef.invokeMethodAsync('ElementSelectedEvent', null);
        }
    });
}
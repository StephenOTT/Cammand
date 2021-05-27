/// <reference path="./bpmn-navigated-viewer.production.min.js" />

import "./bpmn-navigated-viewer.production.min.js"

export function createBpmnJSInstance(element) {
    return new BpmnJS({
        container: element
    });
}

export function getElement(element) {
    return document.getElementById(element);
}
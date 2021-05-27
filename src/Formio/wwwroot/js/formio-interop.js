import "./formio.full.min.js";

export function createForm(element, schema) {
    return Formio.createForm(element, schema)
}

export function setNoSubmit(form, value){
    form.nosubmit = value;
}

export function setDefaultData(form, data){
    form.submission = data;
}
//
// export function getElement(element) {
//     return document.getElementById(element);
// }
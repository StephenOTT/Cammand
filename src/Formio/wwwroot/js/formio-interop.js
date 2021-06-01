import "./formio.full.min.js";

Formio.icons = 'fontawesome'

export function createForm(element, schema, config, dotNetRef) {
    return Formio.createForm(element, schema, config).then(form =>{
        form.on("submit", submission => {
            dotNetRef.invokeMethodAsync('SubmitEvent', submission);
        })
        return form
    })
}

export function setNoSubmit(form, value){
    form.nosubmit = value;
}

export function emitFormioEvent(form, eventName, eventData){
    form.emit(eventName, eventData)
}

export function setDefaultData(form, data){
    form.submission = data;
}

export function changeSchemaDisplayMode(schema, newMode){
    if (schema == null || schema.display == null){
        return {display: newMode}
    } else {
        return schema.display = newMode
    }
}


//
// export function getElement(element) {
//     return document.getElementById(element);
// }

export function createBuilder(element, schema, config, dotNetRef) {
    return Formio.builder(element, schema, config).then(builder => {
        builder.on('saveComponent', evt => {
                dotNetRef.invokeMethodAsync('BuilderSaveComponentEvent', builder.schema);
        })

        builder.on('change', evt => {
            dotNetRef.invokeMethodAsync('BuilderChangeEvent', builder.schema);
        })
        return builder
    });
}

// export function saveFile(fileName, Content) {
//     var link = document.createElement('a');
//     link.download = fileName;
//     link.href = "data:application/json;charset=utf-8," + encodeURIComponent(Content)
//     document.body.appendChild(link);
//     link.click();
//     document.body.removeChild(link);
// }
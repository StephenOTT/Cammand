export function saveFile(fileName, Content) {
    let link = document.createElement('a');
    link.download = fileName;
    link.href = "data:application/json;charset=utf-8," + encodeURIComponent(Content)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
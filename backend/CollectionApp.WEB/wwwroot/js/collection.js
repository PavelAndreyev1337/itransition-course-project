FilePond.registerPlugin(
    FilePondPluginImagePreview,
    FilePondPluginImageExifOrientation,
    FilePondPluginFileValidateSize,
    FilePondPluginImageEdit,
    FilePondPluginFileValidateType
);

FilePond.create(
    document.querySelector('input[type="file"]')
);

let xhr = new XMLHttpRequest();

console.log(Cookies.get('collectionId'))

xhr.open('GET', `/Collections/${Cookies.get('collectionId')}/Images`);

xhr.send();

let files = []
xhr.onload = function () {
    if (xhr.status == 200) {
        for (const imageUri of JSON.parse(xhr.response)) {
            files.push({
                source: imageUri
            });
        }
        console.log(files)
    }
    FilePond.setOptions({
        files: files
    })
}

FilePond.setOptions({
    instantUpload: false,
    allowMultiple: true,
    storeAsFile: true,
    acceptedFileTypes: ['image/*',],
});

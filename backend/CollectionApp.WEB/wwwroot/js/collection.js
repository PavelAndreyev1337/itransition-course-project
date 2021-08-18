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

FilePond.setOptions({
    instantUpload: false,
    allowMultiple: true,
    storeAsFile: true,
    acceptedFileTypes: ['image/*',],
})

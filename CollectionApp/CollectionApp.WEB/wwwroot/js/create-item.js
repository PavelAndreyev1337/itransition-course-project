document.addEventListener('DOMContentLoaded', (event) => {
    let tagsInput = document.getElementById('tagsInput');
    let tagify = new Tagify(tagsInput, {
        whitelist: [],
        dropdown: {
            classname: "color-blue",
            enabled: 0,
            maxItems: 5,
            position: "text",
            closeOnSelect: false,
        }
    });
    const xhr = new XMLHttpRequest();
    tagify.on('input', event => {
        if (event.detail.value !== "") {
            xhr.open('GET', `/Tags?input=${event.detail.value}`);
            xhr.send();
            xhr.onload = () => {
                tagify.whitelist = [];
                if (xhr.status === 200) {
                    for (const tag of JSON.parse(xhr.response).entities) {
                        tagify.whitelist.push(tag.name);
                    }
                }
                console.log(tagify);
                tagify.dropdown.show();
            }
        }
    })
});

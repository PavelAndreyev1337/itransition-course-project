document.addEventListener('DOMContentLoaded', (event) => {
    Array.from(document.getElementsByClassName('delete-btn')).forEach(element => {
        element.addEventListener('click', event => {
            if (!window.confirm("Do you want to delete?")) {
                event.preventDefault();
                event.stopPropagation();
            }
        })
    });
});

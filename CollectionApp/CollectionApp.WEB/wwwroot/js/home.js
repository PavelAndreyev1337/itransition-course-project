document.addEventListener('DOMContentLoaded', (event) => {
    Array.from(document.getElementsByClassName('oauth-btn')).forEach(element => {
        element.addEventListener("click", (event) => {
            let modal = document.getElementById('modalContent');
            modal.innerHTML = '';
            let spinner = document.createElement('div');
            spinner.classList.add('spinner-border', 'mx-auto')
            modal.append(spinner);
        })
    })
});

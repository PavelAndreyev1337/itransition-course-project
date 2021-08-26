document.addEventListener('DOMContentLoaded', (event) => {
    const likesCount = document.getElementById('likesCount');
    document.getElementById('likeBtn').addEventListener('click', event => {
        var element = event.target;
        const xhr = new XMLHttpRequest();
        var formData = new FormData();
        formData.append('itemId', parseInt(element.getAttribute('data-item-id')));
        xhr.open('POST', '/Like');
        xhr.onload = () => {
            if (xhr.status === 200) {
                var resp = JSON.parse(xhr.response);
                likesCount.innerHTML = resp.count;
                if (resp.liked) {
                    element.classList.remove('btn-light');
                    element.classList.add('btn-danger');
                }
                else {
                    element.classList.remove('btn-danger');
                    element.classList.add('btn-light');
                }
            }
        }
        xhr.send(formData);
    });
});

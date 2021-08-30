document.addEventListener('DOMContentLoaded', (event) => {
    const likesCount = document.getElementById('likesCount');
    const likeBtn = document.getElementById('likeBtn');
    const itemId = likeBtn.getAttribute('data-item-id');
    likeBtn.addEventListener('click', event => {
        const xhr = new XMLHttpRequest();
        var formData = new FormData();
        formData.append('itemId', itemId);
        xhr.open('POST', '/Like');
        xhr.onload = () => {
            if (xhr.status === 200) {
                var resp = JSON.parse(xhr.response);
                likesCount.innerHTML = resp.count;
                if (resp.liked) {
                    likeBtn.classList.remove('btn-light');
                    likeBtn.classList.add('btn-danger');
                }
                else {
                    likeBtn.classList.remove('btn-danger');
                    likeBtn.classList.add('btn-light');
                }
            }
        }
        xhr.send(formData);
    });
    const commentBtn = document.getElementById('commentBtn');
    const commentInput = document.getElementById('commentInput');
    const container = document.getElementById('commentsContainer');
    const noExistMessages = document.getElementById('noExistMessages');
    const addMessageElement = (username, message) => {
        var div = document.createElement('div');
        div.classList.add(...['shadow-sm', 'p-3', 'my-1', 'bg-white', 'rounded']);
        var usernameElement = document.createElement('p');
        usernameElement.classList.add(...['my-1', 'text-primary']);
        usernameElement.innerText = username;
        div.append(usernameElement);
        commentElement = document.createElement('p');
        commentElement.classList.add('mb-0');
        commentElement.innerText = message;
        div.append(commentElement);
        container.prepend(div);
        if (noExistMessages) {
            noExistMessages.remove();
        }
    }
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/comments").build();
    hubConnection.on('Receive', (username, comment) => {
        addMessageElement(username, comment);
    });
    commentBtn.addEventListener('click', event => {
        addMessageElement(Cookies.get('username'), commentInput.value);
        hubConnection.invoke('Send', Cookies.get('itemId'), commentInput.value);
        commentInput.value = "";
    });
    hubConnection.start()
        .then(() => {
            hubConnection.invoke('JoinRoom', Cookies.get('itemId')).catch(err => console.log(err));
        }).catch(err => console.log(err));
});

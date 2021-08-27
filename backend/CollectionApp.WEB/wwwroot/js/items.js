document.addEventListener('DOMContentLoaded', (event) => {
    const urlSearchParams = new URLSearchParams(document.location.search.substring(1));
    var isLikedInput = document.getElementById('isLiked');
    isLikedInput.checked = urlSearchParams.get('isLiked')?.toLowerCase() === 'true';
    var isCommentedInput = document.getElementById('isCommented');
    isCommentedInput.checked = urlSearchParams.get('isCommented')?.toLowerCase() === 'true';
    var sortOrderInput = document.getElementById('sortOrder');
    sortOrderInput.value = urlSearchParams.get('sortOrder') ?? "Default";
    
    [isLikedInput, isCommentedInput, sortOrderInput].forEach(input => {
        input.addEventListener('change', event => {
            const element = event.target;
            if (element.type == 'checkbox') {
                element.value = element.checked;
            }
            urlSearchParams.set(element.name, element.value);
            urlSearchParams.set("page", 1);
            document.location.search = urlSearchParams.toString();
        });
    });
});

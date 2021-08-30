anychart.onDocumentReady(() => {
    var data = [];
    const xhr = new XMLHttpRequest();
    xhr.open('GET', 'Home/GetTagsCloud');
    xhr.send();
    xhr.onload = () => {
        if (xhr.status === 200) {
            for (var tag of JSON.parse(xhr.response)) {
                data.push({
                    x: tag.name,
                    value: parseInt(tag.count),
                    category: tag.name
                });
            }
            var chart = anychart.tagCloud(data);

            chart.angles([0])
            chart.container("container");
            chart.draw();

            chart.listen("pointClick", event => {
                window.open(`/Items?tag=` + event.point.get("x"), "_blank");
            });
        }
    };

});
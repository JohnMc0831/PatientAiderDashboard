// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    sortable('.topicList', {
        placeholderClass: "foo"
    });

    $(".saveTopicOrder").on("click", function(e) {
        e.preventDefault();
        console.log("inside .saveTopicOrder click event!");
        var result = sortable($("#" + $(this).data('listid')), 'serialize');
        console.log('returning serialized topicList for' + $(this).data("listid"));

        //construct string of ids to store in db...
        var topicOrder = "";
        $.each(result[0].items, function (index, item) {
            var $topicItem = $("<div>" + item.html.trim() + "</div>");
            var topicId = $topicItem.find('a').data('id');
            topicOrder += topicId + ",";
        });
        topicOrder = topicOrder.substring(0, topicOrder.length - 1);
        //TODO:  ajax call to persist new order to db.
        alert("New Topic Order: " + topicOrder + "");
    });
});

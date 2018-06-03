// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    sortable('.topicList', {
        placeholderClass: "foo"
    });

    $(".saveTopicOrder").on("click", function(e) {
        e.preventDefault();
        var currList = $(this).closest('.topicList');
        var sortedList = $(currList).sortable("widget");
        var sortedArray = sortedList.toArray();
    });
});

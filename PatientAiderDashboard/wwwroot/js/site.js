$(document).ready(function () {
    $('#topicInfo').qtip({
        content: {
            text: "Topics can be sorted by dragging and dropping them into the desired order, then clicking the <strong>Save Topic Order</strong> button.",
            title: "How to sort topics"
        },
        style: {
            classes: "qtip-blue qtip-rounded qtip-shadow qtip-bootstrap"
        }
    });

    $('.saveTopicOrder').qtip({
        style: {
            classes: "qtip-rounded qtip-shadow qtip-bootstrap"
        }
    });

    $('.addRemoveTopics').qtip({
        style: {
            classes: "qtip-rounded qtip-shadow qtip-bootstrap"
        }
    });

    sortable('.topicList');

    $(".saveTopicOrder").on("click", function(e) {
        e.preventDefault();
        console.log("inside .saveTopicOrder click event!");
        var list = $('#' + $(this).data('listid'));
        var sectionId = $(this).data('sectionid');
        var encId = $(this).data('encounterid');
        persistNewTopicOrder(list, sectionId, encId, "saveOrder");
        $(list).removeClass("dirty");
    });

    function saveTopicOrderToString(list) {
        var result = sortable(list, 'serialize');
        console.log('returning serialized topicList for ' + $(this).data("listid"));

        //construct string of ids to store in db...
        var topicOrder = "";
        $.each(result[0].items, function (index, item) {
            var $topicItem = $("<div>" + item.html.trim() + "</div>");
            var topicId = $topicItem.find('a').data('id');
            topicOrder += topicId + ",";
        });
        topicOrder = topicOrder.substring(0, topicOrder.length - 1);
        return topicOrder;
    }

    function persistNewTopicOrder(list, sectionId, encounterId, updateType) {
        var topicOrder = saveTopicOrderToString(list);
        //ajax call to persist new order to db.
        $.ajax({
            type: "POST",
            async: false,
            cache: false,
            url: "/Home/UpdateTopicsForSection/?sectionId=" + sectionId+ "&encounterId=" + encounterId + "&topics=" + topicOrder,
            dataType: "json",
            complete: function (mselect) {
                var r = mselect.responseJSON;
                if (updateType !== "saveOrder") {
                    $("#addRemoveTopicsListbox").val('').trigger('chosen:updated');
                    $.each(r,
                        function(index, item) {
                            if (item.selected) {
                                $('#addRemoveTopicsListbox').append("<option selected value='" +
                                    item.value +
                                    "'>" +
                                    item.text +
                                    "</option>");
                            } else {
                                $('#addRemoveTopicsListbox')
                                    .append("<option value='" + item.value + "'>" + item.text + "</option>");
                            }
                        });
                    $("#addRemoveTopicsListbox").trigger('chosen:updated');
                    $('#addRemoveTopicsListbox').chosen({
                        disable_search_threshold: 10,
                        no_results_text: "Oops, no topics found!",
                        width: "100%",
                        placeholder_text_multiple: "Add/Remove Topics..."
                    });
                    $('#sectionId').val(sectionId);
                    $('#encounterId').val(encounterId);
                    $('#addRemoveTopicsDialog').modal('show');
                }
            }
        });
    }

    //sortable('.topicList')[0].addEventListener('sortupdate', function (e) {
    //    console.log(e.detail);
    //    var list = $('#' + $(this).prop('id'));
    //    var sectionId = $(this).data('sectionid');
    //    var encId = $(this).data('encounterid');
    //    //persist new topicOrder to db.
    //    persistNewTopicOrder(list, sectionId, encId);
    //});

    $('.topicList').on("drop", function (event) {
        event.preventDefault();
        event.stopPropagation();
        $(this).addClass('dirty');
        //var saveButton = $('#' + $(this).prop('id').replace('topicList', 'saveTopicOrderButton'));
        //saveButton.addGlow();
    });

    $('.addRemoveTopics').on('click', function () {
        var sectId = $(this).data('sectionid');
        var encId = $(this).data('encounterid');
        $.ajax({
            type: "GET",
            async: false,
            cache: false,
            url: "/Home/GetTopicsForAddRemove/?sectionId=" + $(this).data('sectionid')+ "&encounterId=" + $(this).data('encounterid'),
            dataType: "json",
            complete: function (mselect) {
                var r = mselect.responseJSON;
                $("#addRemoveTopicsListbox").val('').trigger('chosen:updated');
                $.each(r, function (index, item) {
                    if (item.selected) {
                        $('#addRemoveTopicsListbox').append("<option selected value='" + item.value + "'>" + item.text + "</option>");
                    } else {
                        $('#addRemoveTopicsListbox').append("<option value='" + item.value + "'>" + item.text + "</option>");
                    }
                });
                $("#addRemoveTopicsListbox").trigger('chosen:updated');
                $('#addRemoveTopicsListbox').chosen({
                    disable_search_threshold: 10,
                    no_results_text: "Oops, no topics found!",
                    width: "100%",
                    placeholder_text_multiple: "Add/Remove Topics..."
                });
                $('#sectionId').val(sectId);
                $('#encounterId').val(encId);
                $('#addRemoveTopicsDialog').modal('show');
            }
        });
    });

    $("#btnSaveTopics").on("click", function(e) {
        e.preventDefault();
        var selectedTopics = $('#addRemoveTopicsListbox').chosen().val();
        var topicOrder = "";

        $.each(selectedTopics,
            function(index, item) {
                topicOrder += item + ",";
            });

        topicOrder = topicOrder.substr(0, topicOrder.length - 1);

        $.ajax({
            type: "POST",
            async: false,
            cache: false,
            data: { "topics": selectedTopics },
            url: "/Home/UpdateTopicsForSection/?sectionId=" +
                $('#sectionId').val() +
                "&encounterId=" +
                $('#encounterId').val() +
                "&topics=" +
                topicOrder,
            dataType: "json",
            complete: function(model) {
                var listName = '#' + model.responseJSON.SectionTopicListName;
                $(listName).empty();
                $.each(model.responseJSON.Topics,
                    function(index, item) {
                        $(listName).append('<li>' +
                            '<a asp-controller="Home" data-id="' +
                            item.Id +
                            '" title="' +
                            item.Title +
                            '" - "' +
                            item.Summary +
                            '" ' +
                            'asp-action="EditTopic" asp-route-id="' +
                            item.Id +
                            '">' +
                            item.Title +
                            '</a></li>');
                    });
                $(model.responseJSON.SectionTopicListName).trigger('chosen:updated');
                sortable('.topicList');
                $("#addRemoveTopicsDialog").modal("hide");
            }
        });
    });
});

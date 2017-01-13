if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof (TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.DeleteEventForm = (function () {
    var _config;

    return {
        init: function (config) {
            _config = config;

            $('#delete-event-form').ajaxSubmitWebApi({
                onBeforeSubmit: function (e) {
                    $('#DeleteModal .modal-content').mask('Deleting Event.. ');
                },
                onSuccess: function (response) {
                    if (response.Success == true) {
                        $('#DeleteModal').modal('hide');
                        $('#DeleteModal .model-content').unmask();
                        //alert(response.Message);
                        $('#event-grid').jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');

                        JQueryUX.Msg.SlideIn({
                            width: 200,
                            height: 50,
                            msg: response.Message,
                            dockAt: 'tc',
                            wrapperWidthOffset: 8,
                            wrapperHeightOffset: 8
                        });
                    }
                    else {
                        alert(response.Message);
                    }
                },
                onError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $('#new-event-btn').click(function () {
                TestAndLearn.Home.EventForm.newEvent();
            });
        }

        , deleteEvent: function (id) { 
            $('#DeleteModal').modal('show');
            $('#delete-event-EventId').val(id);
        }
    };
})();
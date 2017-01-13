﻿if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof (TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.DeleteTestForm
 = (function () {
    var _config;

    return {
        init: function (config) {
            _config = config;

            $('#delete-test-form').ajaxSubmitWebApi({
                onBeforeSubmit: function (e) {
                    $('#DeleteModal .modal-content').mask('Deleting Test.. ');
                },
                onSuccess: function (response) {
                    if (response.Success == true) {
                        $('#DeleteTestModal').modal('hide');
                        $('#DeleteTestModal .model-content').unmask();
                        //alert(response.Message);
                        $('#submitted-test-grid').jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');

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
        }
        , deleteTest: function (id) {
            $('#DeleteTestModal').modal('show');
            $('#delete-test-TestId').val(id);
        }
        };

})();
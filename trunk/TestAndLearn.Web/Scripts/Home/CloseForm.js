﻿if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof (TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.CloseForm = (function () {
    var _config;

    return {
        init: function (config) {
            _config = config;

            $('#close-test-form').ajaxSubmitWebApi({
                onBeforeSubmit: function (e) {
                    $('#close-test-modal .model-content').mask('Closing Test...');
                },
                onSuccess: function (response) {
                    $('#save-test-modal').modal('hide');
                    $('#save-test-modal .model-content').unmask();

                    if (response.Success == true) {
                        $('#close-test-modal').modal('hide');
                        $('#close-test-modal .model-content').unmask();
                        TestAndLearn.Home.Grids.reloadAll();

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

        , closeTest: function (testId) {
            $.ajax({
                url: _config.getTestUrl,
                dataType: 'json',
                type: 'GET',
                data: {
                    id: testId
                },
                success: function (response) {
                    var test = response;

                    $('#close-test-modal').modal('show');
                    document.getElementById('close-test-form').reset();

                    var testName = document.getElementById("input-closeTestName");
                    testName.innerHTML = test.Name;

                    var pre = '#input-close-test-';
                    $(pre + 'testid').val(test.TestId);
                    $(pre + 'outcome').val(test.OutCome);
                    $(pre + 'recommendation').val(test.Recommendation);

                },
                error: function (xhr, status, error) {
                    alert(error);
                }
            });
        }
    };
})();
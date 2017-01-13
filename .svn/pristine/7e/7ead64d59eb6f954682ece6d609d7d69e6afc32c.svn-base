if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof(TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.Forms = (function () {
    var _config;

    return {
        init: function (config) {
            _config = config;

            this.initTestForm();

            this.initCloseForm();

            $('#new-test-btn').click(function () {
                TestAndLearn.Home.Forms.newTest();
            });
            this.initRankForm();
        }

        ,initTestForm: function () {
            $('#save-test-form').submit(function (event) {
                $('#save-test-modal .model-content').mask('Saving Test...');
                event.preventDefault();

                var data = {};
                $(this).serializeArray().map(function (x) {
                    if (x.name.indexOf('[]') > -1) {
                        var arr = x.name.split('[]');
                        var name = arr[0];
                        var propertyName = arr[1].replace('.', '');

                        if (typeof (data[name]) == 'undefined') {
                            data[name] = [];
                        }

                        var obj = {};
                        obj[propertyName] = x.value;
                        data[name].push(obj);
                    }
                    else {
                        data[x.name] = x.value;
                    }
                });
                
                $.ajax({
                    url: $(this).attr('action'),
                    type: 'POST',
                    dataType: 'json',
                    data: $.toJSON(data),
                    contentType: 'application/json',
                    success: function (response)
                    {
                        if (response.Success == true) {
                            $('#save-test-modal').modal('hide');
                            $('#save-test-modal .model-content').unmask();
                            $('#submitted-test-grid').trigger('reloadGrid');
                            $('#approved-test-grid').trigger('reloadGrid');
                            $('#closed-test-grid').trigger('reloadGrid');

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
                    error: function (xhr, status, error) {
                        alert(error);
                    }
                });
            });
        }

        ,initCloseForm: function () {
            $('#close-test-form').submit(function (event) {
                $('#close-test-modal .model-content').mask('Closing Test...');
                event.preventDefault();

                var data = {};
                $(this).serializeArray().map(function (x) {
                    if (x.name.indexOf('[]') > -1) {
                        var arr = x.name.split('[]');
                        var name = arr[0];
                        var propertyName = arr[1].replace('.', '');

                        if (typeof (data[name]) == 'undefined') {
                            data[name] = [];
                        }

                        var obj = {};
                        obj[propertyName] = x.value;
                        data[name].push(obj);
                    }
                    else {
                        data[x.name] = x.value;
                    }
                });
                
                $.ajax({
                    url: $(this).attr('action'),
                    type: 'POST',
                    dataType: 'json',
                    data: $.toJSON(data),
                    contentType: 'application/json',
                    success: function (response)
                    {
                        if (response.Success == true) {
                            $('#close-test-modal').modal('hide');
                            $('#close-test-modal .model-content').unmask();
                            $('#approved-test-grid').trigger('reloadGrid');

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
                    error: function (xhr, status, error) {
                        alert(error);
                    }
                });
            });
        }

        ,newTest: function () {
            $('#save-test-modal').modal('show');
            document.getElementById('save-test-form').reset();
            var pre = '#input-test-';
            $(pre + 'clientid').val(_config.clientId);
            $(pre + 'testid').val(0);
        }

        ,editTest: function (testId) {
            $.ajax({
                url: _config.getTestUrl,
                dataType: 'json',
                type: 'GET',
                data: {
                    id: testId
                },
                success: function (response) {
                    var test = response;

                    $('#save-test-modal').modal('show');
                    document.getElementById('save-test-form').reset();

                    var pre = '#input-test-';
                    $(pre + 'clientid').val(test.ClientId);
                    $(pre + 'testid').val(test.TestId);
                    $(pre + 'name').val(test.Name);
                    $(pre + 'hypothesis').val(test.Hypothesis);
                    $(pre + 'additionalnotes').val(test.AdditionalNotes);
                    $(pre + 'expectedbusinessvalue').val(test.ExpectedBusinessValue);
                    $(pre + 'submittedby').val(test.SubmittedBy);
                    $(pre + 'quarter').val(test.Quarter);
                    $(pre + 'year').val(test.Year);

                    //test types
                    for (var i = 0; i < test.TestTypes.length; i++) {
                        $(pre + 'testtype-' + test.TestTypes[i].TestTypeId).attr('checked', 'checked');
                    }

                    //success metrics
                    for (var i = 0; i < test.SuccessMetrics.length; i++) {
                        $(pre + 'successmetric-' + test.SuccessMetrics[i].SuccessMetricId).attr('checked', 'checked');
                    }
                },
                error: function (xhr, status, error) {
                    alert(error);
                }
            });
        }
    };
})();
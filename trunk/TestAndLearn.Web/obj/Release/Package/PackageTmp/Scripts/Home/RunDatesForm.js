﻿if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof (TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.RunDatesForm = (function () {
    var _config;

    return {
        init: function (config) {
            _config = config;

            $('#run-dates-test-form').ajaxSubmitWebApi({
                onBeforeSubmit: function (e) {
                    $('#run-dates-test-modal .model-content').mask('Submitting Dates...');
                },
                onSuccess: function (response) {
                    $('#run-dates-test-modal').modal('hide');
                    $('#run-dates-test-modal .model-content').unmask();

                    if (response.Success == true) {
                        $('#calendar').fullCalendar('refetchEvents');
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

       , submitRunDates: function (testId) {
           $.ajax({
               url: _config.getTestUrl,
               dataType: 'json',
               type: 'GET',
               data: {
                   id: testId
               },
               success: function (response) {
                   var test = response;

                   $('#run-dates-test-modal').modal('show');
                   document.getElementById('run-dates-test-form').reset();

                   var testName = document.getElementById("input-testName");
                   testName.innerHTML = test.Name;

                   var pre = '#input-run-dates-test-';
                   $(pre + 'testid').val(test.TestId);

                   $(pre + 'startdate').val(test.StartDate);
                   $(pre + 'startdate').datepicker('update');

                   $(pre + 'enddate').val(test.EndDate);
                   $(pre + 'enddate').datepicker('update');
               },
               error: function (xhr, status, error) {
                   alert(error);
               }
           });
       }
    };
})();
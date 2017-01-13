if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof (TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.TestUrlForm = (function () {
    var _config;

    return {
        init: function (config) {
            _config = config;

            $('#test-url-form').ajaxSubmitWebApi({
                onBeforeSubmit: function (e) {
                    $('#sav .model-content').mask('Saving Test Urls...');
                },
                onSuccess: function (response) {
                    $('#TestUrlModal').modal('hide');
                    $('#TestUrlModal .model-content').unmask();

                    if (response.Success == true) {
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

              , addUrl: function (id) {
                  
                  $('#TestUrlModal').modal('show');
                  $('#test-event-TestId').val(id);

                  $.ajax({
                      url: _config.getUrls,
                      datatype: 'json',
                      type: 'GET',
                      data: {
                          TestId: id
                      },
                      success: function (response) {
                          var testUrlModel = response;
                          $('#TestUrlModal').modal('show');
                          document.getElementById('test-url-form').reset();

                          var element = document.getElementById("testUrlName");
                          element.innerHTML = testUrlModel.TestName;

                          $('#test-event-TestId').val(id);

                          if(typeof testUrlModel.Url1!=null)
                              $('#input-url-name-1').val(testUrlModel.Url1);
                          if(typeof testUrlModel.Url2!=null)
                              $('#input-url-name-2').val(testUrlModel.Url2);
                          if(typeof testUrlModel.Url3!=null)
                              $('#input-url-name-3').val(testUrlModel.Url3);
                          if(typeof testUrlModel.Url4!=null)
                              $('#input-url-name-4').val(testUrlModel.Url4);
                          if(typeof testUrlModel.Url5!=null)
                              $('#input-url-name-5').val(testUrlModel.Url5);
                          if (typeof testUrlModel.Url6 != null)
                              $('#input-url-name-6').val(testUrlModel.Url6);

                      },
                      error: function (xhr, status, error) {
                          alert(error);
                      }

                  });

              }

    };


})();
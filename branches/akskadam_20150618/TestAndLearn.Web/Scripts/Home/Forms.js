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
        },

        initTestForm: function () {
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
                            //$('#user-grid').trigger('reloadGrid');
                            alert(response.Message);
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
    };
})();
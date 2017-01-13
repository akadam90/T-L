if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof(TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.Forms = (function () {
    var _config;

    var _reloadAllGrids = function () {
        $('#submitted-test-grid').jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
        $('#approved-test-grid').jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
        $('#closed-test-grid').jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
        $('#event-grid').jqGrid('setGridParam', {datatype:'json'}).trigger('reloadGrid');
    }

    var _mapFormData = function (form) {
        var data = {};

        $(form).serializeArray().map(function (x) {
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
            else if (x.name.indexOf('.') > -1) {
                var arr = x.name.split('.');
                var name = arr[0];
                var propertyName = arr[1].replace('.', '');

                data[name] = {};
                data[name][propertyName] = x.value;
            }
            else {
                data[x.name] = x.value;
            }
        });

        return data;
    }

    return {
        init: function (config) {
            _config = config;
        }

        , deleteEvent: function (id) { 
            $('#DeleteModal').modal('show');
            $('#delete-event-EventId').val(id);

        }

        
    };
})();
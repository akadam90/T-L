﻿$(function () {
    $.fn.ajaxSubmitWebApi = function (options) {
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

        $(this).submit(function (event) {
            if (typeof (options.onBeforeSubmit) == 'function') {
                options.onBeforeSubmit(event);
            }

            event.preventDefault();

            var data = _mapFormData(this);

            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                dataType: 'json',
                data: $.toJSON(data),
                contentType: 'application/json',
                success: function (response) {
                    if (typeof (options.onSuccess) == 'function') {
                        options.onSuccess(response);
                    }
                },
                error: function (xhr, status, error) {
                    if (typeof (options.onError) == 'function') {
                        options.onError(xhr, status, error);
                    }
                }
            });
        });
    }
});
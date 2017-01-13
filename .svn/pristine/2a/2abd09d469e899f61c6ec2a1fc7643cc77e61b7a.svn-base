if (typeof (Gadget) == 'undefined') {
    var Gadget = {};
}

Gadget.MainNavigation = (function () {
    var _config;
    var _active;

    return {
        init: function (config) {
            _config = config;
            _active = _config.active;

            $('#' + _config.dropDownToggleId).html(_config.dropDownToggleValue + '<span class="caret"></span>');

            $('.' + _config.contentClass).css('display', 'none');
            $('#' + _config.content[_active]).css('display', 'block');
            $('#' + _active).parent().addClass('active');

            $('.' + _config.linkClass).unbind('click');
            $('.' + _config.linkClass).click(function () {
                $('#' + _config.content[_active]).css('display', 'none');
                $('#' + _active).parent().removeClass('active');

                _active = $(this).attr('id');

                $('#' + _config.content[_active]).css('display', 'block');
                $('#' + _active).parent().addClass('active');
            });
        }
    };
})();
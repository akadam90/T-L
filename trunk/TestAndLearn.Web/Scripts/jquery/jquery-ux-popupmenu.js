JQueryUX.PopUpMenu = (function () {
    var menuDiv = null;
    var _itemSelected = false;
    var _menuInitialized = false;
    var _menuJqElementOpener = null;

    $(document).ready(function () {
        $(document.body).click(function () {
            if (_menuInitialized == false || _itemSelected == true) {
                if (menuDiv != null) {
                    document.body.removeChild(menuDiv);
                    menuDiv = null;
                }

                if (_menuJqElementOpener != null) {
                    $(_menuJqElementOpener).removeClass('pop-up-menu-opener');
                    $(_menuJqElementOpener).removeClass('pop-up-menu-opener-long');
                    _menuJqElementOpener = null;
                }
            }

            _menuInitialized = false;
        });
    });

    return {
        show: function (clickEvent, config) {
            _itemSelected = false;
            _menuInitialized = true;

            if (menuDiv != null) {
                document.body.removeChild(menuDiv);
                menuDiv = null;
            }

            if (_menuJqElementOpener != null) {
                $(_menuJqElementOpener).removeClass('pop-up-menu-opener');
                $(_menuJqElementOpener).removeClass('pop-up-menu-opener-long');
                _menuJqElementOpener = null;
            }

            menuDiv = document.createElement('div');
            menuDiv.setAttribute('class', 'pop-up-menu');

            if (config.isGroupMenu == true) {
                var dl = document.createElement('dl');

                for (var i = 0; i < config.items.length; i++) {
                    var item = config.items[i];

                    var dt = document.createElement('dt');
                    dt.innerHTML = item.groupName;
                    dl.appendChild(dt);

                    for (var j = 0; j < item.items.length; j++) {
                        var groupItem = item.items[j];

                        var dd = document.createElement('dd');
                        dd.setAttribute('class', 'pop-up-menu-item ' + groupItem.className);
                        dd.setAttribute('id', config.menuName + '-item-' + i + '-' + j);
                        var a = document.createElement('a');
                        a.setAttribute('href', 'javascript:void(0)');
                        a.innerHTML = groupItem.name;

                        dd.appendChild(a);
                        dl.appendChild(dd);
                    }
                }

                menuDiv.appendChild(dl);
            }
            else {

                var ul = document.createElement('ul');
                for (var i = 0; i < config.items.length; i++) {
                    var item = config.items[i];

                    var li = document.createElement('li');
                    li.setAttribute('class', 'pop-up-menu-item ' + item.className);
                    li.setAttribute('id', config.menuName + '-item-' + i);
                    var a = document.createElement('a');
                    a.setAttribute('href', 'javascript:void(0)');
                    a.innerHTML = item.name;
                    li.appendChild(a);
                    ul.appendChild(li);
                }

                menuDiv.appendChild(ul);
            }

            document.body.appendChild(menuDiv);

            if (typeof (config.jqElementClickedOn) != 'undefined') {
                $(config.jqElementClickedOn).addClass('pop-up-menu-opener');

                var widthOfOpener = $(config.jqElementClickedOn).width();
                var popUpMenuWidth = $(menuDiv).width();

                if (widthOfOpener > popUpMenuWidth) {
                    $(config.jqElementClickedOn).addClass('pop-up-menu-opener-long');
                    $(menuDiv).addClass('pop-up-menu-short');
                }

                var position = $(config.jqElementClickedOn).offset();
                var height = $(config.jqElementClickedOn).get(0).offsetHeight;
                menuDiv.style.top = (position.top + height - 2) + 'px';
                menuDiv.style.left = (position.left) + 'px';

                _menuJqElementOpener = config.jqElementClickedOn;
            }
            else {
                menuDiv.style.top = clickEvent.pageY + 'px';
                menuDiv.style.left = (clickEvent.pageX - currentWidth) + 'px';
            }

            if (config.isGroupMenu == true) {
                for (var i = 0; i < config.items.length; i++) {
                    for (var j = 0; j < config.items[i].items.length; j++) {
                        $('#' + config.menuName + '-item-' + i + '-' + j).click(config.items[i].items[j], function (event) {
                            _itemSelected = true;

                            if (typeof (event.data.onClick) == 'function') {
                                event.data.onClick(event.data);
                            }
                        });
                    }
                }
            }
            else {
                for (var i = 0; i < config.items.length; i++) {
                    $('#' + config.menuName + '-item-' + i).click(config.items[i], function (event) {
                        _itemSelected = true;

                        if (typeof (event.data.onClick) == 'function') {
                            event.data.onClick(event.data);
                        }
                    });
                }
            }
        }
    }
})();
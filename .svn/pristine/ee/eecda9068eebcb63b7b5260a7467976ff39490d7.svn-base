if (typeof(JQueryUX) == 'undefined') {
	var JQueryUX = {};
}
/*
* A singleton that contains various methods that will create some
* type of dynamic message box
*/
JQueryUX.Msg = (function () {
    return {
        /*
        * Creates a basic jquery modal dialog box
        * a div is auto created and used for this purpose
        * no div needs to exist on the page
        * the auto created div is destroyed when the modal is closed.
        */
        /*
        * Config options
        *  width - the width of the message box
        *  height - the height of the message box
        *  title - the title of the message box
        *  msg - the message of the message box
        */
        Show: function (config) {
            var div = document.createElement('div');
            div.innerHTML = '<P>' + config.msg + '</p>';
            document.body.appendChild(div);

            var dialogClass = '';
            if (config.msgType == 'Error') {
                dialogClass = 'error-dialog';
            }
            else if (config.msgType == 'Warning') {
                dialogClass = 'warning-dialog';
            }
            else if (config.msgType == 'Success') {
                dialogClass = 'success-dialog';
            }

            var buttonText = 'Ok';

            if (typeof (config.okButtonText) == 'string') {
                buttonText = config.okButtonText;
            }

            var buttons = {};


            buttons[buttonText] = function () {
                if (typeof (config.onOk) == 'function') {
                    config.onOk();
                }
                $(this).dialog("close");
            }

            if (typeof (config.showButtons) != 'undefined' && config.showButtons == false) {
                buttons = {};
            }

            var dialog = $(div).dialog({
                dialogClass: dialogClass,
                width: config.width,
                height: config.height,
                title: config.title,
                autoOpen: true,
                modal: true,
                resizable: false,
                buttons: buttons,
                close: function (event, ui) {
                    $(this).dialog("destroy");
                    document.body.removeChild(div);
                }
            });

            return $(dialog);
        },

        /*
        * Creates a basic jquery modal dialog box with yes no buttons
        * functions can be passed for yes/no operations.
        * a div is auto created and used for this purpose
        * no div needs to exist on the page
        * the auto created div is destroyed when the modal is closed.
        */
        /*
        * Config options
        *  width - the width of the message box
        *  height - the height of the message box
        *  title - the title of the message box
        *  msg - the message of the message box
        *  onYes - the function to call when the Yes button is clicked
        *  onNo - the function to call when the No button is clicked
        */
        Prompt: function (config) {
            function Yes() {
                if (typeof (config.onYes) == 'function') {
                    config.onYes();
                }
            }

            function No() {
                if (typeof (config.onNo) == 'function') {
                    config.onNo();
                }
            }
            /*
            var id = 'msg-prompt-' + Math.random();
            var div = document.getElementById(id);
            
            if (typeof(div) != 'undefined') {
            document.body.removeChild(div);
            }
            */
            var div = document.createElement('div');
            div.innerHTML = '<P>' + config.msg + '</p>';
            document.body.appendChild(div);

            var dialogClass = '';
            if (config.msgType == 'Error') {
                dialogClass = 'error-dialog';
            }
            else if (config.msgType == 'Warning') {
                dialogClass = 'warning-dialog';
            }

            $(div).dialog({
                dialogClass: dialogClass,
                width: config.width,
                height: config.height,
                title: config.title,
                autoOpen: true,
                modal: true,
                resizable: false,
                buttons: {
                    "Yes": function () {
                        $(this).dialog("close");
                        Yes();
                    },
                    "No": function () {
                        $(this).dialog("close");
                        No();
                    }
                },
                close: function (event, ui) {
                    $(this).dialog("destroy");
                    document.body.removeChild(div);
                }
            });
        },

        /*
        * A customizable simple message slides in from a desired location
        * then slides out after a few seconds.
        * you can choose to have the message slide in and slide out at:
        * top left tl
        * top center tc
        * top right tr
        * bottom left bl
        * bottom center bc
        * bottom right br
        */
        /*
        * Config options
        *  width - the width of the message box
        *  height - the height of the message box
        *  msg - the message of the message box
        *  dockAt - the position to the message will slide in and slide out from values are:
        *      top left | top-left | tl
        *      top center | top-center | tc
        *      top right | top-right | tr
        *      bottom left | bottom-left | bl
        *      bottom center | bottom-center | bc
        *      bottom right | bottom-right | br
        */
        SlideIn: function (config) {
            var div = document.createElement('div');
            div.className = 'slide-in-msg';
            div.innerHTML = '<p>' + config.msg + '</p>';

            div.style.width = config.width + 'px';
            div.style.height = config.height + 'px';
            div.style.position = 'relative';
            //div.style.bottom = '-' + config.height + 'px';

            var wrapperWidthOffset = parseInt($(div).css("border-left-width")) + parseInt($(div).css("border-right-width"));
            var wrapperHeightOffset = parseInt($(div).css("border-top-width")) + parseInt($(div).css("border-bottom-width"));

            if (isNaN(wrapperWidthOffset)) {
                if (typeof (config.wrapperWidthOffset) == 'number') {
                    wrapperWidthOffset = config.wrapperWidthOffset;
                }
                else {
                    wrapperWidthOffset = 0;
                }
            }
            if (isNaN(wrapperHeightOffset)) {
                if (typeof (config.wrapperHeightOffset) == 'number') {
                    wrapperHeightOffset = config.wrapperHeightOffset;
                }
                else {
                    wrapperHeightOffset = 0;
                }
            }

            var wrapper = document.createElement('div');
            wrapper.style.position = 'absolute';
            wrapper.style.zIndex = 10001;
            wrapper.appendChild(div);
            document.body.appendChild(wrapper);

            $(wrapper).width((config.width + wrapperWidthOffset));
            $(wrapper).height((config.height + wrapperHeightOffset));
            wrapper.style.overflow = 'hidden';

            var startAnimateConfig = {};
            var endAnimateConfig = {};
            switch (config.dockAt) {
                case 'tl':
                case 'top-left':
                case 'top left':
                    $(wrapper).css('top', '0px');
                    $(wrapper).css('left', '5px');
                    $(div).css('top', '-' + config.height + 'px');
                    startAnimateConfig = {
                        top: '0px'
                    }
                    endAnimateConfig = {
                        top: '-' + config.height + 'px'
                    }
                    break;

                case 'tc':
                case 'top-center':
                case 'top center':
                    $(wrapper).css('top', '0px');
                    $(wrapper).css('left', (($(document).width() / 2) - ((config.width + wrapperWidthOffset) / 2)) + 'px');
                    $(div).css('top', '-' + config.height + 'px');
                    startAnimateConfig = {
                        top: '0px'
                    }
                    endAnimateConfig = {
                        top: '-' + config.height + 'px'
                    }
                    break;

                case 'tr':
                case 'top-right':
                case 'top right':
                    $(wrapper).css('top', '0px');
                    $(wrapper).css('right', '5px');
                    $(div).css('top', '-' + config.height + 'px');
                    startAnimateConfig = {
                        top: '0px'
                    }
                    endAnimateConfig = {
                        top: '-' + config.height + 'px'
                    }
                    break;

                case 'bl':
                case 'bottom-left':
                case 'bottom left':
                    $(wrapper).css('bottom', '0px');
                    $(wrapper).css('left', '5px');
                    $(div).css('bottom', '-' + config.height + 'px');
                    startAnimateConfig = {
                        bottom: '0px'
                    }
                    endAnimateConfig = {
                        bottom: '-' + config.height + 'px'
                    }
                    break;

                case 'bc':
                case 'bottom-center':
                case 'bottom center':
                    $(wrapper).css('bottom', '0px');
                    $(wrapper).css('left', (($(document).width() / 2.0) - ((config.width + wrapperWidthOffset) / 2.0)) + 'px');
                    $(div).css('bottom', '-' + config.height + 'px');
                    startAnimateConfig = {
                        bottom: '0px'
                    }
                    endAnimateConfig = {
                        bottom: '-' + config.height + 'px'
                    }
                    break;

                case 'br':
                case 'bottom-right':
                case 'bottom right':
                default:
                    $(wrapper).css('bottom', '0px');
                    $(wrapper).css('right', '5px');
                    $(div).css('bottom', '-' + config.height + 'px');
                    startAnimateConfig = {
                        bottom: '0px'
                    }
                    endAnimateConfig = {
                        bottom: '-' + config.height + 'px'
                    }
                    break;
            }

            $(div).animate(startAnimateConfig, 2000)
			.delay(1000)
			.animate(endAnimateConfig, 2000, function () {
			    document.body.removeChild(wrapper);
			});
        }
    };
})();
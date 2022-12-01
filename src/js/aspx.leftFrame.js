$(function ($) {
    $.fn.aspxPage({
        data: ($("input[type=hidden][id$=hfData]").val() !== "") ? JSON.parse($("input[type=hidden][id$=hfData]").val()) : []
    });
});

; (function ($, window, undefined) {

    var defaults = undefined;

    var aspxPageFunctions = { //PRIVATE FUNCTIONS
        _init: function () {
            this._bind();
        },

        _bind: function () {
            var $this = this;
            $this._buildMenu(defaults.data);

            $("#menudiv").on('click', 'li a', function () {
                $this._verifyLoad($(this).data('url'));
            })
        },

        _buildMenu: function (obj) {
            $("#menudiv").empty();
            var object = {};
            object.items = obj;
            var items = fn_LoadTemplates("div-items", object);
            $("#menudiv").append(items);
            for (let i = 0; i < object.items[0].Menu.length; i++) {
                $(`#html${i + 1}`).jstree();
            }
        },

        _verifyLoad: function (targetURL) {
            var $this = this;
            if (window.frames.parent["TopPane"].contentDocument.getElementById('PivotPane').contentWindow.document.readyState == "complete") {
                if (window.frames.parent['RightPane'].contentWindow.document.readyState == "complete") {
                    SeleccionarNodoAnterior = false;
                    if ($this._ejecutarMetodoPivod(targetURL))
                        window.frames.parent['RightPane'].contentWindow.location.href = targetURL;
                }
                else {
                    if (window.confirm(LeftFrameNoListoPanelDerecho)) {
                        SeleccionarNodoAnterior = false;
                        if (EjecutarMetodoPivot(targetURL))
                            window.frames.parent['RightPane'].contentWindow.location.href = targetURL;
                    }
                    else {
                        SeleccionarNodoAnterior = true;
                    }
                }
            }
            else {
                SeleccionarNodoAnterior = true;
                alert(LeftFrameNoListoPanelSuperior);
            }
        },

        _ejecutarMetodoPivod: function (targetURL) {
            var documentoPivot = window.frames.parent["TopPane"].contentDocument.getElementById('PivotPane');
            var etiquetaPivot = documentoPivot.contentDocument.getElementById("lb_TipoProductoTitulo");
            var botonPivot = documentoPivot.contentDocument.getElementById("ibtSeleccionarPortafolio__1");
            var visibility = (targetURL.indexOf('&ocultarCabecera=true') != -1) ? "hidden" : "visible";

            if (etiquetaPivot != null)
                etiquetaPivot.style.visibility = visibility;

            if (botonPivot != null)
                botonPivot.style.visibility = visibility;
            return true;
        }
    };

    var aspxPageMethods = { //PUBLIC METHODS
        init: function (options) {
            defaults = $.extend({}, options);
            aspxPageFunctions._init();
        },
        debug: function (msg) {
            if (window.console && window.console.log) {
                window.console.log(msg);
            }
        }
    };

    $.fn.aspxPage = function (method) {
        if (aspxPageMethods[method]) { return aspxPageMethods[method].apply(this, Array.prototype.slice.call(arguments, 1)); }
        else if (typeof method === 'object' || !method) { return aspxPageMethods.init.apply(this, arguments); }
        else { $.error('Method ' + method + ' does not exist'); }
    };

})(jQuery, window, document);
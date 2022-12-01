$(function ($) {
    $.fn.aspxPage({
        data: ($("input[type=hidden][id$=hfData]").val() !== "") ? JSON.parse($("input[type=hidden][id$=hfData]").val()) : [],
        aspxPage_G: aspxPage_G
    });
});

; (function ($, window, undefined) {

    var defaults = undefined;
    var seEjecuto = false

    var aspxPageFunctions = { //PRIVATE FUNCTIONS
        _init: function () {
            this._bind();
        },

        _bind: function () {
            var $this = this;            
            $this._plugin();

            $("[id$=tb_SeleccionarPerfil]").on('dblclick', 'tbody tr', function () {
                //alert($(this).data('id'));
                $this._llamarOpener($(this).data('id'));
                //window.open('SmartFrame.htm?Dummy=' + new Date().getTime(), new Date().getTime(), 'resizable=yes,location=no,menubar=no,status=yes,toolbar=no'); window.open('Close.aspx', '_self');
            });
        },

        _plugin: function () {
            $("#tb_SeleccionarPerfil").on("click", "tbody tr", function (e) {
                if (!$(this).hasClass("row-selected")) {
                    $($("#tb_SeleccionarPerfil tbody tr")).each(function (index, element) {
                        $(element).removeClass('row-selected');
                    });
                    $(this).addClass('row-selected');
                }
                else {
                    $(this).removeClass('row-selected');
                }
            });
        },

        _llamarOpener: function (perfil) {
            var $this = this;
            if (!seEjecuto) {
                seEjecuto = true;                
                window.returnValue = perfil + ',' + $("[id$=cambioClave]").val();
                if (window.AsignarPerfil != null) {
                    $this._asignarPerfilPrincipal(perfil, $("[id$=cambioClave]").val());
                }
            }

        },

        _asignarPerfilPrincipal: function (perfil, cambioClave) {
            var isLogged = aspxPage_G.isLogged;

            $("[id$=ih_codigoPerfil]").val(perfil);
            $("[id$=ih_cambioClave]").val(cambioClave);
            $("[id$=ih_codigoDobleClick]").val("S");
            $("[id$=ConexionAdmin]").submit();
            //Form1.submit();

            var fecha = new Date().getTime();
            //parent.closeIframe();
            $("[id$=modalSelectPerfil]").hide()
            /*if (isLogged == "False")
                parent.open("SmartFrame.htm?Dummy=" + fecha, "_self");*/

            window.close();
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

})(jQuery, window, document)
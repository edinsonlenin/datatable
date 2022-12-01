$(function ($) {
    $.fn.aspxPage({
        data: ($("input[type=hidden][id$=hfDataTabs]").val() !== "") ? JSON.parse($("input[type=hidden][id$=hfDataTabs]").val()) : []
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
            $this._buildTabs(defaults.data);

            $("[id$=divTabs]").on('click', 'li a', function () {
                $("#divTabs li.navBarsActive").addClass("navBars");
                $("#divTabs li.navBarsActive").removeClass("navBarsActive");
                $(this).parent().removeClass("navBars");
                $(this).parent().addClass("navBarsActive");
                $this._navegationTabs($(this).data('url'))
            });

            $("#divTabs li.navBarsActive a").click()
        },

        _buildTabs: function (obj) {
            $("#divTabs").empty();
            var object = {};
            object.items = obj;
            var items = fn_LoadTemplates("div-items", object);
            $("#divTabs").append(items);
        },

        _navegationTabs: function (targetUrl) {
            var frame = document.getElementById("ContentPanel");
           
            frame.src = targetUrl;
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
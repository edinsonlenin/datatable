$(function ($) {
    $.fn.aspxPage({
        data: ($("input[type=hidden][id$=hfDataAccordion]").val() !== "") ? JSON.parse($("input[type=hidden][id$=hfDataAccordion]").val()) : []
    });
});

; (function ($, window, undefined) {

    var defaults = undefined;

    var aspxPageFunctions = {
        _init: function () {
            this._buildAccordion(defaults.data);
        },
        _buildAccordion: function (obj) {
            $("#accordionExample").empty();
            var object = {};
            object.items = obj;
            var items = fn_LoadTemplates("div-items", object);
            $("#accordionExample").append(items);
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
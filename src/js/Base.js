function fn_message(url) {        
    var data = new FormData();
    var success;
    $.ajax({
        url: url, //Url a donde la enviaremos
        type: 'POST', //Metodo que usaremos
        contentType: false, //Debe estar en false para que pase el objeto sin procesar
        data: data, //Le pasamos el objeto que creamos con los archivos
        processData: false, //Debe estar en false para que JQuery no procese los datos a enviar
        cache: false //Para que el formulario no guarde cache
    }).done(function (msg) {
        success = $.parseJSON(msg);
        console.log(success); 
        var html =
            '<div class="modal-dialog modal-dialog-centered modal-dialog-scrollable ui-modal-width">' +
            '<div class="ui-modal modal-content" style="left: -88px; top: -107px;">' +
            '<div class="ui-modal-header modal-header">' +
            '<h5 class="modal-title" id="tittle">' + success.Titulo + '</h5>' +
            '<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>' +
            '</div>' +
            '<div class="modal-body ui-modal-content">' +
            '<table style="left: 10px; top: 10px" height="100%" cellspacing="10" width="100%" border="0">' +
            '<tr>' +
            '<td valign="top" align="left" height="3" colspan="1">' +
            (success.ImagenUrl === "" || success.ImagenUrl === null ? '<img id="Image1" src="../Framework/Imagenes/informacion.bmp" />' : '<img id="Image1" src="' + success.ImagenUrl + '" />') +
            '</td>' +
            '<td height="95%" width="98%" class="tab-Negro">' +
            '<div style="overflow: auto; width: 100%; height: 100%">' +
            '<table height="100%" width="100%">' +
            '<tr>' +
            '<td valign="center" align="center">' + success.Mensaje + '</td>' +
            '</tr>' +
            '</table>' +
            '</div>' +
            '</td>' +
            '</tr>' +
            '<tr>' +
            '<td align="center" colspan="2">' +
            '<input onclick="Resultado(\'A\');" type="button" value="' + success.ButtonAValue + '" id="ButtonA" name="ButtonA" class="TablasItemAzul" />' +
            (!success.ButtonB ? '<input onclick="Resultado(\'B\');" type="button" value="' + success.ButtonBValue + '" id="ButtonB" name="ButtonB" class="TablasItemAzul" />' : '') +
            (!success.ButtonC ? '<input onclick="Resultado(\'C\');" type="button" value="' + success.ButtonCValue + '" id="ButtonC" name="ButtonC" class="TablasItemAzul" />' : '') +
            (!success.ButtonD ? '<input onclick="Resultado(\'D\');" type="button" value="' + success.ButtonDValue + '" id="ButtonD" name="ButtonD" class="TablasItemAzul" />' : '') +
            (!success.ButtonE ? '<input onclick="Resultado(\'E\');" type="button" value="' + success.ButtonEValue + '" id="ButtonE" name="ButtonE" class="TablasItemAzul" />' : '') +
            '</td>' +
            '</tr>' +
            '</table>' +
            '</div>' +
            '</div>' +
            '</div>';

        $('div[id$=modalMessageBox]').empty().fadeIn().append(html);
        $('#modalMessageBox').show();
    });
}

function Resultado(valor) {
    window.returnValue = valor;
    $('#modalMessageBox').hide();
}

function fn_callmethod(url, data, success, error, complete, beforeSend) {

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: beforeSend,
        success: success,
        error: error,
        complete: complete
    });
}

function fn_LoadTemplates(templateID, JsonObject) {
    Handlebars.registerHelper('ifCond', function (v1, v2, options) {
        if (v1 > v2) {
            return options.fn(this);
        }
        return options.inverse(this);
    });

    Handlebars.registerHelper('ifEquals', function (v1, v2, options) {
        if (v1 === v2) {
            return options.fn(this);
        }
        return options.inverse(this);
    });

    Handlebars.registerHelper('ifEqualsOr', function (v1, v2, v3, options) {
        if (v1 === v2 || v1 === v3) {
            return options.fn(this);
        }
        return options.inverse(this);
    });

    Handlebars.registerHelper('ifEqualsOrMultiple', function (v1, v2, v3, v4, options) {
        if (v1 === v2 || v1 === v3 || v1 === v4) {
            return options.fn(this);
        }
        return options.inverse(this);
    });

    Handlebars.registerHelper('ifDistinct', function (v1, v2, options) {
        if (v1 !== v2) {
            return options.fn(this);
        }
        return options.inverse(this);
    });

    Handlebars.registerHelper('ifDistinctMultiple', function (v1, v2, v3, v4, v5, v6, options) {
        if (v1 !== v2 && (v3 === v4 || v3 === v5 || v3 === v6)) {
            return options.fn(this);
        }
        return options.inverse(this);
    });

    let stemplate = $("#" + templateID).html();
    let tmpl = Handlebars.compile(stemplate);
    let html = tmpl(JsonObject);
    return html;
}
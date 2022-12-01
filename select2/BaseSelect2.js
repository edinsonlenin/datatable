var first = true;



function getScrollBarWidth() {
    var $outer = $('<div>').css({visibility: 'hidden', width: 100, overflow: 'scroll'}).appendTo('body'),
        widthWithScroll = $('<div>').css({width: '100%'}).appendTo($outer).outerWidth();
    $outer.remove();
    return 100 - widthWithScroll;
};

function construirCabecera(cabecera){
    if(first)
    {
        $(".select2-search.select2-search--dropdown").append(cabecera)

    }
    $(".select2-search.select2-search--dropdown").removeClass("select2-search--dropdown").removeClass("select2-search");
    if(first)
    {
        const anchoSroll = getScrollBarWidth();
        const anchoCabecera = $(".cabecera").width();
        $(".cabecera").width(anchoCabecera - anchoSroll);
    }
    first = false;
}

function construir(id, cabecera, detalle)
{
    
    $(id).select2({
        data,
        templateResult: detalle,
        placeholder:"ingrese algo"
    });

    $('.select2-hidden-accessible').on('select2:open', function (e) {
        construirCabecera(cabecera);
    });
}





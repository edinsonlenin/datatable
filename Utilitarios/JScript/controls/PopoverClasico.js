

class PopoverClasico {


    constructor() {

        const className = '.popoverClasico-menuContextual';//ojo esta clase esta ubicada en el archivo: PopoverClasico.css

        //se ubican a ls div que contienen a los menus emergentes y se los oculta al inicio
        $(className).hide();


        //para ocultar el popover cuando se da click en otros controles o se redimensiona el navegador
        $('body').on('click', function () {
            $(className).hide();
        });
        $(document).on('click', function () {
            $(className).hide();
        });
        $(window).on('resize', function () {
            $(className).hide();
        });

    }


     //retorna la ubicacion en pantalla de un elemento html
    getOffset(htmlElement) {
        const rect = htmlElement.getBoundingClientRect();
         
        return {
            left: rect.left + window.scrollX,
            top: rect.top + window.scrollY
        };
    }


    reubicarPopoper(tablaId, celda, divPopoverId) {
        let positionCell =this. getOffset(celda);//la posicion de la celda servira para configurar la cordenada Y
        let positionTable =this. getOffset(document.getElementById(tablaId));//la posicion de la tabla serviva para configurar la cordenada X

        //se define la posicion del dicPopover
        let divPopover = document.getElementById(divPopoverId);

        divPopover.style.top = (positionCell.top + celda.offsetHeight + 1) + "px";//coordenada Y
        divPopover.style.left = (positionTable.left + 1) + "px";//coordenada X

    }

     

}
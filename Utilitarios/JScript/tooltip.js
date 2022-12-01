	var lastTooltip =null;
    var tooltipBackColor='#C8DFFF';
    
    showTooltip = function (strTitle,strText,strHelpText,strhelpImage,strmainImage)
    {   
		if (lastTooltip==null)
		{
           var newDiv = document.createElement("div");
           newDiv.style.background=tooltipBackColor;
           newDiv.style.color='#1E2126';           
           newDiv.style.position='absolute';
           newDiv.style.width='200px';
           newDiv.style.border='#767676 1px solid';         
           newDiv.style.visibility='hidden';

           var title = document.createElement("span"); 
           title.style.padding='6px 0 4px 10px';
           newDiv.style.font='bold 12px "Trebuchet MS" , "Arial"';
		   title.style.styleFloat='left';
		   title.style.cssFloat='left';
		   title.style.clear='both';
		   title.style.width='100%';
           var titleText = document.createTextNode(strTitle);
           title.appendChild(titleText);

           var mainParagraf = document.createElement("p");
           mainParagraf.style.font='normal 12px "Trebuchet MS" , "Arial"';
           mainParagraf.style.padding='0 2px 6px 20px';
           mainParagraf.style.margin='0';
           mainParagraf.style.styleFloat='left';
		   mainParagraf.style.cssFloat='left';
            
           if (strmainImage)
           {
               var mainImg = document.createElement("img");
               mainImg.setAttribute("src",strmainImage);
               mainImg.style.font='bold 12px "Trebuchet MS" , "Arial"';
               mainImg.style.marginRight='10px';
               mainImg.style.border='#BDBDBD 1px solid';
               mainImg.style.styleFloat='left';
		       mainImg.style.cssFloat='left';
               mainParagraf.appendChild(mainImg);  
           }
           
           
           var mainText = document.createTextNode(strText);
           mainParagraf.appendChild(mainText);
           newDiv.appendChild(title);
           newDiv.appendChild(mainParagraf);
           
           if (strHelpText)
           {
               var horLine = document.createElement("hr"); 
               horLine.style.width='96%';
		       horLine.style.clear='both';

               var helpDiv = document.createElement("div");   
               helpDiv.style.styleFloat='left';
		       helpDiv.style.cssFloat='left';   
		       helpDiv.style.clear='both';
		       helpDiv.style.paddingLeft='6px';     
		       helpDiv.style.height='24px';     
               
               if (strhelpImage)
               {
                   var helpImg = document.createElement("img");    
                   helpImg.setAttribute("src",strhelpImage);
                   helpImg.style.marginRight='8px';
                   helpImg.style.verticalAlign='middle';
                   helpDiv.appendChild(helpImg);
               }

               var helpText = document.createTextNode(strHelpText);                           
               helpDiv.appendChild(helpText);  
               newDiv.appendChild(horLine);
               newDiv.appendChild(helpDiv);                         
           }                    
                                                                        
            lastTooltip=newDiv;    
            if (document.addEventListener) document.addEventListener("mousemove",moveTooltip, true);
            if (document.attachEvent) document.attachEvent("onmousemove",moveTooltip);  
            
            var bodyRef = document.getElementsByTagName("body").item(0);
            bodyRef.appendChild(newDiv);                    
            }                                       
        };
        
        showTooltip2 = function (nombreControlTitulo, nombreControlTexto)
		{   
			var controlTitulo;
			var controlTexto;
			var titulo, texto;

			if (document.getElementById(nombreControlTitulo) == null)
			{
				controlTitulo = document.getElementById('wpnCabecera:' + nombreControlTitulo);
				
				if (controlTitulo == null)
				{
					controlTitulo = document.getElementById('wpnCabecera_' + nombreControlTitulo);
				}
			}
			else
			{
				controlTitulo = document.getElementById(nombreControlTitulo);
			}
			
			titulo = '';
			
			if (controlTitulo != null)
			{
				if (controlTitulo.nodeName == "INPUT") titulo = controlTitulo.value;
				if (controlTitulo.nodeName == "SPAN") titulo = controlTitulo.innerText;
				if (controlTitulo.nodeName == "SELECT")
				{
					if (controlTitulo.selectedIndex != -1) titulo = controlTitulo.options[controlTitulo.selectedIndex].innerText;
				}
			}
			else
			{
				titulo = 'No se encuentra el control ' + nombreControlTitulo;
			}
			
			if (document.getElementById(nombreControlTexto) == null)
			{
				controlTexto = document.getElementById('wpnCabecera:' + nombreControlTexto);
				
				if (controlTexto == null)
				{
					controlTexto = document.getElementById('wpnCabecera_' + nombreControlTexto);
				}
			}
			else
			{
				controlTexto = document.getElementById(nombreControlTexto);
			}
			
			texto = '';
			
			if (controlTexto != null)
			{
				if (controlTexto.nodeName == "INPUT") texto = controlTexto.value;
				if (controlTexto.nodeName == "SPAN") texto = controlTexto.innerText;
				if (controlTexto.nodeName == "SELECT")
				{
					if (controlTexto.selectedIndex != -1) texto = controlTexto.options[controlTexto.selectedIndex].innerText;
				}
			}
			else
			{
				texto = 'No se encuentra el control ' + nombreControlTexto;
			}
			if(true)//Solo Mostramos si se selecciona Por Monto, en general, se puede borrar este if.
			{
				if ((titulo != '') && (texto != '')) showTooltip(titulo, texto);
			}
        };
                
       moveTooltip = function (e)
       {
			if (lastTooltip)
			{
               if (document.all)
                    e = event;
               if (e.target)
                    sourceEl = e.target;
               else if (e.srcElement)
                    sourceEl = e.srcElement;
                      
               var coors=findPos(sourceEl);
               var positionLeft = e.clientX;         
               var positionTop = -1;
               if(sourceEl!= null)   
               {
					if( sourceEl.clientHeight != null)
						positionTop  = coors[1] + sourceEl.clientHeight + 0;
			   }
				               
               lastTooltip.style.top=positionTop+'px';
               lastTooltip.style.left=positionLeft+'px';
               lastTooltip.style.visibility='visible';
               if(positionTop == -1)
					lastTooltip.style.visibility='hidden';
           }	
       }
    
       hideTooltip = function ()
       {              
            var bodyRef = document.getElementsByTagName("body").item(0);
            if (lastTooltip) bodyRef.removeChild(lastTooltip);
            lastTooltip=null;            
       };
       
       function findPos(obj)
       {
	        var curleft = curtop = 0;
	        if (obj.offsetParent)
	        {
		        curleft = obj.offsetLeft
		        curtop = obj.offsetTop
		        
		        while (obj = obj.offsetParent)
		        {
			        curleft += obj.offsetLeft
			        curtop += obj.offsetTop
		        }
	        }
	        return [curleft,curtop];
        }
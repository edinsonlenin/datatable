﻿//  EDITOR PUBLIC (API)
function setHTML(szHTML) 
{
	if (g_state.bMode)
		idEditbox.document.body.innerHTML = szHTML
	else
		idEditbox.document.body.innerText = szHTML
	_Editor_MoveSelection(true)
}

function getHTML() 
{
	var szRet = (g_state.bMode ? idEditbox.document.body.innerHTML : idEditbox.document.body.innerText)
	return szRet
}

function getText() {
	var szRet = ""
	if (g_state.bMode)
		szRet = idEditbox.document.body.innerText
	else {
		setMode(true)
		szRet = idEditbox.document.body.innerText
		setMode(false)
	}
	return szRet
}

function getBody() 
{
	var oRet = idEditbox.document.body
	return oRet
}

function getWidth() 
{
	var nRet = document.body.offsetWidth
	return nRet
}

function getHeight() 
{
	var nRet = document.body.offsetHeight
	return nRet
}

function insertHTML(szHTML) 
{
	var sType
	
	var sel = g_state.GetSelection()
	sType = sel.type
	if (g_state.bMode) {
		if (sType=="Control")
			sel.item(0).outerHTML = szHTML
		else 
			sel.pasteHTML(szHTML)
	}
	else
		sel.text = szHTML
}

function setFocus() {
	idEditbox.focus()
}

function appendHTML(szHTML) {
	if (g_state.bMode) 
		idEditbox.document.body.insertAdjacentHTML("beforeEnd",szHTML)
	else
		idEditbox.document.body.insertAdjacentText("beforeEnd",szHTML)
}

function setBGColor(szValue) 
{
	g_state.bgColor = szValue

	if (g_state.bMode)
	{
		idEditbox.document.body.bgColor = g_state.bgColor
		//parent.frmPlantilla.txhBODY_BGCOLOR.value=g_state.bgColor	TODO
	}
}

function getBGColor() 
{
	var szRet = g_state.bgColor
	return szRet
}

function setDefaultStyle(szValue) 
{
	g_state.css = szValue
	if (g_state.bMode)
		idEditbox.document.body.style.cssText = g_state.css
}

function getDefaultStyle() 
{
	var oRet = g_state.css
	return oRet
}

function setSkin(szSkin) 
{
	if (szSkin == null)
		document.styleSheets.skin.cssText = g_state.defaultSkin
	else
		document.styleSheets.skin.cssText = szSkin
	document.styleSheets.skin.disabled = false	
}

function setPopupSkin(szSkin) 
{
	if (szSkin == null)
		document.styleSheets.popupSkin.cssText = g_state.popupSkin
	else
		document.styleSheets.popupSkin.cssText = szSkin

	_CPopup_Init()
}

function setToolbar(id,g_state) 
{
	var el = document.all[id]
	if (el)
		el.style.display = (g_state) ? "" : "none"	
	if (id=="tbmode")
		_setSize()
}

function setLinks(arLinks) 
{
	g_state.aLinks = arLinks
}

function setBindings(aBindings) 
{
	if ((aBindings) && (aBindings.length>0)) 
	{
		g_state.aBindings = aBindings

		for (var iField = DBSelect.length-1; iField > 0; iField--)
			DBSelect[iField] = null

		for (var iField = 0; iField < g_state.aBindings.length; iField++)
			DBSelect.add(new Option(g_state.aBindings[iField]))

		tbDBSelect.style.display = "inline"
	}
	else
		tbDBSelect.style.display = ""
}



function setMode(bMode) 
{
	if (bMode!=g_state.bMode) {
		g_state.bMode = bMode
	
		var objBody = idEditbox.document.body
		if (!bMode&& !g_state.bMode) 
		{
			_CPopup_Hide()
			objBody.bgColor = objBody.style.cssText = "#fffff0"
			if (g_state.customButtons)
				idStandardBar.style.display = "none"
			else
				idToolbar.style.display = "none"
	
			objBody.innerText = idEditbox.document.body.innerHTML
			objBody.className = "textMode"
		}
		if ((bMode) && (g_state.bMode)) 
		{
			setDefaultStyle(g_state.css)
			setBGColor(g_state.bgColor)
			objBody.className = idStandardBar.style.display = idToolbar.style.display = ""
			objBody.innerHTML = idEditbox.document.body.innerText
		}
		_setSize()

		document.all.cbMode.checked = !bMode


		_Editor_MoveSelection(true)
		setFocus()
	}	
	return bMode
}

function addButton(sID,sButton) 
{
	if (!sID)
		tbButtons.insertAdjacentHTML("beforeEnd","<BR>")
	else	
		tbButtons.insertAdjacentHTML("beforeEnd","<INPUT TYPE=\"button\" ONCLICK=\"_userButtonClick(this)\" CLASS=\"userButton\" ID=\"" + sID + "\" VALUE=\"" + sButton + "\">&nbsp;")
	
	g_state.customButtons = true
}

//  EDITOR PRIVATE

function _Format(szHow, szValue) {
	var oSel	= g_state.GetSelection()
	var sType   = oSel.type 
	var oTarget = (sType == "None" ? idEditbox.document : oSel)
	var oBlock  = (oSel.parentElement != null ? _CUtil_GetBlock(oSel.parentElement()) : oSel.item(0))
	setFocus()
	switch(szHow)
	{
		case "BackColor":
			var el = null
			if (oSel.parentElement != null) {
				el =  _CUtil_GetElement(oSel.parentElement(),"TD")
				if (!el) el =  _CUtil_GetElement(oSel.parentElement(),"TH")
				if (!el) el =  _CUtil_GetElement(oSel.parentElement(),"TR")
				if (!el) el =  _CUtil_GetElement(oSel.parentElement(),"TABLE")
			}
			else 
				el = _CUtil_GetElement(oSel.item(0),"TABLE")
			if (el)
				el.bgColor = szValue
			else
				setBGColor(szValue)
			break;
		case "Datos":
			appendHTML(szValue)
			break;


		case "Justify":
			if (oBlock) 
			{
				oBlock.style.textAlign = ""
				if (((oBlock.tagName=="TABLE") || (oBlock.tagName=="IMG")) && (("left"==oBlock.align) && ("Left"==szValue))) {
					oBlock.align = ""
					break;
				}	
				oBlock.align = szValue
				if ((oBlock.tagName=="HR") || ((oBlock.tagName=="IMG") && szValue!="Center")) break;
			}
			szHow=szHow+szValue
			szValue=""
			// Fall through
		default:
			oTarget.execCommand(szHow, false, szValue)
			break
	}
	g_state.RestoreSelection()
	setFocus()
	return true
}

function _initEditor() {
	g_state = new _CState()
	window.onresize = _setSize

	var sz  =   ""
	sz  +=  ""
	+   "<STYLE>"
	+	   ".DataBound{border:1 solid #999999;margin:1;font-family:Courier;background:#F1F1F1}\n"
	+	   ".textMode {border-top: 1px black solid;font: 10pt courier}\n.NOBORDER TD {border:1px gray solid}"
	+	   "BODY {border: 1px black solid;border-top: none;}"
	+   "</STYLE>"
	+   "<BODY ONCONTEXTMENU=\"return false\">"
	+   "</BODY>"
	_CPopup_Init()
	idEditbox.document.designMode = "on"
	idEditbox.document.open("text/html","replace")

	idEditbox.document.write(sz)
	idEditbox.document.close()
	idEditbox.document.body.onblur = g_state.SaveSelection	
	idEditbox.document.onkeydown = _Editor_KeyDownHandler
	idEditbox.document.onmousedown = _Editor_ClickHandler
	idEditbox.document.ondblclick = _Editor_DblClickHandler
	setTimeout("_pageReady()",0)
}

function _Editor_MoveSelection(bDir) {
	var tr = idEditbox.document.body.createTextRange()
	tr.collapse(bDir)
	tr.select()
}

function _Editor_ClickHandler() {
	g_state.selection = null
}

function _Editor_KeyDownHandler() {
	var ev = this.parentWindow.event
	if (ev.keyCode==9)
		g_state.SaveSelection() 
	else 
		g_state.selection=null
}

function _Editor_DblClickHandler() {
	// Shortcuts
	var el = this.parentWindow.event.srcElement
	if (el.tagName=="IMG")  {
		el.removeAttribute("width")
		el.removeAttribute("height")
		el.style.removeAttribute("width")
		el.style.removeAttribute("height")
		el.width = el.width
		el.height = el.height
	}
	if (el.tagName=="TABLE") 
		_CPopup_Show('Table','')
}

function _setSize() {
	var offsetHeight = 0
	if (document.all.idMode){
		offsetHeight = document.all.idMode.offsetHeight
	}
	document.all.idEditbox.style.pixelHeight = document.body.clientHeight - idToolbar.offsetHeight - offsetHeight-400 //dependiendo de lo q se resta el iframe se hace mas chico
	document.all.idPopup.style.pixelLeft = (document.body.clientWidth - idPopup.document.all.puRegion.offsetWidth) / 2	
}



function _userButtonClick(el) 
{
	if (parent.RTEButton) parent.RTEButton(self, el.id)
}

function _drawToolbar(szIndicador,nzPlantilla)
{
 
    if (szIndicador=="")	
	{
	
	var aSizes = new Array(25,25,25,7,80,76,71,7,25,25,25,8,25,25,25,8,25,25,25,25,8,25,25,25,8,25,25,25,25);

	var aIds = new Array("cut","copy","paste","bar1","formatblock","fontstyle","fontsize","bar2","bold","italic","underline","bar3","left","center","right","bar4","orderedlist","unorderedlist","outdent","indent","bar5","line","bgcolor","textcolor","bar6","link","table","datos","guardar")
	var aTips = new Array(L_TIPCUT_TEXT,L_TIPCOPY_TEXT,L_TIPPASTE_TEXT,"",L_TIPP_TEXT,L_TIPFSTYLE_TEXT,L_TIPFSIZE_TEXT,"",L_TIPB_TEXT,L_TIPI_TEXT,L_TIPU_TEXT,"",L_TIPLJ_TEXT,L_TIPCJ_TEXT,L_TIPRJ_TEXT,"",L_TIPOL_TEXT,L_TIPUL_TEXT,L_TIPDINDENT_TEXT,L_TIPIINDENT_TEXT,"",L_TIPLINE_TEXT,L_TIPBGCOLOR_TEXT,L_TIPFGCOLOR_TEXT,"",L_TIPLINK_TEXT,L_TIPTABLE_TEXT,L_TIPEMOTICON_TEXT,L_TIPGUARDAR_TEXT)
	var aCommand = new Array("_Format('cut')","_Format('copy')","_Format('paste')",null,"_CPopup_Show('formatblock','')","_CPopup_Show('font','')","_CPopup_Show('fontsize','')",null,"_Format('bold')","_Format('italic')","_Format('underline')",null,"_Format('Justify','Left')","_Format('Justify','Center')","_Format('Justify','Right')",null,"_Format('insertorderedlist')","_Format('insertunorderedlist')","_Format('outdent')","_Format('indent')",null,"_Format('InsertHorizontalRule')","_CPopup_Show('BackColor','')","_CPopup_Show('ForeColor','')",null,"_CPopup_Show('Link','')","_CPopup_Show('Table','')","_CPopup_Show('Datos','')","SaveSourceCode()")
	}
    else
	{
	var aSizes = new Array(25,25,25,7,80,76,71,7,25,25,25,8,25,25,25,8,25,25,25,25,8,25,25,25,8,25,25);
	var aIds = new Array("cut","copy","paste","bar1","formatblock","fontstyle","fontsize","bar2","bold","italic","underline","bar3","left","center","right","bar4","orderedlist","unorderedlist","outdent","indent","bar5","line","bgcolor","textcolor","bar6","link","table")
	var aTips = new Array(L_TIPCUT_TEXT,L_TIPCOPY_TEXT,L_TIPPASTE_TEXT,"",L_TIPP_TEXT,L_TIPFSTYLE_TEXT,L_TIPFSIZE_TEXT,"",L_TIPB_TEXT,L_TIPI_TEXT,L_TIPU_TEXT,"",L_TIPLJ_TEXT,L_TIPCJ_TEXT,L_TIPRJ_TEXT,"",L_TIPOL_TEXT,L_TIPUL_TEXT,L_TIPDINDENT_TEXT,L_TIPIINDENT_TEXT,"",L_TIPLINE_TEXT,L_TIPBGCOLOR_TEXT,L_TIPFGCOLOR_TEXT,"",L_TIPLINK_TEXT,L_TIPTABLE_TEXT)
	var aCommand = new Array("_Format('cut')","_Format('copy')","_Format('paste')",null,"_CPopup_Show('formatblock','')","_CPopup_Show('font','')","_CPopup_Show('fontsize','')",null,"_Format('bold')","_Format('italic')","_Format('underline')",null,"_Format('Justify','Left')","_Format('Justify','Center')","_Format('Justify','Right')",null,"_Format('insertorderedlist')","_Format('insertunorderedlist')","_Format('outdent')","_Format('indent')",null,"_Format('InsertHorizontalRule')","_CPopup_Show('BackColor','')","_CPopup_Show('ForeColor','')",null,"_CPopup_Show('Link','')","_CPopup_Show('Table','')")
	}
	var sz = "<DIV ID=idStandardBar><NOBR>", iLeft=0, iHeight=24
	for (var i = 0 ; i < aSizes.length; i++) 
	{
		sz	+=  ""
		+   "<SPAN CLASS=tbButton ONKEYPRESS=\"if (event.keyCode==13) {" + aCommand[i] + ";event.keyCode=0}\" " + (aTips[i]=="" ? "" : ("TABINDEX=" + (i+1))) + " "
		+	   "ID=\"tb" + aIds[i] + "\" "
		+	   "STYLE=\"width: " + aSizes[i] + ";height:" + iHeight
		+	   "\""
		+   ">" 
		+	   "<SPAN "
		+		   "STYLE=\""
		+			   "position:absolute;"
		+			   "width:" + aSizes[i] + ";height:" + iHeight + ";"
		+			   "clip: rect(0 " + aSizes[i] + " " + iHeight + " 0)"
		+		   "\""
		+	   ">"
		+		   "<IMG "
		+			   "TITLE=\"" + aTips[i] + "\" "
		+			   "ONCLICK=\"" + aCommand[i] + "; event.cancelBubble=true\" "
		+			   "ONMOUSEDOWN=\"if (event.button==1) this.style.pixelTop=-" + (iHeight*2) + "\" "
		+			   "ONMOUSEOVER=\"this.style.pixelTop=-" + iHeight + "\" "
		+			   "ONMOUSEOUT=\"this.style.pixelTop=0\" "
		+			   "ONMOUSEUP=\"this.style.pixelTop=-" + iHeight + "\" "
		+			   "SRC=\"" + L_TOOLBARGIF_TEXT + "\" "
		+			   "STYLE=\"position:absolute;top:0;left:-" + iLeft + "\""
		+		   ">"
		+	   "</SPAN>"
		+   "</SPAN>" 
		+  (aTips[i]=="" ?  "</NOBR><NOBR>" : "")
		iLeft += aSizes[i]
	}
	sz  +=  ""
	+   "</NOBR>"
	+   "<SPAN CLASS=tbButton ID=\"tbDBSelect\">" 
	+	   "<SPAN "
	+		   "STYLE=\""
	+			   "position:absolute;"
	+			   "width: 100;"
	+			   "clip: rect(0 100 " + iHeight + " 0)"
	+		   "\""
	+	   ">"
	+	   "<SELECT "
	+		   "ID=DBSelect "
	+		   "ONCLICK='event.cancelBubble=true;' "
	+		   "ONCHANGE='_CPopup_InsertDatabound(this)' "
	+	   ">"
	+		   "<OPTION>"
	+			   "- " + L_TBDATABINDING_TEXT + " -"
	+		   "</OPTION>"
	+	   "</SELECT>"
	document.write(sz + "</DIV>")
}

function _drawModeSelect() {
	var sz = "<TABLE CELLSPACING=0 CELLPADDING=0 ID=idMode>"
	+	"<TR><TD><INPUT TYPE=checkbox ID=cbMode ONCLICK=\"setMode(!this.checked)\"></TD>"
	+   "<TD><FONT color=navy>&nbsp;&nbsp;<LABEL FOR=cbMode color=navy><B><FONT color=navy>" + L_MODETITLE_TEXT + "</B></FONT></LABEL>" + L_MODEDESC_TEXT + "</FONT>"
	+   "</TD></TR></TABLE>"
	document.write(sz)
	//cbMode.checked = false
	//idMode.visible = false
}

//  _CState 

function _CState()
{
	this.selection		= null
	this.bMode			= true
	this.customButtons 	= false
	this.css = this.bgColor	= ""
	this.defaultSkin	= document.styleSheets.skin.cssText
	this.popupSkin		= document.styleSheets.popupSkin.cssText
	this.aLinks			= new Array()
	this.szSearch		= location.search.substring(1)
	this.aBindings		= new Array()
	this.aListPopups	= new Object()
	this.aCache			= new Object()
	
	this.RestoreSelection	= _CState_RestoreSelection
	this.GetSelection	= _CState_GetSelection
	this.SaveSelection	= _CState_SaveSelection
}

function _CState_RestoreSelection() 
{
	if (this.selection) this.selection.select()
}

function _CState_GetSelection() 
{
	var oSel = this.selection

	if (!oSel) {
		oSel = idEditbox.document.selection.createRange()
		oSel.type = idEditbox.document.selection.type
	}
	return oSel
}

function _CState_SaveSelection() 
{
	g_state.selection = idEditbox.document.selection.createRange()
	g_state.selection.type = idEditbox.document.selection.type
}


//  POPUP (Link, table and image popup need to be worked on)

function _CPopup_Init() 
{
	var sz  =   ""
	+   "<HTML ID=popup>"
	+	   "<STYLE>" 
	+		   document.styleSheets.defPopupSkin.cssText 
	+		   "\n" 
	+		   document.styleSheets.popupSkin.cssText 
	+	   "</STYLE>"
	+	   "<SCRIPT>function IMAGELoaded(w) {parent._IMAGELoaded(w,self)}</SCRIPT>"
	+	   "<BODY "
	+		   "ONSCROLL=\"return false\" SCROLL=no TABINDEX=-1 "
	+		   "ONSELECTSTART=\"return event.srcElement.tagName=='INPUT'\" "
	+	   "><DIV ID=puRegion>"
	+		   "<TABLE ID=header>"
	+			   "<TR>"
	+				   "<TH NOWRAP ID=caption></TH>"
	+				   "<TH VALIGN=middle ALIGN=RIGHT><DIV ID=close ONCLICK=\"parent._CPopup_Hide()\">"
	+					   L_CLOSEBUTTON_TEXT
	+				   "</DIV></TH>"
	+			   "</TR>"
	+		   "</TABLE>"
	+		   "<DIV ALIGN=CENTER ID=content></DIV>"
	+	   "</DIV></BODY>"
	+   "</HTML>"

	idPopup.document.open("text/html","replace")
	idPopup.document.write(sz)
	idPopup.document.close()
}

function _CPopup_InsertDatabound(eSelect)
{
	if (eSelect.selectedIndex != 0)
	{
		var sElemName = eSelect.options[eSelect.selectedIndex].text;
		var iLen = sElemName.length
		sElemName = sElemName.replace(/"/g, '&#034;')
		insertHTML('<INPUT CLASS=DataBound SIZE=' + (iLen + 2) + ' NAME="' + sElemName +'" VALUE=" ' + sElemName + ' ">')
		eSelect.selectedIndex = 0;
		idEditbox.focus()	
	}
}

function _CPopup_Hide() 
{
	document.all.idPopup.style.zIndex=-1
	document.all.idPopup.style.visibility = "hidden"	
	idPopup.document._type = ""
   	idPopup.document.onkeydown=idPopup.document.onmouseover=idPopup.document.onclick = null
	idEditbox.focus()
}

function _CPopup_Show(szType,nzPlantilla) 
{

	var oRenderer, szCacheKey = "PopupRenderer." + szType
   	if (idPopup.document._type == szType)
		_CPopup_Hide()
	else
	{
		document.all.idPopup.style.zIndex = -1
		oRenderer = g_state.aCache[szCacheKey]
		if ((!oRenderer) || ("Link"==szType))
			g_state.aCache[szCacheKey] = oRenderer = new _CPopupRenderer(szType,nzPlantilla)
		// Force Sizing
		document.all.idPopup.style.visibility = ""
		idPopup.document.all.puRegion.style.pixelHeight = idPopup.document.all.puRegion.style.pixelWidth = 100
		
		idPopup.document._type					= szType
		idPopup.document._renderer				= oRenderer
		idPopup.document.all.caption.innerText	= oRenderer.GetCaption()
		idPopup.document.all.content.innerHTML	= oRenderer.GetHTML()
		idPopup.document.onkeydown				= new Function("this._renderer.OnKeyDown()")
		idPopup.document.onmouseover			= new Function("this._renderer.OnMouseOver()")
		idPopup.document.onclick				= new Function("this._renderer.OnClick()")
		oRenderer.ResetContext(idPopup.document)
		setTimeout("_CPopupRenderer_Display('" + szType + "')",0)
	}
}

function _CPopupRenderer_Display(szType) {
	var offsetHeight, oRenderer, szCacheKey = "PopupRenderer." + szType
	oRenderer = g_state.aCache[szCacheKey]
	if (oRenderer.autoSize) {	
		idPopup.document.all.puRegion.style.pixelHeight = document.all.idPopup.style.pixelHeight = idPopup.document.all.puRegion.offsetHeight
		idPopup.document.all.puRegion.style.pixelWidth = document.all.idPopup.style.pixelWidth = idPopup.document.all.puRegion.offsetWidth + 50
		document.all.idPopup.style.pixelLeft = (document.body.clientWidth - idPopup.document.all.puRegion.offsetWidth) / 2
	}
	else { 
		offsetHeight = 0
		if (document.all.idMode){
			offsetHeight = document.all.idMode.offsetHeight
		}		
		
		idPopup.document.all.puRegion.style.pixelHeight  = document.all.idPopup.style.pixelHeight = document.body.clientHeight - idToolbar.offsetHeight-offsetHeight-120
		idPopup.document.all.puRegion.style.pixelWidth  = document.all.idPopup.style.pixelWidth = document.body.clientWidth - 120
		document.all.idPopup.style.pixelLeft = 25
	}
	document.all.idPopup.style.zIndex=2
	idPopup.focus()
}

function _CPopupRenderer(szType,nzPlantilla)
{
	this.szType		=  szType
	this.elCurrent	=  this.oDocument  = null

	this.ResetContext   =  _CPopupRenderer_ResetContext
	this.GetCaption	= _CPopupRenderer_GetCaption	
	this.GetHTML	= _CPopupRenderer_GetHTML
	this.autoSize	= true
	this.OnMouseOver = new Function()
	this.OnKeyDown	= _CListPopupRenderer_GenericOnKeyDown
	switch(szType)
	{
		case "formatblock":
		case "font":
		case "fontsize":
			this.OnMouseOver= _CListPopupRenderer_OnMouseOver
			this.OnKeyDown  = _CListPopupRenderer_OnKeyDown
		case "BackColor": 
		case "ForeColor":
			this.OnClick	= _CListPopupRenderer_OnClick
			this.Highlight  = _CListPopupRenderer_Highlight
			this.Select	= _CListPopupRenderer_Select
			break
		case "Datos":
			this.OnMouseOver= _CListPopupRenderer_OnMouseOver
			this.Highlight  = _CListPopupRenderer_Highlight
			this.OnClick	= _CListPopupRenderer_OnClick
			this.Select	= _CListPopupRenderer_Select
			break

		default:
			this.OnClick	= new Function()
			break	
	}

	switch(szType)
	{
		case "formatblock":
			this.szCaption		= L_PUTITLEPARAGRAPHSTYLE_TEXT
			this.PrepareHTML	= _CFormatBlockPopupRenderer_PrepareHTML
			this.szHTML			= this.PrepareHTML()
			break
		case "font":
			this.szCaption		= L_PUTITLEFONTFACE_TEXT
			this.PrepareHTML	= _CFontFacesPopupRenderer_PrepareHTML
			this.szHTML		= this.PrepareHTML()
			break
		case "fontsize":
			this.szCaption		= L_PUTITLEFONTSIZE_TEXT
			this.PrepareHTML	=_CFontSizesPopupRenderer_PrepareHTML
			this.szHTML			= this.PrepareHTML()
			break
		case "Link":
			this.szCaption		= L_PUTITLELINK_TEXT
			this.PrepareHTML	= _CLinkPopupRenderer_PrepareHTML
			this.szHTML			= this.PrepareHTML()
			break
		case "Table": 
			this.szCaption		= L_PUTITLENEWTABLE_TEXT
			this.PrepareHTML	= _CTablePopupRenderer_PrepareHTML
			this.szHTML			= this.PrepareHTML()
			break
		case "Image":
			this.szCaption		= L_PUTITLEIMAGE_TEXT
			this.PrepareHTML	= _CImagePopupRenderer_PrepareHTML
			this.szHTML			= this.PrepareHTML()
   			this.autoSize		= false
			break
		case "BackColor": 
			this.szCaption		= L_PUTITLEBGCOLOR_TEXT
			this.szHTML			= "<DIV ID=ColorPopup ALIGN=CENTER>" + _CUtil_BuildColorTable("") + "</DIV>"
			break
		case "ForeColor":
			this.szCaption		= L_PUTITLETEXTCOLOR_TEXT
			this.szHTML			= "<DIV ID=ColorPopup ALIGN=CENTER>" + _CUtil_BuildColorTable("") + "</DIV>"
			break
		case "Datos":
			this.szCaption		= L_PUTITLECAMPOS_TEXT
			this.PrepareHTML	= _CCamposRenderer_PrepareHTML
			this.szHTML		= this.PrepareHTML()
			break
		case "previa":
			this.szCaption		= L_PUTITLEPREVIA_TEXT
			this.PrepareHTML	= _CPreviaRenderer_PrepareHTML
			this.szHTML		= this.PrepareHTML()
			break
		case "enviar":
			this.szCaption		= L_PUTITLEENVIAR_TEXT
			this.PrepareHTML	= _CEnviarRenderer_PrepareHTML
			this.szHTML		= this.PrepareHTML(nzPlantilla)
			break
		default:
			this.szCaption		= ""
			break
	}
}

function _CCampoPopupRenderer_Select(elTD){	
	_Format("Datos","[CA|" + defaultFields[elTD][1] + "|" +  defaultFields[elTD][0] + "]")
	_CPopup_Hide()
}

function _CPopupRenderer_ResetContext(oDoc)
{
	this.oDocument  = oDoc
	this.elCurrent  = null

	if (this.szType=="Table") {
			var oSel	= idEditbox.document.selection.createRange() 
		var oBlock  = (oSel.parentElement != null ? _CUtil_GetElement(oSel.parentElement(),"TABLE") : _CUtil_GetElement(oSel.item(0),"TABLE"))
		if (oBlock!=null) {
			oDoc.all.tabEdit.className=""
			oDoc.all.tabEditBodytxtPadding.value = oBlock.cellPadding
			oDoc.all.tabEditBodytxtSpacing.value = oBlock.cellSpacing
			oDoc.all.tabEditBodytxtBorder.value = oBlock.border
			oDoc.all.tabEditBodytxtBorderColor.value = oBlock.borderColor
			oDoc.all.tabEditBodytxtBackgroundImage.value = oBlock.background
			oDoc.all.tabEditBodytxtBackgroundColor.value = oBlock.bgColor
		}
		oDoc.elCurrent = oBlock
	}
}

function _CPopupRenderer_GetCaption()
{
	return this.szCaption
}

function _CPopupRenderer_GetHTML()
{
	return this.szHTML
}
function _CEnviarRenderer_PrepareHTML(nzPlantilla)
{

	var sz="<TABLE ALIGN=center ID=idList CELLSPACING=0 CELLPADDING=0>"
	sz += ""
	+   "<TR><TD colspan=2 align=center><BR> </TD></TR>"
	+   "<TR><TD>Código: </TD>"
	+   	"<TD><INPUT TYPE=text NAME=txtCodigo size=20 maxlength=15></TD></TR>"
	+   "<TR><TD>E-Mail: </TD>"
	+   	"<TD><INPUT TYPE=text NAME=txtEmail size=45 maxlength=40></TD></TR>"
	+   "<TR><TD colspan=2 align=center><BR><INPUT TYPE=button name=cmdAceptar value=Aceptar ONCLICK=\"parent._CGeneraEnvioPrueba(" + nzPlantilla +",txtCodigo.value,txtEmail.value)\"><INPUT TYPE=button name=cmdCerrar value=Cerrar ONCLICK=\"parent._CPopup_Hide()\"> </TD></TR>"
	+   "</TABLE>"	

	return sz
}

function _CPreviaRenderer_PrepareHTML()
{
	var sz="<TABLE ALIGN=center ID=idList CELLSPACING=0 CELLPADDING=0>"
	sz += ""
	+   "<TR><TD colspan=2 align=center><BR> </TD></TR>"
	+   "<TR><TD>Código: </TD>"
	+   	"<TD><INPUT TYPE=text NAME=txtCodigo size=20 maxlength=15></TD></TR>"
	+   "<TR><TD colspan=2 align=center><BR><INPUT TYPE=button name=cmdAceptar value=Aceptar ONCLICK=\"parent._CGeneraVistaPrevia(txtCodigo.value)\"><INPUT TYPE=button name=cmdCerrar value=Cerrar ONCLICK=\"parent._CPopup_Hide()\"> </TD></TR>"
	+   "</TABLE>"	

	return sz
}

function _CGeneraEnvioPrueba(nzPlantilla,szValor,szEmail)
{

	if (szValor=="")
	{
		alert("Debe Ingresar un valor.")
	}
	else
	{
		if (szEmail=="")
		   {
			alert("Debe Ingresar la direccion e-mail.")
		   }
		else
		   {		   
			 var szPagina="VistaPrevia.asp?Cod=" + nzPlantilla +  "&Inm=" + szValor + "&Email=" + szEmail
			 window.open(szPagina,"456","title=ENVIO DE PRUEBA,status=yes,resizable=yes,toolbar=no,scrollbars=yes,top=30,left=30,width=700,height=500", 1 );
		   }

	}

}



function _CGeneraVistaPrevia(szValor)
{

	if (szValor=="")
	{
		alert("Debe Ingresar un valor.")
	}
	else
	{
		//Resolver la página para el valor dado

		// PRIMERO: BUSCAR VALOR
		
		var sNum=_CBuscarValor(szValor)

		if (sNum!=-1)
		{
			// OBTENER Arreglo con datos

			var vCodigoHTML=_CResolverPlantillaHTML(sNum)
			openindex(vCodigoHTML)
		}
		else
		{
			alert("El código ingresado No Existe")
		}	

	}

}

function _CResolverPlantillaHTML(sNum)
{
	var objBody = idEditbox.document.body
	var vBodyColor="#ffffff"	//TODO
	var vCadena=""
	var vCodigoHTML=""
	if (g_state.bMode) 
	  {
		vCadena= objBody.innerHTML 
	  }
	else
	  {
		vCadena= objBody.innerText 
	  }

	// BUSCAR en objBody los caracteres "[" y "]" Para identificar los campos en defaultFields

	var vCadenaIni=vCadena
	var nPos
	var nPosCampo
	var vCadena2
	var nPosFin
	var sCampoNombre
	var nPosFinalVariable
	var nIndice
	var sValorDato
	var sCadenaAReemplazar
	
	var nLongitud
	var k = 0 
	var nPosAbsoluto=0

	while(k==0) 
	{ 
		nPos=vCadena.indexOf("[CA|")
		if (nPos==-1)
		  {
			k=1
		  }
		else
		  {	
			nPosCampo=nPos + 4

			vCadena2=vCadena.substring(nPosCampo,nPos+500)
			nPosFin=vCadena2.indexOf("|")

			sCampoNombre=vCadena2.substring(0,nPosFin)
			nPosFinalVariable=vCadena2.indexOf("]")
			nPosAbsoluto= nPosAbsoluto + nPosFinalVariable+nPosCampo+2

			nIndice=_CBuscarIndiceCampo(sCampoNombre) 
			sValorDato=defaultDatos[sNum][nIndice]

			sCadenaAReemplazar="[CA|" + vCadena2.substring(0,nPosFinalVariable+1)

			nLongitud=vCadena.length

			vCadenaIni=vCadenaIni.replace(sCadenaAReemplazar,sValorDato)
			vCadena=vCadena.substring(nPosFinalVariable+nPos+5,nLongitud)

		  }		
	} 


	if(vBodyColor==""){ vBodyColor="#FFFFFF" }

	if (g_state.bMode) 
	  {
		vCodigoHTML="<HTML><BODY BGCOLOR=" + vBodyColor + " >"+ vCadenaIni + "<BODY></HTML>"
	  }
	else
	  {
		vCodigoHTML="<HTML><BODY BGCOLOR=" + vBodyColor + " >"+ vCadenaIni + "<BODY></HTML>"
	  }

	return vCodigoHTML
}


function _CBuscarIndiceCampo(szValor) 
{
var sz  =  -1
for (var i=0; i < defaultFields.length; i++) 
	{
		if (defaultFields[i][0]==szValor) 
			return i
	}
return sz

}



function _CBuscarValor(szValor) 
{
var sz  =  -1
for (var i=0; i < defaultDatos.length; i++) 
	{
		if (defaultDatos[i][0]==szValor) 
			return i
	}
return sz

}

var strTabla
function _CCamposRenderer_PrepareHTML() 
{   
	var sz  =  "<TABLE ALIGN=center ID=idList CELLSPACING=0 CELLPADDING=0>"
		sz  +=  ""
		+   "<TR><TD STYLE='font-size: 8pt;color: midnightblue;font-family: Verdana;text-align: left;' width=7%>Tabla:</TD>"
		+   "<TD><SELECT style='font-size: 8pt;color: black;font-family: Verdana' ID=cboTabla width=180  ONCHANGE=\"parent._CCamposPopupRenderer__UpdateList(cboTabla.value)\">"
				
		for (var i = 0; i < arrCampos.length; i++) 
		{			
			sz  +=  ""
			+   "<OPTION VALUE=\"" + arrCampos[i][1] + "\" "	
			+	   ((arrCampos[i][1]==strTabla) ? "SELECTED" : "")
			+   ">"
			+	   arrCampos[i][0]
			+   "</OPTION>"
		}

		sz  +=  "</SELECT></TD></TR>"
		
		sz  +=  ""
		+   "<TR><TD STYLE='font-size: 8pt;color: midnightblue;font-family: Verdana;text-align: left;' width=7%>Campo:</TD>"
		+   "<TD><SELECT style='font-size: 8pt;color: black;font-family: Verdana' ID=cboList width=180>"
		for (var i = 0; i < defaultFields.length; i++) 
		{
			sz  +=  ""
			+   "<OPTION VALUE=\"" + defaultFields[i][1] + "\" "			
			+   ">"
			+	   defaultFields[i][0]
			+   "</OPTION>"
		}

		sz  +=  "</SELECT></TD></TR>"

		sz  +=  "<TR><TD HEIGHT=30 ALIGN=CENTER COLSPAN=2>"
		sz  +=  "<INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' ONCLICK=\"parent._CCampoPopupRenderer_Select(cboList.selectedIndex)\" TYPE=submit ID=idSave VALUE=\"" + L_INSERT_TEXT + "\">"
		sz  +=  " <INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' ONCLICK=\"parent._CPopup_Hide()\" TYPE=reset ID=idCancel VALUE=\"" + L_CANCEL_TEXT + "\"></TD></TR>"

//	sz += "<TR><TD ONCLICK=\"parent._CCamposPopupRenderer_InsertOther(this)\" ALIGN=center _item=\"custom\" STYLE=\"margin:0pt;padding:0pt\" NOWRAP><FONT ID=customFont>" + L_CUSTOMFONT_TEXT + "</FONT></TR>"
	sz  +=  "</TABLE>"
	return sz
}

function _CCamposPopupRenderer_InsertOther(el) {
	if (el._item=="custom") {
		el._item = "input"
		var sz = "<INPUT ONSELECTSTART=\"event.cancelBubble=true\" ONKEYDOWN=\"event.cancelBubble=true\" ONKEYPRESS=\"if (event.keyCode==13) {this.face=this.value;document._renderer.Select(this.parentElement)};event.cancelBubble=true\" VALUE=\"" + L_CUSTOMFONTENTRY_TEXT + "\" ONFOCUS=\"if (this.value==this.defaultValue) this.select()\" TYPE=text>"
		el.innerHTML = sz
		el.children[0].focus()
	}
	el.document.parentWindow.event.cancelBubble = true	
}
function _CImageRenderer_Select(elImg) {
	insertHTML("<IMG SRC=\"" + elImg + "\">")
	g_state.RestoreSelection()
	_CPopup_Hide()
}

function _CEmoticonRenderer_Select(elImg) {
	insertHTML("<IMG SRC=\"" + elImg.src + "\" WIDTH=" + elImg.width + " HEIGHT=" + elImg.height + ">")
	g_state.RestoreSelection()
	_CPopup_Hide()
}

function _CFontSizesPopupRenderer_PrepareHTML()
{   
	var sz  =  "<TABLE ALIGN=center ID=idList CELLSPACING=0 CELLPADDING=0>"
	for (var i=1; i <= 7; i++) 
	{
		sz  +=  ""
		+   "<TR>"
		+	   "<TD NOWRAP "
		+		   "_item=" + i + " "
		+		   "ALIGN=center "
		+		   "STYLE=\"margin:0pt;padding:0pt\""
		+	   ">"
		+		   "<FONT SIZE=" + i + ">" 
		+			   L_STYLESAMPLE_TEXT + "(" + i + ")"
		+		   "</FONT>"
		+	   "</TD>"
		+   "</TR>"
	}			
	sz  +=  "</TABLE>"
	return sz
}

function _CFontFacesPopupRenderer_PrepareHTML()
{   
	var sz  =  "<TABLE ALIGN=center ID=idList CELLSPACING=0 CELLPADDING=0>"
	for (var i=0; i < defaultFonts.length; i++) 
	{
		sz  +=  ""
		+   "<TR>"
		+	   "<TD NOWRAP "
		+		   "_item=" + i + " "
		+		   "ALIGN=center "
		+		   "STYLE=\"margin:0pt;padding:0pt\""
		+	   ">"
		+		   "<FONT FACE=\"" + defaultFonts[i][0] + "\">" 
		+			   defaultFonts[i][1] 
		+		   "</FONT>"
		+			(defaultFonts[i][2] ? ("(" + defaultFonts[i][1] + ")") : "")
		+	   "</TD>"
		+   "</TR>"
	}
	
//	sz += "<TR><TD ONCLICK=\"parent._CFontFacesPopupRenderer_InsertOther(this)\" ALIGN=center _item=\"custom\" STYLE=\"margin:0pt;padding:0pt\" NOWRAP><FONT ID=customFont>" + L_CUSTOMFONT_TEXT + "</FONT></TR>"
	sz  +=  "</TABLE>"

	return sz
}

function _CFontFacesPopupRenderer_InsertOther(el) {
	if (el._item=="custom") {
		el._item = "input"
		var sz = "<INPUT ONSELECTSTART=\"event.cancelBubble=true\" ONKEYDOWN=\"event.cancelBubble=true\" ONKEYPRESS=\"if (event.keyCode==13) {this.face=this.value;document._renderer.Select(this.parentElement)};event.cancelBubble=true\" VALUE=\"" + L_CUSTOMFONTENTRY_TEXT + "\" ONFOCUS=\"if (this.value==this.defaultValue) this.select()\" TYPE=text>"
		el.innerHTML = sz
		el.children[0].focus()
	}
	el.document.parentWindow.event.cancelBubble = true	
}

function _CFormatBlockPopupRenderer_PrepareHTML()
{   
	var sz, defaultParagraphs   = new Array()

	defaultParagraphs[0] = new Array("<P>", L_STYLENORMAL_TEXT + " (P)")	
	for (var i=1; i <= 6; i++) 
		defaultParagraphs[i] = new Array("<H"+i+">", L_STYLEHEADING_TEXT + i + " (H" + i + ")")	
	defaultParagraphs[7] = new Array("<PRE>", L_STYLEFORMATTED_TEXT + "(PRE)")

	sz  =  "<TABLE CLASS=block ALIGN=center ID=idList CELLSPACING=0 CELLPADDING=0>"
	for (var i=0; i < defaultParagraphs.length; i++) 
	{
		sz  +=  ""
		+   "<TR>"
		+	   "<TD NOWRAP "
		+		   "_item=" + i + " "
		+		   "ALIGN=center "
		+		   "STYLE=\"margin:0pt;padding:0pt\""
		+	   ">"
		+		   defaultParagraphs[i][0] 
		+		   defaultParagraphs[i][1] 
		+		   "</" + defaultParagraphs[i][0].substring(1) 
		+	   "</TD>"
		+   "</TR>"
	}
	sz  +=  "</TABLE>"
	return sz
}

function _CImagePopupRenderer_PrepareHTML()
{   

	var d = this.oDocument
	var oSel = g_state.GetSelection()
	var oEl, szURL = sz = ""

	if (oSel.parentElement)  
	{
		oEl = _CUtil_GetElement(oSel.parentElement(),"IMG")
	}
	else 
	{

		oEl = _CUtil_GetElement(oSel.item(0),"IMG")
	}

	if (oEl)
		szURL = oEl.src

	sz  ="<TABLE ALIGN=center >" 
	+   "<BR>" 
	+   L_IMAGEWEB_TEXT 
	+   "<NOBR><INPUT ID=urlValue SIZE=45 VALUE=\"" + szURL + "\" TYPE=file></NOBR>"
	+		   "</TD>"
	+	   "</TR>"
	+	   "<TR>"
	+		   "<TD ALIGN=center>"
	+		   "<TABLE><TR>"
	+		   	"<TD colspan=3 ALIGN=center>"
	+			   "<INPUT ONCLICK=\"parent._CImageRenderer_Select(urlValue.value)\" TYPE=submit ID=idSave VALUE=\"" + L_INSERT_TEXT + "\"> <INPUT ONCLICK=\"parent._CPopup_Hide()\" TYPE=reset ID=idCancel VALUE=\"" + L_CANCEL_TEXT + "\">"
	+		  	 "</TD><TD width=25%></TD></TR></TABLE>"
	+		   "</TD>"
	+	   "</TR>"
	+   "</TABLE>"


//	return "<IFRAME ID=idPhotos WIDTH=98% HEIGHT=90% STYLE=\"visibility: hidden;border: none\" SRC=\"photos/photos.htm" + ((g_state.szSearch!="") ? ("?" + g_state.szSearch) : "") +  "\"></IFRAME>"
	return sz
}


function _IMAGELoaded(wPhotos,wPopup) {
	wPhotos.setBGColor("#F1F1F1")
	wPopup.document.all.idPhotos.style.visibility = ""
}

function _CTablePopupRenderer_PrepareHTMLPage(szID,bDisplay) {
	var sz=""
	+   "<TABLE height=100% " + ((!bDisplay) ? " style=\"display: none\"" : "") + " width=100% CELLSPACING=0 CELLPADDING=0 ID=" + szID + ">"
	+	   "<TR ID=tableContents>"
	+		   "<TD ID=tableOptions VALIGN=TOP NOWRAP WIDTH=150 ROWSPAN=2>"
	+			   "<A HREF=\"javascript:parent._CTablePopupRenderer_Select(this,'" + szID + "','prop1')\">"
	+				   L_TABLEROWSANDCOLS_TEXT
	+			   "</A>"
	+			   "<BR>"
	+			   "<A HREF=\"javascript:parent._CTablePopupRenderer_Select(this,'" + szID + "','prop2')\">"
	+				   L_TABLEPADDINGANDSPACING_TEXT
	+			   "</A>"
	+			   "<BR>"
	+			   "<A HREF=\"javascript:parent._CTablePopupRenderer_Select(this,'" + szID + "','prop3')\">"
	+				   L_TABLEBORDERS_TEXT
	+			   "</A>"
	+			   "<BR>"
	+			   "<A HREF=\"javascript:parent._CTablePopupRenderer_Select(this,'" + szID + "','prop4')\">"
	+				   L_TABLEBG_TEXT
	+			   "</A>"
	+			   "<BR>"
	+		   "</TD>"
	+		   "<TD BGCOLOR=black ID=puDivider ROWSPAN=2>"
	+		   "</TD>"
	+		   "<TD ID=tableProps VALIGN=TOP>"
	if (szID=="tabNewBody") {
		sz+= "<DIV ID='" + szID + "prop1'>"
		+	"<P CLASS=tablePropsTitle>" + L_TABLEROWSANDCOLS_TEXT + "</P>"
		+  "<TABLE><TR><TD>"
		+				   L_TABLEINPUTROWS_TEXT
		+				   "</TD><TD><INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' SIZE=2 TYPE=text ID=" + szID + "txtRows VALUE=2 MAXLENGTH=2>"
		+				   "</TD></TR><TR><TD>"
		+				   L_TABLEINPUTCOLUMNS_TEXT
		+				   "</TD><TD><INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' SIZE=2 TYPE=text ID=" + szID + "txtColumns VALUE=2 MAXLENGTH=2>"
		+			   "</TD></TR></TABLE></DIV>" 
	} 
	else  {
		sz+= "<DIV ID='" + szID + "prop1'>"
			+	"<P CLASS=tablePropsTitle>" + L_TABLEROWSANDCOLS_TEXT + "</P>"	
			+   "<INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' type=button ID=" + szID + "txtRows VALUE=\"" + L_TABLEINSERTROW_TEXT + "\" ONCLICK=\"parent._CTablePopupRenderer_AddRow(this)\"><P>"
			+   "<INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' type=button ID=" + szID + "txtCells VALUE=\"" + L_TABLEINSERTCELL_TEXT + "\" ONCLICK=\"parent._CTablePopupRenderer_AddCell(this)\"><BR>"
			+	"</DIV>" 
	}
	sz +=			   "<DIV ID='" + szID + "prop2' STYLE=\"display: none\">"
	+					"<P CLASS=tablePropsTitle>" + L_TABLEPADDINGANDSPACING_TEXT + "</P>"
	+				   L_TABLEINPUTCELLPADDING_TEXT
	+				   "<INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' SIZE=2 TYPE=text ID=" + szID + "txtPadding VALUE=0 MAXLENGTH=1>"
	+				   "<BR>"
	+				   L_TABLEINPUTCELLSPACING_TEXT
	+				   "<INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' SIZE=2 TYPE=text ID=" + szID + "txtSpacing VALUE=0 MAXLENGTH=1>"
	+			   "</DIV>"
	+			   "<DIV ID=" + szID + "prop3 STYLE=\"display: none\">"
	+					"<P CLASS=tablePropsTitle>" + L_TABLEBORDERS_TEXT + "</P>"
	+				   L_TABLEINPUTBORDER_TEXT
	+				   "<INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' SIZE=2 TYPE=text ID=" + szID + "txtBorder VALUE=1 MAXLENGTH=1>"
	+				   "<BR>"
	+				   L_TABLEINPUTBORDERCOLOR_TEXT
	+				   "&nbsp;<INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' SIZE=7 TYPE=text ID=" + szID + "txtBorderColor value=#000000 MAXLENGTH=7><BR>" 
	+				   _CUtil_BuildColorTable("idBorder"+szID, "", "parent._CTablePopupRenderer_ColorSelect(this,'" + szID + "txtBorderColor')") 
	+			   "</DIV>"
	+			   "<DIV ID=" + szID + "prop4 SIZE=12 STYLE=\"display: none\">"
	+					"<P CLASS=tablePropsTitle>" + L_TABLEBG_TEXT + "</P>"
	+				   L_TABLEINPUTBGIMGURL_TEXT
	+				   "<INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' TYPE=text ID=" + szID + "txtBackgroundImage SIZE=19 MAXLENGTH=20>"
	+				   "<BR>"
	+				   L_TABLEINPUTBGCOLOR_TEXT	
	+				   "<INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana;align=right' TYPE=text SIZE=7 ID=" + szID + "txtBackgroundColor MAXLENGTH=7><BR>" 
	+				   _CUtil_BuildColorTable("idBackground"+szID, "", "parent._CTablePopupRenderer_ColorSelect(this,'" + szID + "txtBackgroundColor')") 
	+			   "</DIV>"
	+		   "</TD>"
	+	   "</TR><TR><TD align=center ID=tableButtons valign=bottom>"
	if (szID=="tabNewBody") {
		sz +=	"<INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' TYPE=submit ONCLICK=\"parent._CTablePopupRenderer_BuildTable('" + szID + "',this.document)\" VALUE=\"" + L_TABLEINSERT_TEXT + "\">"
			+   " <INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' TYPE=reset VALUE=\"" + L_CANCEL_TEXT + "\" ONCLICK=\"parent._CPopup_Hide()\">"
	} else {
		sz +=	"<INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' TYPE=submit ONCLICK=\"parent._CTablePopupRenderer_BuildTable('" + szID + "',this.document)\" VALUE=\"" + L_TABLEUPDATE_TEXT + "\">"
			+   " <INPUT STYLE='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;	height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' TYPE=reset VALUE=\"" + L_CANCEL_TEXT + "\" ONCLICK=\"parent._CPopup_Hide()\">"
	}
	sz+=   "</TD></TR></TABLE>"
	return sz
}

function _CTablePopupRenderer_PrepareHTML()
{   
	var sz  = "<TABLE CLASS=tabBox ID=\"tabSelect\" CELLSPACING=0 CELLPADDING=0 WIDTH=95%><TR HEIGHT=15><TD CLASS=tabItem STYLE=\"border-bottom: none\" NOWRAP><DIV ONCLICK=\"if (tabEdit.className!='disabled') {this.className='selected';this.parentElement.style.borderBottom = tabEdit.className=tabNewBody.style.display='';tabEditBody.style.display='none';tabEdit.parentElement.style.borderBottom='1px black solid'}\" CLASS=selected ID=tabNew>Nueva Tabla</DIV></TD>"
	+   "<TD CLASS=tabItem NOWRAP><DIV ONCLICK=\"if (this.className!='disabled') {this.className='selected';this.parentElement.style.borderBottom = tabNew.className=tabEditBody.style.display='';tabNew.parentElement.style.borderBottom='1px black solid';tabNewBody.style.display='none'}\" CLASS=disabled ID=tabEdit>Editar Tabla</DIV></TD><TD CLASS=tabSpace WIDTH=100%>&nbsp;</TD></TR><TR><TD VALIGN=TOP CLASS=tabBody COLSPAN=3>"
	+   _CTablePopupRenderer_PrepareHTMLPage("tabNewBody",true)
	+   _CTablePopupRenderer_PrepareHTMLPage("tabEditBody",false)
	+	"</TD></TR></TABLE>"
	return sz
}

function _CTablePopupRenderer_Select(el,szID, id) 
{
	var d = el.document

	for (var i = 1; i < 5; i++)
		d.all[szID + "prop" + i].style.display = "none"

	d.all[szID + id].style.display = ""
}


function _CTablePopupRenderer_ColorSelect(el,id) 
{
	el.document.all[id].value = el.bgColor
}	

function _CTablePopupRenderer_AddRow(el) {
	var elRow = el.document.elCurrent.insertRow()
	for (var i=0;i<el.document.elCurrent.rows[0].cells.length;i++) {
		var elCell = elRow.insertCell()
		elCell.innerHTML = "&nbsp;"
	}
}

function _CTablePopupRenderer_AddCell(el) {
	for (var i=0;i<el.document.elCurrent.rows.length;i++) {
		var elCell = el.document.elCurrent.rows[i].insertCell()
		elCell.innerHTML = "&nbsp;"
	}
}

function _CTablePopupRenderer_BuildTable(szID, d) 
{
	if (szID=="tabNewBody") {
		var sz =   ""
		+   "<TABLE "
		+  (((d.all[szID + "txtBorder"].value=="") || (d.all[szID + "txtBorder"].value=="0")) ? "class=\"NOBORDER\"" : "")
		+	   (d.all[szID + "txtPadding"].value != "" ? "cellPadding=\"" + d.all[szID + "txtPadding"].value + "\" " : "")
		+	   (d.all[szID + "txtSpacing"].value != "" ? "cellSpacing=\"" + d.all[szID + "txtSpacing"].value + "\" " : "")

		+	   (d.all[szID + "txtBorder"].value != "" ? "border=\"" + d.all[szID + "txtBorder"].value + "\" " : "")
		+	   (d.all[szID + "txtBorderColor"].value != "" ? "bordercolor=\"" + d.all[szID + "txtBorderColor"].value + "\" " : "")
		+	   (d.all[szID + "txtBackgroundImage"].value != "" ? "background=\"" + d.all[szID + "txtBackgroundImage"].value + "\" " : "")

		+	   (d.all[szID + "txtBackgroundColor"].value != "" ? "bgColor=\"" + d.all[szID + "txtBackgroundColor"].value + "\" " : "")
		+   ">"

		for (var r=0; r < d.all[szID + "txtRows"].value; r++) 
		{
			sz +=  "<TR>"
			
			for (var c=0; c < d.all[szID + "txtColumns"].value; c++)
				sz +=  "<TD>&nbsp;</TD>"
			
			sz +=  "</TR>"
		}

		sz +=  "</TABLE>"
		insertHTML(sz)
   } else
		if (d.elCurrent) {
			d.elCurrent.cellPadding = d.all.tabEditBodytxtPadding.value
			d.elCurrent.cellSpacing = d.all.tabEditBodytxtSpacing.value
			d.elCurrent.border = d.all.tabEditBodytxtBorder.value
			d.elCurrent.className = (d.elCurrent.border=="" || d.elCurrent.border==0) ? "NOBORDER" : ""
 			d.elCurrent.borderColor = d.all.tabEditBodytxtBorderColor.value
			d.elCurrent.bgColor = d.all.tabEditBodytxtBackgroundColor.value
			d.elCurrent.background = d.all.tabEditBodytxtBackgroundImage.value
   }
	_CPopup_Hide()	
}

function _CListPopupRenderer_OnClick() 
{
	var elTD = _CUtil_GetElement(this.oDocument.parentWindow.event.srcElement, "TD") 
	if (elTD && elTD._item) this.Select(elTD)
}

function _CListPopupRenderer_GenericOnKeyDown() {
	var ev		= this.oDocument.parentWindow.event
	if (ev.keyCode==27) _CPopup_Hide()
}

function _CListPopupRenderer_OnKeyDown() 
{
	var el
	var iRow = iCell	= 0
	var ev		= this.oDocument.parentWindow.event
	var idList  = this.oDocument.all.idList
	var elTR	= _CUtil_GetElement(this.elCurrent,"TR")
	var elTD	= _CUtil_GetElement(this.elCurrent,"TD")

	
	if (elTR != null) 
	{
		iRow	= elTR.rowIndex
		iCell   = elTD.cellIndex
	}
	switch (ev.keyCode) 
	{
		case 37:
			iCell--
			if (iCell < 0) 
				iCell = idList.rows[iRow].cells.length-1
			break
		case 38:
			iRow--
			if (iRow < 0) 
				iRow = idList.rows.length-1
			break
		case 39:
			iCell++
			if (iCell > idList.rows[iRow].cells.length-1) 
				iCell = 0
			break
		case 40:
			iRow++
			if (iRow > idList.rows.length-1) 
				iRow = 0
			break
		case 13:
			break;
		case 27:
			_CPopup_Hide()
			break;
		default:
			return;
	}

	el = idList.rows[iRow].cells[iCell]
	if (el && el._item)
		if (13 == ev.keyCode) {
			ev.keyCode=0		
			this.Select(el)
		}
		else
			this.Highlight(el)
}

function _CListPopupRenderer_OnMouseOver() 
{
	var el = _CUtil_GetElement(this.oDocument.parentWindow.event.srcElement, "TD") 

	if (el && el._item && el != this.elCurrent)
		this.Highlight(el)
}

function _CListPopupRenderer_Highlight(el) 
{
	var elC = this.elCurrent
	if (elC) elC.style.borderWidth =  elC.style.borderColor = elC.style.borderStyle  =   ""
	el.style.borderWidth	=   "1px"
	el.style.borderColor	=   "green"
	el.style.borderStyle	=   "solid"
	this.elCurrent		=   el
}

function _CListPopupRenderer_Select(elTD) 
{
	g_state.RestoreSelection()

	var el = elTD.children[0]
	switch (this.szType) 
	{
		case "font":
			_Format("FontName",el.face)
			break
		case "fontsize":
			_Format("FontSize",el.size)			
			break
		case "formatblock":
			_Format("FormatBlock","<" + el.tagName + ">")
			break
		case "ForeColor":
			_Format("ForeColor", elTD.bgColor)
			break
		case "BackColor":
			_Format("BackColor",elTD.bgColor)
			break
		case "Datos":
			_Format("Datos","[CA|" + defaultFields[elTD._item][0] + "|" +  defaultFields[elTD._item][1] + "]")
			break

	}
		
	_CPopup_Hide()
}

function _CLinkPopupRenderer_AddLink(d) 
{
	var szURL = d.all.urlValue.value
	var szType = d.all.urlType[d.all.urlType.selectedIndex].text
	var oSel = g_state.GetSelection()
	var sType = oSel.type
	szURL = ((0 == szURL.indexOf("mailto:") || 0 == szURL.indexOf("http://") || 0 == szURL.indexOf("ftp://") || 0 == szURL.indexOf("javascript:")) ? "" : szType) + szURL
	if (szURL!="") 
	{
		if ((oSel.parentElement) && (oSel.text=="")) 
		{
			oSel.expand("word")
			if (oSel.text=="") 
			{
				var oStore = oSel.duplicate()
				if (d.all.pageList) {
					var idx = d.all.pageList.selectedIndex
					if (d.all.pageList[idx].value==szURL)
						oSel.text = d.all.pageList[idx].text
					else
						oSel.text = szURL
				}
				else
						oSel.text = szURL				
				oSel.setEndPoint("StartToStart",oStore)
			} 
			oSel.select()
			sType="Text"
		}

		if ((oSel.item) && (oSel.item(0).tagName=="IMG")) 
		{
				oSel.item(0).width = oSel.item(0).offsetWidth
				oSel.item(0).height = oSel.item(0).offsetHeight
				oSel.item(0).border = (d.all.displayBorder.checked) ? 1 : ""
		}
		if (d.all.urlValue.value!="")
			oSel.execCommand("CreateLink",false,szURL)	
		else
			oSel.execCommand("UnLink",false,szURL)			
	}

	idEditbox.focus()	
}

function _CCamposPopupRenderer__UpdateList(oValue){
var oRenderer
strTabla = oValue
_CamposPlantilla(oValue)
oRenderer = new _CPopupRenderer('Datos','')
idPopup.document.all.content.innerHTML	= oRenderer.GetHTML()
}

function _CLinkPopupRenderer__UpdateURL(oDoc,szURL) {
	var szType = szURL.substring(0,szURL.indexOf(":"))
	for (var i=0;i<oDoc.all.urlType.length;i++) 
		if (oDoc.all.urlType[i].value==szType)
			oDoc.all.urlType.selectedIndex = i
	if (("http"==szType) || ("ftp"==szType)) 
			szURL = szURL.substring(szURL.indexOf("//")+2)
	else
			szURL = szURL.substring(szURL.indexOf(":")+1)
	oDoc.all.urlValue.value = szURL
}

function _CLinkPopupRenderer_PrepareHTML() 
{
	var d = this.oDocument
	var oSel = g_state.GetSelection()
	var oEl, sType = oSel.type, bImg = false, szURL = sz = ""
	if (oSel.parentElement)  
	{
		oEl = _CUtil_GetElement(oSel.parentElement(),"A")
	}
	else 
	{

		oEl = _CUtil_GetElement(oSel.item(0),"A")
		bImg = oSel.item(0).tagName=="IMG"
	}

	if (oEl)
		szURL = oEl.href
	
	sz  ="<TABLE ALIGN=center>" 
	
	if (g_state.aLinks.length>0) 
	{
		sz  +=  ""
		+   "<TR>"
		+	   "<TD>" 
		+		   L_LINKSELECT_TEXT 
		+		   "<SELECT style='font-size: 8pt;color: black;font-family: Verdana' ID=pageList ONCHANGE=\"parent._CLinkPopupRenderer__UpdateURL(this.document,this[this.selectedIndex].value)\">"
		+			   "<OPTION VALUE=''>" 
		+				   "=="
		+				   L_LINKSELECTPAGE_TEXT
		+				   "=="
		+			   "</OPTION>"
		
		for (var i = 0; i < g_state.aLinks.length; i++) 
		{
			sz  +=  ""
			+   "<OPTION VALUE=\"" + g_state.aLinks[i][0] + "\" "
			+	   (oEl && (g_state.aLinks[i][0]==oEl.href) ? "SELECTED" : "")
			+   ">"
			+	   g_state.aLinks[i][1]
			+   "</OPTION>"
		}

		sz  +=  "</SELECT>"
	}
	var arTypes = new Array("http","ftp","mailto")
	var arText = new Array("http://","ftp://","mailto:")
	var szType = szURL.substring(0,szURL.indexOf(":"))
	if (("http"==szType) || ("ftp"==szType)) 
			szURL = szURL.substring(szURL.indexOf("//")+2)
	else
			szURL = szURL.substring(szURL.indexOf(":")+1)

	sz  += ""
	+   "<BR>" 
	+   L_LINKWEB_TEXT 
	+   "<NOBR><SELECT ID=urlType style='font-size: 8pt;color: black;font-family: Verdana'>"
	
	for (var i=0;i<arTypes.length;i++) {
		sz+= "<OPTION VALUE='" + arTypes[i] + "' "
		+	(arTypes[i]==szType ? " SELECTED " : "")
		+ ">" + arText[i]
	}
	sz += "</SELECT><INPUT STYLE='border-right: midnightblue 1px solid;border-top: midnightblue 1px solid;font-size: 8pt;border-left: midnightblue 1px solid;border-bottom: midnightblue 1px solid;font-family: Verdana' ID=urlValue SIZE=45 VALUE=\"" + szURL + "\" TYPE=text></NOBR>"

	if (bImg)
	{
		sz  +=  ""
		+   "<BR>"
		+   "<INPUT TYPE=checkbox ID=displayBorder " + ((oSel.item(0).border!=0) ? " checked " : "") + ">" 
		+   L_LINKIMGBORDER_TEXT
	}

	sz  +=  ""
	+		   "</TD>"
	+	   "</TR>"
	+	   "<TR>"
	+		   "<TD ALIGN=center>"
	+			   "<INPUT style='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' ONCLICK=\"parent._CLinkPopupRenderer_AddLink(this.document)\" TYPE=submit ID=idSave VALUE=\"" + L_INSERT_TEXT + "\">"
	+			   " <INPUT style='border-right: #436390 1px solid;border-top: #436390 1px solid;font-size: 11px;font-weight:bold;border-left: #436390 1px solid;cursor: hand;border-bottom: #436390 1px solid;font-family: Arial;height: 20px;text-align: center;background-color:#f0f0e0;color:#003399' ONCLICK=\"parent._CPopup_Hide()\" TYPE=reset ID=idCancel VALUE=\"" + L_CANCEL_TEXT + "\">"
	+		   "</TD>"
	+	   "</TR>"
	+   "</TABLE>"
	return sz
}

//  UTIL

function _CUtil_GetElement(oEl,sTag) 
{
	while (oEl!=null && oEl.tagName!=sTag)
		oEl = oEl.parentElement

	return oEl
}

function _CUtil_BuildColorTable(sID,fmt,szClick) 
{
	var sz, cPick = new Array("00","33","66","99","CC","FF"), iCnt=2
	var iColors = cPick.length, szColor = ""


	sz = "<TABLE CELLSPACING=0 CELLPADDING=0><TR><TD VALIGN=middle><DIV CLASS=currentColor ID=\"" + sID + "Current\">&nbsp;</DIV></TD><TD>"
	+   "<TABLE ONMOUSEOUT=\"document.all." + sID + "Current.style.backgroundColor = ''\" ONMOUSEOVER=\"document.all." + sID + "Current.style.backgroundColor = event.srcElement.bgColor\" CLASS=colorTable CELLSPACING=0 CELLPADDING=0 ID=\"" + sID + "\">"
	for (var r=0;r<iColors;r++) {
		sz+="<TR>"
		for (var g=iColors-1;g>=0;g--)
			for (var b=iColors-1;b>=0;b--) {
				szColor = cPick[r]+cPick[g]+cPick[b] 
				sz+="<TD"
						+ " BGCOLOR=\"#" + szColor + "\""
						+ "_item=\"" + szColor + "\" "
						+ "TITLE=\"#" + szColor + "\" "
						+ (szClick ? "ONCLICK=\"" + szClick + "\" " : "")
						+ ">&nbsp;</TD>"
			}
		sz+="</TR>"
	}
	sz+="</TABLE></TD></TR></TABLE>"
	return sz
}

function _CUtil_GetBlock(oEl) 
{
	var sBlocks = "|H1|H2|H3|H4|H5|H6|P|PRE|LI|TD|DIV|BLOCKQUOTE|DT|DD|TABLE|HR|IMG|"

	while ((oEl!=null) && (sBlocks.indexOf("|"+oEl.tagName+"|")==-1))
		oEl = oEl.parentElement
	return oEl
}

function openindex(oText)
      {
OpenWindow=window.open("", "newwin", "top=50,left=50,height=420, width=480,resizable=yes,toolbar=no,scrollbars=yes,menubar=no");
OpenWindow.document.write(oText)

//OpenWindow.document.close()
//self.name="main"
     }

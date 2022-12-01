	
	function HTMLfilter(data)
	{
		data = data.replace(/&/g, "&amp\;");
		data = data.replace(/\"/g, "&quot\;");
		data = data.replace(/</g, "&lt\;");
		data = data.replace(/>/g, "&gt\;");
		return data;
	}

	function onSubmitCompose(xfield,xedit) {
			if (xedit.getBGColor() != "") {
				xfield.value = "<div style='background-color:" 
				+ xedit.getBGColor() 
				+ "'>"
				+ xedit.getHTML()
				+ "</div>"
			} else{
				xfield.value = xedit.getHTML();
			}
	}
	
	function RTELoaded(w) {
		w.setToolbar("tbmode",true)
		w.setToolbar("tbtable",true)
		w.setToolbar("tbbar5",false)
		w.setToolbar("tbemoticon",false)
		w.setSkin("#idToolbar {border: 1px black solid; background:#e0dfed}")
	}
	
	function EditorLoaded(xfield,xedit)
	{
			plaintext = xfield.value;
			if(plaintext=="") plaintext="<FONT face='Arial, Sans-serif' size=2>&nbsp;</FONT>";
			xedit.setHTML(plaintext);
	}


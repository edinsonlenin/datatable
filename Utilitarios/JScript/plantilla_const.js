// List of emoticon gifs. Add or remove to change selection
// arEmoticons - 12x12 pixels
// arBigEmoticons - 16x16 pixels

// Customize Font List
// FONTNAME_TEXT - Displayed in the pop-up
// FONTNAMEDEF_TEXT - The font definition used in the HTML
var L_FONTARIAL_TEXT = "Arial";
var L_FONTARIALDEF_TEXT = "Arial, Sans-serif";
var L_FONTARIALBLACK_TEXT = "Arial Black";
var L_FONTARIALBLACKDEF_TEXT = "Arial Black, Geneva, Arial, Sans-serif";
var L_FONTCOURIERNEW_TEXT = "Courier New";
var L_FONTCOURIERNEWDEF_TEXT = "Courier New, Courier, Monospace";
var L_FONTTIMESNEWROMAN_TEXT = "Times New Roman";
var L_FONTTIMESNEWROMANDEF_TEXT = "Times New Roman, Times, Serif";
var L_FONTVERDANA_TEXT = "Verdana";
var L_FONTVERDANADEF_TEXT = "Verdana, Geneva, Arial, Sans-serif";
var L_LUCIDAHAND_TEXT = "Lucida Handwriting";
var L_LUCIDAHANDDEF_TEXT = "Lucida Handwriting, Cursive";
var L_GARAMOND_TEXT = "Garamond";
var L_GARAMONDDEF_TEXT = "Garamond, Times, Serif";
var L_WEBDINGS_TEXT = "Webdings";
var L_WEBDINGSDEF_TEXT = "Webdings";
var L_WINGDINGS_TEXT = "Wingdings";
var L_WINGDINGSDEF_TEXT = "Wingdings";

// Add/ Remove fonts by modifying array
// _CFont(Definition, Display Text, Symbol)
// Set Symbol=true for non-alphabetic fonts to append display text in default font to the sample string

function _CFont(szDef,szText,bSymbol) {
	return new Array(szDef,szText,bSymbol);
};

defaultFonts = new Array();
defaultFonts[0] = _CFont(L_FONTARIALDEF_TEXT, L_FONTARIAL_TEXT, false);
defaultFonts[1] = _CFont(L_FONTARIALBLACKDEF_TEXT, L_FONTARIALBLACK_TEXT, false);
defaultFonts[2] = _CFont(L_FONTVERDANADEF_TEXT, L_FONTVERDANA_TEXT, false);
defaultFonts[3] = _CFont(L_FONTTIMESNEWROMANDEF_TEXT, L_FONTTIMESNEWROMAN_TEXT, false);
defaultFonts[4] = _CFont(L_GARAMONDDEF_TEXT,L_GARAMOND_TEXT, false);
defaultFonts[5] = _CFont(L_LUCIDAHANDDEF_TEXT,L_LUCIDAHAND_TEXT, false);
defaultFonts[6] = _CFont(L_FONTCOURIERNEWDEF_TEXT, L_FONTCOURIERNEW_TEXT, false);
defaultFonts[7] = _CFont(L_WEBDINGSDEF_TEXT, L_WEBDINGS_TEXT, true);
defaultFonts[8] = _CFont(L_WINGDINGSDEF_TEXT, L_WINGDINGS_TEXT, true);

// Entry 5-8 specify "Paragraph","Font Style", "Font Size"
// Update widths 5-8 if localized
var L_TOOLBARGIF_TEXT = "../../Utilitarios/imagenes/plantilla_tbEN.gif";




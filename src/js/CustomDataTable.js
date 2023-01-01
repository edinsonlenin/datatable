var objSelect = null;
var objSelectHTML = "";
var objSelectIndex = -1;
var objSelectRow = null;
var objSelectRowDel = null;

$.fn.dataTable.render.luxon = function (from, to, locale) {
    let DateTime = luxon.DateTime;
    // Argument shifting
    if (arguments.length === 1) {
        locale = "es";
        to = from;
        from = null;
    } else if (arguments.length === 2) {
        locale = "es";
    }

    return function (d, type, row) {
        if (!d) {
            return type === "sort" || type === "type" ? 0 : d;
        }

        return (from ? DateTime.fromFormat(from) : DateTime.fromISO(d)).setLocale(locale).toFormat(to);
    };
};

class ExportExcel {
    hojaDeEstilos;
    datatableInstance;
    options;

    styleUwg = `<styleSheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:x14ac="http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac" xmlns:x16r2="http://schemas.microsoft.com/office/spreadsheetml/2015/02/main" mc:Ignorable="x14ac x16r2">
  <numFmts count="4">
      <numFmt numFmtId="179" formatCode="###,###,###,##0"/>
      <numFmt numFmtId="180" formatCode="###,###,###,##0.00"/>
      <numFmt numFmtId="181" formatCode="##,##0.0000"/>
      <numFmt numFmtId="182" formatCode="##,##0.000000"/>
      <numFmt numFmtId="183" formatCode="##,###,##0.00000000"/>
  </numFmts>
  <fonts count="4" x14ac:knownFonts="1">
      <font>
          <sz val="10"/>
          <color indexed="13"/>
          <name val="Arial"/>
      </font>
      <font>
          <sz val="8"/>
          <color indexed="13"/>
          <name val="Arial"/>
      </font>
      <font>
          <b/>
          <sz val="8"/>
          <color indexed="10"/>
          <name val="Arial"/>
      </font>
      <font>
          <b/>
          <sz val="8"/>
          <color rgb="FFA40000"/>
          <name val="Arial"/>
          <family val="2"/>
      </font>
  </fonts>
  <fills count="8">
      <fill>
          <patternFill patternType="none"/>
      </fill>
      <fill>
          <patternFill patternType="solid">
              <fgColor rgb="FFFF0000"/>
              <bgColor indexed="64"/>
          </patternFill>
      </fill>
      <fill>
          <patternFill patternType="solid">
              <fgColor rgb="FFFF0000"/>
              <bgColor indexed="64"/>
          </patternFill>
      </fill>
      <fill>
          <patternFill patternType="solid">
              <fgColor rgb="FF00B050"/>
              <bgColor indexed="64"/>
          </patternFill>
      </fill>   
      <fill>
          <patternFill patternType="solid">
              <fgColor rgb="FFAAD3F2"/>
              <bgColor indexed="64"/>
          </patternFill>
      </fill>
      <fill>
          <patternFill patternType="solid">
              <fgColor rgb="FFFFFF00"/>
              <bgColor indexed="64"/>
          </patternFill>
      </fill>               
      <fill>
          <patternFill patternType="solid">
              <fgColor rgb="FFC0C0C0"/>
              <bgColor indexed="64"/>
          </patternFill>
      </fill>  
      <fill>
          <patternFill patternType="solid">
              <fgColor rgb="FFFFA500"/>
              <bgColor indexed="64"/>
          </patternFill>
      </fill>

  </fills>
  <borders count="2">
      <border>
          <left/>
          <right/>
          <top/>
          <bottom/>
          <diagonal/>
      </border>
      <border>
          <left style="thin">
              <color indexed="9"/>
          </left>
          <right style="thin">
              <color indexed="9"/>
          </right>
          <top style="thin">
              <color indexed="9"/>
          </top>
          <bottom style="thin"><color indexed="9"/></bottom> <diagonal/></border></borders><cellStyleXfs count="1">    <xf numFmtId="0" fontId="0" fillId="0" borderId="0"/>    </cellStyleXfs>  <cellXfs count="94">
  <xf numFmtId="0" fontId="0" fillId="0" borderId="0" xfId="0"/>
  <xf numFmtId="0" fontId="2" fillId="4" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyProtection="1">    <alignment horizontal="center"/></xf>    
  <xf numFmtId="0" fontId="2" fillId="4" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyProtection="1">    <alignment horizontal="left"/></xf>
  <xf numFmtId="0" fontId="2" fillId="4" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyProtection="1">    <alignment horizontal="right"/></xf>
  <xf numFmtId="0" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="0" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="0" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="0" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="180" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="180" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="180" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="181" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="181" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="181" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="182" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="182" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="182" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="183" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="183" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="183" fontId="3" fillId="0" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="2" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="3" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="5" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="6" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="0" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="center"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="left"/>    </xf>
  <xf numFmtId="180" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">    <alignment horizontal="right"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="181" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="182" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="center"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="left"/>    </xf>
  <xf numFmtId="183" fontId="1" fillId="7" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1">        <alignment horizontal="right"/>    </xf>



  </cellXfs>
  <cellStyles count="1">
  <cellStyle name="Normal" xfId="0" builtinId="0"/>
  </cellStyles>
  <dxfs count="0"/>
  <tableStyles count="0" defaultTableStyle="TableStyleMedium2" defaultPivotStyle="PivotStyleLight16"/>
  <colors>
  <indexedColors>



  <rgbColor rgb="00000000"/>
  <rgbColor rgb="00FFFFFF"/>
  <rgbColor rgb="00FF0000"/>
  <rgbColor rgb="0000FF00"/>
  <rgbColor rgb="000000FF"/>
  <rgbColor rgb="00FFFF00"/>
  <rgbColor rgb="00FF00FF"/>
  <rgbColor rgb="0000FFFF"/>
  <rgbColor rgb="00AAD3F2"/>
  <rgbColor rgb="00808080"/>
  <rgbColor rgb="00000080"/>
  <rgbColor rgb="00000000"/>
  <rgbColor rgb="00F7F7F7"/>
  <rgbColor rgb="00000000"/>
  <rgbColor rgb="00FF00FF"/>
  <rgbColor rgb="0000FFFF"/>
  <rgbColor rgb="00800000"/>
  <rgbColor rgb="00008000"/>
  <rgbColor rgb="00000080"/>
  <rgbColor rgb="00808000"/>
  <rgbColor rgb="00800080"/>
  <rgbColor rgb="00008080"/>
  <rgbColor rgb="00C0C0C0"/>
  <rgbColor rgb="00808080"/>
  <rgbColor rgb="009999FF"/>
  <rgbColor rgb="00993366"/>
  <rgbColor rgb="00FFFFCC"/>
  <rgbColor rgb="00CCFFFF"/>
  <rgbColor rgb="00660066"/>
  <rgbColor rgb="00FF8080"/>
  <rgbColor rgb="000066CC"/>
  <rgbColor rgb="00CCCCFF"/>
  <rgbColor rgb="00000080"/>
  <rgbColor rgb="00FF00FF"/>
  <rgbColor rgb="00FFFF00"/>
  <rgbColor rgb="0000FFFF"/>
  <rgbColor rgb="00800080"/>
  <rgbColor rgb="00800000"/>
  <rgbColor rgb="00008080"/>
  <rgbColor rgb="000000FF"/>
  <rgbColor rgb="0000CCFF"/>
  <rgbColor rgb="00CCFFFF"/>
  <rgbColor rgb="00CCFFCC"/>
  <rgbColor rgb="00FFFF99"/>
  <rgbColor rgb="0099CCFF"/>
  <rgbColor rgb="00FF99CC"/>
  <rgbColor rgb="00CC99FF"/>
  <rgbColor rgb="00FFCC99"/>
  <rgbColor rgb="003366FF"/>
  <rgbColor rgb="0033CCCC"/>
  <rgbColor rgb="0099CC00"/>
  <rgbColor rgb="00FFCC00"/>
  <rgbColor rgb="00FF9900"/>
  <rgbColor rgb="00FF6600"/>
  <rgbColor rgb="00666699"/>
  <rgbColor rgb="00969696"/>
  <rgbColor rgb="00003366"/>
  <rgbColor rgb="00339966"/>
  <rgbColor rgb="00003300"/>
  <rgbColor rgb="00333300"/>
  <rgbColor rgb="00993300"/>
  <rgbColor rgb="00993366"/>
  <rgbColor rgb="00333399"/>
  <rgbColor rgb="00333333"/>
  </indexedColors>
  </colors>
  <extLst>
  <ext xmlns:x14="http://schemas.microsoft.com/office/spreadsheetml/2009/9/main" uri="{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}">
  <x14:slicerStyles defaultSlicerStyle="SlicerStyleLight1"/>
  </ext>
  <ext xmlns:x15="http://schemas.microsoft.com/office/spreadsheetml/2010/11/main" uri="{9260A510-F301-46a8-8635-F512D64BE5F5}">
  <x15:timelineStyles defaultTimelineStyle="TimeSlicerStyleLight1"/>
  </ext>
  </extLst>
  </styleSheet>`;

    defaultValues = {
        numFmtId: 0,
        fontId: 1,
        fillId: 0,
        alineamiento: "left",
        fuente: {
            tipo: "Arial",
            color: "FF000000",
            negrita: false,
            tamanio: 8,
        },
        repetirCabeceraDetalle: false,
    };

    constructor(options, datatableInstance) {
        this.datatableInstance = datatableInstance;
        this.options = options;
    }

    formatDefault(element) {
        let s = 4;
        let currentClassProperty = element.class ?? element.className;

        if (!currentClassProperty) return s;

        if (currentClassProperty.includes("text-center")) s = 4;
        else if (currentClassProperty.includes("text-start")) s = 5;
        else if (currentClassProperty.includes("text-end")) s = 6;
        return s;
    }

    crearEstilo(estilo) {
        let { numFmtId, fontId, fillId, alineamiento, formatoNumero, fuente, colorFondo } = estilo;

        numFmtId = numFmtId ?? this.crearFormatoNumero(formatoNumero) ?? this.defaultValues.numFmtId;
        fontId = fontId ?? this.crearFuente(fuente) ?? this.defaultValues.fontId;
        fillId = fillId ?? this.crearFondo(colorFondo) ?? this.defaultValues.fillId;
        alineamiento = alineamiento ?? this.defaultValues.alineamiento;

        let plantilla = `<xf numFmtId="${numFmtId}" fontId="${fontId}" fillId="${fillId}" borderId="1" xfId="0" applyNumberFormat="1" applyFont="1" applyFill="1" applyBorder="1" applyAlignment="1" applyProtection="1"><alignment horizontal="${alineamiento}"/></xf>`;
        $("cellXfs", this.hojaDeEstilos).append(plantilla);
        return $("cellXfs", this.hojaDeEstilos).children().length - 1;
    }

    crearFormatoNumero(formatoNumero) {
        if (!formatoNumero) return formatoNumero;
        let plantilla = `<numFmt numFmtId="180" formatCode="${formatoNumero}"/>`;

        $("numFmts", this.hojaDeEstilos).append(plantilla);
        return $("numFmts", this.hojaDeEstilos).children().length - 1;
    }

    crearFuente(fuente) {
        if (!fuente) return fuente;
        let { tipo, color, negrita, tamanio } = fuente;
        tipo = tipo ?? this.defaultValues.fuente.tipo;
        color = color ?? this.defaultValues.fuente.color;
        negrita = negrita ?? this.defaultValues.fuente.negrita;
        tamanio = tamanio ?? this.defaultValues.fuente.tamanio;
        let plantilla = `<font>
                            ${negrita ? "<b/>" : ""}
                            <sz val="${tamanio}"/>
                            <color rgb="${color.length == 6 ? "FF" + color : color}"/>
                            <name val="${tipo}"/>
                        </font>`;

        $("fonts", this.hojaDeEstilos).append(plantilla);
        return $("fonts", this.hojaDeEstilos).children().length - 1;
    }

    crearFondo(colorFondo) {
        if (!colorFondo) return colorFondo;
        let plantilla = `<fill>
                            <patternFill patternType="solid">
                                <fgColor rgb="${colorFondo.length == 6 ? "FF" + colorFondo : colorFondo}"/>
                                <bgColor indexed="64"/>
                            </patternFill>
                        </fill>`;

        $("fills", this.hojaDeEstilos).append(plantilla);
        return $("fills", this.hojaDeEstilos).children().length - 1;
    }

    createBodyData(data, showedColumns) {
        return data.map(p => {
            return showedColumns.map(col => {
                if (col.x.render instanceof Function && col.x.render.length > 2) {
                    return col.x.render(p[col.x.data]);
                }

                return p[col.x.data];
            });
        });
    }

    updateDataDetail(data, showedColumns, propiedadDetalle) {
        data.map(p => p[propiedadDetalle]?.forEach(x => {
            showedColumns.forEach(col => {
                if (col.x.render instanceof Function && col.x.render.length > 2) {
                    x[col.x.data] = col.x.render(x[col.x.data]);
                }
            })
        }));
    }

    generateExportButton(getDataFromServer) {
        let that = this;
        let columns = this.datatableInstance.options.columns;

        let { tabla, footer, footerStyle, llaves, alignHeader, formatData, columnasDetalle, datos, propiedadDetalle, repetirCabeceraDetalle, nombreReporte } = this.options;

        if (this.datatableInstance.options.version === "1.1" && !columnasDetalle) {
            columnasDetalle = this.datatableInstance.options.childTableProperties.columns;
        }



        alignHeader = alignHeader ?? "start";
        propiedadDetalle = propiedadDetalle ?? "Detalle";
        formatData = formatData ?? this.formatDefault;
        repetirCabeceraDetalle = repetirCabeceraDetalle ?? false;
        nombreReporte = nombreReporte ?? $("title").text();

        let parentRowsIndex = [0];
        let secondRowsIndex = [0];

        let columnasAux = columns
                    .map((x, y) => {
                        return { y, x };
                    })
                    .filter((x, y) => (x.x.visible == undefined || x.x.visible || x.x.export) && (x.x.export == undefined || x.x.export));

        let columnasExportar = columnasAux.map((x) => x.y);

        let dataFooter = $(that.datatableInstance.tableId + " tfoot tr th").toArray().map(x=>x.innerText).filter((value, index)=> columnasExportar.find(x=>x===index));
        footer = footer === undefined ? (dataFooter.length > 0) : (footer && dataFooter.length > 0);


        return {
            extend: "excelHtml5",
            footer: footer,
            title: "",
            filename: function () {
                return nombreReporte;
            },
            customizeData: function (data) {
                let columnasPadre = columns;
                let columnasAux = columnasPadre
                    .map((x, y) => {
                        return { y, x };
                    })
                    .filter((x, y) => (x.x.visible == undefined || x.x.visible || x.x.export) && (x.x.export == undefined || x.x.export));

                let titulosColumnasExportar = columnasAux.map((x) => x.x.title);

                if (getDataFromServer instanceof Function) {
                    datos = getDataFromServer();
                }

                if (!datos) {
                    datos = that.datatableInstance.datatableObject.data().toArray();
                }

                datos.forEach(x => { columnasPadre.forEach(y => { if (y.longNumber) x[y.data] = '_' + x[y.data] }) });

                
                data.body = that.createBodyData(datos, columnasAux);
                data.header = titulosColumnasExportar;


                if (columnasDetalle) {
                    let columnasAuxDetalle = columnasDetalle
                        .map((x, y) => {
                            return { y, x };
                        })
                        .filter((x, y) => (x.x.visible == undefined || x.x.visible || x.x.export) && (x.x.export == undefined || x.x.export));

                    columnasDetalle = columnasAuxDetalle.map(x => x.x)
                    that.updateDataDetail(datos, columnasAuxDetalle, propiedadDetalle);
                }

                if (!columnasDetalle) return;

                let subchild = [...data.body];
                let secondTable = ["", ...columnasDetalle.map((x) => x.title)];
                let columnas = columnasDetalle.map((x) => x.data);
                let numberRow = 0;

                datos.forEach((e, i) => {
                    let filas = e[propiedadDetalle];
                    filas.forEach(x => { columnasDetalle.forEach(y => { if (y.longNumber) x[y.data] = '_' + x[y.data] }) });

                    ++numberRow;
                    parentRowsIndex.push(numberRow);

                    filas?.forEach((el, idx) => {
                        let detalle = [""];
                        Object.entries(el).forEach(([key, value]) => {
                            if (columnas.includes(key)) detalle.push(value);
                        });
                        if (repetirCabeceraDetalle || idx == 0) {
                            data.body.splice(numberRow, null, secondTable);
                            ++numberRow;
                            secondRowsIndex.push(numberRow);
                        }
                        data.body.splice(numberRow, null, detalle);
                        ++numberRow;
                    });
                });

                
                if(footer){
                    data.footer = dataFooter;
                    parentRowsIndex.push(++numberRow);
                }

            },
            customize: function (xlsx) {
                let agregarLongitud = (posicion, ancho) => $("cols", sheet).append(`<col min="${posicion}" max="${posicion}" width="${ancho}" customWidth="1"/>`);

                let style = xlsx.xl["styles.xml"];
                that.hojaDeEstilos = style;
                let s = "1";

                $("styleSheet", style).replaceWith(that.styleUwg);

                let sheet = xlsx.xl.worksheets["sheet1.xml"];
                let sheetPr = `<sheetPr><outlinePr summaryBelow="0"/></sheetPr>`;
                let position;
                let rowDataLevel1;
                let rowNumberLevel1 = 0;
                let rowDataLevel2;
                let rowNumberLevel2 = 0;

                $("worksheet", sheet).prepend(sheetPr);
                if (columnasDetalle) $("sheetFormatPr", sheet).attr("outlineLevelRow", "1");
                let numberRows = $("row", sheet).length;
                $("row", sheet).each(function (i, element) {
                    if (!parentRowsIndex.includes(i) && columnasDetalle) {
                        $(element).attr("outlineLevel", "1");
                        $(element).attr("hidden", "1");
                    }
                    s = "1";
                    if (!secondRowsIndex.includes(i)) {
                        if (parentRowsIndex.includes(i) || !columnasDetalle) {
                            rowDataLevel1 = datos[rowNumberLevel1];
                            rowNumberLevel1++;
                            rowNumberLevel2 = 0;
                            $(element)
                                .find("c")
                                .each(function (j) {
                                    position = columnasExportar[j];
                                    let columna = columns[position];
                                    if(i === numberRows -1 && footer && footerStyle)
                                        columna = footerStyle[j];
                                    $(this).removeAttr("s");
                                    if(columna.class)
                                        s = formatData(columna, rowDataLevel1,0);
                                    $(this).attr("s", s);

                                    if (columna.widthExport) agregarLongitud(j + 1, columna.widthExport);
                                    if (columna.longNumber) $(this).find('t').text($(this).text().replace('_', ''));
                                });
                        }

                        else {
                            if (columnasDetalle) {
                                rowDataLevel2 = datos[rowNumberLevel1-1][propiedadDetalle][rowNumberLevel2];
                                rowNumberLevel2++;
                                $(element)
                                    .find("c")
                                    .each(function (j) {
                                        if (j > 0) {
                                            let columnaDetalle = columnasDetalle[j - 1];
                                            $(this).removeAttr("s");
                                            if(columnaDetalle.class)
                                                s = formatData(columnaDetalle,rowDataLevel2,1);
                                            $(this).attr("s", s);

                                            if (columnaDetalle.widthExport) agregarLongitud(j + 1, columnaDetalle.widthExport);
                                            if (columnaDetalle.longNumber) $(this).find('t').text($(this).text().replace('_', ''));
                                        }
                                    });
                            }
                        }
                    } else {
                        if (alignHeader === "center") s = "1";
                        else if (alignHeader === "start") s = "2";
                        else if (alignHeader === "end") s = "3";

                        $(element)
                            .find("c")
                            .each(function (k) {
                                if (i == 0 || k > 0) {
                                    $(this).attr("s", s);
                                }
                            });
                    }
                });
            },
            createEmptyCells: true,
        };
    }
}

class BaseDataTable {
    #_tableId;
    #datatableObject;
    #_options;
    #_nestedDatatables = {};
    #_parentTable;

    exportExcel;

    #defaults = {
        version: "1.0",
        dom: "t",
        ordering: false,
        searching: false,
        paging: false,
        editMode: "dblclick",
        shareSelectionWithChildren: true,
        renderAfterUpdate: true,
        initialExpanded: false,
        innerContentCss: {
            className: "ps-5",
            styles: { "padding-right": "62%" },
        },
        colResize: {
            isEnabled: true,
            saveState: false,
            hasBoundCheck: true,
        },
        language: {
            emptyTable: "",
            zeroRecords: "",
        },
    };

    get parentTable() {
        return this.#_parentTable;
    }

    set parentTable(value) {
        this.#_parentTable = value;
    }

    get tableId() {
        return this.#_tableId;
    }

    set tableId(value) {
        this.#_tableId = value;
    }

    get options() {
        return this.#_options;
    }

    set options(value) {
        this.#_options = value;
    }

    get datatableObject() {
        return this.#datatableObject;
    }

    get nestedDatatables() {
        return this.#_nestedDatatables;
    }

    getSelectRowDel() {
        return objSelectRowDel;
    }

    getSelectRow() {
        return objSelectRow;
    }

    constructor(tableId, options) {
        this.tableId = tableId;
        this.options = { ...this.#defaults, ...options };

        if (options.paramButtons) {
            this.exportExcel = new ExportExcel(this.options.paramButtons, this);
            this.options.buttons = [this.exportExcel.generateExportButton(this.options.paramButtons.getData)];
        }
    }

    focusCellEdit(fun_FirstItem) {
        if (this.options.aoColumns.length > 2) {
            var contf = 0;
            var tableId = this.tableId;
            $.each(this.options.aoColumns, function (a, b) {
                if (b.visible == true || b.visible == undefined) {
                    $($(tableId).find("tbody>tr").last().find("td")[contf]).html('<input id="idEditData' + b.data + '" value="" style="width:100%;z-index:1000">');
                    contf = contf + 1;
                }
            });
        } else {
            $(this.tableId).find("tbody>tr").last().find("td").html('<input id="idEditData" value="" style="width:100%;z-index:1000">');
        }
        $(this.tableId).find("tbody>tr").last().find("td").find("input").first().focus();
        objSelectRow = new Object();
        objSelectRow.Codigo = "-1";
        objSelectRow.Descripcion = "";
        if (fun_FirstItem != undefined) fun_FirstItem(objSelectRow);
    }

    reset() {
        $(this.tableId).DataTable().destroy();
        $(this.tableId).find("tbody").html("");
        $(this.tableId).find("th").css("width", "0px");
        $(".dataTable").css("width", "auto");
        $(".dataTables_scrollHeadInner").css("width", "auto");
        objSelect = null;
        objSelectIndex = -1;
        objSelectRow = null;
        objSelectRowDel = null;
    }

    init() {
        let that = this;
        this.#_parentTable = this.options.parentTable;

        this.#datatableObject = $(this.tableId).DataTable(this.#_options);

        if (this.options.clickHandler instanceof Function) {
            $(this.tableId).on("click", "tbody td", function (e) {
                let closestTable = $(this).closest("table")[0];
                if (closestTable !== that.datatableObject.table().node()) return false;

                let clickedRow = $($(this).closest("td")).closest("tr");
                let rowData = that.datatableObject.row(clickedRow).data();

                that.options.clickHandler(this, rowData);
            });
        }

        if (this.options.dblClickHandler instanceof Function) {
            $(this.tableId)
                .off("dblclick")
                .on("dblclick", "tbody tr td", function (e) {
                    let closestTable = $(this).closest("table")[0];
                    if (closestTable !== that.datatableObject.table().node()) return false;

                    let clickedRow = $($(this).closest("td")).closest("tr");
                    let rowData = that.datatableObject.row(clickedRow).data();
                    that.options.dblClickHandler(this, rowData);
                });
        }
        if (this.options.dblClickHandlerCellEdit instanceof Function) {
            $(this.tableId)
                .off("dblclick")
                .on("dblclick", "tbody tr td", function (e) {
                    let closestTable = $(this).closest("table")[0];
                    if (closestTable !== that.datatableObject.table().node()) return false;

                    let clickedRow = $($(this).closest("td")).closest("tr");
                    let rowData = that.datatableObject.row(clickedRow).data();

                    $(this.tableId).find("tr").removeClass("selected");
                    $(this).parent().addClass("selected");

                    var table = $(this.tableId).DataTable();
                    //var data = table.cell(this).data();
                    var data = e.target.innerText;
                    var rowIndex = this._DT_CellIndex.row;
                    //var rowData2 = table.cell(this).row(rowIndex).data();
                    if (rowIndex === objSelectIndex) {
                        return;
                    }
                    if (objSelect != null) {
                        if (that.options.aoColumns.length > 2) {
                            var contf = 0;
                            $.each(that.options.aoColumns, function (a, b) {
                                if (b.visible == true || b.visible == undefined) {
                                    $(objSelect[contf]).html(objSelectHTML[contf]);
                                    contf = contf + 1;
                                }
                            });
                        } else {
                            objSelect.html(objSelectHTML);
                        }
                        objSelect = null;
                        objSelectIndex = -1;
                        objSelectRow = null;
                        objSelectRowDel = null;
                    }
                    if (objSelect == null) {
                        objSelectIndex = rowIndex;
                        objSelectRow = rowData;
                        objSelectRowDel = rowData;
                        var contf = 0;
                        var coluf = $(this).parent().find("td");
                        if (that.options.aoColumns.length > 2) {
                            objSelect = coluf;
                            objSelectHTML = new Array();

                            $.each(that.options.aoColumns, function (a, b) {
                                if (b.visible == true || b.visible == undefined) {
                                    var dataCell = $(coluf[contf]).html();
                                    objSelectHTML.push(dataCell);
                                    $(coluf[contf]).html('<input id="idEditData' + b.data + '" value="' + dataCell + '" style="width:100%;z-index:1000">');
                                    contf = contf + 1;
                                }
                            });
                        } else {
                            objSelect = $(this);
                            objSelectHTML = $(this).html();

                            $(this).html('<input id="idEditData" value="' + data + '" style="width:100%;z-index:1000">');
                        }
                    }
                    that.options.dblClickHandlerCellEdit(this, rowData, rowData, objSelect, data);
                });
        }

        if (this.options.rowClickHandler instanceof Function) {
            $(this.tableId).on("click", "tbody tr", function (e) {
                let closestTable = $(this).closest("table")[0];
                if (closestTable !== that.datatableObject.table().node()) return false;

                let clickedRow = $(this);
                let rowData = that.datatableObject.row(clickedRow).data();

                that.options.rowClickHandler(this, rowData);
            });
        }

        if (this.options.rowClickHandlerCellEdit instanceof Function) {
            $(this.tableId).on("click", "tbody tr", function (e) {
                let closestTable = $(this).closest("table")[0];
                if (closestTable !== that.datatableObject.table().node()) return false;

                let clickedRow = $(this);
                let rowData = that.datatableObject.row(clickedRow).data();

                //var rowData2 = table.cell(this).row(rowIndex).data(); verificar
                var table = $(this.tableId).DataTable();
                var rowIndex = this._DT_RowIndex;
                var rowData2 = table.cell(clickedRow).row(rowIndex).data();
                objSelectRowDel = rowData;

                that.options.rowClickHandlerCellEdit(this, rowData);
            });
        }

        if (this.options.rowDblClickHandler instanceof Function) {
            $(this.tableId)
                .off("dblclick")
                .on("dblclick", "tbody tr", function (e) {
                    let closestTable = $(this).closest("table")[0];
                    if (closestTable !== that.datatableObject.table().node()) return false;

                    let clickedRow = $(this);
                    let rowData = that.datatableObject.row(clickedRow).data();

                    that.options.rowDblClickHandler(this, rowData);
                });
        }

        if (!this.options.select) {
            $(this.tableId).on("click", "tbody tr", function () {
                if (!$(this).hasClass("selected")) {
                    that.datatableObject.$("tr.selected").removeClass("selected");
                    $(this).addClass("selected");
                }
            });
        }

        $(this.tableId).on("click", "tbody td.dt-control", async function () {
            let closestTable = $(this).closest("table")[0];
            if (closestTable !== that.datatableObject.table().node()) return false;

            let tr = $(this).closest("tr");

            if (that.options.version === "1.0") {
                that.expandRow(tr);
            } else if (that.options.version === "1.1") {
                that.expandRow_V2(tr);
            }
        });

        if (that.options.shareSelectionWithChildren) {
            this.#datatableObject.on("select", (e, dt, type, indexes) => {
                if (type === "row") {
                    if (e.target === e.currentTarget) {
                        $("body").trigger("onSelected", [dt, indexes]);
                    }
                }
            });

            $("body").on("onSelected", (event, datatable, indexes) => {
                if (datatable.table().node().id !== that.datatableObject.table().node().id) {
                    this.deselectAllItems();
                }
            });
        }

        if (this.options.initialExpanded) {
            let expanders = $(this.tableId).find("tbody td.dt-control").toArray();

            if (expanders.length > 0) {
                expanders.forEach((p) => {
                    let currentTr = p.closest("tr");
                    that.expandRow($(currentTr));
                });
            }
        }
    }

    async expandRow_V2($tr) {
        let tr = $tr;
        let row = this.datatableObject.row(tr);
        let innerContentCss = this.options.innerContentCss;
        let childProperties = this.options.childTableProperties;

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass("shown");

            let nestedDatatable = this.nestedDatatables[row.id()];
            if (nestedDatatable) {
                nestedDatatable.destroy();
            }
        } else {
            let source = document.getElementById(childProperties.htmlTemplateId).innerHTML;
            let template = Handlebars.compile(source);
            row.child(template(row.data())).show();

            if (innerContentCss) {
                row.child().children("td").addClass(innerContentCss.className);
                row.child().children("td").css(innerContentCss.styles);
            }

            let tableHtml = $(row.child()).find("table");
            let nestedDataTable = childProperties.isEditableDatatable
                ? new EditableDataTable(tableHtml, childProperties)
                : new BaseDataTable(tableHtml, childProperties);

            nestedDataTable.init();

            this.nestedDatatables[row.id()] = nestedDataTable;
            tr.addClass("shown");
        }
    }

    async expandRow($tr) {
        let tr = $tr;
        let row = this.datatableObject.row(tr);
        let innerContentCss = this.options.innerContentCss;

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass("shown");

            let nestedDatatable = this.nestedDatatables[row.id()];
            if (nestedDatatable) {
                nestedDatatable.destroy();
            }
        } else {
            if (this.options.renderRowExpanded instanceof Function) {
                let expandedContent = await this.options.renderRowExpanded(row.data());
                row.child(expandedContent).show();

                if (innerContentCss) {
                    row.child().children("td").addClass(innerContentCss.className);
                    row.child().children("td").css(innerContentCss.styles);
                }

                if (this.options.onRowExpand instanceof Function) {
                    let response = await this.options.onRowExpand(row);
                    if (response instanceof BaseDataTable) {
                        this.nestedDatatables[row.id()] = response;
                    }
                }

                tr.addClass("shown");
            }
        }
    }

    deselectAllItems() {
        this.#datatableObject.rows({ selected: true }).deselect();
    }

    deselectParentItems() {
        if (!this.parentTable) return;

        this.parentTable.deselectAllItems();
        this.parentTable.deselectParentItems();
    }

    deselectChildItems() {
        let keys = Object.keys(this.#_nestedDatatables);
        if (keys.length == 0) return;

        keys.forEach((p) => {
            this.#_nestedDatatables[p].deselectAllItems();
            this.#_nestedDatatables[p].deselectChildItems();
        });
    }

    destroy() {
        $(this.tableId).off("dblclick");
        $(this.tableId).off("blur");
        $(this.tableId).off("click");
        $(this.tableId).off("keyup");
        $(this.tableId).off("input");

        this.datatableObject.destroy();
    }

    getSelectedData() {
        let that = this;
        let mainTableSelection = this.datatableObject.rows({ selected: true }).data().toArray();

        if (this.options.shareSelectionWithChildren) {
            if (mainTableSelection.length == 0) {
                let keys = Object.keys(this.nestedDatatables);
                if (keys.length == 0) return [];

                let currentSelectedChild = [];
                keys.forEach((p) => {
                    currentSelectedChild = that.nestedDatatables[p].getSelectedData();
                    if (currentSelectedChild.length > 0) {
                        return currentSelectedChild;
                    }
                });

                return currentSelectedChild;
            }
        }

        return mainTableSelection;
    }

    getRowData(rowId) {
        return this.datatableObject.row(`#${rowId}`).data();
    }

    getAllData() {
        return this.datatableObject.rows().data();
    }

    updateRowData(rowId, newRowData) {
        this.datatableObject.row(`#${rowId}`).data(newRowData);
    }

    refresh() {
        this.datatableObject.draw();
        this.datatableObject.rows().invalidate();
    }

    reload(data) {
        this.datatableObject.rows().deselect();

        if (data) {
            this.datatableObject.clear();
            this.datatableObject.rows.add(data).draw();
        }
    }

    fnResetControls() {
        $(this.tableId).off("keyup");
        $(this.tableId).off("input");

        let that = this;
        $(this.tableId).find("td.editing").removeClass("editing");

        if (this.options.renderAfterUpdate) {
            let rowsModified = $(this.tableId).find("tr").has("input, select");

            $.each(rowsModified, function (k, $row) {
                let rowData = that.datatableObject.row($row).data();
                that.datatableObject.row($row).data(rowData);
            });
        }
    }

    createStyle(style) {
        return this.exportExcel.crearEstilo(style);
    }

    export(buttonIndex=0) {
        return this.datatableObject.button(buttonIndex).trigger();
    }
}

class EditableDataTable extends BaseDataTable {
    constructor(tableId, options) {
        super(tableId, options);
    }

    init() {
        super.init();
        let that = this;

        $(this.tableId).on(this.options.editMode, "tbody td.editable:not(.editing)", function (e) {
            let closestTable = $(this).closest("table")[0];
            if (closestTable !== that.datatableObject.table().node()) return false;

            that.#editCell(this);
        });

        $(this.tableId).on("blur", "tbody tr td > *[data-field]", function (evt) {
            let closestTable = $(this).closest("table")[0];
            if (closestTable !== that.datatableObject.table().node()) return false;

            let $cell = $(this).closest("td");
            let $row = $(this).closest("tr");
            let newValue = $(this).val() ?? "";
            let cellIndexData = that.datatableObject.cell($cell).index();
            let columnOptions = that.options.columns[cellIndexData.column];
            let currentValue = that.datatableObject.cell($cell).data() ?? "";

            newValue = that.#formatValue(newValue, currentValue, columnOptions);

            if (newValue.toString() !== currentValue.toString()) {
                let isValid = true;
                if (that.options.validateValue instanceof Function) {
                    isValid = that.options.validateValue(newValue, that.datatableObject.cell($cell), that.datatableObject.row($row));
                }

                if (isValid) {
                    that.datatableObject.cell($cell).data(newValue);
                    $cell.addClass("modified");

                    if (that.options.valueChanged instanceof Function) {
                        that.options.valueChanged(currentValue, that.datatableObject.cell($cell), that.datatableObject.row($row), $cell, $row);
                    }

                    if (that.parentTable) {
                        that.parentTable.childValuechanged(currentValue, that.datatableObject.cell($cell), that.datatableObject.row($row));
                    }
                }
            }

            that.fnResetControls();
        });
    }

    childValuechanged(oldValue, $childCell, $childRow) {
        if (this.options.childValuechanged instanceof Function) {
            this.options.childValuechanged(oldValue, $childCell, $childRow);
        }
    }

    #editRow($cell) {
        this.fnResetControls();
        let clickedRow = $($($cell).closest("td")).closest("tr");

        $(clickedRow)
            .find("td")
            .each(function (index) {
                this.#editCell(this);
            });

        return clickedRow;
    }

    #editCell($cell) {
        if ($($cell).hasClass("editable")) {
            let dataValue = this.datatableObject.cell($cell).data();
            let cellIndexData = this.datatableObject.cell($cell).index();
            let columnOptions = this.options.columns[cellIndexData.column];
            let dataName = columnOptions.data;

            $($cell).addClass("editing");

            let html;
            if ($($cell).hasClass("text")) {
                html = this.#fnCreateTextBox(dataValue, dataName, columnOptions);
                $($cell).html($(html));

                $($cell).find("input").focus();
            }

            if ($($cell).hasClass("dropdown")) {
                html = this.#fnCreateDropDown(dataValue, dataName, columnOptions);

                $($cell).html($(html));
                $($cell).find("select").focus();
            }

            if ($($cell).hasClass("date")) {
                html = this.#fnCreatedate(dataValue, dataName);
                $($cell).html($(html));

                $($cell).find("input").focus();
            }
        }
    }

    #formatValue(newValue, currentValue, columnOptions) {
        if (columnOptions.datatype && (columnOptions.datatype === "decimal" || columnOptions.datatype.includes("decimal("))) {

            let precision =
                columnOptions.datatype.indexOf("(") > -1
                    ? parseInt(columnOptions.datatype.substring(columnOptions.datatype.indexOf("(") + 1, columnOptions.datatype.length - 1))
                    : this.#calculatePrecision(newValue);

            let parsedValue = new Number(newValue);

            if (isNaN(parsedValue)) return "";

            return parsedValue.toFixed(precision);
        }

        return newValue;
    }

    #calculatePrecision(value) {
        if (value.endsWith("."))
            return 1;
        else if (value.indexOf(".") < 0)
            return 0;
        else {
            return value.substring(value.indexOf(".")).length;
        }
    }

    #fnCreateTextBox(value, fieldprop, columnOptions) {
        let that = this;
        let type = columnOptions.datatype ?? "free";
        let length = columnOptions.maxlength ?? "";

        let $input = `<input class="form-control form-control-sm" data-field="${fieldprop}" type="text" value="${value ?? ""}" maxlength="${length ?? ""}" />`;

        $(this.tableId).on("keyup", $($input), this.#fnOnKeyUpHandler);

        $(this.tableId).on("input", $($input), function (event) {
            let isValid = that.#fnOnTextInputHandler(event, type);
            if (columnOptions.onTextInputHandler instanceof Function) {
                columnOptions.onTextInputHandler(columnOptions, event, isValid);
            }
        });

        return $input;
    }

    #fnOnTextInputHandler(event, datatype) {
        let isValid = true;
        let target = event.target;

        if (datatype === "integer") {
            let oldvalue = target.value;
            target.value = target.value.replace(/[^0-9]/g, "");

            if (oldvalue != target.value) isValid = false;
        } else if (datatype === "decimal" || datatype.includes('decimal(')) {
            let regex = new RegExp(/^((\d+(\.\d*)?)|(\.\d*))$/);
            let currentValue = target.value;
            isValid = regex.test(currentValue);

            if (!isValid) {
                let match = currentValue.match(/^((\d+(\.\d*)?)|(\.\d*))/g);
                target.value = match ? match[0] : "";
            }
        } else if (datatype === "alphanumeric") {
            let oldvalue = target.value;
            target.value = target.value.replace(/[^a-zA-Z ]/g, "");

            if (oldvalue != target.value) isValid = false;
        } else if (datatype === "integer-negative") {
            let regex = new RegExp(/^-?\d*(\.\d+)?$/);
            let currentValue = target.value;
            isValid = regex.test(currentValue);

            if (!isValid) {
                target.value = target.value.replace(/[^0-9\-]/g, "");
            }
        } else if (datatype === "decimal-negative" || datatype.includes('decimal-negative(')) {
            if (target.value !== "") {
                let regex = new RegExp(/^-?((\d+(\.\d*)?)|(\.\d*)|(-))$/);
                let currentValue = target.value;
                isValid = regex.test(currentValue);

                if (!isValid) {
                    let match = currentValue.match(/^-?((\d+(\.\d*)?)|(\.\d*))/g);
                    target.value = match ? match[0] : "";
                }
            }
        }
        return isValid;
    }

    #fnOnKeyUpHandler(event) {
        if (event.originalEvent.key === "Enter") {
            event.target.blur();
        }
    }

    #fnCreateDropDown(value, fieldprop, columnOptions) {
        let idColumn = columnOptions.idColumn;
        let textColumn = columnOptions.textColumn;
        let arrayList = columnOptions.list;
        value = value ?? "";

        return `<select class="form-select form-select-sm" data-field="${fieldprop}">${arrayList
            .map((p) => `<option value='${p[idColumn]}' ${p[idColumn]?.toString() === value.toString() ? "selected" : ""}>${p[textColumn]}</option>`)
            .join()}</select>`;
    }

    #fnCreatedate(value, fieldprop) {
        let date, month, year;
        let fecha;
        if (value.length == 10) {
            value = value + "T00:00:00";
        }
        var inputDate = new Date(value);
        date = inputDate.getDate();
        month = inputDate.getMonth() + 1;
        year = inputDate.getFullYear();
        date = date.toString().padStart(2, "0");
        month = month.toString().padStart(2, "0");
        fecha = year + "-" + month + "-" + date;
        //${ value }
        let $input = `<input class="form-control form-control-sm" data-field="${fieldprop}" type="date" value="${fecha}" ></input>`;
        return $input;
    }
}

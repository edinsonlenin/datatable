<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Calendario.ascx.cs" Inherits="Seriva.Bolsa.Presentacion.Utilitarios.Controles.Calendario" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<SCRIPT language="javascript" src=pathScript  ? ig_dropCalendar.js?+></SCRIPT>
<igsch:webcalendar id="SharedCalendar" runat="server">

<Layout FooterFormat="Today: {0:d}" TitleFormat="Month" PrevMonthImageUrl="ig_cal_silverP0.gif" NextMonthImageUrl="ig_cal_silverN0.gif">

<FooterStyle Height="16pt" Font-Size="8pt" ForeColor="#707377" BackgroundImage="ig_cal_silver1.gif">
</FooterStyle>

<SelectedDayStyle ForeColor="White" BackColor="#888990">
</SelectedDayStyle>

<OtherMonthDayStyle ForeColor="#888B90">
</OtherMonthDayStyle>

<NextPrevStyle BackgroundImage="ig_cal_silver2.gif">
</NextPrevStyle>

<CalendarStyle BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="Gray" BorderStyle="Solid" ForeColor="#404050" BackColor="#EFF6F8">
</CalendarStyle>

<TodayDayStyle ForeColor="Black" BackColor="#D0D2D6">
</TodayDayStyle>

<DayHeaderStyle Height="1pt" Font-Size="8pt" Font-Bold="True" ForeColor="White" BackColor="#9A98AE">
</DayHeaderStyle>

<TitleStyle Height="18pt" Font-Size="10pt" Font-Bold="True" ForeColor="#303040" BackgroundImage="ig_cal_silver2.gif" BackColor="#D8E0E2">
</TitleStyle>

</Layout>
</igsch:webcalendar>

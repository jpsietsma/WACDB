<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjaxCalendar.ascx.cs" Inherits="WAC_CustomControls.CustomControls_AjaxCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%--<asp:UpdatePanel ID="upnlCal" runat="server"  UpdateMode="Always" EnableViewState="true" >
        <ContentTemplate>
--%>
<div style="position:relative;" >
    <asp:TextBox ID="tb" runat="server" OnTextChanged="tbTextChanged" OnDataBinding="tbDataBound" ></asp:TextBox>&nbsp;<asp:ImageButton ID="imgbtn" runat="server" 
    ImageUrl="~/images/calendar.png" /><ajtk:CalendarExtender ID="calExt" CssClass="cal_Theme1" runat="server" TargetControlID="tb" 
    Format="MM/dd/yyyy" PopupButtonID="imgbtn" PopupPosition="Right"></ajtk:CalendarExtender>
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

    

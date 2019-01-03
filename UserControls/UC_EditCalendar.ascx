<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_EditCalendar.ascx.cs" Inherits="UC_EditCalendar" %>
<div style="width:250px; padding:5px; background-color:#FFFFC0; border:solid 1px #CCCCCC;">
    <asp:Calendar ID="cal" runat="server"></asp:Calendar>
    <div style="margin-top:5px;">
        <asp:DropDownList ID="ddl" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        &nbsp;
        <asp:LinkButton ID="lb" runat="server" Text="clear" OnClick="lb_Click"></asp:LinkButton>
    </div>
</div>
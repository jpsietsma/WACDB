<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_Communication_EditInsert.ascx.cs" Inherits="UC_Communication_EditInsert" %>
<div>
    Area Code: <asp:DropDownList ID="ddlAreaCode" runat="server" OnSelectedIndexChanged="ddlAreaCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    &nbsp;Number: <asp:DropDownList ID="ddlNumber" runat="server"></asp:DropDownList>
    &nbsp;[<asp:LinkButton ID="lbClear" runat="server" Text="Clear" OnClick="lbClear_Click"></asp:LinkButton>]
</div>
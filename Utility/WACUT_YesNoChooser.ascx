<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACUT_YesNoChooser.ascx.cs" Inherits="Utility_WACUT_YesNoChooser" %>

<asp:Panel ID="pnlActiveChooser" runat="server" >
    <asp:Label ID="lblYesNoChooser" runat="server" />
    <asp:RadioButtonList ID="rblYesNo" runat="server" RepeatDirection="Horizontal" >
        <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
        <asp:ListItem Text="No" Value="N"></asp:ListItem>
    </asp:RadioButtonList>
</asp:Panel>

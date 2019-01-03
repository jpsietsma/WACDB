<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_DocumentArchive_orig.ascx.cs" Inherits="UC_DocumentArchive_orig" %>
<asp:Panel ID="pnlTop" runat="server">
    <div style="margin-bottom:5px;">
        <span class="B">Document Archive</span> [<asp:LinkButton ID="lbUpload" runat="server" Text="Upload Document"></asp:LinkButton> | <asp:LinkButton ID="lRefresh" runat="server" Text="Refresh Document List" OnClick="lbRefresh_Click"></asp:LinkButton>]
    </div>
</asp:Panel>
<asp:LinkButton ID="lb" runat="server" OnClick="lb_Click"></asp:LinkButton>
<asp:Panel ID="pnl" runat="server" Visible="false">
    <asp:Label ID="lbl" runat="server"></asp:Label>
</asp:Panel>
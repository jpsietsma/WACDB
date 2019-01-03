<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_DropDownListByAlphabet.ascx.cs" Inherits="UC_DropDownListByAlphabet" %>
<div>
    <div><asp:Label ID="lblTitle" runat="server"></asp:Label></div>
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbSearchName_Click" CommandArgument="A" Text="A"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbSearchName_Click" CommandArgument="B" Text="B"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lbSearchName_Click" CommandArgument="C" Text="C"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbSearchName_Click" CommandArgument="D" Text="D"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="lbSearchName_Click" CommandArgument="E" Text="E"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lbSearchName_Click" CommandArgument="F" Text="F"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="lbSearchName_Click" CommandArgument="G" Text="G"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="lbSearchName_Click" CommandArgument="H" Text="H"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="lbSearchName_Click" CommandArgument="I" Text="I"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="lbSearchName_Click" CommandArgument="J" Text="J"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="lbSearchName_Click" CommandArgument="K" Text="K"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton12" runat="server" OnClick="lbSearchName_Click" CommandArgument="L" Text="L"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="lbSearchName_Click" CommandArgument="M" Text="M"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton14" runat="server" OnClick="lbSearchName_Click" CommandArgument="N" Text="N"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton15" runat="server" OnClick="lbSearchName_Click" CommandArgument="O" Text="O"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton16" runat="server" OnClick="lbSearchName_Click" CommandArgument="P" Text="P"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton17" runat="server" OnClick="lbSearchName_Click" CommandArgument="Q" Text="Q"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton18" runat="server" OnClick="lbSearchName_Click" CommandArgument="R" Text="R"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton19" runat="server" OnClick="lbSearchName_Click" CommandArgument="S" Text="S"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton20" runat="server" OnClick="lbSearchName_Click" CommandArgument="T" Text="T"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton21" runat="server" OnClick="lbSearchName_Click" CommandArgument="U" Text="U"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton22" runat="server" OnClick="lbSearchName_Click" CommandArgument="V" Text="V"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton23" runat="server" OnClick="lbSearchName_Click" CommandArgument="W" Text="W"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton24" runat="server" OnClick="lbSearchName_Click" CommandArgument="X" Text="X"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton25" runat="server" OnClick="lbSearchName_Click" CommandArgument="Y" Text="Y"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton26" runat="server" OnClick="lbSearchName_Click" CommandArgument="Z" Text="Z"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton27" runat="server" OnClick="lbSearchName_Click" CommandArgument="1" Text="Number" Visible="false"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton28" runat="server" OnClick="lbSearchName_Click" CommandArgument="ORG" Text="Organization" Visible="false"></asp:LinkButton>&nbsp;
    <asp:Panel ID="pnl" runat="server"><div style="padding-top:3px;"><asp:Label ID="lblLetter" runat="server" Font-Bold="true">
    </asp:Label>&nbsp;<asp:DropDownList ID="ddl" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div></asp:Panel>
</div>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_ParticipantAlphaPicker.ascx.cs" Inherits="UC_ParticipantAlphaPicker" %>

<asp:Panel ID="pnlOuter" runat="server">

<div>
    <div><asp:Label ID="lblTitle" Text="Select first letter of contact last name, or organization to narrow search:" runat="server"></asp:Label></div>
    <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="A" CommandName="A" Text="A"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton2" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="B" CommandName="B" Text="B"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton3" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="C" CommandName="C" Text="C"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton4" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="D" CommandName="D" Text="D"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton5" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="E" CommandName="E" Text="E"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton6" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="F" CommandName="F" Text="F"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton7" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="G" CommandName="G" Text="G"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton8" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="H" CommandName="H" Text="H"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton9" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="I" CommandName="I" Text="I"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton10" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="J" CommandName="J" Text="J"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton11" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="K" CommandName="K" Text="K"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton12" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="L" CommandName="L" Text="L"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton13" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="M" CommandName="M" Text="M"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton14" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="N" CommandName="N" Text="N"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton15" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="O" CommandName="O" Text="O"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton16" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="P" CommandName="P" Text="P"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton17" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="Q" CommandName="Q" Text="Q"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton18" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="R" CommandName="R" Text="R"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton19" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="S" CommandName="S" Text="S"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton20" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="T" CommandName="T" Text="T"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton21" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="U" CommandName="U" Text="U"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton22" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="V" CommandName="V" Text="V"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton23" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="W" CommandName="W" Text="W"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton24" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="X" CommandName="X" Text="X"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton25" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="Y" CommandName="Y" Text="Y"></asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LinkButton26" runat="server" OnCommand="lbStartsWith_Click" CommandArgument="Z" CommandName="Z" Text="Z"></asp:LinkButton>&nbsp;
    <div style="padding-top:3px;">
        <asp:DropDownList ID="ddlParticipant" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlParticipant_SelectedIndexChanged" 
            ViewStateMode="Disabled" ></asp:DropDownList>

    </div>
</div>
</asp:Panel>

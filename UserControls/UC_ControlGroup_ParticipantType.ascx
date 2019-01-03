<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_ControlGroup_ParticipantType.ascx.cs" Inherits="UC_ControlGroup_ParticipantType" %>
<div style="margin-bottom:5px;">
    <div style="float:left;" class="B fsM">Participant Types</div>
    <div style="float:right;" class="B">Insert a Participant Type: <asp:DropDownList ID="ddlInsert" runat="server" OnSelectedIndexChanged="ddlInsert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
    <div style="clear:both;"></div>
</div>
<asp:HiddenField ID="hfPK_Participant" runat="server" Value='<%# Eval("pk_participant") %>' />
<asp:ListView ID="lvParticipant_Properties" runat="server" DataSource='<%# Eval("participantTypes") %>'>
    <EmptyDataTemplate><div class="I">No Participant Type Records</div></EmptyDataTemplate>
    <LayoutTemplate>
        <table cellpadding="5" rules="cols">
            <tr valign="top">
                <td class="B U"></td>
                <td class="B U">Type</td>
            </tr>
            <tr id="itemPlaceholder" runat="server"></tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr valign="top">
            <td>[<asp:LinkButton ID="lbDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_participantType") %>' OnClick="lbDelete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>]</td>
            <td><%# Eval("list_participantType.participantType") %></td>
        </tr>
    </ItemTemplate>
</asp:ListView>
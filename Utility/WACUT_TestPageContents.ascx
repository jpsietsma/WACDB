<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACUT_TestPageContents.ascx.cs" Inherits="Utility_WACUT_TestPageContents" %>
<%@ Register Src="~/Participant/WACPT_ParticipantListSearch.ascx" TagPrefix="uc1" TagName="WACPT_ParticipantListSearch" %>
<div style="margin-top: 10px;">
    <asp:Panel ID="pnlParticipantListSearch" runat="server">
        <uc1:WACPT_ParticipantListSearch runat="server" ID="WACPT_ParticipantListSearch" />
    </asp:Panel>
</div>

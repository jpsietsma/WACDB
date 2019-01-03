<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPT_ParticipantListSearch.ascx.cs" Inherits="Participant_WACPT_ParticipantListSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>

<asp:literal ID="Literal1" runat="server">Participant/Organization (start typing to narrow search):</asp:literal><br />
<asp:TextBox ID="tbParticipants" runat="server"></asp:TextBox>
    <ajtk:AutoCompleteExtender 
    ServiceMethod="GetParticipantOrgList" 
    MinimumPrefixLength="2" 
    CompletionInterval="100" EnableCaching="true" CompletionSetCount="30"
    TargetControlID="tbParticipants"
    ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" UseContextKey="True"  >
</ajtk:AutoCompleteExtender>



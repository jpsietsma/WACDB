<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPT_ParticipantCreate.ascx.cs" Inherits="Participant_WACPT_ParticipantCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>



<asp:Panel ID="pnlParticipant_add_rs" runat="server" CssClass="ModalPopup_Panel_Tall" ScrollBars="Vertical">
    <asp:UpdatePanel ID="upParticipant_add_rs" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="fsM B" style="float:left;">Participant >> Create</div>
            <div style="float:right;">[<asp:LinkButton ID="lbParticipant_add_rs_Close" runat="server" Text="Close" onclick="lbParticipant_add_rs_Close_Click"></asp:LinkButton>]</div>
            <div style="clear:both;"></div>
            <hr />

<asp:FormView ID="fvParticipantCreate" runat="server" Width="100%" OnModeChanging="fvParticipantCreate_ModeChanging" OnItemInserting="fvParticipantCreate_ItemInserting">
    <ItemTemplate></ItemTemplate>
    <InsertItemTemplate>
            <div>[<asp:LinkButton ID="lbParticipant_add_rs_Insert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton>]</div>
            <hr />
            <table class="tp3">
                <tr class="taT"><td class="B taR">Prefix:</td><td><asp:DropDownList ID="ddlPrefix" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">First Name:</td><td><asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Last Name:</td><td><asp:TextBox ID="tbLastName" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Suffix Name:</td><td><asp:DropDownList ID="ddlSuffix" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR" colspan="2"><hr /></td></tr>
<%--                <tr class="taT"><td class="B taR">Organization:</td><td><asp:DropDownList ID="ddlOrganization" runat="server"></asp:DropDownList></td></tr>--%>
                <tr class="taT"><td class="B taR">Organization:</td><td><asp:TextBox ID="tbOrganization" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">DBA:</td><td><asp:TextBox ID="tbDBA" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR" colspan="2"><hr /></td></tr>
                <tr class="taT"><td class="B taR">Address Type:</td><td><asp:DropDownList ID="ddlAddressType" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Address Base:</td><td><asp:TextBox ID="tbAddressBase" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Address Number:</td><td><asp:TextBox ID="tbAddressNumber" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">USPS Abbr:</td><td><asp:DropDownList ID="ddlUSPSAbbr" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Zip Code:</td><td><asp:TextBox ID="tbZipCode" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR" colspan="2"><hr /></td></tr>
                <tr class="taT"><td class="B taR">Communication Type:</td><td><asp:DropDownList ID="ddlCommunicationType" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Communication Usage:</td><td><asp:DropDownList ID="ddlCommunicationUsage" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbPhoneNumber" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR" colspan="2"><hr /></td></tr>
                <tr class="taT"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Participant Type:</td><td><asp:DropDownList ID="ddlParticipantType" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlWACRegion" runat="server"></asp:DropDownList></td></tr>
            </table>
    </InsertItemTemplate>
</asp:FormView>
                    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
<asp:LinkButton ID="lbHidden_Participant_add_rs" runat="server"></asp:LinkButton>
<ajtk:ModalPopupExtender ID="mpeParticipant_add_rs" runat="server" TargetControlID="lbHidden_Participant_add_rs" PopupControlID="pnlParticipant_add_rs" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>


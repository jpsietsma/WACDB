<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_FarmBusinessForm.ascx.cs" Inherits="AG_WACAG_FarmBusinessForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/Participant/WACPT_ParticipantAlphaPicker.ascx" TagPrefix="uc1" TagName="WACPT_ParticipantAlphaPicker" %>
<%@ Register Src="~/Property/WACPR_TaxParcelPicker.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelPicker" %>

<div style="margin-top:10px;">
    <asp:Panel ID="pnlFarmBusiness" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
        <asp:UpdatePanel ID="upFarmBusiness" runat="server" UpdateMode="Conditional">
            <ContentTemplate>  
                <asp:FormView ID="fvFarmBusiness" runat="server" Width="100%" OnItemCommand="fvItemCommand" >
                    <ItemTemplate>
                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;"></div>   
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;"></div> 
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                            <div><span class="fsM B">Farm Business</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton> | 
                                <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>]</div>
                            <hr />
                            <table class="tp3">
                                <tr class="taT"><td class="B taR">Tax Parcel:</td><td><br />
                                    <uc1:WACPR_TaxParcelPicker runat="server" ID="WACPR_TaxParcelPicker" NewTaxParcelsOnly="false"
                                        IsActiveInsert="true" IsActiveReadOnly="false" IsActiveUpdate="false" DefaultVisibility="false" />
                                </td></tr>  
                                 <tr class="taT"><td class="B taR">Owner/Producer:</td><td>
                                    <uc1:WACPT_ParticipantAlphaPicker runat="server" ID="WACPT_ParticipantAlphaPicker_Owner" ListType="ParticipantOrganization"
                                        OnParticipantAlphaPicker_Clicked="WACPT_ParticipantAlphaPicker_Owner_ParticipantAlphaPicker_Clicked" 
                                        DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="false" IsActiveUpdate="false" />
                                </td></tr>           
                                <tr class="taT"><td class="B taR">Operator:</td><td>
                                    <uc1:WACPT_ParticipantAlphaPicker runat="server" ID="WACPT_ParticipantAlphaPicker" ListType="Operator" 
                                        OnParticipantAlphaPicker_Clicked="WACPT_ParticipantAlphaPicker_ParticipantAlphaPicker_Clicked" 
                                        DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="false" IsActiveUpdate="false" />
                                </td></tr>
                                <tr class="taT"><td class="B taR">Farm Name:</td><td><asp:TextBox ID="tbFarmName" runat="server" Text='<%# Bind("farm_name") %>' Width="400px"></asp:TextBox></td></tr>                  
                                <tr class="taT"><td class="B taR">WAC Program:</td><td><asp:DropDownList ID="ddlProgramWAC" runat="server"></asp:DropDownList></td></tr>
                                <tr class="taT"><td class="B taR">Farm Size:</td><td><asp:DropDownList ID="ddlFarmSize" runat="server"></asp:DropDownList></td></tr>
                                <tr class="taT"><td class="B taR">Basin:</td><td><asp:DropDownList ID="ddlBasin" runat="server" ></asp:DropDownList></td></tr>
                                <tr class="taT"><td class="B taR">Sold Farm:</td><td><asp:DropDownList ID="ddlSoldFarm" runat="server"></asp:DropDownList></td></tr>
                                <tr class="taT"><td class="B taR">Generate FarmID:</td><td><asp:CheckBox ID="cbGenerateFarmID" runat="server" /></td></tr>
                            </table>
                        </div>
                    </InsertItemTemplate>
                </asp:FormView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>   
    <asp:LinkButton ID="lbHiddenFarmBusiness" runat="server"></asp:LinkButton>
    <ajtk:ModalPopupExtender ID="mpeFarmBusiness" runat="server" TargetControlID="lbHiddenFarmBusiness" PopupControlID="pnlFarmBusiness" 
        BackgroundCssClass="ModalPopup_BG">
    </ajtk:ModalPopupExtender>       
</div>
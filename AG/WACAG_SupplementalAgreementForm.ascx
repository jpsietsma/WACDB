<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_SupplementalAgreementForm.ascx.cs" Inherits="AG_WACAG_SupplementalAgreementForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/Property/WACPR_TaxParcelPicker.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelPicker" %>
<%@ Register Src="~/CustomControls/AjaxCalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<div style="margin-top:10px;">
    <asp:Panel ID="pnlSA" runat="server" CssClass="ModalPopup_Panel_Medium" ScrollBars="Vertical">
        <asp:UpdatePanel ID="upSupplementalAgreement" runat="server" UpdateMode="Conditional">
            <ContentTemplate>  
                <asp:FormView ID="fvSA" runat="server" Width="100%" OnItemCommand="fvItemCommand" >
                    <ItemTemplate>
                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;"></div>   
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;"></div> 
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                            <div><span class="fsM B">Supplemental Agreement</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton> | 
                                <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>]</div>
                            <hr />
                            <table class="tp3">
                                <tr class="taT"><td class="B taR">Tax Parcel:</td><td><br />
                                    <uc1:WACPR_TaxParcelPicker runat="server" ID="WACPR_TaxParcelPicker" NewTaxParcelsOnly="false"
                                        IsActiveInsert="true" IsActiveReadOnly="false" IsActiveUpdate="false" DefaultVisibility="false" />
                                </td></tr>             
                                <tr class="taT"><td class="B taR">Agreement Date:</td><td><br />
                                    <uc1:AjaxCalendar runat="server" ID="tbCalAgreementDate"  />
                                </td></tr>                                        
                            </table>
                        </div>
                    </InsertItemTemplate>
                </asp:FormView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>   
    <asp:LinkButton ID="lbHiddenSA" runat="server"></asp:LinkButton>
    <ajtk:ModalPopupExtender ID="mpeSA" runat="server" TargetControlID="lbHiddenSA" PopupControlID="pnlSA" 
        BackgroundCssClass="ModalPopup_BG">
    </ajtk:ModalPopupExtender>       
</div>
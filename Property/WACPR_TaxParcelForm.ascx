<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelForm.ascx.cs" Inherits="Property_WACPR_TaxParcelForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%--<%@ Register Src="~/UserControls/UC_DocumentArchive.ascx" TagPrefix="uc1" TagName="UC_DocumentArchive" %>--%>
<%@ Register Src="~/Utility/wacut_associations.ascx" TagPrefix="uc1" TagName="WACUT_Associations" %>
<%@ Register Src="~/Property/WACPR_TaxParcelPicker.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelPicker" %>
<%@ Register Src="~/Property/WACPR_TaxParcelOwnerContainer.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelOwnerContainer" %>
<%@ Register Src="~/Utility/WACUT_AttachedDocumentViewer.ascx" TagPrefix="uc1" TagName="WACUT_AttachedDocumentViewer" %>


<asp:Panel ID="pnlTaxParcelModal" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
    <asp:UpdatePanel ID="upTaxParcelForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="fvTaxParcel" runat="server" Width="100%" OnItemCommand="fvItemCommand" >
                <ItemTemplate>
                    <div>
                        <span class="fsM B">Tax Parcel</span> 
                        [ <asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="EditData" CommandArgument=<%#Eval("pk_taxParcel") %> Text="Edit"></asp:LinkButton> 
                        | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" CommandArgument=<%#Eval("pk_taxParcel") %> Text="Delete" 
                            OnClientClick="return confirm_delete();"></asp:LinkButton>
                        | <asp:LinkButton ID="lbClose" runat="server" CausesValidation="false" CommandName="CloseForm" Text="Close"></asp:LinkButton> ] 
                        <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_taxParcel"), Eval("created"), Eval("created_by"), 
                                                    Eval("modified"), Eval("modified_by"))%></span></div>
                        <hr />
                        <div>       
                        <%--  <div class="DivBoxOrange" style="margin: 5px 0px 5px 0px;">
                            <uc1:UC_DocumentArchive runat="server" ID="UC_DocumentArchive_TP" StrArea="TP" />
                        </div>--%>
                            <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewer" SectorCode="TP"
                                 DefaultVisibility="false" IsActiveInsert="false" IsActiveReadOnly="true" IsActiveUpdate="false" />
                            <uc1:WACUT_Associations runat="server" ID="WACUT_Associations" AssociationType="TaxParcel"
                                OnContentStateChanged="WACUT_Associations_ListContentsChanged" 
                                DefaultVisibility="false" IsActiveInsert="false" IsActiveReadOnly="true" IsActiveUpdate="false"  />
                        <table class="tp3">
                            <tr class="taT"><td class="B taR">Tax Parcel ID (Print Key):</td><td><%# Eval("taxParcelID") %></td></tr>
                            <tr class="taT"><td class="B taR">SBL (Section Block Lot):</td><td><%# Eval("SBL") %></td></tr>
                            <tr class="taT"><td class="B taR">County:</td><td><%# Eval("Swis.county")%></td></tr>
                            <tr class="taT"><td class="B taR">Jurisdiction:</td><td><%# Eval("Swis.jurisdiction")%></td></tr>
                            <tr class="taT"><td class="B taR">SWIS:</td><td><%# Eval("fk_list_swis")%></td></tr>
                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                        </table>      
                        </div>
                      <%--  <hr />
                        <uc1:WACPR_TaxParcelOwnerContainer runat="server" ID="WACPR_TaxParcelOwnerContainer" IsActiveReadOnly="true" 
                    IsActiveUpdate="false" IsActiveInsert="false" DefaultVisibility="false" />--%>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="fvTemplate">
                        <div><span class="fsM B">Tax Parcel</span> 
                            [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" CommandArgument=<%#Eval("pk_taxParcel") %> Text="Update"></asp:LinkButton> 
                            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>] 
                            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_taxParcel"), Eval("created"), 
                                                    Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span>
                        </div>
                        <hr />
                            <table class="tp3">
                                <tr class="taT"><td class="B taR">Tax Parcel ID (Print Key):</td><td><%# Eval("taxParcelID") %></td></tr>
                                <tr class="taT"><td class="B taR">SBL (Section Block Lot):</td><td><%# Eval("SBL") %></td></tr>
                                <tr class="taT"><td class="B taR">County:</td><td><%# Eval("Swis.county")%></td></tr>
                                <tr class="taT"><td class="B taR">Jurisdiction:</td><td><%# Eval("Swis.jurisdiction")%></td></tr>
                                <tr class="taT"><td class="B taR">SWIS:</td><td><%# Eval("fk_list_swis")%></td></tr>
                                <tr class="taT">
                                    <td class="B taR">Note:</td>
                                    <td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="6"></asp:TextBox></td>
                                </tr>
                            </table>
                    </div>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <div class="fvTemplate">
                        <div><span class="fsM B">Tax Parcel</span> 
                        [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton> 
                        | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>] 
                        <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_taxParcel"), Eval("created"), 
                                                    Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                        <hr />
                        <div style="margin-top:10px; border:1px solid #CCCCCC; background-color:#EEEEEE; padding:5px;">
                            <uc1:WACPR_TaxParcelPicker runat="server" ID="WACPR_TaxParcelPicker" OnTaxParcelPickerNotify="TaxParcelPicker_Notify"
                                 DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="false" IsActiveUpdate="false" CommandButtonsVisible="false"  />
                            <table class="tp3">
                                <tr class="taT">
                                    <td class="B taR">SBL:</td>
                                    <td><asp:Label ID="lblSBL" runat="server" Text='<%# Bind("SBL") %>' Width="250px" MaxLength="20"></asp:Label></td>
                                </tr>
                                <tr class="taT">
                                    <td class="B taR">Note:</td>
                                    <td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="6"></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>
                    </div> 
                </InsertItemTemplate>
            </asp:FormView>
            <hr />
                <uc1:WACPR_TaxParcelOwnerContainer runat="server" ID="WACPR_TaxParcelOwnerContainer" IsActiveReadOnly="true" 
                    IsActiveUpdate="false" IsActiveInsert="false" DefaultVisibility="false" />
        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Panel>
<asp:LinkButton ID="lbTaxParcelHiddenModal" runat="server"></asp:LinkButton>
<ajtk:ModalPopupExtender ID="mpeTaxParcelModal" runat="server" TargetControlID="lbTaxParcelHiddenModal"
    PopupControlID="pnlTaxParcelModal" BackgroundCssClass="ModalPopup_BG">
</ajtk:ModalPopupExtender>


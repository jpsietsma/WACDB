<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelOwnerContainer.ascx.cs" Inherits="Property_WACPR_TaxParcelOwnerContainer" %>
<%@ Register Src="~/Property/WACPR_TaxParcelOwnerGrid.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelOwnerGrid" %>
<%@ Register Src="~/Property/WACPR_TaxParcelOwnerForm.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelOwnerForm" %>
<%@ Register Src="~/Utility/WACUT_AddNewItem.ascx" TagPrefix="uc1" TagName="WACUT_AddNewItem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>

<div class="DivBoxPurple">
    <asp:UpdatePanel ID="upTaxParcelOwner" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pHeader" runat="server" CssClass="cpHeader" >
                <asp:Image ID="imgExpander" runat="server" ImageAlign="Middle" />
                <asp:literal runat="server">Tax Parcel Owners</asp:literal>
                <asp:Label ID="lblOwnerCount" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnlTaxParcelOwner" runat="server">
                <uc1:WACUT_AddNewItem runat="server" ID="WACUT_AddNewItem" OnAddNewItem_Clicked="WACUT_AddNewItem_AddNewItem_Clicked" ButtonLabel="Add Tax Parcel Owner"
                        DefaultVisibility="true" IsActiveInsert="false" IsActiveReadOnly="true" IsActiveUpdate="false" />
                <uc1:WACPR_TaxParcelOwnerGrid runat="server" ID="WACPR_TaxParcelOwnerGrid" OnContentStateChanged="WACPR_TaxParcelOwnerGrid_ContentStateChanged"
                    IsActiveReadOnly="true" IsActiveUpdate="false" IsActiveInsert="false" DefaultVisibility="true" />
                <uc1:WACPR_TaxParcelOwnerForm runat="server" ID="WACPR_TaxParcelOwnerForm" OnContentStateChanged="WACPR_TaxParcelOwnerForm_ContentStateChanged"
                     DefaultVisibility="true" IsActiveInsert="true" IsActiveReadOnly="true" IsActiveUpdate="true"/>
            </asp:Panel>
            <ajtk:CollapsiblePanelExtender ID="tpOwnerCollapsiblePanelExtender" runat="server" TargetControlID="pnlTaxParcelOwner" CollapseControlID="pHeader" 
            ExpandControlID="pHeader" Collapsed="true" TextLabelID="lblExpander" CollapsedImage="~/images/expand_24.png" 
            ExpandedImage="~/images/collapse_24.png" ImageControlID="imgExpander" CollapsedSize="0">
        </ajtk:CollapsiblePanelExtender>        
        </ContentTemplate>
    </asp:UpdatePanel>    
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelPageContents.ascx.cs" Inherits="Property_WACPR_TaxParcelPageContents" %>
<%@ Register Src="~/Property/WACPR_TaxParcelFilter.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelFilter" %>
<%@ Register Src="~/Property/WACPR_TaxParcelGrid.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelGrid" %>
<%@ Register Src="~/Property/WACPR_TaxParcelForm.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelForm" %>
<%@ Register Src="~/Utility/WACUT_AddNewItem.ascx" TagPrefix="uc1" TagName="WACUT_AddNewItem" %>


<uc1:WACUT_AddNewItem runat="server" ID="WACUT_AddNewItem" OnAddNewItem_Clicked="WACUT_AddNewItem_AddNewItem_Clicked" ButtonLabel="Add Tax Parcel"
     IsActiveInsert="false" IsActiveReadOnly="false" IsActiveUpdate="false" DefaultVisibility="true" />
<asp:UpdatePanel ID="upTaxParcel" runat="server" UpdateMode="Conditional">  
    <ContentTemplate>
        <uc1:WACPR_TaxParcelFilter runat="server" id="WACPR_TaxParcelFilter" OnContentStateChanged="WACPR_TaxParcelFilter_FilterContentsChanged"
             IsActiveInsert="false" IsActiveReadOnly="false" IsActiveUpdate="false" DefaultVisibility="true" />
        <uc1:WACPR_TaxParcelGrid runat="server" id="WACPR_TaxParcelGrid" OnContentStateChanged="WACPR_TaxParcelGrid_GridContentsChanged"
             IsActiveInsert="false" IsActiveReadOnly="false" IsActiveUpdate="false" DefaultVisibility="true" />
        <uc1:WACPR_TaxParcelForm runat="server" id="WACPR_TaxParcelForm" OnContentStateChanged="WACPR_TaxParcelForm_FormContentsChanged" 
            DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="true" IsActiveUpdate="true" CurrentState="Closed" ViewStateMode="Enabled" />
    </ContentTemplate>
</asp:UpdatePanel>
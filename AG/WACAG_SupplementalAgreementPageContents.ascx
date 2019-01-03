<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_SupplementalAgreementPageContents.ascx.cs" Inherits="AG_WACAG_SupplementalAgreementPageContents" %>
<%@ Register Src="~/Utility/WACUT_AddNewItem.ascx" TagPrefix="uc1" TagName="WACUT_AddNewItem" %>
<%@ Register Src="~/AG/WACAG_SupplementalAgreementForm.ascx" TagPrefix="uc1" TagName="WACAG_SupplementalAgreementForm" %>

<uc1:WACUT_AddNewItem runat="server" ID="WACUT_AddNewItem" OnAddNewItem_Clicked="WACUT_AddNewItem_AddNewItem_Clicked" ButtonLabel="Add New Supplemental Agreement"
     IsActiveInsert="false" IsActiveReadOnly="false" IsActiveUpdate="false" DefaultVisibility="true" />
<asp:UpdatePanel ID="upSupplementalAgreement" runat="server" UpdateMode="Conditional">  
    <ContentTemplate>
         <uc1:WACAG_SupplementalAgreementForm runat="server" ID="WACAG_SupplementalAgreementForm"  OnContentStateChanged="WACAG_SupplementalAgreementsForm_ContentStateChanged"
            DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="true" IsActiveUpdate="true" CurrentState="Closed" ViewStateMode="Enabled" />
    </ContentTemplate>
</asp:UpdatePanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_AgPageContents.ascx.cs" Inherits="AG_WACAG_AgPageContents" %>
<%@ Register Src="~/Utility/WACUT_AddNewItem.ascx" TagPrefix="uc1" TagName="WACUT_AddNewItem" %>
<%@ Register Src="~/AG/WACAG_FarmBusinessForm.ascx" TagPrefix="uc1" TagName="WACAG_FarmBusinessForm" %>

<uc1:WACUT_AddNewItem runat="server" ID="WACUT_AddNewItem" OnAddNewItem_Clicked="WACUT_AddNewItem_AddNewItem_Clicked" ButtonLabel="Add New Farm"
     IsActiveInsert="false" IsActiveReadOnly="false" IsActiveUpdate="false" DefaultVisibility="true" />
<asp:UpdatePanel ID="upFarmBusiness" runat="server" UpdateMode="Conditional">  
    <ContentTemplate>
        <uc1:WACAG_FarmBusinessForm runat="server" ID="WACAG_FarmBusinessForm" OnContentStateChanged="WACAG_FarmBusinessForm_ContentStateChanged"
            DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="true" IsActiveUpdate="true" CurrentState="Closed" ViewStateMode="Enabled" />
    </ContentTemplate>
</asp:UpdatePanel>

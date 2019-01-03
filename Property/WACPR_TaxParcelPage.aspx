<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACPR_TaxParcelPage.aspx.cs" Inherits="Property_WACPR_TaxParcelPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/Property/WACPR_TaxParcelFilter.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelFilter" %>
<%@ Register Src="~/Property/WACPR_TaxParcelGrid.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelGrid" %>
<%@ Register Src="~/Property/WACPR_TaxParcelForm.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelForm" %>
<%@ Register Src="~/Property/WACPR_TaxParcelPageContents.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelPageContents" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
    <div class="divContentClass">
      <%--  <div style="background-image: url(../images/farm_o.jpg); background-repeat: no-repeat; min-height: 250px;">--%>
            <div style="padding: 5px;">
                <div class="fsXL B">Tax Parcels</div>
                <uc1:WACPR_TaxParcelPageContents runat="server" ID="WACPR_TaxParcelPageContents" ViewStateMode="Disabled" />
            </div>
        </div>
<%--    </div>--%>
    <asp:UpdateProgress ID="UpdateProgressWaiting" runat="server" CssClass="ModalPopupWait">
        <ProgressTemplate>
            <asp:Image ID="imgWaiting" ImageUrl="~/images/ajax-loader-Purple-noback.gif" Width="100px" Height="100px" CssClass="ModalPopupWait"
                ImageAlign="AbsMiddle" AlternateText="Processing" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
   <%-- <ajtk:ModalPopupExtender ID="modalPopupWaiting" runat="server" ClientIDMode="Static" TargetControlID="UpdateProgressWaiting"
        PopupControlID="UpdateProgressWaiting" BackgroundCssClass="ModalPopupWait" />--%>
   
</asp:Content>



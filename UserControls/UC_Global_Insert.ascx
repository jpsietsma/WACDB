<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_Global_Insert.ascx.cs" Inherits="UC_Global_Insert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_Express_Global_Insert.ascx" tagname="UC_Express_Global_Insert" tagprefix="uc1" %>
<div>
    <asp:Panel ID="pnlGlobal_Insert" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
        <asp:UpdatePanel ID="upGlobal_Insert" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="float:left;"><span class="fsM B">Global Insert >> Express</span></div>
                <div style="float:right;">[<asp:LinkButton ID="lbGlobal_Insert_Close" runat="server" Text="Close" OnClick="lbGlobal_Insert_Close_Click"></asp:LinkButton>]</div>
                <div style="clear:both;"></div>
                <hr />
                <uc1:UC_Express_Global_Insert ID="UC_Express_Global_Insert1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lbHidden" runat="server"></asp:LinkButton>
    <ajtk:ModalPopupExtender ID="mpeGlobal_Insert" runat="server" TargetControlID="lbHidden" PopupControlID="pnlGlobal_Insert" BackgroundCssClass="ModalPopup_BG">
    </ajtk:ModalPopupExtender>
</div>
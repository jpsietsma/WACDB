<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelFilter.ascx.cs" Inherits="Property_WACPR_TaxParcelFilter" %>
<%@ Register Src="~/Participant/WACPT_ParticipantAlphaPicker.ascx" TagPrefix="uc1" TagName="WACPT_ParticipantAlphaPicker" %>
<%@ Register Src="~/Property/WACPR_TaxParcelPrintKeySearch.ascx" TagPrefix="uc1" TagName="WACPR_TaxParcelPrintKeySearch" %>
<%@ Register Src="~/Participant/WACPT_ParticipantListSearch.ascx" TagPrefix="uc1" TagName="WACPT_ParticipantListSearch" %>


<div class="SearchDivOuter">
    <div class="SearchDivContentContainer">
        <div class="SearchDivContent">
            <div style="float: left;">Search Options:</div>
            <div style="float: right; font-weight: normal;">
                [<asp:LinkButton ID="lbAll" runat="server" Text="All Registered Tax Parcels"
                    OnClick="lbAll_Click"></asp:LinkButton> | <asp:LinkButton ID="lbReloadReset" runat="server" Text="Reload/Reset Search Options"
                    OnClick="lbReloadReset_Click"></asp:LinkButton>]</div>
            <div style="clear: both;"></div>
            <div class="SearchDivInner">
                <div style="float: left; margin-left: 20px;">
                    <uc1:WACPR_TaxParcelPrintKeySearch runat="server" id="WACPR_TaxParcelPrintKeySearch" OnPrintKeySearch_Clicked="PrintKeySearch_Clicked" />&nbsp
              
                    <div style="float: right" class="B">Tax Parcel ID (Print Key):
                    <asp:DropDownList ID="ddlTaxParcelID" runat="server" OnSelectedIndexChanged="ddlTaxParcel_Search" AutoPostBack="true" ViewStateMode="Disabled" ></asp:DropDownList>
                    </div>
                </div>
                <div style="clear: both;"></div>
                </div>
                <hr />
                <div>
                    <div style="float: left;">
                      <%--  <uc1:WACPT_ParticipantListSearch runat="server" ID="WACPT_ParticipantListSearch" />--%>
                        <uc1:WACPT_ParticipantAlphaPicker runat="server" ID="WACPT_ParticipantAlphaPicker" 
                            OnParticipantAlphaPicker_Clicked="ParticipantAlphaPicker_Selected" ListType="TaxParcelOwner" />
                    </div>
                   <%-- <div style="float: left; margin-left: 20px;">
                        <div class="B">SBL (Section, Block, Lot):</div>
                        <asp:DropDownList ID="ddlSBL" runat="server" OnSelectedIndexChanged="ddlTaxParcel_Search" AutoPostBack="true" ></asp:DropDownList></div>--%>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>
    </div>


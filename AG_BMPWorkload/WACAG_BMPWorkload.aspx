<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACAG_BMPWorkload.aspx.cs" Inherits="AG_BMPWorkload_WACAG_BMPWorkload" %>
<%@ Register Src="~/ag_bmpworkload/wacag_bmpworkloadfilters.ascx" TagPrefix="uc" TagName="WACAG_BMPWorkloadFilters" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_EditCalendar.ascx" tagname="UC_EditCalendar" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
<div class="divContentClass">
        <div style=" background-image:url(../images/agriculture_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Agriculture >> BMP Workloads</div>
                    <div style="clear:both;"></div>
                </div>
                <uc1:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <div>
                            <div style="float:left;">&nbsp;</div>
                            <div style="float:right;">
                                <uc1:UC_Express_PageButtonsInsert ID="UC_Express_PageButtonsInsert1" runat="server" BoolShowGlobal="true" />
                            </div>
                            <div style="float:right;">
                                <asp:HyperLink ID="hlAg_Agriculture" runat="server" NavigateUrl="~/AG/WACAgriculture.aspx" Text="Agriculture" Font-Bold="true"></asp:HyperLink> |  
                                <asp:HyperLink ID="hlAg_BMP_Planning" runat="server" NavigateUrl="~/AG/WACAgriculture_BMPPlanning.aspx" Text="BMP Planning" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_BMP_Workload" runat="server" NavigateUrl="~/AG/WACAgriculture_BMPWorkloads.aspx" Text="BMP Workloads" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_Supplemental_Agreement" runat="server" NavigateUrl="~/AG/WACAgriculture_SupplementalAgreements.aspx" Text="Supplemental Agreements" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_Workload_Funding" runat="server" NavigateUrl="~/AG/WACAgriculture_WorkloadFunding.aspx" Text="Workload Funding" Font-Bold="true"></asp:HyperLink> | 
                            </div>
                            <div style="clear:both;"></div>
                        <uc:WACAG_BMPWorkloadFilters runat="server" ID="ucWACAG_BMPWorkloadFilters" />
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel> 
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


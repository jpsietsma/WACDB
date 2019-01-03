<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="~/AG/WACAgriculture_WorkloadFunding.aspx.cs" Inherits="WACAgriculture_WorkloadFunding" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(../images/agriculture_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Agriculture >> Workload Funding</div>
                    <div style="clear:both;"></div>
                </div>
                <uc1:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <div>
                            <div style="float:left;"><asp:LinkButton ID="lbAg_WorkloadFunding_Insert" runat="server" Text="Add New Workload Funding" OnClick="lbAg_WorkloadFunding_Insert_Click" Font-Bold="True"></asp:LinkButton></div>
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
                        </div>
                        <div class="SearchDivOuter">
                            <div class="SearchDivContentContainer">
                                <div class="SearchDivContent">
                                    <div style="float:left;">Filter Options:</div>
                                    <div style="float:right;">&nbsp;</div>
                                    <div style="clear:both;"></div>
                                </div>
                                <div class="SearchDivInner">
                                    <table cellpadding="5">
                                        <tr valign="top">
                                            <td class="taR B">Workload Funding:</td><td><asp:DropDownList ID="ddlFilter_Workload" runat="server" OnSelectedIndexChanged="ddlFilter_Workload_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div style="margin-top:10px;">
                            <asp:FormView ID="fv" runat="server" Width="100%" OnModeChanging="fv_ModeChanging" OnItemUpdating="fv_ItemUpdating" OnItemInserting="fv_ItemInserting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Workload</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbFV_Close" runat="server" Text="Close" OnClick="lbFV_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_agWorkloadFunding"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Year:</td><td><%# Eval("year") %></td></tr>
                                            <tr valign="top"><td class="taR B">Source:</td><td><%# Eval("list_agWorkloadFunding.source") %></td></tr>
                                            <tr valign="top"><td class="taR B">Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt")) %></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Workload</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_agWorkloadFunding"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Year:</td><td><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Source:</td><td><asp:DropDownList ID="ddlSource" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Amount:</td><td><asp:TextBox ID="tbAmt" runat="server" Text='<%# Bind("amt") %>'></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Workload</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Year:</td><td><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Source:</td><td><asp:DropDownList ID="ddlSource" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Amount:</td><td><asp:TextBox ID="tbAmt" runat="server"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


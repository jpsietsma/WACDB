<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WACAG_WFP3.aspx.cs" Inherits="AG_WFP3_WACAG_WFP3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Bmp.ascx" TagPrefix="aguc" TagName="AG_Wfp3Bmp" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Bid.ascx" TagPrefix="aguc" TagName="AG_Wfp3Bid" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Encumbrance.ascx" TagPrefix="aguc" TagName="AG_Wfp3Encumbrance" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Invoice.ascx" TagPrefix="aguc" TagName="AG_Wfp3Invoice" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Payment.ascx" TagPrefix="aguc" TagName="AG_Wfp3Payment" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3PaymentBMP.ascx" TagPrefix="aguc" TagName="AG_Wfp3PaymentBMP" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Modification.ascx" TagPrefix="aguc" TagName="AG_Wfp3Modification" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Specification.ascx" TagPrefix="aguc" TagName="AG_Wfp3Specification" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3Tech.ascx" TagPrefix="aguc" TagName="AG_Wfp3Tech" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>
<%--<%@ Register src="~/UserControls/UC_DocumentArchive.ascx" tagname="UC_DocumentArchive" tagprefix="uc1" %>--%>
<%@ Register Src="~/Utility/WACUT_AttachedDocumentViewer.ascx" TagPrefix="aguc" TagName="WACUT_AttachedDocumentViewer" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager" EnablePartialRendering="true" runat="server" />
    <telerik:RadWindowManager RenderMode="Lightweight" ID="WacRadWindowManager" runat="server" EnableShadow="true" Skin="Office2007" 
        Style="z-index: 90001 !important" />
        <div>
            <asp:Panel ID="pnlAg_WFP3" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                <asp:UpdatePanel ID="upAg_WFP3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="fsM B" style="float: left;">Agriculture >> WFP3 </div>
                        <div style="float: right;">
                            [<asp:LinkButton ID="lbAg_WFP3_Close" runat="server" Text="Close"
                                OnClick="lbAg_WFP3_Close_Click"></asp:LinkButton>]
                        </div>
                        <div style="clear: both;"></div>
                        <hr />
                        <asp:Literal ID="litAg_WFP3_Header" runat="server"></asp:Literal>
                        <aguc:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewerWFP3" SectorCode="A_WFP3" />

                        <hr />
                        <asp:FormView ID="fvAg_WFP3" runat="server" OnItemCommand="fvAg_WFP3_ItemCommand" Width="100%">
                            <ItemTemplate>
                                <div class="NestedDivLevel01">
                                    <div>
                                        [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton>
                                        |
                                        <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete"
                                            OnClientClick="return confirm_delete();"></asp:LinkButton>] 
                    <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3"), 
                                             Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span>
                                    </div>
                                    <hr />
                                    <asp:Literal ID="litAg_WFP3_Basic_Grid" runat="server"></asp:Literal>
                                    <hr />
                                    <table cellpadding="3">
                                        <tr valign="top">
                                            <td class="B taR">Package:</td>
                                            <td><%# Eval("packageName") %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Drawing Number:</td>
                                            <td><%# Eval("drawing_nbr") %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Description:</td>
                                            <td><%# Eval("description") %></td>
                                        </tr>
                                        <%-- <tr valign="top"><td class="B taR">On Hold:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("onHold")) %></td></tr>--%>
                                        <tr valign="top">
                                            <td class="B taR">Special Provisions:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("specialProvisions")) %></td>
                                        </tr>
                                        <%-- <tr valign="top"><td class="B taR">Special Provisions Count:</td><td><%# Eval("specialProvisions_cnt") %></td></tr>--%>
                                        <tr valign="top">
                                            <td class="B taR">Install From Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("install_from")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Install To Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("install_to")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Plan Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("print_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Bid Deadline Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("bid_deadline_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Design Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("design_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Procurement Plan Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("procurementPlan_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Out for Bid Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("outForBid_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Contract Awarded Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("contract_awarded_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Under Construction Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("construction_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Certification Date:</td>
                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("certification_date")) %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Pages:</td>
                                            <td><%# Eval("attachedPages_cnt") %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Specifications:</td>
                                            <td><%# Eval("attachedSpecifications") %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Procurement Type:</td>
                                            <td><%# Eval("list_procurementType.type") %></td>
                                        </tr>
                                        <%--  <tr valign="top"><td class="B taR">Fixed Note 1:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_FormWFP3_FixedText(Eval("fk_formWFP3_fixedText_code"))%></td></tr>
                <tr valign="top"><td class="B taR">Fixed Note 2:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_FormWFP3_FixedText(Eval("fk_formWFP3_fixedText2_code"))%></td></tr>--%>
                                        <tr valign="top">
                                            <td class="B taR">Free-Text Note:</td>
                                            <td><%# Eval("message2_freeform") %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Note:</td>
                                            <td><%# Eval("note") %></td>
                                        </tr>
                                        <%-- <tr valign="top"><td class="B taR">Project Location:</td><td><%# Eval("projectLocation") %></td></tr>--%>
                                        <tr valign="top">
                                            <td class="B U taR fsXS">
                                                <br />
                                                LEGACY</td>
                                            <td></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR fsXS">Package ID:</td>
                                            <td class="fsXS"><%# Eval("package_id") %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR fsXS">Package Transferred From:</td>
                                            <td class="fsXS"><%#Eval("fk_form_wfp3_transferredFrom.packageTransferredFrom") %></td>
                                        </tr>
                                    </table>
                                    <hr />
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="NestedDivLevel01">
                                    <div>
                                        [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton>
                                        |
                                        <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span>
                                    </div>
                                    <hr />
                                    <table cellpadding="3">
                                        <tr valign="top">
                                            <td class="B taR">Package:</td>
                                            <td><%# Eval("packageName") %></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Drawing Number:</td>
                                            <td><%# Eval("drawing_nbr")%></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Description:</td>
                                            <td>
                                                <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("description") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                                        </tr>
                                        <%-- <tr valign="top"><td class="B taR">On Hold:</td><td><asp:DropDownList ID="ddlOnHold" runat="server"></asp:DropDownList></td></tr>--%>
                                        <tr valign="top">
                                            <td class="B taR">Special Provisions:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlSpecialProvisions" runat="server"></asp:DropDownList></td>
                                        </tr>
                                        <%-- <tr valign="top"><td class="B taR">Special Provisions Count:</td><td><asp:TextBox ID="tbSpecialProvisionsCount" runat="server" Text='<%# Bind("specialProvisions_cnt") %>'></asp:TextBox></td></tr>--%>
                                        <tr valign="top">
                                            <td class="B taR">Install From Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalInstallFromDate" runat="server" Text='<%# Bind("install_from") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Install To Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalInstallToDate" runat="server" Text='<%# Bind("install_to") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Plan Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalPrintDate" runat="server" Text='<%# Bind("print_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Design Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalDesignDate" runat="server" Text='<%# Bind("design_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Procurement Plan Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalProcurementPlanDate" runat="server" Text='<%# Bind("procurementPlan_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Bid Deadline Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalBidDeadlineDate" runat="server" Text='<%# Bind("bid_deadline_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Out for Bid Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalOutForBidDate" runat="server" Text='<%# Bind("outForBid_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Contract Awarded Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalContractAwardDate" runat="server" Text='<%# Bind("contract_awarded_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Under Construction Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalConstructionDate" runat="server" Text='<%# Bind("construction_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Certification Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalCertificationDate" runat="server" Text='<%# Bind("certification_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Pages:</td>
                                            <td>
                                                <asp:TextBox ID="tbAttachedPages" runat="server" Text='<%# Bind("attachedPages_cnt") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Specifications:</td>
                                            <td>
                                                <asp:TextBox ID="tbAttachedSpecifications" runat="server" Text='<%# Bind("attachedSpecifications") %>' TextMode="MultiLine" Width="400px" Rows="4" MaxLength="255"></asp:TextBox></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Procurement Type:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlProcurementType" runat="server"></asp:DropDownList></td>
                                        </tr>
                                        <%-- <tr valign="top"><td class="B taR">Fixed Note 1:</td><td><asp:DropDownList ID="ddlFixedNote1" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Fixed Note 2:</td><td><asp:DropDownList ID="ddlFixedNote2" runat="server"></asp:DropDownList></td></tr>--%>
                                        <tr valign="top">
                                            <td class="B taR">Free-Text Note:</td>
                                            <td>
                                                <asp:TextBox ID="tbMessage2FreeForm" runat="server" Text='<%# Bind("message2_freeform") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Note:</td>
                                            <td>
                                                <asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                                        </tr>
                                        <%--  <tr valign="top"><td class="B taR">Project Location:</td><td><asp:TextBox ID="tbProjectLocation" runat="server" Text='<%# Bind("projectLocation") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td></tr>--%>
                                    </table>
                                </div>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <div class="NestedDivLevel01">
                                    <div>
                                        [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton>
                                        |
                                        <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>]
                                    </div>
                                    <hr />
                                    <table cellpadding="3">
                                        <tr valign="top">
                                            <td class="B taR">Description:</td>
                                            <td>
                                                <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("description") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Special Provisions:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlSpecialProvisions" runat="server"></asp:DropDownList></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Install From Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalInstallFromDate" runat="server" Text='<%# Bind("install_from") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Install To Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalInstallToDate" runat="server" Text='<%# Bind("install_to") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Plan Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalPrintDate" runat="server" Text='<%# Bind("print_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Design Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalDesignDate" runat="server" Text='<%# Bind("design_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Procurement Plan Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalProcurementPlanDate" runat="server" Text='<%# Bind("procurementPlan_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Bid Deadline Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalBidDeadlineDate" runat="server" Text='<%# Bind("bid_deadline_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Out for Bid Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalOutForBidDate" runat="server" Text='<%# Bind("outForBid_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Under Construction Date:</td>
                                            <td>
                                                <uc1:AjaxCalendar ID="tbCalConstructionDate" runat="server" Text='<%# Bind("construction_date") %>' />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Pages:</td>
                                            <td>
                                                <asp:TextBox ID="tbAttachedPages" runat="server" Text='<%# Bind("attachedPages_cnt") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Attached Specifications:</td>
                                            <td>
                                                <asp:TextBox ID="tbAttachedSpecifications" runat="server" Text='<%# Bind("attachedSpecifications") %>' TextMode="MultiLine" Width="400px" Rows="4" MaxLength="255"></asp:TextBox></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Procurement Type:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlProcurementType" runat="server"></asp:DropDownList></td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="B taR">Note:</td>
                                            <td>
                                                <asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
                        <asp:Panel ID="pnlSubPanels" runat="server" Visible="false">
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">BMPs</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_BMP_Add" runat="server"
                                        Text="Add a BMP" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <%-- <div class="DivBoxOrange">
            <uc1:UC_DocumentArchive ID="UC_DocumentArchive_A_WFP3_BMP" runat="server" StrArea="A" StrAreaSector="A_WFP3_BMP" />
        </div>--%>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_BMPs" runat="server">
                                        <%--DataSource='<%# Eval("form_wfp3_bmps") %>'--%>
                                        <EmptyDataTemplate>
                                            <div class="I">No BMP Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr>
                                                    <td></td>
                                                    <td class="B U">BMP</td>
                                                    <td class="B U">Unit</td>
                                                    <td class="B U">Units Designed</td>
                                                    <td class="B U">Design Cost</td>
                                                    <td class="B U">Units Completed</td>
                                                    <%--<td class="B U">Cost Per Unit</td>
                            <td class="B U">Dimensions</td>--%>
                                                    <td class="B U">Completed Cost</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_BMP_View" runat="server" Text="View"
                                                    CommandArgument='<%# Eval("pk_form_wfp3_bmp") %>' OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# Eval("CompositBmpNum") %> <%# Eval("description") %></td>
                                                <td><%# Eval("fk_unit_code") %></td>
                                                <td><%# Eval("units_designed") %></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("design_cost")) %></td>
                                                <td><%# Eval("units_completed") %></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("final_cost")) %></td>
                                                <%--<td><%# WACGlobal_Methods.Format_Global_Currency(Eval("cost_per_unit")) %></td>
                        <td><%# Eval("dimensions") %></td>
                        <%--<td><%# WACGlobal_Methods.Format_Global_Currency(Eval("completed_cost_ro")) %></td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Bmp" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Bmp ID="ucWfp3Bmp" runat="server" />
                                </asp:Panel>
                            </div>
                            <hr />
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">Bids</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_Bid_Add" runat="server"
                                        Text="Add a Bid" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_Bids" runat="server">
                                        <%--DataSource='<%# Eval("form_wfp3_bids") %>'--%>
                                        <EmptyDataTemplate>
                                            <div class="I">No Bid Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr>
                                                    <td></td>
                                                    <td class="B U">Contractor</td>
                                                    <td class="B U">Bid Amount</td>
                                                    <td class="B U">Bid Awarded</td>
                                                    <td class="B U">Modification Amt.</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_Bid_View" runat="server" Text="View"
                                                    CommandArgument='<%# Eval("pk_form_wfp3_bid") %>' OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# Eval("participant.fullname_LF_dnd")%></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("bid_amt"))%></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Date(Eval("bid_awarded")) %></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("modification_amt")) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Bid" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Bid ID="ucWfp3Bid" runat="server" />
                                </asp:Panel>
                            </div>
                            <hr />
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">Encumbrances</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_Encumbrance_Add"
                                        runat="server" Text="Add an Encumbrance" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_Encumbrances" runat="server">
                                        <%--DataSource='<%# Eval("form_wfp3_encumbrances") %>'--%>
                                        <EmptyDataTemplate>
                                            <div class="I">No Encumbrance Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr>
                                                    <td></td>
                                                    <td class="B U">Encumbrance</td>
                                                    <td class="B U">Encumbrance ID</td>
                                                    <td class="B U">Issued</td>
                                                    <td class="B U">Type</td>
                                                    <td class="B U">Amount</td>
                                                    <td class="B U">Approved</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_Encumbrance_View" runat="server" Text="View"
                                                    CommandArgument='<%# Eval("pk_form_wfp3_encumbrance") %>' OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# Eval("list_encumbrance.encumbrance")%></td>
                                                <td><%# Eval("encumbrance_id")%></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td>
                                                <td><%# Eval("list_encumbranceType.type")%></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt"))%></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Date(Eval("approved")) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Encumbrance" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Encumbrance ID="ucWfp3Encumbrance" runat="server" />
                                </asp:Panel>
                                <hr />
                                <asp:Literal ID="litAg_WFP3_Encumbrance_Possible_Grid" runat="server"></asp:Literal>
                            </div>
                            <hr />
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">Invoices</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_Invoice_Add"
                                        runat="server" Text="Add an Invoice" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_Invoices" runat="server">
                                        <%--DataSource='<%# Eval("form_wfp3_invoices") %>'--%>
                                        <EmptyDataTemplate>
                                            <div class="I">No Invoice Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr>
                                                    <td></td>
                                                    <td class="B U">Date</td>
                                                    <td class="B U">Invoice Number</td>
                                                    <td class="B U">Amount</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_Invoice_View" runat="server" Text="View"
                                                    CommandArgument='<%# Eval("pk_form_wfp3_invoice") %>' OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td>
                                                <td><%# Eval("invoice_nbr") %></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt")) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Invoice" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Invoice ID="ucWfp3Invoice" runat="server" />
                                </asp:Panel>
                            </div>
                            <hr />
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">Payments</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_Payment_Add"
                                        runat="server" Text="Add a Payment" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_Payments" runat="server">
                                        <%--DataSource='<%# Eval("form_wfp3_payments") %>'--%>
                                        <EmptyDataTemplate>
                                            <div class="I">No Payment Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr valign="top">
                                                    <td></td>
                                                    <td class="B U">Date</td>
                                                    <td class="B U">Amount</td>
                                                    <td class="B U">Check Number</td>
                                                    <td class="B U">Encumbrance</td>
                                                    <td class="B U">Invoice</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr valign="top">
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_Payment_View" runat="server" Text="View"
                                                    CommandArgument='<%# Eval("pk_form_wfp3_payment") %>' OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt"))%></td>
                                                <td><%# Eval("check_nbr") %></td>
                                                <td><%# Eval("list_encumbrance.encumbrance")%></td>
                                                <td><%# WACGlobal_Methods.SpecialText_Agriculture_WFP3_Invoice(Eval("fk_form_wfp3_invoice")) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Payment" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Payment ID="ucWfp3Payment" runat="server" />
                                    <asp:Panel ID="pnlWfp3PaymentBmp" runat="server" Visible="false">
                                        <aguc:AG_Wfp3PaymentBMP ID="ucWfp3PaymentBmp" runat="server" />
                                    </asp:Panel>
                                    <hr />
                                    <asp:Literal ID="litAg_WFP3_Payments_BMPs_Payments_Overview_Grid" runat="server"></asp:Literal>
                                    <hr />
                                    <asp:Literal ID="litAg_WFP3_Payments_BMPs_Funding_Overview_Grid" runat="server"></asp:Literal>
                                </asp:Panel>
                            </div>
                            <hr />
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">Modifications</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_Modification_Add"
                                        runat="server" Text="Add a Modification" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_Modifications" runat="server">
                                        <%--DataSource='<%# Eval("form_wfp3_modifications") %>'--%>
                                        <EmptyDataTemplate>
                                            <div class="I">No Modification Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr valign="top">
                                                    <td></td>
                                                    <td class="B U">BMP</td>
                                                    <td class="B U">Amount</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr valign="top">
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_Modification_View" runat="server" Text="View"
                                                    CommandArgument='<%# Eval("pk_form_wfp3_modification") %>' OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# Eval("form_wfp3_bmp.bmp_ag.CompositBmpNum") %> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td>
                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amount"))%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Mod" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Modification ID="ucWfp3Mod" runat="server" />
                                </asp:Panel>
                            </div>
                            <hr />
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">Specifications</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_Specification"
                                        runat="server" Text="Add a Specification" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_Specifications" runat="server">
                                        <EmptyDataTemplate>
                                            <div class="I">No Specification Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr valign="top">
                                                    <td></td>
                                                    <td class="B U">BMP</td>
                                                    <td class="B U">Practice</td>
                                                    <td class="B U">List Position</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr valign="top">
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_Specification_View" runat="server" Text="View"
                                                    CommandArgument='<%# Eval("pk_form_wfp3_specification") %>' OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# Eval("form_wfp3_bmp.bmp_ag.CompositBmpNum") %> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td>
                                                <td><%# Eval("fk_bmpPractice_code")%> <%# Eval("list_bmpPractice.practice") %></td>
                                                <td><%# Eval("sort_position") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Spec" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Specification ID="ucWfp3Spec" runat="server" />
                                </asp:Panel>
                            </div>
                            <hr />
                            <div class="NestedDivLevel02">
                                <div style="margin-bottom: 5px;">
                                    <span class="fsM B">Technicians</span> >>
                                    <asp:LinkButton ID="lbAg_WFP3_Technician_Add"
                                        runat="server" Text="Add a Technician" OnClick="lbWFP3_Subtable_Insert_Click"></asp:LinkButton>
                                </div>
                                <div style="margin-left: 5px;">
                                    <asp:ListView ID="lvAg_WFP3_Techs" runat="server">
                                        <%--DataSource='<%# Eval("form_wfp3_teches") %>'--%>
                                        <EmptyDataTemplate>
                                            <div class="I">No Technician Records</div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table cellpadding="5" rules="cols">
                                                <tr>
                                                    <td></td>
                                                    <td class="B U">Technician</td>
                                                    <td class="B U">Type</td>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>[<asp:LinkButton ID="lbAg_WFP3_Technician_View" runat="server"
                                                    Text="View" CommandArgument='<%# Eval("pk_form_wfp3_tech") %>'
                                                    OnClick="lbWFP3_Subtable_View_Click"></asp:LinkButton>]</td>
                                                <td><%# Eval("list_designerEngineerType.grouping")%></td>
                                                <td><%# Eval("list_designerEngineer.designerEngineer") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <asp:Panel ID="pnlWfp3Tech" runat="server" Visible="false">
                                    <aguc:AG_Wfp3Tech ID="ucWfp3Tech" runat="server" />
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:LinkButton ID="lbHidden_Ag_WFP3" runat="server"></asp:LinkButton>
            <ajtk:ModalPopupExtender ID="mpeAg_WFP3" runat="server" TargetControlID="lbHidden_Ag_WFP3" PopupControlID="pnlAg_WFP3"
                BackgroundCssClass="ModalPopup_BG">
            </ajtk:ModalPopupExtender>
            <asp:UpdateProgress ID="UpdateProgressWaiting" runat="server" CssClass="ModalPopupWait">
                <ProgressTemplate>
                    <asp:Image ID="imgWaiting" ImageUrl="~/images/ajax-loader-Purple-noback.gif" Width="100px" Height="100px" CssClass="ModalPopupWait"
                        ImageAlign="AbsMiddle" AlternateText="Processing" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <%--<ajtk:ModalPopupExtender ID="modalPopupWaiting" runat="server" ClientIDMode="Static" TargetControlID="UpdateProgressWaiting" 
                             PopupControlID="UpdateProgressWaiting" BackgroundCssClass="ModalPopupWait" />--%>

            <script type="text/javascript">
                //
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
                prm.add_beginRequest(BeginRequestHandler);
                // Raised after an asynchronous postback is finished and control has been returned to the browser.
                prm.add_endRequest(EndRequestHandler);
                //

                <%--   function BeginRequestHandler(sender, args) {
        //Shows the modal popup - the update progress
        var popup = $find('<%= modalPopupWaiting.ClientID %>');
        //        var popup = document.getElementById("modalPopupWaiting"); this messes up positioning... WHY???
        if (popup != null) {
            popup.show();
        }
    }
    //
    function EndRequestHandler(sender, args) {
        //Hide the modal popup - the update progress
        var popup = $find('<%= modalPopupWaiting.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }--%>

            </script>

        </div>
    </form>

</body>
</html>

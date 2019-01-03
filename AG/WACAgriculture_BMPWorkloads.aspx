<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="~/AG/WACAgriculture_BMPWorkloads.aspx.cs" Inherits="WACAgriculture_BMPWorkloads" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc" TagName="AjaxCalendar" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_EditCalendar.ascx" tagname="UC_EditCalendar" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style="min-height:250px;">
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
                        </div>
                        <div class="SearchDivOuter">
                            <div class="SearchDivContentContainer">
                                <div class="SearchDivContent">
                                    <div style="float:left;">Filter Options:</div>
                                    <div style="float:right;">[<asp:LinkButton ID="lbResetFilters" runat="server" Text="Reset Filters" OnClick="lbResetFilters_Click"></asp:LinkButton>]</div>
                                    <div style="clear:both;"></div>
                                </div>
                                <div class="SearchDivInner">
                                    <table class="tp5">
                                        <tr class="taT">
                                            <td class="taR B">Workload:</td><td><asp:DropDownList ID="ddlFilter_Workload" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            <td class="taR B">Funding:</td><td><asp:DropDownList ID="ddlFilter_Funding" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            <td class="taR B">Agency:</td><td><asp:DropDownList ID="ddlFilter_Agency" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                        </tr>
                                        <tr class="taT">
                                            <td class="taR B">Group:</td><td><asp:DropDownList ID="ddlFilter_Group" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            <td class="taR B">Planner:</td><td><asp:DropDownList ID="ddlFilter_Planner" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            <td class="taR B">Technician:</td><td><asp:DropDownList ID="ddlFilter_Technician" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                        </tr>
                                        <tr class="taT">
                                            <td class="taR B">Status:</td><td><asp:DropDownList ID="ddlFilter_Status" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            <td class="taR B">Owner:</td><td colspan="3" ><asp:DropDownList ID="ddlFilter_Owner" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div style="margin:0px 0px 5px 0px;">
                            <div style="float:left;"><asp:Label ID="lblCount" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></div>
                            <div style="float:right;"><asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></div>
                            <div style="clear:both;"></div>
                        </div>
                        <asp:GridView ID="gv" runat="server" Width="100%" BackColor="White" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gv_SelectedIndexChanged" OnPageIndexChanging="gv_PageIndexChanging" OnSorting="gv_Sorting" AllowSorting="True" class="tp5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast" HeaderStyle-Wrap="False" AutoGenerateColumns="False">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <EditRowStyle BackColor="#FFFFAA" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:TemplateField HeaderText="Workload" SortExpression="year">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlWorkload" runat="server" OnSelectedIndexChanged="ddlWorkload_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Funding" SortExpression="bmp_ag_workload.fk_agWorkloadFunding_code" ItemStyle-Width="75px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlFunding" runat="server" OnSelectedIndexChanged="ddlFunding_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="BMP Description" SortExpression="bmp_ag.bmp_nbr">
                                    <ItemTemplate>
                                        <%# Eval("bmp_ag.bmp_nbr")%> <%# Eval("bmp_ag.description")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="list_statusBMPWorkload.status">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                   <%--             <asp:TemplateField HeaderText="Priority" SortExpression="priority">
                                    <ItemTemplate>
                                        <%# Eval("priority") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Owner" SortExpression="bmp_ag.farmBusiness.ownerStr_dnd">
                                    <ItemTemplate>
                                        <%# Eval("bmp_ag.farmBusiness.ownerStr_dnd")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agency" SortExpression="list_agency.agency">
                                    <ItemTemplate>
                                        <%# Eval("list_agency.agency") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group" SortExpression="bmp_ag.farmBusiness.fk_groupPI_code">
                                    <ItemTemplate>
                                        <%# Eval("bmp_ag.farmBusiness.fk_groupPI_code")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Planner">
                                    <ItemTemplate>
                                        <%# WACGlobal_Methods.SpecialText_Agriculture_BMP_Workload_Planner(Eval("bmp_ag.fk_farmBusiness")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Techs">
                                    <ItemTemplate>
                                        <asp:ListView ID="lvTechnicians" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "T") %>'>
                                            <ItemTemplate>
                                                <div><%# Eval("list_designerEngineer.nickname")%></div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="ddlPercentage" runat="server" OnSelectedIndexChanged="ddlPercentage_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top:10px;">
                            <table cellpadding="3" width="60%">
                            <tr class="taT">
                                <td>
                                <asp:FormView ID="fv" runat="server" OnModeChanging="fv_ModeChanging" OnItemUpdating="fv_ItemUpdating" 
                                                        OnItemDeleting="fv_ItemDeleting" Width="600px" >
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Workload</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbFV_Close" runat="server" Text="Close" OnClick="lbFV_Close_Click"></asp:LinkButton>]</div>
                                        <div><span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_workload"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                                    <%--<div class="DivBoxLightGreen" style="margin-bottom:10px;">
                                                        <div class="fsM B" style="margin-bottom:10px;">Workload Progress</div>
                                                        <asp:GridView ID="gvProgress" runat="server" DataSource='<%# WACGlobal_Methods.Order_Agriculture_BMP_AG_Workload_Progress(Eval("bmp_ag_workloadProgresses")) %>' AutoGenerateColumns="false" class="tp5">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate><%# WACGlobal_Methods.Format_Global_Date(Eval("created")) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Percentage" DataField="progress_pct" DataFormatString="{0}%" ItemStyle-HorizontalAlign="Right" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>--%>
                                                    <table cellpadding="3">
                                                        <%--<tr class="taT"><td class="taC B U" colspan="2">Editable Fields</td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Workload:</td><td><%# Eval("year") %></td></tr>
                                                        <tr class="taT"><td class="taR B">Funding Source:</td><td><%# Eval("list_agWorkloadFunding.source") %></td></tr>
                                                        <tr class="taT"><td class="taR B">Status:</td><td><%# Eval("list_statusBMPWorkload.status")%></td></tr>
                                                        <%--<tr class="taT"><td class="taR B">Sort:</td><td><%# Eval("list_BMPWorkloadSortGroup.SortGroup")%></td></tr>--%>
                                                        <tr class="taT">
                                                            <td class="taR B">Technicians:</td>
                                                            <td>
                                                                <asp:ListView ID="lvTechnicians" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "T") %>'>
                                                                    <ItemTemplate>
                                                                        <div><%# Eval("list_designerEngineer.designerEngineer")%></div>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taR B">Checkers:</td>
                                                            <td>
                                                                <asp:ListView ID="lvCheckers" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "C") %>'>
                                                                    <ItemTemplate>
                                                                        <div><%# Eval("list_designerEngineer.designerEngineer")%></div>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taR B">Construction:</td>
                                                            <td>
                                                                <asp:ListView ID="lvConstruction" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "O") %>'>
                                                                    <ItemTemplate>
                                                                        <div><%# Eval("list_designerEngineer.designerEngineer")%></div>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taR B">Engineers:</td>
                                                            <td>
                                                                <asp:ListView ID="lvEngineers" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "E") %>'>
                                                                    <ItemTemplate>
                                                                        <div><%# Eval("list_designerEngineer.designerEngineer")%></div>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </td>
                                                        </tr>
                                                        <%--<tr class="taT"><td class="taR B">Rollover:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("rollover")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Revision Required:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("revisionReqd"))%></td></tr>
                                                        <tr class="taT"><td class="taR B">SWCD:</td><td><%# Eval("SWCD")%></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Priority:</td><td><%# Eval("priority")%></td></tr>
                                                       <%-- <tr class="taT"><td class="taR B">Target Design Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("targetDesign_date")) %></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Engineering Consultation - Target:</td><td></td></tr>
                                                        <tr class="taT"><td class="taR B">Engineering Consultation - Target:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_engConsult")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Engineering Consultation - Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_engConsult_done")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Survey - Target:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_survey")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Survey - Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_survey_done")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 30% - Target:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_inProg30")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 30% - Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_inProg30_done")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 60% - Target:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_inProg60")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 60% - Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_inProg60_done")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 90% - Target:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_inProg90")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 90% - Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_inProg90_done")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Package 95% - Target:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_design95")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Package 95% - Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_design95_done")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Designed 100%:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_readyForBid100")) %></td></tr>
                                                      <%--  <tr class="taT"><td class="taR B">Ready for Bid 100% _ Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("prog_readyForBid100_done")) %></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Agency:</td><td><%# Eval("list_agency.agency")%></td></tr>
                                                        <%--<tr class="taT"><td class="taR B">Contractor Assigned:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("contractorAssigned"))%></td></tr>
                                                        <tr class="taT"><td class="taR B">AWEP Repair:</td><td><%# Eval("list_workloadNotation.notation")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Is Valid BMP:</td><td><%# Eval("isValidBMP")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Estimate:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("design_est")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Estimate Note:</td><td><%# Eval("design_est_note")%></td></tr>
                                                        
                                                        <tr class="taT"><td class="taR B">Contract Modification:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("contract_amt_mod"))%></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">Partial Payment:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("payment_partial"))%></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">FSA NRCS Payment:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("payment_FSANRCS"))%></td></tr>
                                                        --%>
                                                        <%----%>
                                                        <%--<tr class="taT"><td class="taR B">Contractor #2:</td><td><%# Eval("contractor2") %></td></tr>
                                                        <tr class="taT"><td class="taR B">Under Construction Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("underConstruction_date")) %></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Note:</td><td><%# Eval("note") %></td></tr>
                                                        <%--<tr class="taT"><td class="taR B">Completed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("completed_date")) %></td></tr>--%>
                                                    </table>
                                                <%--<td style="width:20px;">&nbsp;</td>
                                                <td>
                                                    <table cellpadding="3">
                                                        <tr class="taT"><td class="taC B U" colspan="2">Read Only Fields</td></tr>
                                                        <tr class="taT"><td class="taR B">Farm ID:</td><td><%# Eval("bmp_ag.farmBusiness.farmID")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Owner:</td><td><%# Eval("bmp_ag.farmBusiness.ownerStr_dnd")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Region:</td><td><%# Eval("bmp_ag.farmBusiness.fk_regionWAC_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Rank:</td><td><%# Eval("bmp_ag.farmBusiness.ranking_ro")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Group:</td><td><%# Eval("bmp_ag.farmBusiness.fk_groupPI_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Planner:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_BMP_Workload_Planner(Eval("bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers"), false)%></td></tr>
                                                        <tr class="taT"><td class="taR B">BMP:</td><td><%# Eval("bmp_ag.bmp_nbr")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Description:</td><td><%# Eval("bmp_ag.description")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Year Scheduled:</td><td><%# Eval("bmp_ag.scheduled_implementation_year") %></td></tr>
                                                        <tr class="taT"><td class="taR B">Pollutant Category:</td><td><%# Eval("bmp_ag.fk_pollutant_category_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">NRCS WAC Practice:</td><td><%# Eval("bmp_ag.fk_BMPPractice_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Funding WAC:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetFunding(Eval("bmp_ag.pk_bmp_ag"), "WAC")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Subsequent Funding:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetFunding_AgencySource(Eval("bmp_ag.pk_bmp_ag"), null, "SA")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Plan Estimate:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("bmp_ag.est_plan_prior")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Partial Payment:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetPaymentByStatus(Eval("bmp_ag.pk_bmp_ag"), "P")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Contract Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("contract_amt")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Contract Amount Note:</td><td><%# Eval("contract_amt_note")%></td></tr>
                                                        <tr class="taT"><td class="taR B">WAC Payment:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("payment_WAC"))%></td></tr>
                                                        <tr class="taT"><td class="taR B">Designed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("designed_date")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Out For Bid Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("outForBid_date")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Contracted Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("contracted_date")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Completed Units:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetImplementation(Eval("bmp_ag.pk_bmp_ag"), Eval("year"), WACGlobal_Methods.Enum_Ag_BMP_Implementation.CompletedUnits)%></td></tr>
                                                        <tr class="taT"><td class="taR B">Completed Date:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetCompletedDate(Eval("bmp_ag.pk_bmp_ag"))%></td></tr>
                                                        
                                                    </table>
                                                </td>--%>
                                                
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Workload</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <div><span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_workload"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <%--<table cellpadding="3">
                                            <tr class="taT">
                                                <td>--%>
                                                   <%-- <div class="DivBoxLightGreen" style="margin-bottom:10px;">
                                                        <div class="fsM B" style="margin-bottom:10px;">Workload Progress</div>
                                                        <asp:GridView ID="gvProgress" runat="server" AutoGenerateColumns="false" class="tp5">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>[<asp:LinkButton ID="LinkButton1" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadProgress") %>' OnClick="BMP_AG_Workload_Progress_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>]</ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate><%# WACGlobal_Methods.Format_Global_Date(Eval("created")) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Percentage" DataField="progress_pct" DataFormatString="{0}%" ItemStyle-HorizontalAlign="Right" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>--%>
                                                    <table cellpadding="3">
                                                        <%--<tr class="taT"><td class="taC B U" colspan="2">Editable Fields</td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Workload:</td><td><asp:DropDownList ID="ddlWorkload" runat="server"></asp:DropDownList></td></tr>

                                                        <tr class="taT"><td class="taR B">Funding Source:</td><td><asp:DropDownList ID="ddlFundingSource" runat="server"></asp:DropDownList></td></tr>
                                                        <tr class="taT"><td class="taR B">Status:</td><td><asp:DropDownList ID="ddlStatusBMPWorkload" runat="server"></asp:DropDownList></td></tr>
                                                        <%--<tr class="taT"><td class="taR B">Sort:</td><td><asp:DropDownList ID="ddlSortGroupCode" runat="server"></asp:DropDownList></td></tr>--%>
                                                       <%-- <tr class="taT"><td class="taR B">%:</td><td><asp:DropDownList ID="ddlPercentage" runat="server"></asp:DropDownList></td></tr>--%>
                                                        <tr class="taT">
                                                            <td class="taR B">Technicians:</td>
                                                            <td>
                                                                <div class="DivBoxLightYellow" >
                                                                    <div><span class="B">Add:</span> <asp:DropDownList ID="ddlTechnician_Insert" Width="200px" runat="server" OnSelectedIndexChanged="ddlTechnician_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                    <asp:ListView ID="lvTechnicians" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "T") %>'>
                                                                        <ItemTemplate>
                                                                            <div>[<asp:LinkButton ID="lbTechnician_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taR B">Checkers:</td>
                                                            <td>
                                                                <div class="DivBoxLightYellow">
                                                                    <div><span class="B">Add:</span> <asp:DropDownList ID="ddlChecker_Insert" runat="server" OnSelectedIndexChanged="ddlChecker_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                    <asp:ListView ID="lvCheckers" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "C") %>'>
                                                                        <ItemTemplate>
                                                                            <div>[<asp:LinkButton ID="lbChecker_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taR B">Construction:</td>
                                                            <td>
                                                                <div class="DivBoxLightYellow">
                                                                    <div><span class="B">Add:</span> <asp:DropDownList ID="ddlConstruction_Insert" runat="server" OnSelectedIndexChanged="ddlConstruction_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                    <asp:ListView ID="lvConstruction" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "O") %>'>
                                                                        <ItemTemplate>
                                                                            <div>[<asp:LinkButton ID="lbConstruction_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taR B">Engineers:</td>
                                                            <td>
                                                                <div class="DivBoxLightYellow">
                                                                    <div><span class="B">Add:</span> <asp:DropDownList ID="ddlEngineer_Insert" runat="server" OnSelectedIndexChanged="ddlEngineer_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                    <asp:ListView ID="lvEngineers" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "E") %>'>
                                                                        <ItemTemplate>
                                                                            <div>[<asp:LinkButton ID="lbEngineer_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <%--<tr class="taT"><td class="taR B">Rollover:</td><td><asp:DropDownList ID="ddlRollover" runat="server"></asp:DropDownList></td></tr>
                                                        <tr class="taT"><td class="taR B">Revision Required:</td><td><asp:DropDownList ID="ddlRevisionRequired" runat="server"></asp:DropDownList></td></tr>
                                                        <tr class="taT"><td class="taR B">SWCD:</td><td><asp:TextBox ID="tbSWCD" runat="server" Text='<%# Bind("SWCD") %>' MaxLength="8"></asp:TextBox></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Priority:</td><td><asp:TextBox ID="tbPriority" runat="server" Text='<%# Bind("priority") %>'></asp:TextBox></td></tr>
                                                        <tr class="taT"><td class="taR B">Priority Tech:</td><td><asp:TextBox ID="tbPriorityTech" runat="server" Text='<%# Bind("priorityTech") %>'></asp:TextBox></td></tr>
                                                       <%-- <tr class="taT"><td class="taR B">Target Design Date:</td><td><uc:AjaxCalendar runat="server" ID="tbCalTargetDesignDate" Text='<%# Bind("targetDesign_date") %>' /></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Engineering Consultation - Target:</td><td><uc:AjaxCalendar runat="server" ID="tbCalEngConsult" Text='<%# Bind("prog_engConsult") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">Engineering Consultation - Completed:</td><td><uc:AjaxCalendar runat="server" ID="tbCalEngConsultDone" Text='<%# Bind("prog_engConsult_done") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">Survey - Target:</td><td><uc:AjaxCalendar runat="server" ID="tbCalSurveyDate" Text='<%# Bind("prog_survey") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">Survey - Completed:</td><td><uc:AjaxCalendar runat="server" ID="tbCalSurveyDateDone" Text='<%# Bind("prog_survey_done") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 30% - Target:</td><td><uc:AjaxCalendar runat="server" ID="tbCalProg30" Text='<%# Bind("prog_inProg30") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 30% - Completed:</td><td><uc:AjaxCalendar runat="server" ID="tbCalProg30Done" Text='<%# Bind("prog_inProg30_done") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 60% - Target:</td><td><uc:AjaxCalendar runat="server" ID="tbCalProg60" Text='<%# Bind("prog_inProg60") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 60% - Completed:</td><td><uc:AjaxCalendar runat="server" ID="tbCalProg60Done" Text='<%# Bind("prog_inProg60_done") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 90% - Target:</td><td><uc:AjaxCalendar runat="server" ID="tbCalProg90" Text='<%# Bind("prog_inProg90") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">In Progress 90% - Completed:</td><td><uc:AjaxCalendar runat="server" ID="tbCalProg90Done" Text='<%# Bind("prog_inProg90_done") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Package 95% - Target:</td><td><uc:AjaxCalendar runat="server" ID="tbCalDesignPkg95" Text='<%# Bind("prog_design95") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Package 95% - Completed:</td><td><uc:AjaxCalendar runat="server" ID="tbCalDesignPkg95Done" Text='<%# Bind("prog_design95_done") %>' /></td></tr>
                                                        <tr class="taT"><td class="taR B">Designed 100%:</td><td><asp:Label ID="lblReadyForBid" runat="server" Text='<%# WACGlobal_Methods.Format_Global_Date(Eval("prog_readyForBid100")) %>'/></td></tr>
                                                        <%--<tr class="taT"><td class="taR B">Ready for Bid 100% - Completed:</td><td><uc:AjaxCalendar runat="server" ID="tbCalReadyForBidDone" Text='<%# Bind("prog_readyForBid100_done") %>' /></td></tr>--%>
                                                       <%-- <tr class="taT"><td class="taR B">Target Design Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_targetDesign_date" runat="server" /></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Agency:</td><td><asp:DropDownList ID="ddlAgency" runat="server"></asp:DropDownList></td></tr>
                                                        <%--<tr class="taT"><td class="taR B">Contractor Assigned:</td><td><asp:DropDownList ID="ddlContractorAssigned" runat="server"></asp:DropDownList></td></tr>
                                                        <tr class="taT"><td class="taR B">AWEP Repair:</td><td><asp:DropDownList ID="ddlWorkloadNotation" runat="server"></asp:DropDownList></td></tr>
                                                        <tr class="taT"><td class="taR B">Is Valid BMP:</td><td><asp:TextBox ID="tbIsValidBMP" runat="server" Text='<%# Bind("isValidBMP") %>' MaxLength="24" Width="250px"></asp:TextBox></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Estimate:</td><td><asp:TextBox ID="tbDesignEstimate" runat="server" Text='<%# Bind("design_est") %>'></asp:TextBox></td></tr>
                                                        <tr class="taT"><td class="taR B">Design Estimate Note:</td><td><asp:TextBox ID="tbDesignEstimateNote" runat="server" Text='<%# Bind("design_est_note") %>' MaxLength="24" Width="250px"></asp:TextBox></td></tr>
                                                        <tr class="taT"><td class="taR B">Contract Amount:</td><td><asp:TextBox ID="tbContractAmount" runat="server" Text='<%# Bind("contract_amt") %>'></asp:TextBox></td></tr>
                                                        <tr class="taT"><td class="taR B">Contract Amount Note:</td><td><asp:TextBox ID="tbContractAmountNote" runat="server" Text='<%# Bind("contract_amt_note") %>' MaxLength="24" Width="250px"></asp:TextBox></td></tr>
                                                        <tr class="taT"><td class="taR B">Contract Modification:</td><td><asp:TextBox ID="tbContractAmountMod" runat="server" Text='<%# Bind("contract_amt_mod") %>'></asp:TextBox></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">Partial Payment:</td><td><asp:TextBox ID="tbPaymentPartial" runat="server" Text='<%# Bind("payment_partial") %>'></asp:TextBox></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">FSA NRCS Payment:</td><td><asp:TextBox ID="tbPaymentFSANRCS" runat="server" Text='<%# Bind("payment_FSANRCS") %>'></asp:TextBox></td></tr>
                                                        <tr class="taT"><td class="taR B">WAC Payment:</td><td><asp:TextBox ID="tbPaymentWAC" runat="server" Text='<%# Bind("payment_WAC") %>'></asp:TextBox></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">Completed Units:</td><td><asp:TextBox ID="tbCompletedUnits" runat="server" Text='<%# Bind("completed_units") %>' MaxLength="24" Width="250px"></asp:TextBox></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">Contractor #2:</td><td><asp:TextBox ID="tbContractor2" runat="server" Text='<%# Bind("contractor2") %>' MaxLength="48" Width="250px"></asp:TextBox></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">Designed Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_DesignedDate" runat="server" /></td></tr>
                                                        <tr class="taT"><td class="taR B">Out For Bid Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_OutForBidDate" runat="server" /></td></tr>
                                                        <tr class="taT"><td class="taR B">Contracted Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_ContractedDate" runat="server" /></td></tr>--%>
                                                        <%--<tr class="taT"><td class="taR B">Under Construction Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_UnderConstructionDate" runat="server" /></td></tr>--%>
                                                        <tr class="taT"><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' Width="300px" TextMode="MultiLine" Rows="4"></asp:TextBox></td></tr>
                                                        <%--<tr class="taT"><td class="taR B">Completed Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_CompletedDate" runat="server" /></td></tr>--%>
                                                    </table>
                                                <%--<td style="width:20px;">&nbsp;</td>
                                                <td>
                                                    <table cellpadding="3">
                                                        <tr class="taT"><td class="taC B U" colspan="2">Read Only Fields</td></tr>
                                                        <tr class="taT"><td class="taR B">Farm ID:</td><td><%# Eval("bmp_ag.farmBusiness.farmID")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Owner:</td><td><%# Eval("bmp_ag.farmBusiness.ownerStr_dnd")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Region:</td><td><%# Eval("bmp_ag.farmBusiness.fk_regionWAC_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Rank:</td><td><%# Eval("bmp_ag.farmBusiness.ranking_ro")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Group:</td><td><%# Eval("bmp_ag.farmBusiness.fk_groupPI_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Planner:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_BMP_Workload_Planner(Eval("bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers"), false)%></td></tr>
                                                        <tr class="taT"><td class="taR B">BMP:</td><td><%# Eval("bmp_ag.bmp_nbr")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Description:</td><td><%# Eval("bmp_ag.description")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Year Scheduled:</td><td><%# Eval("bmp_ag.scheduled_implementation_year") %></td></tr>
                                                        <tr class="taT"><td class="taR B">Pollutant Category:</td><td><%# Eval("bmp_ag.fk_pollutant_category_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">NRCS WAC Practice:</td><td><%# Eval("bmp_ag.fk_BMPPractice_code")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Funding WAC:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetFunding(Eval("bmp_ag.pk_bmp_ag"), "WAC")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Subsequent Funding:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetFunding_AgencySource(Eval("bmp_ag.pk_bmp_ag"), null, "SA")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Plan Estimate:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("bmp_ag.est_plan_prior")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Partial Payment:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetPaymentByStatus(Eval("bmp_ag.pk_bmp_ag"), "P")%></td></tr>
                                                        <tr class="taT"><td class="taR B">Contract Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("contract_amt")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Contract Amount Note:</td><td><%# Eval("contract_amt_note")%></td></tr>
                                                        <tr class="taT"><td class="taR B">WAC Payment:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("payment_WAC"))%></td></tr>
                                                        <tr class="taT"><td class="taR B">Designed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("designed_date")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Out For Bid Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("outForBid_date")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Contracted Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("contracted_date")) %></td></tr>
                                                        <tr class="taT"><td class="taR B">Completed Units:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetImplementation(Eval("bmp_ag.pk_bmp_ag"), Eval("year"), WACGlobal_Methods.Enum_Ag_BMP_Implementation.CompletedUnits)%></td></tr>
                                                        <tr class="taT"><td class="taR B">Completed Date:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetCompletedDate(Eval("bmp_ag.pk_bmp_ag"))%></td></tr>
                                                    </table>
                                                </td>--%>
                                                <%--</td>
                                            </tr>
                                        </table>--%>
                                    </div>
                                </EditItemTemplate>
                            </asp:FormView>
                            </td>
                            <td>
                            
                                <asp:ListView ID="lv" runat="server">
                                    <LayoutTemplate>
                                        <table cellpadding="3">
                                            <tr class="taT"><td class="taC B U" colspan="2">Read Only Fields</td></tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="taT"><td class="taR B">Farm ID:</td><td><%# Eval("farmID")%></td></tr>
                                        <tr class="taT"><td class="taR B">Easement:</td><td><span style="color:red;font-weight:bold"><%# Eval("statusEasement") %></span></td></tr>
                                        <tr class="taT"><td class="taR B">Owner:</td><td><%# Eval("ownerStr_dnd")%></td></tr>
                                        <tr class="taT"><td class="taR B">Region:</td><td><%# Eval("fk_regionWAC_code")%></td></tr>
                                        <tr class="taT"><td class="taR B">Rank:</td><td><%# Eval("farmRank")%></td></tr>
                                        <tr class="taT"><td class="taR B">Group:</td><td><%# Eval("fk_groupPI_code")%></td></tr>
                                        <tr class="taT"><td class="taR B">Planner:</td><td><%# Eval("plannerMaster") %></td></tr>
                                        <tr class="taT"><td class="taR B">BMP:</td><td><%# Eval("bmp_nbr")%></td></tr>
                                        <tr class="taT"><td class="taR B">Description:</td><td><%# Eval("bmp_descrip")%></td></tr>
                                 <%--       <tr class="taT"><td class="taR B">Year Scheduled:</td><td><%# Eval("year_implement") %></td></tr>--%>
                                    
                                        <tr class="taT"><td class="taR B">Workload Project Alias:</td><td><%# Eval("workload_project") %></td></tr>
                                        <tr class="taT"><td class="taR B">Workload Project Code (WFP2):</td><td><%# Eval("workload_projectCode") %></td></tr>
                                        <tr class="taT"><td class="taR B">Pollutant Category:</td><td><%# Eval("fk_pollutant_category_code")%></td></tr>
                                        <tr class="taT"><td class="taR B">NRCS WAC Practice:</td><td><%# Eval("fk_BMPPractice_code")%></td></tr>
                                        <tr class="taT"><td class="taR B">Funding WAC:</td><td><%# Eval("fundingWAC") %></td></tr>
                                        <tr class="taT"><td class="taR B">Subsequent Funding:</td><td><%# Eval("subsequentFundingWAC") %></td></tr>
                                        <tr class="taT"><td class="taR B">Plan Estimate:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("planEstimate")) %></td></tr>
                                        <tr class="taT"><td class="taR B">Units Planned:</td><td><%# Eval("units_planned") %></td></tr>
                                        <tr class="taT"><td class="taR B">Design Cost:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("design_cost")) %></td></tr>
                                        <tr class="taT"><td class="taR B">Units Designed:</td><td><%# Eval("units_designed") %></td></tr>
                                        <tr class="taT"><td class="taR B">Units Completed:</td><td><%# Eval("units_completed") %></td></tr>
                                        <tr class="taT"><td class="taR B">Contractor:</td><td><%# Eval("contractor") %></td></tr>
                                        <tr class="taT"><td class="taR B">Contract Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("contract_amt")) %></td></tr>
                                        <tr class="taT"><td class="taR B">Contract Amount Note:</td><td><%# Eval("contract_amt_note")%></td></tr>
                                        <tr class="taT"><td class="taR B">Payment WAC:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("paymentWAC"))%></td></tr>
                                        <tr class="taT"><td class="taR B">Payment AWEP:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("paymentAWEP"))%></td></tr>
                                        <tr class="taT"><td class="taR B">Payment CREP:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("paymentCREP"))%></td></tr>
                                        <tr class="taT"><td class="taR B">WFP3 Package:</td><td><%# Eval("packageName") %></td></tr>
                                        <tr class="taT"><td class="taR B">Designed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("design_date")) %></td></tr>
                                        <tr class="taT"><td class="taR B">Procurement Plan Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("procurementPlan_date")) %></td></tr>
                                        <tr class="taT"><td class="taR B">Out For Bid Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("outForBid_date")) %></td></tr>
                                        <tr class="taT"><td class="taR B">Contracted Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("contract_date")) %></td></tr>
                                        <tr class="taT"><td class="taR B">Certified/Completed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("certification_date"))%></td></tr>
                                      <%--  <tr class="taT"><td class="taR B">On Hold:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("onHold"))%></td></tr>--%>
                                        <tr class="taT"><td class="taR B">Procurement Status:</td><td><%# Eval("statusBMPWorkloadProcurement")%></td></tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                                            </tr>
                                        </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
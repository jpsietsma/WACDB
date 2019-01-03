<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="~/AG/WACAgriculture_BMPPlanning.aspx.cs" Inherits="WACAgriculture_BMPPlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
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
                    <div style="float:left;" class="fsXL B">Agriculture >> BMP Planning</div>
                    <div style="float:right;"><asp:HyperLink ID="hlAg_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <uc1:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                 <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <div style="float:left;"><asp:LinkButton ID="lbBMPPlanning_Insert" runat="server" Text="Insert a New Draft BMP" OnClick="lbBMPPlanning_Insert_Click" Font-Bold="True"></asp:LinkButton></div>
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
                                    <div style="float:left;">Search Options:</div>
                                    <div style="float:right; font-weight:normal;">[<asp:LinkButton ID="lbBMPPlanning_Search_ReloadReset" runat="server" Text="Reload/Reset Search Options" OnClick="lbBMPPlanning_Search_ReloadReset_Click"></asp:LinkButton>]</div>
                                    <div style="clear:both;"></div>
                                </div>
                                <div class="SearchDivInner">
                                    <div>
                                        <div style="float:left;"><div class="B">Farm ID:</div><asp:DropDownList ID="ddlBMPPlanning_Search_FarmID" runat="server" OnSelectedIndexChanged="ddlBMPPlanning_Search_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                        <div style="float:left; margin-left:20px;"><div class="B">Farm Name:</div><asp:DropDownList ID="ddlBMPPlanning_Search_FarmName" runat="server" OnSelectedIndexChanged="ddlBMPPlanning_Search_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                        <div style="float:left; margin-left:20px;"><div class="B">Owners:</div><asp:DropDownList ID="ddlBMPPlanning_Search_FarmOwners" runat="server" OnSelectedIndexChanged="ddlBMPPlanning_Search_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                        <div style="clear:both;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <asp:GridView ID="gvBMPPlanning" runat="server" Width="100%" BackColor="White" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" HeaderStyle-Wrap="False" AutoGenerateColumns="false" OnSelectedIndexChanged="gvBMPPlanning_SelectedIndexChanged">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:BoundField HeaderText="BMP" DataField="bmp_nbr" />
                                <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <div><span class="B">Farm:</span> <%# WACGlobal_Methods.SpecialText_Agriculture_FarmBusiness_ID_NAME_OWNER(Eval("FARMID"), Eval("FARMNAME"), Eval("FARMOWNER"), true, true)%></div>
                                        <div><span class="B">Source:</span> <%# Eval("list_BMPSource.source")%></div>
                                        <div><span class="B">Description:</span> <%# Eval("description")%></div>
                                        <div><span class="B">Pollutant Category:</span> <b><%# WACGlobal_Methods.Format_Color_Global_ColorText(Eval("list_pollutant_category.pk_pollutant_category_code"), WACGlobal_Methods.Enum_Color.PURPLE) %></b> <%# Eval("list_pollutant_category.descrip")%></div>
                                        <div><span class="B">Practice:</span> <b><%# WACGlobal_Methods.Format_Color_Global_ColorText(Eval("list_bmpPractice.pk_bmpPractice_code"), WACGlobal_Methods.Enum_Color.PURPLE) %></b> <%# Eval("list_bmpPractice.practice")%>, <span class="B">ABC:</span> <%# WACGlobal_Methods.Format_Global_Currency(Eval("list_bmpPractice.ABC"))%></div>
                                        <div><span class="B">Revision Description:</span> <%# Eval("list_revisionDescription.description")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Years">
                                    <ItemTemplate>
                                        <asp:ListView ID="lvBMPPlanning_Implementation" runat="server" DataSource='<%# Eval("YEARS") %>'>
                                            <ItemTemplate>
                                                <div><%# Eval("year") %></div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Information">
                                    <ItemTemplate>
                                        <div><span class="B">Status:</span> <b><%# WACGlobal_Methods.Format_Color_Global_ColorText(Eval("list_statusBMP.status"), WACGlobal_Methods.Enum_Color.PURPLE) %></b></div>
                                        <div><span class="B">Location:</span> <%# Eval("location") %></div>
                                        <div><span class="B">Units Planned:</span> <%# Eval("units_planned")%> <%# Eval("fk_unit_code")%></div>
                                        <div><span class="B">Prior Planning Estimate:</span> <%# WACGlobal_Methods.Format_Global_Currency(Eval("est_plan_prior"))%></div>
                                        <div><span class="B">Planning Estimate:</span> <%# WACGlobal_Methods.Format_Global_Currency(Eval("est_plan_rev")) %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hfPK_FarmBusiness" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:Panel ID="pnlBMPPlanning" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upBMPPlanning" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> BMP Planning</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbBMPPlanning_Close" runat="server" Text="Close" OnClick="lbBMPPlanning_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:FormView ID="fvBMPPlanning" runat="server" Width="100%" OnModeChanging="fvBMPPlanning_ModeChanging" OnItemUpdating="fvBMPPlanning_ItemUpdating" OnItemInserting="fvBMPPlanning_ItemInserting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton><%-- | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>--%>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Farm:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_FarmBusiness_ID_NAME_OWNER(Eval("FARMID"), Eval("FARMNAME"), Eval("FARMOWNER"), true, true)%></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Source:</td><td><%# Eval("list_BMPSource.source")%></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Status:</td><td class="B"><%# WACGlobal_Methods.Format_Color_Global_ColorText(Eval("list_statusBMP.status"), WACGlobal_Methods.Enum_Color.PURPLE) %></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Number:</td><td><%# Eval("bmp_nbr") %></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Description:</td><td><%# Eval("description")%></td></tr>
                                            <tr valign="top"><td class="B taR">SubIssue Statement:</td><td><%# Eval("issueStatement_wfp2") %></td></tr>
                                            <tr valign="top"><td class="taR B">Location:</td><td><%# Eval("location")%></td></tr>
                                            <tr valign="top"><td class="taR B">Revision Description:</td><td><%# Eval("list_revisionDescription.description")%></td></tr>
                                            <tr valign="top"><td class="taR B">Pollutant Category:</td><td><b><%# WACGlobal_Methods.Format_Color_Global_ColorText(Eval("list_pollutant_category.pk_pollutant_category_code"), WACGlobal_Methods.Enum_Color.PURPLE) %></b> <%# Eval("list_pollutant_category.descrip")%></td></tr>
                                            <tr valign="top"><td class="taR B">Practice:</td><td><b><%# WACGlobal_Methods.Format_Color_Global_ColorText(Eval("list_bmpPractice.pk_bmpPractice_code"), WACGlobal_Methods.Enum_Color.PURPLE) %></b> <%# Eval("list_bmpPractice.practice")%>, <span class="B">ABC:</span> <%# WACGlobal_Methods.Format_Global_Currency(Eval("list_bmpPractice.ABC"))%></td></tr>
                                            <tr valign="top"><td class="taR B">Units Planned:</td><td><%# Eval("units_planned")%> <%# Eval("list_bmpPractice.list_unit.unit")%></td></tr>
                                            <tr valign="top"><td class="taR B">Prior Planning Estimate:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("est_plan_prior"))%></td></tr>
                                            <tr valign="top"><td class="taR B">Current Planning Estimate:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("est_plan_rev")) %></td></tr>
                                            <tr valign="top"><td class="taR B">CREP:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("CREP")) %></td></tr>
                                          <%--  <tr valign="top"><td class="taR B">NMCP Type:</td><td><%# Eval("list_NMCPType.type") %></td></tr>--%>
                                            <tr valign="top"><td class="taR B">Supplemental Agreement Assignment:</td><td><%# Eval("list_SAAssignType.type")%></td></tr>
                                            <tr valign="top"><td class="taR B">Supplemental Agreement:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_SA_TP_Owner(Eval("supplementalAgreementTaxParcel"))%></td></tr>
                                        </table>
                                        <%--<hr />
                                        <div>
                                            <div class="B">Implementation Years</div>
                                            <div style="margin:5px 0px 5px 0px;">Add an Implementation Year: <asp:DropDownList ID="ddlBMPPlanning_ImplementationYears_Insert" runat="server" OnSelectedIndexChanged="ddlBMPPlanning_ImplementationYears_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                            <asp:ListView ID="lvBMPPlanning_ImplementationYears" runat="server" DataSource='<%# Eval("YEARS") %>'>
                                                <EmptyDataTemplate><div class="I">No Implementation Year Records</div></EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <div style="padding:3px;">[<asp:LinkButton ID="lbBMPPlanning_ImplementationYears_Delete" CommandArgument='<%# Eval("pk_bmp_ag_implementation") %>' runat="server" Text="Delete" OnClick="lbBMPPlanning_ImplementationYears_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("year") %></div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>--%>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Farm:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_FarmBusiness_ID_NAME_OWNER(Eval("FARMID"), Eval("FARMNAME"), Eval("FARMOWNER"), true, true)%></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Source:</td><td><asp:DropDownList ID="ddlBMPSource" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Status:</td><td><font color="purple"><b><%# Eval("list_statusBMP.status")%></b></font></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Number:</td><td><asp:TextBox ID="tbBMPNumber" runat="server" Text='<%# Bind("bmp_nbr") %>'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Description:</td><td><asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("description") %>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="B taR">SubIssue Statement:</td><td><asp:TextBox ID="tbSubIssueStatement" runat="server" Text='<%# Bind("issueStatement_wfp2") %>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Location:</td><td><asp:TextBox ID="tbLocation" runat="server" Text='<%# Bind("location") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Revision Description:</td><td><asp:DropDownList ID="ddlRevisionDescription" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Pollutant Category:</td><td><asp:DropDownList ID="ddlPollutantCategory" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Practice:</td><td><asp:DropDownList ID="ddlBMPPractice" runat="server" OnSelectedIndexChanged="ddlPractice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Units Planned:</td><td><asp:TextBox ID="tbUnitsPlanned" runat="server" Text='<%# Bind("units_planned") %>'></asp:TextBox> <asp:Label ID="lblUnits" runat="server"></asp:Label></td></tr>
                                            <tr valign="top"><td class="taR B">Prior Planning Estimate:</td><td><asp:TextBox ID="tbPriorPlanningEstimate" runat="server" Text='<%# Bind("est_plan_prior") %>'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Current Planning Estimate:</td><td><asp:TextBox ID="tbPlanningEstimate" runat="server" Text='<%# Bind("est_plan_rev") %>'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">CREP:</td><td><asp:DropDownList ID="ddlCREP" runat="server"></asp:DropDownList></td></tr>
                                          <%--  <tr valign="top"><td class="taR B">NMCP Type:</td><td><asp:DropDownList ID="ddlNMCPType" runat="server"></asp:DropDownList></td></tr>--%>
                                            <tr valign="top">
                                                <td class="taR B">Supplemental Agreement:</td>
                                                <td>
                                                    <asp:Panel ID="pnlSA_Available" runat="server" Visible="false">
                                                        <div class="NestedDivLevel02">
                                                            <table cellpadding="3">
                                                                <tr valign="top"><td colspan="2">[<asp:LinkButton ID="lbSA_Clear" runat="server" Text="Clear Supplemental Agreement" OnClick="lbSA_Clear_Click"></asp:LinkButton>]</td></tr>
                                                                <tr valign="top"><td class="taR B">Assignment:</td><td><asp:DropDownList ID="ddlSAA" runat="server"></asp:DropDownList></td></tr>
                                                                <tr valign="top"><td class="taR B">SA:</td><td><asp:DropDownList ID="ddlSA" runat="server"></asp:DropDownList></td></tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlSA_Message" runat="server"><asp:Label ID="lblSA_Message" runat="server" Text=""></asp:Label></asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Planning</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Farm:</td><td><asp:DropDownList ID="ddlFarm" runat="server" OnSelectedIndexChanged="ddlFarm_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Source:</td><td><asp:DropDownList ID="ddlBMPSource" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Number:</td><td><asp:TextBox ID="tbBMPNumber" runat="server" Text='<%# Bind("bmp_nbr") %>'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">BMP Description:</td><td><asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("description") %>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr> 
                                            <tr valign="top"><td class="B taR">SubIssue Statement:</td><td><asp:TextBox ID="tbSubIssueStatement" runat="server" Text='<%# Bind("issueStatement_wfp2") %>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Location:</td><td><asp:TextBox ID="tbLocation" runat="server" Text='<%# Bind("location") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Revision Description:</td><td><asp:DropDownList ID="ddlRevisionDescription" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Pollutant Category:</td><td><asp:DropDownList ID="ddlPollutantCategory" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Practice:</td><td><asp:DropDownList ID="ddlBMPPractice" runat="server" OnSelectedIndexChanged="ddlPractice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Units Planned:</td><td><asp:TextBox ID="tbUnitsPlanned" runat="server" Text='<%# Bind("units_planned") %>'></asp:TextBox> <asp:Label ID="lblUnits" runat="server"></asp:Label></td></tr>
                                            <tr valign="top"><td class="taR B">Prior Planning Estimate:</td><td><asp:TextBox ID="tbPriorPlanningEstimate" runat="server" Text='0.00'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Current Planning Estimate:</td><td><asp:TextBox ID="tbPlanningEstimate" runat="server" Text='<%# Bind("est_plan_rev") %>'></asp:TextBox></td></tr>
                                          <%--  <tr valign="top"><td class="taR B">Implementation Year:</td><td><asp:DropDownList ID="ddlImplementationYear" runat="server"></asp:DropDownList></td></tr>--%>
                                            <tr valign="top"><td class="taR B">CREP:</td><td><asp:DropDownList ID="ddlCREP" runat="server"></asp:DropDownList></td></tr>
                                          <%--  <tr valign="top"><td class="taR B">NMCP Type:</td><td><asp:DropDownList ID="ddlNMCPType" runat="server"></asp:DropDownList></td></tr>--%>
                                            <tr valign="top">
                                                <td class="taR B">Supplemental Agreement:</td>
                                                <td>
                                                    <asp:Panel ID="pnlSA_Available" runat="server" Visible="false">
                                                        <div class="NestedDivLevel02">
                                                            <table cellpadding="3">
                                                                <tr valign="top"><td colspan="2">[<asp:LinkButton ID="lbSA_Clear" runat="server" Text="Clear Supplemental Agreement" OnClick="lbSA_Clear_Click"></asp:LinkButton>]</td></tr>
                                                                <tr valign="top"><td class="taR B">Assignment:</td><td><asp:DropDownList ID="ddlSAA" runat="server"></asp:DropDownList></td></tr>
                                                                <tr valign="top"><td class="taR B">Supplemental Agreement:</td><td><asp:DropDownList ID="ddlSA" runat="server"></asp:DropDownList></td></tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlSA_Message" runat="server"><asp:Label ID="lblSA_Message" runat="server" Text="Select a Farm to View Available Supplemental Agreements"></asp:Label></asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_BMPPlanning" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeBMPPlanning" runat="server" TargetControlID="lbHidden_BMPPlanning" PopupControlID="pnlBMPPlanning" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>
                
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


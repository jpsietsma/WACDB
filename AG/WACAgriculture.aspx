<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="~/AG/WACAgriculture.aspx.cs" Inherits="WACAgriculture" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>
<%@ Register src="~/UserControls/UC_Property_EditInsert.ascx" tagname="UC_Property_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_Participant.ascx" tagname="UC_Express_Participant" tagprefix="uc1" %>
<%@ Register src="~/Property/UC_Express_Property.ascx" tagname="UC_Express_Property" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtons.ascx" tagname="UC_Express_PageButtons" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_ControlGroup_TaxParcel.ascx" tagname="UC_ControlGroup_TaxParcel" tagprefix="uc1" %>
<%@ Register Src="~/AG_WFP3/WACAG_WFP3_grid.ascx" TagPrefix="uc" TagName="WACAG_WFP3_Grid" %>
<%@ Register Src="~/AG/WACAG_FarmBusinessForm.ascx" TagPrefix="uc" TagName="WACAG_FarmBusinessForm" %>
<%@ Register Src="~/AG/WACAG_AgPageContents.ascx" TagPrefix="uc" TagName="WACAG_AgPageContents" %>
<%@ Register Src="~/Utility/WACUT_AttachedDocumentViewer.ascx" TagPrefix="uc1" TagName="WACUT_AttachedDocumentViewer" %>
<%@ Register Src="~/AG/WACAG_QMACheckbox.ascx" TagPrefix="uc" TagName="WACAG_QMACheckbox" %>
<%@ Register Src="~/AG/WACAG_SupplementalAgreementPageContents.ascx" TagPrefix="uc" TagName="WACAG_SupplementalAgreementPageContents" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
<asp:ScriptManagerProxy runat="server" />       
    <div class="divContentClass">
        <div style="min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Agriculture</div>
                    <div style="float:right;"><asp:HyperLink ID="hlAg_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <asp:UpdatePanel ID="upAgSearch" runat="server" UpdateMode="Conditional">                   
                    <ContentTemplate>     
                        <div style="float:left;"><uc:WACAG_AgPageContents runat="server" ID="WACAG_AgPageContents" /></div>
                        <div>
                            <div style="float:right;">
                                <asp:HyperLink ID="hlAg_Agriculture" runat="server"  NavigateUrl="~/AG/WACAgriculture.aspx" Text="Agriculture" Font-Bold="true"></asp:HyperLink> |  
                                <asp:HyperLink ID="hlAg_BMP_Planning" runat="server" NavigateUrl="~/AG/WACAgriculture_BMPPlanning.aspx" Text="BMP Planning" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_BMP_Workload" runat="server" NavigateUrl="~/AG/WACAgriculture_BMPWorkloads.aspx" Text="BMP Workloads" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_Supplemental_Agreement" runat="server" NavigateUrl="~/AG/WACAgriculture_SupplementalAgreements.aspx" Text="Supplemental Agreements" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_Workload_Funding" runat="server" NavigateUrl="~/AG/WACAgriculture_WorkloadFunding.aspx" Text="Workload Funding" Font-Bold="true"></asp:HyperLink> 
                            </div>
                            <div style="clear:both;"></div>
                        </div>
                        
                        <div class="SearchDivOuter">
                            <div class="SearchDivContentContainer">
                                <div class="SearchDivContent">
                                    <div style="float:left;font-weight:bold;">&nbsp;[<asp:LinkButton ID="lbAg_Search_ReloadReset" runat="server" Text="Search Again..." 
                                        OnClick="lbAg_Search_ReloadReset_Click"></asp:LinkButton>]
                                    </div>
                                    <div style="clear:both;"></div>
                                </div>
                                <asp:PlaceHolder ID="upAgSearchPlaceHolder" runat="server">  
                                    <div class="SearchDivInner">
                                        <div>
                                            <div style="float:left;"><asp:LinkButton ID="lbAgAll" runat="server" Text="All Farms" OnClick="lbAgAll_Click" Font-Bold="True"></asp:LinkButton></div>
                                            <div style="float:left; margin-left:20px;"><div class="B">Farm ID:</div><asp:DropDownList ID="ddlAg_Search_FarmID" runat="server" OnSelectedIndexChanged="ddlAg_Search_FarmID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                            <div style="float:left; margin-left:20px;"><div class="B">Farm Name:</div><asp:DropDownList ID="ddlAg_Search_FarmName" runat="server" OnSelectedIndexChanged="ddlAg_Search_FarmName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                      
                                            <div style="clear:both;"></div>
                                        </div>
                                        <hr />
                                        <div>
                                            <div style="float:left;"><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Search_FarmOwner" runat="server" StrParentCase="AGRICULTURE_FARMOWNEROPERATOR_SEARCH" StrEntityType="AGRICULTURE_FARMOWNEROPERATOR" ShowOrganization="false" /></div>
                                     
                                            <div style="clear:both;"></div>
                                        </div>
                                        <hr />
                                        <div>
                                            <div style="float:left;"><div class="B">Supplemental Agreement:</div><asp:DropDownList ID="ddlAg_Search_SA" runat="server" OnSelectedIndexChanged="ddlAg_Search_SA_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                            <div style="clear:both;"></div>
                                        </div>
                                    </div>
                                </asp:PlaceHolder>
                            </div>
                        </div>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>               
                <asp:UpdatePanel ID="upAgs" runat="server" UpdateMode="Conditional"  >
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:Label ID="lblCount" runat="server" CssClass="fsM B I"></asp:Label></div>
                        <asp:PlaceHolder ID="foundFarmGridPlaceholder" runat="server" Visible =" true">
                            <asp:GridView ID="gvAg" Width="100%" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gvAg_SelectedIndexChanged" OnPageIndexChanging="gvAg_PageIndexChanging" OnSorting="gvAg_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast">
                                <HeaderStyle BackColor="#BBBBAA" />
                                <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                                <AlternatingRowStyle BackColor="#DDDDCC" />
                                <PagerStyle BackColor="#BBBBAA" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View" />
                                    <asp:BoundField HeaderText="Farm ID" DataField="farmID" SortExpression="farmID" />
                                    <asp:BoundField HeaderText="Farm Name" DataField="farm_name" SortExpression="farm_name" />
                                    <asp:TemplateField HeaderText="Owner Name(s)">
                                        <ItemTemplate>
                                            <asp:ListView ID="lvAg_Agriculture_Owners" runat="server" DataSource='<%# WACGlobal_Methods.Order_Agriculture_FarmBusinessOwner(Eval("farmBusinessOwners")) %>' >
                                                <EmptyDataTemplate><div class="I">No Owner Records</div></EmptyDataTemplate>
                                                <ItemTemplate>
                                            
                                                    <div><%# Eval("participant.fullname_LF_dnd") %></div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="list_status.status">
                                        <ItemTemplate>
                                            <%# Eval("list_status.status") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Farm Size" SortExpression="list_farmSize.farmSize">
                                        <ItemTemplate>
                                            <%# Eval("list_farmSize.farmSize")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City" SortExpression="farmland.property.city">
                                        <ItemTemplate>
                                            <%# Eval("farmland.property.city")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:PlaceHolder>
                        <div style="margin-top:10px;">                         
                            <asp:FormView ID="fvAg" runat="server" Width="100%" 
                                OnModeChanging="fvAg_ModeChanging" OnItemUpdating="fvAg_ItemUpdating" 
                                OnItemInserting="fvAg_ItemInserting" OnItemDeleting="fvAg_ItemDeleting" >
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div>
                                            <div style="float:left;">
                                                <div>
                                                    <span class="fsM B">Farm Business</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbAgClose" runat="server" Text="Close" OnClick="lbAgClose_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusiness"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span>
                                                </div>
                                            </div>
                                       
                                            <div style="clear:both;"></div>
                                        </div>
                                        <hr />
                                        <div style="float:left;">
                                            <div class="fsL B"><%# WACGlobal_Methods.SpecialText_Agriculture_RecordTitle(Eval("farmID"), Eval("farm_name"), Eval("fk_farmSize_code"))%></div>
                                            <div class="fsM"><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Property" runat="server" StrExpressType="PROPERTY" StrSection="AGRICULTURE" />&nbsp;<%# WACGlobal_Methods.SpecialText_Global_Address(Eval("farmland.property.addressFull"), Eval("farmland.property.csz_ro"))%></div>
                                        </div>
                                        <div style="float:left; margin-left:20px;">
                                            <asp:ImageButton ID="imgbtnAg_Phone" runat="server" ImageUrl="~/images/icon_32_Phone_Red.png" CommandArgument='<%# Eval("pk_farmBusiness") %>' ToolTip="Phone and Cell numbers for Farm Owners and Operators" OnClick="imgbtnAg_Phone_Click" />
                                        </div>
                                        <div style="float:right;">
                                            <div class="B U">Operators</div>
                                            <div>                                    
                                                <asp:ListView ID="lvAg_FarmBusinessOperators" runat="server" >
                                                    <EmptyDataTemplate><div class="I">No Operator Records</div></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <div><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Operators" runat="server" StrSection="AGRICULTURE" />&nbsp;<%# Eval("participant.fullname_LF_dnd") %></div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div style="float:right; margin-right:20px;">
                                            <div class="B U">Owners</div>
                                            <div>
                                                <asp:ListView ID="lvAg_FarmBusinessOwners" runat="server">
                                                    <EmptyDataTemplate><div class="I">No Owner Records</div></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <div><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Owners" runat="server" StrSection="AGRICULTURE" />&nbsp;<%# Eval("participant.fullname_LF_dnd")%></div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div style="clear:both;"></div>
                                        <hr />
                                        <ajtk:TabContainer ID="tcAg" runat="server" Width="100%" AutoPostBack="true" OnActiveTabChanged="tcAg_ActiveTabChanged">
                                            <ajtk:TabPanel ID="tbAg_FarmOverview" HeaderText="Overview" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <div class="fsM B" style="float:left;">Farm Overview</div>
                                                   
                                                        <div style="clear:both;"></div>
                                                    </div>
                                                    <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewer" SectorCode="A_OVER" />
                                                    <div style="margin:10px 0px 10px 0px;"><asp:Literal ID="litAg_Overview" runat="server"></asp:Literal></div>
                                                    <table class="taT" >
                                                      
                                                        <tr class="taT"><td class="B taR">WFP0 Signed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("wfp0_signed")) %></td></tr>
                                                        <tr class="taT"><td class="B taR">WFP1 Signed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("wfp1_signed_date")) %></td></tr>
                                                        <tr class="taT"><td class="B taR">Ranking:</td><td><%# Eval("ranking_ro") %></td></tr>
                                                        <tr class="taT"><td class="B taR">Ranking Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("ranking_date_ro")) %></td></tr>
                                                        <tr class="taT"><td class="B taR">Prior Large Farm ID:</td><td><%# Eval("prior_LF_FarmID") %></td></tr>
                                                        <tr class="taT"><td class="B taR">Forestry:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("forestry"))%></td></tr>
                                                        <tr class="taT"><td class="B taR">FarmToMarket:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("farmToMarket"))%></td></tr>
                                                        <tr class="taT"><td class="B taR">Sold Farm:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("sold_farm"))%></td></tr>
                                                        <tr class="taT"><td class="B taR">Prior Owner:</td><td><%# Eval("priorOwner") %></td></tr>
                                                        <tr class="taT"><td class="B taR">Farm Count:</td><td><%# Eval("farm_cnt") %></td></tr>
                                                        <tr class="taT"><td class="B taR">Subfarm Count:</td><td><%# Eval("subfarm_cnt") %></td></tr>
                                                        <tr class="taT"><td class="B taR">Multiple Farm Equiv.:</td><td><%# Eval("multiple_farm_equivalents") %></td></tr>
                                                        <tr class="taT"><td class="B taR">Group PI:</td><td><%# Eval("list_groupPI.name") %></td></tr>
                                                        <tr class="taT"><td class="B taR">Environmental Impact:</td><td><%# Eval("list_environmentalImpact.environmentalImpact") %></td></tr>
                                                        <tr class="taT"><td class="B taR">IA Prior to Implementation:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("ia_prior_to_implementation"))%></td></tr>
                                                        <tr class="taT"><td class="B taR">Implementation Commenced:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("prior_implementation_commenced"))%></td></tr>
                                                        <tr class="taT"><td class="B taR">Implementation Substantially Complete Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("implementation_substantially_complete_date")) %></td></tr>
                                                        <tr class="taT"><td class="B taR">Implementation Fully Complete Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("implementation_fully_complete_date")) %></td></tr>
                                                        <tr class="taT"><td class="B taR">Status Comment:</td><td><%# Eval("status_comment") %></td></tr>
                                                        <tr class="taT"><td class="B U taR fsXS"><br />LEGACY</td><td></td></tr>
                                                        <tr class="taT"><td class="B taR fsXS">Approved Date:</td><td class="fsXS"><%# WACGlobal_Methods.Format_Global_Date(Eval("approved_date_legacy")) %></td></tr>
                                                        <tr class="taT"><td class="B taR fsXS">WFP2 Total Funds Approved (WAP):</td><td class="fsXS"><%# String.Format("{0:C}", Eval("form_wfp2_total_plan_approved")) %></td></tr>
                                                        <tr class="taT"><td class="B taR fsXS">WFP2 Total Funds Approved (CREP):</td><td class="fsXS"><%# WACGlobal_Methods.Format_Global_Currency(Eval("form_wfp2_total_crep_approved"))%></td></tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tbAg_FarmLand" HeaderText="Land" runat="server">
                                                <ContentTemplate>
                                                    <div><span class="fsM B">Farm Land</span> <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("farmland.pk_farmLand"), Eval("farmland.created"), Eval("farmland.created_by"), Eval("farmland.modified"), Eval("farmland.modified_by"))%></span></div>
                                                    <asp:HiddenField ID="hfAg_FarmLand_PK" runat="server" Value='<%# Eval("farmland.pk_farmLand") %>' />
                                                    <hr />
                                                    <div><span class="fsM B">Farm Land Tracts</span> [<asp:LinkButton ID="lbAg_FarmLandTract_Add" runat="server" Text="Add Tract" OnClick="lbAg_FarmLandTract_Add_Click"></asp:LinkButton>]</div>
                                                    <hr />
                                                    <asp:ListView ID="lvAg_FarmLandTracts" runat="server">
                                                        <EmptyDataTemplate><div class="I">No Tract Records</div></EmptyDataTemplate>
                                                        <ItemTemplate>
                                                            <div style="margin:10px 0px 0px 20px;">
                                                                <div>[<asp:LinkButton ID="lbAg_FarmLandTract_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmLandTract") %>' OnClick="lbAg_FarmLandTract_View_Click"></asp:LinkButton>] <span class="B">Tract: <%# Eval("tract") %></span></div>
                                                                <div style="margin:10px 0px 0px 20px;">
                                                                    <asp:ListView ID="lvAg_FarmLandTractFields" runat="server" DataSource='<%# Eval("farmLandTractFields") %>'>
                                                                        <EmptyDataTemplate><div class="I">No Field Records</div></EmptyDataTemplate>
                                                                        <LayoutTemplate>
                                                                            <table cellpadding="5" rules="cols">
                                                                                <tr class="taT">
                                                                                    <td class="B U">Field</td>
                                                                                    <td class="B U">Year</td>
                                                                                    <td class="B U">Acres</td>
                                                                                    <td class="B U">Soil</td>
                                                                                    <td class="B U">Active</td>
                                                                                </tr>
                                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="taT">
                                                                                <td><%# Eval("field") %></td>
                                                                                <td><%# Eval("year") %></td>
                                                                                <td><%# Eval("acres") %></td>
                                                                                <td><%# Eval("soil") %></td>
                                                                                <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active"))%></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_FarmOperation" HeaderText="Operation" runat="server">
                                                <ContentTemplate>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div class="fsM B">Farm Types</div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_FarmTypes" runat="server" AutoGenerateColumns="false" CssClass="taT" >
                                                                <EmptyDataTemplate><div class="I">No Farm Type Records</div></EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField><ItemTemplate>[<asp:LinkButton ID="lbAg_Type_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_farmBusinessType") %>' OnClientClick="return confirm_delete();" OnClick="lbAg_Type_Delete_Click"></asp:LinkButton>]</ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Farm Type"><ItemTemplate><%# Eval("list_farmType.farmType") %></ItemTemplate></asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div style=" margin-top:10px; border:1px solid #CCCCCC; background-color:#EEEEEE; padding:5px;">
                                                                <div class="B">Add a Farm Type: <asp:DropDownList ID="ddlAg_Type_Add" runat="server" OnSelectedIndexChanged="ddlAg_Type_Add_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div><span class="fsM B">Farm Contacts</span> [<asp:LinkButton ID="lbAg_FarmBusinessContact_Add" runat="server" Text="Add a Contact" 
                                                        OnClick="lbAg_FarmBusinessContact_Add_Click"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_FarmBusinessContacts" runat="server" AutoGenerateColumns="false" CssClass="taT">
                                                                <EmptyDataTemplate><div class="I">No Farm Business Contact Records</div></EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>[<asp:LinkButton ID="lbAg_FarmBusinessContact_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmBusinessContact") %>' OnClick="lbAg_FarmBusinessContact_View_Click"></asp:LinkButton>]</ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Express">
                                                                        <ItemTemplate><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Mails" runat="server" StrSection="AGRICULTURE" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Participant">
                                                                        <ItemTemplate><%# Eval("participant.fullname_LF_dnd") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Note" DataField="note" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div><span class="fsM B">Farm Mail</span></div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_FarmBusinessMail" runat="server" AutoGenerateColumns="false" >
                                                                <EmptyDataTemplate><div class="I">No Farm Business Mail Records</div></EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>[<asp:LinkButton ID="lbAg_FarmBusinessMail_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_farmBusinessMail") %>' OnClick="lbAg_FarmBusinessMail_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>]</ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Express">
                                                                        <ItemTemplate><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Mails" runat="server" StrSection="AGRICULTURE" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Participant">
                                                                        <ItemTemplate><%# Eval("participant.fullname_LF_dnd") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                            
                                                            <div style="margin-top:10px; border:1px solid #CCCCCC; background-color:#EEEEEE; padding:5px;">
                                                                <div class="B">Add A Participant:
                                                                    <asp:DropDownList ID="ddlAg_FarmBusinessMail_Participant_Insert" runat="server" OnSelectedIndexChanged="ddlAg_FarmBusinessMail_Participant_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </div>
                                                                
                                                            </div> 
                                                        </div>
                                                    </div>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div><span class="fsM B">Owners</span></div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_Owners" runat="server" AutoGenerateColumns="false" CssClass="taT">
                                                                <EmptyDataTemplate><div class="I">No Owner Records</div></EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>[<asp:LinkButton ID="lbAg_FarmBusinessOwner_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_farmBusinessOwner") %>' OnClick="lbAg_FarmBusinessOwner_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>]</ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Express">
                                                                        <ItemTemplate><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Owners" runat="server" StrSection="AGRICULTURE" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Owner">
                                                                        <ItemTemplate><%# Eval("participant.fullname_LF_dnd") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Active">
                                                                        <ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active"))%></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Master">
                                                                        <ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master"))%></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div style="margin-top:10px; border:1px solid #CCCCCC; background-color:#EEEEEE; padding:5px;">
                                                                <div><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Ag_FarmBusinessOwner" runat="server" StrParentCase="AGRICULTURE_FARMOWNER_INSERT" StrParticipantType="A" ShowOrganization="true" /></div>
                                                                <table class="taT">
                                                                <tr>
                                                                        <td>Active:</td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblAg_FarmBusinessOwner_Active" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                               
                                                                    </tr>
                                                                    <tr>
                                                                    <tr class="taT">
                                                                        <td>Master:</td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblAg_FarmBusinessOwner_Master" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td rowspan="2"><asp:Button id="btnAg_FarmBusinessOwner_Insert" runat="server" Text="Add Owner" OnClick="btnAg_FarmBusinessOwner_Insert_Click" /></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div><span class="fsM B">Operators</span></div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_Operators" runat="server" AutoGenerateColumns="false" CssClass="taT">
                                                                <EmptyDataTemplate>No Operator Records</EmptyDataTemplate>
                                                                <EmptyDataRowStyle Font-Italic="true" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>[<asp:LinkButton ID="lbAg_Operator_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_farmBusinessOperator") %>' OnClientClick="return confirm_delete();" OnClick="lbAg_Operator_Delete_Click"></asp:LinkButton>]</ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Express">
                                                                        <ItemTemplate><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Operators" runat="server" StrSection="AGRICULTURE" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Operator"><ItemTemplate><%# Eval("participant.fullname_LF_dnd") %></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Active"><ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active"))%></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Master"><ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master"))%></ItemTemplate></asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div style=" margin-top:10px; border:1px solid #CCCCCC; background-color:#EEEEEE; padding:5px;">
                                                                <div class="B">Add an Operator: <asp:DropDownList ID="ddlAg_Operator_Add" runat="server"></asp:DropDownList></div>
                                                                <table class="taT">
                                                                    <tr>
                                                                        <td>Active:</td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblAg_Operator_Active" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td rowspan="2"><asp:Button id="btnAg_Operator_Add" runat="server" Text="Add Operator" OnClick="btnAg_Operator_Add_Click" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Master:</td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblAg_Operator_Master" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div><span class="fsM B">Planners</span></div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_Planners" runat="server" AutoGenerateColumns="false" CssClass="taT">
                                                                <EmptyDataTemplate>No Planner Records</EmptyDataTemplate>
                                                                <EmptyDataRowStyle Font-Italic="true" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>[<asp:LinkButton ID="lbAg_Planner_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_farmBusinessPlanner") %>' OnClientClick="return confirm_delete();" OnClick="lbAg_Planner_Delete_Click"></asp:LinkButton>]</ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Planner"><ItemTemplate><%# Eval("list_designerEngineer.designerEngineer_title") %></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Master"><ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master"))%></ItemTemplate></asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div style=" margin-top:10px; border:1px solid #CCCCCC; background-color:#EEEEEE; padding:5px;">
                                                                <div class="B">Add a Planner: <asp:DropDownList ID="ddlAg_Planner_Add" runat="server"></asp:DropDownList></div>
                                                                <table class="taT">
                                                                    <tr>
                                                                        <td>Master:</td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblAg_Planner_Master" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td rowspan="2"><asp:Button id="btnAg_Planner_Add" runat="server" Text="Add Planner" OnClick="btnAg_Planner_Add_Click" /></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div><span class="fsM B">Supplemental Agreements</span></div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:ListView ID="lvAg_SAs" runat="server">
                                                                <EmptyDataTemplate><div class="I">No Supplemental Agreement Records</div></EmptyDataTemplate>
                                                                <ItemTemplate>
                                                                    <div class="NestedDivLevel00" style="margin-bottom:10px;">
                                                                        <div>
                                                                            <span class="B">Agreement Number:</span> <%# Eval("agreement_nbr_ro")%>
                                                                            <span class="B" style="margin-left:20px;">Agreement Date:</span> <%# WACGlobal_Methods.Format_Global_Date(Eval("agreement_date")) %>
                                                                            <span class="B" style="margin-left:20px;">Inactive Date:</span> <%# WACGlobal_Methods.Format_Global_Date(Eval("inactive_date"))%>
                                                                        </div>
                                                                     
                                                                        <div style="margin: 5px 0px 0px 20px" class="NestedDivLevel02">
                                                                            <asp:ListView ID="lvAg_SA_TaxParcels" runat="server" DataSource='<%# Eval("supplementalAgreementTaxParcels") %>'>
                                                                                <EmptyDataTemplate><div class="I">No Supplemental Agreement Tax Parcel Records</div></EmptyDataTemplate>
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <div>
                                                                                            <span class="B">Tax Parcel:</span> <%# WACGlobal_Methods.SpecialText_Global_TaxParcel_ID_OwnerStr(Eval("taxParcel.taxParcelID"), Eval("taxParcel.ownerStr_dnd"), false)%>
                                                                                            <span class="B" style="margin-left:20px;">Cancel Date:</span> <%# WACGlobal_Methods.Format_Global_Date(Eval("cancel_date"))%>
                                                                                        </div>
                                                                                        <div style="margin: 5px 0px 0px 20px;" class="NestedDivLevel03">
                                                                                            <asp:GridView ID="gvAg_SA_TaxParcel_BMPs" runat="server" DataSource='<%# Eval("bmp_ag_SAs") %>' Width="100%" AutoGenerateColumns="false" CellPadding="5">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="BMP">
                                                                                                        <ItemTemplate><%# Eval("bmp_ag.bmp_nbr") %></ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Active">
                                                                                                        <ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField HeaderText="Note" DataField="note" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                    <div class="NestedDivLevel01" style="margin-bottom:10px;">
                                                        <div><span class="fsM B">Tax Parcels</span></div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_TaxParcels" runat="server" AutoGenerateColumns="false" CssClass="taT">
                                                                <EmptyDataTemplate>No Tax Parcel Records</EmptyDataTemplate>
                                                                <EmptyDataRowStyle Font-Italic="true" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>[<asp:LinkButton ID="lbAg_TaxParcel_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_farmBusinessTaxParcel") %>' OnClientClick="return confirm_delete();" OnClick="lbAg_TaxParcel_Delete_Click"></asp:LinkButton>]</ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="View in Map"><ItemTemplate><asp:HyperLink ID="hypMap" runat="server" Text="Map" Target="_blank" NavigateUrl='<%# WACGlobal_Methods.SpecialMethod_Global_FAMEMAP_NavigateURL("TP", Eval("taxParcel.SBL")) %>' Visible='<%# WACGlobal_Methods.SpecialMethod_Global_FAMEMAP_HyperlinkVisible(Eval("taxParcel.SBL")) %>'></asp:HyperLink></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="County"><ItemTemplate><%# Eval("taxParcel.list_swi.county") %></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jurisdiction"><ItemTemplate><%# Eval("taxParcel.list_swi.jurisdiction") %></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tax Parcel ID"><ItemTemplate><%# Eval("taxParcel.taxParcelID") %></ItemTemplate></asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div style=" margin-top:10px; border:1px solid #CCCCCC; background-color:#EEEEEE; padding:5px;">
                                                                <div class="B" style="margin-bottom:10px;">Add a Tax Parcel:</div>
                                                                <asp:Panel ID="pnlTaxParcelPicker" runat="server" >
                                                                    <table>
                                                                        <tr class="taT">
                                                                            <td class="B taL">County:</td>
                                                                            <td><asp:DropDownList ID="ddlCounty" runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                                       
                                                                            <td class="B taL">Jurisdiction:</td>
                                                                            <td><asp:DropDownList ID="ddlJurisdiction" runat="server" OnSelectedIndexChanged="ddlJurisdiction_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                                          
                                                                            <td class="B taL">Tax Parcel ID:</td>
                                                                            <td><asp:DropDownList ID="ddlOperTaxParcels" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOperTaxParcels_SelectedIndexChanged"></asp:DropDownList></td>
                                                                        </tr>
                                                                     
                                                                    </table>
                                                                </asp:Panel>
                                                               
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_OandM" HeaderText="O & M" runat="server" >
                                                <ContentTemplate>
                                                    [set up O&M tab to only take O&M Agreeemtns (.pdf) from documentUploader]
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_FarmStatus" HeaderText="Farm Status" runat="server">
                                                <ContentTemplate>
                                                    <div><span class="fsM B">Farm Status</span> [<asp:LinkButton ID="lbAg_FarmStatus_Add" runat="server" Text="Add a Farm Status" OnClick="lbAg_FarmStatus_Add_Click"></asp:LinkButton>]</div>
                                                    <hr />
                                                    <div style="margin:10px 0px 0px 20px;">
                                                        <asp:GridView ID="gvAg_FarmStatus" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                            <HeaderStyle BackColor="#DDDDDD" />
                                                            <RowStyle VerticalAlign="Top" />
                                                            <AlternatingRowStyle BackColor="#EEEEEE" />
                                                            <EmptyDataTemplate>No Farm Status Records</EmptyDataTemplate>
                                                            <EmptyDataRowStyle Font-Italic="true" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>[<asp:LinkButton ID="lbAg_FarmStatus_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmBusinessStatus") %>' OnClick="lbAg_FarmStatus_View_Click"></asp:LinkButton>]</ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date"><ItemTemplate><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></ItemTemplate></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Farm Status"><ItemTemplate><%# Eval("list_status.status") %></ItemTemplate></asp:TemplateField>
                                                                <asp:BoundField HeaderText="Note" DataField="note" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <asp:Panel ID="pnlAg_FAD" runat="server">
                                                        <hr /><hr />
                                                        <div><span class="fsM B">FAD Status</span> [<asp:LinkButton ID="lbAg_FarmBusinessFAD_Add" runat="server" Text="Add a FAD Status" OnClick="lbAg_FarmBusinessFAD_Add_Click"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <div style="margin:10px 0px 0px 20px;">
                                                            <asp:GridView ID="gvAg_FarmBusinessFAD" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                                <HeaderStyle BackColor="#DDDDDD" />
                                                                <RowStyle VerticalAlign="Top" />
                                                                <AlternatingRowStyle BackColor="#EEEEEE" />
                                                                <EmptyDataTemplate>No FAD Status Records</EmptyDataTemplate>
                                                                <EmptyDataRowStyle Font-Italic="true" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>[<asp:LinkButton ID="lbAg_FarmBusinessFAD_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmBusinessFAD") %>' OnClick="lbAg_FarmBusinessFAD_View_Click"></asp:LinkButton>]</ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FAD Status"><ItemTemplate><%# Eval("list_FAD.setting")%></ItemTemplate></asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Note" DataField="note" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_Animals" HeaderText="Animals" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <div style="float:left;"><span class="fsM B">Animals</span> [<asp:LinkButton ID="lbAg_Animal_Add" runat="server" Text="Add an Animal" OnClick="lbAg_Animal_Add_Click"></asp:LinkButton>]</div>
                                                        <div style="float:right;" class="B">Animal Units: <asp:Label ID="lblAg_Animals_AnimalUnits" runat="server"></asp:Label></div>
                                                        <div style="clear:both;"></div>
                                                    </div>
                                                    <hr />
                                                    <asp:ListView ID="lvAg_Animals" runat="server">
                                                        <EmptyDataTemplate><div class="I">No Animal Records</div></EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <table cellpadding="5" rules="cols" border="1">
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td style="width:20px;">&nbsp;</td>
                                                                    <td class="U B I">Age</td>
                                                                    <td class="U B I taR">Count</td>
                                                                    <td class="U B I taR">Weight</td>
                                                                    <td class="U B I taR">Total</td>
                                                                    <td class="U B I taR">Animal Units</td>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>[<asp:LinkButton ID="lbAg_Animal_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmBusinessAnimal") %>' OnClick="lbAg_Animal_View_Click"></asp:LinkButton>]</td>
                                                                <td colspan="6" style="background-color:#EEEEDD;">
                                                                    <div style="float:left;"><span class="fsM B"><%# Eval("list_animal.animal") %></span> <%# Eval("ASR_yr") %></div>
                                                                    <div style="float:right;"><span>Animal Units:</span> <%# Eval("AU_ro") %></div>
                                                                    <div style="clear:both;"></div>
                                                                </td>
                                                            </tr>
                                                                <asp:ListView ID="lvAg_AnimalAges" runat="server" DataSource='<%# Eval("farmBusinessAnimalAges") %>'>
                                                                    <EmptyDataTemplate>
                                                                        <tr><td></td><td class="I" colspan="5">No Animal Information Records</td></tr>
                                                                    </EmptyDataTemplate>
                                                                    <LayoutTemplate>
                                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td style="width:20px;">&nbsp;</td>
                                                                            <td><%# Eval("list_animalAge.ageBracket") %></td>
                                                                            <td class="taR"><%# Eval("cnt") %></td>
                                                                            <td class="taR"><%# Eval("weight") %> lbs.</td>
                                                                            <td class="taR"><%# Eval("total") %> lbs.</td>
                                                                            <td class="taR"><%# WACGlobal_Methods.Math_Round(Eval("AU"), 2) %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_ASR" HeaderText="ASRs" runat="server">
                                                <ContentTemplate>
                                                    <div><span class="fsM B">Annual Status Reviews</span> [<asp:LinkButton ID="lbAg_ASR_Add" runat="server" Text="Add ASR" OnClick="lbAg_ASR_Add_Click"></asp:LinkButton>]</div>
                                                    <hr />
                                                    <div style="margin:10px 0px 0px 20px;">
                                                        <asp:GridView ID="gvAg_ASRs" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                            <HeaderStyle BackColor="#DDDDDD" />
                                                            <RowStyle VerticalAlign="Top" />
                                                            <AlternatingRowStyle BackColor="#EEEEEE" />
                                                            <EmptyDataTemplate><div class="I">No ASR Records</div></EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField><ItemTemplate>[<asp:LinkButton ID="lbAg_ASR_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_asrAg") %>' OnClick="lbAg_ASR_View_Click"></asp:LinkButton>]</ItemTemplate></asp:TemplateField>
                                                                <asp:BoundField HeaderText="Year" DataField="year" />
                                                                <asp:TemplateField HeaderText="Type"><ItemTemplate><%# Eval("list_asrType.asrType") %></ItemTemplate></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date Completed"><ItemTemplate><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></ItemTemplate></asp:TemplateField>
                                                                <asp:BoundField HeaderText="Interviewee" DataField="interviewee" />
                                                                 <asp:TemplateField HeaderText="Planner"><ItemTemplate><%# Eval("list_designerEngineer.designerEngineer") %></ItemTemplate></asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Planner"><ItemTemplate><%# WACGlobal_Methods.SpecialText_Global_DesignerEngineer(Eval("list_designerEngineer"), false, true) %></ItemTemplate></asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Revision"><ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("revision")) %></ItemTemplate></asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_BMP" HeaderText="BMPs" runat="server">
                                                <ContentTemplate>
                                                    <div><span class="fsM B">BMPs</span> [<asp:LinkButton ID="lbAg_BMP_Add" runat="server" Text="Add BMP" 
                                                        OnClick="lbAg_BMP_Add_Click"></asp:LinkButton>]&nbsp;
                                                       <%-- <telerik:RadCheckBox ID="BmpListToggleCheckBox" runat="server" OnCheckedChanged="BmpListToggleCheckBox_CheckedChanged"></telerik:RadCheckBox>Show only Approved and Draft BMPs--%>
                                                    </div>
                                                    <hr />
                                                    <div style="margin:10px 0px 0px 20px;">
                                                        <asp:GridView ID="gvAg_BMPs" runat="server" AutoGenerateColumns="false" CssClass="taT" CellPadding="5">
                                                            <HeaderStyle BackColor="#DDDDDD" />
                                                            <RowStyle VerticalAlign="Top" />
                                                            <AlternatingRowStyle BackColor="#EEEEEE" />
                                                            <EmptyDataTemplate><div class="I">No BMP Records</div></EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate><asp:ImageButton ID="lbAg_BMP_View" runat="server" ToolTip="View BMP" ImageUrl="~/images/view16.png" CommandArgument='<%# Eval("pk_bmp_ag") %>' OnClick="ibAg_BMP_View_Click"/>&nbsp;
                                                                        <asp:ImageButton ID="ibAg_BMP_Clone" runat="server" ToolTip="Copy BMP to Create New Version" ImageUrl="~/images/copy16.png" CommandArgument='<%# Eval("pk_bmp_ag") %>' OnClick="ibAg_BMP_Clone_Click" />&nbsp; 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Group" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                                                    <ItemTemplate><%# WACGlobal_Methods.FormatImplementationProject(Eval("fk_statusBMP_code"),Eval("ImplementationProject")) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SA">
                                                                    <ItemTemplate><asp:HyperLink ID="hlAg_BMP_SA" runat="server" Text='<%# Eval("SA") %>' ToolTip='<%# Eval("SA_owner") %>' NavigateUrl='<%# WACGlobal_Methods.EventControl_Custom_Hyperlink_NavigateURL(WACGlobal_Methods.Enum_NavigateURL_Special.AG_2_SA, Eval("SA")) %>'></asp:HyperLink></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BL" ItemStyle-HorizontalAlign="Center" >
                                                                    <ItemTemplate><%# WACGlobal_Methods.BmpGridIrcTextColor(Eval("IsIrc"),Eval("Backlog")) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PCat" ItemStyle-HorizontalAlign="Center" >
                                                                    <ItemTemplate><%# WACGlobal_Methods.BmpGridIrcTextColor(Eval("IsIrc"),Eval("fk_pollutant_category_code")) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                               <%-- <asp:BoundField HeaderText="BMP #" DataField="bmp_nbr" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />--%>
                                                                <asp:TemplateField HeaderText="BMP #" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" >
                                                                    <ItemTemplate><%# WACGlobal_Methods.BmpGridIrcTextColor(Eval("IsIrc"),Eval("bmp_nbr")) %></ItemTemplate>     
                                                                </asp:TemplateField>
                                                             <%--   <asp:BoundField HeaderText="BMP Description" DataField="description" />--%>
                                                                <asp:TemplateField HeaderText="BMP Description">
                                                                    <ItemTemplate><%# WACGlobal_Methods.BmpGridIrcTextColor(Eval("IsIrc"),Eval("description")) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Desc- riptor">
                                                                    <ItemTemplate><%# WACGlobal_Methods.BmpGridIrcTextColor(Eval("IsIrc"),Eval("BmpDescriptor")) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate><%# WACGlobal_Methods.SpecialText_Agriculture_BMP_Status_ColorCoded(Eval("fk_statusBMP_code"), Eval("status"), Eval("completed_date"))%></ItemTemplate> 
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="WAC Funds" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate><%# WACGlobal_Methods.Format_Global_Currency(Eval("funding"))%></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Other Funds" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate><%# WACGlobal_Methods.Format_Global_Currency(Eval("funding_other"))%></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="WAC Payments" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate><%# WACGlobal_Methods.Format_Global_Currency(Eval("payment"))%></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="WAC Available Funds" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate><%# WACGlobal_Methods.Format_Global_Currency(Eval("balance"))%></ItemTemplate>
                                                                </asp:TemplateField>
                                          
                                                            </Columns>
                                                        </asp:GridView>                                                       
                                                    </div>                                
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_Cropware" HeaderText="Cropware" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <div class="fsM B" style="float:left;">Cropware</div>
                                                        <div style="float:right;"><span class="B">Filter by Year:</span> <asp:DropDownList ID="ddlAg_Cropware_Year_Filter" runat="server" OnSelectedIndexChanged="ddlAg_Cropware_Year_Filter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                        <div style="clear:both;"></div>
                                                    </div>
                                                    <hr />
                                                    <div style="margin:10px 0px 0px 20px;">
                                                        <asp:GridView ID="gvAg_Cropware" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                            <HeaderStyle BackColor="#DDDDDD" />
                                                            <RowStyle VerticalAlign="Top" />
                                                            <AlternatingRowStyle BackColor="#EEEEEE" />
                                                            <EmptyDataTemplate><div class="I">No Cropware Records</div></EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField><ItemTemplate>[<asp:LinkButton ID="lbAg_Cropware_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_cropware") %>' OnClick="lbAg_Cropware_View_Click"></asp:LinkButton>]</ItemTemplate></asp:TemplateField>
                                                                <asp:BoundField HeaderText="Tract/Field" DataField="tractField" />
                                                                <asp:BoundField HeaderText="Plan Year" DataField="plan_year" />
                                                                <asp:BoundField HeaderText="Acres" DataField="acres" />
                                                                <asp:BoundField HeaderText="Soil" DataField="soil" />
                                                                <asp:BoundField HeaderText="Current Crop" DataField="current_crop" />
                                                                <asp:BoundField HeaderText="Sample Date" DataField="sample_date" />
                                                                <asp:BoundField HeaderText="Rotation" DataField="rotation" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_LandBaseInfo" HeaderText="Land Base" runat="server">
                                                <ContentTemplate>
                                                    <asp:FormView ID="fvAg_LandBaseInfo" runat="server" Width="100%" OnModeChanging="fvAg_LandBaseInfo_ModeChanging" OnItemUpdating="fvAg_LandBaseInfo_ItemUpdating">
                                                        <EmptyDataTemplate><div><span class="fsM B">Land Base Information</span></div><div class="I">No Land Base Information Record</div></EmptyDataTemplate>
                                                        <ItemTemplate>
                                                            <div><span class="fsM B">Land Base Information</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessLandBaseInfo"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td colspan="2" class="fsM B taC">OWNED</td></tr>
                                                                <tr class="taT"><td class="B taR">Crop Description:</td><td><%# Eval("crop_description")%></td></tr>
                                                                <tr class="taT"><td class="B taR">Crop, acres:</td><td><%# Eval("crop_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Hay, acres:</td><td><%# Eval("hay_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pasture, acres:</td><td><%# Eval("pasture_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Scrubland, acres:</td><td><%# Eval("scrubland_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Woodland, acres:</td><td><%# Eval("woodland_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Other, acres:</td><td><%# Eval("other_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Farmstead, acres:</td><td><%# Eval("farmstead_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Residential, acres:</td><td><%# Eval("residential_acre") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Total, acres:</td><td><%# Eval("acre_total") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Tax Parcel, acres:</td><td><%# Eval("taxParcel_acre") %></td></tr>
                                                                <tr class="taT"><td colspan="2">&nbsp;</td></tr>
                                                                <tr class="taT"><td class="fsM B taC" colspan="2">RENTED</td></tr>
                                                                <tr class="taT"><td class="B taR">Crop Description:</td><td><%# Eval("crop_description_rent")%></td></tr>
                                                                <tr class="taT"><td class="B taR">Crop, acres:</td><td><%# Eval("crop_acre_rent") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Hay, acres:</td><td><%# Eval("hay_acre_rent")%></td></tr>
                                                                <tr class="taT"><td class="B taR">Pasture, acres:</td><td><%# Eval("pasture_acre_rent")%></td></tr>
                                                                <tr class="taT"><td class="B taR">Woodland, acres:</td><td><%# Eval("woodland_acre_rent") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Other, acres:</td><td><%# Eval("other_acre_rent")%></td></tr>
                                                                <tr class="taT"><td class="B taR">Total, acres:</td><td><%# Eval("acre_rent_total")%></td></tr>
                                                                <tr class="taT"><td colspan="2">&nbsp;</td></tr>
                                                                <tr class="taT"><td class="fsM B taC" colspan="2">MISCELLANEOUS</td></tr>
                                                                <tr class="taT"><td class="B taR">Forested:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("forested"))%></td></tr>
                                                                <tr class="taT"><td class="B taR">Paddock Count:</td><td><%# Eval("paddock_cnt") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Riding Ring Count:</td><td><%# Eval("ridingRing_cnt") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Commitment 480A:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("commitment_480A"))%></td></tr>
                                                                <tr class="taT"><td class="B taR">Barn To Water (Feet):</td><td><%# Eval("barnToH2O_ft") %></td></tr>
                                                                <tr class="taT"><td class="B taR">EOH PCs I-III to Water (Feet):</td><td><%# Eval("pollutantCategoryTop3ToH20_ft")%></td></tr>
                                                                <tr class="taT"><td class="B taR">EOH Field to Water (Feet):</td><td><%# Eval("fieldDistanceToH20_eoh")%></td></tr>
                                                                    <tr class="taT"><td class="B taR">PJ Points (EOH):</td><td><%#Eval("farmRankingProfJudgement_eoh")%></td></tr>
                                                             
                                                                <tr class="taT"><td class="B taR">Comment:</td><td><%# Eval("comment") %></td></tr>
                                                                <tr class="taT"><td class="B U taR fsXS"><br />LEGACY</td><td></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Rotated Row Crop:</td><td class="fsXS"><%# Eval("rotated_row_crop_own") %></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Hayland Permanent:</td><td class="fsXS"><%# Eval("hayland_permanent") %></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Pasture Permanent:</td><td class="fsXS"><%# Eval("pasture_permanent") %></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Total Acreage:</td><td class="fsXS"><%# Eval("acreage_own_total")%></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Rented Cropland Acreage:</td><td class="fsXS"><%# Eval("cropland_acre_rent")%></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Rented Hayland Acreage:</td><td class="fsXS"><%# Eval("hayland_acre_rent")%></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Rented Total Acreage:</td><td class="fsXS"><%# Eval("acreage_rent_total") %></td></tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <div><span class="fsM B">Land Base Info</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessLandBaseInfo"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td colspan="2" class="fsM B taC">OWNED</td></tr>
                                                                <tr class="taT"><td class="B taR">Crop Description:</td><td><asp:TextBox ID="tbCropDescription" runat="server" Text='<%# Bind("crop_description") %>' Width="400px"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Crop, acres:</td><td><asp:TextBox ID="tbCropAcres" runat="server" Text='<%# Bind("crop_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Hay, acres:</td><td><asp:TextBox ID="tbHayAcres" runat="server" Text='<%# Bind("hay_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pasture, acres:</td><td><asp:TextBox ID="tbPastureAcres" runat="server" Text='<%# Bind("pasture_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Scrubland, acres:</td><td><asp:TextBox ID="tbScrublandAcres" runat="server" Text='<%# Bind("scrubland_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Woodland, acres:</td><td><asp:TextBox ID="tbWoodlandAcres" runat="server" Text='<%# Bind("woodland_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Other, acres:</td><td><asp:TextBox ID="tbOtherAcres" runat="server" Text='<%# Bind("other_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Farmstead, acres:</td><td><asp:TextBox ID="tbFarmsteadAcres" runat="server" Text='<%# Bind("farmstead_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Residential, acres:</td><td><asp:TextBox ID="tbResidentialAcres" runat="server" Text='<%# Bind("residential_acre") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td colspan="2">&nbsp;</td></tr>
                                                                <tr class="taT"><td class="fsM B taC" colspan="2">RENTED</td></tr>
                                                                <tr class="taT"><td class="B taR">Crop Description:</td><td><asp:TextBox ID="tbCropDescriptionRented" runat="server" Text='<%# Bind("crop_description_rent") %>' Width="400px"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Crop, acres:</td><td><asp:TextBox ID="tbCropAcresRented" runat="server" Text='<%# Bind("crop_acre_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Hay, acres:</td><td><asp:TextBox ID="tbHayAcresRented" runat="server" Text='<%# Bind("hay_acre_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pasture, acres:</td><td><asp:TextBox ID="tbPastureAcresRented" runat="server" Text='<%# Bind("pasture_acre_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Woodland, acres:</td><td><asp:TextBox ID="tbWoodlandAcresRented" runat="server" Text='<%# Bind("woodland_acre_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Other, acres:</td><td><asp:TextBox ID="tbOtherAcresRented" runat="server" Text='<%# Bind("other_acre_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td colspan="2">&nbsp;</td></tr>
                                                                <tr class="taT"><td class="fsM B taC" colspan="2">MISCELLANEOUS</td></tr>
                                                                <tr class="taT"><td class="B taR">Forested:</td><td><asp:DropDownList ID="ddlForested" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Paddock Count:</td><td><asp:TextBox ID="tbPaddockCount" runat="server" Text='<%# Bind("paddock_cnt") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Riding Ring Count:</td><td><asp:TextBox ID="tbRidingRingCount" runat="server" Text='<%# Bind("ridingRing_cnt") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Commitment 480A:</td><td><asp:DropDownList ID="ddlCommitment480A" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Barn To Water (Feet):</td><td><asp:TextBox ID="tbBarnToH2O" runat="server" Text='<%# Bind("barnToH2O_ft") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">EOH PCs I-III to Water (Feet):</td><td><asp:TextBox ID="tbEOH_PCToH2O" runat="server" Text='<%# Bind("pollutantCategoryTop3ToH20_ft") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">EOH Field to Water (Feet):</td><td><asp:TextBox ID="tbFieldDistanceToH2O_eoh" runat="server" Text='<%# Bind("fieldDistanceToH20_eoh") %>'></asp:TextBox></td></tr>
                                                                    <tr class="taT"><td class="B taR">PJ Points (EOH):</td><td><asp:TextBox ID="tbfarmRankingProfJudgement_eoh" runat="server" Text='<%# Bind("farmRankingProfJudgement_eoh")%>'></asp:TextBox></td></tr>
                                                             
                                                                <tr class="taT"><td class="B taR">Comment:</td><td><asp:TextBox ID="tbComment" runat="server" Text='<%# Bind("comment") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </asp:FormView>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_Note" HeaderText="Notes" runat="server">
                                                <ContentTemplate>
                                                    <div><span class="fsM B">Notes</span> [<asp:LinkButton ID="lbAg_Note_Add" runat="server" Text="Add a Note" OnClick="lbAg_Note_Add_Click"></asp:LinkButton>]</div>
                                                    <hr />
                                                    <div style="margin:10px 0px 0px 20px;">
                                                        <asp:GridView ID="gvAg_Notes" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                            <HeaderStyle BackColor="#DDDDDD" />
                                                            <RowStyle VerticalAlign="Top" />
                                                            <AlternatingRowStyle BackColor="#EEEEEE" />
                                                            <EmptyDataTemplate><div class="I">No Note Records</div></EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField><ItemTemplate>[<asp:LinkButton ID="lbAg_Note_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmBusinessNote") %>' OnClick="lbAg_Note_View_Click"></asp:LinkButton>]</ItemTemplate></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Created By"><ItemTemplate><%# Eval("created_by") %> (PK: <%# Eval("pk_farmBusinessNote") %>)</ItemTemplate></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Created"><ItemTemplate><%# WACGlobal_Methods.Format_Global_Date(Eval("created")) %></ItemTemplate></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Note Type"><ItemTemplate><%# Eval("list_farmBusinessNoteType.type") %></ItemTemplate></asp:TemplateField>
                                                                <asp:BoundField HeaderText="Note" DataField="note" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_NMP" HeaderText="NMP" runat="server">
                                                <ContentTemplate>
                                                    <div class="fsM B">Nutrient Management Plan</div>
                                                    <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewerNMP" SectorCode="A_NMP" />  
                                                    <asp:FormView ID="fvAg_NMP" runat="server" Width="100%" OnModeChanging="fvAg_NMP_ModeChanging" OnItemUpdating="fvAg_NMP_ItemUpdating">
                                                        <EmptyDataTemplate><div><span class="fsM B">Nutrient Management Plan</span></div><div class="I">No Nutrient Management Plan Record</div></EmptyDataTemplate>

                                                        <ItemTemplate>
                                                            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_nmp"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>      
                                                            <hr />                     
                                                            <table class="taT">
                                                                <tr class="taT"><td class="fsM B taC" colspan="11">REGULAR NMP</td></tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">BMP Number NMP:</td>
                                                                    <td class="taL" colspan="10"><%# WACGlobal_Methods.SpecialText_Global_BMPAgNMP(Eval("fk_bmp_ag_nmp"))%></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">NMP Date:</td>
                                                                    <td><%# WACGlobal_Methods.Format_Global_Date(Eval("nmp_date")) %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Needs NMP:</td>
                                                                    <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("needs_nmp"))%></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Plan Type:</td>
                                                                    <td><%# Decode_ListNMPlanTypeCode(Eval("basic_plan"))%></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Follow-up NMP:</td>
                                                                    <td><%# Eval("list_followUpNMP.description") %></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">NM Planner:</td>
                                                                    <td colspan="3"><%# WACGlobal_Methods.SpecialText_Global_DesignerEngineer(Eval("fk_list_designerEngineer_nmp"), true, true)%></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL" colspan="2">Whole Farm Planner:</td>
                                                                    <td colspan="4"><%# WACGlobal_Methods.SpecialText_Global_Planner(Eval("fk_farmBusiness"))%></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">NMP Credit:</td>
                                                                    <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("nmp_credit"))%></td>
                                                                    <td colspan="9">&nbsp;</td>
                                                                </tr>
                                                                 <tr class="taT">
                                                                    <td colspan="11" class="taL"><asp:Literal ID="litAg_NutrientManagementCreditHistory" runat="server" EnableViewState="true" Visible="true"></asp:Literal></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td colspan="11"><hr /></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">Primary Enterprise:</td><td><%# Eval("enterprise_primary") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Secondary Enterprise:</td><td><%# Eval("enterprise_secondary") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Storage:</td><td><%# Eval("list_NMPStorage.storage") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Status:</td><td><%# Eval("status") %></td>
                                                                     
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">Units Designed:</td><td><%# Eval("acres_planned") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Animal Units:</td><td><asp:Label ID="lblAnimalUnits" runat="server"></asp:Label></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Manure Sample Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("manure_sample_date")) %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Spreader Calibration Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("spreader_calibration_date")) %></td>
                                                                </tr>
                                                                 <tr class="taT">
                                                                    <td class="B taL">MTC:</td>
                                                                    <td colspan="10" class="taL" ><%# WACGlobal_Methods.Format_Global_YesNo(Eval("mtc"))%></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">Most Recent Sample:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("sample_date_most_recent")) %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Sample Count:</td><td><%# Eval("sample_cnt") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Sample Priority:</td><td><%# Eval("sample_priority") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Sampler:</td><td><%# Eval("sampler") %></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">Crop Year:</td><td><%# Eval("crop_year") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Crop Year Expiration:</td><td><%# Eval("crop_year_expiration") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Acres of Corn:</td><td><%# Eval("acres_corn") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Goal:</td><td><%# Eval("goal") %></td>
                                                                </tr>
                                                                 <tr class="taT">
                                                                    <td colspan="11"><hr /></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">O&amp;M Signature Date:</td>
                                                                    <td><%# WACGlobal_Methods.Format_Global_Date(Eval("operationsMaintenance_signature_date")) %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">Completed Date:</td>
                                                                    <td><%# WACGlobal_Methods.Format_Global_Date(Eval("wfp3_signature_date")) %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">WFP3 Year:</td><td><%# Eval("wfp3_year") %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">WFP1 Signatures:</td><td><%# Eval("wfp1_signatures") %></td>
                                                                </tr>
                                                                <tr class="taT">
                                                                    <td class="B taL">FSA Release Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("fsa_release_date")) %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">NMP Workshop Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("nmp_workshop_date")) %></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL">CREP:</td>
                                                                    <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("CREP"))%></td>
                                                                    <td colspan="3">&nbsp;</td>
                                                                </tr>
                                                                 <tr class="taT">
                                                                    <td class="B taL">EQIP/AWEP:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("EQIP"))%></td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="B taL" >AWEP Signup:</td>
                                                                    <td><%# Eval("AWEP_Signup") %></td>
                                                                    <td colspan="6">&nbsp;</td>
                                                                    
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_nmp"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr><td colspan="2"><asp:Literal ID="litAg_NutrientManagementCreditHistory" runat="server"></asp:Literal></td></tr>
                                                                <tr class="taT"><td class="fsM B taC" colspan="2">REGULAR NMP</td></tr>
                                                                <tr class="taT"><td class="B taR">NMP Date:</td><td><uc1:AjaxCalendar ID="tbCalNMPDate" runat="server" Text='<%# Bind("nmp_date") %>' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Needs NMP:</td><td><asp:DropDownList ID="ddlNeedsNMP" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Plan Type:</td><td><asp:DropDownList ID="ddlBasicPlan" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Follow-up NMP:</td><td><asp:DropDownList ID="ddlFollowUpNMP" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">EQIP/AWEP:</td><td><asp:DropDownList ID="ddlEQIP" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">NMP Credit:</td><td><asp:DropDownList ID="ddlNMPCredit" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">MTC:</td><td><asp:DropDownList ID="ddlMTC" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Storage:</td><td><asp:DropDownList ID="ddlStorageCode" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Status:</td><td><asp:TextBox ID="tbStatus" runat="server" Text='<%# Bind("status") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Whole Farm Planner:</td><td><%# WACGlobal_Methods.SpecialText_Global_Planner(Eval("fk_farmBusiness"))%></td></tr>
                                                                <tr class="taT"><td class="B taR">Nutrient Management Planner:</td><td><asp:DropDownList ID="ddlDesignerEngineerNMP" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">CREP:</td><td><asp:DropDownList ID="ddlCREP" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">AWEP Signup:</td><td><asp:DropDownList ID="ddlAWEPSignup" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">BMP Number NMP:</td><td><asp:DropDownList ID="ddlBMPAgNMP" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Primary Enterprise:</td><td><asp:TextBox ID="tbEnterprisePrimary" runat="server" Text='<%# Bind("enterprise_primary") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Secondary Enterprise:</td><td><asp:TextBox ID="tbEnterpriseSecondary" runat="server" Text='<%# Bind("enterprise_secondary") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Animal Units:</td><td><asp:Label ID="lblAnimalUnits" runat="server"></asp:Label></td></tr>
                                                                <tr class="taT"><td class="B taR">Units Designed:</td><td><asp:TextBox ID="tbAcresPlanned" runat="server" Text='<%# Bind("acres_planned") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres of Corn:</td><td><asp:TextBox ID="tbAcresCorn" runat="server" Text='<%# Bind("acres_corn") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Goal:</td><td><asp:TextBox ID="tbGoal" runat="server" Text='<%# Bind("goal") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Crop Year:</td><td><asp:DropDownList ID="ddlCropYear" runat="server"></asp:DropDownList></tr>
                                                                <tr class="taT"><td class="B taR">Crop Year Expiration:</td><td><asp:DropDownList ID="ddlCropYearExpiration" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Operations Maintenance Signature Date:</td><td><uc1:AjaxCalendar ID="tbCalNMPOpMaintSignatureDate" runat="server" Text='<%# Bind("operationsMaintenance_signature_date") %>' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Completed Date:</td><td><uc1:AjaxCalendar ID="tbCalWFP3SignatureDate" runat="server" Text='<%#Bind("wfp3_signature_date") %> ' /></td></tr>
                                                                <tr class="taT"><td class="B taR">WFP3 Year:</td><td><asp:TextBox ID="tbWFP3Year" runat="server" Text='<%# Bind("wfp3_year") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Most Recent Sample Date:</td><td><uc1:AjaxCalendar ID="tbCalMostRecentSampleDate" runat="server" Text='<%# Bind("sample_date_most_recent") %>' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Sample Count:</td><td><asp:TextBox ID="tbSampleCount" runat="server" Text='<%# Bind("sample_cnt") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Sample Priority:</td><td><asp:TextBox ID="tbSamplePriority" runat="server" Text='<%# Bind("sample_priority") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Sampler:</td><td><asp:TextBox ID="tbSampler" runat="server" Text='<%# Bind("sampler") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">FSA Release Date:</td><td><uc1:AjaxCalendar ID="tbCalFSAReleaseDate" runat="server" Text='<%# Bind("fsa_release_date") %>' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Manure Sample Date:</td><td><uc1:AjaxCalendar ID="tbCalManureSampleDate" runat="server" Text='<%# Bind("manure_sample_date") %>' /></td></tr>
                                                                <tr class="taT"><td class="B taR">NMP Workshop Date:</td><td><uc1:AjaxCalendar ID="tbCalNMPWorkshopDate" runat="server" Text='<%# Bind("nmp_workshop_date") %> ' /></td></tr>
                                                                <tr class="taT"><td class="B taR">WFP1 Signatures:</td><td><asp:TextBox ID="tbWFP1Signatures" runat="server" Text='<%# Bind("wfp1_signatures") %>' Width="400px"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Spreader Calibration Date:</td><td><uc1:AjaxCalendar ID="tbCalSpreaderCalibrationDate" runat="server" Text='<%# Bind("spreader_calibration_date") %>'  /></td></tr>
                                                                <tr class="taT"><td class="B taR">Comment:</td><td><asp:TextBox ID="tbComment" runat="server" Text='<%# Bind("comment") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </asp:FormView>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_Tier1" HeaderText="AEM" runat="server">
                                                <ContentTemplate>
                                                    <div><span class="fsM B">AEM</span></div>
                                                    <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewerAEM" SectorCode="A_AEM" />
                                                    <hr />
                                                    <asp:FormView ID="fvAg_Tier1" runat="server" Width="100%" OnModeChanging="fvAg_Tier1_ModeChanging" 
                                                        OnItemUpdating="fvAg_Tier1_ItemUpdating" OnItemInserting="fvAg_Tier1_ItemInserting">
                                                        <EmptyDataTemplate><div class="I">No AEM Record <asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="New" Text="[Add AEM Record]"  ></asp:LinkButton></div></EmptyDataTemplate>
                                                        <ItemTemplate>
                                                            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessTier1"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <div style="margin:10px 0px 10px 0px;"><asp:Literal ID="litAg_Tier1" runat="server"></asp:Literal></div>
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Tier 1 Received:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("tier1_recd")) %></td></tr>
                                                                <tr class="taT"><td class="B taR">Tier 2 Received:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("tier2_recd")) %></td></tr>
                                                                <tr class="taT"><td class="B taR">Tier 2 Points:</td><td><%# Eval("tier2_points") %></td></tr>
                                                               
                                                                <tr class="taT"><td class="B taR">AU (Animal-based):</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_Tier1_Animal_GetAU(Eval("fk_farmBusiness")) %></td></tr>
                                                                <tr class="taT"><td class="B taR">AU (EOH):</td><td><%# Eval("eoh_au_manual") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Ranking:</td><td><%# Eval("ranking") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Farm Name Location:</td><td><%# Eval("farmNameLocation") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Farm Description:</td><td><%# Eval("farm_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Rent:</td><td><%# Eval("acres_rent") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Forest:</td><td><%# Eval("acres_forest") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Crop:</td><td><%# Eval("acres_till") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Crop Rent:</td><td><%# Eval("acres_till_rent") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Hay:</td><td><%# Eval("acres_hay") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Pasture:</td><td><%# Eval("acres_pasture") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Total:</td><td><%# Eval("acres_total") %></td></tr>
                                                                <tr class="taT"><td class="B taR">$5K Income (EOH):</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("eoh_income1K")) %></td></tr>
                                                                <tr class="taT"><td class="B taR">Last Update (EOH):</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("eoh_lastUpdate")) %></td></tr>
                                                                <tr class="taT"><td class="B U taR fsXS"><br />LEGACY</td><td></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">EOH ID:</td><td class="fsXS"><%# Eval("eoh_id_legacy") %></td></tr>
                                                            </table>
                                                            <div style="margin-top:20px;"><span class="fsM B">Tier 1 Animals</span> [<asp:LinkButton ID="lbAg_Tier1_Animal_Add" runat="server" Text="Add Tier 1 Animal" OnClick="lbAg_Tier1_Animal_Add_Click"></asp:LinkButton>]</div>
                                                            <hr />
                                                            <div>
                                                            <asp:GridView ID="gvAg_Tier1_Animals" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                                <HeaderStyle BackColor="#DDDDDD" />
                                                                <RowStyle VerticalAlign="Top" />
                                                                <AlternatingRowStyle BackColor="#EEEEEE" />
                                                                <EmptyDataTemplate><div class="I">No Tier 1 Animal Records</div></EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField><ItemTemplate>[<asp:LinkButton ID="lbAg_Tier1_Animal_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmBusinessTier1Animal") %>' OnClick="lbAg_Tier1_Animal_View_Click"></asp:LinkButton>]</ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Animal"><ItemTemplate><%# Eval("list_animal.animal") %></ItemTemplate></asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Count" DataField="cnt" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            </div>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <div><span class="fsM B">Tier 1</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessTier1"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Tier 1 Received:</td><td><uc1:AjaxCalendar ID="calTier1Received" runat="server" Text='<%# Bind("tier1_recd") %>'  /></td></tr>
                                                                <tr class="taT"><td class="B taR">Tier 2 Received:</td><td><uc1:AjaxCalendar ID="calTier2Received" runat="server" Text='<%# Bind("tier2_recd") %> ' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Tier 2 Points:</td><td><asp:TextBox ID="tbTier2Points" runat="server" Text='<%# Bind("tier2_points") %>'></asp:TextBox></td></tr>
                                                                <%--<tr class="taT"><td class="B taR">PJ Points (EOH):</td><td><asp:TextBox ID="tbfarmRankingJudgement_eoh" runat="server" Text='<%# Bind("farmRankingJudgement_eoh") %>'></asp:TextBox></td></tr>--%>
                                                                <tr class="taT"><td class="B taR">AU (EOH):</td><td><asp:TextBox ID="tbEOHAUManual" runat="server" Text='<%# Bind("eoh_au_manual") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Ranking:</td><td><asp:TextBox ID="tbRanking" runat="server" Text='<%# Bind("ranking") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Farm Name Location:</td><td><asp:TextBox ID="tbFarmNameLocation" runat="server" Text='<%# Bind("farmNameLocation") %>' Width="400px"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Farm Description:</td><td><asp:TextBox ID="tbFarmDescription" runat="server" Text='<%# Bind("farm_descrip") %>' Width="400px"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Rent:</td><td><asp:TextBox ID="tbAcresRent" runat="server" Text='<%# Bind("acres_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Forest:</td><td><asp:TextBox ID="tbAcresForest" runat="server" Text='<%# Bind("acres_forest") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Crop:</td><td><asp:TextBox ID="tbAcresTill" runat="server" Text='<%# Bind("acres_till") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Crop Rent:</td><td><asp:TextBox ID="tbAcresTillRent" runat="server" Text='<%# Bind("acres_till_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Hay:</td><td><asp:TextBox ID="tbAcresHay" runat="server" Text='<%# Bind("acres_hay") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Pasture:</td><td><asp:TextBox ID="tbAcresPasture" runat="server" Text='<%# Bind("acres_pasture") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Total:</td><td><%# Eval("acres_total") %></td></tr>
                                                                <tr class="taT"><td class="B taR">$5K Income (EOH):</td><td><asp:DropDownList ID="ddlEOHIncome1K" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Last Update (EOH):</td><td><uc1:AjaxCalendar ID="tbCalEOHLastUpdate" runat="server" Text='<%# Bind("eoh_lastUpdate") %> ' /></td></tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                        <InsertItemTemplate>
                                                            <div><span class="fsM B">Tier 1</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessTier1"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Tier 1 Received:</td><td><uc1:AjaxCalendar ID="calTier1Received" runat="server" Text='<%# Bind("tier1_recd") %> ' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Tier 2 Received:</td><td><uc1:AjaxCalendar ID="calTier2Received" runat="server" Text='<%# Bind("tier2_recd") %> ' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Tier 2 Points:</td><td><asp:TextBox ID="tbTier2Points" runat="server" Text='<%# Bind("tier2_points") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">AU (EOH):</td><td><asp:TextBox ID="tbEOHAUManual" runat="server" Text='<%# Bind("eoh_au_manual") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Ranking:</td><td><asp:TextBox ID="tbRanking" runat="server" Text='<%# Bind("ranking") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Farm Name Location:</td><td><asp:TextBox ID="tbFarmNameLocation" runat="server" Text='<%# Bind("farmNameLocation") %>' Width="400px"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Farm Description:</td><td><asp:TextBox ID="tbFarmDescription" runat="server" Text='<%# Bind("farm_descrip") %>' Width="400px"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Rent:</td><td><asp:TextBox ID="tbAcresRent" runat="server" Text='<%# Bind("acres_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Forest:</td><td><asp:TextBox ID="tbAcresForest" runat="server" Text='<%# Bind("acres_forest") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Crop:</td><td><asp:TextBox ID="tbAcresTill" runat="server" Text='<%# Bind("acres_till") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Crop Rent:</td><td><asp:TextBox ID="tbAcresTillRent" runat="server" Text='<%# Bind("acres_till_rent") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Hay:</td><td><asp:TextBox ID="tbAcresHay" runat="server" Text='<%# Bind("acres_hay") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Pasture:</td><td><asp:TextBox ID="tbAcresPasture" runat="server" Text='<%# Bind("acres_pasture") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Acres Total:</td><td><%# Eval("acres_total") %></td></tr>
                                                            
                                                            </table>
                                                        </InsertItemTemplate>
                                                    </asp:FormView>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_WFP2" HeaderText="WFP2" runat="server">
                                                <ContentTemplate>
                                                    <div  style="float:left;" class="fsM B">WFP2</div>
                                                    <div style="clear:both"> </div>
                                                    <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewerWFP2" SectorCode="A_WFP2" />
                                                    <asp:Panel ID="pnlAg_WFP2_MultipleWFP2s" runat="server" Visible="false">
                                                        <span class="B">WFP2 Type:</span> <asp:DropDownList ID="ddlAg_WFP2_Type" runat="server" OnSelectedIndexChanged="ddlAg_WFP2_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </asp:Panel>
                                                    <asp:FormView ID="fvAg_WFP2" runat="server" Width="100%" OnModeChanging="fvAg_WFP2_ModeChanging" OnItemUpdating="fvAg_WFP2_ItemUpdating">
                                                        <EmptyDataTemplate><div><span class="fsM B">WFP2</span></div><div class="I">No WFP2 Record</div></EmptyDataTemplate>
                                                        <ItemTemplate>
                                                            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp2"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Pollutant I Description:</td><td><%# Eval("pollutant_i_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant II Description:</td><td><%# Eval("pollutant_ii_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant III Description:</td><td><%# Eval("pollutant_iii_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant IV Description:</td><td><%# Eval("pollutant_iv_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant V Description:</td><td><%# Eval("pollutant_v_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant V.2 Description (CREP):</td><td><%# Eval("pollutant_v2_descrip_CREP")%></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant VI Description:</td><td><%# Eval("pollutant_vi_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant VII Description:</td><td><%# Eval("pollutant_vii_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant VIII Description:</td><td><%# Eval("pollutant_viii_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant IX Description:</td><td><%# Eval("pollutant_ix_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant X Description:</td><td><%# Eval("pollutant_x_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant XI Description:</td><td><%# Eval("pollutant_xi_descrip") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Planner:</td><td><%# WACGlobal_Methods.SpecialText_Global_DesignerEngineer(Eval("list_designerEngineer"), true, true) %></td></tr>
                                                                <tr class="taT"><td class="B taR">Planner/Technician 1:</td><td><%# WACGlobal_Methods.SpecialText_Global_DesignerEngineer(Eval("list_designerEngineer1"), true, true)%></td></tr>
                                                                <tr class="taT"><td class="B taR">Planner/Technician 2:</td><td><%# WACGlobal_Methods.SpecialText_Global_DesignerEngineer(Eval("list_designerEngineer2"), true, true)%></td></tr>
                                                                <tr class="taT"><td class="B taR">Agency:</td><td><%# Eval("list_agency.agency") %></td></tr>
                                                            </table>
                                                            <div style="margin-top:20px;"><span class="fsM B">WFP2 Revisions </span>[<asp:LinkButton ID="lbAg_WFP2_Version_Add" runat="server" Text="Add WFP2 Revision" OnClick="lbAg_WFP2_Version_Add_Click"></asp:LinkButton>]</div>
                                                            <hr />
                                                            <div>
                                                                <asp:GridView ID="gvAg_WFP2_Revisions" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                                    <HeaderStyle BackColor="#DDDDDD" />
                                                                    <RowStyle VerticalAlign="Top" />
                                                                    <AlternatingRowStyle BackColor="#EEEEEE" />
                                                                    <EmptyDataTemplate><div class="I">No WFP2 Revision Records</div></EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField><ItemTemplate>[<asp:LinkButton ID="lbAg_WFP2_Version_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_form_wfp2_version") %>' OnClick="lbAg_WFP2_Version_View_Click"></asp:LinkButton>]</ItemTemplate></asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Revision" DataField="version" />
                                                                        <asp:TemplateField HeaderText="Inhouse Revision"><ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("inhouse_revision")) %></ItemTemplate></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Setup Date"><ItemTemplate><%# WACGlobal_Methods.Format_Global_Date(Eval("setup_date")) %></ItemTemplate></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approved Date"><ItemTemplate><%# WACGlobal_Methods.Format_Global_Date(Eval("approved_date")) %></ItemTemplate></asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp2"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Pollutant I Description:</td><td><asp:TextBox ID="tbPollutantI" runat="server" Text='<%# Bind("pollutant_i_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant II Description:</td><td><asp:TextBox ID="tbPollutantII" runat="server" Text='<%# Bind("pollutant_ii_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant III Description:</td><td><asp:TextBox ID="tbPollutantIII" runat="server" Text='<%# Bind("pollutant_iii_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant IV Description:</td><td><asp:TextBox ID="tbPollutantIV" runat="server" Text='<%# Bind("pollutant_iv_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant V Description:</td><td><asp:TextBox ID="tbPollutantV" runat="server" Text='<%# Bind("pollutant_v_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant V.2 Description (CREP):</td><td><asp:TextBox ID="tbPollutantV2_CREP" runat="server" Text='<%# Bind("pollutant_v2_descrip_CREP") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant VI Description:</td><td><asp:TextBox ID="tbPollutantVI" runat="server" Text='<%# Bind("pollutant_vi_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant VII Description:</td><td><asp:TextBox ID="tbPollutantVII" runat="server" Text='<%# Bind("pollutant_vii_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant VIII Description:</td><td><asp:TextBox ID="tbPollutantVIII" runat="server" Text='<%# Bind("pollutant_viii_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant IX Description:</td><td><asp:TextBox ID="tbPollutantIX" runat="server" Text='<%# Bind("pollutant_ix_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant X Description:</td><td><asp:TextBox ID="tbPollutantX" runat="server" Text='<%# Bind("pollutant_x_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Pollutant XI Description:</td><td><asp:TextBox ID="tbPollutantXI" runat="server" Text='<%# Bind("pollutant_xi_descrip") %>' TextMode="MultiLine" Width="600px" Rows="2"></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Planner:</td><td><asp:DropDownList ID="ddlPlanner" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Planner/Technician 1:</td><td><asp:DropDownList ID="ddlPlannerTechnician1" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Planner/Technician 2:</td><td><asp:DropDownList ID="ddlPlannerTechnician2" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Agency:</td><td><asp:DropDownList ID="ddlAgency" runat="server"></asp:DropDownList></td></tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </asp:FormView>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_WFP3" HeaderText="WFP3" runat="server" >
                                                <ContentTemplate>
                                                    <uc:WACAG_WFP3_Grid runat="server" ID="ucWACAG_WFP3_Grid" />
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                            <ajtk:TabPanel ID="tpAg_WLProjects" HeaderText="WL Groups" runat="server">
                                                <ContentTemplate>
                                                    <div><span class="fsM B">Workload Groupings</span> [<asp:LinkButton ID="lbAg_WLProject_Add" runat="server" Text="Add Workload Grouping" OnClick="lbAg_WLProject_Add_Click"></asp:LinkButton>]</div>
                                                    <hr />
                                                    <div style="margin:10px 0px 0px 20px;">
                                                        <asp:GridView ID="gvAg_WLProjects" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="taT">
                                                            <HeaderStyle BackColor="#DDDDDD" />
                                                            <RowStyle VerticalAlign="Top" />
                                                            <AlternatingRowStyle BackColor="#EEEEEE" />
                                                            <EmptyDataTemplate><div class="I">No Workload Grouping Records</div></EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>[<asp:LinkButton ID="lbAg_WLProject_View" runat="server" Text="View"
                                                                        CommandArgument='<%# Eval("pk_farmBusinessWLProject") %>' OnClick="lbAg_WLProject_View_Click"></asp:LinkButton>]
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        
                                                                <asp:BoundField HeaderText="Workload Grouping" DataField="ImplementationProject" />
                                                                <asp:TemplateField HeaderText="BMPs">
                                                                    <ItemTemplate>
                                                                        <asp:ListView ID="lvAg_WLProject_BMPs" runat="server" DataSource='<%# WACGlobal_Methods.Order_Agriculture_farmBusinessWLProjectBMP_BMPNumber(Eval("farmBusinessWLProjectBMPs")) %>'>
                                                                            <EmptyDataTemplate><div class="I">No Workload Project BMP Records</div></EmptyDataTemplate>
                                                                            <ItemTemplate>
                                                                                <div><%# WACGlobal_Methods.SpecialText_Agriculture_WorkloadBmp(Eval("fk_bmp_ag")) %></div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Design Year" DataField="design_year" />
                                                                <asp:BoundField HeaderText="Build Year" DataField="build_year" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </ajtk:TabPanel>
                                        </ajtk:TabContainer>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Farm Business</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" 
                                        CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" 
                                        CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" 
                                        CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusiness"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Farm ID:</td><td><%# Eval("farmID") %><asp:LinkButton ID="lbAg_SetFarmID" runat="server" Text="Assign Farm ID" CommandArgument='<%# Eval("pk_farmBusiness") %>' OnClick="lbAg_SetFarmID_Click" Visible="false"></asp:LinkButton></td></tr>
                                            <tr class="taT"><td class="B taR">Farm Name:</td><td><asp:TextBox ID="tbFarmName" runat="server" Text='<%# Bind("farm_name") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr><td colspan="2"><hr /><asp:HiddenField ID="hfPropertyPK" runat="server" /></td></tr>
                                            <tr class="taT">
                                                <td colspan="2">
                                                    <div class="NestedDivLevel01">
                                                        <div>Change Farm Address</div>
                                                        <hr />
                                                        <table class="tp3">
                                                            <tr class="taT">
                                                                <td colspan="2">
                                                                    <asp:Panel ID ="pnlPropertyLookup" runat="server">
                                                                        <table class="tp3">
                                                                   
                                                                            <tr class="taT">
                                                                                <td class="taL B">Current Address:</td>
                                                                                <td class="taL"><asp:label ID="lblCurrentAddress" runat="server"></asp:label> </td>
                                                                               
                                                                            </tr>
                                                                            <tr class="taT"><td class="B taR">Lookup New Address with Zip Code:</td><td><asp:DropDownList ID="ddlZipCode" runat="server" OnSelectedIndexChanged="ddlZipCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                                                            <tr class="taT">
                                                                                <td class="B taR">Address Starts With?</td>
                                                                                <td><asp:TextBox ID="tbAddressStartsWith" runat="server" Visible="false" ></asp:TextBox>&nbsp;<asp:Button ID="FindStartsWith" runat="server" Text="Click to Search" OnClick="FindStartsWith_Click" Visible="false" /></td>
                                                                            </tr>
                                                                            <tr class="taT">
                                                                                <td class="B taR">Select Address:</td>
                                                                                <td><asp:DropDownList ID="ddlAddress" runat="server" Visible="false"></asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr class="taT">
                                                                                <td class="taL" colspan="2"><asp:Button ID="btnInsertAddress" runat="server" Text="Change to Selected Address" OnClick="btnInsertAddress_Click" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>

                                            </tr>
                                            <tr><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlRegionWAC" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">WAC Program:</td><td><asp:DropDownList ID="ddlProgramWAC" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">WFP0 Signed Date:</td><td><uc1:AjaxCalendar ID="tbCalWFP0SignedDate" runat="server" Text='<%# Bind("wfp0_signed") %>' /></td></tr>
                                            <tr class="taT"><td class="B taR">WFP1 Signed Date:</td><td><uc1:AjaxCalendar ID="tbCalWFP1SignedDate" runat="server" Text='<%# Bind("wfp1_signed_date") %>' /></td></tr>
                                            <tr class="taT"><td class="B taR">ASR Required:</td><td><asp:DropDownList ID="ddlASRRequired" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Farm Size:</td><td><asp:DropDownList ID="ddlFarmSize" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Forestry:</td><td><asp:DropDownList ID="ddlForestry" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">FarmToMarket:</td><td><asp:DropDownList ID="ddlFarmToMarket" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Prior Large Farm ID:</td><td><asp:TextBox ID="tbPriorLargeFarmID" runat="server" Text='<%# Bind("prior_LF_FarmID") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Sold Farm:</td><td><asp:DropDownList ID="ddlSoldFarm" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Farm Transferred To:</td><td><%# Eval("farmBusiness2.farmID")%></td></tr>
                                            <tr class="taT"><td class="B taR">Farm Transferred From:</td><td><%# Eval("farmBusiness1.farmID")%></td></tr>
                                            <tr class="taT"><td class="B taR">Prior Owner:</td><td><asp:TextBox ID="tbPriorOwner" runat="server" Text='<%# Bind("priorOwner") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Farm Count:</td><td><asp:TextBox ID="tbFarms" runat="server" Text='<%# Bind("farm_cnt") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Subfarm Count:</td><td><asp:TextBox ID="tbSubfarms" runat="server" Text='<%# Bind("subfarm_cnt") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Multiple Farm Equiv.:</td><td><asp:Label ID="lblMultipleFarmEquiv" runat="server" Text='<%# Bind("multiple_farm_equivalents") %>'></asp:Label></td></tr>
                                            <tr class="taT"><td class="B taR">Group PI:</td><td><asp:DropDownList ID="ddlGroupPI" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Environmental Impact:</td><td><asp:DropDownList ID="ddlEnvironmentalImpact" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">IA Prior to Implementation:</td><td><asp:DropDownList ID="ddlIAPriorToImplementation" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Implementation Commenced:</td><td><asp:DropDownList ID="ddlPriorImplementationCommenced" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Implementation Substantially Complete Date:</td>
                                                    <td><uc1:AjaxCalendar ID="tbCalISCDate" runat="server" Text='<%# Bind("implementation_substantially_complete_date") %>' /></td></tr>
                                            <tr class="taT"><td class="B taR">Implementation Fully Complete Date:</td>
                                                    <td><uc1:AjaxCalendar ID="tbCalIFCDate" runat="server" Text='<%# Bind("implementation_fully_complete_date") %>' /></td></tr>
                                    
                                            <tr class="taT"><td class="B taR">Status Comment:</td><td><asp:TextBox ID="tbStatusComment" runat="server" Text='<%# Bind("status_comment") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td></tr>
                                            
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Farm Business</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Owner:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet1" runat="server" StrParticipantType="A" /></td>
                                            <tr class="taT"><td class="B taR">Operator:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet2" runat="server" StrParticipantType="A" /></td>
                                            <tr class="taT"><td class="B taR">Farm Name:</td><td><asp:TextBox ID="tbFarmName" runat="server" Text='<%# Bind("farm_name") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlRegionWAC" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">WAC Program:</td><td><asp:DropDownList ID="ddlProgramWAC" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Farm Size:</td><td><asp:DropDownList ID="ddlFarmSize" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Sold Farm:</td><td><asp:DropDownList ID="ddlSoldFarm" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Generate FarmID:</td><td><asp:CheckBox ID="cbGenerateFarmID" runat="server" /></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>                            
                            </asp:FormView>   
                        </div>                
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbAg_Search_ReloadReset" />
                        <asp:AsyncPostBackTrigger ControlID="lbAgAll" />
                        <asp:AsyncPostBackTrigger ControlID="ddlAg_Search_SA" />
                    </Triggers>
                </asp:UpdatePanel>
<!-- AG_ANIMAL -->
                <asp:Panel ID="pnlAg_Animal" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical" >
                    <asp:UpdatePanel ID="upAg_Animal" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Animal</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_Animal_Close" runat="server" Text="Close" 
                                OnClick="lbAg_Animal_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_Animal_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_Animal" runat="server" Width="100%" OnModeChanging="fvAg_Animal_ModeChanging" OnItemUpdating="fvAg_Animal_ItemUpdating" OnItemInserting="fvAg_Animal_ItemInserting" OnItemDeleting="fvAg_Animal_ItemDeleting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; background-color:#EEEEDD; padding:5px;">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessAnimal"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Animal:</td><td><%# Eval("list_animal.animal") %></td></tr>
                                            <tr class="taT"><td class="B taR">ASR Year:</td><td><%# Eval("ASR_yr") %></td></tr>
                                        </table>
                                        <hr />
                                        <div style="padding:5px; background-color:#DDEEEE; border:solid 1px #888888;">
                                            <div style="margin-bottom:5px;"><span class="fsM B">Animal Ages</span> >> <asp:LinkButton ID="lbAg_Animal_Age_Add" runat="server" Text="Add an Animal Age" OnClick="lbAg_Animal_Age_Add_Click"></asp:LinkButton></div>
                                            <div style="margin-left:5px;">
                                                <asp:ListView ID="lvAg_Animal_Ages" runat="server" DataSource='<%# Eval("farmBusinessAnimalAges") %>'>
                                                    <EmptyDataTemplate><div class="I">No Animal Age Records</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="taT">
                                                            <tr class="taT">
                                                                <td></td>
                                                                <td class="B U">Age</td>
                                                                <td class="B U taR">Count</td>
                                                                <td class="B U taR">Weight</td>
                                                                <td class="B U taR">Total</td>
                                                                <td class="B U taR">AU</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbAg_Animal_Age_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmBusinessAnimalAge") %>' OnClick="lbAg_Animal_Age_View_Click"></asp:LinkButton>]</td>
                                                            <td><%# Eval("list_animalAge.ageBracket") %></td>
                                                            <td class="taR"><%# Eval("cnt") %></td>
                                                            <td class="taR"><%# Eval("weight") %> lbs.</td>
                                                            <td class="taR"><%# Eval("total") %> lbs.</td>
                                                            <td class="taR"><%# WACGlobal_Methods.Math_Round(Eval("AU"), 2) %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <asp:FormView ID="fvAg_Animal_Age" runat="server" Width="100%" OnModeChanging="fvAg_Animal_Age_ModeChanging" OnItemUpdating="fvAg_Animal_Age_ItemUpdating" OnItemInserting="fvAg_Animal_Age_ItemInserting" OnItemDeleting="fvAg_Animal_Age_ItemDeleting">
                                                <ItemTemplate>
                                                    <hr />
                                                    <div style="border:solid 1px #999999; background-color:#CCDDDD; padding:5px;">
                                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbAg_Animal_Age_Close" runat="server" Text="Close" OnClick="lbAg_Animal_Age_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessAnimalAge"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="B taR">Age:</td><td><%# Eval("list_animalAge.ageBracket") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Count:</td><td><%# Eval("cnt") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Weight:</td><td><%# Eval("weight") %> lbs.</td></tr>
                                                            <tr class="taT"><td class="B taR">Total:</td><td><%# Eval("total") %> lbs.</td></tr>
                                                            <tr class="taT"><td class="B taR">AU:</td><td><%# WACGlobal_Methods.Math_Round(Eval("AU"), 2) %></td></tr>
                                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <hr />
                                                    <div style="border:solid 1px #999999; background-color:#CCDDDD; padding:5px;">
                                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessAnimalAge"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="B taR">Age:</td><td><asp:DropDownList ID="ddlAge" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="B taR">Count:</td><td><asp:TextBox ID="tbCount" runat="server" Text='<%# Bind("cnt") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Weight:</td><td><asp:TextBox ID="tbWeight" runat="server" Text='<%# Bind("weight") %>'></asp:TextBox> lbs.</td></tr>
                                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                        </table>
                                                    </div>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <hr />
                                                    <div style="border:solid 1px #999999; background-color:#CCDDDD; padding:5px;">
                                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="B taR">Age:</td><td><asp:DropDownList ID="ddlAge" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="B taR">Count:</td><td><asp:TextBox ID="tbCount" runat="server" Text='<%# Bind("cnt") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Weight:</td><td><asp:TextBox ID="tbWeight" runat="server" Text='<%# Bind("weight") %>'></asp:TextBox> lbs.</td></tr>
                                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                        </table>
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; background-color:#EEEEDD; padding:5px;">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessAnimal"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Animal:</td><td><asp:DropDownList ID="ddlAnimal" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">ASR Year:</td><td><asp:DropDownList ID="ddlASRYear" runat="server"></asp:DropDownList></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; background-color:#EEEEDD; padding:5px;">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Animal:</td><td><asp:DropDownList ID="ddlAnimal" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">ASR Year:</td><td><asp:DropDownList ID="ddlASRYear" runat="server"></asp:DropDownList></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_Animal" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_Animal" runat="server" TargetControlID="lbHidden_Ag_Animal" PopupControlID="pnlAg_Animal" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>

<!-- AG_ASR -->
                <asp:Panel ID="pnlAg_ASR" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_ASR" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Annual Status Review</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_ASR_Close" runat="server" Text="Close" OnClick="lbAg_ASR_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_ASR_Header" runat="server"></asp:Literal>
                            <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewerASR" SectorCode="A_ASR" />                    
                            <hr />
                            <asp:FormView ID="fvAg_ASR" runat="server" Width="100%" OnModeChanging="fvAg_ASR_ModeChanging" OnItemUpdating="fvAg_ASR_ItemUpdating" OnItemInserting="fvAg_ASR_ItemInserting" OnItemDeleting="fvAg_ASR_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_asrAg"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Year:</td><td><%# Eval("year") %></td></tr>
                                            <tr class="taT"><td class="B taR">Date Completed:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Interviewee:</td><td><%# Eval("interviewee") %></td></tr>
                                            <tr class="taT"><td class="B taR">Planner:</td><td><%# Eval("list_designerEngineer.designerEngineer") %></td></tr>
                                            <tr class="taT"><td class="B taR">Assigned To:</td><td><%# WACGlobal_Methods.SpecialText_Global_DesignerEngineer(Eval("list_designerEngineer1"), false, true)%></td></tr>
                                            <tr class="taT"><td class="B taR">Type:</td><td><%# Eval("list_asrType.asrType") %></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Revision Required:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("revisionReqd")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Refer to Question:</td><td><%# Eval("revisionReqd_reference") %></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Land Change:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("landChange"))%></td></tr>
                                            <tr class="taT"><td class="B taR">Land Change Note:</td><td><%# Eval("landChange_note") %></td></tr>
                                            <tr class="taT"><td class="B taR">Goals Change:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("goalsChange")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Goals Change Note:</td><td><%# Eval("goalsChange_note") %></td></tr>
                                            <tr class="taT"><td class="B taR">WFP Effective:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("WFPEffective")) %></td></tr>
                                            <tr class="taT"><td class="B taR">WFP Effective Note:</td><td><%# Eval("WFPEffective_note") %></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs Effective:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("BMPsEffective")) %></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs Effective Note:</td><td><%# Eval("BMPsEffective_note")%></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs OMA:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("BMPsOMA")) %></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs OMA Note:</td><td><%# Eval("BMPsOMA_note")%></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Revision:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("revision")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Participant Note:</td><td><%# Eval("revision_note_participant") %></td></tr>
                                            <tr class="taT"><td class="B taR">Interviewer Note:</td><td><%# Eval("revision_note") %></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Issues:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("issues")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Participant Note:</td><td><%# Eval("issues_note") %></td></tr>
                                            <tr class="taT"><td class="B taR">Interviewer Note:</td><td><%# Eval("comment") %></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Active:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Crops:</td><td><%# Eval("inactive_crops") %></td></tr>
                                            <tr class="taT"><td class="B taR">Animals:</td><td><%# Eval("inactive_animal") %></td></tr>
                                            <tr class="taT"><td class="B taR">Land Utilization:</td><td><%# Eval("inactive_landUtilization") %></td></tr>
                                            <tr class="taT"><td class="B taR">Other:</td><td><%# Eval("inactive_other") %></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">CREP Info Request:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("CREPInfoReq")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Easement Info Request:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("easementInfoReq")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Forestry Info Request:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("forestryInfoReq")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Farm To Market Info Request:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("F2MInfoReq")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Has Sign:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("hasSign")) %></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>                                     
                                            <tr class="taT"><td colspan="2">
                                                <uc:WACAG_QMACheckbox runat="server" ID="cbAsrQma" />
                                            </td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                           
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_asrAg"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Year:</td><td><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Date Completed:</td><td><uc1:AjaxCalendar ID="tbCalASRDate" runat="server" Text='<%# Bind("date") %> '/></td></tr>
                                            <tr class="taT"><td class="B taR">Interviewee:</td><td><asp:TextBox ID="tbInterviewee" runat="server" Text='<%# Bind("interviewee")%>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Planner:</td><td><asp:DropDownList ID="ddlPlanner" runat="server"></asp:DropDownList></td></tr>
                               <%--             <tr class="taT"><td class="B taR">Planner:</td><td><%# Eval("list_designerEngineer.designerEngineer") %></td></tr>--%>
                                            <tr class="taT"><td class="B taR">Assigned To:</td><td><asp:DropDownList ID="ddlAssignTo" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Revision Required:</td><td><asp:DropDownList ID="ddlRevisionRequired" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Refer to Question:</td><td><asp:TextBox ID="tbRevisionRequiredReference" runat="server" Text='<%# Bind("revisionReqd_reference")%>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Land Change:</td><td><asp:DropDownList ID="ddlLandChange" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Land Change Note:</td><td><asp:TextBox ID="tbLandChangeNote" runat="server" Text='<%# Bind("landChange_note")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Goals Change:</td><td><asp:DropDownList ID="ddlGoalsChange" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Goals Change Note:</td><td><asp:TextBox ID="tbGoalsChangeNote" runat="server" Text='<%# Bind("goalsChange_note")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">WFP Effective:</td><td><asp:DropDownList ID="ddlWFPEffective" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">WFP Effective Note:</td><td><asp:TextBox ID="tbWFPEffectiveNote" runat="server" Text='<%# Bind("WFPEffective_note")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs Effective:</td><td><asp:DropDownList ID="ddlBMPsEffective" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs Effective Note:</td><td><asp:TextBox ID="tbBMPsEffectiveNote" runat="server" Text='<%# Bind("BMPsEffective_note")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs OMA:</td><td><asp:DropDownList ID="ddlBMPsOMA" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">BMPs OMA Note:</td><td><asp:TextBox ID="tbBMPsOMANote" runat="server" Text='<%# Bind("BMPsOMA_note")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Revision:</td><td><asp:DropDownList ID="ddlRevision" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Participant Note:</td><td><asp:TextBox ID="tbRevisionNoteByParticipant" runat="server" Text='<%# Bind("revision_note_participant")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Interviewer Note:</td><td><asp:TextBox ID="tbRevisionNote" runat="server" Text='<%# Bind("revision_note")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Issues:</td><td><asp:DropDownList ID="ddlIssues" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Participant Note:</td><td><asp:TextBox ID="tbIssuesNote" runat="server" Text='<%# Bind("issues_note")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Interviewer Note:</td><td><asp:TextBox ID="tbComment" runat="server" Text='<%# Bind("comment")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Crops:</td><td><asp:TextBox ID="tbInactiveCrops" runat="server" Text='<%# Bind("inactive_crops")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Animals:</td><td><asp:TextBox ID="tbInactiveAnimals" runat="server" Text='<%# Bind("inactive_animal")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Land Utilization:</td><td><asp:TextBox ID="tbInactiveLandUtilization" runat="server" Text='<%# Bind("inactive_landUtilization")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Other:</td><td><asp:TextBox ID="tbInactiveOther" runat="server" Text='<%# Bind("inactive_other")%>' TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">CREP Info Request:</td><td><asp:DropDownList ID="ddlCREPInfoRequest" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Easement Info Request:</td><td><asp:DropDownList ID="ddlEasementInfoRequest" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Forestry Info Request:</td><td><asp:DropDownList ID="ddlForestryInfoRequest" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Farm To Market Info Request:</td><td><asp:DropDownList ID="ddlF2MInfoRequest" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Has Sign:</td><td><asp:DropDownList ID="ddlHasSign" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>                                     
                                            <tr class="taT"><td colspan="2">
                                                <uc:WACAG_QMACheckbox runat="server" ID="cbAsrQma" />
                                            </td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                           
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Year:</td><td><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Planner:</td><td><asp:DropDownList ID="ddlPlanner" runat="server"></asp:DropDownList></td></tr>
                                          <%--  <tr class="taT"><td class="B taR">Planner:</td><td><asp:Label ID="AsrPlanner" runat="server"></asp:Label></td></tr>--%>
                                            <tr class="taT"><td class="B taR">Assigned To:</td><td><asp:DropDownList ID="ddlAssignTo" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_ASR" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_ASR" runat="server" TargetControlID="lbHidden_Ag_ASR" PopupControlID="pnlAg_ASR" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_BMP -->
                <asp:Panel ID="pnlAg_BMP" runat="server" CssClass="ModalPopup_Panel_Large" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_BMP" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <br />
                            <div class="fsM B" style="float:left;">Agriculture >> BMP</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_BMP_Close" runat="server" Text="Close" OnClick="lbAg_BMP_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_BMP_Header" runat="server"></asp:Literal>
                            <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewerBMP" SectorCode="A_BMP" />
                            <hr />                           
                            <asp:FormView ID="fvAg_BMP" runat="server" Width="100%" OnModeChanging="fvAg_BMP_ModeChanging" OnItemUpdating="fvAg_BMP_ItemUpdating" OnItemInserting="fvAg_BMP_ItemInserting" OnItemDeleting="fvAg_BMP_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>
                                            <div style="float:left;">[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%>&nbsp;[Original BMP #: <%# Eval("bmp_nbr") %>]</span></div>              
                                            <div style="clear:both;"></div>
                                        </div>
                                        <hr />
                                            <table class="taT tp5" width="100%"> 
                                                <tr>
                                                    <td class="B taL" ><%# Eval("fk_BMPTypeCode") %></td>
                                                    <td class="tAl"><%# Eval("CompositBmpNum") %></td>
                                                    <td colspan="6"><asp:Label ID="IsBacklogEntity" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class ="B taL"  >Pollutant:</td>
                                                    <td colspan="3" class="taL"><%# Eval("list_pollutant_category.pk_pollutant_category_code") %>. <%# Eval("list_pollutant_category.descrip") %></td>        
                                                    <td class="B taL" >Status:</td>
                                                    <td class="taL" ><%# Eval("list_statusBMP.status") %></td> 
                                                    <td class="B taL">Workload Grouping:</td>
                                                    <td class="taL" ><asp:Label ID="workloadGroupingLabel" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr class="taT">      
                                                  
                                                    <td class="B taL">Number:</td>
                                                    <td class="taL" ><%# Eval("bmp") %></td>
                                                    <td class="B taL" >Descriptor:</td>
                                                    <td class="taL" ><%# Eval("AgBmpDescriptorCode.DescriptorCode") %></td>
                                                    <td class="B taL" >Qualifier Code:</td>
                                                    <td class="taL" ><%# Eval("fk_bmpCode_code") %></td>
                                                    <td class="B taL" >Qualifier Version:</td>
                                                    <td class="taL" ><%# Eval("QualifierVersion") %></td>  
                                                </tr>
                                                <tr class="taT">
                                                    <td class="B taL">Practice Code:</td>
                                                    <td><%# Eval("list_bmpPractice.pk_bmpPractice_code") %></td> 
                                                    <td class="B taL">Lifespan:</td>
                                                    <td colspan="3"><%# Eval("list_bmpPractice.life_reqd_yr") %></td>
                                                    <td class="B taL">Parent BMP:</td>
                                                    <td><asp:label ID="parent" runat="server"></asp:label></td>
                                                </tr>
                                                <tr class="taT">                                                         
                                                    <td class="B taL">BMP Description:</td>
                                                    <td colspan="7"><%# Eval("description") %></td>
                                                </tr>
                                                          
                                                <tr class="taT">
                                                    <td class="B taL">Location:</td>
                                                    <td colspan="3" class="taL"><%# Eval("location") %></td>
                                                    <td class="B taL">Tax Parcels:</td>
                                                    <td class="taL" colspan="3"><%# Eval("TaxParcels") %></td>
                                                </tr>
                                                       
                                            </table>

                                            <table width="100%">
                                                <tr class="taT">
                                                    <td class="B taL" style="width: 125px">Planning Units:</td>
                                                    <td style="width: 90px"><%# Eval("units_planned") %></td>
                                                    <td class="taL" style="width: 90px"><asp:Label ID="lblUnits1" runat="server"></asp:Label></td>
                                                    <td class="B taL" style="width: 175px">Prior Planning  Estimate:</td>
                                                    <td style="width: 90px"><%# Eval("est_plan_prior") %></td>
                                                    <td class="B taL" style="width: 175px">Current Planning Estimate:</td>
                                                    <td style="width: 90px"><%# Eval("est_plan_rev") %></td>
                                                </tr>
                                                <tr class="taT">
                                                    <td class="B taL" style="width: 125px">Designed Units:</td>
                                                    <td style="width: 90px"><%# Eval("units_designed") %></td>
                                                    <td class="taL" style="width: 90px"><asp:Label ID="lblUnits2" runat="server"></asp:Label></td>
                                                    <td class="B taL" style="width: 175px">Designed Cost:</td>
                                                    <td colspan="3" style="width: 90px"><%# Eval("design_cost") %></td>
                                                </tr>
                                                <tr class="taT">
                                                    <td class="B taL" style="width: 125px">Units Completed:</td>
                                                    <td style="width: 90px"><%# Eval("units_completed") %></td>
                                                    <td class="taL" style="width: 90px"><asp:Label ID="lblUnits3" runat="server"></asp:Label></td>
                                                    <td class="B taL" style="width: 175px">Final Cost:</td>
                                                    <td style="width: 90px"><%# Eval("final_cost") %></td>
                                                    <td class="B taL" style="width: 175px">Remaining Funds:</td>
                                                    <td style="width: 90px"><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetBalance(Eval("pk_bmp_ag"), true)%></td>
                                                </tr>
                                                <tr class="taT">
                                                    <td class="B taL" style="width: 175px">Designed Dimensions:</td>
                                                    <td colspan="6" style="width: 90px"><%# Eval("dimensions_designed") %></td>
                                                </tr> 
                                                <tr class="taT">
                                                    <td colspan="7"><hr /></td>
                                                </tr>                                                    
                                                <tr class="taT">
                                                    <td class="taL B" colspan="2">Supplemental&nbsp;Agreement:</td>
                                                    <td colspan="5"><%# WACGlobal_Methods.SpecialText_Agriculture_SA_TP_Owner(Eval("supplementalAgreementTaxParcel"))%></td>
                                                </tr>
                                                <tr class="taT">
                                                    <td class="taL B">SA Assignment:</td>
                                                    <td colspan="6"><%# Eval("list_SAAssignType.type")%></td>         
                                                </tr>
                                            </table>
                                        <hr />
                                        <asp:PlaceHolder ID="bmpDetailsPlaceHolder" runat="server">
                                            <div class="NestedDivLevel02">
                                                <div style="margin-bottom:5px;"><span class="fsM B">Funding</span> >> <asp:LinkButton ID="lbAg_BMP_Funding_Add" runat="server" Text="Add Funding" OnClick="lbAg_BMP_Funding_Add_Click"></asp:LinkButton></div>
                                                <div style="margin-left:5px;">
                                                    <asp:ListView ID="lvAg_BMP_Fundings" runat="server" DataSource='<%# Eval("bmp_ag_fundings") %>'>
                                                        <EmptyDataTemplate><div class="I">No Funding Records</div></EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <table class="tp5">
                                                                <tr class="taT">
                                                                    <td>&nbsp;</td>
                                                                    <td class="B U">Date</td>
                                                                    <td class="B U">Funding</td>
                                                                    <td class="B U">Purpose</td>
                                                                    <td class="B U">Agency</td>
                                                                    <td class="B U">Source</td>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="taT">
                                                                <td>[<asp:LinkButton ID="lbAg_BMP_Funding_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_bmp_ag_funding") %>' OnClick="lbAg_BMP_Funding_View_Click"></asp:LinkButton>]</td>
                                                                <td><%# WACGlobal_Methods.Format_Global_Date(Convert.ToDateTime(Eval("date"))) %></td>
                                                                <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("funding")) %></td>
                                                                <td><%# Eval("list_fundingPurpose.purpose") %></td>
                                                                <td><%# Eval("list_agencyFunding.agency") %></td>
                                                                <td><%# Eval("list_fundingSource.source") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <asp:FormView ID="fvAg_BMP_Funding" runat="server" Width="100%" OnModeChanging="fvAg_BMP_Funding_ModeChanging" OnItemUpdating="fvAg_BMP_Funding_ItemUpdating" OnItemInserting="fvAg_BMP_Funding_ItemInserting" OnItemDeleting="fvAg_BMP_Funding_ItemDeleting">
                                                    <ItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbAg_BMP_Funding_Close" runat="server" Text="Close" OnClick="lbAg_BMP_Funding_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_funding"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Purpose:</td><td><%# Eval("list_fundingPurpose.purpose") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Agency:</td><td><%# Eval("list_agencyFunding.agency") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Source:</td><td><%# Eval("list_fundingSource.source") %></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("funding")) %></td></tr>
                                                                <tr class="taT"><td class="B taR">Description:</td><td><%# Eval("description") %></td></tr>
                                                           
                                                            </table>
                                                            <hr />
                                                            <div class="fsM B">Transfer Funds</div>
                                                            <div>
                                                                <asp:Label ID="lblBMPTransferNoFunds" runat="server" Text="No funds are available for transfer" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                                <asp:Panel ID="pnlBMPTransfer" runat="server" Visible="false">
                                                                    <table class="taT" style="margin-left:20px;">
                                                                        <tr class="taT"><td class="taR">BMP To Transfer From:</td><td><asp:DropDownList ID="ddlBMPToTransferFrom" runat="server" OnSelectedIndexChanged="ddlBMPToTransferFrom_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                                                        <tr class="taT"><td class="taR">Amount to Transfer:</td><td><asp:TextBox ID="tbAmountToTransfer" runat="server"></asp:TextBox></td></tr>
                                                                        <tr class="taT"><td class="taR">Transfer to this BMP:</td><td><asp:Button ID="btnBMPAmountTransfer" runat="server" Text="Transfer" OnClick="btnBMPAmountTransfer_Click" /></td></tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B U taR fsXS">LEGACY</td><td></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">New BMP ID:</td><td class="fsXS"><%# Eval("bmp_id_new_legacy") %></td></tr>
                                                                <tr class="taT"><td class="B taR fsXS">Compenent ID:</td><td class="fsXS"><%# Eval("component_legacy") %></td></tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_funding"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalFundingDate" runat="server" Text='<%# Bind("date") %> '/></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Purpose:</td><td><asp:DropDownList ID="ddlFundingPurpose" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Agency:</td><td><asp:DropDownList ID="ddlFundingAgency" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Source:</td><td><asp:DropDownList ID="ddlFundingSource" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding:</td><td><asp:TextBox ID="tbFunding" runat="server" Text='<%# Bind("funding") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Description:</td><td><asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("description") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                          
                                                            </table>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                            <hr />
                                                        
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalFundingDate" runat="server" Text='<%# Bind("date") %>' /></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Purpose:</td><td><asp:DropDownList ID="ddlFundingPurpose" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Funding Agency:</td><td><asp:DropDownList ID="ddlFundingAgency" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT">
                                                                    <td class="B taR">Funding Source:</td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlFundingSource" runat="server" OnSelectedIndexChanged="ddlFundingSource_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                                                                        <asp:Label ID="lblBMPTransferNoFunds" runat="server" Text="No funds are available for transfer" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                                        <asp:Panel ID="pnlBMPTransfer" runat="server" Visible="false" HorizontalAlign="Left">
                                                                            <div class="B" style="margin-top:5px;">Transfer Funds</div>
                                                                            <table class="taT">
                                                                                <tr class="taT"><td class="taR">BMP To Transfer From:</td><td><asp:DropDownList ID="ddlBMPToTransferFrom" runat="server" OnSelectedIndexChanged="ddlBMPToTransferFrom_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr class="taT"><td class="B taR">Funding:</td><td><asp:TextBox ID="tbFunding" runat="server" Text='<%# Bind("funding") %>'></asp:TextBox></td></tr>
                                                                <tr class="taT"><td class="B taR">Description:</td><td><asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("description") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                         
                                                            </table>
                                                       
                                                        </div>
                                                    </InsertItemTemplate>
                                                </asp:FormView>
                                                <asp:Literal ID="litAg_BMP_Funding_Overview_Grid" runat="server"></asp:Literal>
                                            </div>

                                            <div class="NestedDivLevel02">
                                                <div style="margin-bottom:5px;"><span class="fsM B">Notes</span> >> <asp:LinkButton ID="lbAg_BMP_Note_Add" runat="server" Text="Add Note" OnClick="lbAg_BMP_Note_Add_Click"></asp:LinkButton></div>
                                                <div style="margin-left:5px;">
                                                    <asp:ListView ID="lvAg_BMP_Notes" runat="server" DataSource='<%# Eval("bmp_ag_notes") %>'>
                                                        <EmptyDataTemplate><div class="I">No Note Records</div></EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <table class="taT">
                                                                <tr class="taT">
                                                                    <td></td>
                                                                    <td class="B U">Note</td>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>[<asp:LinkButton ID="lbAg_BMP_Note_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_bmp_ag_note") %>' OnClick="lbAg_BMP_Note_View_Click"></asp:LinkButton>]</td>
                                                                <td><%# Eval("note") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <asp:FormView ID="fvAg_BMP_Note" runat="server" Width="100%" OnModeChanging="fvAg_BMP_Note_ModeChanging" OnItemUpdating="fvAg_BMP_Note_ItemUpdating" OnItemInserting="fvAg_BMP_Note_ItemInserting" OnItemDeleting="fvAg_BMP_Note_ItemDeleting">
                                                    <ItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbAg_BMP_Note_Close" runat="server" Text="Close" OnClick="lbAg_BMP_Note_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_note"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_note"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                            </table>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                            </table>
                                                        </div>
                                                    </InsertItemTemplate>
                                                </asp:FormView>
                                            </div>
                                            <hr />
                                            <div class="NestedDivLevel02">
                                                <div style="margin-bottom:5px;"><span class="fsM B">Status</span> >> <asp:LinkButton ID="lbAg_BMP_Status_Add" runat="server" Text="Add Status" OnClick="lbAg_BMP_Status_Add_Click"></asp:LinkButton></div>
                                                <div style="margin-left:5px;">
                                                    <asp:ListView ID="lvAg_BMP_Status" runat="server" DataSource='<%# Eval("bmp_ag_status") %>'>
                                                        <EmptyDataTemplate><div class="I">No Status Records</div></EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <table cellpadding="5" rules="cols">
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="B U">Status</td>
                                                                    <td class="B U">WFP2</td>
                                                                    <td class="B U">Date</td>
                                                                    <td class="B U">Note</td>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="taT">
                                                                <td>[<asp:LinkButton ID="lbAg_BMP_Status_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_bmp_ag_status") %>' OnClick="lbAg_BMP_Status_View_Click"></asp:LinkButton>]</td>
                                                                <td><%# Eval("list_statusBMP.status") %></td>
                                                                <td><%# WACGlobal_Methods.Specialtext_Agriculture_BMP_Status_WFP2_Revision(Eval("bmp_ag.fk_farmBusiness"), Eval("fk_form_wfp2_version"))%></td>
                                                                <td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td>
                                                                <td><%# Eval("note") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <asp:FormView ID="fvAg_BMP_Status" runat="server" Width="100%" OnModeChanging="fvAg_BMP_Status_ModeChanging" OnItemUpdating="fvAg_BMP_Status_ItemUpdating" OnItemInserting="fvAg_BMP_Status_ItemInserting" OnItemDeleting="fvAg_BMP_Status_ItemDeleting">
                                                    <ItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbAg_BMP_Status_Close" runat="server" Text="Close" OnClick="lbAg_BMP_Status_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_status"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Status:</td><td><%# Eval("list_statusBMP.status")%></td></tr>
                                                                <tr class="taT"><td class="B taR">WFP2:</td><td><%# WACGlobal_Methods.Specialtext_Agriculture_BMP_Status_WFP2_Revision(Eval("bmp_ag.fk_farmBusiness"), Eval("fk_form_wfp2_version"))%></td></tr>
                                                                <tr class="taT"><td class="B taR">Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date"))%></td></tr>
                                                                <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_status"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Status:</td><td><%# Eval("list_statusBMP.status")%></td></tr>
                                                                <tr class="taT"><td class="B taR">WFP2:</td><td><asp:DropDownList ID="ddlWFP2" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalBMPStatusDate" runat="server" Text='<%#Bind("date") %> '/></td></tr>
                                                                <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="6"></asp:TextBox></td></tr>
                                                            </table>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        <hr />
                                                        <div class="NestedDivLevel02A">
                                                            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                            <hr />
                                                            <table class="taT">
                                                                <tr class="taT"><td class="B taR">Status:</td><td><asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">WFP2:</td><td><asp:DropDownList ID="ddlWFP2" runat="server"></asp:DropDownList></td></tr>
                                                                <tr class="taT"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalBMPStatusDate" runat="server" Text='<%#Bind("date") %>' /></td></tr>
                                                            </table>
                                                        </div>
                                                    </InsertItemTemplate>
                                                </asp:FormView>
                                            </div>
                                            <hr />
                                            <div class="NestedDivLevel02">
                                                <div style="margin-bottom:5px;"><span class="fsM B">Supplemental Agreements</span></div>
                                                <div style="margin-left:5px;">
                                                    <asp:ListView ID="lvAg_BMP_SAs" runat="server" DataSource='<%# Eval("bmp_ag_SAs") %>'>
                                                        <EmptyDataTemplate><div class="I">No Supplemental Agreement Records</div></EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <table cellpadding="5" rules="cols">
                                                                <tr class="taT">
                                                                    <td>&nbsp;</td>
                                                                    <td class="B U">SA #</td>
                                                                    <td class="B U">Revision #</td>
                                                                    <td class="B U">Tax Parcel</td>
                                                                    <td class="B U">Active</td>
                                                                    <td class="B U">Note</td>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="taT">
                                                                <td>[<asp:LinkButton ID="lbAg_BMP_SA_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_SA") %>' OnClick="lbAg_BMP_SA_Delete_Click"></asp:LinkButton>]</td>
                                                                <td><%# Eval("supplementalAgreementTaxParcel.supplementalAgreement.agreement_nbr_ro")%></td>
                                                                <td><%# Eval("form_wfp2_version.version") %></td>
                                                                <td><%# WACGlobal_Methods.SpecialText_Global_TaxParcel_ID_OwnerStr(Eval("supplementalAgreementTaxParcel.taxParcel.taxParcelID"), Eval("supplementalAgreementTaxParcel.taxParcel.ownerStr_dnd"), false)%></td>
                                                                <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td>
                                                                <td><%# Eval("note") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                        <hr />
                                        <div class="NestedDivLevel02">
                                            <div style="margin-bottom:5px;"><span class="fsM B">Workloads</span> >> <asp:LinkButton ID="lbAg_BMP_Workload_Add" runat="server" Text="Add Workload" OnClick="lbAg_BMP_Workload_Add_Click"></asp:LinkButton></div>
                                            <div style="margin-left:5px;">
                                                <asp:ListView ID="lvAg_BMP_Workloads" runat="server" DataSource='<%# Eval("bmp_ag_workloads") %>' >
                                              
                                                    <EmptyDataTemplate><div class="I">No Workload Records</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp5">
                                                            <tr class="taT">
                                                                <td></td>
                                                                <td class="B U">Workload</td>
                                                                <td class="B U">Easement</td>
                                                                <td class="B U">Priority</td>

                                                                <td class="B U">Status</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbAg_BMP_Workload_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_bmp_ag_workload") %>' OnClick="lbAg_BMP_Workload_View_Click"></asp:LinkButton>]</td>
                                                            <td><%# Eval("year") %></td>
                                                       
                                                            <td><span style="color:red;font-weight:bold"><%# IsFarmEased(Eval("bmp_ag.fk_farmBusiness")) %></span></td>
                                                            <td><%# Eval("priority") %></td>
                                   
                                                            <td><%# Eval("list_statusBMPWorkload.status")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <asp:FormView ID="fvAg_BMP_Workload" runat="server" Width="100%" OnModeChanging="fvAg_BMP_Workload_ModeChanging" OnItemUpdating="fvAg_BMP_Workload_ItemUpdating" OnItemInserting="fvAg_BMP_Workload_ItemInserting" OnItemDeleting="fvAg_BMP_Workload_ItemDeleting">
                                                <ItemTemplate>
                                                    <hr />
                                                    <div class="NestedDivLevel02A">
                                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbAg_BMP_Workload_Close" runat="server" Text="Close" OnClick="lbAg_BMP_Workload_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_workload"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="taR B">Workload:</td><td><%# Eval("year") %></td></tr>

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
                                                            <tr class="taT"><td class="taR B">Agency:</td><td><%# Eval("list_agency.agency")%></td></tr>
                                                            <tr class="taT"><td class="taR B">Status:</td><td><%# Eval("list_statusBMPWorkload.status")%></td></tr>
                                                            <tr class="taT"><td class="taR B">Note:</td><td><%# Eval("note") %></td></tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <hr />
                                                    <div class="NestedDivLevel02A">
                                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag_workload"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="taR B">Workload:</td><td><asp:DropDownList ID="ddlWorkload" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT">
                                                                <td class="taR B">Technicians:</td>
                                                                <td>
                                                                    <div class="DivBoxLightYellow">
                                                                        <div><span class="B">Add:</span> <asp:DropDownList ID="ddlTechnician_Insert" runat="server" OnSelectedIndexChanged="Ag_BMP_Workload_ddlTechnician_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                        <asp:ListView ID="lvTechnicians" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "T") %>'>
                                                                            <ItemTemplate>
                                                                                <div>[<asp:LinkButton ID="lbTechnician_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="Ag_BMP_Workload_DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr class="taT">
                                                                <td class="taR B">Checkers:</td>
                                                                <td>
                                                                    <div class="DivBoxLightYellow">
                                                                        <div><span class="B">Add:</span> <asp:DropDownList ID="ddlChecker_Insert" runat="server" OnSelectedIndexChanged="Ag_BMP_Workload_ddlChecker_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                        <asp:ListView ID="lvCheckers" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "C") %>'>
                                                                            <ItemTemplate>
                                                                                <div>[<asp:LinkButton ID="lbChecker_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="Ag_BMP_Workload_DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr class="taT">
                                                                <td class="taR B">Construction:</td>
                                                                <td>
                                                                    <div class="DivBoxLightYellow">
                                                                        <div><span class="B">Add:</span> <asp:DropDownList ID="ddlConstruction_Insert" runat="server" OnSelectedIndexChanged="Ag_BMP_Workload_ddlConstruction_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                        <asp:ListView ID="lvConstruction" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "O") %>'>
                                                                            <ItemTemplate>
                                                                                <div>[<asp:LinkButton ID="lbConstruction_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="Ag_BMP_Workload_DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr class="taT">
                                                                <td class="taR B">Engineers:</td>
                                                                <td>
                                                                    <div class="DivBoxLightYellow">
                                                                        <div><span class="B">Add:</span> <asp:DropDownList ID="ddlEngineer_Insert" runat="server" OnSelectedIndexChanged="Ag_BMP_Workload_ddlEngineer_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                                        <asp:ListView ID="lvEngineers" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "E") %>'>
                                                                            <ItemTemplate>
                                                                                <div>[<asp:LinkButton ID="lbEngineer_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_bmp_ag_workloadSupport")%>' OnClick="Ag_BMP_Workload_DesignerEngineer_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>] <%# Eval("list_designerEngineer.designerEngineer")%> </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr class="taT"><td class="taR B">Agency:</td><td><asp:DropDownList ID="ddlAgency" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="taR B">Status:</td><td><asp:DropDownList ID="ddlStatusBMPWorkload" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                            </table>
                                                    </div>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <hr />
                                                    <div class="NestedDivLevel02A">
                                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="taR B">Workload:</td><td><asp:DropDownList ID="ddlWorkload" runat="server"></asp:DropDownList></td></tr>
                                                        </table>
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                            <hr />
                                             <asp:ListView ID="lvAg_BMP_Workload_ReadOnly" runat="server">
                                                <LayoutTemplate>
                                                    <table class="taT">
                                                        <tr class="taT"><td class="taC B U" colspan="2">Read Only Fields</td></tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="taT"><td class="taR B">Farm ID:</td><td><%# Eval("farmID")%></td></tr>
                                                   
                                                    <tr class="taT"><td class="taR B">Owner:</td><td><%# Eval("ownerStr_dnd")%></td></tr>
                                                    <tr class="taT"><td class="taR B">Region:</td><td><%# Eval("fk_regionWAC_code")%></td></tr>
                                                    <tr class="taT"><td class="taR B">Rank:</td><td><%# Eval("farmRank")%></td></tr>
                                                    <tr class="taT"><td class="taR B">Group:</td><td><%# Eval("fk_groupPI_code")%></td></tr>
                                                    <tr class="taT"><td class="taR B">Planner:</td><td><%# Eval("plannerMaster") %></td></tr>
                                                    <tr class="taT"><td class="taR B">BMP:</td><td><%# Eval("bmp_nbr")%></td></tr>
                                                    <tr class="taT"><td class="taR B">Description:</td><td><%# Eval("bmp_descrip")%></td></tr>
                                                
                                                    <tr class="taT"><td class="taR B">Workload Project Alias:</td><td><%# Eval("workload_project") %></td></tr>
                                                    <tr class="taT"><td class="taR B">Workload Project Code (WFP2):</td><td><%# Eval("workload_projectCode") %></td></tr>
                                                    <tr class="taT"><td class="taR B">Pollutant Category:</td><td><%# Eval("fk_pollutant_category_code")%></td></tr>
                                                    <tr class="taT"><td class="taR B">NRCS WAC Practice:</td><td><%# Eval("fk_BMPPractice_code")%></td></tr>
                                                    <tr class="taT"><td class="taR B">Funding WAC:</td><td><%# Eval("fundingWAC") %></td></tr>
                                                    <tr class="taT"><td class="taR B">Subsequent Funding:</td><td><%# Eval("subsequentFundingWAC") %></td></tr>
                                                    <tr class="taT"><td class="taR B">Plan Estimate:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("planEstimate")) %></td></tr>
                                                    <tr class="taT"><td class="taR B">Units Planned:</td><td><%# Eval("units_planned") %></td></tr>
                                                    <tr class="taT"><td class="taR B">Design Cost:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("design_cost")) %></td></tr>
      
                                                    <tr class="taT"><td class="B taR">Design Dimensions:</td><td><%# Eval("dimensions_designed")%></td></tr>
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
                                                 <%--   <tr class="taT"><td class="taR B">On Hold:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("onHold"))%></td></tr>--%>
                                                    <tr class="taT"><td class="taR B">Procurement Status:</td><td><%# Eval("statusBMPWorkloadProcurement")%></td></tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>
                                            [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> 
                                            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] 
                                            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_bmp_ag"), Eval("created"), 
                                                                     Eval("created_by"), Eval("modified"), Eval("modified_by"))%>&nbsp;[Original BMP #: <%# Eval("bmp_nbr") %>]
                                            </span>
                                        </div>
                                        <hr />
                                        <table class="taT tp5" width="100%"> 
                                             <tr>
                                                <td class="taL"><asp:DropDownList ID="ddlBmpType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBmpType_SelectedIndexChanged"></asp:DropDownList>               
                                                <td class="taL"><%# Eval("CompositBmpNum") %></td>
                                                <td colspan="6"><asp:Label ID="IsBacklogEntity" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                            </tr>                             
                                            <tr>
                                                <td class ="B taL" >Pollutant:</td>
                                                <td colspan="3" class="taL"><asp:DropDownList ID="ddlPollutant" runat="server" Width="450px"></asp:DropDownList></td>        
                                                <td class="B taL" >Status:</td>
                                                <td class="taL" ><%# Eval("list_statusBMP.status") %></td>
                                             <%--   <td class="taL"><asp:DropDownList ID="ddlBMPStatus" runat="server"></asp:DropDownList></td> --%>
                                                <td class="B taL" >Workload Grouping:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbWorkloadGroup"  DbValue='<%# Bind("farmBusinessWLProject.ImplementationProject") %>'
                                                        EmptyMessage="Group" MinValue="0" MaxValue="99" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td> 
                                            </tr>
                                            <asp:PlaceHolder ID="bmpEditClone" runat="server" Visible="false">
                                                   <%-- <td class="B taL" >BMP: (<telerik:RadCheckBox CssClass="RTL" ID="BmpIsIrcClone" runat="server" Enabled="true" Text="IRC" RenderMode="Lightweight" AutoPostBack="false"></telerik:RadCheckBox> )</td>--%>
<%--                                                    <td class="B taR"><asp:DropDownList ID="ddlCloneBmpType" runat="server"></asp:DropDownList>
                                                    </td>--%>
                                                    <td class="B taL">Number:</td>
                                                    <td class="taL" ><%# Eval("bmp") %></td>
                                                    <td class="B taL">Descriptor:</td>
                                                    <td class="taL"><asp:DropDownList ID="ddlBmpDescriptorClone" runat="server"></asp:DropDownList></td>
                                                    <td class="B taL" >Qualifier Code:</td>
                                                    <td class="taL" ><asp:DropDownList ID="ddlBMPCodeClone" runat="server"></asp:DropDownList></td>
                                                    <td class="B taL" >Qualifier Version:</td>
                                                    <td class="taL">
                                                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbQualifierVersionClone"  DbValue='<%# Bind("QualifierVersion") %>'
                                                            EmptyMessage="Version" MinValue="0" MaxValue="10" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                </tr>
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="bmpEditOriginal" runat="server" Visible="true">
                                                <tr class="taT"> 
                                                   <%-- <td class="B taR">Type: <asp:DropDownList ID="ddlBmpType" runat="server"></asp:DropDownList>  --%>   
                                                    <td class="B taL">Number:</td>                      
                                                    <td class="taL"><asp:TextBox ID="tbBMP" runat="server" Text='<%# Bind("Bmp") %>'></asp:TextBox></td>
                                                    <td class="B taL">Descriptor:</td>
                                                    <td class="taL"><asp:DropDownList ID="ddlBmpDescriptor" runat="server"></asp:DropDownList></td>
                                                    <td class="B taL">Qualifier Code:</td>
                                                    <td class="taL"><asp:DropDownList ID="ddlBMPCode" runat="server"></asp:DropDownList></td>
                                                    <td class="B taL" >Qualifier Version:</td>
                                                    <td class="taL">
                                                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbQualifierVersion"  DbValue='<%# Bind("QualifierVersion") %>'
                                                            EmptyMessage="Version" MinValue="0" MaxValue="10" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                      
                                                </tr>    
                                            </asp:PlaceHolder>   
                                                <tr class="taT">
                                                    <td class="B taL" >Practice Code:</td>
                                                    <td colspan="3"><asp:DropDownList ID="ddlPractice" runat="server" OnSelectedIndexChanged="ddlPractice_SelectedIndexChanged" AutoPostBack="true" Width="450px"></asp:DropDownList></td> 
                                                    <td class="B taL" >Lifespan:</td>
                                                    <td colspan="1" ><asp:Label ID="lblLife" runat="server" Text='<%# Eval("list_bmpPractice.life_reqd_yr") %>' /></td>
                                                    <td class="B taL" >Parent BMP:</td>
                                                    <td colspan="1"> <asp:DropDownList ID="ddlAncestorBmp" runat="server"></asp:DropDownList></td> 
                                                                
                                                </tr>                                                
                                                    <tr class="taT">
                                                    <td class="B taL" >BMP Description:</td>
                                                    <td colspan="7"><asp:TextBox ID="tbDescription" Width="500px" runat="server" Text='<%# Bind("description") %>' ></asp:TextBox></td>
                                                </tr>
                                                <tr class="taT">
                                                    <td class="B taL">Location:</td>
                                                    <td class="taL" colspan="7"><asp:TextBox ID="tbLocation" runat="server" Width="500px" Text='<%# Bind("location") %>' ></asp:TextBox></td>
                                                </tr>
                                                <tr class="taT">
                                                    <td class="B taL">Tax Parcels:</td>
                                                    <td class="taL" colspan="7"><asp:TextBox ID="tbTaxParcels" runat="server" Width="500px" Enabled="false" Text='<%# Bind("TaxParcels") %>' ></asp:TextBox></td>
                                                </tr>
                                            <tr class="taT">
                                                <td class="B taL">&nbsp;</td>
                                                <td class="taL" colspan="7">
                                                    <asp:Button ID="Parcels" runat="server" OnClick="Parcels_Click" Text="Add Tax Parcels"/>&nbsp;
                                                    <%-- <asp:Button ID="ClearParcels" runat="server" Text="Clear All Tax Parcels" OnClick="ClearParcels_Click" />--%>
                                                </td>
                                            </tr>
                                            <tr class="taT">
                                                <td class="taL" colspan="8">
                                                    <asp:Panel ID="pnlTaxParcelPicker" runat="server" Visible="false" >
                                                        <table>
                                                            <tr class="taT">
                                                                <td class="B taL">County:</td>
                                                                <td><asp:DropDownList ID="ddlCounty" runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                                <td>&nbsp;</td>
                                                                <td class="B taL">Jurisdiction:</td>
                                                                <td><asp:DropDownList ID="ddlJurisdiction" runat="server" OnSelectedIndexChanged="ddlJurisdiction_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                                <td>&nbsp;</td>
                                                                <td class="B taL">SBL Section:</td>
                                                                <td><asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;(first&nbsp;part&nbsp;of&nbsp;Tax&nbsp;Parcel&nbsp;ID)</td>
                                                            </tr>
                                                            <tr class="taT">
                                                                <td class="taT taL B" colspan="2"><asp:Button ID="AddParcels" runat="server" Text="Add Selected Tax Parcels" OnClick="AddParcels_Click"></asp:Button></td>
                                                                <td class="taT taL B" colspan="2"><asp:Button ID="CloseParcels" runat="server" Text="Close Parcel List" OnClick="CloseParcels_Click" /></td>
                                                                <td colspan="4">&nbsp;</td>
                                                            </tr>
                                                            <tr class="taT">
                                                                <td class="taT taL" colspan="8"><asp:CheckBoxList ID="cblTaxParcels" runat="server" ></asp:CheckBoxList></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                             
                                                </td>
                                            </tr>
                                        </table>

                                        <table width="100%" >
                                                        <tr class="taT">
                                                            <td class="B taL" style="width:125px">Units Planned:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbUnitsPlanned" runat="server" Text='<%# Bind("units_planned") %>'></asp:TextBox></td>
                                                            <td class="taL" style="width:90px"><asp:Label ID="lblUnits1" runat="server"></asp:Label></td>
                                                            <td class="B taL" style="width:175px">Prior Planning Estimate:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbEstPlanPrior" runat="server" Text='<%# Bind("est_plan_prior") %>'></asp:TextBox></td>
                                                            <td class="B taL" style="width:175px">Current Planning Estimate:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbEstPlanRevision" runat="server" Text='<%# Bind("est_plan_rev") %>' ></asp:TextBox></td>
                                                        </tr>                                            
                                                        <tr class="taT">
                                                            <td class="B taL" style="width:125px">Units Designed:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbUnitsDesigned" runat="server" Text='<%# Bind("units_designed") %>' ></asp:TextBox></td>
                                                            <td class="taL" style="width:90px"><asp:Label ID="lblUnits2" runat="server"></asp:Label></td>
                                                            <td class="B taL" style="width:175px">Designed Cost:</td>
                                                            <td colspan="4"><asp:TextBox ID="tbDesignCost" runat="server" Text='<%# Bind("design_cost") %>' ></asp:TextBox></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="B taL" style="width:125px">Units Completed:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbUnitsCompleted" runat="server" Text='<%# Bind("units_completed") %>' ></asp:TextBox></td>
                                                            <td style="width:90px"><asp:Label ID="lblUnits3" runat="server"></asp:Label></td>
                                                            <td class="B taL" style="width:175px">Final Cost:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbFinalCost" runat="server" Text='<%# Bind("final_cost") %>' ></asp:TextBox></td>
                                                            <td class="B taL" style="width:175px">Remaining Funds:</td>
                                                            <td style="width:90px"><%# WACGlobal_Methods.DatabaseFunction_Agriculture_BMP_GetBalance(Eval("pk_bmp_ag"), true)%></td>
                                                        </tr>
                                                         <tr class="taT">
                                                            <td class="B taL" style="width:175px" >Designed Dimensions:</td>
                                                            <td colspan="6"><asp:TextBox ID="tbDesignDimensions" runat="server" Width="500px" Text='<%# Bind("dimensions_designed") %>' ></asp:TextBox></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td colspan="7"><hr /></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taL B" colspan="2">Supplemental&nbsp;Agreement:</td>
                                                            <td colspan="5">&nbsp;</td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taL B">Assignment:</td>
                                                            <td><asp:DropDownList ID="ddlSAA" runat="server"></asp:DropDownList></td>
                                                            <td colspan="5">[<asp:LinkButton ID="lbSA_Clear" runat="server" Text="Clear Supplemental Agreement" OnClick="lbSA_Clear_Click"></asp:LinkButton>]</td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taL B">SA:</td>
                                                            <td colspan="6"><asp:DropDownList ID="ddlSA" runat="server"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taL B" colspan="7"><asp:Label ID="lblSA_Message" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                    </table>
 
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> 
                                            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]
                                        </div>
                                        <hr />

                                        <table class="taT tp5" width="100%" >
                                            <tr>
                                                <td style="width:125px">&nbsp;</td>
                                                <td style="width:100px">&nbsp;</td>   
                                                <td style="width:100px">&nbsp;</td>  
                                                <td style="width:125px">&nbsp;</td>       
                                                <td style="width:100px">&nbsp;</td>
                                                <td style="width:50px">&nbsp;</td>  
                                                <td style="width:100px">&nbsp;</td>
                                                <td style="width:75px">&nbsp;</td> 
                                            </tr>
                                                <tr>
                                                <td class ="B taL">Pollutant:</td>
                                                <td colspan="3" class="taL"><asp:DropDownList ID="ddlPollutant" runat="server" Width="400px"></asp:DropDownList></td>        
                                                <td class="B taL">Status:</td>
                                                   
                                                <td class="taL"><asp:DropDownList ID="ddlBMPStatus" runat="server"></asp:DropDownList></td>  
                                                    <td class="B taL">Workload Grouping:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" Witdh="75" runat="server" ID="ntbWorkloadGroup" DbValue='<%# Bind("farmBusinessWLProject.ImplementationProject") %>'
                                                        EmptyMessage="Group" MinValue="0" MaxValue="99"   ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>    
                                            </tr>
                                            <tr class="taT"> 
                                                <%--<td class="B taL">BMP: (<telerik:RadCheckBox CssClass="RTL" ID="BmpIsIrc" runat="server" Text="IRC" RenderMode="Lightweight" AutoPostBack="false"></telerik:RadCheckBox>&nbsp; )</td>--%>
                                                <td class="taR"><asp:DropDownList ID="ddlBmpType" runat="server"></asp:DropDownList>        
                                                <td class="taL"><asp:TextBox ID="tbBMP" runat="server" Text='<%# Bind("Bmp") %>'></asp:TextBox></td>
                                                <td class="B taL">Descriptor:</td>
                                                <td class="taL"><asp:DropDownList ID="ddlBmpDescriptor" runat="server"></asp:DropDownList></td>
                                                <td class="B taL">Qualifier Code:</td>
                                                <td class="taL"><asp:DropDownList ID="ddlBMPCode" runat="server"></asp:DropDownList></td>
                                                <td class="B taL">Qualifier Version:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" Width="75" runat="server" ID="ntbQualifierVersion" DbValue='<%# Bind("QualifierVersion") %>'
                                                        EmptyMessage="Version" MinValue="0" MaxValue="20" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                                              
                                            </tr>            
                                            <tr class="taT">
                                                    <td class="B taL">Practice Code:</td>
                                                    <td colspan="3"><asp:DropDownList ID="ddlPractice" runat="server" OnSelectedIndexChanged="ddlPractice_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td> 
                                                    <td class="B taL">Lifespan:</td>
                                                    <td colspan="1"><asp:Label ID="lblLife" runat="server" Text='<%# Eval("list_bmpPractice.life_reqd_yr") %>' /></td>
                                                    <td class="B taL" >Parent BMP:</td>
                                                    <td colspan="1"> <asp:DropDownList ID="ddlAncestorBmp" runat="server"></asp:DropDownList></td>
                                                                
                                            </tr>                                                
                                            <tr class="taT">
                                                <td class="B taL">BMP Description:</td>
                                                <td colspan="7"><asp:TextBox ID="tbDescription" runat="server" Width="500px" Text='<%# Bind("description") %>' ></asp:TextBox></td>
                                            </tr>
                                            <tr class="taT">
                                                <td class="B taL">Location:</td>
                                                <td class="taL" colspan="7"><asp:TextBox ID="tbLocation" runat="server" Width="500px" Text='<%# Bind("location") %>' ></asp:TextBox></td>
                                                            
                                            </tr>
                                            <tr class="taT">
                                                <td class="B taL">Tax Parcels:</td>
                                                <td class="taL" colspan="7"><asp:TextBox ID="tbTaxParcels" runat="server" Width="500px" Enabled="false" Text='<%# Bind("TaxParcels") %>' ></asp:TextBox></td>
                                            </tr>
                                            <tr class="taT">
                                                <td class="B taL">&nbsp;</td>
                                                <td class="taL" colspan="7">
                                                    <asp:Button ID="Parcels" runat="server" OnClick="Parcels_Click" Text="Add Tax Parcels"/>&nbsp;
                                                    <%-- <asp:Button ID="ClearParcels" runat="server" Text="Clear All Tax Parcels" OnClick="ClearParcels_Click" />--%>
                                                </td>
                                            </tr>
                                            <tr class="taT">

                                            </tr>
                                            <tr class="taT">
                                                <td class="taL" colspan="8">
                                                    <asp:Panel ID="pnlTaxParcelPicker" runat="server" Visible="false" >
                                                        <table>
                                                            <tr class="taT">
                                                                <td class="B taL">County:</td>
                                                                <td><asp:DropDownList ID="ddlCounty" runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                                <td>&nbsp;</td>
                                                                <td class="B taL">Jurisdiction:</td>
                                                                <td><asp:DropDownList ID="ddlJurisdiction" runat="server" OnSelectedIndexChanged="ddlJurisdiction_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                                <td>&nbsp;</td>
                                                                <td class="B taL">SBL Section:</td>
                                                                <td><asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;(first&nbsp;part&nbsp;of&nbsp;Tax&nbsp;Parcel&nbsp;ID)</td>
                                                            </tr>
                                                            <tr class="taT">
                                                                <td class="taT taL B" colspan="2"><asp:Button ID="AddParcels" runat="server" Text="Add Selected Tax Parcels" OnClick="AddParcels_Click"></asp:Button></td>
                                                                <td class="taT taL B" colspan="2"><asp:Button ID="CloseParcels" runat="server" Text="Close Parcel List" OnClick="CloseParcels_Click" /></td>
                                                                <td colspan="4">&nbsp;</td>
                                                            </tr>
                                                            <tr class="taT">
                                                                <td class="taT taL" colspan="8"><asp:CheckBoxList ID="cblTaxParcels" runat="server" ></asp:CheckBoxList></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                            
                                                </td>
                                            </tr>
                                        </table>

                                        <table >
                                                        <tr>
                                                            <td class="B taL" style="width:125px">Units Planned:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbUnitsPlanned" runat="server" Text='<%# Bind("units_planned") %>' ></asp:TextBox></td>
                                                            <td class="taL" style="width:90px"><asp:Label ID="lblUnits1" runat="server"></asp:Label></td>
                                                            <td class="B taL" style="width:175px">Prior Planning Estimate:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbEstPlanPrior" runat="server" Text='<%# Bind("est_plan_prior") %>' ></asp:TextBox></td>
                                                            <td class="B taL" style="width:175px">Current Planning Estimate:</td>
                                                            <td style="width:90px"><asp:TextBox ID="tbEstPlanRevision" runat="server" Text='<%# Bind("est_plan_rev") %>' ></asp:TextBox></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taL B" colspan="2">Supplemental&nbsp;Agreement:</td>
                                                            <td colspan="5">&nbsp;</td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taL B">Assignment:</td>
                                                            <td><asp:DropDownList ID="ddlSAA" runat="server"></asp:DropDownList></td>
                                                            <td colspan="5">[<asp:LinkButton ID="lbSA_Clear" runat="server" Text="Clear Supplemental Agreement" OnClick="lbSA_Clear_Click"></asp:LinkButton>]</td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="taL B">SA:</td>
                                                            <td colspan="6"><asp:DropDownList ID="ddlSA" runat="server"></asp:DropDownList></td>
                                                        </tr>
                                                         <tr class="taT">
                                                            <td class="taL B" colspan="7"><asp:Label ID="lblSA_Message" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                    </table>
                                       
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                            <br />     
                        </ContentTemplate>
                    </asp:UpdatePanel>                   
                </asp:Panel>         
                <asp:LinkButton ID="lbHidden_Ag_BMP" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_BMP" runat="server" TargetControlID="lbHidden_Ag_BMP" PopupControlID="pnlAg_BMP" 
                    BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
                
<!-- AG_CROPWARE -->
                <asp:Panel ID="pnlAg_Cropware" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_Cropware" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Cropware</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_Cropware_Close" runat="server" Text="Close" OnClick="lbAg_Cropware_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_Cropware_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_Cropware" runat="server" Width="100%">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_cropware"), Eval("created"), Eval("created_by"), null, null)%></div>
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Farm ID:</td><td><%# Eval("farmID") %></td></tr>
                                            <tr class="taT"><td class="B taR">Tract/Field:</td><td><%# Eval("tractField") %></td></tr>
                                            <tr class="taT"><td class="B taR">Plan Year:</td><td><%# Eval("plan_year") %></td></tr>
                                            <tr class="taT"><td class="B taR">Field Name:</td><td><%# Eval("fieldname") %></td></tr>
                                            <tr class="taT"><td class="B taR">Acres:</td><td><%# Eval("acres") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil:</td><td><%# Eval("soil") %></td></tr>
                                            <tr class="taT"><td class="B taR">Current Crop:</td><td><%# Eval("current_crop") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 1 Application Method:</td><td><%# Eval("manure1_app_method") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 1 Timing:</td><td><%# Eval("manure1_timing") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 2 Application Method:</td><td><%# Eval("manure2_app_method") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 2 Timing:</td><td><%# Eval("manure2_timing") %></td></tr>
                                            <tr class="taT"><td class="B taR">Fertilizer 1 Name:</td><td><%# Eval("fert1_name") %></td></tr>
                                            <tr class="taT"><td class="B taR">Fertilizer 1 Units:</td><td><%# Eval("fert1_units") %></td></tr>
                                            <tr class="taT"><td class="B taR">Fertilizer 1 Rate Per Acre:</td><td><%# Eval("fert1_ratePerAcre") %></td></tr>
                                            <tr class="taT"><td class="B taR">Fertilizer 2 Name:</td><td><%# Eval("fert2_name") %></td></tr>
                                            <tr class="taT"><td class="B taR">Fertilizer 2 Units:</td><td><%# Eval("fert2_units") %></td></tr>
                                            <tr class="taT"><td class="B taR">Fertilizer 2 Rate Per Acre:</td><td><%# Eval("fert2_ratePerAcre") %></td></tr>
                                            <tr class="taT"><td class="B taR">PI-DP:</td><td><%# Eval("PI_DP") %></td></tr>
                                            <tr class="taT"><td class="B taR">PI-PP:</td><td><%# Eval("PI_PP") %></td></tr>
                                            <tr class="taT"><td class="B taR">Leaching Index:</td><td><%# Eval("leaching_index") %></td></tr>
                                            <tr class="taT"><td class="B taR">Sample Date:</td><td><%# Eval("sample_date") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 1 Source:</td><td><%# Eval("manure1_source") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 1 Analysis:</td><td><%# Eval("manure1_analysis") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 1 Units:</td><td><%# Eval("manure1_units") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 1 Applied Per Acre:</td><td><%# Eval("manure1_appliedPerAcre") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 2 Source</td><td><%# Eval("manure2_source") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 2 Analysis:</td><td><%# Eval("manure2_analysis") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 2 Units:</td><td><%# Eval("manure2_units") %></td></tr>
                                            <tr class="taT"><td class="B taR">Manure 2 Applied Per Acre:</td><td><%# Eval("manure2_appliedPerAcre") %></td></tr>
                                            <tr class="taT"><td class="B taR">RUSLE:</td><td><%# Eval("RUSLE") %></td></tr>
                                            <tr class="taT"><td class="B taR">Flooding Frequency:</td><td><%# Eval("flooding_frequency") %></td></tr>
                                            <tr class="taT"><td class="B taR">Waterbody Type:</td><td><%# Eval("waterbody_type") %></td></tr>
                                            <tr class="taT"><td class="B taR">Flow Distance</td><td><%# Eval("flow_distance") %></td></tr>
                                            <tr class="taT"><td class="B taR">Drainage Class:</td><td><%# Eval("drainage_class") %></td></tr>
                                            <tr class="taT"><td class="B taR">Concentration Flows:</td><td><%# Eval("conc_flows") %></td></tr>
                                            <tr class="taT"><td class="B taR">Extraction Method:</td><td><%# Eval("extraction_method") %></td></tr>
                                            <tr class="taT"><td class="B taR">Lab ID:</td><td><%# Eval("lab_id") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Test Units:</td><td><%# Eval("soil_test_units") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil pH:</td><td><%# Eval("soil_ph") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Phosphorus:</td><td><%# Eval("soil_p") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Potassium:</td><td><%# Eval("soil_k") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Magnesium:</td><td><%# Eval("soil_mg") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Calcium:</td><td><%# Eval("soil_ca") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Manganese:</td><td><%# Eval("soil_mn") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Zinc:</td><td><%# Eval("soil_zn") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Iron:</td><td><%# Eval("soil_fe") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Aluminum:</td><td><%# Eval("soil_al") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Organic Matter (%):</td><td><%# Eval("soil_om") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Test Phosphorus (Lbs/Acre):</td><td><%# Eval("morgan_eq_p_lbsPerAcre") %></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Test Potassium (Lbs/Acre):</td><td><%# Eval("morgan_eq_k_lbsPerAcre")%></td></tr>
                                            <tr class="taT"><td class="B taR">Soil Buffer Capacity:</td><td><%# Eval("ex_acidity") %></td></tr>
                                            <tr class="taT"><td class="B taR">Rotation:</td><td><%# Eval("rotation") %></td></tr>
                                            <tr class="taT"><td class="B taR">Phosphorous Level (EOH):</td><td><%# Eval("list_phosphorousLevel.level")%></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_Cropware" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_Cropware" runat="server" TargetControlID="lbHidden_Ag_Cropware" PopupControlID="pnlAg_Cropware" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_FARM_LAND -->
                <asp:Panel ID="pnlAg_FarmlandTract" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_FarmLandTract" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Farm Land Tract</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_FarmLandTract_Close" runat="server" Text="Close" OnClick="lbAg_FarmLandTract_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_FarmLandTract_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_FarmLandTract" runat="server" Width="100%" OnModeChanging="fvAg_FarmLandTract_ModeChanging" OnItemUpdating="fvAg_FarmLandTract_ItemUpdating" OnItemInserting="fvAg_FarmLandTract_ItemInserting" OnItemDeleting="fvAg_FarmLandTract_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmLandTract"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Tract:</td><td><%# Eval("tract") %></td></tr>
                                        </table>
                                        <hr />
                                        <div class="NestedDivLevel02">
                                            <div style="margin-bottom:5px;"><span class="fsM B">Fields</span> >> <asp:LinkButton ID="lbAg_FarmLandTractField_Add" runat="server" Text="Add a Field" OnClick="lbAg_FarmLandTractField_Add_Click"></asp:LinkButton></div>
                                            <div style="margin-left:5px;">
                                                <asp:ListView ID="lvAg_FarmLandTractFields" runat="server" DataSource='<%# Eval("farmLandTractFields") %>'>
                                                    <EmptyDataTemplate><div class="I">No Field Records</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="taT">
                                                            <tr class="taT">
                                                                <td></td>
                                                                <td class="B U">Field</td>
                                                                <td class="B U">Year</td>
                                                                <td class="B U">Acres</td>
                                                                <td class="B U">Soil</td>
                                                                <td class="B U">Active</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbAg_FarmLandTractField_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_farmLandTractField") %>' OnClick="lbAg_FarmLandTractField_View_Click"></asp:LinkButton>]</td>
                                                            <td><%# Eval("field") %></td>
                                                            <td><%# Eval("year") %></td>
                                                            <td><%# Eval("acres") %></td>
                                                            <td><%# Eval("soil") %></td>
                                                            <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <asp:FormView ID="fvAg_FarmLandTractField" runat="server" Width="100%" OnModeChanging="fvAg_FarmLandTractField_ModeChanging" OnItemUpdating="fvAg_FarmLandTractField_ItemUpdating" OnItemInserting="fvAg_FarmLandTractField_ItemInserting" OnItemDeleting="fvAg_FarmLandTractField_ItemDeleting">
                                                <ItemTemplate>
                                                    <hr />
                                                    <div class="NestedDivLevel02A">
                                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbAg_FarmLandTractField_Close" runat="server" Text="Close" OnClick="lbAg_FarmLandTractField_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmLandTractField"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="B taR">Year:</td><td><%# Eval("year") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Field:</td><td><%# Eval("field") %></td></tr>
                                                            <tr class="taT"><td class="B taR">TractField:</td><td><%# Eval("tractField") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Soil:</td><td><%# Eval("soil") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Acres:</td><td><%# Eval("acres") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Area:</td><td><%# Eval("area") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Perimeter:</td><td><%# Eval("perimeter") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Available to Rent:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("availableToRent")) %></td></tr>
                                                            <tr class="taT"><td class="B taR">Active:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td></tr>
                                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                                       
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <hr />
                                                    <div class="NestedDivLevel02A">
                                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmLandTractField"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="B taR">Year:</td><td><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="B taR">Field:</td><td><asp:TextBox ID="tbField" runat="server" Text='<%# Bind("field") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Soil:</td><td><asp:TextBox ID="tbSoil" runat="server" Text='<%# Bind("soil") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Acres:</td><td><asp:TextBox ID="tbAcres" runat="server" Text='<%# Bind("acres") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Area:</td><td><asp:TextBox ID="tbArea" runat="server" Text='<%# Bind("area") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Perimeter:</td><td><asp:TextBox ID="tbPerimeter" runat="server" Text='<%# Bind("perimeter") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Available to Rent:</td><td><asp:DropDownList ID="ddlAvailableToRent" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="B taR">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                                           
                                                        </table>
                                                    </div>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <hr />
                                                    <div class="NestedDivLevel02A">
                                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <table class="taT">
                                                            <tr class="taT"><td class="B taR">Year:</td><td><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td></tr>
                                                            <tr class="taT"><td class="B taR">Field:</td><td><asp:TextBox ID="tbField" runat="server" Text='<%# Bind("field") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Soil:</td><td><asp:TextBox ID="tbSoil" runat="server" Text='<%# Bind("soil") %>'></asp:TextBox></td></tr>
                                                            <tr class="taT"><td class="B taR">Acres:</td><td><asp:TextBox ID="tbAcres" runat="server" Text='<%# Bind("acres") %>'></asp:TextBox></td></tr>
                                                        </table>
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmLandTract"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Tract:</td><td><asp:TextBox ID="tbTract" runat="server" Text='<%# Bind("tract") %>'></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Tract:</td><td><asp:TextBox ID="tbTract" runat="server" Text='<%# Bind("tract") %>'></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_FarmLandTract" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_FarmLandTract" runat="server" TargetControlID="lbHidden_Ag_FarmLandTract" PopupControlID="pnlAg_FarmLandTract" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_FARMBUSINESSCONTACT -->
                <asp:Panel ID="pnlAg_FarmBusinessContact" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_FarmBusinessContact" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Farm Contact</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_FarmBusinessContact_Close" runat="server" Text="Close" OnClick="lbAg_FarmBusinessContact_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_FarmBusinessContact_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_FarmBusinessContact" runat="server" Width="100%" OnModeChanging="fvAg_FarmBusinessContact_ModeChanging" OnItemUpdating="fvAg_FarmBusinessContact_ItemUpdating" OnItemInserting="fvAg_FarmBusinessContact_ItemInserting" OnItemDeleting="fvAg_FarmBusinessContact_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessContact"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Participant:</td><td><%# Eval("participant.fullname_LF_dnd")%></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessContact"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Participant:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Ag_Contact" runat="server" StrParticipantType="A" /></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Participant:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Ag_Contact" runat="server" StrParticipantType="A"  /></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_FarmBusinessContact" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_FarmBusinessContact" runat="server" TargetControlID="lbHidden_Ag_FarmBusinessContact" PopupControlID="pnlAg_FarmBusinessContact" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_FARM_STATUS -->
                <asp:Panel ID="pnlAg_FarmStatus" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_FarmStatus" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Farm Status</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_FarmStatus_Close" runat="server" Text="Close" OnClick="lbAg_FarmStatus_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_FarmStatus_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_FarmStatus" runat="server" Width="100%" OnModeChanging="fvAg_FarmStatus_ModeChanging" OnItemUpdating="fvAg_FarmStatus_ItemUpdating" OnItemInserting="fvAg_FarmStatus_ItemInserting" OnItemDeleting="fvAg_FarmStatus_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessStatus"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Farm Status:</td><td><%# Eval("list_status.status") %></td></tr>
                                            <tr class="taT"><td class="B taR">Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessStatus"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Farm Status:</td><td><asp:Label ID="lblStatus" runat="server" Text='<%#Bind("list_status.status") %>' /></td></tr>
                                            <%--<tr class="taT"><td class="B taR">Farm Status:</td><td><asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td></tr>--%>
                                            <tr class="taT"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalStatusDate" runat="server" Text='<%#Bind("date") %>' /></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Farm Status:</td><td><asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalStatusDate" runat="server" Text='<%#Bind("date") %>' /></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_FarmStatus" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_FarmStatus" runat="server" TargetControlID="lbHidden_Ag_FarmStatus" PopupControlID="pnlAg_FarmStatus" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_FAD_STATUS -->
                <asp:Panel ID="pnlAg_FarmBusinessFAD" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_FarmBusinessFAD" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> FAD Status</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_FarmBusinessFAD_Close" runat="server" Text="Close" OnClick="lbAg_FarmBusinessFAD_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_FarmBusinessFAD_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_FarmBusinessFAD" runat="server" Width="100%" OnModeChanging="fvAg_FarmBusinessFAD_ModeChanging" OnItemUpdating="fvAg_FarmBusinessFAD_ItemUpdating" OnItemInserting="fvAg_FarmBusinessFAD_ItemInserting" OnItemDeleting="fvAg_FarmBusinessFAD_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessFAD"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">FAD Status:</td><td><%# Eval("list_FAD.setting") %></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessFAD"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">FAD Status:</td><td><asp:DropDownList ID="ddlFADStatus" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Farm Status:</td><td><asp:DropDownList ID="ddlFADStatus" runat="server"></asp:DropDownList></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_FarmBusinessFAD" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_FarmBusinessFAD" runat="server" TargetControlID="lbHidden_Ag_FarmBusinessFAD" PopupControlID="pnlAg_FarmBusinessFAD" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_NOTE -->
                <asp:Panel ID="pnlAg_Note" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_Note" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Note</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_Note_Close" runat="server" Text="Close" OnClick="lbAg_Note_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_Note_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_Note" runat="server" Width="100%" OnModeChanging="fvAg_Note_ModeChanging" OnItemUpdating="fvAg_Note_ItemUpdating" OnItemInserting="fvAg_Note_ItemInserting" OnItemDeleting="fvAg_Note_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Note Type:</td><td><%# Eval("list_farmBusinessNoteType.type")%></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Note Type:</td><td><asp:DropDownList ID="ddlNoteType" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Note Type:</td><td><asp:DropDownList ID="ddlNoteType" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_Note" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_Note" runat="server" TargetControlID="lbHidden_Ag_Note" PopupControlID="pnlAg_Note" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_TIER1_ANIMAL -->
                <asp:Panel ID="pnlAg_Tier1_Animal" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_Tier1_Animal" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Tier 1 Animal</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_Tier1_Animal_Close" runat="server" Text="Close" OnClick="lbAg_Tier1_Animal_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_Tier1_Animal_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_Tier1_Animal" runat="server" Width="100%" OnModeChanging="fvAg_Tier1_Animal_ModeChanging" OnItemUpdating="fvAg_Tier1_Animal_ItemUpdating" OnItemInserting="fvAg_Tier1_Animal_ItemInserting" OnItemDeleting="fvAg_Tier1_Animal_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div><asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessTier1Animal"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <%# WACGlobal_Methods.View_Agriculture_Tier1_Animal_AU(Eval("pk_farmBusinessTier1Animal"))%>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div><asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton> <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessTier1Animal"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Animal:</td><td><asp:DropDownList ID="ddlAnimal" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Count:</td><td><asp:TextBox ID="tbCount" runat="server" Text='<%# Bind("cnt") %>'></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div><asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Animal:</td><td><asp:DropDownList ID="ddlAnimal" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Count:</td><td><asp:TextBox ID="tbCount" runat="server" Text='<%# Bind("cnt") %>'></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Tier1_Animal" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_Tier1_Animal" runat="server" TargetControlID="lbHidden_Tier1_Animal" PopupControlID="pnlAg_Tier1_Animal" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_WFP2 -->
                <asp:Panel ID="pnlAg_WFP2" runat="server" CssClass="divBoxGrayPadded ModalPopup_Panel_XL" ScrollBars="None">
                    <asp:UpdatePanel ID="upAg_WFP2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> WFP2</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_WFP2_Close" runat="server" Text="Close" OnClick="lbAg_WFP2_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_WFP2" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_WFP2" runat="server" TargetControlID="lbHidden_WFP2" PopupControlID="pnlAg_WFP2" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_WFP2_VERSION -->
                <asp:Panel ID="pnlAg_WFP2_Version" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_WFP2_Version" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> WFP2 Revision</div>
                            <div style="float:right;"><asp:LinkButton ID="lbAg_WFP2_Version_Close" runat="server" Text="Close" OnClick="lbAg_WFP2_Version_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_WFP2_Version_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_WFP2_Version" runat="server" Width="100%" OnModeChanging="fvAg_WFP2_Version_ModeChanging" OnItemUpdating="fvAg_WFP2_Version_ItemUpdating" OnItemInserting="fvAg_WFP2_Version_ItemInserting" OnItemDeleting="fvAg_WFP2_Version_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div><asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp2_version"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Revision:</td><td><%# Eval("version") %></td></tr>
                                            <tr class="taT"><td class="B taR">Inhouse Revision:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("inhouse_revision")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Setup Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("setup_date")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Approved Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("approved_date")) %></td></tr>
                                            <tr class="taT"><td class="B taR">WFP2 Approved By:</td><td><%# Eval("list_WFP2ApprovedBy.approvedBy") %></td></tr>
                                            <tr class="taT"><td class="B taR">Cost Guideline:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("guideline"))%></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                           
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div><asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton> <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp2_version"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Revision:</td><td><%# Eval("version") %></td></tr>
                                            <tr class="taT"><td class="B taR">Inhouse Revision:</td><td><asp:DropDownList ID="ddlInhouseRevision" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Setup Date:</td><td><uc1:AjaxCalendar ID="tbCalSetupDate" runat="server" Text='<%#Bind("setup_date") %> ' /></td></tr>
                                            <tr class="taT"><td class="B taR">Approved Date:</td><td><uc1:AjaxCalendar ID="tbCalApprovedDate" runat="server" Text='<%#Bind("approved_date") %>' /></td></tr>
                                            <tr class="taT"><td class="B taR">WFP2 Approved By:</td><td><asp:DropDownList ID="ddlWFP2ApprovedBy" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Cost Guideline:</td><td><asp:TextBox ID="tbGuideline" runat="server" Text='<%# Bind("guideline") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                         
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div><asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton></div>
                                        <hr />
                                        <table class="taT">
                                            <tr class="taT"><td class="B taR">Revision:</td><td><asp:DropDownList ID="ddlRevision" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Inhouse Revision:</td><td><asp:DropDownList ID="ddlInhouseRevision" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Setup Date:</td><td><uc1:AjaxCalendar ID="tbCalSetupDate" runat="server" Text='<%#Bind("setup_date") %>'  /> /></td></tr>
                                            <tr class="taT"><td class="B taR">Approved Date:</td><td><uc1:AjaxCalendar ID="tbCalApprovedDate" runat="server" Text='<%#Bind("approved_date") %>' /></td></tr>
                                            <tr class="taT"><td class="B taR">Cost Guideline:</td><td><asp:TextBox ID="tbGuideline" runat="server" Text='<%# Bind("guideline") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_WFP2_Version" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_WFP2_Version" runat="server" TargetControlID="lbHidden_WFP2_Version" PopupControlID="pnlAg_WFP2_Version" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_WFP3 -->          
                <asp:Panel ID="pnlAg_WFP3" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_WFP3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    
                    </ContentTemplate>  
                    </asp:UpdatePanel>                     
                 </asp:Panel>
               <asp:LinkButton ID="lbHidden_Ag_WFP3" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_WFP3" runat="server" TargetControlID="lbHidden_Ag_WFP3" PopupControlID="pnlAg_WFP3" 
                    BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_WLPROJECT -->
                <asp:Panel ID="pnlAg_WLProject" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_WLProject" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Workload Groupings</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbAg_WLProject_Close" runat="server" Text="Close" OnClick="lbAg_WLProject_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:Literal ID="litAg_WLProject_Header" runat="server"></asp:Literal>
                            <hr />
                            <asp:FormView ID="fvAg_WLProject" runat="server" Width="100%" OnModeChanging="fvAg_WLProject_ModeChanging" OnItemUpdating="fvAg_WLProject_ItemUpdating" OnItemInserting="fvAg_WLProject_ItemInserting" OnItemDeleting="fvAg_WLProject_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessWLProject"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">                              
                                            <tr class="taT"><td class="B taR">Workload Grouping:</td><td><%# Eval("ImplementationProject") %></td></tr>
                                            <tr class="taT"><td class="B taR">Design Year:</td><td><%# Eval("design_year") %></td></tr>
                                            <tr class="taT"><td class="B taR">Build Year:</td><td><%# Eval("build_year") %></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                        <hr />
                                        <div class="NestedDivLevel02">
                                            <div style="margin-bottom:5px;">
                                                <div style="float:left;"><span class="fsM B">Workload Grouping BMPs</span></div>
                                                <div style="float:right;"><span class="B">Add a BMP:</span> <asp:DropDownList ID="ddlAg_WLProject_BMP_Insert" runat="server" 
                                                    OnSelectedIndexChanged="ddlAg_WLProject_BMP_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                                <div style="clear:both;"></div>
                                            </div>
                                            <hr />
                                            <div style="margin-left:5px;">
                                                <asp:ListView ID="lvAg_WLProject_BMPs" runat="server" DataSource='<%# WACGlobal_Methods.Order_Agriculture_farmBusinessWLProjectBMP_BMPNumber(Eval("farmBusinessWLProjectBMPs")) %>'>
                                                    <EmptyDataTemplate><div class="I">No BMP Records</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table cellpadding="5" rules="cols">
                                                            <tr>
                                                                <td></td>
                                                                <td class="B U">BMP</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>[<asp:LinkButton ID="lbAg_WLProject_BMP_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_farmBusinessWLProjectBMP") %>' OnClick="lbAg_WLProject_BMP_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>]</td>
                                                            <td><%# WACGlobal_Methods.SpecialText_Agriculture_WorkloadBmp(Eval("fk_bmp_ag")) %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_farmBusinessWLProject"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="taT">
                                            <tr>
                                                <tr class="taT"><td class="B taR">Workload Grouping:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbWorkloadGroup" Width="90px"  DbValue='<%# Bind("ImplementationProject") %>'
                                                        EmptyMessage="Grouping" MinValue="0" MaxValue="99" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>     
                                            </tr>
                                            <tr>
                                                <tr class="taT"><td class="B taR">Design Year:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbDesignYear" Width="90px"  DbValue='<%# Bind("design_year") %>'
                                                        EmptyMessage="Year" MinValue="2016" MaxValue="2040"  ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>     
                                            </tr>
                                            <tr>
                                                <tr class="taT"><td class="B taR">Build Year:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbBuildYear" Width="90px"  DbValue='<%# Bind("build_year") %>'
                                                        EmptyMessage="Year" MinValue="2016" MaxValue="2040" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>     
                                            </tr>
                                            <tr class="taT"><td class="B taR" colspan="2">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="taT">
                                            <tr>
                                                <tr class="taT"><td class="B taR">Workload Grouping:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbWorkloadGroup" Width="90px"  DbValue='<%# Bind("farmBusinessWLProject.ImplementationProject") %>'
                                                        EmptyMessage="Grouping" MinValue="0" MaxValue="99" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>     
                                            </tr>
                                            <tr>
                                                <tr class="taT"><td class="B taR">Design Year:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbDesignYear" Width="90px"  DbValue='<%# Bind("farmBusinessWLProject.design_year") %>'
                                                        EmptyMessage="Year" MinValue="2016" MaxValue="2040" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>     
                                            </tr>
                                            <tr>
                                                <tr class="taT"><td class="B taR">Build Year:</td>
                                                <td class="taL">
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="ntbBuildYear" Width="90px"  DbValue='<%# Bind("farmBusinessWLProject.build_year") %>'
                                                        EmptyMessage="Year" MinValue="2016" MaxValue="2040" ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>     
                                            </tr>
                                            <tr class="taT"><td class="B taR" colspan="2">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_WLProject" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_WLProject" runat="server" TargetControlID="lbHidden_Ag_WLProject" PopupControlID="pnlAg_WLProject" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>

<!-- AG Owner/Operator Phone -->
                <asp:Panel ID="pnlAg_OwnerOperator_Phone" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upAg_OwnerOperator_Phone" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Agriculture >> Owner/Operator Phone Numbers</div>
                            <div style="float:right;">[<asp:LinkButton runat="server" Text="Close" OnClick="lbAg_OwnerOperator_Phone_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:ListView ID="lvAg_OwnerOperator_Phone" runat="server">
                                <EmptyDataTemplate>No Owner/Operator Phone Records</EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table cellpadding="5">
                                        <tr class="taT">
                                            <th>Mode</th>
                                            <th>Participant</th>
                                            <th>Type</th>
                                            <th>Number</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="taT">
                                        <td><%# Eval("Mode") %></td>
                                        <td><%# Eval("Participant") %></td>
                                        <td><%# Eval("Type") %></td>
                                        <td><%# Eval("Number") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Ag_OwnerOperator_Phone" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeAg_OwnerOperator_Phone" runat="server" TargetControlID="lbHidden_Ag_OwnerOperator_Phone" PopupControlID="pnlAg_OwnerOperator_Phone" BackgroundCssClass="ModalPopup_BG">
                </ajtk:ModalPopupExtender>
<!-- AG_EXPRESS -->
                <uc1:UC_Express_Property ID="UC_Express_Property" runat="server" />
                <uc1:UC_Express_Participant ID="UC_Express_Participant1" runat="server" />
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>

   
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="~/AG/WACAgriculture_SupplementalAgreements.aspx.cs" Inherits="WACAgriculture_SupplementalAgreements" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc1" %>
<%--<%@ Register src="~/UserControls/UC_EditCalendar.ascx" tagname="UC_EditCalendar" tagprefix="uc1" %>--%>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<%@ Register Src="~/AG/WACAG_SupplementalAgreementPageContents.ascx" TagPrefix="uc1" TagName="WACAG_SupplementalAgreementPageContents" %>
<%@ Register Src="~/CustomControls/AjaxCalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
     <div class="divContentClass">
        <div style=" background-image:url(../images/agriculture_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Agriculture >> Supplemental Agreements</div>
                    <div style="float:right;"><asp:HyperLink ID="hlAg_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <hr />
                <div style="float:left;">
                                <uc1:WACAG_SupplementalAgreementPageContents runat="server" ID="WACAG_SupplementalAgreementPageContents" />
                            </div>
                <asp:UpdatePanel ID="upSASearch" runat="server">
                    <ContentTemplate>
                        <div>
                            
                            <div style="float:right;">
                                <asp:HyperLink ID="hlAg_Agriculture" runat="server" NavigateUrl="~/AG/WACAgriculture.aspx" Text="Agriculture" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_BMP_Workload" runat="server" NavigateUrl="~/AG/WACAgriculture_BMPWorkloads.aspx" Text="BMP Workloads" Font-Bold="true"></asp:HyperLink> | 
                                <asp:HyperLink ID="hlAg_BMP_Planning" runat="server" NavigateUrl="~/AG/WACAgriculture_BMPPlanning.aspx" Text="BMP Planning" Font-Bold="true"></asp:HyperLink> | 
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
                                    <table cellpadding="5">
                                        <tr valign="top">
                                            <td class="B">Agreement Number:<br /><asp:DropDownList ID="ddlFilter_AgreementNumber" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            <td class="B">Tax Parcel ID:<br /><asp:DropDownList ID="ddlFilter_TaxParcelID" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            <td class="B">Tax Parcel Owner:<br /><asp:DropDownList ID="ddlFilter_TaxParcelOwner" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        
 
                        <div style="margin:0px 0px 5px 0px;">
                            <asp:Label ID="lblCount" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </div>
                        <asp:GridView ID="gv" runat="server" Width="100%" BackColor="White" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gv_SelectedIndexChanged" OnPageIndexChanging="gv_PageIndexChanging" OnSorting="gv_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast" HeaderStyle-Wrap="False" AutoGenerateColumns="False">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <EditRowStyle BackColor="#FFFFAA" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:BoundField HeaderText="Agreement Number" DataField="agreement_nbr_ro" SortExpression="agreement_nbr_ro" />
                                <asp:TemplateField HeaderText="Agreement Date" SortExpression="agreement_date">
                                    <ItemTemplate>
                                        <%# WACGlobal_Methods.Format_Global_Date(Eval("agreement_date"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inactive Date" SortExpression="inactive_date">
                                    <ItemTemplate>
                                        <%# WACGlobal_Methods.Format_Global_Date(Eval("inactive_date"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Parcels (Farm ID: BMPs)">
                                    <ItemTemplate>
                                        <asp:ListView ID="lvSA_TaxParcels" runat="server" DataSource='<%# Eval("supplementalAgreementTaxParcels") %>'>
                                            <EmptyDataTemplate><div class="I">No Tax Parcel Records</div></EmptyDataTemplate>
                                            <ItemTemplate>
                                                <div><%# WACGlobal_Methods.SpecialText_Global_TaxParcel_ID_OwnerStr(Eval("taxParcel.taxParcelID"), Eval("taxParcel.ownerStr_dnd"), false)%></div>
                                                <div style="margin-left:20px;"><asp:HyperLink ID="hlSA_AG" runat="server" Text='<%# WACGlobal_Methods.View_Agriculture_SupplementalAgreement(Eval("pk_supplementalAgreementTaxParcel"), WACGlobal_Methods.Enum_Agriculture_SupplementalAgreement_View_StringReturned.FarmID_BMPs) %>' NavigateUrl='<%# WACGlobal_Methods.EventControl_Custom_Hyperlink_NavigateURL(WACGlobal_Methods.Enum_NavigateURL_Special.SA_2_AG, WACGlobal_Methods.View_Agriculture_SupplementalAgreement(Eval("pk_supplementalAgreementTaxParcel"), WACGlobal_Methods.Enum_Agriculture_SupplementalAgreement_View_StringReturned.PK_FarmBusiness)) %>'></asp:HyperLink></div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top:10px;">
                            <asp:FormView ID="fvSA" runat="server" Width="100%" OnModeChanging="fvSA_ModeChanging" OnItemUpdating="fvSA_ItemUpdating" OnItemInserting="fvSA_ItemInserting" OnItemDeleting="fvSA_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel00">
                                        <div><span class="fsM B">Supplemental Agreement</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbSA_Close" runat="server" Text="Close" OnClick="lbSA_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreement"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <div style="float:left;">
                                            <table cellpadding="3">
                                                <tr valign="top"><td class="taR B">Agreement Number:</td><td><%# Eval("agreement_nbr_ro")%></td></tr>
                                                <tr valign="top"><td class="taR B">Agreement Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("agreement_date"))%></td></tr>
                                                <tr valign="top"><td class="taR B">Inactive Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("inactive_date"))%></td></tr>
                                            </table>
                                        </div>
                                        <div style="float:right;">
                                            <div class="NestedDivReadOnly">
                                                <div class="fsS B" style="margin-bottom:5px;">Supplemental Agreement Activity</div>
                                                <asp:ListView ID="lvSA_Activity" runat="server">
                                                    <EmptyDataTemplate><div class="I">No Activity Found</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table cellpadding="3" rules="cols">
                                                            <tr valign="top">
                                                                <td class="B U">Farm ID</td>
                                                                <td class="B U">Farm Owner</td>
                                                                <td class="B U">Revision</td>
                                                                <td class="B U">Approved</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr valign="top">
                                                            <td><asp:HyperLink ID="hlSA_AG" runat="server" Text='<%# Eval("farmID") %>' NavigateUrl='<%# WACGlobal_Methods.EventControl_Custom_Hyperlink_NavigateURL(WACGlobal_Methods.Enum_NavigateURL_Special.SA_2_AG, Eval("pk_farmBusiness")) %>'></asp:HyperLink></td>
                                                            <td><%# Eval("Farm_Owner") %></td>
                                                            <td><%# Eval("Revision") %></td>
                                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("Approved")) %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div style="clear:both;"></div>
<!-- NOTES -->
                                        <div class="NestedDivLevel01" style="margin-top:10px;">
                                            <div><span class="fsM B">Notes</span> [<asp:LinkButton ID="lbSA_Note_Insert" runat="server" Text="Insert a Note" OnClick="lbSA_Note_Insert_Click"></asp:LinkButton>]</div>
                                            <hr />
                                            <asp:ListView ID="lvSA_Notes" runat="server" DataSource='<%# Eval("supplementalAgreementNotes") %>'>
                                                <EmptyDataTemplate><div class="I">No Note Records</div></EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table cellpadding="5" rules="cols">
                                                        <tr valign="top">
                                                            <td class="B U"></td>
                                                            <td class="B U">Created By</td>
                                                            <td class="B U">Note</td>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr valign="top">
                                                        <td>[<asp:LinkButton ID="lbSA_Note_View" runat="server" CommandArgument='<%# Eval("pk_supplementalAgreementNote") %>' Text="View" OnClick="lbSA_Note_View_Click"></asp:LinkButton>]</td>
                                                        <td><%# Eval("created_by") %></td>
                                                        <td><%# Eval("note") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <hr />
                                            <asp:FormView ID="fvSA_Note" runat="server" Width="100%" OnModeChanging="fvSA_Note_ModeChanging" OnItemUpdating="fvSA_Note_ItemUpdating" OnItemInserting="fvSA_Note_ItemInserting" OnItemDeleting="fvSA_Note_ItemDeleting">
                                                <ItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Note</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbSA_Note_Close" runat="server" Text="Close" OnClick="lbSA_Note_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreementNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Note:</td><td><%# Eval("note") %></td></tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Note</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreementNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4" MaxLength="255"></asp:TextBox></td></tr>
                                                        </table>
                                                    </div>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Note</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4" MaxLength="255"></asp:TextBox></td></tr>
                                                        </table>
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </div>
<!-- TAX PARCELS -->
                                        <div class="NestedDivLevel01" style="margin-top:10px;">
                                            <div><span class="fsM B">Tax Parcels</span> [<asp:LinkButton ID="lbSA_TaxParcel_Insert" runat="server" Text="Insert a Tax Parcel" OnClick="lbSA_TaxParcel_Insert_Click"></asp:LinkButton>]</div>
                                            <hr />
                                            <asp:ListView ID="lvSA_TaxParcels" runat="server" DataSource='<%# Eval("supplementalAgreementTaxParcels") %>'>
                                                <EmptyDataTemplate><div class="I">No Tax Parcel Records</div></EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table cellpadding="5" rules="cols">
                                                        <tr valign="top">
                                                            <td class="B U"></td>
                                                            <td class="B U">Tax Parcel ID</td>
                                                            <td class="B U">Tax Parcel Owners</td>
                                                            <td class="B U">Cancel Date</td>
                                                            <td class="B U">Farm ID: BMPs</td>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr valign="top">
                                                        <td>[<asp:LinkButton ID="lbSA_TaxParcel_View" runat="server" CommandArgument='<%# Eval("pk_supplementalAgreementTaxParcel") %>' Text="View" OnClick="lbSA_TaxParcel_View_Click"></asp:LinkButton>]</td>
                                                        <td><%# Eval("taxParcel.taxParcelID")%></td>
                                                        <td><%# Eval("taxParcel.ownerStr_dnd")%></td>
                                                        <td><%# WACGlobal_Methods.Format_Global_Date(Eval("cancel_date"))%></td>
                                                        <td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_SupplementalAgreement_TaxParcel_GetFarmBMPs(Eval("pk_supplementalAgreementTaxParcel")) %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <hr />
                                            <asp:FormView ID="fvSA_TaxParcel" runat="server" Width="100%" OnModeChanging="fvSA_TaxParcel_ModeChanging" OnItemUpdating="fvSA_TaxParcel_ItemUpdating" OnItemInserting="fvSA_TaxParcel_ItemInserting" OnItemDeleting="fvSA_TaxParcel_ItemDeleting">
                                                <ItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Tax Parcel</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbSA_TaxParcel_Close" runat="server" Text="Close" OnClick="lbSA_TaxParcel_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreementTaxParcel"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Tax Parcel ID:</td><td><%# Eval("taxParcel.taxParcelID")%></td></tr>
                                                            <tr valign="top"><td class="taR B">Tax Parcel Owner:</td><td><%# Eval("taxParcel.ownerStr_dnd")%></td></tr>
                                                            <tr valign="top"><td class="taR B">Cancel Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("cancel_date"))%></td></tr>
                                                            <tr valign="top"><td class="taR B">Farm ID: BMPs:</td><td><%# WACGlobal_Methods.DatabaseFunction_Agriculture_SupplementalAgreement_TaxParcel_GetFarmBMPs(Eval("pk_supplementalAgreementTaxParcel")) %></td></tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Tax Parcel</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreementTaxParcel"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Tax Parcel ID:</td><td><asp:DropDownList ID="ddlTaxParcelID" runat="server"></asp:DropDownList></td></tr>
                                                           <%-- <tr valign="top"><td class="taR B">Cancel Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_SA_TaxParcel_CancelDate" runat="server" /></td></tr>--%>
                                                            <tr valign="top"><td class="taR B">
                                                                <uc1:AjaxCalendar runat="server" ID="tbCalTaxParcelCancelDate" Text='<%# Bind("cancel_date") %>' />
                                                            </td></tr>
                                                        </table>
                                                    </div>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <div class="NestedDivLevel00">
                                                        <div><span class="fsM B">Supplemental Agreement >> Tax Parcel</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Tax Parcel ID:</td><td><asp:DropDownList ID="ddlTaxParcelID" runat="server"></asp:DropDownList></td></tr>
                                                        </table>
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </div>
<!-- NOTES -->
                                        <%--<div class="NestedDivLevel01" style="margin-top:10px;">
                                            <div><span class="fsM B">Versions</span> [<asp:LinkButton ID="lbSA_Version_Insert_Click" runat="server" Text="Insert a Version" OnClick="lbSA_Version_Insert_Click"></asp:LinkButton>]</div>
                                            <hr />
                                            <asp:ListView ID="lvSA_Versions" runat="server" DataSource='<%# Eval("supplementalAgreementVersions") %>'>
                                                <EmptyDataTemplate><div class="I">No Version Records</div></EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table cellpadding="5" rules="cols">
                                                        <tr valign="top">
                                                            <td class="B U"></td>
                                                            <td class="B U">Date</td>
                                                            <td class="B U">Version</td>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr valign="top">
                                                        <td>[<asp:LinkButton ID="lbSA_Version_View" runat="server" CommandArgument='<%# Eval("pk_supplementalAgreementVersion") %>' Text="View" OnClick="lbSA_Version_View_Click"></asp:LinkButton>]</td>
                                                        <td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td>
                                                        <td><%# Eval("version") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <hr />
                                            <asp:FormView ID="fvSA_Version" runat="server" Width="100%" OnModeChanging="fvSA_Version_ModeChanging" OnItemUpdating="fvSA_Version_ItemUpdating" OnItemInserting="fvSA_Version_ItemInserting" OnItemDeleting="fvSA_Version_ItemDeleting">
                                                <ItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Version</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbSA_Version_Close" runat="server" Text="Close" OnClick="lbSA_Version_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreementVersion"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td></tr>
                                                            <tr valign="top"><td class="taR B">Version:</td><td><%# Eval("version") %></td></tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Version</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreementVersion"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                        <hr />
                                                        <table cellpadding="3">                                
                                                            <tr valign="top"><td class="taR B">Date:</td><td><uc1:AjaxCalendar runat="server" ID="tbCalVersionDate" Text='<%#Bind("date") %>'/></td></tr>
                                                            <tr valign="top"><td class="taR B">Version:</td><td><%# Eval("version") %></td></tr>
                                                        </table>
                                                    </div>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <div class="NestedDivLevel02">
                                                        <div><span class="fsM B">Supplemental Agreement >> Version</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                                        <hr />
                                                        <table cellpadding="3">
                                                            <tr valign="top"><td class="taR B">Date:</td><td><uc1:AjaxCalendar runat="server" ID="tbCalVersionDate" Text='<%#Bind("date") %>'/></td></tr>
                                                        </table>
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>--%>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel00">
                                        <div><span class="fsM B">Supplemental Agreement</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_supplementalAgreement"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Agreement Number:</td><td><%# Eval("agreement_nbr_ro")%></td></tr>
                                             <tr valign="top"><td class="taR B">Agreement Date:</td><td>
                                                 <uc1:AjaxCalendar runat="server" ID="tbCalAgreementDate" Text='<%#Bind("agreement_date") %>' />
                                             </td></tr>
                                            <tr valign="top"><td class="taR B">Inactive Date:</td><td>
                                                <uc1:AjaxCalendar runat="server" ID="tbCalInactiveDate" Text='<%#Bind("inactive_date") %>' />
                                            </td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                           
                            </asp:FormView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


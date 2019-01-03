<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACPT_Organizations.aspx.cs" 
    Inherits="Participant_WACPT_Organizations" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Property_EditInsert.ascx" tagname="UC_Property_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_SearchByAddress.ascx" tagname="UC_SearchByAddress" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_Participant.ascx" tagname="UC_Express_Participant" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtons.ascx" tagname="UC_Express_PageButtons" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<%@ Register Src="~/Utility/wacut_associations.ascx" TagPrefix="uc1" TagName="WACUT_Associations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Organizations</div>
                    <div style="float:right;" class="B"><asp:HyperLink ID="hlOrganization_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <uc1:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:UpdatePanel ID="upOrganizationSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <div>
                            <div style="float:left;" class="B">
                                <asp:LinkButton ID="lbOrganization_Add" runat="server" Text="Add a New Organization" Font-Bold="true" OnClick="lbOrganization_Add_Click"></asp:LinkButton>
                            </div>
                            <div style="float:right;">
                                <asp:UpdatePanel ID="upParticipantExpressInsert" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <uc1:UC_Express_PageButtonsInsert ID="UC_Express_PageButtonsInsert1" runat="server" BoolShowGlobal="true" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="SearchDivOuter">
                            <div class="SearchDivContentContainer">
                                <div class="SearchDivContent">
                                    <div style="float:left;">Search Options:</div>
                                    <div style="float:right; font-weight:normal;">[<asp:LinkButton ID="lbOrganization_Search_ReloadReset" runat="server" Text="Reload/Reset Search Options" OnClick="lbOrganization_Search_ReloadReset_Click"></asp:LinkButton>]</div>
                                    <div style="clear:both;"></div>
                                </div>
                                <div class="SearchDivInner">
                                    <div>
                                        <div style="float:left;"><asp:LinkButton ID="lbOrganization_Search_All" runat="server" Text="Search for All Organizations" OnClick="lbOrganization_Search_All_Click" Font-Bold="True"></asp:LinkButton></div>
                                        <div style="clear:both;"></div>
                                        <uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Search_Organization_Multi" runat="server" StrParentCase="ORGANIZATION_SEARCH_MULTI" HandleLinkButtonEventInParent="true" ShowStartsWithNumber="true" />
                                       
                                    </div>
                                    <hr />
                                    <div>
                                        <uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Search_Organization" runat="server" StrParentCase="ORGANIZATION_SEARCH" StrEntityType="ORGANIZATION" ShowStartsWithNumber="true" />
                                    </div>
                                   <hr />
                                   
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="progOrganization" runat="server" AssociatedUpdatePanelID="upOrganizationSearch" DisplayAfter="1000">
                    <ProgressTemplate><div class="fsL I cGray">Data Loading... Please Wait...</div></ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="upOrganization" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:Label ID="lblCount" runat="server" CssClass="fsM B I"></asp:Label></div>
                        <asp:GridView ID="gvOrganization" Width="100%" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gvOrganization_SelectedIndexChanged" OnPageIndexChanging="gvOrganization_PageIndexChanging" OnSorting="gvOrganization_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View" />
                                <asp:BoundField HeaderText="Org" DataField="org" SortExpression="org" />
                                <asp:TemplateField HeaderText="Address" SortExpression="property.address">
                                    <ItemTemplate>
                                        <%# Eval("property.address") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City" SortExpression="property.city">
                                    <ItemTemplate>
                                        <%# Eval("property.city") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" SortExpression="property.state">
                                    <ItemTemplate>
                                        <%# Eval("property.state") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zip Code" SortExpression="property.fk_zipcode">
                                    <ItemTemplate>
                                        <%# Eval("property.fk_zipcode") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Township" SortExpression="property.list_townshipNY.township">
                                    <ItemTemplate>
                                        <%# Eval("property.list_townshipNY.township")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="County" SortExpression="property.list_countyNY.county">
                                    <ItemTemplate>
                                        <%# Eval("property.list_countyNY.county")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top:10px;">
                            <asp:FormView ID="fvOrganization" runat="server" Width="100%" OnModeChanging="fvOrganization_ModeChanging" OnItemUpdating="fvOrganization_ItemUpdating" OnItemInserting="fvOrganization_ItemInserting" OnItemDeleting="fvOrganization_ItemDeleting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Organization</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbOrganization_Close" runat="server" Text="Close" OnClick="lbOrganization_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_organization"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <uc1:WACUT_Associations runat="server" ID="WACUT_Associations" AssociationType="Organization" 
                                            OnContentStateChanged="WACUT_Associations_ContentStateChanged" />
                                        <div>
                                            <div style="float:left;">
                                                <table class="tp3">
                                                    <tr class="taT"><td class="B taR">Organization Name:</td><td><%# Eval("org") %></td></tr>
                                                    <tr class="taT">
                                                        <td class="B taR">Business Address:</td>
                                                        <td>
                                                            <%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), Eval("property.list_address2Type.longname"), Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), false)%>
                                                        </td>
                                                    </tr>
                                                    <tr class="taT"><td class="B taR">NY County:</td><td><%# Eval("property.list_countyNY.county")%></td></tr>
                                                    <tr class="taT"><td class="B taR">NY Township:</td><td><%# Eval("property.list_townshipNY.township")%></td></tr>
                                                </table>
                                            </div>
                                            <div style="float:right;">
                                                <table class="tp3">
                                                    <tr class="taT">
                                                        <td class="B taR">Organization Contact:</td>
                                                        <td>
                                                            <uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Organization_Participant" runat="server" />
                                                            <%# WACGlobal_Methods.SpecialText_Global_Participant(Eval("participant"), true, true, true, true) %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="clear:both;"></div>
                                        </div>
                                        <hr />
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div><span class="B fsM">Notes</span> [<asp:LinkButton ID="lbOrganization_Note_Add" runat="server" Text="Add Note" OnClick="lbOrganization_Note_Add_Click"></asp:LinkButton>]</div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvOrganization_Notes" runat="server" DataSource='<%# Eval("organizationNotes") %>'>
                                                    <EmptyDataTemplate><div class="I">No Note Records</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp5">
                                                            <tr class="taT">
                                                                <td class="B U"></td>
                                                                <td class="B U">Created By</td>
                                                                <td class="B U">Created Date</td>
                                                                <td class="B U">Note</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbOrganization_Note_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_OrganizationNote") %>' OnClick="lbOrganization_Note_View_Click"></asp:LinkButton>]</td>
                                                            <td><%# Eval("created_by") %></td>
                                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("created")) %></td>
                                                            <td><%# Eval("note") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <hr />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Organization</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_organization"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Organization Name:</td><td><asp:TextBox ID="tbOrg" runat="server" Text='<%# Bind("org") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td colspan="2">[<asp:LinkButton ID="lbParticipant_Property_Add" runat="server" Text="Add/Change Participant Property" OnClick="lbParticipant_Property_Add_Click"></asp:LinkButton>]</td></tr>
                                             <tr class="taT">
                                                <td class="B taR">Business Address:</td>
                                                <td>
                                                    <%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), Eval("property.list_address2Type.longname"), Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), false)%>
                                                </td>
                                            </tr>
                                            <tr class="taT"><td class="B taR">NY County:</td><td><%# Eval("property.list_countyNY.county")%></td></tr>
                                            <tr class="taT"><td class="B taR">NY Township:</td><td><%# Eval("property.list_townshipNY.township")%></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">Change Organization Contact:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet" runat="server" StrParentCase="PARTICIPANT_WITH_ORGANIZATION" StrEntityType="PARTICIPANT" /></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Organization</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Organization Name:</td><td><asp:TextBox ID="tbOrg" runat="server" Text='<%# Bind("org") %>' Width="400px"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbOrganization_Add" />
                        <asp:AsyncPostBackTrigger ControlID="lbOrganization_Search_ReloadReset" />
                        <asp:AsyncPostBackTrigger ControlID="lbOrganization_Search_All" />
                  
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Panel ID="pnlParticipant_Property" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                        <asp:UpdatePanel ID="upParticipant_Property" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="fsM B" style="float:left;">Participant >> Property</div>
                                <div style="float:right;">[<asp:LinkButton ID="lbParticipant_Property_Close" runat="server" Text="Close" OnClick="lbParticipant_Property_Close_Click"></asp:LinkButton>]</div>
                                <div style="clear:both;"></div>
                                <hr />
                                <asp:FormView ID="fvParticipant_Property" runat="server" Width="100%" OnModeChanging="fvParticipant_Property_ModeChanging" OnItemUpdating="fvParticipant_Property_ItemUpdating" OnItemInserting="fvParticipant_Property_ItemInserting" OnItemDeleting="fvParticipant_Property_ItemDeleting">
                                    <ItemTemplate>
                                        <div class="NestedDivLevel01">
                                            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantProperty"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                            <hr />
                                            <table class="tp3">
                                                <tr class="taT"><td class="B taR">Property:</td><td><%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), Eval("property.list_address2Type.longname"), Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), true) %></td></tr>
                                                <%-- <tr class="taT"><td class="B taR">Master:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master")) %></td></tr>--%>
                                                <tr class="taT"><td class="B taR">NY County:</td><td><%# Eval("property.list_countyNY.county")%></td></tr>
                                                <tr class="taT"><td class="B taR">NY Township</td><td><%# Eval("property.list_townshipNY.township")%></td></tr>
                                            <%--      <tr class="taT"><td class="B taR">Participant CC:</td><td><%# Eval("participant1.fullname_LF_dnd")%></td></tr>--%>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="NestedDivLevel01">
                                            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantProperty"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                            <hr />
                                            <table class="tp3">
                                                <tr class="taT"><td colspan="2"><uc1:UC_Property_EditInsert ID="UC_Property_EditInsert_Participant_Property" runat="server" ShowExpressImageButton="false" /></td></tr>
                                                <tr class="taT"><td colspan="2"><hr /></td></tr>
                                                <%-- <tr class="taT"><td class="B taR">Master:</td><td><asp:DropDownList ID="ddlMaster" runat="server"></asp:DropDownList></td></tr>--%>
                                                <%--  <tr class="taT"><td class="B taR">Participant CC:</td>
                                                    <td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Participant_Property" runat="server" 
                                                            StrParentCase="PARTICIPANT_WITH_ORGANIZATION" StrEntityType="PARTICIPANT_WITH_ORGANIZATION" ShowStartsWithNumber="false" />
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </div>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <div class="NestedDivLevel01">
                                            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                            <hr />
                                            <table class="tp3">
                                                <tr class="taT">
                                                    <td colspan="2">
                                                        <asp:Panel ID ="pnlPropertyLookup" runat="server">
                                                            <table class="tp3">
                                                                <tr class="taT">
                                                                    <td class="taR"><b>Property Address Lookup:</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr class="taT"><td class="B taR">Zip Code:</td><td><asp:DropDownList ID="ddlZipCode" runat="server" OnSelectedIndexChanged="ddlZipCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                                                <tr class="taT">
                                                                    <td class="B taR">Address Starts With?</td>
                                                                    <td><asp:TextBox ID="tbAddressStartsWith" runat="server" Visible="false" ></asp:TextBox>&nbsp;<asp:Button ID="FindStartsWith" runat="server" Text="Click to Search" OnClick="FindStartsWith_Click" Visible="false" /></td>
                                                                </tr>
                                                                <tr class="taT"><td class="B taR">Select Address:</td><td><asp:DropDownList ID="ddlAddress" runat="server" Visible="false"></asp:DropDownList></td></tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <%-- <tr class="taT"><td colspan="2"><hr /></td></tr>
                                                <tr class="taT">
                                                    <td class="B taR">Master:&nbsp;</td>
                                                    <td class="taL"><asp:CheckBox ID="cbMaster" runat="server" /></td>
                                                </tr>--%>
                                            </table>
                                        </div>
                                    </InsertItemTemplate>
                                </asp:FormView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:LinkButton ID="lbHidden_Participant_Property" runat="server"></asp:LinkButton>
                    <ajtk:ModalPopupExtender ID="mpeParticipant_Property" runat="server" TargetControlID="lbHidden_Participant_Property" PopupControlID="pnlParticipant_Property" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>
                <uc1:UC_Express_Participant ID="UC_Express_Participant1" runat="server" />
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
                <asp:Panel ID="pnlOrganization_Note" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upOrganization_Note" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Organization >> Note</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbOrganization_Note_Close" runat="server" Text="Close" OnClick="lbOrganization_Note_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:FormView ID="fvOrganization_Note" runat="server" Width="100%" OnModeChanging="fvOrganization_Note_ModeChanging" OnItemUpdating="fvOrganization_Note_ItemUpdating" OnItemInserting="fvOrganization_Note_ItemInserting" OnItemDeleting="fvOrganization_Note_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_organizationNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_organizationNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Organization_Note" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeOrganization_Note" runat="server" TargetControlID="lbHidden_Organization_Note" PopupControlID="pnlOrganization_Note" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

            </div>
        </div>
    </div>

</asp:Content>


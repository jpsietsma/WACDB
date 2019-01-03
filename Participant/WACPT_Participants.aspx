<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="~/Participant/WACPT_Participants.aspx.cs" Inherits="Participant_WACPT_Participants" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc" TagName="AjaxCalendar" %>
<%@ Register Src="~/Utility/wacut_associations.ascx" TagPrefix="uc1" TagName="WACUT_Associations" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc2" %>
<%@ Register src="~/UserControls/UC_Property_EditInsert.ascx" tagname="UC_Property_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Communication_EditInsert.ascx" tagname="UC_Communication_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_SearchByAddress.ascx" tagname="UC_SearchByAddress" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_Organization.ascx" tagname="UC_Express_Organization" tagprefix="uc1" %>
<%@ Register src="~/Property/UC_Express_Property.ascx" tagname="UC_Express_Property" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtons.ascx" tagname="UC_Express_PageButtons" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<%@ Register Src="~/Utility/WACUT_AttachedDocumentViewer.ascx" TagPrefix="uc" TagName="WACUT_AttachedDocumentViewer" %>
<%@ Register Src="~/Participant/WACPT_ParticipantCreate.ascx" TagPrefix="uc" TagName="WACPT_ParticipantCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Participants</div>
                    <div style="float:right;" class="B"><asp:HyperLink ID="hlParticipant_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <uc2:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <div>
                    <div style="float:left;" class="B">

                        <asp:LinkButton ID="lbParticipantCreate" runat="server" Text="Add a New Participant" onclick="lbParticipantCreate_Click"/> | 
                      
                        <asp:LinkButton ID="lbCreate_Ag_Contractor" runat="server" Text="Create Ag Contractor"></asp:LinkButton> | 
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
                <asp:UpdatePanel ID="upParticipantSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="SearchDivOuter">
                            <div class="SearchDivContentContainer">
                                  
                                <div class="SearchDivContent">
                                    <div class="fsM" style="float:left;">Search Options:</div>
                                    <div style="float:right; font-weight:normal;">[<asp:LinkButton ID="lbParticipant_Search_ReloadReset" runat="server" Text="Reload/Reset Search Options" OnClick="lbParticipant_Search_ReloadReset_Click"></asp:LinkButton>]</div>
                                    <div style="clear:both;"></div>
                                </div>
                                <div class="SearchDivInner">
                                    <div>
                                        <div style="float:left;"><asp:LinkButton ID="lbParticipant_Search_All" runat="server" Text="Search for All Participants" OnClick="lbParticipant_Search_All_Click" Font-Bold="true"></asp:LinkButton></div>
                                        <div style="float:left; margin-left:20px;">
                                            <span class="B">All or Part of Last Name or Organization:</span>
                                            <asp:Panel ID="pnlParticipant_Search_LastName" runat="server" DefaultButton="btnSearchLastName">
                                                <asp:TextBox ID="tbSearchLastName" runat="server" OnTextChanged="tbSearchLastName_TextChanged"  ></asp:TextBox> 
                                                <asp:Button ID="btnSearchLastName" runat="server" Text="Search" OnClick="btnSearchLastName_Click" />
                                            </asp:Panel>
                                        </div>
                                        <div style="float:left; margin-left:50px">
                                            <span class="B">Filter on Participant Type:</span>
                                            <div>
                                                <asp:DropDownList ID="ddlSearchType" runat="server" OnSelectedIndexChanged="ddlSearchType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div style="clear:both;"></div>
                                    </div>
                                    <hr />

                                        <div style="float:left;"><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Search_Person" runat="server" StrParentCase="PARTICIPANT_WITH_ORGANIZATION" StrEntityType="PARTICIPANT" /></div>
                                      
                                        <div style="clear:both;"></div>
                                    </div>
                                    <div>
                                        <div style="float:left; margin-left:20px;">
                                            <span class="B">Participant Interest:</span>
                                            <div>
                                                <asp:DropDownList ID="ddlSearchInterest" runat="server" OnSelectedIndexChanged="ddlSearchInterest_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="clear:both;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="progParticipant" runat="server" AssociatedUpdatePanelID="upParticipantSearch" DisplayAfter="1000">
                    <ProgressTemplate><div class="fsL I cGray">Data Loading... Please Wait...</div></ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="upParticipants" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:Label ID="lblCount" runat="server" CssClass="fsM B I"></asp:Label></div>
                        <asp:GridView ID="gvParticipant" Width="100%" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" 
                                OnSelectedIndexChanged="gvParticipant_SelectedIndexChanged" OnPageIndexChanging="gvParticipant_PageIndexChanging" OnSorting="gvParticipant_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View" />
                                <asp:BoundField HeaderText="Name" DataField="fullname_LF_dnd" SortExpression="fullname_LF_dnd" />
                                <asp:TemplateField HeaderText="Address" SortExpression="property.address">
                                    <ItemTemplate>
                                        <div><%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), "", "", "", "", "", true) %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City" SortExpression="property.city">
                                    <ItemTemplate>
                                        <div><%# Eval("property.city")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" SortExpression="property.state">
                                    <ItemTemplate>
                                        <div><%# Eval("property.state")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zip" SortExpression="property.fk_zipCode">
                                    <ItemTemplate>
                                        <div><%# Eval("property.fk_zipCode")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="County" SortExpression="property.list_countyNY.county">
                                    <ItemTemplate>
                                        <div><%# Eval("property.list_countyNY.county") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Township" SortExpression="property.list_townshipNY.township">
                                    <ItemTemplate>
                                        <div><%# Eval("property.list_townshipNY.township")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top:10px;">
                            <asp:FormView ID="fvParticipant" runat="server" Width="100%" OnModeChanging="fvParticipant_ModeChanging" OnItemUpdating="fvParticipant_ItemUpdating" OnItemInserting="fvParticipant_ItemInserting" OnItemDeleting="fvParticipant_ItemDeleting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div>
                                            <div><span class="fsM B">Participant</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbParticipantClose" runat="server" Text="Close" OnClick="lbParticipantClose_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participant"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                            <hr />
                                            <uc:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewer" SectorCode="PART" />
                                            <uc1:WACUT_Associations runat="server" ID="WACUT_Associations" AssociationType="Participant"
                                                 OnContentStateChanged="WACUT_Associations_ContentStateChanged" />
                                            <hr />
                                  
                                            <asp:Panel ID="ModalPanelError" runat="server" Width="500px"  CssClass="ModalPopup_Panel">
                                                <asp:TextBox ID="ErrorTextBox" runat="server" Width="400px" TextMode="MultiLine" Rows="20"></asp:TextBox>
                                                <br />
                                                 <asp:Button ID="OKButton" runat="server" Text="Close" />
                                            </asp:Panel>
                                            <asp:LinkButton ID="lbHidden_Participant_Property" runat="server"></asp:LinkButton>
                                            <ajtk:ModalPopupExtender ID="mpeError" runat="server" TargetControlID="lbHidden_Participant_Property" 
                                                PopupControlID="ModalPanelError" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>
                                            <table class="tp3">
                                                <tr class="taT">
                                                    <td>
                                                        <table class="tp3">
                                                            <tr class="taT"><td class="I B U" colspan="2" align="center">Participant Information</td></tr>
                                                            <tr class="taT"><td class="B taR">Active:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td></tr>
                                                            <tr class="taT"><td class="B taR">Prefix:</td><td><%# Eval("list_prefix.prefix")%></td></tr>
                                                            <tr class="taT"><td class="B taR">Name:</td><td><%# Eval("fullname_FL_dnd") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Nickname:</td><td><%# Eval("nickname") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Organization:</td>
                                                                <td>
                                                                    <asp:HyperLink ID="hLinkOrganization" runat="server" 
                                                                        Text='<%# Eval("organization.org") %>'
                                                                        ToolTip="Navigate to Organization Record" 
                                                                        NavigateUrl='<%# String.Format("~/Participant/WACPT_Organizations.aspx?pk={0}", Eval("fk_organization")) %>'/>
                                                                </td>
                                                            </tr>
                                                            <tr class="taT"><td class="B taR">DBA:</td><td><%# Eval("DBA")%></td></tr>
                                                            <tr class="taT"><td class="B taR">WAC Region:</td><td><%# Eval("list_regionWAC.regionWAC")%></td></tr>
                                                            <tr class="taT"><td class="B taR">Mailing Status:</td><td><%# Eval("list_mailingStatus.status")%></td></tr>
                                                            <tr class="taT"><td class="B taR">Email:</td><td><%# WACGlobal_Methods.Format_Global_Email_MailTo(Eval("email")) %></td></tr>
                                                            <tr class="taT"><td class="B taR">Web Site:</td><td><%# WACGlobal_Methods.Format_Global_URL(Eval("web")) %></td></tr>
                                                            <tr class="taT"><td class="B taR">Gender:</td><td><%# Eval("list_gender.gender")%></td></tr>
                                                            <tr class="taT"><td class="B taR">Ethnicity:</td><td><%# Eval("list_ethnicity.ethnicity")%></td></tr>
                                                            <tr class="taT"><td class="B taR">Race:</td><td><%# Eval("list_race.race")%></td></tr>
                                                            <tr class="taT"><td class="B taR">Diversity Data:</td><td><%# Eval("list_diversityData.dataSetVia")%></td></tr>
                                                            <tr class="taT"><td class="B taR">Form W9 Signed Date:</td><td><%# WACGlobal_Methods.Format_Color_General_ExpiredDates(Eval("form_W9_signed_date"), 2)%></td></tr>
                                                            <tr class="taT"><td class="B taR">Data Review:</td><td><%# Eval("list_dataReview.dataReview_status") %></td></tr>
                                                            <tr class="taT"><td class="B taR">Data Review Note:</td><td><%# Eval("dataReview_note") %></td></tr>
                                                        </table>
                                                    </td>
                                                    <td style="width:20px;">&nbsp;</td>
                                                   
                                                </tr>
                                            </table>
                                        </div>
                                        <hr />
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div><span class="B fsM">Participant Communications</span> [<asp:LinkButton ID="lbParticipant_Communication_Add" runat="server" Text="Add Participant Communication" OnClick="lbParticipant_Communication_Add_Click"></asp:LinkButton>]</div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvParticipant_Communications" runat="server" DataSource='<%# Eval("participantCommunications") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Communication Records</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp5">
                                                            <tr class="taT">
                                                                <td class="B U"></td>
                                                                <td class="B U">Express</td>
                                                                <td class="B U">Type</td>
                                                                <td class="B U">Usage</td>
                                                                <td class="B U">Number</td>
                                                                <td class="B U">Extension</td>
                                                                <td class="B U">Note</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbParticipant_Communication_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_participantCommunication") %>' OnClick="lbParticipant_Communication_View_Click"></asp:LinkButton>]</td>
                                                            <td><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Participant_Communications" runat="server" StrExpressType="COMMUNICATION" ShowImageButtonView="false" StrSection="PARTICIPANTCOMMUNICATION" /></td>
                                                            <td><%# Eval("list_communicationType.type")%></td>
                                                            <td><%# Eval("list_communicationUsage.usage")%></td>
                                                            <td><%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("communication.areacode"), Eval("communication.number")) %></td>
                                                            <td><%# Eval("extension")%></td>
                                                            <td><%# Eval("note")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div><span class="B fsM">Participant Properties</span> [<asp:LinkButton ID="lbParticipant_Property_Add" runat="server" Text="Add Participant Property" OnClick="lbParticipant_Property_Add_Click"></asp:LinkButton>]</div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvParticipant_Properties" runat="server" DataSource='<%# Eval("participantProperties") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Properties Found</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp5">
                                                            <tr class="taT">
                                                                <td class="B U"></td>
                                                                <td class="B U">Express</td>
                                                                <td class="B U">Master</td>
                                                                <td class="B U">Address</td>
                                                                <td class="B U">NY County</td>
                                                                <td class="B U">NY Township</td>
                                                                <td class="B U">Participant CC</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbParticipant_Property_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_participantProperty") %>' OnClick="lbParticipant_Property_View_Click"></asp:LinkButton>]</td>
                                                            <td><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Participant_Properties" runat="server" StrExpressType="PROPERTY" /></td>
                                                            <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master")) %></td>
                                                            <td><%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), "", Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), true) %></td>
                                                            <td><%# Eval("property.list_countyNY.county")%></td>
                                                            <td><%# Eval("property.list_townshipNY.township")%></td>
                                                            <td><%# Eval("participant1.fullname_LF_dnd")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div><span class="B fsM">Participant Organizations</span> [<asp:LinkButton ID="lbParticipant_Organization_Add" runat="server" Text="Add Participant Organization" OnClick="lbParticipant_Organization_Add_Click"></asp:LinkButton>]</div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvParticipant_Organizations" runat="server" DataSource='<%# Eval("participantOrganizations") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Organizations Found</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp5">
                                                            <tr class="taT">
                                                                <td class="B U"></td>
                                                                <td class="B U">Express</td>
                                                                <td class="B U">Master</td>
                                                                <td class="B U">Org</td>
                                                                <td class="B U">Title</td>
                                                                <td class="B U">Division</td>
                                                                <td class="B U">Department</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbParticipant_Organization_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_participantOrganization") %>' OnClick="lbParticipant_Organization_View_Click"></asp:LinkButton>]</td>
                                                            <td><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Participant_Organizations" runat="server" StrExpressType="ORGANIZATION" /></td>
                                                            <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master")) %></td>
                                                            <td><%# Eval("organization.org") %></td>
                                                            <td><%# Eval("title") %></td>
                                                            <td><%# Eval("division") %></td>
                                                            <td><%# Eval("department") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div class="B fsM">Participant Types</div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvParticipantTypes" runat="server" DataSource='<%# Eval("participantTypes") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Types Found</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp3">
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT"><td>[<asp:LinkButton ID="lbD_ParticipantType" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_participantType") %>' OnClick="lbParticipantTypeDelete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>]</td><td><%# Eval("list_participantType.participantType") %></td></tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <div class="NestedDivLevel00" style="margin-top:10px;">
                                                    <span class="B">Add Participant Type:</span> <asp:DropDownList ID="ddlParticipantType" runat="server" OnSelectedIndexChanged="ddlParticipantType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlParticipantTypeForestry" runat="server" Visible="false">
                                            <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                                <div class="B fsM">Participant Forestry Types</div>
                                                <div style="margin-left:20px;">
                                                    <asp:ListView ID="lvParticipantTypesForestry" runat="server" DataSource='<%# Eval("participantForestryTypes") %>'>
                                                        <EmptyDataTemplate><div class="I">No Participant Forestry Types Found</div></EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <table class="tp3">
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="taT"><td>[<asp:LinkButton ID="lbD_ParticipantForestryType" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_participantForestryType") %>' OnClick="lbParticipantForestryTypeDelete_Click" OnClientClick="return confirm_delete();" />]</td><td><%# Eval("list_forestryCode.description") %></td></tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    <div class="NestedDivLevel00" style="margin-top:10px;">
                                                        <span class="B">Add Participant Forestry Type:</span> <asp:DropDownList ID="ddlParticipantForestryType" runat="server" OnSelectedIndexChanged="ddlParticipantForestryType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div class="B fsM">Participant Interests</div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvParticipantInterests" runat="server" DataSource='<%# Eval("participantInterests") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Interests Found</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp3">
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT"><td>[<asp:LinkButton ID="lbD_ParticipantInterest" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_participantInterest") %>' OnClick="lbParticipantInterestDelete_Click" OnClientClick="return confirm_delete();" />]</td><td><%# Eval("list_interestType.list_participantType.participantType") %> - <%# Eval("list_interestType.list_interest.interest")%></td></tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <div class="NestedDivLevel00" style="margin-top:10px;">
                                                    <span class="B">Add Participant Interest:</span> <asp:DropDownList ID="ddlParticipantInterest" runat="server" OnSelectedIndexChanged="ddlParticipantInterest_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <asp:FormView ID="fvContractor" runat="server" Width="100%" OnModeChanging="fvContractor_ModeChanging" OnItemUpdating="fvContractor_ItemUpdating">
                                                <ItemTemplate>
                                                    <div><span class="fsM B">Participant Contractor</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantContractor"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                    <table class="tp3">
                                                        <tr>
                                                            <td class="B taR">Active:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Landowner:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("landowner")) %></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="B taR">WAC Region:</td><td><%# Eval("list_regionWAC.regionWAC")%></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">EIN:</td><td><%# Eval("ein")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="B taR">Is Supplier:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("isSupplier")) %></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Vendex Date:</td><td><%# WACGlobal_Methods.Format_Color_General_ExpiredDates(Eval("vendex_date"), 3) %></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="B taR">Workers' Comp Start:</td><td><%# WACGlobal_Methods.Format_Global_Date(Convert.ToDateTime(Eval("workmensComp_start"))) %></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Liability Insurance Start:</td><td><%# WACGlobal_Methods.Format_Global_Date(Convert.ToDateTime(Eval("liabilityIns_start"))) %></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="B taR">Workers' Comp End:</td><td><%# WACGlobal_Methods.Format_Color_General_ExpiredDates(Eval("workmensComp_end"), 0) %></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Liability Insurance End:</td><td><%# WACGlobal_Methods.Format_Color_General_ExpiredDates(Eval("liabilityIns_end"), 0) %></td>
                                                        </tr>
                                                    </table>
                                                    <hr />
                                                    <div class="B">Contractor Types:</div>
                                                    <div style="margin-left:20px;">
                                                        <asp:ListView ID="lvParticipant_Contractor_Types" runat="server" DataSource='<%# Eval("participantContractorTypes") %>'>
                                                            <EmptyDataTemplate><div class="I">No Contractor Type Records</div></EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <div>[<asp:LinkButton ID="lbParticipant_Contractor_Type_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_participantContractorType") %>' OnClientClick="return confirm_delete()" OnClick="lbParticipant_Contractor_Type_Delete_Click"></asp:LinkButton>] <%# Eval("list_contractorType.type") %></div>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="NestedDivLevel00" style="margin-top:10px;">
                                                            <span class="B">Add a Contractor Type:</span> <asp:DropDownList ID="ddlAg_Participant_Contractor_Type_Add" runat="server" OnSelectedIndexChanged="ddlAg_Participant_Contractor_Type_Add_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="B">Affidavit Filed:</div>
                                                    <div style="margin-left:20px;">
                                                        <asp:ListView ID="lvParticipant_Contractor_Vendex" runat="server" DataSource='<%# WACGlobal_Methods.Order_Participant_ContractorVendexes(Eval("participantContractor_vendexes")) %>'>
                                                            <EmptyDataTemplate><div class="I">No Affidavit Filed Records</div></EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <div>[<asp:LinkButton ID="lbParticipant_Contractor_Vendex_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_participantContractor_vendex") %>' OnClientClick="return confirm_delete()" OnClick="lbParticipant_Contractor_Vendex_Delete_Click"></asp:LinkButton>] <%# Eval("affidavit_yr") %></div>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="NestedDivLevel00" style="margin-top:10px;">
                                                            <span class="B">Add an Affadavit Year:</span> <asp:DropDownList ID="ddlAg_Participant_Contractor_Vendex_Add" runat="server" OnSelectedIndexChanged="ddlAg_Participant_Contractor_Vendex_Add_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div><span class="fsM B">Participant Contractor</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantContractor"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                                    <table class="tp3">
                                                        <tr class="taT">
                                                            <td class="B taR">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Landowner:</td><td><asp:DropDownList ID="ddlLandowner" runat="server"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlWACRegion" runat="server"></asp:DropDownList></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">EIN:</td><td><asp:TextBox ID="tbEIN" runat="server" Text='<%# Bind("ein")%>'></asp:TextBox></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="B taR">Is Supplier:</td><td><asp:DropDownList ID="ddlIsSupplier" runat="server"></asp:DropDownList></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Vendex Date:</td><td><uc:AjaxCalendar runat="server" ID="calVendexDate" Text=<%# Bind("vendex_date") %> /></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="B taR">Workers' Comp Start:</td><td><uc:AjaxCalendar runat="server" ID="calWorkersCompStart" Text=<%# Bind("workmensComp_start") %> /></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Liability Insurance Start:</td><td><uc:AjaxCalendar runat="server" ID="calLiabilityInsStart" Text=<%# Bind("liabilityIns_start") %>/></td>
                                                        </tr>
                                                        <tr class="taT">
                                                            <td class="B taR">Workers' Comp End:</td><td><uc:AjaxCalendar runat="server" ID="calWorkersCompEnd" Text=<%# Bind("workmensComp_end") %> /></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            <td class="B taR">Liability Insurance End:</td><td><uc:AjaxCalendar ID="calLiabilityInsEnd" runat="server" Text=<%# Bind("liabilityIns_end") %> /></td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                            </asp:FormView>
                                        </div>
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div><span class="B fsM">Participant Notes</span> [<asp:LinkButton ID="lbParticipant_Note_Add" runat="server" Text="Add Note" OnClick="lbParticipant_Note_Add_Click"></asp:LinkButton>]</div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvParticipantNotes" runat="server" DataSource='<%# Eval("participantNotes") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Notes Found</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp5">
                                                            <tr>
                                                                <td></td>
                                                                <td class="B U">Created By</td>
                                                                <td class="B U">Created Date</td>
                                                                <td class="B U">Note Type</td>
                                                                <td class="B U">Note</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbParticipant_Note_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_participantNote") %>' OnClick="lbParticipant_Note_View_Click" />]</td>
                                                            <td><%# Eval("created_by")%></td>
                                                            <td><%# WACGlobal_Methods.Format_Global_Date(Eval("created")) %></td>
                                                            <td><%# Eval("list_participantType.participantType")%></td>
                                                            <td><%# Eval("note")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div style="margin-top:10px; margin-bottom:10px;" class="NestedDivLevel01">
                                            <div><span class="B fsM">Participant Mailings</span></div>
                                            <div style="margin-left:20px;">
                                                <asp:ListView ID="lvParticipantMailings" runat="server" DataSource='<%# Eval("mailingParticipants") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Mailings Found</div></EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table class="tp5">
                                                            <tr>
                                                                <td></td>
                                                                <td class="B U">Mailing</td>
                                                                <td class="B U">Note</td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="taT">
                                                            <td>[<asp:LinkButton ID="lbParticipant_MailingParticipant_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_mailingParticipant") %>' OnClick="lbParticipant_MailingParticipant_Delete_Click" OnClientClick="return confirm_delete()" />]</td>
                                                            <td><%# Eval("mailing.mailing1") %></td>
                                                            <td><%# Eval("mailing.note")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <div class="NestedDivLevel00" style="margin-top:10px;">
                                                    <span class="B">Add Participant Mailing:</span> <asp:DropDownList ID="ddlParticipant_MailingParticipant_Insert" runat="server" OnSelectedIndexChanged="ddlParticipant_MailingParticipant_Insert_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <hr />
                                                <div class="NestedDivLevel00">
                                                    <div class="B fsM">Mailings Sent</div>
                                                    <asp:ListView ID="lvMailingsSentTo" runat="server" DataSource='<%# Eval("mailingSentTos") %>'>
                                                        <EmptyDataTemplate><div class="I">No Mailings Sent Found</div></EmptyDataTemplate>
                                                         <LayoutTemplate>
                                                            <table class="tp5">
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="B U">Date</td>
                                                                    <td class="B U">Mailing</td>
                                                                    <td class="B U">Type</td>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="taT">
                                                                <td>[<asp:LinkButton ID="lbParticipant_MailingSentTo_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_mailingSentTo") %>' OnClick="lbParticipant_MailingSentTo_Delete_Click" OnClientClick="return confirm_delete()" />]</td>
                                                                <td><%# WACGlobal_Methods.Format_Global_Date(Eval("mailingDate.dateSent")) %></td>
                                                                <td><%# Eval("mailingDate.mailing.mailing1")%></td>
                                                                <td><%# Eval("mailingDate.list_mailingType.type")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                               
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Participant</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participant"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Prefix:</td><td><asp:DropDownList ID="ddlPrefix" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">First Name:</td><td><asp:TextBox ID="tbNameFirst" runat="server" Text='<%# Bind("fname") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Middle Name:</td><td><asp:TextBox ID="tbNameMiddle" runat="server" Text='<%# Bind("mname") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Last Name:</td><td><asp:TextBox ID="tbNameLast" runat="server" Text='<%# Bind("lname") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Suffix:</td><td><asp:DropDownList ID="ddlSuffix" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Nickname:</td><td><asp:TextBox ID="tbNickname" runat="server" Text='<%# Bind("nickname") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Organization:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Organization" runat="server" StrParentCase="ORGANIZATION" StrEntityType="ORGANIZATION" ShowStartsWithNumber="true" /></td></tr>
                                            <tr class="taT"><td class="B taR">DBA:</td><td><asp:TextBox ID="tbDBA" runat="server" Text='<%# Bind("DBA") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlRegionWAC" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Mailing Status:</td><td><asp:DropDownList ID="ddlMailingStatus" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Web Site:</td><td><asp:TextBox ID="tbWeb" runat="server" Text='<%# Bind("web") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Gender:</td><td><asp:DropDownList ID="ddlGender" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Ethnicity:</td><td><asp:DropDownList ID="ddlEthnicity" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Race:</td><td><asp:DropDownList ID="ddlRace" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Diversity Data:</td><td><asp:DropDownList ID="ddlDiversityData" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Form W9 Signed Date:</td><td><uc:AjaxCalendar runat="server" ID="calFormW9SignedDate" Text='<%# Bind("form_W9_signed_date") %>' /></td></tr>
                                            <tr class="taT"><td class="B taR">Data Review:</td><td><asp:DropDownList ID="ddlDataReview" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Data Review Note:</td><td><asp:TextBox ID="tbDataReviewNote" runat="server" Text='<%# Bind("dataReview_note") %>' Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Participant</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <div><b>Select a Participant Type: </b>
                                            <asp:RadioButtonList ID="rblParticipants" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblParticipants_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Person" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Person with Organization" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Organization" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <hr />
                                        <asp:Panel ID="pnlParticipant_Person_Insert" runat="server">
                                            <table class="tp3">
                                                <tr class="taT"><td class="B taR">First Name:</td><td><asp:TextBox ID="tbParticipants_NameFirst" runat="server"></asp:TextBox></td></tr>
                                                <tr class="taT"><td class="B taR">Last Name:</td><td><asp:TextBox ID="tbParticipants_NameLast" runat="server"></asp:TextBox></td></tr>
                                                <tr class="taT"><td class="B taR">Suffix:</td><td><asp:DropDownList ID="ddlParticipants_Suffix" runat="server"></asp:DropDownList></td></tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlParticipant_Organization_Insert" runat="server" Visible="false">
                                            <table class="tp3">
                                                <tr class="taT"><td class="B taR">Organization:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Organizations" runat="server" StrParentCase="ORGANIZATION" StrEntityType="ORGANIZATION" ShowStartsWithNumber="true" /></td></tr>
                                            </table>
                                        </asp:Panel>
                                        <hr />
                                        <table class="tp3">
                                             <tr class="taT"><td class="B taR">DBA:</td><td><asp:TextBox ID="tbDBA" runat="server"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Gender:</td><td><asp:DropDownList ID="ddlGender" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Ethnicity:</td><td><asp:DropDownList ID="ddlEthnicity" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Race:</td><td><asp:DropDownList ID="ddlRace" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Diversity Data:</td><td><asp:DropDownList ID="ddlDiversityData" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlRegionWAC" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Initial Participant Type:</td><td><asp:DropDownList ID="ddlInitialParticipantType" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT"><td colspan="2"><uc1:UC_Property_EditInsert ID="UC_Property_EditInsert_Participant" runat="server" /></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </div>                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbParticipant_Search_ReloadReset" />
                        <asp:AsyncPostBackTrigger ControlID="lbParticipant_Search_All" />
                        <asp:AsyncPostBackTrigger ControlID="ddlSearchType" />
                        <asp:AsyncPostBackTrigger ControlID="ddlSearchInterest" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearchLastName" />
                    </Triggers>
                </asp:UpdatePanel>

                <uc1:UC_Express_Property ID="UC_Express_Property1" runat="server" />
                <uc1:UC_Express_Organization ID="UC_Express_Organization1" runat="server" />
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />

                <asp:Panel ID="pnlCreate_Ag_Contractor" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upCreate_Ag_Contractor" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Participants >> Create Ag Contractor</div>
                            <div style="float:right;"><asp:LinkButton ID="lbCreate_Ag_Contractor_Close" runat="server" Text="Close" OnClick="lbCreate_Ag_Contractor_Close_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <div class="NestedDivLevel01">
                                <div>[<asp:LinkButton ID="lbCreate_Ag_Contractor_Insert" runat="server" Text="Insert" OnClick="lbCreate_Ag_Contractor_Insert_Click"></asp:LinkButton>]</div>
                                <hr />
                                <table class="tp3">
                                    <tr class="taT"><td class="B taR">First Name:</td><td><asp:TextBox ID="tbCreate_Ag_Contractor_FNAME" runat="server" MaxLength="48"></asp:TextBox></td></tr> 
                                    <tr class="taT"><td class="B taR">Last Name:</td><td><asp:TextBox ID="tbCreate_Ag_Contractor_LNAME" runat="server" MaxLength="48"></asp:TextBox></td></tr>
                                    <tr class="taT"><td class="B taR">Organization:</td><td><asp:TextBox ID="tbCreate_Ag_Contractor_ORG" runat="server" MaxLength="96"></asp:TextBox></td></tr>
                                    <tr class="taT"><td class="B taR">Address:</td><td><asp:TextBox ID="tbCreate_Ag_Contractor_Address" runat="server" MaxLength="48"></asp:TextBox></td></tr>
                                    <tr class="taT"><td class="B taR">Zip Code:</td><td><asp:DropDownList ID="ddlCreate_Ag_Contractor_Zip" runat="server"></asp:DropDownList></td></tr>
                                    <tr class="taT"><td class="B taR">Phone:</td><td><asp:TextBox ID="tbCreate_Ag_Contractor_Phone" runat="server" MaxLength="16"></asp:TextBox></td></tr>
                                    <tr class="taT"><td class="B taR">Cell:</td><td><asp:TextBox ID="tbCreate_Ag_Contractor_Cell" runat="server" MaxLength="16"></asp:TextBox></td></tr>
                                    <tr class="taT"><td class="B taR">Region:</td><td><asp:DropDownList ID="ddlCreate_Ag_Contractor_Region" runat="server"></asp:DropDownList></td></tr>
                                    <tr class="taT"><td class="B taR">Contractor Type:</td><td><asp:DropDownList ID="ddlCreate_Ag_Contractor_ContractorType" runat="server"></asp:DropDownList></td></tr> 
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <ajtk:ModalPopupExtender ID="mpeCreate_Ag_Contractor" runat="server" TargetControlID="lbCreate_Ag_Contractor" PopupControlID="pnlCreate_Ag_Contractor" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

                <asp:Panel ID="pnlWACEmployees" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upWACEmployees" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Participants >> WAC Employees</div>
                            <div style="float:right;"><asp:LinkButton ID="lbWACEmployeesClose" runat="server" Text="Close" OnClick="lbWACEmployeesClose_Click"></asp:LinkButton></div>
                            <div style="clear:both;"></div>
                            <hr />
                            <div>
                                <asp:LinkButton ID="lbWACEmployeesAdd" runat="server" Text="Add a WAC Employee" OnClick="lbWACEmployeesAdd_Click"></asp:LinkButton> | Edit Existing WAC Employee: <asp:DropDownList ID="ddlWACEmployees" runat="server" OnSelectedIndexChanged="ddlWACEmployees_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <hr />
                            <asp:FormView ID="fvWACEmployee" runat="server" Width="100%" OnModeChanging="fvWACEmployee_ModeChanging" OnItemUpdating="fvWACEmployee_ItemUpdating" OnItemInserting="fvWACEmployee_ItemInserting" OnItemDeleting="fvWACEmployee_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Participant:</td><td><%# Eval("participant1.fullname_LF_dnd")%></td></tr>
                                            <tr class="taT"><td class="B taR">Location:</td><td><%# Eval("list_wacLocation.location")%></td></tr>
                                            <tr class="taT"><td class="B taR">Phone Number:</td><td><%# Eval("list_wacLocation.phone")%></td></tr>
                                            <tr class="taT"><td class="B taR">Phone Extension:</td><td><%# Eval("phone_ext") %></td></tr>
                                            <tr class="taT"><td class="B taR">Email:</td><td><a href='mailto:<%# Eval("email") %>'><%# Eval("email") %></a></td></tr>
                                            <tr class="taT"><td class="B taR"></td><td></td></tr>
                                            <tr class="taT"><td class="B taR">Date of Birth:</td><td><%# Eval("dob", "{0:d}") %></td></tr>
                                            <tr class="taT"><td class="B taR">Marital Status:</td><td><%# Eval("list_maritalStatus.status") %></td></tr>
                                            <tr class="taT"><td class="B taR">Emergency Contact:</td><td><%# Eval("participant.fullname_LF_dnd") %></td></tr>
                                            <tr class="taT"><td class="B taR">Emergency Contact Relationship:</td><td><%# Eval("list_employeeRelationship.relationship") %></td></tr>
                                            <tr class="taT"><td class="B taR">Allergies:</td><td><%# Eval("allergies") %></td></tr>
                                            <tr class="taT"><td class="B taR"></td><td></td></tr>
                                            <tr class="taT"><td class="B taR">Hire Letter:</td><td><%# Eval("hireLetter") %></td></tr>
                                            <tr class="taT"><td class="B taR">Resume:</td><td><%# Eval("resume") %></td></tr>
                                            <tr class="taT"><td class="B taR">Conflict of Interest:</td><td><%# Eval("conflictOfInterest") %></td></tr>
                                            <tr class="taT"><td class="B taR">Received Manual:</td><td><%# Eval("recdManual") %></td></tr>
                                            <tr class="taT"><td class="B taR">LENS:</td><td><%# Eval("LENS") %></td></tr>
                                            <tr class="taT"><td class="B taR">Ethics:</td><td><%# Eval("ethics") %></td></tr>
                                            <tr class="taT"><td class="B taR">Personal Info Sheet:</td><td><%# Eval("personalInfoSheet") %></td></tr>
                                            <tr class="taT"><td class="B taR">Telecommute Begin:</td><td><%# Eval("teleCommute_date", "{0:d}") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Participant:</td><td><%# Eval("participant.fullname_LF_dnd")%></td></tr>
                                            <%--<tr class="taT"><td class="B taR">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Title:</td><td><asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("title") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">WAC Sector:</td><td><asp:DropDownList ID="ddlWACSector" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Phone:</td><td><asp:TextBox ID="tbPhone" runat="server" Text='<%# Bind("phone") %>' Width="400px"></asp:TextBox></td></tr>--%>
                                            <tr class="taT"><td class="B taR">Phone Extension:</td><td><asp:TextBox ID="tbPhoneExt" runat="server" Text='<%# Bind("phone_ext") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Date of Birth:</td><td><uc:AjaxCalendar runat="server" ID="calDOB" Text=<%# Bind("dob") %> /></td></tr>
                                        </table>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Participant:</td><td><asp:DropDownList ID="ddlWACEmployees" runat="server"></asp:DropDownList></td></tr>
                                            <%--<tr class="taT"><td class="B taR">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Title:</td><td><asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("title") %>' Width="400px"></asp:TextBox></td></tr>--%>
                                            <%--<tr class="taT"><td class="B taR">WAC Sector:</td><td><asp:DropDownList ID="ddlWACSector" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Phone:</td><td><asp:TextBox ID="tbPhone" runat="server" Text='<%# Bind("phone") %>' Width="400px"></asp:TextBox></td></tr>--%>
                                            <tr class="taT"><td class="B taR">Phone Extension:</td><td><asp:TextBox ID="tbPhoneExt" runat="server" Text='<%# Bind("phone_ext") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Date of Birth:</td><td><uc:AjaxCalendar runat="server" ID="calDOB" Text=<%# Bind("dob") %> /></td></tr>
                                        </table>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_WACEmployees" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeWACEmployees" runat="server" TargetControlID="lbHidden_WACEmployees" PopupControlID="pnlWACEmployees" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

                <asp:Panel ID="pnlParticipant_Communication" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upParticipant_Communication" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Participant >> Communication</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbParticipant_Communication_Close" runat="server" Text="Close" OnClick="lbParticipant_Communication_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:FormView ID="fvParticipant_Communication" runat="server" Width="100%" OnModeChanging="fvParticipant_Communication_ModeChanging" OnItemUpdating="fvParticipant_Communication_ItemUpdating" OnItemInserting="fvParticipant_Communication_ItemInserting" OnItemDeleting="fvParticipant_Communication_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantCommunication"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Type:</td><td><%# Eval("list_communicationType.type") %></td></tr>
                                            <tr class="taT"><td class="B taR">Usage:</td><td><%# Eval("list_communicationUsage.usage") %></td></tr>
                                            <tr class="taT"><td class="B taR">Number:</td><td><%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("communication.areacode"), Eval("communication.number"))%></td></tr>
                                            <tr class="taT"><td class="B taR">Extension:</td><td><%# Eval("extension") %></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantCommunication"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlParticipant_Communication_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Usage:</td><td><asp:DropDownList ID="ddlUsage" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Area Code:</td><td><asp:TextBox ID="tbAreaCode" runat="server" Text='<%# Eval("communication.areacode") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbPhoneNumber" runat="server" Text='<%# Eval("communication.number") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Extension:</td><td><asp:TextBox ID="tbExtension" runat="server" Text='<%# Bind("extension") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlParticipant_Communication_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Usage:</td><td><asp:DropDownList ID="ddlUsage" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Area Code:</td><td><asp:TextBox ID="tbAreaCode" runat="server" Text='<%# Eval("communication.areacode") %>'></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbPhoneNumber" runat="server" Text='<%# Eval("communication.number") %>'></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Participant_Communication" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeParticipant_Communication" runat="server" TargetControlID="lbHidden_Participant_Communication" PopupControlID="pnlParticipant_Communication" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

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
                                            <tr class="taT"><td class="B taR">Master:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master")) %></td></tr>
                                            <tr class="taT"><td class="B taR">NY County:</td><td><%# Eval("property.list_countyNY.county")%></td></tr>
                                            <tr class="taT"><td class="B taR">NY Township</td><td><%# Eval("property.list_townshipNY.township")%></td></tr>
                                            <tr class="taT"><td class="B taR">Participant CC:</td><td><%# Eval("participant1.fullname_LF_dnd")%></td></tr>
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
                                            <tr class="taT"><td class="B taR">Master:</td><td><asp:DropDownList ID="ddlMaster" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Participant CC:</td>
                                                <td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Participant_Property" runat="server" 
                                                StrParentCase="PARTICIPANT_WITH_ORGANIZATION" StrEntityType="PARTICIPANT_WITH_ORGANIZATION" ShowStartsWithNumber="false" />
                                                </td>

                                            </tr>
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
                                            <tr class="taT"><td colspan="2"><hr /></td></tr>
                                            <tr class="taT">
                                                <td class="B taR">Master:&nbsp;</td>
                                                <td class="taL"><asp:CheckBox ID="cbMaster" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Participant_Property" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeParticipant_Property" runat="server" TargetControlID="lbHidden_Participant_Property" PopupControlID="pnlParticipant_Property" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

                <asp:Panel ID="pnlParticipant_Organization" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upParticipant_Organization" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Participant >> Organization</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbParticipant_Organization_Close" runat="server" Text="Close" OnClick="lbParticipant_Organization_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:FormView ID="fvParticipant_Organization" runat="server" Width="100%" OnModeChanging="fvParticipant_Organization_ModeChanging" OnItemUpdating="fvParticipant_Organization_ItemUpdating" OnItemInserting="fvParticipant_Organization_ItemInserting" OnItemDeleting="fvParticipant_Organization_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantOrganization"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Organization:</td><td><%# Eval("organization.org") %></td></tr>
                                            <tr class="taT"><td class="B taR">Master:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master")) %></td></tr>
                                            <tr class="taT"><td class="B taR">Title:</td><td><%# Eval("title") %></td></tr>
                                            <tr class="taT"><td class="B taR">Division:</td><td><%# Eval("division") %></td></tr>
                                            <tr class="taT"><td class="B taR">Department:</td><td><%# Eval("department") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantOrganization"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Organization:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Participant_Organization" runat="server" StrParentCase="ORGANIZATION" StrEntityType="ORGANIZATION" ShowStartsWithNumber="true" /></td></tr>
                                            <tr class="taT"><td class="B taR">Master:</td><td><asp:DropDownList ID="ddlMaster" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Title:</td><td><asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("title") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Division:</td><td><asp:TextBox ID="tbDivision" runat="server" Text='<%# Bind("division") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr class="taT"><td class="B taR">Department:</td><td><asp:TextBox ID="tbDepartment" runat="server" Text='<%# Bind("department") %>' Width="400px"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Organization:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Participant_Organization" runat="server" StrParentCase="ORGANIZATION" StrEntityType="ORGANIZATION" ShowStartsWithNumber="true" /></td></tr>
                                            <tr class="taT"><td class="B taR">Master:</td><td><asp:DropDownList ID="ddlMaster" runat="server"></asp:DropDownList></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Participant_Organization" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeParticipant_Organization" runat="server" TargetControlID="lbHidden_Participant_Organization" PopupControlID="pnlParticipant_Organization" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

                <asp:Panel ID="pnlParticipant_Note" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upParticipant_Note" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Participant >> Note</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbParticipant_Note_Close" runat="server" Text="Close" OnClick="lbParticipant_Note_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:FormView ID="fvParticipant_Note" runat="server" Width="100%" OnModeChanging="fvParticipant_Note_ModeChanging" OnItemUpdating="fvParticipant_Note_ItemUpdating" OnItemInserting="fvParticipant_Note_ItemInserting" OnItemDeleting="fvParticipant_Note_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Note Type:</td><td><%# Eval("list_participantType.participantType")%></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantNote"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Note Type:</td><td><asp:DropDownList ID="ddlNoteType" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Note Type:</td><td><asp:DropDownList ID="ddlNoteType" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Participant_Note" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeParticipant_Note" runat="server" TargetControlID="lbHidden_Participant_Note" PopupControlID="pnlParticipant_Note" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

                <%--<asp:Panel ID="pnlParticipant_Mailing" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upParticipant_Mailing" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">Participant >> Mailing</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbParticipant_Mailing_Close" runat="server" Text="Close" OnClick="lbParticipant_Mailing_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:FormView ID="fvParticipant_Mailing" runat="server" Width="100%" OnModeChanging="fvParticipant_Mailing_ModeChanging" OnItemUpdating="fvParticipant_Mailing_ItemUpdating" OnItemInserting="fvParticipant_Mailing_ItemInserting" OnItemDeleting="fvParticipant_Mailing_ItemDeleting">
                                <ItemTemplate>
                                    <div class="NestedDivLevel01">
                                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_mailingParticipant"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table class="tp3">
                                            <tr class="taT"><td class="B taR">Mailing:</td><td><%# Eval("mailing.mailing1")%></td></tr>
                                            <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("mailing.note") %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_Participant_Mailing" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeParticipant_Mailing" runat="server" TargetControlID="lbHidden_Participant_Mailing" PopupControlID="pnlParticipant_Mailing" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>--%>

                <uc:WACPT_ParticipantCreate ID="WACPT_ParticipantCreate" runat="server" OnInserted="WACPT_ParticipantCreate_Inserted" />

            </div>
        </div>
    </div>
</asp:Content>


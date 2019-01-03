<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACVenues.aspx.cs" Inherits="WACVenues" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc2" %>
<%@ Register src="~/UserControls/UC_Property_EditInsert.ascx" tagname="UC_Property_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Communication_EditInsert.ascx" tagname="UC_Communication_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_SearchByAddress.ascx" tagname="UC_SearchByAddress" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtons.ascx" tagname="UC_Express_PageButtons" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>
<%@ Register Src="~/Property/UC_Express_Property.ascx" TagPrefix="uc2" TagName="UC_Express_Property" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Venues</div>
                    <div style="float:right;" class="B"><asp:HyperLink ID="hlVenue_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <uc2:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:UpdatePanel ID="upVenueSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <div>
                            <div style="float:left;" class="B">
                                <asp:LinkButton ID="lbVenue_Add" runat="server" Text="Add a New Venue" Font-Bold="true" OnClick="lbVenue_Add_Click"></asp:LinkButton>
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
                                    <div style="float:right; font-weight:normal;">[<asp:LinkButton ID="lbVenue_Search_ReloadReset" runat="server" Text="Reload/Reset Search Options" OnClick="lbVenue_Search_ReloadReset_Click"></asp:LinkButton>]</div>
                                    <div style="clear:both;"></div>
                                </div>
                            </div>
                            <div class="SearchDivInner">
                                <div>
                                    <div style="float:left;"><asp:LinkButton ID="lbVenue_Search_All" runat="server" Text="Search for All Venues" OnClick="lbVenue_Search_All_Click" Font-Bold="True"></asp:LinkButton></div>
                                    <div style="float:left; margin-left:20px;">
                                        <div>
                                            <div class="B">Select first letter of Venue:</div>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbSearchName_Click" CommandArgument="A" Text="A"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbSearchName_Click" CommandArgument="B" Text="B"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lbSearchName_Click" CommandArgument="C" Text="C"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbSearchName_Click" CommandArgument="D" Text="D"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton5" runat="server" OnClick="lbSearchName_Click" CommandArgument="E" Text="E"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lbSearchName_Click" CommandArgument="F" Text="F"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="lbSearchName_Click" CommandArgument="G" Text="G"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton8" runat="server" OnClick="lbSearchName_Click" CommandArgument="H" Text="H"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton9" runat="server" OnClick="lbSearchName_Click" CommandArgument="I" Text="I"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="lbSearchName_Click" CommandArgument="J" Text="J"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton11" runat="server" OnClick="lbSearchName_Click" CommandArgument="K" Text="K"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton12" runat="server" OnClick="lbSearchName_Click" CommandArgument="L" Text="L"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton13" runat="server" OnClick="lbSearchName_Click" CommandArgument="M" Text="M"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton14" runat="server" OnClick="lbSearchName_Click" CommandArgument="N" Text="N"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton15" runat="server" OnClick="lbSearchName_Click" CommandArgument="O" Text="O"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton16" runat="server" OnClick="lbSearchName_Click" CommandArgument="P" Text="P"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton17" runat="server" OnClick="lbSearchName_Click" CommandArgument="Q" Text="Q"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton18" runat="server" OnClick="lbSearchName_Click" CommandArgument="R" Text="R"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton19" runat="server" OnClick="lbSearchName_Click" CommandArgument="S" Text="S"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton20" runat="server" OnClick="lbSearchName_Click" CommandArgument="T" Text="T"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton21" runat="server" OnClick="lbSearchName_Click" CommandArgument="U" Text="U"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton22" runat="server" OnClick="lbSearchName_Click" CommandArgument="V" Text="V"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton23" runat="server" OnClick="lbSearchName_Click" CommandArgument="W" Text="W"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton24" runat="server" OnClick="lbSearchName_Click" CommandArgument="X" Text="X"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton25" runat="server" OnClick="lbSearchName_Click" CommandArgument="Y" Text="Y"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkButton26" runat="server" OnClick="lbSearchName_Click" CommandArgument="Z" Text="Z"></asp:LinkButton>&nbsp;
                                        </div>
                                    </div>
                                    <div style="clear:both;"></div>
                                </div>
                                <hr />
                                <div>
                                    <div style="float:left;"><div class="B">Venues:</div><asp:DropDownList ID="ddlVenue_Search_Venue" runat="server" OnSelectedIndexChanged="ddlVenue_Search_Venue_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                    <div style="float:left; margin-left:20px;"><div class="B">Zip Code:</div><asp:DropDownList ID="ddlVenue_Search_ZipCode" runat="server" OnSelectedIndexChanged="ddlVenue_Search_ZipCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                    <div style="float:left; margin-left:20px;"><div class="B">NY Township:</div><asp:DropDownList ID="ddlVenue_Search_NYTownship" runat="server" OnSelectedIndexChanged="ddlVenue_Search_NYTownship_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                    <div style="float:left; margin-left:20px;"><div class="B">NY County:</div><asp:DropDownList ID="ddlVenue_Search_NYCounty" runat="server" OnSelectedIndexChanged="ddlVenue_Search_NYCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                    <div style="clear:both;"></div>
                                </div>
                                <hr />
                                <uc1:UC_SearchByAddress ID="UC_SearchByAddress1" runat="server" StrPropertyType="VENUE" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="progVenue" runat="server" AssociatedUpdatePanelID="upVenueSearch" DisplayAfter="1000">
                    <ProgressTemplate><div class="fsL I cGray">Data Loading... Please Wait...</div></ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="upVenue" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:Label ID="lblCount" runat="server" CssClass="fsM B I"></asp:Label></div>
                        <asp:GridView ID="gvVenue" Width="100%" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gvVenue_SelectedIndexChanged" OnPageIndexChanging="gvVenue_PageIndexChanging" OnSorting="gvVenue_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View" />
                                <asp:BoundField HeaderText="Location" DataField="location" SortExpression="location" />
                                <asp:TemplateField HeaderText="Event Venue Types">
                                    <ItemTemplate>
                                        <%# WACGlobal_Methods.Format_Venue_EventVenueTypes(Eval("eventVenueTypes"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City" SortExpression="property.city">
                                    <ItemTemplate>
                                        <%# Eval("property.city") %>
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
                            <asp:FormView ID="fvVenue" runat="server" Width="100%" OnModeChanging="fvVenue_ModeChanging" OnItemUpdating="fvVenue_ItemUpdating" OnItemInserting="fvVenue_ItemInserting" OnItemDeleting="fvVenue_ItemDeleting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Venue</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbVenue_Close" runat="server" Text="Close" OnClick="lbVenue_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_eventVenue"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Location:</td><td><%# Eval("location") %></td></tr>
                                            <tr valign="top"><td class="B taR">Phone:</td><td><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Venue_Communication_Phone" runat="server" StrExpressType="COMMUNICATION" StrSection="VENUEPHONE" ShowImageButtonView="false" />&nbsp;<%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("communication1.areacode"), Eval("communication1.number")) %></td></tr>
                                            <tr valign="top"><td class="B taR">Fax:</td><td><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Venue_Communication_Fax" runat="server" StrExpressType="COMMUNICATION" StrSection="VENUEFAX" ShowImageButtonView="false" />&nbsp;<%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("communication.areacode"), Eval("communication.number")) %></td></tr>
                                            <tr valign="top"><td class="B taR">Email:</td><td><%# Eval("email") %></td></tr>
                                            <tr valign="top"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                                            <tr valign="top"><td class="B taR">Property Express:</td><td><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Venue_Property" runat="server" StrExpressType="PROPERTY" /></td></tr>
                                            <tr valign="top"><td class="B taR">Property Address:</td><td><%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), Eval("property.list_address2Type.longname"), Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), false)%></td></tr>
                                            <tr valign="top"><td class="B taR">NY Township:</td><td><%# Eval("property.list_townshipNY.township")%></td></tr>
                                            <tr valign="top"><td class="B taR">NY County:</td><td><%# Eval("property.list_countyNY.county")%></td></tr>
                                        </table>
                                        <hr />
                                        <div class="B">Event Venue Type(s):</div>
                                        <div style="margin-left:20px;">
                                            <asp:ListView ID="lvVenueParticipantTypes" runat="server" DataSource='<%# WACGlobal_Methods.Order_Venue_EventVenueTypes(Eval("eventVenueTypes")) %>'>
                                                <EmptyDataTemplate><div class="I">No Event Venue Type Records</div></EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table cellpadding="3">
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr><td>[<asp:LinkButton id="lbVenue_EventVenueType_Delete" runat="server" Text="Delete" CommandArgument='<%# Eval("pk_eventVenueType") %>' OnClick="lbVenue_EventVenueType_Delete_Click" OnClientClick="return confirm_delete();"></asp:LinkButton>]</td><td><%# Eval("list_participantType.participantType") %></td>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <div><span class="B">Add Event Venue Type: </span><asp:DropDownList ID="ddlVenue_EventVenueType_Add" runat="server" OnSelectedIndexChanged="ddlVenue_EventVenueType_Add_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                        </div>
                                        <hr />
                                        <div class="DivBoxPurple">
                                            <div class="fsL B" style="margin-bottom:5px;">Venue Associations</div>
                                            <div class="B" style="margin-top:10px;">Forestry Events:</div>
                                            <div style="margin: 5px 0px 0px 20px;">
                                                <asp:ListView ID="lvVenue_Forestry_Events" runat="server" DataSource='<%# Eval("eventDateForestries") %>'>
                                                    <EmptyDataTemplate><div class="I">No Event Records</div></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <div><%# Eval("eventNameForestry.title")%></div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <div class="B" style="margin-top:10px;">Marketing Events:</div>
                                            <div style="margin: 5px 0px 0px 20px;">
                                                <asp:ListView ID="lvVenue_F2M_Events" runat="server" DataSource='<%# Eval("events") %>'>
                                                    <EmptyDataTemplate><div class="I">No Event Records</div></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <div><%# Eval("eventName.title")%></div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Venue</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_eventVenue"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Location:</td><td><asp:TextBox ID="tbLocation" runat="server" Text='<%# Bind("location") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="B taR">Phone:</td><td><uc1:UC_Communication_EditInsert ID="UC_Communication_EditInsert_Phone" runat="server" /></td></tr>
                                            <tr valign="top"><td class="B taR">Fax:</td><td><uc1:UC_Communication_EditInsert ID="UC_Communication_EditInsert_Fax" runat="server" /></td></tr>
                                            <tr valign="top"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                                            <tr valign="top"><td colspan="2"><hr /></td></tr>
                                            <tr valign="top"><td colspan="2"><uc1:UC_Property_EditInsert ID="UC_Property_EditInsert1" runat="server" /></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Venue</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Location:</td><td><asp:TextBox ID="tbLocation" runat="server" Text='<%# Bind("location") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="B taR">Phone:</td><td><uc1:UC_Communication_EditInsert ID="UC_Communication_EditInsert_Phone" runat="server" /></td></tr>
                                            <tr valign="top"><td class="B taR">Fax:</td><td><uc1:UC_Communication_EditInsert ID="UC_Communication_EditInsert_Fax" runat="server" /></td></tr>
                                            <tr valign="top"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="6"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbVenue_Add" />
                        <asp:AsyncPostBackTrigger ControlID="lbVenue_Search_ReloadReset" />
                        <asp:AsyncPostBackTrigger ControlID="lbVenue_Search_All" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton2" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton3" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton4" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton5" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton6" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton7" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton8" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton9" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton10" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton11" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton12" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton13" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton14" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton15" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton16" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton17" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton18" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton19" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton20" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton21" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton22" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton23" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton24" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton25" />
                        <asp:AsyncPostBackTrigger ControlID="LinkButton26" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVenue_Search_Venue" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVenue_Search_ZipCode" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVenue_Search_NYTownship" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVenue_Search_NYCounty" />
                    </Triggers>
                </asp:UpdatePanel>
                <uc2:UC_Express_Property runat="server" ID="UC_Express_Property" />
               <%-- <uc1:UC_Express_Property ID="UC_Express_Property1" runat="server" />--%>
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


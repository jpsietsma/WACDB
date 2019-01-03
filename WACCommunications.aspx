<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACCommunications.aspx.cs" Inherits="WACCommunications" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc2" %>
<%@ Register src="~/UserControls/UC_Express_Participant.ascx" tagname="UC_Express_Participant" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtons.ascx" tagname="UC_Express_PageButtons" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtonsInsert.ascx" tagname="UC_Express_PageButtonsInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Global_Insert.ascx" tagname="UC_Global_Insert" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Communications</div>
                    <div style="float:right;" class="B"><asp:HyperLink ID="hlCommunication_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <uc2:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:UpdatePanel ID="upCommunicationSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <div style="float:left;"><asp:LinkButton ID="lbCommunication_PhoneNumber_Add" runat="server" Text="Add a New Phone Number" Font-Bold="true" OnClick="lbCommunication_PhoneNumber_Add_Click"></asp:LinkButton></div>
                            <div style="float:right;"><uc1:UC_Express_PageButtonsInsert ID="UC_Express_PageButtonsInsert1" runat="server" BoolShowGlobal="true" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="SearchDivOuter">
                            <div class="SearchDivContentContainer">
                                <div class="SearchDivContent">
                                    <div style="float:left;">Search Options:</div>
                                    <div style="float:right; font-weight:normal;">[<asp:LinkButton ID="lbCommunication_Search_ReloadReset" runat="server" Text="Reload/Reset Search Options" OnClick="lbCommunication_Search_ReloadReset_Click"></asp:LinkButton>]</div>
                                    <div style="clear:both;"></div>
                                </div>
                                <div class="SearchDivInner">
                                    <div>
                                        <div style="float:left;"><asp:LinkButton ID="lbCommunication_Search_PhoneNumber_All" runat="server" Text="Search for All Phone Numbers" OnClick="lbCommunication_Search_PhoneNumber_All_Click" Font-Bold="True"></asp:LinkButton></div>
                                        <div style="float:left; margin-left:20px;"><div class="B">Phone Number Area Code:</div><asp:DropDownList ID="ddlCommunication_Search_PhoneNumber_AreaCode" runat="server" OnSelectedIndexChanged="ddlCommunication_Search_PhoneNumber_AreaCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                        <div style="float:left; margin-left:20px;">
                                                <span class="B">All or Part of Phone Number:</span>
                                                <asp:Panel ID="pnlCommunication_Search_PhoneNumber" runat="server" DefaultButton="btnCommunication_Search_PhoneNumber">
                                                    <asp:TextBox ID="tbCommunication_Search_PhoneNumber" runat="server"></asp:TextBox>&nbsp;<asp:Image ImageAlign="Bottom" ID="imgAdvisory1" runat="server" ImageUrl="~/images/exclamation.png" />&nbsp;<asp:Button ID="btnCommunication_Search_PhoneNumber" runat="server" Text="Search" OnClick="btnCommunication_Search_PhoneNumber_Click" />
                                                </asp:Panel>
                                            </div>
                                        <div style="clear:both;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="progCommunication" runat="server" AssociatedUpdatePanelID="upCommunicationSearch" DisplayAfter="1000">
                    <ProgressTemplate><div class="fsL I cGray">Data Loading... Please Wait...</div></ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="upCommunication" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:Label ID="lblCount" runat="server" CssClass="fsM B I"></asp:Label></div>
                        <asp:GridView ID="gvCommunication_PhoneNumbers" Width="100%" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gvCommunication_PhoneNumbers_SelectedIndexChanged" OnPageIndexChanging="gvCommunication_PhoneNumbers_PageIndexChanging" OnSorting="gvCommunication_PhoneNumbers_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View" />
                                <asp:TemplateField HeaderText="Phone Number" SortExpression="number">
                                    <ItemTemplate>
                                        <%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("areacode"), Eval("number")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top:10px;">
                            <asp:FormView ID="fvCommunication_PhoneNumber" runat="server" Width="100%" OnModeChanging="fvCommunication_PhoneNumber_ModeChanging" OnItemUpdating="fvCommunication_PhoneNumber_ItemUpdating" OnItemInserting="fvCommunication_PhoneNumber_ItemInserting" OnItemDeleting="fvCommunication_PhoneNumber_ItemDeleting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Phone Number</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbCommunication_PhoneNumber_Close" runat="server" Text="Close" OnClick="lbCommunication_PhoneNumber_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_communication"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Phone Number:</td><td><%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("areacode"), Eval("number")) %></td></tr>
                                        </table>
                                        <hr />
                                        <div class="DivBoxPurple">
                                            <div class="fsL B" style="margin-bottom:5px;">Communication Associations</div>
                                            <div class="B" style="margin-top:10px;">Participants:</div>
                                            <div style="margin: 5px 0px 0px 20px;">
                                                <asp:ListView ID="lvCommunication_ParticipantCommunications" runat="server" DataSource='<%# Eval("participantCommunications") %>'>
                                                    <EmptyDataTemplate><div class="I">No Participant Communication Records</div></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <div><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Communication_Participants" runat="server" />&nbsp;<%# Eval("participant.fullname_LF_dnd") %></div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <div class="B" style="margin-top:10px;">Venues (Phone):</div>
                                            <div style="margin: 5px 0px 0px 20px;">
                                                <asp:ListView ID="lvCommunication_Venues_Phone" runat="server" DataSource='<%# Eval("eventVenues1") %>'>
                                                    <EmptyDataTemplate><div class="I">No Venue Phone Number Records</div></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <div><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Communication_Venues_Phone" runat="server" StrExpressType="VENUE" ShowImageButtonView="false" />&nbsp;<%# Eval("location")%></div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <div class="B" style="margin-top:10px;">Venues (Fax):</div>
                                            <div style="margin: 5px 0px 0px 20px;">
                                                <asp:ListView ID="lvCommunication_Venues_Fax" runat="server" DataSource='<%# Eval("eventVenues") %>'>
                                                    <EmptyDataTemplate><div class="I">No Venue Fax Number Records</div></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <div><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Communication_Venues_Fax" runat="server" StrExpressType="VENUE" ShowImageButtonView="false" />&nbsp;<%# Eval("location")%></div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Phone Number</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_communication"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Area Code:</td><td><asp:TextBox ID="tbAreaCode" runat="server" Text='<%# Bind("areacode") %>'></asp:TextBox></td><td>ex: 670 (3 numbers)</td></tr>
                                            <tr valign="top"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbPhoneNumber" runat="server" Text='<%# Bind("number") %>'></asp:TextBox></td><td>ex: 8657090 (7 numbers)</td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Phone Number</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Area Code:</td><td><asp:TextBox ID="tbAreaCode" runat="server" Text='<%# Bind("areacode") %>'></asp:TextBox></td><td>ex: 670 (3 numbers)</td></tr>
                                            <tr valign="top"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbPhoneNumber" runat="server" Text='<%# Bind("number") %>'></asp:TextBox></td><td>ex: 8657090 (7 numbers)</td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbCommunication_PhoneNumber_Add" />
                        <asp:AsyncPostBackTrigger ControlID="lbCommunication_Search_ReloadReset" />
                        <asp:AsyncPostBackTrigger ControlID="lbCommunication_Search_PhoneNumber_All" />
                        <asp:AsyncPostBackTrigger ControlID="btnCommunication_Search_PhoneNumber" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCommunication_Search_PhoneNumber_AreaCode" />
                    </Triggers>
                </asp:UpdatePanel>

                <uc1:UC_Express_Participant ID="UC_Express_Participant1" runat="server" />
                <uc1:UC_Global_Insert ID="UC_Global_Insert1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_Express_Participant.ascx.cs" Inherits="UC_Express_Participant" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_EditCalendar.ascx" tagname="UC_EditCalendar" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Property_EditInsert.ascx" tagname="UC_Property_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_ControlGroup_ParticipantCommunication.ascx" tagname="UC_ControlGroup_ParticipantCommunication" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_ControlGroup_ParticipantProperty.ascx" tagname="UC_ControlGroup_ParticipantProperty" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_ControlGroup_ParticipantType.ascx" tagname="UC_ControlGroup_ParticipantType" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_Global_Insert.ascx" tagname="UC_Express_Global_Insert" tagprefix="uc1" %>
<div>
    <asp:Panel ID="pnlExpress_Participant" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
        <asp:UpdatePanel ID="upExpress_Participant" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="float:left;"><div><span class="fsM B">Express Participant</span><span style="margin-left:20px;"><asp:ImageButton ID="ibGlobal" runat="server" ImageUrl="~/images/arrow_orb_plus_16.png" OnClick="btnExpress_GlobalInsert_Click" ToolTip="Click to Open the Global Insert Express Window" ImageAlign="Bottom"></asp:ImageButton> <asp:LinkButton ID="lbGlobal" runat="server" Text="Global" OnClick="btnExpress_GlobalInsert_Click" ToolTip="Click to Open the Global Insert Express Window" Font-Bold="true"></asp:LinkButton></span></div></div>
                <div style="float:right;">[<asp:LinkButton ID="lbExpress_Participant_Close" runat="server" Text="Close" OnClick="lbExpress_Participant_Close_Click"></asp:LinkButton>]</div>
                <div style="clear:both;"></div>
                <asp:HiddenField ID="hfParticipantPK" runat="server" />
                <hr />
                <asp:Panel ID="pnlGlobalInsert" runat="server" Visible="false">
                    <div class="NestedDivLevel03" style="margin-bottom:10px;">
                        <div style="float:left;"><span class="fsM B">Global Insert >> Express</span></div>
                        <div style="float:right;">[<asp:LinkButton ID="lbGlobal_Insert_Close" runat="server" Text="Close Global Insert" OnClick="lbGlobal_Insert_Close_Click"></asp:LinkButton>]</div>
                        <div style="clear:both;"></div>
                        <hr />
                        <uc1:UC_Express_Global_Insert ID="UC_Express_Global_Insert1" runat="server" />
                    </div>
                </asp:Panel>
                <asp:FormView ID="fvParticipant" runat="server" Width="100%" OnModeChanging="fvParticipant_ModeChanging" OnItemUpdating="fvParticipant_ItemUpdating">
                    <ItemTemplate>
                        <div class="NestedDivLevel01">
                            <div><span class="fsM B">Participant View</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participant"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                            <hr />
                            <table cellpadding="3">
                                <tr valign="top">
                                    <td>
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Active:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active"))%></td></tr>
                                            <tr valign="top"><td class="B taR">Prefix:</td><td><%# Eval("list_prefix.prefix")%></td></tr>
                                            <tr valign="top"><td class="B taR">First Name:</td><td><%# Eval("fname") %></td></tr>
                                            <tr valign="top"><td class="B taR">Middle Name:</td><td><%# Eval("mname") %></td></tr>
                                            <tr valign="top"><td class="B taR">Last Name:</td><td><%# Eval("lname") %></td></tr>
                                            <tr valign="top"><td class="B taR">Suffix:</td><td><%# Eval("list_suffix.suffix")%></td></tr>
                                            <tr valign="top"><td class="B taR">Nickname:</td><td><%# Eval("nickname") %></td></tr>
                                            <tr valign="top"><td class="B taR">Organization:</td><td><%# Eval("organization.org")%></td></tr>
                                            <tr valign="top"><td class="B taR">WAC Region:</td><td><%# Eval("list_regionWAC.regionWAC")%></td></tr>
                                        </table>
                                    </td>
                                    <td style="width:20px;">&nbsp;</td>
                                    <td>
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="B taR">Mailing Status:</td><td><%# Eval("list_mailingStatus.status")%></td></tr>
                                            <tr valign="top"><td class="B taR">Email:</td><td><%# WACGlobal_Methods.Format_Global_Email_MailTo(Eval("email")) %></td></tr>
                                            <tr valign="top"><td class="B taR">Web Site:</td><td><%# WACGlobal_Methods.Format_Global_URL(Eval("web")) %></td></tr>
                                            <tr valign="top"><td class="B taR">Gender:</td><td><%# Eval("list_gender.gender")%></td></tr>
                                            <tr valign="top"><td class="B taR">Ethnicity:</td><td><%# Eval("list_ethnicity.ethnicity")%></td></tr>
                                            <tr valign="top"><td class="B taR">Race:</td><td><%# Eval("list_race.race")%></td></tr>
                                            <tr valign="top"><td class="B taR">Diversity Data:</td><td><%# Eval("list_diversityData.dataSetVia")%></td></tr>
                                            <tr valign="top"><td class="B taR">Form W9 Signed Date:</td><td><%# WACGlobal_Methods.Format_Color_General_ExpiredDates(Eval("form_W9_signed_date"), 2)%></td></tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <div class="NestedDivBlue00"><uc1:UC_ControlGroup_ParticipantCommunication ID="UC_ControlGroup_ParticipantCommunication1" runat="server" /></div>
                            <hr />
                            <div class="NestedDivBlue00"><uc1:UC_ControlGroup_ParticipantProperty ID="UC_ControlGroup_ParticipantProperty1" runat="server" /></div>
                            <hr />
                            <div class="NestedDivBlue00"><uc1:UC_ControlGroup_ParticipantType ID="UC_ControlGroup_ParticipantType1" runat="server" /></div>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="NestedDivLevel01">
                            <div><span class="fsM B">Participant Edit</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participant"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                            <hr />
                            <table cellpadding="3">
                                <tr valign="top"><td class="B taR">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Prefix:</td><td><asp:DropDownList ID="ddlPrefix" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">First Name:</td><td><asp:TextBox ID="tbNameFirst" runat="server" Text='<%# Bind("fname") %>' Width="400px"></asp:TextBox></td></tr>
                                <tr valign="top"><td class="B taR">Middle Name:</td><td><asp:TextBox ID="tbNameMiddle" runat="server" Text='<%# Bind("mname") %>' Width="400px"></asp:TextBox></td></tr>
                                <tr valign="top"><td class="B taR">Last Name:</td><td><asp:TextBox ID="tbNameLast" runat="server" Text='<%# Bind("lname") %>' Width="400px"></asp:TextBox></td></tr>
                                <tr valign="top"><td class="B taR">Suffix:</td><td><asp:DropDownList ID="ddlSuffix" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Nickname:</td><td><asp:TextBox ID="tbNickname" runat="server" Text='<%# Bind("nickname") %>' Width="400px"></asp:TextBox></td></tr>
                                <tr valign="top"><td class="B taR">Organization:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Organization" runat="server" StrParentCase="ORGANIZATION" StrEntityType="ORGANIZATION" ShowStartsWithNumber="true" /></td></tr>
                                <tr valign="top"><td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlRegionWAC" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Mailing Status:</td><td><asp:DropDownList ID="ddlMailingStatus" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                                <tr valign="top"><td class="B taR">Web Site:</td><td><asp:TextBox ID="tbWeb" runat="server" Text='<%# Bind("web") %>' Width="400px"></asp:TextBox></td></tr>
                                <tr valign="top"><td class="B taR">Gender:</td><td><asp:DropDownList ID="ddlGender" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Ethnicity:</td><td><asp:DropDownList ID="ddlEthnicity" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Race:</td><td><asp:DropDownList ID="ddlRace" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Diversity Data:</td><td><asp:DropDownList ID="ddlDiversityData" runat="server"></asp:DropDownList></td></tr>
                                <tr valign="top"><td class="B taR">Form W9 Signed Date:</td><td><uc1:UC_EditCalendar ID="UC_EditCalendar_Participant_FormW9SignedDate" runat="server" /></td></tr>
                            </table>
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lbHiddenExpress_Participant" runat="server"></asp:LinkButton>
    <ajtk:ModalPopupExtender ID="mpeExpress_Participant" runat="server" TargetControlID="lbHiddenExpress_Participant" PopupControlID="pnlExpress_Participant" BackgroundCssClass="ModalPopup_BG">
    </ajtk:ModalPopupExtender>
</div>
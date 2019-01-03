<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACHR_EmployeeDetail.ascx.cs" 
 Inherits="HR_WACHR_EmployeeDetail" %>
<%@ Register Src="~/hr/wachr_position.ascx" TagPrefix="uc" TagName="WACHR_Position" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/CustomControls/AjaxCalendar.ascx" tagname="AjaxCalendar" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DocumentArchive.ascx" tagname="UC_DocumentArchive" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>
<%@ Register Src="~/HR/WACHR_Filters.ascx" tagname="HRFilters" TagPrefix="hruc" %>
<%@ Register Src="~/HR/WACHR_Position.ascx" TagName="HR_PositionPanel" TagPrefix="hruc" %>
<%@ Register Src="~/HR/WACHR_Activity.ascx" TagName="HR_ActivityPanel" TagPrefix="hruc" %>
<%@ Register Src="~/HR/WACHR_Note.ascx" TagName="HR_NotePanel" TagPrefix="hruc" %>
<%@ Register Src="~/HR/WACHR_Evaluation.ascx" TagName="HR_EvalPanel" TagPrefix="hruc" %>
<%@ Register Src="~/HR/WACHR_Training.ascx" TagName="HR_TrainingPanel" TagPrefix="hruc" %>
<%@ Register Src="~/HR/WACHR_Phone.ascx" TagName="HR_PhonePanel" TagPrefix="hruc" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
<asp:UpdatePanel ID="upHR_WACEmployee" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <%--<div class="fsM"><uc1:UC_Express_PageButtons ID="UC_Express_PageButtons_Ag_FarmBusiness_Property" runat="server" StrExpressType="PROPERTY" StrSection="AGRICULTURE" />&nbsp;<%# WACGlobal_Methods.SpecialText_Global_Address(Eval("farmland.property.address"), Eval("farmland.property.address2"), Eval("farmland.property.list_address2Type.longname"), Eval("farmland.property.city"), Eval("farmland.property.state"), Eval("farmland.property.fk_zipcode"), Eval("farmland.property.zip4"), true)%></div>--%>
        <asp:FormView ID="fvHR_WACEmployee" runat="server" Width="100%" OnModeChanging="fvHR_WACEmployee_ModeChanging" 
        OnItemUpdating="fvHR_WACEmployee_ItemUpdating" OnItemInserting="fvHR_WACEmployee_ItemInserting" 
        OnItemDeleting="fvHR_WACEmployee_ItemDeleting">
    <ItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participant1.fullname_LF_dnd")%></span> 
                [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>|
                <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" 
                    OnClientClick="return confirm_delete();"></asp:LinkButton>|
                <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel" 
                    OnCommand="HRDetails_Cancel_Click"></asp:LinkButton>] 
                <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC"), 
                Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span>
                <br /><div class="fsM"><%# ParticipantAddress(Eval("pk_participantWAC"))%></div> 
            </div>
            <hr />           
            <ajtk:TabContainer ID="tcHR_WACEmployee" runat="server" OnActiveTabChanged="tcHR_WACEmployee_TabChanged" OnDemand="true" >
                <ajtk:TabPanel ID="tpHR_WACEmployee_Details" runat="server" HeaderText="Details" OnDemandMode="Always">
                    <ContentTemplate>
                        <div class="DivBoxOrange" style="margin:10px 0px 0px 0px;">
                            <uc1:UC_DocumentArchive ID="UC_DocumentArchive_HR_OVER" runat="server" StrArea="HR" StrAreaSector="HR_OVER" />
                        </div>
                        <div style="float:left;">
                            <table cellpadding="3">
                                <tr valign="top"><td class="B taR">Participant:</td><td><%# Eval("participant1.fullname_LF_dnd")%></td></tr>
                                <tr valign="top"><td class="B taR">Location:</td><td><%# Eval("list_wacLocation.location")%></td></tr>
                                <tr valign="top"><td class="B taR">Phone Number:</td><td><%# Eval("list_wacLocation.phone")%></td></tr>
                                <tr valign="top"><td class="B taR">Phone Extension:</td><td><%# Eval("phone_ext") %></td></tr>
                                <tr valign="top"><td class="B taR">Email:</td><td><a href='mailto:<%# Eval("email") %>'><%# Eval("email") %></a></td></tr>
                                <tr valign="top"><td class="B taR">Date of Birth:</td><td><%# Eval("dob", "{0:d}") %></td></tr>
                                <tr valign="top"><td class="B taR">Marital Status:</td><td><%# Eval("list_maritalStatus.status") %></td></tr>
                                <tr valign="top"><td class="B taR">Emergency Contact:</td><td><%# Eval("participant.fullname_LF_dnd") %></td></tr>
                                <tr valign="top"><td class="B taR">Emergency Contact Relationship:</td><td><%# Eval("list_employeeRelationship.relationship") %></td></tr>
                                <tr valign="top"><td class="B taR">Allergies:</td><td><%# Eval("allergies") %></td></tr>
                                <tr valign="top"><td class="B taR">Start Date:</td><td><%# Eval("start_date", "{0:d}")%></td></tr>
                                <tr valign="top"><td class="B taR">Finish Date:</td><td><%# Eval("finish_date", "{0:d}")%></td></tr>
                                                
                            </table>
                        </div>
                        <div style="float:left; margin-left:20px;">
                            <table cellpadding="3">
                                <tr valign="top"><td class="B taR">Hire Letter:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("hireLetter")) %></td></tr>
                                <tr valign="top"><td class="B taR">Resume:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("resume")) %></td></tr>
                                <tr valign="top"><td class="B taR">Conflict of Interest:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("conflictOfInterest")) %></td></tr>
                                <tr valign="top"><td class="B taR">Received Manual:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("recdManual")) %></td></tr>
                                <tr valign="top"><td class="B taR">LENS:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("LENS")) %></td></tr>
                                <%--<tr valign="top"><td class="B taR">Ethics:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("ethics")) %></td></tr>--%>
                                <tr valign="top"><td class="B taR">Work Schedule:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("workSchedule")) %></td></tr>
                                <tr valign="top"><td class="B taR">Flex Schedule:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("flexSchedule")) %></td></tr>
                                <tr valign="top"><td class="B taR">NWD Schedule:</td><td><%# Eval("list_NWD.setting") %></td></tr>
                                <tr valign="top"><td class="B taR">Confidentiality Agreement:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("confidentialityAgreement")) %></td></tr>
                                <tr valign="top"><td class="B taR">Personal Info Sheet:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("personalInfoSheet"))%></td></tr>
                                <tr valign="top"><td class="B taR">SLT:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("SLT"))%></td></tr>
                                <tr valign="top"><td class="B taR">Field Staff:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("fieldStaff"))%></td></tr>
                                <tr valign="top"><td class="B taR">TeleCommute Approved:</td><td><%# Eval("teleCommute_date", "{0:d}") %></td></tr>
                                <tr valign="top"><td class="B taR">TeleCommute Schedule:</td><td><%# Eval("list_telecommuteSchedule.setting") %></td></tr>
                            </table>
                        </div>
                        <div style="clear:both;"></div>
                    </ContentTemplate>
                </ajtk:TabPanel>
                <ajtk:TabPanel ID="tpHR_WACEmployee_Activity" runat="server" HeaderText="Activity" OnDemandMode="Always">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_Activity_Insert" runat="server" Text="Add an Activity" 
                            Font-Bold="true" CommandArgument="activity" OnClick="lbHR_WACEmployees_Subtable_Insert_Click"></asp:LinkButton></div>
                        <asp:GridView ID="gvHR_WACEmployees_Activity" runat="server" SkinID="gvSkin" Width="100%" AutoGenerateColumns="false" 
                            OnRowCommand="gvHR_WACEmployees_Activity_RowCommand">
                            <EmptyDataTemplate>No Activity Records</EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:BoundField HeaderText="Date" DataField="date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Note" DataField="note" />
                               <%-- <asp:TemplateField HeaderText="Fiscal Year"><ItemTemplate><%# Eval("list_fiscalYear.fiscalYear") %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Position"><ItemTemplate><%# Eval("list_positionWAC.position") %></ItemTemplate></asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </ajtk:TabPanel>
                <ajtk:TabPanel ID="tpHR_WACEmployee_Evaluation" runat="server" HeaderText="Evaluation" OnDemandMode="Always">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_Evaluation_Insert" runat="server" 
                        Text="Add an Evaluation" Font-Bold="true" 
                        CommandArgument="evaluation" OnClick="lbHR_WACEmployees_Subtable_Insert_Click"></asp:LinkButton></div>
                        <asp:GridView ID="gvHR_WACEmployees_Evaluation" runat="server" SkinID="gvSkin" Width="100%" 
                        AutoGenerateColumns="false" OnRowCommand="gvHR_WACEmployees_Evaluation_RowCommand">
                            <EmptyDataTemplate>No Evaluation Records</EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:TemplateField HeaderText="Position"><ItemTemplate><%# Eval("list_positionWAC.position") %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Fiscal Year"><ItemTemplate><%# Eval("list_fiscalYear.fiscalYear") %></ItemTemplate></asp:TemplateField>
                                <asp:BoundField HeaderText="Date" DataField="date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Rating" DataField="rating" />
                                <asp:TemplateField HeaderText="Job Description"><ItemTemplate><%# Eval("jobDescription") != null ? Eval("jobDescription").ToString() == "Y" ? "Yes" : "No" : "" %></ItemTemplate></asp:TemplateField>
                                <asp:BoundField HeaderText="Follow Up Date" DataField="followup_date" DataFormatString="{0:d}" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </ajtk:TabPanel>
                <ajtk:TabPanel ID="tpHR_WACEmployee_Note" runat="server" HeaderText="Note" OnDemandMode="Always">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_Note_Insert" runat="server" Text="Add a Note" Font-Bold="true" CommandArgument="note" OnClick="lbHR_WACEmployees_Subtable_Insert_Click"></asp:LinkButton></div>
                        <asp:GridView ID="gvHR_WACEmployees_Note" runat="server" SkinID="gvSkin" Width="100%" AutoGenerateColumns="false" OnRowCommand="gvHR_WACEmployees_Note_RowCommand">
                            <EmptyDataTemplate>No Note Records</EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:BoundField HeaderText="Created" DataField="created" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Note" DataField="note" />
                                <asp:TemplateField HeaderText="Position"><ItemTemplate><%# Eval("list_positionWAC.position") %></ItemTemplate></asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </ajtk:TabPanel>
                <ajtk:TabPanel ID="tpHR_WACEmployee_Phone" runat="server" HeaderText="Phone" OnDemandMode="Always">
                     <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_Phone_Insert" runat="server" 
                            Text="Add a Phone Number" Font-Bold="true" CommandArgument="phone" 
                            OnClick="lbHR_WACEmployees_Subtable_Insert_Click"></asp:LinkButton></div>
                        <asp:GridView ID="gvHR_WACEmployees_Phone" runat="server" SkinID="gvSkin" Width="100%" AutoGenerateColumns="false" 
                            OnRowCommand="gvHR_WACEmployees_Phone_RowCommand">
                            <EmptyDataTemplate>No Phone Records</EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:TemplateField HeaderText="Phone Number"><ItemTemplate><%# Eval("PhoneFormattedHR") %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Public"><ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("publicUsage"))%></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Emergency"><ItemTemplate><%# WACGlobal_Methods.Format_Global_YesNo(Eval("emergency"))%></ItemTemplate></asp:TemplateField>
                            </Columns>  
                        </asp:GridView>
                    </ContentTemplate>
                </ajtk:TabPanel>
                <ajtk:TabPanel ID="tpHR_WACEmployee_Position" runat="server" HeaderText="Position" OnDemandMode="Always">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_Position_Insert" runat="server" 
                        Text="Add a Position" Font-Bold="true" CommandArgument="position" OnClick="lbHR_WACEmployees_Subtable_Insert_Click"></asp:LinkButton></div>
                        <asp:GridView ID="gvHR_WACEmployees_Position" runat="server" SkinID="gvSkin" Width="100%" AutoGenerateColumns="false" 
                        OnRowCommand="gvHR_WACEmployees_Position_RowCommand">
                            <EmptyDataTemplate>No Position Records</EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:TemplateField HeaderText="Position"><ItemTemplate><%# Eval("list_positionWAC.position") %></ItemTemplate></asp:TemplateField>
                                <asp:BoundField HeaderText="Start Date" DataField="start_date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Finish Date" DataField="finish_date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Beginning Salary" DataField="salary_begin" DataFormatString="{0:c}" />
                                <asp:BoundField HeaderText="Note" DataField="note" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </ajtk:TabPanel>
                <ajtk:TabPanel ID="tpHR_WACEmployee_Training" runat="server" HeaderText="Training" OnDemandMode="Always">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_Training_Insert" runat="server" Text="Add a Training Record" Font-Bold="true" CommandArgument="training" OnClick="lbHR_WACEmployees_Subtable_Insert_Click"></asp:LinkButton></div>
                        <asp:GridView ID="gvHR_WACEmployees_Training" runat="server" SkinID="gvSkin" Width="100%" AutoGenerateColumns="false" OnRowCommand="gvHR_WACEmployees_Training_RowCommand">
                            <EmptyDataTemplate>No Training Records</EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                 <asp:TemplateField HeaderText="Position"><ItemTemplate><%# Eval("list_positionWAC.position") %></ItemTemplate></asp:TemplateField>
                                 <asp:TemplateField HeaderText="Fiscal Year"><ItemTemplate><%# Eval("list_fiscalYear.fiscalYear") %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Training Required"><ItemTemplate><%# Eval("list_trainingReqd.title") %></ItemTemplate></asp:TemplateField>
                                <asp:BoundField HeaderText="Completed Date" DataField="completed_date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Expiration Date" DataField="expiration_date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Note" DataField="note" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </ajtk:TabPanel>
            </ajtk:TabContainer>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <div class="NestedDivLevel01">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton>|<asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Participant:</td><td><%# Eval("participant1.fullname_LF_dnd")%></td></tr>
                <tr valign="top"><td class="B taR">Location:</td><td><asp:DropDownList ID="ddlWACLocation" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Phone Extension:</td><td><asp:TextBox ID="tbPhoneExt" runat="server" Text='<%# Bind("phone_ext") %>' MaxLength="4"></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' MaxLength="48" Width="400px"></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">&nbsp;</td><td></td></tr>
                <tr valign="top"><td class="B taR">Date of Birth:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_DOB" runat="server" /></td></tr>
                <tr valign="top"><td class="B taR">Marital Status:</td><td><asp:DropDownList ID="ddlMaritalStatus" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Emergency Contact:</td><td><uc1:UC_DropDownListByAlphabet ID="DropDownListByAlphabet_EmergencyContact" runat="server" StrParentCase="PARTICIPANT_PERSON_SEARCH" StrEntityType="PARTICIPANT" /></td></tr>
                <tr valign="top"><td class="B taR">Emergency Contact Relationship:</td><td><asp:DropDownList ID="ddlEmergencyContactRelationship" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Allergies:</td><td><asp:TextBox ID="tbAllergies" runat="server" Text='<%# Bind("allergies") %>' MaxLength="255" TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Start Date:</td><td><uc1:AjaxCalendar ID="calStartDate" runat="server" Text=<%# Bind("start_date") %> /></td></tr>
                <tr valign="top"><td class="B taR">Finish Date:</td><td><uc1:AjaxCalendar ID="calFinishDate" runat="server" Text=<%# Bind("finish_date") %> /></td></tr>
                <tr valign="top"><td class="B taR">&nbsp;</td><td></td></tr>
                <tr valign="top"><td class="B taR">Hire Letter:</td><td><asp:DropDownList ID="ddlHireLetter" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Resume:</td><td><asp:DropDownList ID="ddlResume" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Conflict of Interest:</td><td><asp:DropDownList ID="ddlConflictOfInterest" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Received Manual:</td><td><asp:DropDownList ID="ddlReceivedManual" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">LENS:</td><td><asp:DropDownList ID="ddlLENS" runat="server"></asp:DropDownList></td></tr>
                <%--<tr valign="top"><td class="B taR">Ethics:</td><td><asp:DropDownList ID="ddlEthics" runat="server"></asp:DropDownList></td></tr>--%>
                <tr valign="top"><td class="B taR">Work Schedule:</td><td><asp:DropDownList ID="ddlWorkSchedule" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Flex Schedule:</td><td><asp:DropDownList ID="ddlFlexSchedule" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">NWD Schedule:</td><td><asp:DropDownList ID="ddlNWD" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Confidentiality Agreement:</td><td><asp:DropDownList ID="ddlConfidentialityAgreement" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Personal Info Sheet:</td><td><asp:DropDownList ID="ddlPersonalInfoSheet" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">SLT:</td><td><asp:DropDownList ID="ddlSLT" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Field Staff:</td><td><asp:DropDownList ID="ddlFieldStaff" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">TeleCommute Approved:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_TeleCommuteDate" runat="server" /></td></tr>
                <tr valign="top"><td class="B taR">TeleCommute Schedule:</td><td><asp:DropDownList ID="ddlTeleCommuteSchedule" runat="server"></asp:DropDownList></td></tr>
            </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div class="NestedDivLevel01">
            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Participant:</td><td><asp:DropDownList ID="ddlWACEmployees" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Location:</td><td><asp:DropDownList ID="ddlWACLocation" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Phone Extension:</td><td><asp:TextBox ID="tbPhoneExt" runat="server" Text='<%# Bind("phone_ext") %>' Width="400px"></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Email:</td><td><asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("email") %>' Width="400px"></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">&nbsp;</td><td></td></tr>
                <tr valign="top"><td class="B taR">Date of Birth:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_DOB" runat="server" /></td></tr>
                <tr valign="top"><td class="B taR">Marital Status:</td><td><asp:DropDownList ID="ddlMaritalStatus" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Emergency Contact:</td><td><uc1:UC_DropDownListByAlphabet ID="DropDownListByAlphabet_EmergencyContact" runat="server" StrParentCase="PARTICIPANT_PERSON_SEARCH" StrEntityType="PARTICIPANT" /></td></tr>
                <tr valign="top"><td class="B taR">Emergency Contact Relationship:</td><td><asp:DropDownList ID="ddlEmergencyContactRelationship" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Allergies:</td><td><asp:TextBox ID="tbAllergies" runat="server" Text='<%# Bind("allergies") %>' MaxLength="255" TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
                 <tr valign="top"><td class="B taR">Start Date:</td><td><uc1:AjaxCalendar ID="calStartDate" runat="server" Text=<%# Bind("start_date") %> /></td></tr>
                <tr valign="top"><td class="B taR">Finish Date:</td><td><uc1:AjaxCalendar ID="calFinishDate" runat="server" Text=<%# Bind("finish_date") %> /></td></tr>
                <tr valign="top"><td class="B taR">&nbsp;</td><td></td></tr>
                <tr valign="top"><td class="B taR">Hire Letter:</td><td><asp:DropDownList ID="ddlHireLetter" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Resume:</td><td><asp:DropDownList ID="ddlResume" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Conflict of Interest:</td><td><asp:DropDownList ID="ddlConflictOfInterest" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Received Manual:</td><td><asp:DropDownList ID="ddlReceivedManual" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">LENS:</td><td><asp:DropDownList ID="ddlLENS" runat="server"></asp:DropDownList></td></tr>
                <%--<tr valign="top"><td class="B taR">Ethics:</td><td><asp:DropDownList ID="ddlEthics" runat="server"></asp:DropDownList></td></tr>--%>
                <tr valign="top"><td class="B taR">Work Schedule:</td><td><asp:DropDownList ID="ddlWorkSchedule" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Flex Schedule:</td><td><asp:DropDownList ID="ddlFlexSchedule" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">NWD Schedule:</td><td><asp:DropDownList ID="ddlNWD" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Confidentiality Agreement:</td><td><asp:DropDownList ID="ddlConfidentialityAgreement" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Personal Info Sheet:</td><td><asp:DropDownList ID="ddlPersonalInfoSheet" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">SLT:</td><td><asp:DropDownList ID="ddlSLT" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Field Staff:</td><td><asp:DropDownList ID="ddlFieldStaff" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">TeleCommute Approved:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_TeleCommuteDate" runat="server" /></td></tr>
                <tr valign="top"><td class="B taR">TeleCommute Schedule:</td><td><asp:DropDownList ID="ddlTeleCommuteSchedule" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">&nbsp;</td><td></td></tr>
                <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server" /></td></tr>
                <tr><td class="taR B">Start Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_StartDate" runat="server" /></td></tr>
            </table>
    </InsertItemTemplate>


</asp:FormView>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:Panel ID="pnlHR_Modal" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
    <asp:UpdatePanel ID="upHR_Modal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="fsM B" style="float:left;"><asp:Label ID="lblHR_Modal_Title" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label></div>
            <div style="float:right;">[<asp:LinkButton ID="lbHR_Modal_Close" runat="server" Text="Close" OnClick="lbHR_Modal_Close_Click"></asp:LinkButton>]</div>
            <div style="clear:both;"></div>
            <hr />
            <asp:HiddenField ID="hfHR_Modal" runat="server" />
            <asp:Panel ID="pnlHR_Modal_Activity" runat="server" Visible="false">
                <hruc:HR_ActivityPanel ID="pnlHR_Activity" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlHR_Modal_Evaluation" runat="server" Visible="false">
                <hruc:HR_EvalPanel ID="pnlHR_Evaluation" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlHR_Modal_Note" runat="server" Visible="false">
                <hruc:HR_NotePanel ID="pnlHR_Note" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlHR_Modal_Phone" runat="server" Visible="false">
                <hruc:HR_PhonePanel ID="pnlHR_Phone" runat="server"/>
            </asp:Panel>
            <asp:Panel ID="pnlHR_Modal_Position" runat="server" Visible="false">
                 <hruc:HR_PositionPanel ID="pnlHR_Position" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlHR_Modal_Training" runat="server" Visible="false">
                <hruc:HR_TrainingPanel ID="pnlHR_Training" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>

<asp:LinkButton ID="lbHR_Hidden_Modal" runat="server"></asp:LinkButton>
<ajtk:ModalPopupExtender ID="mpeHR_Modal" runat="server" TargetControlID="lbHR_Hidden_Modal" PopupControlID="pnlHR_Modal" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

           


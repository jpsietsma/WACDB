<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/HR/WACHR_Evaluation.ascx.cs" Inherits="HR_WACHR_Evaluation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<asp:FormView ID="fvHR_WACEmployee_Evaluation" runat="server" Width="100%" 
                    OnModeChanging="fvHR_WACEmployee_Evaluation_ModeChanging" 
                    OnItemInserting="fvHR_WACEmployee_Evaluation_ItemInserting" 
                    OnItemUpdating="fvHR_WACEmployee_Evaluation_ItemUpdating" 
                    OnItemDeleting="fvHR_WACEmployee_Evaluation_ItemDeleting">
    <EmptyDataTemplate>No Note Records</EmptyDataTemplate>
    <ItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
            [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" 
                OnClientClick="return confirm_delete();"></asp:LinkButton>] 
                <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_evaluation"), 
                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr><td class="taR B">Position:</td><td><%# Eval("list_positionWAC.position")%></td></tr>
                <tr><td class="taR B">Fiscal Year:</td><td><%# Eval("list_fiscalYear.fiscalYear")%></td></tr>
                <tr><td class="taR B">Evaluation Date:</td><td><%# Eval("date", "{0:d}") %></td></tr>
                <tr><td class="taR B">Note:</td><td><%# Eval("note")%></td></tr>
                <tr><td class="taR B">Rating:</td><td><%# Eval("rating")%></td></tr>
                <tr><td class="taR B">Six Month Eval:</td><td><%# Eval("sixMonthEval") != null ? Eval("sixMonthEval").ToString() == "Y" ? "Yes" : "No" : "" %></td></tr>
                <tr><td class="taR B">Annual Eval:</td><td><%# Eval("annualEval") != null ? Eval("annualEval").ToString() == "Y" ? "Yes" : "No" : "" %></td></tr>
                <tr><td class="taR B">Job Description:</td><td><%# Eval("jobDescription") != null ? Eval("jobDescription").ToString() == "Y" ? "Yes" : "No" : "" %></td></tr>
                <tr><td class="taR B">Follow Up Date:</td><td><%# Eval("followup_date", "{0:d}")%></td></tr>
                <tr><td class="taR B">Follow Up Note:</td><td><%# Eval("followup_note")%></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
            [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_evaluation"), 
                                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Fiscal Year:</td><td><asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_Date" runat="server" /></td></tr>
                <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' MaxLength="255" TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
                <tr><td class="taR B">Rating:</td><td><asp:TextBox ID="tbRating" runat="server" Text='<%# Bind("rating")%>'></asp:TextBox></td></tr>
                <tr><td class="taR B">Six Month Eval:</td><td><asp:DropDownList ID="ddlSixMonthEval" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Annual Eval:</td><td><asp:DropDownList ID="ddlAnnualEval" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Job Description:</td><td><asp:DropDownList ID="ddlJobDescription" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Follow Up Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_FollowUpDate" runat="server" /></td></tr>
                <tr><td class="taR B">Follow Up Note:</td><td><asp:TextBox ID="tbFollowUpNote" runat="server" Text='<%# Bind("followup_note")%>' MaxLength="255" TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
            <hr />
            <table cellpadding="3">
                <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Fiscal Year:</td><td><asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_Date" runat="server" /></td></tr>
                <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' MaxLength="255" TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
                <tr><td class="taR B">Rating:</td><td><asp:TextBox ID="tbRating" runat="server" Text='<%# Bind("rating")%>'></asp:TextBox></td></tr>
                <tr><td class="taR B">Six Month Eval:</td><td><asp:DropDownList ID="ddlSixMonthEval" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Annual Eval:</td><td><asp:DropDownList ID="ddlAnnualEval" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Job Description:</td><td><asp:DropDownList ID="ddlJobDescription" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Follow Up Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_FollowUpDate" runat="server" /></td></tr>
                <tr><td class="taR B">Follow Up Note:</td><td><asp:TextBox ID="tbFollowUpNote" runat="server" Text='<%# Bind("followup_note")%>' MaxLength="255" TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
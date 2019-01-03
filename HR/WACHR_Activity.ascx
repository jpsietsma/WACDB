<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/HR/WACHR_Activity.ascx.cs" Inherits="HR_WACHR_Activity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>
<asp:FormView ID="fvHR_WACEmployee_Activity" runat="server" Width="100%" 
                    OnModeChanging="fvHR_WACEmployee_Activity_ModeChanging" 
                    OnItemInserting="fvHR_WACEmployee_Activity_ItemInserting" 
                    OnItemUpdating="fvHR_WACEmployee_Activity_ItemUpdating" 
                    OnItemDeleting="fvHR_WACEmployee_Activity_ItemDeleting">
    <ItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
                [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> 
                | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" 
                    OnClientClick="return confirm_delete();"></asp:LinkButton>] 
                    <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_activity"),
                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                <hr />
                <table cellpadding="3">
                                <tr><td class="taR B">Date:</td><td><%# Eval("date", "{0:d}") %></td></tr>
                                <tr><td class="taR B">Note:</td><td><%# Eval("note") %></td></tr>
                              <%--  <tr><td class="taR B">Fiscal Year:</td><td><%# Eval("list_fiscalYear.fiscalYear")%></td></tr>
                                <tr><td class="taR B">Position:</td><td><%# Eval("list_positionWAC.position")%></td></tr>--%>
                </table>
                <hr />
            <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_ActivityCost_Insert" runat="server" 
                    Text="Add an Activity Cost" Font-Bold="true" OnClick="lbHR_WACEmployees_ActivityCost_Insert_Click"></asp:LinkButton>
            </div>
            <asp:GridView ID="gvHR_WACEmployees_ActivityCost" runat="server" SkinID="gvSkin" Width="100%" AutoGenerateColumns="false" 
            OnRowCommand="gvHR_WACEmployees_ActivityCost_RowCommand">
                                <EmptyDataTemplate>No Activity Cost Records</EmptyDataTemplate>
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                    <asp:BoundField HeaderText="Date" DataField="date" DataFormatString="{0:d}" />
                                    <asp:TemplateField HeaderText="Item"><ItemTemplate><%# Eval("list_trainingCost.item") %></ItemTemplate></asp:TemplateField>
                                    <asp:BoundField HeaderText="Cost" DataField="cost" DataFormatString="{0:c}" />
                                    <asp:BoundField HeaderText="Note" DataField="note" />
                                </Columns>
                            </asp:GridView>
            <asp:FormView ID="fvHR_WACEmployee_ActivityCost" runat="server" Width="100%" 
                                OnModeChanging="fvHR_WACEmployee_ActivityCost_ModeChanging" 
                                OnItemInserting="fvHR_WACEmployee_ActivityCost_ItemInserting" 
                                OnItemUpdating="fvHR_WACEmployee_ActivityCost_ItemUpdating" 
                                OnItemDeleting="fvHR_WACEmployee_ActivityCost_ItemDeleting">
                <ItemTemplate>
                    <div class="NestedDivLevel02" style="margin-top:5px;">
                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbHR_WACEmployees_ActivityCost_Close" runat="server" Text="Close" OnClick="lbHR_WACEmployees_ActivityCost_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_activityCost"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                        <hr />
                        <table cellpadding="3">
                            <tr><td class="taR B">Date:</td><td><%# Eval("date", "{0:d}") %></td></tr>
                            <tr><td class="taR B">Item:</td><td><%# Eval("list_trainingCost.item")%></td></tr>
                            <tr><td class="taR B">Cost:</td><td><%# Eval("cost", "{0:c}") %></td></tr>
                            <tr><td class="taR B">Note:</td><td><%# Eval("note") %></td></tr>
                        </table>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                            <div class="NestedDivLevel02" style="margin-top:5px;">
                                <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" 
                                    Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" 
                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>] 
                                    <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(
                                                             Eval("pk_participantWAC_activityCost"), Eval("created"), 
                                                             Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                                <hr />
                                <table cellpadding="3">
                                    <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_Date" runat="server" /></td></tr>
                                    <tr><td class="taR B">Item:</td><td><asp:DropDownList ID="ddlItem" runat="server"></asp:DropDownList></td></tr>
                                    <tr><td class="taR B">Cost:</td><td><asp:TextBox ID="tbCost" runat="server" Text='<%# Bind("cost") %>'></asp:TextBox></td></tr>
                                    <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' MaxLength="255" TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                </table>
                            </div>
                        </EditItemTemplate>
                <InsertItemTemplate>
                                    <div class="NestedDivLevel02" style="margin-top:5px;">
                                        <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_Date" runat="server" /></td></tr>
                                            <tr><td class="taR B">Item:</td><td><asp:DropDownList ID="ddlItem" runat="server"></asp:DropDownList></td></tr>
                                            <tr><td class="taR B">Cost:</td><td><asp:TextBox ID="tbCost" runat="server" Text='<%# Bind("cost") %>'></asp:TextBox></td></tr>
                                            <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' MaxLength="255" TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
                [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> 
                | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]
                 <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_activity"), 
                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                                <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_Date" runat="server" /></td></tr>
                                <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' MaxLength="255" TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                               <%-- <tr><td class="taR B">Fiscal Year:</td><td><asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList></td></tr>
                                <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>--%>
                            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
            [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]
            </div>
            <hr />
            <table cellpadding="3">
                                <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_Date" runat="server" /></td></tr>
                                <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' MaxLength="255" TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
                               <%-- <tr><td class="taR B">Fiscal Year:</td><td><asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList></td></tr>
                                <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>--%>
                            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
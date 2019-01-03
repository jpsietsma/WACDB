<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/HR/WACHR_Training.ascx.cs" Inherits="HR_WACHR_Training" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<asp:FormView ID="fvHR_WACEmployee_Training" runat="server" Width="100%" 
                    OnModeChanging="fvHR_WACEmployee_Training_ModeChanging" 
                    OnItemInserting="fvHR_WACEmployee_Training_ItemInserting" 
                    OnItemUpdating="fvHR_WACEmployee_Training_ItemUpdating" 
                    OnItemDeleting="fvHR_WACEmployee_Training_ItemDeleting">
    <ItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
                [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> 
                | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" 
                    OnClientClick="return confirm_delete();"></asp:LinkButton>] 
                    <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_training"),
                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                <hr />
                <table cellpadding="3">
                    <tr><td class="taR B">Position:</td><td><%# Eval("list_positionWAC.position")%></td></tr>
                    <tr><td class="taR B">Fiscal Year:</td><td><%# Eval("list_fiscalYear.fiscalYear")%></td></tr>
                    <tr><td class="taR B">Training:</td><td><%# Eval("list_trainingReqd.title")%></td></tr>
                    <tr><td class="taR B">Training Completed:</td><td><%# Eval("completed_date", "{0:d}") %></td></tr>
                    <tr><td class="taR B"><asp:Image ImageAlign="Bottom" ID="imgAdvisory" runat="server" 
                        ImageUrl="~/images/exclamation.png" />&nbsp;Training Expires:</td><td><%# Eval("expiration_date", "{0:d}") %></td></tr>
                    <tr><td class="taR B">Note:</td><td><%# Eval("note") %></td></tr>
                </table>
                <hr />
            <div style="margin-bottom:5px;"><asp:LinkButton ID="lbHR_WACEmployees_TrainingCost_Insert" runat="server" 
                    Text="Add a Training Cost" Font-Bold="true" OnClick="lbHR_WACEmployees_TrainingCost_Insert_Click"></asp:LinkButton>
            </div>
    <%--<tr valign="top"><td class="B taR"><asp:Image ImageAlign="Bottom" ID="imgAdvisory2" runat="server" ImageUrl="~/images/exclamation.png" />&nbsp;Completed Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("completed_date")) %></td></tr>--%>
            <asp:GridView ID="gvHR_WACEmployees_TrainingCost" runat="server" SkinID="gvSkin" Width="100%" AutoGenerateColumns="false" 
                    OnRowCommand="gvHR_WACEmployees_TrainingCost_RowCommand">
                <EmptyDataTemplate>No Training Cost Records</EmptyDataTemplate>
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="View" />
                    <asp:TemplateField HeaderText="Type of Cost"><ItemTemplate><%# Eval("list_trainingCost.item")%></ItemTemplate></asp:TemplateField>
                    <asp:BoundField HeaderText="Date" DataField="date" DataFormatString="{0:d}" />
                    <asp:BoundField HeaderText="Cost" DataField="cost" DataFormatString="{0:c}" />
                    <asp:BoundField HeaderText="Note" DataField="note" />
                </Columns>
            </asp:GridView>
            <asp:FormView ID="fvHR_WACEmployee_TrainingCost" runat="server" Width="100%" 
                                OnModeChanging="fvHR_WACEmployee_TrainingCost_ModeChanging" 
                                OnItemInserting="fvHR_WACEmployee_TrainingCost_ItemInserting" 
                                OnItemUpdating="fvHR_WACEmployee_TrainingCost_ItemUpdating" 
                                OnItemDeleting="fvHR_WACEmployee_TrainingCost_ItemDeleting">
                <ItemTemplate>
                    <div class="NestedDivLevel02" style="margin-top:5px;">
                        <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbHR_WACEmployees_TrainingCost_Close" runat="server" Text="Close" OnClick="lbHR_WACEmployees_TrainingCost_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_TrainingCost"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                        <hr />
                        <table cellpadding="3">
                            <tr><td class="taR B">Type of Cost:</td><td><%# Eval("list_trainingCost.item")%></td></tr>
                            <tr><td class="taR B">Date:</td><td><%# Eval("date", "{0:d}") %></td></tr>
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
                                                        Eval("pk_participantWAC_trainingCost"), Eval("created"), 
                                                        Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                        <hr />
                        <table cellpadding="3">
                            <tr><td class="taR B">Item:</td><td><asp:DropDownList ID="ddlTrainingCostCode" runat="server"></asp:DropDownList></td></tr>
                            <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_TrCostDate" runat="server" /></td></tr>                           
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
                             <tr><td class="taR B">Item:</td><td><asp:DropDownList ID="ddlTrainingCostCode" runat="server"></asp:DropDownList></td></tr>
                            <tr><td class="taR B">Date:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_TrCostDate" runat="server" /></td></tr>                           
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
                 <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_training"), 
                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                    <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>
                    <tr><td class="taR B">Fiscal Year:</td><td><asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList></td></tr>
                    <tr><td class="taR B">Training:</td><td><asp:DropDownList ID="ddlTrainingReqd" runat="server"></asp:DropDownList></td></tr>
                    <tr><td class="taR B">Training Completed:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_CompDate" runat="server" /></td></tr>
                    <tr><td class="taR B">Training Expires:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_ExpDate" runat="server" /></td></tr>
                    <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' MaxLength="255" TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
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
                    <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>
                    <tr><td class="taR B">Fiscal Year:</td><td><asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList></td></tr>
                    <tr><td class="taR B">Training:</td><td><asp:DropDownList ID="ddlTrainingReqd" runat="server"></asp:DropDownList></td></tr>
                    <tr><td class="taR B">Training Completed:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_CompDate" runat="server" /></td></tr>
                    <tr><td class="taR B">Training Expires:</td><td><uc1:AjaxCalendar ID="AjaxCalendar_ExpDate" runat="server" /></td></tr>
                    <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' MaxLength="255" TextMode="MultiLine" Width="400px" Rows="2"></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/HR/WACHR_Note.ascx.cs" Inherits="HR_WACHR_Note" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<asp:FormView ID="fvHR_WACEmployee_Note" runat="server" Width="100%" OnModeChanging="fvHR_WACEmployee_Note_ModeChanging" 
                    OnItemInserting="fvHR_WACEmployee_Note_ItemInserting" OnItemUpdating="fvHR_WACEmployee_Note_ItemUpdating" 
                    OnItemDeleting="fvHR_WACEmployee_Note_ItemDeleting">
    <EmptyDataTemplate>No Note Records</EmptyDataTemplate>
    <ItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
            [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" 
            OnClientClick="return confirm_delete();"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_note"), 
                                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr><td class="taR B">Position:</td><td><%# Eval("fk_positionWAC_code")%></td></tr>
                <tr><td class="taR B">Note:</td><td><%# Eval("note")%></td></tr>             
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span> 
            [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]
                <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_note"), 
                                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' MaxLength="255" 
                    TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
       </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("participantWAC.participant1.fullname_LF_dnd")%></span>
            [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
            <hr />
            <table cellpadding="3">
                <tr><td class="taR B">Position:</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>
                <tr><td class="taR B">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' MaxLength="255" 
                    TextMode="MultiLine" Rows="2" Width="400px"></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>

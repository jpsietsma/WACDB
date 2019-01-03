<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACHR_Phone.ascx.cs" Inherits="HR_WACHR_Phone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>
<asp:FormView ID="fvHR_WACEmployee_Phone" runat="server" Width="100%" OnModeChanging="fvHR_WACEmployee_Phone_ModeChanging" 
     OnItemInserting="fvHR_WACEmployee_Phone_ItemInserting" OnItemUpdating="fvHR_WACEmployee_Phone_ItemUpdating" 
     OnItemDeleting="fvHR_WACEmployee_Phone_ItemDeleting">
    <EmptyDataTemplate>No Phone Records</EmptyDataTemplate>
    <ItemTemplate>
        <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("fullname_LF_dnd")%></span> 
            [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" 
            OnClientClick="return confirm_delete();"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_phone"), 
                                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr><td class="taR B">Phone number:</td><td><%# Eval("PhoneFormattedHR") %></td></tr>
                <tr><td class="taR B">Public:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("publicUsage"))%></td></tr>
                <tr><td class="taR B">Emergency:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("emergency"))%></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
    <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("fullname_LF_dnd")%></span> 
            [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]
                <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantWAC_phone"), 
                                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />  
        <table cellpadding="3">
                <tr><td class="taR B">Phone number:</td><td><%# Eval("PhoneFormattedHR") %></td></tr>
                <tr><td class="taR B">Public:</td><td><asp:RadioButtonList ID="rblPublicNumber" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Text="Yes" Value="Y" ></asp:ListItem><asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList></td></tr>
                <tr><td class="taR B">Emergency:</td><td><asp:RadioButtonList ID="rblEmergencyNumber" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                </asp:RadioButtonList></td></tr>
            </table>
    </div>
    </EditItemTemplate>
    <InsertItemTemplate>
    <div class="NestedDivLevel01">
            <div><span style="font-size:medium; font-weight:bold;"><%# Eval("fullname_LF_dnd")%></span>
            [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
            <hr />
      <table cellpadding="3">
                <tr><td class="taR B">Phone number:</td><td><asp:DropDownList ID="ddlPhone" runat="server" 
                    OnSelectedIndexChanged="ddlPhone_SelectedIndexChanged" ></asp:DropDownList></td></tr>
                <tr><td class="taR B">Public:</td><td><asp:RadioButtonList ID="rblPublicNumber" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Text="Yes" Value="Y" ></asp:ListItem><asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList></td></tr>
                <tr><td class="taR B">Emergency:</td><td><asp:RadioButtonList ID="rblEmergencyNumber" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                </asp:RadioButtonList></td></tr>
            </table>
    </div> 
    </InsertItemTemplate>
</asp:FormView>

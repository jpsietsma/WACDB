<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_Wfp3Invoice.ascx.cs" Inherits="AG_WACAG_Wfp3Invoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<asp:FormView ID="fvAg_WFP3_Invoice" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_Invoice_ItemCommand" >
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" 
            OnClientClick="return confirm_delete();"></asp:LinkButton> 
            | <asp:LinkButton ID="lbAg_WFP3_Invoice_Close" runat="server" CommandName="CloseForm" Text="Close" >
            </asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(
                        Eval("pk_form_wfp3_invoice"), Eval("created"), Eval("created_by"), Eval("modified"), 
                        Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td></tr>
                <tr valign="top"><td class="B taR">Invoice Number:</td><td><%# Eval("invoice_nbr") %></td></tr>
                <tr valign="top"><td class="B taR">Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt")) %></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>
            ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_invoice"), 
                                       Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalInvoiceDate" runat="server" Text=<%# Bind("date") %> /></td></tr>
                <tr valign="top"><td class="B taR">Invoice Number:</td><td><asp:TextBox ID="tbInvoiceNumber" runat="server" Text='<%# Bind("invoice_nbr")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amt")%>'></asp:TextBox></td></tr>
            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>
            ]</div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalInvoiceDate" runat="server" Text=<%# Bind("date") %> /></td></tr>
                <tr valign="top"><td class="B taR">Invoice Number:</td><td><asp:TextBox ID="tbInvoiceNumber" runat="server" Text='<%# Bind("invoice_nbr")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amt")%>'></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
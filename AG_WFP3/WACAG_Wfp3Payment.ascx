<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/AG_WFP3/WACAG_WFP3Payment.ascx.cs" Inherits="AG_WACAG_Wfp3Payment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<asp:FormView ID="fvAg_WFP3_Payment" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_Payment_ItemCommand" >
    <ItemTemplate>
    <hr />
        <div class="NestedDivLevel02A">
    <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
    | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" 
        OnClientClick="return confirm_delete();"></asp:LinkButton> 
        | <asp:LinkButton ID="lbAg_WFP3_Payment_Close" runat="server" CommandName="CloseForm" Text="Close" ></asp:LinkButton>] 
        <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_payment"), 
                                 Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
    <hr />
    <table cellpadding="3">
        <tr valign="top"><td class="B taR">Payee:</td><td><%# Eval("participant.fullname_LF_dnd")%></td></tr>
        <tr valign="top"><td class="B taR">Invoice:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_WFP3_Invoice(Eval("fk_form_wfp3_invoice")) %></td></tr>
        <tr valign="top"><td class="B taR">Date:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td></tr>
        <tr valign="top"><td class="B taR">Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt"))%></td></tr>
        <tr valign="top"><td class="B taR">Check Number:</td><td><%# Eval("check_nbr")%></td></tr>
        <tr valign="top"><td class="B taR">Encumbrance:</td><td><%# Eval("list_encumbrance.encumbrance")%></td></tr>
 <%--       <tr valign="top"><td class="B taR">Is Contractor:</td><td><%# Eval("is_contractor")%></td></tr>--%>
<%--        <tr valign="top"><td class="B taR">Flood 2006:</td><td><%# Eval("flood_2006")%></td></tr>--%>
        <tr valign="top"><td class="B taR">Note:</td><td><%# Eval("note")%></td></tr>
        <tr valign="top"><td class="B U taR fsXS"><br />LEGACY</td><td></td></tr>
        <tr valign="top"><td class="B taR fsXS">BMP Number New:</td><td class="fsXS"><%# Eval("bmp_nbr_new_legacy")%></td></tr>
        <tr valign="top"><td class="B taR fsXS">Contractor ID:</td><td class="fsXS"><%# Eval("contractor_id_legacy")%></td></tr>
        <tr valign="top"><td class="B taR fsXS">Component Number:</td><td class="fsXS"><%# Eval("component_nbr_legacy")%></td></tr>
    </table>
    <hr />
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_payment"), Eval("created"), 
                                     Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Payee:</td><td><asp:DropDownList ID="ddlPayee" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Invoice:</td><td><asp:DropDownList ID="ddlInvoice" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalPaymentDate" runat="server" Text=<%# Bind("date") %> /></td></tr>
                <tr valign="top"><td class="B taR">Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt"))%></td></tr>
                <tr valign="top"><td class="B taR">Check Number:</td><td><asp:TextBox ID="tbCheckNumber" runat="server" Text='<%# Bind("check_nbr") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Encumbrance:</td><td><asp:DropDownList ID="ddlEncumbrance" runat="server"></asp:DropDownList></td></tr>
              <%--  <tr valign="top"><td class="B taR">Is Contractor:</td><td><asp:DropDownList ID="ddlIsContractor" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Flood 2006:</td><td><asp:DropDownList ID="ddlFlood2006" runat="server"></asp:DropDownList></td></tr>--%>
                <tr valign="top"><td class="B taR">Note</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>]</div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Payee:</td><td><asp:DropDownList ID="ddlPayee" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Invoice:</td><td><asp:DropDownList ID="ddlInvoice" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Date:</td><td><uc1:AjaxCalendar ID="tbCalPaymentDate" runat="server" Text=<%# Bind("date") %>  /></td></tr>
                <tr valign="top"><td class="B taR">Check Number:</td><td><asp:TextBox ID="tbCheckNumber" runat="server" Text='<%# Bind("check_nbr") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Encumbrance:</td><td><asp:DropDownList ID="ddlEncumbrance" runat="server"></asp:DropDownList></td></tr>
               <%-- <tr valign="top"><td class="B taR">Is Contractor:</td><td><asp:DropDownList ID="ddlIsContractor" runat="server"></asp:DropDownList></td></tr>--%>
                <tr valign="top"><td class="B taR">Note</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
    </InsertItemTemplate>
</asp:FormView>
<div class="NestedDivLevel03">
    <div style="margin-bottom:5px;"><span class="fsM B">Payment BMPs</span> >> <asp:LinkButton ID="lbAg_WFP3_PaymentBMP_Add" 
        runat="server" Text="Add a Payment BMP"  OnClick="lbAg_WFP3_PaymentBMP_AddClicked" > </asp:LinkButton></div>
    <div style="margin-left:5px;">
        <asp:ListView ID="lvAg_WFP3_PaymentBMPs" runat="server" > 
            <EmptyDataTemplate><div class="I">No Payment BMP Records</div></EmptyDataTemplate>
            <LayoutTemplate>
                <table cellpadding="3" rules="cols">
                    <tr valign="top">
                        <td></td>
                        <td class="B U">BMP</td>
                        <td class="B U">BMP Practice</td>
                        <td class="B U">Payment Status</td>
                        <td class="B U">WAC Amount</td>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr valign="top">
                    <td>[<asp:LinkButton ID="lbAg_WFP3_PaymentBMP_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_form_wfp3_paymentBMP") %>' 
                         OnClick="lbAg_WFP3_PaymentBMP_ViewClicked" ></asp:LinkButton>]</td> 
                    <td><%# Eval("form_wfp3_bmp.bmp_ag.bmp_nbr") %> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td>
                    <td><%# WACGlobal_Methods.SpecialText_Agriculture_BMPPractice(Eval("list_BMPPractice.pk_bmpPractice_code"), Eval("list_BMPPractice.practice"), Eval("list_BMPPractice.ABC"), false)%></td>
                    <td><%# Eval("list_paymentStatus.status")%></td>
                    <td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt"))%></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>
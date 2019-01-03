<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/AG_WFP3/WACAG_WFP3PaymentBMP.ascx.cs" Inherits="AG_WACAG_Wfp3PaymentBMP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<asp:FormView ID="fvAg_WFP3_PaymentBMP" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_PaymentBMP_ItemCommand" >
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel03A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
                | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" 
                OnClientClick="return confirm_delete();"></asp:LinkButton> 
                | <asp:LinkButton ID="lbAg_WFP3_PaymentBMP_Close" runat="server" CommandName="CloseForm" Text="Close" ></asp:LinkButton>
                ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_paymentBMP"), 
                                            Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <asp:Literal ID="litAgriculture_WFP3_PaymentBMP_PercentCalc_Grid" runat="server"></asp:Literal>
            <hr />
            <table class="tp3">
                <tr class="taT"><td class="B taR">BMP:</td><td><%# Eval("form_wfp3_bmp.bmp_ag.CompositBmpNum") %> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td></tr>
                <tr class="taT"><td class="B taR">BMP Practice:</td><td><%# WACGlobal_Methods.SpecialText_Agriculture_BMPPractice(Eval("list_BMPPractice.pk_bmpPractice_code"), Eval("list_BMPPractice.practice"), Eval("list_BMPPractice.ABC"), false)%></td></tr>
                <tr class="taT"><td class="B taR">Payment Status:</td><td><%# Eval("list_paymentStatus.status")%></td></tr>
                <tr class="taT"><td class="B taR">WAC Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt"))%></td></tr>
                <tr class="taT"><td class="B taR">Funding Agency Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt_agencyFunding"))%></td></tr>
                <tr class="taT"><td class="B taR">Funding Agency:</td><td><%# Eval("list_agencyFunding.agency")%></td></tr>
                <tr class="taT"><td class="B taR">Reimbursement:</td><td><%#Eval("reimbursement") %></td></tr>
                <tr class="taT"><td class="B taR">NMCP Purchase:</td><td><%#Eval("purchaseNMCP") %></td></tr>
                <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note")%></td></tr>
                <tr class="taT"><td class="B U taR fsXS"><br />LEGACY</td><td></td></tr>
                <tr class="taT"><td class="B taR fsXS">Component:</td><td class="fsXS"><%# Eval("component_legacy")%></td></tr>
                <tr class="taT"><td class="B taR fsXS">New BMP Number:</td><td class="fsXS"><%# Eval("bmp_nbr_new_legacy")%></td></tr>
            </table>
        </div>             
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel03A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>
            ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_paymentBMP"),
                                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%>

              </span>
            </div>
            <hr />
            <table class="tp3"> 
                 <tr class="taT"><td class="B taR">BMP:</td><td><%# Eval("form_wfp3_bmp.bmp_ag.CompositBmpNum") %> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td></tr>
                <tr class="taT"><td class="B taR">BMP Practice:</td><td><asp:DropDownList ID="ddlBMPPractice" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Payment Status:</td><td><asp:DropDownList ID="ddlPaymentStatus" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">WAC Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amt") %>'></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Funding Agency Amount:</td><td><asp:TextBox ID="tbAmountFundingAgency" runat="server" Text='<%# Bind("amt_agencyFunding") %>'></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Funding Agency:</td><td><asp:DropDownList ID="ddlFundingAgency" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Reimbursement:</td><td><asp:DropDownList ID="ddlReimbursementYN" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">NMCP Purchase:</td><td><asp:TextBox ID="tbNMCPPurchase" runat="server" Text='<%# Bind("purchaseNMCP") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <hr />
        <div class="NestedDivLevel03A">
            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>]

            </div>
            <hr />
            <table class="tp3">
                <tr class="taT">
                    <td class="B taR">BMP:</td><td><asp:DropDownList ID="ddlBMP" runat="server" OnSelectedIndexChanged="ddlAg_WFP3_PaymentBMP_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                </tr>
                <tr class="taT"><td class="B taR">BMP Practice:</td><td><asp:DropDownList ID="ddlBMPPractice" runat="server"></asp:DropDownList></td></tr>                                                                       
                <tr class="taT"><td class="B taR">Payment Status:</td><td><asp:DropDownList ID="ddlPaymentStatus" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">WAC Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amt") %>'></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Funding Agency Amount:</td><td><asp:TextBox ID="tbAmountFundingAgency" runat="server" Text='<%# Bind("amt_agencyFunding") %>'></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Funding Agency:</td><td><asp:DropDownList ID="ddlFundingAgency" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Reimbursement:</td><td><asp:DropDownList ID="ddlReimbursementYN" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">NMCP Purchase:</td><td><asp:TextBox ID="tbNMCPPurchase" runat="server" Text='<%# Bind("purchaseNMCP") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_Wfp3Encumbrance.ascx.cs" Inherits="AG_WACAG_Wfp3Encumbrance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

<asp:FormView ID="fvAg_WFP3_Encumbrance" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_Encumbrance_ItemCommand" >
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete"
             OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbAg_WFP3_Encumbrance_Close" runat="server" 
             Text="Close" OnClick="lbAg_WFP3_Encumbrance_Close_Click"></asp:LinkButton>
             ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_encumbrance"), 
                                        Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table class="tp3">
                <tr class="taT"><td class="B taR">Encumbrance:</td><td><%# Eval("list_encumbrance.encumbrance")%></td></tr>
                <tr class="taT"><td class="B taR">Encumbrance ID:</td><td><%# Eval("encumbrance_id")%></td></tr>
                <tr class="taT"><td class="B taR">Issued:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("date")) %></td></tr>
                <tr class="taT"><td class="B taR">Type:</td><td><%# Eval("list_encumbranceType.type")%></td></tr>
                <tr class="taT"><td class="B taR">Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amt"))%></td></tr>
                <tr class="taT"><td class="B taR">Approved:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("approved")) %></td></tr>
                <tr class="taT"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_encumbrance"), 
                                     Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table class="tp3">
                <tr class="taT"><td class="B taR">Encumbrance:</td><td><asp:DropDownList ID="ddlEncumbrance" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Encumbrance ID:</td><td><%# Eval("encumbrance_id")%></td></tr>
                <tr class="taT"><td class="B taR">Issued:</td><td><uc1:AjaxCalendar ID="tbCalEncumbranceDate" runat="server" Text=<%# Bind("date") %> /></td></tr>
                <tr class="taT"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amt")%>'></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Approved:</td><td><uc1:AjaxCalendar ID="tbCalEncumbranceApprovedDate" runat="server" Text=<%# Bind("approved") %> /></td></tr>
                <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
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
            <table class="tp3">
                <tr class="taT"><td class="B taR">Encumbrance:</td><td><asp:DropDownList ID="ddlEncumbrance" runat="server"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Issued:</td><td><uc1:AjaxCalendar ID="tbCalEncumbranceDate" runat="server" Text=<%# Bind("date") %> /></td></tr>
                <tr class="taT"><td class="B taR">Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amt")%>'></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
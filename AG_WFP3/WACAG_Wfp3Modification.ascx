<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_Wfp3Modification.ascx.cs" Inherits="AG_WACAG_Wfp3Modification" %>
 <asp:FormView ID="fvAg_WFP3_Modification" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_Modification_ItemCommand" > 
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> 
            | <asp:LinkButton ID="lbAg_WFP3_Modification_Close" runat="server" CommandName="CloseForm" Text="Close" ></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_modification"), Eval("created"), 
                                     Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">BMP:</td><td><%# Eval("form_wfp3_bmp.bmp_ag.bmp_nbr") %> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td></tr>
                <tr valign="top"><td class="B taR">Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("amount"))%></td></tr>
                <tr valign="top"><td class="B taR">Note:</td><td><%# Eval("note")%></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_modification"), Eval("created"),
                                     Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">BMP:</td><td><asp:DropDownList ID="ddlBMP" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox></td></tr>
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
                <tr valign="top"><td class="B taR">BMP:</td><td><asp:DropDownList ID="ddlBMP" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Amount:</td><td><asp:TextBox ID="tbAmount" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Note</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
    </InsertItemTemplate>
</asp:FormView>
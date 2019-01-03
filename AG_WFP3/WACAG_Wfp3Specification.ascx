<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_Wfp3Specification.ascx.cs" Inherits="AG_WACAG_Wfp3Specification" %>

 <asp:FormView ID="fvAg_WFP3_Specification" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_Specification_ItemCommand" >
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" 
                OnClientClick="return confirm_delete();"></asp:LinkButton> 
            | <asp:LinkButton ID="lbAg_WFP3_Specification_Close" runat="server" CommandName="CloseForm" Text="Close"></asp:LinkButton> 
            ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_specification"), 
                                       Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">BMP:</td><td><%# Eval("form_wfp3_bmp.bmp_ag.bmp_nbr")%> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td></tr>
                <tr valign="top"><td class="B taR">Practice:</td><td><%# Eval("fk_bmpPractice_code")%> <%# Eval("list_bmpPractice.practice") %></td></tr>
                <tr valign="top"><td class="B taR">Bid Required:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("bid_reqd")) %></td></tr>
                <tr valign="top"><td class="B taR">List Position:</td><td><%# Eval("sort_position") %></td></tr>
                <tr valign="top"><td class="B taR">Bid:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("bid")) %></td></tr>
                <tr valign="top"><td class="B taR">Units:</td><td><%# Eval("units") %></td></tr>
                <tr valign="top"><td class="B taR">Cost Per Unit:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("cost_per_unit")) %></td></tr>
                <tr valign="top"><td class="B taR">ABC:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("ABC")) %></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>] 
            <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_specification"), 
                                     Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">BMP:</td><td><%# Eval("form_wfp3_bmp.bmp_ag.bmp_nbr")%> <%# Eval("form_wfp3_bmp.bmp_ag.description")%></td></tr>
                <tr valign="top"><td class="B taR">Practice:</td><td><asp:DropDownList ID="ddlPractice" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Bid Required:</td><td><asp:DropDownList ID="ddlBidRequired" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">List Position:</td><td><asp:TextBox ID="tbSortPosition" runat="server" Text='<%# Bind("sort_position") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Bid:</td><td><asp:TextBox ID="tbBid" runat="server" Text='<%# Bind("bid") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Units:</td><td><asp:TextBox ID="tUnits" runat="server" Text='<%# Bind("units") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">ABC:</td><td><asp:TextBox ID="tbABC" runat="server" Text='<%# Bind("ABC") %>'></asp:TextBox></td></tr>                                                           
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
                <tr valign="top"><td class="B taR">Practice:</td><td><asp:DropDownList ID="ddlPractice" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Bid Required:</td><td><asp:DropDownList ID="ddlBidRequired" runat="server"></asp:DropDownList></td></tr>  
            </table>
    </InsertItemTemplate>
</asp:FormView>
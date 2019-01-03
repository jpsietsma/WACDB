<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/AG_WFP3/WACAG_WFP3Bid.ascx.cs" Inherits="AG_WACAG_Wfp3Bid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register Src="~/customcontrols/ajaxcalendar.ascx" TagPrefix="uc1" TagName="AjaxCalendar" %>

 <asp:FormView ID="fvAg_WFP3_Bid" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_Bid_ItemCommand" > 
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" 
                OnClientClick="return confirm_delete();"></asp:LinkButton> 
                | <asp:LinkButton ID="lbAg_WFP3_Bid_Close" runat="server" CommandName="CloseForm" Text="Close" ></asp:LinkButton>
                ]<span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_bid"), 
                                          Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Contractor:</td><td><%# Eval("participant.fullname_LF_dnd")%></td></tr>
                <tr valign="top"><td class="B taR">Bid Amount:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("bid_amt"))%></td></tr>
                                                         
                <tr valign="top"><td class="B taR">Bid Awarded:</td><td><%# WACGlobal_Methods.Format_Global_Date(Eval("bid_awarded")) %></td></tr>
                <tr valign="top"><td class="B taR">Modification Amount:</td><td><%# Eval("modification_amt") %></td></tr>
                <tr valign="top"><td class="B taR">Bid Rank:</td><td><%# Eval("bid_rank") %></td></tr>
                <tr valign="top"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
                <tr valign="top"><td class="B U taR fsXS"><br />LEGACY</td><td></td></tr>
                <tr valign="top"><td class="B taR fsXS">BMP Number:</td><td class="fsXS"><%# Eval("bid_nbr_legacy")%></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>
            ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_bid"), 
                                       Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr /><div>
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Contractor:</td><td><asp:DropDownList ID="ddlContractor" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Bid Amount:</td><td><asp:TextBox ID="tbBidAmount" runat="server" Text='<%# Bind("bid_amt")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Bid Awarded:</td><td><uc1:AjaxCalendar ID="tbCalBidAwardDate" runat="server" Text='<%# Bind("bid_awarded") %>'/></td></tr>
                <tr valign="top"><td class="B taR">Modification Amount:</td><td><asp:TextBox ID="tbModificationAmount" runat="server" Text='<%# Bind("modification_amt")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Bid Rank:</td><td><asp:TextBox ID="tbBidRank" runat="server" Text='<%# Bind("bid_rank")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table></div>
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
                <tr valign="top"><td class="B taR">Contractor:</td><td><asp:DropDownList ID="ddlContractor" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Bid Amount:</td><td><asp:TextBox ID="tbBidAmount" runat="server" Text='<%# Bind("bid_amt")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Bid Awarded:</td><td><uc1:AjaxCalendar ID="tbCalBidAwardDate" runat="server" Text=<%# Bind("bid_awarded") %>/></td></tr>
                <tr valign="top"><td class="B taR">Modification Amount:</td><td><asp:TextBox ID="tbModificationAmount" runat="server" Text='<%# Bind("modification_amt")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Bid Rank:</td><td><asp:TextBox ID="tbBidRank" runat="server" Text='<%# Bind("bid_rank")%>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note")%>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
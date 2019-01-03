<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/AG_WFP3/WACAG_WFP3Tech.ascx.cs" Inherits="AG_WACAG_Wfp3Tech" %>

<asp:FormView ID="fvAg_WFP3_Technician" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_Technician_ItemCommand" > 
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit"></asp:LinkButton> 
                |<asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" 
                OnClientClick="return confirm_delete();"></asp:LinkButton> | 
                <asp:LinkButton ID="lbAg_WFP3_Tech_Close" runat="server" CommandName="CloseForm" Text="Close" ></asp:LinkButton>] 
                <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_tech"), 
                                     Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Type:</td><td><%# Eval("list_designerEngineerType.grouping")%></td></tr>
                <tr valign="top"><td class="B taR">Technician:</td><td><%# Eval("list_designerEngineer.designerEngineer")%></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>
            ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_tech"), 
                                       Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlAg_WFP3_Technician_Type" runat="server" 
                OnSelectedIndexChanged="ddlAg_WFP3_Technician_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Technician:</td><td><asp:DropDownList ID="ddlTechnician" runat="server"></asp:DropDownList></td></tr>
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
                <tr valign="top"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlAg_WFP3_Technician_Type" runat="server" 
                OnSelectedIndexChanged="ddlAg_WFP3_Technician_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Technician:</td><td><asp:DropDownList ID="ddlTechnician" runat="server"></asp:DropDownList></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
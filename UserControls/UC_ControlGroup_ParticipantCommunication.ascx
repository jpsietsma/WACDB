<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_ControlGroup_ParticipantCommunication.ascx.cs" Inherits="UC_ControlGroup_ParticipantCommunication" %>
<%@ Register src="~/UserControls/UC_Communication_EditInsert.ascx" TagName="UC_Communication_EditInsert" TagPrefix="uc1" %>
<div style="margin-bottom:5px;"><span class="B fsM">Participant Communications</span>  [<asp:LinkButton ID="lbParticipant_Communication_Add" runat="server" Text="Add Participant Communication" OnClick="lbParticipant_Communication_Add_Click"></asp:LinkButton>]</div>
<asp:HiddenField ID="hfPK_Participant" runat="server" Value='<%# Eval("pk_participant") %>' />
<asp:ListView ID="lvParticipant_Communications" runat="server" DataSource='<%# Eval("participantCommunications") %>'>
    <EmptyDataTemplate><div class="I">No Participant Communication Records</div></EmptyDataTemplate>
    <LayoutTemplate>
        <table cellpadding="5" rules="cols">
            <tr valign="top">
                <td class="B U"></td>
                <td class="B U">Type</td>
                <td class="B U">Usage</td>
                <td class="B U">Phone Number</td>
                <td class="B U">Extension</td>
            </tr>
            <tr id="itemPlaceholder" runat="server"></tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr valign="top">
            <td>[<asp:LinkButton ID="lbView" runat="server" Text="View" CommandArgument='<%# Eval("pk_participantCommunication") %>' OnClick="lbView_Click"></asp:LinkButton>]</td>
            <td><%# Eval("list_communicationType.type") %></td>
            <td><%# Eval("list_communicationUsage.usage") %></td>
            <td><%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("communication.areacode"), Eval("communication.number")) %></td>
            <td><%# Eval("extension") %></td>
        </tr>
    </ItemTemplate>
</asp:ListView>
<asp:FormView ID="fvParticipant_Communication" runat="server" Width="100%" OnModeChanging="fvParticipant_Communication_ModeChanging" OnItemUpdating="fvParticipant_Communication_ItemUpdating" OnItemInserting="fvParticipant_Communication_ItemInserting" OnItemDeleting="fvParticipant_Communication_ItemDeleting">
    <ItemTemplate>
        <div>
            <hr />
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbParticipant_Communication_Close" runat="server" Text="Close" OnClick="lbParticipant_Communication_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantCommunication"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Type:</td><td><%# Eval("list_communicationType.type") %></td></tr>
                <tr valign="top"><td class="B taR">Usage:</td><td><%# Eval("list_communicationUsage.usage") %></td></tr>
                <tr valign="top"><td class="B taR">Number:</td><td><%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("communication.areacode"), Eval("communication.number"))%></td></tr>
                <tr valign="top"><td class="B taR">Extension:</td><td><%# Eval("extension") %></td></tr>
                <tr valign="top"><td class="B taR">Note:</td><td><%# Eval("note") %></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <div>
            <hr />
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantCommunication"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlParticipant_Communication_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Usage:</td><td><asp:DropDownList ID="ddlUsage" runat="server"></asp:DropDownList></td></tr>
                <%--<tr valign="top"><td class="B taR">Phone:</td><td><uc1:UC_Communication_EditInsert ID="UC_Communication_EditInsert_Phone" runat="server" /></td></tr>--%>
                <tr valign="top"><td class="B taR">Area Code:</td><td><asp:TextBox ID="tbAreaCode" runat="server" Text='<%# Eval("communication.areacode") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbPhoneNumber" runat="server" Text='<%# Eval("communication.number") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Extension:</td><td><asp:TextBox ID="tbExtension" runat="server" Text='<%# Bind("extension") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Note:</td><td><asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td></tr>
            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div>
            <hr />
            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Type:</td><td><asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlParticipant_Communication_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">Usage:</td><td><asp:DropDownList ID="ddlUsage" runat="server"></asp:DropDownList></td></tr>
                <%--<tr valign="top"><td class="B taR">Phone:</td><td><uc1:UC_Communication_EditInsert ID="UC_Communication_EditInsert_Phone" runat="server" /></td></tr>--%>
                <tr valign="top"><td class="B taR">Area Code:</td><td><asp:TextBox ID="tbAreaCode" runat="server" Text='<%# Eval("communication.areacode") %>'></asp:TextBox></td></tr>
                <tr valign="top"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbPhoneNumber" runat="server" Text='<%# Eval("communication.number") %>'></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>

<%--<asp:GridView ID="gv" runat="server" DataSource='<%# Eval("participantCommunications") %>' AutoGenerateColumns="false" Width="100%" OnRowEditing="gv_RowEditing">
    <Columns>
        <asp:CommandField ShowEditButton="true" />
        <asp:BoundField HeaderText="PK" DataField="pk_participantCommunication" />
        <asp:TemplateField HeaderText="Type">
            <ItemTemplate><%# Eval("list_communicationType.type") %></ItemTemplate>
            <EditItemTemplate><asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlParticipant_Communication_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></EditItemTemplate>
            <InsertItemTemplate><asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlParticipant_Communication_Type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></InsertItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Usage">
            <ItemTemplate><%# Eval("list_communicationUsage.usage") %></ItemTemplate>
            <EditItemTemplate></EditItemTemplate>
            <InsertItemTemplate></InsertItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Phone Number">
            <ItemTemplate><%# WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(Eval("communication.areacode"), Eval("communication.number"))%></ItemTemplate>
            <EditItemTemplate></EditItemTemplate>
            <InsertItemTemplate></InsertItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Extension" DataField="extension" />
    </Columns>
</asp:GridView>--%>
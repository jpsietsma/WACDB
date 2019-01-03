<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_ControlGroup_ParticipantProperty.ascx.cs" Inherits="UC_ControlGroup_ParticipantProperty" %>
<%@ Register src="~/UserControls/UC_Property_EditInsert.ascx" tagname="UC_Property_EditInsert" tagprefix="uc1" %>
<div style="margin-bottom:5px;"><span class="B fsM">Participant Properties</span>  [<asp:LinkButton ID="lbParticipant_Property_Add" runat="server" Text="Add Participant Property" OnClick="lbParticipant_Property_Add_Click"></asp:LinkButton>]</div>
<asp:HiddenField ID="hfPK_Participant" runat="server" Value='<%# Eval("pk_participant") %>' />
<asp:ListView ID="lvParticipant_Properties" runat="server" DataSource='<%# Eval("participantProperties") %>'>
    <EmptyDataTemplate><div class="I">No Participant Property Records</div></EmptyDataTemplate>
    <LayoutTemplate>
        <table cellpadding="5" rules="cols">
            <tr valign="top">
                <td class="B U"></td>
                <td class="B U">Master</td>
                <td class="B U">Address</td>
            </tr>
            <tr id="itemPlaceholder" runat="server"></tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr valign="top">
            <td>[<asp:LinkButton ID="lbView" runat="server" Text="View" CommandArgument='<%# Eval("pk_participantProperty") %>' OnClick="lbView_Click"></asp:LinkButton>]</td>
            <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master")) %></td>
            <td><%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), Eval("property.list_address2Type.longname"), Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), true)%></td>
        </tr>
    </ItemTemplate>
</asp:ListView>
<asp:FormView ID="fvParticipant_Property" runat="server" Width="100%" OnModeChanging="fvParticipant_Property_ModeChanging" OnItemUpdating="fvParticipant_Property_ItemUpdating" OnItemInserting="fvParticipant_Property_ItemInserting" OnItemDeleting="fvParticipant_Property_ItemDeleting">
    <ItemTemplate>
        <div>
            <hr />
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbParticipant_Property_Close" runat="server" Text="Close" OnClick="lbParticipant_Property_Close_Click"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantProperty"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Master:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("master")) %></td></tr>
                <tr valign="top"><td class="B taR">Address:</td><td><%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), Eval("property.list_address2Type.longname"), Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), true) %></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <div>
            <hr />
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_participantProperty"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Master:</td><td><asp:DropDownList ID="ddlMaster" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td colspan="2"><hr /></td></tr>
                <tr valign="top"><td colspan="2"><uc1:UC_Property_EditInsert ID="UC_Property_EditInsert1" runat="server" ShowExpressImageButton="false" /></td></tr>
            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div>
            <hr />
            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Master:</td><td><asp:DropDownList ID="ddlMaster" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td colspan="2"><hr /></td></tr>
                <tr valign="top"><td colspan="2"><uc1:UC_Property_EditInsert ID="UC_Property_EditInsert1" runat="server" ShowExpressImageButton="false" /></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_Express_Organization.ascx.cs" Inherits="UC_Express_Organization" %>
<%@ Register src="~/UserControls/UC_Property_EditInsert.ascx" tagname="UC_Property_EditInsert" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<div>
    <asp:Panel ID="pnlExpress_Organization" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
        <asp:UpdatePanel ID="upExpress_Organization" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="float:left;"><div><span class="fsM B">Express Organization</span> [<asp:LinkButton ID="lbExpress_Organization_Add" runat="server" Text="Add a New Organization" OnClick="lbExpress_Organization_Add_Click"></asp:LinkButton>]</div></div>
                <div style="float:right;">[<asp:LinkButton ID="lbExpress_Organization_Close" runat="server" Text="Close" OnClick="lbExpress_Organization_Close_Click"></asp:LinkButton>]</div>
                <div style="clear:both;"></div>
                <asp:HiddenField ID="hfOrganizationPK" runat="server" />
                <hr />
                <asp:FormView ID="fvOrganization" runat="server" Width="100%" OnModeChanging="fvOrganization_ModeChanging" OnItemUpdating="fvOrganization_ItemUpdating" OnItemInserting="fvOrganization_ItemInserting">
                    <ItemTemplate>
                        <div class="NestedDivLevel01">
                            <div><span class="fsM B">Organization View</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_organization"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                            <hr />
                            <table cellpadding="3">
                                <tr valign="top"><td class="B taR">Organization:</td><td><%# Eval("org") %></td></tr>
                                <tr valign="top"><td class="B taR">Contact:</td><td><%# WACGlobal_Methods.SpecialText_Global_Participant(Eval("participant"), true, true, true, true) %></td></tr>
                                <tr valign="top"><td class="B taR">Property Address:</td><td><%# WACGlobal_Methods.SpecialText_Global_Address(Eval("property.address"), Eval("property.address2"), Eval("property.list_address2Type.longname"), Eval("property.city"), Eval("property.state"), Eval("property.fk_zipcode"), Eval("property.zip4"), false) %></td></tr>
                                <tr valign="top"><td class="B taR">NY Township:</td><td><%# Eval("property.list_townshipNY.township")%></td></tr>
                                <tr valign="top"><td class="B taR">NY County:</td><td><%# Eval("property.list_countyNY.county")%></td></tr>
                            </table>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="NestedDivLevel01">
                            <div><span class="fsM B">Organization Edit</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_organization"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span></div>
                            <hr />
                            <table cellpadding="3">
                                <tr valign="top"><td class="B taR">Organization:</td><td><asp:TextBox ID="tbOrg" runat="server" Text='<%# Bind("org") %>' Width="400px"></asp:TextBox></td></tr>
                                <tr valign="top"><td class="B taR">Contact:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Organization" runat="server" /></td>
                                <tr valign="top"><td colspan="2"><hr /></td></tr>
                                <tr valign="top"><td colspan="2"><uc1:UC_Property_EditInsert ID="UC_Property_EditInsert_Organization" runat="server" ShowExpressImageButton="false" /></td></tr>
                            </table>
                        </div>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <div class="NestedDivLevel01">
                            <div><span class="fsM B">Organization Insert</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                            <div>(Note: Inserting a new Organization record will not change the current attached Organization for the section record you are viewing. The new Organization record, however, will be available in the Organization Edit/Insert area.)</div>
                            <hr />
                            <table cellpadding="3">
                                <tr valign="top"><td class="B taR">Organization:</td><td><asp:TextBox ID="tbOrg" runat="server" Text='<%# Bind("org") %>' Width="400px"></asp:TextBox></td></tr>
                            </table>
                        </div>
                    </InsertItemTemplate>
                </asp:FormView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lbHiddenExpress_Organization" runat="server"></asp:LinkButton>
    <ajtk:ModalPopupExtender ID="mpeExpress_Organization" runat="server" TargetControlID="lbHiddenExpress_Organization" PopupControlID="pnlExpress_Organization" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>
</div>
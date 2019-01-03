<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACLists.aspx.cs" Inherits="WACLists" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Lists</div>
                    <div style="float:right;" class="B"><asp:HyperLink ID="hlList_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <uc2:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:UpdatePanel ID="upLists" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div><span class="B">Select a List:</span>
                            <asp:DropDownList ID="ddlLists" runat="server" OnSelectedIndexChanged="ddlLists_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                                <asp:ListItem Text="Agriculture - BMP Practices" Value="Ag_BMPPractice"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <hr />
                        <asp:Panel ID="pnlList_Ag_BMPPractice" runat="server" Visible="false">
                            <div>
                                <div style="float:left;"><span class="fsL B">Agriculture - BMP Practices</span> [<asp:LinkButton ID="lbList_Ag_BMPPractice_Insert" runat="server" Text="Add a New BMP Practice" OnClick="lbList_Ag_BMPPractice_Insert_Click" Font-Bold="True"></asp:LinkButton>]</div>
                                <div style="float:right;"><font color="red"><b>Red denotes inactive practices</b></font></div>
                                <div style="clear:both;"></div>
                            </div>
                            <hr />
                            <div style="padding:5px;">
                                <asp:GridView ID="gvList_Ag_BMPPractice" Width="100%" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvList_Ag_BMPPractice_SelectedIndexChanged" OnSorting="gvList_Ag_BMPPractice_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                    <HeaderStyle BackColor="#BBBBAA" />
                                    <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#DDDDCC" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View" />
                                        <asp:BoundField HeaderText="Code" DataField="pk_bmpPractice_code" SortExpression="pk_bmpPractice_code" />
                                        <%--<asp:BoundField HeaderText="Practice" DataField="practice" SortExpression="practice" />--%>
                                        <asp:TemplateField HeaderText="Practice" SortExpression="practice">
                                            <ItemTemplate>
                                                <%# WACGlobal_Methods.Format_Color_YesNo(Eval("practice"), Eval("active"), WACGlobal_Methods.Enum_Color.GREEN, WACGlobal_Methods.Enum_Color.RED, false, true) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Lifespan (Years)" DataField="life_reqd_yr" SortExpression="life_reqd_yr" />
                                        <asp:BoundField HeaderText="Agency" DataField="fk_agency_code" SortExpression="fk_agency_code" />
                                        <asp:TemplateField HeaderText="ABC" SortExpression="ABC" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# WACGlobal_Methods.Format_Global_Currency(Eval("ABC")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Units" DataField="fk_unit_code" SortExpression="fk_unit_code" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:Panel ID="pnlList_Ag_BMPPracticePopUp" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="upList_Ag_BMPPractice" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="fsM B" style="float:left;">List >> Agriculture >> BMP Practice</div>
                            <div style="float:right;">[<asp:LinkButton ID="lbList_Ag_BMPPracticePopUp_Close" runat="server" Text="Close" OnClick="lbList_Ag_BMPPracticePopUp_Close_Click"></asp:LinkButton>]</div>
                            <div style="clear:both;"></div>
                            <hr />
                            <asp:FormView ID="fvList_Ag_BMPPractice" runat="server" Width="100%" OnModeChanging="fvList_Ag_BMPPractice_ModeChanging" OnItemUpdating="fvList_Ag_BMPPractice_ItemUpdating" OnItemInserting="fvList_Ag_BMPPractice_ItemInserting" OnItemDeleting="fvList_Ag_BMPPractice_ItemDeleting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Practice</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Practice Code:</td><td><%# Eval("pk_bmpPractice_code") %></td></tr>
                                            <tr valign="top"><td class="taR B">Practice:</td><td><%# Eval("practice") %></td></tr>
                                            <tr valign="top"><td class="taR B">Agency:</td><td><%# Eval("list_agency.agency") %></td></tr>
                                            <tr valign="top"><td class="taR B">Lifespan (Years):</td><td><%# Eval("life_reqd_yr") %></td></tr>
                                            <tr valign="top"><td class="taR B">Agronomic:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("agronomic")) %></td></tr>
                                            <tr valign="top"><td class="taR B">Unit:</td><td><%# Eval("list_unit.unit") %></td></tr>
                                            <tr valign="top"><td class="taR B">WAC Practice Category:</td><td><%# Eval("list_wacPracticeCategory.category") %></td></tr>
                                            <tr valign="top"><td class="taR B">ABC:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("ABC")) %></td></tr>
                                            <tr valign="top"><td class="taR B">ABC Note:</td><td><%# Eval("ABC_note") %></td></tr>
                                            <tr valign="top"><td class="taR B">Active:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td></tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                        <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Practice</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Practice Code:</td><td><%# Eval("pk_bmpPractice_code") %></td></tr>
                                            <tr valign="top"><td class="taR B">Practice:</td><td><asp:TextBox ID="tbPractice" runat="server" Text='<%# Bind("practice") %>' TextMode="MultiLine" Width="300px" Rows="2" MaxLength="96"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Agency:</td><td><asp:DropDownList ID="ddlAgency" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Lifespan (Years):</td><td><asp:DropDownList ID="ddlLifespan" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Agronomic:</td><td><asp:DropDownList ID="ddlAgronomic" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Unit:</td><td><asp:DropDownList ID="ddlUnit" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">WAC Practice Category:</td><td><asp:DropDownList ID="ddlWACPracticeCategory" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">ABC:</td><td><asp:TextBox ID="tbABC" runat="server" Text='<%# Bind("ABC") %>'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">ABC Note:</td><td><asp:TextBox ID="tbABCNote" runat="server" Text='<%# Bind("ABC_note") %>' TextMode="MultiLine" Width="300px" Rows="4" MaxLength="255"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Active:</td><td><asp:DropDownList ID="ddlActive" runat="server"></asp:DropDownList></td></tr>
                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">BMP Practice</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]</div>
                                        <hr />
                                        <table cellpadding="3">
                                            <tr valign="top"><td class="taR B">Practice Code:</td><td><asp:TextBox ID="tbPK" runat="server" Text='<%# Bind("pk_bmpPractice_code") %>'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Practice:</td><td><asp:TextBox ID="tbPractice" runat="server" Text='<%# Bind("practice") %>' TextMode="MultiLine" Width="300px" Rows="2" MaxLength="96"></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">Agency:</td><td><asp:DropDownList ID="ddlAgency" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Lifespan (Years):</td><td><asp:DropDownList ID="ddlLifespan" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Agronomic:</td><td><asp:DropDownList ID="ddlAgronomic" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">Unit:</td><td><asp:DropDownList ID="ddlUnit" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">WAC Practice Category:</td><td><asp:DropDownList ID="ddlWACPracticeCategory" runat="server"></asp:DropDownList></td></tr>
                                            <tr valign="top"><td class="taR B">ABC:</td><td><asp:TextBox ID="tbABC" runat="server" Text='<%# Bind("ABC") %>'></asp:TextBox></td></tr>
                                            <tr valign="top"><td class="taR B">ABC Note:</td><td><asp:TextBox ID="tbABCNote" runat="server" Text='<%# Bind("ABC_note") %>' TextMode="MultiLine" Width="300px" Rows="4" MaxLength="255"></asp:TextBox></td></tr>
                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:LinkButton ID="lbHidden_List_Ag_BMPPractice" runat="server"></asp:LinkButton>
                <ajtk:ModalPopupExtender ID="mpeList_Ag_BMPPractice" runat="server" TargetControlID="lbHidden_List_Ag_BMPPractice" PopupControlID="pnlList_Ag_BMPPracticePopUp" BackgroundCssClass="ModalPopup_BG"></ajtk:ModalPopupExtender>

            </div>
        </div>
    </div>
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelOwnerGrid.ascx.cs" Inherits="Property_WACPR_TaxParcelOwnerGrid" %>
<%@ Register Src="~/Utility/WACUT_ExpressNavigate.ascx" TagPrefix="uc1" TagName="WACUT_ExpressNavigate" %>

<asp:GridView ID="gvTaxParcelOwner" Width="100%" runat="server" AutoGenerateColumns="false"
    SkinID="gvSkin" AllowPaging="False" AllowSorting="False">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                [<asp:LinkButton ID="lb_gvOpenView" runat="server" Text="View" CommandArgument='<%# Eval("pk_taxParcelOwner") %>'
                    OnClick="lb_gvOpenView_Click"></asp:LinkButton>]
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Express">
            <ItemTemplate>
                <uc1:WACUT_ExpressNavigate runat="server" ID="WACUT_ExpressNavigate" BindToProperty="pk_participant"
                    WACSector="Participant" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Participant" DataField="fullname_FL_dnd" />
        <asp:TemplateField HeaderText="Active">
            <ItemTemplate>
                <%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Note" DataField="note" />
    </Columns>
</asp:GridView>

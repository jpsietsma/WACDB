<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelGrid.ascx.cs" Inherits="Property_WACPR_TaxParcelGrid" %>


<asp:GridView ID="gvTaxParcel" Width="100%" runat="server" AutoGenerateColumns="false"
    SkinID="gvSkin" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvTaxParcel_PageIndexChanging"
    OnSorting="gvTaxParcel_Sorting" AllowSorting="True" PagerSettings-Mode="NumericFirstLast">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                [<asp:LinkButton ID="lb_gvOpenView" runat="server" Text="View" CommandArgument='<%# Eval("pk_taxParcel") %>'
                    OnClick="lb_gvOpenView_Click"></asp:LinkButton>]
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Print Key" DataField="taxParcelID" SortExpression="taxParcelID" />
        <asp:BoundField HeaderText="SBL (Section Block Lot)" DataField="SBL" SortExpression="SBL" />
        <asp:BoundField HeaderText="SWIS" DataField="fk_list_swis" SortExpression="fk_list_swis" />
        <asp:TemplateField HeaderText="County" SortExpression="Swis.county">
            <ItemTemplate>
                <%# Eval("Swis.county")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Jurisdiction" SortExpression="Swis.jurisdiction">
            <ItemTemplate>
                <%# Eval("Swis.jurisdiction")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Owners" DataField="ownerStr_dnd" SortExpression="ownerStr_dnd" />
        <asp:BoundField HeaderText="Acres" DataField="acreage" SortExpression="acreage" />
        <asp:BoundField HeaderText="Retired" DataField="retired" SortExpression="retired" />
    </Columns>
</asp:GridView>



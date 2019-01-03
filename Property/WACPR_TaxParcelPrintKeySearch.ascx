<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelPrintKeySearch.ascx.cs" Inherits="Property_WACPR_TaxParcelPrintKeySearch" %>

<asp:UpdatePanel ID="upTaxParcelIDSearch" runat="server" DefaultButton="btnTaxParceIDSearch" UpdateMode="Conditional">
    <ContentTemplate>
        <div style="float: left; margin-left: 20px;">
        <span class="B">All or Part of Tax Parcel ID:</span>
        <asp:TextBox ID="tbTaxParcelIDSearch" runat="server" OnTextChanged="tbTaxParcelIDSearch_TextChanged"></asp:TextBox>
        <asp:Button ID="btnTaxParceIDSearch" runat="server" Text="Search" OnCommand="btnTaxParceIDSearch_Command" CommandArgument='<%#Eval("tbTaxParcelIDSearch") %>' />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

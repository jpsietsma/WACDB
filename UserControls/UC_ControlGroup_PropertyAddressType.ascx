<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_ControlGroup_PropertyAddressType.ascx.cs" Inherits="UC_ControlGroup_PropertyAddressType" %>
<table class="TaT">
    <tr class="taT">
        <td class="B taR">Address Type:</td>
        <td><asp:DropDownList ID="ddlAddressType" runat="server" OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
    <tr class="taT">
        <td class="B taR">Number:</td>
        <td><asp:TextBox ID="tbNumber" runat="server" AutoPostBack="true" Text='<%# Bind("nbr") %>' OnTextChanged="tbNumber_TextChanged"></asp:TextBox> (Street Number, Road Number, PO Box Number)</td>
    </tr>
    <tr class="taT">
        <td class="B taR">Road/Street Name:</td>
        <td><asp:TextBox ID="tbAddressBase" runat="server" AutoPostBack="true" Text='<%# Bind("address_base") %>' Width="300px" OnTextChanged="tbAddressBase_TextChanged"></asp:TextBox></td>
    </tr>
    <tr class="taT">
        <td class="B taR">Resulting Address:</td>
        <td><asp:Label ID="AddressString" runat="server" AutoPostBack="true"></asp:Label>&nbsp; (the complete address as you build it)</td>
    </tr>
</table>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_SearchByAddress.ascx.cs" Inherits="UC_SearchByAddress" %>
<div>
    <div>
        <div style="float:left;"><div class="B">Address:</div>State: <asp:DropDownList ID="ddl_Search_State" runat="server" OnSelectedIndexChanged="ddl_Search_State_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> <asp:Button ID="btn_Search_State" runat="server" Text="Search State" OnClick="btn_Search_State_Click" /></div>
        <div style="float:left; margin-left:20px;"><div class="B">&nbsp;</div>City: <asp:DropDownList ID="ddl_Search_City" runat="server" OnSelectedIndexChanged="ddl_Search_City_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> <asp:Button ID="btn_Search_City" runat="server" Text="Search City" OnClick="btn_Search_City_Click" /></div>
        <div style="clear:both;"></div>
    </div>
    <div style="margin-top:5px;">
        <div style="float:left;">Address Type: <asp:DropDownList ID="ddl_Search_AddressType" runat="server" OnSelectedIndexChanged="ddl_Search_AddressType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> <asp:Button ID="btn_Search_AddressType" runat="server" Text="Search Address Type" OnClick="btn_Search_AddressType_Click" /></div>
        <asp:Panel ID="pnl_Search_Base" runat="server" Visible="false">
            <div style="float:left; margin-left:20px;"><asp:Label ID="lblBase" runat="server"></asp:Label>: <asp:DropDownList ID="ddl_Search_Address" runat="server" OnSelectedIndexChanged="ddl_Search_Address_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> <asp:Button ID="btn_Search_Address" runat="server" OnClick="btn_Search_Address_Click" /></div>
        </asp:Panel>
        <div style="clear:both;"></div>
    </div>
    <div style="margin-top:5px;">
        <div style="float:left;">Number: <asp:DropDownList ID="ddl_Search_AddressNumber" runat="server" OnSelectedIndexChanged="ddl_Search_AddressNumber_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
        <div style="clear:both;"></div>
    </div>
</div>
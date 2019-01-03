<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_Express_Global_Insert.ascx.cs" Inherits="UC_Express_Global_Insert" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_ControlGroup_PropertyAddressType.ascx" tagname="UC_ControlGroup_PropertyAddressType" tagprefix="uc1" %>


<div>
    <div>
        <span class="B">Select a Global Section:</span> 
        <asp:DropDownList ID="ddlGlobalList" runat="server" OnSelectedIndexChanged="ddlGlobalList_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
            <asp:ListItem Text="Communications" Value="Communications"></asp:ListItem>
            <asp:ListItem Text="Organizations" Value="Organizations"></asp:ListItem>
            <asp:ListItem Text="Participants" Value="Participants"></asp:ListItem>
            <asp:ListItem Text="Properties" Value="Properties"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <hr />
<!-- Communications -->
    <asp:Panel ID="pnlGlobal_Insert_Communications" runat="server" Visible="false">
        <div class="NestedDivLevel01">
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Area Code:</td><td><asp:TextBox ID="tbCommunications_AreaCode" runat="server"></asp:TextBox></td><td>ex: 670 (3 numbers)</td></tr>
                <tr valign="top"><td class="B taR">Phone Number:</td><td><asp:TextBox ID="tbCommunications_PhoneNumber" runat="server"></asp:TextBox></td><td>ex: 8657090 (7 numbers)</td></tr>
            </table>
            <hr />
            <div><asp:Button ID="btnGlobal_Insert_Communications" runat="server" Text="Insert Communication" CommandArgument="Communications" OnClick="btnGlobal_Insert_Click" /></div>
            <div style="padding:3px;"><asp:Label ID="lblGlobal_Insert_Communications" runat="server"></asp:Label></div>
        </div>
    </asp:Panel>
<!-- Organizations -->
    <asp:Panel ID="pnlGlobal_Insert_Organizations" runat="server" Visible="false">
        <div class="NestedDivLevel01">
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Organization:</td><td><asp:TextBox ID="tbOrganizations_Organization" runat="server" Width="400px"></asp:TextBox></td><td>ex: Watershed Agricultural Council</td></tr>
            </table>
            <hr />
            <div><asp:Button ID="btnGlobal_Insert_Organizations" runat="server" Text="Insert Organization" CommandArgument="Organizations" OnClick="btnGlobal_Insert_Click" /></div>
            <div style="padding:3px;"><asp:Label ID="lblGlobal_Insert_Organizations" runat="server"></asp:Label></div>
        </div>
    </asp:Panel>
<!-- Participants -->
    <asp:Panel ID="pnlGlobal_Insert_Participants" runat="server" Visible="false">
        <div class="NestedDivLevel01">
            <div><b>Select a Participant Variation: </b>
                <asp:RadioButtonList ID="rblParticipants" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblParticipants_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Person" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Person with Organization" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Organization" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <hr />
            <asp:Panel ID="pnlParticipant_Person" runat="server">
                <table cellpadding="3">
                    <tr valign="top"><td class="B taR">First Name:</td><td><asp:TextBox ID="tbParticipants_NameFirst" runat="server"></asp:TextBox></td></tr>
                    <tr valign="top"><td class="B taR">Last Name:</td><td><asp:TextBox ID="tbParticipants_NameLast" runat="server"></asp:TextBox></td></tr>
                    <tr valign="top"><td class="B taR">Suffix:</td><td><asp:DropDownList ID="ddlParticipants_Suffix" runat="server"></asp:DropDownList></td></tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlParticipant_Organization" runat="server" Visible="false">
                <table cellpadding="3">
                    <tr valign="top"><td class="B taR">Organization:</td><td><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Participants" runat="server" StrParentCase="ORGANIZATION" StrEntityType="ORGANIZATION" ShowStartsWithNumber="true" /></td></tr>
                </table>
            </asp:Panel>
            <hr />
            <table cellpadding="3">
                <tr valign="top"><td class="B taR">Initial Participant Type:</td><td><asp:DropDownList ID="ddlParticipantType" runat="server"></asp:DropDownList></td></tr>
                <tr valign="top"><td class="B taR">WAC Region:</td><td><asp:DropDownList ID="ddlWACRegion" runat="server"></asp:DropDownList></td></tr>
            </table>
            <hr />
            <div><asp:Button ID="btnGlobal_Insert_Participants" runat="server" Text="Insert Participant" CommandArgument="Participants" OnClick="btnGlobal_Insert_Click" /></div>
            <div style="padding:3px;"><asp:Label ID="lblGlobal_Insert_Participants" runat="server"></asp:Label></div>
        </div>
    </asp:Panel>
<!-- Property -->
    <asp:Panel ID="pnlGlobal_Insert_Properties" runat="server" Visible="false">
        <div class="NestedDivLevel01">

          <table class="tp3">
            <tr class="TaT"><td class="B taR">Address Line 1:</td><td><asp:TextBox ID="tbAddress1" runat="server" Text='<%# Bind("address") %>'></asp:TextBox></td></tr>
            <tr class="TaT"><td class="B taR">Address Line 2:</td><td><asp:TextBox ID="tbAddress2" runat="server" Text='<%# Bind("address2") %>'></asp:TextBox></td></tr>
            <tr class="TaT"><td class="B taR">City:</td><td><asp:TextBox ID="tbCity" runat="server" Text='<%# Bind("city") %>'></asp:TextBox></td></tr>
            <tr class="TaT"><td class="B taR">State:</td><td><asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
            <tr class="TaT"><td class="B taR">Zip:</td><td><asp:DropDownList ID="ddlZip" runat="server" OnSelectedIndexChanged="ddlZip_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
            <tr class="TaT"><td class="B taR">Zip4:</td><td><asp:TextBox ID="tbZip4" runat="server" Text='<%# Bind("zip4") %>'></asp:TextBox></td></tr>
            <tr class="TaT"><td class="B taR">NY County:</td><td><asp:DropDownList ID="ddlCounty" runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
            <tr class="TaT"><td class="B taR">NY Township:</td><td><asp:DropDownList ID="ddlTownshipNY" runat="server"></asp:DropDownList></td></tr>
            <tr class="TaT"><td class="B taR">Basin:</td><td><asp:DropDownList ID="ddlBasin" runat="server" OnSelectedIndexChanged="ddlBasin_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
            <tr class="TaT"><td class="B taR">SubBasin</td><td><asp:DropDownList ID="ddlSubbasin" runat="server"></asp:DropDownList></td></tr>

        </table>
            <div><asp:Button ID="btnGlobal_Insert_Properties" runat="server" Text="Insert Property" CommandArgument="Properties" OnClick="btnGlobal_Insert_Click" /></div>
            <div style="padding:3px;"><asp:Label ID="lblGlobal_Insert_Properties" runat="server"></asp:Label></div>
        </div>
    </asp:Panel>
</div>
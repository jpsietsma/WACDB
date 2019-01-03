<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WACTest.aspx.cs" Inherits="WACTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-image:none; background-color:#FFFFFF; font-size:x-small;">
    <form id="form1" runat="server">
    <div>
        <div style="padding:5px;"><asp:Button ID="btnFarmBusinessActive" runat="server" Text="Farm Business >> Active Farms" OnClick="btnFarmBusinessActive_Click" /></div>
        <div style="padding:5px;"><asp:Button ID="btnBMPs" runat="server" Text="All BMPs" OnClick="btnBMPs_Click" /></div>
        <div style="padding:5px;"><asp:Button ID="btnNoBMPs" runat="server" Text="No BMPs" OnClick="btnNoBMPs_Click" /></div>
        <div style="padding:5px;"><asp:Button ID="btnFarmLand" runat="server" Text="Show Farm Land Connections" OnClick="btnFarmLand_Click" /></div>
    </div>
    <div style="padding:5px;">
        <div><asp:Label ID="lblRecordCount" runat="server"></asp:Label></div>
        <asp:GridView ID="gvFarmBusiness" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="Farm ID" DataField="farmID" />
                <asp:BoundField HeaderText="Farm Name" DataField="farm_name" />
                <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:ListView ID="lv" runat="server" DataSource='<%# Eval("farmBusinessStatus") %>'>
                            <ItemTemplate>
                                <div><%# Eval("fk_status_code")%></div>
                            </ItemTemplate>
                        </asp:ListView>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Owners">
                    <ItemTemplate>
                        <asp:ListView ID="lv" runat="server" DataSource='<%# Eval("farmBusinessOwners") %>'>
                            <ItemTemplate>
                                <div><%# Eval("participant.fname") %> <%# Eval("participant.lname") %></div>
                            </ItemTemplate>
                        </asp:ListView>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="orgs">
                    <ItemTemplate>
                        <asp:ListView ID="lv" runat="server" DataSource='<%# Eval("farmBusinessOwners") %>'>
                            <ItemTemplate>
                                <div><%# Eval("participant.organization.org") %></div>
                            </ItemTemplate>
                        </asp:ListView>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <div style="width:300px;"></div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvBMPs" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Farm ID" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("farmBusiness.farmID") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gv" runat="server"></asp:GridView>
        <asp:Label ID="lbl" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>

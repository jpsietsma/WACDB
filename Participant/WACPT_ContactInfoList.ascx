<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPT_ContactInfoList.ascx.cs" Inherits="Participant_WACPT_ContactInfoList" %>


<asp:ListView runat="server" ID="lvContactInfo">
    <LayoutTemplate>
    <table class="tp3" >
        <tr class="taT">
            <td class="B U">Title</td>
            <td class="B U">Name</td>
            <td class="B U">Phone</td>
            <td class="B U">Email</td>
        </tr>
        <tr id="itemPlaceholder" runat="server"></tr>
    </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr class="taT">
                <td><%# Eval("Position")%></td>
                <td><%# Eval("Employee")%></td>
                <td><%# Eval("Phone") %></td>   
                <td><a href='mailto:<%# Eval("email") %>'><%# Eval("email") %></a></td>
        </tr>       
    </ItemTemplate>
</asp:ListView>
       
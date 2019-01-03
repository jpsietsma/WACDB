<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACExpressAdd_Participant.ascx.cs" Inherits="WACExpressAdd_Participant" %>
<div>
    <div class="fsL B">Express Add: Participant</div>
    <div>
        <hr />
        <div>1. Check to see if the Participant exists</div>
        <div>2. If not, express add the Participant</div>
        <div>3. Go to participants and edit the Participant</div>
        <hr />
    </div>
    <table>
        <tr><td class="B taR">First Name:</td><td><asp:TextBox ID="tbF" runat="server"></asp:TextBox></td></tr>
        <tr><td class="B taR">Last Name:</td><td><asp:TextBox ID="tbL" runat="server"></asp:TextBox></td></tr>
        <tr><td class="B taR">Organization:</td><td><asp:TextBox ID="tbO" runat="server"></asp:TextBox></td></tr>
        <tr><td colspan="2" align="center"><asp:Button ID="btnMatch" runat="server" Text="Check" OnClick="btnMatch_Click" /></td></tr>
    </table>
    <asp:Panel ID="pnl" runat="server" Visible="false">
        <hr />
        <div>No matches or don't see who you are looking for in the results list? Express Add the participant you defined above:</div>
        <div style="margin-top:10px;">
            <asp:Button ID="btnAdd" runat="server" Text="Express Add" OnClick="btnAdd_Click" OnClientClick="return confirm_add();" />
        </div>
        <hr />
    </asp:Panel>
    <asp:ListView ID="lv" runat="server">
        <LayoutTemplate><div class="fsM B">Results:</div><span id="itemPlaceholder" runat="server"></span></LayoutTemplate>
        <EmptyDataTemplate><div class="B">No matches found</div></EmptyDataTemplate>
        <ItemTemplate>
            <div style="margin:5px; padding:5px; border:solid 1px #CCCCCC; background-color:#FFFFEC;">
                <div class="B"><%# Eval("fullname_LF_dnd")%></div>
                <div style="margin-left:10px;">
                    <div class="B">Participant Types:</div>
                    <div style="margin-left:10px;">
                        <asp:ListView ID="lvPT" runat="server" DataSource='<%# Eval("participantTypes") %>'>
                            <LayoutTemplate>
                                <span id="itemPlaceholder" runat="server"></span>
                                <div><asp:LinkButton ID="lbAddPT" runat="server" OnClick="lbAddPT_Click"></asp:LinkButton></div>
                            </LayoutTemplate>
                            <EmptyDataTemplate><div>No Participant Types</div><div><asp:LinkButton ID="lbAddPTEmpty" runat="server" OnClick="lbAddPT_Click"></asp:LinkButton></div></EmptyDataTemplate>
                            <ItemTemplate>
                                <div><%# Eval("list_participantType.participantType") %></div>
                                <asp:HiddenField ID="hfPT" runat="server" Value='<%# Eval("list_participantType.pk_participantType_code") %>' />
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfP" runat="server" Value='<%# Eval("pk_participant") %>' />
        </ItemTemplate>
    </asp:ListView>
</div>
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACContact.aspx.cs" Inherits="WACContact" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div class="fsXL B">Contact Information</div>
                <uc2:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:DataList ID="dlContactInformation" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <div style="padding:10px;">
                     <%--       <div class="fsM B"><%# WACGlobal_Methods.Math_CountRecords(ref iCount)%>. <%# Eval("participantWAC.list_sectorWAC.sector")%></div>--%>
                            <div class="B I"><%# Eval("participantWAC.title") %></div>
                            <div><%# Eval("participantWAC.participant.fname") %> <%# Eval("participantWAC.participant.lname") %></div>
                            <div><%# WACGlobal_Methods.Format_Global_PhoneNumberPlusExtension(Eval("participantWAC.phone"), Eval("participantWAC.phone_ext")) %></div>
                            <div><a href='mailto:<%# Eval("participantWAC.email") %>'><%# Eval("participantWAC.email") %></a></div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>


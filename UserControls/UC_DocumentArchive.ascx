<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/UC_DocumentArchive.ascx.cs" Inherits="UC_DocumentArchive" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>

<div>
    <div style="float:left;"><asp:LinkButton ID="lb" runat="server" OnClick="lb_Click"></asp:LinkButton></div>
    <div style="clear:both;"></div>
</div>
<asp:Panel ID="pnl" runat="server" Visible="false">
    <asp:Label ID="lbl" runat="server"></asp:Label>
</asp:Panel>
<asp:UpdatePanel ID="upDocuments" runat="server" Visible="false">
    <ContentTemplate>
        <asp:Panel ID="pHeader" runat="server" CssClass="cpHeader" >
            <asp:Image ID="imgExpander" runat="server" ImageAlign="Middle" />
            <asp:Label ID="lblDocuments" Text="Documents " runat="server" ></asp:Label>
            <asp:Label ID="lblDocumentCount" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlListPanel" runat="server" CssClass="cpBody" > 
        <hr />   
        <asp:ListView ID="lvDocuments" runat="server" style="margin:12px;" >
            <LayoutTemplate>
                <table class="tp5" >
                    <tr class="taT">
                        <td class="B U">File Type</td>
                        <td class="B U">File Link</td>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="taT">
                    <td><%# Eval("folder")%></td>
                    <td><%# Eval("alink") %></td>    
                </tr>
            </ItemTemplate>
        </asp:ListView>   
    </asp:Panel>
    <ajtk:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlListPanel" CollapseControlID="pHeader" 
        ExpandControlID="pHeader" Collapsed="true" TextLabelID="lblExpander" CollapsedImage="~/images/expand_24.png" 
        ExpandedImage="~/images/collapse_24.png" ImageControlID="imgExpander" CollapsedSize="0">
    </ajtk:CollapsiblePanelExtender>        
    </ContentTemplate>
</asp:UpdatePanel>  
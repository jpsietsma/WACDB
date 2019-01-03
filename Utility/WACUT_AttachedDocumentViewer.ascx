<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACUT_AttachedDocumentViewer.ascx.cs" Inherits="Utility_WACUT_AttachedDocumentViewer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<div class="DivBoxOrange" style="margin: 5px 0px 5px 0px;">
    <asp:UpdatePanel ID="upDocuments" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pHeader" runat="server" CssClass="cpHeader" >
                <asp:Image ID="imgExpander" runat="server" ImageAlign="Middle" />
                <asp:Literal Text=" Documents" runat="server" ></asp:Literal>
                <asp:Literal ID="ltDocumentCount" runat="server"></asp:Literal>
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
</div>
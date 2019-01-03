<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Utility/wacut_associations.ascx.cs" Inherits="WACUT_Associations" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>

<div class="DivBoxPurple">
    <asp:UpdatePanel ID="upAssociations" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pHeader" runat="server" CssClass="cpHeader" >
                <asp:Image ID="imgExpander" runat="server" ImageAlign="Middle" />
                <asp:Label ID="lblAssociations" runat="server" ></asp:Label>
                <asp:Label ID="lblAssociationCount" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnlListPanel" runat="server" CssClass="cpBody" > 
            <hr />   
            <asp:ListView ID="lvAssociations" runat="server" OnItemCommand="lvItemCommand"  style="margin:12px;" >
                <LayoutTemplate>
                    <table class="tp3" >
                        <tr class="taT">
                            <td class="B U">Source</td>
                            <td class="B U">Association</td>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="taT">
                        <td><%# Eval("source")%></td>
                        <td><asp:LinkButton ID="lbView" runat="server" Text='<%# Eval("label")%>' CommandName=<%#Eval("TableGo") %>
                            CommandArgument='<%# Eval("TablePK") %>' ></asp:LinkButton></td>    
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
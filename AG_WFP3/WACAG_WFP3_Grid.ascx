<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/AG_WFP3/WACAG_WFP3_Grid.ascx.cs" Inherits="AG_WACAG_WFP3_Grid" %>
<%--<%@ Register Src="~/AG_WFP3/WACAG_WFP3.ascx" TagPrefix="uc" TagName="WACAG_WFP3" %>--%>

 <div><span class="fsM B">WFP3</span> [<asp:LinkButton ID="lbAg_WFP3_Add" runat="server" Text="Add WFP3" 
    OnClick="lbAg_WFP3_Add_Click"></asp:LinkButton>]</div>
      <hr />
 <div style="margin:10px 0px 0px 20px;overflow-x:auto;" >
 <asp:UpdatePanel ID="upnl_WFP3" UpdateMode="Conditional" runat="server" >
<ContentTemplate> 
    <asp:GridView ID="gvAg_WFP3" runat="server" AutoGenerateColumns="false" CellPadding="3" EnableViewState="false" Width="100%" >
        <HeaderStyle BackColor="#DDDDDD" />
        <RowStyle VerticalAlign="Top" />
        <AlternatingRowStyle BackColor="#EEEEEE" />
        <EmptyDataTemplate><div class="I">No WFP3 Records</div></EmptyDataTemplate>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>[<asp:LinkButton ID="lbAg_WFP3_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_form_wfp3") %>' 
                OnClick="lbAg_WFP3_View_Click"></asp:LinkButton>]</ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Package" DataField="packageNameBMPs" />
            <%--<asp:TemplateField HeaderText="Package">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"  Width="170px" Text='<%# WACGlobal_Methods.Truncate(Eval("packageName"),18) %>' 
                      ToolTip='<%# String.Format("{0} - {1}", Eval("packageName"),Eval("description")) %>' ></asp:Label>
             
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%--<asp:BoundField HeaderText="Description" DataField="description" />--%>
            <asp:BoundField HeaderText="Designed" DataField="designed" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="Bid" DataField="outForBid" DataFormatString="{0:d}"  />
            <asp:BoundField HeaderText="Contracted" DataField="awarded" DataFormatString="{0:d}"  />
            <asp:BoundField HeaderText="Certified" DataField="certified" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="Design Cost" DataField="cost_est" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Encumbered" DataField="encumbered" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="WAC Funding" DataField="funding" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"  />
            <asp:BoundField HeaderText="Other Funding" DataField="fundingOther" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"  />
        </Columns>
    </asp:GridView> 
</ContentTemplate>
</asp:UpdatePanel> 

</div>



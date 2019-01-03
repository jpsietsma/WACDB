<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Property/WACPR_TaxParcelPicker.ascx.cs" 
   Inherits="WACPR_TaxParcelPicker" %>

<asp:Panel ID="pnlTaxParcelPicker" runat="server" >
    <asp:UpdatePanel ID="upTaxParcelPicker" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tp3">
                <tr class="taT"><td class="B taR">County:</td><td><asp:DropDownList ID="ddlCounty" runat="server" ViewStateMode="Disabled" 
                    OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>                  
                <tr class="taT"><td class="B taR">Jurisdiction:</td><td><asp:DropDownList ID="ddlJurisdiction" runat="server"  ViewStateMode="Disabled"
                    OnSelectedIndexChanged="ddlJurisdiction_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Tax Parcel ID:</td><td><asp:DropDownList ID="ddlTaxParcelID" runat="server" ViewStateMode ="Disabled"
                     OnSelectedIndexChanged="ddlTaxParcelID_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td></tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <asp:Panel ID="pnlTaxParcelPickerCommands" runat="server" Visible="false" >
        <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" CommandName="Cancel" OnCommand="TaxParcelPicker_Command" Visible="false"></asp:LinkButton> |
        <asp:LinkButton ID="lbAddTaxParcel" runat="server" Text="Add Tax Parcel" CommandName="Add" OnCommand="TaxParcelPicker_Command"></asp:LinkButton> 
        <asp:LinkButton ID="lbDeleteTaxParcel" runat="server" Text="Delete Tax Parcel" CommandName="Delete" OnCommand="TaxParcelPicker_Command" Visible="false"></asp:LinkButton>   
    </asp:Panel>
</asp:Panel>

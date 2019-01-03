<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/AG_BMP/WACAG_BMPSupplementalAgreement.ascx.cs" Inherits="AG_BMP_WACAG_BMPSupplementalAgreement" %>

 <div class="NestedDivLevel02">
    <div style="margin-bottom:5px;"><span class="fsM B">Supplemental Agreements</span></div>
    <div style="margin-left:5px;">
        <asp:ListView ID="lvAg_BMP_SAs" runat="server" DataSource='<%# Eval("bmp_ag_SAs") %>'>
            <EmptyDataTemplate><div class="I">No Supplemental Agreement Records</div></EmptyDataTemplate>
            <LayoutTemplate>
                <table cellpadding="5" rules="cols">
                    <tr valign="top">
                        <td>&nbsp;</td>
                        <td class="B U">SA #</td>
                        <td class="B U">Revision #</td>
                        <td class="B U">Tax Parcel</td>
                        <td class="B U">Active</td>
                        <td class="B U">Note</td>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr valign="top">
                    <td>[<asp:LinkButton ID="lbAg_BMP_SA_View" runat="server" Text="View" CommandArgument='<%# Eval("pk_bmp_ag_SA") %>' 
                        OnClick="lbAg_BMP_SA_View_Click"></asp:LinkButton>]</td>
                    <td><%# Eval("supplementalAgreementTaxParcel.supplementalAgreement.agreement_nbr_ro")%></td>
                    <td><%# Eval("form_wfp2_version.version") %></td>
                    <td><%# WACGlobal_Methods.SpecialText_Global_TaxParcel_ID_OwnerStr(Eval("supplementalAgreementTaxParcel.taxParcel.taxParcelID"), 
                            Eval("supplementalAgreementTaxParcel.taxParcel.ownerStr_dnd"), false)%></td>
                    <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td>
                    <td><%# Eval("note") %></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACPR_TaxParcelOwnerForm.ascx.cs" Inherits="Property_WACPR_TaxParcelOwnerForm" %>
<%@ Register Src="~/Participant/WACPT_ParticipantAlphaPicker.ascx" TagPrefix="uc1" TagName="WACPT_ParticipantAlphaPicker" %>
<%@ Register Src="~/Utility/WACUT_YesNoChooser.ascx" TagPrefix="uc1" TagName="WACUT_YesNoChooser" %>

<asp:UpdatePanel ID="upTaxParcelOwnerForm" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:FormView ID="fvTaxParcelOwner" runat="server" Width="100%" OnItemCommand="fvItemCommand" >
            <ItemTemplate>
                <div class="NestedDivLevel01">
                    <div>
                        <span class="fsM B">Tax Parcel Owners</span>
                        [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="EditData" CommandArgument='<%#Eval("pk_taxParcelOwner") %>' Text="Edit"></asp:LinkButton>
                        |<asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" CommandArgument='<%#Eval("pk_taxParcelOwner") %>' Text="Delete"
                            OnClientClick="return confirm_delete();"></asp:LinkButton>
                        |<asp:LinkButton ID="lbClose" runat="server" CausesValidation="false" CommandName="CloseForm" Text="Close"></asp:LinkButton>
                        ] 
                <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_taxParcelOwner"), Eval("created"), Eval("created_by"), 
                                            Eval("modified"), Eval("modified_by"))%></span>
                    </div>
                    <hr />
                    <table class="tp3">
                        <tr class="taT">
                            <td class="B taR">Participant:</td>
                            <td><%# Eval("fullname_FL_dnd")%></td>
                        </tr>
                        <tr class="taT">
                            <td class="B taR">Active:</td>
                            <td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("active")) %></td>
                        </tr>
                        <tr class="taT">
                            <td class="B taR">Note:</td>
                            <td><%# Eval("note") %></td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
            <EditItemTemplate>
                <div class="NestedDivLevel01">
                    <div>
                        <span class="fsM B">Tax Parcel Owner</span>
                        [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton>
                        |<asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>
                        ] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_taxParcelOwner"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%></span>
                    </div>
                    <hr />
                    <table class="tp3">
                        <tr class="taT">
                            <td class="B taR">Participant:</td>
                            <td><%# Eval("fullname_FL_dnd")%></td>
                        </tr>
                        <tr class="taT">
                            <td class="B taR">Active:</td>
                            <td>
                                <uc1:WACUT_YesNoChooser runat="server" ID="WACUT_YesNoChooser" SelectedValue='<%# Bind("active") %>' BoundPropertyName="active" />
                            </td>
                        </tr>
                        <tr class="taT">
                            <td class="B taR">Note:</td>
                            <td>
                                <asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="6"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
            </EditItemTemplate>
            <InsertItemTemplate>
                <div class="NestedDivLevel01">
                    <div>
                        <span class="fsM B">Tax Parcel Owner</span>
                        [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton>
                        |<asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>
                        ]
                    </div>
                    <hr />
                    <div class="B">Add an Owner (Person or Organization):</div>
                    <div style="margin-left: 20px;">                        <uc1:WACPT_ParticipantAlphaPicker runat="server" ID="WACPT_ParticipantAlphaPicker" ListType="TaxParcelOwner"
                             DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="false" IsActiveUpdate="false" />
                    </div>
                    <table class="tp3">
                        <tr class="taT">
                            <td class="B taR">Active:</td>
                            <td>
                                <uc1:WACUT_YesNoChooser runat="server" ID="WACUT_YesNoChooser" SelectedValue='<%# Bind("active") %>' BoundPropertyName="active" 
                                     DefaultVisibility="false" IsActiveInsert="true" IsActiveReadOnly="false" IsActiveUpdate="true"/>
                            </td>
                        </tr>
                        <tr class="taT">
                            <td class="B taR">Note:</td>
                            <td>
                                <asp:TextBox ID="tbNote" runat="server" Text='<%# Bind("note") %>' TextMode="MultiLine" Width="400px" Rows="6"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
            </InsertItemTemplate>
        </asp:FormView>
    </ContentTemplate>
</asp:UpdatePanel>


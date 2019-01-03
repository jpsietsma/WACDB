<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Property/UC_Express_Property.ascx.cs" Inherits="UC_Express_Property" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajtk" %>
<%@ Register src="~/UserControls/UC_Express_Global_Insert.ascx" tagname="UC_Express_Global_Insert" tagprefix="uc1" %>
<div>
    <asp:Panel ID="pnlExpress_Property" runat="server" CssClass="ModalPopup_Panel" ScrollBars="Vertical">
        <asp:UpdatePanel ID="upExpress_Property" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="float:left;"><div><span class="fsM B">Express Property</span><span style="margin-left:20px;"><asp:ImageButton ID="ibGlobal" runat="server" ImageUrl="~/images/arrow_orb_plus_16.png" OnClick="btnExpress_GlobalInsert_Click" ToolTip="Click to Open the Global Insert Express Window" ImageAlign="Bottom"></asp:ImageButton> <asp:LinkButton ID="lbGlobal" runat="server" Text="Global" OnClick="btnExpress_GlobalInsert_Click" ToolTip="Click to Open the Global Insert Express Window" Font-Bold="true"></asp:LinkButton></span></div></div>
                <div style="float:right;">[<asp:LinkButton ID="lbProperty_Close" runat="server" Text="Close" OnClick="lbProperty_Close_Click"></asp:LinkButton>]</div>
                <div style="clear:both;"></div>
                <asp:HiddenField ID="hfPropertyPK" runat="server" />
                <hr />
                <asp:Panel ID="pnlGlobalInsert" runat="server" Visible="false">
                    <div class="NestedDivLevel03" style="margin-bottom:10px;">
                        <div style="float:left;"><span class="fsM B">Global Insert >> Express</span></div>
                        <div style="float:right;">[<asp:LinkButton ID="lbGlobal_Insert_Close" runat="server" Text="Close Global Insert" OnClick="lbGlobal_Insert_Close_Click"></asp:LinkButton>]</div>
                        <div style="clear:both;"></div>
                        <hr />
                        <uc1:UC_Express_Global_Insert ID="UC_Express_Global_Insert1" runat="server" />
                    </div>
                </asp:Panel>
                <asp:FormView ID="fvProperty" runat="server" Width="100%" OnModeChanging="fvProperty_ModeChanging" OnItemUpdating="fvProperty_ItemUpdating" 
                                OnItemInserting="fvProperty_ItemInserting" >
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Property</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" 
                                            CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbProperty_Close" 
                                            runat="server" Text="Close" OnClick="lbProperty_Close_Click"></asp:LinkButton>] <span class="PK_Created">
                                            <%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_property"), Eval("created"), Eval("created_by"), Eval("modified"), 
                                            Eval("modified_by")) %>
                                        </span></div>
                                    <%--    <hr />
                                            <ut:WACUT_Associations runat="server" ID="WACUT_Associations" AssociationType="Property" />
                                        <hr />--%>
                                        <table class="tp3">
                                            <tr><td class="B taR">Address:</td><td><%# Eval("address") %></td></tr>
                                            <tr><td class="B taR">Address2:</td><td><%# WACGlobal_Methods.SpecialText_Global_Address2(Eval("address2"), Eval("fk_address2Type_code")) %></td></tr>
                                            <tr><td class="B taR">City:</td><td><%# Eval("city") %></td></tr>
                                            <tr><td class="B taR">State:</td><td><%# Eval("state") %></td></tr>
                                            <tr><td class="B taR">Zip:</td><td><%# Eval("fk_zipcode") %></td></tr>
                                            <tr><td class="B taR">Zip4:</td><td><%# Eval("zip4") %></td></tr>
                                            <tr><td class="B taR">NY County:</td><td><%# Eval("list_countyNY.county") %></td></tr>
                                            <tr><td class="B taR">NY Township:</td><td><%# Eval("list_townshipNY.township") %></td></tr>
                                            <tr><td class="B taR">Basin:</td><td><%# Eval("list_basin.basin") %></td></tr>
                                            <tr><td class="B taR">Subbasin:</td><td><%# Eval("list_subbasin.subbasin") %></td></tr>
                                        </table>
                                        <hr />
                                  
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Property</span> [<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton> | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>] <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_property"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by")) %></span></div>
                                        <hr />
                                        <table class="tp3">
                                             <tr class="TaT"><td class="B taR">Address Line 1:</td><td><asp:TextBox ID="tbAddress1" runat="server" Text='<%# Bind("address") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">Address Line 2:</td><td><asp:TextBox ID="tbAddress2" runat="server" Text='<%# Bind("address2") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">City:</td><td><asp:TextBox ID="tbCity" runat="server" Text='<%# Bind("city") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">State:</td><td><asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">Zip:</td><td><asp:DropDownList ID="ddlZip" runat="server" OnSelectedIndexChanged="ddlZip_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">Zip4:</td><td><asp:TextBox ID="tbZip4" runat="server" Text='<%# Bind("zip4") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">NY County:</td><td><asp:DropDownList ID="ddlCounty" runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">NY Township:</td><td><asp:DropDownList ID="ddlTownshipNY" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">Basin:</td><td><asp:DropDownList ID="ddlBasin" runat="server" OnSelectedIndexChanged="ddlBasin_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">SubBasin</td><td><asp:DropDownList ID="ddlSubbasin" runat="server"></asp:DropDownList></td></tr>

                                        </table>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Property</span> [<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="Insert" Text="Insert"></asp:LinkButton> | 
                                            <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>]
                                        </div>
                                        <hr />
                                         <table class="tp3">
                                            <tr class="TaT"><td class="B taR">Address Line 1:</td><td><asp:TextBox ID="tbAddress1" runat="server" Text='<%# Bind("address") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">Address Line 2:</td><td><asp:TextBox ID="tbAddress2" runat="server" Text='<%# Bind("address2") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">City:</td><td><asp:TextBox ID="tbCity" runat="server" Text='<%# Bind("city") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">State:</td><td><asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">Zip:</td><td><asp:DropDownList ID="ddlZip" runat="server" OnSelectedIndexChanged="ddlZip_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">Zip4:</td><td><asp:TextBox ID="tbZip4" runat="server" Text='<%# Bind("zip4") %>'></asp:TextBox></td></tr>
                                            <tr class="TaT"><td class="B taR">NY County:</td><td><asp:DropDownList ID="ddlCounty" runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">NY Township:</td><td><asp:DropDownList ID="ddlTownshipNY" runat="server"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">Basin:</td><td><asp:DropDownList ID="ddlBasin" runat="server" OnSelectedIndexChanged="ddlBasin_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                                            <tr class="TaT"><td class="B taR">SubBasin</td><td><asp:DropDownList ID="ddlSubbasin" runat="server"></asp:DropDownList></td></tr>

                                        </table>
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
        
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:LinkButton ID="lbHiddenExpress_Property" runat="server"></asp:LinkButton>
    <ajtk:ModalPopupExtender ID="mpeExpress_Property" runat="server" TargetControlID="lbHiddenExpress_Property" PopupControlID="pnlExpress_Property" BackgroundCssClass="ModalPopup_BG">
    </ajtk:ModalPopupExtender>
</div>

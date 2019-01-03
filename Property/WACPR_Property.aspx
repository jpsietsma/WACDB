<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="~/Property/WACPR_Property.aspx.cs" Inherits="Property_WACPR_Property" %>
<%@ Register Src="~/Utility/wacut_associations.ascx" TagPrefix="ut" TagName="WACUT_Associations" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc2" %>
<%@ Register src="~/UserControls/UC_SearchByAddress.ascx" tagname="UC_SearchByAddress" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_Organization.ascx" tagname="UC_Express_Organization" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_Participant.ascx" tagname="UC_Express_Participant" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Express_PageButtons.ascx" tagname="UC_Express_PageButtons" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_DropDownListByAlphabet.ascx" tagname="UC_DropDownListByAlphabet" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <div style="float:left;" class="fsXL B">Property</div>
                    <div style="float:right;" class="B"><asp:HyperLink ID="hlProperty_Help" runat="server" Target="_blank"></asp:HyperLink></div>
                    <div style="clear:both;"></div>
                </div>
                <uc2:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <asp:UpdatePanel ID="upPropertySearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <div>
                            <div style="float:left;" class="B">
                                <asp:LinkButton ID="lbProperty_Add" runat="server" Text="Add New Property" OnClick="lbProperty_Add_Click" Font-Bold="True"></asp:LinkButton>
                            </div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="SearchDivOuter">
                            <div class="SearchDivContentContainer">
                                <div class="SearchDivContent">
                                    <div style="float:left;">Search Options:</div>
                                    <div style="float:right; font-weight:normal;">[<asp:LinkButton ID="lbProperty_Search_ReloadReset" runat="server" Text="Reload/Reset Search Options" OnClick="lbProperty_Search_ReloadReset_Click"></asp:LinkButton>]</div>
                                    <div style="clear:both;"></div>
                                </div>
                                <div class="SearchDivInner">
                                    <div>
                                        <div>
                                            <div style="float:left;"><asp:LinkButton ID="lbProperty_All" runat="server" Text="Search for All Properties" OnClick="lbProperty_All_Click" Font-Bold="True"></asp:LinkButton></div>
                                            <div style="float:left; margin-left:20px;">
                                                <span class="B">All or Part of Address Text:</span>
                                                <asp:Panel ID="pnlProperty_Search_AddressText" runat="server" DefaultButton="btnProperty_Search_AddressText">
                                                    <asp:TextBox ID="tbProperty_Search_AddressText" runat="server"></asp:TextBox> <asp:Button ID="btnProperty_Search_AddressText" runat="server" Text="Search" OnClick="btnProperty_Search_AddressText_Click" />
                                                </asp:Panel>
                                            </div>
                                            <div style="float:left; margin-left:20px;"><div class="B">Zip Code:</div><asp:DropDownList ID="ddlProperty_Search_Zip" runat="server" OnSelectedIndexChanged="ddlProperty_Search_Zip_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                            <div style="float:left; margin-left:20px;"><div class="B">NY Township:</div><asp:DropDownList ID="ddlProperty_Search_Township" runat="server" OnSelectedIndexChanged="ddlProperty_Search_Township_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                            <div style="float:left; margin-left:20px;"><div class="B">NY County:</div><asp:DropDownList ID="ddlProperty_Search_County" runat="server" OnSelectedIndexChanged="ddlProperty_Search_County_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                            <div style="clear:both;"></div>

                                        </div>
                                        <hr />
                                        <div>
                                            <div style="float:left;"><uc1:UC_DropDownListByAlphabet ID="UC_DropDownListByAlphabet_Search_Participant" runat="server" StrParentCase="PARTICIPANT_PERSON_SEARCH" StrEntityType="PARTICIPANT_PERSON_SEARCH" ShowOrganization="true" /></div>
                                            <div style="float:left; margin-left:20px;"><div class="B">Farm Business:</div><asp:DropDownList ID="ddlProperty_Search_FarmBusiness" runat="server" OnSelectedIndexChanged="ddlProperty_Search_FarmBusiness_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                                            <div style="clear:both;"></div>
                                        </div>
                                        <hr />
                                        <uc1:UC_SearchByAddress ID="UC_SearchByAddress1" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="progProperty" runat="server" AssociatedUpdatePanelID="upPropertySearch" DisplayAfter="1000">
                    <ProgressTemplate><div class="fsL I cGray">Data Loading... Please Wait...</div></ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="upProperty" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-bottom:5px;"><asp:Label ID="lblCount" runat="server" CssClass="fsM B I"></asp:Label></div>
                        <asp:GridView ID="gvProperty" Width="100%" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gvProperty_SelectedIndexChanged" OnPageIndexChanging="gvProperty_PageIndexChanging" OnSorting="gvProperty_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View" />
                                <asp:BoundField HeaderText="Address" DataField="address" SortExpression="address" />
                                <asp:BoundField HeaderText="City" DataField="city" SortExpression="city" />
                                <asp:BoundField HeaderText="State" DataField="state" SortExpression="state" />
                                <asp:BoundField HeaderText="Zip" DataField="fk_zipcode" SortExpression="fk_zipcode" />
                                <asp:TemplateField HeaderText="County" SortExpression="list_countyNY.county">
                                    <ItemTemplate>
                                        <%# Eval("list_countyNY.county")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Township" SortExpression="list_townshipNY.township">
                                    <ItemTemplate>
                                        <%# Eval("list_townshipNY.township")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top:10px;">
                            <asp:FormView ID="fvProperty" runat="server" Width="100%" OnModeChanging="fvProperty_ModeChanging" OnItemUpdating="fvProperty_ItemUpdating" 
                                OnItemInserting="fvProperty_ItemInserting" OnItemDeleting="fvProperty_ItemDeleting">
                                <ItemTemplate>
                                    <div style="border:solid 1px #999999; padding:5px; background-color:#F9F9F9;">
                                        <div><span class="fsM B">Property</span> [<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" 
                                            CommandName="Edit" Text="Edit"></asp:LinkButton> | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" 
                                            CommandName="Delete" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> | <asp:LinkButton ID="lbProperty_Close" 
                                            runat="server" Text="Close" OnClick="lbProperty_Close_Click"></asp:LinkButton>] <span class="PK_Created">
                                            <%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_property"), Eval("created"), Eval("created_by"), Eval("modified"), 
                                            Eval("modified_by")) %>
                                        </span></div>
                                        <hr />
                                            <ut:WACUT_Associations runat="server" ID="WACUT_Associations" AssociationType="Property" />
                                        <hr />
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
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbProperty_Add" />
                        <asp:AsyncPostBackTrigger ControlID="lbProperty_Search_ReloadReset" />
                        <asp:AsyncPostBackTrigger ControlID="lbProperty_All" />
                        <asp:AsyncPostBackTrigger ControlID="btnProperty_Search_AddressText" />
                        <asp:AsyncPostBackTrigger ControlID="ddlProperty_Search_Zip" />
                        <asp:AsyncPostBackTrigger ControlID="ddlProperty_Search_Township" />
                        <asp:AsyncPostBackTrigger ControlID="ddlProperty_Search_County" />
                        <asp:AsyncPostBackTrigger ControlID="ddlProperty_Search_FarmBusiness" />
                    </Triggers>
                </asp:UpdatePanel>

                <uc1:UC_Express_Participant ID="UC_Express_Participant1" runat="server" />
                <uc1:UC_Express_Organization ID="UC_Express_Organization1" runat="server" />

            </div>
        </div>
    </div>
</asp:Content>


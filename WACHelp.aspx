<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACHelp.aspx.cs" Inherits="WACHelp" %>
<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register src="~/UserControls/UC_Explanation.ascx" tagname="UC_Explanation" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div class="fsXL B">Help</div>
                <uc2:UC_Explanation ID="UC_Explanation1" runat="server" />
                <uc1:UC_Advisory ID="UC_Advisory1" runat="server" />
                <hr />
                <div class="DivBoxYellow" style="margin:10px 0px 10px 0px;">
                    <div class="B">In-Depth Help Documents</div>
                    <hr />
                    <asp:UpdatePanel ID="upHelp_Docs" runat="server">
                        <ContentTemplate>
                            <div style="margin-bottom:5px;">
                                <asp:LinkButton ID="lbHelp_Global" runat="server" Text="[+] Global Help Documents" OnClick="lbHelp_Global_Click" Font-Bold="True"></asp:LinkButton>
                                <asp:Panel ID="pnlHelp_Global" runat="server" Visible="false">
                                    <asp:ListView ID="lvHelp_Global" runat="server">
                                        <LayoutTemplate>
                                            <ul><li id="itemPlaceholder" runat="server"></li></ul>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li style="padding:3px;"><a href='<%# Eval("StrDocPath") %>' target="_blank"><%# Eval("StrDocName") %></a></li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div style="margin-bottom:5px; display:none;">
                                <asp:LinkButton ID="lbHelp_Forestry" runat="server" Text="[+] Forestry Help Documents" OnClick="lbHelp_Forestry_Click" Font-Bold="True"></asp:LinkButton>
                                <asp:Panel ID="pnlHelp_Forestry" runat="server" Visible="false">
                                    <asp:ListView ID="lvHelp_Forestry" runat="server">
                                        <LayoutTemplate>
                                            <ul><li id="itemPlaceholder" runat="server"></li></ul>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li style="padding:3px;"><a href='<%# Eval("StrDocPath") %>' target="_blank"><%# Eval("StrDocName") %></a></li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div style="margin-bottom:5px;">
                                <asp:LinkButton ID="lbHelp_Agriculture" runat="server" Text="[+] Agriculture Help Documents" OnClick="lbHelp_Agriculture_Click" Font-Bold="True"></asp:LinkButton>
                                <asp:Panel ID="pnlHelp_Agriculture" runat="server" Visible="false">
                                    <asp:ListView ID="lvHelp_Agriculture" runat="server">
                                        <LayoutTemplate>
                                            <ul><li id="itemPlaceholder" runat="server"></li></ul>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li style="padding:3px;"><a href='<%# Eval("StrDocPath") %>' target="_blank"><%# Eval("StrDocName") %></a></li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div style="margin-bottom:5px;">
                                <asp:LinkButton ID="lbHelp_Marketing" runat="server" Text="[+] Marketing Help Documents" OnClick="lbHelp_Marketing_Click" Font-Bold="True"></asp:LinkButton>
                                <asp:Panel ID="pnlHelp_Marketing" runat="server" Visible="false">
                                    <asp:ListView ID="lvHelp_Marketing" runat="server">
                                        <LayoutTemplate>
                                            <ul><li id="itemPlaceholder" runat="server"></li></ul>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li style="padding:3px;"><a href='<%# Eval("StrDocPath") %>' target="_blank"><%# Eval("StrDocName") %></a></li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div style="margin-bottom:5px; display:none;">
                                <asp:LinkButton ID="lbHelp_Easements" runat="server" Text="[+] Easements Help Documents" OnClick="lbHelp_Easements_Click" Font-Bold="True"></asp:LinkButton>
                                <asp:Panel ID="pnlHelp_Easements" runat="server" Visible="false">
                                    <asp:ListView ID="lvHelp_Easements" runat="server">
                                        <LayoutTemplate>
                                            <ul><li id="itemPlaceholder" runat="server"></li></ul>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li style="padding:3px;"><a href='<%# Eval("StrDocPath") %>' target="_blank"><%# Eval("StrDocName") %></a></li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">Overview</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>The purpose of the site is to provide an easy to use and intuitive way for database data insertion, editing, and deletion.</p>
                    </div>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">Navigating the Site</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>Most of the navigation is done on the top right side of the screen. There are three rows of links. The top row is used for general
                        site navigation as well as linking to other web sites. The middle row contains the links to all of the global sections of the site.
                        These sections are shared by the different areas of the site. Global sections is covered in the next help topic.</p>
                    </div>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">How We See the Data</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>Data is typically viewed using a query to the database. These 'Search' options return data that matches the criteria in a grid format. 
                        You can have returned datasets of zero, one, or many records. From the records grid, you can 'View' a single record. Viewing a record shows 
                        you the table and related subtables for that record. From here, you can edit and delete the record, or insert information into the 
                        subtables for the record. Prior to searching, you can also 'Insert' a new record into the main table for the section you are viewing. 
                        To sum up, with the database there are the capabilities to View, Edit, Insert, or Delete.</p>
                    </div>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">Global Sections</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>The global sections are sections that are utilized by all of the main sections, such as Agriculture. For example, if you create a 
                        new Property, that property object can be used by Forestry, Agriculture, Marketing, and/or Easements. These shared resources make it 
                        easy to make changes because it is done in just one place.</p>
                        <p><span class="B">Communications</span> - Communications are area codes adn phone numbers, for use by participants and venues.</p>
                        <p><span class="B">Organizations</span> - Organizations are entities that have a name and address. Organizations are tied to participants.</p>
                        <p><span class="B">Participants</span> - Participants are entities that are typically represented as 'people' (or in some cases, 
                        organizations). Participants are the backbone of the database. Participants can be tied to organizations and property, as well as each of 
                        the main sections (Forestry, Agriculture, Marketing, Easements). Participants can have types and interests as well.</p>
                        <p><span class="B">Property</span> - Property are addresses that constitute a physical or real world location. A single, specific Property 
                        can be assigned to, ie, used by, one or more participants, organizations or venues.</p>
                        <p><span class="B">Tax Parcels</span> - Tax Parcels are the IDs for a parcel of land. Tax Parcels can have one or more owners.</p>
                        <p><span class="B">Venues</span> - Venues are buildings or gathering areas where events are held. Venues are tied to property and events.</p>
                    </div>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">Forestry</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>To Be Expanded</p>
                    </div>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">Agriculture</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>To Be Expanded</p>
                    </div>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">Marketing</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>The Marketing sector consists of Events and the Pure Catskills membership. Events can be sponsored by any WAC Sector (eg, Forestry, Agriculture or Easements), as well as by Marketing itself and Cornell Co-operative Extension (CCE).</p>
                        <p>Events are logically ordered by a common name, a venue (location where the event is held) and a scheduled date of the event.</p>
                        <p>The Pure Catskills records represent the active membership of the Pure Catskills website (<a href="http://www.purecatskills.org" target="_blank">http://www.purecatskills.org</a>) and reflects the data appearing in the annual Pure Catskills guide published by Marketing.</p>
                    </div>
                </div>
                <div style="margin:0px 0px 10px 0px;">
                    <div class="fsM B">Easements</div>
                    <div class="taJ" style="margin:0px 20px 0px 20px;">
                        <p>To Be Expanded</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


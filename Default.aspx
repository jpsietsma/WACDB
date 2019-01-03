<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register src="~/UserControls/UC_Advisory.ascx" tagname="UC_Advisory" tagprefix="uc1" %>
<%@ Register Src="~/Utility/WACUT_AttachedDocumentViewer.ascx" TagPrefix="uc1" TagName="WACUT_AttachedDocumentViewer" %>

<%--<%@ Register src="~/UserControls/UC_DocumentArchive.ascx" tagname="UC_DocumentArchive" tagprefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
<div class="divContentClass">
<div style="margin:0px 5px 0px 5px;">
    <br />
    <div style="float:left; width:543px;">
        <div class="fsM I B" style="text-align:justify; margin: 0px 0px 10px 0px;">The WAC Database is an integrated, relational database bringing together the four main segments of WAC’s primary responsibilities: Agriculture, Forestry, Easements and Farm To Market.</div>
        <div id="silverlightControlHost">
            <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="360" height="270">
	            <param name="source" value="ClientBin/WACDBSlideShow.xap"/>
	            <param name="onError" value="onSilverlightError" />
	            <param name="background" value="white" />
	            <param name="minRuntimeVersion" value="3.0.40818.0" />
	            <param name="autoUpgrade" value="true" />
	            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration:none">
		            <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
	            </a>
            </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe>
        </div>
        <div style="text-align:justify; margin:10px 5px 0px 5px;">
            <div style="margin: 10px 0px 10px 0px;">
                <div class="B">TOP-MOST LINKS</div>
                <%--The top-most links of each screen (Home, Participants, Property, Venues, Contact Us, Help and Logout) are “global” links and available to all Users depending on each individual User’s permissions. Assigned permissions determine whether a User can only view data, add new records and/or update existing data.--%>
                The top-most links of each screen (Home, Map, Reports, Contact Information, Help and Logout) are links and available to all Users. Home is the screen you are currently on; clicking Map will take you to the main screen of the mapping/GIS component of the website; Contact Information offers information on WAC administrators; Help offers detailed PDFs on how to work within the various Sectors and tabs of the site; and Logout will close out your current work session.
            </div>
            <div style="margin: 10px 0px 10px 0px;">
                <div class="B">GLOBAL DATA LINKS</div>
                The "global data" links -- Organizations, Participants, Property, Tax Parcels and Venues -- are located immediately below the top-most links. Access to the global data is generally available to all users for viewing but the additional actions of Edit, Insert and Delete and available to Users depending on each individual User’s permissions. Assigned permissions determine whether a User can only view global data, add new records, update existing data or deleting existing records.
            </div>
            <div style="margin: 10px 0px 10px 0px;">
                <div class="B">PRIMARY SECTIONS</div>
                The four primary sections (Agriculture, Forestry, Easements and Farm To Market) are generally set so that immediate members of any group can add and/or edit their own Group-specific data – and only view the other groups’ data. This allows everyone to access other Sectors’ data but only Users within that Sector to actually effect changes (Note: Some exceptions may appear to this policy due to other issues as determined by each Sector’s Project Manager.).  
            </div>
            <div style="margin: 10px 0px 10px 0px;">
                <div class="B">LIST MANAGEMENT</div>
                There are many lists attached to this database which feed the numerous pull-down menus found throughout this application.  Unless a screen is offered to maintain a specific list, you will have to contact the Database Group to make corrections/additions to these lists. Contact <a href="mailto:sethhersh@nycwatershed.org">Seth Hersh</a> to make any changes you require for any list.
            </div>
            <div style="margin: 10px 0px 10px 0px;">
                <div class="B">GIS/MAPPING</div>
                GIS issues are handled by WAC’s GIS coordinator, James Samek. All mapping components must be finalized and approved by James. Should you require any help, guidance or training whatsoever regarding any aspect of GIS found throughout this site, please contact <a href="mailto:jsamek@nycwatershed.org">James Samek</a> directly to manage these issues, as well as receive guidance in preparing any GIS files for inclusion on the site.
            </div>
            <div style="margin: 10px 0px 10px 0px;">
                <div class="B">NEW FEATURES/ADDITIONS</div>
                New features of any sort will be taken under advisement by the Database Group. Please submit any requests to modify this application to <a href="mailto:sethhersh@nycwatershed.org">Seth Hersh</a>.
            </div>
            <div style="margin: 10px 0px 10px 0px;">
                <div class="B">BUGS</div>
                Any errors and bugs you encounter should be immediately reported to <a href="mailto:sethhersh@nycwatershed.org">Seth Hersh</a>. When reporting a bug or other similar type of issue, please include the URL address of the screen you were on when the bug occurred and provide an explanation of what occurred.
            </div>
        </div>
    </div>
    <div style="float:right; width:415px;">
        <div class="NestedDivLevel00">
            <div><asp:Image ID="imgWACLogo" runat="server" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px" /></div>
            <div><uc1:UC_Advisory ID="UC_Advisory1" runat="server" /></div>
            <div style="margin:10px 0px 10px 0px;">
                <uc1:WACUT_AttachedDocumentViewer runat="server" ID="WACUT_AttachedDocumentViewer" SectorCode="HOME" />
                <%--<div class="fsL B">Documents</div>
                <div class="DivBoxOrange">
                    <uc1:UC_DocumentArchive ID="UC_DocumentArchive_H" runat="server" StrArea="H" StrAreaSector="H_OVER" BoolShowPanelTop="false" />
                </div>--%>
            </div>
            <div style="margin:0px 0px 10px 0px;">
                <div class="fsL B">Related Software</div>
                <div style="margin:3px 0px 3px 20px">
                    <div><a href="http://potok.wac.local/wac_fameapp/publish.htm" target="_blank">FAME Windows Application</a></div>
                    <div class="taJ">The FAME Application is a piece of stand-alone software, developed by WAC, that you install on your computer. The FAME Application provides additional functionality not available or hindered in a web environment.</div>
                </div>
            </div>
            <div>
                <div class="fsL B">Links</div>
                <ul class="fsM">
                <div class="fsM B">Software-related GIS Sites:</div>
                <li><a href="http://www.esri.com">ESRI</a></li>
                <li><a href="http://www.arcgis.com">ArcGIS Online</a></li>
                <br />
                <div class="fsM B">County-related GIS Sites:</div>
                <li><a href="http://www.co.delaware.ny.us/departments/tax/taxmap.htm" target="_blank">Delaware County Tax Mapping</a></li>
                <li><a href="http://www.giswebhost.org/delaware/main.asp" target="_blank">Delaware County COMIT</a></li>
                <li><a href="http://geoaccess.co.dutchess.ny.us/parcelaccess/" target="_blank">Dutchess County Parcel Access</a></li>
                <li><a href="http://gis.greenegovernment.com/giswebmap/default.aspx" target="_blank">Greene County GIS Web Map</a></li>
                <li><a href="http://www.putnamcountyny.com/realproperty/eparcel.htm" target="_blank">Putnam County eParcel</a></li>
                <li><a href="http://www.schohariecounty-ny.gov/remote/RPSSearchMgr?menuItem=New" target="_blank">Schoharie County Real Property Parcel Search</a></li>
                <li><a href="http://www.co.sullivan.ny.us/index.asp?orgid=542&storyTypeID=&sid=&" target="_blank">Sullivan County Property Assessment Data</a></li>
                <li><a href="http://gis.co.ulster.ny.us/pviewer/" target="_blank">Ulster County Parcel Viewer</a></li>
                <li><a href="http://giswww.westchestergov.com/" target="_blank">Westchester County GIS</a></li>
                <br />
                <li><a href="http://magic.lib.uconn.edu/connecticut_data_counties_fairfield.html" target="_blank">Fairfield County, CT GIS Data</a></li>
                </ul>
            </div>
            <div>
                <div class="fsL B">Updates</div>
                <asp:ListView ID="lvUpdates" runat="server" DataSourceID="xdsUpdates">
                    <LayoutTemplate>
                        <ul class="fsM">
                        <li id="itemPlaceholder" runat="server"></li>
                        </ul>
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                        <div class="I" style="margin:20px;">There are not any updates at this time.</div>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <li style="margin:0px 0px 20px 0px;"><span class="B"><%# Eval("date") %></span> <%# Eval("text") %></li>
                    </ItemTemplate>
                </asp:ListView>
                <asp:XmlDataSource ID="xdsUpdates" runat="server" DataFile="WACUpdates.xml" XPath="UPDATES/UPDATE[@active='true']"></asp:XmlDataSource>
            </div>
        </div>
    </div>
    <div style="clear:both;"></div>
</div>
</div>
</asp:Content>


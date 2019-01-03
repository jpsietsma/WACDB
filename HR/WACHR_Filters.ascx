<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACHR_Filters.ascx.cs"
     Inherits="WACHR_HRFilters" %>

 <asp:UpdatePanel ID="upHR_WACEmployeeFilter" runat="server" UpdateMode="Conditional" EnableViewState="true" >
     <ContentTemplate>
        <div>
            <asp:LinkButton ID="lbHR_WACEmployee_Insert" runat="server" Text="Add a WAC Employee" Font-Bold="true" 
                OnClick="lbHR_WACEmployee_Insert_Click"></asp:LinkButton>
        </div>
        <div class="SearchDivOuter">
            <div class="SearchDivContentContainer">
                <div class="SearchDivContent">
                    <div style="float:left;">Filter Options:</div>
                    <div style="float:right;">[<asp:LinkButton ID="lbResetFilters" runat="server" Text="Reset Filters" 
                        OnClick="lbResetFilters_Click"></asp:LinkButton>]</div>
                    <div style="clear:both;"></div>
                </div>
                <div class="SearchDivInner">
                     <table cellpadding="3">
                        <tr valign="top">
                            <td class="taR B">Employee:</td><td><asp:DropDownList ID="ddlFilter_Employee" runat="server" 
                                OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            <td class="taR B">Field Staff:</td><td><asp:DropDownList ID="ddlFilter_FieldStaff" runat="server" 
                                OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            <td class="taR B">Location:</td><td><asp:DropDownList ID="ddlFilter_Location" runat="server" 
                                OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                        </tr>
                        </table>
                        <table cellpadding="3">
                        <tr valign="top">
                            <td class="taR B">EEO:</td><td><asp:DropDownList ID="ddlFilter_EEOClass" runat="server" 
                                OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                             <td class="taR B">Sector:</td><td><asp:DropDownList ID="ddlFilter_HRSector" runat="server" 
                            OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>   
                            <td class="taR B">SLT:</td><td><asp:DropDownList ID="ddlFilter_SLT" runat="server" 
                                OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                        </tr>
                        </table>
                         <table cellpadding="3">
                        <tr valign="top">
                        <td class="taR B">Classification:</td><td><asp:DropDownList ID="ddlFilter_HRClass" runat="server" 
                            OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                             <td class="taR B">Position:</td><td><asp:DropDownList ID="ddlFilter_Position" runat="server" 
                                OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                  
                        </tr>
                    </table>
                    <div style="text-align:right"><asp:CheckBox ID="cbActiveEmployee" runat="server" Checked="true" 
                        Text="Check to include only active employees" OnCheckedChanged="cbActiveEmployee_CheckChanged" AutoPostBack="true" /></div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>



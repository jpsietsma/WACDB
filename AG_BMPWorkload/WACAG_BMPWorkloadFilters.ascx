<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_BMPWorkloadFilters.ascx.cs"
    Inherits="AG_BMPWorkload_WACAG_BMPWorkloadFilters" %>
<asp:UpdatePanel ID="upWACAG_BMPWorkloadFilter" runat="server" UpdateMode="Conditional"
    EnableViewState="true">
    <ContentTemplate>
        <div class="SearchDivOuter">
            <div class="SearchDivContentContainer">
                <div class="SearchDivContent">
                    <div style="float: left;">
                        Filter Options:</div>
                    <div style="float: right;">
                        [<asp:LinkButton ID="lbResetFilters" runat="server" Text="Reset Filters" OnClick="lbResetFilters_Click"></asp:LinkButton>]</div>
                    <div style="clear: both;">
                    </div>
                </div>
                <div class="SearchDivInner">
                    <table cellpadding="5">
                        <tr valign="top">
                            <td class="taR B">
                                Workload:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilter_Workload" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="taR B">
                                Funding:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilter_Funding" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="taR B">
                                Agency:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilter_Agency" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="taR B">
                                Group:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilter_Group" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="taR B">
                                Planner:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilter_Planner" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="taR B">
                                Technician:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilter_Technician" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="taR B">
                                Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilter_Status" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="taR B">
                                Owner:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlFilter_Owner" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

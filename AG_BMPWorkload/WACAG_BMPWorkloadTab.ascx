<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WACAG_BMPWorkloadTab.ascx.cs" Inherits="AG_BMPWorkload_WACAG_BMPWorkloadTab" %>

                       <div style="margin:0px 0px 5px 0px;">
                            <div style="float:left;"><asp:Label ID="lblCount" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></div>
                            <div style="float:right;"><asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></div>
                            <div style="clear:both;"></div>
                        </div>
                        <asp:GridView ID="gv" runat="server" Width="100%" BackColor="White" AllowPaging="True" PageSize="10" OnSelectedIndexChanged="gv_SelectedIndexChanged" 
                        OnPageIndexChanging="gv_PageIndexChanging" OnSorting="gv_Sorting" AllowSorting="True" CellPadding="5" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PagerSettings-Mode="NumericFirstLast" HeaderStyle-Wrap="False" AutoGenerateColumns="False">
                            <HeaderStyle BackColor="#BBBBAA" />
                            <RowStyle BackColor="#EEEEDD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#DDDDCC" />
                            <EditRowStyle BackColor="#FFFFAA" />
                            <PagerStyle BackColor="#BBBBAA" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="View" />
                                <asp:TemplateField HeaderText="Workload" SortExpression="year">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlWorkload" runat="server" OnSelectedIndexChanged="ddlWorkload_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Funding" SortExpression="bmp_ag_workload.fk_agWorkloadFunding_code" ItemStyle-Width="75px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlFunding" runat="server" OnSelectedIndexChanged="ddlFunding_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                     <%--   <%# Eval("fk_agWorkloadFunding_code")%>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BMP Description" SortExpression="bmp_ag.bmp_nbr">
                                    <ItemTemplate>
                                        <%# Eval("bmp_ag.bmp_nbr")%> <%# Eval("bmp_ag.description")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="list_statusBMPWorkload.status">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                   <%--             <asp:TemplateField HeaderText="Priority" SortExpression="priority">
                                    <ItemTemplate>
                                        <%# Eval("priority") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Owner" SortExpression="bmp_ag.farmBusiness.ownerStr_dnd">
                                    <ItemTemplate>
                                        <%# Eval("bmp_ag.farmBusiness.ownerStr_dnd")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agency" SortExpression="list_agency.agency">
                                    <ItemTemplate>
                                        <%# Eval("list_agency.agency") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group" SortExpression="bmp_ag.farmBusiness.fk_groupPI_code">
                                    <ItemTemplate>
                                        <%# Eval("bmp_ag.farmBusiness.fk_groupPI_code")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Planner">
                                    <ItemTemplate>
                                        <%# WACGlobal_Methods.SpecialText_Agriculture_BMP_Workload_Planner(Eval("bmp_ag.farmBusiness")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Techs">
                                    <ItemTemplate>
                                        <asp:ListView ID="lvTechnicians" runat="server" DataSource='<%# WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(Eval("bmp_ag_workloadSupports"), "T") %>'>
                                            <ItemTemplate>
                                                <div><%# Eval("list_designerEngineer.nickname")%></div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPercentage" runat="server" OnSelectedIndexChanged="ddlPercentage_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/HR/WACHR_EmployeeTab.ascx.cs" 
 Inherits="WACHR_HREmployeeTab" %>

<asp:UpdatePanel ID="upHRMaster" runat="server">
    <ContentTemplate>
       <div style="margin:5px 0px 5px 0px;"><asp:Label ID="lblCount" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></div>
            <asp:GridView ID="gvHR_WACEmployees" runat="server" SkinID="gvSkin" AllowPaging="True" PageSize="10" 
            OnSelectedIndexChanged="gvHR_WACEmployees_SelectedIndexChanged" OnPageIndexChanging="gvHR_WACEmployees_PageIndexChanging" 
            OnSorting="gvHR_WACEmployees_Sorting" AllowSorting="True" PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="False">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="View" />
                    <asp:BoundField HeaderText="Employee" DataField="Employee" SortExpression="Employee" />
                    <asp:BoundField HeaderText="Location" DataField="Location" SortExpression="Location" />
                    <asp:BoundField HeaderText="SLT" DataField="SLT" SortExpression="SLT" />
                    <asp:BoundField HeaderText="Field Staff" DataField="FieldStaff" SortExpression="FieldStaff" />
                    <asp:BoundField HeaderText="Current Position" DataField="Position" SortExpression="Position" />
                    <asp:BoundField HeaderText="Start Date" DataField="StartDate" SortExpression="StartDate" DataFormatString="{0:d}" />
                </Columns>
            </asp:GridView>
            <hr />
    </ContentTemplate>
</asp:UpdatePanel>

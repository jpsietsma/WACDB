<%@ Page Title="" Language="C#" MasterPageFile="~/HR/HRMasterPage.master" AutoEventWireup="true" 
  CodeFile="~/HR/WACHR.aspx.cs" Inherits="HR_WACHR" %>

<%@ Register src="~/HR/WACHR_Filters.ascx" tagname="Filter" tagprefix="hruc" %>
<%@ Register src="~/HR/WACHR_EmployeeTab.ascx" tagname="EmployeeTab" tagprefix="hruc" %>
<%@ Register src="~/HR/WACHR_EmployeeDetail.ascx" tagname="EmployeeDetail" tagprefix="hruc" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HRFilters" Runat="Server">
    <hruc:Filter ID="Filters" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HRMaster" Runat="Server">
    <hruc:EmployeeTab ID="EmployeeMaster" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HRDetail" Runat="Server">
   <hruc:EmployeeDetail ID="EmployeeDetails" runat="server" />
</asp:Content>


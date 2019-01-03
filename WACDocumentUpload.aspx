<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WACDocumentUpload.aspx.cs" Inherits="WACDocumentUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Watershed Agricultural Council - FAME</title>
</head>
<body style="background-image:none;">
    <form id="form1" runat="server">
    <div style="padding:10px;">
        <div class="fsM B taC"><asp:Label ID="lblTitle" runat="server"></asp:Label></div>
        <hr />
        <div>
            <table cellpadding="3">
                <tr valign="top">
                    <td class="B taR">File Date:</td>
                    <td><div style="width:250px; padding:5px; background-color:#FFFFC0; border:solid 1px #CCCCCC;"><asp:Calendar ID="cal" runat="server"></asp:Calendar></div></td>
                </tr>
                <tr valign="top">
                    <td class="B taR">File:</td>
                    <td><asp:FileUpload id="fuFile" runat="server" Width="500px" /></td>
                </tr>
                <tr valign="top">
                    <td class="B taR">File Type:</td>
                    <td><asp:DropDownList ID="ddlAreaSectorFolder" runat="server"></asp:DropDownList></td>
                </tr>
                <tr valign="top">
                    <td class="B taR">WAC Form:</td>
                    <td><asp:DropDownList ID="ddlWACForm" runat="server"></asp:DropDownList></td>
                </tr>
                <tr valign="top">
                    <td class="B taR"><asp:Label ID="lblSpecialLevel3" runat="server" Visible="false"></asp:Label></td>
                    <td><asp:DropDownList ID="ddlSpecialLevel3" runat="server" Visible="false"></asp:DropDownList></td>
                </tr>
                <tr valign="top">
                    <td colspan="2" class="taC"><asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Label ID="lblMessage" runat="server"></asp:Label></td>
                </tr>
                 <tr>
                    <td colspan="2"><asp:Button ID="btnCloseWindow" runat="server" Text="Close Window" OnClientClick="window.close(); return false;" Visible="false" /></td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style="min-height:250px;">
            <div style="padding:5px;">
                <div>
                    <center>
                    <h1>Login to the Database</h1>
                   <%-- <h1>System maintenance in progress. FAME is unavailable. Expected time to be back in service 13 March 2017 11:00 AM</h1>--%>
                    <table>
                        <tr>
                            <td style="text-align:right;">Username:</td>
                            <td style="text-align:left;"><asp:TextBox ID="tbUN" runat="server" Width="150px" AutoCompleteType="DisplayName"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Password:</td>
                            <td style="text-align:left;"><asp:TextBox ID="tbPW" runat="server" TextMode="Password" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:Button id="btn" runat="server" Text="Login" OnClick="btn_Click"  /></td> 
                        </tr>
                    </table>
                  <%-- <div style="text-align:justify; width:600px; margin-top:20px;"><asp:Label ID="lbl" runat="server"></asp:Label></div>
                    <div class="I" style="text-align:justify; width:600px; margin-top:20px;"><B>Disclaimer:</B> The data provided on this site is prepared for the analysis of lands found within Watershed Agricultural Council responsible areas, and are compiled from data managed by WAC and its partner organizations. This data is for informational purposes only and should not be used for title search, property appraisal, survey, or for zoning verification. WAC and its partners assume no legal responsibility for the information contained in this data.</div>
                    --%>
                    </center>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


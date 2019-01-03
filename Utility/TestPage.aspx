<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="TestPage.aspx.cs" Inherits="Utility_TestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="Server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" runat="Server">
     <script type="text/javascript">
         $(document).ready(function () {
             var makesCache = {}, makesXhr;

             $('<%= string.Format("#{0}",tbParticipant.ClientID) %>').autocomplete({
                 source: function (request, response) {
                     var term = request.term;
                     if (term in makesCache) {
                         response(makesCache[term]);
                         return;
                     }
                     if (makesXhr != null) {
                         makesXhr.abort();
                     }
                     makesXhr = $.getJSON('WAC_WCFServices.WCFService.svc/GetParticipantOrgList', request, function (data, status, xhr) {
                         makesCache[term] = data.d;
                         if (xhr == makesXhr) {
                             response(data.d);
                             makesXhr = null;
                         }
                     });
                 }
             });
         });
    </script>

 <asp:literal ID="Literal1" runat="server">Participant/Organization (start typing to narrow search):</asp:literal><br />
 <asp:TextBox ID="tbParticipant" runat="server"></asp:TextBox>  
</asp:Content>


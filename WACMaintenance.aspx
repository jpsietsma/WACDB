<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="WACMaintenance.aspx.cs" Inherits="WACMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                 <div class="fsXL B">Maintenance</div>
                 <hr />
                 <div><asp:Label ID="lbl" runat="server"></asp:Label></div>
                 <hr />
                 <div><a href="WACAgriculture.aspx">Try Again</a></div>
            </div>
        </div>
    </div>
</asp:Content>


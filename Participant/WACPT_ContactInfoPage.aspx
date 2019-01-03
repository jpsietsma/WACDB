<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WACPT_ContactInfoPage.aspx.cs" Inherits="WACContact" %>

<%@ Register Src="~/Participant/WACPT_ContactInfoList.ascx" TagPrefix="uc1" TagName="WACPT_ContactInfoList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterMain" Runat="Server">
    <div class="divContentClass">
        <div style=" background-image:url(images/farm_o.jpg); background-repeat:no-repeat; min-height:250px;">
            <div style="padding:5px;">
                <div class="fsXL B">Contact Information</div>
                <uc1:WACPT_ContactInfoList runat="server" ID="WACPT_ContactInfoList" 
                    OnContentStateChanged="WACPT_ContactInfoList_ContentStateChanged"
                    DefaultVisibility="true" ViewStateMode="Disabled"  />
            </div>
        </div>
    </div>
</asp:Content>


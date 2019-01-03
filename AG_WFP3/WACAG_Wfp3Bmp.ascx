<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/AG_WFP3/WACAG_WFP3Bmp.ascx.cs" Inherits="AG_WACAG_Wfp3Bmp" %>

 <asp:FormView ID="fvAg_WFP3_BMP" runat="server" Width="100%" OnItemCommand="fvAg_WFP3_BMP_ItemCommand" >    
    <ItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="false" CommandName="UpdateMode" Text="Edit" ></asp:LinkButton> 
            | <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="DeleteData" Text="Delete" OnClientClick="return confirm_delete();"></asp:LinkButton> 
            | <asp:LinkButton ID="lbAg_WFP3_BMP_Close" runat="server" CommandName="CloseForm" Text="Close" ></asp:LinkButton>]
                 <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(
                        Eval("pk_form_wfp3_bmp"), Eval("created"), Eval("created_by"), Eval("modified"), Eval("modified_by"))%>

                 </span>
            </div>
            <hr />
            <table class="taT">
                <tr class="taT"><td class="B taR">BMP:</td><td><%# Eval("CompositBmpNum") %> <%# Eval("description") %> <span class="PK_Created">[FK: <%# Eval("pk_bmp_ag") %>]</span></td></tr>
                 <tr class="taT"><td class="B taR">Unit:</td><td><%# Eval("fk_unit_code")%></td></tr>
                <tr class="taT"><td class="B taR">Units Designed:</td><td><%# Eval("units_designed") %> <%# Eval("fk_unit_code")%></td></tr>                                                           
                <tr class="taT"><td class="B taR">Design Cost:</td><td><%# WACGlobal_Methods.Format_Global_Currency(Eval("design_cost")) %></td></tr>
               <tr class="taT"><td class="B taR">Units Completed:</td><td><asp:Label ID="lblUnitsCompleted" runat="server" Text='<%# Bind("units_completed") %>'></asp:Label></td></tr>
                <tr class="taT"><td class="B taR">Completed Cost:</td><td><asp:Label ID="lblCompletedCost" runat="server" Text=<%#Bind("final_cost") %>></asp:Label></td></tr>
                <tr class="taT"><td class="B taR">Created Programmatically:</td><td><%# WACGlobal_Methods.Format_Global_YesNo(Eval("createdProgrammatically_ro"))%></td></tr>
            </table>
        </div>
    </ItemTemplate>
    <EditItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="false" CommandName="UpdateData" Text="Update"></asp:LinkButton> 
            |   <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelUpdate" Text="Cancel"></asp:LinkButton>] 
                <span class="PK_Created"><%# WACGlobal_Methods.SpecialText_Global_PK_Creator_Modifier(Eval("pk_form_wfp3_bmp"), Eval("created"), 
                                     Eval("created_by"), Eval("modified"), Eval("modified_by"))%>

                </span>
            </div>
            <hr />
            <table class="taT">
                <tr class="taT"><td class="B taR">BMP:</td><td><asp:DropDownList ID="ddlBMP" runat="server" OnSelectedIndexChanged="ddlAg_WFP3_BMP_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                <tr class="taT"><td class="B taR">Units Designed:</td><td><asp:TextBox ID="tbUnitsDesigned" runat="server" Text='<%# Bind("units_designed") %>'></asp:TextBox> <asp:Label ID="lblAg_WFP3_BMP_Units" runat="server"></asp:Label></td></tr>
                <tr class="taT"><td class="B taR">Design Cost:</td><td><asp:TextBox ID="tbDesignCost" runat="server" Text='<%# Bind("design_cost") %>'></asp:TextBox></td></tr>          
                <tr class="taT"><td class="B taR">Units Completed:</td><td><asp:TextBox ID="tbUnitsCompleted" runat="server" Text='<%# Bind("units_completed") %>'></asp:TextBox></td></tr>
            </table>
        </div>
    </EditItemTemplate>
    <InsertItemTemplate>
        <hr />
        <div class="NestedDivLevel02A">
            <div>[<asp:LinkButton ID="lbInsert" runat="server" CausesValidation="false" CommandName="InsertData" Text="Insert"></asp:LinkButton> 
            | <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="false" CommandName="CancelInsert" Text="Cancel"></asp:LinkButton>]

            </div>
            <hr />
            <table class="taT">
                <tr class="taT"><td class="B taR">BMP:</td><td><asp:DropDownList ID="ddlBMP" runat="server" OnSelectedIndexChanged="ddlAg_WFP3_BMP_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td></tr>
                   <tr class="taT"><td class="B taR">Units Designed:</td><td><asp:TextBox ID="tbUnitsDesigned" runat="server" Text='<%# Bind("units_designed") %>'></asp:TextBox> <asp:Label ID="lblAg_WFP3_BMP_Units" runat="server"></asp:Label></td></tr>
                    <tr class="taT"><td class="B taR">Design Cost:</td><td><asp:TextBox ID="tbDesignCost" runat="server" Text='<%# Bind("design_cost") %>'></asp:TextBox></td></tr>
                <tr class="taT"><td class="B taR">Units Completed:</td><td><asp:TextBox ID="tbUnitsCompleted" runat="server" Text='<%# Bind("units_completed") %>'></asp:TextBox></td></tr>
            </table>
        </div>
    </InsertItemTemplate>
</asp:FormView>
               
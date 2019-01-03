using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_DropDownListByAlphabet : System.Web.UI.UserControl
{
    public string StrParentCase = null;
    public string StrEntityType = "PARTICIPANT";
    public string StrParticipantType = null; // F, A, M, E
    public bool ShowStartsWithNumber = false;
    public bool ShowOrganization = false;
    public bool HandleLinkButtonEventInParent = false;

    public DropDownList DDL
    {
        get { return ddl; }
    }
    public Label LblLetter 
    { 
        get { return lblLetter; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.Text = WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet_Header(StrParentCase);
        if (ShowStartsWithNumber) LinkButton27.Visible = true;
        if (ShowOrganization) LinkButton28.Visible = true;
        if (HandleLinkButtonEventInParent) pnl.Visible = false;   
    }

    protected void lbSearchName_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        if (HandleLinkButtonEventInParent) 
        {
            try
            {
                object[] oArgs = new object[] { StrParentCase, lb.CommandArgument };
                Page.GetType().InvokeMember("InvokedMethod_DropDownListByAlphabet_LinkButtonEvent", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
            }
            catch { }
        }
        else WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lblLetter, lb.CommandArgument, StrEntityType, StrParticipantType, null);
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl.SelectedValue))
        {
            try
            {
                object[] oArgs = new object[] { StrParentCase };
                Page.GetType().InvokeMember("InvokedMethod_DropDownListByAlphabet", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message, 0);
            }
        }
    }

    public void ResetControls()
    {
        lblLetter.Text = "";
        if (pnl.Visible == true) ddl.Items.Clear();
    }
}
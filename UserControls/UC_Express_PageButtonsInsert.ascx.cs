using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Express_PageButtonsInsert : System.Web.UI.UserControl
{
    public bool BoolShowOrganization = false;
    public bool BoolShowParticipant = false;
    public bool BoolShowProperty = false;
    public bool BoolShowGlobal = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (BoolShowOrganization)
        {
            ibOrganization.Visible = true;
            lbOrganization.Visible = true;
        }
        if (BoolShowParticipant)
        {
            ibParticipant.Visible = true;
            lbParticipant.Visible = true;
        }
        if (BoolShowProperty)
        {
            ibProperty.Visible = true;
            lbProperty.Visible = true;
        }
        if (BoolShowGlobal)
        {
            ibGlobal.Visible = true;
            lbGlobal.Visible = true;
        }
    }

    protected void ibExpressInsert_Click(object sender, EventArgs e)
    {
        ImageButton ib = (ImageButton)sender;
        ExpressInsert(ib.CommandArgument);
    }

    protected void lbExpressInsert_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        ExpressInsert(lb.CommandArgument);
    }

    private void ExpressInsert(string sType)
    {
        object[] oArgs = new object[] { -1 };
        try
        {
            switch (sType)
            {
                case "ORGANIZATION":
                    Page.GetType().InvokeMember("Organization_ViewEditInsertWindow", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
                    break;
                case "PARTICIPANT":
                    Page.GetType().InvokeMember("Participant_ViewEditInsertWindow", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
                    break;
                case "PROPERTY":
                    Page.GetType().InvokeMember("Property_ViewEditInsertWindow", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
                    break;
                case "GLOBAL":
                    Page.GetType().InvokeMember("InvokedMethod_Insert_Global", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                    break;
            }
        }
        catch { WACAlert.Show(sType + " not implemented on this page.", 0); }
    }
}
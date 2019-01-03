using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_HRMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["siteUnderMaintenance"])) Response.Redirect("WACMaintenance.aspx");
        if (Convert.ToBoolean(Session["user"]) == false && Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["useAuthentication"]))
        {
            //pnlHeader.Visible = false;
            //pnlTabs.Visible = false;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            System.Web.UI.HtmlControls.HtmlGenericControl myJs = new System.Web.UI.HtmlControls.HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript"); //don't need it usually but for cross browser.
            myJs.Attributes.Add("src", ResolveUrl("~/Global.js"));
            this.Page.Header.Controls.Add(myJs);

            user u = WACGlobal_Methods.Security_WACObjects_GetUserByID(Convert.ToInt32(Session["userID"]));
            if (u != null) lblUser.Text = "Welcome " + u.fullname;
        }

        if (!string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings["globalMessage"].ToString()))
        {
            lblGlobalMessage.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["globalMessage"].ToString();
            pnlGlobalMessage.Visible = true;
        }

        if (!Page.IsPostBack)
        {
            if (Session["userName"] == null) Session["userName"] = "GISAdmin";
            hypReports.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["ReportsLink"];
        }
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        Session["user"] = false;
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void lbHumanResources_Click(object sender, EventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.HR_WAC);
        if (b)
        {
            Response.Redirect("~/HR/WACHR.aspx");
        }
        else WACAlert.Show("You do not have permission to view the WAC Employees.", 0);
    }
}

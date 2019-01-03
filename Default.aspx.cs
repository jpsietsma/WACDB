using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_DataObjects;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            imgWACLogo.ImageUrl = "~/App_Themes/" + Page.Theme + "/images/WAClogo.jpg";
            Utility_WACUT_AttachedDocumentViewer docView = WACUT_AttachedDocumentViewer;
            List<WACParameter> parms = new List<WACParameter>();
            docView.InitControl(parms);
            Response.Redirect("~/Ag/WACAgriculture.aspx");
        }
    }
}
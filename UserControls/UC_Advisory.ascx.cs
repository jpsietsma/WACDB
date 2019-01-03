using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Advisory : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    var p = Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1);
                    var v = (from q in wDataContext.webScreens.Where(o => o.pk_webScreen.ToUpper() == p)
                             select q).Single();
                    if (!string.IsNullOrEmpty(v.advisory))
                    {
                        pnlAdvisory.Visible = true;
                        lblAdvisory.Text = v.advisory;
                    }
                    else
                    {
                        pnlAdvisory.Visible = false;
                        lblAdvisory.Text = "";
                    }
                }
                catch
                {
                    pnlAdvisory.Visible = false;
                    lblAdvisory.Text = "";
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Explanation : System.Web.UI.UserControl
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
                    if (!string.IsNullOrEmpty(v.explanation)) lblExplanation.Text = v.explanation;
                    else lblExplanation.Text = "";
                }
                catch { lblExplanation.Text = ""; }
            }
        }
    }
}

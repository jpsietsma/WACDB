using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACContact : System.Web.UI.Page
{
    public int iCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        iCount = 0;
        if (!Page.IsPostBack)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.contactWACs.Where(w => w.active == "Y").OrderBy(o => o.sort_nbr) select b;
                dlContactInformation.DataSource = a;
                dlContactInformation.DataBind();
            }
        }
    }
}
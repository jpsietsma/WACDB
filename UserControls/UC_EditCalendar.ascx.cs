using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_EditCalendar : System.Web.UI.UserControl
{
    public bool UseClearLinkButton = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        lb.Visible = UseClearLinkButton;
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = Convert.ToDateTime("1/1/" + ddl.SelectedValue);
            cal.SelectedDate = dt;
            cal.VisibleDate = dt;
        }
        catch { WACAlert.Show("Error setting new date.", 0); }
    }

    protected void lb_Click(object sender, EventArgs e)
    {
        try
        {
            cal.SelectedDates.Clear();
            cal.VisibleDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ddl.SelectedIndex = 0;
        }
        catch { WACAlert.Show("Error clearing calendar.", 0); }
    }
}
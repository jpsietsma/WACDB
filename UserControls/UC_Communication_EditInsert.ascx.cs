using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Communication_EditInsert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ddlAreaCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlNumber.Items.Clear();
        if (!string.IsNullOrEmpty(ddlAreaCode.SelectedValue))
        {
            WACGlobal_Methods.PopulateControl_Communication_PhoneNumbersByAreaCode_DDL(ddlNumber, null, ddlAreaCode.SelectedValue);
        }
    }

    protected void lbClear_Click(object sender, EventArgs e)
    {
        ddlNumber.Items.Clear();
        ddlAreaCode.SelectedIndex = 0;
    }
}
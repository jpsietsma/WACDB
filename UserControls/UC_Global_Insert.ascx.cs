using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Global_Insert : System.Web.UI.UserControl
{
    #region Initialization

    //public string StringParticipantType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //UC_Express_Global_Insert1.StringParticipantType = StringParticipantType;
    }

    #endregion

    #region Global Methods and Event Handling

    public void ShowGlobal_Insert()
    {
        mpeGlobal_Insert.Show();
    }

    protected void lbGlobal_Insert_Close_Click(object sender, EventArgs e)
    {
        UC_Express_Global_Insert1.HideGlobal_Insert_Panels(true);
        try { Page.GetType().InvokeMember("InvokedMethod_SectionPage_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null); }
        catch { }
        mpeGlobal_Insert.Hide();
    }

    #endregion
}
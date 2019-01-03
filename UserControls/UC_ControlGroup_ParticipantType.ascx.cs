using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_ControlGroup_ParticipantType : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ddlInsert_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            if (!string.IsNullOrEmpty(ddlInsert.SelectedValue))
            {
                int? i = null;
                int iCode = 0;
                try
                {
                    using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                    {
                        iCode = wac.participantType_add(Convert.ToInt32(hfPK_Participant.Value), ddlInsert.SelectedValue, Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            object[] oArgs = new object[] { Convert.ToInt32(hfPK_Participant.Value) };
                            Page.GetType().InvokeMember("InvokedMethod_ControlGroup_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
                        }
                        else WACAlert.Show("Error Returned from Database.", iCode);
                    }
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantType_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        object[] oArgs = new object[] { Convert.ToInt32(hfPK_Participant.Value) };
                        Page.GetType().InvokeMember("InvokedMethod_ControlGroup_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACExpressAdd_Participant : System.Web.UI.UserControl
{
    public string sPTCode;
    public string sPTName;

    protected void Page_Load(object sender, EventArgs e)
    {
        SetPTStrings();
    }

    private void SetPTStrings()
    {
        string p = Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1);
        switch (p.ToUpper())
        {
            case "WACEASEMENTS.ASPX":
                sPTCode = "E";
                sPTName = "Easement";
                break;
            case "WACFARM2MARKET.ASPX":
                sPTCode = "M";
                sPTName = "Farm to Market";
                break;
        }
    }

    protected void btnMatch_Click(object sender, EventArgs e)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            System.Data.Linq.ISingleResult<participant_match_florgResult> aResult = wDataContext.participant_match_florg(tbF.Text, tbL.Text, tbO.Text);

            List<int> l = new List<int>();
            foreach (var c in aResult)
            {
                l.Add(c.pk_participant);
            }

            var a = from b in wDataContext.participants
                    where l.Contains(b.pk_participant)
                    select b;

            lv.DataSource = a.OrderBy(o => o.lname).ThenBy(o => o.fname);
            lv.DataBind();

            foreach (ListViewItem lvi in lv.Items)
            {
                ListView lvPT = lvi.FindControl("lvPT") as ListView;
                HiddenField hfP = lvi.FindControl("hfP") as HiddenField;
                bool bFoundPT = false;
                foreach (ListViewItem lvi2 in lvPT.Items)
                {
                    HiddenField hfPT = lvi2.FindControl("hfPT") as HiddenField;
                    if (hfPT != null)
                    {
                        if (hfPT.Value == sPTCode)
                        {
                            bFoundPT = true;
                            break;
                        }
                    }
                }

                Control cAddPT = FindControl("lbAddPT", lvPT.Controls);
                if (cAddPT != null)
                {
                    LinkButton lbAddPT = (LinkButton)cAddPT;
                    if (bFoundPT)
                    {
                        lbAddPT.Visible = false;
                    }
                    else
                    {
                        lbAddPT.CommandArgument = hfP.Value;
                        lbAddPT.Text = "Add " + sPTName + " Participant Type";
                        lbAddPT.Visible = true;
                    }
                }

                Control cAddPTEmpty = FindControl("lbAddPTEmpty", lvPT.Controls);
                if (cAddPTEmpty != null)
                {
                    LinkButton lbAddPTEmpty = (LinkButton)cAddPTEmpty;
                    lbAddPTEmpty.CommandArgument = hfP.Value;
                    lbAddPTEmpty.Text = "Add " + sPTName + " Participant Type";
                }
            }

            pnl.Visible = true;
        }
    }

    protected void lbAddPT_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            LinkButton lbtn = (LinkButton)sender;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = 0;
                int? iPKParticipantType = null;
                iCode = wDataContext.participantType_add(Convert.ToInt32(lbtn.CommandArgument), sPTCode, Session["userID"].ToString(), ref iPKParticipantType);
                if (iCode == 0)
                {
                    btnMatch_Click(null, null);
                    WACAlert.Show("Participant Type Added", 0);
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    private Control FindControl(string controlID, ControlCollection controls)
    {
        foreach (Control c in controls)
        {
            if (c.ID == controlID)
                return c;
            if (c.HasControls())
            {
                Control cTmp = this.FindControl(controlID, c.Controls);
                if (cTmp != null)
                    return cTmp;
            }
        }
        return null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                string sL = tbL.Text.Trim();
                string sF = tbF.Text.Trim();
                string sO = tbO.Text.Trim();
                try
                {
                    bool bUserExists = false;

                    if (string.IsNullOrEmpty(sO))
                    {
                        var a = from b in wDataContext.participants.Where(w => w.lname == sL && w.fname == sF)
                                select new { b.pk_participant };
                        if (a.Count() > 0) bUserExists = true;
                    }
                    //else
                    //{
                    //    var a = from b in wDataContext.participants.Where(w => w.lname == sL && w.fname == sF && w.org == sO)
                    //            select new { b.pk_participant };
                    //    if (a.Count() > 0) bUserExists = true;
                    //}

                    if (bUserExists) WACAlert.Show("That participant already exists. A duplicate Participant cannot be added.", 0);
                    else
                    {
                        bool bCanAdd = false;

                        if (!string.IsNullOrEmpty(sL) && !string.IsNullOrEmpty(sF)) bCanAdd = true;
                        else if (!string.IsNullOrEmpty(sO)) bCanAdd = true;

                        if (bCanAdd)
                        {
                            //iCode = wDataContext.participant_add_express(sF, sL, sO, Session["userID"].ToString(), ref i);
                            //if (iCode == 0)
                            //{
                            //    if (!string.IsNullOrEmpty(sPTCode))
                            //    {
                            //        int iCode2 = 0;
                            //        int? iPKParticipantType = null;
                            //        iCode2 = wDataContext.participantType_add(i, sPTCode, Session["userID"].ToString(), ref iPKParticipantType);
                            //        if (iCode2 == 0) WACAlert.Show("Participant successfully added and participant type assigned: " + sPTName, 0);
                            //        else WACAlert.Show("Participant successfully added, but an error occurred trying to assign a participant type:", iCode2);
                            //    }
                            //    else WACAlert.Show("Participant successfully added.", 0);

                            //}
                            //else WACAlert.Show("Error Returned from Database.", iCode);
                        }
                        else WACAlert.Show("The Participant needs to have either both a first name and a last name or an organization", 0);
                    }
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }            
    }
}
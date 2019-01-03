using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_ControlGroup_ParticipantProperty : System.Web.UI.UserControl
{
    #region Initialization

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region Event Handling

    protected void lbParticipant_Property_Close_Click(object sender, EventArgs e)
    {
        fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
        Participant_Property_BindRecord(-1);

        object[] oArgs = new object[] { Convert.ToInt32(hfPK_Participant.Value) };
        Page.GetType().InvokeMember("InvokedMethod_ControlGroup_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
    }

    protected void lbParticipant_Property_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            fvParticipant_Property.ChangeMode(FormViewMode.Insert);
            Participant_Property_BindRecord(-1);
        }
    }

    protected void lbView_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
        Participant_Property_BindRecord(Convert.ToInt32(lb.CommandArgument));
    }

    protected void fvParticipant_Property_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvParticipant_Property.ChangeMode(e.NewMode);
            Participant_Property_BindRecord(Convert.ToInt32(fvParticipant_Property.DataKey.Value));
        }
    }

    protected void fvParticipant_Property_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlMaster = fvParticipant_Property.FindControl("ddlMaster") as DropDownList;
        HiddenField hfPropertyPK = fvParticipant_Property.FindControl("UC_Property_EditInsert1").FindControl("hfPropertyPK") as HiddenField;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.participantProperties.Where(w => w.pk_participantProperty == Convert.ToInt32(fvParticipant_Property.DataKey.Value)).Select(s => s).Single();

                a.master = ddlMaster.SelectedValue;

                if (!string.IsNullOrEmpty(hfPropertyPK.Value)) a.fk_property = Convert.ToInt32(hfPropertyPK.Value);
                else sb.Append("Property was not updated. Property is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
                Participant_Property_BindRecord(Convert.ToInt32(fvParticipant_Property.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvParticipant_Property_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlMaster = fvParticipant_Property.FindControl("ddlMaster") as DropDownList;
        HiddenField hfPropertyPK = fvParticipant_Property.FindControl("UC_Property_EditInsert1").FindControl("hfPropertyPK") as HiddenField;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPK_Property = null;
                try { iPK_Property = Convert.ToInt32(hfPropertyPK.Value); }
                catch { }
                if (iPK_Property == null) sb.Append("Property is required. ");

                string sMaster = ddlMaster.SelectedValue;

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.participantProperty_add(Convert.ToInt32(hfPK_Participant.Value), iPK_Property, sMaster, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
                        Participant_Property_BindRecord(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvParticipant_Property_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantProperty_delete(Convert.ToInt32(fvParticipant_Property.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbParticipant_Property_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Data Binding

    private void Participant_Property_BindRecord(int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.participantProperties.Where(w => w.pk_participantProperty == i).Select(s => s);
            fvParticipant_Property.DataKeyNames = new string[] { "pk_participantProperty" };
            fvParticipant_Property.DataSource = a;
            fvParticipant_Property.DataBind();

            if (fvParticipant_Property.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant_Property.FindControl("ddlMaster") as DropDownList, "N", false);
                WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvParticipant_Property.FindControl("UC_Property_EditInsert1") as UserControl, null);
            }

            if (fvParticipant_Property.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant_Property.FindControl("ddlMaster") as DropDownList, a.Single().master, false);
                WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvParticipant_Property.FindControl("UC_Property_EditInsert1") as UserControl, a.Single().property);
            }
        }
    }

    #endregion
}
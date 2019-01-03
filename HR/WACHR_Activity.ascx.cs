using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class HR_WACHR_Activity : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_ParticipantWAC
    {
        get { return Convert.ToInt32(Session["FK_ParticipantWAC"]) == 0 ? -1 : Convert.ToInt32(Session["FK_ParticipantWAC"]); }
        set { Session["FK_ParticipantWAC"] = value; }
    }
    public int PK_ParticipantActivity
    {
        get { return Convert.ToInt32(Session["PK_ParticipantActivity"]) == 0 ? -1 : Convert.ToInt32(Session["PK_ParticipantActivity"]); }
        set { Session["PK_ParticipantActivity"] = value; }
    }
    
    public delegate void OpenFormViewEventHandler(object sender, FormViewEventArgs e);
    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_ParticipantWAC = e.ForeignKey;
        PK_ParticipantActivity = e.PrimaryKey;
        fvHR_WACEmployee_Activity.ChangeMode(e.ViewMode);
        BindHR_WACEmployee_Activity();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);

    protected void fvHR_WACEmployee_Activity_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvHR_WACEmployee_Activity.ChangeMode(e.NewMode);
        BindHR_WACEmployee_Activity();
    }

    protected void fvHR_WACEmployee_Activity_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        PK_ParticipantActivity = Convert.ToInt32(fvHR_WACEmployee_Activity.DataKey.Value);
        TextBox tbDate = fvHR_WACEmployee_Activity.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_Activity.FindControl("tbNote") as TextBox;
 
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var x = wac.participantWAC_activities.Where(w => w.pk_participantWAC_activity == PK_ParticipantActivity).Select(s => s).Single();
                try { x.date = Convert.ToDateTime(tbDate.Text); }
                catch { throw new Exception("Date was not updated. Invalid date. "); }
                if (!string.IsNullOrEmpty(tbNote.Text)) x.note = tbNote.Text;
                else x.note = null;
                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();
                wac.SubmitChanges();
                fvHR_WACEmployee_Activity.ChangeMode(FormViewMode.ReadOnly);
                BindHR_WACEmployee_Activity();
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "activity"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }      
    }

    protected void fvHR_WACEmployee_Activity_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        TextBox tbDate = fvHR_WACEmployee_Activity.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_Activity.FindControl("tbNote") as TextBox;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                DateTime? dtDate = null;
                try { dtDate = Convert.ToDateTime(tbDate.Text); }
                catch { throw new Exception("Date is required / Invalid date format. "); }
                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = tbNote.Text;
                iCode = wac.participantWAC_activity_add(FK_ParticipantWAC, dtDate, sNote, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    PK_ParticipantActivity = (Int32)i;
                    fvHR_WACEmployee_Activity.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Activity();
                }
                else
                    WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "activity"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    protected void fvHR_WACEmployee_Activity_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        int iCode = 0;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wac.participantWAC_activity_delete(PK_ParticipantActivity, Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Activity.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Activity();
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "activity"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    private void BindHR_WACEmployee_Activity()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var x = wac.participantWAC_activities.Where(w => w.pk_participantWAC_activity == PK_ParticipantActivity)
                .Select(s => s).OrderByDescending(o => o.created); ;
            fvHR_WACEmployee_Activity.DataSource = x;
            fvHR_WACEmployee_Activity.DataKeyNames = new string[] { "pk_participantWAC_activity" };
            fvHR_WACEmployee_Activity.DataBind();

            if (fvHR_WACEmployee_Activity.CurrentMode == FormViewMode.Edit)
            {
                if (x.Single().date != null)
                {
                    TextBox tbDate = fvHR_WACEmployee_Activity.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
                    tbDate.Text = Convert.ToDateTime(x.Single().date).ToShortDateString();
                }
            }
            if (fvHR_WACEmployee_Activity.CurrentMode == FormViewMode.ReadOnly)
            {
                var z = wac.participantWAC_activityCosts.Where(w => w.fk_participantWAC_activity == PK_ParticipantActivity)
                    .Select(s => s).OrderByDescending(o => o.created); ;
                GridView gvHR_WACEmployees_ActivityCost = fvHR_WACEmployee_Activity.FindControl("gvHR_WACEmployees_ActivityCost") as GridView;
                gvHR_WACEmployees_ActivityCost.DataSource = z;
                gvHR_WACEmployees_ActivityCost.DataKeyNames = new string[] { "pk_participantWAC_activityCost" };
                gvHR_WACEmployees_ActivityCost.DataBind();
            }
        }
        
    }


    #region Subtable - Activity - Cost

    protected void lbHR_WACEmployees_ActivityCost_Insert_Click(object sender, EventArgs e)
    {
        FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
        fvHR_WACEmployee_ActivityCost.ChangeMode(FormViewMode.Insert);
        BindHR_WACEmployee_ActivityCost(-1);
    }

    protected void gvHR_WACEmployees_ActivityCost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView gvHR_WACEmployees_ActivityCost = fvHR_WACEmployee_Activity.FindControl("gvHR_WACEmployees_ActivityCost") as GridView;
        FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
        fvHR_WACEmployee_ActivityCost.ChangeMode(FormViewMode.ReadOnly);
        BindHR_WACEmployee_ActivityCost(Convert.ToInt32(gvHR_WACEmployees_ActivityCost.DataKeys[Convert.ToInt32(e.CommandArgument)].Value));
    }

    protected void lbHR_WACEmployees_ActivityCost_Close_Click(object sender, EventArgs e)
    {
        Clear_WACEmployee_ActivityCost();
        BindHR_WACEmployee_Activity();
    }

    protected void fvHR_WACEmployee_ActivityCost_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
        fvHR_WACEmployee_ActivityCost.ChangeMode(e.NewMode);
        BindHR_WACEmployee_ActivityCost(Convert.ToInt32(fvHR_WACEmployee_ActivityCost.DataKey.Value));
    }

    protected void fvHR_WACEmployee_ActivityCost_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {       
        FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
        TextBox tbDate = fvHR_WACEmployee_ActivityCost.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
        DropDownList ddlItem = fvHR_WACEmployee_ActivityCost.FindControl("ddlItem") as DropDownList;
        TextBox tbCost = fvHR_WACEmployee_ActivityCost.FindControl("tbCost") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_ActivityCost.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var x = wac.participantWAC_activityCosts.Where(w => w.pk_participantWAC_activityCost == 
                    Convert.ToInt32(fvHR_WACEmployee_ActivityCost.DataKey.Value)).Select(s => s).Single();
                try { x.date = Convert.ToDateTime(tbDate.Text); }
                catch { throw new Exception("Date missing or invalid. Date is required."); }

                if (!string.IsNullOrEmpty(ddlItem.SelectedValue)) x.fk_trainingCost_code = ddlItem.SelectedValue;
                else throw new Exception("Item is required. ");

                try { x.cost = Convert.ToDecimal(tbCost.Text); }
                catch { throw new Exception("Cost missing or invalid.  Cost is required."); }

                if (!string.IsNullOrEmpty(tbNote.Text)) x.note = tbNote.Text;
                else x.note = null;

                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();
                wac.SubmitChanges();
                fvHR_WACEmployee_ActivityCost.ChangeMode(FormViewMode.ReadOnly);
                BindHR_WACEmployee_ActivityCost(Convert.ToInt32(fvHR_WACEmployee_ActivityCost.DataKey.Value));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_ActivityCost_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
        TextBox tbDate = fvHR_WACEmployee_ActivityCost.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
        DropDownList ddlItem = fvHR_WACEmployee_ActivityCost.FindControl("ddlItem") as DropDownList;
        TextBox tbCost = fvHR_WACEmployee_ActivityCost.FindControl("tbCost") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_ActivityCost.FindControl("tbNote") as TextBox;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iFK_participantWAC_activity = Convert.ToInt32(fvHR_WACEmployee_Activity.DataKey.Value);

                DateTime? dtDate = null;
                try { dtDate = Convert.ToDateTime(tbDate.Text); }
                 catch { throw new Exception("Date is required. / Invalid date format. "); }

                string sFK_trainingCost = null;
                if (!string.IsNullOrEmpty(ddlItem.SelectedValue)) sFK_trainingCost = ddlItem.SelectedValue;
                else throw new Exception("Item is required. ");

                decimal? dCost = null;
                try { dCost = Convert.ToDecimal(tbCost.Text); }
                catch { throw new Exception("Cost is required. / Invalid decimal format. "); }

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = tbNote.Text;

                iCode = wac.participantWAC_activityCost_add(iFK_participantWAC_activity, sFK_trainingCost, dtDate, dCost, sNote, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fvHR_WACEmployee_ActivityCost.ChangeMode(FormViewMode.ReadOnly);
                    fvHR_WACEmployee_Activity.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Activity();
                    //fvHR_WACEmployee_ActivityCost.ChangeMode(FormViewMode.ReadOnly);
                    //BindHR_WACEmployee_ActivityCost(iFK_participantWAC_activity);
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_ActivityCost_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        int iCode = 0;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
                iCode = wac.participantWAC_activityCost_delete(Convert.ToInt32(fvHR_WACEmployee_ActivityCost.DataKey.Value), Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_ActivityCost.ChangeMode(FormViewMode.ReadOnly);
                    fvHR_WACEmployee_Activity.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Activity();
                    //lbHR_WACEmployees_ActivityCost_Close_Click(null, null);
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }
    
    private void BindHR_WACEmployee_ActivityCost(int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
            var x = wac.participantWAC_activityCosts.Where(w => w.pk_participantWAC_activityCost == i).Select(s => s);
            fvHR_WACEmployee_ActivityCost.DataSource = x;
            fvHR_WACEmployee_ActivityCost.DataKeyNames = new string[] { "pk_participantWAC_activityCost" };
            fvHR_WACEmployee_ActivityCost.DataBind();

            if (fvHR_WACEmployee_ActivityCost.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_TrainingCost_DDL(fvHR_WACEmployee_ActivityCost.FindControl("ddlItem") as DropDownList, null, true);
            }

            if (fvHR_WACEmployee_ActivityCost.CurrentMode == FormViewMode.Edit)
            {
                if (x.Single().date != null)
                {
                    TextBox tbDate = fvHR_WACEmployee_ActivityCost.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
                    tbDate.Text = Convert.ToDateTime(x.Single().date).ToShortDateString();
                }
                WACGlobal_Methods.PopulateControl_DatabaseLists_TrainingCost_DDL(fvHR_WACEmployee_ActivityCost.FindControl("ddlItem") as DropDownList, x.Single().fk_trainingCost_code, false);
            }
        }
    }

    private void Clear_WACEmployee_ActivityCost()
    {
        FormView fvHR_WACEmployee_ActivityCost = fvHR_WACEmployee_Activity.FindControl("fvHR_WACEmployee_ActivityCost") as FormView;
        fvHR_WACEmployee_ActivityCost.DataSource = "";
        fvHR_WACEmployee_ActivityCost.DataBind();
    }

    #endregion
}
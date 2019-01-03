using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_WACHR_Evaluation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_ParticipantWAC
    {
        get { return Convert.ToInt32(Session["FK_ParticipantWAC"]) == 0 ? -1 : Convert.ToInt32(Session["FK_ParticipantWAC"]); }
        set { Session["FK_ParticipantWAC"] = value; }
    }
    public int PK_ParticipantEval
    {
        get { return Convert.ToInt32(Session["PK_ParticipantWACEval"]) == 0 ? -1 : Convert.ToInt32(Session["PK_ParticipantWACEval"]); }
        set { Session["PK_ParticipantWACEval"] = value; }
    }

    public delegate void OpenFormViewEventHandler(object sender, FormViewEventArgs e);
    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_ParticipantWAC = e.ForeignKey;
        PK_ParticipantEval = e.PrimaryKey;
        fvHR_WACEmployee_Evaluation.ChangeMode(e.ViewMode);
        BindHR_WACEmployee_Evaluation();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);

    #region Subtable - Evaluation

    protected void fvHR_WACEmployee_Evaluation_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvHR_WACEmployee_Evaluation.ChangeMode(e.NewMode);
        BindHR_WACEmployee_Evaluation();
    }

    private int? SafeIntFromText(string s)
    {
        int i;
        Decimal d;
        if (string.IsNullOrEmpty(s))
            return null;       
        if (Int32.TryParse(s, out i))
            return i;
        if (Decimal.TryParse(s, out d))
        {
            return Convert.ToInt32(Math.Round(d, MidpointRounding.AwayFromZero));
        }
        else
            throw new FormatException("Cannot convert input to Int");
    }
    protected void fvHR_WACEmployee_Evaluation_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        int iCode = 0;
        PK_ParticipantEval = Convert.ToInt32(fvHR_WACEmployee_Evaluation.DataKey.Value);
        TextBox tbDate = fvHR_WACEmployee_Evaluation.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
        TextBox tbFollowUpDate = fvHR_WACEmployee_Evaluation.FindControl("AjaxCalendar_FollowUpDate").FindControl("tb") as TextBox;
        TextBox tbRating = fvHR_WACEmployee_Evaluation.FindControl("tbRating") as TextBox;
        DropDownList ddlSixMonthEval = fvHR_WACEmployee_Evaluation.FindControl("ddlSixMonthEval") as DropDownList;
        DropDownList ddlAnnualEval = fvHR_WACEmployee_Evaluation.FindControl("ddlAnnualEval") as DropDownList;
        DropDownList ddlFiscalYear = fvHR_WACEmployee_Evaluation.FindControl("ddlFiscalYear") as DropDownList;
        DropDownList ddlPosition = fvHR_WACEmployee_Evaluation.FindControl("ddlPosition") as DropDownList;
        DropDownList ddlJobDescription = fvHR_WACEmployee_Evaluation.FindControl("ddlJobDescription") as DropDownList;
        TextBox tbNote = fvHR_WACEmployee_Evaluation.FindControl("tbNote") as TextBox;
        TextBox tbFollowUpNote = fvHR_WACEmployee_Evaluation.FindControl("tbFollowUpNote") as TextBox;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                DateTime? dtDate = null;
                try { dtDate = Convert.ToDateTime(tbDate.Text); }
                catch { throw new Exception("Evaluation Date missing or Invalid "); }

                DateTime? dtFollowUpDate = null;
                try { dtFollowUpDate = Convert.ToDateTime(tbFollowUpDate.Text); }
                catch { }

                int? iRating = SafeIntFromText(tbRating.Text);            

                string sSixMonthEval = null;
                if (!string.IsNullOrEmpty(ddlSixMonthEval.SelectedValue)) sSixMonthEval = ddlSixMonthEval.SelectedValue;

                string sAnnualEval = null;
                if (!string.IsNullOrEmpty(ddlAnnualEval.SelectedValue)) sAnnualEval = ddlAnnualEval.SelectedValue;

                string sFK_positionWAC = null;
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue)) sFK_positionWAC = ddlPosition.SelectedValue;
                else throw new Exception("Position is required. ");

                string sFK_fiscalYear = null;
                if (!string.IsNullOrEmpty(ddlFiscalYear.SelectedValue)) sFK_fiscalYear = ddlFiscalYear.SelectedValue;
                else throw new Exception("Fiscal Year is required. ");

                string sJobDescription = null;
                if (!string.IsNullOrEmpty(ddlJobDescription.SelectedValue)) sJobDescription = ddlJobDescription.SelectedValue;

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = tbNote.Text;

                string sFollowUpNote = null;
                if (!string.IsNullOrEmpty(tbFollowUpNote.Text)) sFollowUpNote = tbFollowUpNote.Text;

                iCode = wac.participantWAC_evaluation_update(PK_ParticipantEval, sFK_fiscalYear, sFK_positionWAC, dtDate, iRating, sSixMonthEval,
                    sAnnualEval, dtFollowUpDate, sFollowUpNote, sNote, sJobDescription, Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Evaluation.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Evaluation();
                    OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "evaluation"));
                }
                else WACAlert.Show("", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_Evaluation_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? pk = null;
        int iCode = 0;

        TextBox tbDate = fvHR_WACEmployee_Evaluation.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
        TextBox tbFollowUpDate = fvHR_WACEmployee_Evaluation.FindControl("AjaxCalendar_FollowUpDate").FindControl("tb") as TextBox;
        TextBox tbRating = fvHR_WACEmployee_Evaluation.FindControl("tbRating") as TextBox;
        DropDownList ddlSixMonthEval = fvHR_WACEmployee_Evaluation.FindControl("ddlSixMonthEval") as DropDownList;
        DropDownList ddlAnnualEval = fvHR_WACEmployee_Evaluation.FindControl("ddlAnnualEval") as DropDownList;
        DropDownList ddlFiscalYear = fvHR_WACEmployee_Evaluation.FindControl("ddlFiscalYear") as DropDownList;
        DropDownList ddlPosition = fvHR_WACEmployee_Evaluation.FindControl("ddlPosition") as DropDownList;
        DropDownList ddlJobDescription = fvHR_WACEmployee_Evaluation.FindControl("ddlJobDescription") as DropDownList;
        TextBox tbNote = fvHR_WACEmployee_Evaluation.FindControl("tbNote") as TextBox;
        TextBox tbFollowUpNote = fvHR_WACEmployee_Evaluation.FindControl("tbFollowUpNote") as TextBox;
        
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                DateTime? dtDate = null;
                try { dtDate = Convert.ToDateTime(tbDate.Text); }
                catch { throw new Exception("Evaluation Date missing or Invalid "); }

                DateTime? dtFollowUpDate = null;
                try { dtFollowUpDate = Convert.ToDateTime(tbFollowUpDate.Text); }
                    catch {}

                int? iRating = SafeIntFromText(tbRating.Text);      

                string sSixMonthEval = null;
                if (!string.IsNullOrEmpty(ddlSixMonthEval.SelectedValue)) sSixMonthEval = ddlSixMonthEval.SelectedValue;

                string sAnnualEval = null;
                if (!string.IsNullOrEmpty(ddlAnnualEval.SelectedValue)) sAnnualEval = ddlAnnualEval.SelectedValue;

                string sFK_positionWAC = null;
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue)) sFK_positionWAC = ddlPosition.SelectedValue;
                else throw new Exception("Position is required. ");

                string sFK_fiscalYear = null;
                if (!string.IsNullOrEmpty(ddlFiscalYear.SelectedValue)) sFK_fiscalYear = ddlFiscalYear.SelectedValue;
                else throw new Exception("Fiscal Year is required. ");

                string sJobDescription = null;
                if (!string.IsNullOrEmpty(ddlJobDescription.SelectedValue)) sJobDescription = ddlJobDescription.SelectedValue;

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = tbNote.Text;

                string sFollowUpNote = null;
                if (!string.IsNullOrEmpty(tbFollowUpNote.Text)) sFollowUpNote = tbFollowUpNote.Text;

                iCode = wac.participantWAC_evaluation_add(FK_ParticipantWAC, sFK_fiscalYear, sFK_positionWAC, dtDate, iRating, sSixMonthEval,
                    sAnnualEval, dtFollowUpDate, sFollowUpNote, sNote, sJobDescription, Session["userName"].ToString(), ref pk);
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Evaluation.ChangeMode(FormViewMode.ReadOnly);
                    PK_ParticipantEval = Convert.ToInt32(pk);
                    BindHR_WACEmployee_Evaluation();
                    OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "evaluation"));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_Evaluation_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        int iCode = 0;
        PK_ParticipantEval = Convert.ToInt32(fvHR_WACEmployee_Evaluation.DataKey.Value);
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wac.participantWAC_evaluation_delete(PK_ParticipantEval, Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Evaluation.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Evaluation();
                    OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "evaluation"));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    private void BindHR_WACEmployee_Evaluation()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var x = wac.participantWAC_evaluations.Where(w => w.pk_participantWAC_evaluation == PK_ParticipantEval).Select(s => s);
            fvHR_WACEmployee_Evaluation.DataSource = x;
            fvHR_WACEmployee_Evaluation.DataKeyNames = new string[] { "pk_participantWAC_evaluation" };
            fvHR_WACEmployee_Evaluation.DataBind();

            if (fvHR_WACEmployee_Evaluation.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee_Evaluation.FindControl("ddlSixMonthEval") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee_Evaluation.FindControl("ddlAnnualEval") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PositionWAC_DDL(fvHR_WACEmployee_Evaluation.FindControl("ddlPosition") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FiscalYear_DDL(fvHR_WACEmployee_Evaluation.FindControl("ddlFiscalYear") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee_Evaluation.FindControl("ddlJobDescription") as DropDownList, null, true);
            }
            else if (fvHR_WACEmployee_Evaluation.CurrentMode == FormViewMode.Edit)
            {
                if (x.Single().date != null)
                {
                    TextBox tbDate = fvHR_WACEmployee_Evaluation.FindControl("AjaxCalendar_Date").FindControl("tb") as TextBox;
                    tbDate.Text = Convert.ToDateTime(x.Single().date).ToShortDateString();
                }
                if (x.Single().followup_date != null)
                {
                    TextBox tbFollowUpDate = fvHR_WACEmployee_Evaluation.FindControl("AjaxCalendar_FollowUpDate").FindControl("tb") as TextBox;
                    tbFollowUpDate.Text = Convert.ToDateTime(x.Single().followup_date).ToShortDateString();
                }
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee_Evaluation.FindControl("ddlSixMonthEval") as DropDownList, x.Single().sixMonthEval, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee_Evaluation.FindControl("ddlAnnualEval") as DropDownList, x.Single().annualEval, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PositionWAC_DDL(fvHR_WACEmployee_Evaluation.FindControl("ddlPosition") as DropDownList, x.Single().fk_positionWAC_code, false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FiscalYear_DDL(fvHR_WACEmployee_Evaluation.FindControl("ddlFiscalYear") as DropDownList, x.Single().fk_fiscalYear_code, false);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee_Evaluation.FindControl("ddlJobDescription") as DropDownList, x.Single().jobDescription, true);
            }
        }
    }

    #endregion
}
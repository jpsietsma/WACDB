using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WAC_DataObjects;

public partial class HR_WACHR_Training : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int FK_ParticipantWAC
    {
        get { return Convert.ToInt32(Session["FK_ParticipantWAC"]) == 0 ? -1 : Convert.ToInt32(Session["FK_ParticipantWAC"]); }
        set { Session["FK_ParticipantWAC"] = value; }
    }
    public int PK_ParticipantTraining
    {
        get { return Convert.ToInt32(Session["PK_ParticipantTraining"]) == 0 ? -1 : Convert.ToInt32(Session["PK_ParticipantTraining"]); }
        set { Session["PK_ParticipantTraining"] = value; }
    }
    
    public delegate void OpenFormViewEventHandler(object sender, FormViewEventArgs e);
    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_ParticipantWAC = e.ForeignKey;
        PK_ParticipantTraining = e.PrimaryKey;
        fvHR_WACEmployee_Training.ChangeMode(e.ViewMode);
        BindHR_WACEmployee_Training();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);

    protected void fvHR_WACEmployee_Training_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvHR_WACEmployee_Training.ChangeMode(e.NewMode);
        BindHR_WACEmployee_Training();
    }

    protected void fvHR_WACEmployee_Training_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        PK_ParticipantTraining = Convert.ToInt32(fvHR_WACEmployee_Training.DataKey.Value);
        DropDownList ddlPosition = fvHR_WACEmployee_Training.FindControl("ddlPosition") as DropDownList;
        DropDownList ddlFiscalYear = fvHR_WACEmployee_Training.FindControl("ddlFiscalYear") as DropDownList;
        DropDownList ddlTrainingReqd = fvHR_WACEmployee_Training.FindControl("ddlTrainingReqd") as DropDownList;
        TextBox tbCompDate = fvHR_WACEmployee_Training.FindControl("AjaxCalendar_CompDate").FindControl("tb") as TextBox;
        TextBox tbExpDate = fvHR_WACEmployee_Training.FindControl("AjaxCalendar_ExpDate").FindControl("tb") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_Training.FindControl("tbNote") as TextBox;
 
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var x = wac.participantWAC_trainings.Where(w => w.pk_participantWAC_training == PK_ParticipantTraining).Select(s => s).Single();
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue)) 
                {
                    x.fk_positionWAC_code = ddlPosition.SelectedValue;
                }
                else
                    throw new Exception("Blank or invalid Position. Position is required");
                if (!string.IsNullOrEmpty(ddlFiscalYear.SelectedValue))
                {
                    x.fk_fiscalYear_code = ddlFiscalYear.SelectedValue;
                }
                else
                    throw new Exception("Blank or invalid Fiscal Year.  Fiscal year is required");
                if (!string.IsNullOrEmpty(ddlTrainingReqd.SelectedValue))
                {
                    x.fk_trainingReqd_code = ddlTrainingReqd.SelectedValue;
                }
                else
                    throw new Exception("Blank or invalid Training.  Training is required");
                try { x.completed_date = Convert.ToDateTime(tbCompDate.Text); }
                catch { throw new Exception("Blank or invalid Completion Date. Completion Date is required. "); }
                try { x.expiration_date = Convert.ToDateTime(tbExpDate.Text); }
                catch {}
                if (!string.IsNullOrEmpty(tbNote.Text)) x.note = tbNote.Text;
                else x.note = null;
                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();
                wac.SubmitChanges();
                fvHR_WACEmployee_Training.ChangeMode(FormViewMode.ReadOnly);
                BindHR_WACEmployee_Training();
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "training"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }      
    }

    protected void fvHR_WACEmployee_Training_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        PK_ParticipantTraining = Convert.ToInt32(fvHR_WACEmployee_Training.DataKey.Value);
        DropDownList ddlPosition = fvHR_WACEmployee_Training.FindControl("ddlPosition") as DropDownList;
        DropDownList ddlFiscalYear = fvHR_WACEmployee_Training.FindControl("ddlFiscalYear") as DropDownList;
        DropDownList ddlTrainingReqd = fvHR_WACEmployee_Training.FindControl("ddlTrainingReqd") as DropDownList;
        TextBox tbCompDate = fvHR_WACEmployee_Training.FindControl("AjaxCalendar_CompDate").FindControl("tb") as TextBox;
        TextBox tbExpDate = fvHR_WACEmployee_Training.FindControl("AjaxCalendar_ExpDate").FindControl("tb") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_Training.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                string position = null;
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue))
                {
                    position = ddlPosition.SelectedValue;
                }
                else
                    throw new Exception("Position is required");
                string fiscalYear = null;
                if (!string.IsNullOrEmpty(ddlFiscalYear.SelectedValue))
                {
                    fiscalYear = ddlFiscalYear.SelectedValue;
                }
                else
                    throw new Exception("Blank or invalid Fiscal Year.  Fiscal year is required");
                string trainingReqd = null;
                if (!string.IsNullOrEmpty(ddlTrainingReqd.SelectedValue))
                {
                    trainingReqd = ddlTrainingReqd.SelectedValue;
                }
                else
                    throw new Exception("Blank or invalid Training.  Training is required");
                DateTime? dtCompDate = null;
                try { dtCompDate = Convert.ToDateTime(tbCompDate.Text); }
                catch { throw new Exception("Blank or invalid Completion Date. Completion Date is required. "); }
                DateTime? dtExpDate = null;
                try { dtExpDate = Convert.ToDateTime(tbExpDate.Text); }
                catch {}
                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = tbNote.Text;
                iCode = wac.participantWAC_training_add(FK_ParticipantWAC, fiscalYear,position,trainingReqd, dtCompDate,
                    dtExpDate, sNote, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    PK_ParticipantTraining = Convert.ToInt32(i);
                    fvHR_WACEmployee_Training.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Training();
                }
                else
                    WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "training"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    protected void fvHR_WACEmployee_Training_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        int iCode = 0;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wac.participantWAC_training_delete(PK_ParticipantTraining, Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Training.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Training();
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "training"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    private void BindHR_WACEmployee_Training()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var x = wac.participantWAC_trainings.Where(w => w.pk_participantWAC_training == PK_ParticipantTraining).Select(s => s).OrderByDescending(o => o.created); 
            fvHR_WACEmployee_Training.DataSource = x;
            fvHR_WACEmployee_Training.DataKeyNames = new string[] { "pk_participantWAC_training" };
            fvHR_WACEmployee_Training.DataBind();

            if (fvHR_WACEmployee_Training.CurrentMode == FormViewMode.ReadOnly)
            {
                Image imgAdvisory = fvHR_WACEmployee_Training.FindControl("imgAdvisory") as Image;
                if (imgAdvisory != null)
                    imgAdvisory.ToolTip = WACGlobal_Methods.Specialtext_Global_Advisory(9);
                var z = wac.participantWAC_trainingCosts.Where(w => w.fk_participantWAC_training == PK_ParticipantTraining)
                    .Select(s => s).OrderByDescending(o => o.created);
                GridView gvHR_WACEmployees_TrainingCost = fvHR_WACEmployee_Training.FindControl("gvHR_WACEmployees_TrainingCost") as GridView;
                if (gvHR_WACEmployees_TrainingCost != null)
                {
                    gvHR_WACEmployees_TrainingCost.DataSource = z;
                    gvHR_WACEmployees_TrainingCost.DataKeyNames = new string[] { "pk_participantWAC_trainingCost" };
                    gvHR_WACEmployees_TrainingCost.DataBind();
                }
            }
            else if (fvHR_WACEmployee_Training.CurrentMode == FormViewMode.Insert)
            {
                ddlPositionPopulate(fvHR_WACEmployee_Training.FindControl("ddlPosition") as DropDownList,FK_ParticipantWAC, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FiscalYear_DDL(fvHR_WACEmployee_Training.FindControl("ddlFiscalYear") as DropDownList, null, true);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_TrainingReqd_DDL(fvHR_WACEmployee_Training.FindControl("ddlTrainingReqd") as DropDownList, null, true);
                DropDownList ddlTrainingReqd = fvHR_WACEmployee_Training.FindControl("ddlTrainingReqd") as DropDownList;
                var trList = wac.list_trainingReqds.OrderBy(o => o.pk_trainingReqd_code).Select(s =>
                    new DDLListItem(s.pk_trainingReqd_code, s.title)).ToList();
                //string trCode = x.Single().fk_trainingReqd_code;
                //if (!string.IsNullOrEmpty(trCode))
                //{
                //    foreach (DDLListItem item in trList)
                //    {
                //        if (trCode.Contains(item.DataValueField))
                //        {
                //            ddlTrainingReqd.SelectedValue = item.DataValueField;
                //            break;
                //        }
                //    }
                //}
                ddlTrainingReqd.DataSource = trList;
                ddlTrainingReqd.DataTextField = "DataTextField";
                ddlTrainingReqd.DataValueField = "DataValueField";
                ddlTrainingReqd.DataBind();
                ddlTrainingReqd.Items.Insert(0, new ListItem("[SELECT]"));
            }
            else if (fvHR_WACEmployee_Training.CurrentMode == FormViewMode.Edit)
            {
                ddlPositionPopulate(fvHR_WACEmployee_Training.FindControl("ddlPosition") as DropDownList,x.Single().fk_participantWAC, x.Single().fk_positionWAC_code, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FiscalYear_DDL(fvHR_WACEmployee_Training.FindControl("ddlFiscalYear") as DropDownList, x.Single().fk_fiscalYear_code, false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_TrainingReqd_DDL(fvHR_WACEmployee_Training.FindControl("ddlTrainingReqd") as DropDownList, x.Single().fk_trainingReqd_code, false);
                TextBox tbCompDate = fvHR_WACEmployee_Training.FindControl("AjaxCalendar_CompDate").FindControl("tb") as TextBox;
                tbCompDate.Text = Convert.ToDateTime(x.Single().completed_date).ToShortDateString();
                if (x.Single().expiration_date != null)
                {
                    TextBox tbExpDate = fvHR_WACEmployee_Training.FindControl("AjaxCalendar_ExpDate").FindControl("tb") as TextBox;
                    tbExpDate.Text = Convert.ToDateTime(x.Single().completed_date).ToShortDateString();
                }
                DropDownList ddlTrainingReqd = fvHR_WACEmployee_Training.FindControl("ddlTrainingReqd") as DropDownList;
                var trList = wac.list_trainingReqds.OrderBy(o => o.pk_trainingReqd_code).Select(s =>
                    new DDLListItem(s.pk_trainingReqd_code, s.title)).ToList();
                string trCode = x.Single().fk_trainingReqd_code;
                if (!string.IsNullOrEmpty(trCode))
                {
                    foreach (DDLListItem item in trList)
                    {
                        if (trCode.Contains(item.DataValueField))
                        {
                            ddlTrainingReqd.SelectedValue = item.DataValueField;
                            break;
                        }
                    }
                }
                ddlTrainingReqd.DataSource = trList;
                ddlTrainingReqd.DataTextField = "DataTextField";
                ddlTrainingReqd.DataValueField = "DataValueField";
                ddlTrainingReqd.DataBind();
                ddlTrainingReqd.Items.Insert(0, new ListItem("[SELECT]"));
            }
        }
    }


    #region Subtable - Activity - Cost

    protected void lbHR_WACEmployees_TrainingCost_Insert_Click(object sender, EventArgs e)
    {
        FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
        fvHR_WACEmployee_TrainingCost.ChangeMode(FormViewMode.Insert);
        BindHR_WACEmployee_TrainingCost(-1);
    }

    protected void gvHR_WACEmployees_TrainingCost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView gvHR_WACEmployees_TrainingCost = fvHR_WACEmployee_Training.FindControl("gvHR_WACEmployees_TrainingCost") as GridView;
        FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
        fvHR_WACEmployee_TrainingCost.ChangeMode(FormViewMode.ReadOnly);
        BindHR_WACEmployee_TrainingCost(Convert.ToInt32(gvHR_WACEmployees_TrainingCost.DataKeys[Convert.ToInt32(e.CommandArgument)].Value));
    }

    protected void lbHR_WACEmployees_TrainingCost_Close_Click(object sender, EventArgs e)
    {
        Clear_WACEmployee_TrainingCost();
        BindHR_WACEmployee_Training();
    }

    protected void fvHR_WACEmployee_TrainingCost_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
        fvHR_WACEmployee_TrainingCost.ChangeMode(e.NewMode);
        BindHR_WACEmployee_TrainingCost(Convert.ToInt32(fvHR_WACEmployee_TrainingCost.DataKey.Value));
    }

    protected void fvHR_WACEmployee_TrainingCost_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {       
        FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
        DropDownList ddlTrainingCostCode = fvHR_WACEmployee_TrainingCost.FindControl("ddlTrainingCostCode") as DropDownList;
        TextBox tbDate = fvHR_WACEmployee_TrainingCost.FindControl("AjaxCalendar_TrCostDate").FindControl("tb") as TextBox;        
        TextBox tbCost = fvHR_WACEmployee_TrainingCost.FindControl("tbCost") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_TrainingCost.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var x = wac.participantWAC_trainingCosts.Where(w => w.pk_participantWAC_trainingCost == 
                    Convert.ToInt32(fvHR_WACEmployee_TrainingCost.DataKey.Value)).Select(s => s).Single();
                if (!string.IsNullOrEmpty(ddlTrainingCostCode.SelectedValue)) x.fk_trainingCost_code = ddlTrainingCostCode.SelectedValue;
                else throw new Exception("Type of Cost is required. ");
                try { x.date = Convert.ToDateTime(tbDate.Text); }
                catch { throw new Exception("Date missing or invalid. Date is required."); }
                try { x.cost = Convert.ToDecimal(tbCost.Text); }
                catch { throw new Exception("Cost missing or invalid.  Cost is required."); }
                if (!string.IsNullOrEmpty(tbNote.Text)) x.note = tbNote.Text;
                else x.note = null;

                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();
                wac.SubmitChanges();
                fvHR_WACEmployee_TrainingCost.ChangeMode(FormViewMode.ReadOnly);
                BindHR_WACEmployee_TrainingCost(Convert.ToInt32(fvHR_WACEmployee_TrainingCost.DataKey.Value));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_TrainingCost_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
        DropDownList ddlTrainingCostCode = fvHR_WACEmployee_TrainingCost.FindControl("ddlTrainingCostCode") as DropDownList;
        TextBox tbDate = fvHR_WACEmployee_TrainingCost.FindControl("AjaxCalendar_TrCostDate").FindControl("tb") as TextBox;
        TextBox tbCost = fvHR_WACEmployee_TrainingCost.FindControl("tbCost") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_TrainingCost.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iFK_participantWAC_Training = Convert.ToInt32(fvHR_WACEmployee_Training.DataKey.Value);
 
                string trainingCostType = null;
                if (!string.IsNullOrEmpty(ddlTrainingCostCode.SelectedValue)) 
                    trainingCostType = ddlTrainingCostCode.SelectedValue;
                else throw new Exception("Type of Cost is required. ");

                DateTime? dtDate = null;
                try { dtDate = Convert.ToDateTime(tbDate.Text); }
                catch { throw new Exception("Date missing or invalid. Date is required."); }

                decimal? dCost = null;
                try { dCost = Convert.ToDecimal(tbCost.Text); }
                catch { throw new Exception("Cost missing or invalid.  Cost is required."); }

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = tbNote.Text;

                iCode = wac.participantWAC_trainingCost_add(iFK_participantWAC_Training, trainingCostType, dtDate, dCost, sNote, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fvHR_WACEmployee_TrainingCost.ChangeMode(FormViewMode.ReadOnly);
                    fvHR_WACEmployee_Training.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Training();
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_TrainingCost_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        int iCode = 0;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
                iCode = wac.participantWAC_trainingCost_delete(Convert.ToInt32(fvHR_WACEmployee_TrainingCost.DataKey.Value), Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_TrainingCost.ChangeMode(FormViewMode.ReadOnly);
                    fvHR_WACEmployee_Training.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Training();
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }
    
    private void BindHR_WACEmployee_TrainingCost(int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
            var x = wac.participantWAC_trainingCosts.Where(w => w.pk_participantWAC_trainingCost == i).
                Select(s => s).OrderByDescending(o => o.created); 
            fvHR_WACEmployee_TrainingCost.DataSource = x;
            fvHR_WACEmployee_TrainingCost.DataKeyNames = new string[] { "pk_participantWAC_trainingCost" };
            fvHR_WACEmployee_TrainingCost.DataBind();

            if (fvHR_WACEmployee_TrainingCost.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_TrainingCost_DDL(fvHR_WACEmployee_TrainingCost.FindControl("ddlTrainingCostCode") as DropDownList, null, true);
            }

            if (fvHR_WACEmployee_TrainingCost.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_TrainingCost_DDL(fvHR_WACEmployee_TrainingCost.FindControl("ddlTrainingCostCode") as DropDownList, x.Single().fk_trainingCost_code, false);
                if (x.Single().date != null)
                {
                    TextBox tbDate = fvHR_WACEmployee_TrainingCost.FindControl("AjaxCalendar_TrCostDate").FindControl("tb") as TextBox;
                    tbDate.Text = Convert.ToDateTime(x.Single().date).ToShortDateString();
                }
            }
        }
    }

    private void Clear_WACEmployee_TrainingCost()
    {
        FormView fvHR_WACEmployee_TrainingCost = fvHR_WACEmployee_Training.FindControl("fvHR_WACEmployee_TrainingCost") as FormView;
        fvHR_WACEmployee_TrainingCost.DataSource = "";
        fvHR_WACEmployee_TrainingCost.DataBind();
    }

    #endregion

    public static void ddlPositionPopulate(DropDownList ddl, int pk_participantWAC, string selectedValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "PositionName";
                ddl.DataValueField = "PositionCode";
                var p = wac.vw_participantWAC_positions.Where(w => w.pk_participantWAC == pk_participantWAC).OrderBy(o => o.position);
                ddl.DataSource = p.OrderBy(o => o.position).Select(s => new
                 {
                     PositionCode = s.fk_positionWAC_code,
                     PositionName = s.position
                 });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(selectedValue)) ddl.SelectedValue = selectedValue; }
                catch { }
            }
        }
    }
}
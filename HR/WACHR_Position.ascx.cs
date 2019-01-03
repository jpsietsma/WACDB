using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_CustomControls;

public partial class HR_WACHR_Position : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_ParticipantWAC
    {
        get { return Convert.ToInt32(Session["FK_ParticipantWAC"]) == 0 ? -1 : Convert.ToInt32(Session["FK_ParticipantWAC"]); }
        set { Session["FK_ParticipantWAC"] = value; }
    }
    public int PK_ParticipantPosition
    {
        get { return Convert.ToInt32(Session["PK_ParticipantWACPosition"]) == 0 ? -1 : Convert.ToInt32(Session["PK_ParticipantWACPosition"]); }
        set { Session["PK_ParticipantWACPosition"] = value; }
    }
   
    public delegate void OpenFormViewEventHandler(object sender, FormViewEventArgs e);
    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_ParticipantWAC = e.ForeignKey;
        PK_ParticipantPosition = e.PrimaryKey;
        fvHR_WACEmployee_Position.ChangeMode(e.ViewMode);
        BindHR_WACEmployee_Position();
        Session["ActiveTabIndex"] = 5;
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    public void fvHR_WACEmployee_Position_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvHR_WACEmployee_Position.ChangeMode(e.NewMode);
        BindHR_WACEmployee_Position();
    }

    public void fvHR_WACEmployee_Position_ItemUpdating(object sender, EventArgs e)
    {
        PK_ParticipantPosition = Convert.ToInt32(fvHR_WACEmployee_Position.DataKey.Value);
        DropDownList ddlPosition = fvHR_WACEmployee_Position.FindControl("ddlPosition") as DropDownList;
        //TextBox tbStartDate = fvHR_WACEmployee_Position.FindControl("AjaxCalendar_StartDate").FindControl("tb") as TextBox;
        CustomControls_AjaxCalendar calStartDate = fvHR_WACEmployee_Position.FindControl("AjaxCalendar_StartDate") as CustomControls_AjaxCalendar;
        //TextBox tbFinishDate = fvHR_WACEmployee_Position.FindControl("AjaxCalendar_FinishDate").FindControl("tb") as TextBox;
        CustomControls_AjaxCalendar calFinishDate = fvHR_WACEmployee_Position.FindControl("AjaxCalendar_FinishDate") as CustomControls_AjaxCalendar;
        TextBox tbStartSalary = fvHR_WACEmployee_Position.FindControl("tbStartSalary") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_Position.FindControl("tbNote") as TextBox;
        CustomControls_AjaxCalendar calExitInterview = fvHR_WACEmployee_Position.FindControl("calExitInterview") as CustomControls_AjaxCalendar;
        TextBox tbExitInterviewNote = fvHR_WACEmployee_Position.FindControl("tbExitInterviewNote") as TextBox;

        string fk_positionWAC_code = null;
        DateTime startDate;
        DateTime? finishDate;
        Decimal startSalary;
        string note = null;
        DateTime? exitInterviewDate;
        string exitInterviewNote = null;
        DateTime modified;
        string modified_by;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            //var x = wac.participantWAC_positions.Where(w => w.pk_participantWAC_position == PK_ParticipantPosition)
            //    .Select(s => s).Single();
            try
            {
                // Position               
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue))
                    fk_positionWAC_code = ddlPosition.SelectedValue;
                else
                    throw new Exception("Position is a required Field");
                // Start Date
                startDate = calStartDate.CalDateNotNullable;
                if (startDate == null)
                    throw new Exception("Invalid or empty Start Date."); 
                // Finish Date - not required
                    //try { x.finish_date= Convert.ToDateTime(tbFinishDate.Text); }
                    //catch { x.finish_date = null; }
                finishDate = calFinishDate.CalDateNullable;
                // Starting Salary not required
                try { startSalary = Convert.ToDecimal(tbStartSalary.Text); }
                catch { throw new Exception("Invalid or empty Starting Salary."); }
                // Note not required
                if (!string.IsNullOrEmpty(tbNote.Text))
                    note = tbNote.Text;
                exitInterviewDate = calExitInterview.CalDateNullable;
                if (!string.IsNullOrEmpty(tbExitInterviewNote.Text))
                    exitInterviewNote = tbExitInterviewNote.Text;
                modified = DateTime.Now;
                modified_by = Session["userName"].ToString();
                //wac.SubmitChanges();
                int rCode = wac.participantWAC_position_update(PK_ParticipantPosition, fk_positionWAC_code, startDate, finishDate, startSalary, note, Session["userName"].ToString(),
                    exitInterviewDate, exitInterviewNote);
                if (rCode == 0)
                {
                    fvHR_WACEmployee_Position.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Position();
                    OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "position"));
                }
                else
                    throw new Exception("Error occurred updating HR:Position");
               // OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "position"));
            }
            catch (Exception ex) 
                    { WACAlert.Show("Error: " + ex.Message, 0); }
        }        
    }

    public void fvHR_WACEmployee_Position_ItemInserting(object sender, EventArgs e)
    {
        DropDownList ddlPosition = fvHR_WACEmployee_Position.FindControl("ddlPosition") as DropDownList;
        TextBox tbStartDate = fvHR_WACEmployee_Position.FindControl("AjaxCalendar_StartDate").FindControl("tb") as TextBox;
        TextBox tbFinishDate = fvHR_WACEmployee_Position.FindControl("AjaxCalendar_FinishDate").FindControl("tb") as TextBox;
        TextBox tbStartSalary = fvHR_WACEmployee_Position.FindControl("tbStartSalary") as TextBox;
        TextBox tbNote = fvHR_WACEmployee_Position.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            int? pk = null;
            string positionCode = null;
            DateTime? startDate = null;
            DateTime? finishDate = null;
            Decimal? startSalary = 0;
            string note = null;
            try
            {
                // Position               
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue))
                {
                    positionCode = ddlPosition.SelectedValue;
                }
                else
                    throw new Exception("Position is a required Field");
                // Start Date
                    try { startDate = Convert.ToDateTime(tbStartDate.Text); }
                    catch { throw new Exception("Invalid or empty Start Date.  Start Date is required."); }

                // Finish Date - not required
                try { finishDate = Convert.ToDateTime(tbFinishDate.Text); }
                catch {}
                
                // Starting Salary not required
                if (!string.IsNullOrEmpty(tbStartSalary.Text))
                {
                    try { startSalary = Convert.ToDecimal(tbStartSalary.Text); }
                    catch { throw new Exception("Invalid or empty Starting Salary."); }
                }
                // Note not required
                if (!string.IsNullOrEmpty(tbNote.Text))
                {
                    note = tbNote.Text;
                }
                 int iCode = wac.participantWAC_position_add(FK_ParticipantWAC,positionCode,startDate,finishDate,startSalary,note,
                     Session["userName"].ToString(),ref pk);
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Position.ChangeMode(FormViewMode.ReadOnly);
                    PK_ParticipantPosition = Convert.ToInt32(pk);
                    BindHR_WACEmployee_Position();
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "position"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }      
    }

    public void fvHR_WACEmployee_Position_ItemDeleting(object sender, EventArgs e)
    {
        int iCode = 0;
        PK_ParticipantPosition = Convert.ToInt32(fvHR_WACEmployee_Position.DataKey.Value);
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wac.participantWAC_position_delete(PK_ParticipantPosition, 
                    Session["userName"].ToString());
                if (iCode == 0) 
                {
                    fvHR_WACEmployee_Position.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Position(); 
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "position"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    private void BindHR_WACEmployee_Position()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var x = wac.participantWAC_positions.Where(w => w.pk_participantWAC_position == PK_ParticipantPosition).Select(s => s);
            fvHR_WACEmployee_Position.DataSource = x;
            fvHR_WACEmployee_Position.DataKeyNames = new string[] { "pk_participantWAC_position" };
            fvHR_WACEmployee_Position.DataBind();
            if (fvHR_WACEmployee_Position.CurrentMode == FormViewMode.Insert)
            {
                ddlPositionPopulate(fvHR_WACEmployee_Position.FindControl("ddlPosition") as DropDownList, null, true);
            }
            else if (fvHR_WACEmployee_Position.CurrentMode == FormViewMode.Edit)
            {
                ddlPositionPopulate(fvHR_WACEmployee_Position.FindControl("ddlPosition") as DropDownList, x.Single().fk_positionWAC_code, false);
            }
        }
    }

    public static void ddlPositionPopulate(DropDownList ddl, string sValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "PositionName";
                ddl.DataValueField = "PositionCode";
                ddl.DataSource = wac.list_positionWACs.OrderBy(o => o.position).Select(s => new
                {
                    PositionCode = s.pk_positionWAC_code,
                    PositionName = s.position
                }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(sValue)) ddl.SelectedValue = sValue; }
                catch { }
            }
        }
    }

}
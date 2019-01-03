using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Collections;
using WAC_DataObjects;
using WAC_CustomControls;

public partial class HR_WACHR_EmployeeDetail : System.Web.UI.UserControl
{
    public int PK_ParticipantWAC
    {
        get { return Convert.ToInt32(Session["PK_ParticipantWAC"]) == 0 ? -1 : Convert.ToInt32(Session["PK_ParticipantWAC"]); }
        set { Session["PK_ParticipantWAC"] = value; }
    }
    public int PK_Participant
    {
        get { return Convert.ToInt32(Session["PK_Participant"]) == 0 ? -1 : Convert.ToInt32(Session["PK_Participant"]); }
        set { Session["PK_Participant"] = value; }
    }
    public int PK_Participant_EmergencyContact
    {
        get { return Convert.ToInt32(Session["PK_Participant_EmergencyContact"]) == 0 ? -1 : Convert.ToInt32(Session["PK_Participant_EmergencyContact"]); }
        set { Session["PK_Participant_EmergencyContact"] = value; }
    }
    #region Page Load Events
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.HR_WAC);
        }
        //
        OnPositionOpen += new HR_WACHR_Position.OpenFormViewEventHandler(pnlHR_Position.OpenFormView);
        OnActivityOpen += new HR_WACHR_Activity.OpenFormViewEventHandler(pnlHR_Activity.OpenFormView);
        OnNoteOpen += new HR_WACHR_Note.OpenFormViewEventHandler(pnlHR_Note.OpenFormView);
        OnEvaluationOpen += new HR_WACHR_Evaluation.OpenFormViewEventHandler(pnlHR_Evaluation.OpenFormView);
        OnTrainingOpen += new HR_WACHR_Training.OpenFormViewEventHandler(pnlHR_Training.OpenFormView);
        OnPhoneOpen += new HR_WACHR_Phone.OpenFormViewEventHandler(pnlHR_Phone.OpenFormView);
        pnlHR_Note.OnFormActionCompleted += new HR_WACHR_Note.FormActionCompletedEventHandler(this.OnSubtableActionComplete);
        pnlHR_Position.OnFormActionCompleted += new HR_WACHR_Position.FormActionCompletedEventHandler(this.OnSubtableActionComplete);
        pnlHR_Activity.OnFormActionCompleted += new HR_WACHR_Activity.FormActionCompletedEventHandler(this.OnSubtableActionComplete); 
        pnlHR_Evaluation.OnFormActionCompleted += new HR_WACHR_Evaluation.FormActionCompletedEventHandler(this.OnSubtableActionComplete);
        pnlHR_Training.OnFormActionCompleted += new HR_WACHR_Training.FormActionCompletedEventHandler(this.OnSubtableActionComplete);
        pnlHR_Phone.OnFormActionCompleted += new HR_WACHR_Phone.FormActionCompletedEventHandler(this.OnSubtableActionComplete);
        AjaxControlToolkit.TabContainer tcHR_WACEmployee = fvHR_WACEmployee.FindControl("tcHR_WACEmployee") as AjaxControlToolkit.TabContainer;
        //if (tcHR_WACEmployee != null)
        //{
        //    int tabIndex = WACGlobal_Methods.KeyAsInt(Session["ActiveTabIndex"]);
        //    if (tabIndex > 0)
        //        tcHR_WACEmployee.ActiveTab = tcHR_WACEmployee.Tabs[tabIndex];
        //}
    }
    #endregion

    #region Invoked Methods

    public int Delegate_GetHRWACEmployeesPK()
    {
        return Convert.ToInt32(fvHR_WACEmployee.DataKey.Value);
    }
    
    #endregion

    #region Events and Handlers

    protected void tcHR_WACEmployee_TabChanged(object sender, EventArgs e)
    {
        AjaxControlToolkit.TabContainer t = (AjaxControlToolkit.TabContainer)sender;
        switch (t.ActiveTab.HeaderText)
        {
            case "Details":
                BindHR_WACEmployee(PK_ParticipantWAC);
                upHR_WACEmployee.Update();
                break;
            case "Activity":
               // WACAlert.Show(t.ActiveTab.HeaderText, 0);
                break;
            case "Evaluation":
               // WACAlert.Show(t.ActiveTab.HeaderText, 0);
                break;
            case "Note":
              //  WACAlert.Show(t.ActiveTab.HeaderText, 0);
                break;
            case "Phone":
             //   WACAlert.Show(t.ActiveTab.HeaderText, 0);
                break;
            case "Position":
              //  WACAlert.Show(t.ActiveTab.HeaderText, 0);
                break;
           case "Training":
               // WACAlert.Show(t.ActiveTab.HeaderText, 0);
                break;
            default:
                break;
        }
    }
    public void OnSubtableActionComplete(object sender, FormViewEventArgs e)
    {
        AjaxControlToolkit.TabContainer t = WACGlobal_Methods.FindControl<AjaxControlToolkit.TabContainer>(this);
        lbHR_Modal_Close_Click(sender, e);
        Control c = sender as Control;
        string cID = c.ID.Substring(c.ID.LastIndexOf("_") + 1);
        switch (cID)
        {
            case "Details":
                t.ActiveTabIndex = 0;
                break;
            case "Activity":
                t.ActiveTabIndex = 1;
                break;
            case "Evaluation":
                t.ActiveTabIndex = 2;
                break;
            case "Note":
                t.ActiveTabIndex = 3;
                break;
            case "Phone":
                t.ActiveTabIndex = 4;
                break;
            case "Position":
                t.ActiveTabIndex = 5;
                break;
            case "Training":
                t.ActiveTabIndex = 6;
                break;
            default:
                break;
        }
        upHR_WACEmployee.Update();     
    }

    public event WACHR_HRFilters.FilterChangedEventHandler OnEmployeeDetailsClosed;
    public event HR_WACHR_Position.OpenFormViewEventHandler OnPositionOpen;
    public event HR_WACHR_Activity.OpenFormViewEventHandler OnActivityOpen;
    public event HR_WACHR_Note.OpenFormViewEventHandler OnNoteOpen;
    public event HR_WACHR_Evaluation.OpenFormViewEventHandler OnEvaluationOpen;
    public event HR_WACHR_Training.OpenFormViewEventHandler OnTrainingOpen;
    public event HR_WACHR_Phone.OpenFormViewEventHandler OnPhoneOpen;
    
    
    public void HRDetails_Close(object sender, FilterChangedEventArgs f)
    {
        Session["EmployeeDetails"] = false;
        fvHR_WACEmployee.ChangeMode(FormViewMode.ReadOnly);
        fvHR_WACEmployee.DataSource = "";
        fvHR_WACEmployee.DataBind();
        upHR_WACEmployee.Update();
    }
    public void HRDetails_Cancel_Click(object sender, CommandEventArgs c)
    {
        FilterChangedEventArgs args = new FilterChangedEventArgs(new List<string>());
        HRDetails_Close(sender,args);
        OnEmployeeDetailsClosed(this, args);
    }

    public void WACEmployee_View_Click(PrimaryKeyEventArgs p)
    {
        fvHR_WACEmployee.ChangeMode(FormViewMode.ReadOnly);
        PK_ParticipantWAC = p.PrimaryKey;
        BindHR_WACEmployee(p.PrimaryKey);
        upHR_WACEmployee.Update();
        Session["EmployeeDetails"] = true;
    }
    public void WACEmployee_Insert_Click()
    {
        fvHR_WACEmployee.ChangeMode(FormViewMode.Insert);
        BindHR_WACEmployee(-1);
        upHR_WACEmployee.Update();
    }
    
    protected void lbHR_WACEmployees_Subtable_Insert_Click(object sender, EventArgs e)
    {
        HideModalPanels();
        LinkButton btn = (LinkButton)sender;
        int fk_participantWAC = Convert.ToInt32(fvHR_WACEmployee.DataKey.Value);
        switch (btn.CommandArgument)
        {
            case "activity":
                hfHR_Modal.Value = "activity";
                lblHR_Modal_Title.Text = "Activity: Insert New Record";
                pnlHR_Modal_Activity.Visible = true;
                OnActivityOpen(this, new FormViewEventArgs(-1,fk_participantWAC,FormViewMode.Insert));
                break;
            case "evaluation":
                hfHR_Modal.Value = "evaluation";
                lblHR_Modal_Title.Text = "Evaluation: Insert New Record";
                pnlHR_Modal_Evaluation.Visible = true;
                OnEvaluationOpen(this,new FormViewEventArgs(-1,fk_participantWAC,FormViewMode.Insert));
                break;
            case "note":
                hfHR_Modal.Value = "note";
                lblHR_Modal_Title.Text = "Note: Insert New Record";
                pnlHR_Modal_Note.Visible = true;
                OnNoteOpen(this, new FormViewEventArgs(-1,fk_participantWAC,FormViewMode.Insert));
                break;
            case "phone":
                hfHR_Modal.Value = "phone";
                lblHR_Modal_Title.Text = "Phone: Insert New Record";
                pnlHR_Modal_Phone.Visible = true;
                OnPhoneOpen(this, new FormViewEventArgs(-1, fk_participantWAC, FormViewMode.Insert));
                break;
            case "position":
                hfHR_Modal.Value = "position";
                lblHR_Modal_Title.Text = "Position: Insert New Record";
                pnlHR_Modal_Position.Visible = true;
                OnPositionOpen(this, new FormViewEventArgs(-1,fk_participantWAC,FormViewMode.Insert));
                break;
            case "training":
                hfHR_Modal.Value = "training";
                lblHR_Modal_Title.Text = "Training: Insert New Record";
                pnlHR_Modal_Training.Visible = true;
                OnTrainingOpen(this, new FormViewEventArgs(-1, fk_participantWAC, FormViewMode.Insert));
                break;
        }
        mpeHR_Modal.Show();
        upHR_Modal.Update();
    }

    protected void fvHR_WACEmployee_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvHR_WACEmployee.ChangeMode(e.NewMode);
        BindHR_WACEmployee(Convert.ToInt32(fvHR_WACEmployee.DataKey.Value));
    }

    protected void fvHR_WACEmployee_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlWACLocation = fvHR_WACEmployee.FindControl("ddlWACLocation") as DropDownList;
        TextBox tbPhoneExt = fvHR_WACEmployee.FindControl("tbPhoneExt") as TextBox;
        TextBox tbEmail = fvHR_WACEmployee.FindControl("tbEmail") as TextBox;
        TextBox tbDOB = fvHR_WACEmployee.FindControl("AjaxCalendar_DOB").FindControl("tb") as TextBox;
        DropDownList ddlMaritalStatus = fvHR_WACEmployee.FindControl("ddlMaritalStatus") as DropDownList;
        DropDownList ddlEmergencyContact = fvHR_WACEmployee.FindControl("DropDownListByAlphabet_EmergencyContact").FindControl("ddl") as DropDownList;
        DropDownList ddlEmergencyContactRelationship = fvHR_WACEmployee.FindControl("ddlEmergencyContactRelationship") as DropDownList;
        TextBox tbAllergies = fvHR_WACEmployee.FindControl("tbAllergies") as TextBox;
        DropDownList ddlHireLetter = fvHR_WACEmployee.FindControl("ddlHireLetter") as DropDownList;
        DropDownList ddlResume = fvHR_WACEmployee.FindControl("ddlResume") as DropDownList;
        DropDownList ddlConflictOfInterest = fvHR_WACEmployee.FindControl("ddlConflictOfInterest") as DropDownList;
        DropDownList ddlReceivedManual = fvHR_WACEmployee.FindControl("ddlReceivedManual") as DropDownList;
        DropDownList ddlLENS = fvHR_WACEmployee.FindControl("ddlLENS") as DropDownList;
        //DropDownList ddlEthics = fvHR_WACEmployee.FindControl("ddlEthics") as DropDownList;
        DropDownList ddlWorkSchedule = fvHR_WACEmployee.FindControl("ddlWorkSchedule") as DropDownList;
        DropDownList ddlFlexSchedule = fvHR_WACEmployee.FindControl("ddlFlexSchedule") as DropDownList;
        DropDownList ddlNWD = fvHR_WACEmployee.FindControl("ddlNWD") as DropDownList;
        DropDownList ddlConfidentialityAgreement = fvHR_WACEmployee.FindControl("ddlConfidentialityAgreement") as DropDownList;
        DropDownList ddlPersonalInfoSheet = fvHR_WACEmployee.FindControl("ddlPersonalInfoSheet") as DropDownList;
        DropDownList ddlSLT = fvHR_WACEmployee.FindControl("ddlSLT") as DropDownList;
        DropDownList ddlFieldStaff = fvHR_WACEmployee.FindControl("ddlFieldStaff") as DropDownList;
        TextBox tbTeleCommuteDate = fvHR_WACEmployee.FindControl("AjaxCalendar_TeleCommuteDate").FindControl("tb") as TextBox;
        DropDownList ddlTeleCommuteSchedule = fvHR_WACEmployee.FindControl("ddlTeleCommuteSchedule") as DropDownList;
        CustomControls_AjaxCalendar calStartDate = fvHR_WACEmployee.FindControl("calStartDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar calFinishDate = fvHR_WACEmployee.FindControl("calFinishDate") as CustomControls_AjaxCalendar;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var x = wac.participantWACs.Where(w => w.pk_participantWAC == Convert.ToInt32(fvHR_WACEmployee.DataKey.Value)).Select(s => s).Single();

                if (!string.IsNullOrEmpty(ddlWACLocation.SelectedValue)) x.fk_wacLocation_code = ddlWACLocation.SelectedValue;
                else x.fk_wacLocation_code = null;

                if (!string.IsNullOrEmpty(tbPhoneExt.Text)) x.phone_ext = tbPhoneExt.Text;
                else x.phone_ext = null;

                if (!string.IsNullOrEmpty(tbEmail.Text)) x.email = tbEmail.Text;
                else x.email = null;

                if (!string.IsNullOrEmpty(tbDOB.Text))
                {
                    try { x.dob = Convert.ToDateTime(tbDOB.Text); }
                    catch { sb.Append("Date of Birth was not updated. Invalid Date. "); }
                }
                else x.dob = null;

                if (!string.IsNullOrEmpty(ddlMaritalStatus.SelectedValue)) x.fk_maritalStatus_code = ddlMaritalStatus.SelectedValue;
                else x.fk_maritalStatus_code = null;

                if (!string.IsNullOrEmpty(ddlEmergencyContact.SelectedValue)) x.fk_participant_emergency = Convert.ToInt32(ddlEmergencyContact.SelectedValue);
                else x.fk_participant_emergency = null;

                if (!string.IsNullOrEmpty(ddlEmergencyContactRelationship.SelectedValue)) x.fk_employeeRelationship_code = ddlEmergencyContactRelationship.SelectedValue;
                else x.fk_employeeRelationship_code = null;

                if (!string.IsNullOrEmpty(tbAllergies.Text)) x.allergies = tbAllergies.Text;
                else x.allergies = null;

                if (!string.IsNullOrEmpty(ddlHireLetter.SelectedValue)) x.hireLetter = ddlHireLetter.SelectedValue;
                else x.hireLetter = null;

                if (!string.IsNullOrEmpty(ddlResume.SelectedValue)) x.resume = ddlResume.SelectedValue;
                else x.resume = null;

                if (!string.IsNullOrEmpty(ddlConflictOfInterest.SelectedValue)) x.conflictOfInterest = ddlConflictOfInterest.SelectedValue;
                else x.conflictOfInterest = null;

                if (!string.IsNullOrEmpty(ddlReceivedManual.SelectedValue)) x.recdManual = ddlReceivedManual.SelectedValue;
                else x.recdManual = null;

                if (!string.IsNullOrEmpty(ddlLENS.SelectedValue)) x.LENS = ddlLENS.SelectedValue;
                else x.LENS = null;

                //if (!string.IsNullOrEmpty(ddlEthics.SelectedValue)) x.ethics = ddlEthics.SelectedValue;
                //else x.ethics = null;

                if (!string.IsNullOrEmpty(ddlWorkSchedule.SelectedValue)) x.workSchedule = ddlWorkSchedule.SelectedValue;
                else x.workSchedule = null;
                if (!string.IsNullOrEmpty(ddlFlexSchedule.SelectedValue)) x.flexSchedule = ddlFlexSchedule.SelectedValue;
                else x.flexSchedule = null;
                if (!string.IsNullOrEmpty(ddlConfidentialityAgreement.SelectedValue)) x.confidentialityAgreement = ddlConfidentialityAgreement.SelectedValue;
                else x.confidentialityAgreement = null;

                if (!string.IsNullOrEmpty(ddlNWD.SelectedValue)) x.fk_NWD_code = ddlNWD.SelectedValue;
                else x.fk_NWD_code = null;

                if (!string.IsNullOrEmpty(ddlPersonalInfoSheet.SelectedValue)) x.personalInfoSheet = ddlPersonalInfoSheet.SelectedValue;
                else x.personalInfoSheet = null;

                if (!string.IsNullOrEmpty(ddlSLT.SelectedValue)) x.SLT = ddlSLT.SelectedValue;
                else x.SLT = null;

                if (!string.IsNullOrEmpty(ddlFieldStaff.SelectedValue)) x.fieldStaff = ddlFieldStaff.SelectedValue;
                else x.fieldStaff = null;

                if (!string.IsNullOrEmpty(tbTeleCommuteDate.Text))
                {
                    try { x.teleCommute_date = Convert.ToDateTime(tbTeleCommuteDate.Text); }
                    catch { sb.Append("TeleCommute Date was not updated. Invalid Date. "); }
                }
                else x.teleCommute_date = null;

                if (!string.IsNullOrEmpty(ddlTeleCommuteSchedule.SelectedValue)) x.fk_telecommuteSchedule_code = ddlTeleCommuteSchedule.SelectedValue;
                else x.fk_telecommuteSchedule_code = null;

                x.start_date = calStartDate.CalDateNullable;
                x.finish_date = calFinishDate.CalDateNullable;

                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();

                wac.SubmitChanges();
                fvHR_WACEmployee.ChangeMode(FormViewMode.ReadOnly);
                BindHR_WACEmployee(Convert.ToInt32(fvHR_WACEmployee.DataKey.Value));
                upHR_WACEmployee.Update();
                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? pk = null;
        int iCode = 0;

        DropDownList ddlWACEmployees = fvHR_WACEmployee.FindControl("ddlWACEmployees") as DropDownList;
        DropDownList ddlWACLocation = fvHR_WACEmployee.FindControl("ddlWACLocation") as DropDownList;
        TextBox tbPhoneExt = fvHR_WACEmployee.FindControl("tbPhoneExt") as TextBox;
        TextBox tbEmail = fvHR_WACEmployee.FindControl("tbEmail") as TextBox;
        TextBox tbDOB = fvHR_WACEmployee.FindControl("AjaxCalendar_DOB").FindControl("tb") as TextBox;
        DropDownList ddlMaritalStatus = fvHR_WACEmployee.FindControl("ddlMaritalStatus") as DropDownList;
        DropDownList ddlEmergencyContact = fvHR_WACEmployee.FindControl("DropDownListByAlphabet_EmergencyContact").FindControl("ddl") as DropDownList;
        DropDownList ddlEmergencyContactRelationship = fvHR_WACEmployee.FindControl("ddlEmergencyContactRelationship") as DropDownList;
        TextBox tbAllergies = fvHR_WACEmployee.FindControl("tbAllergies") as TextBox;
        DropDownList ddlHireLetter = fvHR_WACEmployee.FindControl("ddlHireLetter") as DropDownList;
        DropDownList ddlResume = fvHR_WACEmployee.FindControl("ddlResume") as DropDownList;
        DropDownList ddlConflictOfInterest = fvHR_WACEmployee.FindControl("ddlConflictOfInterest") as DropDownList;
        DropDownList ddlReceivedManual = fvHR_WACEmployee.FindControl("ddlReceivedManual") as DropDownList;
        DropDownList ddlLENS = fvHR_WACEmployee.FindControl("ddlLENS") as DropDownList;
        //DropDownList ddlEthics = fvHR_WACEmployee.FindControl("ddlEthics") as DropDownList;
        DropDownList ddlWorkSchedule = fvHR_WACEmployee.FindControl("ddlWorkSchedule") as DropDownList;
        DropDownList ddlFlexSchedule = fvHR_WACEmployee.FindControl("ddlFlexSchedule") as DropDownList;
        DropDownList ddlNWD = fvHR_WACEmployee.FindControl("ddlNWD") as DropDownList;
        DropDownList ddlConfidentialityAgreement = fvHR_WACEmployee.FindControl("ddlConfidentialityAgreement") as DropDownList;
        DropDownList ddlPersonalInfoSheet = fvHR_WACEmployee.FindControl("ddlPersonalInfoSheet") as DropDownList;
        DropDownList ddlSLT = fvHR_WACEmployee.FindControl("ddlSLT") as DropDownList;
        DropDownList ddlFieldStaff = fvHR_WACEmployee.FindControl("ddlFieldStaff") as DropDownList;
        //TextBox tbTeleCommuteDate = fvHR_WACEmployee.FindControl("AjaxCalendar_TeleCommuteDate").FindControl("tb") as TextBox;
        CustomControls_AjaxCalendar calTeleCommuteDate = fvHR_WACEmployee.FindControl("AjaxCalendar_TeleCommuteDate") as CustomControls_AjaxCalendar;
        DropDownList ddlTeleCommuteSchedule = fvHR_WACEmployee.FindControl("ddlTeleCommuteSchedule") as DropDownList;
        DropDownList ddlPosition = fvHR_WACEmployee.FindControl("ddlPosition") as DropDownList;
        //TextBox tbStartDate = fvHR_WACEmployee.FindControl("AjaxCalendar_StartDate").FindControl("tb") as TextBox;
        CustomControls_AjaxCalendar calPositionStartDate = fvHR_WACEmployee.FindControl("AjaxCalendar_StartDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar calStartDate = fvHR_WACEmployee.FindControl("calStartDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar calFinishDate = fvHR_WACEmployee.FindControl("calFinishDate") as CustomControls_AjaxCalendar;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int? iParticipant = null;
                if (!string.IsNullOrEmpty(ddlWACEmployees.SelectedValue)) iParticipant = Convert.ToInt32(ddlWACEmployees.SelectedValue);
                else throw new Exception("Participant is required. ");

                string sWACLocation = null;
                if (!string.IsNullOrEmpty(ddlWACLocation.SelectedValue)) sWACLocation = ddlWACLocation.SelectedValue;

                string sPhoneExt = null;
                if (!string.IsNullOrEmpty(tbPhoneExt.Text)) sPhoneExt = tbPhoneExt.Text;

                string sEmail = null;
                if (!string.IsNullOrEmpty(tbEmail.Text)) sEmail = tbEmail.Text;

                DateTime? dtDOB = null;
                if (!string.IsNullOrEmpty(tbDOB.Text))
                {
                    try { dtDOB = Convert.ToDateTime(tbDOB.Text); }
                    catch { throw new Exception("Incorrect Date Format for Date of Birth. "); }
                }

                string sFK_MaritalStatus = null;
                if (!string.IsNullOrEmpty(ddlMaritalStatus.SelectedValue)) sFK_MaritalStatus = ddlMaritalStatus.SelectedValue;

                int? sFK_EmergencyContact = null;
                if (!string.IsNullOrEmpty(ddlEmergencyContact.SelectedValue)) sFK_EmergencyContact = Convert.ToInt32(ddlEmergencyContact.SelectedValue);

                string sFK_EmployeeRelationship = null;
                if (!string.IsNullOrEmpty(ddlEmergencyContactRelationship.SelectedValue)) sFK_EmployeeRelationship = ddlEmergencyContactRelationship.SelectedValue;

                string sAllergies = null;
                if (!string.IsNullOrEmpty(tbAllergies.Text)) sAllergies = tbAllergies.Text;

                string sHireLetter = null;
                if (!string.IsNullOrEmpty(ddlHireLetter.SelectedValue)) sHireLetter = ddlHireLetter.SelectedValue;

                string sResume = null;
                if (!string.IsNullOrEmpty(ddlResume.SelectedValue)) sResume = ddlResume.SelectedValue;

                string sConflictOfInterest = null;
                if (!string.IsNullOrEmpty(ddlConflictOfInterest.SelectedValue)) sConflictOfInterest = ddlConflictOfInterest.SelectedValue;

                string sReceivedManual = null;
                if (!string.IsNullOrEmpty(ddlReceivedManual.SelectedValue)) sReceivedManual = ddlReceivedManual.SelectedValue;

                string sLENS = null;
                if (!string.IsNullOrEmpty(ddlLENS.SelectedValue)) sLENS = ddlLENS.SelectedValue;

                //string sEthics = null;
                //if (!string.IsNullOrEmpty(ddlEthics.SelectedValue)) sEthics = ddlEthics.SelectedValue;

                string sWorkSchedule = null;
                if (!string.IsNullOrEmpty(ddlWorkSchedule.SelectedValue)) sWorkSchedule = ddlWorkSchedule.SelectedValue;

                string sFlexSchedule = null;
                if (!string.IsNullOrEmpty(ddlFlexSchedule.SelectedValue)) sFlexSchedule = ddlFlexSchedule.SelectedValue;

                string sNWD = null;
                if (!string.IsNullOrEmpty(ddlNWD.SelectedValue)) sNWD = ddlNWD.SelectedValue;

                string sConfidentialityAgreement = null;
                if (!string.IsNullOrEmpty(ddlConfidentialityAgreement.SelectedValue)) sConfidentialityAgreement = ddlConfidentialityAgreement.SelectedValue;

                string sPersonalInfoSheet = null;
                if (!string.IsNullOrEmpty(ddlPersonalInfoSheet.SelectedValue)) sPersonalInfoSheet = ddlPersonalInfoSheet.SelectedValue;

                string sSLT = null;
                if (!string.IsNullOrEmpty(ddlSLT.SelectedValue)) sSLT = ddlSLT.SelectedValue;

                string sFieldStaff = null;
                if (!string.IsNullOrEmpty(ddlFieldStaff.SelectedValue)) sFieldStaff = ddlFieldStaff.SelectedValue;

                DateTime? dtTeleCommuteDate = calTeleCommuteDate.CalDateNullable;

                string sTeleCommuteSched = null;
                if (!string.IsNullOrEmpty(ddlTeleCommuteSchedule.SelectedValue)) sTeleCommuteSched = ddlTeleCommuteSchedule.SelectedValue;

                //if (!string.IsNullOrEmpty(tbTeleCommuteDate.Text))
                //{
                //    try { dtTeleCommuteDate = Convert.ToDateTime(tbTeleCommuteDate.Text); }
                //    catch { throw new Exception("Incorrect Date Format for TeleCommute Date. "); }
                //}
                // add position here is optional
                string positionCode = null;
               
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue))
                {
                    positionCode = ddlPosition.SelectedValue;
                }
                DateTime? startDate = calPositionStartDate.CalDateNullable;
                DateTime? start = calStartDate.CalDateNullable;
                DateTime? finish = calFinishDate.CalDateNullable;

                iCode = wac.participantWAC_add(iParticipant, sPhoneExt, sEmail, dtDOB, sWACLocation, sFK_MaritalStatus, sAllergies, sFK_EmergencyContact,
                    sFK_EmployeeRelationship, sHireLetter, sResume, sConflictOfInterest, sReceivedManual, sLENS, sFlexSchedule, sWorkSchedule, sConfidentialityAgreement,
                    sPersonalInfoSheet, dtTeleCommuteDate, sSLT, sFieldStaff, positionCode, startDate, start, finish, sNWD, sTeleCommuteSched, Session["userName"].ToString(), ref pk);
                
                if (iCode == 0)
                {
                    PK_ParticipantWAC = Convert.ToInt32(pk);
                    fvHR_WACEmployee.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee(PK_ParticipantWAC);
                }
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantWAC_delete(Convert.ToInt32(fvHR_WACEmployee.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        fvHR_WACEmployee.ChangeMode(FormViewMode.ReadOnly);
                        BindHR_WACEmployee(-1);
                        HRDetails_Cancel_Click(this, new CommandEventArgs("delete",null));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
        }
    }

    #endregion
    #region Subtable - Activity

    protected void gvHR_WACEmployees_Activity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HideModalPanels();
        hfHR_Modal.Value = "activity";
        lblHR_Modal_Title.Text = "Activity: View/Edit/Delete Record";
        pnlHR_Modal_Activity.Visible = true;
        OnActivityOpen(this,new FormViewEventArgs(Convert.ToInt32(gvHR_WACEmployees_Activity.DataKeys[Convert.ToInt32(e.CommandArgument)].Value),
            PK_ParticipantWAC, FormViewMode.ReadOnly));
        mpeHR_Modal.Show();
        upHR_Modal.Update();
    }

    #endregion

    #region Subtable - Evaluation

    protected void gvHR_WACEmployees_Evaluation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HideModalPanels();
        hfHR_Modal.Value = "evaluation";
        lblHR_Modal_Title.Text = "Evaluation: View/Edit/Delete Record";
        pnlHR_Modal_Evaluation.Visible = true;
        OnEvaluationOpen(this, new FormViewEventArgs(Convert.ToInt32(gvHR_WACEmployees_Evaluation.DataKeys[Convert.ToInt32(e.CommandArgument)]
            .Value),PK_ParticipantWAC, FormViewMode.ReadOnly));
        mpeHR_Modal.Show();
        upHR_Modal.Update();
    }

    #endregion

    #region Subtable - Note

    protected void gvHR_WACEmployees_Note_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HideModalPanels();
        lblHR_Modal_Title.Text = "Note: View/Edit/Delete Record";
        pnlHR_Modal_Note.Visible = true;
        OnNoteOpen(this, new FormViewEventArgs(Convert.ToInt32(gvHR_WACEmployees_Note.DataKeys[Convert.ToInt32(e.CommandArgument)].Value),
            PK_ParticipantWAC, FormViewMode.ReadOnly));
        mpeHR_Modal.Show();
        upHR_Modal.Update();
    }

    #endregion

    #region Subtable - Phone
    
    protected void gvHR_WACEmployees_Phone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HideModalPanels();
        lblHR_Modal_Title.Text = "Phone: View/Edit/Delete Record";
        pnlHR_Modal_Phone.Visible = true;
        OnPhoneOpen(this, new FormViewEventArgs(Convert.ToInt32(gvHR_WACEmployees_Phone.DataKeys[Convert.ToInt32(e.CommandArgument)].Value), 
            PK_ParticipantWAC, FormViewMode.ReadOnly));
        mpeHR_Modal.Show();
        upHR_Modal.Update();
    }
    private void bindGVHR_WACEmployees_Phone(WACDataClassesDataContext wac)
    {
        var p = wac.vw_participantWAC_phones.Where(w => w.pk_participantWAC == PK_ParticipantWAC).Select(s => s).
               OrderByDescending(o => o.created);
        //var p = from pwp in wac.participantWAC_phones.Where(w => w.fk_participantWAC == PK_ParticipantWAC)
        //        from v in wac.vw_participantWAC_phones
        //        where pwp.fk_communication == v.pk_communication
        //        select new
        //        {
        //            pwp.created,
        //            pwp.created_by,
        //            pwp.modified,
        //            pwp.modified_by,
        //            pwp.fk_participantWAC,
        //            pwp.pk_participantWAC_phone,
        //            pwp.fk_communication,
        //            pwp.emergency,
        //            pwp.publicUsage,
        //            HRNumber = v.Phone_Formated_HR
        //        };
        gvHR_WACEmployees_Phone.DataSource = p;
        gvHR_WACEmployees_Phone.DataKeyNames = new string[] { "pk_participantWAC_phone" };
        gvHR_WACEmployees_Phone.DataBind();
    }

    #endregion

    #region Subtable - Position

    protected void gvHR_WACEmployees_Position_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HideModalPanels();
        hfHR_Modal.Value = "position";
        lblHR_Modal_Title.Text = "Position: View/Edit/Delete Record";
        pnlHR_Modal_Position.Visible = true;
        OnPositionOpen(this, new FormViewEventArgs(Convert.ToInt32(gvHR_WACEmployees_Position.DataKeys[Convert.ToInt32(e.CommandArgument)].Value),
            PK_ParticipantWAC, FormViewMode.ReadOnly));
        mpeHR_Modal.Show();
        upHR_Modal.Update();
    }

    #endregion

    #region Subtable - Training

    protected void gvHR_WACEmployees_Training_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HideModalPanels();
        hfHR_Modal.Value = "training";
        lblHR_Modal_Title.Text = "Training: View/Edit/Delete Record";
        pnlHR_Modal_Training.Visible = true;
        OnTrainingOpen(this, new FormViewEventArgs(Convert.ToInt32(gvHR_WACEmployees_Training.DataKeys[Convert.ToInt32(e.CommandArgument)].Value),
            PK_ParticipantWAC, FormViewMode.ReadOnly));
        mpeHR_Modal.Show();
        upHR_Modal.Update();
    }

    #endregion

    #region Control Population - Participant - WAC

    private void PopulateDDL4WACEmployeeParticipants()
    {
        DropDownList ddl = fvHR_WACEmployee.FindControl("ddlWACEmployees") as DropDownList;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "fullname_LF_dnd";
            ddl.DataValueField = "pk_participant";
            ddl.DataSource = from p in wac.participants.Where(w => w.participantInterests.Any(r => r.list_interestType.list_interest.interest == "WAC Employee"))
                             where !(from wp in wac.participantWACs select wp.fk_participant).Contains(p.pk_participant)
                             orderby p.fullname_LF_dnd
                             select new
                             {
                                 p.pk_participant,
                                 p.fullname_LF_dnd
                             };
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
        if (ddl.Items.Count == 1)
        {
            WACAlert.Show("All Participants with an interest of WAC Employee have already been added.", 0);
            fvHR_WACEmployee.ChangeMode(FormViewMode.ReadOnly);           
        }
    }

    #endregion

    #region Data Binding

    private void BindHR_WACEmployee(int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                wac.participantWAC_phoneCell_autoPopulate(i, Session["userName"].ToString());
            }
            catch (Exception)
            {
               
            }
            PK_ParticipantWAC = i;
            var x = wac.participantWACs.Where(w => w.pk_participantWAC == i).Select(s => s);
            //var x = wac.vw_participantWACs.Where(w => w.pk_participantWAC == i).Select(s => s);
            
            fvHR_WACEmployee.DataKeyNames = new string[] { "pk_participantWAC" };
            fvHR_WACEmployee.DataSource = x;
            fvHR_WACEmployee.DataBind();
           
            if (fvHR_WACEmployee.CurrentMode == FormViewMode.Insert)
            {
                PopulateDDL4WACEmployeeParticipants();
                WACGlobal_Methods.PopulateControl_DatabaseLists_WACLocation_DDL(fvHR_WACEmployee.FindControl("ddlWACLocation") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_MaritalStatus_DDL(fvHR_WACEmployee.FindControl("ddlMaritalStatus") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_EmployeeRelationship_DDL(fvHR_WACEmployee.FindControl("ddlEmergencyContactRelationship") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlHireLetter") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlResume") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlConflictOfInterest") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlReceivedManual") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlLENS") as DropDownList, null, true);
                //WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlEthics") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlWorkSchedule") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlFlexSchedule") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlConfidentialityAgreement") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlPersonalInfoSheet") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlSLT") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlFieldStaff") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PositionWAC_DDL(fvHR_WACEmployee.FindControl("ddlPosition") as DropDownList, null, true);

                DropDownList ddlNWD = fvHR_WACEmployee.FindControl("ddlNWD") as DropDownList;
                var nwdList = wac.list_NWDs.OrderBy(o => o.setting).Select(s =>
                    new DDLListItem(s.pk_NWD_code, s.setting)).ToList();
                nwdList.Insert(0, new DDLListItem(null, "[SELECT]"));
                ddlNWD.DataSource = nwdList;
                ddlNWD.DataTextField = "DataTextField";
                ddlNWD.DataValueField = "DataValueField";
                ddlNWD.DataBind();

                DropDownList ddlTeleCommuteSchedule = fvHR_WACEmployee.FindControl("ddlTeleCommuteSchedule") as DropDownList;
                var tcsList = wac.list_telecommuteSchedules.OrderBy(o => o.sort).Select(s =>
                    new DDLListItem(s.pk_telecommuteSchedule_code, s.setting)).ToList();
                tcsList.Insert(0,new DDLListItem(null,"[SELECT]"));
                ddlTeleCommuteSchedule.DataSource = tcsList;
                ddlTeleCommuteSchedule.DataTextField = "DataTextField";
                ddlTeleCommuteSchedule.DataValueField = "DataValueField";
                ddlTeleCommuteSchedule.DataBind();
                
            }
            else
            {
                if (x.Count() == 1 && fvHR_WACEmployee.CurrentMode == FormViewMode.ReadOnly)
                {
                    PK_Participant = x.Single().fk_participant;
                    UC_DocumentArchive_HR_OVER.SetupViewer();

                    gvHR_WACEmployees_Activity.DataSource = x.Single().participantWAC_activities.OrderByDescending(o => o.date);
                    gvHR_WACEmployees_Activity.DataKeyNames = new string[] { "pk_participantWAC_activity" };
                    gvHR_WACEmployees_Activity.DataBind();

                    gvHR_WACEmployees_Evaluation.DataSource = x.Single().participantWAC_evaluations.OrderByDescending(o => o.fk_fiscalYear_code);
                    gvHR_WACEmployees_Evaluation.DataKeyNames = new string[] { "pk_participantWAC_evaluation" };
                    gvHR_WACEmployees_Evaluation.DataBind();

                    gvHR_WACEmployees_Note.DataSource = x.Single().participantWAC_notes.OrderByDescending(o => o.created);
                    gvHR_WACEmployees_Note.DataKeyNames = new string[] { "pk_participantWAC_note" };
                    gvHR_WACEmployees_Note.DataBind();

                    gvHR_WACEmployees_Position.DataSource = x.Single().participantWAC_positions.OrderByDescending(o => o.created);
                    gvHR_WACEmployees_Position.DataKeyNames = new string[] { "pk_participantWAC_position" };
                    gvHR_WACEmployees_Position.DataBind();

                    gvHR_WACEmployees_Training.DataSource = x.Single().participantWAC_trainings.OrderByDescending(o => o.created);
                    gvHR_WACEmployees_Training.DataKeyNames = new string[] { "pk_participantWAC_training" };
                    gvHR_WACEmployees_Training.DataBind();

                    bindGVHR_WACEmployees_Phone(wac);
                }

                else if (fvHR_WACEmployee.CurrentMode == FormViewMode.Edit)
                {
                    if (x.Single().dob != null)
                    {
                        TextBox tbTDOB = fvHR_WACEmployee.FindControl("AjaxCalendar_DOB").FindControl("tb") as TextBox;
                        tbTDOB.Text = Convert.ToDateTime(x.Single().dob).ToShortDateString();
                    }
                    WACGlobal_Methods.PopulateControl_DatabaseLists_WACLocation_DDL(fvHR_WACEmployee.FindControl("ddlWACLocation") as DropDownList, x.Single().fk_wacLocation_code, true);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_MaritalStatus_DDL(fvHR_WACEmployee.FindControl("ddlMaritalStatus") as DropDownList, x.Single().fk_maritalStatus_code, true);
                    if (x.Single().fk_participant_emergency != null)
                    {
                        DropDownList ddlEmergencyContact = fvHR_WACEmployee.FindControl("DropDownListByAlphabet_EmergencyContact").FindControl("ddl") as DropDownList;
                        Label lblEmergencyContact = fvHR_WACEmployee.FindControl("DropDownListByAlphabet_EmergencyContact").FindControl("lblLetter") as Label;
                        WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddlEmergencyContact, lblEmergencyContact, x.Single().
                            participant.fullname_LF_dnd.Substring(0, 1), "PARTICIPANT", "", x.Single().fk_participant_emergency);
                    }
                    WACGlobal_Methods.PopulateControl_DatabaseLists_EmployeeRelationship_DDL(fvHR_WACEmployee.FindControl("ddlEmergencyContactRelationship") as DropDownList, x.Single().fk_employeeRelationship_code, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlHireLetter") as DropDownList, x.Single().hireLetter, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlResume") as DropDownList, x.Single().resume, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlConflictOfInterest") as DropDownList, x.Single().conflictOfInterest, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlReceivedManual") as DropDownList, x.Single().recdManual, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlLENS") as DropDownList, x.Single().LENS, true);
                    //WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlEthics") as DropDownList, x.Single().ethics, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlWorkSchedule") as DropDownList, x.Single().workSchedule, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlFlexSchedule") as DropDownList, x.Single().flexSchedule, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlConfidentialityAgreement") as DropDownList, x.Single().confidentialityAgreement, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlPersonalInfoSheet") as DropDownList, x.Single().personalInfoSheet, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlSLT") as DropDownList, x.Single().SLT, true);
                    WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvHR_WACEmployee.FindControl("ddlFieldStaff") as DropDownList, x.Single().fieldStaff, true);
                    if (x.Single().teleCommute_date != null)
                    {
                        TextBox tbTeleCommuteDate = fvHR_WACEmployee.FindControl("AjaxCalendar_TeleCommuteDate").FindControl("tb") as TextBox;
                        tbTeleCommuteDate.Text = Convert.ToDateTime(x.Single().teleCommute_date).ToShortDateString();
                    }                 
                    DropDownList ddlNWD = fvHR_WACEmployee.FindControl("ddlNWD") as DropDownList;
                    var nwdList = wac.list_NWDs.OrderBy(o => o.setting).Select(s =>
                        new DDLListItem(s.pk_NWD_code, s.setting)).ToList();
                    string nwdCode = x.Single().fk_NWD_code;
                    if (!string.IsNullOrEmpty(nwdCode))
                    {
                        foreach (DDLListItem item in nwdList)
                        {
                            if (nwdCode.Contains(item.DataValueField))
                            {
                                ddlNWD.SelectedValue = item.DataValueField;
                                break;
                            }
                        }
                    }
                    nwdList.Insert(0, new DDLListItem(null, "[SELECT]"));
                    ddlNWD.DataSource = nwdList;
                    ddlNWD.DataTextField = "DataTextField";
                    ddlNWD.DataValueField = "DataValueField";
                    ddlNWD.DataBind();
                    
                    DropDownList ddlTeleCommuteSchedule = fvHR_WACEmployee.FindControl("ddlTeleCommuteSchedule") as DropDownList;
                    var tcsList = wac.list_telecommuteSchedules.OrderBy(o => o.sort).Select(s =>
                        new DDLListItem(s.pk_telecommuteSchedule_code, s.setting)).ToList();
                    string tcsCode = x.Single().fk_telecommuteSchedule_code;
                    if (!string.IsNullOrEmpty(tcsCode))
                    {
                        foreach (DDLListItem item in tcsList)
                        {
                            if (tcsCode.Contains(item.DataValueField))
                            {
                                ddlTeleCommuteSchedule.SelectedValue = item.DataValueField;
                                break;
                            }
                        }
                    }
                    tcsList.Insert(0, new DDLListItem(null, "[SELECT]"));
                    ddlTeleCommuteSchedule.DataSource = tcsList;
                    ddlTeleCommuteSchedule.DataTextField = "DataTextField";
                    ddlTeleCommuteSchedule.DataValueField = "DataValueField";
                    ddlTeleCommuteSchedule.DataBind();
                }
            }
        }
    }
    public string ParticipantAddress(object _pkParticipant)
    {
        int pkParticipant = WACGlobal_Methods.KeyAsInt(_pkParticipant);
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var p = wac.vw_participantWACs.Where(w => w.pk_participantWAC == pkParticipant).Select(s =>
                new { s.Address, s.CSZ });

                return p.Single().Address + ", " + p.Single().CSZ;
            }
            catch (Exception)
            {
                return string.Empty;
            }         
        }
    }
    #endregion

    #region Modal

    protected void lbHR_Modal_Close_Click(object sender, EventArgs e)
    {
        string switchOn = null;
        if (e is FormViewEventArgs)
        {
            switchOn = ((FormViewEventArgs)e).FormType;
            PK_ParticipantWAC = ((FormViewEventArgs)e).PrimaryKey;
        }
        else
        {
            switchOn = hfHR_Modal.Value;
            PK_ParticipantWAC = Convert.ToInt32(fvHR_WACEmployee.DataKey.Value);
        }
        mpeHR_Modal.Hide();
        AjaxControlToolkit.TabContainer tcHR_WACEmployee = fvHR_WACEmployee.FindControl("tcHR_WACEmployee") as AjaxControlToolkit.TabContainer;
        int tabIndex = 0;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            switch (switchOn)
            {
                case "activity":
                    gvHR_WACEmployees_Activity.DataSource = wac.participantWAC_activities.Where(w => w.fk_participantWAC == 
                        PK_ParticipantWAC).OrderBy(o => o.date);
                    gvHR_WACEmployees_Activity.DataKeyNames = new string[] { "pk_participantWAC_activity" };
                    gvHR_WACEmployees_Activity.DataBind();
                    tabIndex = 1; 
                    break;
                case "evaluation":
                    gvHR_WACEmployees_Evaluation.DataSource = wac.participantWAC_evaluations.Where(w => w.fk_participantWAC ==
                        PK_ParticipantWAC).OrderByDescending(o => o.date);
                    gvHR_WACEmployees_Evaluation.DataKeyNames = new string[] { "pk_participantWAC_evaluation" };
                    gvHR_WACEmployees_Evaluation.DataBind();
                    tabIndex = 2; 
                    break;
                case "note":
                    gvHR_WACEmployees_Note.DataSource = wac.participantWAC_notes.Where(w => w.fk_participantWAC ==
                        PK_ParticipantWAC).OrderByDescending(o => o.created);
                    gvHR_WACEmployees_Note.DataKeyNames = new string[] { "pk_participantWAC_note" };
                    gvHR_WACEmployees_Note.DataBind();
                    tabIndex = 3; 
                    break;
                case "phone":
                    bindGVHR_WACEmployees_Phone(wac);
                    //gvHR_WACEmployees_Phone.DataSource = wac.participantWAC_phones.Where(w => w.fk_participantWAC ==
                    //    PK_ParticipantWAC).OrderByDescending(o => o.created);
                    //gvHR_WACEmployees_Note.DataKeyNames = new string[] { "pk_participantWAC_phone" };
                    //gvHR_WACEmployees_Phone.DataBind();
                    tabIndex = 4; 
                    break;
                case "position":
                    gvHR_WACEmployees_Position.DataSource = wac.participantWAC_positions.Where(w => w.fk_participantWAC ==
                        PK_ParticipantWAC).OrderByDescending(o => o.created);
                    gvHR_WACEmployees_Position.DataKeyNames = new string[] { "pk_participantWAC_position" };
                    gvHR_WACEmployees_Position.DataBind();
                    tabIndex = 5; 
                    break;
                case "training":
                    gvHR_WACEmployees_Training.DataSource = wac.participantWAC_trainings.Where(w => w.fk_participantWAC ==
                        PK_ParticipantWAC).OrderByDescending(o => o.created);
                    gvHR_WACEmployees_Training.DataKeyNames = new string[] { "pk_participantWAC_training" };
                    gvHR_WACEmployees_Training.DataBind();
                    tabIndex = 6;                   
                    break;
            }
            //Session["ActiveTabIndex"] = tabIndex;
            //tcHR_WACEmployee.ActiveTab = tcHR_WACEmployee.Tabs[tabIndex];
            upHR_WACEmployee.Update();
        }
        
    }

    private void HideModalPanels()
    {
        pnlHR_Modal_Activity.Visible = false;
        pnlHR_Modal_Evaluation.Visible = false;
        pnlHR_Modal_Note.Visible = false;
        pnlHR_Modal_Phone.Visible = false;
        pnlHR_Modal_Position.Visible = false;
        pnlHR_Modal_Training.Visible = false;
    }

    #endregion


}
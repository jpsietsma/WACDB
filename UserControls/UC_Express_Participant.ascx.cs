using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class UC_Express_Participant : System.Web.UI.UserControl
{
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadFormView_Participant(int iPK_Participant)
    {
        fvParticipant.ChangeMode(FormViewMode.ReadOnly);
        if (iPK_Participant == -1) fvParticipant.ChangeMode(FormViewMode.Insert);
        BindParticipant(iPK_Participant);
        mpeExpress_Participant.Show();
        upExpress_Participant.Update();
    }

    #endregion
    
    #region Event Handling

    protected void lbExpress_Participant_Close_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        hfParticipantPK.Value = "";
        UC_Express_Global_Insert1.HideGlobal_Insert_Panels(true);
        pnlGlobalInsert.Visible = false;
        object target = WACGlobal_Methods.ContainingObject(lb.Page, "InvokedMethod_SectionPage_RebindRecord");
        target.GetType().InvokeMember("InvokedMethod_SectionPage_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, target, null);
        mpeExpress_Participant.Hide();
    }

    protected void fvParticipant_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvParticipant.ChangeMode(e.NewMode);
        if (!string.IsNullOrEmpty(hfParticipantPK.Value)) BindParticipant(Convert.ToInt32(hfParticipantPK.Value));
        else lbExpress_Participant_Close_Click(null, null);
    }

    protected void fvParticipant_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlActive = fvParticipant.FindControl("ddlActive") as DropDownList;
        DropDownList ddlPrefix = fvParticipant.FindControl("ddlPrefix") as DropDownList;
        TextBox tbNameFirst = fvParticipant.FindControl("tbNameFirst") as TextBox;
        TextBox tbNameMiddle = fvParticipant.FindControl("tbNameMiddle") as TextBox;
        TextBox tbNameLast = fvParticipant.FindControl("tbNameLast") as TextBox;
        DropDownList ddlSuffix = fvParticipant.FindControl("ddlSuffix") as DropDownList;
        TextBox tbNickname = fvParticipant.FindControl("tbNickname") as TextBox;
        DropDownList ddlOrganization = fvParticipant.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("ddl") as DropDownList;
        TextBox tbEmail = fvParticipant.FindControl("tbEmail") as TextBox;
        TextBox tbWeb = fvParticipant.FindControl("tbWeb") as TextBox;
        DropDownList ddlRegionWAC = fvParticipant.FindControl("ddlRegionWAC") as DropDownList;
        DropDownList ddlMailingStatus = fvParticipant.FindControl("ddlMailingStatus") as DropDownList;
        DropDownList ddlGender = fvParticipant.FindControl("ddlGender") as DropDownList;
        DropDownList ddlEthnicity = fvParticipant.FindControl("ddlEthnicity") as DropDownList;
        DropDownList ddlRace = fvParticipant.FindControl("ddlRace") as DropDownList;
        DropDownList ddlDiversityData = fvParticipant.FindControl("ddlDiversityData") as DropDownList;
        Calendar calFormW9SignedDate = fvParticipant.FindControl("UC_EditCalendar_Participant_FormW9SignedDate").FindControl("cal") as Calendar;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.participants.Where(o => o.pk_participant == Convert.ToInt32(fvParticipant.DataKey.Value)).Select(s => s).Single();

                if (!string.IsNullOrEmpty(ddlActive.SelectedValue)) a.active = ddlActive.SelectedValue;

                if (!string.IsNullOrEmpty(ddlPrefix.SelectedValue)) a.fk_prefix_code = ddlPrefix.SelectedValue;
                else a.fk_prefix_code = null;

                a.fname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNameFirst.Text, 48).Trim();
                a.mname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNameMiddle.Text, 24).Trim();
                a.lname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNameLast.Text, 48).Trim();

                if (!string.IsNullOrEmpty(ddlSuffix.SelectedValue)) a.fk_suffix_code = ddlSuffix.SelectedValue;
                else a.fk_suffix_code = null;

                a.nickname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNickname.Text, 24).Trim();

                if (!string.IsNullOrEmpty(ddlOrganization.SelectedValue)) a.fk_organization = Convert.ToInt32(ddlOrganization.SelectedValue);
                else a.fk_organization = null;

                a.email = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEmail.Text, 48).Trim();
                a.web = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbWeb.Text, 96).Trim();

                if (!string.IsNullOrEmpty(ddlRegionWAC.SelectedValue)) a.fk_regionWAC_code = ddlRegionWAC.SelectedValue;
                else a.fk_regionWAC_code = null;

                a.fk_mailingStatus_code = ddlMailingStatus.SelectedValue;
                a.fk_gender_code = ddlGender.SelectedValue;
                a.fk_ethnicity_code = ddlEthnicity.SelectedValue;
                a.fk_race_code = ddlRace.SelectedValue;
                a.fk_diversityData_code = ddlDiversityData.SelectedValue;

                if (calFormW9SignedDate.SelectedDate.Year > 1900) a.form_W9_signed_date = calFormW9SignedDate.SelectedDate;
                else a.form_W9_signed_date = null;

                //if (!string.IsNullOrEmpty(hfPropertyPK.Value)) a.fk_property = Convert.ToInt32(hfPropertyPK.Value);
                //else a.fk_property = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvParticipant.ChangeMode(FormViewMode.ReadOnly);
                BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    #endregion

    #region Data Binding

    public void BindParticipant(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.participants.Where(w => w.pk_participant == i).Select(s => s);
            fvParticipant.DataKeyNames = new string[] { "pk_participant" };
            fvParticipant.DataSource = a;
            fvParticipant.DataBind();

            hfParticipantPK.Value = a.Single().pk_participant.ToString();

            if (fvParticipant.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_DDL(fvParticipant.FindControl("UC_ControlGroup_ParticipantType1").FindControl("ddlInsert") as DropDownList, null, true);
            }

            if (fvParticipant.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Prefix_DDL(fvParticipant, "ddlPrefix", a.Single().fk_prefix_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Suffix_DDL(fvParticipant.FindControl("ddlSuffix") as DropDownList, a.Single().fk_suffix_code, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(fvParticipant, "ddlRegionWAC", a.Single().fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant, "ddlActive", a.Single().active);
                WACGlobal_Methods.PopulateControl_DatabaseLists_MailingStatus_DDL(fvParticipant, "ddlMailingStatus", a.Single().fk_mailingStatus_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Gender_DDL(fvParticipant, "ddlGender", a.Single().fk_gender_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Ethnicity_DDL(fvParticipant, "ddlEthnicity", a.Single().fk_ethnicity_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Race_DDL(fvParticipant, "ddlRace", a.Single().fk_race_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DiversityData_DDL(fvParticipant, "ddlDiversityData", a.Single().fk_diversityData_code);
                WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvParticipant, "UC_EditCalendar_Participant_FormW9SignedDate", a.Single().form_W9_signed_date, null);

                if (a.Single().fk_organization != null)
                {
                    DropDownList ddl = fvParticipant.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("ddl") as DropDownList;
                    Label lblLetter = fvParticipant.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("lblLetter") as Label;
                    string sLetter = null;
                    try { sLetter = a.Single().organization.org.Substring(0, 1); }
                    catch { }
                    WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lblLetter, sLetter, "ORGANIZATION", null, a.Single().fk_organization);
                }
            }
        }
    }

    #endregion

    #region Global Insert

    protected void lbGlobal_Insert_Close_Click(object sender, EventArgs e)
    {
        UC_Express_Global_Insert1.HideGlobal_Insert_Panels(true);
        pnlGlobalInsert.Visible = false;
        BindParticipant(Convert.ToInt32(hfParticipantPK.Value));
    }

    protected void btnExpress_GlobalInsert_Click(object sender, EventArgs e)
    {
        pnlGlobalInsert.Visible = true;
    }

    #endregion
}
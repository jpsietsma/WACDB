using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_WACHR_Phone : System.Web.UI.UserControl
{
    public int FK_ParticipantWAC
    {
        get { return Convert.ToInt32(Session["FK_ParticipantWAC"]) == 0 ? -1 : Convert.ToInt32(Session["FK_ParticipantWAC"]); }
        set { Session["FK_ParticipantWAC"] = value; }
    }
    public int PK_ParticipantWAC_Phone
    {
        get { return Convert.ToInt32(Session["PK_ParticipantWAC_Phone"]) == 0 ? -1 : Convert.ToInt32(Session["PK_ParticipantWAC_Phone"]); }
        set { Session["PK_ParticipantWAC_Phone"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public delegate void OpenFormViewEventHandler(object sender, FormViewEventArgs e);
    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_ParticipantWAC = e.ForeignKey;
        PK_ParticipantWAC_Phone = e.PrimaryKey;
        fvHR_WACEmployee_Phone.ChangeMode(e.ViewMode);
        BindHR_WACEmployeePhone();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);

    protected void ddlPhone_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        PK_ParticipantWAC_Phone = Convert.ToInt32(ddl.SelectedValue);
    }

    protected void fvHR_WACEmployee_Phone_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvHR_WACEmployee_Phone.ChangeMode(e.NewMode);
        BindHR_WACEmployeePhone();
    }
    protected void fvHR_WACEmployee_Phone_ItemDeleting(object sender, EventArgs e)
    {       
        int iCode = 0;
        PK_ParticipantWAC_Phone = Convert.ToInt32(fvHR_WACEmployee_Phone.DataKey.Value);
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wac.participantWAC_phone_delete(PK_ParticipantWAC_Phone, Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Phone.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployeePhone();
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "phone"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvHR_WACEmployee_Phone_ItemUpdating(object sender, EventArgs e)
    {
        PK_ParticipantWAC_Phone = Convert.ToInt32(fvHR_WACEmployee_Phone.DataKey.Value);
        DropDownList ddl = fvHR_WACEmployee_Phone.FindControl("ddlPhone") as DropDownList;
        RadioButtonList rblPublicNumber = fvHR_WACEmployee_Phone.FindControl("rblPublicNumber") as RadioButtonList;
        RadioButtonList rblEmergencyNumber = fvHR_WACEmployee_Phone.FindControl("rblEmergencyNumber") as RadioButtonList;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var x = wac.participantWAC_phones.Where(w => w.pk_participantWAC_phone == PK_ParticipantWAC_Phone).Select(s => s).Single();
                if (!string.IsNullOrEmpty(rblPublicNumber.SelectedValue))
                    x.publicUsage = rblPublicNumber.SelectedValue;
                else
                    x.publicUsage = "N";
                if (!string.IsNullOrEmpty(rblEmergencyNumber.SelectedValue))
                    x.emergency = rblEmergencyNumber.SelectedValue;
                else
                    x.emergency = "N";
                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();
                wac.SubmitChanges();
                fvHR_WACEmployee_Phone.ChangeMode(FormViewMode.ReadOnly);
                BindHR_WACEmployeePhone();
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "phone"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }
    protected void fvHR_WACEmployee_Phone_ItemInserting(object sender, EventArgs e)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {            
            try
            {
                int pk_communication;
                string publicUsage, emergency;
                DropDownList ddl = fvHR_WACEmployee_Phone.FindControl("ddlPhone") as DropDownList;
                RadioButtonList rblPublicNumber = fvHR_WACEmployee_Phone.FindControl("rblPublicNumber") as RadioButtonList;
                RadioButtonList rblEmergencyNumber = fvHR_WACEmployee_Phone.FindControl("rblEmergencyNumber") as RadioButtonList;
                int? pk = -1;
                try { pk_communication = Convert.ToInt32(ddl.SelectedValue); }
                catch { throw new Exception("Missing or invalid phone number"); }
                if (!string.IsNullOrEmpty(rblPublicNumber.SelectedValue))
                    publicUsage = rblPublicNumber.SelectedValue;
                else
                    publicUsage = "N";
                if (!string.IsNullOrEmpty(rblEmergencyNumber.SelectedValue))
                    emergency = rblEmergencyNumber.SelectedValue;
                else
                    emergency = "N";
                int iCode = wac.participantWAC_phone_add(FK_ParticipantWAC, pk_communication, publicUsage, emergency, 
                    Session["userName"].ToString(), ref pk);
                if (iCode == 0)
                {
                    PK_ParticipantWAC_Phone = Convert.ToInt32(pk);
                    fvHR_WACEmployee_Phone.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployeePhone();
                }
                else
                    WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "phone"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); } 
        }
    }
   
    private void BindHR_WACEmployeePhone()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var p = wac.vw_participantWAC_phones.Where(w => w.pk_participantWAC_phone == PK_ParticipantWAC_Phone).Select(s => s).
                OrderByDescending(o => o.created);
                
            fvHR_WACEmployee_Phone.DataSource = p;
            fvHR_WACEmployee_Phone.DataKeyNames = new string[] { "pk_participantWAC_phone" };
            fvHR_WACEmployee_Phone.DataBind();
            if (fvHR_WACEmployee_Phone.CurrentMode == FormViewMode.Insert)
            {
                ddlPhonePopulate(fvHR_WACEmployee_Phone.FindControl("ddlPhone") as DropDownList, null, true);
            }
            else if (fvHR_WACEmployee_Phone.CurrentMode == FormViewMode.Edit)
            {
                RadioButtonList rblPublicNumber = fvHR_WACEmployee_Phone.FindControl("rblPublicNumber") as RadioButtonList;
                rblPublicNumber.SelectedIndex = p.First().publicUsage.Contains("Y") ? 0 : 1;
                RadioButtonList rblEmergencyNumber = fvHR_WACEmployee_Phone.FindControl("rblEmergencyNumber") as RadioButtonList;
                rblEmergencyNumber.SelectedIndex = p.First().emergency.Contains("Y") ? 0 : 1;
            }
        }   
    }
    public void ddlPhonePopulate(DropDownList ddl, string selectedValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "number";
                ddl.DataValueField = "pk_communication";
                ddl.DataSource = wac.vw_participantWAC_phone_eligibles.Where(w => w.pk_participantWAC == FK_ParticipantWAC).Select(s => new
                {
                    number = s.PhoneFormattedHR,
                    pk_communication = s.pk_communication
                });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(selectedValue)) ddl.SelectedValue = selectedValue; }
                catch { }
            }
        }
    }

}
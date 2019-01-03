using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_WACHR_Note : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_ParticipantWAC
    {
        get { return Convert.ToInt32(Session["FK_ParticipantWAC"]) == 0 ? -1 : Convert.ToInt32(Session["FK_ParticipantWAC"]); }
        set { Session["FK_ParticipantWAC"] = value; }
    }
    public int PK_ParticipantNote
    {
        get { return Convert.ToInt32(Session["PK_ParticipantWACNote"]) == 0 ? -1 : Convert.ToInt32(Session["PK_ParticipantWACNote"]); }
        set { Session["PK_ParticipantWACNote"] = value; }
    }
    
    public delegate void OpenFormViewEventHandler(object sender, FormViewEventArgs e);
    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_ParticipantWAC = e.ForeignKey;
        PK_ParticipantNote = e.PrimaryKey;
        fvHR_WACEmployee_Note.ChangeMode(e.ViewMode);
        BindHR_WACEmployee_Note();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
   
    public void fvHR_WACEmployee_Note_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvHR_WACEmployee_Note.ChangeMode(e.NewMode);
        BindHR_WACEmployee_Note();
    }

    public void fvHR_WACEmployee_Note_ItemUpdating(object sender, EventArgs e)
    {
        PK_ParticipantNote = Convert.ToInt32(fvHR_WACEmployee_Note.DataKey.Value);
        DropDownList ddlPosition = fvHR_WACEmployee_Note.FindControl("ddlPosition") as DropDownList;
        TextBox tbNote = fvHR_WACEmployee_Note.FindControl("tbNote") as TextBox;
    
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {        
            try
            {
                var x = wac.participantWAC_notes.Where(w => w.pk_participantWAC_note == PK_ParticipantNote).Select(s => s).Single();

                // Position - not required              
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue))
                {
                    x.fk_positionWAC_code = ddlPosition.SelectedValue;
                }
                // Note - required
                if (!string.IsNullOrEmpty(tbNote.Text))
                {
                    x.note = tbNote.Text;
                }
                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();
                wac.SubmitChanges();
                fvHR_WACEmployee_Note.ChangeMode(FormViewMode.ReadOnly);
                BindHR_WACEmployee_Note();
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "note"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    public void fvHR_WACEmployee_Note_ItemInserting(object sender, EventArgs e)
    {
        DropDownList ddlPosition = fvHR_WACEmployee_Note.FindControl("ddlPosition") as DropDownList;
        TextBox tbNote = fvHR_WACEmployee_Note.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            int? pk = null;
            string positionCode = null;
            string note = null;
            try
            {
                // Position - not required              
                if (!string.IsNullOrEmpty(ddlPosition.SelectedValue))
                {
                    positionCode = ddlPosition.SelectedValue;
                }
                // Note - required
                if (!string.IsNullOrEmpty(tbNote.Text) && !string.IsNullOrWhiteSpace(tbNote.Text))
                {
                    note = tbNote.Text;
                }
                else
                    throw new Exception("Note is a required field, please enter a note to save.");
                int iCode = wac.participantWAC_note_add(FK_ParticipantWAC, positionCode, note, Session["userName"].ToString(), 
                    ref pk);
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Note.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Note();                   
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "note"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
       
    }

    public void fvHR_WACEmployee_Note_ItemDeleting(object sender, EventArgs e)
    {
        int iCode = 0;
        PK_ParticipantNote = Convert.ToInt32(fvHR_WACEmployee_Note.DataKey.Value);
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wac.participantWAC_note_delete(PK_ParticipantNote, Session["userName"].ToString());
                if (iCode == 0)
                {
                    fvHR_WACEmployee_Note.ChangeMode(FormViewMode.ReadOnly);
                    BindHR_WACEmployee_Note();
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
                OnFormActionCompleted(this, new FormViewEventArgs(FK_ParticipantWAC, "note"));
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
        
    }

    private void BindHR_WACEmployee_Note()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var x = wac.participantWAC_notes.Where(w => w.pk_participantWAC_note == PK_ParticipantNote).Select(s => s);
            fvHR_WACEmployee_Note.DataSource = x;
            fvHR_WACEmployee_Note.DataKeyNames = new string[] { "pk_participantWAC_note" };
            fvHR_WACEmployee_Note.DataBind();
            
            if (fvHR_WACEmployee_Note.CurrentMode == FormViewMode.Insert)
            {
                ddlPositionPopulate(fvHR_WACEmployee_Note.FindControl("ddlPosition") as DropDownList, null, true);
            }
            else if (fvHR_WACEmployee_Note.CurrentMode == FormViewMode.Edit)
            {
                ddlPositionPopulate(fvHR_WACEmployee_Note.FindControl("ddlPosition") as DropDownList, x.Single().fk_positionWAC_code, true);              
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
                });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(sValue)) ddl.SelectedValue = sValue; }
                catch { }
            }
        }
    }
}
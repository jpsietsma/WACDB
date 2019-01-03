using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class AG_WACAG_Wfp3Modification : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_Wfp3
    {
        get { return Convert.ToInt32(Session["FK_Wfp3"]) == 0 ? -1 : Convert.ToInt32(Session["FK_Wfp3"]); }
        set { Session["FK_Wfp3"] = value; }
    }
    public int PK_Wfp3Mod
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Mod"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Mod"]); }
        set { ViewState["PK_Wfp3Mod"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Mod = e.PrimaryKey;
        fvAg_WFP3_Modification.ChangeMode(e.ViewMode);
        BindAg_WFP3_Modification();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    #region Event Handling - Ag - WFP3 - Modification

    protected void lbAg_WFP3_Modification_Close_Click(object sender, EventArgs e)
    {
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "Modification"));
        FormView fv = fvAg_WFP3_Modification;
        fv.DataSource = "";
        fv.DataBind();
        
    }

    protected void fvAg_WFP3_Modification_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_Modification_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_modification", "msgInsert"))
                    fvAg_WFP3_Modification.ChangeMode(FormViewMode.Insert);
                else
                    fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                bind = false;
                break;
            case "InsertData":
                fvAg_WFP3_Modification_ItemInserting(sender, null);
                fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelInsert":
                fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_modification", "msgInsert"))
                {
                    fvAg_WFP3_Modification.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_Modification_ItemUpdating(sender, null);
                fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelUpdate":
                fvAg_WFP3_Modification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_Modification_ItemDeleting(sender, null);
                lbAg_WFP3_Modification_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_Modification();
    }
    protected void fvAg_WFP3_Modification_ModeChanging(object sender, FormViewModeEventArgs e)
    { }

    protected void fvAg_WFP3_Modification_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_WFP3_Modification;

        DropDownList ddlBMP = fv.FindControl("ddlBMP") as DropDownList;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_modifications.Where(w => w.pk_form_wfp3_modification == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlBMP.SelectedValue)) a.fk_form_wfp3_bmp = Convert.ToInt32(ddlBMP.SelectedValue);
                else sb.Append("BMP was not updated. This is a required field. ");

                try { a.amount = Convert.ToDecimal(tbAmount.Text); }
                catch { sb.Append("Amount was not updated. This is a required field of type number (Decimal). "); }

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Modification_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_Modification;

        DropDownList ddlBMP = fv.FindControl("ddlBMP") as DropDownList;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iBMP = null;
                if (!string.IsNullOrEmpty(ddlBMP.SelectedValue)) iBMP = Convert.ToInt32(ddlBMP.SelectedValue);
                else sb.Append("BMP is required. ");

                decimal? dAmount = null;
                try { dAmount = Convert.ToDecimal(tbAmount.Text); }
                catch { sb.Append("Amount is required. Amount is a number (Decimal). "); }

                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_modification_add(FK_Wfp3, iBMP, dAmount, sNote, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database. " + sb.ToString(), iCode);
                    else PK_Wfp3Mod = Convert.ToInt32(i);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Modification_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_modification", "msgDelete"))
        {
            FormView fv = fvAg_WFP3_Modification;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_modification_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion
    private void BindAg_WFP3_Modification()
    {
        FormView fv = fvAg_WFP3_Modification;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_modifications.Where(w => w.pk_form_wfp3_modification == PK_Wfp3Mod).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_form_wfp3_modification" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_ByWFP3BMP_DDL(fv, "ddlBMP", FK_Wfp3, null);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_ByWFP3BMP_DDL(fv, "ddlBMP", FK_Wfp3, a.Single().fk_form_wfp3_bmp);
            }
        }
    }
}
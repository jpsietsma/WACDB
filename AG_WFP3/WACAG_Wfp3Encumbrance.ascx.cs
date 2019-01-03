using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WAC_CustomControls;

public partial class AG_WACAG_Wfp3Encumbrance : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_FarmBusiness
    {
        get { return Convert.ToInt32(Session["FK_FarmBusiness"]) == 0 ? -1 : Convert.ToInt32(Session["FK_FarmBusiness"]); }
        set { Session["FK_FarmBusiness"] = value; }
    }
    public int FK_Wfp3
    {
        get { return Convert.ToInt32(Session["FK_Wfp3"]) == 0 ? -1 : Convert.ToInt32(Session["FK_Wfp3"]); }
        set { Session["FK_Wfp3"] = value; }
    }
    public int PK_Wfp3Encumbrance
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Encumbrance"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Encumbrance"]); }
        set { ViewState["PK_Wfp3Encumbrance"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Encumbrance = e.PrimaryKey;
        fvAg_WFP3_Encumbrance.ChangeMode(e.ViewMode);
        BindAg_WFP3_Encumbrance();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    #region Event Handling - Ag - WFP3 - Encumbrance

    protected void lbAg_WFP3_Encumbrance_Close_Click(object sender, EventArgs e)
    {
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "Encumbrance"));
        FormView fv = fvAg_WFP3_Encumbrance;
        fv.DataSource = "";
        fv.DataBind();     
    }

    protected void fvAg_WFP3_Encumbrance_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_Encumbrance_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_encumbrance", "msgInsert"))
                {
                    fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.Insert);
                    PK_Wfp3Encumbrance = -1;
                }
                else
                {
                    fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                    bind = false;
                }
                break;
            case "InsertData":
                fvAg_WFP3_Encumbrance_ItemInserting(sender, null);
                fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelInsert":
                fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_encumbrance", "msgInsert"))
                {
                    fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_Encumbrance_ItemUpdating(sender, null);
                fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelUpdate":
                fvAg_WFP3_Encumbrance.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_Encumbrance_ItemDeleting(sender, null);
                lbAg_WFP3_Encumbrance_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_Encumbrance();
    }

    protected void fvAg_WFP3_Encumbrance_ModeChanging(object sender, FormViewModeEventArgs e)
    { }

    protected void fvAg_WFP3_Encumbrance_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        FormView fv = fvAg_WFP3_Encumbrance;

        DropDownList ddlEncumbrance = fv.FindControl("ddlEncumbrance") as DropDownList;
        CustomControls_AjaxCalendar ecDate = fv.FindControl("tbCalEncumbranceDate") as CustomControls_AjaxCalendar;
        DropDownList ddlType = fv.FindControl("ddlType") as DropDownList;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;
        CustomControls_AjaxCalendar approvedDate = fv.FindControl("tbCalEncumbranceApprovedDate") as CustomControls_AjaxCalendar;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_encumbrances.Where(w => w.pk_form_wfp3_encumbrance == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlEncumbrance.SelectedValue)) a.fk_encumbrance_code = ddlEncumbrance.SelectedValue;
                else sb.Append("Encumbrance was not updated. Encumbrance is required. ");

                DateTime? dt = ecDate.CalDateNullable;
                if (dt == null)
                    sb.Append("Date was not updated. Date is required. ");
                else
                    a.date = (DateTime)ecDate.CalDateNullable;

                if (!string.IsNullOrEmpty(ddlType.SelectedValue)) a.fk_encumbranceType_code = ddlType.SelectedValue;
                else a.fk_encumbranceType_code = null;

                if (!string.IsNullOrEmpty(tbAmount.Text))
                {
                    try { a.amt = Convert.ToDecimal(tbAmount.Text); }
                    catch { sb.Append("Amount was not updated. Must be a number (Decimal). "); }
                }
                else sb.Append("Amount was not updated. Amount is required. ");

                a.approved = approvedDate.CalDateNullable;

                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);
                else a.note = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Encumbrance_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_Encumbrance;

        DropDownList ddlEncumbrance = fv.FindControl("ddlEncumbrance") as DropDownList;
        CustomControls_AjaxCalendar ecDate = fvAg_WFP3_Encumbrance.FindControl("tbCalEncumbranceDate") as CustomControls_AjaxCalendar;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                if (wDataContext.form_wfp3_get_okToEncumber(FK_Wfp3) != "N")
                {
                    string sEncumbrance = null;
                    if (!string.IsNullOrEmpty(ddlEncumbrance.SelectedValue)) sEncumbrance = ddlEncumbrance.SelectedValue;
                    else sb.Append("Encumbrance is required. ");

                    DateTime? dtDate = ecDate.CalDateNotNullable;
                    if (dtDate == null) 
                        sb.Append("Date is required. ");

                    decimal? dAmount = null;
                    if (!string.IsNullOrEmpty(tbAmount.Text))
                    {
                        try 
                        { 
                            dAmount = Convert.ToDecimal(tbAmount.Text);
                            if (dAmount < (decimal)0.01)
                                sb.Append("Encumbrance ammount must be greater than zero");
                        }
                        catch { sb.Append("Amount is incorrect. Must be a number (Decimal). "); }
                    }
                    else sb.Append("Amount is required. ");

                    string sNote = null;
                    if (!string.IsNullOrEmpty(tbNote.Text)) sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                    if (string.IsNullOrEmpty(sb.ToString()))
                    {
                        iCode = wDataContext.form_wfp3_encumbrance_add(FK_Wfp3, sEncumbrance, dtDate, dAmount, sNote, Session["userName"].ToString(), ref i);
                        if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                        else PK_Wfp3Encumbrance = Convert.ToInt32(i);
                    }
                    else WACAlert.Show(sb.ToString(), iCode);
                }
                else WACAlert.Show("It is not okay to encumber this package. ", 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Encumbrance_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_Encumbrance", "msgDelete"))
        {
            FormView fv = fvAg_WFP3_Encumbrance;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_encumbrance_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode); 
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    private void BindAg_WFP3_Encumbrance()
    {
        FormView fv = fvAg_WFP3_Encumbrance;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.form_wfp3_encumbrances.Where(w => w.pk_form_wfp3_encumbrance == PK_Wfp3Encumbrance) select b;
            fv.DataKeyNames = new string[] { "pk_form_wfp3_encumbrance" };
            fv.DataSource = a;
            fv.DataBind();

            string sRegionWAC = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(FK_FarmBusiness);

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;
                try
                {
                    var x = wDataContext.form_wfp3_get_encumbranceAmt_all(FK_Wfp3);
                    tbAmount.Text = x.Single().amt_encumbrance.ToString();
                }
                catch { }
                WACGlobal_Methods.PopulateControl_DatabaseLists_Encumbrance_DDL(fv, "ddlEncumbrance", null);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Encumbrance_DDL(fv, "ddlEncumbrance", a.Single().fk_encumbrance_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_EncumbranceType_DDL(fv.FindControl("ddlType") as DropDownList, a.Single().fk_encumbranceType_code, true);
            }
        }
    }
}
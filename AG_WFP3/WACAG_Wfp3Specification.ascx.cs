using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class AG_WACAG_Wfp3Specification : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int FK_Wfp3
    {
        get { return Convert.ToInt32(ViewState["FK_Wfp3"]) == 0 ? -1 : Convert.ToInt32(ViewState["FK_Wfp3"]); }
        set { ViewState["FK_Wfp3"] = value; }
    }
    public int PK_Wfp3Spec
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Spec"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Spec"]); }
        set { ViewState["PK_Wfp3Spec"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Spec = e.PrimaryKey;
        fvAg_WFP3_Specification.ChangeMode(e.ViewMode);
        BindAg_WFP3_Specification();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    #region Event Handling - Ag - WFP3 - Specification

    protected void lbAg_WFP3_Specification_Close_Click(object sender, EventArgs e)
    {
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "Specification"));
        FormView fv = fvAg_WFP3_Specification;
        fv.DataSource = "";
        fv.DataBind();
       
    }

    protected void fvAg_WFP3_Specification_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_Specification_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_specification", "msgInsert"))
                    fvAg_WFP3_Specification.ChangeMode(FormViewMode.Insert);
                else
                    fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                bind = false;
                break;
            case "InsertData":
                fvAg_WFP3_Specification_ItemInserting(sender, null);
                fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelInsert":
                fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_specification", "msgInsert"))
                {
                    fvAg_WFP3_Specification.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_Specification_ItemUpdating(sender, null);
                fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelUpdate":
                fvAg_WFP3_Specification.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_Specification_ItemDeleting(sender, null);
                lbAg_WFP3_Specification_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_Specification();
    }
    protected void fvAg_WFP3_Specification_ModeChanging(object sender, FormViewModeEventArgs e)
    {

    }

    protected void fvAg_WFP3_Specification_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_WFP3_Specification;

        DropDownList ddlPractice = fv.FindControl("ddlPractice") as DropDownList;
        DropDownList ddlBidRequired = fv.FindControl("ddlBidRequired") as DropDownList;
        TextBox tbSortPosition = fv.FindControl("tbSortPosition") as TextBox;
        TextBox tbBid = fv.FindControl("tbBid") as TextBox;
        TextBox tUnits = fv.FindControl("tUnits") as TextBox;
        TextBox tbABC = fv.FindControl("tbABC") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_specifications.Where(w => w.pk_form_wfp3_specification == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlPractice.SelectedValue)) a.fk_bmpPractice_code = Convert.ToDecimal(ddlPractice.SelectedValue);
                else sb.Append("BMP Practice was not updated. BMP Practice is required. ");

                if (!string.IsNullOrEmpty(ddlBidRequired.SelectedValue)) a.bid_reqd = ddlBidRequired.SelectedValue;
                else a.bid_reqd = null;

                if (!string.IsNullOrEmpty(tbSortPosition.Text))
                {
                    try { a.sort_position = Convert.ToInt32(tbSortPosition.Text); }
                    catch { sb.Append("List Position was not updated. Must be a number (Integer). "); }
                }
                else a.sort_position = null;

                if (!string.IsNullOrEmpty(tbBid.Text))
                {
                    try { a.bid = Convert.ToDecimal(tbBid.Text); }
                    catch { sb.Append("Bid was not updated. Bid must be a number (Decimal)."); }
                }
                else sb.Append("Bid was not updated. Bid is required. ");

                if (!string.IsNullOrEmpty(tUnits.Text))
                {
                    try { a.units = Convert.ToDecimal(tUnits.Text); }
                    catch { sb.Append("Units was not updated. Units must be a number (Decimal)."); }
                }
                else sb.Append("Units was not updated. Units is required. ");

                if (!string.IsNullOrEmpty(tbABC.Text))
                {
                    try { a.ABC = Convert.ToDecimal(tbABC.Text); }
                    catch { sb.Append("ABC was not updated. ABC must be a number (Decimal)."); }
                }
                else sb.Append("ABC was not updated. ABC is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Specification_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_Specification;

        DropDownList ddlBMP = fv.FindControl("ddlBMP") as DropDownList;
        DropDownList ddlPractice = fv.FindControl("ddlPractice") as DropDownList;
        DropDownList ddlBidRequired = fv.FindControl("ddlBidRequired") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iBMP = null;
                if (!string.IsNullOrEmpty(ddlBMP.SelectedValue)) iBMP = Convert.ToInt32(ddlBMP.SelectedValue);
                else sb.Append("BMP is required. ");

                decimal? dPractice = null;
                if (!string.IsNullOrEmpty(ddlPractice.SelectedValue)) dPractice = Convert.ToDecimal(ddlPractice.SelectedValue);
                else sb.Append("Practice is required. ");

                string sBidRequired = null;
                if (!string.IsNullOrEmpty(ddlBidRequired.SelectedValue)) sBidRequired = ddlBidRequired.SelectedValue;

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_specification_add(FK_Wfp3, iBMP, dPractice, sBidRequired, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database. " + sb.ToString(), iCode);
                    else PK_Wfp3Spec = Convert.ToInt32(i);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Specification_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_specification", "msgDelete"))
        {
            FormView fv = fvAg_WFP3_Specification;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_specification_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_WFP3_Specification_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion


    private void BindAg_WFP3_Specification()
    {
        FormView fv = fvAg_WFP3_Specification;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_specifications.Where(w => w.pk_form_wfp3_specification == PK_Wfp3Spec).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_form_wfp3_specification" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_ByWFP3BMP_DDL(fv, "ddlBMP", FK_Wfp3, null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL(fv.FindControl("ddlPractice") as DropDownList, null, true, true, false);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv.FindControl("ddlBidRequired") as DropDownList, null, true);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_ByWFP3BMP_DDL(fv, "ddlBMP", FK_Wfp3, a.Single().fk_form_wfp3_bmp);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL(fv.FindControl("ddlPractice") as DropDownList, a.Single().fk_bmpPractice_code, true, true, false);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv.FindControl("ddlBidRequired") as DropDownList, a.Single().bid_reqd, true);
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class AG_WACAG_Wfp3Bmp : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
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
    public int PK_Wfp3Bmp
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Bmp"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Bmp"]); }
        set { ViewState["PK_Wfp3Bmp"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Bmp = e.PrimaryKey;
        fvAg_WFP3_BMP.ChangeMode(e.ViewMode);
        BindAg_WFP3_BMP();
    }

    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    #region Event Handling - Ag - WFP3 - BMP

    protected void lbAg_WFP3_BMP_Close_Click(object sender, EventArgs e)
    {       
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "BMP"));
        FormView fv = fvAg_WFP3_BMP;
        fv.DataSource = "";
        fv.DataBind();
    }

    protected void ddlAg_WFP3_BMP_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FormView fv = fvAg_WFP3_BMP;
        //DropDownList ddl = (DropDownList)sender;
        //Label lbl = fv.FindControl("lblAg_WFP3_BMP_Units") as Label;
        //decimal? d = WACGlobal_Methods.SpecialQuery_Agriculture_BMPPracticeCode_By_BMP(ddl.SelectedValue);
        //WACGlobal_Methods.PopulateControl_Custom_Agriculture_Units_By_BMPPractice(d, lbl);
        //TextBox tb = fv.FindControl("tbDimensions") as TextBox;
        //ddl.Focus();
    }
    protected void fvAg_WFP3_BMP_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        FormView fv = sender as FormView;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fv.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fv.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_BMP_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_bmp", "msgInsert"))
                    fv.ChangeMode(FormViewMode.Insert);
                else
                    fv.ChangeMode(FormViewMode.ReadOnly);
                bind = false;
                break;               
            case "InsertData":
                fvAg_WFP3_BMP_ItemInserting(sender, null);
                lbAg_WFP3_BMP_Close_Click(sender, null);
                break;
            case "CancelInsert":
                fv.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_bmp", "msgInsert"))
                {
                    fv.ChangeMode(FormViewMode.Edit);
                }
                else
                    fv.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_BMP_ItemUpdating(sender, null);
                lbAg_WFP3_BMP_Close_Click(sender, null);
                break;
            case "CancelUpdate":
                fv.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_BMP_ItemDeleting(sender, null);
                lbAg_WFP3_BMP_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_BMP();
    }

    protected void fvAg_WFP3_BMP_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        
    }

    protected void fvAg_WFP3_BMP_ItemUpdating(object sender, EventArgs e)
    {
        StringBuilder sbErrorCollection = new StringBuilder();

        FormView fv = fvAg_WFP3_BMP;
        DropDownList ddlBMP = fv.FindControl("ddlBMP") as DropDownList;
        TextBox tbUnitsDesigned = fv.FindControl("tbUnitsDesigned") as TextBox;
        TextBox tbDesignCost = fv.FindControl("tbDesignCost") as TextBox;
        //TextBox tbDimensions = fv.FindControl("tbDimensions") as TextBox;
        TextBox tbUnitsCompleted = fv.FindControl("tbUnitsCompleted") as TextBox;
        //TextBox tbDesignCostNote = fv.FindControl("tbDesignCostNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.vw_form_wfp3_BMPs.Where(w => w.pk_form_wfp3_bmp == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            int pk_form_wfp3_bmp = a.pk_form_wfp3_bmp;
            int fk_bmp_ag = a.pk_bmp_ag;
            decimal? units_designed = a.units_designed;
            decimal? design_cost = a.design_cost;
            decimal? units_completed = a.units_completed;

            try
            {
                if (!string.IsNullOrEmpty(ddlBMP.SelectedValue)) fk_bmp_ag = Convert.ToInt32(ddlBMP.SelectedValue);

                if (!string.IsNullOrEmpty(tbUnitsDesigned.Text))
                {
                    try { units_designed = Convert.ToDecimal(tbUnitsDesigned.Text); }
                    catch
                    { 
                        sbErrorCollection.Append("Units Designed was not updated. Units Designed must be a number (Decimal). ");
                        units_designed = a.units_designed;
                    }
                }

                if (!string.IsNullOrEmpty(tbDesignCost.Text))
                {
                    try { design_cost = Convert.ToDecimal(tbDesignCost.Text); }
                    catch 
                    { 
                        sbErrorCollection.Append("Design Cost was not updated. Design Cost must be a number (Decimal). ");
                        design_cost = a.design_cost;
                    }
                }

                if (!string.IsNullOrEmpty(tbUnitsCompleted.Text))
                {
                    try { units_completed = Convert.ToDecimal(tbUnitsCompleted.Text); }
                    catch
                    {
                        sbErrorCollection.Append("Units Completed was not updated. Design Cost must be a number (Decimal). ");
                        units_completed = a.units_completed;
                    }
                }

                int iCode = wDataContext.form_wfp3_bmp_update(pk_form_wfp3_bmp, fk_bmp_ag, units_designed, units_completed, design_cost, Session["userName"].ToString());
                if (iCode != 0) WACAlert.Show("Error Returned from Database. ", iCode);

                if (!string.IsNullOrEmpty(sbErrorCollection.ToString())) WACAlert.Show(sbErrorCollection.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_BMP_ItemInserting(object sender, EventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_BMP;

        DropDownList ddlBMP = fv.FindControl("ddlBMP") as DropDownList;
        TextBox tbUnitsDesigned = fv.FindControl("tbUnitsDesigned") as TextBox;
        TextBox tbDesignCost = fv.FindControl("tbDesignCost") as TextBox;
        TextBox tbUnitsCompleted = fv.FindControl("tbUnitsCompleted") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iBMP = null;
                if (!string.IsNullOrEmpty(ddlBMP.SelectedValue)) iBMP = Convert.ToInt32(ddlBMP.SelectedValue);
                else sb.Append("BMP is required. ");

                decimal? dUnitsDesigned = 0;
                if (!string.IsNullOrEmpty(tbUnitsDesigned.Text))
                {
                    try { dUnitsDesigned = Convert.ToDecimal(tbUnitsDesigned.Text); }
                    catch { sb.Append("Units Designed must be a number (Decimal). "); }
                }

                decimal? dDesignCost = 0;
                if (!string.IsNullOrEmpty(tbDesignCost.Text))
                {
                    try { dDesignCost = Convert.ToDecimal(tbDesignCost.Text); }
                    catch { sb.Append("Design Cost must be a number (Decimal). "); }
                }

                decimal? dUnitsCompleted = 0;
                if (!string.IsNullOrEmpty(tbUnitsCompleted.Text))
                {
                    try { dUnitsCompleted = Convert.ToDecimal(tbUnitsCompleted.Text); }
                    catch { sb.Append("Units Completed must be a number (Decimal). "); }
                }

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_bmp_add(FK_Wfp3, iBMP, dUnitsDesigned, dUnitsCompleted, dDesignCost, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database. " + sb.ToString(), iCode);
                    else PK_Wfp3Bmp = Convert.ToInt32(i);
                 
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_BMP_ItemDeleting(object sender, EventArgs e)
    { 
        FormView fv = fvAg_WFP3_BMP;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wDataContext.form_wfp3_bmp_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    #endregion

    private void BindAg_WFP3_BMP()
    {
        FormView fv = fvAg_WFP3_BMP;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.vw_form_wfp3_BMPs.Where(w => w.pk_form_wfp3_bmp == PK_Wfp3Bmp).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_form_wfp3_bmp" };
            fv.DataSource = a;
            fv.DataBind();
            
            if (fv.CurrentMode == FormViewMode.Insert)
            {
                // JWJ 9/17/2012 changes parameter 3 from { 0,1 } to { 0 } re: BA934
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_ByFarmBusiness_DDL(fv.FindControl("ddlBMP") as DropDownList, Convert.ToInt32(FK_FarmBusiness), null, new int?[] { 0 }, true);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                // JWJ 9/17/2012 changes parameter 3 from { 0,1 } to { 0 } re: BA934
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_ByFarmBusiness_DDL(fv.FindControl("ddlBMP") as DropDownList, Convert.ToInt32(FK_FarmBusiness), a.Single().pk_bmp_ag, new int?[] { 0 }, true);
                //var b = wDataContext.bmp_ags.Where(w => w.pk_bmp_ag == a.Single().pk_bmp_ag).Select(s => s);
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_Units_By_BMPPractice(a.Single().fk_bmpPractice_code, fv.FindControl("lblAg_WFP3_BMP_Units") as Label);
            }
        }
    }
}
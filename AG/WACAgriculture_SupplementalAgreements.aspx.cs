using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_CustomControls;

public partial class WACAgriculture_SupplementalAgreements : System.Web.UI.Page
{
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["orderAg_SA"] = "";

            PopulateFilters();

            string sQS_PK = Request.QueryString["pk"];
            if (!string.IsNullOrEmpty(sQS_PK))
            {
                try 
                { 
                    ddlFilter_AgreementNumber.SelectedValue = sQS_PK;
                    PopulateFilters();
                }
                catch { }
            }

            hlAg_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Agriculture Express - Supplemental Agreement.pdf";
            hlAg_Help.ImageUrl = "~/images/help_24.png"; 
        }
    }

    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    #endregion

    #region Filters

    private void PopulateFilters()
    {
        PopulateFilter_AgreementNumber();
        PopulateFilter_TaxParcelID();
        PopulateFilter_TaxParcelOwner();

        Populate_GridView();
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearFormView_SA();
        gv.EditIndex = -1;
        PopulateFilters();
    }

    protected void lbResetFilters_Click(object sender, EventArgs e)
    {
        Session["orderAg_SA"] = "";
        ClearFormView_SA();
        ddlFilter_AgreementNumber.Items.Clear();
        ddlFilter_TaxParcelID.Items.Clear();
        ddlFilter_TaxParcelOwner.Items.Clear();
        PopulateFilters();
    }

    private void PopulateFilter_AgreementNumber()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sAgreementNumber = null;
            if (!string.IsNullOrEmpty(ddlFilter_AgreementNumber.SelectedValue)) sAgreementNumber = ddlFilter_AgreementNumber.SelectedValue;

            var a = wDataContext.supplementalAgreements.Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelID.SelectedValue)) a = a.Where(w => w.supplementalAgreementTaxParcels.First(f => f.taxParcel.taxParcelID == ddlFilter_TaxParcelID.SelectedValue).taxParcel.taxParcelID == ddlFilter_TaxParcelID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelOwner.SelectedValue)) a = a.Where(w => w.supplementalAgreementTaxParcels.First(f => f.taxParcel.ownerStr_dnd == ddlFilter_TaxParcelOwner.SelectedValue).taxParcel.ownerStr_dnd == ddlFilter_TaxParcelOwner.SelectedValue);
            
            var b = a.GroupBy(g => g.agreement_nbr_ro).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_AgreementNumber.Items.Clear();
            ddlFilter_AgreementNumber.DataSource = b;
            ddlFilter_AgreementNumber.DataBind();
            ddlFilter_AgreementNumber.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sAgreementNumber))
            {
                try { ddlFilter_AgreementNumber.SelectedValue = sAgreementNumber; }
                catch { }
            }
        }
    }

    private void PopulateFilter_TaxParcelID()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sTaxParcelID = null;
            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelID.SelectedValue)) sTaxParcelID = ddlFilter_TaxParcelID.SelectedValue;

            var a = wDataContext.supplementalAgreements.Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_AgreementNumber.SelectedValue)) a = a.Where(w => w.agreement_nbr_ro == ddlFilter_AgreementNumber.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelOwner.SelectedValue)) a = a.Where(w => w.supplementalAgreementTaxParcels.First(f => f.taxParcel.ownerStr_dnd == ddlFilter_TaxParcelOwner.SelectedValue).taxParcel.ownerStr_dnd == ddlFilter_TaxParcelOwner.SelectedValue);
            
            var b = a.SelectMany(sm => sm.supplementalAgreementTaxParcels).Select(s => s.taxParcel.taxParcelID).OrderBy(o => o).Distinct();

            ddlFilter_TaxParcelID.Items.Clear();
            ddlFilter_TaxParcelID.DataSource = b;
            ddlFilter_TaxParcelID.DataBind();
            ddlFilter_TaxParcelID.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sTaxParcelID))
            {
                try { ddlFilter_TaxParcelID.SelectedValue = sTaxParcelID; }
                catch { }
            }
        }
    }

    private void PopulateFilter_TaxParcelOwner()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sTaxParcelOwner = null;
            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelOwner.SelectedValue)) sTaxParcelOwner = ddlFilter_TaxParcelOwner.SelectedValue;

            var a = wDataContext.supplementalAgreements.Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_AgreementNumber.SelectedValue)) a = a.Where(w => w.agreement_nbr_ro == ddlFilter_AgreementNumber.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelID.SelectedValue)) a = a.Where(w => w.supplementalAgreementTaxParcels.First(f => f.taxParcel.taxParcelID == ddlFilter_TaxParcelID.SelectedValue).taxParcel.taxParcelID == ddlFilter_TaxParcelID.SelectedValue);
            
            var b = a.SelectMany(sm => sm.supplementalAgreementTaxParcels).Where(w => w.taxParcel.ownerStr_dnd != "").Select(s => s.taxParcel.ownerStr_dnd).OrderBy(o => o).Distinct();

            ddlFilter_TaxParcelOwner.Items.Clear();
            ddlFilter_TaxParcelOwner.DataSource = b;
            ddlFilter_TaxParcelOwner.DataBind();
            ddlFilter_TaxParcelOwner.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sTaxParcelOwner))
            {
                try { ddlFilter_TaxParcelOwner.SelectedValue = sTaxParcelOwner; }
                catch { }
            }
        }
    }

    #endregion

    #region GridView Events

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Populate_GridView();
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderAg_SA"] = e.SortExpression;
        Populate_GridView();
    }

    protected void gv_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        fvSA.ChangeMode(FormViewMode.ReadOnly);
        Populate_SA_FormView(Convert.ToInt32(gv.SelectedDataKey.Value));
        if (gv.SelectedIndex != -1) ViewState["SelectedValue"] = gv.SelectedValue.ToString();
    }

    private void Populate_GridView()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.supplementalAgreements.OrderBy(o => o.agreement_nbr_ro).ThenByDescending(o => o.agreement_date).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_AgreementNumber.SelectedValue)) a = a.Where(w => w.agreement_nbr_ro == ddlFilter_AgreementNumber.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelID.SelectedValue)) a = a.Where(w => w.supplementalAgreementTaxParcels.First(f => f.taxParcel.taxParcelID == ddlFilter_TaxParcelID.SelectedValue).taxParcel.taxParcelID == ddlFilter_TaxParcelID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_TaxParcelOwner.SelectedValue)) a = a.Where(w => w.supplementalAgreementTaxParcels.First(f => f.taxParcel.ownerStr_dnd == ddlFilter_TaxParcelOwner.SelectedValue).taxParcel.ownerStr_dnd == ddlFilter_TaxParcelOwner.SelectedValue);
            
            if (!string.IsNullOrEmpty(Session["orderAg_SA"].ToString())) a = a.OrderBy(Session["orderAg_SA"].ToString(), null);
            
            gv.DataKeyNames = new string[] { "pk_supplementalAgreement" };
            gv.DataSource = a;
            gv.DataBind();

            if (ViewState["SelectedValue"] != null)
            {
                string sSelectedValue = (string)ViewState["SelectedValue"];
                foreach (GridViewRow gvr in gv.Rows)
                {
                    string sKeyValue = gv.DataKeys[gvr.RowIndex].Value.ToString();
                    if (sKeyValue == sSelectedValue)
                    {
                        gv.SelectedIndex = gvr.RowIndex;
                        return;
                    }
                    else gv.SelectedIndex = -1;
                }
            }
            lblCount.Text = "Records: " + a.Count();

            if (gv.Rows.Count == 1)
            {
                gv.SelectedIndex = 0;
                gv.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
                fvSA.ChangeMode(FormViewMode.ReadOnly);
                Populate_SA_FormView(Convert.ToInt32(gv.SelectedDataKey.Value));
                if (gv.SelectedIndex != -1) ViewState["SelectedValueAgs"] = gv.SelectedValue.ToString();
            }
        }
    }

    #endregion

    #region Supplemental Agreement

    protected void lbSA_Close_Click(object sender, EventArgs e)
    {
        ClearFormView_SA();
        gv.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        PopulateFilters();
    }

    //protected void lbSA_Insert_Click(object sender, EventArgs e)
    //{
    //    if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "supplementalAgreement", "msgInsert"))
    //    {
    //        fvSA.ChangeMode(FormViewMode.Insert);
    //        Populate_SA_FormView(-1);
    //    }
    //}

    protected void fvSA_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "supplementalAgreement", "msgUpdate");
        if (bChangeMode)
        {
            fvSA.ChangeMode(e.NewMode);
            Populate_SA_FormView(Convert.ToInt32(fvSA.DataKey.Value));
        }
    }

    protected void fvSA_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        //Calendar calAgreementDate = fvSA.FindControl("UC_EditCalendar_SA_AgreementDate").FindControl("cal") as Calendar;
        //Calendar calInactiveDate = fvSA.FindControl("UC_EditCalendar_SA_InactiveDate").FindControl("cal") as Calendar;
        CustomControls_AjaxCalendar tbCalAgreementDate = (CustomControls_AjaxCalendar)fvSA.FindControl("tbCalAgreementDate");
        CustomControls_AjaxCalendar tbCalInactiveDate = (CustomControls_AjaxCalendar)fvSA.FindControl("tbCalInactiveDate");
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.supplementalAgreements.Where(w => w.pk_supplementalAgreement == Convert.ToInt32(fvSA.DataKey.Value)).Select(s => s).Single();
            try
            {
                a.agreement_date = tbCalAgreementDate.CalDateNotNullable;

                //if (calInactiveDate.SelectedDate.Year > 1900) a.inactive_date = calInactiveDate.SelectedDate;
                //else a.inactive_date = null;
                a.inactive_date = tbCalInactiveDate.CalDateNullable;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvSA.ChangeMode(FormViewMode.ReadOnly);
                Populate_SA_FormView(Convert.ToInt32(fvSA.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvSA_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        //int? i = null;
        //int iCode = 0;

        //Calendar calAgreementDate = fvSA.FindControl("UC_EditCalendar_SA_AgreementDate").FindControl("cal") as Calendar;

        //StringBuilder sb = new StringBuilder();

        //using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        //{
        //    try
        //    {
        //        DateTime? dtAgreementDate = calAgreementDate.SelectedDate;

        //        if (string.IsNullOrEmpty(sb.ToString()))
        //        {
        //            iCode = wDataContext.supplementalAgreement_add(dtAgreementDate, null,null, Session["userName"].ToString(), ref i);
        //            if (iCode == 0)
        //            {
        //                fvSA.ChangeMode(FormViewMode.ReadOnly);
        //                Populate_SA_FormView(Convert.ToInt32(i));
        //            }
        //            else WACAlert.Show("Error Returned from Database.", iCode);
        //        }
        //        else WACAlert.Show(sb.ToString(), 0);
        //    }
        //    catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        //}
    }

    protected void fvSA_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "supplementalAgreement", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.supplementalAgreement_delete(Convert.ToInt32(fvSA.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbSA_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    private void Populate_SA_FormView(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.supplementalAgreements.Where(w => w.pk_supplementalAgreement == i).Select(s => s);
            fvSA.DataKeyNames = new string[] { "pk_supplementalAgreement" };
            fvSA.DataSource = a;
            fvSA.DataBind();

            if (fvSA.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                ListView lvSA_Activity = fvSA.FindControl("lvSA_Activity") as ListView;
                lvSA_Activity.DataSource = wDataContext.supplementalAgreement_get_activity(i).OrderBy(o => o.farmID).ThenBy(o => o.Revision);
                lvSA_Activity.DataBind();
            }

            //if (fvSA.CurrentMode == FormViewMode.Insert)
            //{
            //    WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvSA, "UC_EditCalendar_SA_AgreementDate", WACGlobal_Methods.SpecialDataType_DateTime_Today(), null);
            //}

            //if (fvSA.CurrentMode == FormViewMode.Edit)
            //{
            //    WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvSA, "UC_EditCalendar_SA_AgreementDate", a.Single().agreement_date, null);
            //    WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvSA, "UC_EditCalendar_SA_InactiveDate", a.Single().inactive_date, null);
            //}
        }
    }

    private void ClearFormView_SA()
    {
        gv.SelectedRowStyle.BackColor = System.Drawing.Color.Empty;
        fvSA.ChangeMode(FormViewMode.ReadOnly);
        fvSA.DataSource = "";
        fvSA.DataBind();
    }

    #endregion

    #region SA - Notes

    protected void lbSA_Note_Close_Click(object sender, EventArgs e)
    {
        ClearFormView_SA_Note();
        Populate_SA_FormView(Convert.ToInt32(fvSA.DataKey.Value));
    }

    protected void lbSA_Note_Insert_Click(object sender, EventArgs e)
    {
        FormView fv = fvSA.FindControl("fvSA_Note") as FormView;
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "supplementalAgreementNote", "msgInsert"))
        {
            fv.ChangeMode(FormViewMode.Insert);
            Populate_SA_Note_FormView(fv, -1);
        }
    }

    protected void lbSA_Note_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        FormView fv = fvSA.FindControl("fvSA_Note") as FormView;
        fv.ChangeMode(FormViewMode.ReadOnly);
        Populate_SA_Note_FormView(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void fvSA_Note_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        FormView fv = fvSA.FindControl("fvSA_Note") as FormView;
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "supplementalAgreementNote", "msgUpdate");
        if (bChangeMode)
        {
            fv.ChangeMode(e.NewMode);
            Populate_SA_Note_FormView(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvSA_Note_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        FormView fv = fvSA.FindControl("fvSA_Note") as FormView;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.supplementalAgreementNotes.Where(w => w.pk_supplementalAgreementNote == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
            try
            {
                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fv.ChangeMode(FormViewMode.ReadOnly);
                Populate_SA_Note_FormView(fv, Convert.ToInt32(fv.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvSA_Note_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        FormView fv = fvSA.FindControl("fvSA_Note") as FormView;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.supplementalAgreementNote_add(Convert.ToInt32(fvSA.DataKey.Value), sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        Populate_SA_Note_FormView(fv, Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvSA_Note_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        FormView fv = fvSA.FindControl("fvSA_Note") as FormView;
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "supplementalAgreementNote", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.supplementalAgreementNote_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbSA_Note_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    private void Populate_SA_Note_FormView(FormView fv, int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.supplementalAgreementNotes.Where(w => w.pk_supplementalAgreementNote == i).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_supplementalAgreementNote" };
            fv.DataSource = a;
            fv.DataBind();
        }
    }

    private void ClearFormView_SA_Note()
    {
        FormView fv = fvSA.FindControl("fvSA_Note") as FormView;
        fv.DataSource = "";
        fv.DataBind();
    }

    #endregion

    #region SA - Tax Parcels

    protected void lbSA_TaxParcel_Close_Click(object sender, EventArgs e)
    {
        ClearFormView_SA_TaxParcel();
        Populate_SA_FormView(Convert.ToInt32(fvSA.DataKey.Value));
    }

    protected void lbSA_TaxParcel_Insert_Click(object sender, EventArgs e)
    {
        FormView fv = fvSA.FindControl("fvSA_TaxParcel") as FormView;
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "supplementalAgreementTaxParcel", "msgInsert"))
        {
            fv.ChangeMode(FormViewMode.Insert);
            Populate_SA_TaxParcel_FormView(fv, -1);
        }
    }

    protected void lbSA_TaxParcel_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        FormView fv = fvSA.FindControl("fvSA_TaxParcel") as FormView;
        fv.ChangeMode(FormViewMode.ReadOnly);
        Populate_SA_TaxParcel_FormView(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void fvSA_TaxParcel_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        FormView fv = fvSA.FindControl("fvSA_TaxParcel") as FormView;
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "supplementalAgreementTaxParcel", "msgUpdate");
        if (bChangeMode)
        {
            fv.ChangeMode(e.NewMode);
            Populate_SA_TaxParcel_FormView(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvSA_TaxParcel_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        FormView fv = fvSA.FindControl("fvSA_TaxParcel") as FormView;
        DropDownList ddlTaxParcelID = fv.FindControl("ddlTaxParcelID") as DropDownList;
        //Calendar calCancelDate = fv.FindControl("UC_EditCalendar_SA_TaxParcel_CancelDate").FindControl("cal") as Calendar;
        CustomControls_AjaxCalendar tbCalCancelDate = (CustomControls_AjaxCalendar)fv.FindControl("tbCalTaxParcelCancelDate");
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.supplementalAgreementTaxParcels.Where(w => w.pk_supplementalAgreementTaxParcel == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlTaxParcelID.SelectedValue)) a.fk_taxParcel = Convert.ToInt32(ddlTaxParcelID.SelectedValue);
                else sb.Append("Tax Parcel ID was not updated. Tax Parcel ID is required. ");

                //if (calCancelDate.SelectedDate.Year > 1900) a.cancel_date = calCancelDate.SelectedDate;
                //else a.cancel_date = null;
                a.cancel_date = tbCalCancelDate.CalDateNullable;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fv.ChangeMode(FormViewMode.ReadOnly);
                Populate_SA_TaxParcel_FormView(fv, Convert.ToInt32(fv.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvSA_TaxParcel_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        FormView fv = fvSA.FindControl("fvSA_TaxParcel") as FormView;
        DropDownList ddlTaxParcelID = fv.FindControl("ddlTaxParcelID") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPK_TaxParcel = null;
                if (!string.IsNullOrEmpty(ddlTaxParcelID.SelectedValue)) iPK_TaxParcel = Convert.ToInt32(ddlTaxParcelID.SelectedValue);
                else sb.Append("Tax Parcel ID is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.supplementalAgreementTaxParcel_add(Convert.ToInt32(fvSA.DataKey.Value), iPK_TaxParcel, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        Populate_SA_TaxParcel_FormView(fv, Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvSA_TaxParcel_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_workload", "msgDelete"))
        //{
        //    int iCode = 0;
        //    using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        //    {
        //        try
        //        {
        //            iCode = wDataContext.bmp_ag_workload_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
        //            if (iCode == 0) lbFV_Close_Click(null, null);
        //            else WACAlert.Show("Error Returned from Database.", iCode);
        //        }
        //        catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        //    }
        //}
    }

    private void Populate_SA_TaxParcel_FormView(FormView fv, int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.supplementalAgreementTaxParcels.Where(w => w.pk_supplementalAgreementTaxParcel == i).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_supplementalAgreementTaxParcel" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_TaxParcels_TaxParcel_DDL(fv.FindControl("ddlTaxParcelID") as DropDownList, null);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_TaxParcels_TaxParcel_DDL(fv.FindControl("ddlTaxParcelID") as DropDownList, a.Single().taxParcel.pk_taxParcel);
               
            }
        }
    }

    private void ClearFormView_SA_TaxParcel()
    {
        FormView fv = fvSA.FindControl("fvSA_TaxParcel") as FormView;
        fv.DataSource = "";
        fv.DataBind();
    }

    #endregion

    //#region SA - Versions

    //protected void lbSA_Version_Close_Click(object sender, EventArgs e)
    //{
    //    ClearFormView_SA_Version();
    //    Populate_SA_FormView(Convert.ToInt32(fvSA.DataKey.Value));
    //}

    //protected void lbSA_Version_Insert_Click(object sender, EventArgs e)
    //{
    //    FormView fv = fvSA.FindControl("fvSA_Version") as FormView;
    //    if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "supplementalAgreementVersion", "msgInsert"))
    //    {
    //        fv.ChangeMode(FormViewMode.Insert);
    //        Populate_SA_Version_FormView(fv, -1);
    //    }
    //}

    //protected void lbSA_Version_View_Click(object sender, EventArgs e)
    //{
    //    LinkButton lb = (LinkButton)sender;
    //    FormView fv = fvSA.FindControl("fvSA_Version") as FormView;
    //    fv.ChangeMode(FormViewMode.ReadOnly);
    //    Populate_SA_Version_FormView(fv, Convert.ToInt32(lb.CommandArgument));
    //}

    //protected void fvSA_Version_ModeChanging(object sender, FormViewModeEventArgs e)
    //{
    //    FormView fv = fvSA.FindControl("fvSA_Version") as FormView;
    //    bool bChangeMode = true;
    //    if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "supplementalAgreementVersion", "msgUpdate");
    //    if (bChangeMode)
    //    {
    //        fv.ChangeMode(e.NewMode);
    //        Populate_SA_Version_FormView(fv, Convert.ToInt32(fv.DataKey.Value));
    //    }
    //}

    //protected void fvSA_Version_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    //{
    //    StringBuilder sb = new StringBuilder();

    //    FormView fv = fvSA.FindControl("fvSA_Version") as FormView;
    //    //Calendar calDate = fv.FindControl("UC_EditCalendar_Date").FindControl("cal") as Calendar;
    //    CustomControls_AjaxCalendar tbCalVersionDate = (CustomControls_AjaxCalendar)fv.FindControl("tbCalVersionDate");
    //    using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //    {
    //        var a = wDataContext.supplementalAgreementVersions.Where(w => w.pk_supplementalAgreementVersion == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
    //        try
    //        {
    //            a.date = tbCalVersionDate.CalDateNotNullable;

    //            a.modified = DateTime.Now;
    //            a.modified_by = Session["userName"].ToString();

    //            wDataContext.SubmitChanges();
    //            fv.ChangeMode(FormViewMode.ReadOnly);
    //            Populate_SA_Version_FormView(fv, Convert.ToInt32(fv.DataKey.Value));

    //            if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
    //        }
    //        catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
    //    }
    //}

    //protected void fvSA_Version_ItemInserting(object sender, FormViewInsertEventArgs e)
    //{
    //    int? i = null;
    //    int iCode = 0;

    //    FormView fv = fvSA.FindControl("fvSA_Version") as FormView;
    //    //Calendar calDate = fv.FindControl("UC_EditCalendar_Date").FindControl("cal") as Calendar;
    //    CustomControls_AjaxCalendar tbCalVersionDate = (CustomControls_AjaxCalendar)fv.FindControl("tbCalVersionDate");
    //    StringBuilder sb = new StringBuilder();

    //    using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //    {
    //        try
    //        {
    //            DateTime? dtDate = tbCalVersionDate.CalDateNullable;

    //            if (string.IsNullOrEmpty(sb.ToString()))
    //            {
    //                iCode = wDataContext.supplementalAgreementVersion_add(Convert.ToInt32(fvSA.DataKey.Value), dtDate, Session["userName"].ToString(), ref i);
    //                if (iCode == 0)
    //                {
    //                    fv.ChangeMode(FormViewMode.ReadOnly);
    //                    Populate_SA_Version_FormView(fv, Convert.ToInt32(i));
    //                }
    //                else WACAlert.Show("Error Returned from Database.", iCode);
    //            }
    //            else WACAlert.Show(sb.ToString(), 0);
    //        }
    //        catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
    //    }
    //}

    //protected void fvSA_Version_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    //{
    //    FormView fv = fvSA.FindControl("fvSA_Version") as FormView;
    //    if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "supplementalAgreementVersion", "msgDelete"))
    //    {
    //        int iCode = 0;
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            try
    //            {
    //                iCode = wDataContext.supplementalAgreementVersion_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
    //                if (iCode == 0) lbSA_Version_Close_Click(null, null);
    //                else WACAlert.Show("Error Returned from Database.", iCode);
    //            }
    //            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
    //        }
    //    }
    //}

    //private void Populate_SA_Version_FormView(FormView fv, int i)
    //{
    //    using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //    {
    //        var a = wac.supplementalAgreementVersions.Where(w => w.pk_supplementalAgreementVersion == i).Select(s => s);
    //        fv.DataKeyNames = new string[] { "pk_supplementalAgreementVersion" };
    //        fv.DataSource = a;
    //        fv.DataBind();

    //        //if (fv.CurrentMode == FormViewMode.Insert)
    //        //{
    //        //    WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fv, "UC_EditCalendar_Date", WACGlobal_Methods.SpecialDataType_DateTime_Today(), null);
    //        //}

    //        //if (fv.CurrentMode == FormViewMode.Edit)
    //        //{
    //        //    WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fv, "UC_EditCalendar_Date", a.Single().date, null);
    //        //}
    //    }
    //}

    //private void ClearFormView_SA_Version()
    //{
    //    FormView fv = fvSA.FindControl("fvSA_Version") as FormView;
    //    fv.DataSource = "";
    //    fv.DataBind();
    //}

    //#endregion
}
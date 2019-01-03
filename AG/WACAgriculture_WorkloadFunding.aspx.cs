using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACAgriculture_WorkloadFunding : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateFilters();
        }
    }

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    #endregion

    private void PopulateFilters()
    {
        ddlFilter_Workload.Items.Clear();
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var x = wac.agWorkloadFundings.OrderBy(o => o.year).Select(s => s);
            foreach (var y in x)
            {
                string s = y.year + " - " + y.list_agWorkloadFunding.source + " - " + WACGlobal_Methods.Format_Global_Currency(y.amt);
                ddlFilter_Workload.Items.Add(new ListItem(s, y.pk_agWorkloadFunding.ToString()));
            }
            ddlFilter_Workload.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    protected void lbFV_Close_Click(object sender, EventArgs e)
    {
        ClearFormView();
        PopulateFilters();
    }

    protected void lbAg_WorkloadFunding_Insert_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "agWorkloadFunding", "msgInsert"))
        {
            fv.ChangeMode(FormViewMode.Insert);
            BindWorkloadFunding(-1);
        }
    }

    protected void ddlFilter_Workload_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue))
        {
            fv.ChangeMode(FormViewMode.ReadOnly);
            BindWorkloadFunding(Convert.ToInt32(ddlFilter_Workload.SelectedValue));
        }
    }

    protected void fv_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "agWorkloadFunding", "msgUpdate");
        if (bChangeMode)
        {
            fv.ChangeMode(e.NewMode);
            BindWorkloadFunding(Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fv_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlYear = fv.FindControl("ddlYear") as DropDownList;
        DropDownList ddlSource = fv.FindControl("ddlSource") as DropDownList;
        TextBox tbAmt = fv.FindControl("tbAmt") as TextBox;

        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.agWorkloadFundings.Where(w => w.pk_agWorkloadFunding == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();

                if (!string.IsNullOrEmpty(ddlYear.SelectedValue)) x.year = Convert.ToInt16(ddlYear.SelectedValue);
                else sb.Append("Year is required. ");

                if (!string.IsNullOrEmpty(ddlSource.SelectedValue)) x.fk_agWorkloadFunding_code = ddlSource.SelectedValue;
                else sb.Append("Source is required. ");

                if (!string.IsNullOrEmpty(tbAmt.Text))
                {
                    try { x.amt = Convert.ToDecimal(tbAmt.Text); }
                    catch { sb.Append("Amount was not updated. Amount must be a number (Decimal). "); }
                }
                else sb.Append("Amount was not updated. Amount is required. ");

                x.modified = DateTime.Now;
                x.modified_by = Session["userName"].ToString();

                wac.SubmitChanges();
                fv.ChangeMode(FormViewMode.ReadOnly);
                BindWorkloadFunding(Convert.ToInt32(fv.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
        }
        catch (Exception ex) { WACAlert.Show(ex.Message, 0); }

    }

    protected void fv_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlYear = fv.FindControl("ddlYear") as DropDownList;
        DropDownList ddlSource = fv.FindControl("ddlSource") as DropDownList;
        TextBox tbAmt = fv.FindControl("tbAmt") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                short? shYear = null;
                if (!string.IsNullOrEmpty(ddlYear.SelectedValue)) shYear = Convert.ToInt16(ddlYear.SelectedValue);
                else sb.Append("Year is required. ");

                string sSource = null;
                if (!string.IsNullOrEmpty(ddlSource.SelectedValue)) sSource = ddlSource.SelectedValue;
                else sb.Append("Source is required. ");

                decimal? dAmt = null;
                if (!string.IsNullOrEmpty(tbAmt.Text))
                {
                    try { dAmt = Convert.ToDecimal(tbAmt.Text); }
                    catch { sb.Append("Amount must be a number (Decimal). "); }
                }
                else sb.Append("Amount is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wac.agWorkloadFunding_add(shYear, sSource, dAmt, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        BindWorkloadFunding(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else
                {
                    sb.Insert(0, "The Insert Failed: ");
                    WACAlert.Show(sb.ToString(), 0);
                }
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    private void BindWorkloadFunding(int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.agWorkloadFundings.Where(w => w.pk_agWorkloadFunding == i).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_agWorkloadFunding" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fv.FindControl("ddlYear") as DropDownList, DateTime.Now.Year);
                WACGlobal_Methods.PopulateControl_DatabaseLists_AgWorkloadFunding_DDL(fv.FindControl("ddlSource") as DropDownList, null, true);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fv.FindControl("ddlYear") as DropDownList, a.Single().year);
                WACGlobal_Methods.PopulateControl_DatabaseLists_AgWorkloadFunding_DDL(fv.FindControl("ddlSource") as DropDownList, a.Single().fk_agWorkloadFunding_code, true);
            }
        }
    }

    private void ClearFormView()
    {
        fv.ChangeMode(FormViewMode.ReadOnly);
        fv.DataSource = "";
        fv.DataBind();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACLists : System.Web.UI.Page
{
    #region Initialization

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["orderList_Ag_BMPPractice"] = "pk_bmpPractice_code";

            //hlList_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Global Data Communication.pdf";
            hlList_Help.ImageUrl = "~/images/help_24.png";
        }
    }

    protected void ddlLists_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlList_Ag_BMPPractice.Visible = false;
        if (!string.IsNullOrEmpty(ddlLists.SelectedValue))
        {
            switch (ddlLists.SelectedValue)
            {
                case "Ag_BMPPractice": SetUpList_Ag_BMPPractice(); break;
            }
        }
    }

    #endregion

    #region Agriculture - BMP Practice

    private void SetUpList_Ag_BMPPractice()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            gvList_Ag_BMPPractice.DataKeyNames = new string[] { "pk_bmpPractice_code" };
            gvList_Ag_BMPPractice.DataSource = wac.list_bmpPractices.OrderBy(Session["orderList_Ag_BMPPractice"].ToString(), null).Select(s => new { s.pk_bmpPractice_code, s.practice, s.life_reqd_yr, s.fk_agency_code, s.ABC, s.fk_unit_code, s.active });
            gvList_Ag_BMPPractice.DataBind();
        }
        pnlList_Ag_BMPPractice.Visible = true;
    }

    protected void gvList_Ag_BMPPractice_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderList_Ag_BMPPractice"] = e.SortExpression;
        SetUpList_Ag_BMPPractice();
    }

    protected void gvList_Ag_BMPPractice_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvList_Ag_BMPPractice.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");

        fvList_Ag_BMPPractice.ChangeMode(FormViewMode.ReadOnly);
        BindData_Ag_BMPPractice(Convert.ToDecimal(gvList_Ag_BMPPractice.SelectedDataKey.Value));
        mpeList_Ag_BMPPractice.Show();
        upList_Ag_BMPPractice.Update();
    }

    protected void lbList_Ag_BMPPracticePopUp_Close_Click(object sender, EventArgs e)
    {
        fvList_Ag_BMPPractice.ChangeMode(FormViewMode.ReadOnly);
        BindData_Ag_BMPPractice(-1);
        mpeList_Ag_BMPPractice.Hide();
        SetUpList_Ag_BMPPractice();
        upLists.Update();
    }

    protected void lbList_Ag_BMPPractice_Insert_Click(object sender, EventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_ABC);
        if (b)
        {
            fvList_Ag_BMPPractice.ChangeMode(FormViewMode.Insert);
            BindData_Ag_BMPPractice(-1);
            mpeList_Ag_BMPPractice.Show();
            upList_Ag_BMPPractice.Update();
        }
        else WACAlert.Show("You do not have permission to insert BMP Practice data.", 0);

        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        //{
            
        //}
    }

    protected void lbList_Ag_BMPPractice_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvList_Ag_BMPPractice.ChangeMode(FormViewMode.ReadOnly);
        BindData_Ag_BMPPractice(Convert.ToDecimal(lb.CommandArgument));
        mpeList_Ag_BMPPractice.Show();
        upList_Ag_BMPPractice.Update();
    }

    protected void fvList_Ag_BMPPractice_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_ABC);
        if (b)
        {
            fvList_Ag_BMPPractice.ChangeMode(e.NewMode);
            BindData_Ag_BMPPractice(Convert.ToDecimal(fvList_Ag_BMPPractice.DataKey.Value));
        }
        else WACAlert.Show("You do not have permission to modify BMP Practice data.", 0);

        //bool bChangeMode = true;
        //if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        //if (bChangeMode)
        //{
            
        //}
    }

    protected void fvList_Ag_BMPPractice_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbPractice = fvList_Ag_BMPPractice.FindControl("tbPractice") as TextBox;
        DropDownList ddlAgency = fvList_Ag_BMPPractice.FindControl("ddlAgency") as DropDownList;
        DropDownList ddlLifespan = fvList_Ag_BMPPractice.FindControl("ddlLifespan") as DropDownList;
        DropDownList ddlAgronomic = fvList_Ag_BMPPractice.FindControl("ddlAgronomic") as DropDownList;
        DropDownList ddlUnit = fvList_Ag_BMPPractice.FindControl("ddlUnit") as DropDownList;
        DropDownList ddlWACPracticeCategory = fvList_Ag_BMPPractice.FindControl("ddlWACPracticeCategory") as DropDownList;
        TextBox tbABC = fvList_Ag_BMPPractice.FindControl("tbABC") as TextBox;
        TextBox tbABCNote = fvList_Ag_BMPPractice.FindControl("tbABCNote") as TextBox;
        DropDownList ddlActive = fvList_Ag_BMPPractice.FindControl("ddlActive") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.list_bmpPractices.Where(w => w.pk_bmpPractice_code == Convert.ToDecimal(fvList_Ag_BMPPractice.DataKey.Value)).Select(s => s).Single();

                if (!string.IsNullOrEmpty(tbPractice.Text)) a.practice = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPractice.Text, 96);
                else sb.Append("Practice was not updated. Practice is required. ");

                if (!string.IsNullOrEmpty(ddlAgency.SelectedValue)) a.fk_agency_code = ddlAgency.SelectedValue;
                else a.fk_agency_code = null;

                a.life_reqd_yr = Convert.ToByte(ddlLifespan.SelectedValue);

                if (!string.IsNullOrEmpty(ddlAgronomic.SelectedValue)) a.agronomic = ddlAgronomic.SelectedValue;
                else a.agronomic = null;

                if (!string.IsNullOrEmpty(ddlUnit.SelectedValue)) a.fk_unit_code = ddlUnit.SelectedValue;
                else a.fk_unit_code = null;

                if (!string.IsNullOrEmpty(ddlWACPracticeCategory.SelectedValue)) a.fk_list_wacPracticeCategory = Convert.ToInt32(ddlWACPracticeCategory.SelectedValue);
                else a.fk_list_wacPracticeCategory = null;

                if (!string.IsNullOrEmpty(tbABC.Text))
                {
                    try { a.ABC = Convert.ToDecimal(tbABC.Text); }
                    catch { sb.Append("ABC was not updated. Must be a number (Interger). "); }
                }

                if (!string.IsNullOrEmpty(tbABCNote.Text)) a.ABC_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbABCNote.Text, 255);
                else a.ABC_note = null;

                a.active = ddlActive.SelectedValue;

                wDataContext.SubmitChanges();
                fvList_Ag_BMPPractice.ChangeMode(FormViewMode.ReadOnly);
                BindData_Ag_BMPPractice(Convert.ToDecimal(fvList_Ag_BMPPractice.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvList_Ag_BMPPractice_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int iCode = 0;

        TextBox tbPK = fvList_Ag_BMPPractice.FindControl("tbPK") as TextBox;
        TextBox tbPractice = fvList_Ag_BMPPractice.FindControl("tbPractice") as TextBox;
        DropDownList ddlAgency = fvList_Ag_BMPPractice.FindControl("ddlAgency") as DropDownList;
        DropDownList ddlLifespan = fvList_Ag_BMPPractice.FindControl("ddlLifespan") as DropDownList;
        DropDownList ddlAgronomic = fvList_Ag_BMPPractice.FindControl("ddlAgronomic") as DropDownList;
        DropDownList ddlUnit = fvList_Ag_BMPPractice.FindControl("ddlUnit") as DropDownList;
        DropDownList ddlWACPracticeCategory = fvList_Ag_BMPPractice.FindControl("ddlWACPracticeCategory") as DropDownList;
        TextBox tbABC = fvList_Ag_BMPPractice.FindControl("tbABC") as TextBox;
        TextBox tbABCNote = fvList_Ag_BMPPractice.FindControl("tbABCNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                decimal? dPracticeCode = null;
                if (!string.IsNullOrEmpty(tbPractice.Text))
                {
                    try { dPracticeCode = Convert.ToDecimal(tbPK.Text); }
                    catch { sb.Append("Practice Code must be a number (Decimal). "); }
                }
                else sb.Append("Practice Code is required. ");

                string sAgency = null;
                if (!string.IsNullOrEmpty(ddlAgency.SelectedValue)) sAgency = ddlAgency.SelectedValue;
                else sb.Append("Agency is required. ");

                string sPractice = null;
                if (!string.IsNullOrEmpty(tbPractice.Text)) sPractice = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPractice.Text, 96);
                else sb.Append("Practice is required. ");

                byte? byLifeRequiredYears = Convert.ToByte(ddlLifespan.SelectedValue);

                string sAgronomic = null;
                if (!string.IsNullOrEmpty(ddlAgronomic.SelectedValue)) sAgronomic = ddlAgronomic.SelectedValue;

                string sUnit = null;
                if (!string.IsNullOrEmpty(ddlUnit.SelectedValue)) sUnit = ddlUnit.SelectedValue;
                else sb.Append("Units is required. ");

                int? iWACPracticeCategory = null;
                if (!string.IsNullOrEmpty(ddlWACPracticeCategory.SelectedValue)) iWACPracticeCategory = Convert.ToInt32(ddlWACPracticeCategory.SelectedValue);

                decimal? dABC = null;
                if (!string.IsNullOrEmpty(tbABC.Text))
                {
                    try { dABC = Convert.ToDecimal(tbABC.Text); }
                    catch { sb.Append("ABC must be a number (Interger). "); }
                }

                string sABCNote = null;
                if (!string.IsNullOrEmpty(tbABCNote.Text)) sABCNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbABCNote.Text, 255);
                
                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.list_bmpPractice_add(dPracticeCode, sAgency, sPractice, byLifeRequiredYears, sAgronomic, sUnit, iWACPracticeCategory, dABC, sABCNote);
                    if (iCode == 0)
                    {
                        fvList_Ag_BMPPractice.ChangeMode(FormViewMode.ReadOnly);
                        BindData_Ag_BMPPractice(Convert.ToDecimal(dPracticeCode));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvList_Ag_BMPPractice_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_ABC);
        if (b)
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.list_bmpPractice_delete(Convert.ToDecimal(fvList_Ag_BMPPractice.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbList_Ag_BMPPracticePopUp_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
        }
        else WACAlert.Show("You do not have permission to delete BMP Practice data.", 0);

        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        //{
            
        //}
    }

    private void BindData_Ag_BMPPractice(decimal d)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.list_bmpPractices.Where(w => w.pk_bmpPractice_code == d).Select(s => s);

            fvList_Ag_BMPPractice.DataKeyNames = new string[] { "pk_bmpPractice_code" };
            fvList_Ag_BMPPractice.DataSource = a;
            fvList_Ag_BMPPractice.DataBind();

            if (fvList_Ag_BMPPractice.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Agency_DDL(fvList_Ag_BMPPractice, "ddlAgency", null);
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_Lifespan_DDL(fvList_Ag_BMPPractice.FindControl("ddlLifespan") as DropDownList, null, false);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvList_Ag_BMPPractice, "ddlAgronomic", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Unit_DDL(fvList_Ag_BMPPractice, "ddlUnit", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_WACPracticeCategory_DDL(fvList_Ag_BMPPractice.FindControl("ddlWACPracticeCategory") as DropDownList, null);
            }
            if (fvList_Ag_BMPPractice.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Agency_DDL(fvList_Ag_BMPPractice, "ddlAgency", a.Single().fk_agency_code);
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_Lifespan_DDL(fvList_Ag_BMPPractice.FindControl("ddlLifespan") as DropDownList, a.Single().life_reqd_yr, false);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvList_Ag_BMPPractice, "ddlAgronomic", a.Single().agronomic);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Unit_DDL(fvList_Ag_BMPPractice, "ddlUnit", a.Single().fk_unit_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_WACPracticeCategory_DDL(fvList_Ag_BMPPractice.FindControl("ddlWACPracticeCategory") as DropDownList, a.Single().fk_list_wacPracticeCategory);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvList_Ag_BMPPractice, "ddlActive", a.Single().active, false);
            }
        }
    }

    #endregion
}
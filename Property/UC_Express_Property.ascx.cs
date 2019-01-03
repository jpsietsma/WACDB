using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Express_Property : System.Web.UI.UserControl
{
    public bool _BoolShowCountyTownship = true;
    public bool _BoolShowBasinSubbasin = true;
    public bool _BoolShowTownship = true;
    public bool _BoolShowSubbasin = true;
    public bool _CountyControlsBasin = true;
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadFormView_Property(int iPK_Property)
    {
        try
        {
            fvProperty.ChangeMode(FormViewMode.ReadOnly);
            if (iPK_Property == -1) fvProperty.ChangeMode(FormViewMode.Insert);
            BindProperty(iPK_Property);
            mpeExpress_Property.Show();
            upExpress_Property.Update();
        }
        catch { WACAlert.Show("The property is disassociated and the express property window is not available.", 0); }
    }

    #endregion

    #region Event Handling

    protected void lbProperty_Close_Click(object sender, EventArgs e)
    {
        hfPropertyPK.Value = "";
        Page.GetType().InvokeMember("InvokedMethod_SectionPage_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
        mpeExpress_Property.Hide();
    }

    protected void lbProperty_Add_Click(object sender, EventArgs e)
    {
        fvProperty.ChangeMode(FormViewMode.Insert);
        BindProperty(-1);
    }
  
    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCounty = (DropDownList)sender;
        DropDownList ddlTownshipNY = (DropDownList)fvProperty.FindControl("ddlTownshipNY");
        DropDownList ddlBasin = (DropDownList)fvProperty.FindControl("ddlbasin");
        DropDownList ddlSubbasin = (DropDownList)fvProperty.FindControl("ddlSubbasin");

        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
        {
            WACGlobal_Methods.PopulateControl_DatabaseLists_TownshipNY_DDL(ddlTownshipNY, Convert.ToInt32(ddlCounty.SelectedValue), null);
            if (_CountyControlsBasin)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(ddlBasin, null, null, null, Convert.ToInt32(ddlCounty.SelectedValue), null, true);
                if (_BoolShowSubbasin) ddlSubbasin.Items.Clear();
            }
        }
        else
        {
            if (_BoolShowTownship) ddlTownshipNY.Items.Clear();
            if (_CountyControlsBasin)
            {
                ddlBasin.Items.Clear();
                if (_BoolShowSubbasin) ddlSubbasin.Items.Clear();
            }
        }
    }

    protected void ddlBasin_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlBasin = (DropDownList)sender;
        DropDownList ddlSubbasin = (DropDownList)fvProperty.FindControl("ddlSubbasin");
        if (_BoolShowSubbasin)
        {
            if (!string.IsNullOrEmpty(ddlBasin.SelectedValue))
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Subbasin_DDL(ddlSubbasin, ddlBasin.SelectedValue, null);
            }
            else ddlSubbasin.Items.Clear();
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(fvProperty, "ddlZip", null, ddl.SelectedValue);

        if (fvProperty.CurrentMode == FormViewMode.Edit)
        {
            Label lblCity = fvProperty.FindControl("lblCity") as Label;
            DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
            DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
            DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
            DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;
            if (ddl.SelectedValue == "NY")
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCountyNY, false, null);
                ddlTownshipNY.Items.Clear();
                ddlBasin.Items.Clear();
                ddlSubbasin.Items.Clear();
            }
            else
            {
                ddlCountyNY.Items.Clear();
                ddlTownshipNY.Items.Clear();
                ddlBasin.Items.Clear();
                ddlSubbasin.Items.Clear();
            }

            if (lblCity != null) lblCity.Text = "";
        }
    }

    protected void ddlZip_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        DropDownList ddlState = fvProperty.FindControl("ddlState") as DropDownList;
        DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;
        TextBox tbCity = fvProperty.FindControl("tbCity") as TextBox;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.zipcodes.Where(w => w.pk_zipcode == ddl.SelectedValue) select b).Single();
            if (ddlState.SelectedValue.ToUpper() == "NY")
            {
                try
                {
                    var c = wDataContext.list_countyNies.Where(w => w.county == a.county).Select(s => s.pk_list_countyNY);
                    if (c.Count() == 1)
                    {
                        WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCountyNY, false, c.Single());
                        WACGlobal_Methods.PopulateControl_DatabaseLists_TownshipNY_DDL(ddlTownshipNY, c.Single(), null);
                        WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(ddlBasin, null, null, null, c.Single(), null, true);
                        ddlSubbasin.Items.Clear();
                    }
                    else
                    {
                        WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCountyNY, false, null);
                        ddlTownshipNY.Items.Clear();
                        ddlBasin.Items.Clear();
                        ddlSubbasin.Items.Clear();
                    }
                }
                catch
                {
                    ddlCountyNY.SelectedIndex = 0;
                    ddlTownshipNY.Items.Clear();
                    ddlBasin.Items.Clear();
                    ddlSubbasin.Items.Clear();
                    tbCity.Text = "";
                }
            }
            else
            {
                ddlCountyNY.Items.Clear();
                ddlTownshipNY.Items.Clear();
                ddlBasin.Items.Clear();
                ddlSubbasin.Items.Clear();
            }
            tbCity.Text = a.city;
        }
    }
    protected void fvProperty_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        try
        {
            fvProperty.ChangeMode(e.NewMode);
            if (!string.IsNullOrEmpty(hfPropertyPK.Value)) BindProperty(Convert.ToInt32(hfPropertyPK.Value));
            else lbProperty_Close_Click(null, null);
        }
        catch { }
    }

    protected void fvProperty_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbAddress1 = fvProperty.FindControl("tbAddress1") as TextBox;
        TextBox tbAddress2 = fvProperty.FindControl("tbAddress2") as TextBox;
        DropDownList ddlState = fvProperty.FindControl("ddlState") as DropDownList;
        DropDownList ddlZip = fvProperty.FindControl("ddlZip") as DropDownList;
        TextBox tbCity = fvProperty.FindControl("tbCity") as TextBox;
        TextBox tbZip4 = fvProperty.FindControl("tbZip4") as TextBox;
        DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.properties.Where(w => w.pk_property == Convert.ToInt32(fvProperty.DataKey.Value)).Select(s => s).Single();

                a.address = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress1.Text, 100).Trim();
                a.address2 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress2.Text, 48).Trim();
                a.city = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCity.Text, 48).Trim();
                if (!string.IsNullOrEmpty(ddlState.SelectedValue)) a.state = ddlState.SelectedValue;
                else a.state = null;
                if (!string.IsNullOrEmpty(ddlZip.SelectedValue)) a.fk_zipcode = ddlZip.SelectedValue;
                else a.fk_zipcode = null;
                a.zip4 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbZip4.Text, 4).Trim();
                if (!string.IsNullOrEmpty(ddlCountyNY.SelectedValue)) a.fk_list_countyNY = Convert.ToInt32(ddlCountyNY.SelectedValue);
                else a.fk_list_countyNY = null;
                if (!string.IsNullOrEmpty(ddlTownshipNY.SelectedValue)) a.fk_list_townshipNY = Convert.ToInt32(ddlTownshipNY.SelectedValue);
                else a.fk_list_townshipNY = null;
                if (!string.IsNullOrEmpty(ddlBasin.SelectedValue)) a.fk_basin_code = ddlBasin.SelectedValue;
                else a.fk_basin_code = null;
                if (!string.IsNullOrEmpty(ddlSubbasin.SelectedValue)) a.fk_subbasin_code = ddlSubbasin.SelectedValue;
                else a.fk_subbasin_code = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();
                wDataContext.SubmitChanges();
                fvProperty.ChangeMode(FormViewMode.ReadOnly);
                BindProperty(Convert.ToInt32(fvProperty.DataKey.Value));
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }   
    }

    protected void fvProperty_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        TextBox tbAddress1 = fvProperty.FindControl("tbAddress1") as TextBox;
        TextBox tbAddress2 = fvProperty.FindControl("tbAddress2") as TextBox;
        DropDownList ddlState = fvProperty.FindControl("ddlState") as DropDownList;
        DropDownList ddlZip = fvProperty.FindControl("ddlZip") as DropDownList;
        TextBox tbCity = fvProperty.FindControl("tbCity") as TextBox;
        TextBox tbZip4 = fvProperty.FindControl("tbZip4") as TextBox;
        DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;


        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {

                string addr1 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress1.Text, 100).Trim();
                string addr2 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress2.Text, 48).Trim();
                string city = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCity.Text, 48).Trim();
                string state = string.IsNullOrEmpty(ddlState.SelectedValue) ? null : ddlState.SelectedValue;
                string zipCode = string.IsNullOrEmpty(ddlZip.SelectedValue) ? null : ddlZip.SelectedValue;
                string zip4 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbZip4.Text, 4).Trim();
                int? county = null;
                if (!string.IsNullOrEmpty(ddlCountyNY.SelectedValue))
                    county = Convert.ToInt32(ddlCountyNY.SelectedValue);
                int? township = null;
                if (!string.IsNullOrEmpty(ddlTownshipNY.SelectedValue))
                    township = Convert.ToInt32(ddlTownshipNY.SelectedValue);
                string basinCode = string.IsNullOrEmpty(ddlBasin.SelectedValue) ? null : ddlBasin.SelectedValue;
                string subBasinCode = string.IsNullOrEmpty(ddlSubbasin.SelectedValue) ? null : ddlSubbasin.SelectedValue;
                iCode = wDataContext.property_add_express(addr1, addr2, city, state, zipCode, zip4, county, township, basinCode, subBasinCode, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fvProperty.ChangeMode(FormViewMode.ReadOnly);
                    BindProperty(Convert.ToInt32(i));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);

            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    #endregion

    #region Data Binding

    public void BindProperty(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.properties.Where(w => w.pk_property == i).Select(s => s);
            fvProperty.DataKeyNames = new string[] { "pk_property" };
            fvProperty.DataSource = a;
            fvProperty.DataBind();

            if (fvProperty.CurrentMode != FormViewMode.Insert)
            {
                hfPropertyPK.Value = a.Single().pk_property.ToString();
            }

            if (fvProperty.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_StatesUS_DDL(fvProperty, "ddlState", "NY");
                WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(fvProperty, "ddlZip", null, "NY");
            }

            if (fvProperty.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_StatesUS_DDL(fvProperty, "ddlState", a.Single().state);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(fvProperty, "ddlZip", a.Single().fk_zipcode, a.Single().state);
                WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(fvProperty.FindControl("ddlCounty") as DropDownList, false, a.Single().fk_list_countyNY);
                if (a.Single().fk_list_countyNY != null)
                {
                    WACGlobal_Methods.PopulateControl_DatabaseLists_TownshipNY_DDL(fvProperty.FindControl("ddlTownshipNY") as DropDownList, a.Single().fk_list_countyNY, a.Single().fk_list_townshipNY);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(fvProperty.FindControl("ddlBasin") as DropDownList, null, null, null, a.Single().fk_list_countyNY, a.Single().fk_basin_code, true);
                    if (!string.IsNullOrEmpty(a.Single().fk_basin_code))
                        WACGlobal_Methods.PopulateControl_DatabaseLists_Subbasin_DDL(fvProperty.FindControl("ddlSubbasin") as DropDownList, a.Single().fk_basin_code, a.Single().fk_subbasin_code);
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
        BindProperty(Convert.ToInt32(hfPropertyPK.Value));
    }

    protected void btnExpress_GlobalInsert_Click(object sender, EventArgs e)
    {
        pnlGlobalInsert.Visible = true;
    }

    #endregion
}
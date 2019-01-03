using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Extensions;

public partial class UC_ControlGroup_TaxParcel : System.Web.UI.UserControl
{
    public string StrEntityType = null;
    public bool TaxParcelCountyBound
    {
        get { return Convert.ToBoolean(ViewState["TaxParcelCountyBound"]); }
        set { ViewState["TaxParcelCountyBound"] = value; }
    }
    public bool AddButtonVisible
    {
        get { return Convert.ToBoolean(Session["AddTPButtonVisible"]); }
        set { ViewState["AddTPButtonVisible"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BindCountyDDL(null);
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
        {
            BindJurisdictionDDL(ddlCounty.SelectedValue);
        }
        else ddlJurisdiction.Items.Clear();
        ddlTaxParcelID.Items.Clear();
    }

    protected void ddlJurisdiction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlJurisdiction.SelectedValue))
        {
           BindTaxParcelDDL(ddlCounty.SelectedValue, ddlJurisdiction.SelectedItem.Text);
        }
        else ddlTaxParcelID.Items.Clear();
    }

    protected void ddlTaxParcelID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        LinkButton lb = ddl.Parent.FindControl("lbAddTaxParcel") as LinkButton;
    }
    protected void lbAddTaxParcel_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        DropDownList ddl = lb.Parent.FindControl("ddlTaxParcelID") as DropDownList;
        object target = WACGlobal_Methods.ContainingObject(ddl.Page, "InvokedMethod_ControlGroup_TaxParcel");
        if (!string.IsNullOrEmpty(ddlTaxParcelID.SelectedValue))
        {
            try
            {
                object[] oArgs = new object[] { ddlTaxParcelID.SelectedValue };
                target.GetType().InvokeMember("InvokedMethod_ControlGroup_TaxParcel", System.Reflection.BindingFlags.InvokeMethod, null, target, oArgs);
            }
            catch { }
        }
        ResetControls();

    }
    public void ResetControls()
    {
        ddlTaxParcelID.Items.Clear();
        ddlJurisdiction.Items.Clear();
        ddlCounty.SelectedIndex = 0;
    }

    #region Custom - Tax Parcels

    private void BindCountyDDL(string selected)
    {
        DropDownList ddl = ddlCounty;
        if (ddl != null)
        {
            if (!TaxParcelCountyBound)
            {
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    var c = wac.taxParcels.Where(w => w.list_swi.county != "" && w.list_swi.county != "unknown" && w.list_swi.county != "n/a").
                        Select(s => new { s.list_swi.county }).Distinct().OrderBy("county", false);
                    ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                    foreach (var county in c)
                    {
                        ddl.Items.Add(new ListItem(county.county, county.county));
                    }
                    TaxParcelCountyBound = true;
                } 
            }             
            try { if (!string.IsNullOrEmpty(selected)) ddl.SelectedValue = selected; }
            catch { }
        }
    }

    private void BindJurisdictionDDL(string sCounty)
    {
        DropDownList ddl = ddlJurisdiction;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.taxParcels.Where(w => w.list_swi.county == sCounty).Select(s => new { s.list_swi.pk_list_swis, s.list_swi.jurisdiction }).Distinct().OrderBy(o => o.jurisdiction);
                ddl.DataTextField = "jurisdiction";
                ddl.DataValueField = "pk_list_swis";
                ddl.DataSource = x;
                ddl.DataBind();
            }
        }
    }

    private void BindTaxParcelDDL(string sCounty, string sJurisdiction)
    {
        DropDownList ddl = ddlTaxParcelID;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.taxParcels.OrderBy(o => o.taxParcelID).Select(s => new { s.pk_taxParcel, s.taxParcelID, s.list_swi });
                if (!string.IsNullOrEmpty(sCounty)) a = a.Where(w => w.list_swi.county == sCounty);
                if (!string.IsNullOrEmpty(sJurisdiction)) a = a.Where(w => w.list_swi.jurisdiction == sJurisdiction);
                ddl.DataTextField = "taxParcelID";
                ddl.DataValueField = "pk_taxParcel";
                ddl.DataSource = a;
                ddl.DataBind();
            }
        }
    }

    #endregion
}
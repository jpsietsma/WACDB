using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Property_EditInsert : System.Web.UI.UserControl
{
    public string StrPropertySection = "GLOBAL"; // AGRICULTURE, GLOBAL
    public bool ShowClearLinkButton = true;
    public bool ShowExpressImageButton = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ShowClearLinkButton) lbParticipant_PropertyAddress_Clear.Visible = false;
        if (!ShowExpressImageButton)
        {
            UC_Express_PageButtons1.Visible = false;
            lblExpress_Property.Visible = false;
        }
        if (StrPropertySection == "AGRICULTURE")
        {
            UC_Express_PageButtons1.StrSection = "AGRICULTURE";
        }
    }

    public string GetPropertyFieldsBySection(string sField)
    {
        string s = sField;
        if (StrPropertySection.ToUpper() == "AGRICULTURE") s = "farmland." + s;
        return s;
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlState.SelectedValue)) WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL("PROPERTY", "CITY", ddlCity, ddlState.SelectedValue, null, null, null, false);
        else ddlCity.Items.Clear();

        ddlAddressType.Items.Clear();
        ddlAddress.Items.Clear();
        ddlAddressNumber.Items.Clear();
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlCity.SelectedValue)) 
            WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL("PROPERTY", "ADDRESSTYPE", ddlAddressType, ddlState.SelectedValue, ddlCity.SelectedValue, null, null, false);
        else ddlAddressType.Items.Clear();

        ddlAddress.Items.Clear();
        ddlAddressNumber.Items.Clear();
    }

    protected void ddlAddressType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlAddressType.SelectedValue))
        {
            if (ddlAddressType.SelectedValue != "POB")
            {
                lblBase.Text = ddlAddressType.SelectedItem.Text + ":";
                ddlAddress.Visible = true;
                WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL("PROPERTY", ddlAddressType.SelectedValue, ddlAddress, ddlState.SelectedValue, ddlCity.SelectedValue, null, null, false);
                ddlAddressNumber.Items.Clear();
            }
            else
            {
                lblBase.Text = "";
                ddlAddress.Items.Clear();
                ddlAddress.Visible = false;
                WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL("PROPERTY", "POB" , ddlAddressNumber, ddlState.SelectedValue, ddlCity.SelectedValue, null, null, true);
            }
        }
    }

    protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL("PROPERTY", "ADDRESSNUMBER", ddlAddressNumber, ddlState.SelectedValue, ddlCity.SelectedValue, ddlAddress.SelectedValue, null, true);
    }

    protected void ddlAddressNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        ImageButton ibExpressView = UC_Express_PageButtons1.FindControl("ibExpressView") as ImageButton;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl_Controls(ibExpressView, lblPropertyAddress, hfPropertyPK, Convert.ToInt32(ddl.SelectedValue));
    }

    protected void lbParticipant_PropertyAddress_Clear_Click(object sender, EventArgs e)
    {
        ImageButton ibExpressView = UC_Express_PageButtons1.FindControl("ibExpressView") as ImageButton;
        ibExpressView.Visible = false;
        lblPropertyAddress.Text = "";
        hfPropertyPK.Value = "";
        try { ddlCity.SelectedIndex = 0; }
        catch { }
        try { ddlAddressType.Items.Clear(); }
        catch { }
        try { ddlAddress.Items.Clear(); }
        catch { }
        try { ddlAddressNumber.Items.Clear(); }
        catch { }
    }
}
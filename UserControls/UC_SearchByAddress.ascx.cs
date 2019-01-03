using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_SearchByAddress : System.Web.UI.UserControl
{
    // StrPropertyType { ORGANIZATION, PARTICIPANT, PROPERTY, VENUE }
    public string StrPropertyType = "PROPERTY";

    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateDDL4Search_State();
        }
    }

    private void PopulateDDL4Search_State()
    {
        WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL(StrPropertyType, "STATE", ddl_Search_State, null, null, null, "NY", false);
        PopulateDDL4Search_City("NY");
    }

    private void PopulateDDL4Search_City(string sState)
    {
        WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL(StrPropertyType, "CITY", ddl_Search_City, sState, null, null, null, false);
    }

    private void PopulateDDL4Search_AddressType()
    {
        //WACGlobal_Methods.PopulateControl_DatabaseLists_AddressType_DDL(ddl_Search_AddressType, null, true);
        WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL(StrPropertyType, "ADDRESSTYPE", ddl_Search_AddressType, ddl_Search_State.SelectedValue, ddl_Search_City.SelectedValue, null, null, false);
    }

    private void PopulateDDL4Search_Address(string sState, string sCity, string sAddressType)
    {
        if (sAddressType != "POB") WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL(StrPropertyType, sAddressType, ddl_Search_Address, sState, sCity, null, "", false);
        else WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL(StrPropertyType, "POB", ddl_Search_AddressNumber, sState, sCity, null, "", false);
    }

    private void PopulateDDL4Search_AddressNumber(string sState, string sCity, string sAddress)
    {
        WACGlobal_Methods.PopulateControl_Property_AddressItems_DDL(StrPropertyType, "ADDRESSNUMBER", ddl_Search_AddressNumber, sState, sCity, sAddress, "", false);
    }

    #endregion

    #region Event Handling

    protected void ddl_Search_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        object target = WACGlobal_Methods.ContainingObject(ddl.Page, "ChangeIndex2Zero4SearchDDLs");
        try
        {
            object[] oArgs = new object[] { ddl.SelectedValue };
            //target.GetType().InvokeMember("ChangeIndex2Zero4SearchDDLs", System.Reflection.BindingFlags.InvokeMethod, null, target, oArgs);
            //Page.GetType().InvokeMember("ChangeIndex2Zero4SearchDDLs", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
            ddl_Search_City.Items.Clear();
            ddl_Search_AddressType.Items.Clear();
            pnl_Search_Base.Visible = false;
            ddl_Search_Address.Items.Clear();
            ddl_Search_AddressNumber.Items.Clear();
            if (!string.IsNullOrEmpty(ddl_Search_State.SelectedValue)) PopulateDDL4Search_City(ddl_Search_State.SelectedValue);
        }
        catch (Exception Exception)
        {
            //WACAlert.Show(Exception.Message, 0);
        }
        
        
    }

    protected void btn_Search_State_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl_Search_State.SelectedValue))
        {
            object[] o = new object[] { ddl_Search_State.SelectedValue };
            HandleMethodInvoke(o, "DefineSpecificValuesForState");
        }
        else WACAlert.Show("You must select a state.", 0);
    }

    protected void ddl_Search_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Search_AddressType.Items.Clear();
        pnl_Search_Base.Visible = false;
        ddl_Search_Address.Items.Clear();
        ddl_Search_AddressNumber.Items.Clear();
        if (!string.IsNullOrEmpty(ddl_Search_City.SelectedValue)) PopulateDDL4Search_AddressType();
    }

    protected void btn_Search_City_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl_Search_City.SelectedValue))
        {
            List<string> l = new List<string>();
            l.Add(ddl_Search_State.SelectedValue);
            l.Add(ddl_Search_City.SelectedValue);
            object[] o = new object[] { l };
            HandleMethodInvoke(o, "DefineSpecificValuesForCity");
        }
        else WACAlert.Show("You must select a city.", 0);
    }

    protected void ddl_Search_AddressType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl_Search_AddressType.SelectedValue))
        {
            if (ddl_Search_AddressType.SelectedValue != "POB")
            {
                ddl_Search_AddressNumber.Items.Clear();
                PopulateDDL4Search_Address(ddl_Search_State.SelectedValue, ddl_Search_City.SelectedValue, ddl_Search_AddressType.SelectedValue);
                lblBase.Text = ddl_Search_AddressType.SelectedItem.Text;
                btn_Search_Address.Text = "Search " + ddl_Search_AddressType.SelectedItem.Text;
                pnl_Search_Base.Visible = true;
            }
            else
            {
                PopulateDDL4Search_Address(ddl_Search_State.SelectedValue, ddl_Search_City.SelectedValue, ddl_Search_AddressType.SelectedValue);
                pnl_Search_Base.Visible = false;
            }
        }
    }

    protected void btn_Search_AddressType_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl_Search_AddressType.SelectedValue))
        {
            List<string> l = new List<string>();
            l.Add(ddl_Search_State.SelectedValue);
            l.Add(ddl_Search_City.SelectedValue);
            l.Add(ddl_Search_AddressType.SelectedValue);
            object[] o = new object[] { l };
            HandleMethodInvoke(o, "DefineSpecificValuesForAddressType");
        }
        else WACAlert.Show("You must select an address type.", 0);
    }

    protected void ddl_Search_Address_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Search_AddressNumber.Items.Clear();
        if (!string.IsNullOrEmpty(ddl_Search_Address.SelectedValue)) PopulateDDL4Search_AddressNumber(ddl_Search_State.SelectedValue, ddl_Search_City.SelectedValue, ddl_Search_Address.SelectedValue);
    }

    protected void btn_Search_Address_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl_Search_Address.SelectedValue))
        {
            List<string> l = new List<string>();
            l.Add(ddl_Search_State.SelectedValue);
            l.Add(ddl_Search_City.SelectedValue);
            l.Add(ddl_Search_Address.SelectedValue);
            object[] o = new object[] { l };
            HandleMethodInvoke(o, "DefineSpecificValuesForAddress");
        }
        else WACAlert.Show("You must select an address.", 0);
    }

    protected void ddl_Search_AddressNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl_Search_AddressNumber.SelectedValue))
        {
            if (ddl_Search_AddressType.SelectedValue != "POB")
            {
                List<string> l = new List<string>();
                l.Add(ddl_Search_State.SelectedValue);
                l.Add(ddl_Search_City.SelectedValue);
                l.Add(ddl_Search_Address.SelectedValue);
                l.Add(ddl_Search_AddressNumber.SelectedValue);
                object[] o = new object[] { l };
                HandleMethodInvoke(o, "DefineSpecificValuesForAddressNumber");
            }
            else
            {
                List<string> l = new List<string>();
                l.Add(ddl_Search_State.SelectedValue);
                l.Add(ddl_Search_City.SelectedValue);
                l.Add(ddl_Search_AddressType.SelectedValue);
                l.Add(ddl_Search_AddressNumber.SelectedValue);
                object[] o = new object[] { l };
                HandleMethodInvoke(o, "DefineSpecificValuesForPOB");
            }
        }
    }

    private void HandleMethodInvoke(object[] o, string sSpecificValueMethodName)
    {
        Page.GetType().InvokeMember(sSpecificValueMethodName, System.Reflection.BindingFlags.InvokeMethod, null, this.Page, o);
        Page.GetType().InvokeMember("HandleQueryType", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
        Page.GetType().InvokeMember("UpdateUpdatePanel", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
    }

    #endregion
}
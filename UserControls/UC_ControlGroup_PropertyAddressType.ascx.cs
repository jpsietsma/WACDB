using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_ControlGroup_PropertyAddressType : System.Web.UI.UserControl
{
    public string Address { get; set; }
    public string AddressType { get; set; }
    public string AddressTypeCode { get; set; }
    public string Number { get; set; }
    public string AddressBase { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void DecodeAddressParts()
    {
        if (!(string.IsNullOrEmpty(AddressType) && !string.IsNullOrEmpty(Number)))
        {
            if (AddressTypeCode.Equals("POB"))
            {
                Address = AddressType + " " + Number;
            }
            else if (!string.IsNullOrEmpty(AddressBase))
            {
                
            }
        }
        
    }
    protected void ddlAddressType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        AddressType = ddl.SelectedItem.Text;
        AddressTypeCode = ddl.SelectedItem.Value;
    }
    protected void tbNumber_TextChanged(object sender, EventArgs e)
    {
        Number = ((TextBox)sender).Text;
    }
    protected void tbAddressBase_TextChanged(object sender, EventArgs e)
    {
        AddressBase = ((TextBox)sender).Text;
    }
}
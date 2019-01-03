using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_ViewModels;
using WAC_Event;
using WAC_Connectors;
using WAC_Services;
using WAC_UserControls;
using WAC_Containers;
using System.Collections;

public partial class Property_WACPR_TaxParcelPrintKeySearch : WACUtilityControl, IWACIndependentControl
{
    public event EventHandler<UserControlResultEventArgs> PrintKeySearch_Clicked;
    
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
    }

    protected void btnTaxParceIDSearch_Command(object sender, CommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(tbTaxParcelIDSearch.Text))
        {
            if (PrintKeySearch_Clicked != null)
            {
                List<WACParameter> eventParms = new List<WACParameter>();
                eventParms.Add(new WACParameter("partialPrintKey", e.CommandArgument));
                PrintKeySearch_Clicked(this, new UserControlResultEventArgs(eventParms));
            }
        }
    }
    protected void tbTaxParcelIDSearch_TextChanged(object sender, EventArgs e)
    {
        Button btnTaxParceIDSearch = FindControl("btnTaxParceIDSearch") as Button;
        TextBox tbTaxParcelIDSearch = sender as TextBox;
        btnTaxParceIDSearch.CommandArgument = tbTaxParcelIDSearch.Text;
    }

    
    public override void InitControl(List<WACParameter> parms)
    {
       
    }

    public override void ResetControl()
    {
        
    }

    public override void UpdateControl(List<WACParameter> parms)
    {
        
    }

    public override void CloseControl()
    {
        
    }

    public override void ReBindControl()
    {
        
    }
}
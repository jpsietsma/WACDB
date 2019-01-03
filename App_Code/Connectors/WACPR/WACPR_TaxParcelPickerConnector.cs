using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;

/// <summary>
/// Summary description for WACPR_TaxParcelPickerConnector
/// </summary>
namespace WAC_Connectors
{
    public class WACPR_TaxParcelPickerConnector : UserControlConnector
    {
        public WACPR_TaxParcelPickerConnector() { }
        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPR_TaxParcelPickerVM tpvm = (WACPR_TaxParcelPickerVM)ViewModel;
            WACPR_TaxParcelPickerDP tpdp = (WACPR_TaxParcelPickerDP)tpvm.DataProvider;
            if (ViewModel.DDLBinders.Count < 1)
            {
                tpvm.DDLBinders.TryAdd("ddlCounty", new DDLDataObject(new WACCountyDP(), true, "ddlJurisdiction", false, null));
                tpvm.DDLBinders.TryAdd("ddlJurisdiction", new DDLDataObject(new TaxJurisdictionDP(), true, "ddlTaxParcelID", true, "ddlCounty"));
                tpvm.DDLBinders.TryAdd("ddlTaxParcelID", new DDLDataObject(new TaxParcelPickerIDDP(), false, null, true, "ddlJurisdiction"));
            }   
            
        }
    }
}
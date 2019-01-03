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
using WAC_Validators;

namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelFilterConnector
    /// </summary>
    public class WACPR_TaxParcelFilterConnector : UserControlConnector
    {
        public WACPR_TaxParcelFilterConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
          
            WACPR_TaxParcelFilterVM vm = ViewModel as WACPR_TaxParcelFilterVM;
            if (vm.DDLBinders.Count < 1)
            {
                vm.DDLBinders.TryAdd("ddlTaxParcelID", new DDLDataObject(new TaxParcelIDDP()));
                vm.DDLBinders.TryAdd("ddlSBL", new DDLDataObject(new TaxParcelSBLDP()));
            }
        }
    }
}
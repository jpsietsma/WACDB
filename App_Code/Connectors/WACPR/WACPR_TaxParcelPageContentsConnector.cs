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
using WAC_Connectors;

/// <summary>
/// Summary description for WACPR_TaxParcelPageContentsConnector
/// </summary>
namespace WAC_Connectors
{
    public class WACPR_TaxParcelPageContentsConnector : UserControlConnector
    {
        public WACPR_TaxParcelPageContentsConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPR_TaxParcelPageContentsVM vm = ViewModel as WACPR_TaxParcelPageContentsVM;
            if (vm.ListSource == null)
                vm.ListSource = new MasterDetailDataObject(TaxParcel.PrimaryKeyName, TaxParcel.PrimaryKeyName);
        }
    }
}
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

namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelOwnerGridConnector
    /// </summary>
    public class WACPR_TaxParcelOwnerGridConnector : UserControlConnector
    {
        public WACPR_TaxParcelOwnerGridConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPR_TaxParcelOwnerGridVM vm = (WACPR_TaxParcelOwnerGridVM)ViewModel;
            //vm.WireUp();
        }
    }
}
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

namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelPrintKeySearchConnector
    /// </summary>
    public class WACPR_TaxParcelPrintKeySearchConnector : UserControlConnector
    {
        public WACPR_TaxParcelPrintKeySearchConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPR_TaxParcelPrintKeySearchVM tpvm = (WACPR_TaxParcelPrintKeySearchVM)ViewModel;
            WACPR_TaxParcelPrintKeySearchDP tpdp = (WACPR_TaxParcelPrintKeySearchDP)tpvm.DataProvider;
        }
    }
}
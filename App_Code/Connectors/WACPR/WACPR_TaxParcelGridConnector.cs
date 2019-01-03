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
    /// Summary description for WACPR_TaxParcelGridConnector
    /// </summary>
    public class WACPR_TaxParcelGridConnector : UserControlConnector
    {
        public WACPR_TaxParcelGridConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
           
        }
    }
}
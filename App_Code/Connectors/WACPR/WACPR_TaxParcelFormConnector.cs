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
    /// Summary description for WACPR_TaxParcelFormConnector
    /// </summary>
    public class WACPR_TaxParcelFormConnector : UserControlConnector
    {
        public WACPR_TaxParcelFormConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPR_TaxParcelFormVM vm = (WACPR_TaxParcelFormVM)ViewModel;
            if (vm.TaxParcelValidator == null)
                vm.TaxParcelValidator = _factory.CreateValidator("WACPR_TaxParcelValidator") as WACPR_TaxParcelValidator;
        }
    }
}
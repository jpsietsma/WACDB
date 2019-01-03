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
using WAC_Validators;

namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelOwnerFormConnector
    /// </summary>
    public class WACPR_TaxParcelOwnerFormConnector : UserControlConnector
    {
        public WACPR_TaxParcelOwnerFormConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPR_TaxParcelOwnerFormVM vm = ViewModel as WACPR_TaxParcelOwnerFormVM;
            //vm.WireUp();
            if (vm.TaxParcelOwnerValidator == null)
                vm.TaxParcelOwnerValidator = _factory.CreateValidator("WACPR_TaxParcelOwnerValidator") as WACPR_TaxParcelOwnerValidator;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Validators;
using WAC_Services;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelOwnerContainerVM
    /// </summary>
    public class WACPR_TaxParcelOwnerContainerVM : WACViewModel
    {
        public WACPR_TaxParcelOwnerContainerVM() 
        {
            ServiceRequestEvent += new ServiceRequestHandler(ServiceFactory.Instance.ServiceRequested);
        }

        public override void ContentStateChanged(Control _control, Enum e)
        {
            throw new NotImplementedException();
        }
    }
}
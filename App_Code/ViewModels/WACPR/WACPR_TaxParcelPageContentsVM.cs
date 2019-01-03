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

/// <summary>
/// Summary description for WACPR_TaxParcelPageContentsVM
/// </summary>
namespace WAC_ViewModels
{
    public class WACPR_TaxParcelPageContentsVM : WACViewModel
    {
        public WACPR_TaxParcelPageContentsVM() 
        {
            ServiceRequestEvent += new ServiceRequestHandler(ServiceFactory.Instance.ServiceRequested);
        }

        public override void ContentStateChanged(Control _control, Enum e)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_UserControls;
using WAC_ViewModels;
using System.Collections.Concurrent;

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelGridVM
    /// </summary>
    public class WACPR_TaxParcelGridVM : WACGridViewModel
    {
        public WACPR_TaxParcelGridVM() 
        {
            ServiceRequestEvent += new ServiceRequestHandler(ServiceFactory.Instance.ServiceRequested);
        }

        public override void Delete(WACGridControl wgc, GridView gv, List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }
    }
}
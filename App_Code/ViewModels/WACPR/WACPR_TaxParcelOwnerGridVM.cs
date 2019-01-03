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
using WAC_Validators;

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelOwnerGridVM
    /// </summary>
    public class WACPR_TaxParcelOwnerGridVM : WACGridViewModel
    {
        public WACPR_TaxParcelOwnerGridVM() 
        {
            AuthorizationArea = "taxParcel";
            ServiceRequestEvent += new ServiceRequestHandler(ServiceFactory.Instance.ServiceRequested);
        }
        public override void Delete(WACGridControl wgc, GridView gv, List<WACParameter> parms)
        {
            if (base.Delete<TaxParcelOwner>(parms))
                ContentStateChanged(wgc, WACGridControl.ListState.ItemDeleted);
        }
       
    }
}
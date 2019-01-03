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
    /// Summary description for WACPR_TaxParcelFormVM
    /// </summary>
    public class WACPR_TaxParcelFormVM : WACFormViewModel
    {
        public WACPR_TaxParcelValidator TaxParcelValidator { get; set; }
        public WACPR_TaxParcelFormVM()
        {
            AuthorizationArea = "taxParcel";
            ShowModalReadOnly = true;
            ServiceRequestEvent += new ServiceRequestHandler(ServiceFactory.Instance.ServiceRequested);
        }

        public override void Insert(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            if (base.Insert<TaxParcel>(parms, TaxParcelValidator.ValidateInsert))
                ContentStateChanged(wfc,WACFormControl.FormState.ItemInserted);
        }

        public override void Update(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            if (base.Update<TaxParcel>(parms, TaxParcelValidator.ValidateUpdate))
                ReturnToViewMode(wfc);
        }

        public override void Delete(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            if (base.Delete<TaxParcel>(parms))
                ContentStateChanged(wfc,WACFormControl.FormState.ItemDeleted);
        }

        public override string CustomString(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

    }
}
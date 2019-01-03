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
    /// Summary description for WACPR_TaxParcelOwnerFormVM
    /// </summary>
    public class WACPR_TaxParcelOwnerFormVM : WACFormViewModel
    {
        public WACPR_TaxParcelOwnerValidator TaxParcelOwnerValidator { get; set; }
        public WACPR_TaxParcelOwnerFormVM()
        {
            AuthorizationArea = "taxParcel";
            ShowModalReadOnly = true;
            ServiceRequestEvent += new ServiceRequestHandler(ServiceFactory.Instance.ServiceRequested);
        }

        public override void Insert(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            WACParameter masterKey = ListSource.MasterKey;
            parms.Add(new WACParameter("fk_taxParcel", masterKey.ParmValue, WACParameter.ParameterType.Property));
            if (base.Insert<TaxParcelOwner>(parms, TaxParcelOwnerValidator.ValidateInsert))
            {
                parms.Clear();
                parms.Add(new WACParameter(string.Empty, WACFormControl.FormState.ItemInserted, WACParameter.ParameterType.FormState));
                parms.Add(masterKey);
                ListSource.SetCurrentItem<TaxParcelOwner>(null, null);
                wfc.StateChanged(parms);
            }
        }

        public override void Update(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            if (base.Update<TaxParcelOwner>(parms, TaxParcelOwnerValidator.ValidateUpdate))
            {
                wfc.CurrentState = WACFormControl.FormState.ItemUpdated;
                ReturnToViewMode(wfc);
            }
        }

        public override void Delete(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            if (base.Delete<TaxParcelOwner>(parms))
            {
               // ContentStateChanged(wfc, WACFormControl.FormState.ItemDeleted);
                parms.Add(new WACParameter(string.Empty, WACFormControl.FormState.ItemDeleted, WACParameter.ParameterType.FormState));
                wfc.StateChanged(parms);
            }
        }

        public override string CustomString(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }
    }
}
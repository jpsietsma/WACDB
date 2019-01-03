using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_Services;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Validators;

namespace WAC_ViewModels
{

    /// <summary>
    /// Summary description for WACAG_SupplementalAgreementFormVM
    /// </summary>
    public class WACAG_SupplementalAgreementFormVM : WACFormViewModel
    {
        public WACAG_SupplementalAgreementValidator Validator { get; set; }
        public WACAG_SupplementalAgreementFormVM()
        {
           AuthorizationArea = "Ag";
           ShowModalReadOnly = true;
        }

        public override void Insert(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            if (base.Insert<SupplementalAgreement>(parms, Validator.ValidateInsert))
                //base.ShowModal(wfc, false);
                base.CloseFormView(wfc, fv);
        }

        public override void Update(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        public override void Delete(WACFormControl wfc, FormView fv, List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        public override string CustomString(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }
    }
}
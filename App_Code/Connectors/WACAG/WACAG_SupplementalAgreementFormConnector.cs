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
    /// Summary description for WACAG_SupplementalAgreementFormConnector
    /// </summary>
    public class WACAG_SupplementalAgreementFormConnector : UserControlConnector
    {
        public WACAG_SupplementalAgreementFormConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACAG_SupplementalAgreementFormVM vm = ViewModel as WACAG_SupplementalAgreementFormVM;
            if (vm.Validator == null)
                vm.Validator = _factory.CreateValidator("WACAG_SupplementalAgreementValidator") as WACAG_SupplementalAgreementValidator;
            if (vm.ListSource == null)
                vm.ListSource = new MasterDetailDataObject(SupplementalAgreement.PrimaryKeyName, SupplementalAgreement.PrimaryKeyName);
        }
    }
}
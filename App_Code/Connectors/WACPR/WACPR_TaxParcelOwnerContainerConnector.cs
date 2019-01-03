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
using WAC_Services;

namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelOwnerContainerConnector
    /// </summary>
    public class WACPR_TaxParcelOwnerContainerConnector : UserControlConnector
    {
        public WACPR_TaxParcelOwnerContainerConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPR_TaxParcelOwnerContainerVM vm = ViewModel as WACPR_TaxParcelOwnerContainerVM;
            if (vm.ListSource == null)
                vm.ListSource = new MasterDetailDataObject(TaxParcelOwner.MasterKeyName, TaxParcelOwner.PrimaryKeyName);      
        }
    }
}
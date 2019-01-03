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
    /// Summary description for WACUT_AssociationsConnector
    /// </summary>
    public class WACUT_AssociationsConnector : UserControlConnector
    {
        public WACUT_AssociationsConnector() { }

        public override void ConnectControlSpecific(System.Web.UI.Control _control, ConnectorFactory _factory)
        {
            WACUT_AssociationsVM vm = (WACUT_AssociationsVM)ViewModel;
            WACUT_AssociationsDP dp = (WACUT_AssociationsDP)vm.DataProvider;
            if (vm.ListSource == null)
                vm.ListSource = new MasterDetailDataObject(Association.PrimaryKeyName, Association.PrimaryKeyName);
        }
    }
}
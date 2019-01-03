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
    /// Summary description for WACAG_FarmBusinessFormConnector
    /// </summary>
    public class WACAG_FarmBusinessFormConnector : UserControlConnector
    {
        public WACAG_FarmBusinessFormConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACAG_FarmBusinessFormVM vm = ViewModel as WACAG_FarmBusinessFormVM;
            if (vm.Validator == null)
                vm.Validator = _factory.CreateValidator("WACAG_FarmBusinessValidator") as WACAG_FarmBusinessValidator;
            if (vm.ListSource == null)
                vm.ListSource = new MasterDetailDataObject(FarmBusiness.PrimaryKeyName, FarmBusiness.PrimaryKeyName);
            if (ViewModel.DDLBinders.Count < 1)
            {
                ViewModel.DDLBinders.TryAdd("ddlProgramWAC", new DDLDataObject(new WACProgramDP()));
                ViewModel.DDLBinders.TryAdd("ddlFarmSize", new DDLDataObject(new FarmSizeDP()));
                ViewModel.DDLBinders.TryAdd("ddlBasin", new DDLDataObject(new BasinDP()));
                ViewModel.DDLBinders.TryAdd("ddlSoldFarm", new DDLDataObject(new FarmIDFarmNameDP()));
            }    
        }
    }
}
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
    /// Summary description for WACUT_AttachedDocumentViewerConnector
    /// </summary>
    public class WACUT_AttachedDocumentViewerConnector : UserControlConnector
    {
        public WACUT_AttachedDocumentViewerConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACViewModel vm = ViewModel;
            WACUT_AttachedDocumentViewerDP dp = (WACUT_AttachedDocumentViewerDP)vm.DataProvider;
            if (vm.ListSource == null)
                vm.ListSource = new MasterDetailDataObject(AttachedDocument.MasterKeyName, AttachedDocument.PrimaryKeyName);
        }
    }
}
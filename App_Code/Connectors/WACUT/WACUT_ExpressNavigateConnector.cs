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

    public class WACUT_ExpressNavigateConnector : UserControlConnector
    {
        public WACUT_ExpressNavigateConnector()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            
        }
    }
}
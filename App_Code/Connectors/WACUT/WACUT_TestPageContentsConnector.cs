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


namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACUT_TestPageContentsConnector
    /// </summary>
    public class WACUT_TestPageContentsConnector : UserControlConnector
    {
        public WACUT_TestPageContentsConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            return;
        }
    }
}
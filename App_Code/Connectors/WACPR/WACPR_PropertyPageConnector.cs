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
/// <summary>
/// Summary description for WACPR_PropertyPageConnector
/// </summary>
namespace WAC_Connectors
{
    public class WACPR_PropertyPageConnector : UserControlConnector
    {
        public WACPR_PropertyPageConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            //throw new NotImplementedException();
        }
    }
}
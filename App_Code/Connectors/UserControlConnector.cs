using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;


/// <summary>
/// Summary description for UserControlConnector
/// </summary>
namespace WAC_Connectors
{
    public abstract class UserControlConnector : WACControlConnector
    {
        
        //public override string GetConnectorType()
        //{
        //    return "USERCONTROL";
        //}
        
        public WACDataProvider GetDataProvider(string _dpName, ConnectorFactory _factory)
        {
            return _factory.CreateDataProvider(_dpName);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Event;
using WAC_DataObjects;
using WAC_Connectors;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Exceptions;
using System.Web.SessionState;
using System.Collections.Concurrent;

/// <summary>
/// Summary description for AbstractServiceMessage
/// </summary>
namespace WAC_Services
{
    public abstract class ServiceMessage
    {
        public HttpSessionState Session { get; set; }
        public Control Requestor { get; set; }
        public Control ServiceFor { get; set; }
        public List<WACParameter> ParmList { get; set; }
        public ServiceFactory.ServiceTypes ServiceRequested { get; set; }
    }
}
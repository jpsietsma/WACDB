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
/// <summary>
/// Summary description for ServiceRequest
/// </summary>
namespace WAC_Services
{
    public class ServiceRequest : ServiceMessage
    {
        public ServiceRequest() { }
        public ServiceRequest(Control _requestor)
        {
            Requestor = _requestor;
            ParmList = new List<WACParameter>();
        }
        public ServiceRequest(Control _requestor, ServiceFactory.ServiceTypes _service)
        {
            Requestor = _requestor;
            ServiceRequested = _service;
            ParmList = new List<WACParameter>();
        }
    }
}
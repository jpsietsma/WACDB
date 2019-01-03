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
/// Summary description for ServiceResponse
/// </summary>
namespace WAC_Services
{
    public class ServiceResponse : ServiceMessage
    {
        public object ResponseObject { get; set; }
        //public ServiceFactory.ServiceResults Result { get; set; }
        private ServiceResponse(Control _requestor, ServiceFactory.ServiceTypes _service)
        {
            Requestor = _requestor;
            ServiceRequested = _service;
        }
        public ServiceResponse(ServiceMessage _request)
        {
            new ServiceResponse(_request.Requestor, _request.ServiceRequested);
        }
    }
}
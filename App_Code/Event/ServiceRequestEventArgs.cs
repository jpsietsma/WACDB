using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Services;

/// <summary>
/// Summary description for ServiceRequestEventArgs
/// </summary>
namespace WAC_Event
{

    public class ServiceRequestEventArgs
    {
        public ServiceRequest Sr { get; set; }
        public ServiceRequestEventArgs(ServiceRequest sr)
        {
            this.Sr = sr;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_DataObjects;
using WAC_Containers;

/// <summary>
/// Summary description for WACPage
/// </summary>
namespace WAC_UserControls
{
    public abstract class WACPage : System.Web.UI.Page, IWACControl
    {
        
        public override string ClientID { get { return this.ID; } }
        public ServiceRequest sReq { get; set; }
        public ServiceResponse sResp { get; set; }
        public bool Visibility { get; set; }
        public abstract void OpenDefaultDataView(List<WACParameter> parms);

        public void Register(Control c)
        {
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.RegisterConnectionComponents;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
        
        public void HandleRedirectFromAnotherPage(HttpRequest _request)
        {           
            string pk = _request.QueryString["pk"];
            if (!string.IsNullOrEmpty(pk))
            {
                List<WACParameter> parms = new List<WACParameter>();
                parms.Add(new WACParameter(string.Empty, pk, WACParameter.ParameterType.PrimaryKey));
                OpenDefaultDataView(parms);
                parms = null;
            }           
        }
        
    }
}
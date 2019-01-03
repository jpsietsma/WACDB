using System;
using System.Text;
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
using WAC_Services;

namespace WAC_UserControls
{
    /// <summary>
    /// Summary description for WACControl
    /// </summary>
    public abstract class WACControl : UserControl, IWACControl
    {
        
        public enum WACControlState { PersistedValue }
        public bool IsActiveReadOnly { get; set; }
        public bool IsActiveUpdate { get; set; }
        public bool IsActiveInsert { get; set; }
        public bool DefaultVisibility { get; set; }
        public bool IsInitalized { get; set; }  
        public ServiceRequest sReq { get; set; }
        public abstract void InitControl(List<WACParameter> parms);
        public abstract void UpdateControl(List<WACParameter> parms);
        public abstract void CloseControl();
        public abstract void ResetControl();
        public abstract void ReBindControl();

        public delegate void ServiceRequestHandler(object sender, ServiceRequestEventArgs s);
        public event ServiceRequestHandler ServiceRequestEvent;
        public void OnServiceRequested(object sender, ServiceRequestEventArgs s)
        {
            if (ServiceRequestEvent != null)
                ServiceRequestEvent(sender, s);
        }
        public event EventHandler<UserControlResultEventArgs> ContentStateChanged;
        public void StateChanged(List<WACParameter> parms)
        {
            if (ContentStateChanged != null)
                ContentStateChanged(this, new UserControlResultEventArgs(parms));
        }

        public void Register(Control c)
        {
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.RegisterConnectionComponents;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
        public void Connect(Control c)
        {
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.MakeConnections;
            sReq.Session = c.Page.Session;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
        public void RegisterAndConnect(Control c)
        {
            Register(c);
            Connect(c);
        }
      
       
        public override bool Equals(object obj)
        {
            if (!ServiceFactory.IsWACControl((Control)obj))
                return false;
            else
                return this.ClientID == ((WACControl)obj).ClientID;
        }
        
       
        public bool IsActiveFormMode(List<WACParameter> parms)
        {
            try
            {
                WACFormControl.FormState state = (WACFormControl.FormState)WACParameter.GetParameterValue(parms, WACParameter.ParameterType.FormState);
                if (state == WACFormControl.FormState.OpenView && IsActiveReadOnly)
                    return true;
                if (state == WACFormControl.FormState.OpenInsert && IsActiveInsert)
                    return true;
                if (state == WACFormControl.FormState.OpenUpdate && IsActiveUpdate)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return true;
            }
            
        }
        public void UpdatePanelUpdate()
        {
            UpdatePanel up = GetUpdatePanel(this);
            if (up != null)
                up.Update();
        }
        public UpdatePanel GetUpdatePanel(Control c)
        {
            return WACGlobal_Methods.FindControl<UpdatePanel>(c); 
        }

    }
}

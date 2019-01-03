using System;
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
using System.Text;

/// <summary>
/// Summary description for AbstractWACFormControl
/// </summary>
namespace WAC_UserControls
{

    public abstract class WACFormControl : WACControl
    {
        public enum FormState { Closed, OpenView, OpenInsert, OpenUpdate, UpdateCanceled, ItemDeleted, ItemInserted, ItemUpdated, Stale }
        public FormState CurrentState 
        {
            get { return (FormState)(ViewState[ClientID + "_FormState"] ?? FormState.Stale); }
            set { ViewState[ClientID + "_FormState"] = value; } 
        }
       
        public void OpenView(Control form, List<WACParameter> parms)
        {          
            ServiceRequest sr = new ServiceRequest(form);
            sr.ServiceFor = form;
            sr.ParmList = parms;          
            sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenFormViewReadOnly;
            ServiceFactory.Instance.ServiceRequest(sr);
          //  OnServiceRequested(form, new ServiceRequestEventArgs(sr));
        }
        public void OpenAdd(FormView fv)
        {
            ServiceRequest sr = new ServiceRequest(this);
            OpenAdd(sr, fv);
            sr = null;
        }
        public void OpenAdd(ServiceRequest sr, FormView fv)
        {
            CurrentState = FormState.OpenInsert;           
            sr.ServiceFor = fv;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenFormViewInsert;
            ServiceFactory.Instance.ServiceRequest(sr);
        }
        public void OpenEdit(FormView fv, string keyName, object key)
        {
            ServiceRequest sr = new ServiceRequest(this);
            OpenEdit(sr, fv, keyName, key);
            sr = null;
        }
        public void OpenEdit(Control form, List<WACParameter> parms)
        {

            ServiceRequest sr = new ServiceRequest(form);
            sr.ServiceFor = form;
            sr.ParmList = parms;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenFormViewUpdate;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public void OpenEdit(ServiceRequest sr, FormView fv, string keyName, object key)
        {
            CurrentState = FormState.OpenUpdate;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenFormViewUpdate;
            sr.ServiceFor = fv;
            WACParameter wp = new WACParameter(keyName, key, WACParameter.ParameterType.SelectedKey);
            sr.ParmList.Clear();
            sr.ParmList.Add(wp);
            ServiceFactory.Instance.ServiceRequest(sr);
        }
        public void Close(FormView fv)
        {
            ServiceRequest sr = new ServiceRequest(this);
            Close(sr, fv);
            sr = null;
        }
        public void Close(ServiceRequest sr, FormView fv)
        {
            CurrentState = FormState.Closed;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.CloseFormView;
            sr.ServiceFor = fv;
            ServiceFactory.Instance.ServiceRequest(sr);
        }
        public void CancelUpdate(FormView fv)
        {
            ServiceRequest sr = new ServiceRequest(this);
            CancelUpdate(sr, fv);
            sr = null;
        }
        public void CancelUpdate(ServiceRequest sr, FormView fv)
        {
            CurrentState = FormState.UpdateCanceled;
            sr.ServiceFor = fv;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ReturnFormToViewMode;
            ServiceFactory.Instance.ServiceRequest(sr);
        }

    }
}
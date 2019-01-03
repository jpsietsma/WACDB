using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
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

namespace WAC_Containers
{

    /// <summary>
    /// Summary description for WACGridFormContainer
    /// </summary>
    public abstract class WACGridFormContainer : WACContainer
    {

        public void ContainedFormStateChanged(Control container, Control grid, Control form, List<WACParameter> parms)
        {
            WACParameter wp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.FormState);
            WACFormControl.FormState state = (WACFormControl.FormState)wp.ParmValue;
            ((WACFormControl)form).CurrentState = state;     
            ServiceRequest sr = new ServiceRequest(container);
            sr.ServiceFor = form;
            sr.ParmList = parms;
            switch (state)
            {
                case WACFormControl.FormState.Closed:
                    
                    //((WACGridControl)grid).InitControl(parms);
                    break;
                //case WACFormControl.FormState.OpenView:
                //    break;
                //case WACFormControl.FormState.OpenInsert:
                //    break;
                case WACFormControl.FormState.OpenUpdate:
                   
                    break;
                //case WACFormControl.FormState.UpdateCanceled:
                //    break;
                case WACFormControl.FormState.ItemDeleted:
                    sr.ServiceFor = form;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.CloseFormView;
                    //OnServiceRequested(this, new ServiceRequestEventArgs(sr));
                    ServiceFactory.Instance.ServiceRequest(sr);
                    ((WACControl)container).UpdateControl(null);
                    break;
                case WACFormControl.FormState.ItemInserted:
                    WACParameter.RemoveAllButParameterType(parms, WACParameter.ParameterType.MasterKey);
                    ((WACGridControl)grid).CurrentState = WACDataBoundListControl.ListState.ListEmpty;
                    ((WACGridControl)grid).InitControl(parms);
                    sr.ServiceFor = form;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.CloseFormView;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    //OnServiceRequested(this, new ServiceRequestEventArgs(sr));
                    ((WACControl)container).UpdateControl(null);
                    break;
                case WACFormControl.FormState.ItemUpdated:
                    break;
                default:
                    //grid.Visible = ((WACControl)grid).DefaultVisibility;
                    //form.Visible = ((WACControl)form).DefaultVisibility;
                    //container.Visible = ((WACControl)container).DefaultVisibility;
                    //if (up != null)
                    //    up.Visible = ((WACContainer)container).DefaultVisibility;
                    break;
            }
         //   this.UpdatePanelUpdate();
            sr = null;
        }
        
        public void ContainedGridStateChanged(Control container, Control grid, Control form, List<WACParameter> parms)
        {
            WACParameter wp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.ListState);
            WACGridControl.ListState state = (WACGridControl.ListState)wp.ParmValue;
            ((WACGridControl)grid).CurrentState = state;
            ServiceRequest sr = new ServiceRequest(container);
            sr.ServiceFor = grid;
            sr.ParmList = parms;
            switch (state)
            {
                case WACGridControl.ListState.MasterKeySet:
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.InitControls;
                    //OnServiceRequested(this, new ServiceRequestEventArgs(sr));
                    //ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACGridControl.ListState.ListFull:
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
                    //OnServiceRequested(this, new ServiceRequestEventArgs(sr));
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACGridControl.ListState.ListSingle:
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
                    //OnServiceRequested(this, new ServiceRequestEventArgs(sr));
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACGridControl.ListState.ListEmpty:
                    goto case WACGridControl.ListState.Closed;
                //case WACGridControl.ListState.OpenView:
                //    grid.Visible = ((WACGridControl)grid).DefaultVisibility;
                //    break;
                case WACGridControl.ListState.SelectionMade:
                    WACParameter fp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.FormState);
                    if (fp != null)
                    {
                        WACFormControl.FormState fs = (WACFormControl.FormState)fp.ParmValue;
                        if (fs == WACFormControl.FormState.OpenView)
                            ((WACFormControl)form).OpenView(form, sr.ParmList);
                        else
                            ((WACFormControl)form).OpenEdit(form, sr.ParmList);
                    }
                    break;
                case WACGridControl.ListState.Closed:
                    FormView fv = WACGlobal_Methods.FindControl<FormView>(form);
                    ((WACFormControl)form).Close(fv);
                    break;
                case WACGridControl.ListState.ItemDeleted:
                     sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
                     //OnServiceRequested(this, new ServiceRequestEventArgs(sr));
                    ServiceFactory.Instance.ServiceRequest(sr);
                    ((WACControl)container).UpdateControl(null);
                    break;
                default:
                    break;
            }
          //  this.UpdatePanelUpdate();
            sr = null;
        }
    }
}
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
    /// Summary description for WACFilterGridFormContainer
    /// </summary>
    public abstract class WACFilterGridFormContainer : WACContainer, IWACIndependentControl
    {       
        public void ContainedFilterStateChanged(Control container, Control filter, Control grid, Control form, List<WACParameter> parms)
        {
            WACParameter wp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.FilterState);
            WACFilterControl.FilterState state = (WACFilterControl.FilterState)wp.ParmValue;
            ServiceRequest sr = new ServiceRequest(container);
            switch (state)
            {
                case WACFilterControl.FilterState.Unfiltered:
                    sr.ServiceFor = grid;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.GridViewList;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACFilterControl.FilterState.Filtered:
                    sr.ServiceFor = grid;
                    sr.ParmList = parms;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.FilteredGridViewList;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACFilterControl.FilterState.Reset:
                    sr.ServiceFor = grid;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.ClearGridView;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                default:
                    break;
            }
            sr = null;
        }
        public void ContainedGridStateChanged(Control container, Control filter, Control grid, Control form, List<WACParameter> parms)
        {
            WACParameter wp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.ListState);
            WACGridControl.ListState state = (WACGridControl.ListState)wp.ParmValue;
            ((WACGridControl)grid).CurrentState = state;
            ServiceRequest sr = new ServiceRequest(container);
            sr.ServiceFor = grid;
            sr.ParmList = parms;
            switch (state)
            {
                case WACGridControl.ListState.ListFull:
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    sr.ServiceFor = form;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.ClearFormView;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACGridControl.ListState.ListSingle:
                    goto case WACGridControl.ListState.SelectionMade;
                case WACGridControl.ListState.ListEmpty:
                    goto case WACGridControl.ListState.Closed;
                case WACGridControl.ListState.OpenView:
                    break;
                case WACGridControl.ListState.SelectionMade:
                    ((WACFormControl)form).OpenView(form, sr.ParmList);
                    break;
                case WACGridControl.ListState.Closed:
                    FormView fv = WACGlobal_Methods.FindControl<FormView>(form);
                    ((WACFormControl)form).Close(fv);
                    break;
                default:
                    break;
            }
            this.UpdatePanelUpdate();
            sr = null;
        }

        public void ContainedFormStateChanged(Control container, Control filter, Control grid, Control form, List<WACParameter> parms)
        {
            WACParameter wp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.FormState);
            WACFormControl.FormState state = (WACFormControl.FormState)wp.ParmValue;
            ((WACFormControl)form).CurrentState = state;
            ServiceRequest sr = new ServiceRequest(container);
            sr.ServiceFor = form;
            sr.ParmList = parms;
            switch (state)
            {
                //case WACFormControl.FormState.Closed:
                //    grid.Visible = ((WACGridControl)grid).DefaultVisibility;
                //    filter.Visible = ((WACFilterControl)filter).DefaultVisibility;
                //    break;
                //case WACFormControl.FormState.OpenView:
                //    grid.Visible = ((WACGridControl)grid).IsActiveReadOnly;
                //    filter.Visible = ((WACFilterControl)filter).IsActiveReadOnly;
                //    break;
                //case WACFormControl.FormState.OpenInsert:
                //    grid.Visible = ((WACGridControl)grid).IsActiveInsert;
                //    filter.Visible = ((WACFilterControl)filter).IsActiveInsert;
                //    break;
                //case WACFormControl.FormState.OpenUpdate:
                //    break;
                case WACFormControl.FormState.UpdateCanceled:
                    break;
                case WACFormControl.FormState.ItemDeleted:
                    sr.ServiceFor = grid;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACFormControl.FormState.ItemInserted:
                    WACParameter.RemoveAllButParameterType(parms, WACParameter.ParameterType.SelectedKey);
                    sr.ServiceFor = grid;
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.FilteredGridViewList;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACFormControl.FormState.ItemUpdated:
                    break;
                default:
                    break;
            }
            this.UpdatePanelUpdate();
            sr = null;
        }

    }
}
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
using System.Text;
using WAC_Containers;

/// <summary>
/// Summary description for WACControl
/// </summary>
namespace WAC_UserControls
{
    public abstract class WACTabControl : System.Web.UI.UserControl//, IWACIndependentDataSourceContainer
    {
        public abstract UpdatePanel GetUpdatePanel();
        public abstract void ContainedGridChanged(List<WACParameter> parms);
        public abstract void ContainedFormChanged(List<WACParameter> parms, WACFormControl form);
        public abstract void ContainedListChanged(List<WACParameter> parms);
        public abstract void InitContainedControls(List<WACParameter> parms);
        public int MyTabIndex { get; set; }
        public ServiceRequest sReq { get; set; }
        public ServiceResponse sResp { get; set; }
        public bool Visibility { get; set; }
        public void Register(Control c)
        {
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.RegisterConnectionComponents;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
        //public void RegisterAndConnect(Control c, ref ServiceRequest sReq, ServiceResponse sResp)
        //{
        //    sReq = new ServiceRequest(c);
        //    sReq.ServiceRequested = ServiceFactory.ServiceTypes.RegisterConnectionComponents;
        //    ServiceFactory.Instance.ServiceRequest(sReq);
        //    if (sResp.Result == ServiceFactory.ServiceResults.Success)
        //    {
        //        sReq.ServiceRequested = ServiceFactory.ServiceTypes.MakeConnections;
        //        sReq.Session = c.Page.Session;
        //        ServiceFactory.Instance.ServiceRequest(sReq);
        //    }
        //}

        public void OpenTabControl(Control tabControl, List<WACParameter> parms)
        {
            sReq.ParmList = null;
            sReq.ParmList = parms;
            sReq.Requestor = tabControl;
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.FilteredGridViewList;
            ServiceFactory.Instance.ServiceRequest(sReq);
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
            ServiceFactory.Instance.ServiceRequest(sReq);
            InitContainedControls(parms);
            UpdatePanel up = GetUpdatePanel();
            if (up != null)
                up.Update();
        }
        public void ResetTabControl(Control tabControl, List<WACParameter> parms)
        {
            sReq.Requestor = tabControl;
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.ClearGridView;
            ServiceFactory.Instance.ServiceRequest(sReq);
            UpdatePanel up = GetUpdatePanel();
            if (up != null)
                up.Update();
        }
        public void ChildFormStateChanged(Control c, List<WACParameter> parms, WACFormControl wfc)
        {
        }
        public void ChildGridStateChanged(Control c, List<WACParameter> parms)
        {
            WACGridControl.ListState state = (WACGridControl.ListState)WACParameter.GetParameterValue(parms, WACParameter.ParameterType.ListState);
            ServiceRequest sr = new ServiceRequest(c);
            switch (state)
            {
                case WACGridControl.ListState.ListFull:
                    //sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
                    //ServiceFactory.Instance.ServiceRequest(sr);
                    //sr.ServiceRequested = ServiceFactory.ServiceTypes.CloseFormView;
                    //ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACGridControl.ListState.ListSingle:
                    //sr.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
                    //ServiceFactory.Instance.ServiceRequest(sr);
                    //sr.ParmList = Parms;
                    //sr.ServiceRequested = ServiceFactory.ServiceTypes.HandleGridViewSelection;
                    //ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                case WACGridControl.ListState.ListEmpty:
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.ClearFormView;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    //ResetTabs(sr);
                    break;
                case WACGridControl.ListState.OpenView:
                    break;
                case WACGridControl.ListState.SelectionMade:
                    break;
                case WACGridControl.ListState.Closed:
                    sr.ServiceRequested = ServiceFactory.ServiceTypes.ClearFormView;
                    ServiceFactory.Instance.ServiceRequest(sr);
                    break;
                default:
                    break;
            }
            sr = null;
        }
    }
}
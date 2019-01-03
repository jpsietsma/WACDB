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

/// <summary>
/// Summary description for AbstractWACGridControl
/// </summary>
namespace WAC_UserControls
{
    public abstract class WACGridControl : WACDataBoundListControl
    {
        public void LoadList(List<WACParameter> parms)
        {
            if (CurrentState == ListState.OpenView)
                sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
            else
                sReq.ServiceRequested = ServiceFactory.ServiceTypes.FilteredGridViewList;
            sReq.ServiceFor = this;
            sReq.ParmList = parms;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
        public void RowSelected(WACGridControl wgc, string pkName, object pkValue)
        {
            wgc.CurrentState = ListState.SelectionMade;
            List<WACParameter> eParms = new List<WACParameter>();
            eParms.Add(new WACParameter(string.Empty, WACGridControl.ListState.SelectionMade, WACParameter.ParameterType.ListState));
            eParms.Add(new WACParameter(pkName, pkValue, WACParameter.ParameterType.SelectedKey));
            StateChanged(eParms);
        }
        public void PageIndexChanging(WACDataBoundListControl wc, int newPageIndex)
        {
            ServiceRequest sr = new ServiceRequest(this, ServiceFactory.ServiceTypes.PageGridView);
            sr.Requestor = wc;
            sr.ParmList.Add(new WACParameter(string.Empty, newPageIndex, WACParameter.ParameterType.ListPage));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public void ListSorting(WACDataBoundListControl wc, string _sortBy)
        {
            ServiceRequest sr = new ServiceRequest(this, ServiceFactory.ServiceTypes.SortGridView);
            sr.Requestor = wc;
            sr.ParmList.Add(new WACParameter(string.Empty, _sortBy, WACParameter.ParameterType.ListSort));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }

    }
}
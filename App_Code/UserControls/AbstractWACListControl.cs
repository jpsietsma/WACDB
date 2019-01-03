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


namespace WAC_UserControls
{
    /// <summary>
    /// Summary description for WACListControl
    /// </summary>
    public abstract class WACListControl : WACDataBoundListControl
    {
       
        public void LoadList(List<WACParameter> parms)
        {
            if (CurrentState == ListState.OpenView)
                sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenListView;
            else
                sReq.ServiceRequested = ServiceFactory.ServiceTypes.FilteredListViewList;
            sReq.ServiceFor = this;
            sReq.ParmList = parms;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
        public void PageIndexChanging(WACListControl wc, int newPageIndex)
        {
            ServiceRequest sr = new ServiceRequest(this, ServiceFactory.ServiceTypes.PageGridView);
            sr.Requestor = wc;
            sr.ParmList.Add(new WACParameter(string.Empty, newPageIndex, WACParameter.ParameterType.ListPage));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr.ServiceRequested = ServiceFactory.ServiceTypes.UpdatePanelUpdate;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }

        public void ListSorting(WACListControl wc, string _sortBy)
        {
            ServiceRequest sr = new ServiceRequest(this, ServiceFactory.ServiceTypes.SortGridView);
            sr.Requestor = wc;
            sr.ParmList.Add(new WACParameter(string.Empty, _sortBy, WACParameter.ParameterType.ListSort));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr.ServiceRequested = ServiceFactory.ServiceTypes.UpdatePanelUpdate;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }

        public void ListReset(WACListControl wc)
        {

        }
    }
}
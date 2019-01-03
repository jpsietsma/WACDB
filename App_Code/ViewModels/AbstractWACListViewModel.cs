using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Containers;

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACListViewModel
    /// </summary>
    public abstract class WACListViewModel : WACListControlViewModel
    {
        public abstract void CustomAction(List<WACParameter> parms);
        public abstract string CustomString(List<WACParameter> parms);
        public override void ContentStateChanged(Control control, Enum state)
        {
            ServiceRequest sr = new ServiceRequest(control);
            sr.ParmList.Add(new WACParameter(string.Empty, state, WACParameter.ParameterType.ListState));
            sr.ParmList.Add(new WACParameter(string.Empty, ListSource.RowCount().ToString(), WACParameter.ParameterType.RowCount));
            if (ListSource.SelectedKey != null)
                sr.ParmList.Add(ListSource.SelectedKey);
            if (ListSource.PrimaryKey != null)
                sr.ParmList.Add(ListSource.PrimaryKey);
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContentStateChanged;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public void BindListView(ListView lv, IList list)
        {
            lv.DataKeyNames = ListSource.DataKeyNames;
            lv.DataSource = list;
            lv.DataBind();
        }
        public void BindListView(ListView lv)
        {
            if (ListSource != null && ListSource.VList != null)
                BindListView(lv, ListSource.VList);
        }
 
        public void OpenListViewReadOnly(WACListControl wlc, ListView lv)
        {
            Enum state;
            try
            {
                if (ListSource.VList != null)
                {
                    BindListView(lv);
                    state = WACListControl.ListState.OpenView;
                }
                else
                {
                    CloseListView(lv);
                    state = WACListControl.ListState.Closed;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not bind ListView " + lv.ID + " " + ex.Message);
            }
            ContentStateChanged(wlc,state);
        }

        public void CloseListView(ListView lv)
        {
            lv.DataKeyNames = null;
            lv.DataSource = null;
            lv.DataBind();
        }
        public void ClearListView(WACListControl wlc, ListView lv)
        {
            if (ListSource.VList != null)
                ListSource.VList.Clear();
            CloseListView(lv);
            if (wlc != null)
                ContentStateChanged(wlc,WACListControl.ListState.ListEmpty);
        }
        
    }
}
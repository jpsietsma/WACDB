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


/// <summary>
/// Summary description for AbstractWACGridViewModel
/// </summary>
namespace WAC_ViewModels
{
    public abstract class WACGridViewModel : WACListControlViewModel
    {
        private string sortExpression = null;
        private SortDirection sortDirection = SortDirection.Ascending;
        public abstract void Delete(WACGridControl wgc, GridView gv, List<WACParameter> parms);

        public override void ContentStateChanged(Control listControl, Enum state)
        {
            ServiceRequest sr = new ServiceRequest(listControl);
            sr.ParmList.Add(new WACParameter(string.Empty, state, WACParameter.ParameterType.ListState));
            sr.ParmList.Add(new WACParameter(string.Empty, ListSource.RowCount().ToString(), WACParameter.ParameterType.RowCount));
            if (ListSource.SelectedKey != null)
                sr.ParmList.Add(ListSource.SelectedKey);
            if (ListSource.PrimaryKey != null)
                sr.ParmList.Add(ListSource.PrimaryKey);
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContentStateChanged;
            OnServiceRequested(listControl, new ServiceRequestEventArgs(sr));
            sr = null;
        }
        public void BindGridView(GridView gv, IList list)
        {
            gv.DataKeyNames = ListSource.DataKeyNames;
            gv.DataSource = list;
            gv.DataBind();
        }
        public void BindGridView(GridView gv)
        {
            if (ListSource != null && ListSource.VList != null)
                BindGridView(gv,ListSource.VList);          
        }
      
        public void OpenGridViewReadOnly(WACGridControl wgc, GridView gv)
        {
            Enum state;
            try
            {
                //CloseGridView(gv);
                if (ListSource.VList != null)
                {
                    BindGridView(gv);
                    state = WACGridControl.ListState.OpenView;
                }
                else
                    state = WACGridControl.ListState.Closed;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not bind GridView " + gv.ID + " " + ex.Message);
            }
            ContentStateChanged(wgc,state);
        }

        public void CloseGridView(GridView gv)
        {
            gv.DataKeyNames = null;
            gv.DataSource = null;
            gv.DataBind();
        }
        public void ClearGridView(WACGridControl wgc, GridView gv)
        {
            if (ListSource.VList != null)
                ListSource.VList.Clear();
            CloseGridView(gv);
            if (wgc != null)
                ContentStateChanged(wgc,WACGridControl.ListState.ListEmpty);
        }
        public void PageGridView(GridView gv, List<WACParameter> parms)
        {
            gv.PageIndex = WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms, WACParameter.ParameterType.ListPage));
            BindGridView(gv);
        }
        public void SortGridView(GridView gv, List<WACParameter> parms)
        {
            string srt = WACParameter.GetParameterValue(parms, WACParameter.ParameterType.ListSort) as string;
            string sortDirection = ToggleSortDirection(srt);
            sortExpression = srt;
            IList sorted = ListSource.GetSortedList(DataProvider.SortedList, sortExpression, sortDirection);
            BindGridView(gv, sorted);
        }
        private string ToggleSortDirection(string exp)
        {
            // if the "new" sort expression is the current sort expression, change the order otherwise revert to Ascending
            if (sortExpression != null)
            {
                if (sortExpression != null && exp.Contains(sortExpression))
                {
                    sortDirection = sortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
                }
                else
                    sortDirection = SortDirection.Descending;
            }
            else
                sortDirection = SortDirection.Descending;
            return sortDirection == SortDirection.Ascending ? "ASC" : "DESC";
        }
    }
}
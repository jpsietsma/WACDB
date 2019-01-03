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


namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACListControlViewModel
    /// </summary>
    public abstract class WACListControlViewModel : WACViewModel
    {
        public void GetListRowCount(Control listControl)
        {
            ((WACDataBoundListControl)listControl).RowCount = ListSource.RowCount().ToString();
        }
       
        public void GetViewList(Control listControl, IDataBoundListControl lv, List<WACParameter> parms, MasterDetailDataObject.FilteredListGetterDelegate _getList)
        {
            Enum state = WACListControl.ListState.ListEmpty;
            try
            {
                ListSource.FList = null;
                ListSource.GetFilteredItemList(parms, _getList);
                state = processIDataBoundListControlList(listControl,lv);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not bind IDataBoundListControl " + listControl.ID + " " + ex.Message);
            }
            finally
            {
                ContentStateChanged(listControl,state);
            }
        }
        public void GetViewList(Control listControl, IDataBoundListControl lv, List<WACParameter> parms)
        {
            Enum state = WACListControl.ListState.ListEmpty;
            try
            {
                ListSource.FList = null;
                if (parms.Count == 0)
                    ListSource.GetFullItemList(DataProvider.GetList);
                else
                    ListSource.GetFilteredItemList(parms, DataProvider.GetFilteredList);
                state = processIDataBoundListControlList(listControl,lv);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not bind IDataBoundListControl " + listControl.ID + " " + ex.Message);
            }
            finally
            {
                ContentStateChanged(listControl,state);
            }
        }
        public void GetViewList(Control listControl, IDataBoundListControl lv)
        {
            List<WACParameter> parms = new List<WACParameter>();
            if (ListSource.SelectedKey != null)
                parms.Add(ListSource.SelectedKey);
            GetViewList(listControl, lv, parms);
        }
        private Enum processIDataBoundListControlList(Control listControl,IDataBoundListControl lv)
        {
            if (ListSource.VList != null || ListSource.VList.Count < 1)
            {
                if (ListSource.VList.Count == 1)
                {
                    ListSource.SetPrimaryKeyValue(DataProvider.PrimaryKeyName(), DataProvider.PrimaryKeyValue(ListSource.VList));
                    ListSource.SetSelectedKeyValue(DataProvider.PrimaryKeyName(), DataProvider.PrimaryKeyValue(ListSource.VList));
                    return WACGridControl.ListState.ListSingle;
                }
                else
                    return WACGridControl.ListState.ListFull;
            }
            else
                return WACGridControl.ListState.ListEmpty;
        }
    }
}
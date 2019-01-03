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
using WAC_Extensions;
using WAC_Services;
using WAC_DataProviders;

/// <summary>
/// Summary description for MasterDetailDataObject
/// </summary>
namespace WAC_DataObjects
{
    public class MasterDetailDataObject
    {
        public IList VList { get; set; }
        public IList FList { get; set; }
        public WACParameter MasterKey { get; set; }
        public WACParameter PrimaryKey { get; set; }
        public WACParameter SelectedKey { get; set; }
        public string[] DataKeyNames { get; set; }
        public int RemovedAt { get; set; }
        public delegate IList SortedIListDelegate(IList list, string sortExp, string sortDir);
        public delegate IList UnfilteredListGetterDelegate();
        public delegate IList FilteredListGetterDelegate(List<WACParameter> parms);
        public delegate IList ItemGetterDelegate(List<WACParameter> parms, IList list);
        public delegate IList EmptyItemGetterDelegate();
        public delegate object KeyGetterDelegate(IList list);
        public delegate WACParameter PKGetter<T>(T item);

        public MasterDetailDataObject() { }
        public MasterDetailDataObject(string _master, string _primary)
        {
            MasterKey = new WACParameter();
            MasterKey.ParmName = _master;
            MasterKey.ParmType = WACParameter.ParameterType.MasterKey;
            PrimaryKey = new WACParameter();
            PrimaryKey.ParmName = _primary;
            PrimaryKey.ParmType = WACParameter.ParameterType.PrimaryKey;
            DataKeyNames = new string[] { _primary };
        }
        public void SetSelectedKeyValue(object _value)
        {
            if (SelectedKey == null)
                SelectedKey = new WACParameter();
            SelectedKey.ParmValue = _value;
            SelectedKey.ParmType = WACParameter.ParameterType.SelectedKey;
            SelectedKey.ParmName = PrimaryKey.ParmName;
        }
        public void SetSelectedKeyValue(string _keyName, object _value)
        {
            if (SelectedKey == null)
                SelectedKey = new WACParameter(_keyName, _value, WACParameter.ParameterType.SelectedKey);
            else
            {
                SelectedKey.ParmValue = _value;
                SelectedKey.ParmType = WACParameter.ParameterType.SelectedKey;
                SelectedKey.ParmName = PrimaryKey.ParmName;
            }
        }
        public void SetMasterKeyValue(KeyGetterDelegate _getKey)
        {
            // sets master record key
            MasterKey.ParmValue = _getKey(VList);
        }
        public void SetMasterKeyValue(object _value)
        {
            // sets master record key
            MasterKey.ParmValue = _value;
        }
        public void SetPrimaryKeyValue(KeyGetterDelegate _getKey)
        {
            // sets primary key
            PrimaryKey.ParmValue = _getKey(VList);
        }
        public void SetPrimaryKeyValue(object _value)
        {
            // sets primary key
            if (PrimaryKey == null)
                PrimaryKey = new WACParameter("", _value, WACParameter.ParameterType.PrimaryKey);
            else
                PrimaryKey.ParmValue = _value;
        }
        public void SetPrimaryKeyValue(string _keyName, object _value)
        {
            // sets primary key
            if (PrimaryKey == null)
                PrimaryKey = new WACParameter(_keyName, _value, WACParameter.ParameterType.PrimaryKey);
            else
                PrimaryKey.ParmValue = _value;
        }
        public void GetFullItemList(UnfilteredListGetterDelegate _getList)
        {
            // gets list for GridView datasource
            VList = _getList();
        }
        public void GetFilteredItemList(List<WACParameter> parms, FilteredListGetterDelegate _getList)
        {
            // gets list for GridView datasource
            VList = _getList(parms);
        }
        public IList GetSortedList(SortedIListDelegate _sort, string sortExp, string sortDir)
        {
            return _sort(VList, sortExp, sortDir);
        }
        private IList _getNewSingleItemList(List<WACParameter> parms, ItemGetterDelegate _getItem)
        {
            if (VList != null)
                FList = _getItem(parms, VList);
            if (FList != null && VList == null)
            {
                Type itemType = FList[0].GetType();
                Type listType = typeof(List<>).MakeGenericType(itemType);
                VList = (System.Collections.IList)Activator.CreateInstance(listType);
                VList.Add(FList[0]);
            }
            return FList;
        }
        public IList GetSingleItemList(List<WACParameter> parms, ItemGetterDelegate _getItem)
        {
            WACParameter wp = WACParameter.GetSelectedKey(parms);
            if (FList == null || FList.Count < 1)
                FList = _getNewSingleItemList(parms, _getItem);
            else if (!this.SelectedKey.Equals(wp))
                FList = _getItem(parms, VList);
            this.SelectedKey = wp;
            return FList;
        }
        public IList GetSingleItemList()
        {
            return FList;
        }
        public object GetCurrentItem()
        {
            try
            {
                return FList[0];
            }
            catch (Exception)
            {
                WACAlert.Show("Current Item not set", 0);
            }
            return null;
        }
        public void SetCurrentItem<T>(object item, PKGetter<T> _pkGetter)
        {
            if (item != null)
            {
                FList = new List<T>();
                FList.Add((T)item);
                InsertItemInViewList<T>(item);
                PrimaryKey = _pkGetter((T)item);
                SetSelectedKeyValue(PrimaryKey.ParmValue);
            }
            else
            {
                FList = null;
                SetSelectedKeyValue(null);
            }
        }
        public void AddItemToViewList<T>(object item)
        {
            try
            {
                if (VList == null)
                    VList = new List<T>();
                VList.Add((T)item);
            }
            catch (Exception)
            {
                WACAlert.Show("Error in MasterDetailDataObject.AddItemToList", 0);
            }
        }
        public void RemoveItemFromViewlist<T>(object item)
        {
            if (VList != null)
            {
                RemovedAt = VList.IndexOf((T)item);
                if (RemovedAt > -1)
                    VList.RemoveAt(RemovedAt);
            }
            if (FList != null && FList.Count > 0)
                FList.Clear();
        }
        public void InsertItemInViewList<T>(object item)
        {
            if (VList != null)
            {
                try
                {
                    VList.Insert(RemovedAt, (T)item);
                }
                catch (ArgumentOutOfRangeException)
                {
                    VList.Add((T)item);
                }
                RemovedAt = -1;
            }
            else
                AddItemToViewList<T>(item);
        }
       
        public int RowCount()
        {
            if (VList != null)
                return VList.Count;
            else
                return 0;
        }
        public void Dispose()
        {
            VList.Clear();
            FList.Clear();
            VList = null;
            FList = null;
        }
    }
}
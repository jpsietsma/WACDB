using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WAC_DataObjects;
using WAC_Event;
using WAC_Exceptions;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for AbstractDDLDataProvider
    /// </summary>
    public abstract class DDLDataProvider
    {
        public abstract IList<DDLListItem> GetList(List<WACParameter> parms);
        public abstract string GetSelected(object item);
        public DDLDataProvider() { }
        
        public IList<DDLListItem> GetDDLList(List<WACParameter> parms, object item)
        {
            try
            {
                IList<DDLListItem> _items = null;
                _items = GetList(parms);
                string selectedValue = GetSelected(item);
                if (!string.IsNullOrEmpty(selectedValue))
                {
                    foreach (DDLListItem ddls in _items)
                    {
                        if (selectedValue.Contains(ddls.DataValueField))
                        {
                            ddls.SelectedValue = true;
                            break;
                        }
                    }
                }
                return _items;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading DDL list. " + ex.Message);
            }
        }

        public string PropertyValue<T>(object o, string _propName)
        {
            Object value = null;
            if (o != null)
            {
                PropertyInfo p = typeof(T).GetProperty(_propName);
                Type pT = p.PropertyType;
                value = p.GetValue(o, null);
            }
            if (value == null)
                return string.Empty;
            else
                return string.IsNullOrEmpty(value.ToString()) ? string.Empty : value.ToString();
        }
    }
}
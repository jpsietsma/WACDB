using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

/// <summary>
/// Summary description for WACParameter
/// </summary>
namespace WAC_DataObjects
{
    [Serializable]
    public class WACParameter : IEquatable<WACParameter>
    {
        public string ParmName { get; set; }
        public object ParmValue { get; set; }
        public ParameterType ParmType { get; set; }
       
        public enum ParameterType { MasterKey, PrimaryKey, DataKeyName, SelectedKey, DataProvider, QueryString, ListState, FilterState, 
            FormState, ControlState, Visibility, RowCount, PickerListType, AssociationType, Navigate, Property, ListPage, ListSort, Close, NotKey,
            Expanded, SectorCode}

        public WACParameter() { }

        public WACParameter(string _name, object o)
        {
            ParmName = _name;
            ParmValue = o;
            ParmType = ParameterType.NotKey;  
        }
        public WACParameter(string _name, object o, ParameterType pType)
        {
            ParmName = _name;
            ParmValue = o;
            ParmType = pType;
        }
        public static WACParameter GetParameter(List<WACParameter> parms, string _parmName)
        {
             try
             {
                 var a = parms.Where(w => w.ParmName.Equals(_parmName, StringComparison.InvariantCultureIgnoreCase)).Select(s => s);
                 return a.First<WACParameter>();
             }              
            catch (Exception)
            {
                return null;
            }
        }
        public static WACParameter GetParameter(List<WACParameter> parms, WACParameter.ParameterType pType)
        {
            try
            {
                var a = parms.Where(w => w.ParmType == pType).Select(s => s);
                return a.First<WACParameter>();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static object GetParameterValue(List<WACParameter> parms, string _parmName)
        {
            try
            {
                //var a = parms.Where(w => w.ParmName.Equals(_parmName, StringComparison.InvariantCultureIgnoreCase)).Select(s =>
                //new WACParameter(s.ParmName, s.ParmValue));
                return (object)GetParameter(parms,_parmName).ParmValue;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public static object GetParameterValue(List<WACParameter> parms, ParameterType _keyType)
        {
            try
            {
                var a = parms.Where(w => w.ParmType == _keyType).Select(s =>
                new WACParameter(s.ParmName, s.ParmValue));
                return (object)a.First<WACParameter>().ParmValue;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static WACParameter GetMasterKey(List<WACParameter> parms)
        {
            try
            {
                var a = parms.Where(w => w.ParmType == ParameterType.MasterKey).Select(s =>
                new WACParameter(s.ParmName, s.ParmValue, s.ParmType));
                return a.First<WACParameter>() as WACParameter;
            }
            catch (Exception)
            {
                return null;
            }           
        }
        public static WACParameter GetPrimaryKey(List<WACParameter> parms)
        {
            try
            {
                var a = parms.Where(w => w.ParmType == ParameterType.PrimaryKey).Select(s =>
                new WACParameter(s.ParmName, s.ParmValue, s.ParmType));
                return a.First<WACParameter>() as WACParameter;
            }
            catch (Exception)
            {
                return null;
            }        
        }
        public static WACParameter GetSelectedKey(List<WACParameter> parms)
        {
            try
            {
                var a = parms.Where(w => w.ParmType == ParameterType.SelectedKey).Select(s =>
                new WACParameter(s.ParmName, s.ParmValue, s.ParmType));
                return a.First<WACParameter>() as WACParameter;
            }
            catch (Exception)
            {
                return null;
            }          
        }
        public static void RemoveAllButParameterType(List<WACParameter> parms, WACParameter.ParameterType pType)
        {
            WACParameter wp = RemoveParameterType(parms, pType);
            parms.Clear();
            if (wp != null)
                parms.Add(wp);
        }
        public static WACParameter RemoveParameterType(List<WACParameter> parms, WACParameter.ParameterType pType)
        {
            WACParameter p = null;
            try
            {
                p = WACParameter.GetParameter(parms, pType);
                if (p != null)
                {
                    int at = parms.IndexOf(p);
                    if (at > -1)
                        parms.RemoveAt(at);
                }
            }
            catch (Exception) { }
            return p;
        }
        public bool Equals(WACParameter other)
        {
            if (other == null)
                return false;
            return (ParmType == other.ParmType && ParmName == other.ParmName && ParmValue == other.ParmValue);
        }
        public WACParameter Clone()
        {
            return (WACParameter)this.MemberwiseClone();
        }
    }
   
}
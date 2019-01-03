using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Extensions;
using WAC_Extensions;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Concurrent;
/// <summary>
/// Summary description for WAC_DataProvider
/// </summary>
namespace WAC_DataProviders
{
    public abstract class WACDataProvider
    {
        public delegate Func<T, bool> WhereFunctionDelegate<T>(List<WACParameter> parms);
        public abstract IList GetList();
        public abstract IList GetFilteredList(List<WACParameter> parms);
        public abstract IList GetItem(List<WACParameter> parms, IList list);
        public abstract IList GetNewItem();
        public abstract object PrimaryKeyValue(IList list);
        public abstract string PrimaryKeyName();
        public abstract IList SortedList(IList _iList, string sortExpression, string sortDirection);

        public bool PrimaryKeyVerify(ref List<WACParameter> parms)
        {
            Object pk = null;
            WACParameter wp = null;
            if (parms.Count() < 1)
                return false;
            try
            {
                // get pk parameter based on parameter type
                wp = WACParameter.GetPrimaryKey(parms);
                if (wp == null)
                    return false;
                // if pk parameter exists, make sure the pk property name is set in the parameter
                if (string.IsNullOrEmpty(wp.ParmName))
                {
                    parms.Remove(wp);
                    wp.ParmName = PrimaryKeyName();
                    parms.Add(wp);
                }
                // verify the pk parameter has a value
                pk = WACParameter.GetParameterValue(parms, PrimaryKeyName());
                if (pk == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message + " in WACDataProvider.PrimaryKeyVerify()", 0);
                return false;
            }           
        }
        private bool IsProperty(WACParameter parm)
        {
            return parm.ParmType == WACParameter.ParameterType.Property ? true :
                parm.ParmType == WACParameter.ParameterType.PrimaryKey ? true :
                parm.ParmType == WACParameter.ParameterType.SelectedKey ? true :
                parm.ParmType == WACParameter.ParameterType.MasterKey ? true : false;          
        }
        public Func<T, bool> FuncWhere<T>(List<WACParameter> parms)
        {
            List<Expression<Func<T, bool>>> filters = new List<Expression<Func<T, bool>>>();
            ParameterExpression parmExp = Expression.Parameter(typeof(T), "w");
            foreach (WACParameter property in parms)
            {
                if (!IsProperty(property))
                    continue;
                string _propName = property.ParmName;
                object _value = property.ParmValue;
                ExpressionType operation;
                if (!operationMap.TryGetValue(FilterOperation.Equal, out operation))
                    throw new ArgumentOutOfRangeException("FilterOperation", FilterOperation.Equal, "Invalid filter operation");
                PropertyInfo p = typeof(T).GetProperty(_propName);
                Type pT = p.PropertyType;
                object value = null;
                if (!_value.GetType().Equals(pT))
                    value = ConvertToType(_value, pT);
                else
                    value = _value;
                var right = Expression.Constant(value, pT);
                var left = Expression.PropertyOrField(parmExp, _propName);
                var whereEqual = Expression.MakeBinary(operation, left, right);
                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(whereEqual, parmExp);
                filters.Add(predicate);
            }
            if (filters.Count < 1)
                return null;
            Expression<Func<T, bool>> f = filters.ElementAt(0);
            filters.RemoveAt(0);
            foreach (Expression<Func<T, bool>> _where in filters)
            {
                // inclusive OR
                f = Expression.Lambda<Func<T, bool>>(Expression.OrElse(
                    // exclusive AND
                    //f = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(
                    ((LambdaExpression)f).Body, ((LambdaExpression)_where).Body), parmExp);
            }
            return f.Compile();
        }

        // to restrict the allowable range of operations
        public enum FilterOperation
        {
            Equal,
            NotEqual,
            LessThan,
            LessThanOrEqual,
            GreaterThan,
            GreaterThanOrEqual,
        }
        // we could have used reflection here instead since they have the same names
        static Dictionary<FilterOperation, ExpressionType> operationMap = new Dictionary<FilterOperation, ExpressionType>
        {
            { FilterOperation.Equal,                ExpressionType.Equal },
            { FilterOperation.NotEqual,             ExpressionType.NotEqual },
            { FilterOperation.LessThan,             ExpressionType.LessThan },
            { FilterOperation.LessThanOrEqual,      ExpressionType.LessThanOrEqual },
            { FilterOperation.GreaterThan,          ExpressionType.GreaterThan },
            { FilterOperation.GreaterThanOrEqual,   ExpressionType.GreaterThanOrEqual },
        };

        private object ConvertToType(object value, Type type)
        {
            Type baseType = null;
            if (value.GetType().Equals(type))
                return value;
            if (type.Equals(typeof(string)))
                return Convert.ToString(value);
            if (type.Equals(typeof(Int32)))
                return Convert.ToInt32(value);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                baseType = Nullable.GetUnderlyingType(type);
            {
                int? i = null;
                if (baseType.Equals(typeof(Int32)))
                    i = Convert.ToInt32(value);
                return i;
            }
        }
        
    }
}
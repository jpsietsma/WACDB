using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Connectors;
using WAC_Exceptions;
using System.Collections;
using System.Web.UI;
using System.Text;


/// <summary>
/// Summary description for WACValidator
/// </summary>
namespace WAC_Validators
{
    public abstract class WACValidator 
    {
        public delegate T InsertValidateDelegate<T>(List<WACParameter> parms);
        public delegate T UpdateValidateDelegate<T>(List<WACParameter> parms, T originalItem);

        public string ValidateString(object objToValidate, string parameterName, bool required)
        {
            string s = objToValidate as string;
            if (required && string.IsNullOrEmpty(s))
                throw new WACEX_ValidationException(ValidationErrorMessage(parameterName, "string", null, required));
            else if (string.IsNullOrEmpty(s))
                return null;
            else
                return s;
        }
        public DateTime? ValidateDate(object objToValidate, string parameterName, bool required)
        {
            DateTime? dt = null;           
            try
            {
                dt = Convert.ToDateTime(objToValidate);
            }
            catch (Exception ex)
            {
                if (!required)
                {
                    WACAlert.Show(ValidationErrorMessage(parameterName, "Date", null), 0);
                    return null;
                }
                else
                    throw new WACEX_ValidationException(ValidationErrorMessage(parameterName, "Date", null, required) + ex.Message);
            }         
            return dt;
            // other tests, max-min year etc??
        }
        public Decimal ValidateDecimal(object objToValidate, string parameterName, bool required)
        {
            Decimal d = 0;
            try
            {
                d = Convert.ToDecimal(objToValidate);
            }
            catch (Exception ex)
            {
                if (!required)
                {
                    WACAlert.Show(ValidationErrorMessage(parameterName, "Decimal", null), 0);
                }
                else
                    throw new WACEX_ValidationException(ValidationErrorMessage(parameterName, "Decimal", null, required) + ex.Message);
            }
            return d;
        }
        public Int32 ValidateInt(object objToValidate, string parameterName, bool required, bool silent)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(objToValidate);
            }
            catch (Exception ex)
            {
                if (!required)
                {
                    if (!silent)
                        WACAlert.Show(ValidationErrorMessage(parameterName, "Int", null), 0);
                }
                else
                    throw new WACEX_ValidationException(ValidationErrorMessage(parameterName, "Int", null, required) + ex.Message);
            }
            return i;
        }
        
        public Double ValidateDouble(object objToValidate, string parameterName, bool required)
        {
            double d = 0.0;
            try
            {
                d = Convert.ToDouble(objToValidate);
            }
            catch (Exception ex)
            {
                if (!required)
                {
                    WACAlert.Show(ValidationErrorMessage(parameterName, "Double", null), 0);
                }
                else
                    throw new WACEX_ValidationException(ValidationErrorMessage(parameterName, "Double", null, required) + ex.Message);
            }
            return d;
        }
        public bool ValidateBool(object objToValidate, string parameterName, bool required)
        {
            bool b = false;
            try
            {
                b = Convert.ToBoolean(objToValidate);
            }
            catch (Exception ex)
            {
                if (!required)
                {
                    WACAlert.Show(ValidationErrorMessage(parameterName, "Boolean", null), 0);
                }
                else
                    throw new WACEX_ValidationException(ValidationErrorMessage(parameterName, "Boolean", null, required) + ex.Message);
            }
            return b;
        }
        private string ValidationErrorMessage(string parmName, string valError, string exMessage)
        {
            return ValidationErrorMessage(parmName, valError, exMessage, false);
        }
        private string ValidationErrorMessage(string parmName, string valError, string exMessage, bool required)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(parmName))
            {
                sb.Append("[");
                sb.Append(parmName);
                sb.Append("] ");
            }
            if (!string.IsNullOrEmpty(valError))
            {
                if (required)
                    sb.Append(" is Required and is ");   
                sb.Append("Invalid or missing ");
                sb.Append(valError);
            }
            if (!string.IsNullOrEmpty(exMessage))
            {
                sb.Append(" ");
                sb.Append(exMessage);
            }
            return sb.ToString();
        }
    }
}
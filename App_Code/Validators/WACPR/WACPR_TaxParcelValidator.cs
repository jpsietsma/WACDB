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

namespace WAC_Validators
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelValidator
    /// </summary>
    public class WACPR_TaxParcelValidator : WACValidator
    {
        public WACPR_TaxParcelValidator() { }

        public TaxParcel ValidateInsert(List<WACParameter> parms)
        {
            TaxParcel t = new TaxParcel();
            foreach (WACParameter parm in parms)
            {
                switch (parm.ParmName)
                {
                    case "swis":
                        t.Swis = ValidateSwis(parm.ParmValue, parm.ParmName, true);
                        break;
                    case "printKey":
                        t.taxParcelID = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    case "note":
                        t.note = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "SBL":
                        t.SBL = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "created_by":
                        t.created_by = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    default:
                        break;
                }
            }
            return t;
        }
        public TaxParcel ValidateUpdate(List<WACParameter> parms, TaxParcel original)
        {
            TaxParcel t = original.Clone();
            foreach (WACParameter parm in parms)
            {
                switch (parm.ParmName)
                {
                    case "pk_taxParcel":
                        t.pk_taxParcel = ValidateInt(parm.ParmValue, parm.ParmName, true, true);
                        break;
                    case "note":
                        t.note = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "modified_by":
                        t.created_by = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    default:
                        break;
                }
            }
            return t;
        }
        private SWIS ValidateSwis(object objToValidate, string parameterName, bool required)
        {
            string swisCode = ValidateString(objToValidate, parameterName, required);
            return new SWIS(swisCode, string.Empty, string.Empty);
        }
    }
}
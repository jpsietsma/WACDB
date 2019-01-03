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
    /// Summary description for WACPR_TaxParcelOwnerValidator
    /// </summary>
    public class WACPR_TaxParcelOwnerValidator : WACValidator
    {
        public WACPR_TaxParcelOwnerValidator() { }

        public TaxParcelOwner ValidateUpdate(List<WACParameter> parms, TaxParcelOwner originalItem)
        {
            TaxParcelOwner e = originalItem.Clone();
            foreach (WACParameter parm in parms.Where(w => w.ParmType == WACParameter.ParameterType.Property).Select(s => s))
            {
                switch (parm.ParmName)
                {
                    case "pk_taxParcelOwner":
                        e.pk_taxParcelOwner = ValidateInt(parm.ParmValue, parm.ParmName, true, false);
                        break;
                    case "fk_participant":                        
                        e.fk_participant = ValidateInt(parm.ParmValue, parm.ParmName, true, false);
                        break;
                    case "participant":
                        e.fullname_LF_dnd = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "active":
                        e.active = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "note":
                        e.note = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "modified_by":
                        e.modified_by = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    default:
                        break;
                }
            }
            return e;
        }
        public TaxParcelOwner ValidateInsert(List<WACParameter> parms)
        {
            TaxParcelOwner e = new TaxParcelOwner();
            foreach (WACParameter parm in parms.Where(w => w.ParmType == WACParameter.ParameterType.Property).Select(s => s))
            {
                switch (parm.ParmName)
                {
                    case "fk_taxParcel":
                        e.fk_taxParcel = ValidateInt(parm.ParmValue, parm.ParmName, true, false);
                        break;
                    case "fk_participant":
                        e.fk_participant = ValidateInt(parm.ParmValue, parm.ParmName, true, false);
                        break;
                    case "active":
                        e.active = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "note":
                        e.note = ValidateString(parm.ParmValue, parm.ParmName, false);
                        break;
                    case "created_by":
                        e.created_by = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    default:
                        break;
                }
            }
            return e;
        }

    }

   
}
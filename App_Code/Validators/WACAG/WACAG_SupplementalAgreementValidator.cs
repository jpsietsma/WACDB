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
    /// Summary description for WACAG_SupplementalAgreementValidator
    /// </summary>
    public class WACAG_SupplementalAgreementValidator : WACValidator
    {
        public WACAG_SupplementalAgreementValidator()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SupplementalAgreement ValidateInsert(List<WACParameter> parms)
        {
            SupplementalAgreement sa = new SupplementalAgreement();
            int val;
            foreach (WACParameter parm in parms)
            {
                switch (parm.ParmName)
                {
                    case "agreement_date":
                        sa.agreement_date = ValidateDate(parm.ParmValue, parm.ParmName, true);
                        break;
                    case "swis":
                        sa.swis = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    case "printKey":
                        sa.printKey = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    case "created_by":
                        sa.created_by = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    default:
                        break;
                }
            }
            return sa;
        }
    }
}
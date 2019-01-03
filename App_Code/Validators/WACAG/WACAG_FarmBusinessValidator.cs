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
    /// Summary description for WACAG_FarmBusinessValidator
    /// </summary>
    public class WACAG_FarmBusinessValidator : WACValidator
    {
        public WACAG_FarmBusinessValidator() { }

        public FarmBusiness ValidateInsert(List<WACParameter> parms)
        {
            FarmBusiness f = new FarmBusiness();
            int val;
            foreach (WACParameter parm in parms)
            {
                switch (parm.ParmName)
                {
                    case "swis":
                        f.swis = ValidateString(parm.ParmValue,parm.ParmName, false);
                        break;
                    case "printKey":
                        f.printKey = ValidateString(parm.ParmValue,parm.ParmName, false);
                        break;
                    case "fk_participantOwner":
                        val = ValidateInt(parm.ParmValue, parm.ParmName, false, true);
                        if (val == 0)
                            f.fk_participantOwner = null;
                        else
                            f.fk_participantOwner = val;
                        break;
                    case "fk_participantOperator":
                        val = ValidateInt(parm.ParmValue,parm.ParmName, false, true);
                        if (val == 0)
                            f.fk_participantOperator = null;
                        else
                            f.fk_participantOperator = val;
                        break;
                    case "farm_name":
                        f.farm_name = ValidateString(parm.ParmValue,parm.ParmName, false);
                        break;
                    case "fk_programWAC_code":
                        f.fk_programWAC_code = ValidateString(parm.ParmValue,parm.ParmName, true);
                        break;         
                    case "fk_farmSize_code":
                        string programWAC_code = WACParameter.GetParameterValue(parms, "fk_programWAC_code") as string;
                        if (programWAC_code.Contains("EOH"))
                            f.fk_farmSize_code = ValidateString(parm.ParmValue, parm.ParmName, false);
                        else
                            f.fk_farmSize_code = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;  
                    case "fk_basin_code":
                        f.fk_basin_code = ValidateString(parm.ParmValue,parm.ParmName, true);
                        break;                  
                    case "sold_farm":
                        val = ValidateInt(parm.ParmValue, parm.ParmName, false, true);
                        if (val == 0)
                            f.sold_farm = null;
                        else
                            f.sold_farm = val;
                        break;
                    case "GenerateID":
                        f.GenerateID = ValidateString(parm.ParmValue, parm.ParmName, true);
                        break;
                    case "created_by":
                        f.created_by = ValidateString(parm.ParmValue,parm.ParmName, true);
                        break;
                    default:
                        break;
                }
            }
            return f;
        }
    }
}
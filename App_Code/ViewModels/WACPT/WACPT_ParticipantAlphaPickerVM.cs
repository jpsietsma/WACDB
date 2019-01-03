using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_Services;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Validators;

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACPT_ParticipantAlphaPickerVM
    /// </summary>
    public class WACPT_ParticipantAlphaPickerVM : WACUtilityControlViewModel
    {
        public WACPT_ParticipantAlphaPickerVM() { }

        public override string CustomString(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        public override void CustomAction(List<WACParameter> parms)
        {
            Control parent = WACParameter.GetParameterValue(parms, "ContainingControl") as Control;
            base.BindControlDDLs(parent, parms);
        }

        public override void ContentStateChanged(Control _control, Enum e)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_UserControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACUT_ExpressNavigateVM
    /// </summary>
    public class WACUT_ExpressNavigateVM : WACViewModel
    {
        public WACUT_ExpressNavigateVM() { }

        public override void ContentStateChanged(Control _control, Enum e)
        {
            return;
        }
    }
}
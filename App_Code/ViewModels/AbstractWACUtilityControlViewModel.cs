using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Validators;
using AjaxControlToolkit;

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACUtilityControlViewModel
    /// </summary>
    public abstract class WACUtilityControlViewModel : WACViewModel
    {
        public bool Toggle { get; set; }
        public abstract void CustomAction(List<WACParameter> parms);
        public abstract string CustomString(List<WACParameter> parms);
    
    }
}
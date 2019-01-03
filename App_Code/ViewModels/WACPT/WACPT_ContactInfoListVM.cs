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
    /// Summary description for WACPT_ContactInfoListVM
    /// </summary>
    public class WACPT_ContactInfoListVM : WACListViewModel
    {
        public WACPT_ContactInfoListVM() { }



        public override void CustomAction(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        public override string CustomString(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }
    }
}
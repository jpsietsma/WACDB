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
/// <summary>
/// Summary description for WACUT_AssociationsVM
/// </summary>
namespace WAC_ViewModels
{
    public class WACUT_AssociationsVM : WACListViewModel
    {
        public WACUT_AssociationsVM() { }


        public override string CustomString(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        public override void CustomAction(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

    }
}
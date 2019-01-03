using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Connectors;
using WAC_Exceptions;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;
/// <summary>
/// Summary description for WACPT_OrganizationPageVM
/// </summary>
namespace WAC_ViewModels
{
    public class WACPT_OrganizationPageVM : WACTabControlViewModel
    {
        public WACPT_OrganizationPageVM() { }


        public override void Insert(Control c, List<WACParameter> args)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Control c, List<WACParameter> args)
        {
            throw new NotImplementedException();
        }   

        public override void ContentStateChanged(Control _control, Enum e)
        {
            throw new NotImplementedException();
        }
    }
}
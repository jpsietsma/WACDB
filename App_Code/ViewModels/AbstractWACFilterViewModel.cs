using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI;
using WAC_Validators;
using WAC_UserControls;
/// <summary>
/// Summary description for WACFilterViewModel
/// </summary>
namespace WAC_ViewModels
{
    public abstract class WACFilterViewModel : WACViewModel
    {
        public WACFilterViewModel() { }

        public override void ContentStateChanged(Control filter, Enum state)
        {
            ServiceRequest sr = new ServiceRequest(filter);
            sr.ParmList.Add(new WACParameter(null, state, WACParameter.ParameterType.FilterState));
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContentStateChanged;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public void InitializeFilterDropDownLists(WACFilterControl fc, List<WACParameter> parms)
        {
            foreach (string ddlID in DDLBinders.Keys)
            {
                DropDownList ddl = fc.FindControl(ddlID) as DropDownList;
                DDLDataObject ddlDO = DDLBinders[ddlID];
                if (ddl != null && ddlDO != null)
                    ddlDO.DataBindDDL(ddl, parms, null);
            }
            //ContentStateChanged(fc, WACFilterControl.FilterState.Initialized);
        }
        public void ResetReloadFilterDropDownLists(WACFilterControl fc, List<WACParameter> parms)
        {
            foreach (string ddlID in DDLBinders.Keys)
            {
                DropDownList ddl = fc.FindControl(ddlID) as DropDownList;
                DDLDataObject ddlDO = DDLBinders[ddlID];
                if (ddl != null && ddlDO != null)
                {
                    ddl.Items.Clear();
                    ddlDO.DataBindDDL(ddl, parms, null);
                }
            }
            ContentStateChanged(fc, WACFilterControl.FilterState.Reset);
        }
    }
}
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
using WAC_Containers;

/// <summary>
/// Summary description for AbstractWACFilterControl
/// </summary>
namespace WAC_UserControls
{
    public abstract class WACFilterControl : WACControl
    {
        public enum FilterState { Unfiltered, Filtered, Reset, Initialized, Stale }
        public abstract List<WACParameter> BuildFilter();
        public FilterState CurrentState
        {
            get { return ViewState[ClientID + "_FilterState"] != null ? (FilterState)ViewState[ClientID + "_FilterState"] : FilterState.Stale; }
            set { ViewState[ClientID + "_FilterState"] = value; }
        }
        public void ResetReload()
        {
            sReq.ServiceFor = this;
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.ResetReloadFilterDDLs;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
  
        public void LoadAll()
        {
            List<WACParameter> eventParms = new List<WACParameter>();
            eventParms.Add(new WACParameter(string.Empty, FilterState.Unfiltered, WACParameter.ParameterType.FilterState));
            StateChanged(eventParms);
        }
        public void FilterList()
        {
            if (sReq.ParmList == null || sReq.ParmList.Count < 1)
                ResetControl();
            else
            {
                sReq.ParmList.Add(new WACParameter(string.Empty, FilterState.Filtered, WACParameter.ParameterType.FilterState));
                StateChanged(sReq.ParmList);
            }
        }
      
        public override void InitControl(List<WACParameter> parms)
        {
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.InitFilterDDLs;
            sReq.ServiceFor = this;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }

    }
}
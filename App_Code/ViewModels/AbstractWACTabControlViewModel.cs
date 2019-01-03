using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Collections;
using System.Web.UI;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Validators;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;

/// <summary>
/// Summary description for WACTabControlViewModel
/// </summary>
namespace WAC_ViewModels
{

    public abstract class WACTabControlViewModel : WACViewModel
    {
        public enum TabState { Uninitialized, Initialized, Open, Reset };
        public TabState MyTabState { get; set; }
        public WACValidator Validator { get; set; }
        public abstract void Insert(Control c, List<WACParameter> args);
        public abstract void Delete(Control c, List<WACParameter> args);
        
        public void SetPrimaryKey(List<WACParameter> parms)
        {
            if (ListSource != null)
                ListSource.MasterKey.ParmValue = WACParameter.GetMasterKey(parms).ParmValue;
        }
        public void ClearPrimaryKey()
        {
            if (ListSource != null)
                ListSource.MasterKey.ParmValue = null;
        }
        public void RefreshContainedGridView(Control c)
        {
            ServiceRequest sReq = new ServiceRequest(c);
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.RefreshGridView;
            sReq.ParmList.Clear();
            sReq.ParmList.Add(ListSource.PrimaryKey);
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_ViewModels;
using WAC_Event;
using WAC_Connectors;
using WAC_Services;
using WAC_UserControls;
using WAC_Containers;
using System.Collections;

public partial class Participant_WACPT_ContactInfoList : WACListControl, IWACIndependentControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
            ReBindControl();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenListView;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    public override void InitControl(List<WACParameter> parms)
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ListViewList;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }

    public override void ReBindControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindList;
        ServiceFactory.Instance.ServiceRequest(sReq);

    }
   
    public override void UpdateControl(List<WACParameter> parms)
    {
        //WACParameter wp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.ListState);
        //if (wp != null && (WACListControl.ListState)wp.ParmValue == WACListControl.ListState.ListFull)
        //{
        //    sReq.ServiceFor = this;
        //    sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenListView;
        //    ServiceFactory.Instance.ServiceRequest(sReq);
        //}  
    }

    
    public override void ResetControl()
    {
        throw new NotImplementedException();
    }


    public override void CloseControl()
    {
        throw new NotImplementedException();
    }

 
}
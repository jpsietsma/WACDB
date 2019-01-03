using System;
using System.Collections;
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

public partial class Property_WACPR_TaxParcelOwnerGrid : WACGridControl, IWACContainer, IWACDependentControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
        ReBindControl();
    }

    protected void lb_gvOpenView_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        WACParameter wp = new WACParameter("pk_taxParcelOwner", lb.CommandArgument, WACParameter.ParameterType.SelectedKey);
        WACParameter state = new WACParameter(string.Empty, ListState.SelectionMade, WACParameter.ParameterType.ListState);
        List<WACParameter> parms = new List<WACParameter>();
        parms.Add(wp);
        parms.Add(state);
        StateChanged(parms);
    }
  
    public override void UpdateControl(List<WACParameter> parms)
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenGridView;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }

    public override void ResetControl()
    {
        throw new NotImplementedException();
    }
   
    public override void InitControl(List<WACParameter> parms)
    {
        base.LoadList(parms);
        UpdateControl(parms);
    }
    public List<WACParameter> GetContents()
    {
        List<WACParameter> parms = new List<WACParameter>();
        parms.Add(new WACParameter(string.Empty, RowCount, WACParameter.ParameterType.RowCount));
        return parms;
    }

    public void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    public override void CloseControl()
    {
        
    }

    public override void ReBindControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindGrid;
        ServiceFactory.Instance.ServiceRequest(sReq);
        
    }


    //public void AdjustContentVisibility(List<WACControl> ContainedControls, WACFormControl form)
    //{
    //    throw new NotImplementedException();
    //}

    //event EventHandler<UserControlResultEventArgs> IWACContainer.ContentStateChanged
    //{
    //    add { throw new NotImplementedException(); }
    //    remove { throw new NotImplementedException(); }
    //}

    void IWACContainer.InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    void IWACContainer.UpdatePanelUpdate()
    {
        throw new NotImplementedException();
    }

  
}
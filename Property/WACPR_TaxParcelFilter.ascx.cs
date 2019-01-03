using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Connectors;
using WAC_Containers;
using WAC_Exceptions;
using WAC_Services;
using WAC_DataObjects;


public partial class Property_WACPR_TaxParcelFilter : WACFilterControl, IWACDependentControl, IWACContainer
{
    protected override void OnInit(EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
        ReBindControl();
        base.OnInit(e);
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        
        bool pageIsPostBack = Page.IsPostBack;
        bool asyncPostBack = ScriptManager.GetCurrent(Page).IsInAsyncPostBack;
        if (!Page.IsPostBack)
            base.InitControl(null);
    }   
    protected void lbReloadReset_Click(object sender, EventArgs e)
    {
        ResetControl();
    }
    protected void lbAll_Click(object sender, EventArgs e)
    {
        base.LoadAll();
    }

    protected void ddlTaxParcel_Search(object sender, EventArgs e)
    {
        sReq.ParmList = BuildFilter();
        base.FilterList();
    }
    public override List<WACParameter> BuildFilter()
    {       
        List<WACParameter> parms = new List<WACParameter>();
        if (!string.IsNullOrEmpty(ddlTaxParcelID.SelectedValue))
            parms.Add(new WACParameter("pk_taxParcel", ddlTaxParcelID.SelectedValue,WACParameter.ParameterType.Property));
        //if (!string.IsNullOrEmpty(ddlSBL.SelectedValue))
        //    parms.Add(new WACParameter("pk_taxParcel", ddlSBL.SelectedValue, WACParameter.ParameterType.Property));
        return parms;
    }
    protected void ParticipantAlphaPicker_Selected(object sender, UserControlResultEventArgs e)
    {
        sReq.ParmList = e.Parms;
        base.FilterList();
    }
    protected void PrintKeySearch_Clicked(object sender, UserControlResultEventArgs e)
    {
        sReq.ParmList = e.Parms;
        base.FilterList();
    }
    public override void InitControl(List<WACParameter> parms)
    {
        base.InitControl(parms);
    }
    public override void ResetControl()
    {
        base.ResetReload();
    }
    public override void ReBindControl()
    {
        ServiceRequest sr = new ServiceRequest(this);
        sr.ServiceFor = this;
        sr.ServiceRequested = ServiceFactory.ServiceTypes.ReBindDDLs;
        ServiceFactory.Instance.ServiceRequest(sr);
        sr.ServiceRequested = ServiceFactory.ServiceTypes.ReBindControls;
        ServiceFactory.Instance.ServiceRequest(sr);
        sr = null;
    }
    public override void UpdateControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }
    public override void CloseControl()
    {
        throw new NotImplementedException();
    }
    public void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }
    public List<WACParameter> GetContents()
    {
        throw new NotImplementedException();
    }

}
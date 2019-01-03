using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Containers;
using WAC_UserControls;
using WAC_DataObjects;
using WAC_Services;
using AjaxControlToolkit;
using System.Text;

public partial class Property_WACPR_TaxParcelOwnerContainer : WACGridFormContainer, IWACIndependentControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
        if (Page.IsPostBack)
            ReBindControl();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            ServiceRequest sr = new ServiceRequest(this);
            sr.ServiceFor = WACPR_TaxParcelOwnerForm; ;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContainerVisibility;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
    }
    public override void ResetControl()
    {
        throw new NotImplementedException();
    }
    protected void WACPR_TaxParcelOwnerGrid_ContentStateChanged(object sender, WAC_Event.UserControlResultEventArgs e)
    {       
        base.ContainedGridStateChanged(this, (WACGridControl)sender, WACPR_TaxParcelOwnerForm, e.Parms);
    }
    protected void WACPR_TaxParcelOwnerForm_ContentStateChanged(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        base.ContainedFormStateChanged(this, WACPR_TaxParcelOwnerGrid, (WACFormControl)sender, e.Parms);
    }
    protected void WACUT_AddNewItem_AddNewItem_Clicked(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        FormView fv = WACGlobal_Methods.FindControl<FormView>(WACPR_TaxParcelOwnerForm);
        if (fv != null)
            WACPR_TaxParcelOwnerForm.OpenAdd(fv);
    }
    public override void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }
    public override void InitControl(List<WACParameter> parms)
    {          
            WACParameter masterKey = WACParameter.GetSelectedKey(parms);
            masterKey.ParmType = WACParameter.ParameterType.MasterKey;
            parms.Add(masterKey);
            sReq.ServiceFor = this;
            sReq.ParmList = parms;
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.SetMasterKeyForContainer;
            ServiceFactory.Instance.ServiceRequest(sReq);
            WACPR_TaxParcelOwnerGrid.InitControl(parms);
            lblOwnerCount.Text = getCount();
    }
    public override void UpdateControl(List<WACParameter> parms)
    {
        lblOwnerCount.Text = getCount();
    }   
    public override void CloseControl()
    {
        sReq.ServiceFor = WACPR_TaxParcelOwnerGrid;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ClearGridView;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    private string getCount()
    {
        string rowCount;
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        sReq.ServiceFor = WACPR_TaxParcelOwnerGrid;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ListRowCount;
        ServiceFactory.Instance.ServiceRequest(sReq);
        rowCount = WACParameter.GetParameterValue(WACPR_TaxParcelOwnerGrid.GetContents(), WACParameter.ParameterType.RowCount).ToString();
        if (!string.IsNullOrEmpty(rowCount))
            sb.Append(rowCount);
        else
            sb.Append("?");
        sb.Append(")");
        return sb.ToString();
    }
    public override void ReBindControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindControls;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
}
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

public partial class Property_WACPR_TaxParcelPageContents : WACFilterGridFormContainer, IWACContainer
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            sReq = new ServiceRequest(this);
            base.RegisterAndConnect(this);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            ServiceRequest sr = new ServiceRequest(this);
            sr.ServiceFor = WACPR_TaxParcelForm;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContainerVisibility;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
    }
    protected void WACPR_TaxParcelFilter_FilterContentsChanged(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        WACFilterControl filter = (WACFilterControl)sender;
        base.ContainedFilterStateChanged(this, filter, WACPR_TaxParcelGrid, WACPR_TaxParcelForm, e.Parms);
    }
    protected void WACPR_TaxParcelGrid_GridContentsChanged(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        WACGridControl grid = (WACGridControl)sender;
        base.ContainedGridStateChanged(this, WACPR_TaxParcelFilter, grid, WACPR_TaxParcelForm, e.Parms);
    }
    protected void WACPR_TaxParcelForm_FormContentsChanged(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        WACFormControl form = (WACFormControl)sender;
        base.ContainedFormStateChanged(this, WACPR_TaxParcelFilter, WACPR_TaxParcelGrid, form, e.Parms);
    }
    protected void WACUT_AddNewItem_AddNewItem_Clicked(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        FormView fv = WACGlobal_Methods.FindControl<FormView>(WACPR_TaxParcelForm);
        if (fv != null)
            WACPR_TaxParcelForm.OpenAdd(fv);
    }
    public override void UpdateControl(List<WACParameter> parms)
    {
        sReq.ParmList = parms;
        sReq.ServiceFor = WACPR_TaxParcelGrid;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.FilteredGridViewList;
        ServiceFactory.Instance.ServiceRequest(sReq);   
    }

    public override void ResetControl()
    {
        throw new NotImplementedException();
    }

    public override void InitControls(List<WACParameter> parms)
    {
        
    }

    public override void InitControl(List<WACParameter> parms)
    {
        
    }

    public override void CloseControl()
    {
        throw new NotImplementedException();
    }


    public override void ReBindControl()
    {
        throw new NotImplementedException();
    }
}
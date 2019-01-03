using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using System.Text;
using WAC_DataObjects;

public partial class Property_WACPR_TaxParcelGrid : WACGridControl, IWACDependentControl
{

    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
        //if (!ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            ReBindControl();
    }
    
    protected void lb_gvOpenView_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        base.RowSelected(this, TaxParcel.PrimaryKeyName, lb.CommandArgument);
    }

    protected void gvTaxParcel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        base.PageIndexChanging(this, e.NewPageIndex);
    }

    protected void gvTaxParcel_Sorting(object sender, GridViewSortEventArgs e)
    {
        base.ListSorting(this, e.SortExpression);
    }
    public override void InitControl(List<WACParameter> parms)
    {
       
    }

    public override void UpdateControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    public override void ResetControl()
    {
        throw new NotImplementedException();
    }

    public override void CloseControl()
    {
        throw new NotImplementedException();
    }

    public override void ReBindControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindGrid;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
}
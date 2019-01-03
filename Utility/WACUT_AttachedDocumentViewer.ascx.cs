using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Services;
using WAC_DataObjects;
using WAC_Containers;
using WAC_DataProviders;
using System.Text;

public partial class Utility_WACUT_AttachedDocumentViewer : WACListControl, IWACIndependentControl
{
    public WACEnumerations.WACSectorCodes SectorCode { get; set; }
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bool asyncPostBack = ScriptManager.GetCurrent(Page).IsInAsyncPostBack;
        if (asyncPostBack)
            ReBindControl();    
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ltDocumentCount.Text = GetDocumentCount();
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenListView;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    public override void InitControl(List<WACParameter> parms)
    {
        parms.Add(new WACParameter(string.Empty, SectorCode, WACParameter.ParameterType.SectorCode));
        base.LoadList(parms);
    }
    public override void ReBindControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindList;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    public override void UpdateControl(List<WACParameter> parms)
    {
        WACParameter wp = WACParameter.RemoveParameterType(parms, WACParameter.ParameterType.ListState);
        CurrentState = (WACListControl.ListState)wp.ParmValue;
    }
    public override void ResetControl()
    {
        throw new NotImplementedException();
    }

    public override void CloseControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ClearListView;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }

    private string GetDocumentCount()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ListRowCount;
        ServiceFactory.Instance.ServiceRequest(sReq);
        if (!string.IsNullOrEmpty(RowCount))
            sb.Append(RowCount);
        else
            sb.Append("0");
        sb.Append(")");
        return sb.ToString();
    }
}
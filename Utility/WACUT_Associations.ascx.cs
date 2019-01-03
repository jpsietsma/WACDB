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

public partial class WACUT_Associations : WACListControl, IWACIndependentControl
{
    public WACEnumerations.AssociationTypes AssociationType { get; set; }

    #region Session and ViewState Properties
    public string ControlLabel
    {
        get { return ViewState[this.ID + "_Label"] == null ? "Associations" : ViewState[this.ID + "_Label"].ToString(); }
        set { ViewState[this.ID + "_Label"] = value; }
    }
    #endregion

    public string AssociationsLabel
    {
        get
        {
            switch (AssociationType)
            {
                case WACEnumerations.AssociationTypes.Participant:
                    return "Participant";
                case WACEnumerations.AssociationTypes.Property:
                    return "Property";
                case WACEnumerations.AssociationTypes.Organization:
                    return "Organization";
                case WACEnumerations.AssociationTypes.TaxParcel:
                    return "TaxParcel";
                default:
                    return null;
            }
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //bool pageIsPostBack = Page.IsPostBack;
        bool asyncPostBack = ScriptManager.GetCurrent(Page).IsInAsyncPostBack;
        if (asyncPostBack)
            ReBindControl();        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //if (Page.IsPostBack)
        //{
            lblAssociationCount.Text = GetAssociationCount();
            lblAssociations.Text = AssociationsLabel + " " + ControlLabel;
            sReq.ServiceFor = this;
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.OpenListView;
            ServiceFactory.Instance.ServiceRequest(sReq);
        //}
    }
    public override void InitControl(List<WACParameter> parms)
    {
        parms.Add(new WACParameter(string.Empty, AssociationType, WACParameter.ParameterType.AssociationType));
        base.LoadList(parms);
        lblAssociationCount.Text = GetAssociationCount();
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
    protected void lvItemCommand(object sender, CommandEventArgs e)
    {
        ListView lv = sender as ListView;
        string queryString = queryString = "pk=" + e.CommandArgument;
        string url = null;
        switch (e.CommandName)
        {
            case "dbo.easement":
                url = "~/Easements/WACEA_OverviewPage.aspx?";
                break;
            case "dbo.farmBusiness":
                url = "~/AG/WACAgriculture.aspx?";
                break;
            case "dbo.supplementalAgreement":
                url = "~/AG/WACAgriculture_SupplementalAgreements.aspx?";
                break;
            case "dbo.forestryFMP":
                //url = "~/Forestry/WACFO_FMP.aspx?";
                url = "~/Forestry/WACFO.aspx"; queryString = "";
                break;
            case "dbo.forestryBMP":
                //url = "~/Forestry/WACFO_BMP.aspx?";
                url = "~/Forestry/WACFO.aspx"; queryString = "";
                break;
            case "dbo.forestryMAP":
                //url = "~/Forestry/WACFO_MAP.aspx?";
                url = "~/Forestry/WACFO.aspx"; queryString = "";
                break;
            case "dbo.farmToMarket":
                url = "~/Market/WACFarm2Market.aspx?";
                break;
            case "dbo.organization":
                url = "~/Participant/WACPT_Organizations.aspx?";
                break;
            case "dbo.participant":
                url = "~/Participant/WACPT_Participants.aspx?";
                break;
            case "dbo.taxParcel":
                url = "~/Property/WACPR_TaxParcelPage.aspx?";
                break;
            default:
                break;
        }
        if (url != null)
            Response.Redirect(url + queryString);
    }
   
    private string GetAssociationCount()
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



   
}


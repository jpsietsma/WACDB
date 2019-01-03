using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Connectors;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Services;
using WAC_DataObjects;
using WAC_Containers;
using WAC_Event;

public partial class WACContact : WACPage
{
    public int iCount;
    public override string ID { get { return "WACPT_ContactInfoPage"; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            OpenDefaultDataView(null);
        }
    }


    public override void OpenDefaultDataView(List<WACParameter> parms)
    {
        WACListControl wlc = WACGlobal_Methods.FindControl<WACListControl>(this);
        wlc.InitControl(null);
    }
    protected void WACPT_ContactInfoList_ContentStateChanged(object sender, UserControlResultEventArgs e)
    {
        WACListControl wlc = (WACListControl)sender;
        wlc.UpdateControl(e.Parms);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Event;
using WAC_DataObjects;
using WAC_Connectors;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Exceptions;
using WAC_Extensions;
using WAC_Services;
using WAC_Containers;
using System.Collections.Concurrent;

public partial class Property_WACPR_TaxParcelPage : WACPage
{
    public override string ID { get { return "WACPR_TaxParcelPage"; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HandleRedirectFromAnotherPage(Request);
        }
        
    }
    #region Delegates

    public int Delegate_GetTaxParcelPK()
    {
        return Convert.ToInt32(0);
    }

    #endregion

    public override void OpenDefaultDataView(List<WACParameter> parms)
    {
        ServiceRequest sr = new ServiceRequest(this);
        sr.ServiceFor = WACPR_TaxParcelPageContents;
        sr.ServiceRequested = ServiceFactory.ServiceTypes.UpdateControl;
        sr.ParmList = parms;
        ServiceFactory.Instance.ServiceRequest(sr);
        sr = null;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetParticipantOrgList(string prefixText, int count, string contextKey)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.vw_participant_participantTypes.Where(w => w.fullname_LF_dnd.StartsWith(prefixText)).
                Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                Select(s => s.fullname_LF_dnd.TrimEnd());
            return a.ToArray<string>();
        }
    }
}
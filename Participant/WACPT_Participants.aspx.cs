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
using WAC_CustomControls;
using WAC_DataObjects;
using WAC_Event;
using WAC_Containers;
using WAC_Exceptions;
using WAC_Extensions;
using System.Collections;
using System.IO;

public partial class Participant_WACPT_Participants : WACPage, IWACContainer
{
    public string ParticipantName { get; set; }
    public override string ID { get { return "WACPT_ParticipantPage"; } }
    
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.Register(this);
    }
    public override void OpenDefaultDataView(List<WACParameter> parms)
    {
        BindParticipant(WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms, WACParameter.ParameterType.PrimaryKey)));
    }
   
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            PopulateDDLs4Searching();

            hlParticipant_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Global Data Participant.pdf";
            hlParticipant_Help.ImageUrl = "~/images/help_24.png";

            HandleRedirectFromAnotherPage(Request);

            // Create Ag Contractor
            WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(ddlCreate_Ag_Contractor_Zip, null, "NY", true);
            WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(ddlCreate_Ag_Contractor_Region, null);
            WACGlobal_Methods.PopulateControl_DatabaseLists_ContractorType_DDL(ddlCreate_Ag_Contractor_ContractorType, null, true);

            // This is to handle displaying a new participant created through ParticipantCreate
            if (Session["searchType"] == "P")
                HandleQueryType();

            Session["searchType"] = "";
            Session["searchKey"] = "";
            Session["results"] = "";
            Session["order"] = "";
            Session["count"] = "";
        }
       
    }

    private void PopulateDDLs4Searching()
    {
        try
        {

            PopulateSearchTypeDLL();
            PopulateSearchInterestDLL();

            PopulateDDL4WACEmployees();
        }
        catch (Exception ex)
        {
            WACAlert.Show("Page loading error in Participants: " + ex.Message, 0);
        }
    }

    private void PopulateSearchTypeDLL()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddlSearchType.Items.Clear();
            var a = from b in wDataContext.participantTypes
                    group b by b.list_participantType into g
                    orderby g.Key.participantType
                    select g.Key;

            foreach (var c in a)
            {
                ddlSearchType.Items.Add(new ListItem(c.participantType, c.pk_participantType_code));
            }
            ddlSearchType.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateSearchInterestDLL()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddlSearchInterest.Items.Clear();
            var a = from b in wDataContext.participantInterests
                    group b by b.list_interestType.list_interest into g
                    orderby g.Key.list_interestBase.@base, g.Key.interest
                    select g.Key;

            foreach (var c in a)
            {
                try
                {
                    string s = c.list_interestBase.@base + " - " + c.interest;
                    ddlSearchInterest.Items.Add(new ListItem(s, c.pk_list_interest.ToString()));
                }
                catch { }
            }
            ddlSearchInterest.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateDDL4WACEmployees()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddlWACEmployees.Items.Clear();
            ddlWACEmployees.DataTextField = "NAME";
            ddlWACEmployees.DataValueField = "PK";
            var a = wDataContext.participantWACs.Select(s => new { PK = s.pk_participantWAC, NAME = s.participant1.fullname_LF_dnd }).OrderBy(o => o.NAME);
            ddlWACEmployees.DataSource = a;
            ddlWACEmployees.DataBind();
            ddlWACEmployees.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    #endregion

    #region Delegates

    public int Delegate_GetParticipantPK()
    {
        return Convert.ToInt32(fvParticipant.DataKey.Value);
    }

    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    public void Organization_ViewEditInsertWindow(object oPK_Organization)
    {
        try
        {
            UC_Express_Organization1.LoadFormView_Organization(Convert.ToInt32(oPK_Organization));
        }
        catch { WACAlert.Show("Could not open Organization Window.", 0); }
    }

    public void Property_ViewEditInsertWindow(object oPK_Property)
    {
        try
        {
            UC_Express_Property1.LoadFormView_Property(Convert.ToInt32(oPK_Property));
        }
        catch { WACAlert.Show("Could not open Property Window.", 0); }
    }

    public void InvokedMethod_SectionPage_RebindRecord()
    {
        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
        upParticipants.Update();
        upParticipantSearch.Update();
    }

    public void InvokedMethod_DropDownListByAlphabet(object oType)
    {
        switch (oType.ToString())
        {
            case "PARTICIPANT_PERSON_SEARCH": Search_Participant_Person(); break;
            case "PARTICIPANT_WITH_ORGANIZATION": Search_Participant_Person(); break;
        }
    }

    public void InvokedMethod_DropDownListByAlphabet_LinkButtonEvent(object oType, object oValue)
    {
        switch (oType.ToString())
        {
            case "PARTICIPANT_PERSON_SEARCH_MULTI": Search_Participant_Person_Multi(oValue.ToString()); break;
            case "PARTICIPANT_ORGANIZATION_SEARCH_MULTI": Search_Participant_Organization_Multi(oValue.ToString()); break;
        }
    }

    #endregion

    #region Event Handling - Search

    public void HandleQueryType()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (Session["searchType"].ToString())
            {
                case "A":
                    // Search for All Participants
                    var aA = wDataContext.participants.OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aA.Count();
                    Session["results"] = aA;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aA.OrderBy(Session["order"].ToString(), null);
                    break;
                case "G":
                    // Participant Type
                    var aG = wDataContext.participants.Where(w => w.participantTypes.First(f => f.list_participantType.pk_participantType_code == ddlSearchType.SelectedValue).list_participantType.pk_participantType_code == ddlSearchType.SelectedValue).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    if (tbSearchLastName.Text != "")
                        aG = wDataContext.participants.Where(w => w.fullname_LF_dnd.StartsWith(tbSearchLastName.Text) && w.participantTypes.First(f => f.list_participantType.pk_participantType_code == ddlSearchType.SelectedValue).list_participantType.pk_participantType_code == ddlSearchType.SelectedValue).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aG.Count();
                    Session["results"] = aG;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aG.OrderBy(Session["order"].ToString(), null);
                    break;
                case "H":
                    // Participant Interest
                    var aH = wDataContext.participants.Where(w => w.lname != null && w.participantInterests.First(f => f.list_interestType.list_interest.pk_list_interest == Convert.ToInt32(ddlSearchInterest.SelectedValue)).list_interestType.list_interest.pk_list_interest == Convert.ToInt32(ddlSearchInterest.SelectedValue)).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aH.Count();
                    Session["results"] = aH;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aH.OrderBy(Session["order"].ToString(), null);
                    break;
                case "I":
                    // All or Part of Last Name or Organization plus DBAs
                    var dbas = from p in wDataContext.participants
                                       join d in wDataContext.vw_participant_LFDBAs on p.pk_participant equals d.PK
                                       where d.listing.StartsWith(Session["searchKey"].ToString())
                                           select p;
                    var parts = wDataContext.participants.Where(w => w.fullname_LF_dnd.StartsWith(Session["searchKey"].ToString())).
                            OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    var dbaParts = parts.Union(dbas);
                    if (!string.IsNullOrEmpty(ddlSearchType.SelectedValue))
                    {
                        dbas = from pt in dbaParts
                                join t in wDataContext.participantTypes on pt.pk_participant equals t.fk_participant
                                where t.fk_participantType_code == ddlSearchType.SelectedValue
                                select pt;
                        dbaParts = dbas.AsQueryable();
                    }
                    var finalCut = dbaParts.OrderBy(o => o.fullname_LF_dnd).ToList();
                    Session["count"] = finalCut.Count();
                    Session["results"] = finalCut;
                    break;
                case "J":
                    var aJ = wDataContext.participants.Where(w => w.property.state == Session["searchKey"].ToString()).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aJ.Count();
                    Session["results"] = aJ;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aJ.OrderBy(Session["order"].ToString(), null);
                    break;
                case "K":
                    List<string> listStateCity = (List<string>)Session["searchKey"];
                    var aK = wDataContext.participants.Where(w => w.property.state == listStateCity[0] && w.property.city == listStateCity[1]).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aK.Count();
                    Session["results"] = aK;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aK.OrderBy(Session["order"].ToString(), null);
                    break;
                case "L":
                    List<string> listStateCityAddressType = (List<string>)Session["searchKey"];
                    var aL = wDataContext.participants.Where(w => w.property.state == listStateCityAddressType[0] && w.property.city == listStateCityAddressType[1] && w.property.fk_addressType_code == listStateCityAddressType[2]).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aL.Count();
                    Session["results"] = aL;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aL.OrderBy(Session["order"].ToString(), null);
                    break;
                case "M":
                    List<string> listStateCityAddress = (List<string>)Session["searchKey"];
                    var aM = wDataContext.participants.Where(w => w.property.state == listStateCityAddress[0] && w.property.city == listStateCityAddress[1] && w.property.address_base == listStateCityAddress[2]).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aM.Count();
                    Session["results"] = aM;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aM.OrderBy(Session["order"].ToString(), null);
                    break;
                case "N":
                    List<string> listStateCityAddressNumber = (List<string>)Session["searchKey"];
                    var aN = wDataContext.participants.Where(w => w.property.state == listStateCityAddressNumber[0] && w.property.city == listStateCityAddressNumber[1] && w.property.address_base == listStateCityAddressNumber[2] && w.property.nbr == listStateCityAddressNumber[3]).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aN.Count();
                    Session["results"] = aN;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aN.OrderBy(Session["order"].ToString(), null);
                    break;
                case "O":
                    List<string> listStateCityNonRoadNumber = (List<string>)Session["searchKey"];
                    var aO = wDataContext.participants.Where(w => w.property.state == listStateCityNonRoadNumber[0] && w.property.city == listStateCityNonRoadNumber[1] && w.property.fk_addressType_code == listStateCityNonRoadNumber[2] && w.property.nbr == listStateCityNonRoadNumber[3]).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aO.Count();
                    Session["results"] = aO;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aO.OrderBy(Session["order"].ToString(), null);
                    break;
                case "P":
                    // search on pk_participant
                    var aP = wDataContext.participants.Where(w => w.pk_participant == Convert.ToInt32(Session["searchKey"])).OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                    Session["count"] = aP.Count();
                    Session["results"] = aP;
                    if (!string.IsNullOrEmpty(Session["order"].ToString())) Session["results"] = aP.OrderBy(Session["order"].ToString(), null);
                    break;
                default:
                    Session["count"] = 0;
                    Session["results"] = "";
                    Session["order"] = "";
                    break;
            }
            BindParticipants();
        }
    }

    public void ChangeIndex2Zero4SearchDDLs(bool bResetUC)
    {
        gvParticipant.SelectedIndex = -1;
        ViewState["SelectedValue"] = null;
        ClearParticipant();
        try { ddlSearchType.SelectedIndex = 0; }
        catch { }
        try { ddlSearchInterest.SelectedIndex = 0; }
        catch { }
        try { if (bResetUC) UC_DropDownListByAlphabet_Search_Person.ResetControls(); }
        catch { }
        tbSearchLastName.Text = "";
    }

    protected void lbParticipant_Search_ReloadReset_Click(object sender, EventArgs e)
    {
        ChangeIndex2Zero4SearchDDLs(true);
        ClearParticipants();
    }

    protected void lbParticipant_Search_All_Click(object sender, EventArgs e)
    {
        Session["order"] = "";
        Session["searchKey"] = "";
        Session["searchType"] = "A";
        ChangeIndex2Zero4SearchDDLs(true);
        HandleQueryType();
    }

    private void Search_Participant_Person_Multi(string sValue)
    {
        Session["order"] = "";
        Session["searchKey"] = sValue;
        Session["searchType"] = "B";
        ChangeIndex2Zero4SearchDDLs(true);
        HandleQueryType();
        upParticipants.Update();
    }

    private void Search_Participant_Organization_Multi(string sValue)
    {
        Session["order"] = "";
        Session["searchKey"] = sValue;
        Session["searchType"] = "C";
        ChangeIndex2Zero4SearchDDLs(true);
        HandleQueryType();
        upParticipants.Update();
    }

    private void Search_Participant_Person()
    {
        DropDownList ddl = UC_DropDownListByAlphabet_Search_Person.FindControl("ddl") as DropDownList;
        Session["order"] = "";
        Session["searchKey"] = ddl.SelectedValue;
        Session["searchType"] = "P";
        ChangeIndex2Zero4SearchDDLs(false);
        ddl.SelectedValue = Session["searchKey"].ToString();
        HandleQueryType();
        upParticipants.Update();
    }

 
    protected void ddlSearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlSearchType.SelectedValue))
        {
            Session["order"] = "";
            Session["searchType"] = "G";
            Session["searchKey"] = ddlSearchType.SelectedValue;
            gvParticipant.SelectedIndex = -1;
            ViewState["SelectedValue"] = null;
            ClearParticipant();
            ddlSearchType.SelectedValue = Session["searchKey"].ToString();
            HandleQueryType();
        }
    }

    protected void ddlSearchInterest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlSearchInterest.SelectedValue))
        {
            Session["order"] = "";
            Session["searchType"] = "H";
            Session["searchKey"] = ddlSearchInterest.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            ddlSearchInterest.SelectedValue = Session["searchKey"].ToString();
            HandleQueryType();
        }
    }

    protected void btnSearchLastName_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbSearchLastName.Text))
        {
            Session["order"] = "";
            Session["searchType"] = "I";
            Session["searchKey"] = tbSearchLastName.Text;
            gvParticipant.SelectedIndex = -1;
            ViewState["SelectedValue"] = null;
            ClearParticipant();
            tbSearchLastName.Text = Session["searchKey"].ToString();
            HandleQueryType();
        }
        else WACAlert.Show("Please enter a full or partial last name.", 0);
    }

    protected void tbSearchLastName_TextChanged(object sender, EventArgs e)
    {
        // This only gets called if they hit Enter in the search textbox
        if (!string.IsNullOrEmpty(tbSearchLastName.Text))
        {
            Session["order"] = "";
            Session["searchType"] = "I";
            Session["searchKey"] = tbSearchLastName.Text;
            gvParticipant.SelectedIndex = -1;
            ViewState["SelectedValue"] = null;
            ClearParticipant();
            tbSearchLastName.Text = Session["searchKey"].ToString();
            HandleQueryType();
        }
        else WACAlert.Show("Please enter a full or partial last name.", 0);

    }

    #endregion

    #region Event Handling - Results

    protected void gvParticipant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvParticipant.PageIndex = e.NewPageIndex;
        HandleQueryType();
    }

    protected void gvParticipant_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["order"] = e.SortExpression;
        HandleQueryType();
    }

    protected void gvParticipant_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvParticipant.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        fvParticipant.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant(Convert.ToInt32(gvParticipant.SelectedDataKey.Value));
        if (gvParticipant.SelectedIndex != -1) ViewState["SelectedValue"] = gvParticipant.SelectedValue.ToString();

    }

    #endregion

    #region Event Handling - Participant

    protected void lbParticipantClose_Click(object sender, EventArgs e)
    {
        Session["count"] = 0;
        Session["results"] = "";
        Session["order"] = "";       
        gvParticipant.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        HandleQueryType();
        ClearParticipant();
        upParticipants.Update();
        upParticipantSearch.Update();
    }

    protected void lbtnParticipantAdd_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            ChangeIndex2Zero4SearchDDLs(true);
            ClearParticipants();
            //fvParticipant.ChangeMode(FormViewMode.Insert);
            Session["searchType"] = "";
            BindParticipant(-1);

            WACPT_ParticipantCreate.LoadMe();

        }
    }

    protected void rblParticipants_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rblParticipants = fvParticipant.FindControl("rblParticipants") as RadioButtonList;
        Handle_Participant_TypeChange(rblParticipants.SelectedValue);
    }

    private void Handle_Participant_TypeChange(string sValue)
    {
        Panel pnlParticipant_Person_Insert = fvParticipant.FindControl("pnlParticipant_Person_Insert") as Panel;
        Panel pnlParticipant_Organization_Insert = fvParticipant.FindControl("pnlParticipant_Organization_Insert") as Panel;
        switch (sValue)
        {
            case "0":
                pnlParticipant_Organization_Insert.Visible = false;
                pnlParticipant_Person_Insert.Visible = true;
                break;
            case "1":
                pnlParticipant_Person_Insert.Visible = true;
                pnlParticipant_Organization_Insert.Visible = true;
                break;
            case "2":
                pnlParticipant_Person_Insert.Visible = false;
                pnlParticipant_Organization_Insert.Visible = true;
                break;
        }
    }

    protected void fvParticipant_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvParticipant.ChangeMode(e.NewMode);
            BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
        }
    }

    protected void fvParticipant_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlActive = fvParticipant.FindControl("ddlActive") as DropDownList;
        DropDownList ddlPrefix = fvParticipant.FindControl("ddlPrefix") as DropDownList;
        TextBox tbNameFirst = fvParticipant.FindControl("tbNameFirst") as TextBox;
        TextBox tbNameMiddle = fvParticipant.FindControl("tbNameMiddle") as TextBox;
        TextBox tbNameLast = fvParticipant.FindControl("tbNameLast") as TextBox;
        DropDownList ddlSuffix = fvParticipant.FindControl("ddlSuffix") as DropDownList;
        TextBox tbNickname = fvParticipant.FindControl("tbNickname") as TextBox;
        DropDownList ddlOrganization = fvParticipant.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("ddl") as DropDownList;
        TextBox tbEmail = fvParticipant.FindControl("tbEmail") as TextBox;
        TextBox tbWeb = fvParticipant.FindControl("tbWeb") as TextBox;
        DropDownList ddlRegionWAC = fvParticipant.FindControl("ddlRegionWAC") as DropDownList;
        DropDownList ddlMailingStatus = fvParticipant.FindControl("ddlMailingStatus") as DropDownList;
        DropDownList ddlGender = fvParticipant.FindControl("ddlGender") as DropDownList;
        DropDownList ddlEthnicity = fvParticipant.FindControl("ddlEthnicity") as DropDownList;
        DropDownList ddlRace = fvParticipant.FindControl("ddlRace") as DropDownList;
        DropDownList ddlDiversityData = fvParticipant.FindControl("ddlDiversityData") as DropDownList;
        TextBox tbDBA = (TextBox)fvParticipant.FindControl("tbDBA");
        CustomControls_AjaxCalendar calFormW9SignedDate = fvParticipant.FindControl("calFormW9SignedDate") as CustomControls_AjaxCalendar;
        DropDownList ddlDataReview = fvParticipant.FindControl("ddlDataReview") as DropDownList;
        TextBox tbDataReviewNote = fvParticipant.FindControl("tbDataReviewNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var v = (from q in wDataContext.participants.Where(o => o.pk_participant == Convert.ToInt32(fvParticipant.DataKey.Value))
                     select q).Single();

            if (!string.IsNullOrEmpty(ddlActive.SelectedValue)) v.active = ddlActive.SelectedValue;

            if (!string.IsNullOrEmpty(ddlPrefix.SelectedValue)) v.fk_prefix_code = ddlPrefix.SelectedValue;
            else v.fk_prefix_code = null;

            if (!string.IsNullOrEmpty(tbNameFirst.Text)) v.fname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNameFirst.Text, 48).Trim();
            else v.fname = null;

            if (!string.IsNullOrEmpty(tbNameMiddle.Text)) v.mname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNameMiddle.Text, 24).Trim();
            else v.mname = null;

            if (!string.IsNullOrEmpty(tbNameLast.Text)) v.lname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNameLast.Text, 48).Trim();
            else v.lname = null;

            if (!string.IsNullOrEmpty(ddlSuffix.SelectedValue)) v.fk_suffix_code = ddlSuffix.SelectedValue;
            else v.fk_suffix_code = null;

            if (!string.IsNullOrEmpty(tbNickname.Text)) v.nickname = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNickname.Text, 24).Trim();
            else v.nickname = null;

            if (!string.IsNullOrEmpty(ddlOrganization.SelectedValue)) v.fk_organization = Convert.ToInt32(ddlOrganization.SelectedValue);
            else v.fk_organization = null;

            if (!string.IsNullOrEmpty(tbEmail.Text)) v.email = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEmail.Text, 48).Trim();
            else v.email = null;

            if (!string.IsNullOrEmpty(tbWeb.Text)) v.web = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbWeb.Text, 96).Trim();
            else v.web = null;

            if (!string.IsNullOrEmpty(ddlRegionWAC.SelectedValue)) v.fk_regionWAC_code = ddlRegionWAC.SelectedValue;
            else v.fk_regionWAC_code = null;

            if (!string.IsNullOrEmpty(ddlMailingStatus.SelectedValue)) v.fk_mailingStatus_code = ddlMailingStatus.SelectedValue;
            else v.fk_mailingStatus_code = null;

            if (!string.IsNullOrEmpty(ddlGender.SelectedValue)) v.fk_gender_code = ddlGender.SelectedValue;
            else v.fk_gender_code = null;

            if (!string.IsNullOrEmpty(ddlEthnicity.SelectedValue)) v.fk_ethnicity_code = ddlEthnicity.SelectedValue;
            else v.fk_ethnicity_code = null;

            if (!string.IsNullOrEmpty(ddlRace.SelectedValue)) v.fk_race_code = ddlRace.SelectedValue;
            else v.fk_race_code = null;

            if (!string.IsNullOrEmpty(ddlDiversityData.SelectedValue)) v.fk_diversityData_code = ddlDiversityData.SelectedValue;
            else v.fk_diversityData_code = null;

            v.form_W9_signed_date = calFormW9SignedDate.CalDateNullable;

            if (!string.IsNullOrEmpty(ddlDataReview.SelectedValue)) v.fk_dataReview_code = ddlDataReview.SelectedValue;
            else v.fk_dataReview_code = null;

            if (!string.IsNullOrEmpty(tbDataReviewNote.Text)) v.dataReview_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDataReviewNote.Text, 255);
            else v.dataReview_note = null;
            v.DBA = tbDBA.Text;
            v.modified_by = Session["userName"].ToString();

            try
            {
                int iCode = wDataContext.participant_update(v.pk_participant, v.fk_regionWAC_code, v.active, v.fname, v.mname, v.lname, v.fk_prefix_code, 
                    v.fk_suffix_code,v.nickname, v.fk_organization, v.email, v.web, v.fk_gender_code, v.fk_ethnicity_code, v.fk_race_code, v.fk_diversityData_code, 
                    v.fk_mailingStatus_code,v.fk_dataReview_code, v.dataReview_note, v.DBA, v.form_W9_signed_date, v.modified_by);
                if (iCode == 0)
                {
                    fvParticipant.ChangeMode(FormViewMode.ReadOnly);
                    BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                }
                else
                {
                    WACAlert.Show("Error Returned from Database.", iCode);
                    e.Cancel = true;
                }

            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
        if (e.Cancel == false) HandleQueryType();
    }

    protected void fvParticipant_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        RadioButtonList rblParticipants = fvParticipant.FindControl("rblParticipants") as RadioButtonList;
        TextBox tbParticipants_NameFirst = fvParticipant.FindControl("tbParticipants_NameFirst") as TextBox;
        TextBox tbParticipants_NameLast = fvParticipant.FindControl("tbParticipants_NameLast") as TextBox;
        DropDownList ddlParticipants_Suffix = fvParticipant.FindControl("ddlParticipants_Suffix") as DropDownList;
        DropDownList ddl = fvParticipant.FindControl("UC_DropDownListByAlphabet_Organizations").FindControl("ddl") as DropDownList;
        DropDownList ddlRegionWAC = fvParticipant.FindControl("ddlRegionWAC") as DropDownList;
        DropDownList ddlInitialParticipantType = fvParticipant.FindControl("ddlInitialParticipantType") as DropDownList;
        TextBox tbEmail = fvParticipant.FindControl("tbEmail") as TextBox;
        DropDownList ddlGender = fvParticipant.FindControl("ddlGender") as DropDownList;
        DropDownList ddlEthnicity = fvParticipant.FindControl("ddlEthnicity") as DropDownList;
        DropDownList ddlRace = fvParticipant.FindControl("ddlRace") as DropDownList;
        DropDownList ddlDiversityData = fvParticipant.FindControl("ddlDiversityData") as DropDownList;
        HiddenField hfPropertyPK = fvParticipant.FindControl("UC_Property_EditInsert_Participant").FindControl("hfPropertyPK") as HiddenField;

        StringBuilder sb = new StringBuilder();

        int? i = null;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sNameFirst = null;
                string sNameLast = null;
                string sFK_Suffix = null;
                int? iFK_Organization = null;

                if (rblParticipants.SelectedValue == "0" || rblParticipants.SelectedValue == "1")
                {
                    if (!string.IsNullOrEmpty(tbParticipants_NameFirst.Text)) sNameFirst = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbParticipants_NameFirst.Text, 48);
                    else sb.Append("First Name is required. ");

                    if (!string.IsNullOrEmpty(tbParticipants_NameLast.Text)) sNameLast = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbParticipants_NameLast.Text, 48);
                    else sb.Append("Last Name is required. ");

                    if (!string.IsNullOrEmpty(ddlParticipants_Suffix.SelectedValue)) sFK_Suffix = ddlParticipants_Suffix.SelectedValue;
                }

                if (rblParticipants.SelectedValue == "1" || rblParticipants.SelectedValue == "2")
                {
                    if (!string.IsNullOrEmpty(ddl.SelectedValue)) iFK_Organization = Convert.ToInt32(ddl.SelectedValue);
                    else sb.Append("Organization is required.");
                }

                string sPhone = null;
                string sPhone2 = null;
                string sPhoneCell = null;
                string sPhoneWork = null;
                string sPhoneOther = null;
                string sFax = null;

                string sGender = null;
                if (!string.IsNullOrEmpty(ddlGender.SelectedValue)) sGender = ddlGender.SelectedValue;

                string sEthnicity = null;
                if (!string.IsNullOrEmpty(ddlEthnicity.SelectedValue)) sEthnicity = ddlEthnicity.SelectedValue;

                string sRace = null;
                if (!string.IsNullOrEmpty(ddlRace.SelectedValue)) sRace = ddlRace.SelectedValue;

                string sDiversity = null;
                if (!string.IsNullOrEmpty(ddlDiversityData.SelectedValue)) sDiversity = ddlDiversityData.SelectedValue;

                string sEmail = null;
                if (!string.IsNullOrEmpty(tbEmail.Text)) sEmail = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEmail.Text, 48).Trim();
            
                int? iPropertyPK = null;
                if (!string.IsNullOrEmpty(hfPropertyPK.Value)) iPropertyPK = Convert.ToInt32(hfPropertyPK.Value);

                string sFK_ParticipantType = null;
                if (!string.IsNullOrEmpty(ddlInitialParticipantType.SelectedValue)) sFK_ParticipantType = ddlInitialParticipantType.SelectedValue;

                string sFK_WACRegion = null;
                if (!string.IsNullOrEmpty(ddlRegionWAC.SelectedValue)) sFK_WACRegion = ddlRegionWAC.SelectedValue;
                string DBA = null;
                ddl = (DropDownList)FindControl("ddlDBA");
                if (ddl.SelectedValue != string.Empty)
                    DBA = ddl.SelectedValue;
                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.participant_add(sNameFirst, sNameLast, iFK_Organization, iPropertyPK, sPhone, sPhone2, sFax, sPhoneWork, 
                        sPhoneOther, sPhoneCell, sEmail, sGender, sEthnicity, sRace, sDiversity, sFK_ParticipantType, sFK_WACRegion, sFK_Suffix, 
                        DBA,Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvParticipant.ChangeMode(FormViewMode.ReadOnly);
                        BindParticipant(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvParticipant_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            TextBox t = (TextBox)fvParticipant.FindControl("ErrorTextBox");
            FormView fv = (FormView)sender;
            AjaxControlToolkit.ModalPopupExtender mpeError = (AjaxControlToolkit.ModalPopupExtender)fv.FindControl("mpeError");
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participant_delete(Convert.ToInt32(fvParticipant.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0)
                        lbParticipantClose_Click(null, null);
                    else
                        WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex)
                {
                    
                    t.Text = ex.Message;
                    mpeError.Show();

                    
                    //Page page = HttpContext.Current.CurrentHandler as Page;
                    //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";

                    //if (page != null)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(page, typeof(WACAlert), "alert", script, false);
                    //}

                }
            }
        }
    }

    #endregion

    #region Event Handling - Participant Communication

    protected void lbParticipant_Communication_Close_Click(object sender, EventArgs e)
    {
        fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Communication(-1);
        mpeParticipant_Communication.Hide();
        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
        upParticipants.Update();
        upParticipantSearch.Update();
    }

    protected void lbParticipant_Communication_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            fvParticipant_Communication.ChangeMode(FormViewMode.Insert);
            BindParticipant_Communication(-1);

            mpeParticipant_Communication.Show();
            upParticipant_Communication.Update();
        }
    }

    protected void lbParticipant_Communication_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Communication(Convert.ToInt32(lb.CommandArgument));
        mpeParticipant_Communication.Show();
        upParticipant_Communication.Update();
    }

    protected void ddlParticipant_Communication_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlType = (DropDownList)sender;
        DropDownList ddlUsage = fvParticipant_Communication.FindControl("ddlUsage") as DropDownList;
        ddlUsage.Items.Clear();
        if (!string.IsNullOrEmpty(ddlType.SelectedValue))
        {
            WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationUsage_DDL(fvParticipant_Communication, "ddlUsage", null, ddlType.SelectedValue);
        }
    }

    protected void fvParticipant_Communication_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvParticipant_Communication.ChangeMode(e.NewMode);
            BindParticipant_Communication(Convert.ToInt32(fvParticipant_Communication.DataKey.Value));
        }
    }

    protected void fvParticipant_Communication_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlType = fvParticipant_Communication.FindControl("ddlType") as DropDownList;
        DropDownList ddlUsage = fvParticipant_Communication.FindControl("ddlUsage") as DropDownList;
        TextBox tbAreaCode = fvParticipant_Communication.FindControl("tbAreaCode") as TextBox;
        TextBox tbPhoneNumber = fvParticipant_Communication.FindControl("tbPhoneNumber") as TextBox;
        TextBox tbExtension = fvParticipant_Communication.FindControl("tbExtension") as TextBox;
        TextBox tbNote = fvParticipant_Communication.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? i = null;
                int iCode = 0;
                iCode = wDataContext.communication_add(tbAreaCode.Text, tbPhoneNumber.Text, Session["userName"].ToString(), ref i);
                if (iCode != 0) i = null;

                var a = wDataContext.participantCommunications.Where(w => w.pk_participantCommunication == Convert.ToInt32(fvParticipant_Communication.DataKey.Value)).Select(s => s).Single();

                if (!string.IsNullOrEmpty(ddlType.SelectedValue)) a.fk_communicationType_code = ddlType.SelectedValue;
                else sb.Append("Type was not updated. Type is required. ");

                if (!string.IsNullOrEmpty(ddlUsage.SelectedValue)) a.fk_communicationUsage_code = ddlUsage.SelectedValue;
                else sb.Append("Usage was not updated. Usage is required. ");

                if (i != null) a.fk_communication = Convert.ToInt32(i);
                else sb.Append("Phone Number was not updated. Bad Phone Number. ");

                a.extension = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbExtension.Text, 8).Trim();

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 48).Trim();

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
                BindParticipant_Communication(Convert.ToInt32(fvParticipant_Communication.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvParticipant_Communication_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlType = fvParticipant_Communication.FindControl("ddlType") as DropDownList;
        DropDownList ddlUsage = fvParticipant_Communication.FindControl("ddlUsage") as DropDownList;
        TextBox tbAreaCode = fvParticipant_Communication.FindControl("tbAreaCode") as TextBox;
        TextBox tbPhoneNumber = fvParticipant_Communication.FindControl("tbPhoneNumber") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sType = null;
                if (!string.IsNullOrEmpty(ddlType.SelectedValue)) sType = ddlType.SelectedValue;
                else sb.Append("Type is required. ");

                string sUsage = null;
                if (!string.IsNullOrEmpty(ddlUsage.SelectedValue)) sUsage = ddlUsage.SelectedValue;
                else sb.Append("Usage is required. ");

                string sAreaCode = null;
                if (!string.IsNullOrEmpty(tbAreaCode.Text)) sAreaCode = tbAreaCode.Text;
                else sb.Append("Area Code is required. ");

                string sPhoneNumber = null;
                if (!string.IsNullOrEmpty(tbPhoneNumber.Text)) sPhoneNumber = tbPhoneNumber.Text;
                else sb.Append("Phone Number is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.participantCommunication_add_express(Convert.ToInt32(fvParticipant.DataKey.Value), sAreaCode, sPhoneNumber, sType, sUsage, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
                        BindParticipant_Communication(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvParticipant_Communication_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantCommunication_delete(Convert.ToInt32(fvParticipant_Communication.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbParticipant_Communication_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Participant Property

    protected void lbParticipant_Property_Close_Click(object sender, EventArgs e)
    {
        fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Property(-1);
        mpeParticipant_Property.Hide();
        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
        upParticipants.Update();
        upParticipantSearch.Update();
    }

    protected void lbParticipant_Property_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            fvParticipant_Property.ChangeMode(FormViewMode.Insert);
            BindParticipant_Property(-1);

            mpeParticipant_Property.Show();
            upParticipant_Property.Update();
        }
    }

    protected void lbParticipant_Property_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Property(Convert.ToInt32(lb.CommandArgument));
        mpeParticipant_Property.Show();
        upParticipant_Property.Update();
    }

    protected void fvParticipant_Property_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvParticipant_Property.ChangeMode(e.NewMode);
            BindParticipant_Property(Convert.ToInt32(fvParticipant_Property.DataKey.Value));
        }
    }

    protected void fvParticipant_Property_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        HiddenField hfPropertyPK = fvParticipant_Property.FindControl("UC_Property_EditInsert_Participant_Property").FindControl("hfPropertyPK") as HiddenField;
        DropDownList ddlMaster = fvParticipant_Property.FindControl("ddlMaster") as DropDownList;
        DropDownList ddlParticipantCC = fvParticipant_Property.FindControl("UC_DropDownListByAlphabet_Participant_Property").FindControl("ddl") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = (from b in wDataContext.participantProperties.Where(w => w.pk_participantProperty == Convert.ToInt32(fvParticipant_Property.DataKey.Value))
                         select b).Single();

                if (!string.IsNullOrEmpty(hfPropertyPK.Value)) a.fk_property = Convert.ToInt32(hfPropertyPK.Value);
                else sb.Append("Property was not updated. Property is required. ");

                a.master = ddlMaster.SelectedValue;

                if (!string.IsNullOrEmpty(ddlParticipantCC.SelectedValue)) a.fk_participant_cc = Convert.ToInt32(ddlParticipantCC.SelectedValue);
                else a.fk_participant_cc = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();
                int code = 0;
                string errMsg = !string.IsNullOrEmpty(sb.ToString()) ? sb.ToString() : string.Empty;
                if (string.IsNullOrEmpty(errMsg))
                    code = wDataContext.participantProperty_update(a.pk_participantProperty, a.fk_participant, a.fk_property,
                        a.master, a.modified_by);
                if (code != 0 || !string.IsNullOrEmpty(errMsg))
                    throw new WAC_Exceptions.WACEX_GeneralDatabaseException(errMsg, code);
                else
                {
                    fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
                    BindParticipant_Property(Convert.ToInt32(fvParticipant_Property.DataKey.Value));
                }              
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvParticipant_Property_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlAddress = fvParticipant_Property.FindControl("ddlAddress") as DropDownList;
        CheckBox cbMaster = (CheckBox)fvParticipant_Property.FindControl("cbMaster");

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPK_Property = null;
                if (!string.IsNullOrEmpty(ddlAddress.SelectedValue))
                    iPK_Property = Convert.ToInt32(ddlAddress.SelectedValue);
                else
                    throw new WACEX_GeneralDatabaseException("No Address Selected.", 0);
                string sMaster = cbMaster.Checked ? "Y" : "N";

                iCode = wDataContext.participantProperty_add(Convert.ToInt32(fvParticipant.DataKey.Value), iPK_Property, sMaster, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
                    BindParticipant_Property(Convert.ToInt32(i));
                }
                else 
                    WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) 
            { 
                WACAlert.Show("Error: " + ex.Message, 0); 
            }
        }
    }

    protected void fvParticipant_Property_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {

            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantProperty_delete(Convert.ToInt32(fvParticipant_Property.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbParticipant_Property_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
               
            }
        }
    }

    #endregion

    #region Event Handling - Participant Organization

    protected void lbParticipant_Organization_Close_Click(object sender, EventArgs e)
    {
        fvParticipant_Organization.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Organization(-1);
        mpeParticipant_Organization.Hide();
        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
        upParticipants.Update();
        upParticipantSearch.Update();
    }

    protected void lbParticipant_Organization_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            fvParticipant_Organization.ChangeMode(FormViewMode.Insert);
            BindParticipant_Organization(-1);

            mpeParticipant_Organization.Show();
            upParticipant_Organization.Update();
        }
    }

    protected void lbParticipant_Organization_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvParticipant_Organization.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Organization(Convert.ToInt32(lb.CommandArgument));
        mpeParticipant_Organization.Show();
        upParticipant_Organization.Update();
    }

    protected void fvParticipant_Organization_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvParticipant_Organization.ChangeMode(e.NewMode);
            BindParticipant_Organization(Convert.ToInt32(fvParticipant_Organization.DataKey.Value));
        }
    }

    protected void fvParticipant_Organization_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlOrganization = fvParticipant_Organization.FindControl("UC_DropDownListByAlphabet_Participant_Organization").FindControl("ddl") as DropDownList;
        DropDownList ddlMaster = fvParticipant_Organization.FindControl("ddlMaster") as DropDownList;
        TextBox tbTitle = fvParticipant_Organization.FindControl("tbTitle") as TextBox;
        TextBox tbDivision = fvParticipant_Organization.FindControl("tbDivision") as TextBox;
        TextBox tbDepartment = fvParticipant_Organization.FindControl("tbDepartment") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = (from b in wDataContext.participantOrganizations.Where(w => w.pk_participantOrganization == Convert.ToInt32(fvParticipant_Organization.DataKey.Value))
                         select b).Single();

                if (!string.IsNullOrEmpty(ddlOrganization.SelectedValue)) a.fk_organization = Convert.ToInt32(ddlOrganization.SelectedValue);
                else sb.Append("Organization was not updated. Organization is required. ");

                a.master = ddlMaster.SelectedValue;

                a.title = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbTitle.Text, 48).Trim();

                a.division = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDivision.Text, 48).Trim();

                a.department = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDepartment.Text, 48).Trim();

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvParticipant_Organization.ChangeMode(FormViewMode.ReadOnly);
                BindParticipant_Organization(Convert.ToInt32(fvParticipant_Organization.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvParticipant_Organization_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlOrganization = fvParticipant_Organization.FindControl("UC_DropDownListByAlphabet_Participant_Organization").FindControl("ddl") as DropDownList;
        DropDownList ddlMaster = fvParticipant_Organization.FindControl("ddlMaster") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPK_Organization = null;
                if (!string.IsNullOrEmpty(ddlOrganization.SelectedValue)) iPK_Organization = Convert.ToInt32(ddlOrganization.SelectedValue);
                else sb.Append("Organization is required. ");

                string sMaster = ddlMaster.SelectedValue;

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.participantOrganization_add(Convert.ToInt32(fvParticipant.DataKey.Value), iPK_Organization, sMaster, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvParticipant_Organization.ChangeMode(FormViewMode.ReadOnly);
                        BindParticipant_Organization(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvParticipant_Organization_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantOrganization_delete(Convert.ToInt32(fvParticipant_Organization.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbParticipant_Organization_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Participant Contractor

    protected void fvContractor_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            FormView fvContractor = (FormView)sender;
            fvContractor.ChangeMode(e.NewMode);
            BindParticipantContractor(Convert.ToInt32(fvContractor.DataKey.Value));
        }
    }

    protected void fvContractor_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fvContractor = (FormView)sender;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            int i = Convert.ToInt32(fvContractor.DataKey.Value);
            var a = (from b in wDataContext.participantContractors.Where(w => w.pk_participantContractor == i)
                     select b).Single();

            DropDownList ddlActive = fvContractor.FindControl("ddlActive") as DropDownList;
            DropDownList ddlWACRegion = fvContractor.FindControl("ddlWACRegion") as DropDownList;
            TextBox tbEIN = fvContractor.FindControl("tbEIN") as TextBox;
            DropDownList ddlLandowner = fvContractor.FindControl("ddlLandowner") as DropDownList;
            DropDownList ddlIsSupplier = fvContractor.FindControl("ddlIsSupplier") as DropDownList;
            //Calendar calVendexDate = fvContractor.FindControl("UC_EditCalendar_Participant_Contractor_VendexDate").FindControl("cal") as Calendar;
            //Calendar calWorkersCompStart = fvContractor.FindControl("UC_EditCalendar_Participant_Contractor_WorkersCompStart").FindControl("cal") as Calendar;
            //Calendar calWorkersCompEnd = fvContractor.FindControl("UC_EditCalendar_Participant_Contractor_WorkersCompEnd").FindControl("cal") as Calendar;
            ////Calendar calFormW9DateSigned = fvContractor.FindControl("UC_EditCalendar_Participant_Contractor_FormW9DateSigned").FindControl("cal") as Calendar;
            //Calendar calLiabilityInsStart = fvContractor.FindControl("UC_EditCalendar_Participant_Contractor_LiabilityInsuranceStart").FindControl("cal") as Calendar;
            //Calendar calLiabilityInsEnd = fvContractor.FindControl("UC_EditCalendar_Participant_Contractor_LiabilityInsuranceEnd").FindControl("cal") as Calendar;
            CustomControls_AjaxCalendar calVendexDate = fvContractor.FindControl("calVendexDate") as CustomControls_AjaxCalendar;
            CustomControls_AjaxCalendar calWorkersCompStart = fvContractor.FindControl("calWorkersCompStart") as CustomControls_AjaxCalendar;
            CustomControls_AjaxCalendar calWorkersCompEnd = fvContractor.FindControl("calWorkersCompEnd") as CustomControls_AjaxCalendar;
            CustomControls_AjaxCalendar calLiabilityInsStart = fvContractor.FindControl("calLiabilityInsStart") as CustomControls_AjaxCalendar;
            CustomControls_AjaxCalendar calLiabilityInsEnd = fvContractor.FindControl("calLiabilityInsEnd") as CustomControls_AjaxCalendar;
            if (!string.IsNullOrEmpty(ddlActive.SelectedValue)) a.active = ddlActive.SelectedValue;
            else a.active = null;

            if (!string.IsNullOrEmpty(ddlWACRegion.SelectedValue)) a.fk_regionWAC_code = ddlWACRegion.SelectedValue;
            else a.fk_regionWAC_code = null;

            a.ein = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEIN.Text, 16).Trim();

            if (!string.IsNullOrEmpty(ddlLandowner.SelectedValue)) a.landowner = ddlLandowner.SelectedValue;
            else a.landowner = null;

            if (!string.IsNullOrEmpty(ddlIsSupplier.SelectedValue)) a.isSupplier = ddlIsSupplier.SelectedValue;
            else a.isSupplier = null;
            
            //if (calVendexDate.SelectedDate.Year > 1900) a.vendex_date = calVendexDate.SelectedDate;
            //else a.vendex_date = null;
            
            //if (calWorkersCompStart.SelectedDate.Year > 1900) a.workmensComp_start = calWorkersCompStart.SelectedDate;
            //else a.workmensComp_start = null;
            
            //if (calWorkersCompEnd.SelectedDate.Year > 1900) a.workmensComp_end = calWorkersCompEnd.SelectedDate;
            //else a.workmensComp_end = null;
            
            //if (calLiabilityInsStart.SelectedDate.Year > 1900) a.liabilityIns_start = calLiabilityInsStart.SelectedDate;
            //else a.liabilityIns_start = null;
            
            //if (calLiabilityInsEnd.SelectedDate.Year > 1900) a.liabilityIns_end = calLiabilityInsEnd.SelectedDate;
            //else a.liabilityIns_end = null;
            a.vendex_date = calVendexDate.CalDateNullable;
            a.workmensComp_start = calWorkersCompStart.CalDateNullable;
            a.workmensComp_end = calWorkersCompEnd.CalDateNullable;
            a.liabilityIns_start = calLiabilityInsStart.CalDateNullable;
            a.liabilityIns_end = calLiabilityInsEnd.CalDateNullable;

            a.modified = DateTime.Now;
            a.modified_by = Session["userName"].ToString();

            bool bCheckStartAndEndDateWorkmansComp = true;
            bool bCheckStartAndEndDateLiabilityIns = true;

            if (a.workmensComp_start != null) if (a.workmensComp_end == null) bCheckStartAndEndDateWorkmansComp = false;
            //if (a.workmensComp_start == null) bCheckStartAndEndDateWorkmansComp = false;
            if (a.workmensComp_end != null) if (a.workmensComp_start == null) bCheckStartAndEndDateWorkmansComp = false;
            if (a.liabilityIns_start != null) if (a.liabilityIns_end == null) bCheckStartAndEndDateLiabilityIns = false;
            if (a.liabilityIns_end != null) if (a.liabilityIns_start == null) bCheckStartAndEndDateLiabilityIns = false;

            if (bCheckStartAndEndDateWorkmansComp && bCheckStartAndEndDateLiabilityIns)
            {
                try
                {
                    wDataContext.SubmitChanges();
                    fvContractor.ChangeMode(FormViewMode.ReadOnly);
                    BindParticipantContractor(i);
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
            else
            {
                string sAlert = string.Empty;
                if (!bCheckStartAndEndDateWorkmansComp) sAlert += "Workman's Comp needs to have a start date. ";
                if (!bCheckStartAndEndDateLiabilityIns) sAlert += "Liability Insurance needs to have both a start and end date. ";
                WACAlert.Show(sAlert, 0);
            }
        }
    }

    #endregion

    #region Event Handling - Participant Contractor - Type

    protected void ddlAg_Participant_Contractor_Type_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            FormView fvContractor = fvParticipant.FindControl("fvContractor") as FormView;
            DropDownList ddl = (DropDownList)sender;
            if (!String.IsNullOrEmpty(ddl.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    int? i = null;
                    int iCode = wDataContext.participantContractorType_add(Convert.ToInt32(fvContractor.DataKey.Value), ddl.SelectedValue, Session["userName"].ToString(), ref i);
                    if (iCode == 0) BindParticipantContractor(Convert.ToInt32(fvContractor.DataKey.Value));
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
            }
        }
    }

    protected void lbParticipant_Contractor_Type_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            FormView fvContractor = fvParticipant.FindControl("fvContractor") as FormView;
            LinkButton lb = (LinkButton)sender;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = wDataContext.participantContractorType_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0) BindParticipantContractor(Convert.ToInt32(fvContractor.DataKey.Value));
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    #endregion

    #region Event Handling - Participant Contractor - Vendex

    protected void ddlAg_Participant_Contractor_Vendex_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            FormView fvContractor = fvParticipant.FindControl("fvContractor") as FormView;
            DropDownList ddl = (DropDownList)sender;
            if (!String.IsNullOrEmpty(ddl.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    int? i = null;
                    int iCode = wDataContext.participantContractor_vendex_add(Convert.ToInt32(fvContractor.DataKey.Value), Convert.ToInt16(ddl.SelectedValue), Session["userName"].ToString(), ref i);
                    if (iCode == 0) BindParticipantContractor(Convert.ToInt32(fvContractor.DataKey.Value));
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
            }
        }
    }

    protected void lbParticipant_Contractor_Vendex_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            FormView fvContractor = fvParticipant.FindControl("fvContractor") as FormView;
            LinkButton lb = (LinkButton)sender;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = wDataContext.participantContractor_vendex_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0) BindParticipantContractor(Convert.ToInt32(fvContractor.DataKey.Value));
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    #endregion

    #region Event Handling - Participant - Type

    protected void ddlParticipantType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            DropDownList ddlParticipantType = (DropDownList)sender;
            if (!string.IsNullOrEmpty(ddlParticipantType.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    int? i = null;
                    int iCode = wDataContext.participantType_add(Convert.ToInt32(fvParticipant.DataKey.Value), ddlParticipantType.SelectedValue, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
            }
        }
    }

    protected void lbParticipantTypeDelete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            DropDownList ddlParticipantType = fvParticipant.FindControl("ddlParticipantType") as DropDownList;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = wDataContext.participantType_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0)
                {
                    BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    #endregion

    #region Event Handling - Participant - Forestry Type

    protected void ddlParticipantForestryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            DropDownList ddlParticipantForestryType = (DropDownList)sender;
            if (!string.IsNullOrEmpty(ddlParticipantForestryType.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    int? i = null;
                    int iCode = wDataContext.participantForestryType_add(Convert.ToInt32(fvParticipant.DataKey.Value), ddlParticipantForestryType.SelectedValue, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
            }
        }
    }

    protected void lbParticipantForestryTypeDelete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            DropDownList ddlParticipantForestryType = fvParticipant.FindControl("ddlParticipantForestryType") as DropDownList;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = wDataContext.participantForestryType_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0)
                {
                    BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    #endregion

    #region Event Handling - Participant - Interest

    protected void ddlParticipantInterest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            DropDownList ddlParticipantInterest = (DropDownList)sender;
            if (!string.IsNullOrEmpty(ddlParticipantInterest.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    int? i = null;
                    int iCode = wDataContext.participantInterest_add(Convert.ToInt32(fvParticipant.DataKey.Value), Convert.ToInt32(ddlParticipantInterest.SelectedValue), Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
            }
        }
    }

    protected void lbParticipantInterestDelete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            DropDownList ddlParticipantInterest = fvParticipant.FindControl("ddlParticipantInterest") as DropDownList;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = wDataContext.participantInterest_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0)
                {
                    BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    #endregion

    #region Event Handling - Participant - Note

    protected void lbParticipant_Note_Close_Click(object sender, EventArgs e)
    {
        fvParticipant_Note.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Note(-1);
        mpeParticipant_Note.Hide();
        BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
        upParticipants.Update();
        upParticipantSearch.Update();
    }

    protected void lbParticipant_Note_Add_Click(object sender, EventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        //{
            fvParticipant_Note.ChangeMode(FormViewMode.Insert);
            BindParticipant_Note(-1);

            mpeParticipant_Note.Show();
            upParticipant_Note.Update();
        //}
    }

    protected void lbParticipant_Note_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvParticipant_Note.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Note(Convert.ToInt32(lb.CommandArgument));
        mpeParticipant_Note.Show();
        upParticipant_Note.Update();
    }

    protected void fvParticipant_Note_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit)
        {
            //bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
            bChangeMode = WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "participantNote", Convert.ToInt32(fvParticipant_Note.DataKey.Value));
        }
        if (bChangeMode)
        {
            fvParticipant_Note.ChangeMode(e.NewMode);
            BindParticipant_Note(Convert.ToInt32(fvParticipant_Note.DataKey.Value));
        }
    }

    protected void fvParticipant_Note_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlNoteType = fvParticipant_Note.FindControl("ddlNoteType") as DropDownList;
        TextBox tbNote = fvParticipant_Note.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = (from b in wDataContext.participantNotes.Where(w => w.pk_participantNote == Convert.ToInt32(fvParticipant_Note.DataKey.Value))
                         select b).Single();

                if (!string.IsNullOrEmpty(ddlNoteType.SelectedValue)) a.fk_participantType_code = ddlNoteType.SelectedValue;
                else sb.Append("Note Type was not updated. Note Type is required. ");

                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000).Trim();
                else sb.Append("Note was not updated. Note is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvParticipant_Note.ChangeMode(FormViewMode.ReadOnly);
                BindParticipant_Note(Convert.ToInt32(fvParticipant_Note.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvParticipant_Note_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlNoteType = fvParticipant_Note.FindControl("ddlNoteType") as DropDownList;
        TextBox tbNote = fvParticipant_Note.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sNoteType = null;
                if (!string.IsNullOrEmpty(ddlNoteType.SelectedValue)) sNoteType = ddlNoteType.SelectedValue;
                else sb.Append("Note Type is required. ");

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000).Trim();
                else sb.Append("Note is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.participantNote_add(Convert.ToInt32(fvParticipant.DataKey.Value), sNoteType, sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvParticipant_Note.ChangeMode(FormViewMode.ReadOnly);
                        BindParticipant_Note(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvParticipant_Note_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        //{
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    if (WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "participantNote", Convert.ToInt32(fvParticipant_Note.DataKey.Value)))
                    {
                        iCode = wDataContext.participantNote_delete(Convert.ToInt32(fvParticipant_Note.DataKey.Value), Session["userName"].ToString());
                        if (iCode == 0) lbParticipant_Note_Close_Click(null, null);
                        else WACAlert.Show("Error Returned from Database.", iCode);
                    }
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        //}
    }

    #endregion

    #region Event Handling - Participant - Mailing

    protected void ddlParticipant_MailingParticipant_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            DropDownList ddl = (DropDownList)sender;
            if (!string.IsNullOrEmpty(ddl.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        var a = wDataContext.participants.Where(w => w.pk_participant == Convert.ToInt32(fvParticipant.DataKey.Value)).Select(s => new { s.pk_participant });
                        int? iPK_Participant = a.Single().pk_participant;
                        int? i = null;
                        int iCode = wDataContext.mailingParticipant_add(Convert.ToInt32(ddl.SelectedValue), iPK_Participant, Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                        }
                        else WACAlert.Show("Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
                }
            }
        }
    }

    protected void lbParticipant_MailingParticipant_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = wDataContext.mailingParticipant_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0)
                {
                    BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    protected void lbParticipant_MailingSentTo_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iCode = wDataContext.mailingSentTo_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0)
                {
                    BindParticipant(Convert.ToInt32(fvParticipant.DataKey.Value));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
        }
    }

    #endregion

    #region Event Handling - Participant - WAC

    protected void lbWACEmployees_Click(object sender, EventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.HR_WAC);
        if (b)
        {
            mpeWACEmployees.Show();
        }
        else WACAlert.Show("You do not have permission to view the WAC Employees.", 0);
    }

    protected void lbWACEmployeesClose_Click(object sender, EventArgs e)
    {
        fvWACEmployee.ChangeMode(FormViewMode.ReadOnly);
        fvWACEmployee.DataSource = "";
        fvWACEmployee.DataBind();
        try { ddlWACEmployees.SelectedIndex = 0; }
        catch { }
        mpeWACEmployees.Hide();
    }

    protected void lbWACEmployeesAdd_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            fvWACEmployee.ChangeMode(FormViewMode.Insert);
            BindParticipantWAC(-1);
        }
    }

    protected void ddlWACEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlWACEmployees.SelectedValue))
        {
            fvWACEmployee.ChangeMode(FormViewMode.ReadOnly);
            BindParticipantWAC(Convert.ToInt32(ddlWACEmployees.SelectedValue));
        }
    }

    protected void fvWACEmployee_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvWACEmployee.ChangeMode(e.NewMode);
            BindParticipantWAC(Convert.ToInt32(fvWACEmployee.DataKey.Value));
        }
    }

    protected void fvWACEmployee_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        //DropDownList ddlActive = fvWACEmployee.FindControl("ddlActive") as DropDownList;
        //TextBox tbTitle = fvWACEmployee.FindControl("tbTitle") as TextBox;
        //DropDownList ddlWACSector = fvWACEmployee.FindControl("ddlWACSector") as DropDownList;
        //TextBox tbPhone = fvWACEmployee.FindControl("tbPhone") as TextBox;
        TextBox tbPhoneExt = fvWACEmployee.FindControl("tbPhoneExt") as TextBox;
        TextBox tbEmail = fvWACEmployee.FindControl("tbEmail") as TextBox;
        //Calendar calDOB = fvWACEmployee.FindControl("UC_EditCalendar_Participant_DOB").FindControl("cal") as Calendar;
        CustomControls_AjaxCalendar calDOB = fvWACEmployee.FindControl("calDOB") as CustomControls_AjaxCalendar;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.participantWACs.Where(w => w.pk_participantWAC == Convert.ToInt32(fvWACEmployee.DataKey.Value)).Select(s => s).Single();

                //if (!string.IsNullOrEmpty(ddlActive.SelectedValue)) a.active = ddlActive.SelectedValue;
                //else sb.Append("Active was not updated. Active is required. ");

                //a.title = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbTitle.Text, 48).Trim();

                //if (!string.IsNullOrEmpty(ddlWACSector.SelectedValue)) a.fk_sectorWAC_code = ddlWACSector.SelectedValue;
                //else a.fk_sectorWAC_code = null;

                //a.phone = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbPhone.Text, WACGlobal_Methods.Enum_Number_Type.PHONENUMBER);

                a.phone_ext = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPhoneExt.Text, 4).Trim();

                a.email = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEmail.Text, 48).Trim();

                //if (calDOB.SelectedDate.Year > 1900) a.dob = calDOB.SelectedDate;
                //else a.dob = null;
                a.dob = calDOB.CalDateNullable;
                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvWACEmployee.ChangeMode(FormViewMode.ReadOnly);
                BindParticipantWAC(Convert.ToInt32(fvWACEmployee.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvWACEmployee_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlWACEmployees = fvWACEmployee.FindControl("ddlWACEmployees") as DropDownList;
        //DropDownList ddlActive = fvWACEmployee.FindControl("ddlActive") as DropDownList;
        //TextBox tbTitle = fvWACEmployee.FindControl("tbTitle") as TextBox;
        //DropDownList ddlWACSector = fvWACEmployee.FindControl("ddlWACSector") as DropDownList;
        //TextBox tbPhone = fvWACEmployee.FindControl("tbPhone") as TextBox;
        TextBox tbPhoneExt = fvWACEmployee.FindControl("tbPhoneExt") as TextBox;
        TextBox tbEmail = fvWACEmployee.FindControl("tbEmail") as TextBox;
        //Calendar calDOB = fvWACEmployee.FindControl("UC_EditCalendar_Participant_DOB").FindControl("cal") as Calendar;
        CustomControls_AjaxCalendar calDOB = fvWACEmployee.FindControl("calDOB") as CustomControls_AjaxCalendar;
        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iParticipant = null;
                if (!string.IsNullOrEmpty(ddlWACEmployees.SelectedValue)) iParticipant = Convert.ToInt32(ddlWACEmployees.SelectedValue);
                else sb.Append("Participant is required. ");

                //string sActive = null;
                //if (!string.IsNullOrEmpty(ddlActive.SelectedValue)) sActive = ddlActive.SelectedValue;
                //else sb.Append("Active is required. ");

                //string sTitle = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbTitle.Text, 48).Trim();

                //string sWACSector = null;
                //if (!string.IsNullOrEmpty(ddlWACSector.SelectedValue)) sWACSector = ddlWACSector.SelectedValue;
                //else sb.Append("WAC Sector is required. ");

                //string sPhone = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbPhone.Text, WACGlobal_Methods.Enum_Number_Type.PHONENUMBER);

                string sPhoneExt = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPhoneExt.Text, 4).Trim();

                string sEmail = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEmail.Text, 48).Trim();

                DateTime? dtDOB;
                //if (calDOB.SelectedDate.Year > 1900) dtDOB = calDOB.SelectedDate;
                //else dtDOB = null;
                dtDOB = calDOB.CalDateNullable;
                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    //iCode = wDataContext.participantWAC_add(iParticipant, sActive, sTitle, sWACSector, sPhone, sPhoneExt, sEmail, dtDOB, Session["userName"].ToString(), ref i);
                    //iCode = wDataContext.participantWAC_add(iParticipant, sPhoneExt, sEmail, dtDOB, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvWACEmployee.ChangeMode(FormViewMode.ReadOnly);
                        BindParticipantWAC(Convert.ToInt32(i));
                        PopulateDDL4WACEmployees();
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvWACEmployee_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantWAC_delete(Convert.ToInt32(fvWACEmployee.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        fvWACEmployee.ChangeMode(FormViewMode.ReadOnly);
                        BindParticipantWAC(-1);
                        PopulateDDL4WACEmployees();
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Participant - Create Ag Contractor

    protected void lbCreate_Ag_Contractor_Close_Click(object sender, EventArgs e)
    {
        mpeCreate_Ag_Contractor.Hide();
    }

    protected void lbCreate_Ag_Contractor_Insert_Click(object sender, EventArgs e)
    {
        int? i = null;
        int iCode = 0;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                string sFNAME = null;
                if (!string.IsNullOrEmpty(tbCreate_Ag_Contractor_FNAME.Text)) sFNAME = tbCreate_Ag_Contractor_FNAME.Text;

                string sLNAME = null;
                if (!string.IsNullOrEmpty(tbCreate_Ag_Contractor_LNAME.Text)) sLNAME = tbCreate_Ag_Contractor_LNAME.Text;

                string sORG = null;
                if (!string.IsNullOrEmpty(tbCreate_Ag_Contractor_ORG.Text)) sORG = tbCreate_Ag_Contractor_ORG.Text;

                string sAddress = null;
                if (!string.IsNullOrEmpty(tbCreate_Ag_Contractor_Address.Text)) sAddress = tbCreate_Ag_Contractor_Address.Text;

                string sZip = null;
                if (!string.IsNullOrEmpty(ddlCreate_Ag_Contractor_Zip.SelectedValue)) sZip = ddlCreate_Ag_Contractor_Zip.SelectedValue;

                string sPhone = null;
                if (!string.IsNullOrEmpty(tbCreate_Ag_Contractor_Phone.Text)) sPhone = tbCreate_Ag_Contractor_Phone.Text;

                string sCell = null;
                if (!string.IsNullOrEmpty(tbCreate_Ag_Contractor_Cell.Text)) sCell = tbCreate_Ag_Contractor_Cell.Text;

                string sRegion = null;
                if (!string.IsNullOrEmpty(ddlCreate_Ag_Contractor_Region.SelectedValue)) sRegion = ddlCreate_Ag_Contractor_Region.SelectedValue;
                else sb.Append("Region is required. ");

                string sContractorType = null;
                if (!string.IsNullOrEmpty(ddlCreate_Ag_Contractor_ContractorType.SelectedValue)) sContractorType = ddlCreate_Ag_Contractor_ContractorType.SelectedValue;

                if (string.IsNullOrEmpty(sFNAME) && string.IsNullOrEmpty(sLNAME) && string.IsNullOrEmpty(sORG)) sb.Append("At least one of the following are required: First Name, Last Name, Organization. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wac.contractor_add_express_agriculture(sFNAME, sLNAME, sORG, sAddress, sZip, sPhone, sCell, sRegion, sContractorType, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        tbCreate_Ag_Contractor_FNAME.Text = "";
                        tbCreate_Ag_Contractor_LNAME.Text = "";
                        tbCreate_Ag_Contractor_ORG.Text = "";
                        tbCreate_Ag_Contractor_Address.Text = "";
                        ddlCreate_Ag_Contractor_Zip.SelectedIndex = 0;
                        tbCreate_Ag_Contractor_Phone.Text = "";
                        tbCreate_Ag_Contractor_Cell.Text = "";
                        ddlCreate_Ag_Contractor_Region.SelectedIndex = 0;
                        ddlCreate_Ag_Contractor_ContractorType.SelectedIndex = 0;

                        string contractorName = string.Empty;
                        string newConFolderName = string.Empty;
                        string contractorDocHome = @"J:\CONTRACTORS\Contractor_docs\";
                        string newContractorTemplate = @"~NewContractorTemplate\";

                        if (string.IsNullOrEmpty(sORG))
                        {
                            contractorName = sFNAME + " " + sLNAME;
                            newConFolderName = contractorName.Replace(@"/", " ");
                        }
                        else
                        {
                            contractorName = sORG;
                        }

                        Directory.CreateDirectory(contractorDocHome + newConFolderName);

                        foreach (string dir in Directory.GetDirectories(contractorDocHome + newContractorTemplate))
                        {
                            Directory.CreateDirectory(dir.Replace(@"~NewContractorTemplate", contractorName));
                        }

                        WACAlert.Show("Successfully Created Ag Contractor", 0);
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    #endregion

    #region Control Population - Participant - WAC

    private void PopulateDDL4WACEmployeeParticipants()
    {
        DropDownList ddl = fvWACEmployee.FindControl("ddlWACEmployees") as DropDownList;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.participants.Where(w => w.participantInterests.First(f => f.list_interestType.list_interest.interest == "WAC Employee").list_interestType.list_interest.interest == "WAC Employee").OrderBy(o => o.fullname_LF_dnd)
                    select new { b.pk_participant, b.fullname_LF_dnd };
            var x = from y in wDataContext.participantWACs select new { y.fk_participant };
            foreach (var c in a)
            {
                bool bCanAdd = true;
                foreach (var z in x)
                {
                    if (c.pk_participant == z.fk_participant) bCanAdd = false;
                }
                if (bCanAdd) ddl.Items.Add(new ListItem(c.fullname_LF_dnd, c.pk_participant.ToString()));
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
        if (ddl.Items.Count == 1)
        {
            WACAlert.Show("All Participants with an interest of WAC Employee have already been added.", 0);
            fvWACEmployee.ChangeMode(FormViewMode.ReadOnly);
            BindParticipantWAC(-1);
        }
    }

    #endregion

    #region Data Binding - Participant

    private void BindParticipants()
    {
        try
        {
            gvParticipant.DataKeyNames = new string[] { "pk_participant" };
            gvParticipant.DataSource = Session["results"];
            gvParticipant.DataBind();
        }
        catch { }
        if (ViewState["SelectedValue"] != null)
        {
            string sSelectedValue = (string)ViewState["SelectedValue"];
            foreach (GridViewRow gvr in gvParticipant.Rows)
            {
                string sKeyValue = gvParticipant.DataKeys[gvr.RowIndex].Value.ToString();
                if (sKeyValue == sSelectedValue)
                {
                    gvParticipant.SelectedIndex = gvr.RowIndex;
                    return;
                }
                else gvParticipant.SelectedIndex = -1;
            }
        }
        try 
        { 
            if (Convert.ToInt32(Session["count"]) > 0) lblCount.Text = "Records: " + Session["count"];
            else lblCount.Text = "Records: 0";
        }
        catch {  }

        if (gvParticipant.Rows.Count == 1)
        {
            gvParticipant.SelectedIndex = 0;
            gvParticipant.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
            fvParticipant.ChangeMode(FormViewMode.ReadOnly);
            BindParticipant(Convert.ToInt32(gvParticipant.SelectedDataKey.Value));
            if (gvParticipant.SelectedIndex != -1) ViewState["SelectedValueAgs"] = gvParticipant.SelectedValue.ToString();
        }
    }

    private void BindParticipant(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.participants.Where(w => w.pk_participant == i).Select(s => s);
            fvParticipant.DataKeyNames = new string[] { "pk_participant" };
            fvParticipant.DataSource = a;
            fvParticipant.DataBind();

            if (a.Any())
                ParticipantName = a.FirstOrDefault().fullname_FL_dnd;

            if (fvParticipant.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
               
                //this needs to be replaced when page is converted
                WACUT_Associations assoc = fvParticipant.FindControl("WACUT_Associations") as WACUT_Associations;
                List<WACParameter> parms = new List<WACParameter>();
                parms.Add(new WACParameter("pk_participant", i, WACParameter.ParameterType.SelectedKey));
                parms.Add(new WACParameter("pk_participant", i, WACParameter.ParameterType.PrimaryKey));
                assoc.InitControl(parms);
                Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)fvParticipant.FindControl("WACUT_AttachedDocumentViewer");
                parms.Clear();
                parms.Add(new WACParameter("pk_participant", i, WACParameter.ParameterType.MasterKey));
                docView.InitControl(parms);
               
                try { BindParticipantContractor(Convert.ToInt32(a.Single().participantContractors.Single().pk_participantContractor)); }
                catch { }

                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_DDL(fvParticipant.FindControl("ddlParticipantType") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ForestryCode_DDL(fvParticipant, "ddlParticipantForestryType", null, false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Interest_DDL(fvParticipant, "ddlParticipantInterest", null, true, a.Single().participantTypes);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Mailing_DDL(fvParticipant, "ddlParticipant_MailingParticipant_Insert", null);
            }

            if (fvParticipant.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Suffix_DDL(fvParticipant.FindControl("ddlParticipants_Suffix") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Gender_DDL(fvParticipant, "ddlGender", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Ethnicity_DDL(fvParticipant, "ddlEthnicity", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Race_DDL(fvParticipant, "ddlRace", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DiversityData_DDL(fvParticipant, "ddlDiversityData", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(fvParticipant, "ddlRegionWAC", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_DDL(fvParticipant.FindControl("ddlInitialParticipantType") as DropDownList, "P", false);
                WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvParticipant.FindControl("UC_Property_EditInsert_Participant") as UserControl, null);
                DropDownList ddlDBA = (DropDownList)fvParticipant.FindControl("ddlDBA");
                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantDBA_DDL(ddlDBA, null);
            }

            if (fvParticipant.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Prefix_DDL(fvParticipant, "ddlPrefix", a.Single().fk_prefix_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Suffix_DDL(fvParticipant.FindControl("ddlSuffix") as DropDownList, a.Single().fk_suffix_code, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(fvParticipant, "ddlRegionWAC", a.Single().fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant, "ddlActive", a.Single().active);
                WACGlobal_Methods.PopulateControl_DatabaseLists_MailingStatus_DDL(fvParticipant, "ddlMailingStatus", a.Single().fk_mailingStatus_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Gender_DDL(fvParticipant, "ddlGender", a.Single().fk_gender_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Ethnicity_DDL(fvParticipant, "ddlEthnicity", a.Single().fk_ethnicity_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Race_DDL(fvParticipant, "ddlRace", a.Single().fk_race_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DiversityData_DDL(fvParticipant, "ddlDiversityData", a.Single().fk_diversityData_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DataReview_DDL(fvParticipant.FindControl("ddlDataReview") as DropDownList, a.Single().fk_dataReview_code, true);
                DropDownList ddlDBA = (DropDownList)fvParticipant.FindControl("ddlDBA");
                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantDBA_DDL(ddlDBA, null);

                if (a.Single().fk_organization != null)
                {
                    DropDownList ddl = fvParticipant.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("ddl") as DropDownList;
                    Label lblLetter = fvParticipant.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("lblLetter") as Label;
                    string sLetter = null;
                    try { sLetter = a.Single().organization.org.Substring(0, 1); }
                    catch { }
                    WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lblLetter, sLetter, "ORGANIZATION", null, a.Single().fk_organization);
                }
            }


            Panel pnlParticipantTypeForestry = fvParticipant.FindControl("pnlParticipantTypeForestry") as Panel;
            if (pnlParticipantTypeForestry != null)
            {
                bool bPTF = false;
                foreach (participantType pt in a.Single().participantTypes)
                {
                    if (pt.fk_participantType_code == "F") bPTF = true;
                }
                if (bPTF) pnlParticipantTypeForestry.Visible = true;
                else pnlParticipantTypeForestry.Visible = false;
            }
            if (fvParticipant != null)
            {
                WACUT_Associations ucAssociations = fvParticipant.FindControl("ucWACUT_Associations") as WACUT_Associations;
            }

        }
    }

    private void ClearParticipants()
    {
        lblCount.Text = "";
        gvParticipant.DataSource = null;
        gvParticipant.DataBind();
        
    }

    private void ClearParticipant()
    {
        fvParticipant.ChangeMode(FormViewMode.ReadOnly);
        fvParticipant.DataSource = null;
        fvParticipant.DataBind();
        
    }

    #endregion

    #region Data Binding - Participant Communication

    private void BindParticipant_Communication(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.participantCommunications.Where(w => w.pk_participantCommunication == i).Select(s => s);
            fvParticipant_Communication.DataKeyNames = new string[] { "pk_participantCommunication" };
            fvParticipant_Communication.DataSource = a;
            fvParticipant_Communication.DataBind();

            if (fvParticipant_Communication.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationType_DDL(fvParticipant_Communication, "ddlType", null);
            }

            if (fvParticipant_Communication.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationType_DDL(fvParticipant_Communication, "ddlType", a.Single().fk_communicationType_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationUsage_DDL(fvParticipant_Communication, "ddlUsage", a.Single().fk_communicationUsage_code, a.Single().fk_communicationType_code);
            }
        }
    }

    #endregion

    #region Data Binding - Participant Property

    private void BindParticipant_Property(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.participantProperties.Where(w => w.pk_participantProperty == i) select b;
            fvParticipant_Property.DataKeyNames = new string[] { "pk_participantProperty" };
            fvParticipant_Property.DataSource = a;
            fvParticipant_Property.DataBind();

            if (fvParticipant_Property.CurrentMode == FormViewMode.Insert)
            {
                DropDownList ddlZipCode = (DropDownList)fvParticipant_Property.FindControl("ddlZipCode");
                WACGlobal_Methods.ZipCodesFromExistingAddresses(ddlZipCode);
            }

            if (fvParticipant_Property.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvParticipant_Property.FindControl("UC_Property_EditInsert_Participant_Property") as UserControl, a.Single().property);

                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant_Property, "ddlMaster", a.Single().master, false);
                DropDownList ddl = fvParticipant_Property.FindControl("UC_DropDownListByAlphabet_Participant_Property").FindControl("ddl") as DropDownList;
                Label lblLetter = fvParticipant_Property.FindControl("UC_DropDownListByAlphabet_Participant_Property").FindControl("lblLetter") as Label;
                string sLetter = null;
                try { sLetter = a.Single().participant1.lname.Substring(0, 1); }
                catch { }
                WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lblLetter, sLetter, "PARTICIPANT", "ALL", a.Single().fk_participant_cc);
            }
        }
    }

    #endregion

    #region Data Binding - Participant Organization

    private void BindParticipant_Organization(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.participantOrganizations.Where(w => w.pk_participantOrganization == i) select b;
            fvParticipant_Organization.DataKeyNames = new string[] { "pk_participantOrganization" };
            fvParticipant_Organization.DataSource = a;
            fvParticipant_Organization.DataBind();

            if (fvParticipant_Organization.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant_Organization, "ddlMaster", "N", false);
            }

            if (fvParticipant_Organization.CurrentMode == FormViewMode.Edit)
            {
                DropDownList ddl = fvParticipant_Organization.FindControl("UC_DropDownListByAlphabet_Participant_Organization").FindControl("ddl") as DropDownList;
                Label lblLetter = fvParticipant_Organization.FindControl("UC_DropDownListByAlphabet_Participant_Organization").FindControl("lblLetter") as Label;
                string sLetter = null;
                try { sLetter = a.Single().organization.org.Substring(0, 1); }
                catch { }
                WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lblLetter, sLetter, "ORGANIZATION", "ALL", a.Single().fk_organization);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant_Organization, "ddlMaster", a.Single().master, false);
            }
        }
    }

    #endregion

    #region Data Binding - Participant Contractor

    private void BindParticipantContractor(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            FormView fvContractor = fvParticipant.FindControl("fvContractor") as FormView;
            var a = from b in wDataContext.participantContractors.Where(w => w.pk_participantContractor == i)
                    select b;
            fvContractor.DataKeyNames = new string[] { "pk_participantContractor" };
            fvContractor.DataSource = a;
            fvContractor.DataBind();

            if (fvContractor.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_ContractorType_DDL(fvContractor, "ddlAg_Participant_Contractor_Type_Add", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvContractor, "ddlAg_Participant_Contractor_Vendex_Add", null);
            }

            if (fvContractor.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvContractor, "ddlActive", a.Single().active);
                WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(fvContractor, "ddlWACRegion", a.Single().fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvContractor, "ddlLandowner", a.Single().landowner);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvContractor, "ddlIsSupplier", a.Single().isSupplier);
                //WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvContractor, "UC_EditCalendar_Participant_Contractor_VendexDate", a.Single().vendex_date, null);
                //WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvContractor, "UC_EditCalendar_Participant_Contractor_WorkersCompStart", a.Single().workmensComp_start, null);
                //WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvContractor, "UC_EditCalendar_Participant_Contractor_WorkersCompEnd", a.Single().workmensComp_end, null);
                //WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvContractor, "UC_EditCalendar_Participant_Contractor_LiabilityInsuranceStart", a.Single().liabilityIns_start, null);
                //WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvContractor, "UC_EditCalendar_Participant_Contractor_LiabilityInsuranceEnd", a.Single().liabilityIns_end, null);
            }
        }
    }

    #endregion

    #region Data Binding - Participant Note

    private void BindParticipant_Note(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.participantNotes.Where(w => w.pk_participantNote == i) select b;
            fvParticipant_Note.DataKeyNames = new string[] { "pk_participantNote" };
            fvParticipant_Note.DataSource = a;
            fvParticipant_Note.DataBind();

            var x = wDataContext.participants.Where(w => w.pk_participant == Convert.ToInt32(fvParticipant.DataKey.Value)).Select(s => new { s.participantTypes });

            if (fvParticipant_Note.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_ByParticipantTypeCollection_DDL(fvParticipant_Note, "ddlNoteType", x.Single().participantTypes, null);
            }

            if (fvParticipant_Note.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_ByParticipantTypeCollection_DDL(fvParticipant_Note, "ddlNoteType", x.Single().participantTypes, a.Single().fk_participantType_code);
            }
        }
    }

    #endregion

    #region Data Binding - Participant - WAC

    private void BindParticipantWAC(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.participantWACs.Where(w => w.pk_participantWAC == i) select b;
            fvWACEmployee.DataKeyNames = new string[] { "pk_participantWAC" };
            fvWACEmployee.DataSource = a;
            fvWACEmployee.DataBind();


            if (fvWACEmployee.CurrentMode == FormViewMode.Insert)
            {
                //WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvWACEmployee, "ddlActive", "Y");
                //WACGlobal_Methods.PopulateControl_DatabaseLists_SectorWAC_DDL(fvWACEmployee, "ddlWACSector", null);
                //WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvWACEmployee, "UC_EditCalendar_Participant_DOB", null, 1930);
                PopulateDDL4WACEmployeeParticipants();
            }

            if (fvWACEmployee.CurrentMode == FormViewMode.Edit)
            {
                //WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvWACEmployee, "ddlActive", a.Single().active);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_SectorWAC_DDL(fvWACEmployee, "ddlWACSector", a.Single().fk_sectorWAC_code);
               // WACGlobal_Methods.PopulateControl_Generic_CalendarAndDDL(fvWACEmployee, "UC_EditCalendar_Participant_DOB", a.Single().dob, 1930);
            }
        }
    }

    #endregion

    protected void WACUT_Associations_ContentStateChanged(object sender, UserControlResultEventArgs e)
    {
        WACListControl associations = (WACListControl)sender;
        associations.UpdateControl(e.Parms);
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.UpdatePanelUpdate;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }

    public event EventHandler<UserControlResultEventArgs> ContentStateChanged;

    public void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    public void UpdatePanelUpdate()
    {
        //throw new NotImplementedException();
    }

    public List<WACParameter> GetContents()
    {
        throw new NotImplementedException();
    }


    public void AdjustContentVisibility(List<WACControl> ContainedControls, WACFormControl form)
    {
        throw new NotImplementedException();
    }

    protected void lbParticipantCreate_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            ChangeIndex2Zero4SearchDDLs(true);
            ClearParticipants();
            //fvParticipant.ChangeMode(FormViewMode.Insert);
            Session["searchType"] = "";
            BindParticipant(-1);

            WACPT_ParticipantCreate.LoadMe();

        }
    }

    protected void WACPT_ParticipantCreate_Inserted(object sender, EventArgs e)
    {
        if (WACPT_ParticipantCreate.pk_participant > 0)
        {
            Session["order"] = "";
            Session["searchKey"] = WACPT_ParticipantCreate.pk_participant.ToString();
            Session["searchType"] = "P";
            Response.Redirect("WACPT_Participants.aspx", true);
        }
               
    }


    protected void ddlZipCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        Control nc = ddl.NamingContainer;
        TextBox tbAddressStartsWith = (TextBox)nc.FindControl("tbAddressStartsWith");
        Button button = (Button)nc.FindControl("FindStartsWith");
        DropDownList ddlAddress = (DropDownList)nc.FindControl("ddlAddress");
        tbAddressStartsWith.Visible = true;
        button.Visible = true;
        ddlAddress.Visible = true;
    }
    protected void FindStartsWith_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        TextBox startsWith = (TextBox)button.NamingContainer.FindControl("tbAddressStartsWith");
        DropDownList ddlAddress = (DropDownList)button.NamingContainer.FindControl("ddlAddress");
        DropDownList ddlZipCode = (DropDownList)button.NamingContainer.FindControl("ddlZipCode");
        WACGlobal_Methods.AddressStartsWith(ddlAddress, ddlZipCode.SelectedItem.Text, startsWith.Text);
        ddlAddress.Visible = true;
    }
    //protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //    DropDownList ddl = (DropDownList)sender;
    //    HiddenField hf = (HiddenField)ddl.NamingContainer.FindControl("hfPropertyPK");
    //    hf.Value = ddl.SelectedValue;
    //}
}
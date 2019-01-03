using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_CustomControls;
using WAC_DataObjects;
using WAC_Extensions;
using Telerik.Web.UI;


public partial class WACAgriculture : System.Web.UI.Page
{  
    public RadWindowManager WacRadWindowManager
    {
        get
        {
            return (RadWindowManager)this.Master.FindControl("WacRadWindowManager");
        }
    }
    #region Constants


    private const int _tabFarmLand = 1;
    private const int _tabFarmOperation = 2;
    private const int _tabOandM = 3;
    private const int _tabFarmStatus = 4;
    private const int _tabAnimal = 5;
    private const int _tabASR = 6;
    private const int _tabBMP = 7;
    private const int _tabCropware = 8;
    private const int _tabLandBase = 9;
    private const int _tabNote = 10;
    private const int _tabNMP = 11;
    private const int _tabTier1 = 12;
    private const int _tabWFP2 = 13;
    private const int _tabWFP3 = 14;
    private const int _tabWLProject = 15;

    #endregion

    #region Session Properties
    public int PK_FarmBusiness
    {
        get { return Convert.ToInt32(Session["PK_FarmBusiness"]) == 0 ? -1 : Convert.ToInt32(Session["PK_FarmBusiness"]); }
        set { Session["PK_FarmBusiness"] = value; }
    }
    public int PK_FormWfp3
    {
        get { return Convert.ToInt32(Session["PK_FormWfp3"]) == 0 ? -1 : Convert.ToInt32(Session["PK_FormWfp3"]); }
        set { Session["PK_FormWfp3"] = value; }
    }
  
    public string PracticeBMP 
    {
        get { return Session["PracticeBMP"] == null ? " " : Session["PracticeBMP"] as string; }
        set { Session["PracticeBMP"] = value; }
    }
    public IList<DepTaxParcel> TaxParcels 
    { 
        get { return Session["TaxParcels"] == null ? null : (IList<DepTaxParcel>)Session["TaxParcels"]; }
        set { Session["TaxParcels"] = value;  } 
    }
    public bool CloneBMP
    {
        get { return Session["CloneBMP"] == null ? false : (bool)Session["CloneBMP"]; }
        set { Session["CloneBMP"] = value; }
    }
    public bool LimitBMPList
    {
        get { return Session["LimitBMPList"] == null ? false : (bool)Session["LimitBMPList"]; }
        set { Session["LimitBMPList"] = value; }
    }
    # endregion

    #region Delegates
    public int Delegate_GetFarmBusinessPK()
    {
        return Convert.ToInt32(fvAg.DataKey.Value);
    }

    public int Delegate_GetASRPK()
    {
        return Convert.ToInt32(fvAg_ASR.DataKey.Value);
    }

    public int Delegate_GetBMPPK()
    {
        return Convert.ToInt32(fvAg_BMP.DataKey.Value);
    }

    public int Delegate_GetFormWFP2PK()
    {
        return Convert.ToInt32(fvAg_WFP2.DataKey.Value);
    }

    public int Delegate_GetFormWFP3PK()
    {
        return PK_FormWfp3;
    }
   
    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show(WacRadWindowManager,"Could not open Global Insert Express Window.", 0); }
    }

    public void Participant_ViewEditInsertWindow(object oPK_Participant)
    {
        try { UC_Express_Participant1.LoadFormView_Participant(Convert.ToInt32(oPK_Participant)); }
        catch { WACAlert.Show(WacRadWindowManager,"Could not open Participant Window.", 0); }
    }

    public void Property_ViewEditInsertWindow(object oPK_Property)
    {
        try 
        { 
            UC_Express_Property.LoadFormView_Property(Convert.ToInt32(oPK_Property)); 
        }
        catch { WACAlert.Show(WacRadWindowManager,"Could not open Property Window.", 0); }
    }

    public void InvokedMethod_SectionPage_RebindRecord()
    {
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));
        //upAgs.Update();
        //upAgSearch.Update();
    }

    public void InvokedMethod_ControlGroup_RebindRecord(int iPK_Participant)
    {
        UC_Express_Participant1.BindParticipant(iPK_Participant);
    }

    public void InvokedMethod_ControlGroup_TaxParcel(object oType)
    {
        //FarmBusinessTaxParcel_Insert();
    }

    public void InvokedMethod_DropDownListByAlphabet(object oType)
    {
        if (oType != null)
        {
            switch (oType.ToString())
            {
                case "AGRICULTURE_FARMOWNER_SEARCH": Search_FarmOwner_SelectedIndexChanged(); break;
                case "AGRICULTURE_FARMOWNEROPERATOR_SEARCH": Search_FarmOwner_SelectedIndexChanged(); break;
            }
        }
    }

    #endregion    

    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["searchTypeAgs"] = "";
            Session["searchKeyAgs"] = "";
            Session["resultsAgs"] = "";
            Session["orderAgs"] = "";
            Session["countAgs"] = "";
            Session["PK_FarmBusiness"] = "";
            PopulateDDLs4Searching();

            string sQS_PK = Request.QueryString["pk"];
            if (!string.IsNullOrEmpty(sQS_PK)) BindAg(Convert.ToInt32(sQS_PK));

            string sQS_TC = Request.QueryString["tc"];
            if (!string.IsNullOrEmpty(sQS_TC)) SwitchTabContainerTab(HandleQueryString_TC(sQS_TC));

            hlAg_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Agriculture General.pdf";
            hlAg_Help.ImageUrl = "~/images/help_24.png";

            AjaxControlToolkit.TabContainer tc = fvAg.FindControl("tcAg") as AjaxControlToolkit.TabContainer;
            if (tc != null)
            {
                BindAgOnDemand(tc);
            }

        }

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        
    }

    private int HandleQueryString_TC(string sTC)
    {
        int i = 0;
        switch (sTC)
        {
            case "FarmOperation": i = _tabFarmOperation; break;
            case "BMP": i = _tabBMP; break;
            case "WFP3": i = _tabWFP3; break;
        }
        return i;
    }

    private void PopulateDDLs4Searching()
    {
        try
        {
            PopulateDDL4FarmID();
            PopulateDDL4FarmName();
            WACGlobal_Methods.View_Agriculture_SupplementalAgreement_DDL(ddlAg_Search_SA, null, true);          
        }
        catch (Exception ex) {  WACAlert.Show(WacRadWindowManager,"Page loading error in Agriculture: " + ex.Message, 0); }
    }

    private void PopulateDDL4FarmID()
    {
        ddlAg_Search_FarmID.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinesses.Where(w => w.farmID != null).OrderBy(o => o.farmID)
                    select new { b.pk_farmBusiness, b.farmID };
            foreach (var c in a)
            {
                ddlAg_Search_FarmID.Items.Add(new ListItem(c.farmID, c.pk_farmBusiness.ToString()));
            }
            ddlAg_Search_FarmID.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateDDL4FarmName()
    {
        ddlAg_Search_FarmName.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinesses.Where(w => w.farm_name != "").OrderBy(o => o.farm_name)
                    select new { b.pk_farmBusiness, b.farm_name };
            foreach (var c in a)
            {
                ddlAg_Search_FarmName.Items.Add(new ListItem(c.farm_name, c.pk_farmBusiness.ToString()));
            }
            ddlAg_Search_FarmName.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    #endregion

    #region WACEnumerations

    public enum Enum_Ag_RebindableSections { farmBusinessMail, farmBusinessOperator, farmBusinessOwner, farmBusinessPlanner }

    #endregion

    #region Event Handling - Search

    private void HandleQueryType()
    { 
        upAgSearchPlaceHolder.Visible = false;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {   
            switch (Session["searchTypeAgs"].ToString())
            {
                case "A":
                    var aA = from b in wDataContext.farmBusinesses
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aA.Count();
                    Session["resultsAgs"] = aA;
                    Session["PK_FarmBusiness"] = aA.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aA.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "B":
                    var aB = from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == Convert.ToInt32(Session["searchKeyAgs"]))
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aB.Count();
                    Session["resultsAgs"] = aB;
                    Session["PK_FarmBusiness"] = aB.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aB.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "C":
                    var aC = from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == Convert.ToInt32(Session["searchKeyAgs"]))
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aC.Count();
                    Session["resultsAgs"] = aC;
                    Session["PK_FarmBusiness"] = aC.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aC.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "D":
                    object ska = Session["searchKeyAgs"];
                    int fk = Convert.ToInt32(Session["searchKeyAgs"]);
                    //var aD = wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == pk ).OrderBy(o => o.farmID).ThenBy(o => o.farm_name).Select(s => new { s.pk_farmBusiness, s.farmID, s.farm_name, s.list_farmSize, s.farmBusinessOwners, s.farmLand, s.list_status });
                    var aaD = from f in wDataContext.farmBusinesses
                             join o in wDataContext.vw_farmBusiness_ownerOperators on f.pk_farmBusiness equals o.pk_farmBusiness
                             where o.fk_participant == fk
                             orderby f.farmID, f.farm_name
                             select new { f.pk_farmBusiness, f.farmID, f.farm_name, f.list_farmSize, f.farmBusinessOwners, f.farmLand, f.list_status, o.fk_participant };
                    var aD = aaD.Distinct((x,y) => x.pk_farmBusiness == y.pk_farmBusiness).Select(s => s).ToList();
                    Session["countAgs"] = aD.Count();
                    Session["resultsAgs"] = aD;
                    Session["PK_FarmBusiness"] = aD.First().pk_farmBusiness;
                   // if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aD.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "F":
                    var aF = from b in wDataContext.farmBusinesses.Where(w => w.farmLand.property.city == Session["searchKeyAgs"].ToString()).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aF.Count();
                    Session["resultsAgs"] = aF;
                    Session["PK_FarmBusiness"] = aF.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aF.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "G":
                    var aG = from b in wDataContext.farmBusinesses.Where(w => w.farmLand.property.fk_list_townshipNY == Convert.ToInt32(Session["searchKeyAgs"])).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aG.Count();
                    Session["resultsAgs"] = aG;
                    Session["PK_FarmBusiness"] = aG.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aG.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "H":
                    var aH = from b in wDataContext.farmBusinesses.Where(w => w.farmLand.property.fk_list_countyNY == Convert.ToInt32(Session["searchKeyAgs"])).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aH.Count();
                    Session["resultsAgs"] = aH;
                    Session["PK_FarmBusiness"] = aH.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aH.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "I":
                    var aI = from b in wDataContext.farmBusinesses.Where(w => w.farmLand.property.fk_basin_code == Session["searchKeyAgs"].ToString()).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aI.Count();
                    Session["resultsAgs"] = aI;
                    Session["PK_FarmBusiness"] = aI.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aI.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "J":
                    var aJ = from b in wDataContext.farmBusinesses.Where(w => w.fk_programWAC_code == Session["searchKeyAgs"].ToString()).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aJ.Count();
                    Session["resultsAgs"] = aJ;
                    Session["PK_FarmBusiness"] = aJ.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aJ.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "K":
                    var aK = from b in wDataContext.farmBusinesses.Where(w => w.fk_farmSize_code == Session["searchKeyAgs"].ToString()).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aK.Count();
                    Session["resultsAgs"] = aK;
                    Session["PK_FarmBusiness"] = aK.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aK.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "L":
                    var aL = wDataContext.farmBusinesses.Where(w => w.fk_status_code_curr == Session["searchKeyAgs"].ToString()).OrderBy(o => o.farmID).ThenBy(o => o.farm_name).Select(s => new { s.pk_farmBusiness, s.farmID, s.farm_name, s.list_farmSize, s.farmBusinessOwners, s.farmLand, s.list_status });
                    Session["countAgs"] = aL.Count();
                    Session["resultsAgs"] = aL;
                    Session["PK_FarmBusiness"] = aL.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aL.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "M":
                    var aM = from b in wDataContext.farmBusinesses.Where(w => w.farmBusinessOperators.First(f => f.participant.pk_participant == Convert.ToInt32(Session["searchKeyAgs"])).participant.pk_participant == Convert.ToInt32(Session["searchKeyAgs"])).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aM.Count();
                    Session["resultsAgs"] = aM;
                    Session["PK_FarmBusiness"] = aM.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aM.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "N":
                    var aN = from b in wDataContext.farmBusinesses.Where(w => w.fk_regionWAC_code == Session["searchKeyAgs"].ToString()).OrderBy(o => o.farmID).ThenBy(o => o.farm_name)
                             select new { b.pk_farmBusiness, b.farmID, b.farm_name, b.list_farmSize, b.farmBusinessOwners, b.farmLand, b.list_status };
                    Session["countAgs"] = aN.Count();
                    Session["resultsAgs"] = aN;
                    Session["PK_FarmBusiness"] = aN.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aN.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                case "O":
                    var aO = wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == Convert.ToInt32(Session["searchKeyAgs"])).OrderBy(o => o.farmID).ThenBy(o => o.farm_name).Select(s => new { s.pk_farmBusiness, s.farmID, s.farm_name, s.list_farmSize, s.farmBusinessOwners, s.farmLand, s.list_status });
                    Session["countAgs"] = aO.Count();
                    Session["resultsAgs"] = aO;
                    Session["PK_FarmBusiness"] = aO.First().pk_farmBusiness;
                    if (!string.IsNullOrEmpty(Session["orderAgs"].ToString())) Session["resultsAgs"] = aO.OrderBy(Session["orderAgs"].ToString(), null);
                    break;
                default:
                    Session["countAgs"] = 0;
                    Session["resultsAgs"] = "";
                    Session["orderAgs"] = "";
                    Session["PK_FarmBusiness"] = 0;
                    break;
            }
            BindAgs();
        }
        
    }

    private void ChangeIndex2Zero4SearchDDLs(bool bResetUC)
    {
        gvAg.SelectedIndex = -1;
        PK_FarmBusiness = -1;
        ClearAg();
        try { ddlAg_Search_FarmID.SelectedIndex = 0; }
        catch { }
        try { ddlAg_Search_FarmName.SelectedIndex = 0; }
        catch { }
        try { if (bResetUC) UC_DropDownListByAlphabet_Search_FarmOwner.ResetControls(); }
        catch { }

        try { ddlAg_Search_SA.SelectedIndex = 0; }
        catch { }

    }

    protected void lbAg_Search_ReloadReset_Click(object sender, EventArgs e)
    {
        ChangeIndex2Zero4SearchDDLs(true);
        ClearAgs();
        upAgSearchPlaceHolder.Visible = true;
        foundFarmGridPlaceholder.Visible = true;
    }

    protected void lbAgAll_Click(object sender, EventArgs e)
    {
        Session["orderAgs"] = "";
        Session["searchTypeAgs"] = "A";
        Session["searchKeyAgs"] = "";
        ChangeIndex2Zero4SearchDDLs(true);
        HandleQueryType();
    }

    protected void ddlAg_Search_FarmID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlAg_Search_FarmID.SelectedValue))
        {
            
            Session["orderAgs"] = "";
            Session["searchTypeAgs"] = "B";
            Session["searchKeyAgs"] = ddlAg_Search_FarmID.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            ddlAg_Search_FarmID.SelectedValue = Session["searchKeyAgs"].ToString();
            HandleQueryType();
        }
    }

    protected void ddlAg_Search_FarmName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlAg_Search_FarmName.SelectedValue))
        {
     
            Session["orderAgs"] = "";
            Session["searchTypeAgs"] = "C";
            Session["searchKeyAgs"] = ddlAg_Search_FarmName.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            ddlAg_Search_FarmName.SelectedValue = Session["searchKeyAgs"].ToString();
            HandleQueryType();
        }
    }

    private void Search_FarmOwner_SelectedIndexChanged()
    { 
        DropDownList ddl = UC_DropDownListByAlphabet_Search_FarmOwner.FindControl("ddl") as DropDownList;
        Session["orderAgs"] = "";
        Session["searchTypeAgs"] = "D";
        Session["searchKeyAgs"] = ddl.SelectedValue;
        ChangeIndex2Zero4SearchDDLs(false);
        ddl.SelectedValue = Session["searchKeyAgs"].ToString();
        HandleQueryType();
        upAgs.Update();
    }

    protected void ddlAg_Search_SA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlAg_Search_SA.SelectedValue))
        {         
            Session["orderAgs"] = "";
            Session["searchTypeAgs"] = "O";
            Session["searchKeyAgs"] = ddlAg_Search_SA.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            ddlAg_Search_SA.SelectedValue = Session["searchKeyAgs"].ToString();
            HandleQueryType();
        }
    }

    #endregion

    #region Event Handling - Results

    protected void gvAg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAg.PageIndex = e.NewPageIndex;
        HandleQueryType();
    }

    protected void gvAg_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderAgs"] = e.SortExpression;
        HandleQueryType();
    }

    protected void gvAg_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvAg.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        fvAg.ChangeMode(FormViewMode.ReadOnly);       
        if (gvAg.SelectedIndex != -1)
            PK_FarmBusiness = Convert.ToInt32(gvAg.SelectedDataKey.Value);
        BindAg(PK_FarmBusiness);

    }

    private void SwitchTabContainerTab(int i)
    {
        AjaxControlToolkit.TabContainer tc = fvAg.FindControl("tcAg") as AjaxControlToolkit.TabContainer;
        tc.ActiveTabIndex = i;
    }

    #endregion

    #region Event Handling - Ag
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
    protected void btnInsertAddress_Click(object sender, EventArgs e)
    {
        Control container = ((Button)sender).NamingContainer;
        HiddenField hfPropertyPK = (HiddenField)container.FindControl("hfPropertyPK");
        DropDownList ddlAddress = (DropDownList)container.FindControl("ddlAddress");
        Label lblCurrentAddress = (Label)container.FindControl("lblCurrentAddress");
        if (!string.IsNullOrEmpty(ddlAddress.SelectedValue))
        {
            hfPropertyPK.Value = ddlAddress.SelectedValue;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var addr = wac.properties.Where(w => w.pk_property == Convert.ToInt32(ddlAddress.SelectedValue)).Select(s => new {s.addressFull, s.csz_ro });
                if (addr.Any())
                {
                    lblCurrentAddress.Text = WACGlobal_Methods.SpecialText_Global_Address(addr.First().addressFull, addr.First().csz_ro);
                }
            }
        }
    }
    protected void tcAg_ActiveTabChanged(object sender, EventArgs e)
    {
        BindAgOnDemand(sender);
    }

    protected void lbAgClose_Click(object sender, EventArgs e)
    {
        ClearAg();
        ClearAgs();
        foundFarmGridPlaceholder.Visible = true;
        PlaceHolder search = (PlaceHolder)upAgSearch.FindControl("upAgSearchPlaceHolder");
        search.Visible = true;
        gvAg.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        PopulateDDLs4Searching();
        HandleQueryType();
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAgAdd_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusiness", "msgInsert"))
        {
            ChangeIndex2Zero4SearchDDLs(true);
            ClearAgs();

            fvAg.ChangeMode(FormViewMode.Insert);
            BindAg(-1);
            Session["searchTypeAgs"] = "";
        }
    }

    protected void lbAg_SetFarmID_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string s = string.Empty;
                int iCode = 0;
                iCode = wDataContext.farmBusiness_set_farmID(Convert.ToInt32(lb.CommandArgument), ref s);
                if (iCode == 0)
                {
                    BindAg(Convert.ToInt32(lb.CommandArgument));
                }
                else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void imgbtnAg_Phone_Click(object sender, EventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var x = wac.vw_farmBusiness_phoneCells.Where(w => w.pk_farmBusiness == Convert.ToInt32(imgbtn.CommandArgument)).OrderByDescending(o => o.Mode).ThenBy(o => o.Participant);
            lvAg_OwnerOperator_Phone.DataSource = x;
            lvAg_OwnerOperator_Phone.DataBind();
        }
        mpeAg_OwnerOperator_Phone.Show();
        upAg_OwnerOperator_Phone.Update();
    }

    protected void lbAg_OwnerOperator_Phone_Close_Click(object sender, EventArgs e)
    {
        mpeAg_OwnerOperator_Phone.Hide();
    }

    protected void fvAg_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusiness", "msgUpdate");
        if (bChangeMode)
        {
            fvAg.ChangeMode(e.NewMode);
            BindAg(Convert.ToInt32(fvAg.DataKey.Value));
        }
    }

    protected void fvAg_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sbErrorCollection = new StringBuilder();

        TextBox tbFarmName = fvAg.FindControl("tbFarmName") as TextBox;
        DropDownList ddlRegionWAC = fvAg.FindControl("ddlRegionWAC") as DropDownList;
        DropDownList ddlProgramWAC = fvAg.FindControl("ddlProgramWAC") as DropDownList;
        CustomControls_AjaxCalendar tbCalWFP0SignedDate = fvAg.FindControl("tbCalWFP0SignedDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalWFP1SignedDate = fvAg.FindControl("tbCalWFP1SignedDate") as CustomControls_AjaxCalendar;
        DropDownList ddlASRRequired = fvAg.FindControl("ddlASRRequired") as DropDownList;
        DropDownList ddlFarmSize = fvAg.FindControl("ddlFarmSize") as DropDownList;
        DropDownList ddlForestry = fvAg.FindControl("ddlForestry") as DropDownList;
        DropDownList ddlFarmToMarket = fvAg.FindControl("ddlFarmToMarket") as DropDownList;
        TextBox tbPriorLargeFarmID = fvAg.FindControl("tbPriorLargeFarmID") as TextBox;
        TextBox tbsAgreementYear = fvAg.FindControl("tbAgreementYear") as TextBox;
        DropDownList ddlSoldFarm = fvAg.FindControl("ddlSoldFarm") as DropDownList;
        TextBox tbFarms = fvAg.FindControl("tbFarms") as TextBox;
        TextBox tbSubfarms = fvAg.FindControl("tbSubfarms") as TextBox;
        DropDownList ddlGroupPI = fvAg.FindControl("ddlGroupPI") as DropDownList;
        DropDownList ddlEnvironmentalImpact = fvAg.FindControl("ddlEnvironmentalImpact") as DropDownList;
        DropDownList ddlIAPriorToImplementation = fvAg.FindControl("ddlIAPriorToImplementation") as DropDownList;
        DropDownList ddlPriorImplementationCommenced = fvAg.FindControl("ddlPriorImplementationCommenced") as DropDownList;

        CustomControls_AjaxCalendar tbCalISCDate = fvAg.FindControl("tbCalISCDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalIFCDate = fvAg.FindControl("tbCalIFCDate") as CustomControls_AjaxCalendar;
        TextBox tbStatusComment = fvAg.FindControl("tbStatusComment") as TextBox;
        TextBox tbPriorOwner = fvAg.FindControl("tbPriorOwner") as TextBox;
        
        HiddenField hfPropertyPK = fvAg.FindControl("hfPropertyPK") as HiddenField;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).Select(s => s).Single();
            try
            {

                if (!string.IsNullOrEmpty(tbFarmName.Text)) a.farm_name = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbFarmName.Text, 48);
                else a.farm_name = null;

                if (!string.IsNullOrEmpty(ddlRegionWAC.SelectedValue)) a.fk_regionWAC_code = ddlRegionWAC.SelectedValue;
                else a.fk_regionWAC_code = null;

                if (!string.IsNullOrEmpty(ddlProgramWAC.SelectedValue)) a.fk_programWAC_code = ddlProgramWAC.SelectedValue;
                else a.fk_programWAC_code = null;

                a.wfp0_signed = tbCalWFP0SignedDate.CalDateNullable;
                a.wfp1_signed_date = tbCalWFP1SignedDate.CalDateNullable;

                if (!string.IsNullOrEmpty(ddlFarmSize.SelectedValue)) a.fk_farmSize_code = ddlFarmSize.SelectedValue;
                else a.fk_farmSize_code = null;

                if (!string.IsNullOrEmpty(ddlForestry.SelectedValue)) a.forestry = ddlForestry.SelectedValue;
                else a.forestry = null;

                if (!string.IsNullOrEmpty(ddlFarmToMarket.SelectedValue)) a.farmToMarket = ddlFarmToMarket.SelectedValue;
                else a.farmToMarket = null;

                if (!string.IsNullOrEmpty(tbPriorLargeFarmID.Text)) a.prior_LF_FarmID = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPriorLargeFarmID.Text, 16);
                else a.prior_LF_FarmID = null;

                if (!string.IsNullOrEmpty(ddlSoldFarm.SelectedValue)) a.sold_farm = ddlSoldFarm.SelectedValue;
                else a.sold_farm = null;

                try { if (!string.IsNullOrEmpty(tbFarms.Text)) a.farm_cnt = Convert.ToInt16(tbFarms.Text); }
                catch { sbErrorCollection.Append("Farm Count was not updated due to invalid input text. Must be a number. "); }

                try { if (!string.IsNullOrEmpty(tbSubfarms.Text)) a.subfarm_cnt = Convert.ToInt16(tbSubfarms.Text); }
                catch { sbErrorCollection.Append("Subfarm Count was not updated due to invalid input text. Must be a number. "); }

                try
                {
                    if (!string.IsNullOrEmpty(ddlGroupPI.SelectedValue)) a.fk_groupPI_code = ddlGroupPI.SelectedValue;
                    else a.fk_groupPI_code = null;
                }
                catch { sbErrorCollection.Append("Group PI was not updated. "); }

                if (!string.IsNullOrEmpty(ddlEnvironmentalImpact.SelectedValue)) a.fk_environmentalImpact_code = ddlEnvironmentalImpact.SelectedValue;
                else a.fk_environmentalImpact_code = null;

                if (!string.IsNullOrEmpty(ddlIAPriorToImplementation.SelectedValue)) a.IA_prior_to_implementation = ddlIAPriorToImplementation.SelectedValue;

                if (!string.IsNullOrEmpty(ddlPriorImplementationCommenced.SelectedValue)) a.prior_implementation_commenced = ddlPriorImplementationCommenced.SelectedValue;

                a.implementation_substantially_complete_date = tbCalISCDate.CalDateNullable;

                a.implementation_fully_complete_date = tbCalIFCDate.CalDateNullable;

                if (!string.IsNullOrEmpty(tbStatusComment.Text)) a.status_comment = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbStatusComment.Text, 255);
                else a.status_comment = null;

                if (!string.IsNullOrEmpty(tbPriorOwner.Text))
                    a.priorOwner = tbPriorOwner.Text;
                else
                    a.priorOwner = null;
                 
                if (!string.IsNullOrEmpty(hfPropertyPK.Value)) a.farmLand.fk_property = Convert.ToInt32(hfPropertyPK.Value);
                else a.farmLand.fk_property = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg.ChangeMode(FormViewMode.ReadOnly);
                BindAg(Convert.ToInt32(fvAg.DataKey.Value));

                if (!string.IsNullOrEmpty(sbErrorCollection.ToString())) WACAlert.Show(WacRadWindowManager,sbErrorCollection.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }
    protected void fvAg_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusiness", "msgDelete"))
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int rCode = 0;
                
                try
                {
                    rCode = wDataContext.farmBusiness_delete_DBA(Convert.ToInt32(fvAg.DataKey.Value));
                    if (rCode != 0)
                        throw new Exception();
                    else
                    {
                        fvAg.ChangeMode(FormViewMode.ReadOnly);
                        lbAg_Search_ReloadReset_Click(this, new EventArgs());
                        return;
                    }
                }
                catch (Exception ex) 
                { 
                    WACAlert.Show(WacRadWindowManager,ex.Message, rCode);
                    
                }
            }
        }
        else
            WACAlert.Show(WacRadWindowManager,"User not authorized to delete Farm Business!", 0);
        fvAg.ChangeMode(FormViewMode.Edit);
        BindAg(Convert.ToInt32(fvAg.DataKey.Value));
    }
    protected void fvAg_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        UserControl UC_DropDownListByAlphabet1 = fvAg.FindControl("UC_DropDownListByAlphabet1") as UserControl;
        DropDownList ddlParticipant1 = UC_DropDownListByAlphabet1.FindControl("ddl") as DropDownList;
        UserControl UC_DropDownListByAlphabet2 = fvAg.FindControl("UC_DropDownListByAlphabet2") as UserControl;
        DropDownList ddlParticipant2 = UC_DropDownListByAlphabet2.FindControl("ddl") as DropDownList;
        TextBox tbFarmName = fvAg.FindControl("tbFarmName") as TextBox;
        DropDownList ddlRegionWAC = fvAg.FindControl("ddlRegionWAC") as DropDownList;
        DropDownList ddlProgramWAC = fvAg.FindControl("ddlProgramWAC") as DropDownList;
        DropDownList ddlFarmSize = fvAg.FindControl("ddlFarmSize") as DropDownList;
        DropDownList ddlCountyNY = fvAg.FindControl("UC_GeneralLocation_InsertFarm").FindControl("ddlCounty") as DropDownList;
        DropDownList ddlBasinNY = fvAg.FindControl("UC_GeneralLocation_InsertFarm").FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSoldFarm = fvAg.FindControl("ddlSoldFarm") as DropDownList;
        CheckBox cbGenerateFarmID = fvAg.FindControl("cbGenerateFarmID") as CheckBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iOwner = null;
                if (!string.IsNullOrEmpty(ddlParticipant1.SelectedValue)) iOwner = Convert.ToInt32(ddlParticipant1.SelectedValue);
                else sb.Append("Owner is required. ");

                int? iOperator = null;
                if (!string.IsNullOrEmpty(ddlParticipant2.SelectedValue)) iOperator = Convert.ToInt32(ddlParticipant2.SelectedValue);

                string sRegionWAC = null;
                if (!string.IsNullOrEmpty(ddlRegionWAC.SelectedValue)) sRegionWAC = ddlRegionWAC.SelectedValue;
                else sb.Append("WAC Region is required. ");

                string sProgramWAC = null;
                if (!string.IsNullOrEmpty(ddlProgramWAC.SelectedValue)) sProgramWAC = ddlProgramWAC.SelectedValue;
                else sb.Append("WAC Program is required. ");

                int? iCountyNY = null;
                if (!string.IsNullOrEmpty(ddlCountyNY.SelectedValue)) iCountyNY = Convert.ToInt32(ddlCountyNY.SelectedValue);
                else sb.Append("County is required. ");

                string sBasinNY = null;
                if (!string.IsNullOrEmpty(ddlBasinNY.SelectedValue)) sBasinNY = ddlBasinNY.SelectedValue;
                else sb.Append("Basin is required. ");

                string sFarmSize = null;
                if (!string.IsNullOrEmpty(ddlFarmSize.SelectedValue)) sFarmSize = ddlFarmSize.SelectedValue;
                else sb.Append("Farm Size is required. ");

                string sFarmName = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbFarmName.Text, 48);

                int? iSoldFarmPK = null;
                if (!string.IsNullOrEmpty(ddlSoldFarm.SelectedValue)) iSoldFarmPK = Convert.ToInt32(ddlSoldFarm.SelectedValue);

                string sGenerateFarmID = "N";
                if (cbGenerateFarmID.Checked) sGenerateFarmID = "Y";

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    //iCode = wDataContext.farm_add_express(iOwner, iOperator, sRegionWAC, sBasinNY, sFarmSize, sProgramWAC, sFarmName, iSoldFarmPK, iCountyNY, sGenerateFarmID, Session["userName"].ToString(), ref i);
                    //if (iCode == 0)
                    //{
                    //    fvAg.ChangeMode(FormViewMode.ReadOnly);
                    //    BindAg(Convert.ToInt32(i));

                    //    PopulateDDLs4Searching();
                    //}
                    //else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    #endregion

    #region Event Handling - Ag - Animal

    protected void lbAg_Animal_Close_Click(object sender, EventArgs e)
    {
        fvAg_Animal.ChangeMode(FormViewMode.ReadOnly);
        BindAg_Animal(-1);
        mpeAg_Animal.Hide();
        BindAg(Convert.ToInt32(fvAg.DataKey.Value));
        SwitchTabContainerTab(_tabAnimal);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_Animal_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessAnimal", "msgInsert"))
        {
            fvAg_Animal.ChangeMode(FormViewMode.Insert);
            BindAg_Animal(-1);
            mpeAg_Animal.Show();
            upAg_Animal.Update();
        }
    }

    protected void lbAg_Animal_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_Animal.ChangeMode(FormViewMode.ReadOnly);
        BindAg_Animal(Convert.ToInt32(lb.CommandArgument));
        mpeAg_Animal.Show();
        upAg_Animal.Update();
    }

    protected void fvAg_Animal_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessAnimal", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_Animal.ChangeMode(e.NewMode);
            BindAg_Animal(Convert.ToInt32(fvAg_Animal.DataKey.Value));
        }
    }

    protected void fvAg_Animal_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlAnimal = fvAg_Animal.FindControl("ddlAnimal") as DropDownList;
        DropDownList ddlASRYear = fvAg_Animal.FindControl("ddlASRYear") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmBusinessAnimals.Where(w => w.pk_farmBusinessAnimal == Convert.ToInt32(fvAg_Animal.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlAnimal.SelectedValue)) a.fk_list_animal = Convert.ToInt32(ddlAnimal.SelectedValue);
                else sb.Append("Animal was not updated. Animal is required. ");

                if (!string.IsNullOrEmpty(ddlASRYear.SelectedValue)) a.ASR_yr = Convert.ToInt16(ddlASRYear.SelectedValue);
                else sb.Append("ASR Year was not updated. ASR Year is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_Animal.ChangeMode(FormViewMode.ReadOnly);
                BindAg_Animal(Convert.ToInt32(fvAg_Animal.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Animal_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        DropDownList ddlAnimal = fvAg_Animal.FindControl("ddlAnimal") as DropDownList;
        DropDownList ddlASRYear = fvAg_Animal.FindControl("ddlASRYear") as DropDownList;

        StringBuilder sb = new StringBuilder();

        int? i = null;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iAnimal = null;
                if (!string.IsNullOrEmpty(ddlAnimal.SelectedValue)) iAnimal = Convert.ToInt32(ddlAnimal.SelectedValue);
                else sb.Append("Animal is required. ");

                short? shASRYear = null;
                if (!string.IsNullOrEmpty(ddlASRYear.SelectedValue)) shASRYear = Convert.ToInt16(ddlASRYear.SelectedValue);
                else sb.Append("ASR Year is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmBusinessAnimal_add(Convert.ToInt32(fvAg.DataKey.Value), iAnimal, shASRYear, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_Animal.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_Animal(Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. " + sb.ToString(), iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Animal_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessAnimal", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessAnimal_delete(Convert.ToInt32(fvAg_Animal.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_Animal_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Animal - Age

    protected void lbAg_Animal_Age_Close_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_Animal.FindControl("fvAg_Animal_Age") as FormView;
        fv.DataSource = "";
        fv.DataBind();
        BindAg_Animal(Convert.ToInt32(fvAg_Animal.DataKey.Value));
    }

    protected void lbAg_Animal_Age_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessAnimalAge", "msgInsert"))
        {
            FormView fv = fvAg_Animal.FindControl("fvAg_Animal_Age") as FormView;
            fv.ChangeMode(FormViewMode.Insert);
            BindAg_Animal_Age(fv, -1);
        }
    }

    protected void lbAg_Animal_Age_View_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_Animal.FindControl("fvAg_Animal_Age") as FormView;
        LinkButton lb = (LinkButton)sender;
        fv.ChangeMode(FormViewMode.ReadOnly);
        BindAg_Animal_Age(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void fvAg_Animal_Age_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessAnimalAge", "msgUpdate");
        if (bChangeMode)
        {
            FormView fv = fvAg_Animal.FindControl("fvAg_Animal_Age") as FormView;
            fv.ChangeMode(e.NewMode);
            BindAg_Animal_Age(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvAg_Animal_Age_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_Animal.FindControl("fvAg_Animal_Age") as FormView;
        DropDownList ddlAge = fv.FindControl("ddlAge") as DropDownList;
        TextBox tbCount = fv.FindControl("tbCount") as TextBox;
        TextBox tbWeight = fv.FindControl("tbWeight") as TextBox;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmBusinessAnimalAges.Where(w => w.pk_farmBusinessAnimalAge == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlAge.SelectedValue)) a.fk_list_animalAge = Convert.ToInt32(ddlAge.SelectedValue);
                else sb.Append("Animal Age was not updated. This field is required. ");

                try 
                {
                    int iCount = Convert.ToInt32(tbCount.Text);
                    if (iCount > 0) a.cnt = iCount;
                    else sb.Append("Count was not updated. Must be greater than 0. ");
                }
                catch { sb.Append("Count was not updated. Data type is number (Integer). "); }

                try 
                {
                    decimal dWeight = Convert.ToDecimal(tbWeight.Text);
                    if (dWeight > 0) a.weight = dWeight;
                    else sb.Append("Weight was not updated. Must be greater than 0. ");
                }
                catch { sb.Append("Weight was not updated. Data type is number (Decimal). "); }

                //if (a.created_by == Session["userName"].ToString()) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 48);
                //else sb.Append("You are not the creator of this note. You cannot edit this note.");

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fv.ChangeMode(FormViewMode.ReadOnly);
                BindAg_Animal_Age(fv, Convert.ToInt32(fv.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Animal_Age_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_Animal.FindControl("fvAg_Animal_Age") as FormView;
        DropDownList ddlAge = fv.FindControl("ddlAge") as DropDownList;
        TextBox tbCount = fv.FindControl("tbCount") as TextBox;
        TextBox tbWeight = fv.FindControl("tbWeight") as TextBox;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iAnimalAge = null;
                if (!string.IsNullOrEmpty(ddlAge.SelectedValue)) iAnimalAge = Convert.ToInt32(ddlAge.SelectedValue);
                else sb.Append("Animal Age is required. ");

                int? iCount = null;
                try { iCount = Convert.ToInt32(tbCount.Text); }
                catch { sb.Append("Count is required. Data type is number (Integer). "); }
                if (iCount <= 0) sb.Append("Count must be greater than 0. ");

                decimal? dWeight = null;
                try { dWeight = Convert.ToDecimal(tbWeight.Text); }
                catch { sb.Append("Weight is required. Data type is number (Decimal). "); }
                if (dWeight <= 0) sb.Append("Weight must be greater than 0. ");

                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmBusinessAnimalAge_add(Convert.ToInt32(fvAg_Animal.DataKey.Value), iAnimalAge, iCount, dWeight, sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_Animal_Age(fv, Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. " + sb.ToString(), iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Animal_Age_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessAnimalAge", "msgDelete"))
        {
            FormView fv = fvAg_Animal.FindControl("fvAg_Animal_Age") as FormView;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessAnimalAge_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_Animal_Age_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - ASR

    protected void lbAg_ASR_Close_Click(object sender, EventArgs e)
    {
        fvAg_ASR.ChangeMode(FormViewMode.ReadOnly);
        BindAg_ASR(-1);
        mpeAg_ASR.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            gvAg_ASRs.DataSource = wac.asrAgs.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderByDescending(o => o.year).Select(s => s);
            gvAg_ASRs.DataBind();
        }

        SwitchTabContainerTab(_tabASR);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_ASR_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "asrAg", "msgInsert"))
        {
            fvAg_ASR.ChangeMode(FormViewMode.Insert);
            BindAg_ASR(-1);
            mpeAg_ASR.Show();
            upAg_ASR.Update();
        }
    }

    protected void lbAg_ASR_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_ASR.ChangeMode(FormViewMode.ReadOnly);
        BindAg_ASR(Convert.ToInt32(lb.CommandArgument));
        
       // UC_DocumentArchive_A_ASR.SetupViewer();

        mpeAg_ASR.Show();
        upAg_ASR.Update();
    }

    protected void fvAg_ASR_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "asrAg", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_ASR.ChangeMode(e.NewMode);
            BindAg_ASR(Convert.ToInt32(fvAg_ASR.DataKey.Value));
        }
    }

    protected void fvAg_ASR_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlYear = fvAg_ASR.FindControl("ddlYear") as DropDownList;
        CustomControls_AjaxCalendar tbCalASRDate = fvAg_ASR.FindControl("tbCalASRDate") as CustomControls_AjaxCalendar;

        TextBox tbInterviewee = fvAg_ASR.FindControl("tbInterviewee") as TextBox;
        DropDownList ddlPlanner = fvAg_ASR.FindControl("ddlPlanner") as DropDownList;
        DropDownList ddlAssignTo = fvAg_ASR.FindControl("ddlAssignTo") as DropDownList;
        DropDownList ddlType = fvAg_ASR.FindControl("ddlType") as DropDownList;
        DropDownList ddlCREPInfoRequest = fvAg_ASR.FindControl("ddlCREPInfoRequest") as DropDownList;
        DropDownList ddlEasementInfoRequest = fvAg_ASR.FindControl("ddlEasementInfoRequest") as DropDownList;
        DropDownList ddlForestryInfoRequest = fvAg_ASR.FindControl("ddlForestryInfoRequest") as DropDownList;
        DropDownList ddlF2MInfoRequest = fvAg_ASR.FindControl("ddlF2MInfoRequest") as DropDownList;
        DropDownList ddlHasSign = fvAg_ASR.FindControl("ddlHasSign") as DropDownList;
        DropDownList ddlRevisionRequired = fvAg_ASR.FindControl("ddlRevisionRequired") as DropDownList;
        TextBox tbRevisionRequiredReference = fvAg_ASR.FindControl("tbRevisionRequiredReference") as TextBox;
        DropDownList ddlLandChange = fvAg_ASR.FindControl("ddlLandChange") as DropDownList;
        TextBox tbLandChangeNote = fvAg_ASR.FindControl("tbLandChangeNote") as TextBox;
        DropDownList ddlGoalsChange = fvAg_ASR.FindControl("ddlGoalsChange") as DropDownList;
        TextBox tbGoalsChangeNote = fvAg_ASR.FindControl("tbGoalsChangeNote") as TextBox;
        DropDownList ddlWFPEffective = fvAg_ASR.FindControl("ddlWFPEffective") as DropDownList;
        TextBox tbWFPEffectiveNote = fvAg_ASR.FindControl("tbWFPEffectiveNote") as TextBox;
        DropDownList ddlBMPsEffective = fvAg_ASR.FindControl("ddlBMPsEffective") as DropDownList;
        TextBox tbBMPsEffectiveNote = fvAg_ASR.FindControl("tbBMPsEffectiveNote") as TextBox;
        DropDownList ddlBMPsOMA = fvAg_ASR.FindControl("ddlBMPsOMA") as DropDownList;
        TextBox tbBMPsOMANote = fvAg_ASR.FindControl("tbBMPsOMANote") as TextBox;
        DropDownList ddlRevision = fvAg_ASR.FindControl("ddlRevision") as DropDownList;
        TextBox tbRevisionNote = fvAg_ASR.FindControl("tbRevisionNote") as TextBox;
        TextBox tbRevisionNoteByParticipant = fvAg_ASR.FindControl("tbRevisionNoteByParticipant") as TextBox;
        DropDownList ddlIssues = fvAg_ASR.FindControl("ddlIssues") as DropDownList;
        TextBox tbIssuesNote = fvAg_ASR.FindControl("tbIssuesNote") as TextBox;
        TextBox tbComment = fvAg_ASR.FindControl("tbComment") as TextBox;
        DropDownList ddlActive = fvAg_ASR.FindControl("ddlActive") as DropDownList;
        TextBox tbInactiveCrops = fvAg_ASR.FindControl("tbInactiveCrops") as TextBox;
        TextBox tbInactiveAnimals = fvAg_ASR.FindControl("tbInactiveAnimals") as TextBox;
        TextBox tbInactiveLandUtilization = fvAg_ASR.FindControl("tbInactiveLandUtilization") as TextBox;
        TextBox tbInactiveOther = fvAg_ASR.FindControl("tbInactiveOther") as TextBox;
        TextBox tbNote = fvAg_ASR.FindControl("tbNote") as TextBox;
        //TextBox tbfarmRankingProfJudgement_eoh = fvAg_ASR.FindControl("tbfarmRankingProfJudgement_eoh") as TextBox;
        WACAG_QMACheckbox cbAsrQma = (WACAG_QMACheckbox)fvAg_ASR.FindControl("cbAsrQma");
        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.asrAgs.Where(w => w.pk_asrAg == Convert.ToInt32(fvAg_ASR.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlYear.SelectedValue)) a.year = Convert.ToInt16(ddlYear.SelectedValue);
                else sb.Append("Year was not updated. Year is required. ");

                a.date = tbCalASRDate.CalDateNullable;

                if (!string.IsNullOrEmpty(tbInterviewee.Text)) a.interviewee = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbInterviewee.Text, 48);
                else sb.Append("Interviewee was not updated. Interviewee is required. ");

                if (!string.IsNullOrEmpty(ddlPlanner.SelectedValue)) a.fk_list_designerEngineer_planner = Convert.ToInt32(ddlPlanner.SelectedValue);
                else a.fk_list_designerEngineer_planner = null;

                if (!string.IsNullOrEmpty(ddlAssignTo.SelectedValue)) a.fk_list_designerEngineer_assignTo = Convert.ToInt32(ddlAssignTo.SelectedValue);
                else a.fk_list_designerEngineer_assignTo = null;

                if (!string.IsNullOrEmpty(ddlType.SelectedValue)) a.fk_asrType_code = ddlType.SelectedValue;
                else a.fk_asrType_code = null;

                if (!string.IsNullOrEmpty(ddlCREPInfoRequest.SelectedValue)) a.CREPInfoReq = ddlCREPInfoRequest.SelectedValue;
                else a.CREPInfoReq = null;

                if (!string.IsNullOrEmpty(ddlEasementInfoRequest.SelectedValue)) a.easementInfoReq = ddlEasementInfoRequest.SelectedValue;
                else a.easementInfoReq = null;

                if (!string.IsNullOrEmpty(ddlForestryInfoRequest.SelectedValue)) a.forestryInfoReq = ddlForestryInfoRequest.SelectedValue;
                else a.forestryInfoReq = null;

                if (!string.IsNullOrEmpty(ddlF2MInfoRequest.SelectedValue)) a.F2MInfoReq = ddlF2MInfoRequest.SelectedValue;
                else a.F2MInfoReq = null;

                if (!string.IsNullOrEmpty(ddlHasSign.SelectedValue)) a.hasSign = ddlHasSign.SelectedValue;
                else a.hasSign = null;

                if (!string.IsNullOrEmpty(ddlRevisionRequired.SelectedValue)) a.revisionReqd = ddlRevisionRequired.SelectedValue;
                else a.revisionReqd = null;

                if (!string.IsNullOrEmpty(tbRevisionRequiredReference.Text)) a.revisionReqd_reference = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbRevisionRequiredReference.Text, 8);
                else a.revisionReqd_reference = null;

                if (!string.IsNullOrEmpty(ddlLandChange.SelectedValue)) a.landChange = ddlLandChange.SelectedValue;
                else a.landChange = null;

                a.landChange_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLandChangeNote.Text, 255);

                if (!string.IsNullOrEmpty(ddlGoalsChange.SelectedValue)) a.goalsChange = ddlGoalsChange.SelectedValue;
                else a.goalsChange = null;

                a.goalsChange_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbGoalsChangeNote.Text, 255);

                if (!string.IsNullOrEmpty(ddlWFPEffective.SelectedValue)) a.WFPEffective = ddlWFPEffective.SelectedValue;
                else a.WFPEffective = null;

                a.WFPEffective_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbWFPEffectiveNote.Text, 255);

                if (!string.IsNullOrEmpty(ddlBMPsEffective.SelectedValue)) a.BMPsEffective = ddlBMPsEffective.SelectedValue;
                else a.BMPsEffective = null;

                a.BMPsEffective_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbBMPsEffectiveNote.Text, 255);

                if (!string.IsNullOrEmpty(ddlBMPsOMA.SelectedValue)) a.BMPsOMA = ddlBMPsOMA.SelectedValue;
                else a.BMPsOMA = null;

                a.BMPsOMA_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbBMPsOMANote.Text, 255);

                if (!string.IsNullOrEmpty(ddlRevision.SelectedValue)) a.revision = ddlRevision.SelectedValue;
                else a.revision = null;

                if (!string.IsNullOrEmpty(tbRevisionNote.Text)) a.revision_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbRevisionNote.Text, 255);
                else a.revision_note = null;

                if (!string.IsNullOrEmpty(tbRevisionNoteByParticipant.Text)) a.revision_note_participant = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbRevisionNoteByParticipant.Text, 255);
                else a.revision_note_participant = null;

                if (!string.IsNullOrEmpty(ddlIssues.SelectedValue)) a.issues = ddlIssues.SelectedValue;
                else a.issues = null;

                a.issues_note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbIssuesNote.Text, 255);

                a.comment = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbComment.Text, 255);

                if (!string.IsNullOrEmpty(ddlActive.SelectedValue)) a.active = ddlActive.SelectedValue;
                else a.active = null;

                a.inactive_crops = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbInactiveCrops.Text, 255);

                a.inactive_animal = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbInactiveAnimals.Text, 255);

                a.inactive_landUtilization = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbInactiveLandUtilization.Text, 255);

                a.inactive_other = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbInactiveOther.Text, 255);

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);
                string QMARequest = cbAsrQma.GetCheckedList();

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();
                    
                int code = 0;
                try
                {
                    code = wDataContext.asrAg_update(a.pk_asrAg, a.date, a.year, a.interviewee, a.fk_list_designerEngineer_planner, a.fk_list_designerEngineer_assignTo,
                    a.fk_asrType_code, a.landChange, a.landChange_note, a.goalsChange, a.goalsChange_note, a.WFPEffective, a.WFPEffective_note,
                    a.BMPsEffective, a.BMPsEffective_note, a.BMPsOMA, a.BMPsOMA_note, a.revision, a.revision_note, a.issues, a.issues_note, a.comment,a.active,
                    a.inactive_crops, a.inactive_animal, a.inactive_landUtilization, a.inactive_other, a.easementInfoReq, a.note, a.revision_note_participant,
                    a.revisionReqd, a.revisionReqd_reference, a.forestryInfoReq, a.CREPInfoReq, a.F2MInfoReq, a.hasSign, a.farmRankingProfJudgement_eoh,
                    QMARequest, a.modified_by);
                    if (code != 0)
                        throw new WAC_Exceptions.WACEX_GeneralDatabaseException("", code);
                }
                catch (Exception)
                {
                   WACAlert.Show(WacRadWindowManager,"Database error on Update ASR: ",code);
                }

                
                //wDataContext.SubmitChanges();
                fvAg_ASR.ChangeMode(FormViewMode.ReadOnly);
                BindAg_ASR(Convert.ToInt32(fvAg_ASR.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_ASR_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        DropDownList ddlYear = fvAg_ASR.FindControl("ddlYear") as DropDownList;
        DropDownList ddlPlanner = fvAg_ASR.FindControl("ddlPlanner") as DropDownList;
        DropDownList ddlAssignTo = fvAg_ASR.FindControl("ddlAssignTo") as DropDownList;
        DropDownList ddlType = fvAg_ASR.FindControl("ddlType") as DropDownList;
        Label asrPlanner = (Label)fvAg_ASR.FindControl("AsrPlanner");
        //WACAG_QMACheckbox qma = (WACAG_QMACheckbox)fvAg_ASR.FindControl("WACAG_QMACheckbox");
        StringBuilder sb = new StringBuilder();

        int? i = null;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                short? shYear = null;
                if (!string.IsNullOrEmpty(ddlYear.SelectedValue)) shYear = Convert.ToInt16(ddlYear.SelectedValue);
                else sb.Append("Year is required. ");

                int? iPlanner = null;
                if (!string.IsNullOrEmpty(ddlPlanner.SelectedValue)) iPlanner = Convert.ToInt32(ddlPlanner.SelectedValue);
                //if (!string.IsNullOrEmpty(asrPlanner.Text))
                //    iPlanner = wDataContext.list_designerEngineers.Where(w => w.designerEngineer.Equals(asrPlanner.Text)).First().pk_list_designerEngineer;
                else sb.Append("Planner is required. ");

                string sType = null;
                if (!string.IsNullOrEmpty(ddlType.SelectedValue)) sType = ddlType.SelectedValue;
                else sb.Append("ASR Type is required. ");

                int? iAssignTo = null;
                if (!string.IsNullOrEmpty(ddlAssignTo.SelectedValue)) iAssignTo = Convert.ToInt32(ddlAssignTo.SelectedValue);
                else sb.Append("Assign To is required. ");

                string errMsg = !string.IsNullOrEmpty(sb.ToString()) ? sb.ToString() : string.Empty;
                if (string.IsNullOrEmpty(errMsg))
                    iCode = wDataContext.asrAg_add_express(Convert.ToInt32(fvAg.DataKey.Value), shYear, sType, iPlanner, iAssignTo, Session["userName"].ToString(), ref i);
                if (iCode != 0 || !string.IsNullOrEmpty(errMsg))
                {
                    throw new WAC_Exceptions.WACEX_GeneralDatabaseException(errMsg, iCode);
                }
                else
                {
                    fvAg_ASR.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_ASR(Convert.ToInt32(i));
                }
            }
            catch (WAC_Exceptions.WACEX_GeneralDatabaseException ex) { WACAlert.Show(WacRadWindowManager, ex.Message, ex.ErrorCode); }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_ASR_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "asrAg", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.asrAg_delete(Convert.ToInt32(fvAg_ASR.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_ASR_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - BMP
    protected void BmpListToggleCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        AjaxControlToolkit.TabContainer tc = fvAg.FindControl("tcAg") as AjaxControlToolkit.TabContainer;
        string activeTabID = tc.ActiveTab.ID;
        int activeTabIndex = tc.ActiveTabIndex;
        RadCheckBox BmpListToggleCheckBox = (RadCheckBox)sender;
        bool checkBoxChecked = (bool)BmpListToggleCheckBox.Checked;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            if (checkBoxChecked)
            {
                var g = WACGlobal_Methods.DataBaseFunction_Agriculture_BMP_Grid_All(Convert.ToInt32(fvAg.DataKey.Value), new int?[] { 0 });
                gvAg_BMPs.DataSource = g.Where(w => w.fk_statusBMP_code == "A" || w.fk_statusBMP_code == "DR");
                LimitBMPList = true;
            }
            else
            {
                gvAg_BMPs.DataSource = WACGlobal_Methods.DataBaseFunction_Agriculture_BMP_Grid_All(Convert.ToInt32(fvAg.DataKey.Value), new int?[] { 0 });
                LimitBMPList = false;
            }
            gvAg_BMPs.DataBind();
        }

        SwitchTabContainerTab(activeTabIndex);
        upAgs.Update();
        upAgSearch.Update();
    }
    protected void lbAg_BMP_Close_Click(object sender, EventArgs e)
    {
        AjaxControlToolkit.TabContainer tc = fvAg.FindControl("tcAg") as AjaxControlToolkit.TabContainer;
        string activeTabID = tc.ActiveTab.ID;
        int activeTabIndex = tc.ActiveTabIndex;
        
        fvAg_BMP.ChangeMode(FormViewMode.ReadOnly);
        CloneBMP = false;
        BindAg_BMP(-1);
        mpeAg_BMP.Hide();
        BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            int iCode = wac.AgBmpClearFailedClone(Convert.ToInt32(fvAg.DataKey.Value));
            var g = WACGlobal_Methods.DataBaseFunction_Agriculture_BMP_Grid_All(Convert.ToInt32(fvAg.DataKey.Value), new int?[] { 0 });
            //if (LimitBMPList)
            //    gvAg_BMPs.DataSource = g.Where(w => w.fk_statusBMP_code == "A" || w.fk_statusBMP_code == "DR");
            //else
                gvAg_BMPs.DataSource = g;
            gvAg_BMPs.DataBind();
        }

        SwitchTabContainerTab(activeTabIndex);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_BMP_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag", "msgInsert"))
        {
            fvAg_BMP.ChangeMode(FormViewMode.Insert);
            BindAg_BMP(-1);
            mpeAg_BMP.Show();
            upAg_BMP.Update();
        }
    }

    
    protected void ibAg_BMP_View_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton lb = (ImageButton)sender;
            fvAg_BMP.ChangeMode(FormViewMode.ReadOnly);
            BindAg_BMP(Convert.ToInt32(lb.CommandArgument));
            mpeAg_BMP.Show();
            upAg_BMP.Update();
        }
        catch (Exception ex)
        {
            WACAlert.Show(WacRadWindowManager, "Exception in ibAg_BMP_View_Click " + ex.Message, 0);
        }
    }
   
    protected void ibAg_BMP_Clone_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton ib = (ImageButton)sender;
            GridView gv = (GridView)ib.NamingContainer.BindingContainer;
            int? pkBmp = 0;
            int sourcePk =Convert.ToInt32(ib.CommandArgument);

            // Certain types of BMPs can be copied to jump start creation of a new BMP. This only works for
            // extending an existing BMP with a new qualifier and/or version (ex: bmp# 04a -> bmp# 04aER
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var source = wac.bmp_ags.Where(w => w.pk_bmp_ag == sourcePk).FirstOrDefault();
                if (source == null)
                    throw new Exception("Clone BMP: source BMP not found in data base!");
                var m = Regex.Match(source.bmp_nbr, @"^(IRC.*)$|^(\d{2}(NMCP|PFMQ?)\d{2})$|^(\d{1,3}\.)$|^(MTC)$|^(ENMC)$");
                if (m.Success)
                {
                    WACAlert.Show(WacRadWindowManager, "The selected BMP type cannot be copied! ", 0, WACAlert.AlertLevel.INFORMATION);
                }
                else
                {
                    if (string.IsNullOrEmpty(source.Bmp))
                    {
                        List<string> qualifiers = wac.list_BMPCodes.Select(s => s.pk_BMPCode_code).ToList();
                        Dictionary<string,string> parts = WACGlobal_Methods.BmpNumberDeconstruct(source, qualifiers);
                        string Bmp;
                        string qualifier;
                        string version;
                        byte qv;
                        if (parts != null && parts.TryGetValue("BMP", out Bmp))
                        {
                            if (!parts.TryGetValue("QUALIFIER", out qualifier))
                                qualifier = "UNC";
                            if (!parts.TryGetValue("VERSION", out version))
                                version = "0";

                            source.Bmp = Bmp.Trim();
                            source.fk_BMPCode_code = qualifier;
                            if (!Byte.TryParse(version, out qv))
                                source.QualifierVersion = 0;
                            else
                                source.QualifierVersion = qv;
                      
                            wac.SubmitChanges();
                        }
                    }
                    // to clone insert a copy in the database and open form in Update Mode
                    //ISingleResult<AgBmpCloneResult> code = wac.AgBmpClone(PK_FarmBusiness, sourcePk, Session["userName"].ToString(), ref pkBmp);
                    int iCode = wac.AgBmpClone(PK_FarmBusiness, sourcePk, Session["userName"].ToString(), ref pkBmp);
                    if (iCode == 0 && pkBmp > 0)
                    {
                        CloneBMP = true;
                        fvAg_BMP.ChangeMode(FormViewMode.Edit);
                        BindAg_BMP(Convert.ToInt32(pkBmp));
                        mpeAg_BMP.Show();
                        upAg_BMP.Update();
                    }
                    else
                    {
                        CloneBMP = false;
                        var g = WACGlobal_Methods.DataBaseFunction_Agriculture_BMP_Grid_All(Convert.ToInt32(fvAg.DataKey.Value), new int?[] { 0 });
                        if (LimitBMPList)
                            gv.DataSource = g.Where(w => w.fk_statusBMP_code == "A" || w.fk_statusBMP_code == "DR");
                        else
                            gv.DataSource = g;
                        gv.DataBind();
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            CloneBMP = false;
            WACAlert.Show(WacRadWindowManager, ex.Message,0,WACAlert.AlertLevel.ERROR);         
        }
        
    }
  
    protected void ibAg_BMP_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag", "msgDelete"))
        {

            ImageButton ib = (ImageButton)sender;
            GridView gv = (GridView)ib.NamingContainer.BindingContainer;
            string sStatus = WACGlobal_Methods.SpecialQuery_Agriculture_BMPStatus_By_BMP(ib.CommandArgument);
            if (sStatus == "DR")
            {
                int iCode = 0;
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.bmp_ag_delete(Convert.ToInt32(ib.CommandArgument), Session["userName"].ToString());

                        if (iCode == 0)
                        {

                            var g = WACGlobal_Methods.DataBaseFunction_Agriculture_BMP_Grid_All(Convert.ToInt32(fvAg.DataKey.Value), new int?[] { 0 });
                            if (LimitBMPList)
                                gv.DataSource = g.Where(w => w.fk_statusBMP_code == "A" || w.fk_statusBMP_code == "DR");
                            else
                                gv.DataSource = g;
                            gv.DataBind();
                        }
                          
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
            else WACAlert.Show(WacRadWindowManager,"Only Draft BMPs can be deleted.", 0);
        }
    }


    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCounty = (DropDownList)sender;
        Control container = ddlCounty.NamingContainer;
        DropDownList ddlJurisdiction = (DropDownList)container.FindControl("ddlJurisdiction");
        if (ddlJurisdiction.Items != null)
            ddlJurisdiction.Items.Clear();
        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
        {
            WACGlobal_Methods.LoadJurisdictionDDL(ddlJurisdiction, ddlCounty.SelectedItem.Text);
        }
    }

    //protected void ddlAncestorBmp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList ddlAncestorBmp = (DropDownList)sender;
    //    Control container = ddlAncestorBmp.NamingContainer;
    //    DropDownList ddlParentBmp = (DropDownList)container.FindControl("ddlAncestorBmp");
    //    if (ddlParentBmp.Items != null)
    //        ddlParentBmp.Items.Clear();
    //    if (!string.IsNullOrEmpty(ddlAncestorBmp.SelectedValue))
    //    {
    //        WACGlobal_Methods.LoadAncestorBmpDDL(ddlParentBmp, ddlAncestorBmp.SelectedItem.Text, );
    //    }
    //}

    protected void ddlJurisdiction_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue))
            TaxParcels = WACGlobal_Methods.LoadDepTaxParcels(ddl.SelectedValue);
        else
            return;
        Control container = ddl.NamingContainer;
        DropDownList ddlSection = (DropDownList)container.FindControl("ddlSection");
        if (ddlSection != null)
            WACGlobal_Methods.LoadSblSectionDDL(ddlSection, TaxParcels);
        else
        {
            DropDownList ddlOperTaxParcels = (DropDownList)container.FindControl("ddlOperTaxParcels");
            WACGlobal_Methods.LoadTaxParcelDDL(ddlOperTaxParcels, TaxParcels);
        }
       
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        Control container = ddl.NamingContainer;
        CheckBoxList cblTaxParcels = (CheckBoxList)container.FindControl("cblTaxParcels");
        cblTaxParcels.RepeatColumns = 10;
        cblTaxParcels.RepeatDirection = RepeatDirection.Horizontal;
        cblTaxParcels.DataTextField = "PrintKey";
        cblTaxParcels.DataValueField = "PrintKey";
        cblTaxParcels.DataSource = TaxParcels.Where(w => w.PrintKey.StartsWith(ddl.SelectedValue)).OrderBy(o => o.PrintKey);
        cblTaxParcels.DataBind();
    }
    //protected void AddLocations_Click(object sender, EventArgs e)
    //{
    //    StringBuilder sb = null;
    //    Button btn = (Button)sender;
    //    Control container = btn.NamingContainer;
    //    CheckBoxList cbl = (CheckBoxList)container.FindControl("cblFieldList");
    //    TextBox tbLocation = (TextBox)container.FindControl("tbLocation");
    //    container.FindControl("pnlLocationPicker").Visible = false;
    //    if (!string.IsNullOrEmpty(tbLocation.Text))
    //    {
    //        sb = new StringBuilder(tbLocation.Text);
    //        sb.Append(", ");
    //    }
    //    else
    //        sb = new StringBuilder();

    //    foreach (string parcel in cbl.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Text).ToList())
    //    {
    //        sb.Append(parcel);
    //        sb.Append(", ");
    //    }
    //    sb.Remove(sb.Length - 2, 2);
    //    tbLocation.Text = sb.ToString();
    //    cbl.ClearSelection();
    //    upAg_BMP.Update();
    //}
    //protected void Locations_Click(object sender, EventArgs e)
    //{
    //    Button btn = (Button)sender;
    //    Control container = btn.NamingContainer;
    //    container.FindControl("pnlLocationPicker").Visible=true;
    //    CheckBoxList cbl = (CheckBoxList)container.FindControl("cblFieldList");
    //    cbl.RepeatColumns = 10;
    //    cbl.RepeatDirection = RepeatDirection.Horizontal;
    //    WACGlobal_Methods.PopulateControl_Custom_Agriculture_FarmLandTractField_ByFarmBusiness_CBL(cbl, Convert.ToInt32(fvAg.DataKey.Value));
    //    upAg_BMP.Update();
    //}
    //protected void CloseLocations_Click(object sender, EventArgs e)
    //{
    //    Button btn = (Button)sender;
    //    Control container = btn.NamingContainer;
    //    CheckBoxList cbl = (CheckBoxList)container.FindControl("cblFieldList");
    //    container.FindControl("pnlLocationPicker").Visible = false;
    //    cbl.ClearSelection();
    //    cbl.Items.Clear();
    //    upAg_BMP.Update();
    //}
    //protected void ClearLocations_Click(object sender, EventArgs e)
    //{
    //    Button btn = (Button)sender;
    //    Control container = btn.NamingContainer;
    //    TextBox tbLocations = (TextBox)container.FindControl("tbLocation");
    //    tbLocations.Text = null;
    //    upAg_BMP.Update();
    //}
    protected void Parcels_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Control container = btn.NamingContainer;
        container.FindControl("pnlTaxParcelPicker").Visible = true;
        upAg_BMP.Update();
    }
    protected void CloseParcels_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Control container = btn.NamingContainer;
        CheckBoxList cbl = (CheckBoxList)container.FindControl("cblTaxParcels");
        container.FindControl("pnlTaxParcelPicker").Visible=false;
        cbl.ClearSelection();
        upAg_BMP.Update();
    }
    protected void AddParcels_Click(object sender, EventArgs e)
    {
        StringBuilder sb = null;
        Button btn = (Button)sender;
        Control container = btn.NamingContainer;
        CheckBoxList cbl = (CheckBoxList)container.FindControl("cblTaxParcels");
        TextBox tbTaxParcels = (TextBox)container.FindControl("tbTaxParcels");
        container.FindControl("pnlTaxParcelPicker").Visible=false; 
        if (!string.IsNullOrEmpty(tbTaxParcels.Text))
        {
            sb = new StringBuilder(tbTaxParcels.Text);
            sb.Append(", ");
        }
        else
            sb = new StringBuilder();

        foreach (string parcel in cbl.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList())
        {
            sb.Append(parcel);
            sb.Append(", ");
        }
        sb.Remove(sb.Length - 2, 2);
        tbTaxParcels.Text = sb.ToString();
        cbl.ClearSelection();
        upAg_BMP.Update();
    }
    protected void ClearParcels_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Control container = btn.NamingContainer;
        TextBox tbTaxParcels = (TextBox)container.FindControl("tbTaxParcels");
        tbTaxParcels.Text = null;
        upAg_BMP.Update();
    }
    
    
    protected void ddlPractice_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        Label lbl1 = fvAg_BMP.FindControl("lblUnits1") as Label;
        Label lbl2 = fvAg_BMP.FindControl("lblUnits2") as Label;
        Label lbl3 = fvAg_BMP.FindControl("lblUnits3") as Label;
        Label lblLife = fvAg_BMP.FindControl("lblLife") as Label;
        TextBox tbDescription = fvAg_BMP.FindControl("tbDescription") as TextBox;
        TextBox tbLocation = fvAg_BMP.FindControl("tbLocation") as TextBox;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            decimal p = Convert.ToDecimal(ddl.SelectedValue);
            var v = wac.list_bmpPractices.Where(w => w.pk_bmpPractice_code == p).Select(s => s).First();
            tbDescription.Text = v.practice;
            lblLife.Text = v.life_reqd_yr.ToString();
            if (fvAg_BMP.CurrentMode == FormViewMode.Insert)
                lbl1.Text = v.list_unit.unit;
            if (fvAg_BMP.CurrentMode == FormViewMode.Edit)
            {
                lbl1.Text = v.list_unit.unit;
                lbl2.Text = v.list_unit.unit;
                lbl3.Text = v.list_unit.unit;
            }
        } 
        tbLocation.Focus();
        upAg_BMP.Update();
    }

    protected void lbSA_Clear_Click(object sender, EventArgs e)
    {
        DropDownList ddlSAA = fvAg_BMP.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvAg_BMP.FindControl("ddlSA") as DropDownList;
        try
        {
            ddlSAA.SelectedIndex = -1;
            ddlSA.SelectedIndex = -1;
        }
        catch { }
    }

    private void HandleSupplementalAgreementsBasedOnFarm(int? _fk_BMP, int? _fk_saTP)
    {
        //Panel pnlSA_Available = fvAg_BMP.FindControl("pnlSA_Available") as Panel;
        //Panel pnlSA_Message = fvAg_BMP.FindControl("pnlSA_Message") as Panel;
        Label lblSA_Message = fvAg_BMP.FindControl("lblSA_Message") as Label;
        DropDownList ddlSAA = fvAg_BMP.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvAg_BMP.FindControl("ddlSA") as DropDownList;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.bmp_ag_SAs.Where(w => w.fk_bmp_ag == _fk_BMP && w.fk_supplementalAgreementTaxParcel == _fk_saTP).Select(s => s.pk_bmp_ag_SA);
            if (a.Count() > 0)
            {
                if (fvAg_BMP.CurrentMode == FormViewMode.Insert) 
                    WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(ddlSA, null, true);
                else 
                    WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(ddlSA, _fk_saTP, true);
                lblSA_Message.Visible = false;
                //pnlSA_Available.Visible = true;
            }
            else
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(ddlSA, null, true);
                lblSA_Message.Text = "No Supplemental Agreements have been assigned to this farm";
                lblSA_Message.Visible = true;
                //pnlSA_Message.Visible = true;
            }
        }
    }

    protected void ddlBmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlBmpType = (DropDownList)sender;
        RadNumericTextBox grouping = (RadNumericTextBox)fvAg_BMP.FindControl("ntbWorkloadGroup");
        grouping.Visible = ddlBmpType.SelectedValue != "PIRC";
    }
  
    protected void fvAg_BMP_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "bmp_ag", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_BMP.ChangeMode(e.NewMode);
            if (fvAg_BMP.DataKey.Value != null)
                BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
            else
            {
                fvAg_BMP.DataSource = null;
                fvAg_BMP.DataBind();
                mpeAg_BMP.Hide();
            }
        }
    }
   
    protected void fvAg_BMP_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

        TextBox tbDescription = fvAg_BMP.FindControl("tbDescription") as TextBox;       
        TextBox tbLocation = fvAg_BMP.FindControl("tbLocation") as TextBox;
        TextBox tbTaxParcels = (TextBox)fvAg_BMP.FindControl("tbTaxParcels");
        TextBox tbUnitsPlanned = fvAg_BMP.FindControl("tbUnitsPlanned") as TextBox;
        TextBox tbEstPlanPrior = fvAg_BMP.FindControl("tbEstPlanPrior") as TextBox;
        TextBox tbEstPlanRevision = fvAg_BMP.FindControl("tbEstPlanRevision") as TextBox;
        TextBox tbUnitsDesigned = (TextBox)fvAg_BMP.FindControl("tbUnitsDesigned");
        TextBox tbDesignCost = fvAg_BMP.FindControl("tbDesignCost") as TextBox;
        TextBox tbDesignDimensions = fvAg_BMP.FindControl("tbDesignDimensions") as TextBox;
        TextBox tbUnitsCompleted = (TextBox)fvAg_BMP.FindControl("tbUnitsCompleted");
        TextBox tbFinalCost = fvAg_BMP.FindControl("tbFinalCost") as TextBox;
        DropDownList ddlPractice = fvAg_BMP.FindControl("ddlPractice") as DropDownList;
        DropDownList ddlPollutant = fvAg_BMP.FindControl("ddlPollutant") as DropDownList;
        DropDownList ddlSAA = fvAg_BMP.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvAg_BMP.FindControl("ddlSA") as DropDownList;
        Label lblLife = fvAg_BMP.FindControl("lblLife") as Label;
        DropDownList ddlBmpType = fvAg_BMP.FindControl("ddlBmpType") as DropDownList;
        DropDownList ddlParentBmp = fvAg_BMP.FindControl("ddlAncestorBmp") as DropDownList;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            int? iPH_PK_SA_TP = null;
            bool bPerformSABMPInsert = false;
            
            var a = wDataContext.bmp_ags.Where(w => w.pk_bmp_ag == Convert.ToInt32(fvAg_BMP.DataKey.Value)).Select(s => s).Single();
            try
            {
                string bmp = String.Empty;
                string qualifier = String.Empty;
                string bmpTypeCode = String.Empty;
                int? parent = null;
                byte version = 0;
                int? wlProject = 0;

                TextBox tbBMP = fvAg_BMP.FindControl("tbBMP") as TextBox;
                if (string.IsNullOrEmpty(tbBMP.Text))
                    throw new Exception("BMP Number is required! ");
                if (tbBMP.Text.Length > 24)
                    throw new Exception("BMP Number must be less than 25 characters! ");
                bmp = tbBMP.Text;

                if (!string.IsNullOrEmpty(ddlParentBmp.SelectedValue))
                {

                    parent = Convert.ToInt32(ddlParentBmp.SelectedValue);

                }

                if (!String.IsNullOrEmpty(ddlBmpType.SelectedValue))
                {
                    bmpTypeCode = ddlBmpType.SelectedValue;
                    // handle BMP type changes
                    if (bmpTypeCode.StartsWith("PIRC") && !bmp.StartsWith("PIRC"))
                        throw new Exception("BMP number must start with PIRC for PIRC type");
                    if (bmpTypeCode.StartsWith("IRC") && !bmp.StartsWith("IRC"))
                        throw new Exception("BMP number must start with IRC for IRC type");
                    if (bmpTypeCode.StartsWith("BMP") && (bmp.Contains("IRC")))
                        throw new Exception("Invalid BMP number for BMP type.");
                }
                else
                    bmpTypeCode = a.fk_BMPTypeCode;
                DropDownList ddlBMPCode = (DropDownList)fvAg_BMP.FindControl("ddlBMPCode");
                RadNumericTextBox ntbQualifierVersion = (RadNumericTextBox)fvAg_BMP.FindControl("ntbQualifierVersion");
                RadNumericTextBox ntbWorkloadGroup = (RadNumericTextBox)fvAg_BMP.FindControl("ntbWorkloadGroup");
                DropDownList ddlBmpDescriptor = (DropDownList)fvAg_BMP.FindControl("ddlBmpDescriptor");
                if (CloneBMP)
                {

                    ddlBMPCode = (DropDownList)fvAg_BMP.FindControl("ddlBMPCodeClone");
                    ntbQualifierVersion = (RadNumericTextBox)fvAg_BMP.FindControl("ntbQualifierVersionClone");
                    ddlBmpDescriptor = (DropDownList)fvAg_BMP.FindControl("ddlBmpDescriptorClone");
                }
                qualifier = string.IsNullOrEmpty(ddlBMPCode.SelectedValue) ? "UNC" : ddlBMPCode.SelectedValue;

                try
                {
                    version = Convert.ToByte(ntbQualifierVersion.DbValue);
                }
                catch (OverflowException oe)
                {
                    version = 0;
                    throw new Exception("Qualifier Version must be between 1 and 10", oe);
                }
                try
                {
                    wlProject = Convert.ToInt32(ntbWorkloadGroup.DbValue);  
                }
                catch (OverflowException oe)
                {
                    wlProject = 0;
                    throw new Exception("Workload Group must be between 0 and 98", oe);
                }
               
                if (version < 0 && CloneBMP)
                    throw new Exception("Copied BMP with BMP Qualifier " + qualifier + " requires a valid version number.");

                a.description = string.IsNullOrEmpty(tbDescription.Text) ? null : WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 400);
                if (!IsGoodBmpNumber(bmp))
                    throw new ArgumentException("The BMP number entered does not have the correct format!");
                a.Bmp = bmp;
                a.fk_BMPCode_code = qualifier;
                a.QualifierVersion = version;
                string AgBmpDescriptorCode = string.IsNullOrEmpty(ddlBmpDescriptor.SelectedValue) ? null : ddlBmpDescriptor.SelectedValue;

                if (IsNonGroupingBmp(bmp, AgBmpDescriptorCode, a.description, a.fk_bmpPractice_code) && wlProject != 99)
                {
                    wlProject = 99;
                    WACAlert.Show(WacRadWindowManager, "This BMP cannot be grouped! Using 99 for ungroupable BMPs", 0, WACAlert.AlertLevel.INFORMATION);
                    //throw new Exception("This BMP cannot be grouped! Using 99 for ungroupable BMPs");
                }
                    

                if (WACGlobal_Methods.BmpExistsForFarm(a))
                    throw new Exception("BMP can not be saved with the same BMP #, Qualifier and Version as an existing BMP!");            
                
                if (!string.IsNullOrEmpty(ddlPractice.SelectedValue))
                {
                    a.fk_bmpPractice_code = Convert.ToDecimal(ddlPractice.SelectedValue);
                    if (lblLife != null && !string.IsNullOrEmpty(lblLife.Text))
                        a.lifespan_year = Convert.ToByte(lblLife.Text);
                }                    
                else
                    a.fk_bmpPractice_code = null;
                if (!string.IsNullOrEmpty(ddlPollutant.SelectedValue))
                    a.fk_pollutant_category_code = ddlPollutant.SelectedValue;
                a.location = string.IsNullOrEmpty(tbLocation.Text) ? null : WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLocation.Text, 96);
                a.TaxParcels = string.IsNullOrEmpty(tbTaxParcels.Text) ? null : tbTaxParcels.Text;
                a.units_planned = WACGlobal_Methods.DecimalFromString(tbUnitsPlanned.Text);
                a.est_plan_prior = WACGlobal_Methods.DecimalFromString(tbEstPlanPrior.Text);
                a.est_plan_rev = WACGlobal_Methods.DecimalFromString(tbEstPlanRevision.Text);
                a.units_designed = WACGlobal_Methods.DecimalFromString(tbUnitsDesigned.Text);
                a.design_cost = WACGlobal_Methods.DecimalFromString(tbDesignCost.Text);
                a.dimensions_designed = tbDesignDimensions.Text;
                a.units_completed = WACGlobal_Methods.DecimalFromString(tbUnitsCompleted.Text);
                a.final_cost = WACGlobal_Methods.DecimalFromString(tbFinalCost.Text);
                if (!string.IsNullOrEmpty(ddlSAA.SelectedValue) && !string.IsNullOrEmpty(ddlSA.SelectedValue))
                {
                    iPH_PK_SA_TP = Convert.ToInt32(ddlSA.SelectedValue);
                    if (a.fk_supplementalAgreementTaxParcel != iPH_PK_SA_TP)
                    {
                        bPerformSABMPInsert = true;
                        a.fk_supplementalAgreementTaxParcel = iPH_PK_SA_TP;
                    }
                    a.fk_SAAssignType_code = ddlSAA.SelectedValue;
                }
                else if ((!string.IsNullOrEmpty(ddlSAA.SelectedValue) && string.IsNullOrEmpty(ddlSA.SelectedValue)) || (string.IsNullOrEmpty(ddlSAA.SelectedValue) && !string.IsNullOrEmpty(ddlSA.SelectedValue)))
                {
                    throw new Exception("Supplemental Agreement was not updated. Both Supplemental Agreement Assignment and Supplemental Agreement must be defined when attaching a Supplemental Agreement. ");
                }
                else if (string.IsNullOrEmpty(ddlSAA.SelectedValue) && string.IsNullOrEmpty(ddlSA.SelectedValue))
                {
                    a.fk_supplementalAgreementTaxParcel = null;
                    a.fk_SAAssignType_code = null;
                }

                a.modified_by = Session["userName"].ToString();
                ISingleResult<AgBmpUpdateResult> code = wDataContext.AgBmpUpdate(a.pk_bmp_ag, a.fk_pollutant_category_code, 
                    a.fk_statusBMP_code, a.Bmp, a.fk_BMPCode_code, a.QualifierVersion, wlProject, a.description, a.fk_bmpPractice_code, a.location, 
                    a.TaxParcels, a.units_planned, a.est_plan_prior, a.est_plan_rev, a.units_designed, a.design_cost, a.dimensions_designed, 
                    a.units_completed, a.final_cost, bmpTypeCode, a.fk_SAAssignType_code, a.fk_supplementalAgreementTaxParcel, 
                    AgBmpDescriptorCode, parent, a.modified_by);
                int iCode = (int)code.ReturnValue;

                if (iCode == 0)
                {
                //    if (statusChanged) // create a new status record if status changed
                //    {
                //        int? pk_BMPStatus = null;
                //        int rc = 0;
                //        rc = wDataContext.bmp_ag_status_add(a.pk_bmp_ag, DateTime.Now, a.fk_statusBMP_code, a.fk_form_wfp2_version, a.modified_by, ref pk_BMPStatus);
                //    }
                   

                    // it's already false on a normal update
                    CloneBMP = false; 
                    if (bPerformSABMPInsert)
                    {
                        int? i = null;
                        int iCodeInsert = wDataContext.bmp_ag_SA_add(Convert.ToInt32(fvAg_BMP.DataKey.Value), iPH_PK_SA_TP,
                            a.fk_SAAssignType_code, Session["userName"].ToString(), ref i);
                    }

                    fvAg_BMP.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
                }
                else
                    WACAlert.Show(WacRadWindowManager, "Update failed: ", iCode,WACAlert.AlertLevel.ERROR);
            }
            catch (WACEX_BmpCloneException cex)
            {                
                WACAlert.Show(WacRadWindowManager,cex.Message + "<br />To save a copied BMP the Qualifier cannot be blank or \"UNC\" and the Version must be a number from 1-9. Please correct this and click Update again. ", 0, WACAlert.AlertLevel.ERROR);
            }
            catch (Exception ex)
            {
                WACAlert.Show(WacRadWindowManager, ex.Message, 0);
            }
        }
    }
    protected void fvAg_BMP_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
       
        TextBox tbBMP = fvAg_BMP.FindControl("tbBMP") as TextBox;
       
        RadNumericTextBox ntbQualifierVersion = (RadNumericTextBox)fvAg_BMP.FindControl("ntbQualifierVersion");
        RadNumericTextBox ntbWorkloadGroup = (RadNumericTextBox)fvAg_BMP.FindControl("ntbWorkloadGroup");
        TextBox tbDescription = fvAg_BMP.FindControl("tbDescription") as TextBox;       
        TextBox tbLocation = fvAg_BMP.FindControl("tbLocation") as TextBox;
        TextBox tbTaxParcels = (TextBox)fvAg_BMP.FindControl("tbTaxParcels");
        TextBox tbUnitsPlanned = fvAg_BMP.FindControl("tbUnitsPlanned") as TextBox;
        TextBox tbEstPlanPrior = fvAg_BMP.FindControl("tbEstPlanPrior") as TextBox;
        TextBox tbEstPlanRevision = fvAg_BMP.FindControl("tbEstPlanRevision") as TextBox;
        DropDownList ddlSAA = fvAg_BMP.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvAg_BMP.FindControl("ddlSA") as DropDownList;
        //RadCheckBox bmpIsIrc = (RadCheckBox)fvAg_BMP.FindControl("BmpIsIrc");
        DropDownList ddlBmpDescriptor = (DropDownList)fvAg_BMP.FindControl("ddlBmpDescriptor");
        DropDownList ddlPollutant = fvAg_BMP.FindControl("ddlPollutant") as DropDownList;
        DropDownList ddlBMPStatus = fvAg_BMP.FindControl("ddlBMPStatus") as DropDownList;
        DropDownList ddlBMPCode = (DropDownList)fvAg_BMP.FindControl("ddlBMPCode");
        DropDownList ddlPractice = fvAg_BMP.FindControl("ddlPractice") as DropDownList;
        DropDownList ddlBmpType = fvAg_BMP.FindControl("ddlBmpType") as DropDownList;
        Label lblLife = fvAg_BMP.FindControl("lblLife") as Label;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int farmId = Convert.ToInt32(fvAg.DataKey.Value);
                string sPollutant = ddlPollutant.SelectedValue;
                if (string.IsNullOrEmpty(sPollutant))
                    throw new Exception("Pollutant is required! ");

                string sBMPStatus = String.Empty; ;
                if (!string.IsNullOrEmpty(ddlBMPStatus.SelectedValue))
                    sBMPStatus = ddlBMPStatus.SelectedValue;
                else
                    throw new Exception("Status is required! ");

                string bmp = string.Empty;
                string qualifier = string.Empty;
                byte version = 0;
                int? wlProject = 0;
                if (string.IsNullOrEmpty(tbBMP.Text))
                    throw new Exception("BMP Number is required! ");
                if (tbBMP.Text.Length > 24)
                    throw new Exception("BMP Number must be less than 25 characters! ");
                bmp = tbBMP.Text;
                if (!IsGoodBmpNumber(bmp))
                    throw new ArgumentException("The BMP number entered does not have the correct format!");
                qualifier = string.IsNullOrEmpty(ddlBMPCode.SelectedValue) ? "UNC" : ddlBMPCode.SelectedValue;
                if (!(bmp.Contains("NMCP") || bmp.Contains("PFM")))
                {        
                    try
                    {
                        version = Convert.ToByte(ntbQualifierVersion.DbValue);
                    }
                    catch (OverflowException oe)
                    {
                        throw new Exception("Qualifier Version must be between 0 and 9", oe);
                    }
                    
                    try
                    {
                        wlProject = Convert.ToInt32(ntbWorkloadGroup.DbValue);
                    }
                    catch (OverflowException oe)
                    {
                        //throw new Exception("Workload Group must be between 0 and 20", oe);
                        wlProject = 0;
                    }
                }
                string AgBmpDescriptorCode = string.IsNullOrEmpty(ddlBmpDescriptor.SelectedValue) ? null : ddlBmpDescriptor.SelectedValue;
                if (WACGlobal_Methods.BmpExistsForFarm(0, farmId, bmp, AgBmpDescriptorCode, qualifier, version))
                    throw new Exception("The combination of BMP#, Qualifier Code and Qualifier Version (" + bmp + qualifier + version.ToString() + ") already exists for this farm! ");

                string irc = ddlBmpType.Text;

                decimal? dPractice = null;
                byte? lifeSpan = null;
                if (!string.IsNullOrEmpty(ddlPractice.SelectedValue))
                {
                    dPractice = Convert.ToDecimal(ddlPractice.SelectedValue);
                    if (lblLife != null && !string.IsNullOrEmpty(lblLife.Text))
                        lifeSpan = Convert.ToByte(lblLife.Text);
                }
                else
                    throw new Exception("Valid Practice Code and Lifespan are required! ");
                string sDescription = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 400);
                // for grouping purposes, if a BMP is ungroupable AND IRC, ungroupable takes precidence
                if (IsNonGroupingBmp(bmp, AgBmpDescriptorCode, sDescription, dPractice))
                    wlProject = 99;     
                
                string sLocation = null;
                if (!string.IsNullOrEmpty(tbLocation.Text))
                    sLocation = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLocation.Text, 96);
                string taxParcels = null;
                if (!string.IsNullOrEmpty(tbTaxParcels.Text))
                    taxParcels = tbTaxParcels.Text;
                decimal? dUnitsPlanned = null;
                if (!string.IsNullOrEmpty(tbUnitsPlanned.Text))
                    dUnitsPlanned = WACGlobal_Methods.DecimalFromString(tbUnitsPlanned.Text);
                decimal? dEstPlanPrior = null;
                if (!string.IsNullOrEmpty(tbEstPlanPrior.Text))
                    dEstPlanPrior = WACGlobal_Methods.DecimalFromString(tbEstPlanPrior.Text);
                decimal? dCurrentPlanningEstimate = null;
                if (!string.IsNullOrEmpty(tbEstPlanRevision.Text))
                    dCurrentPlanningEstimate = Convert.ToDecimal(tbEstPlanRevision.Text);
                string sSAAssignType = null;
                int? iPK_SupplementalAgreementTaxParcel = null;
                
                if (!string.IsNullOrEmpty(ddlSAA.SelectedValue) && !string.IsNullOrEmpty(ddlSA.SelectedValue))
                {
                    sSAAssignType = ddlSAA.SelectedValue;
                    iPK_SupplementalAgreementTaxParcel = Convert.ToInt32(ddlSA.SelectedValue);
                }


                else
                {
                    if ((!string.IsNullOrEmpty(ddlSAA.SelectedValue) && string.IsNullOrEmpty(ddlSA.SelectedValue)) || (string.IsNullOrEmpty(ddlSAA.SelectedValue) && !string.IsNullOrEmpty(ddlSA.SelectedValue)))
                    {
                        throw new Exception("Both Supplemental Agreement Assignment and Supplemental Agreement must be defined when attaching a Supplemental Agreement. ");
                    }
                }

                iCode = wDataContext.AgBmpInsert(farmId, sPollutant, sBMPStatus, bmp, qualifier, version, wlProject, 
                    sDescription, dPractice, lifeSpan, sLocation, taxParcels, dUnitsPlanned, dEstPlanPrior, dCurrentPlanningEstimate,
                    sSAAssignType, iPK_SupplementalAgreementTaxParcel, irc, AgBmpDescriptorCode, Session["userName"].ToString(), null, ref i);

                if (iCode == 0)
                {
                    // new status record created from insert operation
                    //int? pk_BMPStatus = null;
                    //int rc = 0;
                    //int?  wfp2Rev = wDataContext.GetCurrentApprovedWfpRevPK(i);
                    //rc = wDataContext.bmp_ag_status_add(i, DateTime.Now, sBMPStatus, wfp2Rev, Session["userName"].ToString(), ref pk_BMPStatus);
                    fvAg_BMP.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_BMP(Convert.ToInt32(i));
                }
                else WACAlert.Show(WacRadWindowManager, "Error Returned from Database.", iCode);
            }
            catch (Exception ex)
            {
                WACAlert.Show(WacRadWindowManager, ex.Message, 0);
            }
        }
    }

    protected void fvAg_BMP_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag", "msgDelete"))
        {
            string sStatus = WACGlobal_Methods.SpecialQuery_Agriculture_BMPStatus_By_BMP(fvAg_BMP.DataKey.Value);
            if (sStatus == "DR")
            {
                int iCode = 0;
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.bmp_ag_delete(Convert.ToInt32(fvAg_BMP.DataKey.Value), Session["userName"].ToString());
                        if (iCode == 0) lbAg_BMP_Close_Click(null, null);
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
            else WACAlert.Show(WacRadWindowManager,"Only Draft BMPs can be deleted.", 0);
        }
    }

    #endregion

    #region Event Handling - Ag - BMP - Funding

    protected void lbAg_BMP_Funding_Close_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
        fv.DataSource = "";
        fv.DataBind();
        BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
    }

    protected void lbAg_BMP_Funding_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag_funding", "msgInsert"))
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
            fv.ChangeMode(FormViewMode.Insert);
            BindAg_BMP_Funding(fv, -1);
        }
    }

    protected void lbAg_BMP_Funding_View_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
        LinkButton lb = (LinkButton)sender;
        fv.ChangeMode(FormViewMode.ReadOnly);
        BindAg_BMP_Funding(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void ddlFundingSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ScriptManager.RegisterStartupScript(fvAg_BMP, fvAg_BMP.GetType(), "WaitCursor", "WaitCursor", true);
        DropDownList ddl = (DropDownList)sender;
        Panel pnlBMPTransfer = fvAg_BMP.FindControl("fvAg_BMP_Funding").FindControl("pnlBMPTransfer") as Panel;
        Label lblBMPTransferNoFunds = fvAg_BMP.FindControl("fvAg_BMP_Funding").FindControl("lblBMPTransferNoFunds") as Label;
        DropDownList ddlBMPToTransferFrom = pnlBMPTransfer.FindControl("ddlBMPToTransferFrom") as DropDownList;
        DropDownList ddlFundingAgency = fvAg_BMP.FindControl("fvAg_BMP_Funding").FindControl("ddlFundingAgency") as DropDownList;
        TextBox tbFunding = fvAg_BMP.FindControl("fvAg_BMP_Funding").FindControl("tbFunding") as TextBox;
        if (ddl.SelectedValue == "T")
        {
            if (ddlFundingAgency.SelectedValue == "WAC")
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    int iPK_BMP_AG = Convert.ToInt32(fvAg_BMP.DataKey.Value);
                    var b = wDataContext.form_wfp3_bmps.Where(w => w.fk_bmp_ag == iPK_BMP_AG).OrderByDescending(o => o.form_wfp3.contract_awarded_date).Select(s => new { s.fk_form_wfp3 });
                    int iPK_FormWFP3 = 0;
                    try { iPK_FormWFP3 = b.First().fk_form_wfp3; }
                    catch { }

                    WACGlobal_Methods.DatabaseFunction_Agriculture_View_BMPAG_Financial_Get_DDL(ddlBMPToTransferFrom, iPK_BMP_AG, iPK_FormWFP3, true);
                    //WACGlobal_Methods.View_Agriculture_BMP_Financial(ddlBMPToTransferFrom, Convert.ToInt32(fvAg.DataKey.Value), iPK_BMP_AG, iPK_FormWFP3, null);
                    if (ddlBMPToTransferFrom.Items.Count > 1)
                    {
                        pnlBMPTransfer.Visible = true;
                        lblBMPTransferNoFunds.Visible = false;
                    }
                    else
                    {
                        pnlBMPTransfer.Visible = false;
                        lblBMPTransferNoFunds.Visible = true;
                    }
                }
            }
            else
            {
                WACAlert.Show(WacRadWindowManager,"Funds can only be transfered when funding agency is WAC.", 0);
                ddl.SelectedIndex = 0;
            }
        }
        else
        {
            pnlBMPTransfer.Visible = false;
            lblBMPTransferNoFunds.Visible = false;
        }
        tbFunding.Focus();
    }

    protected void ddlBMPToTransferFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
        TextBox tbAmountToTransfer = fv.FindControl("pnlBMPTransfer").FindControl("tbAmountToTransfer") as TextBox;
        TextBox tbFunding = fv.FindControl("tbFunding") as TextBox;
        if (!string.IsNullOrEmpty(ddl.SelectedValue))
        {
            string[] sBoundValueItem = ddl.SelectedValue.Split("|".ToCharArray());
            if (fv.CurrentMode == FormViewMode.Insert) tbFunding.Text = sBoundValueItem[1];
            else tbAmountToTransfer.Text = sBoundValueItem[1];
        }
        else
        {
            if (fv.CurrentMode == FormViewMode.Insert) tbFunding.Text = "";
            else tbAmountToTransfer.Text = "";
        }
    }

    protected void btnBMPAmountTransfer_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
        Panel pnlBMPTransfer = fv.FindControl("pnlBMPTransfer") as Panel;
        DropDownList ddlBMPToTransferFrom = pnlBMPTransfer.FindControl("ddlBMPToTransferFrom") as DropDownList;

        int iCode = 0;
        int? i = null;
        if (!string.IsNullOrEmpty(ddlBMPToTransferFrom.SelectedValue))
        {
            TextBox tbAmountToTransfer = pnlBMPTransfer.FindControl("tbAmountToTransfer") as TextBox;
            if (!string.IsNullOrEmpty(tbAmountToTransfer.Text))
            {
                try
                {
                    decimal dTransferAmount = Convert.ToDecimal(tbAmountToTransfer.Text);
                    string[] sBoundValueItem = ddlBMPToTransferFrom.SelectedValue.Split("|".ToCharArray());
                    int iBMPSourcePK = Convert.ToInt32(sBoundValueItem[0]);
                    decimal dMaxAmount = Convert.ToDecimal(sBoundValueItem[1]);

                    if (dTransferAmount <= dMaxAmount)
                    {
                        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                        {
                            iCode = wDataContext.bmp_ag_funding_add_xferSource(iBMPSourcePK, Convert.ToInt32(fv.DataKey.Value), dTransferAmount, "U", Session["userName"].ToString(), ref i);
                            if (iCode != 0) throw new Exception("Stored Procedure error transferring funds. ");
                        }
                    }
                    else throw new Exception("The transfer was terminated. The transfer amount must be less than or equal to the amount of available funds for the selected BMP. ");
                }
                catch { throw new Exception("The transfer was terminated. The transfer amount must be a number (Decimal). "); }
            }
            else throw new Exception("The transfer was terminated. A transfer amount was not input. ");
        }
        else throw new Exception("The transfer was terminated. A transfer BMP was not selected. ");

        if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), iCode);
        else
        {
            WACAlert.Show(WacRadWindowManager,"The transfer was completed.", 0);
            BindAg_BMP_Funding(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvAg_BMP_Funding_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "bmp_ag_funding", "msgUpdate");
        if (bChangeMode)
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
            fv.ChangeMode(e.NewMode);
            BindAg_BMP_Funding(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvAg_BMP_Funding_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
        CustomControls_AjaxCalendar tbCalFundingDate = fv.FindControl("tbCalFundingDate") as CustomControls_AjaxCalendar;
        DropDownList ddlFundingPurpose = fv.FindControl("ddlFundingPurpose") as DropDownList;
        DropDownList ddlFundingAgency = fv.FindControl("ddlFundingAgency") as DropDownList;
        DropDownList ddlFundingSource = fv.FindControl("ddlFundingSource") as DropDownList;
        TextBox tbFunding = fv.FindControl("tbFunding") as TextBox;
        TextBox tbDescription = fv.FindControl("tbDescription") as TextBox;
        DropDownList ddlWFP2Version = fv.FindControl("ddlWFP2Version") as DropDownList;
        //DropDownList ddlEncumbrance = fv.FindControl("ddlEncumbrance") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.bmp_ag_fundings.Where(w => w.pk_bmp_ag_funding == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            try
            {
                a.date = (DateTime)tbCalFundingDate.CalDateNullable;

                if (!string.IsNullOrEmpty(ddlFundingPurpose.SelectedValue)) a.fk_fundingPurpose_code = ddlFundingPurpose.SelectedValue;
                else throw new Exception("Funding Purpose was not updated. Funding Purpose is required. ");

                if (!string.IsNullOrEmpty(ddlFundingAgency.SelectedValue)) a.fk_agencyFunding_code = ddlFundingAgency.SelectedValue;
                else a.fk_agencyFunding_code = null;

                if (!string.IsNullOrEmpty(ddlFundingSource.SelectedValue)) a.fk_fundingSource_code = ddlFundingSource.SelectedValue;
                else throw new Exception("Funding Source was not updated. Funding Source is required. ");

                if (!string.IsNullOrEmpty(tbFunding.Text))
                {
                    try { a.funding = Convert.ToDecimal(tbFunding.Text); }
                    catch { throw new Exception("Funding must be a number (Decimal). "); }
                }
                else throw new Exception("Funding was not updated. Funding is required. ");

                a.description = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 400);

               // if (!string.IsNullOrEmpty(ddlWFP2Version.SelectedValue)) a.fk_form_wfp2_version = Convert.ToInt32(ddlWFP2Version.SelectedValue);
                //else throw new Exception("WFP2 Revision is required. ");

                //if (!string.IsNullOrEmpty(ddlEncumbrance.SelectedValue)) a.fk_encumbrance_code = ddlEncumbrance.SelectedValue;
                //else a.fk_encumbrance_code = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    wDataContext.SubmitChanges();
                    fv.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_BMP_Funding(fv, Convert.ToInt32(fv.DataKey.Value));
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,"BMP Funding Update: " + ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Funding_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;

        CustomControls_AjaxCalendar tbCalFundingDate = fv.FindControl("tbCalFundingDate") as CustomControls_AjaxCalendar;
        DropDownList ddlFundingPurpose = fv.FindControl("ddlFundingPurpose") as DropDownList;
        DropDownList ddlFundingAgency = fv.FindControl("ddlFundingAgency") as DropDownList;
        DropDownList ddlFundingSource = fv.FindControl("ddlFundingSource") as DropDownList;
        TextBox tbFunding = fv.FindControl("tbFunding") as TextBox;
        TextBox tbDescription = fv.FindControl("tbDescription") as TextBox;
        DropDownList ddlWFP2Version = fv.FindControl("ddlWFP2Version") as DropDownList;
        //DropDownList ddlEncumbrance = fv.FindControl("ddlEncumbrance") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                DateTime? dtDate = tbCalFundingDate.CalDateNullable;

                string sFundingPurpose = null;
                if (!string.IsNullOrEmpty(ddlFundingPurpose.SelectedValue)) sFundingPurpose = ddlFundingPurpose.SelectedValue;
                else throw new Exception("Funding Purpose is required. ");

                string sFundingAgency = null;
                if (!string.IsNullOrEmpty(ddlFundingAgency.SelectedValue)) sFundingAgency = ddlFundingAgency.SelectedValue;

                string sFundingSource = null;
                if (!string.IsNullOrEmpty(ddlFundingSource.SelectedValue)) sFundingSource = ddlFundingSource.SelectedValue;
                else throw new Exception("Funding Source is required. ");

                decimal dFunding = 0;
                int? iBMPSourcePK = null;
                if (sFundingSource == "T")
                {
                    Panel pnlBMPTransfer = fv.FindControl("pnlBMPTransfer") as Panel;
                    if (pnlBMPTransfer.Visible == true)
                    {
                        DropDownList ddlBMPToTransferFrom = pnlBMPTransfer.FindControl("ddlBMPToTransferFrom") as DropDownList;
                        if (!string.IsNullOrEmpty(ddlBMPToTransferFrom.SelectedValue))
                        {
                            if (!string.IsNullOrEmpty(tbFunding.Text))
                            {
                                try
                                {
                                    decimal dTransferAmount = Convert.ToDecimal(tbFunding.Text);
                                    string[] sBoundValueItem = ddlBMPToTransferFrom.SelectedValue.Split("|".ToCharArray());
                                    iBMPSourcePK = Convert.ToInt32(sBoundValueItem[0]);
                                    decimal dMaxAmount = Convert.ToDecimal(sBoundValueItem[1]);

                                    if (dTransferAmount <= dMaxAmount) dFunding = dTransferAmount;
                                    else throw new Exception("Funding Source was not updated. The funding amount must be less than or equal to the amount of available funds for the selected BMP. ");
                                }
                                catch { throw new Exception("Funding Source was not updated. The funding must be a number (Decimal). "); }
                            }
                            else throw new Exception("Funding Source was not updated. The funding is empty. ");
                        }
                        else throw new Exception("Funding Source was not updated. A transfer BMP was not selected. ");
                    }
                    else throw new Exception("Funding Source was not updated. There are no funds available for transfer. ");
                }
                else
                {
                    if (!string.IsNullOrEmpty(tbFunding.Text))
                    {
                        try { dFunding = Convert.ToDecimal(tbFunding.Text); }
                        catch { throw new Exception("Funding must be a number (Decimal). "); }
                    }
                    else throw new Exception("Funding is required. ");
                    //if (dFunding <= 0) throw new Exception("Funding is required and must be greater than 0. ");
                }

                string sDescription = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 400);

                int? iWFP2Version = null;
                //if (!string.IsNullOrEmpty(ddlWFP2Version.SelectedValue)) iWFP2Version = Convert.ToInt32(ddlWFP2Version.SelectedValue);
                //else throw new Exception("WFP2 Revision is required. ");

                //string bmpStatusCode = null;
                //if (!string.IsNullOrEmpty(ddlBMPStatus.SelectedValue)) bmpStatusCode = ddlBMPStatus.SelectedValue;
                //else throw new Exception("BMP Status is required. ");

                string sEncumbrance = null;
                //if (!string.IsNullOrEmpty(ddlEncumbrance.SelectedValue)) sEncumbrance = ddlEncumbrance.SelectedValue;
                //else throw new Exception("Encumbrance is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.bmp_ag_funding_add(Convert.ToInt32(fvAg_BMP.DataKey.Value), dtDate, iWFP2Version, sFundingPurpose, sFundingSource, sFundingAgency, dFunding, sDescription, iBMPSourcePK, Session["userName"].ToString(), ref i);
//                    iCode = wDataContext.bmp_ag_funding_add(Convert.ToInt32(fvAg_BMP.DataKey.Value), dtDate, sFundingPurpose, sFundingSource, sFundingAgency, sEncumbrance, dFunding, sDescription, iBMPSourcePK, Session["userName"].ToString(), ref i);

                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_BMP_Funding(fv, Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. ", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Funding_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_funding", "msgDelete"))
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Funding") as FormView;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_funding_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_BMP_Funding_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    
    #region Event Handling - Ag - BMP - Location

    protected void ddlAg_BMP_Location_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag_location", "msgInsert"))
        {
            int? i = null;
            int iCode = 0;
            DropDownList ddl = (DropDownList)sender;
            if (!string.IsNullOrEmpty(ddl.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.bmp_ag_location_add(Convert.ToInt32(fvAg_BMP.DataKey.Value), Convert.ToInt32(ddl.SelectedValue), Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
                        }
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. ", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
        }
    }

    protected void lbAg_BMP_Location_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_location", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_location_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0) BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - BMP - Leased Land

    protected void ddlAg_BMP_LeasedLand_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag_leasedLand", "msgInsert"))
        {
            int? i = null;
            int iCode = 0;
            DropDownList ddl = (DropDownList)sender;
            if (!string.IsNullOrEmpty(ddl.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.bmp_ag_leasedLand_add(Convert.ToInt32(fvAg_BMP.DataKey.Value), Convert.ToInt32(ddl.SelectedValue), Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
                        }
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. ", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
        }
    }

    protected void lbAg_BMP_LeasedLand_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_leasedLand", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_leasedLand_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0) BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - BMP - Note

    protected void lbAg_BMP_Note_Close_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Note") as FormView;
        fv.DataSource = "";
        fv.DataBind();
        BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
    }

    protected void lbAg_BMP_Note_Add_Click(object sender, EventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag_note", "msgInsert"))
        //{
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Note") as FormView;
            fv.ChangeMode(FormViewMode.Insert);
            BindAg_BMP_Note(fv, -1);
        //}
    }

    protected void lbAg_BMP_Note_View_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Note") as FormView;
        LinkButton lb = (LinkButton)sender;
        fv.ChangeMode(FormViewMode.ReadOnly);
        BindAg_BMP_Note(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void fvAg_BMP_Note_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Note") as FormView;
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit)
        {
            //bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "bmp_ag_note", "msgUpdate");
            bChangeMode = WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "bmp_ag_note", Convert.ToInt32(fv.DataKey.Value));
        } 
        if (bChangeMode)
        {
            fv.ChangeMode(e.NewMode);
            BindAg_BMP_Note(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvAg_BMP_Note_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Note") as FormView;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.bmp_ag_notes.Where(w => w.pk_bmp_ag_note == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            try
            {
                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fv.ChangeMode(FormViewMode.ReadOnly);
                BindAg_BMP_Note(fv, Convert.ToInt32(fv.DataKey.Value));
                CreateAgOverviewGrid(Convert.ToInt32(fv.DataKey.Value), null);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Note_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Note") as FormView;

        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000);

                iCode = wDataContext.bmp_ag_note_add(Convert.ToInt32(fvAg_BMP.DataKey.Value), sNote, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fv.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_BMP_Note(fv, Convert.ToInt32(i));
                    //CreateAgOverviewGrid(Convert.ToInt32(fv.DataKey.Value), null);
                }
                else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. ", iCode);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Note_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_note", "msgDelete"))
        //{
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Note") as FormView;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    if (WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "bmp_ag_note", Convert.ToInt32(fv.DataKey.Value)))
                    {
                        iCode = wDataContext.bmp_ag_note_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                        if (iCode == 0) lbAg_BMP_Note_Close_Click(null, null);
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                        CreateAgOverviewGrid(Convert.ToInt32(fv.DataKey.Value), null);
                    }
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        //}
    }

    #endregion

    #region Event Handling - Ag - BMP - SAs

    protected void lbAg_BMP_SA_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_SA", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_SA_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0) BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - BMP - Status

    protected void lbAg_BMP_Status_Close_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Status") as FormView;
        fv.DataSource = "";
        fv.DataBind();
        BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
    }

    protected void lbAg_BMP_Status_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag_status", "msgInsert"))
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Status") as FormView;
            fv.ChangeMode(FormViewMode.Insert);
            BindAg_BMP_Status(fv, -1);
        }
    }

    protected void lbAg_BMP_Status_View_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Status") as FormView;
        LinkButton lb = (LinkButton)sender;
        fv.ChangeMode(FormViewMode.ReadOnly);
        BindAg_BMP_Status(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void fvAg_BMP_Status_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "bmp_ag_status", "msgUpdate");
        if (bChangeMode)
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Status") as FormView;
            fv.ChangeMode(e.NewMode);
            BindAg_BMP_Status(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvAg_BMP_Status_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Status") as FormView;
        DropDownList ddlWFP2 = fv.FindControl("ddlWFP2") as DropDownList;
        CustomControls_AjaxCalendar tbCalBMPStatusDate = fv.FindControl("tbCalBMPStatusDate") as CustomControls_AjaxCalendar;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.bmp_ag_status.Where(w => w.pk_bmp_ag_status == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlWFP2.SelectedValue)) a.fk_form_wfp2_version = Convert.ToInt32(ddlWFP2.SelectedValue);
                else sb.Append("WFP2 was not updated. WFP2 is required. ");

                a.date = (DateTime)tbCalBMPStatusDate.CalDateNullable;

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fv.ChangeMode(FormViewMode.ReadOnly);
                BindAg_BMP_Status(fv, Convert.ToInt32(fv.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Status_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Status") as FormView;
        DropDownList ddlWFP2 = fv.FindControl("ddlWFP2") as DropDownList;
        CustomControls_AjaxCalendar tbCalBMPStatusDate = fv.FindControl("tbCalBMPStatusDate") as CustomControls_AjaxCalendar;
        DropDownList ddlStatus = fv.FindControl("ddlStatus") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                DateTime? dtDate = tbCalBMPStatusDate.CalDateNullable;

                string sStatus = null;
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue)) sStatus = ddlStatus.SelectedValue;
                else sb.Append("Status is required. ");

                int? iRevision = null;
                if (!string.IsNullOrEmpty(ddlWFP2.SelectedValue)) iRevision = Convert.ToInt32(ddlWFP2.SelectedValue);
                else sb.Append("WFP2 is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.bmp_ag_status_add(Convert.ToInt32(fvAg_BMP.DataKey.Value), dtDate, sStatus, iRevision, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_BMP_Status(fv, Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. ", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Status_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_status", "msgDelete"))
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Status") as FormView;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_status_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_BMP_Status_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - BMP - Workload

    protected void lbAg_BMP_Workload_Close_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
        fv.DataSource = "";
        fv.DataBind();
        ListView lv = fvAg_BMP.FindControl("lvAg_BMP_Workload_ReadOnly") as ListView;
        lv.DataSource = "";
        lv.DataBind();
        PlaceHolder bmpDetailsPlaceHolder = (PlaceHolder)fvAg_BMP.FindControl("bmpDetailsPlaceHolder");
        bmpDetailsPlaceHolder.Visible = true;
        BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
    }

    protected void lbAg_BMP_Workload_Add_Click(object sender, EventArgs e)
    {
        bool b = WACGlobal_Methods.SpecialQuery_Agriculture_BMP_Workload_CanInsert(fvAg_BMP.DataKey.Value);
        if (b)
        {
            if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag_workload", "msgInsert"))
            {
                FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
                fv.ChangeMode(FormViewMode.Insert);
                BindAg_BMP_Workload(fv, -1);
            }
        }
        else WACAlert.Show(WacRadWindowManager,"Only draft or approved BMPs can be added to the Workload table.", 0);
    }

    protected void lbAg_BMP_Workload_View_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
        LinkButton lb = (LinkButton)sender;
        fv.ChangeMode(FormViewMode.ReadOnly);
        BindAg_BMP_Workload(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void Ag_BMP_Workload_ddlTechnician_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) Ag_BMP_Workload_DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "T");
    }

    protected void Ag_BMP_Workload_ddlChecker_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) Ag_BMP_Workload_DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "C");
    }

    protected void Ag_BMP_Workload_ddlConstruction_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) Ag_BMP_Workload_DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "O");
    }

    protected void Ag_BMP_Workload_ddlEngineer_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) Ag_BMP_Workload_DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "E");
    }

    private void Ag_BMP_Workload_DesignerEngineer_Insert(int? iDesignerEngineer, string sBMPWorkloadSupportCode)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
        int? i = null;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wDataContext.bmp_ag_workloadSupport_add(Convert.ToInt32(fv.DataKey.Value), sBMPWorkloadSupportCode, iDesignerEngineer, Session["userName"].ToString(), ref i);
                if (iCode == 0) BindAg_BMP_Workload(fv, Convert.ToInt32(fv.DataKey.Value));
                else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void Ag_BMP_Workload_DesignerEngineer_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_workloadSupport", "msgDelete"))
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_workloadSupport_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0) BindAg_BMP_Workload(fv, Convert.ToInt32(fv.DataKey.Value));
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }
    //protected void ddlWorkload_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int? i = null;
    //    int iCode = 0;
    //    FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;

    //    DropDownList ddlWorkload = fv.FindControl("ddlWorkload") as DropDownList;
    //    using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //    {
    //        try
    //        {
    //            Int16? iYear = null;
    //            if (!string.IsNullOrEmpty(ddlWorkload.SelectedValue)) 
    //                iYear = Convert.ToInt16(ddlWorkload.SelectedValue);
    //            iCode = wDataContext.bmp_ag_workload_add_express(Convert.ToInt32(fvAg_BMP.DataKey.Value), iYear, Session["userName"].ToString(), ref i);
    //            if (iCode == 0)
    //            {
    //                fv.ChangeMode(FormViewMode.Edit);
    //                BindAg_BMP_Workload(fv, Convert.ToInt32(i));
    //            }
    //            else 
    //                WACAlert.Show(WacRadWindowManager, "Error Returned from Database. ", iCode);
    //        }
    //        catch (Exception ex) 
    //        { 
    //            WACAlert.Show(WacRadWindowManager, ex.Message, 0); 
    //        }
    //    }
    //}

    protected void fvAg_BMP_Workload_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "bmp_ag_workload", "msgUpdate");
        if (bChangeMode)
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
            fv.ChangeMode(e.NewMode);
            BindAg_BMP_Workload(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvAg_BMP_Workload_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
        DropDownList ddlWorkload = fv.FindControl("ddlWorkload") as DropDownList;
        DropDownList ddlAgency = fv.FindControl("ddlAgency") as DropDownList;
        DropDownList ddlStatusBMPWorkload = fv.FindControl("ddlStatusBMPWorkload") as DropDownList;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == Convert.ToInt32(fv.DataKey.Value)).Single();

                if (!string.IsNullOrEmpty(ddlWorkload.SelectedValue)) a.year = Convert.ToInt16(ddlWorkload.SelectedValue);
                else a.year = null;

                if (!string.IsNullOrEmpty(ddlAgency.SelectedValue)) a.fk_agency_code = ddlAgency.SelectedValue;
                else a.fk_agency_code = null;

                if (!string.IsNullOrEmpty(ddlStatusBMPWorkload.SelectedValue)) a.fk_statusBMPWorkload_code = ddlStatusBMPWorkload.SelectedValue;
                else a.fk_statusBMPWorkload_code = null;

                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);
                else a.note = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    wDataContext.SubmitChanges();
                    fv.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_BMP_Workload(fv, Convert.ToInt32(fv.DataKey.Value));
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Workload_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;

        DropDownList ddlWorkload = fv.FindControl("ddlWorkload") as DropDownList;
        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                Int16? iYear = null;
                if (!string.IsNullOrEmpty(ddlWorkload.SelectedValue)) 
                    iYear = Convert.ToInt16(ddlWorkload.SelectedValue);
                else 
                    sb.Append("Workload is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.bmp_ag_workload_add_express(Convert.ToInt32(fvAg_BMP.DataKey.Value), iYear, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_BMP_Workload(fv, Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. ", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_BMP_Workload_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_workload", "msgDelete"))
        {
            FormView fv = fvAg_BMP.FindControl("fvAg_BMP_Workload") as FormView;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_workload_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_BMP_Workload_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Contact

    protected void lbAg_FarmBusinessContact_Close_Click(object sender, EventArgs e)
    {
        fvAg_FarmBusinessContact.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmBusinessContact(-1);
        mpeAg_FarmBusinessContact.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            gvAg_FarmBusinessContacts.DataSource = wac.farmBusinessContacts.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
            gvAg_FarmBusinessContacts.DataBind();
        }
        
        SwitchTabContainerTab(_tabFarmOperation);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_FarmBusinessContact_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessContact", "msgInsert"))
        {
            fvAg_FarmBusinessContact.ChangeMode(FormViewMode.Insert);
            BindAg_FarmBusinessContact(-1);
            mpeAg_FarmBusinessContact.Show();
            upAg_FarmBusinessContact.Update();
        }
    }

    protected void lbAg_FarmBusinessContact_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_FarmBusinessContact.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmBusinessContact(Convert.ToInt32(lb.CommandArgument));
        mpeAg_FarmBusinessContact.Show();
        upAg_FarmBusinessContact.Update();
    }

    protected void fvAg_FarmBusinessContact_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessContact", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_FarmBusinessContact.ChangeMode(e.NewMode);
            BindAg_FarmBusinessContact(Convert.ToInt32(fvAg_FarmBusinessContact.DataKey.Value));
        }
    }

    protected void fvAg_FarmBusinessContact_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlParticipant = fvAg_FarmBusinessContact.FindControl("UC_DropDownListByAlphabet_Ag_Contact").FindControl("ddl") as DropDownList;
        TextBox tbNote = fvAg_FarmBusinessContact.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmBusinessContacts.Where(w => w.pk_farmBusinessContact == Convert.ToInt32(fvAg_FarmBusinessContact.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlParticipant.SelectedValue)) a.fk_participant = Convert.ToInt32(ddlParticipant.SelectedValue);
                else sb.Append("Participant was not updated. Participant is required. ");

                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 96).Trim();
                else sb.Append("Note was not updated. Note is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_FarmBusinessContact.ChangeMode(FormViewMode.ReadOnly);
                BindAg_FarmBusinessContact(Convert.ToInt32(fvAg_FarmBusinessContact.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmBusinessContact_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlParticipant = fvAg_FarmBusinessContact.FindControl("UC_DropDownListByAlphabet_Ag_Contact").FindControl("ddl") as DropDownList;
        TextBox tbNote = fvAg_FarmBusinessContact.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iParticipant_PK = null;
                if (!string.IsNullOrEmpty(ddlParticipant.SelectedValue)) iParticipant_PK = Convert.ToInt32(ddlParticipant.SelectedValue);
                else sb.Append("Participant is required. ");

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 96).Trim();
                else sb.Append("Note is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmBusinessContact_add(Convert.ToInt32(fvAg.DataKey.Value), iParticipant_PK, sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_FarmBusinessContact.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_FarmBusinessContact(Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmBusinessContact_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessContact", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessContact_delete(Convert.ToInt32(fvAg_FarmBusinessContact.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_FarmBusinessContact_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Cropware

    protected void lbAg_Cropware_Close_Click(object sender, EventArgs e)
    {
        mpeAg_Cropware.Hide();
    }

    protected void lbAg_Cropware_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        BindAg_Cropware(Convert.ToInt32(lb.CommandArgument));
        mpeAg_Cropware.Show();
        upAg_Cropware.Update();
    }

    protected void ddlAg_Cropware_Year_Filter_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            if (string.IsNullOrEmpty(ddlAg_Cropware_Year_Filter.SelectedValue))
            {
                gvAg_Cropware.DataSource = wac.cropwares.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderByDescending(o => o.plan_year).ThenBy(o => o.tractField).Select(s => s);
            }
            else
            {
                gvAg_Cropware.DataSource = wac.cropwares.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value) && w.plan_year == Convert.ToInt16(ddlAg_Cropware_Year_Filter.SelectedValue)).OrderByDescending(o => o.plan_year).ThenBy(o => o.tractField).Select(s => s);
            }
            gvAg_Cropware.DataBind();
        }
    }

    #endregion

    #region Event Handling - Ag - Farm Land Tract

    protected void lbAg_FarmLandTract_Close_Click(object sender, EventArgs e)
    {
        fvAg_FarmLandTract.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmLandTract(-1);
        mpeAg_FarmLandTract.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            int iPK_FarmLand = wac.farmBusinesses.Where(w => w.pk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).Select(s => s.fk_farmLand).Single();
            lvAg_FarmLandTracts.DataSource = wac.farmLandTracts.Where(w => w.fk_farmLand == iPK_FarmLand).OrderBy(o => o.tract).Select(s => s);
            lvAg_FarmLandTracts.DataBind();
        }

        SwitchTabContainerTab(_tabFarmLand);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_FarmLandTract_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmLandTract", "msgInsert"))
        {
            fvAg_FarmLandTract.ChangeMode(FormViewMode.Insert);
            BindAg_FarmLandTract(-1);
            mpeAg_FarmLandTract.Show();
            upAg_FarmLandTract.Update();
        }
    }

    protected void lbAg_FarmLandTract_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_FarmLandTract.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmLandTract(Convert.ToInt32(lb.CommandArgument));
        mpeAg_FarmLandTract.Show();
        upAg_FarmLandTract.Update();
    }

    protected void fvAg_FarmLandTract_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmLandTract", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_FarmLandTract.ChangeMode(e.NewMode);
            BindAg_FarmLandTract(Convert.ToInt32(fvAg_FarmLandTract.DataKey.Value));
        }
    }

    protected void fvAg_FarmLandTract_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        TextBox tbTract = fvAg_FarmLandTract.FindControl("tbTract") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmLandTracts.Where(w => w.pk_farmLandTract == Convert.ToInt32(fvAg_FarmLandTract.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(tbTract.Text)) a.tract = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbTract.Text, 16);
                else sb.Append("Tract was not updated. Tract is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_FarmLandTract.ChangeMode(FormViewMode.ReadOnly);
                BindAg_FarmLandTract(Convert.ToInt32(fvAg_FarmLandTract.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmLandTract_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        TextBox tbTract = fvAg_FarmLandTract.FindControl("tbTract") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPK_FarmLand = null;
                //try { iPK_FarmLand = Convert.ToInt32(hfAg_FarmLand_PK.Value); }
                //catch { }
                if (iPK_FarmLand == null) sb.Append("Could not determine Farm Land PK. ");

                string sTract = null;
                if (!string.IsNullOrEmpty(tbTract.Text)) sTract = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbTract.Text, 16);
                else sb.Append("Tract is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmLandTract_add(iPK_FarmLand, sTract, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_FarmLandTract.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_FarmLandTract(Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmLandTract_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmLandTract", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmLandTract_delete(Convert.ToInt32(fvAg_FarmLandTract.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_FarmLandTract_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Farm Land Tract Field

    protected void lbAg_FarmLandTractField_Close_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_FarmLandTract.FindControl("fvAg_FarmLandTractField") as FormView;
        fv.DataSource = "";
        fv.DataBind();
        BindAg_FarmLandTract(Convert.ToInt32(fvAg_FarmLandTract.DataKey.Value));
    }

    protected void lbAg_FarmLandTractField_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmLandTractField", "msgInsert"))
        {
            FormView fv = fvAg_FarmLandTract.FindControl("fvAg_FarmLandTractField") as FormView;
            fv.ChangeMode(FormViewMode.Insert);
            BindAg_FarmLandTractField(fv, -1);
        }
    }

    protected void lbAg_FarmLandTractField_View_Click(object sender, EventArgs e)
    {
        FormView fv = fvAg_FarmLandTract.FindControl("fvAg_FarmLandTractField") as FormView;
        LinkButton lb = (LinkButton)sender;
        fv.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmLandTractField(fv, Convert.ToInt32(lb.CommandArgument));
    }

    protected void fvAg_FarmLandTractField_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmLandTractField", "msgUpdate");
        if (bChangeMode)
        {
            FormView fv = fvAg_FarmLandTract.FindControl("fvAg_FarmLandTractField") as FormView;
            fv.ChangeMode(e.NewMode);
            BindAg_FarmLandTractField(fv, Convert.ToInt32(fv.DataKey.Value));
        }
    }

    protected void fvAg_FarmLandTractField_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        FormView fv = fvAg_FarmLandTract.FindControl("fvAg_FarmLandTractField") as FormView;
        DropDownList ddlYear = fv.FindControl("ddlYear") as DropDownList;
        TextBox tbField = fv.FindControl("tbField") as TextBox;
        TextBox tbSoil = fv.FindControl("tbSoil") as TextBox;
        TextBox tbAcres = fv.FindControl("tbAcres") as TextBox;
        TextBox tbArea = fv.FindControl("tbArea") as TextBox;
        TextBox tbPerimeter = fv.FindControl("tbPerimeter") as TextBox;
        DropDownList ddlAvailableToRent = fv.FindControl("ddlAvailableToRent") as DropDownList;
        DropDownList ddlActive = fv.FindControl("ddlActive") as DropDownList;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmLandTractFields.Where(w => w.pk_farmLandTractField == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlYear.SelectedValue)) a.year = Convert.ToInt16(ddlYear.SelectedValue);
                else a.year = null;

                a.field = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbField.Text, 16);

                a.soil = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbSoil.Text, 16);

                try
                {
                    if (!string.IsNullOrEmpty(tbAcres.Text)) a.acres = Convert.ToDecimal(tbAcres.Text);
                    else a.acres = null;
                }
                catch { sb.Append("Acres not updated. Data Type is number (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbArea.Text)) a.area = Convert.ToDecimal(tbArea.Text);
                    else a.area = null;
                }
                catch { sb.Append("Area not updated. Data Type is number (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbPerimeter.Text)) a.perimeter = Convert.ToDecimal(tbPerimeter.Text);
                    else a.perimeter = null;
                }
                catch { sb.Append("Perimeter not updated. Data Type is number (Decimal). "); }

                if (!string.IsNullOrEmpty(ddlAvailableToRent.SelectedValue)) a.availableToRent = ddlAvailableToRent.SelectedValue;
                else a.availableToRent = null;

                if (!string.IsNullOrEmpty(ddlActive.SelectedValue)) a.active = ddlActive.SelectedValue;
                else a.active = null;

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 400);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fv.ChangeMode(FormViewMode.ReadOnly);
                BindAg_FarmLandTractField(fv, Convert.ToInt32(fv.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmLandTractField_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_FarmLandTract.FindControl("fvAg_FarmLandTractField") as FormView;
        DropDownList ddlYear = fv.FindControl("ddlYear") as DropDownList;
        TextBox tbField = fv.FindControl("tbField") as TextBox;
        TextBox tbSoil = fv.FindControl("tbSoil") as TextBox;
        TextBox tbAcres = fv.FindControl("tbAcres") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                Int16? iYear = null;
                try
                {
                    if (!string.IsNullOrEmpty(ddlYear.SelectedValue)) iYear = Convert.ToInt16(ddlYear.SelectedValue);
                    else sb.Append("Year is required. ");
                }
                catch { }

                string sField = null;
                if (!string.IsNullOrEmpty(tbField.Text)) sField = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbField.Text, 16);
                else sb.Append("Field is required. ");

                string sSoil = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbSoil.Text, 16);

                decimal? dAcres = null;
                try
                {
                    dAcres = Convert.ToDecimal(tbAcres.Text);
                }
                catch { }

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmLandTractField_add_express(Convert.ToInt32(fvAg_FarmLandTract.DataKey.Value), iYear, sField, sSoil, dAcres, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fv.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_FarmLandTractField(fv, Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmLandTractField_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmLandTractField", "msgDelete"))
        {
            FormView fv = fvAg_FarmLandTract.FindControl("fvAg_FarmLandTractField") as FormView;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmLandTractField_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_FarmLandTractField_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Farm Status

    protected void lbAg_FarmStatus_Close_Click(object sender, EventArgs e)
    {
        fvAg_FarmStatus.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmStatus(-1);
        mpeAg_FarmStatus.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            gvAg_FarmStatus.DataSource = wac.farmBusinessStatus.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderByDescending(o => o.date).Select(s => s);
            gvAg_FarmStatus.DataBind();
        }

        SwitchTabContainerTab(_tabFarmStatus);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_FarmStatus_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessStatus", "msgInsert"))
        {
            fvAg_FarmStatus.ChangeMode(FormViewMode.Insert);
            BindAg_FarmStatus(-1);
            mpeAg_FarmStatus.Show();
            upAg_FarmStatus.Update();
        }
    }

    protected void lbAg_FarmStatus_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_FarmStatus.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmStatus(Convert.ToInt32(lb.CommandArgument));
        mpeAg_FarmStatus.Show();
        upAg_FarmStatus.Update();
    }

    protected void fvAg_FarmStatus_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessStatus", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_FarmStatus.ChangeMode(e.NewMode);
            BindAg_FarmStatus(Convert.ToInt32(fvAg_FarmStatus.DataKey.Value));
        }
    }

    protected void fvAg_FarmStatus_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        //DropDownList ddlStatus = fvAg_FarmStatus.FindControl("ddlStatus") as DropDownList;
        CustomControls_AjaxCalendar tbCalStatusDate = fvAg_FarmStatus.FindControl("tbCalStatusDate") as CustomControls_AjaxCalendar;
        TextBox tbNote = fvAg_FarmStatus.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmBusinessStatus.Where(w => w.pk_farmBusinessStatus == Convert.ToInt32(fvAg_FarmStatus.DataKey.Value))
                     select b).Single();
            try
            {
                //if (!string.IsNullOrEmpty(ddlStatus.SelectedValue)) a.fk_status_code = ddlStatus.SelectedValue;
                //else sb.Append("Farm Status was not updated. Farm Status is required. ");
           
                a.date = (DateTime)tbCalStatusDate.CalDateNullable;

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_FarmStatus.ChangeMode(FormViewMode.ReadOnly);
                BindAg_FarmStatus(Convert.ToInt32(fvAg_FarmStatus.DataKey.Value));

                CreateAgOverviewGrid(Convert.ToInt32(fvAg.DataKey.Value), null);

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmStatus_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlStatus = fvAg_FarmStatus.FindControl("ddlStatus") as DropDownList;
        CustomControls_AjaxCalendar tbCalStatusDate = fvAg_FarmStatus.FindControl("tbCalStatusDate") as CustomControls_AjaxCalendar;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sStatus = null;
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue)) sStatus = ddlStatus.SelectedValue;
                else sb.Append("Farm Status is required. ");

                DateTime? dtDate = tbCalStatusDate.CalDateNullable;

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmBusinessStatus_add(Convert.ToInt32(fvAg.DataKey.Value), sStatus, dtDate, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_FarmStatus.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_FarmStatus(Convert.ToInt32(i));

                        CreateAgOverviewGrid(Convert.ToInt32(fvAg.DataKey.Value), null);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmStatus_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessStatus", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessStatus_delete(Convert.ToInt32(fvAg_FarmStatus.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) 
                    {
                        CreateAgOverviewGrid(Convert.ToInt32(fvAg.DataKey.Value), null);
                        lbAg_FarmStatus_Close_Click(null, null);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Farm Business FAD

    protected void lbAg_FarmBusinessFAD_Close_Click(object sender, EventArgs e)
    {
        fvAg_FarmBusinessFAD.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmBusinessFAD(-1);
        mpeAg_FarmBusinessFAD.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            gvAg_FarmBusinessFAD.DataSource = wac.farmBusinessFADs.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.list_FAD.setting).Select(s => s);
            gvAg_FarmBusinessFAD.DataBind();
        }

        SwitchTabContainerTab(_tabFarmStatus);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_FarmBusinessFAD_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessFAD", "msgInsert"))
        {
            fvAg_FarmBusinessFAD.ChangeMode(FormViewMode.Insert);
            BindAg_FarmBusinessFAD(-1);
            mpeAg_FarmBusinessFAD.Show();
            upAg_FarmBusinessFAD.Update();
        }
    }

    protected void lbAg_FarmBusinessFAD_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_FarmBusinessFAD.ChangeMode(FormViewMode.ReadOnly);
        BindAg_FarmBusinessFAD(Convert.ToInt32(lb.CommandArgument));
        mpeAg_FarmBusinessFAD.Show();
        upAg_FarmBusinessFAD.Update();
    }

    protected void fvAg_FarmBusinessFAD_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessFAD", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_FarmBusinessFAD.ChangeMode(e.NewMode);
            BindAg_FarmBusinessFAD(Convert.ToInt32(fvAg_FarmBusinessFAD.DataKey.Value));
        }
    }

    protected void fvAg_FarmBusinessFAD_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlFADStatus = fvAg_FarmBusinessFAD.FindControl("ddlFADStatus") as DropDownList;
        TextBox tbNote = fvAg_FarmBusinessFAD.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinessFADs.Where(w => w.pk_farmBusinessFAD == Convert.ToInt32(fvAg_FarmBusinessFAD.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlFADStatus.SelectedValue)) a.fk_FAD_code = ddlFADStatus.SelectedValue;
                else sb.Append("FAD Status was not updated. FAD Status is required. ");

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 48);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_FarmBusinessFAD.ChangeMode(FormViewMode.ReadOnly);
                BindAg_FarmBusinessFAD(Convert.ToInt32(fvAg_FarmBusinessFAD.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmBusinessFAD_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlFADStatus = fvAg_FarmBusinessFAD.FindControl("ddlFADStatus") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sStatus = null;
                if (!string.IsNullOrEmpty(ddlFADStatus.SelectedValue)) sStatus = ddlFADStatus.SelectedValue;
                else sb.Append("FAD Status is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmBusinessFAD_add(Convert.ToInt32(fvAg.DataKey.Value), sStatus, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_FarmBusinessFAD.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_FarmBusinessFAD(Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_FarmBusinessFAD_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessFAD", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessFAD_delete(Convert.ToInt32(fvAg_FarmBusinessFAD.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_FarmBusinessFAD_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Land Base Info

    protected void fvAg_LandBaseInfo_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessLandBaseInfo", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_LandBaseInfo.ChangeMode(e.NewMode);
            BindAg_LandBaseInfo(Convert.ToInt32(fvAg_LandBaseInfo.DataKey.Value));
            upAgs.Update();
        }
    }

    protected void fvAg_LandBaseInfo_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        TextBox tbCropDescription = fvAg_LandBaseInfo.FindControl("tbCropDescription") as TextBox;
        TextBox tbCropAcres = fvAg_LandBaseInfo.FindControl("tbCropAcres") as TextBox;
        TextBox tbHayAcres = fvAg_LandBaseInfo.FindControl("tbHayAcres") as TextBox;
        TextBox tbPastureAcres = fvAg_LandBaseInfo.FindControl("tbPastureAcres") as TextBox;
        TextBox tbScrublandAcres = fvAg_LandBaseInfo.FindControl("tbScrublandAcres") as TextBox;
        TextBox tbWoodlandAcres = fvAg_LandBaseInfo.FindControl("tbWoodlandAcres") as TextBox;
        TextBox tbOtherAcres = fvAg_LandBaseInfo.FindControl("tbOtherAcres") as TextBox;
        TextBox tbFarmsteadAcres = fvAg_LandBaseInfo.FindControl("tbFarmsteadAcres") as TextBox;
        TextBox tbResidentialAcres = fvAg_LandBaseInfo.FindControl("tbResidentialAcres") as TextBox;
        TextBox tbCropDescriptionRented = fvAg_LandBaseInfo.FindControl("tbCropDescriptionRented") as TextBox;
        TextBox tbCropAcresRented = fvAg_LandBaseInfo.FindControl("tbCropAcresRented") as TextBox;
        TextBox tbHayAcresRented = fvAg_LandBaseInfo.FindControl("tbHayAcresRented") as TextBox;
        TextBox tbPastureAcresRented = fvAg_LandBaseInfo.FindControl("tbPastureAcresRented") as TextBox;
        TextBox tbWoodlandAcresRented = fvAg_LandBaseInfo.FindControl("tbWoodlandAcresRented") as TextBox;
        TextBox tbOtherAcresRented = fvAg_LandBaseInfo.FindControl("tbOtherAcresRented") as TextBox;
        DropDownList ddlForested = fvAg_LandBaseInfo.FindControl("ddlForested") as DropDownList;
        TextBox tbPaddockCount = fvAg_LandBaseInfo.FindControl("tbPaddockCount") as TextBox;
        TextBox tbRidingRingCount = fvAg_LandBaseInfo.FindControl("tbRidingRingCount") as TextBox;
        DropDownList ddlCommitment480A = fvAg_LandBaseInfo.FindControl("ddlCommitment480A") as DropDownList;
        TextBox tbBarnToH2O = fvAg_LandBaseInfo.FindControl("tbBarnToH2O") as TextBox;
        TextBox tbEOH_PCToH2O = fvAg_LandBaseInfo.FindControl("tbEOH_PCToH2O") as TextBox;
        TextBox tbFieldDistanceToH2O_eoh = fvAg_LandBaseInfo.FindControl("tbFieldDistanceToH2O_eoh") as TextBox;
        //DropDownList ddlEOHBasin = fvAg_LandBaseInfo.FindControl("ddlEOHBasin") as DropDownList;
        TextBox tbComment = fvAg_LandBaseInfo.FindControl("tbComment") as TextBox;
        TextBox tbfarmRankingProfJudgement_eoh = fvAg_LandBaseInfo.FindControl("tbfarmRankingProfJudgement_eoh") as TextBox;


        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinessLandBaseInfos.Where(w => w.pk_farmBusinessLandBaseInfo == Convert.ToInt32(fvAg_LandBaseInfo.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(tbCropDescription.Text)) a.crop_description = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCropDescription.Text, 50);
                else a.crop_description = null;

                try
                {
                    if (!string.IsNullOrEmpty(tbCropAcres.Text)) a.crop_acre = Convert.ToDecimal(tbCropAcres.Text);
                    else a.crop_acre = null;
                }
                catch { sb.Append("Crop Acres Owned not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbHayAcres.Text)) a.hay_acre = Convert.ToDecimal(tbHayAcres.Text);
                    else a.hay_acre = null;
                }
                catch { sb.Append("Hay Acres Owned not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbPastureAcres.Text)) a.pasture_acre = Convert.ToDecimal(tbPastureAcres.Text);
                    else a.pasture_acre = null;
                }
                catch { sb.Append("Pasture Acres Owned not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbScrublandAcres.Text)) a.scrubland_acre = Convert.ToDecimal(tbScrublandAcres.Text);
                    else a.scrubland_acre = null;
                }
                catch { sb.Append("Scrubland Acres Owned not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbWoodlandAcres.Text)) a.woodland_acre = Convert.ToDecimal(tbWoodlandAcres.Text);
                    else a.woodland_acre = null;
                }
                catch { sb.Append("Woodland Acres Owned not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbOtherAcres.Text)) a.other_acre = Convert.ToDecimal(tbOtherAcres.Text);
                    else a.other_acre = null;
                }
                catch { sb.Append("Other Acres Owned not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbFarmsteadAcres.Text)) a.farmstead_acre = Convert.ToDecimal(tbFarmsteadAcres.Text);
                    else a.farmstead_acre = null;
                }
                catch { sb.Append("Farmstead Acres Owned not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbResidentialAcres.Text)) a.residential_acre = Convert.ToDecimal(tbResidentialAcres.Text);
                    else a.residential_acre = null;
                }
                catch { sb.Append("Residential Acres Owned not updated. Data type is numeric (Decimal). "); }

                if (!string.IsNullOrEmpty(tbCropDescriptionRented.Text)) a.crop_description_rent = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCropDescriptionRented.Text, 50);
                else a.crop_description_rent = null;

                try
                {
                    if (!string.IsNullOrEmpty(tbCropAcresRented.Text)) a.crop_acre_rent = Convert.ToDecimal(tbCropAcresRented.Text);
                    else a.crop_acre_rent = null;
                }
                catch { sb.Append("Crop Acres Rented not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbHayAcresRented.Text)) a.hay_acre_rent = Convert.ToDecimal(tbHayAcresRented.Text);
                    else a.hay_acre_rent = null;
                }
                catch { sb.Append("Hay Acres Rented not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbPastureAcresRented.Text)) a.pasture_acre_rent = Convert.ToDecimal(tbPastureAcresRented.Text);
                    else a.pasture_acre_rent = null;
                }
                catch { sb.Append("Pasture Acres Rented not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbWoodlandAcresRented.Text)) a.woodland_acre_rent = Convert.ToDecimal(tbWoodlandAcresRented.Text);
                    else a.woodland_acre_rent = null;
                }
                catch { sb.Append("Woodland Acres Rented not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbOtherAcresRented.Text)) a.other_acre_rent = Convert.ToDecimal(tbOtherAcresRented.Text);
                    else a.other_acre_rent = null;
                }
                catch { sb.Append("Other Acres Rented not updated. Data type is numeric (Decimal). "); }

                if (!string.IsNullOrEmpty(ddlForested.SelectedValue)) a.forested = ddlForested.SelectedValue;
                else a.forested = null;

                try
                {
                    if (!string.IsNullOrEmpty(tbPaddockCount.Text)) a.paddock_cnt = Convert.ToDecimal(tbPaddockCount.Text);
                    else a.paddock_cnt = null;
                }
                catch { sb.Append("Paddock Count not updated. Data type is numeric (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbRidingRingCount.Text)) a.ridingRing_cnt = Convert.ToDecimal(tbRidingRingCount.Text);
                    else a.ridingRing_cnt = null;
                }
                catch { sb.Append("Riding Ring Count not updated. Data type is numeric (Decimal). "); }

                if (!string.IsNullOrEmpty(ddlCommitment480A.SelectedValue)) a.commitment_480A = ddlCommitment480A.SelectedValue;
                else a.commitment_480A = null;

                try
                {
                    if (!string.IsNullOrEmpty(tbBarnToH2O.Text)) a.barnToH2O_ft = Convert.ToInt32(tbBarnToH2O.Text);
                    else a.barnToH2O_ft = null;
                }
                catch { sb.Append("Barn To Water (Feet) not updated. Data type is numeric (Integer). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbEOH_PCToH2O.Text)) a.pollutantCategoryTop3ToH20_ft = Convert.ToInt32(tbEOH_PCToH2O.Text);
                    else a.pollutantCategoryTop3ToH20_ft = null;
                }
                catch { sb.Append("EOH PCs I-III to Water (Feet) not updated. Data type is numeric (Integer). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbFieldDistanceToH2O_eoh.Text)) a.fieldDistanceToH20_eoh = Convert.ToInt32(tbFieldDistanceToH2O_eoh.Text);
                    else a.fieldDistanceToH20_eoh = null;
                }
                catch { sb.Append("EOH Field Distance to Water (Feet) not updated. Data type is numeric (Integer). "); }

                //if (!string.IsNullOrEmpty(ddlEOHBasin.SelectedValue)) a.fk_basin_code_priorityEOH = ddlEOHBasin.SelectedValue;
                //else a.fk_basin_code_priorityEOH = null;

                a.comment = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbComment.Text, 500);

                if (!string.IsNullOrEmpty(tbfarmRankingProfJudgement_eoh.Text))
                {
                    try { a.farmRankingProfJudgement_eoh = Convert.ToInt32(tbfarmRankingProfJudgement_eoh.Text); }
                    catch { sb.Append("PJ Points (EOH) was not updated. Must be a number (Integer). "); }
                }
                else a.farmRankingProfJudgement_eoh = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_LandBaseInfo.ChangeMode(FormViewMode.ReadOnly);
                BindAg_LandBaseInfo(Convert.ToInt32(fvAg_LandBaseInfo.DataKey.Value));
                upAgs.Update();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    #endregion

    #region Event Handling - Ag - Mail
    
    protected void ddlAg_FarmBusinessMail_Participant_Insert_SelectedIndexChanged(object sender, EventArgs e)
    //protected void btnAg_FarmBusinessMail_Participant_Insert_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessMail", "msgInsert"))
        {
            if (!string.IsNullOrEmpty(ddlAg_FarmBusinessMail_Participant_Insert.SelectedValue))
            {
                int? i = null;
                int iCode = 0;
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.farmBusinessMail_add(Convert.ToInt32(fvAg.DataKey.Value), 
                                                                  Convert.ToInt32(ddlAg_FarmBusinessMail_Participant_Insert.SelectedValue),
                                                                  Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            gvAg_FarmBusinessMail.DataSource = wDataContext.farmBusinessMails.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
                            gvAg_FarmBusinessMail.DataBind();
                            WACGlobal_Methods.View_Agriculture_FarmBusinessMail_Candidates_DDL(ddlAg_FarmBusinessMail_Participant_Insert, Convert.ToInt32(fvAg.DataKey.Value), true);
                            SwitchTabContainerTab(_tabFarmOperation);
                        }
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
        }
    }

    protected void lbAg_FarmBusinessMail_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessMail", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessMail_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));
                        gvAg_FarmBusinessMail.DataSource = wDataContext.farmBusinessMails.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
                        gvAg_FarmBusinessMail.DataBind();
                        WACGlobal_Methods.View_Agriculture_FarmBusinessMail_Candidates_DDL(ddlAg_FarmBusinessMail_Participant_Insert, Convert.ToInt32(fvAg.DataKey.Value), true);
                        SwitchTabContainerTab(_tabFarmOperation);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - NMP

    protected void fvAg_NMP_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) 
            bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessNMP", "msgUpdate");

        if (bChangeMode)
        {
            
            fvAg_NMP.ChangeMode(e.NewMode);
            BindAg_NMP(Convert.ToInt32(fvAg_NMP.DataKey.Value));
            upAgs.Update();
        }
    }

    protected void fvAg_NMP_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        FormView fvAg_NMP = (FormView)sender;
        CustomControls_AjaxCalendar tbCalNMPDate = fvAg_NMP.FindControl("tbCalNMPDate") as CustomControls_AjaxCalendar;
        DropDownList ddlNeedsNMP = fvAg_NMP.FindControl("ddlNeedsNMP") as DropDownList;
        DropDownList ddlBasicPlan = fvAg_NMP.FindControl("ddlBasicPlan") as DropDownList;
        DropDownList ddlFollowUpNMP = fvAg_NMP.FindControl("ddlFollowUpNMP") as DropDownList;
        DropDownList ddlEQIP = fvAg_NMP.FindControl("ddlEQIP") as DropDownList;
        DropDownList ddlNMPCredit = fvAg_NMP.FindControl("ddlNMPCredit") as DropDownList;
        DropDownList ddlMTC = (DropDownList)fvAg_NMP.FindControl("ddlMTC");
        TextBox tbStorage = fvAg_NMP.FindControl("tbStorage") as TextBox;
        TextBox tbStatus = fvAg_NMP.FindControl("tbStatus") as TextBox;
        DropDownList ddlDesignerEngineerNMP = fvAg_NMP.FindControl("ddlDesignerEngineerNMP") as DropDownList;
        DropDownList ddlCREP = fvAg_NMP.FindControl("ddlCREP") as DropDownList;
        DropDownList ddlAWEPSignup = fvAg_NMP.FindControl("ddlAWEPSignup") as DropDownList;
        TextBox tbBMPNumberNMP = fvAg_NMP.FindControl("tbBMPNumberNMP") as TextBox;
        TextBox tbBMPNumberWU = fvAg_NMP.FindControl("tbBMPNumberWU") as TextBox;
        TextBox tbBMPNumberREC = fvAg_NMP.FindControl("tbBMPNumberREC") as TextBox;
        TextBox tbEnterprisePrimary = fvAg_NMP.FindControl("tbEnterprisePrimary") as TextBox;
        TextBox tbEnterpriseSecondary = fvAg_NMP.FindControl("tbEnterpriseSecondary") as TextBox;
        TextBox tbAcresPlanned = fvAg_NMP.FindControl("tbAcresPlanned") as TextBox;
        TextBox tbAcresCorn = fvAg_NMP.FindControl("tbAcresCorn") as TextBox;
        TextBox tbGoal = fvAg_NMP.FindControl("tbGoal") as TextBox;
        DropDownList ddlCropYear = fvAg_NMP.FindControl("ddlCropYear") as DropDownList;
        DropDownList ddlCropYearExpiration = fvAg_NMP.FindControl("ddlCropYearExpiration") as DropDownList;
        TextBox tbCropYear = fvAg_NMP.FindControl("tbCropYear") as TextBox;
        TextBox tbCropYearExpiration = fvAg_NMP.FindControl("tbCropYearExpiration") as TextBox;
        CustomControls_AjaxCalendar tbCalNMPOpMaintSignatureDate = fvAg_NMP.FindControl("tbCalNMPOpMaintSignatureDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalWFP3SignatureDate = fvAg_NMP.FindControl("tbCalWFP3SignatureDate") as CustomControls_AjaxCalendar;
        TextBox tbWFP3Year = fvAg_NMP.FindControl("tbWFP3Year") as TextBox;
        CustomControls_AjaxCalendar tbCalMostRecentSampleDate = fvAg_NMP.FindControl("tbCalMostRecentSampleDate") as CustomControls_AjaxCalendar;
        TextBox tbSampleCount = fvAg_NMP.FindControl("tbSampleCount") as TextBox;
        TextBox tbSamplePriority = fvAg_NMP.FindControl("tbSamplePriority") as TextBox;
        TextBox tbSampler = fvAg_NMP.FindControl("tbSampler") as TextBox;
        CustomControls_AjaxCalendar tbCalFSAReleaseDate = fvAg_NMP.FindControl("tbCalFSAReleaseDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalManureSampleDate = fvAg_NMP.FindControl("tbCalManureSampleDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalNMPWorkshopDate = fvAg_NMP.FindControl("tbCalNMPWorkshopDate") as CustomControls_AjaxCalendar;
        TextBox tbWFP1Signatures = fvAg_NMP.FindControl("tbWFP1Signatures") as TextBox;
        CustomControls_AjaxCalendar tbCalSpreaderCalibrationDate = fvAg_NMP.FindControl("tbCalSpreaderCalibrationDate") as CustomControls_AjaxCalendar;
        TextBox tbComment = fvAg_NMP.FindControl("tbComment") as TextBox;
        TextBox tbPhosphorousSaturation_eoh = fvAg_NMP.FindControl("tbPhosphorousSaturation_eoh") as TextBox;
        DropDownList ddlPhosphorousLevel = fvAg_NMP.FindControl("ddlPhosphorousLevel") as DropDownList;
        DropDownList ddlBmpAgNMP = fvAg_NMP.FindControl("ddlBmpAgNMP") as DropDownList;
        DropDownList ddlStorageCode = fvAg_NMP.FindControl("ddlStorageCode") as DropDownList;
        

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            int code = 0;
            try
            {
                DateTime? nmpDate = tbCalNMPDate.CalDateNullable;
                string needsNMP = !string.IsNullOrEmpty(ddlNeedsNMP.SelectedValue) ? ddlNeedsNMP.SelectedValue : null;
                string basicPlan = null;
                if (string.IsNullOrEmpty(ddlBasicPlan.SelectedValue))
                {
                    throw new WAC_Exceptions.WACEX_GeneralDatabaseException(null, 20014);
                }
                else
                {
                    basicPlan = ddlBasicPlan.SelectedValue;
                }
                string fkFollowupNMPCode = !string.IsNullOrEmpty(ddlFollowUpNMP.SelectedValue) ? ddlFollowUpNMP.SelectedValue : null;
                string eqip = !string.IsNullOrEmpty(ddlEQIP.SelectedValue) ? ddlEQIP.SelectedValue : null;
                string nmpCredit = !string.IsNullOrEmpty(ddlNMPCredit.SelectedValue) ? ddlNMPCredit.SelectedValue : null;
                string mtc = !string.IsNullOrEmpty(ddlMTC.SelectedValue) ? ddlMTC.SelectedValue : null;
                string storage = !string.IsNullOrEmpty(ddlStorageCode.SelectedValue) ? ddlStorageCode.SelectedValue : null;
                string status = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbStatus.Text, 255);
                int? designerEngineer = null;
                if (!string.IsNullOrEmpty(ddlDesignerEngineerNMP.SelectedValue))
                    designerEngineer = Convert.ToInt32(ddlDesignerEngineerNMP.SelectedValue);
                string crep = !string.IsNullOrEmpty(ddlCREP.SelectedValue) ? ddlCREP.SelectedValue : null;
                Int16? awepSignup = null;
                if (!string.IsNullOrEmpty(ddlAWEPSignup.SelectedValue))
                    awepSignup = Convert.ToInt16(ddlAWEPSignup.SelectedValue);

                string enterprisePrimary = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEnterprisePrimary.Text, 50);
                string enterpriseSecondary = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEnterpriseSecondary.Text, 50);
                Decimal? unitsDesigned = null;
                if (!string.IsNullOrEmpty(tbAcresPlanned.Text))
                    unitsDesigned = Convert.ToDecimal(tbAcresPlanned.Text);
                int? acresCorn = null;
                if (!string.IsNullOrEmpty(tbAcresCorn.Text))
                    acresCorn = Convert.ToInt32(tbAcresCorn.Text);
                Int16? goal = null;
                if (!string.IsNullOrEmpty(tbGoal.Text))
                    goal = Convert.ToInt16(tbGoal.Text);
                Int16? cropYear = null;
                if (!string.IsNullOrEmpty(ddlCropYear.SelectedValue))
                    cropYear = Convert.ToInt16(ddlCropYear.SelectedValue);
                Int16? cropYearExpiration = null;
                if (!string.IsNullOrEmpty(ddlCropYearExpiration.SelectedValue))
                    cropYearExpiration = Convert.ToInt16(ddlCropYearExpiration.SelectedValue);
                DateTime? omSignatureDate = tbCalNMPOpMaintSignatureDate.CalDateNullable;
                DateTime? completedDate = tbCalWFP3SignatureDate.CalDateNullable;
                Int16? wfp3Year = null;
                if (!string.IsNullOrEmpty(tbWFP3Year.Text))
                    wfp3Year = Convert.ToInt16(tbWFP3Year.Text);
                DateTime? sampleDate = tbCalMostRecentSampleDate.CalDateNullable;
                Int16? sampleCount = null;
                if (!string.IsNullOrEmpty(tbSampleCount.Text))
                    sampleCount = Convert.ToInt16(tbSampleCount.Text);
                Int16? samplePriority = null;
                if (!string.IsNullOrEmpty(tbSamplePriority.Text))
                    samplePriority = Convert.ToInt16(tbSamplePriority.Text);
                string sampler = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbSampler.Text, 255);
                DateTime? fsaReleaseDate = tbCalFSAReleaseDate.CalDateNullable;
                DateTime? manureSampleDate = tbCalManureSampleDate.CalDateNullable;
                DateTime? NMPWorkshopDate = tbCalNMPWorkshopDate.CalDateNullable;
                string wfp1Signatures = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbWFP1Signatures.Text, 50);
                DateTime? spreaderCallibrationDate = tbCalSpreaderCalibrationDate.CalDateNullable;
                string comment = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbComment.Text, 500);
                string fkNMPStorageCode = !string.IsNullOrEmpty(ddlStorageCode.SelectedValue) ? ddlStorageCode.SelectedValue : null;
                int? fkBmpAgNMP = null;
                if (!string.IsNullOrEmpty(ddlBmpAgNMP.SelectedValue))
                    fkBmpAgNMP = Convert.ToInt32(ddlBmpAgNMP.SelectedValue);
                DateTime modified = DateTime.Now;
                string user = Session["userName"].ToString();

                code = wDataContext.farmBusinessNMP_update(Convert.ToInt32(fvAg_NMP.DataKey.Value), nmpDate, needsNMP, basicPlan, fkFollowupNMPCode, eqip, nmpCredit,
                        mtc, status, designerEngineer, crep, enterprisePrimary, enterpriseSecondary, unitsDesigned, acresCorn, goal, cropYear, cropYearExpiration,
                        omSignatureDate, completedDate, wfp3Year, sampleDate, sampleCount, samplePriority, sampler, fsaReleaseDate, manureSampleDate, NMPWorkshopDate,
                        wfp1Signatures, spreaderCallibrationDate, comment, fkNMPStorageCode, awepSignup, fkBmpAgNMP, user);
                fvAg_NMP.ChangeMode(FormViewMode.ReadOnly);
                BindAg_NMP(Convert.ToInt32(fvAg_NMP.DataKey.Value));
                upAgs.Update();
            }
            catch (WAC_Exceptions.WACEX_GeneralDatabaseException we)
            {
                WACAlert.Show(WacRadWindowManager,we.ErrorText, we.ErrorCode);
            }
            catch (Exception ex)
            {
                WACAlert.Show(WacRadWindowManager,"Update NMP failed " + ex.Message, code);
            }
        }      
    }

    #endregion

    #region Event Handling - Ag - Note

    protected void lbAg_Note_Close_Click(object sender, EventArgs e)
    {
        fvAg_Note.ChangeMode(FormViewMode.ReadOnly);
        BindAg_Note(-1);
        mpeAg_Note.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            gvAg_Notes.DataSource = wac.farmBusinessNotes.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderByDescending(o => o.created).Select(s => s);
            gvAg_Notes.DataBind();
        }

        SwitchTabContainerTab(_tabNote);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_Note_Add_Click(object sender, EventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessNote", "msgInsert"))
        //{
            fvAg_Note.ChangeMode(FormViewMode.Insert);
            BindAg_Note(-1);
            mpeAg_Note.Show();
            upAg_Note.Update();
        //}
    }

    protected void lbAg_Note_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_Note.ChangeMode(FormViewMode.ReadOnly);
        BindAg_Note(Convert.ToInt32(lb.CommandArgument));
        mpeAg_Note.Show();
        upAg_Note.Update();
    }

    protected void fvAg_Note_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit)
        {
            //bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessNote", "msgUpdate");
            bChangeMode = WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "farmBusinessNote", Convert.ToInt32(fvAg_Note.DataKey.Value));
        } 
        if (bChangeMode)
        {
            fvAg_Note.ChangeMode(e.NewMode);
            BindAg_Note(Convert.ToInt32(fvAg_Note.DataKey.Value));
            
        }
    }

    protected void fvAg_Note_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlNoteType = fvAg_Note.FindControl("ddlNoteType") as DropDownList;
        TextBox tbNote = fvAg_Note.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmBusinessNotes.Where(w => w.pk_farmBusinessNote == Convert.ToInt32(fvAg_Note.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlNoteType.SelectedValue)) a.fk_farmBusinessNoteType_code = ddlNoteType.SelectedValue;
                else sb.Append("Note Type was not updated. Note Type is required. ");

                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000);
                else sb.Append("Note was not updated. Note is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_Note.ChangeMode(FormViewMode.ReadOnly);
                BindAg_Note(Convert.ToInt32(fvAg_Note.DataKey.Value));
                CreateAgOverviewGrid(Convert.ToInt32(fvAg.DataKey.Value), null);
                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Note_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlNoteType = fvAg_Note.FindControl("ddlNoteType") as DropDownList;
        TextBox tbNote = fvAg_Note.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sNoteType = null;
                if (!string.IsNullOrEmpty(ddlNoteType.SelectedValue)) sNoteType = ddlNoteType.SelectedValue;
                else sb.Append("Note Type is required. ");

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000);
                else sb.Append("Note is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmBusinessNote_add(Convert.ToInt32(fvAg.DataKey.Value), sNoteType, sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_Note.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_Note(Convert.ToInt32(i));
                        CreateAgOverviewGrid(Convert.ToInt32(fvAg.DataKey.Value), null);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Note_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessNote", "msgDelete"))
        //{
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    if (WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "farmBusinessNote", Convert.ToInt32(fvAg_Note.DataKey.Value)))
                    {
                        iCode = wDataContext.farmBusinessNote_delete(Convert.ToInt32(fvAg_Note.DataKey.Value), Session["userName"].ToString());
                        CreateAgOverviewGrid(Convert.ToInt32(fvAg.DataKey.Value), null);
                        if (iCode == 0) lbAg_Note_Close_Click(null, null);
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        //}
    }

    #endregion

    #region Event Handling - Ag - Operator

    protected void btnAg_Operator_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessOperator", "msgInsert"))
        {
            if (!string.IsNullOrEmpty(ddlAg_Operator_Add.SelectedValue))
            {
                int? i = null;
                int iCode = 0;

                int iOperator = Convert.ToInt32(ddlAg_Operator_Add.SelectedValue);
                string sActive = rblAg_Operator_Active.SelectedValue;
                string sMaster = rblAg_Operator_Master.SelectedValue;

                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.farmBusinessOperator_add(Convert.ToInt32(fvAg.DataKey.Value), iOperator, sMaster, sActive, Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOperator, null, false, false, true);
                            SwitchTabContainerTab(_tabFarmOperation);
                        }
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
            else WACAlert.Show(WacRadWindowManager,"You must select an Operator.", 0);
        }
    }

    protected void lbAg_Operator_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessOperator", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessOperator_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOperator, null, false, false, false);
                        SwitchTabContainerTab(_tabFarmOperation);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Owner

    protected void btnAg_FarmBusinessOwner_Insert_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessOwner", "msgInsert"))
        {
            int? i = null;
            int iCode = 0;

            DropDownList ddl = UC_DropDownListByAlphabet_Ag_FarmBusinessOwner.FindControl("ddl") as DropDownList;

            if (!string.IsNullOrEmpty(ddl.SelectedValue))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        int iOwner = Convert.ToInt32(ddl.SelectedValue);
                        string sMaster = rblAg_FarmBusinessOwner_Master.SelectedValue;
                        string sActive = rblAg_FarmBusinessOwner_Active.SelectedValue;
                        iCode = wDataContext.farmBusinessOwner_add(Convert.ToInt32(fvAg.DataKey.Value), iOwner, sMaster, sActive, Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {

                            Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOwner, null, false, false, false);
                            Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessMail, null, true, true, false);
                            SwitchTabContainerTab(_tabFarmOperation);
                        }
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
        }
    }

    protected void lbAg_FarmBusinessOwner_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessOwner", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessOwner_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0)
                    {

                        Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOwner, null, false, false, false);
                        Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessMail, null, true, true, false);
                        SwitchTabContainerTab(_tabFarmOperation);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Planner

    protected void btnAg_Planner_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessPlanner", "msgInsert"))
        {
            if (!string.IsNullOrEmpty(ddlAg_Planner_Add.SelectedValue))
            {
                int? i = null;
                int iCode = 0;

                int iPlanner = Convert.ToInt32(ddlAg_Planner_Add.SelectedValue);
                string sMaster = rblAg_Planner_Master.SelectedValue;

                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.farmBusinessPlanner_add(Convert.ToInt32(fvAg.DataKey.Value), iPlanner, sMaster, Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessPlanner, null, true, false, true);
                            SwitchTabContainerTab(_tabFarmOperation);
                        }
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
            else WACAlert.Show(WacRadWindowManager,"You must select an Planner.", 0);
        }
    }

    protected void lbAg_Planner_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessPlanner", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessPlanner_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessPlanner, null, true, false, false);
                        SwitchTabContainerTab(_tabFarmOperation);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Tax Parcel

    private void FarmBusinessTaxParcel_Insert(DropDownList ddlJurisdiction, DropDownList ddlTaxParcel)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessTaxParcel", "msgInsert"))
        {
            if (ddlJurisdiction != null && ddlTaxParcel != null)
            {
                int? i = null;
                int iCode = 0;
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wac.taxParcel_add_express(ddlJurisdiction.SelectedItem.Value, ddlTaxParcel.SelectedItem.Value, null, Session["userName"].ToString(), ref i);
                        if (iCode == 0 && i != null)
                        {
                            iCode = wac.farmBusinessTaxParcel_add(Convert.ToInt32(fvAg.DataKey.Value), Convert.ToInt32(i), Session["userName"].ToString(), ref i);
                            if (iCode == 0)
                            {
                                //BindAg(Convert.ToInt32(fvAg.DataKey.Value));
                                gvAg_TaxParcels.DataSource = wac.farmBusinessTaxParcels.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.taxParcel.list_swi.county).ThenBy(o => o.taxParcel.list_swi.jurisdiction).ThenBy(o => o.taxParcel.taxParcelID).Select(s => s);
                                gvAg_TaxParcels.DataBind();
                                //UC_ControlGroup_TaxParcel1.ResetControls();

                                // Update Farm Business Owners
                                Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOwner, null, false, false, true);

                                // Update Farm Business Mail
                                gvAg_FarmBusinessMail.DataSource = wac.farmBusinessMails.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
                                gvAg_FarmBusinessMail.DataBind();
                                WACGlobal_Methods.View_Agriculture_FarmBusinessMail_Candidates_DDL(ddlAg_FarmBusinessMail_Participant_Insert, Convert.ToInt32(fvAg.DataKey.Value), true);

                                SwitchTabContainerTab(_tabFarmOperation);
                            }
                            else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                        }
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
                ddlTaxParcel.SelectedIndex = -1;
                //JWJ 09/25/2012 per BA:899 - Refresh Land Base Info when adding or deleting a tax parcel
                ReBindAg_LandBaseInfo(Convert.ToInt32(fvAg.DataKey.Value));
            }
        }
    }

    protected void lbAg_TaxParcel_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessTaxParcel", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wac.farmBusinessTaxParcel_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));
                        gvAg_TaxParcels.DataSource = wac.farmBusinessTaxParcels.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.taxParcel.list_swi.county).ThenBy(o => o.taxParcel.list_swi.jurisdiction).ThenBy(o => o.taxParcel.taxParcelID).Select(s => s);
                        gvAg_TaxParcels.DataBind();
                        SwitchTabContainerTab(_tabFarmOperation);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
            //JWJ 09/25/2012 per BA:899 - Refresh Land Base Info when adding or deleting a tax parcel
            ReBindAg_LandBaseInfo(Convert.ToInt32(fvAg.DataKey.Value));
        }
    }

    #endregion

    #region Event Handling - Ag - Tier1

    protected void fvAg_Tier1_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        FormView fv = (FormView)sender;
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) 
            bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessTier1", "msgUpdate");
        if (bChangeMode)
        {
            fv.ChangeMode(e.NewMode);
            if (e.NewMode == FormViewMode.Insert)
                BindAg_Tier1(-1);
            else
                BindAg_Tier1(Convert.ToInt32(fv.DataKey.Value));
            upAgs.Update();
        }
    }

    protected void fvAg_Tier1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        FormView fvAg_Tier1 = (FormView)sender;
        StringBuilder sb = new StringBuilder();

        TextBox tbTier2Points = fvAg_Tier1.FindControl("tbTier2Points") as TextBox;
        TextBox tbEOHAUManual = fvAg_Tier1.FindControl("tbEOHAUManual") as TextBox;
        TextBox tbRanking = fvAg_Tier1.FindControl("tbRanking") as TextBox;
        TextBox tbFarmNameLocation = fvAg_Tier1.FindControl("tbFarmNameLocation") as TextBox;
        TextBox tbFarmDescription = fvAg_Tier1.FindControl("tbFarmDescription") as TextBox;
        TextBox tbAcresRent = fvAg_Tier1.FindControl("tbAcresRent") as TextBox;
        TextBox tbAcresForest = fvAg_Tier1.FindControl("tbAcresForest") as TextBox;
        TextBox tbAcresTill = fvAg_Tier1.FindControl("tbAcresTill") as TextBox;
        TextBox tbAcresTillRent = fvAg_Tier1.FindControl("tbAcresTillRent") as TextBox;
        TextBox tbAcresHay = fvAg_Tier1.FindControl("tbAcresHay") as TextBox;
        TextBox tbAcresPasture = fvAg_Tier1.FindControl("tbAcresPasture") as TextBox;
        CustomControls_AjaxCalendar calTier1Received = fvAg_Tier1.FindControl("calTier1Received") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar calTier2Received = fvAg_Tier1.FindControl("calTier2Received") as CustomControls_AjaxCalendar;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                decimal? tier2_points = null; 
                if (!string.IsNullOrEmpty(tbTier2Points.Text))
                {
                    try { tier2_points = Convert.ToDecimal(tbTier2Points.Text); }
                    catch { sb.Append("Tier 2 Points cannot be null. Data Type is number (Decimal). "); }
                }
                decimal? eoh_au_manual = null;
                if (!string.IsNullOrEmpty(tbEOHAUManual.Text))
                {
                    try { eoh_au_manual = Convert.ToDecimal(tbEOHAUManual.Text); }
                    catch { sb.Append("EOH AU Manual was not updated. Data Type is number (Decimal). "); }
                }
                int ranking = 0;
                if (!string.IsNullOrEmpty(tbRanking.Text))
                {
                    try { ranking = Convert.ToInt32(tbRanking.Text); }
                    catch { sb.Append("Ranking was not updated. Data Type is number (Integer). "); }
                }

                String farmNameLocation = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbFarmNameLocation.Text, 48).Trim();
                String farmDesc = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbFarmDescription.Text, 48).Trim();

                decimal? acres_rent = 0;
                if (!string.IsNullOrEmpty(tbAcresRent.Text))
                {
                    try { acres_rent = Convert.ToDecimal(tbAcresRent.Text); }
                    catch { sb.Append("Acres Rent was not updated. Data Type is number (Decimal). "); }
                }
                decimal? acres_forest = 0;
                if (!string.IsNullOrEmpty(tbAcresForest.Text))
                {
                    try { acres_forest = Convert.ToDecimal(tbAcresForest.Text); }
                    catch { sb.Append("Acres Forest was not updated. Data Type is number (Decimal). "); }
                }
                decimal? acres_till = 0;
                if (!string.IsNullOrEmpty(tbAcresTill.Text))
                {
                    try { acres_till = Convert.ToDecimal(tbAcresTill.Text); }
                    catch { sb.Append("Acres Crop was not updated. Data Type is number (Decimal). "); }
                }
                decimal? acres_till_rent = 0;
                if (!string.IsNullOrEmpty(tbEOHAUManual.Text))
                {
                    try { acres_till_rent = Convert.ToDecimal(tbAcresTillRent.Text); }
                    catch { sb.Append("Acres Crop Rent was not updated. Data Type is number (Decimal). "); }
                }
                decimal? acres_hay = 0;
                if (!string.IsNullOrEmpty(tbAcresHay.Text))
                {
                    try { acres_hay = Convert.ToDecimal(tbAcresHay.Text); }
                    catch { sb.Append("Acres Hay was not updated. Data Type is number (Decimal). "); }
                }
                decimal? acres_pasture = 0;
                if (!string.IsNullOrEmpty(tbAcresPasture.Text))
                {
                    try { acres_pasture = Convert.ToDecimal(tbAcresPasture.Text); }
                    catch { sb.Append("Acres Pasture was not updated. Data Type is number (Decimal). "); }
                }
                decimal? acres_total = acres_forest + acres_hay + acres_pasture + acres_rent + acres_till + acres_till_rent;

                DateTime? tier1_recd = calTier1Received.CalDateNullable;
                DateTime? tier2_recd = calTier2Received.CalDateNullable;

                string created_by = Session["userName"].ToString();
                int? pk = null;
                int iCode = 0;
                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.AEM_add(PK_FarmBusiness, tier1_recd, tier2_recd, tier2_points, eoh_au_manual, ranking, 
                    farmNameLocation, farmDesc, acres_rent, acres_forest, acres_till, acres_till_rent, acres_hay, acres_pasture, 
                    acres_total, null, null, created_by, ref pk);
                    if (iCode == 0)
                    {
                        fvAg_Tier1.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_Tier1((int)pk);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database. " + sb.ToString(), iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);               
                upAgs.Update();              
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Tier1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        FormView fvAg_Tier1 = (FormView)sender;

        TextBox tbTier2Points = fvAg_Tier1.FindControl("tbTier2Points") as TextBox;
        //TextBox tbfarmRankingJudgement_eoh = fvAg_Tier1.FindControl("tbfarmRankingJudgement_eoh") as TextBox;
        TextBox tbEOHAUManual = fvAg_Tier1.FindControl("tbEOHAUManual") as TextBox;
        TextBox tbRanking = fvAg_Tier1.FindControl("tbRanking") as TextBox;
        TextBox tbFarmNameLocation = fvAg_Tier1.FindControl("tbFarmNameLocation") as TextBox;
        TextBox tbFarmDescription = fvAg_Tier1.FindControl("tbFarmDescription") as TextBox;
        TextBox tbAcresRent = fvAg_Tier1.FindControl("tbAcresRent") as TextBox;
        TextBox tbAcresForest = fvAg_Tier1.FindControl("tbAcresForest") as TextBox;
        TextBox tbAcresTill = fvAg_Tier1.FindControl("tbAcresTill") as TextBox;
        TextBox tbAcresTillRent = fvAg_Tier1.FindControl("tbAcresTillRent") as TextBox;
        TextBox tbAcresHay = fvAg_Tier1.FindControl("tbAcresHay") as TextBox;
        TextBox tbAcresPasture = fvAg_Tier1.FindControl("tbAcresPasture") as TextBox;
      //  TextBox tbAcresTotal = fvAg_Tier1.FindControl("tbAcresTotal") as TextBox;
        DropDownList ddlEOHIncome1K = fvAg_Tier1.FindControl("ddlEOHIncome1K") as DropDownList;
        //Calendar calEOHLastUpdate = fvAg_Tier1.FindControl("UC_EditCalendar_Ag_Tier1_EOHLastUpdate").FindControl("cal") as Calendar;
        CustomControls_AjaxCalendar tbCalEOHLastUpdate = fvAg_Tier1.FindControl("tbCalEOHLastUpdate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar calTier1Received = fvAg_Tier1.FindControl("calTier1Received") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar calTier2Received = fvAg_Tier1.FindControl("calTier2Received") as CustomControls_AjaxCalendar;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmBusinessTier1s.Where(w => w.pk_farmBusinessTier1 == Convert.ToInt32(fvAg_Tier1.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(tbTier2Points.Text))
                {
                    try { a.tier2_points = Convert.ToDecimal(tbTier2Points.Text); }
                    catch { sb.Append("Tier 2 Points was not updated. Data Type is number (Decimal). "); }
                }
                else a.tier2_points = null;

                if (!string.IsNullOrEmpty(tbEOHAUManual.Text))
                {
                    try  {  a.eoh_au_manual = Convert.ToDecimal(tbEOHAUManual.Text); }
                    catch { sb.Append("EOH AU Manual was not updated. Data Type is number (Decimal). "); }
                }
                else a.eoh_au_manual = null;

                if (!string.IsNullOrEmpty(tbRanking.Text))
                {
                    try { a.ranking = Convert.ToInt32(tbRanking.Text); }
                    catch { sb.Append("Ranking was not updated. Data Type is number (Integer). "); }
                }
                else a.ranking = null;

                a.farmNameLocation = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbFarmNameLocation.Text, 48).Trim();

                a.farm_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbFarmDescription.Text, 48).Trim();

                if (!string.IsNullOrEmpty(tbAcresRent.Text))
                {
                    try { a.acres_rent = Convert.ToDecimal(tbAcresRent.Text); }
                    catch { sb.Append("Acres Rent was not updated. Data Type is number (Decimal). "); }
                }
                else a.acres_rent = null;

                if (!string.IsNullOrEmpty(tbAcresForest.Text))
                {
                    try { a.acres_forest = Convert.ToDecimal(tbAcresForest.Text); }
                    catch { sb.Append("Acres Forest was not updated. Data Type is number (Decimal). "); }
                }
                else a.acres_forest = null;

                if (!string.IsNullOrEmpty(tbAcresTill.Text))
                {
                    try { a.acres_till = Convert.ToDecimal(tbAcresTill.Text); }
                    catch { sb.Append("Acres Crop was not updated. Data Type is number (Decimal). "); }
                }
                else a.acres_till = null;

                if (!string.IsNullOrEmpty(tbEOHAUManual.Text))
                {
                    try { a.acres_till_rent = Convert.ToDecimal(tbAcresTillRent.Text); }
                    catch { sb.Append("Acres Crop Rent was not updated. Data Type is number (Decimal). "); }
                }
                else a.acres_till_rent = null;

                if (!string.IsNullOrEmpty(tbAcresHay.Text))
                {
                    try { a.acres_hay = Convert.ToDecimal(tbAcresHay.Text); }
                    catch { sb.Append("Acres Hay was not updated. Data Type is number (Decimal). "); }
                }
                else a.acres_hay = null;

                if (!string.IsNullOrEmpty(tbAcresPasture.Text))
                {
                    try { a.acres_pasture = Convert.ToDecimal(tbAcresPasture.Text); }
                    catch { sb.Append("Acres Pasture was not updated. Data Type is number (Decimal). "); }
                }
                else a.acres_pasture = null;
                a.acres_total = a.acres_forest + a.acres_hay + a.acres_pasture + a.acres_till + a.acres_till_rent + a.acres_rent;
                //if (!string.IsNullOrEmpty(tbAcresTotal.Text))
                //{
                //    try { a.acres_total = Convert.ToDecimal(tbAcresTotal.Text); }
                //    catch { sb.Append("Acres Total was not updated. Data Type is number (Decimal). "); }
                //}
                //else a.acres_total = null;

                if (!string.IsNullOrEmpty(ddlEOHIncome1K.SelectedValue)) a.eoh_income1K = ddlEOHIncome1K.SelectedValue;
                else a.eoh_income1K = null;

                a.eoh_lastUpdate = tbCalEOHLastUpdate.CalDateNullable;
                a.tier1_recd = calTier1Received.CalDateNullable;
                a.tier2_recd = calTier2Received.CalDateNullable;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_Tier1.ChangeMode(FormViewMode.ReadOnly);
                BindAg_Tier1(Convert.ToInt32(fvAg_Tier1.DataKey.Value));
                upAgs.Update();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    #endregion

    #region Event Handling - Ag - Tier1 - Animal

    protected void lbAg_Tier1_Animal_Close_Click(object sender, EventArgs e)
    {
        fvAg_Tier1_Animal.ChangeMode(FormViewMode.ReadOnly);
        BindAg_Tier1_Animal(-1);
        mpeAg_Tier1_Animal.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            GridView gvAg_Tier1_Animals = fvAg_Tier1.FindControl("gvAg_Tier1_Animals") as GridView;
            gvAg_Tier1_Animals.DataSource = wac.farmBusinessTier1Animals.Where(w => w.fk_farmBusinessTier1 == Convert.ToInt32(fvAg_Tier1.DataKey.Value)).OrderBy(o => o.list_animal.animal).Select(s => s);
            gvAg_Tier1_Animals.DataBind();
        }

        SwitchTabContainerTab(_tabTier1);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_Tier1_Animal_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessTier1Animal", "msgInsert"))
        {
            fvAg_Tier1_Animal.ChangeMode(FormViewMode.Insert);
            BindAg_Tier1_Animal(-1);
            mpeAg_Tier1_Animal.Show();
            upAg_Tier1_Animal.Update();
        }
    }

    protected void lbAg_Tier1_Animal_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_Tier1_Animal.ChangeMode(FormViewMode.ReadOnly);
        BindAg_Tier1_Animal(Convert.ToInt32(lb.CommandArgument));
        mpeAg_Tier1_Animal.Show();
        upAg_Tier1_Animal.Update();
    }

    protected void fvAg_Tier1_Animal_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessTier1Animal", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_Tier1_Animal.ChangeMode(e.NewMode);
            BindAg_Tier1_Animal(Convert.ToInt32(fvAg_Tier1_Animal.DataKey.Value));
        }
    }

    protected void fvAg_Tier1_Animal_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlAnimal = fvAg_Tier1_Animal.FindControl("ddlAnimal") as DropDownList;
        TextBox tbCount = fvAg_Tier1_Animal.FindControl("tbCount") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.farmBusinessTier1Animals.Where(w => w.pk_farmBusinessTier1Animal == Convert.ToInt32(fvAg_Tier1_Animal.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlAnimal.SelectedValue)) a.fk_list_animal = Convert.ToInt32(ddlAnimal.SelectedValue);
                else sb.Append("Animal was not updated. Animal is required. ");

                try
                {
                    int? iCount = Convert.ToInt32(tbCount.Text);
                    if (iCount > 0) a.cnt = iCount;
                    else sb.Append("Count was not updated. Count must be greater than 0. ");
                }
                catch { sb.Append("Count was not updated. Count must be a number greater than 0. "); }

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_Tier1_Animal.ChangeMode(FormViewMode.ReadOnly);
                BindAg_Tier1_Animal(Convert.ToInt32(fvAg_Tier1_Animal.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Tier1_Animal_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlAnimal = fvAg_Tier1_Animal.FindControl("ddlAnimal") as DropDownList;
        TextBox tbCount = fvAg_Tier1_Animal.FindControl("tbCount") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iAnimal = null;
                if (!string.IsNullOrEmpty(ddlAnimal.SelectedValue)) iAnimal = Convert.ToInt32(ddlAnimal.SelectedValue);
                else sb.Append("Animal is required. ");

                int? iCount = null;
                try
                {
                    iCount = Convert.ToInt32(tbCount.Text);
                    if (iCount <= 0) sb.Append("Count must be greater than 0. ");
                }
                catch { sb.Append("Count is required. Count must be a number greater than 0. "); }

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.farmBusinessTier1Animal_add(Convert.ToInt32(fvAg_Tier1.DataKey.Value), iAnimal, iCount, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_Tier1_Animal.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_Tier1_Animal(Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_Tier1_Animal_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessTier1Animal", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessTier1Animal_delete(Convert.ToInt32(fvAg_Tier1_Animal.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_Tier1_Animal_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Type

    protected void ddlAg_Type_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessType", "msgInsert"))
        {
            if (!string.IsNullOrEmpty(ddlAg_Type_Add.SelectedValue))
            {
                int? i = null;
                int iCode = 0;
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.farmBusinessType_add(Convert.ToInt32(fvAg.DataKey.Value), ddlAg_Type_Add.SelectedValue, Session["userName"].ToString(), ref i);
                        if (iCode == 0)
                        {
                            gvAg_FarmTypes.DataSource = wDataContext.farmBusinessTypes.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.list_farmType.farmType).Select(s => s);
                            gvAg_FarmTypes.DataBind();
                            ddlAg_Type_Add.SelectedIndex = 0;
                            SwitchTabContainerTab(_tabFarmOperation);
                        }
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
        }
    }

    protected void lbAg_Type_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessType", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessType_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0)
                    {
                        gvAg_FarmTypes.DataSource = wDataContext.farmBusinessTypes.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.list_farmType.farmType).Select(s => s);
                        gvAg_FarmTypes.DataBind();
                        SwitchTabContainerTab(_tabFarmOperation);
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - WFP2

    //protected void lbAg_WFP2_Click(object sender, EventArgs e)
    //{
    //    WACAG_WFP2.Bind(PK_FarmBusiness);
    //    upAg_WFP2.Update();
    //    mpeAg_WFP2.Show();

    //}

    protected void lbAg_WFP2_Close_Click(object sender, EventArgs e)
    {
         mpeAg_WFP2.Hide();
        //rebind the WFP2 tab to get any updates
        AjaxControlToolkit.TabContainer tc = fvAg.FindControl("tcAg") as AjaxControlToolkit.TabContainer;
        if (tc != null)
        {
            BindAgOnDemand(tc);
            upAgs.Update();
        }
    }

    protected void ddlAg_WFP2_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        AjaxControlToolkit.TabPanel tp = (AjaxControlToolkit.TabPanel)ddl.NamingContainer;
        List<WACParameter> parms = new List<WACParameter>();
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int i = Convert.ToInt32(ddl.SelectedValue);
                var wfp2 = wac.form_wfp2s.Where(w => w.pk_form_wfp2 == i).Select(s => s);
                fvAg_WFP2.DataKeyNames = new string[] { "pk_form_wfp2" };
                fvAg_WFP2.DataSource = wfp2.ToList();
                fvAg_WFP2.DataBind();
                Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)tp.FindControl("WACUT_AttachedDocumentViewerWFP2");
                parms.Add(new WACParameter("pk_farmBusiness", wfp2.First().fk_farmBusiness, WACParameter.ParameterType.MasterKey));
                parms.Add(new WACParameter("pk_form_wfp2", i, WACParameter.ParameterType.PrimaryKey));
                docView.InitControl(parms);
                GridView gvAg_WFP2_Revisions = fvAg_WFP2.FindControl("gvAg_WFP2_Revisions") as GridView;
                gvAg_WFP2_Revisions.DataSource = wac.form_wfp2_versions.Where(w => w.fk_form_wfp2 == i).Select(s => s);
                gvAg_WFP2_Revisions.DataBind();
            }
            catch { }
            parms = null;
        }
    }

    protected void fvAg_WFP2_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp2", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_WFP2.ChangeMode(e.NewMode);
            BindAg_WFP2(Convert.ToInt32(fvAg_WFP2.DataKey.Value));
            upAgs.Update();
        }
    }

    protected void fvAg_WFP2_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sbErrorCollection = new StringBuilder();

        TextBox tbPollutantI = fvAg_WFP2.FindControl("tbPollutantI") as TextBox;
        TextBox tbPollutantII = fvAg_WFP2.FindControl("tbPollutantII") as TextBox;
        TextBox tbPollutantIII = fvAg_WFP2.FindControl("tbPollutantIII") as TextBox;
        TextBox tbPollutantIV = fvAg_WFP2.FindControl("tbPollutantIV") as TextBox;
        TextBox tbPollutantV = fvAg_WFP2.FindControl("tbPollutantV") as TextBox;
        TextBox tbPollutantV2_CREP = fvAg_WFP2.FindControl("tbPollutantV2_CREP") as TextBox;
        TextBox tbPollutantVI = fvAg_WFP2.FindControl("tbPollutantVI") as TextBox;
        TextBox tbPollutantVII = fvAg_WFP2.FindControl("tbPollutantVII") as TextBox;
        TextBox tbPollutantVIII = fvAg_WFP2.FindControl("tbPollutantVIII") as TextBox;
        TextBox tbPollutantIX = fvAg_WFP2.FindControl("tbPollutantIX") as TextBox;
        TextBox tbPollutantX = fvAg_WFP2.FindControl("tbPollutantX") as TextBox;
        TextBox tbPollutantXI = fvAg_WFP2.FindControl("tbPollutantXI") as TextBox;
        DropDownList ddlPlanner = fvAg_WFP2.FindControl("ddlPlanner") as DropDownList;
        DropDownList ddlPlannerTechnician1 = fvAg_WFP2.FindControl("ddlPlannerTechnician1") as DropDownList;
        DropDownList ddlPlannerTechnician2 = fvAg_WFP2.FindControl("ddlPlannerTechnician2") as DropDownList;
        DropDownList ddlAgency = fvAg_WFP2.FindControl("ddlAgency") as DropDownList;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp2s.Where(w => w.pk_form_wfp2 == Convert.ToInt32(fvAg_WFP2.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(tbPollutantI.Text)) a.pollutant_i_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantI.Text, 1000);
                else sbErrorCollection.Append("Pollutant I was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantII.Text)) a.pollutant_ii_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantII.Text, 1000);
                else sbErrorCollection.Append("Pollutant II was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantIII.Text)) a.pollutant_iii_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantIII.Text, 1000);
                else sbErrorCollection.Append("Pollutant III was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantIV.Text)) a.pollutant_iv_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantIV.Text, 1000);
                else sbErrorCollection.Append("Pollutant IV was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantV.Text)) a.pollutant_v_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantV.Text, 1000);
                else sbErrorCollection.Append("Pollutant V was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantV2_CREP.Text)) a.pollutant_v2_descrip_CREP = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantV2_CREP.Text, 1000);
                else a.pollutant_v2_descrip_CREP = null;

                if (!string.IsNullOrEmpty(tbPollutantVI.Text)) a.pollutant_vi_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantVI.Text, 1000);
                else sbErrorCollection.Append("Pollutant VI was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantVII.Text)) a.pollutant_vii_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantVII.Text, 1000);
                else sbErrorCollection.Append("Pollutant VII was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantVIII.Text)) a.pollutant_viii_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantVIII.Text, 1000);
                else sbErrorCollection.Append("Pollutant VIII was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantIX.Text)) a.pollutant_ix_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantIX.Text, 1000);
                else sbErrorCollection.Append("Pollutant IX was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantX.Text)) a.pollutant_x_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantX.Text, 1000);
                else sbErrorCollection.Append("Pollutant X was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(tbPollutantXI.Text)) a.pollutant_xi_descrip = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbPollutantXI.Text, 1000);
                else sbErrorCollection.Append("Pollutant XI was not updated due to empty input text. Must have a description.");

                if (!string.IsNullOrEmpty(ddlPlanner.SelectedValue)) a.fk_list_designerEngineer = Convert.ToInt32(ddlPlanner.SelectedValue);
                else a.fk_list_designerEngineer = null;

                if (!string.IsNullOrEmpty(ddlPlannerTechnician1.SelectedValue)) a.fk_list_designerEngineer_planner2 = Convert.ToInt32(ddlPlannerTechnician1.SelectedValue);
                else a.fk_list_designerEngineer_planner2 = null;

                if (!string.IsNullOrEmpty(ddlPlannerTechnician2.SelectedValue)) a.fk_list_designerEngineer_planner3 = Convert.ToInt32(ddlPlannerTechnician2.SelectedValue);
                else a.fk_list_designerEngineer_planner3 = null;

                if (!string.IsNullOrEmpty(ddlAgency.SelectedValue)) a.fk_agency_code = ddlAgency.SelectedValue;
                else a.fk_agency_code = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_WFP2.ChangeMode(FormViewMode.ReadOnly);
                BindAg_WFP2(Convert.ToInt32(fvAg_WFP2.DataKey.Value));

                GridView gvAg_WFP2_Revisions = fvAg_WFP2.FindControl("gvAg_WFP2_Revisions") as GridView;
                gvAg_WFP2_Revisions.DataSource = wDataContext.form_wfp2_versions.Where(w => w.fk_form_wfp2 == Convert.ToInt32(fvAg_WFP2.DataKey.Value)).ToList();
                gvAg_WFP2_Revisions.DataBind();

                upAgs.Update();

                if (!string.IsNullOrEmpty(sbErrorCollection.ToString())) WACAlert.Show(WacRadWindowManager,sbErrorCollection.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    #endregion

    #region Event Handling - Ag - WFP2 - Version

    protected void lbAg_WFP2_Version_Close_Click(object sender, EventArgs e)
    {
        fvAg_WFP2_Version.ChangeMode(FormViewMode.ReadOnly);
        BindAg_WFP2_Version(-1);
        mpeAg_WFP2_Version.Hide();
        //BindAg(Convert.ToInt32(fvAg.DataKey.Value));

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            GridView gvAg_WFP2_Revisions = fvAg_WFP2.FindControl("gvAg_WFP2_Revisions") as GridView;
            gvAg_WFP2_Revisions.DataSource = wac.form_wfp2_versions.Where(w => w.fk_form_wfp2 == Convert.ToInt32(fvAg_WFP2.DataKey.Value)).Select(s => s);
            gvAg_WFP2_Revisions.DataBind();
            var g = WACGlobal_Methods.DataBaseFunction_Agriculture_BMP_Grid_All(Convert.ToInt32(fvAg.DataKey.Value), new int?[] { 0 });
            //if (LimitBMPList)
            //    gvAg_BMPs.DataSource = g.Where(w => w.fk_statusBMP_code == "A" || w.fk_statusBMP_code == "DR");
            //else
                gvAg_BMPs.DataSource = g;
            gvAg_BMPs.DataBind();
        }

        SwitchTabContainerTab(_tabWFP2);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_WFP2_Version_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp2_version", "msgInsert"))
        {
            bool bCanInsert = true;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.form_wfp2_versions.Where(w => w.fk_form_wfp2 == Convert.ToInt32(fvAg_WFP2.DataKey.Value)).OrderByDescending(o => o.version).Select(s => s);
                if (x.Count() >= 1)
                {
                    if (x.First().approved_date == null) bCanInsert = false;
                }
            }
            if (bCanInsert)
            {
                fvAg_WFP2_Version.ChangeMode(FormViewMode.Insert);
                BindAg_WFP2_Version(-1);
                mpeAg_WFP2_Version.Show();
                upAg_WFP2_Version.Update();
            }
            else WACAlert.Show(WacRadWindowManager,"The existing/current Revision must be approved before a new Revision can be issued.", 0);
        }
    }

    protected void lbAg_WFP2_Version_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_WFP2_Version.ChangeMode(FormViewMode.ReadOnly);
        BindAg_WFP2_Version(Convert.ToInt32(lb.CommandArgument));
        mpeAg_WFP2_Version.Show();
        upAg_WFP2_Version.Update();
    }

    protected void fvAg_WFP2_Version_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp2_version", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_WFP2_Version.ChangeMode(e.NewMode);
            BindAg_WFP2_Version(Convert.ToInt32(fvAg_WFP2_Version.DataKey.Value));
        }
    }

    protected void fvAg_WFP2_Version_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        DropDownList ddlInhouseRevision = fvAg_WFP2_Version.FindControl("ddlInhouseRevision") as DropDownList;
        CustomControls_AjaxCalendar tbCalSetupDate = fvAg_WFP2_Version.FindControl("tbCalSetupDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalApprovedDate = fvAg_WFP2_Version.FindControl("tbCalApprovedDate") as CustomControls_AjaxCalendar;
        DropDownList ddlWFP2ApprovedBy = fvAg_WFP2_Version.FindControl("ddlWFP2ApprovedBy") as DropDownList;
        TextBox tbGuideline = fvAg_WFP2_Version.FindControl("tbGuideline") as TextBox;
        TextBox tbNote = fvAg_WFP2_Version.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp2_versions.Where(w => w.pk_form_wfp2_version == Convert.ToInt32(fvAg_WFP2_Version.DataKey.Value)).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlInhouseRevision.SelectedValue)) a.inhouse_revision = ddlInhouseRevision.SelectedValue;
                else a.inhouse_revision = null;

                a.setup_date = tbCalSetupDate.CalDateNullable;

                a.approved_date = tbCalApprovedDate.CalDateNullable;

                if (!string.IsNullOrEmpty(ddlWFP2ApprovedBy.SelectedValue)) a.fk_WFP2ApprovedBy_code = ddlWFP2ApprovedBy.SelectedValue;
                else a.fk_WFP2ApprovedBy_code = null;

                if (!string.IsNullOrEmpty(tbGuideline.Text))
                {
                    try { a.guideline = Convert.ToDecimal(tbGuideline.Text); }
                    catch { sb.Append("Guideline was not updated. Must be a number (Decimal). "); }
                }
                else a.guideline = null;

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 1000);
                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvAg_WFP2_Version.ChangeMode(FormViewMode.ReadOnly);
                BindAg_WFP2_Version(Convert.ToInt32(fvAg_WFP2_Version.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_WFP2_Version_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlRevision = fvAg_WFP2_Version.FindControl("ddlRevision") as DropDownList;
        DropDownList ddlInhouseRevision = fvAg_WFP2_Version.FindControl("ddlInhouseRevision") as DropDownList;
        CustomControls_AjaxCalendar tbCalSetupDate = fvAg_WFP2_Version.FindControl("tbCalSetupDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalApprovedDate = fvAg_WFP2_Version.FindControl("tbCalApprovedDate") as CustomControls_AjaxCalendar;
        TextBox tbGuideline = fvAg_WFP2_Version.FindControl("tbGuideline") as TextBox;
        TextBox tbNote = fvAg_WFP2_Version.FindControl("tbNote") as TextBox;
        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                short? shRevision = Convert.ToInt16(ddlRevision.SelectedValue);

                DateTime? dtApprovedDate = tbCalApprovedDate.CalDateNotNullable;
                
                if (ddlRevision.SelectedValue != ddlRevision.Items[0].Value && dtApprovedDate == null) sb.Append("Approved Date is required. ");

                DateTime? dtSetupDate = tbCalSetupDate.CalDateNullable;

                decimal? dGuideline = null;
                if (!string.IsNullOrEmpty(tbGuideline.Text))
                {
                    try { dGuideline = Convert.ToDecimal(tbGuideline.Text); }
                    catch { sb.Append("Guideline must be a number (Decimal). "); }
                }

                string sInhouseRevision = ddlInhouseRevision.SelectedValue;

                string sNote = null;
                if (!string.IsNullOrEmpty(tbNote.Text)) sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 1000);

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    //iCode = wDataContext.form_wfp2_version_add_express(Convert.ToInt32(fvAg_WFP2.DataKey.Value), shRevision, dtApprovedDate, dtSetupDate, sInhouseRevision, Session["userName"].ToString(), ref i);
                    iCode = wDataContext.form_wfp2_version_add(Convert.ToInt32(fvAg_WFP2.DataKey.Value), shRevision, dtApprovedDate, dtSetupDate, dGuideline, 
                        sInhouseRevision, sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_WFP2_Version.ChangeMode(FormViewMode.ReadOnly);
                        BindAg_WFP2_Version(Convert.ToInt32(i));
                    }
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                else WACAlert.Show(WacRadWindowManager,sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
        }
    }

    protected void fvAg_WFP2_Version_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp2_version", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp2_version_delete(Convert.ToInt32(fvAg_WFP2_Version.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_WFP2_Version_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - WFP3

//    public void OpenWFP3FormView(object sender, FormViewEventArgs e)
//    {
//        ucWACAG_WFP3.OpenFormView(sender, e);
//        ucWACAG_WFP3.Visible = true;
//        mpeAg_WFP3.Show();
//        upAg_WFP3.Update();
//    }
//    public void CloseWFP3FormView(object sender, FormViewEventArgs e)
//    {
//       // OpenWFP3Grid(fvAg, new PrimaryKeyEventArgs(e.PrimaryKey));
//        mpeAg_WFP3.Hide();
//        SwitchTabContainerTab(_tabWFP3);
//        //upAg_WFP3.Update();
//        upAgs.Update();
////        upAgSearch.Update();
//    }
    
    #endregion
  
    #region Event Handling - Ag - Workload Project

    protected void lbAg_WLProject_Close_Click(object sender, EventArgs e)
    {
        fvAg_WLProject.ChangeMode(FormViewMode.ReadOnly);
        mpeAg_WLProject.Hide();
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            gvAg_WLProjects.DataSource = wac.farmBusinessWLProjects.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value) 
                && (w.ImplementationProject != null && w.ImplementationProject > 0 && w.ImplementationProject < 99))              
                .OrderBy(o => o.alias).Select(s => s);
            gvAg_WLProjects.DataBind();
        }

        SwitchTabContainerTab(_tabWLProject);
        upAgs.Update();
        upAgSearch.Update();
    }

    protected void lbAg_WLProject_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessWLProject", "msgInsert"))
        {
            fvAg_WLProject.ChangeMode(FormViewMode.Insert);
            BindAg_WLProject(-1);
            mpeAg_WLProject.Show();
            upAg_WLProject.Update();
        }
    }

    protected void lbAg_WLProject_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvAg_WLProject.ChangeMode(FormViewMode.ReadOnly);
        BindAg_WLProject(Convert.ToInt32(lb.CommandArgument));
        mpeAg_WLProject.Show();
        upAg_WLProject.Update();
    }

    protected void fvAg_WLProject_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "farmBusinessWLProject", "msgUpdate");
        if (bChangeMode)
        {
            fvAg_WLProject.ChangeMode(e.NewMode);
            BindAg_WLProject(Convert.ToInt32(fvAg_WLProject.DataKey.Value));
        }
    }

    protected void fvAg_WLProject_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        RadNumericTextBox project = (RadNumericTextBox)fvAg_WLProject.FindControl("ntbWorkloadGroup");
        RadNumericTextBox buildYear = (RadNumericTextBox)fvAg_WLProject.FindControl("ntbBuildYear");
        RadNumericTextBox designYear = (RadNumericTextBox)fvAg_WLProject.FindControl("ntbDesignYear");
        TextBox tbNote = fvAg_WLProject.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinessWLProjects.Where(w => w.pk_farmBusinessWLProject == Convert.ToInt32(fvAg_WLProject.DataKey.Value)).Select(s => s).Single();

                try
                {
                    a.ImplementationProject = Convert.ToByte(project.DbValue);
                }
                catch (OverflowException oe)
                {
                    a.ImplementationProject = 0;
                    throw new Exception("Project must be between 1 and 00", oe);
                }
                try 
	            {	        
		            a.design_year = Convert.ToInt16(designYear.DbValue);
	            }
	            catch (OverflowException oe)
	            {
		            a.design_year = 2016;
		            throw new Exception("Invalid design year!",oe);
	            }
                try 
	            {
                    a.build_year = Convert.ToInt16(buildYear.DbValue);
	            }
	            catch (OverflowException oe)
	            {
		            a.build_year = 2016;
		            throw new Exception("Invalid build year!",oe);
	            }
                if (a.design_year > a.build_year)
		            throw new Exception("Design year can not be after build year!");
	          
                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();
                try
                {
                    wDataContext.SubmitChanges();
                    fvAg_WLProject.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_WLProject(Convert.ToInt32(fvAg_WLProject.DataKey.Value));
                }
                catch (Exception ex)
                {
                    WACAlert.Show(WacRadWindowManager,ex.Message, 0);
                }
               
        }
            
    }

    protected void fvAg_WLProject_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        RadNumericTextBox project = (RadNumericTextBox)fvAg_WLProject.FindControl("ntbWorkloadGroup");
        RadNumericTextBox buildYear = (RadNumericTextBox)fvAg_WLProject.FindControl("ntbBuildYear");
        RadNumericTextBox designYear = (RadNumericTextBox)fvAg_WLProject.FindControl("ntbDesignYear");
        TextBox tbNote = fvAg_WLProject.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);
                short? projectNumber, designYearValue, buildYearValue;
                try
                {
                    projectNumber = Convert.ToByte(project.DbValue);
                }
                catch (OverflowException oe)
                {
                    throw new Exception("Project must be between 1 and 00", oe);
                }
                try
                {
                    designYearValue = Convert.ToInt16(designYear.DbValue);
                }
                catch (OverflowException oe)
                {
                    throw new Exception("Invalid design year!", oe);
                }
                try
                {
                    buildYearValue = Convert.ToInt16(buildYear.DbValue);
                }
                catch (OverflowException oe)
                {
                    throw new Exception("Invalid build year!", oe);
                }
                if (designYearValue > buildYearValue)
                    throw new Exception("Design year can not be after build year!");
            try
            {
                iCode = wDataContext.farmBusinessWLProject_add(Convert.ToInt32(fvAg.DataKey.Value), projectNumber,designYearValue,buildYearValue, sNote, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fvAg_WLProject.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_WLProject(Convert.ToInt32(i));
                }
                else
                    WACAlert.Show(WacRadWindowManager, "Database error", iCode);
            }  
            catch (Exception ex) 
            { 
                WACAlert.Show(WacRadWindowManager,ex.Message, 0); 
            }
        }
    }

    protected void fvAg_WLProject_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessWLProject", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessWLProject_delete(Convert.ToInt32(fvAg_WLProject.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_WLProject_Close_Click(null, null);
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Ag - Workload Project - BMP

    protected void ddlAg_WLProject_BMP_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue))
        {
            if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "farmBusinessWLProjectBMP", "msgInsert"))
            {
                int? i = null;
                int iCode = 0;

                int? iPK_BMP = Convert.ToInt32(ddl.SelectedValue);

                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.farmBusinessWLProjectBMP_add(Convert.ToInt32(fvAg_WLProject.DataKey.Value), iPK_BMP, Session["userName"].ToString(), ref i);
                        if (iCode == 0) BindAg_WLProject(Convert.ToInt32(fvAg_WLProject.DataKey.Value));
                        else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
                }
            }
        }
    }

    protected void lbAg_WLProject_BMP_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "farmBusinessWLProjectBMP", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.farmBusinessWLProjectBMP_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0) BindAg_WLProject(Convert.ToInt32(fvAg_WLProject.DataKey.Value));
                    else WACAlert.Show(WacRadWindowManager,"Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(WacRadWindowManager,ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Data Binding - Ag

    private void BindAgs()
    {
        try
        {
            IEnumerable x = Session["resultsAgs"] as IEnumerable;
            gvAg.DataKeyNames = new string[] { "pk_farmBusiness" };
            gvAg.DataSource = x;
            gvAg.DataBind();
        }
        catch { }
        if (PK_FarmBusiness != -1)
        {
            string sSelectedValue = PK_FarmBusiness.ToString();
            foreach (GridViewRow gvr in gvAg.Rows)
            {
                string sKeyValue = gvAg.DataKeys[gvr.RowIndex].Value.ToString();
                if (sKeyValue == sSelectedValue)
                   gvAg.SelectedIndex = gvr.RowIndex;

                else 
                    gvAg.SelectedIndex = -1;
            }
        }
        lblCount.Text = "Records: " + Session["countAgs"];

        if (gvAg.Rows.Count == 1)
        {
            gvAg.SelectedIndex = 0;
            gvAg.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
            fvAg.ChangeMode(FormViewMode.ReadOnly);
            
            if (gvAg.SelectedIndex != -1)
                PK_FarmBusiness = Convert.ToInt32(gvAg.SelectedDataKey.Value);
            BindAg(PK_FarmBusiness);
            upAgs.Update();
        }
       
    }

    private void BindAgOnDemand(object sender)
    {
        AjaxControlToolkit.TabContainer tcAg = (AjaxControlToolkit.TabContainer)sender;
      
        List<WACParameter> parms = new List<WACParameter>();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == PK_FarmBusiness) select b;
            switch (tcAg.ActiveTab.ID)
            {
                case "tbAg_FarmOverview":
                    
                    break;
                case "tbAg_FarmLand":
                    lvAg_FarmLandTracts.DataSource = a.Single().farmLand.farmLandTracts.OrderBy(o => o.tract);
                    lvAg_FarmLandTracts.DataBind();
                    break;
                case "tpAg_FarmOperation":
                    gvAg_FarmTypes.DataSource = a.Single().farmBusinessTypes.OrderBy(o => o.list_farmType.farmType);
                    gvAg_FarmTypes.DataBind();

                    var aSA = wDataContext.supplementalAgreements.Where(w => w.supplementalAgreementTaxParcels.First(f => f.bmp_ags.Any(a2 => a2.fk_farmBusiness == PK_FarmBusiness)).bmp_ags.Any(a3 => a3.fk_farmBusiness == PK_FarmBusiness)).Select(s => s);
                    lvAg_SAs.DataSource = aSA;
                    lvAg_SAs.DataBind();

                    gvAg_FarmBusinessContacts.DataSource = a.Single().farmBusinessContacts.OrderBy(o => o.participant.fullname_LF_dnd);
                    gvAg_FarmBusinessContacts.DataBind();

                    Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessMail, a.Single().farmBusinessMails.OrderBy(o => o.participant.fullname_LF_dnd), false, true, false);
                    Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOperator, a.Single().farmBusinessOperators.OrderBy(o => o.participant.fullname_LF_dnd), false, true, false);
                    Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOwner, a.Single().farmBusinessOwners.OrderBy(o => o.participant.fullname_LF_dnd), false, false, false);
                    Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessPlanner, a.Single().farmBusinessPlanners.OrderBy(o => o.list_designerEngineer.designerEngineer), false, true, false);

                    gvAg_TaxParcels.DataSource = a.Single().farmBusinessTaxParcels.OrderBy(o => o.taxParcel.list_swi.county).ThenBy(o => o.taxParcel.list_swi.jurisdiction).ThenBy(o => o.taxParcel.taxParcelID);
                    gvAg_TaxParcels.DataBind();
                    Control tabPanel = (AjaxControlToolkit.TabPanel)gvAg_TaxParcels.NamingContainer;

                    var wfp = wDataContext.WholeFarmPlans().Where(w => w.pk_farmBusiness == PK_FarmBusiness).Select(s => s.County);
                    DropDownList ddlCounty = (DropDownList)tabPanel.FindControl("ddlCounty");
                    DropDownList ddlJurisdiction = (DropDownList)tabPanel.FindControl("ddlJurisdiction");
                    if (wfp.Any())
                    {
                        WACGlobal_Methods.LoadCountyDDL(ddlCounty, wfp.First());
                        WACGlobal_Methods.LoadJurisdictionDDL(ddlJurisdiction, wfp.First());
                    }
                    else
                        WACGlobal_Methods.LoadCountyDDL(ddlCounty, null);
                    //WACGlobal_Methods.PopulateControl_Custom_TaxParcels_County_DDL(UC_ControlGroup_TaxParcel1.FindControl("ddlCounty") as DropDownList, null, true);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_FarmType_DDL(ddlAg_Type_Add, a.Single().fk_farmSize_code, null);

                    break;
                case "tpAg_OandM":
                    break;
                case "tpAg_FarmStatus":
                    gvAg_FarmStatus.DataSource = a.Single().farmBusinessStatus.OrderByDescending(o => o.date);
                    gvAg_FarmStatus.DataBind();

                    pnlAg_FAD.Visible = true;
                    gvAg_FarmBusinessFAD.DataSource = a.Single().farmBusinessFADs.OrderBy(o => o.list_FAD.setting);
                    gvAg_FarmBusinessFAD.DataBind();
                    break;
                case "tpAg_Animals":
                    lvAg_Animals.DataSource = a.Single().farmBusinessAnimals.OrderByDescending(o => o.ASR_yr).ThenBy(o => o.list_animal.animal);
                    lvAg_Animals.DataBind();
                    break;
                case "tpAg_ASR":
                    gvAg_ASRs.DataSource = a.Single().asrAgs.OrderByDescending(o => o.year);
                    gvAg_ASRs.DataBind();
                    break;
                case "tpAg_BMP":
                    wDataContext.AgBmpClearFailedClone(PK_FarmBusiness); // clear up any cloned BMPs that were left incomplete
                    LimitBMPList = false;
                    gvAg_BMPs.DataSource = WACGlobal_Methods.DataBaseFunction_Agriculture_BMP_Grid_All(PK_FarmBusiness, new int?[] { 0 });
                    gvAg_BMPs.DataBind();
                    CloneBMP = false;
                    break;

                case "tpAg_Cropware":
                    try
                    {
                        gvAg_Cropware.DataSource = a.Single().cropwares.OrderByDescending(o => o.plan_year).ThenBy(o => o.tractField).Select(s => s);
                        gvAg_Cropware.DataBind();
                    }
                    catch { }

                    WACGlobal_Methods.PopulateControl_Custom_Agriculture_Cropware_FilterByYear(ddlAg_Cropware_Year_Filter, PK_FarmBusiness);

                    break;
                case "tpAg_LandBaseInfo":
                    if (fvAg_LandBaseInfo.CurrentMode == FormViewMode.ReadOnly)
                    {
                        fvAg_LandBaseInfo.DataKeyNames = new string[] { "pk_farmBusinessLandBaseInfo" };
                        fvAg_LandBaseInfo.DataSource = a.Single().farmBusinessLandBaseInfos;
                        fvAg_LandBaseInfo.DataBind();
                    }
                    break;
                case "tpAg_Note":
                    gvAg_Notes.DataSource = a.Single().farmBusinessNotes.OrderByDescending(o => o.created);
                    gvAg_Notes.DataBind();
                    break;
                case "tpAg_NMP":
                    if (fvAg_NMP.CurrentMode == FormViewMode.ReadOnly)
                    {
                        fvAg_NMP.DataKeyNames = new string[] { "pk_nmp" };
                        fvAg_NMP.DataSource = a.Single().farmBusinessNMPs;
                        fvAg_NMP.DataBind();
                        BindAgNMP_AnimalUnits_ToLabel(PK_FarmBusiness);
                        Image imgAdvisory_NMP_phosphorousSaturation_eoh = fvAg_NMP.FindControl("imgAdvisory_NMP_phosphorousSaturation_eoh") as Image;
                        if (imgAdvisory_NMP_phosphorousSaturation_eoh != null) 
                            imgAdvisory_NMP_phosphorousSaturation_eoh.ToolTip = WACGlobal_Methods.Specialtext_Global_Advisory(7);
                        AjaxControlToolkit.TabPanel tp = (AjaxControlToolkit.TabPanel)tcAg.FindControl(tcAg.ActiveTab.ID);
                        Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)tp.FindControl("WACUT_AttachedDocumentViewerNMP");
                        parms.Clear();
                        parms.Add(new WACParameter("pk_farmBusiness", a.Single().pk_farmBusiness, WACParameter.ParameterType.MasterKey));
                        //    parms.Add(new WACParameter("pk_nmp", a.Single().fk_n, WACParameter.ParameterType.PrimaryKey));
                        docView.InitControl(parms);
                    }
                    Literal nmcpHistory = fvAg_NMP.FindControl("litAg_NutrientManagementCreditHistory") as Literal;
                    nmcpHistory.Text = CreateAgNMCPHistoryTable(PK_FarmBusiness, DateTime.Now.Year.ToString(), wDataContext);
                    break;

                case "tpAg_Tier1":
                    fvAg_Tier1.DataKeyNames = new string[] { "pk_farmBusinessTier1" };
                    fvAg_Tier1.DataSource = a.Single().farmBusinessTier1s;
                    fvAg_Tier1.DataBind();
                    if (a.Single().farmBusinessTier1s.Count() == 1)
                    {
                        CreateAg_Tier1_Grid(PK_FarmBusiness);
                        GridView gvAg_Tier1_Animals = fvAg_Tier1.FindControl("gvAg_Tier1_Animals") as GridView;
                        gvAg_Tier1_Animals.DataSource = wDataContext.farmBusinessTier1Animals.Where(w => w.fk_farmBusinessTier1 == Convert.ToInt32(fvAg_Tier1.DataKey.Value)).OrderBy(o => o.list_animal.animal).Select(s => s);
                        gvAg_Tier1_Animals.DataBind();
                    }
                    break;
                case "tpAg_WFP2":
                    if (fvAg_WFP2.CurrentMode == FormViewMode.ReadOnly)
                    {
                        if (a.Single().form_wfp2s.Count() > 1)
                        {
                            WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP2_And_SAs(ddlAg_WFP2_Type, a.Single().form_wfp2s, false);
                            pnlAg_WFP2_MultipleWFP2s.Visible = true;
                        }
                        fvAg_WFP2.DataKeyNames = new string[] { "pk_form_wfp2" };
                        if (pnlAg_WFP2_MultipleWFP2s.Visible == true)
                            fvAg_WFP2.DataSource = a.Single().form_wfp2s.Where(w => w.pk_form_wfp2 == Convert.ToInt32(ddlAg_WFP2_Type.SelectedValue));
                        else
                            fvAg_WFP2.DataSource = a.Single().form_wfp2s;
                        fvAg_WFP2.DataBind();
                        AjaxControlToolkit.TabPanel tp = (AjaxControlToolkit.TabPanel)tcAg.FindControl(tcAg.ActiveTab.ID);
                        Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)tp.FindControl("WACUT_AttachedDocumentViewerWFP2");                   
                        parms.Add(new WACParameter("pk_farmBusiness", a.First().pk_farmBusiness, WACParameter.ParameterType.MasterKey));
                        var mainWFP2 = a.Single().form_wfp2s.Select(s => s);
                        if (mainWFP2.Any())
                        {
                            var pk_wfp2Main = mainWFP2.Where(w => w.fk_supplementalAgreement == null).Select(s => s.pk_form_wfp2);
                            parms.Add(new WACParameter("pk_form_wfp2", pk_wfp2Main.First(), WACParameter.ParameterType.PrimaryKey));
                        }
                        docView.InitControl(parms);
                        //UC_DocumentArchive_A_WFP2.SetupViewer();

                        // WFP2 Versions
                        GridView gvAg_WFP2_Revisions = fvAg_WFP2.FindControl("gvAg_WFP2_Revisions") as GridView;
                        gvAg_WFP2_Revisions.DataSource = wDataContext.form_wfp2_versions.Where(w => w.fk_form_wfp2 == Convert.ToInt32(fvAg_WFP2.DataKey.Value)).Select(s => s);
                        gvAg_WFP2_Revisions.DataBind();
                    }
                    break;
                case "tpAg_WFP3":
                    //gvAg_WFP3.DataSource = wDataContext.form_wfp3_grid_PK(PK_FarmBusiness).OrderBy(o => o.packageName);
                    //gvAg_WFP3.DataBind();
                    break;
                case "tpAg_WLProjects":
                    gvAg_WLProjects.DataSource = a.Single().farmBusinessWLProjects.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)
                     && (w.ImplementationProject != null && w.ImplementationProject > 0 && w.ImplementationProject < 99))
                .OrderBy(o => o.alias).Select(s => s);
                    gvAg_WLProjects.DataBind();
                    break;
            }
            parms = null;
        }

    }

    private void BindAg(int i)
    {
        PK_FarmBusiness = i;
        foundFarmGridPlaceholder.Visible = false;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == i) select b;
            fvAg.DataKeyNames = new string[] { "pk_farmBusiness" };
            fvAg.DataSource = a;
            fvAg.DataBind();
                                  

            if (fvAg.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                CreateAgOverviewGrid(i, a.Single().farmBusinessPlanners);
                Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOperator, a.Single().farmBusinessOperators.OrderBy(o => o.participant.fullname_LF_dnd), false, true, false);
                Rebind_Data_Controls(Enum_Ag_RebindableSections.farmBusinessOwner, a.Single().farmBusinessOwners.OrderBy(o => o.participant.fullname_LF_dnd), false, false, false);
              
                // this needs to be replaced when page is converted            
                AjaxControlToolkit.TabContainer tc = (AjaxControlToolkit.TabContainer)fvAg.FindControl("tcAg");
                AjaxControlToolkit.TabPanel tp = (AjaxControlToolkit.TabPanel)tc.FindControl("tbAg_FarmOverview");
                Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)tp.FindControl("WACUT_AttachedDocumentViewer");
                List<WACParameter> parms = new List<WACParameter>();
                parms.Add(new WACParameter("pk_farmBusiness", i, WACParameter.ParameterType.SelectedKey));
                parms.Add(new WACParameter("pk_farmBusiness", i, WACParameter.ParameterType.PrimaryKey));
                docView.InitControl(parms);    
            }
           
            if (fvAg.CurrentMode == FormViewMode.Edit)
            {
                if (string.IsNullOrEmpty(a.Single().farmID))
                {
                    LinkButton lbAg_SetFarmID = fvAg.FindControl("lbAg_SetFarmID") as LinkButton;
                    lbAg_SetFarmID.Visible = true;
                }
                HiddenField hfProperty = (HiddenField)fvAg.FindControl("hfPropertyPK");
                if (hfProperty != null)
                    hfProperty.Value = a.Single().farmLand.fk_property.ToString();
                WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(fvAg, "ddlRegionWAC", a.Single().fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ProgramWAC_DDL(fvAg, "ddlProgramWAC", a.Single().fk_programWAC_code);
                WACGlobal_Methods.PopulateControl_Generic_TextBoxCalendar(fvAg, "tbCalWFP0SignedDate", a.Single().wfp0_signed, null);
                WACGlobal_Methods.PopulateControl_Generic_TextBoxCalendar(fvAg, "UC_TextBoxCalendar_WFP1Signed", a.Single().wfp1_signed_date, null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FarmSize_DDL(fvAg, "ddlFarmSize", a.Single().fk_farmSize_code);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg, "ddlForestry", a.Single().forestry);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg, "ddlFarmToMarket", a.Single().farmToMarket);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg, "ddlSoldFarm", a.Single().sold_farm);
                WACGlobal_Methods.PopulateControl_DatabaseLists_GroupPI_DDL(fvAg, "ddlGroupPI", a.Single().fk_groupPI_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_EnvironmentalImpact_DDL(fvAg, "ddlEnvironmentalImpact", a.Single().fk_environmentalImpact_code);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg, "ddlIAPriorToImplementation", a.Single().IA_prior_to_implementation);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg, "ddlPriorImplementationCommenced", a.Single().prior_implementation_commenced);            
                WACGlobal_Methods.PopulateControl_Generic_TextBoxCalendar(fvAg, "UC_TextBoxCalendar_Ag_ISCDate", a.Single().implementation_substantially_complete_date, null);
                WACGlobal_Methods.PopulateControl_Generic_TextBoxCalendar(fvAg, "UC_TextBoxCalendar_Ag_IFCDate", a.Single().implementation_fully_complete_date, null);
                //WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvAg.FindControl("UC_Property_EditInsert1") as UserControl, a.Single().farmLand.property);
                DropDownList ddlZipCode = (DropDownList)fvAg.FindControl("ddlZipCode");
                WACGlobal_Methods.ZipCodesFromExistingAddresses(ddlZipCode);
                Label lblCurrentAddress = (Label)fvAg.FindControl("lblCurrentAddress");
                lblCurrentAddress.Text = WACGlobal_Methods.SpecialText_Global_Address(a.Single().farmLand.property.addressFull,a.Single().farmLand.property.csz_ro);;
              
            }
        }
    }

    private void ClearAgs()
    {
        lblCount.Text = "";
        gvAg.DataSource = null;
        gvAg.DataBind();
    }

    private void ClearAg()
    {
        fvAg.ChangeMode(FormViewMode.ReadOnly);
        fvAg.DataSource = "";
        fvAg.DataBind();

    }

    private void CreateAgOverviewGrid(int iPK_FarmBusiness, object oFarmBusinessPlanners)
    {
        StringBuilder sb = new StringBuilder();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var q = wDataContext.farmBusiness_get_overviewReadOnly(iPK_FarmBusiness);
                if (q.Count() == 1)
                {
                    var x = q.Single();

                    decimal? dBMPArtExpirationNullable = x.BMP_amt_expiration;
                    decimal dBMPArtExpiration = 0;
                    if (dBMPArtExpirationNullable != null) dBMPArtExpiration = Convert.ToDecimal(dBMPArtExpirationNullable);
                    int? iBMPCntExpiration = x.BMP_cnt_expiration;
                    if (iBMPCntExpiration == null) iBMPCntExpiration = 0;

                    sb.Append("<center><div class='NestedDivLevel00'><div class='fsM B U'>Quick View</div><table cellpadding='5'>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>Status:</td><td class='taL'>" + x.status + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>Prior Owner:</td><td class='taL'>" + x.priorOwner + "</td>"); 
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>WFP1 Expiration:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Date(x.wfp1_expiration) + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    // 21-Sep-2012 JWJ changed "# of BMPs" to "# of Notes" and bmp_cnt to note_cnt per BA940
                    sb.Append("<td class='B taR'># of Notes:</td><td class='taL'>" + x.note_cnt + "</td>"); 
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>WFP2 Approved (Original):</td><td class='taL'>" + WACGlobal_Methods.Format_Color_Agriculture_WFP0_NotApproved(WACGlobal_Methods.Format_Global_Date(x.wfp2_v0_approved)) + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>5 Year BMP Funding Expiration:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Currency(Math.Abs(dBMPArtExpiration)) + " (" + iBMPCntExpiration + ")</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>WFP2 Approved WAP:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Currency(x.wfp2_amt_approved_wap) + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>Active Supplemental Agreements:</td><td class='taL'>" + x.SA_cnt + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>WFP2 Approved CREP:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Currency(x.wfp2_amt_approved_crep) + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>ASR:</td><td class='taL'>" + x.asr + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>WFP2 Approved Other:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Currency(x.wfp2_amt_approved_other) + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>Animal Units:</td><td class='taL'>" + x.animal_units + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>WFP2 Approved Total:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Currency(x.wfp2_amt_approved_total) + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>County & Town</td><td class='taL'>" + x.countyTown + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>Current Revision:</td><td class='taL'>" + x.versionCurrent + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>Active CREP Contract:</td><td class='taL'>" + x.CREP + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>Current Revision Approved Date:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Date(x.versionCurrentApproved) + "</td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>Planner(s):</td><td class='taL'>");
                    if (oFarmBusinessPlanners != null)
                    {
                        System.Data.Linq.EntitySet<farmBusinessPlanner> farmBusinessPlanners = (System.Data.Linq.EntitySet<farmBusinessPlanner>)oFarmBusinessPlanners;
                        foreach (farmBusinessPlanner fbp in WACGlobal_Methods.Order_Agriculture_FarmBusinessPlanner_designerEngineer(farmBusinessPlanners))
                        {
                            sb.Append(WACGlobal_Methods.SpecialText_FarmBusinessPlanner_DesignerEngineer_NameAgencyMaster(fbp));
                        }
                    }
                    else
                    {
                        var y = wDataContext.farmBusinessPlanners.Where(w => w.fk_farmBusiness == iPK_FarmBusiness).OrderBy(o => o.list_designerEngineer.designerEngineer).Select(s => s);
                        foreach (var z in y) { sb.Append(WACGlobal_Methods.SpecialText_FarmBusinessPlanner_DesignerEngineer_NameAgencyMaster(z)); }
                    }
                    sb.Append("</td></tr>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B taR'>Easement:</td><td class='taL'><span style=\"color:red;font-weight:bold\">");
                    sb.Append(x.easement);
                    sb.Append("</span></td>");
                    sb.Append("<td width='20px'>&nbsp;</td>");
                    sb.Append("<td class='B taR'>Easement Steward:</td><td class='taL'>");
                    sb.Append(x.easementSteward);
                    sb.Append("</td></tr></table></div></center>");

                    string sAnimalUnits = "0.00";
                    if (x.animal_units > 0) sAnimalUnits = x.animal_units.ToString();
                    lblAg_Animals_AnimalUnits.Text = sAnimalUnits;
                }
                else sb.Append("<div class='B'>Could not create overview list: Could not find farm in Overview Query.</div>");
                litAg_Overview.Text = sb.ToString();
            }
            catch (Exception ex) { litAg_Overview.Text = "<div class='B' style='color:red;'>Could not create overview list: " + ex.Message + "</div>"; }
        }
    }

    #endregion

    #region Data Binding - Ag - Animal

    private void BindAg_Animal(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinessAnimals.Where(w => w.pk_farmBusinessAnimal == i) select b;
            fvAg_Animal.DataKeyNames = new string[] { "pk_farmBusinessAnimal" };
            fvAg_Animal.DataSource = a;
            fvAg_Animal.DataBind();

            litAg_Animal_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_Animal.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Animal_DDL(fvAg_Animal, "ddlAnimal", null, "Y", "");
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_Animal, "ddlASRYear", null);
            }

            if (fvAg_Animal.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Animal_DDL(fvAg_Animal, "ddlAnimal", a.Single().fk_list_animal, "Y", "");
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_Animal, "ddlASRYear", a.Single().ASR_yr);
            }
        }
    }

    private void BindAg_Animal_Age(FormView fv, int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinessAnimalAges.Where(w => w.pk_farmBusinessAnimalAge == i) select b;
            fv.DataKeyNames = new string[] { "pk_farmBusinessAnimalAge" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_AnimalAge_DDL(fv, "ddlAge", null);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_AnimalAge_DDL(fv, "ddlAge", a.Single().fk_list_animalAge);
            }
        }
    }

    #endregion

    #region Data Binding - Ag - ASR

    private void BindAg_ASR(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.asrAgs.Where(w => w.pk_asrAg == i).Select(s => s);
            fvAg_ASR.DataKeyNames = new string[] { "pk_asrAg" };
            fvAg_ASR.DataSource = a;
            fvAg_ASR.DataBind();
            StringBuilder sb = new StringBuilder();
            if (a.Any())
            {
                var q = wDataContext.asrAg_QMAs.Where(w => w.fk_asrAg == a.Single().pk_asrAg).Select(s => s.fk_qmaType_code);
                if (q.Any())
                {
                    foreach (string qmaCode in q)
                    {
                        sb.Append(qmaCode);
                        sb.Append(",");
                    }
                }
            }
            WACAG_QMACheckbox cbAsrQma = (WACAG_QMACheckbox)fvAg_ASR.FindControl("cbAsrQma");
            if (fvAg_ASR.CurrentMode == FormViewMode.ReadOnly && i > 0)
            {
                UpdatePanel up = (UpdatePanel)pnlAg_ASR.FindControl("upAg_ASR"); 
                Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)up.FindControl("WACUT_AttachedDocumentViewerASR");
                List<WACParameter> parms = new List<WACParameter>();
                parms.Add(new WACParameter("pk_farmBusiness", a.First().fk_farmBusiness, WACParameter.ParameterType.MasterKey));
                parms.Add(new WACParameter("pk_asrAg", i, WACParameter.ParameterType.PrimaryKey));
                cbAsrQma.BindCheckList(sb.ToString());
                cbAsrQma.Enabled = false;
                docView.InitControl(parms);
            }
            
            litAg_ASR_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);
            string sWACRegion = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(fvAg.DataKey.Value);

            if (fvAg_ASR.CurrentMode == FormViewMode.Insert)
            {
                var planner = wDataContext.farmBusinessPlanners.Where(w => w.fk_farmBusiness == PK_FarmBusiness && w.master == "Y");
                if (planner.Any())
                    WACGlobal_Methods.PopulateControl_DatabaseLists_ASRPlanner_DDL(fvAg_ASR, "ddlPlanner", planner.First().fk_list_designerEngineer);
                else
                    WACGlobal_Methods.PopulateControl_DatabaseLists_ASRPlanner_DDL(fvAg_ASR, "ddlPlanner", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_ASR, "ddlYear", DateTime.Now.Year);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ASRAssignedTo_DDL(fvAg_ASR, "ddlAssignTo", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ASRType_DDL(fvAg_ASR, "ddlType", null);
            }

            if (fvAg_ASR.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_ASR, "ddlYear", a.Single().year);

                WACGlobal_Methods.PopulateControl_DatabaseLists_ASRPlanner_DDL(fvAg_ASR, "ddlPlanner", a.Single().fk_list_designerEngineer_planner);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ASRAssignedTo_DDL(fvAg_ASR, "ddlAssignTo", a.Single().fk_list_designerEngineer_assignTo);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ASRType_DDL(fvAg_ASR, "ddlType", a.Single().fk_asrType_code);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlCREPInfoRequest", a.Single().CREPInfoReq);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlEasementInfoRequest", a.Single().easementInfoReq);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlForestryInfoRequest", a.Single().forestryInfoReq);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlF2MInfoRequest", a.Single().F2MInfoReq);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlHasSign", a.Single().hasSign);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlRevisionRequired", a.Single().revisionReqd);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlLandChange", a.Single().landChange);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlGoalsChange", a.Single().goalsChange);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlWFPEffective", a.Single().WFPEffective);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlBMPsEffective", a.Single().BMPsEffective);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlBMPsOMA", a.Single().BMPsOMA);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlRevision", a.Single().revision);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlIssues", a.Single().issues);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_ASR, "ddlActive", a.Single().active);
                cbAsrQma.BindCheckList(sb.ToString());
                cbAsrQma.Enabled = true;
            }
        }
    }

    #endregion

    #region Data Binding - Ag - BMP

    private void BindAg_BMP(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            DataLoadOptions dlOpt = new DataLoadOptions();
            int pkFarmBusiness = Convert.ToInt32(fvAg.DataKey.Value);

            var a = wDataContext.bmp_ags.Where(w => w.pk_bmp_ag == i).Select(s => s);
            fvAg_BMP.DataKeyNames = new string[] { "pk_bmp_ag" };
            fvAg_BMP.DataSource = a;
            fvAg_BMP.DataBind();

            if (i > -1 && !a.Any())
                throw new Exception("BindAg_BMP returned empty dataset on pk: " + i);

            bmp_ag bindingBmp = a.Any() ? a.First() : null;
            litAg_BMP_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);
            string sWACRegion = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(fvAg.DataKey.Value);
            CreateAgBMPFundingOverviewGrid(i);
            var b = wDataContext.BmpBacklogs.Where(w => w.fk_bmp_ag == bindingBmp.pk_bmp_ag);


            if (fvAg_BMP.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                
                UpdatePanel up = (UpdatePanel)pnlAg_BMP.FindControl("upAg_BMP");
                Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)up.FindControl("WACUT_AttachedDocumentViewerBMP");
                List<WACParameter> parms = new List<WACParameter>();
                parms.Add(new WACParameter("pk_farmBusiness", bindingBmp.fk_farmBusiness, WACParameter.ParameterType.MasterKey));
                parms.Add(new WACParameter("pk_bmp_ag", i, WACParameter.ParameterType.PrimaryKey));                
                docView.InitControl(parms);
                Label wlGrouping = (Label)fvAg_BMP.FindControl("workloadGroupingLabel");
                if (a.Single().farmBusinessWLProject != null)
                    wlGrouping.Text = bindingBmp.farmBusinessWLProject.ImplementationProject.ToString();
                wlGrouping.Visible = !IsNonGroupingBmp(bindingBmp.Bmp,bindingBmp.AgBmpDescriptorCode.DescriptorCode,bindingBmp.description,bindingBmp.fk_bmpPractice_code);

                Label ancestor = (Label)fvAg_BMP.FindControl("parent");
                ancestor.Text = WACGlobal_Methods.GetBmpAncestorBmpNumber(bindingBmp.pk_bmp_ag);
            }
            if (fvAg_BMP.CurrentMode != FormViewMode.ReadOnly)
            {
                var wfp = wDataContext.WholeFarmPlans().Where(w => w.pk_farmBusiness == pkFarmBusiness).Select(s => s.County);
                DropDownList ddlCounty = (DropDownList)fvAg_BMP.FindControl("ddlCounty");
                DropDownList ddlJurisdiction = (DropDownList)fvAg_BMP.FindControl("ddlJurisdiction");
                
                if (wfp.Any())
                {
                    WACGlobal_Methods.LoadCountyDDL(ddlCounty, wfp.First());
                    WACGlobal_Methods.LoadJurisdictionDDL(ddlJurisdiction, wfp.First());
                }
                else
                    WACGlobal_Methods.LoadCountyDDL(ddlCounty, null);
               
               
            }
            if (fvAg_BMP.CurrentMode != FormViewMode.Insert)
            {
                Label backlogLabel = (Label)fvAg_BMP.FindControl("IsBacklogEntity");
                if (backlogLabel != null)
                    backlogLabel.Text = b.Any() ? "Backlog" : string.Empty;
                Label lbl3 = fvAg_BMP.FindControl("lblUnits3") as Label;
                Label lbl2 = fvAg_BMP.FindControl("lblUnits2") as Label;
                Label lbl1 = fvAg_BMP.FindControl("lblUnits1") as Label;
                if (a.Any())
                {
                    if (bindingBmp.units_planned != null && bindingBmp.list_bmpPractice.list_unit != null)
                    {
                        lbl1.Text = bindingBmp.list_bmpPractice.list_unit.unit;
                        lbl2.Text = bindingBmp.list_bmpPractice.list_unit.unit;
                        lbl3.Text = bindingBmp.list_bmpPractice.list_unit.unit;
                    }
                }               
            }
            
            if (fvAg_BMP.CurrentMode == FormViewMode.Insert)
            {
                DropDownList bmpTypeDDL = (DropDownList)fvAg_BMP.FindControl("ddlBmpType");
                DropDownList ddlAncestor = (DropDownList)fvAg_BMP.FindControl("ddlAncestorBmp");
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPCode_DDL(fvAg_BMP, "ddlBMPCode", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPDescriptor_DDL(fvAg_BMP, "ddlBmpDescriptor", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PollutantCategory_DDL(fvAg_BMP, "ddlPollutant", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMP_DDL(fvAg_BMP, "ddlBMPStatus", "DR", false, true, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL(fvAg_BMP, "ddlPractice", null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_SupplementalAgreementAssignment_DDL(fvAg_BMP.FindControl("ddlSAA") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMP_DDL(fvAg_BMP, "ddlBMPStatus", null, false, false, true);
                RadNumericTextBox ntbQualifierVersion = (RadNumericTextBox)fvAg_BMP.FindControl("ntbQualifierVersion");
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPTypeCode_DDL(fvAg_BMP.CurrentMode, bmpTypeDDL, null);
                WACGlobal_Methods.LoadAncestorBmpDDL(ddlAncestor, pkFarmBusiness, null);

                ntbQualifierVersion.DbValue = 0;
                HandleSupplementalAgreementsBasedOnFarm(null,null);
                dlOpt.LoadWith<list_bmpPractice>(c => c.fk_unit_code);
                wDataContext.LoadOptions = dlOpt;
            }

            if (fvAg_BMP.CurrentMode == FormViewMode.Edit)
            {

                DropDownList ddlAncestor = (DropDownList)fvAg_BMP.FindControl("ddlAncestorBmp");
                WACGlobal_Methods.LoadAncestorBmpDDL(ddlAncestor, bindingBmp.fk_farmBusiness, bindingBmp.pk_bmp_ag);

                RadNumericTextBox ntbWorkloadGroup = (RadNumericTextBox)fvAg_BMP.FindControl("ntbWorkloadGroup");
                ntbWorkloadGroup.Visible = !IsNonGroupingBmp(bindingBmp.Bmp, bindingBmp.AgBmpDescriptorCode.DescriptorCode, bindingBmp.description, bindingBmp.fk_bmpPractice_code);
                PlaceHolder clone = (PlaceHolder)WACGlobal_Methods.FindControlRecursive(fvAg_BMP, "bmpEditClone");
                PlaceHolder original = (PlaceHolder)WACGlobal_Methods.FindControlRecursive(fvAg_BMP, "bmpEditOriginal");
                //RadCheckBox bmpIsIrc;
                DropDownList bmpTypeDDL;
                if (CloneBMP)
                {
                    //bmpIsIrc = (RadCheckBox)fvAg_BMP.FindControl("BmpIsIrcClone");
                    //bmpIsIrc.Checked = bindingBmp.fk_BMPTypeCode == "IRC";
                    bmpTypeDDL = (DropDownList)fvAg_BMP.FindControl("ddlCloneBmpType");
                    WACGlobal_Methods.PopulateControl_DatabaseLists_BMPCode_DDL(fvAg_BMP, "ddlBMPCodeClone", bindingBmp.fk_BMPCode_code);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_BMPDescriptor_DDL(fvAg_BMP, "ddlBmpDescriptorClone", 
                        bindingBmp.AgBmpDescriptorCode.DescriptorCode);
                    clone.Visible = true;
                    original.Visible = false;
                }
                else
                {
                //    bmpIsIrc = (RadCheckBox)fvAg_BMP.FindControl("BmpIsIrc");
                //    bmpIsIrc.Checked = bindingBmp.fk_BMPTypeCode == "IRC";
                    bmpTypeDDL = (DropDownList)fvAg_BMP.FindControl("ddlBmpType");
                    
                    WACGlobal_Methods.PopulateControl_DatabaseLists_BMPCode_DDL(fvAg_BMP, "ddlBMPCode", bindingBmp.fk_BMPCode_code);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_BMPDescriptor_DDL(fvAg_BMP, "ddlBmpDescriptor",
                        bindingBmp.AgBmpDescriptorCode.DescriptorCode);
                    clone.Visible = false;
                    original.Visible = true;
                }
                //if (bmpIsIrc != null)
                //ntbWorkloadGroup.Visible = !IsNonGroupingBmp(bindingBmp.Bmp,bindingBmp.AgBmpDescriptorCode.DescriptorCode,bindingBmp.description, bindingBmp.fk_bmpPractice_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPTypeCode_DDL(fvAg_BMP.CurrentMode, bmpTypeDDL, bindingBmp.fk_BMPTypeCode);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PollutantCategory_DDL(fvAg_BMP, "ddlPollutant", bindingBmp.fk_pollutant_category_code);
                DropDownList ddl = (DropDownList)fvAg_BMP.FindControl("ddlPractice");
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL(ddl, bindingBmp.fk_bmpPractice_code, true,true,false);
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_Units_By_BMPPractice(bindingBmp.fk_bmpPractice_code, fvAg_BMP.FindControl("lblUnits") as Label);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ProcurementType_DDL(fvAg_BMP, "ddlProcurementType", bindingBmp.fk_procurementType_code);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMP_DDL(fvAg_BMP, "ddlBMPStatus", bindingBmp.fk_statusBMP_code, false,false, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_SupplementalAgreementAssignment_DDL(fvAg_BMP.FindControl("ddlSAA") as DropDownList, bindingBmp.fk_SAAssignType_code, true);
                HandleSupplementalAgreementsBasedOnFarm(bindingBmp.pk_bmp_ag, bindingBmp.fk_supplementalAgreementTaxParcel);             


            }
        }
    }
   
   
    private void BindAg_BMP_Funding(FormView fv, int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.bmp_ag_fundings.Where(w => w.pk_bmp_ag_funding == i).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_bmp_ag_funding" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                Label lblBMPTransferNoFunds = fv.FindControl("lblBMPTransferNoFunds") as Label;
                Panel pnlBMPTransfer = fv.FindControl("pnlBMPTransfer") as Panel;
                if (a.Single().fk_agencyFunding_code == "WAC")
                {
                    DropDownList ddlBMPToTransferFrom = pnlBMPTransfer.FindControl("ddlBMPToTransferFrom") as DropDownList;

                    var b = wDataContext.form_wfp3_bmps.Where(w => w.fk_bmp_ag == a.Single().fk_bmp_ag).OrderByDescending(o => o.form_wfp3.contract_awarded_date).Select(s => new { s.fk_form_wfp3 });
                    int iPK_FormWFP3 = 0;
                    try { iPK_FormWFP3 = b.First().fk_form_wfp3; }
                    catch { }

                    WACGlobal_Methods.DatabaseFunction_Agriculture_View_BMPAG_Financial_Get_DDL(ddlBMPToTransferFrom, a.Single().fk_bmp_ag, iPK_FormWFP3, true);
                    //WACGlobal_Methods.View_Agriculture_BMP_Financial(ddlBMPToTransferFrom, Convert.ToInt32(fvAg.DataKey.Value), a.Single().fk_bmp_ag, iPK_FormWFP3, null);
                    if (ddlBMPToTransferFrom.Items.Count > 1)
                    {
                        pnlBMPTransfer.Visible = true;
                        lblBMPTransferNoFunds.Visible = false;
                    }
                    else
                    {
                        pnlBMPTransfer.Visible = false;
                        lblBMPTransferNoFunds.Visible = true;
                    }
                }
                else
                {
                    pnlBMPTransfer.Visible = false;
                    lblBMPTransferNoFunds.Visible = true;
                }
            }

            if (fv.CurrentMode == FormViewMode.Insert)
            {
            
                WACGlobal_Methods.PopulateControl_DatabaseLists_FundingPurpose_DDL(fv, "ddlFundingPurpose", null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FundingSource_DDL(fv, "ddlFundingSource", "", false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_AgencyFunding_DDL(fv.FindControl("ddlFundingAgency") as DropDownList, "Y", null, null, true);
                //WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP2Version_ByFarmBusiness_DDL(fv, "ddlWFP2Version", Convert.ToInt32(fvAg.DataKey.Value), null);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMP_DDL(fv, "ddlBMPStatus", null, false, false, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Encumbrance_DDL(fv, "ddlEncumbrance", null, false);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                
                WACGlobal_Methods.PopulateControl_DatabaseLists_FundingPurpose_DDL(fv, "ddlFundingPurpose", a.Single().fk_fundingPurpose_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FundingSource_DDL(fv, "ddlFundingSource", a.Single().fk_fundingSource_code, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_AgencyFunding_DDL(fv.FindControl("ddlFundingAgency") as DropDownList, "Y", null, a.Single().fk_agencyFunding_code, true);
                //WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP2Version_ByFarmBusiness_DDL(fv, "ddlWFP2Version", Convert.ToInt32(fvAg.DataKey.Value), a.Single().form_wfp2_version);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMP_DDL(fv, "ddlBMPStatus", a.Single().fk_statusBMP_code, false, false, true);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_Encumbrance_DDL(fv, "ddlEncumbrance", a.Single().fk_encumbrance_code, false);
            }
        }
    }

    private void BindAg_BMP_Note(FormView fv, int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.bmp_ag_notes.Where(w => w.pk_bmp_ag_note == i) select b;
            fv.DataKeyNames = new string[] { "pk_bmp_ag_note" };
            fv.DataSource = a;
            fv.DataBind();
        }
    }

    private void BindAg_BMP_Status(FormView fv, int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
         
            var a = from b in wDataContext.bmp_ag_status.Where(w => w.pk_bmp_ag_status == i) select b;
            fv.DataKeyNames = new string[] { "pk_bmp_ag_status" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMP_DDL(fv, "ddlStatus", null, false, true, false);
                WACGlobal_Methods.View_Agriculture_WFP2VersionSA_ByFarmBusiness_DDL(fv.FindControl("ddlWFP2") as DropDownList, Convert.ToInt32(fvAg.DataKey.Value), null, true);
             
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.View_Agriculture_WFP2VersionSA_ByFarmBusiness_DDL(fv.FindControl("ddlWFP2") as DropDownList, Convert.ToInt32(fvAg.DataKey.Value), a.Single().fk_form_wfp2_version, true);           
            }
        }
    }

    private void BindAg_BMP_Workload(FormView fv, int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == i) select b;
            fv.DataKeyNames = new string[] { "pk_bmp_ag_workload" };
            fv.DataSource = a;
            fv.DataBind();
            ListView lv = fvAg_BMP.FindControl("lvAg_BMP_Workload_ReadOnly") as ListView;
            var x = wDataContext.bmp_ag_workload_oneRecord(i);
            lv.DataSource = x;
            lv.DataBind();
           
            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fv.FindControl("ddlWorkload") as DropDownList, null);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fv.FindControl("ddlWorkload") as DropDownList, a.Single().year);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Agency_DDL(fv.FindControl("ddlAgency") as DropDownList, a.Single().fk_agency_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlTechnician_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlChecker_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlConstruction_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlEngineer_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMPWorkload_DDL(fv.FindControl("ddlStatusBMPWorkload") as DropDownList, a.Single().fk_statusBMPWorkload_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPWorkloadSortGroup_code_DDL(fv, "ddlWorkloadSortGroupCode", a.Single().fk_BMPWorkloadSortGroup_code);
               
            }
        }
    }

    private void CreateAgBMPFundingOverviewGrid(int i)
    {
        StringBuilder sb = new StringBuilder();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var x = wDataContext.bmp_ag_get_payments_PK(i);
            if (x.Count() > 0)
            {
                sb.Append("<hr /><div class='taC NestedDivLevel00'>");
                sb.Append("<div class='fsS B'>Payments Assigned to the BMP</div>");
                sb.Append("<center><table cellpadding='5'>");
                sb.Append("<tr valign='top'>");
                sb.Append("<td class='B U'>Date</td>");
                sb.Append("<td class='B U taR'>Amount</td>");
                sb.Append("<td class='B U'>Check Number</td>");
                sb.Append("<td class='B U'>Status</td>");
                sb.Append("</tr>");
                foreach (var y in x)
                {
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td>" + WACGlobal_Methods.Format_Global_Date(y.date) + "</td>");
                    sb.Append("<td class='taR'>" + WACGlobal_Methods.Format_Global_YesNo(y.amt) + "</td>");
                    sb.Append("<td>" + y.check_nbr + "</td>");
                    sb.Append("<td>" + y.status + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table></center>");
                sb.Append("</div><hr />");
            }
            Literal litAg_BMP_Funding_Overview_Grid = fvAg_BMP.FindControl("litAg_BMP_Funding_Overview_Grid") as Literal;
            if (litAg_BMP_Funding_Overview_Grid != null) litAg_BMP_Funding_Overview_Grid.Text = sb.ToString();
        }
    }

    #endregion

    #region Data Binding - Ag - Cropware

    private void BindAg_Cropware(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.cropwares.Where(w => w.pk_cropware == i) select b;
            fvAg_Cropware.DataKeyNames = new string[] { "pk_cropware" };
            fvAg_Cropware.DataSource = a;
            fvAg_Cropware.DataBind();

            litAg_Cropware_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);
        }
    }

    #endregion

    #region Data Binding - Ag - Farm Business Contact

    private void BindAg_FarmBusinessContact(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinessContacts.Where(w => w.pk_farmBusinessContact == i).Select(s => s);
            fvAg_FarmBusinessContact.DataKeyNames = new string[] { "pk_farmBusinessContact" };
            fvAg_FarmBusinessContact.DataSource = a;
            fvAg_FarmBusinessContact.DataBind();

            litAg_FarmBusinessContact_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_FarmBusinessContact.CurrentMode == FormViewMode.Edit)
            {
                DropDownList ddl = fvAg_FarmBusinessContact.FindControl("UC_DropDownListByAlphabet_Ag_Contact").FindControl("ddl") as DropDownList;
                Label lblLetter = fvAg_FarmBusinessContact.FindControl("UC_DropDownListByAlphabet_Ag_Contact").FindControl("lblLetter") as Label;
                WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lblLetter, a.Single().participant.lname[0].ToString(), "PARTICIPANT", "A", a.Single().fk_participant); 
            }
            gvAg_FarmBusinessMail.DataSource = wDataContext.farmBusinessMails.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
            gvAg_FarmBusinessMail.DataBind();
            WACGlobal_Methods.View_Agriculture_FarmBusinessMail_Candidates_DDL(ddlAg_FarmBusinessMail_Participant_Insert, Convert.ToInt32(fvAg.DataKey.Value), true);
        }
    }

    #endregion

    #region Data Binding - Ag - Farm Land

    private void BindAg_FarmLandTract(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmLandTracts.Where(w => w.pk_farmLandTract == i) select b;
            fvAg_FarmLandTract.DataKeyNames = new string[] { "pk_farmLandTract" };
            fvAg_FarmLandTract.DataSource = a;
            fvAg_FarmLandTract.DataBind();

            litAg_FarmLandTract_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);
        }
    }

    private void BindAg_FarmLandTractField(FormView fv, int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmLandTractFields.Where(w => w.pk_farmLandTractField == i) select b;
            fv.DataKeyNames = new string[] { "pk_farmLandTractField" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fv, "ddlYear", DateTime.Now.Year);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fv, "ddlYear", a.Single().year);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv, "ddlAvailableToRent", a.Single().availableToRent);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv, "ddlActive", a.Single().active);
            }
        }
    }

    #endregion

    #region Data Binding - Ag - Farm Business FAD

    private void BindAg_FarmBusinessFAD(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinessFADs.Where(w => w.pk_farmBusinessFAD == i).Select(s => s);
            fvAg_FarmBusinessFAD.DataKeyNames = new string[] { "pk_farmBusinessFAD" };
            fvAg_FarmBusinessFAD.DataSource = a;
            fvAg_FarmBusinessFAD.DataBind();

            litAg_FarmBusinessFAD_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_FarmBusinessFAD.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_FAD_DDL(fvAg_FarmBusinessFAD.FindControl("ddlFADStatus") as DropDownList, null, true);
            }

            if (fvAg_FarmBusinessFAD.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_FAD_DDL(fvAg_FarmBusinessFAD.FindControl("ddlFADStatus") as DropDownList, a.Single().fk_FAD_code, false);
            }
        }
    }

    #endregion

    #region Data Binding - Ag - Farm Status

    private void BindAg_FarmStatus(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinessStatus.Where(w => w.pk_farmBusinessStatus == i) select b;
            fvAg_FarmStatus.DataKeyNames = new string[] { "pk_farmBusinessStatus" };
            fvAg_FarmStatus.DataSource = a;
            fvAg_FarmStatus.DataBind();

            litAg_FarmStatus_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_FarmStatus.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Status_DDL(fvAg_FarmStatus, "ddlStatus", null);
              
            }

            if (fvAg_FarmStatus.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Status_DDL(fvAg_FarmStatus, "ddlStatus", a.Single().fk_status_code);
              
            }
        }
    }

    #endregion

    #region Data Binding - Ag - Land Base Info

    private void BindAg_LandBaseInfo(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinessLandBaseInfos.Where(w => w.pk_farmBusinessLandBaseInfo == i) select b;
            fvAg_LandBaseInfo.DataKeyNames = new string[] { "pk_farmBusinessLandBaseInfo" };
            fvAg_LandBaseInfo.DataSource = a;
            fvAg_LandBaseInfo.DataBind();

            if (fvAg_LandBaseInfo.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_LandBaseInfo, "ddlForested", a.Single().forested);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_LandBaseInfo, "ddlCommitment480A", a.Single().commitment_480A);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(fvAg_LandBaseInfo.FindControl("ddlEOHBasin") as DropDownList, null, null, "Y", null, a.Single().fk_basin_code_priorityEOH, true);
            }
        }
    }


    #endregion

    #region Data Binding - Ag - Note

    private void BindAg_Note(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinessNotes.Where(w => w.pk_farmBusinessNote == i) select b;
            fvAg_Note.DataKeyNames = new string[] { "pk_farmBusinessNote" };
            fvAg_Note.DataSource = a;
            fvAg_Note.DataBind();

            litAg_Note_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_Note.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_FarmBusinessNoteTypes_DDL(fvAg_Note, "ddlNoteType", null);
            }

            if (fvAg_Note.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_FarmBusinessNoteTypes_DDL(fvAg_Note, "ddlNoteType", a.Single().fk_farmBusinessNoteType_code);
            }
        }
    }

    #endregion

    #region Data Binding - Ag - NMP

    private void BindAg_NMP(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinessNMPs.Where(w => w.pk_nmp == i) select b;
            fvAg_NMP.DataKeyNames = new string[] { "pk_nmp" };
            fvAg_NMP.DataSource = a;
            fvAg_NMP.DataBind();
            BindAgNMP_AnimalUnits_ToLabel(Convert.ToInt32(fvAg.DataKey.Value));
            string sWACRegion = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(fvAg.DataKey.Value);

            if (fvAg_NMP.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
               
                Image imgAdvisory_NMP_phosphorousSaturation_eoh = fvAg_NMP.FindControl("imgAdvisory_NMP_phosphorousSaturation_eoh") as Image;
                if (imgAdvisory_NMP_phosphorousSaturation_eoh != null)
                    imgAdvisory_NMP_phosphorousSaturation_eoh.ToolTip = WACGlobal_Methods.Specialtext_Global_Advisory(7);
    
                Literal nmcpHistory = fvAg_NMP.FindControl("litAg_NutrientManagementCreditHistory") as Literal;
                nmcpHistory.Text = CreateAgNMCPHistoryTable(PK_FarmBusiness, DateTime.Now.Year.ToString(), wDataContext);
            }

            if (fvAg_NMP.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_NMP, "ddlNeedsNMP", a.Single().needs_nmp);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_NMP, "ddlMTC", a.Single().mtc);
                WACGlobal_Methods.PopulateControl_DatabaseLists_FollowUpNMP_DDL(fvAg_NMP, "ddlFollowUpNMP", a.Single().fk_followupNMP_code);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_NMP, "ddlEQIP", a.Single().EQIP);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_NMP, "ddlNMPCredit", a.Single().nmp_credit);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_NMP, "ddlCREP", a.Single().CREP);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_NMP, "ddlAWEPSignup", a.Single().AWEP_signup);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fvAg_NMP, "ddlDesignerEngineerNRCS", new string[] { "NRCS" }, a.Single().fk_list_designerEngineer_nrcs, false, sWACRegion);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fvAg_NMP, "ddlDesignerEngineerNMP", new string[] { "NMP" }, a.Single().fk_list_designerEngineer_nmp, false, sWACRegion);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_NMP, "ddlCropYear", a.Single().crop_year);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_NMP, "ddlCropYearExpiration", a.Single().crop_year_expiration);
                WACGlobal_Methods.PopulateControl_DatabaseLists_NMPStorage_code_DDL((DropDownList)fvAg_NMP.FindControl("ddlStorageCode"), i, a.Single().fk_NMPStorage_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPAgNMP_DDL((DropDownList)fvAg_NMP.FindControl("ddlBMPAgNMP"), a.Single().fk_farmBusiness,a.Single().fk_bmp_ag_nmp.ToString());
                DropDownList ddl = (DropDownList)fvAg_NMP.FindControl("ddlBasicPlan");
                if (ddl != null)
                {
                    var t = wDataContext.list_NMPlanTypes.Select(s => s).OrderBy(o => o.type);
                    ddl.Items.Add(new ListItem("[SELECT]", ""));
                    foreach (var item in t.ToList())
                    {
                        ddl.Items.Add(new ListItem(item.type,item.pk_NMPlanType_code));
                    }
                    if (a.Any() && a.Single().basic_plan != null)
                        ddl.SelectedValue = a.Single().basic_plan.ToString();
                }
            }
            if (fvAg_NMP.CurrentMode == FormViewMode.Insert)
            {
                DropDownList ddl = (DropDownList)fvAg_NMP.FindControl("ddlBasicPlan");
                if (ddl != null)
                {
                    var t = wDataContext.list_NMPlanTypes.Select(s => s).OrderBy(o => o.type);
                    ddl.Items.Add(new ListItem("[SELECT]", ""));
                    foreach (var item in t.ToList())
                    {
                        ddl.Items.Add(new ListItem(item.type,item.pk_NMPlanType_code));
                    }
                }
            }
            
        }
    }
    private string CreateAgNMCPHistoryTable(int pk_farmBusiness, string lastCreditYear, WACDataClassesDataContext wDataContext)
    {
        StringBuilder sb = new StringBuilder();
        Int16 start = 0;
        Int16 end = 0;
        if (Int16.TryParse(lastCreditYear, out end))
        {
            var nmc = wDataContext.FarmNMCPAwardHistory(pk_farmBusiness, end)
                .Select(s => new
                {
                    awardTotal = s.award_total,
                    startYear = s.startingYear,
                    balanceY1 = s.nmcpBalYr1,
                    balanceY2 = s.nmcpBalYr2,
                    balanceY3 = s.nmcpBalYr3,
                    balanceY4 = s.nmcpBalYr4,
                    balanceY5 = s.nmcpBalYr5,
                    balanceY6 = s.nmcpBalYr6,
                    balanceY7 = s.nmcpBalYr7,
                    balanceY8 = s.nmcpBalYr8
                })
                .First();
            sb.Append("<div class='taC NestedDivLevel00'>");
            sb.Append("<div class='fsS B'>Nutrient Management Credit Program History</div>");
            sb.Append("<center><table cellpadding='5'>");
            sb.Append("<tr valign='top'>");
            if (nmc != null)
            {
                if (!Int16.TryParse(nmc.startYear.ToString(), out start))
                    sb.Append("<td class='B U'>Error parsing NMCP data.</td>");
                else
                {

                    sb.Append("<td class='B taC'>");
                    sb.Append(start);
                    sb.Append("</td>");
                    sb.Append("<td class='B taC'>");
                    sb.Append(++start);
                    sb.Append("</td>");
                    sb.Append("<td class='B taC'>");
                    sb.Append(++start);
                    sb.Append("</td>");
                    sb.Append("<td class='B taC'>");
                    sb.Append(++start);
                    sb.Append("</td>");
                    sb.Append("<td class='B taC'>");
                    sb.Append(++start);
                    sb.Append("</td>");
                    sb.Append("<td class='B taC'>");
                    sb.Append(++start);
                    sb.Append("</td>");
                    sb.Append("<td class='B taC'>");
                    sb.Append(++start);
                    sb.Append("</td>");
                    sb.Append("<td class='B taC'>");
                    sb.Append(++start);
                    sb.Append("</td></tr>");
                    //sb.Append("<tr><td colspan='8'><hr /></td></tr><tr><td class='taL'>$");
                    sb.Append("<tr><td class='taL'>$");
                    sb.Append(nmc.balanceY1);
                    sb.Append("</td><td class='taL'>$");
                    sb.Append(nmc.balanceY2);
                    sb.Append("</td><td class='taL'>$");
                    sb.Append(nmc.balanceY3);
                    sb.Append("</td><td class='taL'>$");
                    sb.Append(nmc.balanceY4);
                    sb.Append("</td><td class='taL'>$");
                    sb.Append(nmc.balanceY5);
                    sb.Append("</td><td class='taL'>$");
                    sb.Append(nmc.balanceY6);
                    sb.Append("</td><td class='taL'>$");
                    sb.Append(nmc.balanceY7);
                    sb.Append("</td><td class='taL'>$");
                    sb.Append(nmc.balanceY8);
                    sb.Append("</td>");
                }

            }
            else
                sb.Append("<td class='B U'>No Nutrient Management Credit data found</td>");
            sb.Append("</tr>");
            sb.Append("</table></center>");
            sb.Append("</div>");

            return sb.ToString();
        }
        else
        {
            WACAlert.Show(WacRadWindowManager, "Invalid or missing end date for NMCP history!", 0);
        }   
        return string.Empty;
    }

    private void BindAgNMP_AnimalUnits_ToLabel(int i)
    {
        Label lblAnimalUnits = fvAg_NMP.FindControl("lblAnimalUnits") as Label;
        if (lblAnimalUnits != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    var a = wDataContext.farmBusiness_get_animalUnits_PK(i);
                    if (a.Count() == 1) lblAnimalUnits.Text = Math.Round(Convert.ToDecimal(a.Single().AU), 2).ToString();
                }
                catch (Exception ex) { lblAnimalUnits.Text = ex.Message; }
            }
        }
    }

    #endregion

    #region Data Binding - Ag - Tier 1

    private void BindAg_Tier1(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            AjaxControlToolkit.TabContainer tc = (AjaxControlToolkit.TabContainer)fvAg.FindControl("tcAg");
            AjaxControlToolkit.TabPanel tp = (AjaxControlToolkit.TabPanel)tc.FindControl("tpAg_Tier1");
            FormView fv = (FormView)tp.FindControl("fvAg_Tier1");
            
            var a = from b in wDataContext.farmBusinessTier1s.Where(w => w.pk_farmBusinessTier1 == i) select b;

            fv.DataKeyNames = new string[] { "pk_farmBusinessTier1" };
            fv.DataSource = a;
            fv.DataBind();
            
            if (fv.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                CreateAg_Tier1_Grid(Convert.ToInt32(fvAg.DataKey.Value));
            }
            if (fv.CurrentMode == FormViewMode.Edit)
            {
                DropDownList ddl = (DropDownList)fv.FindControl("ddlEOHIncome1K");
                if (ddl != null)
                {
                    ddl.Items.Add(new ListItem("[SELECT]", ""));
                    ddl.Items.Add(new ListItem("Yes", "Y"));
                    ddl.Items.Add(new ListItem("No", "N"));
                    if (a.Any() && a.Single().eoh_income1K != null)
                        ddl.SelectedValue = a.Single().eoh_income1K.ToString();
                }
            }
        }
    }

    private void BindAg_Tier1_Animal(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinessTier1Animals.Where(w => w.pk_farmBusinessTier1Animal == i).Select(s => s);
            fvAg_Tier1_Animal.DataKeyNames = new string[] { "pk_farmBusinessTier1Animal" };
            fvAg_Tier1_Animal.DataSource = a;
            fvAg_Tier1_Animal.DataBind();

            litAg_Tier1_Animal_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_Tier1_Animal.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Animal_DDL(fvAg_Tier1_Animal, "ddlAnimal", null, "", "Y");
            }

            if (fvAg_Tier1_Animal.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Animal_DDL(fvAg_Tier1_Animal, "ddlAnimal", a.Single().fk_list_animal, "", "Y");
            }
        }
    }

    private void CreateAg_Tier1_Grid(int i)
    {
        //AjaxControlToolkit.TabPanel tp = (AjaxControlToolkit.TabPanel)fvAg.FindControl("tpAg_Tier1");
        //FormView fvAg_Tier1 = (FormView)tp.FindControl("fvAg_Tier1");
        Literal litAg_Tier1 = fvAg_Tier1.FindControl("litAg_Tier1") as Literal;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var x = wDataContext.farmBusiness_get_tier1ReadOnly(i);
                if (x.Count() == 1)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<center><div class='NestedDivLevel00'><table cellpadding='5'>");
                    //sb.Append("<tr valign='top'><td class='B taR'>Tier 1 Sent Date:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Date(x.Single().tier1_sent_date) + "</td></tr>");
                    //sb.Append("<tr valign='top'><td class='B taR'>Tier 1 Received Date:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Date(x.Single().tier1_recd_date) + "</td></tr>");
                    //sb.Append("<tr valign='top'><td class='B taR'>Tier 2 Sent Date:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Date(x.Single().tier2_sent_date) + "</td></tr>");
                    //sb.Append("<tr valign='top'><td class='B taR'>Tier 2 Received Date:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Date(x.Single().tier2_recd_date) + "</td></tr>");
                    sb.Append("<tr valign='top'><td class='B taR'>Farm Status:</td><td class='taL'>" + x.Single().status + "</td></tr>");
                    sb.Append("<tr valign='top'><td class='B taR'>AEM Level:</td><td class='taL'>" + x.Single().AEMLevel+ "</td></tr>");
                    sb.Append("<tr valign='top'><td class='B taR'>WFP2 Approved Date:</td><td class='taL'>" + WACGlobal_Methods.Format_Global_Date(x.Single().wfp2_appr_date) + "</td></tr>");
                    sb.Append("</table></div></center>");

                    litAg_Tier1.Text = sb.ToString();
                }
                else litAg_Tier1.Text = "<div class='B' style='color:red;'>Could not create overview list: Record not found.</div>";
            }
            catch (Exception ex) { litAg_Tier1.Text = "<div class='B' style='color:red;'>Could not create overview list: " + ex.Message + "</div>"; }
        }
    }

    #endregion

    #region Data Binding - Ag - WFP2

    private void BindAg_WFP2(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.form_wfp2s.Where(w => w.pk_form_wfp2 == i) select b;
            fvAg_WFP2.DataKeyNames = new string[] { "pk_form_wfp2" };
            fvAg_WFP2.DataSource = a;
            fvAg_WFP2.DataBind();

            string sWACRegion = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(fvAg.DataKey.Value);

            if (fvAg_WFP2.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fvAg_WFP2, "ddlPlanner", new string[] { "PLAN" }, a.Single().fk_list_designerEngineer, false, sWACRegion);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fvAg_WFP2, "ddlPlannerTechnician1", new string[] { "PLAN", "TECH" }, a.Single().fk_list_designerEngineer_planner2, false, sWACRegion);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fvAg_WFP2, "ddlPlannerTechnician2", new string[] { "PLAN", "TECH" }, a.Single().fk_list_designerEngineer_planner3, false, sWACRegion);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Agency_DDL(fvAg_WFP2, "ddlAgency", a.Single().fk_agency_code);
            }
        }
    }

    private void BindAg_WFP2_Version(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.form_wfp2_versions.Where(w => w.pk_form_wfp2_version == i) select b;
            fvAg_WFP2_Version.DataKeyNames = new string[] { "pk_form_wfp2_version" };
            fvAg_WFP2_Version.DataSource = a;
            fvAg_WFP2_Version.DataBind();

            litAg_WFP2_Version_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_WFP2_Version.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP2Revision_PastAndFutureRevisions_DDL(fvAg_WFP2_Version.FindControl("ddlRevision") as DropDownList, Convert.ToInt32(fvAg_WFP2.DataKey.Value), false);

                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_WFP2_Version, "ddlInhouseRevision", "N", false);
            }

            if (fvAg_WFP2_Version.CurrentMode == FormViewMode.Edit)
            {
                
                WACGlobal_Methods.PopulateControl_DatabaseLists_WFP2ApprovedBy_DDL(fvAg_WFP2_Version.FindControl("ddlWFP2ApprovedBy") as DropDownList, a.Single().fk_WFP2ApprovedBy_code, true);
                
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_WFP2_Version, "ddlInhouseRevision", a.Single().inhouse_revision, false);
            }
        }
    }

    #endregion

    #region Data Binding - Ag - WFP3

    #endregion

    #region Data Binding - Ag - Workload Project

    private void BindAg_WLProject(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinessWLProjects.Where(w => w.pk_farmBusinessWLProject == i).Select(s => s);
            fvAg_WLProject.DataKeyNames = new string[] { "pk_farmBusinessWLProject" };
            fvAg_WLProject.DataSource = a;
            fvAg_WLProject.DataBind();

            litAg_WLProject_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(fvAg.DataKey.Value);

            if (fvAg_WLProject.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_WLProject_DDL(fvAg_WLProject.FindControl("ddlAg_WLProject_BMP_Insert") as DropDownList, 
                    Convert.ToInt32(fvAg.DataKey.Value), null, new int?[] { 0, 1 });
            }

            //if (fvAg_WLProject.CurrentMode != FormViewMode.ReadOnly)
            //    WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_WLProject_DDL(fvAg_WLProject.FindControl("ddlAg_WLProject_BMP_Insert") as DropDownList,
            //        Convert.ToInt32(fvAg.DataKey.Value), null, new int?[] { 0, 1 });
            //if (fvAg_WLProject.CurrentMode == FormViewMode.Insert)
            //{
            //    WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_WLProject.FindControl("ddlDesignYear") as DropDownList, null);
            //    WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_WLProject.FindControl("ddlBuildYear") as DropDownList, null);
            //}

            //if (fvAg_WLProject.CurrentMode == FormViewMode.Edit)
            //{
            //    WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_WLProject.FindControl("ddlDesignYear") as DropDownList, a.Single().design_year);
            //    WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvAg_WLProject.FindControl("ddlBuildYear") as DropDownList, a.Single().build_year);
            //}
        }
    }

    #endregion

    #region Rebinding Data Controls

    private void ReBindAg_LandBaseInfo(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.farmBusinessLandBaseInfos.Where(w => w.fk_farmBusiness == i) select b;
            fvAg_LandBaseInfo.DataKeyNames = new string[] { "pk_farmBusinessLandBaseInfo" };
            fvAg_LandBaseInfo.DataSource = a;
            fvAg_LandBaseInfo.DataBind();

            if (fvAg_LandBaseInfo.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_LandBaseInfo, "ddlForested", a.Single().forested);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_LandBaseInfo, "ddlCommitment480A", a.Single().commitment_480A);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(fvAg_LandBaseInfo.FindControl("ddlEOHBasin") as DropDownList, null, null, "Y", null, a.Single().fk_basin_code_priorityEOH, true);
            }
        }
    }
    private void Rebind_Data_Controls(Enum_Ag_RebindableSections enumRebind, object esO, bool bRecreateOverviewGrid, bool bPopulateInsertMechanism, bool bResetInsertMechanism)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            switch (enumRebind)
            {
                case Enum_Ag_RebindableSections.farmBusinessMail:

                    if (esO == null) gvAg_FarmBusinessMail.DataSource = wac.farmBusinessMails.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
                    else gvAg_FarmBusinessMail.DataSource = esO;
                    gvAg_FarmBusinessMail.DataBind();
                    if (bPopulateInsertMechanism) WACGlobal_Methods.View_Agriculture_FarmBusinessMail_Candidates_DDL(ddlAg_FarmBusinessMail_Participant_Insert, Convert.ToInt32(fvAg.DataKey.Value), true);
                    if (bResetInsertMechanism) ddlAg_FarmBusinessMail_Participant_Insert.SelectedIndex = 0;
                    break;
                case Enum_Ag_RebindableSections.farmBusinessOperator:
                    ListView lvAg_FarmBusinessOperators = fvAg.FindControl("lvAg_FarmBusinessOperators") as ListView;
                    if (esO == null)
                    {
                        var x = wac.farmBusinessOperators.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
                        lvAg_FarmBusinessOperators.DataSource = x; 
                        gvAg_Operators.DataSource = x;
                    }
                    else
                    {
                        lvAg_FarmBusinessOperators.DataSource = esO;
                        gvAg_Operators.DataSource = esO;
                    }
                    lvAg_FarmBusinessOperators.DataBind();
                    gvAg_Operators.DataBind();
                    if (bPopulateInsertMechanism) WACGlobal_Methods.PopulateControl_Participant_Name_ByParticipantType_DDL(ddlAg_Operator_Add, "O", null, true);
                    if (bResetInsertMechanism) ddlAg_Operator_Add.SelectedIndex = 0;
                    break;
                case Enum_Ag_RebindableSections.farmBusinessOwner:
                    ListView lvAg_FarmBusinessOwners = fvAg.FindControl("lvAg_FarmBusinessOwners") as ListView;
                    var z = wac.farmBusinessOwners.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value)).
                            OrderBy(o => o.participant.fullname_LF_dnd).Select(s => s);
                    var y = z.Where(w => w.active == "Y");
                    if (esO == null)
                    {                     
                        lvAg_FarmBusinessOwners.DataSource = y;
                        gvAg_Owners.DataSource = z;
                    }
                    else
                    {
                        lvAg_FarmBusinessOwners.DataSource = y;
                        gvAg_Owners.DataSource = esO;
                    }
                    lvAg_FarmBusinessOwners.DataBind();
                    gvAg_Owners.DataBind();
                    if (bResetInsertMechanism) UC_DropDownListByAlphabet_Ag_FarmBusinessOwner.ResetControls();
                    break;
                case Enum_Ag_RebindableSections.farmBusinessPlanner:
                    if (esO == null) gvAg_Planners.DataSource = wac.farmBusinessPlanners.Where(w => w.fk_farmBusiness == Convert.ToInt32(fvAg.DataKey.Value))
                        .OrderBy(o => o.list_designerEngineer.designerEngineer).Select(s => s);
                    else gvAg_Planners.DataSource = esO;
                    gvAg_Planners.DataBind();
                    if (bPopulateInsertMechanism) 
                        WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(ddlAg_Planner_Add, 
                            new string[] { "PLAN" }, null, true, WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(fvAg.DataKey.Value));
                    if (bResetInsertMechanism) 
                        ddlAg_Planner_Add.SelectedIndex = 0;
                    break;
            }
            
        }
        if (bRecreateOverviewGrid) CreateAgOverviewGrid(Convert.ToInt32(fvAg.DataKey.Value), null);
    }

    #endregion

    public string Decode_ListEasementTypeCode(object code)
    {
        if (code == null)
            return "No";
        switch (code.ToString())
        {
            case "F":
                return "Yes";
            case "P":
                return "Partial";
            case "D":
                return "Pending";
            default:
                return "No";
        }
    }
    public string Decode_ListNMPlanTypeCode(object code)
    {
        if (code == null)
            return "No";
        switch (code.ToString())
        {
            case "B":
                return "Basic";
            case "I":
                return "Simple";
            case "S":
                return "Standard";
            default:
                return "No";
        }
    }

    public string Decode_PracticeUnits(object units, object unitCode)
    {
        decimal u;
        string uc;
        try
        {
            u = (decimal)units;
            uc = (string)unitCode;
        }
        catch (Exception)
        {
            return string.Empty;
        }
        return u > 0 ? u.ToString() + " " + uc : u.ToString();
    }
    public bool IsChecked(object yn)
    {
        return (string)yn == "Y";
    }
    public string IsFarmEased(object pkFarmBusiness)
    {
        int ease = 0;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
           ease = wac.GetEasementPK(Convert.ToInt32(pkFarmBusiness)).Value;    
        }
        return ease == 0 ? "N" : "Y";
    }



    protected void ddlOperTaxParcels_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlOperTaxParcels = (DropDownList)sender;
        Control parent = ddlOperTaxParcels.NamingContainer;
        DropDownList ddlJurisdiction = (DropDownList)parent.FindControl("ddlJurisdiction");
        if (ddlOperTaxParcels != null && !string.IsNullOrEmpty(ddlOperTaxParcels.SelectedValue))
            FarmBusinessTaxParcel_Insert(ddlJurisdiction,ddlOperTaxParcels);
    }


    private static bool IsGoodBmpNumber(string bmp)
    {
        if (string.IsNullOrEmpty(bmp))
            return false;
        var m = Regex.Match(bmp, @"^(PIRC.*)|((\d{2,3}|(PIRC|IRC|PH|OE)\d{2,3})((?<!\d{2,3})\d{2})?([a-z-[st]]?)((?<=[a-z-[st]])\d)?(S\d?|T?|ST)?)$|^(\d{2}(NMC?P|PFMQ?)\d{2})$");
        return m.Success;
    }

    public static bool IsNonGroupingBmp(string bmp, string descriptor, string description, decimal? practiceCode)
    {
        if (string.IsNullOrEmpty(bmp))
            return false;
        if (!string.IsNullOrEmpty(descriptor) && (descriptor.Contains("MTC") || descriptor.Contains("ENMC")))
            return true;
        if (practiceCode.HasValue && practiceCode == 590m)
            return true;
        var m = Regex.Match(bmp, @"^(\d{2}(NMC?P|PFMQ?|PIRC)\d{2})$|^(PIRC.*|.*NMP.*|.*ENMC*.|OE.*|PH.*)$");
        return m.Success;
    }

    public static bool IsNonGroupingBmp(bmp_ag bmp)
    {
        return IsNonGroupingBmp(bmp.Bmp, bmp.AgBmpDescriptorCode.DescriptorCode, bmp.description, bmp.fk_bmpPractice_code);
    }


}
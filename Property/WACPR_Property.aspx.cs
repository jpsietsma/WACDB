using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_UserControls;
using WAC_Connectors;
using WAC_ViewModels;
using WAC_Services;
using WAC_Event;
using WAC_DataObjects;

public partial class Property_WACPR_Property : WACPage, IWACControl
{
    public bool _BoolShowCountyTownship = true;
    public bool _BoolShowBasinSubbasin = true;
    public bool _BoolShowTownship = true;
    public bool _BoolShowSubbasin = true;
    public bool _CountyControlsBasin = true;
    ServiceRequest sReq;
    ServiceResponse sResp;
    public override void OpenDefaultDataView(List<WACParameter> parms)
    {
        BindProperty(WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms,WACParameter.ParameterType.PrimaryKey)));
    }
  
    protected void Page_Init(object sender, EventArgs e)
    {
        //base.RegisterAndConnect(this, ref sReq, sResp);
    }
    public override string ID { get { return "WACPR_PropertyPage"; } }
    public override string ClientID { get { return this.ID; } }
    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        //try { UC_Global_Insert1.ShowGlobal_Insert(); }
        //catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
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
            //UC_Express_Property.LoadFormView_Property(Convert.ToInt32(oPK_Property));
        }
        catch { WACAlert.Show("Could not open Property Window.", 0); }
    }

    public void InvokedMethod_SectionPage_RebindRecord()
    {
      //upParticipantSearch.Update();
    }

    public void InvokedMethod_DropDownListByAlphabet(object oType)
    {
        if (oType != null)
        {
            switch (oType.ToString())
            {
                case "PARTICIPANT_PERSON_SEARCH": Search_Participant_Person(); break;
                case "PARTICIPANT_ORGANIZATION_SEARCH": Search_Participant_Organization(); break;
            }
        }
    }
    public void InvokedMethod_DropDownListByAlphabet_LinkButtonEvent(object oType, object oValue)
    {
        switch (oType.ToString())
        {
         //   case "PARTICIPANT_ORGANIZATION_SEARCH_MULTI": Search_Participant_Organization_Multi(oValue.ToString()); break;
        }
    }
private void Search_Participant_Person()
    {
        DropDownList ddl = UC_DropDownListByAlphabet_Search_Participant.FindControl("ddl") as DropDownList;
        Session["order"] = "";
        Session["searchKey"] = ddl.SelectedValue;
        Session["searchTypeProperties"] = "P";
        ChangeIndex2Zero4SearchDDLs(false);
        ddl.SelectedValue = Session["searchKey"].ToString();
        HandleQueryType();
        upProperty.Update();
    }

    private void Search_Participant_Organization()
    {
        DropDownList ddl = UC_DropDownListByAlphabet_Search_Participant.FindControl("ddl") as DropDownList;
        Session["order"] = "";
        Session["searchKey"] = ddl.SelectedValue;
        Session["searchTypeProperties"] = "P";
        ChangeIndex2Zero4SearchDDLs(false);
        ddl.SelectedValue = Session["searchKey"].ToString();
        HandleQueryType();
        upProperty.Update();
    }
    #endregion
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["searchTypeProperties"] = "";
            Session["searchKeyProperties"] = "";
            Session["resultsProperties"] = "";
            Session["orderProperties"] = "";
            Session["countProperties"] = "";

            PopulateDDL4Search_Zip();
            PopulateDDL4Search_Township();
            PopulateDDL4Search_County();
            PopulateDDL4Search_FarmBusiness();

            hlProperty_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Global Data Property.pdf";
            hlProperty_Help.ImageUrl = "~/images/help_24.png";

            string sQS_PK = Request.QueryString["pk"];
            if (!string.IsNullOrEmpty(sQS_PK)) BindProperty(Convert.ToInt32(sQS_PK));
        }
        
    }

    private void PopulateDDL4Search_Zip()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddlProperty_Search_Zip.Items.Clear();
            var a = from b in wDataContext.properties.Where(w => w.fk_zipcode != "")
                    group b by b.fk_zipcode into g
                    orderby g.Key
                    select g.Key;
            ddlProperty_Search_Zip.DataSource = a;
            ddlProperty_Search_Zip.DataBind();
            ddlProperty_Search_Zip.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateDDL4Search_Township()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddlProperty_Search_Township.Items.Clear();
            var a = from b in wDataContext.properties.Where(w => w.fk_list_townshipNY != null)
                    group b by b.list_townshipNY.township into g
                    orderby g.Key
                    select g.Key;
            ddlProperty_Search_Township.DataSource = a;
            ddlProperty_Search_Township.DataBind();
            ddlProperty_Search_Township.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateDDL4Search_County()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddlProperty_Search_County.Items.Clear();
            var a = from b in wDataContext.properties.Where(w => w.fk_list_countyNY != null)
                    group b by b.list_countyNY.county into g
                    orderby g.Key
                    select g.Key;
            ddlProperty_Search_County.DataSource = a;
            ddlProperty_Search_County.DataBind();
            ddlProperty_Search_County.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateDDL4Search_FarmBusiness()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddlProperty_Search_FarmBusiness.Items.Clear();
            var a = from b in wDataContext.properties.Where(w => w.farmLands.First().farmBusinesses.First().farmID != "")
                    group b by b.farmLands.First().farmBusinesses.First().farmID into g
                    orderby g.Key
                    select g.Key;
            ddlProperty_Search_FarmBusiness.DataSource = a;
            ddlProperty_Search_FarmBusiness.DataBind();
            ddlProperty_Search_FarmBusiness.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    #endregion

    #region Event Handling - Search

    public void HandleQueryType()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (Session["searchTypeProperties"].ToString())
            {
                case "P":
                    string pkS = Session["searchKey"].ToString();
                    int pk = Convert.ToInt32(Session["searchKey"].ToString());
                    var aP =
                        from b in wDataContext.properties
                        join c in wDataContext.vw_participant_property_masters.Where(w => w.pk_participant == pk)
                            on b.pk_property equals c.pk_property
                        select b;
                    Session["countProperties"] = aP.Count().ToString();
                    Session["resultsProperties"] = aP;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aP.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "A":
                    var aA = from b in wDataContext.properties select b;
                    Session["countProperties"] = aA.Count();
                    Session["resultsProperties"] = aA;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aA.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "B":
                    var aB = from b in wDataContext.properties.Where(w => w.fk_zipcode == Session["searchKeyProperties"].ToString()) select b;
                    Session["countProperties"] = aB.Count();
                    Session["resultsProperties"] = aB;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aB.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "C":
                    var aC = from b in wDataContext.properties.Where(w => w.list_townshipNY.township == Session["searchKeyProperties"].ToString()) select b;
                    Session["countProperties"] = aC.Count();
                    Session["resultsProperties"] = aC;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aC.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "D":
                    var aD = from b in wDataContext.properties.Where(w => w.list_countyNY.county == Session["searchKeyProperties"].ToString()) select b;
                    Session["countProperties"] = aD.Count();
                    Session["resultsProperties"] = aD;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aD.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "E":
                    var aE = from b in wDataContext.properties.Where(w => w.farmLands.First().farmBusinesses.First().farmID == Session["searchKeyProperties"].ToString()) select b;
                    Session["countProperties"] = aE.Count();
                    Session["resultsProperties"] = aE;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aE.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "F":
                    var aF = wDataContext.properties.Where(w => w.participantProperties.Any(a => a.fk_participant == Convert.ToInt32(Session["searchKeyProperties"]))).Select(s => s);
                    Session["countProperties"] = aF.Count();
                    Session["resultsProperties"] = aF;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aF.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "G":
                    var aG = from b in wDataContext.properties.Where(w => w.state == Session["searchKeyProperties"].ToString()) select b;
                    Session["countProperties"] = aG.Count();
                    Session["resultsProperties"] = aG;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aG.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "H":
                    List<string> listStateCity = (List<string>)Session["searchKeyProperties"];
                    var aH = from b in wDataContext.properties.Where(w => w.state == listStateCity[0] && w.city == listStateCity[1]) select b;
                    Session["countProperties"] = aH.Count();
                    Session["resultsProperties"] = aH;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aH.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "I":
                    List<string> listStateCityAddressType = (List<string>)Session["searchKeyProperties"];
                    var aI = from b in wDataContext.properties.Where(w => w.state == listStateCityAddressType[0] && w.city == listStateCityAddressType[1] && w.fk_addressType_code == listStateCityAddressType[2]) select b;
                    Session["countProperties"] = aI.Count();
                    Session["resultsProperties"] = aI;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aI.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "J":
                    List<string> listStateCityAddress = (List<string>)Session["searchKeyProperties"];
                    var aJ = from b in wDataContext.properties.Where(w => w.state == listStateCityAddress[0] && w.city == listStateCityAddress[1] && w.address_base == listStateCityAddress[2]) select b;
                    Session["countProperties"] = aJ.Count();
                    Session["resultsProperties"] = aJ;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aJ.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "K":
                    List<string> listStateCityAddressNumber = (List<string>)Session["searchKeyProperties"];
                    var aK = from b in wDataContext.properties.Where(w => w.state == listStateCityAddressNumber[0] && w.city == listStateCityAddressNumber[1] && w.address_base == listStateCityAddressNumber[2] && w.nbr == listStateCityAddressNumber[3]) select b;
                    Session["countProperties"] = aK.Count();
                    Session["resultsProperties"] = aK;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aK.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "L":
                    List<string> listStateCityNonRoadNumber = (List<string>)Session["searchKeyProperties"];
                    var aL = from b in wDataContext.properties.Where(w => w.state == listStateCityNonRoadNumber[0] && w.city == listStateCityNonRoadNumber[1] && w.fk_addressType_code == listStateCityNonRoadNumber[2] && w.nbr == listStateCityNonRoadNumber[3]) select b;
                    Session["countProperties"] = aL.Count();
                    Session["resultsProperties"] = aL;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aL.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                case "M":
                    var aM = wDataContext.properties.Where(w => w.address.StartsWith(Session["searchKeyProperties"].ToString())).OrderBy(o => o.address).Select(s => s);
                    Session["countProperties"] = aM.Count();
                    Session["resultsProperties"] = aM;
                    if (!string.IsNullOrEmpty(Session["orderProperties"].ToString())) Session["resultsProperties"] = aM.OrderBy(Session["orderProperties"].ToString(), null);
                    break;
                default:
                    Session["countProperties"] = 0;
                    Session["resultsProperties"] = "";
                    Session["orderProperties"] = "";
                    break;
            }
            BindProperties();
        }
    }

    public void ChangeIndex2Zero4SearchDDLs(bool bResetUC)
    {
        gvProperty.SelectedIndex = -1;
        ViewState["SelectedValueProperties"] = null;
        ClearProperty();
        try { ddlProperty_Search_Zip.SelectedIndex = 0; }
        catch { }
        try { ddlProperty_Search_FarmBusiness.SelectedIndex = 0; }
        catch { }
        try { ddlProperty_Search_Township.SelectedIndex = 0; }
        catch { }
        try { ddlProperty_Search_County.SelectedIndex = 0; }
        catch { }
        if (bResetUC)
        {
            try { UC_DropDownListByAlphabet_Search_Participant.ResetControls(); }
            catch { }
        }
    }

    protected void lbProperty_Search_ReloadReset_Click(object sender, EventArgs e)
    {
        ChangeIndex2Zero4SearchDDLs(true);
        HandleAddressSearchDDLs();
        ClearProperties();
    }

    protected void lbProperty_All_Click(object sender, EventArgs e)
    {
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "A";
        Session["searchKeyProperties"] = "";
        ChangeIndex2Zero4SearchDDLs(true);
        HandleAddressSearchDDLs();
        HandleQueryType();
    }

    protected void ddlProperty_Search_Zip_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlProperty_Search_Zip.SelectedValue))
        {
            Session["orderProperties"] = "";
            Session["searchTypeProperties"] = "B";
            Session["searchKeyProperties"] = ddlProperty_Search_Zip.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            HandleAddressSearchDDLs();
            ddlProperty_Search_Zip.SelectedValue = Session["searchKeyProperties"].ToString();
            HandleQueryType();
        }
    }

    protected void ddlProperty_Search_Township_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlProperty_Search_Township.SelectedValue))
        {
            Session["orderProperties"] = "";
            Session["searchTypeProperties"] = "C";
            Session["searchKeyProperties"] = ddlProperty_Search_Township.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            HandleAddressSearchDDLs();
            ddlProperty_Search_Township.SelectedValue = Session["searchKeyProperties"].ToString();
            HandleQueryType();
        }
    }

    protected void ddlProperty_Search_County_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlProperty_Search_County.SelectedValue))
        {
            Session["orderProperties"] = "";
            Session["searchTypeProperties"] = "D";
            Session["searchKeyProperties"] = ddlProperty_Search_County.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            HandleAddressSearchDDLs();
            ddlProperty_Search_County.SelectedValue = Session["searchKeyProperties"].ToString();
            HandleQueryType();
        }
    }

    protected void ddlProperty_Search_FarmBusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlProperty_Search_FarmBusiness.SelectedValue))
        {
            Session["orderProperties"] = "";
            Session["searchTypeProperties"] = "E";
            Session["searchKeyProperties"] = ddlProperty_Search_FarmBusiness.SelectedValue;
            ChangeIndex2Zero4SearchDDLs(true);
            HandleAddressSearchDDLs();
            ddlProperty_Search_FarmBusiness.SelectedValue = Session["searchKeyProperties"].ToString();
            HandleQueryType();
        }
    }

    private void Search_Participant_SelectedIndexChanged()
    {
        DropDownList ddl = UC_DropDownListByAlphabet_Search_Participant.FindControl("ddl") as DropDownList;
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "F";
        Session["searchKeyProperties"] = ddl.SelectedValue;
        ChangeIndex2Zero4SearchDDLs(false);
        ddl.SelectedValue = Session["searchKeyProperties"].ToString();
        HandleQueryType();
        upProperty.Update();
    }

    protected void btnProperty_Search_AddressText_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbProperty_Search_AddressText.Text))
        {
            Session["orderProperties"] = "";
            Session["searchTypeProperties"] = "M";
            Session["searchKeyProperties"] = tbProperty_Search_AddressText.Text;
            ChangeIndex2Zero4SearchDDLs(true);
            tbProperty_Search_AddressText.Text = Session["searchKeyProperties"].ToString();
            HandleQueryType();
        }
        else WACAlert.Show("Please enter a full or address.", 0);
    }

    #endregion

    #region Event Handling - Search Address

    public void UpdateUpdatePanel()
    {
        upProperty.Update();
    }

    public void DefineSpecificValuesForState(string sValue)
    {
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "G";
        Session["searchKeyProperties"] = sValue;
    }

    public void DefineSpecificValuesForCity(List<string> listProperty)
    {
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "H";
        Session["searchKeyProperties"] = listProperty;
    }

    public void DefineSpecificValuesForAddressType(List<string> listProperty)
    {
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "I";
        Session["searchKeyProperties"] = listProperty;
    }

    public void DefineSpecificValuesForAddress(List<string> listProperty)
    {
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "J";
        Session["searchKeyProperties"] = listProperty;
    }

    public void DefineSpecificValuesForAddressNumber(List<string> listProperty)
    {
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "K";
        Session["searchKeyProperties"] = listProperty;
    }

    public void DefineSpecificValuesForPOB(List<string> listProperty)
    {
        Session["orderProperties"] = "";
        Session["searchTypeProperties"] = "L";
        Session["searchKeyProperties"] = listProperty;
    }

    private void HandleAddressSearchDDLs()
    {
        DropDownList ddl_Search_State = UC_SearchByAddress1.FindControl("ddl_Search_State") as DropDownList;
        DropDownList ddl_Search_City = UC_SearchByAddress1.FindControl("ddl_Search_City") as DropDownList;
        DropDownList ddl_Search_AddressType = UC_SearchByAddress1.FindControl("ddl_Search_AddressType") as DropDownList;
        DropDownList ddl_Search_Address = UC_SearchByAddress1.FindControl("ddl_Search_Address") as DropDownList;
        DropDownList ddl_Search_AddressNumber = UC_SearchByAddress1.FindControl("ddl_Search_AddressNumber") as DropDownList;
        Panel pnl_Search_Base = UC_SearchByAddress1.FindControl("pnl_Search_Base") as Panel;

        try { ddl_Search_State.SelectedIndex = 0; }
        catch { }
        try { ddl_Search_City.Items.Clear(); }
        catch { }
        try { ddl_Search_AddressType.Items.Clear(); }
        catch { }
        try { ddl_Search_Address.Items.Clear(); }
        catch { }
        try { ddl_Search_AddressNumber.Items.Clear(); }
        catch { }
        try { pnl_Search_Base.Visible = false; }
        catch { }
    }

    #endregion

    #region Event Handling - Results

    protected void gvProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProperty.PageIndex = e.NewPageIndex;
        HandleQueryType();
    }

    protected void gvProperty_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderProperties"] = e.SortExpression;
        HandleQueryType();
    }

    protected void gvProperty_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvProperty.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        fvProperty.ChangeMode(FormViewMode.ReadOnly);
        BindProperty(Convert.ToInt32(gvProperty.SelectedDataKey.Value));
        if (gvProperty.SelectedIndex != -1) ViewState["SelectedValueProperties"] = gvProperty.SelectedValue.ToString();
    }

    #endregion

    #region Event Handling - Property

    protected void lbProperty_Close_Click(object sender, EventArgs e)
    {
        ClearProperty();
        gvProperty.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        HandleQueryType();
        upProperty.Update();
        upPropertySearch.Update();
    }

    protected void lbProperty_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            ChangeIndex2Zero4SearchDDLs(true);
            ClearProperties();
            fvProperty.ChangeMode(FormViewMode.Insert);
            Session["searchTypeProperties"] = "";
            BindProperty(-1);
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        
        WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(fvProperty, "ddlZip", null, ddl.SelectedValue);

        if (fvProperty.CurrentMode == FormViewMode.Edit)
        {
            Label lblCity = fvProperty.FindControl("lblCity") as Label;
            DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
            DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
            DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
            DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;
            if (ddl.SelectedValue == "NY")
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCountyNY, false, null);
                ddlTownshipNY.Items.Clear();
                ddlBasin.Items.Clear();
                ddlSubbasin.Items.Clear();
            }
            else
            {
                ddlCountyNY.Items.Clear();
                ddlTownshipNY.Items.Clear();
                ddlBasin.Items.Clear();
                ddlSubbasin.Items.Clear();
            }

            if (lblCity != null) lblCity.Text = "";
        }
    }

    protected void ddlZip_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        DropDownList ddlState = fvProperty.FindControl("ddlState") as DropDownList;
        DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;
        TextBox tbCity = fvProperty.FindControl("tbCity") as TextBox;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.zipcodes.Where(w => w.pk_zipcode == ddl.SelectedValue) select b).Single();
            if (ddlState.SelectedValue.ToUpper() == "NY")
            {
                try
                {
                    var c = wDataContext.list_countyNies.Where(w => w.county == a.county).Select(s => s.pk_list_countyNY);
                    if (c.Count() == 1)
                    {
                        WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCountyNY, false, c.Single());
                        WACGlobal_Methods.PopulateControl_DatabaseLists_TownshipNY_DDL(ddlTownshipNY, c.Single(), null);
                        WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(ddlBasin, null, null, null, c.Single(), null, true);
                        ddlSubbasin.Items.Clear();
                    }
                    else
                    {
                        WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCountyNY, false, null);
                        ddlTownshipNY.Items.Clear();
                        ddlBasin.Items.Clear();
                        ddlSubbasin.Items.Clear();
                    }
                }
                catch
                {
                    ddlCountyNY.SelectedIndex = 0;
                    ddlTownshipNY.Items.Clear();
                    ddlBasin.Items.Clear();
                    ddlSubbasin.Items.Clear();
                    tbCity.Text = "";
                }
            }
            else
            {
                ddlCountyNY.Items.Clear();
                ddlTownshipNY.Items.Clear();
                ddlBasin.Items.Clear();
                ddlSubbasin.Items.Clear();
            }
            tbCity.Text = a.city;
        }
    }

    protected void fvProperty_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvProperty.ChangeMode(e.NewMode);
            BindProperty(Convert.ToInt32(fvProperty.DataKey.Value));
        }
    }

    protected void fvProperty_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

        TextBox tbAddress1 = fvProperty.FindControl("tbAddress1") as TextBox;
        TextBox tbAddress2 = fvProperty.FindControl("tbAddress2") as TextBox;      
        DropDownList ddlState = fvProperty.FindControl("ddlState") as DropDownList;
        DropDownList ddlZip = fvProperty.FindControl("ddlZip") as DropDownList;
        TextBox tbCity = fvProperty.FindControl("tbCity") as TextBox;
        TextBox tbZip4 = fvProperty.FindControl("tbZip4") as TextBox;
        DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.properties.Where(w => w.pk_property == Convert.ToInt32(fvProperty.DataKey.Value)).Select(s => s).Single();

                a.address = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress1.Text, 100).Trim();
                a.address2 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress2.Text, 48).Trim();
                a.city = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCity.Text, 48).Trim();
                if (!string.IsNullOrEmpty(ddlState.SelectedValue)) a.state = ddlState.SelectedValue;
                else a.state = null;
                if (!string.IsNullOrEmpty(ddlZip.SelectedValue)) a.fk_zipcode = ddlZip.SelectedValue;
                else a.fk_zipcode = null;
                a.zip4 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbZip4.Text, 4).Trim();
                if (!string.IsNullOrEmpty(ddlCountyNY.SelectedValue)) a.fk_list_countyNY = Convert.ToInt32(ddlCountyNY.SelectedValue);
                else a.fk_list_countyNY = null;
                if (!string.IsNullOrEmpty(ddlTownshipNY.SelectedValue)) a.fk_list_townshipNY = Convert.ToInt32(ddlTownshipNY.SelectedValue);
                else a.fk_list_townshipNY = null;
                if (!string.IsNullOrEmpty(ddlBasin.SelectedValue)) a.fk_basin_code = ddlBasin.SelectedValue;
                else a.fk_basin_code = null;
                if (!string.IsNullOrEmpty(ddlSubbasin.SelectedValue)) a.fk_subbasin_code = ddlSubbasin.SelectedValue;
                else a.fk_subbasin_code = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();
                wDataContext.SubmitChanges();
                fvProperty.ChangeMode(FormViewMode.ReadOnly);
                BindProperty(Convert.ToInt32(fvProperty.DataKey.Value));
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }   
    }

    protected void fvProperty_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        TextBox tbAddress1 = fvProperty.FindControl("tbAddress1") as TextBox;
        TextBox tbAddress2 = fvProperty.FindControl("tbAddress2") as TextBox;
        DropDownList ddlState = fvProperty.FindControl("ddlState") as DropDownList;
        DropDownList ddlZip = fvProperty.FindControl("ddlZip") as DropDownList;
        TextBox tbCity = fvProperty.FindControl("tbCity") as TextBox;
        TextBox tbZip4 = fvProperty.FindControl("tbZip4") as TextBox;
        DropDownList ddlCountyNY = fvProperty.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = fvProperty.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = fvProperty.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = fvProperty.FindControl("ddlSubbasin") as DropDownList;


        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
               
                string addr1 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress1.Text, 100).Trim();
                string addr2 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAddress2.Text, 48).Trim();
                string city = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCity.Text, 48).Trim();
                string state = string.IsNullOrEmpty(ddlState.SelectedValue) ? null : ddlState.SelectedValue;
                string zipCode = string.IsNullOrEmpty(ddlZip.SelectedValue) ? null : ddlZip.SelectedValue;
                string zip4 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbZip4.Text, 4).Trim();
                int? county = null;
                if (!string.IsNullOrEmpty(ddlCountyNY.SelectedValue))
                    county = Convert.ToInt32(ddlCountyNY.SelectedValue);
                int? township = null;
                if (!string.IsNullOrEmpty(ddlTownshipNY.SelectedValue))
                    township = Convert.ToInt32(ddlTownshipNY.SelectedValue);
                string basinCode = string.IsNullOrEmpty(ddlBasin.SelectedValue) ? null : ddlBasin.SelectedValue;
                string subBasinCode = string.IsNullOrEmpty(ddlSubbasin.SelectedValue) ? null : ddlSubbasin.SelectedValue;
                iCode = wDataContext.property_add_express(addr1,addr2,city,state,zipCode,zip4,county,township,basinCode,subBasinCode, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fvProperty.ChangeMode(FormViewMode.ReadOnly);
                    BindProperty(Convert.ToInt32(i));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);

            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvProperty_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            string sDeleteCheck = WACGlobal_Methods.DatabaseFunction_Global_CheckForeignKeyAssignment(Convert.ToInt32(fvProperty.DataKey.Value), "PROPERTY");

            if (string.IsNullOrEmpty(sDeleteCheck))
            {
                int iCode = 0;
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.property_delete(Convert.ToInt32(fvProperty.DataKey.Value), Session["userName"].ToString());
                        if (iCode == 0) lbProperty_Close_Click(null, null);
                        else WACAlert.Show("Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
                }
            }
            else WACAlert.Show(sDeleteCheck, 0);
        }
    }

    #endregion

    #region Data Binding - Property

    private void BindProperties()
    {
        try
        {
            gvProperty.DataKeyNames = new string[] { "pk_property" };
            gvProperty.DataSource = Session["resultsProperties"];
            gvProperty.DataBind();
        }
        catch { }
        if (ViewState["SelectedValueProperties"] != null)
        {
            string sSelectedValue = (string)ViewState["SelectedValueProperties"];
            foreach (GridViewRow gvr in gvProperty.Rows)
            {
                string sKeyValue = gvProperty.DataKeys[gvr.RowIndex].Value.ToString();
                if (sKeyValue == sSelectedValue)
                {
                    gvProperty.SelectedIndex = gvr.RowIndex;
                    return;
                }
                else gvProperty.SelectedIndex = -1;
            }
        }
        lblCount.Text = "Records: " + Session["countProperties"];

        if (gvProperty.Rows.Count == 1)
        {
            gvProperty.SelectedIndex = 0;
            gvProperty.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
            fvProperty.ChangeMode(FormViewMode.ReadOnly);
            BindProperty(Convert.ToInt32(gvProperty.SelectedDataKey.Value));
            if (gvProperty.SelectedIndex != -1) ViewState["SelectedValueProperties"] = gvProperty.SelectedValue.ToString();
        }
    }

    private void BindProperty(int i)
    {

            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {

                var a = from b in wDataContext.properties.Where(w => w.pk_property == i) select b;
                fvProperty.DataKeyNames = new string[] { "pk_property" };
                fvProperty.DataSource = a;
                fvProperty.DataBind();
                if (fvProperty.CurrentMode == FormViewMode.ReadOnly)
                {
                    if (i > 0)
                    {
                        WACUT_Associations assoc = fvProperty.FindControl("WACUT_Associations") as WACUT_Associations;
                        List<WACParameter> parms = new List<WACParameter>();
                        parms.Add(new WACParameter("pk_property", i, WACParameter.ParameterType.SelectedKey));
                        parms.Add(new WACParameter("pk_property", i, WACParameter.ParameterType.PrimaryKey));
                        assoc.InitControl(parms);
                        parms = null;
                    }
                }

                if (fvProperty.CurrentMode == FormViewMode.Insert)
                {
                    WACGlobal_Methods.PopulateControl_DatabaseLists_StatesUS_DDL(fvProperty, "ddlState", "NY");
                    WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(fvProperty, "ddlZip", null, "NY");
                }

                if (fvProperty.CurrentMode == FormViewMode.Edit)
                {
                    WACGlobal_Methods.PopulateControl_DatabaseLists_StatesUS_DDL(fvProperty, "ddlState", a.Single().state);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(fvProperty, "ddlZip", a.Single().fk_zipcode, a.Single().state);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(fvProperty.FindControl("ddlCounty") as DropDownList, false, a.Single().fk_list_countyNY);
                    if (a.Single().fk_list_countyNY != null)
                    {
                        WACGlobal_Methods.PopulateControl_DatabaseLists_TownshipNY_DDL(fvProperty.FindControl("ddlTownshipNY") as DropDownList, a.Single().fk_list_countyNY, a.Single().fk_list_townshipNY);
                        WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(fvProperty.FindControl("ddlBasin") as DropDownList, null, null, null, a.Single().fk_list_countyNY, a.Single().fk_basin_code, true);
                        if (!string.IsNullOrEmpty(a.Single().fk_basin_code))
                            WACGlobal_Methods.PopulateControl_DatabaseLists_Subbasin_DDL(fvProperty.FindControl("ddlSubbasin") as DropDownList, a.Single().fk_basin_code, a.Single().fk_subbasin_code);
                    }
                }
            }
            
        upProperty.Update();
    }

    private void ClearProperties()
    {
        lblCount.Text = "";
        gvProperty.DataSource = null;
        gvProperty.DataBind();
    }

    private void ClearProperty()
    {
        fvProperty.ChangeMode(FormViewMode.ReadOnly);
        fvProperty.DataSource = "";
        fvProperty.DataBind();
    }

    #endregion



    public void Register(Control c)
    {
        throw new NotImplementedException();
    }

    


    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCounty = (DropDownList)sender;
        DropDownList ddlTownshipNY = (DropDownList)fvProperty.FindControl("ddlTownshipNY");
        DropDownList ddlBasin = (DropDownList)fvProperty.FindControl("ddlbasin");
        DropDownList ddlSubbasin = (DropDownList)fvProperty.FindControl("ddlSubbasin");

        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
        {
            WACGlobal_Methods.PopulateControl_DatabaseLists_TownshipNY_DDL(ddlTownshipNY, Convert.ToInt32(ddlCounty.SelectedValue), null);
            if (_CountyControlsBasin)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(ddlBasin, null, null, null, Convert.ToInt32(ddlCounty.SelectedValue), null, true);
                if (_BoolShowSubbasin) ddlSubbasin.Items.Clear();
            }
        }
        else
        {
            if (_BoolShowTownship) ddlTownshipNY.Items.Clear();
            if (_CountyControlsBasin)
            {
                ddlBasin.Items.Clear();
                if (_BoolShowSubbasin) ddlSubbasin.Items.Clear();
            }
        }
    }

    protected void ddlBasin_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlBasin = (DropDownList)sender;
        DropDownList ddlSubbasin = (DropDownList)fvProperty.FindControl("ddlSubbasin");
        if (_BoolShowSubbasin)
        {
            if (!string.IsNullOrEmpty(ddlBasin.SelectedValue))
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Subbasin_DDL(ddlSubbasin, ddlBasin.SelectedValue, null);
            }
            else ddlSubbasin.Items.Clear();
        }
    }

 
}
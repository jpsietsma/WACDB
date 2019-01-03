using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACVenues : System.Web.UI.Page
{
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["searchTypeVenues"] = "";
            Session["searchKeyVenues"] = "";
            Session["resultsVenues"] = "";
            Session["orderVenues"] = "";
            Session["countVenues"] = "";

            PopulateControl_Venue_Venue_DDL();
            PopulateControl_Venue_Property_ZipCode_DDL();
            PopulateControl_Venue_Property_NYTownship_DDL();
            PopulateControl_Venue_Property_NYCounty_DDL();

            hlVenue_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Global Data Venue.pdf";
            hlVenue_Help.ImageUrl = "~/images/help_24.png";

            string sQS_PK = Request.QueryString["pk"];
            if (!string.IsNullOrEmpty(sQS_PK)) BindVenue(Convert.ToInt32(sQS_PK));
        }
    }

    private void PopulateControl_Venue_Venue_DDL()
    {
        ddlVenue_Search_Venue.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.eventVenues.OrderBy(o => o.location) select new { b.pk_eventVenue, b.location };
            ddlVenue_Search_Venue.DataTextField = "location";
            ddlVenue_Search_Venue.DataValueField = "pk_eventVenue";
            ddlVenue_Search_Venue.DataSource = a;
            ddlVenue_Search_Venue.DataBind();
            ddlVenue_Search_Venue.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateControl_Venue_Property_ZipCode_DDL()
    {
        ddlVenue_Search_ZipCode.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.eventVenues.Where(w => w.property.fk_zipcode != null)
                    group b by b.property.fk_zipcode into g
                    orderby g.Key
                    select g.Key;
            try
            {
                ddlVenue_Search_ZipCode.DataSource = a;
                ddlVenue_Search_ZipCode.DataBind();
                ddlVenue_Search_ZipCode.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
            catch { }
        }
    }

    private void PopulateControl_Venue_Property_NYTownship_DDL()
    {
        ddlVenue_Search_NYTownship.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.eventVenues.Where(w => w.property.fk_list_townshipNY != null)
                    group b by b.property.list_townshipNY.township into g
                    orderby g.Key
                    select g.Key;
            try
            {
                ddlVenue_Search_NYTownship.DataSource = a;
                ddlVenue_Search_NYTownship.DataBind();
                ddlVenue_Search_NYTownship.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
            catch { }
        }
    }

    private void PopulateControl_Venue_Property_NYCounty_DDL()
    {
        ddlVenue_Search_NYCounty.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.eventVenues.Where(w => w.property.fk_list_countyNY != null)
                    group b by b.property.list_countyNY.county into g
                    orderby g.Key
                    select g.Key;
            try
            {
                ddlVenue_Search_NYCounty.DataSource = a;
                ddlVenue_Search_NYCounty.DataBind();
                ddlVenue_Search_NYCounty.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
            catch { }
        }
    }

    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    public void Property_ViewEditInsertWindow(object oPK_Property)
    {
        try
        {
            UC_Express_Property.LoadFormView_Property(Convert.ToInt32(oPK_Property));
        }
        catch { WACAlert.Show("Could not open Property Window.", 0); }
    }

    public void Invoked_Method_SectionPage_RebindRecord()
    {
        BindVenue(Convert.ToInt32(fvVenue.DataKey.Value));
        upVenue.Update();
        upVenueSearch.Update();
    }

    #endregion

    #region Event Handling - Search

    public void HandleQueryType()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (Session["searchTypeVenues"].ToString())
            {
                case "A":
                    var aA = from b in wDataContext.eventVenues.OrderBy(o => o.location) select b;
                    Session["countVenues"] = aA.Count();
                    Session["resultsVenues"] = aA;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aA.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "B":
                    var aB = from b in wDataContext.eventVenues.Where(w => w.location.StartsWith(Session["searchKeyVenues"].ToString())).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aB.Count();
                    Session["resultsVenues"] = aB;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aB.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "C":
                    var aC = from b in wDataContext.eventVenues.Where(w => w.pk_eventVenue == Convert.ToInt32(Session["searchKeyVenues"])).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aC.Count();
                    Session["resultsVenues"] = aC;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aC.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "D":
                    var aD = from b in wDataContext.eventVenues.Where(w => w.property.fk_zipcode == Session["searchKeyVenues"].ToString()).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aD.Count();
                    Session["resultsVenues"] = aD;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aD.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "E":
                    var aE = from b in wDataContext.eventVenues.Where(w => w.property.list_townshipNY.township == Session["searchKeyVenues"].ToString()).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aE.Count();
                    Session["resultsVenues"] = aE;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aE.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "F":
                    var aF = from b in wDataContext.eventVenues.Where(w => w.property.list_countyNY.county == Session["searchKeyVenues"].ToString()).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aF.Count();
                    Session["resultsVenues"] = aF;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aF.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "G":
                    var aG = from b in wDataContext.eventVenues.Where(w => w.property.state == Session["searchKeyVenues"].ToString()).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aG.Count();
                    Session["resultsVenues"] = aG;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aG.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "H":
                    List<string> listStateCity = (List<string>)Session["searchKeyVenues"];
                    var aH = from b in wDataContext.eventVenues.Where(w => w.property.state == listStateCity[0] && w.property.city == listStateCity[1]).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aH.Count();
                    Session["resultsVenues"] = aH;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aH.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "I":
                    List<string> listStateCityAddressType = (List<string>)Session["searchKeyVenues"];
                    var aI = from b in wDataContext.eventVenues.Where(w => w.property.state == listStateCityAddressType[0] && w.property.city == listStateCityAddressType[1] && w.property.fk_addressType_code == listStateCityAddressType[2]).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aI.Count();
                    Session["resultsVenues"] = aI;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aI.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "J":
                    List<string> listStateCityAddress = (List<string>)Session["searchKeyVenues"];
                    var aJ = from b in wDataContext.eventVenues.Where(w => w.property.state == listStateCityAddress[0] && w.property.city == listStateCityAddress[1] && w.property.address_base == listStateCityAddress[2]).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aJ.Count();
                    Session["resultsVenues"] = aJ;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aJ.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "K":
                    List<string> listStateCityAddressNumber = (List<string>)Session["searchKeyVenues"];
                    var aK = from b in wDataContext.eventVenues.Where(w => w.property.state == listStateCityAddressNumber[0] && w.property.city == listStateCityAddressNumber[1] && w.property.address_base == listStateCityAddressNumber[2] && w.property.nbr == listStateCityAddressNumber[3]).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aK.Count();
                    Session["resultsVenues"] = aK;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aK.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                case "L":
                    List<string> listStateCityNonRoadNumber = (List<string>)Session["searchKeyVenues"];
                    var aL = from b in wDataContext.eventVenues.Where(w => w.property.state == listStateCityNonRoadNumber[0] && w.property.city == listStateCityNonRoadNumber[1] && w.property.fk_addressType_code == listStateCityNonRoadNumber[2] && w.property.nbr == listStateCityNonRoadNumber[3]).OrderBy(o => o.location) select b;
                    Session["countVenues"] = aL.Count();
                    Session["resultsVenues"] = aL;
                    if (!string.IsNullOrEmpty(Session["orderVenues"].ToString())) Session["resultsVenues"] = aL.OrderBy(Session["orderVenues"].ToString(), null);
                    break;
                default:
                    Session["countVenues"] = 0;
                    Session["resultsVenues"] = "";
                    Session["orderVenues"] = "";
                    break;
            }
            BindVenues();
        }
    }

    public void ChangeIndex2Zero4SearchDDLs()
    {
        gvVenue.SelectedIndex = -1;
        ViewState["SelectedValueVenues"] = null;
        ClearVenue();
        try { ddlVenue_Search_Venue.SelectedIndex = 0; }
        catch { }
        try { ddlVenue_Search_ZipCode.SelectedIndex = 0; }
        catch { }
        try { ddlVenue_Search_NYTownship.SelectedIndex = 0; }
        catch { }
        try { ddlVenue_Search_NYCounty.SelectedIndex = 0; }
        catch { }
    }

    protected void lbVenue_Search_ReloadReset_Click(object sender, EventArgs e)
    {
        ChangeIndex2Zero4SearchDDLs();
        HandleAddressSearchDDLs();
        ClearVenues();
    }

    protected void lbVenue_Search_All_Click(object sender, EventArgs e)
    {
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "A";
        Session["searchKeyVenues"] = "";
        ChangeIndex2Zero4SearchDDLs();
        HandleAddressSearchDDLs();
        HandleQueryType();
    }

    protected void lbSearchName_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "B";
        Session["searchKeyVenues"] = lbtn.CommandArgument;
        ChangeIndex2Zero4SearchDDLs();
        HandleAddressSearchDDLs();
        HandleQueryType();
    }

    protected void ddlVenue_Search_Venue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlVenue_Search_Venue.SelectedValue))
        {
            Session["orderVenues"] = "";
            Session["searchTypeVenues"] = "C";
            Session["searchKeyVenues"] = ddlVenue_Search_Venue.SelectedValue;
            ChangeIndex2Zero4SearchDDLs();
            HandleAddressSearchDDLs();
            ddlVenue_Search_Venue.SelectedValue = Session["searchKeyVenues"].ToString();
            HandleQueryType();
        }
    }

    protected void ddlVenue_Search_ZipCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlVenue_Search_ZipCode.SelectedValue))
        {
            Session["orderVenues"] = "";
            Session["searchTypeVenues"] = "D";
            Session["searchKeyVenues"] = ddlVenue_Search_ZipCode.SelectedValue;
            ChangeIndex2Zero4SearchDDLs();
            HandleAddressSearchDDLs();
            ddlVenue_Search_ZipCode.SelectedValue = Session["searchKeyVenues"].ToString();
            HandleQueryType();
        }
    }
    protected void ddlVenue_Search_NYTownship_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlVenue_Search_NYTownship.SelectedValue))
        {
            Session["orderVenues"] = "";
            Session["searchTypeVenues"] = "E";
            Session["searchKeyVenues"] = ddlVenue_Search_NYTownship.SelectedValue;
            ChangeIndex2Zero4SearchDDLs();
            HandleAddressSearchDDLs();
            ddlVenue_Search_NYTownship.SelectedValue = Session["searchKeyVenues"].ToString();
            HandleQueryType();
        }
    }
    protected void ddlVenue_Search_NYCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlVenue_Search_NYCounty.SelectedValue))
        {
            Session["orderVenues"] = "";
            Session["searchTypeVenues"] = "F";
            Session["searchKeyVenues"] = ddlVenue_Search_NYCounty.SelectedValue;
            ChangeIndex2Zero4SearchDDLs();
            HandleAddressSearchDDLs();
            ddlVenue_Search_NYCounty.SelectedValue = Session["searchKeyVenues"].ToString();
            HandleQueryType();
        }
    }

    #endregion

    #region Event Handling - Search Address

    public void UpdateUpdatePanel()
    {
        upVenue.Update();
    }

    public void DefineSpecificValuesForState(string sValue)
    {
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "G";
        Session["searchKeyVenues"] = sValue;
    }

    public void DefineSpecificValuesForCity(List<string> listProperty)
    {
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "H";
        Session["searchKeyVenues"] = listProperty;
    }

    public void DefineSpecificValuesForAddressType(List<string> listProperty)
    {
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "I";
        Session["searchKeyVenues"] = listProperty;
    }

    public void DefineSpecificValuesForAddress(List<string> listProperty)
    {
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "J";
        Session["searchKeyVenues"] = listProperty;
    }

    public void DefineSpecificValuesForAddressNumber(List<string> listProperty)
    {
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "K";
        Session["searchKeyVenues"] = listProperty;
    }

    public void DefineSpecificValuesForPOB(List<string> listProperty)
    {
        Session["orderVenues"] = "";
        Session["searchTypeVenues"] = "L";
        Session["searchKeyVenues"] = listProperty;
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

    protected void gvVenue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVenue.PageIndex = e.NewPageIndex;
        HandleQueryType();
    }

    protected void gvVenue_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderVenues"] = e.SortExpression;
        HandleQueryType();
    }

    protected void gvVenue_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvVenue.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        fvVenue.ChangeMode(FormViewMode.ReadOnly);
        BindVenue(Convert.ToInt32(gvVenue.SelectedDataKey.Value));
        if (gvVenue.SelectedIndex != -1) ViewState["SelectedValueVenues"] = gvVenue.SelectedValue.ToString();
    }

    #endregion

    #region Event Handling - Venue

    protected void lbVenue_Close_Click(object sender, EventArgs e)
    {
        ClearVenue();
        gvVenue.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        HandleQueryType();
        upVenue.Update();
        upVenueSearch.Update();
    }

    protected void lbVenue_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            ChangeIndex2Zero4SearchDDLs();
            ClearVenues();

            fvVenue.ChangeMode(FormViewMode.Insert);
            Session["searchTypeVenues"] = "";
            BindVenue(-1);
        }
    }

    protected void fvVenue_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvVenue.ChangeMode(e.NewMode);
            BindVenue(Convert.ToInt32(fvVenue.DataKey.Value));
        }
    }

    protected void fvVenue_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbLocation = fvVenue.FindControl("tbLocation") as TextBox;
        TextBox tbNote = fvVenue.FindControl("tbNote") as TextBox;
        DropDownList ddlPhone = fvVenue.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlNumber") as DropDownList;
        DropDownList ddlFax = fvVenue.FindControl("UC_Communication_EditInsert_Fax").FindControl("ddlNumber") as DropDownList;
        TextBox tbEmail = fvVenue.FindControl("tbEmail") as TextBox;
        HiddenField hfPropertyPK = fvVenue.FindControl("UC_Property_EditInsert1").FindControl("hfPropertyPK") as HiddenField;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.eventVenues.Where(w => w.pk_eventVenue == Convert.ToInt32(fvVenue.DataKey.Value)) select b).Single();

            if (!string.IsNullOrEmpty(tbLocation.Text)) a.location = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLocation.Text, 48).Trim();
            else sb.Append("Location was not updated. This is a required field. ");

            if (!string.IsNullOrEmpty(ddlPhone.SelectedValue)) a.fk_communication_phone = Convert.ToInt32(ddlPhone.SelectedValue);
            else a.fk_communication_phone = null;

            if (!string.IsNullOrEmpty(ddlFax.SelectedValue)) a.fk_communication_fax = Convert.ToInt32(ddlFax.SelectedValue);
            else a.fk_communication_fax = null;
            
            a.email = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEmail.Text, 48).Trim();
            
            a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255).Trim();

            if (!string.IsNullOrEmpty(hfPropertyPK.Value)) a.fk_property = Convert.ToInt32(hfPropertyPK.Value);
            else a.fk_property = null;

            a.modified = DateTime.Now;
            a.modified_by = Session["userName"].ToString();

            try
            {
                wDataContext.SubmitChanges();
                fvVenue.ChangeMode(FormViewMode.ReadOnly);
                BindVenue(Convert.ToInt32(fvVenue.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvVenue_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        TextBox tbLocation = fvVenue.FindControl("tbLocation") as TextBox;
        DropDownList ddlPhone = fvVenue.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlNumber") as DropDownList;
        DropDownList ddlFax = fvVenue.FindControl("UC_Communication_EditInsert_Fax").FindControl("ddlNumber") as DropDownList;
        TextBox tbEmail = fvVenue.FindControl("tbEmail") as TextBox;
        TextBox tbNote = fvVenue.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            
            try
            {
                string sLocation = null;
                if (!string.IsNullOrEmpty(tbLocation.Text)) sLocation = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLocation.Text, 48).Trim();
                else sb.Append("Location is required. ");

                int? iPhone = null;
                if (!string.IsNullOrEmpty(ddlPhone.SelectedValue)) iPhone = Convert.ToInt32(ddlPhone.SelectedValue);
                int? iFax = null;
                if (!string.IsNullOrEmpty(ddlFax.SelectedValue)) iFax = Convert.ToInt32(ddlFax.SelectedValue);
                string sEmail = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbEmail.Text, 48).Trim();
                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255).Trim();

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.eventVenue_add(sLocation, iPhone, iFax, sEmail, sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvVenue.ChangeMode(FormViewMode.ReadOnly);
                        BindVenue(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvVenue_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.eventVenue_delete(Convert.ToInt32(fvVenue.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbVenue_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Venue - Event Venue Type

    protected void ddlVenue_EventVenueType_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            DropDownList ddl = (DropDownList)sender;
            if (!string.IsNullOrEmpty(ddl.SelectedValue))
            {
                int? i = null;
                int iCode = 0;
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        iCode = wDataContext.eventVenueType_add(Convert.ToInt32(fvVenue.DataKey.Value), ddl.SelectedValue, ref i);
                        if (iCode == 0)
                        {
                            BindVenue(Convert.ToInt32(fvVenue.DataKey.Value));
                        }
                        else WACAlert.Show("Error Returned from Database.", iCode);
                    }
                    catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
                }
            }
        }
    }

    protected void lbVenue_EventVenueType_Delete_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            LinkButton lb = (LinkButton)sender;
            int i = Convert.ToInt32(lb.CommandArgument);
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.eventVenueType_delete(i);
                    if (iCode == 0)
                    {
                        BindVenue(Convert.ToInt32(fvVenue.DataKey.Value));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Data Binding

    private void BindVenues()
    {
        try
        {
            gvVenue.DataKeyNames = new string[] { "pk_eventVenue" };
            gvVenue.DataSource = Session["resultsVenues"];
            gvVenue.DataBind();
        }
        catch { }
        if (ViewState["SelectedValueVenues"] != null)
        {
            string sSelectedValue = (string)ViewState["SelectedValueVenues"];
            foreach (GridViewRow gvr in gvVenue.Rows)
            {
                string sKeyValue = gvVenue.DataKeys[gvr.RowIndex].Value.ToString();
                if (sKeyValue == sSelectedValue)
                {
                    gvVenue.SelectedIndex = gvr.RowIndex;
                    return;
                }
                else gvVenue.SelectedIndex = -1;
            }
        }
        try 
        {
            if (Convert.ToInt32(Session["countVenues"]) > 0) lblCount.Text = "Records: " + Session["countVenues"];
            else lblCount.Text = "Records: 0";
        }
        catch { lblCount.Text = ""; }
    }

    private void BindVenue(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.eventVenues.Where(w => w.pk_eventVenue == i)
                    select b;
            fvVenue.DataKeyNames = new string[] { "pk_eventVenue" };
            fvVenue.DataSource = a;
            fvVenue.DataBind();

            if (fvVenue.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_ByParticipantTypeCollection_DDL(fvVenue, "ddlVenue_EventVenueType_Add", new string[] { "M", "F", "A", "E" }, "");
            }

            if (fvVenue.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlAreaCode") as DropDownList, null);
                WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Fax").FindControl("ddlAreaCode") as DropDownList, null);
            }

            if (fvVenue.CurrentMode == FormViewMode.Edit)
            {
                if (a.Single().communication1 != null)
                {
                    WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlAreaCode") as DropDownList, a.Single().communication1.areacode);
                    WACGlobal_Methods.PopulateControl_Communication_PhoneNumbersByAreaCode_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlNumber") as DropDownList, a.Single().fk_communication_phone, a.Single().communication1.areacode);
                }
                else WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlAreaCode") as DropDownList, null);

                if (a.Single().communication != null)
                {
                    WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Fax").FindControl("ddlAreaCode") as DropDownList, a.Single().communication.areacode);
                    WACGlobal_Methods.PopulateControl_Communication_PhoneNumbersByAreaCode_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Fax").FindControl("ddlNumber") as DropDownList, a.Single().fk_communication_fax, a.Single().communication.areacode);
                }
                else WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvVenue.FindControl("UC_Communication_EditInsert_Fax").FindControl("ddlAreaCode") as DropDownList, null);

                WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvVenue.FindControl("UC_Property_EditInsert1") as UserControl, a.Single().property);
            }
        }
    }

    private void ClearVenues()
    {
        lblCount.Text = "";
        gvVenue.DataSource = null;
        gvVenue.DataBind();
    }

    private void ClearVenue()
    {
        fvVenue.ChangeMode(FormViewMode.ReadOnly);
        fvVenue.DataSource = "";
        fvVenue.DataBind();
    }

    #endregion
}
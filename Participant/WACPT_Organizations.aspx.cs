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
using WAC_Event;
using WAC_Containers;
using WAC_Exceptions;

public partial class Participant_WACPT_Organizations : WACPage, IWACContainer
{
    public override void OpenDefaultDataView(List<WACParameter> parms)
    {
        BindOrganization(WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms,WACParameter.ParameterType.PrimaryKey)));
    }

    public override string ID { get { return "WACPT_OrganizationPage"; } }
   
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.Register(this);
    }

    
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["searchTypeOrganizations"] = "";
            Session["searchKeyOrganizations"] = "";
            Session["resultsOrganizations"] = "";
            Session["orderOrganizations"] = "";
            Session["countOrganizations"] = "";

            hlOrganization_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Global Data Organization.pdf";
            hlOrganization_Help.ImageUrl = "~/images/help_24.png";

            HandleRedirectFromAnotherPage(Request);
        }
    }

    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    public void Participant_ViewEditInsertWindow(object oPK_Participant)
    {
        try
        {
            UC_Express_Participant1.LoadFormView_Participant(Convert.ToInt32(oPK_Participant));
        }
        catch { WACAlert.Show("Could not open Participant Window.", 0); }
    }

    public void Property_ViewEditInsertWindow(object oPK_Property)
    {
        try
        {
            //UC_Express_Property1.LoadFormView_Property(Convert.ToInt32(oPK_Property));
        }
        catch { WACAlert.Show("Could not open Property Window.", 0); }
    }

    public void InvokedMethod_SectionPage_RebindRecord()
    {
        BindOrganization(Convert.ToInt32(fvOrganization.DataKey.Value));
        upOrganization.Update();
        upOrganizationSearch.Update();
    }

    public void InvokedMethod_DropDownListByAlphabet(object oType)
    {
        switch (oType.ToString())
        {
            case "ORGANIZATION_SEARCH": Search_Organization(); break;
        }
    }

    public void InvokedMethod_DropDownListByAlphabet_LinkButtonEvent(object oType, object oValue)
    {
        switch (oType.ToString())
        {
            case "ORGANIZATION_SEARCH_MULTI": Search_Organization_Multi(oValue.ToString()); break;
        }
    }

    #endregion

    #region Event Handling - Search

    public void HandleQueryType()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (Session["searchTypeOrganizations"].ToString())
            {
                case "A":
                    var aA = from b in wDataContext.organizations.OrderBy(o => o.org) select b;
                    Session["countOrganizations"] = aA.Count();
                    Session["resultsOrganizations"] = aA;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aA.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                //case "B":
                //    var aB = from b in wDataContext.organizations.Where(w => w.org.StartsWith(Session["searchKeyOrganizations"].ToString())).OrderBy(o => o.org) select b;
                //    Session["countOrganizations"] = aB.Count();
                //    Session["resultsOrganizations"] = aB;
                //    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aB.OrderBy(Session["orderOrganizations"].ToString(), null);
                //    break;
                case "B":
                    var aB = wDataContext.organizations.OrderBy(o => o.org).Select(s => s);
                    if (Session["searchKeyOrganizations"].ToString() == "1")
                    {
                        List<string> l = WACGlobal_Methods.SpecialDataType_ListString_Alphabet();
                        aB = aB.Where(w => !l.Contains(w.org[0].ToString()));
                    }
                    else aB = aB.Where(w => w.org.StartsWith(Session["searchKeyOrganizations"].ToString()));
                    Session["countOrganizations"] = aB.Count();
                    Session["resultsOrganizations"] = aB;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aB.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                //case "C":
                //    var aC = from b in wDataContext.organizations.Where(w => w.pk_organization == Convert.ToInt32(Session["searchKeyOrganizations"])).OrderBy(o => o.org) select b;
                //    Session["countOrganizations"] = aC.Count();
                //    Session["resultsOrganizations"] = aC;
                //    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aC.OrderBy(Session["orderOrganizations"].ToString(), null);
                //    break;
                case "C":
                    var aC = wDataContext.organizations.Where(w => w.pk_organization == Convert.ToInt32(Session["searchKeyOrganizations"])).OrderBy(o => o.org).Select(s => s);
                    Session["countOrganizations"] = aC.Count();
                    Session["resultsOrganizations"] = aC;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aC.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                case "G":
                    var aG = from b in wDataContext.organizations.Where(w => w.property.state == Session["searchKeyOrganizations"].ToString()).OrderBy(o => o.org) select b;
                    Session["countOrganizations"] = aG.Count();
                    Session["resultsOrganizations"] = aG;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aG.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                case "H":
                    List<string> listStateCity = (List<string>)Session["searchKeyOrganizations"];
                    var aH = from b in wDataContext.organizations.Where(w => w.property.state == listStateCity[0] && w.property.city == listStateCity[1]).OrderBy(o => o.org) select b;
                    Session["countOrganizations"] = aH.Count();
                    Session["resultsOrganizations"] = aH;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aH.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                case "I":
                    List<string> listStateCityAddressType = (List<string>)Session["searchKeyOrganizations"];
                    var aI = from b in wDataContext.organizations.Where(w => w.property.state == listStateCityAddressType[0] && w.property.city == listStateCityAddressType[1] && w.property.fk_addressType_code == listStateCityAddressType[2]).OrderBy(o => o.org) select b;
                    Session["countOrganizations"] = aI.Count();
                    Session["resultsOrganizations"] = aI;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aI.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                case "J":
                    List<string> listStateCityAddress = (List<string>)Session["searchKeyOrganizations"];
                    var aJ = from b in wDataContext.organizations.Where(w => w.property.state == listStateCityAddress[0] && w.property.city == listStateCityAddress[1] && w.property.address_base == listStateCityAddress[2]).OrderBy(o => o.org) select b;
                    Session["countOrganizations"] = aJ.Count();
                    Session["resultsOrganizations"] = aJ;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aJ.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                case "K":
                    List<string> listStateCityAddressNumber = (List<string>)Session["searchKeyOrganizations"];
                    var aK = from b in wDataContext.organizations.Where(w => w.property.state == listStateCityAddressNumber[0] && w.property.city == listStateCityAddressNumber[1] && w.property.address_base == listStateCityAddressNumber[2] && w.property.nbr == listStateCityAddressNumber[3]).OrderBy(o => o.org) select b;
                    Session["countOrganizations"] = aK.Count();
                    Session["resultsOrganizations"] = aK;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aK.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                case "L":
                    List<string> listStateCityNonRoadNumber = (List<string>)Session["searchKeyOrganizations"];
                    var aL = from b in wDataContext.organizations.Where(w => w.property.state == listStateCityNonRoadNumber[0] && w.property.city == listStateCityNonRoadNumber[1] && w.property.fk_addressType_code == listStateCityNonRoadNumber[2] && w.property.nbr == listStateCityNonRoadNumber[3]).OrderBy(o => o.org) select b;
                    Session["countOrganizations"] = aL.Count();
                    Session["resultsOrganizations"] = aL;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aL.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                case "M":
                    string[] input = WACGlobal_Methods.SpecialDataType_StringCollection_Alphabet();
                    var aM = wDataContext.organizations.Where(w => !input.Contains(w.org[0].ToString().ToUpper())).OrderBy(o => o.org).Select(s => s);
                    Session["countOrganizations"] = aM.Count();
                    Session["resultsOrganizations"] = aM;
                    if (!string.IsNullOrEmpty(Session["orderOrganizations"].ToString())) Session["resultsOrganizations"] = aM.OrderBy(Session["orderOrganizations"].ToString(), null);
                    break;
                default:
                    Session["countOrganizations"] = 0;
                    Session["resultsOrganizations"] = "";
                    Session["orderOrganizations"] = "";
                    break;
            }
            BindOrganizations();
        }
    }

    public void ChangeIndex2Zero4SearchDDLs(bool bResetUC)
    {
        gvOrganization.SelectedIndex = -1;
        ViewState["SelectedValueOrganizations"] = null;
        ClearOrganization();
        try { if (bResetUC) UC_DropDownListByAlphabet_Search_Organization.ResetControls(); }
        catch { }
        //try { ddlOrganization_Search_Organization.SelectedIndex = 0; }
        //catch { }
    }

    protected void lbOrganization_Search_ReloadReset_Click(object sender, EventArgs e)
    {
        ChangeIndex2Zero4SearchDDLs(true);
        HandleAddressSearchDDLs();
        ClearOrganizations();
    }

    protected void lbOrganization_Search_All_Click(object sender, EventArgs e)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "A";
        Session["searchKeyOrganizations"] = "";
        ChangeIndex2Zero4SearchDDLs(true);
        HandleAddressSearchDDLs();
        HandleQueryType();
    }

    private void Search_Organization_Multi(string sValue)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "B";
        Session["searchKeyOrganizations"] = sValue;
        ChangeIndex2Zero4SearchDDLs(true);
        HandleAddressSearchDDLs();
        HandleQueryType();
        upOrganization.Update();
    }

    private void Search_Organization()
    {
        DropDownList ddl = UC_DropDownListByAlphabet_Search_Organization.FindControl("ddl") as DropDownList;
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "C";
        Session["searchKeyOrganizations"] = ddl.SelectedValue;
        ChangeIndex2Zero4SearchDDLs(false);
        ddl.SelectedValue = Session["searchKeyOrganizations"].ToString();
        HandleQueryType();
        upOrganization.Update();
    }

    #endregion

    #region Event Handling - Search Address

    public void UpdateUpdatePanel()
    {
        upOrganization.Update();
    }

    public void DefineSpecificValuesForState(string sValue)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "G";
        Session["searchKeyOrganizations"] = sValue;
    }

    public void DefineSpecificValuesForCity(List<string> listProperty)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "H";
        Session["searchKeyOrganizations"] = listProperty;
    }

    public void DefineSpecificValuesForAddressType(List<string> listProperty)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "I";
        Session["searchKeyOrganizations"] = listProperty;
    }

    public void DefineSpecificValuesForAddress(List<string> listProperty)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "J";
        Session["searchKeyOrganizations"] = listProperty;
    }

    public void DefineSpecificValuesForAddressNumber(List<string> listProperty)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "K";
        Session["searchKeyOrganizations"] = listProperty;
    }

    public void DefineSpecificValuesForPOB(List<string> listProperty)
    {
        Session["orderOrganizations"] = "";
        Session["searchTypeOrganizations"] = "L";
        Session["searchKeyOrganizations"] = listProperty;
    }

    private void HandleAddressSearchDDLs()
    {
        DropDownList ddl_Search_State = UC_DropDownListByAlphabet_Search_Organization_Multi.FindControl("ddl_Search_State") as DropDownList;
        DropDownList ddl_Search_City = UC_DropDownListByAlphabet_Search_Organization_Multi.FindControl("ddl_Search_City") as DropDownList;
        DropDownList ddl_Search_AddressType = UC_DropDownListByAlphabet_Search_Organization_Multi.FindControl("ddl_Search_AddressType") as DropDownList;
        DropDownList ddl_Search_Address = UC_DropDownListByAlphabet_Search_Organization_Multi.FindControl("ddl_Search_Address") as DropDownList;
        DropDownList ddl_Search_AddressNumber = UC_DropDownListByAlphabet_Search_Organization_Multi.FindControl("ddl_Search_AddressNumber") as DropDownList;
        Panel pnl_Search_Base = UC_DropDownListByAlphabet_Search_Organization_Multi.FindControl("pnl_Search_Base") as Panel;

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

    protected void gvOrganization_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOrganization.PageIndex = e.NewPageIndex;
        HandleQueryType();
    }

    protected void gvOrganization_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderOrganizations"] = e.SortExpression;
        HandleQueryType();
    }

    protected void gvOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "V", "GlobalData", "GlobalData", "msgView"))
        //{
            gvOrganization.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
            fvOrganization.ChangeMode(FormViewMode.ReadOnly);
            BindOrganization(Convert.ToInt32(gvOrganization.SelectedDataKey.Value));
            if (gvOrganization.SelectedIndex != -1) ViewState["SelectedValueOrganizations"] = gvOrganization.SelectedValue.ToString();
        //}
    }

    #endregion

    #region Event Handling - Participant Property
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
    protected void lbParticipant_Property_Close_Click(object sender, EventArgs e)
    {
        fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
        BindParticipant_Property(-1);
        mpeParticipant_Property.Hide();
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
        //LinkButton lb = (LinkButton)sender;
        //fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
        //BindParticipant_Property(Convert.ToInt32(lb.CommandArgument));
        //mpeParticipant_Property.Show();
        //upParticipant_Property.Update();
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

    }

    protected void fvParticipant_Property_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlAddress = fvParticipant_Property.FindControl("ddlAddress") as DropDownList;
        //CheckBox cbMaster = (CheckBox)fvParticipant_Property.FindControl("cbMaster");

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPK_Property = null;
                if (!string.IsNullOrEmpty(ddlAddress.SelectedValue))
                    iPK_Property = Convert.ToInt32(ddlAddress.SelectedValue);
                else
                    throw new WACEX_GeneralDatabaseException("No Address Selected.", 0);

                iCode = wDataContext.organization_address_add(Convert.ToInt32(fvOrganization.DataKey.Value), iPK_Property, Session["userName"].ToString());
               
                if (iCode == 0)
                {
                    fvParticipant_Property.ChangeMode(FormViewMode.ReadOnly);
                    BindParticipant_Property(Convert.ToInt32(i));
                    mpeParticipant_Property.Hide();
                    upParticipant_Property.Update();
                    Session["searchTypeOrganizations"] = "C";
                    Session["searchKeyOrganizations"] = fvOrganization.DataKey.Value;
                    ViewState["SelectedValueOrganizations"] = null;
                    HandleQueryType();
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

    }

    #endregion

    #region Event Handling - Organization
  

    protected void lbOrganization_Close_Click(object sender, EventArgs e)
    {
        ClearOrganization();
        gvOrganization.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        HandleQueryType();
        upOrganization.Update();
        upOrganizationSearch.Update();
    }

    protected void lbOrganization_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            ChangeIndex2Zero4SearchDDLs(true);
            ClearOrganizations();
            fvOrganization.ChangeMode(FormViewMode.Insert);
            Session["searchTypeOrganizations"] = "";
            BindOrganization(-1);
        }
    }

    protected void fvOrganization_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvOrganization.ChangeMode(e.NewMode);
            BindOrganization(Convert.ToInt32(fvOrganization.DataKey.Value));
        }
    }

    protected void fvOrganization_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbOrg = fvOrganization.FindControl("tbOrg") as TextBox;
        DropDownList ddlContact = fvOrganization.FindControl("UC_DropDownListByAlphabet").FindControl("ddl") as DropDownList;
        //HiddenField hfPropertyPK = fvOrganization.FindControl("UC_Property_EditInsert1").FindControl("hfPropertyPK") as HiddenField;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.organizations.Where(w => w.pk_organization == Convert.ToInt32(fvOrganization.DataKey.Value)) select b).Single();

            a.org = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbOrg.Text, 96).Trim();

            if (ddlContact.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(ddlContact.SelectedValue)) a.fk_participant_contact = Convert.ToInt32(ddlContact.SelectedValue);
                else a.fk_participant_contact = null;
            }

            //if (!string.IsNullOrEmpty(hfPropertyPK.Value)) a.fk_property = Convert.ToInt32(hfPropertyPK.Value);
            //else a.fk_property = null;

            a.modified = DateTime.Now;
            a.modified_by = Session["userName"].ToString();

            try
            {
                wDataContext.SubmitChanges();
                fvOrganization.ChangeMode(FormViewMode.ReadOnly);
                BindOrganization(Convert.ToInt32(fvOrganization.DataKey.Value));
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvOrganization_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            TextBox tbOrg = fvOrganization.FindControl("tbOrg") as TextBox;
            try
            {
                string sOrg = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbOrg.Text, 96).Trim();

                iCode = wDataContext.organization_add(sOrg, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    fvOrganization.ChangeMode(FormViewMode.ReadOnly);
                    BindOrganization(Convert.ToInt32(i));
                }
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvOrganization_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.organization_delete(Convert.ToInt32(fvOrganization.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbOrganization_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Event Handling - Organization Note

    protected void lbOrganization_Note_Close_Click(object sender, EventArgs e)
    {
        fvOrganization_Note.ChangeMode(FormViewMode.ReadOnly);
        BindOrganization_Note(-1);
        mpeOrganization_Note.Hide();
        BindOrganization(Convert.ToInt32(fvOrganization.DataKey.Value));
        upOrganization.Update();
        upOrganizationSearch.Update();
    }

    protected void lbOrganization_Note_Add_Click(object sender, EventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        //{
            fvOrganization_Note.ChangeMode(FormViewMode.Insert);
            BindOrganization_Note(-1);

            mpeOrganization_Note.Show();
            upOrganization_Note.Update();
        //}
    }

    protected void lbOrganization_Note_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvOrganization_Note.ChangeMode(FormViewMode.ReadOnly);
        BindOrganization_Note(Convert.ToInt32(lb.CommandArgument));
        mpeOrganization_Note.Show();
        upOrganization_Note.Update();
    }

    protected void fvOrganization_Note_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit)
        {
            //bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
            bChangeMode = WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "organizationNote", Convert.ToInt32(fvOrganization_Note.DataKey.Value));
        }
        if (bChangeMode)
        {
            fvOrganization_Note.ChangeMode(e.NewMode);
            BindOrganization_Note(Convert.ToInt32(fvOrganization_Note.DataKey.Value));
        }
    }

    protected void fvOrganization_Note_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbNote = fvOrganization_Note.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = (from b in wDataContext.organizationNotes.Where(w => w.pk_organizationNote == Convert.ToInt32(fvOrganization_Note.DataKey.Value))
                         select b).Single();

                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000).Trim();
                else sb.Append("Note was not updated. Note is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvOrganization_Note.ChangeMode(FormViewMode.ReadOnly);
                BindOrganization_Note(Convert.ToInt32(fvOrganization_Note.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvOrganization_Note_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        TextBox tbNote = fvOrganization_Note.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 4000).Trim();
                if (string.IsNullOrEmpty(sNote)) sb.Append("Note is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.organizationNote_add(Convert.ToInt32(fvOrganization.DataKey.Value), sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvOrganization_Note.ChangeMode(FormViewMode.ReadOnly);
                        BindOrganization_Note(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvOrganization_Note_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        //{
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    if (WACGlobal_Methods.Security_UserCanModifyDeleteNote(Session["userName"], "organizationNote", Convert.ToInt32(fvOrganization_Note.DataKey.Value)))
                    {
                        iCode = wDataContext.organizationNote_delete(Convert.ToInt32(fvOrganization_Note.DataKey.Value), Session["userName"].ToString());
                        if (iCode == 0) lbOrganization_Note_Close_Click(null, null);
                        else WACAlert.Show("Error Returned from Database.", iCode);
                    }
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        //}
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

                //WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvParticipant_Property, "ddlMaster", a.Single().master, false);
                //DropDownList ddl = fvParticipant_Property.FindControl("UC_DropDownListByAlphabet_Participant_Property").FindControl("ddl") as DropDownList;
                //Label lblLetter = fvParticipant_Property.FindControl("UC_DropDownListByAlphabet_Participant_Property").FindControl("lblLetter") as Label;
                //string sLetter = null;
                //try { sLetter = a.Single().participant1.lname.Substring(0, 1); }
                //catch { }
                //WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lblLetter, sLetter, "PARTICIPANT", "ALL", a.Single().fk_participant_cc);
            }
        }
    }

    #endregion
    #region Data Binding - Organization

    private void BindOrganizations()
    {
        try
        {
            gvOrganization.DataKeyNames = new string[] { "pk_organization" };
            gvOrganization.DataSource = Session["resultsOrganizations"];
            gvOrganization.DataBind();
        }
        catch { }
        if (ViewState["SelectedValueOrganizations"] != null)
        {
            string sSelectedValue = (string)ViewState["SelectedValueOrganizations"];
            foreach (GridViewRow gvr in gvOrganization.Rows)
            {
                string sKeyValue = gvOrganization.DataKeys[gvr.RowIndex].Value.ToString();
                if (sKeyValue == sSelectedValue)
                {
                    gvOrganization.SelectedIndex = gvr.RowIndex;
                    return;
                }
                else gvOrganization.SelectedIndex = -1;
            }
        }
        try
        {
            if (Convert.ToInt32(Session["countOrganizations"]) > 0) lblCount.Text = "Records: " + Session["countOrganizations"];
            else lblCount.Text = "Records: 0";
        }
        catch { lblCount.Text = ""; }

        if (gvOrganization.Rows.Count == 1)
        {
            gvOrganization.SelectedIndex = 0;
            gvOrganization.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
            fvOrganization.ChangeMode(FormViewMode.ReadOnly);
            BindOrganization(Convert.ToInt32(gvOrganization.SelectedDataKey.Value));
            if (gvOrganization.SelectedIndex != -1) ViewState["SelectedValueOrganizations"] = gvOrganization.SelectedValue.ToString();
        }
    }

    private void BindOrganization(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.organizations.Where(w => w.pk_organization == i).Select(s => s);
            fvOrganization.DataKeyNames = new string[] { "pk_organization" };
            fvOrganization.DataSource = a;
            fvOrganization.DataBind();

            if (fvOrganization.CurrentMode == FormViewMode.ReadOnly)
            {
                // this needs to be replaced when page is converted
                WACUT_Associations assoc = fvOrganization.FindControl("WACUT_Associations") as WACUT_Associations;
                
                List<WACParameter> parms = new List<WACParameter>();
                parms.Add(new WACParameter("pk_participant", i, WACParameter.ParameterType.SelectedKey));
                parms.Add(new WACParameter("pk_participant", i, WACParameter.ParameterType.PrimaryKey));
                assoc.InitControl(parms);
            }
            
            if (fvOrganization.CurrentMode == FormViewMode.Edit)
            {
                DropDownList ddl = fvOrganization.FindControl("UC_DropDownListByAlphabet").FindControl("ddl") as DropDownList;
                Label lbl = fvOrganization.FindControl("UC_DropDownListByAlphabet").FindControl("lblLetter") as Label;
                string contact = string.Empty;
                string sLetter = string.Empty;
                if (a.Any())
                {
                    WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvOrganization.FindControl("UC_Property_EditInsert1") as UserControl, a.Single().property);
                    if (a.Single().fk_participant_contact != null)
                        contact = a.Single().participant.fullname_LF_dnd;
                    sLetter = string.IsNullOrEmpty(contact) ? null : contact[0].ToString();
                    WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lbl, sLetter, "PARTICIPANT", "ALL", a.Single().fk_participant_contact);
                }
                 ;
                
            }
        }
        upOrganization.Update();
    }

    private void ClearOrganizations()
    {
        lblCount.Text = "";
        gvOrganization.DataSource = null;
        gvOrganization.DataBind();
    }

    private void ClearOrganization()
    {
        fvOrganization.ChangeMode(FormViewMode.ReadOnly);
        fvOrganization.DataSource = "";
        fvOrganization.DataBind();
    }

    #endregion

    #region Data Binding - Organization Note

    private void BindOrganization_Note(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            fvOrganization_Note.DataKeyNames = new string[] { "pk_organizationNote" };
            fvOrganization_Note.DataSource = wDataContext.organizationNotes.Where(w => w.pk_organizationNote == i).Select(s => s);
            fvOrganization_Note.DataBind();
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
        return;
    }

    public void UpdatePanelUpdate()
    {
        return;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void InsertedEventHandler(object s, EventArgs e);

public partial class Participant_WACPT_ParticipantCreate : System.Web.UI.UserControl
{

    public event InsertedEventHandler Inserted;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void LoadMe()
    {
        mpeParticipant_add_rs.Show();
        fvParticipantCreate.ChangeMode(FormViewMode.Insert);
        PopulateDDLs();
    }

    private int _pk_participant = 0;
    public int pk_participant
    {
        get
        {
            return _pk_participant;
        }
        set
        {
            _pk_participant = value;
        }
    }


    #region PopulateDDLs

    private void PopulateDDLs()
    {
        WACGlobal_Methods.PopulateControl_DatabaseLists_Prefix_DDL(fvParticipantCreate, "ddlPrefix", null);
        WACGlobal_Methods.PopulateControl_DatabaseLists_Suffix_DDL(fvParticipantCreate.FindControl("ddlSuffix") as DropDownList, null, true);
        //PopulateDDLOrganization();
        WACGlobal_Methods.PopulateControl_DatabaseLists_AddressType_DDL(fvParticipantCreate.FindControl("ddlAddressType") as DropDownList, null, true);
        WACGlobal_Methods.PopulateControl_DatabaseLists_USPS_Abbr_DDL(fvParticipantCreate, "ddlUSPSAbbr", null, true);
        WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationType_DDL(fvParticipantCreate, "ddlCommunicationType", null);
        WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationUsage_DDL(fvParticipantCreate, "ddlCommunicationUsage", null, null);
        WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_DDL(fvParticipantCreate.FindControl("ddlParticipantType") as DropDownList, null, true);
        WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(fvParticipantCreate, "ddlWACRegion", null);
        
    }
    
    private void PopulateDDLOrganization()
    {
        DropDownList ddlOrganization = fvParticipantCreate.FindControl("ddlOrganization") as DropDownList;
        if (ddlOrganization != null)
        {
            WACDataClassesDataContext wDataContext = new WACDataClassesDataContext();
            ddlOrganization.Items.Clear();
            ddlOrganization.DataTextField = "Name";
            ddlOrganization.DataValueField = "Name";
            var a = wDataContext.organizations.Select(s => new {  NAME = s.org}).OrderBy(o => o.NAME);
            ddlOrganization.DataSource = a;
            ddlOrganization.DataBind();
            ddlOrganization.Items.Insert(0, new ListItem("[SELECT]", ""));
        }

    }

    #endregion

    #region EventHandlers

    protected void fvParticipantCreate_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        
    }

    protected void fvParticipantCreate_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        WACDataClassesDataContext wDataContext = new WACDataClassesDataContext();
        string modeParticipant = "Participant";
        string fk_prefix_code = null;
        string fname = null;
        string lname = null;
        string fk_suffix_code = null;
        string org = null;
        string fk_addressType_code = null;
        string address_base = null;
        string address_nbr = null;
        string fk_usps_abbr = null;
        string fk_zipcode = null;
        string communicationType = null;
        string fk_communicationUsage_code = null;
        string phoneCell = null;
        string email = null;
        string fk_participantType_code = null;
        string fk_regionWAC_code = null;
        string DBA = null;
        int? fk_user = null;

        DropDownList ddl = fvParticipantCreate.FindControl("ddlPrefix") as DropDownList;
        if (ddl.SelectedValue != "") fk_prefix_code = ddl.SelectedValue;

        TextBox tb = fvParticipantCreate.FindControl("tbFirstName") as TextBox;
        if (tb.Text != "") fname = tb.Text;

        tb = fvParticipantCreate.FindControl("tbLastName") as TextBox;
        if (tb.Text != "") lname = tb.Text;

        ddl = fvParticipantCreate.FindControl("ddlSuffix") as DropDownList;
        if (ddl.SelectedValue != "") fk_suffix_code = ddl.SelectedValue;

        //ddl = fvParticipantCreate.FindControl("ddlOrganization") as DropDownList;
        //if (ddl.SelectedValue != "") org = ddl.SelectedValue;

        tb = fvParticipantCreate.FindControl("tbOrganization") as TextBox;
        org = tb.Text;

        ddl = fvParticipantCreate.FindControl("ddlAddressType") as DropDownList;
        if (ddl.SelectedValue != "") fk_addressType_code = ddl.SelectedValue;

        tb = fvParticipantCreate.FindControl("tbAddressBase") as TextBox;
        if (tb.Text != "") address_base = tb.Text;

        tb = fvParticipantCreate.FindControl("tbAddressNumber") as TextBox;
        if (tb.Text != "") address_nbr = tb.Text;

        ddl = fvParticipantCreate.FindControl("ddlUSPSAbbr") as DropDownList;
        if (ddl.SelectedValue != "") fk_usps_abbr = ddl.SelectedValue;

        tb = fvParticipantCreate.FindControl("tbZipCode") as TextBox;
        if (tb.Text != "") fk_zipcode = tb.Text;

        ddl = fvParticipantCreate.FindControl("ddlCommunicationType") as DropDownList;
        if (ddl.SelectedValue != "") communicationType = ddl.SelectedValue;

        ddl = fvParticipantCreate.FindControl("ddlCommunicationUsage") as DropDownList;
        if (ddl.SelectedValue != "") fk_communicationUsage_code = ddl.SelectedValue;

        tb = fvParticipantCreate.FindControl("tbPhoneNumber") as TextBox;
        if (tb.Text != "") phoneCell = tb.Text;

        tb = fvParticipantCreate.FindControl("tbEmail") as TextBox;
        if (tb.Text != "") email = tb.Text;

        ddl = fvParticipantCreate.FindControl("ddlParticipantType") as DropDownList;
        if (ddl.SelectedValue != "") fk_participantType_code = ddl.SelectedValue;

        ddl = fvParticipantCreate.FindControl("ddlWACRegion") as DropDownList;
        if (ddl.SelectedValue != "") fk_regionWAC_code = ddl.SelectedValue;

        tb = (TextBox)fvParticipantCreate.FindControl("tbDBA");
        DBA = tb.Text;

        fk_user = Convert.ToInt32(Session["userID"]);


        int? pk_participant = 0;

        int iCode = wDataContext.participant_add_multiTable(
            modeParticipant,
            fk_prefix_code, 
            fname, 
            lname, 
            fk_suffix_code, 
            org, 
            fk_addressType_code, 
            address_base, 
            address_nbr, 
            fk_usps_abbr, 
            fk_zipcode, 
            communicationType, 
            fk_communicationUsage_code, 
            phoneCell, 
            email, 
            fk_participantType_code, 
            fk_regionWAC_code, 
            DBA,
            fk_user,
            ref pk_participant);

        if (iCode != 0)
        {
            WACAlert.Show("", iCode);
        }
        else
        {
            if (pk_participant == null)
                _pk_participant = 0;
            else
                _pk_participant = Convert.ToInt32(pk_participant);

            mpeParticipant_add_rs.Hide();

            if (Inserted != null)
                Inserted(this, new EventArgs());
        }
        
    }


    protected void lbParticipant_add_rs_Close_Click(object sender, EventArgs e)
    {
        mpeParticipant_add_rs.Hide();
    }
    #endregion
}
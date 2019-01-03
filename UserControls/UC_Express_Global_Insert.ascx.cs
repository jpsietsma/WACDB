using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Express_Global_Insert : System.Web.UI.UserControl
{
    //public string StringParticipantType = string.Empty; // Set this property to assign an initial participant type to a new participant
    public bool _BoolShowCountyTownship = true;
    public bool _BoolShowBasinSubbasin = true;
    public bool _BoolShowTownship = true;
    public bool _BoolShowSubbasin = true;
    public bool _CountyControlsBasin = true;
    #region Global Methods and Event Handling

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void HideGlobal_Insert_Panels(bool bResetGlobalList)
    {
        if (bResetGlobalList) ddlGlobalList.SelectedIndex = 0;
        pnlGlobal_Insert_Communications.Visible = false;
        pnlGlobal_Insert_Organizations.Visible = false;
        pnlGlobal_Insert_Participants.Visible = false;
        pnlGlobal_Insert_Properties.Visible = false;
    }

    protected void ddlGlobalList_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideGlobal_Insert_Panels(false);
        if (!string.IsNullOrEmpty(ddlGlobalList.SelectedValue))
        {
            switch (ddlGlobalList.SelectedValue)
            {
                case "Communications":
                    lblGlobal_Insert_Communications.Text = "";
                    tbCommunications_AreaCode.Text = "";
                    tbCommunications_PhoneNumber.Text = "";
                    pnlGlobal_Insert_Communications.Visible = true; 
                    break;
                case "Organizations":
                    lblGlobal_Insert_Organizations.Text = "";
                    tbOrganizations_Organization.Text = "";
                    pnlGlobal_Insert_Organizations.Visible = true;
                    break;
                case "Participants":
                    Handle_Participant_TypeChange("0", true);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_Suffix_DDL(ddlParticipants_Suffix, null, true);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_ParticipantType_DDL(ddlParticipantType, "P", false);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_RegionWAC_DDL(ddlWACRegion, null);
                    pnlGlobal_Insert_Participants.Visible = true;
                    break;
                case "Properties":
                    WACGlobal_Methods.PopulateControl_DatabaseLists_StatesUS_DDL(ddlState, "NY", false);
                    WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(ddlZip, null, "NY", true);
                    pnlGlobal_Insert_Properties.Visible = true;
                    break;
            }
        }
    }

    protected void btnGlobal_Insert_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        switch (btn.CommandArgument)
        {
            case "Communications": Insert_Communications(); break;
            case "Organizations": Insert_Organizations(); break;
            case "Participants": Insert_Participants(); break;
            case "Properties": Insert_Properties(); break;
        }
    }

    #endregion

    #region Communications

    private void Insert_Communications()
    {
        lblGlobal_Insert_Communications.ForeColor = System.Drawing.Color.ForestGreen;
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                string sAreaCode = null;
                if (!string.IsNullOrEmpty(tbCommunications_AreaCode.Text)) sAreaCode = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbCommunications_AreaCode.Text, WACGlobal_Methods.Enum_Number_Type.AREACODE);
                else sb.Append("Area Code is required. ");
                if (string.IsNullOrEmpty(sAreaCode)) sb.Append("Area Code not in correct format. ");

                string sPhoneNumber = null;
                if (!string.IsNullOrEmpty(tbCommunications_PhoneNumber.Text)) sPhoneNumber = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbCommunications_PhoneNumber.Text, WACGlobal_Methods.Enum_Number_Type.PHONENUMBER);
                else sb.Append("Phone Number is required. ");
                if (string.IsNullOrEmpty(sPhoneNumber)) sb.Append("Phone Number not in correct format. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    int? i = null;
                    int iCode = wac.communication_add(sAreaCode, sPhoneNumber, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        tbCommunications_AreaCode.Text = "";
                        tbCommunications_PhoneNumber.Text = "";
                        lblGlobal_Insert_Communications.Text = "Successfully added Communication: " + WACGlobal_Methods.Format_Global_PhoneNumberSeparateAreaCode(sAreaCode, sPhoneNumber);
                    }
                    else WACAlert.Show("Error Inserting Communication: ", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
        }
        catch (Exception ex)
        {
            lblGlobal_Insert_Communications.ForeColor = System.Drawing.Color.Red;
            lblGlobal_Insert_Communications.Text = "Error Inserting Communication: " + ex.Message;
        }
    }

    #endregion

    #region Organizations

    private void Insert_Organizations()
    {
        lblGlobal_Insert_Organizations.ForeColor = System.Drawing.Color.ForestGreen;
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                string sOrganization = null;
                if (!string.IsNullOrEmpty(tbOrganizations_Organization.Text)) sOrganization = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbOrganizations_Organization.Text, 96);
                else sb.Append("Organization is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    int? i = null;
                    int iCode = wac.organization_add(sOrganization, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        tbOrganizations_Organization.Text = "";
                        lblGlobal_Insert_Organizations.Text = "Successfully added Organization: " + sOrganization;
                    }
                    else WACAlert.Show("Error Inserting Organization: ", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
        }
        catch (Exception ex)
        {
            lblGlobal_Insert_Organizations.ForeColor = System.Drawing.Color.Red;
            lblGlobal_Insert_Organizations.Text = "Error Inserting Organization: " + ex.Message;
        }
    }

    #endregion

    #region Participants

    protected void rblParticipants_SelectedIndexChanged(object sender, EventArgs e)
    {
        Handle_Participant_TypeChange(rblParticipants.SelectedValue, false);
    }

    private void Handle_Participant_TypeChange(string sValue, bool bResetControls)
    {
        switch (sValue)
        {
            case "0":
                pnlParticipant_Organization.Visible = false;
                pnlParticipant_Person.Visible = true;
                break;
            case "1":
                pnlParticipant_Person.Visible = true;
                pnlParticipant_Organization.Visible = true;
                break;
            case "2":
                pnlParticipant_Person.Visible = false;
                pnlParticipant_Organization.Visible = true;
                break;
        }
        if (bResetControls)
        {
            lblGlobal_Insert_Participants.Text = "";
            tbParticipants_NameFirst.Text = "";
            tbParticipants_NameLast.Text = "";
            UC_DropDownListByAlphabet_Participants.ResetControls();
        }
    }

    private void Insert_Participants()
    {
        lblGlobal_Insert_Participants.ForeColor = System.Drawing.Color.ForestGreen;
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                string sParticipant = null;
                string sParticipantSuffix = null;
                string sParticipantOrg = null;

                string sNameFirst = null;
                string sNameLast = null;
                string sFK_Suffix = null;
                int? iFK_Organization = null;
                string sParticipantType = null;
                string sWACRegion = null;

                if (rblParticipants.SelectedValue == "0" || rblParticipants.SelectedValue == "1")
                {
                    if (!string.IsNullOrEmpty(tbParticipants_NameFirst.Text)) sNameFirst = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbParticipants_NameFirst.Text, 48);
                    else sb.Append("First Name is required. ");

                    if (!string.IsNullOrEmpty(tbParticipants_NameLast.Text)) sNameLast = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbParticipants_NameLast.Text, 48);
                    else sb.Append("Last Name is required. ");

                    if (!string.IsNullOrEmpty(ddlParticipants_Suffix.SelectedValue))
                    {
                        sFK_Suffix = ddlParticipants_Suffix.SelectedValue;
                        sParticipantSuffix = ddlParticipants_Suffix.SelectedItem.Text;
                    }
                }

                if (rblParticipants.SelectedValue == "1" || rblParticipants.SelectedValue == "2")
                {
                    DropDownList ddl = UC_DropDownListByAlphabet_Participants.FindControl("ddl") as DropDownList;
                    if (!string.IsNullOrEmpty(ddl.SelectedValue))
                    {
                        iFK_Organization = Convert.ToInt32(ddl.SelectedValue);
                        sParticipantOrg = ddl.SelectedItem.Text;
                    }
                    else sb.Append("Organization is required.");
                }

                sParticipant = WACGlobal_Methods.SpecialText_Global_Participant_Name_Org(sNameLast, sNameFirst, null, sParticipantSuffix, sParticipantOrg);

                sParticipantType = ddlParticipantType.SelectedValue;

                if (!string.IsNullOrEmpty(ddlWACRegion.SelectedValue)) sWACRegion = ddlWACRegion.SelectedValue;

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    int? i = null;
                    int iCode = wac.participant_add_express(sNameFirst, sNameLast, sFK_Suffix, iFK_Organization, sParticipantType, sWACRegion, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        //if (!string.IsNullOrEmpty(StringParticipantType))
                        //{
                        //    int? i2 = null;
                        //    int iCode2 = wac.participantType_add(i, StringParticipantType, Session["userName"].ToString(), ref i2);
                        //    if (iCode2 != 0) WACAlert.Show("Successfully added Participant, but Error Inserting Participant Type: ", iCode2);
                        //}
                        
                        Handle_Participant_TypeChange("0", true);
                        lblGlobal_Insert_Participants.Text = "Successfully added Participant: " + sParticipant;
                    }
                    else WACAlert.Show("Error Inserting Participant: ", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
        }
        catch (Exception ex)
        {
            lblGlobal_Insert_Participants.ForeColor = System.Drawing.Color.Red;
            lblGlobal_Insert_Participants.Text = "Error Inserting Participant: " + ex.Message;
        }
    }

    #endregion

    #region Properties

    //protected void ddlProperties_State_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(ddlProperties_Zip, null, ddlProperties_State.SelectedValue, true);
    //}
    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCounty = (DropDownList)sender;
        DropDownList ddlTownshipNY = (DropDownList)pnlGlobal_Insert_Properties.FindControl("ddlTownshipNY");
        DropDownList ddlBasin = (DropDownList)pnlGlobal_Insert_Properties.FindControl("ddlbasin");
        DropDownList ddlSubbasin = (DropDownList)pnlGlobal_Insert_Properties.FindControl("ddlSubbasin");

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
        DropDownList ddlSubbasin = (DropDownList)pnlGlobal_Insert_Properties.FindControl("ddlSubbasin");
        if (_BoolShowSubbasin)
        {
            if (!string.IsNullOrEmpty(ddlBasin.SelectedValue))
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Subbasin_DDL(ddlSubbasin, ddlBasin.SelectedValue, null);
            }
            else ddlSubbasin.Items.Clear();
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        TextBox tbCity = pnlGlobal_Insert_Properties.FindControl("tbCity") as TextBox;
        DropDownList ddlCounty = pnlGlobal_Insert_Properties.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = pnlGlobal_Insert_Properties.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = pnlGlobal_Insert_Properties.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = pnlGlobal_Insert_Properties.FindControl("ddlSubbasin") as DropDownList;
        DropDownList ddlZip = (DropDownList)pnlGlobal_Insert_Properties.FindControl("ddlZip");
        WACGlobal_Methods.PopulateControl_DatabaseLists_Zipcode_DDL(ddlZip, null, ddl.SelectedValue, true);
        if (ddl.SelectedValue == "NY")
        {
            WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCounty, false, null);
            ddlTownshipNY.Items.Clear();
            ddlBasin.Items.Clear();
            ddlSubbasin.Items.Clear();
        }
        else
        {
            ddlCounty.Items.Clear();
            ddlTownshipNY.Items.Clear();
            ddlBasin.Items.Clear();
            ddlSubbasin.Items.Clear();
        }

        if (tbCity != null) tbCity.Text = string.Empty;
      
    }

    protected void ddlZip_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        DropDownList ddlState = pnlGlobal_Insert_Properties.FindControl("ddlState") as DropDownList;
        DropDownList ddlCounty = pnlGlobal_Insert_Properties.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = pnlGlobal_Insert_Properties.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = pnlGlobal_Insert_Properties.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = pnlGlobal_Insert_Properties.FindControl("ddlSubbasin") as DropDownList;
        TextBox tbCity = pnlGlobal_Insert_Properties.FindControl("tbCity") as TextBox;
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
                        WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCounty, false, c.Single());
                        WACGlobal_Methods.PopulateControl_DatabaseLists_TownshipNY_DDL(ddlTownshipNY, c.Single(), null);
                        WACGlobal_Methods.PopulateControl_DatabaseLists_Basin_DDL(ddlBasin, null, null, null, c.Single(), null, true);
                        ddlSubbasin.Items.Clear();
                    }
                    else
                    {
                        WACGlobal_Methods.PopulateControl_DatabaseLists_CountyNY_DDL(ddlCounty, false, null);
                        ddlTownshipNY.Items.Clear();
                        ddlBasin.Items.Clear();
                        ddlSubbasin.Items.Clear();
                    }
                }
                catch
                {
                    ddlCounty.SelectedIndex = 0;
                    ddlTownshipNY.Items.Clear();
                    ddlBasin.Items.Clear();
                    ddlSubbasin.Items.Clear();
                    tbCity.Text = "";
                }
            }
            else
            {
                ddlCounty.Items.Clear();
                ddlTownshipNY.Items.Clear();
                ddlBasin.Items.Clear();
                ddlSubbasin.Items.Clear();
            }
            tbCity.Text = a.city;
        }
    }
    private void Insert_Properties()
    {
        lblGlobal_Insert_Properties.ForeColor = System.Drawing.Color.ForestGreen;
        int? i = null;
        int iCode = 0;

        TextBox tbAddress1 = pnlGlobal_Insert_Properties.FindControl("tbAddress1") as TextBox;
        TextBox tbAddress2 = pnlGlobal_Insert_Properties.FindControl("tbAddress2") as TextBox;
        DropDownList ddlState = pnlGlobal_Insert_Properties.FindControl("ddlState") as DropDownList;
        DropDownList ddlZip = pnlGlobal_Insert_Properties.FindControl("ddlZip") as DropDownList;
        TextBox tbCity = pnlGlobal_Insert_Properties.FindControl("tbCity") as TextBox;
        TextBox tbZip4 = pnlGlobal_Insert_Properties.FindControl("tbZip4") as TextBox;
        DropDownList ddlCounty = pnlGlobal_Insert_Properties.FindControl("ddlCounty") as DropDownList;
        DropDownList ddlTownshipNY = pnlGlobal_Insert_Properties.FindControl("ddlTownshipNY") as DropDownList;
        DropDownList ddlBasin = pnlGlobal_Insert_Properties.FindControl("ddlBasin") as DropDownList;
        DropDownList ddlSubbasin = pnlGlobal_Insert_Properties.FindControl("ddlSubbasin") as DropDownList;

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
                if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
                    county = Convert.ToInt32(ddlCounty.SelectedValue);
                int? township = null;
                if (!string.IsNullOrEmpty(ddlTownshipNY.SelectedValue))
                    township = Convert.ToInt32(ddlTownshipNY.SelectedValue);
                string basinCode = string.IsNullOrEmpty(ddlBasin.SelectedValue) ? null : ddlBasin.SelectedValue;
                string subBasinCode = string.IsNullOrEmpty(ddlSubbasin.SelectedValue) ? null : ddlSubbasin.SelectedValue;

                iCode = wDataContext.property_add_express(addr1, addr2, city, state, zipCode, zip4, county, township, basinCode, subBasinCode, Session["userName"].ToString(), ref i);
                if (iCode == 0)
                {
                    lblGlobal_Insert_Properties.Text = "Successfully added Property: ";
                }
                else
                    throw new WAC_Exceptions.WACEX_GeneralDatabaseException(string.Empty, iCode);
            }
            catch (Exception ex)
            {
                lblGlobal_Insert_Properties.ForeColor = System.Drawing.Color.Red;
                lblGlobal_Insert_Properties.Text = "Error Inserting Property: " + ex.Message;
            }
        }
    }

    #endregion
}
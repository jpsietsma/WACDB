using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class UC_Express_PageButtons : System.Web.UI.UserControl
{
    public bool ShowImageButtonView = true;
    public bool ShowImageButtonNav = true;
    public string StrExpressType = "PARTICIPANT"; // COMMUNICATION, FARMBUSINESS, ORGANIZATION, PARTICIPANT, PROPERTY, VENUE
    public string StrSection = "GLOBAL"; // AGRICULTURE, FARMTOMARKET, GLOBAL, PARTICIPANT, PARTICIPANTCOMMUNICATION, PARTICIPANTORGANIZATION, TAXPARCEL, VENUEPHONE, VENUEFAX
    public string StrImageSize = "16";
    public bool BoolJustUsePK = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ShowImageButtonView)
        {
            ibExpressView.ImageUrl = "~/images/arrow_orb_up_16.png";
            if (StrImageSize == "24") ibExpressView.ImageUrl = "~/images/arrow_orb_up_24.png";
            if (StrImageSize == "32") ibExpressView.ImageUrl = "~/images/arrow_orb_up_32.png";
            ibExpressView.ToolTip = "Click to Open the Express Window";
        }
        else ibExpressView.Visible = false;

        if (ShowImageButtonNav)
        {
            ibExpressNav.ImageUrl = "~/images/arrow_orb_right_16.png";
            if (StrImageSize == "24") ibExpressNav.ImageUrl = "~/images/arrow_orb_right_24.png";
            if (StrImageSize == "32") ibExpressNav.ImageUrl = "~/images/arrow_orb_right_32.png";
            ibExpressNav.ToolTip = "Click to Navigate to the Section for this Record";
        }
        else ibExpressNav.Visible = false;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ibExpressView.CommandArgument)) ibExpressView.Visible = false;
        if (string.IsNullOrEmpty(ibExpressNav.CommandArgument)) ibExpressNav.Visible = false;
    }

    protected void ibExpressView_Click(object sender, EventArgs e)
    {
        ImageButton ib = (ImageButton)sender;
        object target; 
        object[] oArgs = null;
        if (!string.IsNullOrEmpty(ib.CommandArgument)) oArgs = new object[] { ib.CommandArgument };
        else oArgs = new object[] { -1 };
        switch (StrExpressType)
        {
            case "ORGANIZATION":
                target = WACGlobal_Methods.ContainingObject(ib.Page, "Organization_ViewEditInsertWindow");
                target.GetType().InvokeMember("Organization_ViewEditInsertWindow", BindingFlags.InvokeMethod, null, target, oArgs);
                break;
            case "PARTICIPANT":
                target = WACGlobal_Methods.ContainingObject(ib.Page, "Participant_ViewEditInsertWindow");
                target.GetType().InvokeMember("Participant_ViewEditInsertWindow", BindingFlags.InvokeMethod, null, target, oArgs);
                break;
            case "PROPERTY":
                target = WACGlobal_Methods.ContainingObject(ib.Page, "Property_ViewEditInsertWindow");
                target.GetType().InvokeMember("Property_ViewEditInsertWindow", BindingFlags.InvokeMethod, null, target, oArgs);
                break;
        }
    }

    protected void ibExpressNav_Click(object sender, EventArgs e)
    {
        ImageButton ib = (ImageButton)sender;
        if (!string.IsNullOrEmpty(ib.CommandArgument))
        {
            switch (StrExpressType)
            {
                case "COMMUNICATION":
                    Response.Redirect("~/WACCommunications.aspx?pk=" + ib.CommandArgument);
                    break;
                case "FARMBUSINESS":
                    Response.Redirect("~/AG/WACAgriculture.aspx?pk=" + ib.CommandArgument);
                    break;
                case "ORGANIZATION":
                    Response.Redirect("~/Participant/WACPT_Organizations.aspx?pk=" + ib.CommandArgument);
                    break;
                case "PARTICIPANT":
                    Response.Redirect("~/Participant/WACPT_Participants.aspx?pk=" + ib.CommandArgument);
                    break;
                case "PROPERTY":
                    Response.Redirect("~/Property/WACPR_Property.aspx?pk=" + ib.CommandArgument);
                    break;
                case "VENUE":
                    Response.Redirect("~/WACVenues.aspx?pk=" + ib.CommandArgument);
                    break;
            }
        }
        else WACAlert.Show("Cannot navigate to the global section for this record.", 0);
    }

    public string GetFieldsBySection()
    {
        string s = string.Empty;
        switch (StrExpressType)
        {
            case "COMMUNICATION":
                s = "pk_communication";
                if (StrSection == "FARMTOMARKET") s = "eventVenue.fk_communication_phone";
                if (StrSection == "PARTICIPANTCOMMUNICATION") s = "fk_communication";
                if (StrSection == "VENUEPHONE") s = "fk_communication_phone";
                if (StrSection == "VENUEFAX") s = "fk_communication_fax";
                break;
            case "FARMBUSINESS":
                s = "pk_farmBusiness";
                if (StrSection == "PARTICIPANT") s = "farmBusiness.pk_farmBusiness";
                break;
            case "ORGANIZATION":
                s = "organization.pk_organization";
                if (StrSection == "TAXPARCEL") s = "participant." + s;
                break;
            case "PARTICIPANT":
                s = "participant.pk_participant";
                if (StrSection == "PARTICIPANTORGANIZATION") s = "fk_participant";
                break;
            case "PROPERTY":
                s = "property.pk_property";
                if (StrSection == "AGRICULTURE") s = "farmland." + s;
                break;
            case "VENUE":
                s = "pk_eventVenue";
                if (StrSection == "FARMTOMARKET") s = "eventVenue." + s;
                break;
        }

        if (BoolJustUsePK) s = s.Substring(s.LastIndexOf(".") + 1);
        
        return s;
    }
}
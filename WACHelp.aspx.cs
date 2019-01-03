using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataBind_HelpDocuments();
        }
    }

    private void DataBind_HelpDocuments()
    {
        List<HelpDoc> l_Global = new List<HelpDoc>
        {
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Global Data Overview.pdf", StrDocName = "FAME Global Data Overview.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Global Data Communication.pdf", StrDocName = "FAME Global Data Communication.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Global Data Organization.pdf", StrDocName = "FAME Global Data Organization.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Global Data Participant.pdf", StrDocName = "FAME Global Data Participant.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Global Data Property.pdf", StrDocName = "FAME Global Data Property.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Global Data Tax Parcel.pdf", StrDocName = "FAME Global Data Tax Parcel.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Global Data Venue.pdf", StrDocName = "FAME Global Data Venue.pdf" },
        };

        List<HelpDoc> l_Forestry = new List<HelpDoc>
        {
            //new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Forestry General.pdf", StrDocName = "FAME Forestry General.pdf" },
            //new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Forestry Overview.pdf", StrDocName = "FAME Forestry Overview.pdf" },
        };

        List<HelpDoc> l_Agriculture = new List<HelpDoc>
        {
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture General.pdf", StrDocName = "FAME Agriculture General.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Overview.pdf", StrDocName = "FAME Agriculture Overview.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - BMP.pdf", StrDocName = "FAME Agriculture Express - BMP.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - Planners Screen.pdf", StrDocName = "FAME Agriculture Express - Planners Screen.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - Supplemental Agreement.pdf", StrDocName = "FAME Agriculture Express - Supplemental Agreement.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - Tax Parcel.pdf", StrDocName = "FAME Agriculture Express - Tax Parcel.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - WFP2.pdf", StrDocName = "FAME Agriculture Express - WFP2.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - WFP2 Revision.pdf", StrDocName = "FAME Agriculture Express - WFP2 Revision.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - WFP3.pdf", StrDocName = "FAME Agriculture Express - WFP3.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Express - WFP3 Specification.pdf", StrDocName = "FAME Agriculture Express - WFP3 Specification.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Field Mappings - APPROVALS.pdf", StrDocName = "FAME Agriculture Field Mappings - APPROVALS.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Field Mappings - CONTRACTOR BIDS.pdf", StrDocName = "FAME Agriculture Field Mappings - CONTRACTOR BIDS.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Field Mappings - DEPINV22.pdf", StrDocName = "FAME Agriculture Field Mappings - DEPINV22.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Field Mappings - TABLE1.pdf", StrDocName = "FAME Agriculture Field Mappings - TABLE1.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Agriculture Field Mappings - WAC PAYMENTS.pdf", StrDocName = "FAME Agriculture Field Mappings - WAC PAYMENTS.pdf" },

            
            
        };

        List<HelpDoc> l_Marketing = new List<HelpDoc>
        {
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Marketing Events.pdf", StrDocName = "FAME Marketing Events.pdf" },
            new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Marketing PureCatskills.pdf", StrDocName = "FAME Marketing PureCatskills.pdf" },
        };

        List<HelpDoc> l_Easements = new List<HelpDoc>
        {
            //new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Easements General.pdf", StrDocName = "FAME Easements General.pdf" },
            //new HelpDoc { StrDocPath = ConfigurationManager.AppSettings["DocsLink"] +  "Help/FAME Easements Overview.pdf", StrDocName = "FAME Easements Overview.pdf" },
        };

        lvHelp_Global.DataSource = l_Global;
        lvHelp_Global.DataBind();

        lvHelp_Forestry.DataSource = l_Forestry;
        lvHelp_Forestry.DataBind();

        lvHelp_Agriculture.DataSource = l_Agriculture;
        lvHelp_Agriculture.DataBind();

        lvHelp_Marketing.DataSource = l_Marketing;
        lvHelp_Marketing.DataBind();

        lvHelp_Easements.DataSource = l_Easements;
        lvHelp_Easements.DataBind();
    }

    protected void lbHelp_Global_Click(object sender, EventArgs e)
    {
        if (pnlHelp_Global.Visible == false)
        {
            pnlHelp_Global.Visible = true;
            lbHelp_Global.Text = "[-] Global Help Documents";
        }
        else
        {
            pnlHelp_Global.Visible = false;
            lbHelp_Global.Text = "[+] Global Help Documents";
        }
    }

    protected void lbHelp_Forestry_Click(object sender, EventArgs e)
    {
        if (pnlHelp_Forestry.Visible == false)
        {
            pnlHelp_Forestry.Visible = true;
            lbHelp_Forestry.Text = "[-] Forestry Help Documents";
        }
        else
        {
            pnlHelp_Forestry.Visible = false;
            lbHelp_Forestry.Text = "[+] Forestry Help Documents";
        }
    }

    protected void lbHelp_Agriculture_Click(object sender, EventArgs e)
    {
        if (pnlHelp_Agriculture.Visible == false)
        {
            pnlHelp_Agriculture.Visible = true;
            lbHelp_Agriculture.Text = "[-] Agriculture Help Documents";
        }
        else
        {
            pnlHelp_Agriculture.Visible = false;
            lbHelp_Agriculture.Text = "[+] Agriculture Help Documents";
        }
    }

    protected void lbHelp_Marketing_Click(object sender, EventArgs e)
    {
        if (pnlHelp_Marketing.Visible == false)
        {
            pnlHelp_Marketing.Visible = true;
            lbHelp_Marketing.Text = "[-] Marketing Help Documents";
        }
        else
        {
            pnlHelp_Marketing.Visible = false;
            lbHelp_Marketing.Text = "[+] Marketing Help Documents";
        }
    }

    protected void lbHelp_Easements_Click(object sender, EventArgs e)
    {
        if (pnlHelp_Easements.Visible == false)
        {
            pnlHelp_Easements.Visible = true;
            lbHelp_Easements.Text = "[-] Easements Help Documents";
        }
        else
        {
            pnlHelp_Easements.Visible = false;
            lbHelp_Easements.Text = "[+] Easements Help Documents";
        }
    }
    
}

public class HelpDoc
{
    public string StrDocPath { get; set; }
    public string StrDocName { get; set; }
}
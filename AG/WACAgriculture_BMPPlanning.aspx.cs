using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACAgriculture_BMPPlanning : System.Web.UI.Page
{
    #region Initialization

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateControl_Search_FarmID_DDL();
            PopulateControl_Search_FarmName_DDL();
            PopulateControl_Search_FarmOwners_DDL();

            hlAg_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Agriculture Express - Planners Screen.pdf";
            hlAg_Help.ImageUrl = "~/images/help_24.png"; 
        }
    }

    private void PopulateControl_Search_FarmID_DDL()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddlBMPPlanning_Search_FarmID.DataTextField = "NAME";
            ddlBMPPlanning_Search_FarmID.DataValueField = "PK";
            ddlBMPPlanning_Search_FarmID.DataSource = wac.bmp_ags.Where(w => w.farmBusiness.farmID != "" && w.fk_statusBMP_code == "DR" && w.fk_programmaticRecord_code == null).Select(s => new { PK = s.farmBusiness.pk_farmBusiness, NAME = s.farmBusiness.farmID }).Distinct().OrderBy(o => o.NAME);
            ddlBMPPlanning_Search_FarmID.DataBind();
            ddlBMPPlanning_Search_FarmID.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateControl_Search_FarmName_DDL()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddlBMPPlanning_Search_FarmName.DataTextField = "NAME";
            ddlBMPPlanning_Search_FarmName.DataValueField = "PK";
            ddlBMPPlanning_Search_FarmName.DataSource = wac.bmp_ags.Where(w => w.farmBusiness.farm_name != "" && w.fk_statusBMP_code == "DR" && w.fk_programmaticRecord_code == null).Select(s => new { PK = s.farmBusiness.pk_farmBusiness, NAME = s.farmBusiness.farm_name }).Distinct().OrderBy(o => o.NAME);
            ddlBMPPlanning_Search_FarmName.DataBind();
            ddlBMPPlanning_Search_FarmName.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    private void PopulateControl_Search_FarmOwners_DDL()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddlBMPPlanning_Search_FarmOwners.DataTextField = "NAME";
            ddlBMPPlanning_Search_FarmOwners.DataValueField = "PK";
            ddlBMPPlanning_Search_FarmOwners.DataSource = wac.bmp_ags.Where(w => w.farmBusiness.ownerStr_dnd != "" && w.fk_statusBMP_code == "DR" && w.fk_programmaticRecord_code == null).Select(s => new { PK = s.farmBusiness.pk_farmBusiness, NAME = s.farmBusiness.ownerStr_dnd }).Distinct().OrderBy(o => o.NAME);
            ddlBMPPlanning_Search_FarmOwners.DataBind();
            ddlBMPPlanning_Search_FarmOwners.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    #endregion

    #region Event Handling

    private void ResetSearch(string sValue, ref DropDownList ddl)
    {
        ddlBMPPlanning_Search_FarmID.SelectedIndex = 0;
        ddlBMPPlanning_Search_FarmName.SelectedIndex = 0;
        ddlBMPPlanning_Search_FarmOwners.SelectedIndex = 0;
        if (!string.IsNullOrEmpty(sValue) && ddl != null) ddl.SelectedValue = sValue;
    }

    protected void lbBMPPlanning_Search_ReloadReset_Click(object sender, EventArgs e)
    {
        DropDownList ddl = new DropDownList();
        ResetSearch(null, ref ddl);
        gvBMPPlanning.DataSource = "";
        gvBMPPlanning.DataBind();
    }

    protected void ddlBMPPlanning_Search_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        string sValue = ddl.SelectedValue;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) BindData_BMPPlanning_GridView(Convert.ToInt32(sValue));

        ResetSearch(sValue, ref ddl);
    }

    protected void gvBMPPlanning_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvBMPPlanning.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");

        fvBMPPlanning.ChangeMode(FormViewMode.ReadOnly);
        BindData_BMPPlanning_FormView(Convert.ToInt32(gvBMPPlanning.SelectedDataKey.Value));
        mpeBMPPlanning.Show();
        upBMPPlanning.Update();
    }

    protected void lbBMPPlanning_Close_Click(object sender, EventArgs e)
    {
        fvBMPPlanning.ChangeMode(FormViewMode.ReadOnly);
        BindData_BMPPlanning_FormView(-1);
        mpeBMPPlanning.Hide();
        int iHF_PK = -1;
        if (!string.IsNullOrEmpty(hfPK_FarmBusiness.Value)) iHF_PK = Convert.ToInt32(hfPK_FarmBusiness.Value);
        BindData_BMPPlanning_GridView(iHF_PK);
        up.Update();
    }

    protected void lbBMPPlanning_Insert_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "bmp_ag", "msgInsert"))
        {
            fvBMPPlanning.ChangeMode(FormViewMode.Insert);
            BindData_BMPPlanning_FormView(-1);
            mpeBMPPlanning.Show();
            upBMPPlanning.Update();
        }
    }

    protected void ddlPractice_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        Label lblUnits = fvBMPPlanning.FindControl("lblUnits") as Label;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) lblUnits.Text = WACGlobal_Methods.SpecialQuery_Agriculture_Unit_By_BMPPractice(ddl.SelectedValue);
    }

    protected void lbSA_Clear_Click(object sender, EventArgs e)
    {
        DropDownList ddlSAA = fvBMPPlanning.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvBMPPlanning.FindControl("ddlSA") as DropDownList;
        try
        {
            ddlSAA.SelectedIndex = 0;
            ddlSA.SelectedIndex = 0;
        }
        catch { }
    }

    protected void ddlFarm_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddlFarm = (DropDownList)sender;
        //Panel pnlSA_Available = fvBMPPlanning.FindControl("pnlSA_Available") as Panel;
        //Panel pnlSA_Message = fvBMPPlanning.FindControl("pnlSA_Message") as Panel;
        //Label lblSA_Message = pnlSA_Message.FindControl("lblSA_Message") as Label;
        //if (!string.IsNullOrEmpty(ddlFarm.SelectedValue)) HandleSupplementalAgreementsBasedOnFarm(Convert.ToInt32(ddlFarm.SelectedValue), null);
        //else
        //{
        //    lblSA_Message.Text = "Select a farm to view available Supplemental Agreements";
        //    pnlSA_Available.Visible = false;
        //    pnlSA_Message.Visible = true;
        //}
    }

    //private void HandleSupplementalAgreementsBasedOnFarm(int iPK_FarmBusiness, int? iPK_SupplementalAgreementTaxParcel)
    private void HandleSupplementalAgreementsBasedOnFarm(int? iPK_SupplementalAgreementTaxParcel)
    {
        Panel pnlSA_Available = fvBMPPlanning.FindControl("pnlSA_Available") as Panel;
        Panel pnlSA_Message = fvBMPPlanning.FindControl("pnlSA_Message") as Panel;
        Label lblSA_Message = fvBMPPlanning.FindControl("lblSA_Message") as Label;
        DropDownList ddlSAA = fvBMPPlanning.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvBMPPlanning.FindControl("ddlSA") as DropDownList;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            //var a = wac.bmp_ag_SAs.Where(w => w.bmp_ag.fk_farmBusiness == iPK_FarmBusiness).Select(s => s.pk_bmp_ag_SA);
            //if (a.Count() > 0)
            //{
                //if (fvBMPPlanning.CurrentMode == FormViewMode.Insert) WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(ddlSA, iPK_FarmBusiness, null, true);
                //else WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(ddlSA, iPK_FarmBusiness, iPK_SupplementalAgreementTaxParcel, true);
                if (fvBMPPlanning.CurrentMode == FormViewMode.Insert) WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(ddlSA, null, true);
                else WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(ddlSA, iPK_SupplementalAgreementTaxParcel, true);
                pnlSA_Message.Visible = false;
                pnlSA_Available.Visible = true;
            //}
            //else
            //{
            //    lblSA_Message.Text = "No Supplemental Agreements have been assigned to this farm";
            //    pnlSA_Available.Visible = false;
            //    pnlSA_Message.Visible = true;
            //}
        }
    }

    protected void fvBMPPlanning_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_PLAN);
        if (b)
        {
            fvBMPPlanning.ChangeMode(e.NewMode);
            BindData_BMPPlanning_FormView(Convert.ToInt32(fvBMPPlanning.DataKey.Value));
        }
        else WACAlert.Show("You do not have permission to modify the BMP Planning data.", 0);
    }

    protected void fvBMPPlanning_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        TextBox tbBMPNumber = fvBMPPlanning.FindControl("tbBMPNumber") as TextBox;
        TextBox tbDescription = fvBMPPlanning.FindControl("tbDescription") as TextBox;
        TextBox tbSubIssueStatement = fvBMPPlanning.FindControl("tbSubIssueStatement") as TextBox;
        DropDownList ddlBMPSource = fvBMPPlanning.FindControl("ddlBMPSource") as DropDownList;
        TextBox tbLocation = fvBMPPlanning.FindControl("tbLocation") as TextBox;
        DropDownList ddlRevisionDescription = fvBMPPlanning.FindControl("ddlRevisionDescription") as DropDownList;
        DropDownList ddlPollutantCategory = fvBMPPlanning.FindControl("ddlPollutantCategory") as DropDownList;
        DropDownList ddlBMPPractice = fvBMPPlanning.FindControl("ddlBMPPractice") as DropDownList;
        TextBox tbUnitsPlanned = fvBMPPlanning.FindControl("tbUnitsPlanned") as TextBox;
        TextBox tbPriorPlanningEstimate = fvBMPPlanning.FindControl("tbPriorPlanningEstimate") as TextBox;
        TextBox tbPlanningEstimate = fvBMPPlanning.FindControl("tbPlanningEstimate") as TextBox;
        DropDownList ddlCREP = fvBMPPlanning.FindControl("ddlCREP") as DropDownList;
        DropDownList ddlNMCPType = fvBMPPlanning.FindControl("ddlNMCPType") as DropDownList;
        DropDownList ddlSAA = fvBMPPlanning.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvBMPPlanning.FindControl("ddlSA") as DropDownList;

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int? iPH_PK_SA_TP = null;
                bool bPerformSABMPInsert = false;

                var a = wac.bmp_ags.Where(w => w.pk_bmp_ag == Convert.ToInt32(fvBMPPlanning.DataKey.Value)).Single();

                string sBMP_NBR_Orig = a.bmp_nbr;
                string sBMP_NBR = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbBMPNumber.Text, 24);
                if (sBMP_NBR_Orig != sBMP_NBR)
                {
                    int iBMP_NBR_Count = wac.bmp_ags.Where(w => w.fk_farmBusiness == a.fk_farmBusiness && w.bmp_nbr == sBMP_NBR).Count();
                    if (iBMP_NBR_Count > 0) sb.Append("BMP Number was not updated. The BMP Number already exists within this farm.");
                    else a.bmp_nbr = sBMP_NBR;
                }

                if (!string.IsNullOrEmpty(tbDescription.Text)) a.description = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 400);
                else sb.Append("Description was not updated. Description is required. ");
                if (!string.IsNullOrEmpty(tbSubIssueStatement.Text)) a.issueStatement_wfp2 = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbSubIssueStatement.Text, 1000);
                else a.issueStatement_wfp2 = null;
                if (!string.IsNullOrEmpty(ddlBMPSource.SelectedValue)) a.fk_BMPSource_code = ddlBMPSource.SelectedValue;
                else sb.Append("BMP Source was not updated. BMP Source is required. ");

                if (!string.IsNullOrEmpty(tbLocation.Text)) a.location = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLocation.Text, 96);
                //else sb.Append("Location was not updated. Location is required. ");

                if (!string.IsNullOrEmpty(ddlRevisionDescription.SelectedValue)) a.fk_revisionDescription_code = ddlRevisionDescription.SelectedValue;
                else sb.Append("Revision Description was not updated. Revision Description is required. ");

                if (!string.IsNullOrEmpty(ddlPollutantCategory.SelectedValue)) a.fk_pollutant_category_code = ddlPollutantCategory.SelectedValue;
                else sb.Append("Pollutant Category was not updated. Pollutant Category is required. ");

                if (!string.IsNullOrEmpty(ddlBMPPractice.SelectedValue)) a.fk_bmpPractice_code = Convert.ToDecimal(ddlBMPPractice.SelectedValue);
                else sb.Append("BMP Practice was not updated. BMP Practice is required. ");

                if (!string.IsNullOrEmpty(tbUnitsPlanned.Text))
                {
                    try { a.units_planned = Convert.ToDecimal(tbUnitsPlanned.Text); }
                    catch { sb.Append("Units Planned was not updated. Units Planned must be a number (Decimal). "); }
                }
                //else sb.Append("Units Planned was not updated. Units Planned is required. ");

                if (!string.IsNullOrEmpty(tbPriorPlanningEstimate.Text))
                {
                    try { a.est_plan_prior = Convert.ToDecimal(tbPriorPlanningEstimate.Text); }
                    catch { sb.Append("Prior Planning Estimate was not updated. Prior Planning Estimate must be a number (Decimal). "); }
                }
                else sb.Append("Prior Planning Estimate was not updated. Prior Planning Estimate is required. ");

                if (!string.IsNullOrEmpty(tbPlanningEstimate.Text))
                {
                    try { a.est_plan_rev = Convert.ToDecimal(tbPlanningEstimate.Text); }
                    catch { sb.Append("Planning Estimate was not updated. Planning Estimate must be a number (Decimal). "); }
                }
                else sb.Append("Planning Estimate was not updated. Planning Estimate is required. ");

                if (!string.IsNullOrEmpty(ddlCREP.SelectedValue)) a.CREP = ddlCREP.SelectedValue;
                else a.CREP = null;

                //if (!string.IsNullOrEmpty(ddlNMCPType.SelectedValue)) a.fk_NMCPType_code = ddlNMCPType.SelectedValue;
                //else a.fk_NMCPType_code = null;

                if (!string.IsNullOrEmpty(ddlSAA.SelectedValue) && !string.IsNullOrEmpty(ddlSA.SelectedValue))
                {
                    iPH_PK_SA_TP = Convert.ToInt32(ddlSA.SelectedValue);
                    if (a.fk_supplementalAgreementTaxParcel != iPH_PK_SA_TP)
                    {
                        bPerformSABMPInsert = true;
                    }
                    a.fk_SAAssignType_code = ddlSAA.SelectedValue;
                }
                else if ((!string.IsNullOrEmpty(ddlSAA.SelectedValue) && string.IsNullOrEmpty(ddlSA.SelectedValue)) || (string.IsNullOrEmpty(ddlSAA.SelectedValue) && !string.IsNullOrEmpty(ddlSA.SelectedValue)))
                {
                    sb.Append("Supplemental Agreement was not updated. Both Supplemental Agreement Assignment and Supplemental Agreement must be defined when attaching a Supplemental Agreement. ");
                }
                else if (string.IsNullOrEmpty(ddlSAA.SelectedValue) && string.IsNullOrEmpty(ddlSA.SelectedValue))
                {
                    if (a.fk_supplementalAgreementTaxParcel != null)
                    {
                        bPerformSABMPInsert = true;
                    }
                    a.fk_SAAssignType_code = null;
                }

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                //wac.SubmitChanges();

                ISingleResult<bmp_ag_update_expressResult> code = wac.bmp_ag_update_express(a.pk_bmp_ag, a.fk_pollutant_category_code, a.bmp_nbr, a.description, a.fk_bmpPractice_code, 
                    a.units_planned,a.units_completed,a.est_plan_prior, a.est_plan_rev, a.CREP, a.fk_BMPSource_code, a.location, a.fk_revisionDescription_code, 
                    a.fk_SAAssignType_code,a.fk_supplementalAgreementTaxParcel, a.fk_BMPTypeCode,a.fk_BMPCode_code,a.fk_BMPCREPH20_code,a.lifespan_year,
                    a.revisionDescription_addl,a.issueStatement_wfp2,a.modified_by);
                int iCode = (int)code.ReturnValue;
                if (iCode == 0)
                {
                    if (bPerformSABMPInsert)
                    {
                        int? i = null;
                        //int iCodeInsert = wac.bmp_ag_SA_add(Convert.ToInt32(fvBMPPlanning.DataKey.Value), iPH_PK_SA_TP, Session["userName"].ToString(), ref i);
                        int iCodeInsert = wac.bmp_ag_SA_add(Convert.ToInt32(fvBMPPlanning.DataKey.Value), iPH_PK_SA_TP,
                            a.fk_SAAssignType_code, Session["userName"].ToString(), ref i);
                    }

                    fvBMPPlanning.ChangeMode(FormViewMode.ReadOnly);
                    BindData_BMPPlanning_FormView(Convert.ToInt32(fvBMPPlanning.DataKey.Value));
                }
                else
                    WACAlert.Show("Update failed: ", iCode);

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvBMPPlanning_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlFarm = fvBMPPlanning.FindControl("ddlFarm") as DropDownList;
        DropDownList ddlBMPSource = fvBMPPlanning.FindControl("ddlBMPSource") as DropDownList;
        DropDownList ddlRevisionDescription = fvBMPPlanning.FindControl("ddlRevisionDescription") as DropDownList;
        DropDownList ddlPollutantCategory = fvBMPPlanning.FindControl("ddlPollutantCategory") as DropDownList;
        TextBox tbBMPNumber = fvBMPPlanning.FindControl("tbBMPNumber") as TextBox;
        TextBox tbDescription = fvBMPPlanning.FindControl("tbDescription") as TextBox;
        TextBox tbSubIssueStatement = fvBMPPlanning.FindControl("tbSubIssueStatement") as TextBox;
        DropDownList ddlBMPPractice = fvBMPPlanning.FindControl("ddlBMPPractice") as DropDownList;
        TextBox tbUnitsPlanned = fvBMPPlanning.FindControl("tbUnitsPlanned") as TextBox;
        TextBox tbPriorPlanningEstimate = fvBMPPlanning.FindControl("tbPriorPlanningEstimate") as TextBox;
        TextBox tbPlanningEstimate = fvBMPPlanning.FindControl("tbPlanningEstimate") as TextBox;
        //DropDownList ddlImplementationYear = fvBMPPlanning.FindControl("ddlImplementationYear") as DropDownList;
        TextBox tbLocation = fvBMPPlanning.FindControl("tbLocation") as TextBox;
        DropDownList ddlCREP = fvBMPPlanning.FindControl("ddlCREP") as DropDownList;
        //DropDownList ddlNMCPType = fvBMPPlanning.FindControl("ddlNMCPType") as DropDownList;
        DropDownList ddlSAA = fvBMPPlanning.FindControl("ddlSAA") as DropDownList;
        DropDownList ddlSA = fvBMPPlanning.FindControl("ddlSA") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPK_FarmBusiness = null;
                if (!string.IsNullOrEmpty(ddlFarm.SelectedValue)) iPK_FarmBusiness = Convert.ToInt32(ddlFarm.SelectedValue);
                else sb.Append("Farm is required. ");

                string sBMPSource = ddlBMPSource.SelectedValue;

                string sPollutant = null;
                if (!string.IsNullOrEmpty(ddlPollutantCategory.SelectedValue)) sPollutant = ddlPollutantCategory.SelectedValue;
                else sb.Append("Pollutant is required. ");

                string sBMPNumber = null;
                if (!string.IsNullOrEmpty(tbBMPNumber.Text)) sBMPNumber = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbBMPNumber.Text, 24);
                else sb.Append("BMP Number is required. ");

                string sDescription = null;
                if (!string.IsNullOrEmpty(tbDescription.Text)) sDescription = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 400);
                string sSubIssueStatement = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbSubIssueStatement.Text, 1000);
                decimal? dPractice = null;
                if (!string.IsNullOrEmpty(ddlBMPPractice.SelectedValue)) dPractice = Convert.ToDecimal(ddlBMPPractice.SelectedValue);
                else sb.Append("Practice is required. ");

                decimal? dUnitsPlanned = null;
                if (!string.IsNullOrEmpty(tbUnitsPlanned.Text))
                {
                    try { dUnitsPlanned = Convert.ToDecimal(tbUnitsPlanned.Text); }
                    catch { }
                }

                decimal? dEstPlanPrior = null;
                if (!string.IsNullOrEmpty(tbPriorPlanningEstimate.Text))
                {
                    try { dEstPlanPrior = Convert.ToDecimal(tbPriorPlanningEstimate.Text); }
                    catch { }
                }
                else sb.Append("Prior Planning Estimate is required. ");

                decimal? dCurrentPlanningEstimate = null;
                if (!string.IsNullOrEmpty(tbPlanningEstimate.Text))
                {
                    try { dCurrentPlanningEstimate = Convert.ToDecimal(tbPlanningEstimate.Text); }
                    catch { sb.Append("Current Planning Estimate must be a number (Decimal). "); }
                }
                else sb.Append("Current Planning Estimate is required. ");

                string sCREP = null;
                if (!string.IsNullOrEmpty(ddlCREP.SelectedValue)) sCREP = ddlCREP.SelectedValue;

                string sBMPStatus = "DR";

                //short? shScheduledImplementationYear = null;
                //if (!string.IsNullOrEmpty(ddlImplementationYear.SelectedValue)) shScheduledImplementationYear = Convert.ToInt16(ddlImplementationYear.SelectedValue);
                //else sb.Append("Implementation Year is required. ");

                //string sNMCPType = null;
                //if (!string.IsNullOrEmpty(ddlNMCPType.SelectedValue)) sNMCPType = ddlNMCPType.SelectedValue;

                string sLocation = null;
                if (!string.IsNullOrEmpty(tbLocation.Text)) sLocation = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLocation.Text, 96);

                string sRevisionDescription = null;
                if (!string.IsNullOrEmpty(ddlRevisionDescription.SelectedValue)) sRevisionDescription = ddlRevisionDescription.SelectedValue;

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
                        sb.Append("Both Supplemental Agreement Assignment and Supplemental Agreement must be defined when attaching a Supplemental Agreement. ");
                    }
                }


                string sfk_BMPSortGroup_code = null;
                //if (!string.IsNullOrEmpty(sfk_BMPSortGroup_code.Text)) sLocation = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbLocation.Text, 96);

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.bmp_ag_add_express(iPK_FarmBusiness, sPollutant, sBMPNumber, sDescription, dPractice,
                                dUnitsPlanned, dEstPlanPrior, dCurrentPlanningEstimate, sCREP, sBMPStatus, sBMPSource, sLocation, sRevisionDescription,
                                sSAAssignType, iPK_SupplementalAgreementTaxParcel, sfk_BMPSortGroup_code, null, null, sSubIssueStatement,
                                Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvBMPPlanning.ChangeMode(FormViewMode.ReadOnly);
                        BindData_BMPPlanning_FormView(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else
                {
                    sb.Insert(0, "The Insert Failed: ");
                    WACAlert.Show(sb.ToString(), 0);
                }
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    #endregion

    #region Event Handling - Implementation Years

    protected void ddlBMPPlanning_ImplementationYears_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_PLAN);
        if (b)
        {
            StringBuilder sb = new StringBuilder();

            DropDownList ddlBMPPlanning_ImplementationYears_Insert = fvBMPPlanning.FindControl("ddlBMPPlanning_ImplementationYears_Insert") as DropDownList;

            int? i = null;
            int iCode = 0;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    short? shYear = null;
                    try { shYear = Convert.ToInt16(ddlBMPPlanning_ImplementationYears_Insert.SelectedValue); }
                    catch { sb.Append("Year is required. "); }

                    iCode = wac.bmp_ag_implementation_add_express(Convert.ToInt32(fvBMPPlanning.DataKey.Value), shYear, "Y", null, Session["userName"].ToString(), ref i);
                    if (iCode == 0) BindData_BMPPlanning_FormView(Convert.ToInt32(fvBMPPlanning.DataKey.Value));
                    else WACAlert.Show("Error Returned from Database. ", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
        else WACAlert.Show("You do not have permission to insert into the BMP Planning Implementation Year table.", 0);
    }

    protected void lbBMPPlanning_ImplementationYears_Delete_Click(object sender, EventArgs e)
    {
        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_PLAN);
        if (b)
        {
            LinkButton lb = (LinkButton)sender;

            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_implementation_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                    if (iCode == 0) BindData_BMPPlanning_FormView(Convert.ToInt32(fvBMPPlanning.DataKey.Value));
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
        else WACAlert.Show("You do not have permission to insert into the BMP Planning Implementation Year table.", 0);
    }

    #endregion

    #region Data Binding

    private void BindData_BMPPlanning_GridView(int iPK_FarmBusiness)
    {
        hfPK_FarmBusiness.Value = iPK_FarmBusiness.ToString();
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.bmp_ags.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_statusBMP_code == "DR" && w.fk_programmaticRecord_code == null).
                OrderBy(o => o.bmp_nbr).Select(s => new { s.pk_bmp_ag, s.list_BMPSource, s.bmp_nbr, s.location, s.description, s.list_statusBMP, 
                    s.list_pollutant_category, s.list_bmpPractice, s.list_revisionDescription, s.units_planned, s.fk_unit_code, s.est_plan_prior, 
                    s.est_plan_rev, FARMID = s.farmBusiness.farmID, FARMNAME = s.farmBusiness.farm_name, FARMOWNER = s.farmBusiness.ownerStr_dnd });
            gvBMPPlanning.DataKeyNames = new string[] { "pk_bmp_ag" };
            gvBMPPlanning.DataSource = a;
            gvBMPPlanning.DataBind();
        }
    }

    private void BindData_BMPPlanning_FormView(decimal iPK_BMP_AG)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.bmp_ags.Where(w => w.pk_bmp_ag == iPK_BMP_AG).Select(s => new { s.pk_bmp_ag, s.fk_farmBusiness, s.bmp_nbr, s.fk_BMPSource_code, 
                s.list_BMPSource, s.location, s.description, s.list_statusBMP, s.fk_pollutant_category_code, s.list_pollutant_category, s.fk_bmpPractice_code, 
                s.list_bmpPractice, s.fk_revisionDescription_code, s.list_revisionDescription, s.units_planned, s.fk_unit_code, s.est_plan_prior, s.est_plan_rev, 
                s.CREP, s.fk_SAAssignType_code, s.list_SAAssignType, s.fk_supplementalAgreementTaxParcel, s.supplementalAgreementTaxParcel, s.issueStatement_wfp2,
                FARMID = s.farmBusiness.farmID, FARMNAME = s.farmBusiness.farm_name, FARMOWNER = s.farmBusiness.ownerStr_dnd, 
                s.created, s.created_by, s.modified, s.modified_by });
            fvBMPPlanning.DataKeyNames = new string[] { "pk_bmp_ag" };
            fvBMPPlanning.DataSource = a;
            fvBMPPlanning.DataBind();

            if (fvBMPPlanning.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvBMPPlanning.FindControl("ddlBMPPlanning_ImplementationYears_Insert") as DropDownList, null);
            }

            if (fvBMPPlanning.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_Farm_DDL(fvBMPPlanning.FindControl("ddlFarm") as DropDownList, null, false, true, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPSource_DDL(fvBMPPlanning, "ddlBMPSource", "N", false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_RevisionDescription_DDL(fvBMPPlanning, "ddlRevisionDescription", null, null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PollutantCategory_DDL(fvBMPPlanning.FindControl("ddlPollutantCategory") as DropDownList, null, false, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL(fvBMPPlanning.FindControl("ddlBMPPractice") as DropDownList, null, true, true, false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fvBMPPlanning.FindControl("ddlImplementationYear") as DropDownList, DateTime.Now.Year);
                WACGlobal_Methods.PopulateControl_DatabaseLists_SupplementalAgreementAssignment_DDL(fvBMPPlanning.FindControl("ddlSAA") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvBMPPlanning.FindControl("ddlCREP") as DropDownList, "N", true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_NMCPType_DDL(fvBMPPlanning, "ddlNMCPType", null, true);
                HandleSupplementalAgreementsBasedOnFarm(null);
            }

            if (fvBMPPlanning.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPSource_DDL(fvBMPPlanning, "ddlBMPSource", a.Single().fk_BMPSource_code, false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_RevisionDescription_DDL(fvBMPPlanning, "ddlRevisionDescription", 
                    a.Single().fk_revisionDescription_code,a.Single().list_revisionDescription.active);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PollutantCategory_DDL(fvBMPPlanning.FindControl("ddlPollutantCategory") as DropDownList, a.Single().fk_pollutant_category_code, false, false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL(fvBMPPlanning.FindControl("ddlBMPPractice") as DropDownList, a.Single().fk_bmpPractice_code, true, false, false);
                WACGlobal_Methods.PopulateControl_DatabaseLists_SupplementalAgreementAssignment_DDL(fvBMPPlanning.FindControl("ddlSAA") as DropDownList, a.Single().fk_SAAssignType_code, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvBMPPlanning.FindControl("ddlCREP") as DropDownList, a.Single().CREP, true);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_NMCPType_DDL(fvBMPPlanning, "ddlNMCPType", a.Single().fk_NMCPType_code, true);
                HandleSupplementalAgreementsBasedOnFarm(a.Single().fk_supplementalAgreementTaxParcel);

                DropDownList ddlPractice = fvBMPPlanning.FindControl("ddlBMPPractice") as DropDownList;
                Label lblUnits = fvBMPPlanning.FindControl("lblUnits") as Label;
                if (!string.IsNullOrEmpty(ddlPractice.SelectedValue)) lblUnits.Text = WACGlobal_Methods.SpecialQuery_Agriculture_Unit_By_BMPPractice(ddlPractice.SelectedValue);
            }
        }
    }

    #endregion
}
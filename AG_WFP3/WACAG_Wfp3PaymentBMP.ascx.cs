using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class AG_WACAG_Wfp3PaymentBMP : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_FarmBusiness
    {
        get { return Convert.ToInt32(Session["FK_FarmBusiness"]) == 0 ? -1 : Convert.ToInt32(Session["FK_FarmBusiness"]); }
        set { Session["FK_FarmBusiness"] = value; }
    }
    public int FK_Wfp3
    {
        get { return Convert.ToInt32(Session["FK_Wfp3"]) == 0 ? -1 : Convert.ToInt32(Session["FK_Wfp3"]); }
        set { Session["FK_Wfp3"] = value; }
    }
    public int FK_Wfp3Payment
    {
        get { return Convert.ToInt32(ViewState["FK_Wfp3Payment"]) == 0 ? -1 : Convert.ToInt32(ViewState["FK_Wfp3Payment"]); }
        set { ViewState["FK_Wfp3Payment"] = value; }
    }
    public int PK_Wfp3PaymentBMP
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3PaymentBMP"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3PaymentBMP"]); }
        set { ViewState["PK_Wfp3PaymentBMP"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3Payment = e.ForeignKey;
        PK_Wfp3PaymentBMP = e.PrimaryKey;
        fvAg_WFP3_PaymentBMP.ChangeMode(e.ViewMode);
        BindAg_WFP3_PaymentBMP();
       
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);


    #region PaymentBMP
    protected void lbAg_WFP3_PaymentBMP_Close_Click(object sender, EventArgs e)
    {
        fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.ReadOnly);
        fvAg_WFP3_PaymentBMP.DataSource = "";
        fvAg_WFP3_PaymentBMP.DataBind();
        
    }

    protected void ddlAg_WFP3_PaymentBMP_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlBMP = (DropDownList)sender;
        FormView fv = fvAg_WFP3_PaymentBMP;
        DropDownList ddlPaymentStatus = fv.FindControl("ddlPaymentStatus") as DropDownList;
        DropDownList ddlBMPPractice = fv.FindControl("ddlBMPPractice") as DropDownList;
        if (!string.IsNullOrEmpty(ddlBMP.SelectedValue))
        {
            int? iPK_BMP = WACGlobal_Methods.SpecialQuery_Agriculture_PK_BMP_By_PK_WFP3BMP(ddlBMP.SelectedValue);

            if (iPK_BMP != null)
            {
                string sStatus = WACGlobal_Methods.SpecialQuery_Agriculture_BMPStatus_By_BMP(iPK_BMP);
                if (!string.IsNullOrEmpty(sStatus)) 
                    WACGlobal_Methods.PopulateControl_DatabaseLists_PaymentStatus_DDL(ddlPaymentStatus, null, true);
                    //WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMP_DDL(fv, "ddlPaymentStatus", sStatus, true, false, true);
                else 
                    ddlPaymentStatus.Items.Clear();

                decimal? d = WACGlobal_Methods.SpecialQuery_Agriculture_BMPPracticeCode_By_BMP(iPK_BMP);
                if (d != null) 
                    WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL(ddlBMPPractice, d, true, true, false);
                else 
                    ddlBMPPractice.Items.Clear();
            }
        }
    }

    protected void fvAg_WFP3_PaymentBMP_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                bind = false;
                lbAg_WFP3_PaymentBMP_Close_Click(sender, null);
                OnFormActionCompleted(this, new FormViewEventArgs(-1, FK_Wfp3Payment, "PaymentBMP", FormViewMode.ReadOnly));
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_paymentBMP", "msgInsert"))
                    fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.Insert);
                else
                {
                    fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.ReadOnly);
                    bind = false;
                }
                break;
            case "InsertData":
                bind = true;
                fvAg_WFP3_PaymentBMP_ItemInserting(sender, null);
                fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.ReadOnly);
                OnFormActionCompleted(this, new FormViewEventArgs(PK_Wfp3PaymentBMP, FK_Wfp3Payment, "PaymentBMP", FormViewMode.Insert));
                break;
            case "CancelInsert":
                bind = false;
                lbAg_WFP3_PaymentBMP_Close_Click(sender, null);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_paymentBMP", "msgUpdate"))
                {
                    fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_PaymentBMP_ItemUpdating(sender, null);
                fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.ReadOnly);
                OnFormActionCompleted(this, new FormViewEventArgs(PK_Wfp3PaymentBMP, FK_Wfp3Payment, "PaymentBMP", FormViewMode.Edit));
                break;
            case "CancelUpdate":
                fvAg_WFP3_PaymentBMP.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                bind = false;
                fvAg_WFP3_PaymentBMP_ItemDeleting(sender, null);
                OnFormActionCompleted(this, new FormViewEventArgs(-1, FK_Wfp3Payment, "PaymentBMP", FormViewMode.ReadOnly));
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_PaymentBMP();
    }

    protected void fvAg_WFP3_PaymentBMP_ModeChanging(object sender, FormViewModeEventArgs e)
    {    }
    protected void fvAg_WFP3_PaymentBMP_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_WFP3_PaymentBMP;
        DropDownList ddlPaymentStatus = fv.FindControl("ddlPaymentStatus") as DropDownList;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;
        TextBox tbAmountFundingAgency = fv.FindControl("tbAmountFundingAgency") as TextBox;
        DropDownList ddlFundingAgency = fv.FindControl("ddlFundingAgency") as DropDownList;
        DropDownList ddlReimbursementYN = fv.FindControl("ddlReimbursementYN") as DropDownList;
        DropDownList ddlBMPPractice = fv.FindControl("ddlBMPPractice") as DropDownList;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;
        TextBox tbNMCPPurchase = fv.FindControl("tbNMCPPurchase") as TextBox;
        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.form_wfp3_paymentBMPs.Where(w => w.pk_form_wfp3_paymentBMP == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlPaymentStatus.SelectedValue)) a.fk_paymentStatus_code = ddlPaymentStatus.SelectedValue;
                else sb.Append("Payment Status was not updated. This field is required. ");

                try { a.amt = Convert.ToDecimal(tbAmount.Text); }
                catch { sb.Append("Amount was not updated. This field is required. Data type is number (Decimal). "); }

                try
                {
                    if (!string.IsNullOrEmpty(tbAmountFundingAgency.Text)) a.amt_agencyFunding = Convert.ToDecimal(tbAmountFundingAgency.Text);
                    else a.amt_agencyFunding = null;
                }
                catch { }

                if (!string.IsNullOrEmpty(ddlFundingAgency.SelectedValue)) a.fk_agencyFunding_code = ddlFundingAgency.SelectedValue;

                if (!string.IsNullOrEmpty(ddlBMPPractice.SelectedValue)) 
                    a.fk_bmpPractice_code = Convert.ToDecimal(ddlBMPPractice.SelectedValue);
                else a.fk_bmpPractice_code = null;

                if (!string.IsNullOrEmpty(tbNMCPPurchase.Text)) 
                    a.purchaseNMCP = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNMCPPurchase.Text, 255);
                else a.purchaseNMCP = null;

                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);
                else a.note = null;
                a.reimbursement = ddlReimbursementYN.SelectedValue;
                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }
    protected void fvAg_WFP3_PaymentBMP_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_PaymentBMP;

        DropDownList ddlBMP = fv.FindControl("ddlBMP") as DropDownList;
        DropDownList ddlPaymentStatus = fv.FindControl("ddlPaymentStatus") as DropDownList;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;
        TextBox tbAmountFundingAgency = fv.FindControl("tbAmountFundingAgency") as TextBox;
        DropDownList ddlFundingAgency = fv.FindControl("ddlFundingAgency") as DropDownList;
        DropDownList ddlReimbursementYN = fv.FindControl("ddlReimbursementYN") as DropDownList;
        TextBox tbNMCPPurchase = fv.FindControl("tbNMCPPurchase") as TextBox;
        DropDownList ddlBMPPractice = fv.FindControl("ddlBMPPractice") as DropDownList;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iBMP = null;
                if (!string.IsNullOrEmpty(ddlBMP.SelectedValue)) iBMP = Convert.ToInt32(ddlBMP.SelectedValue);
                else sb.Append("BMP is required. ");

                string sPaymentStatus = null;
                if (!string.IsNullOrEmpty(ddlPaymentStatus.SelectedValue)) sPaymentStatus = ddlPaymentStatus.SelectedValue;
                else sb.Append("Payment Status is required. ");

                decimal? dAmount = null;
                try { dAmount = Convert.ToDecimal(tbAmount.Text); }
                catch { sb.Append("Amount is required. Data type is number (Decimal). "); }

                decimal? dAmountFundingAgency = null;
                if (!string.IsNullOrEmpty(tbAmountFundingAgency.Text))
                {
                    try { dAmountFundingAgency = Convert.ToDecimal(tbAmountFundingAgency.Text); }
                    catch { }
                }

                string sFundingAgency = null;
                if (!string.IsNullOrEmpty(ddlFundingAgency.SelectedValue)) sFundingAgency = ddlFundingAgency.SelectedValue;

                string sReimbursement = ddlReimbursementYN.SelectedValue;
                string sNMCPPurchase = tbNMCPPurchase.Text;

                decimal? dBMPPracticeCode = null;
                if (!string.IsNullOrEmpty(ddlBMPPractice.SelectedValue)) dBMPPracticeCode = Convert.ToDecimal(ddlBMPPractice.SelectedValue);

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_paymentBMP_add(FK_Wfp3Payment, iBMP, sPaymentStatus, dAmount, dAmountFundingAgency, sFundingAgency, dBMPPracticeCode,
                        sReimbursement, sNMCPPurchase, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database. " + sb.ToString(), iCode);
                    else PK_Wfp3PaymentBMP = Convert.ToInt32(i);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }
    protected void fvAg_WFP3_PaymentBMP_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_paymentBMP", "msgDelete"))
        {
            FormView fv = fvAg_WFP3_PaymentBMP;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_paymentBMP_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }


    private void BindAg_WFP3_PaymentBMP()
    {
        FormView fv = fvAg_WFP3_PaymentBMP;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.form_wfp3_paymentBMPs.Where(w => w.pk_form_wfp3_paymentBMP == PK_Wfp3PaymentBMP) select b;
            fv.DataKeyNames = new string[] { "pk_form_wfp3_paymentBMP" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_BMP_ByWFP3BMP_DDL(fv, "ddlBMP", Convert.ToInt32(FK_Wfp3), null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_PaymentStatus_DDL(fv.FindControl("ddlPaymentStatus") as DropDownList, null, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_AgencyFunding_DDL(fv.FindControl("ddlFundingAgency") as DropDownList, null, "Y", null, true);
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP3PaymentBMP_ByWFP3Specification_DDL(fv.FindControl("ddlBMPPractice") as DropDownList, Convert.ToInt32(FK_Wfp3), null, true, true);    
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv, "ddlReimbursementYN", "N", true);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                decimal? bmp = a.Any() ? a.Single().fk_bmpPractice_code : null;
                WACGlobal_Methods.PopulateControl_DatabaseLists_PaymentStatus_DDL(fv.FindControl("ddlPaymentStatus") as DropDownList, a.Single().fk_paymentStatus_code, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_AgencyFunding_DDL(fv.FindControl("ddlFundingAgency") as DropDownList, null, "Y", a.Single().fk_agencyFunding_code, true);
                //WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP3PaymentBMP_ByWFP3Specification_DDL(fv.FindControl("ddlBMPPractice") as DropDownList, Convert.ToInt32(FK_Wfp3), null, true, true);    
                //WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP3PaymentBMP_ByWFP3Specification_DDL(fv.FindControl("ddlBMPPractice") as DropDownList, Convert.ToInt32(FK_Wfp3), a.Single().fk_bmpPractice_code, true, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_BMPPractice_DDL((DropDownList)fv.FindControl("ddlBMPPractice"), bmp, true, true, false);
                DropDownList ddlReimbursementYN = fv.FindControl("ddlReimbursementYN") as DropDownList;
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv.FindControl("ddlReimbursementYN") as DropDownList,a.Single().reimbursement, true);
                
            }
        }
    }
    #endregion

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WAC_CustomControls;
public partial class AG_WACAG_Wfp3Payment : System.Web.UI.UserControl
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
    public int PK_Wfp3Payment
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Payment"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Payment"]); }
        set { ViewState["PK_Wfp3Payment"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Payment = e.PrimaryKey;
        fvAg_WFP3_Payment.ChangeMode(e.ViewMode);
        BindAg_WFP3_Payment();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    public event EventHandler ViewLinkButtonClicked;
    public event EventHandler InsertLinkButtonClicked;


    #region Payment
    protected void lbAg_WFP3_PaymentBMP_AddClicked(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        lb.CommandArgument = PK_Wfp3Payment.ToString();
        InsertLinkButtonClicked(sender, e);
    }
    protected void lbAg_WFP3_PaymentBMP_ViewClicked(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        ViewLinkButtonClicked(sender, e);
    }

    protected void lbAg_WFP3_Payment_Close_Click(object sender, EventArgs e)
    {
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "Payment"));
        FormView fv = fvAg_WFP3_Payment;
        fv.DataSource = "";
        fv.DataBind();
        
    }

    protected void fvAg_WFP3_Payment_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_Payment_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_payment", "msgInsert"))
                    fvAg_WFP3_Payment.ChangeMode(FormViewMode.Insert);
                else
                    fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                bind = false;
                break;
            case "InsertData":
                fvAg_WFP3_Payment_ItemInserting(sender, null);
                fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelInsert":
                fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_payment", "msgInsert") &&
                    WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_paymentBMP", "msgUpdate"))
                {
                    fvAg_WFP3_Payment.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_Payment_ItemUpdating(sender, null);
                fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelUpdate":
                fvAg_WFP3_Payment.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_Payment_ItemDeleting(sender, null);
                lbAg_WFP3_Payment_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_Payment();
    }

    protected void fvAg_WFP3_Payment_ModeChanging(object sender, FormViewModeEventArgs e)
    {
      
    }

    protected void fvAg_WFP3_Payment_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FormView fv = fvAg_WFP3_Payment;

        DropDownList ddlPayee = fv.FindControl("ddlPayee") as DropDownList;
        DropDownList ddlInvoice = fv.FindControl("ddlInvoice") as DropDownList;
        CustomControls_AjaxCalendar tbCalPaymentDate = fv.FindControl("tbCalPaymentDate") as CustomControls_AjaxCalendar;
        TextBox tbCheckNumber = fv.FindControl("tbCheckNumber") as TextBox;
        DropDownList ddlEncumbrance = fv.FindControl("ddlEncumbrance") as DropDownList;
        //DropDownList ddlIsContractor = fv.FindControl("ddlIsContractor") as DropDownList;
        //DropDownList ddlFlood2006 = fv.FindControl("ddlFlood2006") as DropDownList;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.form_wfp3_payments.Where(w => w.pk_form_wfp3_payment == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlPayee.SelectedValue)) a.fk_participant_payee = Convert.ToInt32(ddlPayee.SelectedValue);
                else sb.Append("Payee was not updated. This is a required field. ");

                if (!string.IsNullOrEmpty(ddlInvoice.SelectedValue)) a.fk_form_wfp3_invoice = Convert.ToInt32(ddlInvoice.SelectedValue);
                else a.fk_form_wfp3_invoice = null;

                a.date = (DateTime)tbCalPaymentDate.CalDateNullable;

                if (!string.IsNullOrEmpty(tbCheckNumber.Text)) a.check_nbr = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCheckNumber.Text, 16);
                else sb.Append("Check Number was not updated. This is a required field. ");

                if (!string.IsNullOrEmpty(ddlEncumbrance.SelectedValue)) a.fk_encumbrance_code = ddlEncumbrance.SelectedValue;
                else sb.Append("Encumbrance was not updated. This is a required field. ");

                //if (!string.IsNullOrEmpty(ddlIsContractor.SelectedValue)) a.is_contractor = ddlIsContractor.SelectedValue;
                //else a.is_contractor = null;

                //if (!string.IsNullOrEmpty(ddlFlood2006.SelectedValue)) a.flood_2006 = ddlFlood2006.SelectedValue;
                //else a.flood_2006 = null;

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 400);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Payment_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_Payment;

        DropDownList ddlPayee = fv.FindControl("ddlPayee") as DropDownList;
        DropDownList ddlInvoice = fv.FindControl("ddlInvoice") as DropDownList;
        CustomControls_AjaxCalendar tbCalPaymentDate = fv.FindControl("tbCalPaymentDate") as CustomControls_AjaxCalendar;
        TextBox tbCheckNumber = fv.FindControl("tbCheckNumber") as TextBox;
        DropDownList ddlEncumbrance = fv.FindControl("ddlEncumbrance") as DropDownList;
        //DropDownList ddlIsContractor = fv.FindControl("ddlIsContractor") as DropDownList;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iPayee = null;
                if (!string.IsNullOrEmpty(ddlPayee.SelectedValue)) iPayee = Convert.ToInt32(ddlPayee.SelectedValue);
                else sb.Append("Payee is required. ");

                int? iInvoice = null;
                if (!string.IsNullOrEmpty(ddlInvoice.SelectedValue)) iInvoice = Convert.ToInt32(ddlInvoice.SelectedValue);

                DateTime? dtDate = tbCalPaymentDate.CalDateNullable;
               
                string sCheckNumber = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbCheckNumber.Text, 16);
                if (string.IsNullOrEmpty(sCheckNumber)) sb.Append("Check Number is required. ");

                string sEncumbrance = null;
                if (!string.IsNullOrEmpty(ddlEncumbrance.SelectedValue)) sEncumbrance = ddlEncumbrance.SelectedValue;
                else sb.Append("Encumbrance is required. ");

                //string sIsContractor = null;
                //if (!string.IsNullOrEmpty(ddlIsContractor.SelectedValue)) sIsContractor = ddlIsContractor.SelectedValue;

                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 400);

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_payment_add(FK_Wfp3, iPayee, iInvoice, dtDate, sCheckNumber, sEncumbrance, sNote, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database. " + sb.ToString(), iCode);
                    else PK_Wfp3Payment = Convert.ToInt32(i);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Payment_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_payment", "msgDelete"))
        {
            FormView fv = fvAg_WFP3_Payment;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_payment_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }
    private void BindAg_WFP3_Payment()
    {
        FormView fv = fvAg_WFP3_Payment;
       
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.form_wfp3_payments.Where(w => w.pk_form_wfp3_payment == PK_Wfp3Payment) select b;
            fv.DataKeyNames = new string[] { "pk_form_wfp3_payment" };
            fv.DataSource = a;
            fv.DataBind();
             
            ListView lv = lvAg_WFP3_PaymentBMPs;
       
            var c = from b in wDataContext.form_wfp3_paymentBMPs.Where(w => w.fk_form_wfp3_payment == PK_Wfp3Payment) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_paymentBMP" };
            lv.DataSource = c;
            lv.DataBind();
       
            string sRegionWAC = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(FK_FarmBusiness);

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                int? iBidWinningContractor = WACGlobal_Methods.SpecialQuery_Agriculture_BidWinningContractor_ByWFP3Package(FK_Wfp3);

                WACGlobal_Methods.PopulateControl_Participant_DBView_DDL(fv.FindControl("ddlPayee") as DropDownList, iBidWinningContractor, new string[] { "A", "C" }, null, null, new string[] { sRegionWAC }, null, null, null, false, false, false, false, false, false, WACGlobal_Methods.Enum_Participant_Forms.ALL, true);
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP3_InvoiceByWFP3(fv.FindControl("ddlInvoice") as DropDownList, FK_Wfp3, null);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Encumbrance_DDL(fv, "ddlEncumbrance", string.Empty, true);
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv, "ddlIsContractor", string.Empty);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Participant_DBView_DDL(fv.FindControl("ddlPayee") as DropDownList, a.Single().fk_participant_payee, new string[] { "A", "C" }, null, null, new string[] { sRegionWAC }, null, null, null, false, false, false, false, false, false, WACGlobal_Methods.Enum_Participant_Forms.ALL, true);
                WACGlobal_Methods.PopulateControl_Custom_Agriculture_WFP3_InvoiceByWFP3(fv.FindControl("ddlInvoice") as DropDownList, a.Single().fk_form_wfp3, a.Single().fk_form_wfp3_invoice);
                WACGlobal_Methods.PopulateControl_DatabaseLists_Encumbrance_DDL(fv, "ddlEncumbrance", a.Single().fk_encumbrance_code, false);
                //WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv, "ddlIsContractor", a.Single().is_contractor);
                //WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fv, "ddlFlood2006", a.Single().flood_2006);
            }
        }
    }
    #endregion


}
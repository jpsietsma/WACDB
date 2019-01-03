using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WAC_CustomControls;

public partial class AG_WACAG_Wfp3Invoice : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    public int FK_Wfp3
    {
        get { return Convert.ToInt32(Session["FK_Wfp3"]) == 0 ? -1 : Convert.ToInt32(Session["FK_Wfp3"]); }
        set { Session["FK_Wfp3"] = value; }
    }
    public int PK_Wfp3Invoice
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Invoice"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Invoice"]); }
        set { ViewState["PK_Wfp3Invoice"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Invoice = e.PrimaryKey;
        fvAg_WFP3_Invoice.ChangeMode(e.ViewMode);
        BindAg_WFP3_Invoice();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    #region Event Handling - Ag - WFP3 - Invoice

    protected void lbAg_WFP3_Invoice_Close_Click(object sender, EventArgs e)
    {
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "Invoice"));
        FormView fv = fvAg_WFP3_Invoice;
        fv.DataSource = "";
        fv.DataBind();
        
    }

    protected void fvAg_WFP3_Invoice_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_Invoice_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_invoice", "msgInsert"))
                    fvAg_WFP3_Invoice.ChangeMode(FormViewMode.Insert);
                else
                    fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                bind = false;
                break;
            case "InsertData":
                fvAg_WFP3_Invoice_ItemInserting(sender, null);
                fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelInsert":
                fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_invoice", "msgInsert"))
                {
                    fvAg_WFP3_Invoice.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_Invoice_ItemUpdating(sender, null);
                fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelUpdate":
                fvAg_WFP3_Invoice.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_Invoice_ItemDeleting(sender, null);
                lbAg_WFP3_Invoice_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_Invoice();
    }
    protected void fvAg_WFP3_Invoice_ModeChanging(object sender, FormViewModeEventArgs e)
    { }

    protected void fvAg_WFP3_Invoice_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        FormView fv = fvAg_WFP3_Invoice;
        CustomControls_AjaxCalendar invoiceDate = fv.FindControl("tbCalInvoiceDate") as CustomControls_AjaxCalendar;
        TextBox tbInvoiceNumber = fv.FindControl("tbInvoiceNumber") as TextBox;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_invoices.Where(w => w.pk_form_wfp3_invoice == Convert.ToInt32(fv.DataKey.Value)).Select(s => s).Single();
            try
            {
                a.date = (DateTime)invoiceDate.CalDateNullable;

                if (!string.IsNullOrEmpty(tbInvoiceNumber.Text)) a.invoice_nbr = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbInvoiceNumber.Text, 48);
                else sb.Append("Invoice Number was not updated. Invoice Number is required. ");


                if (!string.IsNullOrEmpty(tbAmount.Text))
                {
                    try { a.amt = Convert.ToDecimal(tbAmount.Text); }
                    catch { sb.Append("Amount was not updated. Amount must be a number (Decimal). "); }
                }
                else sb.Append("Amount was not updated. Amount is required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Invoice_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_Invoice;

        CustomControls_AjaxCalendar invoiceDate = fv.FindControl("tbCalInvoiceDate") as CustomControls_AjaxCalendar;
        TextBox tbInvoiceNumber = fv.FindControl("tbInvoiceNumber") as TextBox;
        TextBox tbAmount = fv.FindControl("tbAmount") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                DateTime? dtDate = invoiceDate.CalDateNullable;

                string sInvoiceNumber = null;
                if (!string.IsNullOrEmpty(tbInvoiceNumber.Text)) sInvoiceNumber = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbInvoiceNumber.Text, 48);
                else sb.Append("Invoice Number is required. ");

                decimal? dAmount = null;
                if (!string.IsNullOrEmpty(tbAmount.Text))
                {
                    try { dAmount = Convert.ToDecimal(tbAmount.Text); }
                    catch { sb.Append("Amount must be a number (Decimal). "); }
                }
                else sb.Append("Amount is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_invoice_add(FK_Wfp3, dtDate, sInvoiceNumber, dAmount, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                    else PK_Wfp3Invoice = Convert.ToInt32(i);
                }
                else WACAlert.Show(sb.ToString(), iCode);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Invoice_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_invoice", "msgDelete"))
        {
            FormView fv = fvAg_WFP3_Invoice;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_invoice_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    private void BindAg_WFP3_Invoice()
    {
        FormView fv = fvAg_WFP3_Invoice;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_invoices.Where(w => w.pk_form_wfp3_invoice == PK_Wfp3Invoice).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_form_wfp3_invoice" };
            fv.DataSource = a;
            fv.DataBind();
        }
    }
}
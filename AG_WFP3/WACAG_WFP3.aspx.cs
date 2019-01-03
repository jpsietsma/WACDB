using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WAC_CustomControls;
using WAC_DataObjects;

public partial class AG_WFP3_WACAG_WFP3 : System.Web.UI.Page
{
    bool viewMode = true;
    //public event AG_WACAG_WFP3.OpenFormViewEventHandler OnWFP3Clicked;
    public event OpenFormViewEventHandler OnBMPFormClicked;
    public event OpenFormViewEventHandler OnBidFormClicked;
    public event OpenFormViewEventHandler OnEncumbranceFormClicked;
    public event OpenFormViewEventHandler OnInvoiceFormClicked;
    public event OpenFormViewEventHandler OnModFormClicked;
    public event OpenFormViewEventHandler OnPaymentFormClicked;
    public event OpenFormViewEventHandler OnPaymentBMPFormClicked;
    public event OpenFormViewEventHandler OnSpecFormClicked;
    public event OpenFormViewEventHandler OnTechFormClicked;
    public event FormActionCompletedEventHandler OnCloseClicked;
    //public event OpenFormViewEventHandler ShowModal;
    public delegate void OpenFormViewEventHandler(object sender, FormViewEventArgs e);
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);

    public int Delegate_GetFarmBusinessPK()
    {
        return Convert.ToInt32(Session["PK_FarmBusiness"]);
    }
    public int Delegate_GetFormWFP3PK()
    {
        return Convert.ToInt32(Session["PK_FormWFP3"]);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PK_FormWfp3 = Convert.ToInt32(Request.QueryString["PK_FormWFp3"]);
            FK_FarmBusiness = Convert.ToInt32(Request.QueryString["FK_FarmBusiness"]);        
            OpenFormView(Convert.ToBoolean(Request.QueryString["AddPackage"]));
            System.Web.UI.HtmlControls.HtmlGenericControl myJs = new System.Web.UI.HtmlControls.HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript"); //don't need it usually but for cross browser.
            myJs.Attributes.Add("src", ResolveUrl("~/Global.js"));
            this.Page.Header.Controls.Add(myJs);
        }
       
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        WireUp();
    }
    private void WireUp()
    {
        AG_WACAG_Wfp3Bmp BMP = pnlSubPanels.FindControl("ucWfp3Bmp") as AG_WACAG_Wfp3Bmp;
        AG_WACAG_Wfp3Bid Bid = pnlSubPanels.FindControl("ucWfp3Bid") as AG_WACAG_Wfp3Bid;
        AG_WACAG_Wfp3Encumbrance Encumb = pnlSubPanels.FindControl("ucWfp3Encumbrance") as AG_WACAG_Wfp3Encumbrance;
        AG_WACAG_Wfp3Invoice Invoice = pnlSubPanels.FindControl("ucWfp3Invoice") as AG_WACAG_Wfp3Invoice;
        AG_WACAG_Wfp3Payment Payment = pnlSubPanels.FindControl("ucWfp3Payment") as AG_WACAG_Wfp3Payment;
        AG_WACAG_Wfp3PaymentBMP PaymentBMP = pnlSubPanels.FindControl("ucWfp3PaymentBMP") as AG_WACAG_Wfp3PaymentBMP;
        AG_WACAG_Wfp3Modification Mod = pnlSubPanels.FindControl("ucWfp3Mod") as AG_WACAG_Wfp3Modification;
        AG_WACAG_Wfp3Specification Spec = pnlSubPanels.FindControl("ucWfp3Spec") as AG_WACAG_Wfp3Specification;
        AG_WACAG_Wfp3Tech Tech = pnlSubPanels.FindControl("ucWfp3Tech") as AG_WACAG_Wfp3Tech;
        OnBMPFormClicked += new OpenFormViewEventHandler(BMP.OpenFormView);
        OnBidFormClicked += new OpenFormViewEventHandler(Bid.OpenFormView);
        OnEncumbranceFormClicked += new OpenFormViewEventHandler(Encumb.OpenFormView);
        OnInvoiceFormClicked += new OpenFormViewEventHandler(Invoice.OpenFormView);
        OnPaymentFormClicked += new OpenFormViewEventHandler(Payment.OpenFormView);
        OnPaymentBMPFormClicked += new OpenFormViewEventHandler(PaymentBMP.OpenFormView);
        OnModFormClicked += new OpenFormViewEventHandler(Mod.OpenFormView);
        OnSpecFormClicked += new OpenFormViewEventHandler(Spec.OpenFormView);
        OnTechFormClicked += new OpenFormViewEventHandler(Tech.OpenFormView);

        BMP.OnFormActionCompleted += new AG_WACAG_Wfp3Bmp.FormActionCompletedEventHandler(this.OnSubFormClose);
        PaymentBMP.OnFormActionCompleted += new AG_WACAG_Wfp3PaymentBMP.FormActionCompletedEventHandler(this.OnSubFormClose);
        Bid.OnFormActionCompleted += new AG_WACAG_Wfp3Bid.FormActionCompletedEventHandler(this.OnSubFormClose);
        Encumb.OnFormActionCompleted += new AG_WACAG_Wfp3Encumbrance.FormActionCompletedEventHandler(this.OnSubFormClose);
        Invoice.OnFormActionCompleted += new AG_WACAG_Wfp3Invoice.FormActionCompletedEventHandler(this.OnSubFormClose);
        Payment.OnFormActionCompleted += new AG_WACAG_Wfp3Payment.FormActionCompletedEventHandler(this.OnSubFormClose);
        Payment.InsertLinkButtonClicked += new EventHandler(this.lbWFP3_Subtable_Insert_Click);
        Payment.ViewLinkButtonClicked += new EventHandler(this.lbWFP3_Subtable_View_Click);
        Mod.OnFormActionCompleted += new AG_WACAG_Wfp3Modification.FormActionCompletedEventHandler(this.OnSubFormClose);
        Spec.OnFormActionCompleted += new AG_WACAG_Wfp3Specification.FormActionCompletedEventHandler(this.OnSubFormClose);
        Tech.OnFormActionCompleted += new AG_WACAG_Wfp3Tech.FormActionCompletedEventHandler(this.OnSubFormClose);
    }
  
    public int FK_FarmBusiness
    {
        get { return Convert.ToInt32(Session["FK_FarmBusiness"]) == 0 ? -1 : Convert.ToInt32(Session["FK_FarmBusiness"]); }
        set { Session["FK_FarmBusiness"] = value; }
    }
    public int PK_FormWfp3
    {
        get { return Convert.ToInt32(Session["PK_FormWfp3"]) == 0 ? -1 : Convert.ToInt32(Session["PK_FormWfp3"]); }
        set { Session["PK_FormWfp3"] = value; }
    }
    public int PK_Wfp3Bmp
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Bmp"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Bmp"]); }
        set { ViewState["PK_Wfp3Bmp"] = value; }
    }
    public int PK_Wfp3Payment
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Payment"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Payment"]); }
        set { ViewState["PK_Wfp3Payment"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_FarmBusiness = e.ForeignKey;
        PK_FormWfp3 = e.PrimaryKey;
        fvAg_WFP3.ChangeMode(e.ViewMode);
        if (e.ViewMode == FormViewMode.Insert)
            SubPanelsOff();
        else
            SubPanelsOn();
        BindAg_WFP3();
        
      //  UC_DocumentArchive_A_WFP3.SetupViewer();
        //ShowModal(null, null);
        mpeAg_WFP3.Show();
        upAg_WFP3.Update();
    }
    public void OpenFormView(bool newPackage)
    {
        if (newPackage)
        {
            if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3", "msgInsert"))
            {
                fvAg_WFP3.ChangeMode(FormViewMode.Insert);
                SubPanelsOff();
            }
        }
        else
        {
            fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
            SubPanelsOn();
        }
     //   UC_DocumentArchive_A_WFP3.SetupViewer();
        BindAg_WFP3();
        mpeAg_WFP3.Show();
        upAg_WFP3.Update();
    }
    private void SubPanelsOff()
    {
        pnlSubPanels.Visible = false;
    }
    private void SubPanelsOn()
    {
        pnlSubPanels.Visible = true;
    }
    protected void OnSubFormClose(object sender, FormViewEventArgs e)
    {
        string formName = e.FormType as string;
        switch (formName)
        {
            case "BMP":
                pnlSubPanels.FindControl("pnlWfp3Bmp").Visible = false;
                break;
            case "Bid":
                pnlSubPanels.FindControl("pnlWfp3Bid").Visible = false;
                break;
            case "Encumbrance":
                pnlSubPanels.FindControl("pnlWfp3Encumbrance").Visible = false;
                break;
            case "Invoice":
                pnlSubPanels.FindControl("pnlWfp3Invoice").Visible = false;
                break;
            case "Payment":
                pnlSubPanels.FindControl("pnlWfp3PaymentBMP").Visible = false;
                pnlSubPanels.FindControl("pnlWfp3Payment").Visible = false;
                break;
            case "PaymentBMP":
                pnlSubPanels.FindControl("pnlWfp3PaymentBMP").Visible = e.ViewMode != FormViewMode.ReadOnly;
                LinkButton lb = new LinkButton();
                lb.ID = "lb_temp_Payment_view";
                lb.CommandArgument = e.ForeignKey.ToString();
                lbWFP3_Subtable_View_Click(lb, null);
                if (pnlSubPanels.FindControl("pnlWfp3PaymentBMP").Visible)
                {
                    lb.ID = "lb_temp_PaymentBMP_view";
                    lb.CommandArgument = e.PrimaryKey.ToString();
                    lbWFP3_Subtable_View_Click(lb, null);
                }
                break;
            case "Modification":
                pnlSubPanels.FindControl("pnlWfp3Mod").Visible = false;
                break;
            case "Specification":
                pnlSubPanels.FindControl("pnlWfp3Spec").Visible = false;
                break;
            case "Technician":
                pnlSubPanels.FindControl("pnlWfp3Tech").Visible = false;
                break;
            default:
                break;
        }
        BindAg_WFP3();
        // upAg_WFP3.Update();
    }

    protected void lbWFP3_Subtable_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string subTable = lb.ID.Split('_')[2];
        switch (subTable)
        {
            case "BMP":
                pnlSubPanels.FindControl("pnlWfp3Bmp").Visible = true;
                OnBMPFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            case "Bid":
                pnlSubPanels.FindControl("pnlWfp3Bid").Visible = true;
                OnBidFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            case "Encumbrance":
                pnlSubPanels.FindControl("pnlWfp3Encumbrance").Visible = true;
                OnEncumbranceFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            case "Invoice":
                pnlSubPanels.FindControl("pnlWfp3Invoice").Visible = true;
                OnInvoiceFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            case "Payment":
                PK_Wfp3Payment = Convert.ToInt32(lb.CommandArgument);
                pnlSubPanels.FindControl("pnlWfp3Payment").Visible = true;
                OnPaymentFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            case "PaymentBMP":
                pnlSubPanels.FindControl("pnlWfp3PaymentBMP").Visible = true;
                OnPaymentBMPFormClicked(sender, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_Wfp3Payment, FormViewMode.ReadOnly));
                break;
            case "Modification":
                pnlSubPanels.FindControl("pnlWfp3Mod").Visible = true;
                OnModFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            case "Specification":
                pnlSubPanels.FindControl("pnlWfp3Spec").Visible = true;
                OnSpecFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            case "Technician":
                pnlSubPanels.FindControl("pnlWfp3Tech").Visible = true;
                OnTechFormClicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FormWfp3, FormViewMode.ReadOnly));
                break;
            default:
                break;
        }
        //mpeAg_WFP3.Show();
        //upAg_WFP3.Update();

    }

    protected void lbWFP3_Subtable_Insert_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string subTable = lb.ID.Split('_')[2];
        switch (subTable)
        {
            case "BMP":
                pnlSubPanels.FindControl("pnlWfp3Bmp").Visible = true;
                OnBMPFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            case "Bid":
                pnlSubPanels.FindControl("pnlWfp3Bid").Visible = true;
                OnBidFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            case "Encumbrance":
                pnlSubPanels.FindControl("pnlWfp3Encumbrance").Visible = true;
                OnEncumbranceFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            case "Invoice":
                pnlSubPanels.FindControl("pnlWfp3Invoice").Visible = true;
                OnInvoiceFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            case "Payment":
                pnlSubPanels.FindControl("pnlWfp3Payment").Visible = true;
                OnPaymentFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            case "PaymentBMP":
                pnlSubPanels.FindControl("pnlWfp3PaymentBMP").Visible = true;
                OnPaymentBMPFormClicked(this, new FormViewEventArgs(-1, Convert.ToInt32(lb.CommandArgument), FormViewMode.Insert));
                break;
            case "Modification":
                pnlSubPanels.FindControl("pnlWfp3Mod").Visible = true;
                OnModFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            case "Specification":
                pnlSubPanels.FindControl("pnlWfp3Spec").Visible = true;
                OnSpecFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            case "Technician":
                pnlSubPanels.FindControl("pnlWfp3Tech").Visible = true;
                OnTechFormClicked(this, new FormViewEventArgs(-1, PK_FormWfp3, FormViewMode.Insert));
                break;
            default:
                break;
        }
        //mpeAg_WFP3.Show();
        //upAg_WFP3.Update();
    }

    #region Event Handling - Ag - WFP3

    protected void lbAg_WFP3_Close_Click(object sender, EventArgs e)
    {
        //OnCloseClicked(this, new FormViewEventArgs(FK_FarmBusiness, null));
        fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
        PK_FormWfp3 = -1;
        fvAg_WFP3.DataSource = "";
        fvAg_WFP3.DataBind();
        string queryString = "pk=" + FK_FarmBusiness + "&tc=WFP3";
        Response.Redirect("~/AG/WACAgriculture.aspx?" + queryString);
    }

    protected void fvAg_WFP3_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
                SubPanelsOn();
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3", "msgInsert"))
                {
                    fvAg_WFP3.ChangeMode(FormViewMode.Insert);
                    SubPanelsOff();
                }
                else
                    fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "InsertData":
                if (fvAg_WFP3_ItemInserting(sender, null) == 0)
                    SubPanelsOn();
                bind = false;
                break;
            case "CancelInsert":
                lbAg_WFP3_Close_Click(null, null);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3", "msgInsert"))
                    fvAg_WFP3.ChangeMode(FormViewMode.Edit);
                else
                    fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_ItemUpdating(sender, null);
                break;
            case "CancelUpdate":
                fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_ItemDeleting(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3();
    }

    protected void fvAg_WFP3_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        fvAg_WFP3.ChangeMode(e.NewMode);
        BindAg_WFP3();
    }

    protected void fvAg_WFP3_ItemUpdating(object sender, EventArgs e)
    {
        StringBuilder sbErrorCollection = new StringBuilder();
        CustomControls_AjaxCalendar tbCalDesignDate = fvAg_WFP3.FindControl("tbCalDesignDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProcurementPlanDate = fvAg_WFP3.FindControl("tbCalProcurementPlanDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalOutForBidDate = fvAg_WFP3.FindControl("tbCalOutForBidDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalInstallToDate = fvAg_WFP3.FindControl("tbCalInstallToDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalInstallFromDate = fvAg_WFP3.FindControl("tbCalInstallFromDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalConstructionDate = fvAg_WFP3.FindControl("tbCalConstructionDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalCertificationDate = fvAg_WFP3.FindControl("tbCalCertificationDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalPrintDate = fvAg_WFP3.FindControl("tbCalPrintDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalBidDeadlineDate = fvAg_WFP3.FindControl("tbCalBidDeadlineDate") as CustomControls_AjaxCalendar;
        TextBox tbDescription = fvAg_WFP3.FindControl("tbDescription") as TextBox;
        DropDownList ddlSpecialProvisions = fvAg_WFP3.FindControl("ddlSpecialProvisions") as DropDownList;
        TextBox tbSpecialProvisionsCount = fvAg_WFP3.FindControl("tbSpecialProvisionsCount") as TextBox;
        TextBox tbAttachedPages = fvAg_WFP3.FindControl("tbAttachedPages") as TextBox;
        TextBox tbAttachedSpecifications = fvAg_WFP3.FindControl("tbAttachedSpecifications") as TextBox;
        DropDownList ddlProcurementType = fvAg_WFP3.FindControl("ddlProcurementType") as DropDownList;
        TextBox tbMessage2FreeForm = fvAg_WFP3.FindControl("tbMessage2FreeForm") as TextBox;
        TextBox tbNote = fvAg_WFP3.FindControl("tbNote") as TextBox;
        TextBox tbProjectLocation = fvAg_WFP3.FindControl("tbProjectLocation") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.form_wfp3s.Where(w => w.pk_form_wfp3 == Convert.ToInt32(fvAg_WFP3.DataKey.Value)).Select(s => s).Single();

                a.description = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 255);
                a.design_date = tbCalDesignDate.CalDateNullable;
                a.procurementPlan_date = tbCalProcurementPlanDate.CalDateNullable;
                a.outForBid_date = tbCalOutForBidDate.CalDateNullable;
                a.install_from = tbCalInstallFromDate.CalDateNullable;
                a.install_to = tbCalInstallToDate.CalDateNullable;
                a.construction_date = tbCalConstructionDate.CalDateNullable;
                a.certification_date = tbCalCertificationDate.CalDateNullable;
                a.print_date = tbCalPrintDate.CalDateNullable;
                a.bid_deadline_date = tbCalBidDeadlineDate.CalDateNullable;

                if (!string.IsNullOrEmpty(ddlSpecialProvisions.SelectedValue)) a.specialProvisions = ddlSpecialProvisions.SelectedValue;
                else a.specialProvisions = null;

                a.attachedPages_cnt = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAttachedPages.Text, 4);

                if (!string.IsNullOrEmpty(tbAttachedSpecifications.Text)) a.attachedSpecifications = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAttachedSpecifications.Text, 255);
                else a.attachedSpecifications = null;

                if (!string.IsNullOrEmpty(ddlProcurementType.SelectedValue)) a.fk_procurementType_code = ddlProcurementType.SelectedValue;
                else sbErrorCollection.Append("Procurement Type is required.");

                a.message2_freeform = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbMessage2FreeForm.Text, 255);

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();
 
                if (!string.IsNullOrEmpty(sbErrorCollection.ToString()))
                {
                    WACAlert.Show(sbErrorCollection.ToString(), 0);
                }
                else
                {
                    int iCode = wDataContext.form_wfp3_update(a.pk_form_wfp3, a.description, a.specialProvisions, a.install_from, a.install_to,
                          a.print_date, a.bid_deadline_date, a.design_date, a.procurementPlan_date, a.outForBid_date, a.construction_date, a.certification_date,
                          a.attachedPages_cnt, a.attachedSpecifications, a.fk_procurementType_code, a.message2_freeform, a.note, a.modified_by);
                    //wDataContext.SubmitChanges();
                    if (iCode != 0)
                        WACAlert.Show("Update failed", iCode);
                    fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
                    BindAg_WFP3();
                }
                
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected int fvAg_WFP3_ItemInserting(object sender, EventArgs e)
    {
        int? i = null;
        int iCode = -1;

        CustomControls_AjaxCalendar tbCalDesignDate = fvAg_WFP3.FindControl("tbCalDesignDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProcurementPlanDate = fvAg_WFP3.FindControl("tbCalProcurementPlanDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalOutForBidDate = fvAg_WFP3.FindControl("tbCalOutForBidDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalBidDeadlineDate = fvAg_WFP3.FindControl("tbCalBidDeadlineDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalInstallToDate = fvAg_WFP3.FindControl("tbCalInstallToDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalInstallFromDate = fvAg_WFP3.FindControl("tbCalInstallFromDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalConstructionDate = fvAg_WFP3.FindControl("tbCalConstructionDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalPrintDate = fvAg_WFP3.FindControl("tbCalPrintDate") as CustomControls_AjaxCalendar;
        DropDownList ddlSpecialProvisions = fvAg_WFP3.FindControl("ddlSpecialProvisions") as DropDownList;
        TextBox tbDescription = fvAg_WFP3.FindControl("tbDescription") as TextBox;
        TextBox tbAttachedPages = fvAg_WFP3.FindControl("tbAttachedPages") as TextBox;
        TextBox tbAttachedSpecifications = fvAg_WFP3.FindControl("tbAttachedSpecifications") as TextBox;
        DropDownList ddlProcurementType = fvAg_WFP3.FindControl("ddlProcurementType") as DropDownList;
        TextBox tbNote = fvAg_WFP3.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sProcurementType = null;
                if (!string.IsNullOrEmpty(ddlProcurementType.SelectedValue)) sProcurementType = ddlProcurementType.SelectedValue;
                if (string.IsNullOrEmpty(sProcurementType)) sb.Append("Procurement Type is required. ");

                if (!string.IsNullOrEmpty(sProcurementType))
                {
                    string sDescription = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbDescription.Text, 255);

                    DateTime? dtDesignDate = tbCalDesignDate.CalDateNullable;
                   
                    DateTime? dtProcurementPlanDate = tbCalProcurementPlanDate.CalDateNullable;
                    
                    DateTime? dtOutForBid = tbCalOutForBidDate.CalDateNullable;
                    
                    DateTime? dtBidDeadlineDate = tbCalBidDeadlineDate.CalDateNullable;

                    DateTime? dtUnderConstruction = tbCalConstructionDate.CalDateNullable;
                   
                    string sSpecialProvisions = "N";
                    if (!string.IsNullOrEmpty(ddlSpecialProvisions.SelectedValue)) sSpecialProvisions = ddlSpecialProvisions.SelectedValue;

                    DateTime? dtInstallFrom = tbCalInstallFromDate.CalDateNullable;
                 
                    DateTime? dtInstallTo = tbCalInstallToDate.CalDateNullable;
                  
                    DateTime? dtPrintDate = tbCalPrintDate.CalDateNullable;

                    string sAttachedPages = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAttachedPages.Text, 4);

                    string sAttachedSpecifications = null;
                    if (!string.IsNullOrEmpty(tbAttachedSpecifications.Text))
                        sAttachedSpecifications = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbAttachedSpecifications.Text, 255);

                    string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                    iCode = wDataContext.form_wfp3_add_express(Convert.ToInt32(FK_FarmBusiness), sDescription, sProcurementType, dtDesignDate, dtPrintDate,
                        dtProcurementPlanDate, dtBidDeadlineDate, dtOutForBid, dtUnderConstruction, sSpecialProvisions, dtInstallFrom, dtInstallTo, sAttachedPages,
                        sAttachedSpecifications, sNote, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvAg_WFP3.ChangeMode(FormViewMode.ReadOnly);
                        PK_FormWfp3 = Convert.ToInt32(i);
                        BindAg_WFP3();
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
        return iCode;
    }

    protected void fvAg_WFP3_ItemDeleting(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_delete(Convert.ToInt32(fvAg_WFP3.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbAg_WFP3_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Data Binding - Ag - WFP3

    private void BindAg_WFP3()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ListView lv;

            var a = wDataContext.form_wfp3s.Where(w => w.pk_form_wfp3 == PK_FormWfp3).Select(s => s);
            fvAg_WFP3.DataKeyNames = new string[] { "pk_form_wfp3" };
            fvAg_WFP3.DataSource = a;
            fvAg_WFP3.DataBind();
          
            //lvAg_WFP3_BMPs
            lv = pnlSubPanels.FindControl("lvAg_WFP3_BMPs") as ListView;
            var c = from b in wDataContext.vw_form_wfp3_BMPs.Where(w => w.pk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_bmp" };
            lv.DataSource = c;
            lv.DataBind();

            //lvAg_WFP3_Bids
            lv = pnlSubPanels.FindControl("lvAg_WFP3_Bids") as ListView;
            var d = from b in wDataContext.form_wfp3_bids.Where(w => w.fk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_bid" };
            lv.DataSource = d;
            lv.DataBind();

            //lvAg_WFP3_Encumbrances
            lv = pnlSubPanels.FindControl("lvAg_WFP3_Encumbrances") as ListView;
            var e = from b in wDataContext.form_wfp3_encumbrances.Where(w => w.fk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_encumbrance" };
            lv.DataSource = e;
            lv.DataBind();

            //lvAg_WFP3_Invoices
            lv = pnlSubPanels.FindControl("lvAg_WFP3_Invoices") as ListView;
            var i = from b in wDataContext.form_wfp3_invoices.Where(w => w.fk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_invoice" };
            lv.DataSource = i;
            lv.DataBind();

            //lvAg_WFP3_Payments
            lv = pnlSubPanels.FindControl("lvAg_WFP3_Payments") as ListView;
            var p = from b in wDataContext.form_wfp3_payments.Where(w => w.fk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_payment" };
            lv.DataSource = p;
            lv.DataBind();

            //lvAg_WFP3_Modifications
            lv = pnlSubPanels.FindControl("lvAg_WFP3_Modifications") as ListView;
            var m = from b in wDataContext.form_wfp3_modifications.Where(w => w.fk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_modification" };
            lv.DataSource = m;
            lv.DataBind();

            //lvAg_WFP3_Specifications"
            lv = pnlSubPanels.FindControl("lvAg_WFP3_Specifications") as ListView;
            var sp= from b in wDataContext.form_wfp3_specifications.Where(w => w.fk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_specification" };
            lv.DataSource = sp;
            lv.DataBind();

            //lvAg_WFP3_Techs
            lv = pnlSubPanels.FindControl("lvAg_WFP3_Techs") as ListView;
            var t = from b in wDataContext.form_wfp3_teches.Where(w => w.fk_form_wfp3 == PK_FormWfp3) select b;
            lv.DataKeyNames = new string[] { "pk_form_wfp3_tech" };
            lv.DataSource = t;
            lv.DataBind();


            litAg_WFP3_Header.Text = WACGlobal_Methods.SpecialText_Agriculture_PopUpHeader(FK_FarmBusiness);
            string sWACRegion = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(FK_FarmBusiness);

            Create_Agriculture_WFP3_Basic_Grid(PK_FormWfp3);
            Create_Agriculture_WFP3_Encumbrance_CurrentlyAssigned_Summary_Grid(PK_FormWfp3);
            Create_Agriculture_WFP3_Encumbrance_Possible_Summary_Grid(PK_FormWfp3);
            Create_Agriculture_WFP3_PaymentBMP_PaymentStatus_Grid(PK_FormWfp3);
            CreateAgWFP3PaymentBMPFundingOverviewGrid(PK_FormWfp3);

            if (fvAg_WFP3.CurrentMode == FormViewMode.ReadOnly && a.Count() == 1)
            {
                UpdatePanel up = (UpdatePanel)pnlAg_WFP3.FindControl("upAg_WFP3");
                Utility_WACUT_AttachedDocumentViewer docView = (Utility_WACUT_AttachedDocumentViewer)up.FindControl("WACUT_AttachedDocumentViewerWFP3");
                List<WACParameter> parms = new List<WACParameter>();
                parms.Add(new WACParameter("pk_farmBusiness", a.First().fk_farmBusiness, WACParameter.ParameterType.MasterKey));
                parms.Add(new WACParameter("pk_form_wfp2", a.First().pk_form_wfp3, WACParameter.ParameterType.PrimaryKey));
                docView.InitControl(parms);
            }

            if (fvAg_WFP3.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_WFP3, "ddlSpecialProvisions", "N");
                WACGlobal_Methods.PopulateControl_DatabaseLists_ProcurementType_DDL(fvAg_WFP3, "ddlProcurementType", "");
            }

            if (fvAg_WFP3.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Generic_YesNoDDL(fvAg_WFP3, "ddlSpecialProvisions", a.Single().specialProvisions);
                WACGlobal_Methods.PopulateControl_DatabaseLists_ProcurementType_DDL(fvAg_WFP3, "ddlProcurementType", a.Single().fk_procurementType_code);
            }
        }
    }

    private void Create_Agriculture_WFP3_Basic_Grid(int iPK_Form_WFP3)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                //var x = wac.encumbrance_get_basic(null);
                var x = wac.form_wfp3_encumbrance_summary(iPK_Form_WFP3, null);
                if (x.Count() > 0)
                {
                    sb.Append("<div class='taC NestedDivReadOnly'>");
                    sb.Append("<div class='B fsS'>Encumbrance Summary</div>");
                    sb.Append("<table cellpadding='5' rules='cols' align='center'>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B U'>Encumbrance</td>");
                    sb.Append("<td class='B U'>Budget</td>");
                    sb.Append("<td class='B U'>Approved</td>");
                    sb.Append("<td class='B U'>Pending</td>");
                    sb.Append("<td class='B U'>Available</td>");
                    sb.Append("</tr>");
                    foreach (var y in x)
                    {
                        sb.Append("<tr valign='top'>");
                        sb.Append("<td>" + y.encumbrance + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.budget) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.encumbered) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.encumberedPending) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.available) + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
                else sb.Append("<div class='NestedDivLevel00'>No Encumbrance Summary Records</div>");
            }
        }
        catch (Exception ex) { sb.Append("Error Creating Encumbrance Summary: " + ex.Message); }
        Literal litAg_WFP3_Basic_Grid = fvAg_WFP3.FindControl("litAg_WFP3_Basic_Grid") as Literal;
        if (litAg_WFP3_Basic_Grid != null) litAg_WFP3_Basic_Grid.Text = sb.ToString();
    }

    private void Create_Agriculture_WFP3_Encumbrance_CurrentlyAssigned_Summary_Grid(int iPK_Form_WFP3)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.vw_encumbrance_WFP3s.Where(w => w.pk_form_wfp3 == iPK_Form_WFP3).Select(s => s);
                if (x.Count() > 0)
                {
                    sb.Append("<div class='taC NestedDivReadOnly'>");
                    sb.Append("<div class='B fsS'>Encumbrance History for this Package</div>");
                    sb.Append("<table cellpadding='5' rules='cols' align='center'>");
                    sb.Append("<tr valign='top'><td class='B U'>Encumbrance</td><td class='B U'>Encumbrance ID</td><td class='B U'>Amount</td></tr>");
                    foreach (var y in x)
                    {
                        sb.Append("<tr valign='top'>");
                        sb.Append("<td>" + y.encumbrance + "</td>");
                        sb.Append("<td>" + y.encumbrance_id + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.amt) + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
                else sb.Append("<div class='NestedDivLevel00'>No Currently Assigned Encumbrances</div>");
            }
        }
        catch (Exception ex) { sb.Append("Error Creating Encumbrance History for this Package: " + ex.Message); }
        Literal litAg_WFP3_Encumbrance_CurrentlyAssigned_Grid = pnlSubPanels.FindControl("litAg_WFP3_Encumbrance_CurrentlyAssigned_Grid") as Literal;
        if (litAg_WFP3_Encumbrance_CurrentlyAssigned_Grid != null) litAg_WFP3_Encumbrance_CurrentlyAssigned_Grid.Text = sb.ToString();
    }

    private void Create_Agriculture_WFP3_Encumbrance_Possible_Summary_Grid(int iPK_Form_WFP3)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.form_wfp3_encumbrance_summary(iPK_Form_WFP3, null);
                if (x.Count() > 0)
                {
                    sb.Append("<div class='taC NestedDivReadOnly'>");
                    sb.Append("<div class='B fsS'>Encumbrance Possibilities for this Package</div>");
                    sb.Append("<table cellpadding='5' rules='cols' align='center'>");
                    sb.Append("<tr valign='top'><td class='B U'>Encumbrance</td><td class='B U'>Budget</td><td class='B U'>Encumbered Currently</td><td class='B U'>Funds Available</td><td class='B U'>Package Cost</td><td class='B U'>Prior Encumbrances on Package</td><td class='B U'>Max Amt of Package to Encumber</td></tr>");
                    foreach (var y in x)
                    {
                        sb.Append("<tr valign='top'>");
                        sb.Append("<td>" + y.encumbrance + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.budget) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.encumbered) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.available) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.packageCost) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.packageEncumberPrior) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.packageToEncumber) + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
                else sb.Append("<div class='NestedDivLevel00'>No Possible Encumbrances</div>");
            }
        }
        catch (Exception ex) { sb.Append("Error Creating Encumbrance Possibilities for this Package: " + ex.Message); }
        Literal litAg_WFP3_Encumbrance_Possible_Grid = pnlSubPanels.FindControl("litAg_WFP3_Encumbrance_Possible_Grid") as Literal;
        if (litAg_WFP3_Encumbrance_Possible_Grid != null) litAg_WFP3_Encumbrance_Possible_Grid.Text = sb.ToString();
    }

    private void Create_Agriculture_WFP3_PaymentBMP_PaymentStatus_Grid(int iPK_Form_WFP3)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.vw_form_wfp3_paymentBMP_paymentStatus.Where(w => w.fk_form_wfp3 == iPK_Form_WFP3).Select(s => s);
                if (x.Count() > 0)
                {
                    sb.Append("<div class='taC NestedDivReadOnly'>");
                    sb.Append("<div class='B fsS'>BMP Payments</div>");
                    sb.Append("<table cellpadding='5' rules='cols' align='center'>");
                    sb.Append("<tr valign='top'><td class='B U'>Payment Date</td><td class='B U'>BMP #</td><td class='B U'>Amount</td><td class='B U'>Amount, Other Agency</td><td class='B U'>Other Agency</td><td class='B U'>Payment Status</td></tr>");
                    foreach (var y in x)
                    {
                        sb.Append("<tr valign='top'>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Date(y.date) + "</td>");
                        sb.Append("<td>" + y.CompositBmpNum + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.amt) + "</td>");
                        sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(y.amt_agencyFunding) + "</td>");
                        sb.Append("<td>" + y.otherAgencyFunding + "</td>");
                        sb.Append("<td>" + y.status + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
                else sb.Append("<div class='NestedDivLevel00'>No Existing BMP Payments</div>");
            }
        }
        catch (Exception ex) { sb.Append("Error Creating BMP Payments Grid: " + ex.Message); }
        Literal litAg_WFP3_Payments_BMPs_Payments_Overview_Grid = pnlSubPanels.FindControl("litAg_WFP3_Payments_BMPs_Payments_Overview_Grid") as Literal;
        if (litAg_WFP3_Payments_BMPs_Payments_Overview_Grid != null) litAg_WFP3_Payments_BMPs_Payments_Overview_Grid.Text = sb.ToString();
    }

    private void CreateAgWFP3PaymentBMPFundingOverviewGrid(int i)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var x = wDataContext.form_wfp3_payment_get_funding_PK(i);
                if (x.Count() > 0)
                {
                    sb.Append("<div class='taC NestedDivReadOnly'>");
                    sb.Append("<div class='fsS B'>Funding History</div>");
                    sb.Append("<center><table cellpadding='5' rules='cols'>");
                    sb.Append("<tr valign='top'>");
                    sb.Append("<td class='B U'>Date</td>");
                    sb.Append("<td class='B U'>BMP</td>");
                    sb.Append("<td class='B U'>Funding Source</td>");
                    sb.Append("<td class='B U'>Funding Agency</td>");
                    sb.Append("<td class='B U'>Version</td>");
                    sb.Append("<td class='B U taR'>Amount</td>");
                    sb.Append("</tr>");
                    foreach (var y in x)
                    {
                        sb.Append("<tr valign='top'>");
                        sb.Append("<td class='taL'>" + WACGlobal_Methods.Format_Global_Date(y.date) + "</td>");
                        sb.Append("<td class='taL'>" + y.CompositBmpNum + "</td>");
                        sb.Append("<td class='taL'>" + y.Funding_Source + "</td>");
                        sb.Append("<td class='taL'>" + y.Funding_Agency + "</td>");
                        sb.Append("<td class='taL'>" + y.Version + "</td>");
                        sb.Append("<td class='taR'>" + WACGlobal_Methods.Format_Global_Currency(y.funding) + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table></center>");
                    sb.Append("</div>");
                }
                else sb.Append("<div class='NestedDivLevel00'>No Existing Funding History</div>");
            }
        }
        catch (Exception ex) { sb.Append("Error Creating Funding History Grid: " + ex.Message); }
        Literal litAg_WFP3_Payments_BMPs_Funding_Overview_Grid = pnlSubPanels.FindControl("litAg_WFP3_Payments_BMPs_Funding_Overview_Grid") as Literal;
        if (litAg_WFP3_Payments_BMPs_Funding_Overview_Grid != null) litAg_WFP3_Payments_BMPs_Funding_Overview_Grid.Text = sb.ToString();
    }

    //private void Create_Agriculture_WFP3_PaymentBMP_PercentCalc_Grid(FormView fv, int? iPK_Form_WFP3_BMP, decimal? dPK_BMPPractice)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    try
    //    {
    //        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //        {
    //            var x = wac.form_wfp3_paymentBMP_pctCalc(iPK_Form_WFP3_BMP, dPK_BMPPractice, null, null);
    //            if (x.Count() == 1)
    //            {
    //                sb.Append("<div class='NestedDivReadOnly'><center>");
    //                sb.Append("<div class='B fsS'>BMP Completion Status</div>");
    //                sb.Append("<table cellpadding='5' rules='cols'>");
    //                sb.Append("<tr valign='top'><td class='B U'>Contract Amt</td><td class='B U'>% Completed</td><td class='B U'>Amt Paid</td></tr>");
    //                sb.Append("<tr valign='top'>");
    //                sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(x.Single().contract_amt) + "</td>");
    //                sb.Append("<td>" + x.Single().pct_completed + "%</td>");
    //                //sb.Append("<td>" + x.Single().units_paid + "</td>");
    //                sb.Append("<td>" + WACGlobal_Methods.Format_Global_Currency(x.Single().paid_amt) + "</td>");
    //                sb.Append("</tr>");
    //                sb.Append("</table>");
    //                sb.Append("</center></div>");
    //            }
    //            else sb.Append("<div class='NestedDivLevel00'>No Existing Percent Calculations</div>");
    //        }
    //    }
    //    catch (Exception ex) { sb.Append("Error Creating BMP Completion Status Grid: " + ex.Message); }
    //    Literal litAgriculture_WFP3_PaymentBMP_PercentCalc_Grid = fv.FindControl("litAgriculture_WFP3_PaymentBMP_PercentCalc_Grid") as Literal;
    //    if (litAgriculture_WFP3_PaymentBMP_PercentCalc_Grid != null) litAgriculture_WFP3_PaymentBMP_PercentCalc_Grid.Text = sb.ToString();
    //}

    #endregion


}
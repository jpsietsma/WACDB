using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Services;
using WAC_DataObjects;
using WAC_Containers;
using WAC_CustomControls;

public partial class AG_WACAG_SupplementalAgreementForm : WACFormControl, IWACContainer, IWACIndependentControl
{
    public string County { get; set; }
    public string Swis { get; set; }
    public string TaxParcelID { get; set; }
    protected override void OnInit(EventArgs e)
    {
        ReBindControl();
        base.OnInit(e);
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
            ReBindControl();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            ServiceRequest sr = new ServiceRequest(this);
            sr.ServiceFor = this;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContainerVisibility;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
    }

    protected void lb_fvClose_Click(object sender, EventArgs e)
    {
        base.Close(sReq, fvSA);
    }
    public override void UpdateControl(List<WACParameter> parms)
    {
   
    }
    public override void ReBindControl()
    {
        if (sReq != null)
        {
            sReq.ServiceFor = fvSA;
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindFormView;
            ServiceFactory.Instance.ServiceRequest(sReq);
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindDDLs;
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
    }
    private void fvOpenEdit(FormView fv, object key)
    {
        base.OpenEdit(sReq, fvSA, SupplementalAgreement.PrimaryKeyName, key);
    }
    private void CloseFormView()
    {
        base.Close(sReq, fvSA);
        upSupplementalAgreement.Update();
    }
    private void CancelUpdate()
    {
        base.CancelUpdate(sReq, fvSA);
    }
    protected void fvItemCommand(object sender, FormViewCommandEventArgs e)
    {
        FormView fv = sender as FormView;
        CommandEventArgs c = e as CommandEventArgs;
        switch (c.CommandName)
        {
            case "InsertData":
                fvItemInserting(fv);
                break;
            case "CancelInsert":
                goto case "CloseForm";
            case "CloseForm":
                CloseFormView();
                break;
            default:
                break;
        }
        //  e.Handled = true;
    }

    private void fvItemInserting(FormView fv)
    {
        try
        {
            CustomControls_AjaxCalendar agreementDate = (CustomControls_AjaxCalendar)fv.FindControl("tbCalAgreementDate");
            WACPR_TaxParcelPicker taxParcelPicker = fv.FindControl("WACPR_TaxParcelPicker") as WACPR_TaxParcelPicker;
            sReq.ParmList.Clear();
            List<WACParameter> taxParcelProps = taxParcelPicker.GetContents();
            sReq.ParmList.Add(WACParameter.GetParameter(taxParcelProps, "swis"));
            sReq.ParmList.Add(WACParameter.GetParameter(taxParcelProps, "printKey"));
            sReq.ParmList.Add(new WACParameter("agreement_date", agreementDate.CalDateNotNullable, WACParameter.ParameterType.Property));
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.InsertItem;
            sReq.ServiceFor = fv;
            ServiceFactory.Instance.ServiceRequest(sReq);
            taxParcelProps = null;
        }
        catch (Exception ex)
        {
            WACAlert.Show(ex.Message, 0);
        }
        
    }
    private bool fvItemUpdating(FormView fv, object pk_participant)
    {
        return true;
    }
    private bool fvItemDeleting(FormView fv, object pk_easementStatus)
    {
        return true;
    }

    protected void TaxParcelPicker_Notify(object sender, UserControlResultEventArgs e)
    {
        County = WACParameter.GetParameterValue(e.Parms, "county") as string;
        Swis = WACParameter.GetParameterValue(e.Parms, "swis") as string;
        TaxParcelID = WACParameter.GetParameterValue(e.Parms, "printkey") as string;
        sReq.ServiceFor = fvSA;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.BindDDL;
        ServiceFactory.Instance.ServiceRequest(sReq);
       
    }

    public override void ResetControl()
    {
        throw new NotImplementedException();
    }

    public override void InitControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    public override void CloseControl()
    {
        throw new NotImplementedException();
    }

    public void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }
    
}
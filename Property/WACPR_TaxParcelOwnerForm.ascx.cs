using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_ViewModels;
using WAC_Event;
using WAC_Connectors;
using WAC_Services;
using WAC_UserControls;
using WAC_Containers;

public partial class Property_WACPR_TaxParcelOwnerForm : WACFormControl, IWACContainer, IWACDependentControl
{
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
    //protected void lb_fvOpenAdd_Click(object sender, EventArgs e)
    //{
    //    base.OpenAdd(fvTaxParcelOwner);
    //}

    protected void fvItemCommand(object sender, FormViewCommandEventArgs e)
    {
        FormView fv = sender as FormView;
        CommandEventArgs c = e as CommandEventArgs;
        switch (c.CommandName)
        {
            case "EditData":
                base.OpenEdit(fv, TaxParcelOwner.PrimaryKeyName, c.CommandArgument);
                break;
            case "InsertData":
                fvItemInserting(fv);
                break;
            case "UpdateData":
                fvItemUpdating(fv, c.CommandArgument);
                break;
            case "DeleteData":
                fvItemDeleting(fv, c.CommandArgument);
                break;
            case "CancelUpdate":
                base.CancelUpdate(fv);
                break;
            case "CancelInsert":
                goto case "CloseForm";
            case "CloseForm":
                base.Close(fv);
                break;
            default:
                break;
        }
       // e.Handled = true;
    }
    private void fvItemInserting(FormView fv)
    {
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;
        WACUserInputUtilityControl wcc = (WACUserInputUtilityControl)fv.FindControl("WACUT_YesNoChooser");
        sReq.ParmList = wcc.GetContents();
        wcc = (WACUserInputUtilityControl)fv.FindControl("WACPT_ParticipantAlphaPicker");
        WACParameter wp = WACParameter.GetParameter(wcc.GetContents(), "pk_participant");
        wp.ParmName = "fk_participant";
        sReq.ParmList.Add(wp);
        sReq.ParmList.Add(new WACParameter("note", tbNote.Text, WACParameter.ParameterType.Property));
        sReq.ServiceFor = fv;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.InsertItem;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    private void fvItemUpdating(FormView fv, object pk_taxParcelOwner)
    {
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;
        WACUserInputUtilityControl wcc = (WACUserInputUtilityControl)fv.FindControl("WACUT_YesNoChooser");
        sReq.ParmList = wcc.GetContents();
        sReq.ParmList.Add(new WACParameter("pk_taxParcelOwner", pk_taxParcelOwner));
        sReq.ParmList.Add(new WACParameter("note", tbNote.Text,WACParameter.ParameterType.Property));
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.UpdateItem;
        sReq.ServiceFor = fv;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    private void fvItemDeleting(FormView fv, object pk_taxParcel)
    {
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.DeleteItem;
        sReq.ServiceFor = fv;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }

    public override void InitControl(List<WACParameter> parms)
    {
        return;
    }

    public override void UpdateControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    public override void ResetControl()
    {
        throw new NotImplementedException();
    }

    public override void CloseControl()
    {
        return;
    }

    public override void ReBindControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindFormView;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    
    public void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }
}
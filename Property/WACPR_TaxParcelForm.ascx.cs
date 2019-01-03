using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_UserControls;
using WAC_Connectors;
using WAC_Event;
using WAC_ViewModels;
using WAC_Services;
using WAC_DataObjects;
using WAC_Containers;

public partial class Property_WACPR_TaxParcelForm : WACFormControl, IWACContainer, IWACDependentControl
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
    public override void InitControl(List<WACParameter> parms)
    {
        // Note: when DocumentArchive is converted to IWACControl, this InitControl will be in the DocumentArchive control
        // and will be called by the InitControls service request 
        //UC_DocumentArchive ucDA = fvTaxParcel.FindControl("UC_DocumentArchive_TP") as UC_DocumentArchive;
        //if (ucDA != null) ucDA.SetupViewer();
    }
    public override void ReBindControl()
    {
        ServiceRequest sr = new ServiceRequest(this);
        sr.ServiceFor = this;
        sr.ServiceFor = fvTaxParcel;
        sr.ServiceRequested = ServiceFactory.ServiceTypes.ReBindFormView;
        ServiceFactory.Instance.ServiceRequest(sr);
        sr = null;
    }
    protected void TaxParcelPicker_Notify(object sender, UserControlResultEventArgs e)
    {
        Label lblSBL = fvTaxParcel.FindControl("lblSBL") as Label;
        lblSBL.Text = WACParameter.GetParameterValue(e.Parms, "SBL") as string;
    }
    protected void WACUT_Associations_ListContentsChanged(object sender, UserControlResultEventArgs e)
    {
        WACListControl associations = (WACListControl)sender;
        associations.UpdateControl(e.Parms);
    }

    protected void fvItemCommand(object sender, FormViewCommandEventArgs e)
    {
        FormView fv = sender as FormView;
        CommandEventArgs c = e as CommandEventArgs;
        switch (c.CommandName)
        {
            case "EditData":
                base.OpenEdit(fv, TaxParcel.PrimaryKeyName, c.CommandArgument);
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
                base.CancelUpdate(sReq,fv);
                break;
            case "CancelInsert":
                goto case "CloseForm";
            case "CloseForm":
                base.Close(fv);
                break;
            default:
                break;
        }
     //   e.Handled = true;
    }

    private void fvItemInserting(FormView fv)
    {     
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;
        WACUserInputUtilityControl wcc = (WACUserInputUtilityControl)fv.FindControl("WACPR_TaxParcelPicker");
        sReq.ParmList = wcc.GetContents();
        sReq.ParmList.Add(new WACParameter("note", tbNote.Text,WACParameter.ParameterType.Property));
        sReq.ServiceFor = fv;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.InsertItem;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    private void fvItemUpdating(FormView fv, object pk_taxParcel)
    {
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;
        sReq.ParmList.Clear();
        sReq.ParmList.Add(new WACParameter("pk_taxParcel", pk_taxParcel, WACParameter.ParameterType.Property));
        sReq.ParmList.Add(new WACParameter("note", tbNote.Text, WACParameter.ParameterType.Property));
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
    public override void ResetControl()
    {
        throw new NotImplementedException();
    }
    public List<WACParameter> GetContents()
    {
        throw new NotImplementedException();
    }    
    public override void UpdateControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }
    public override void CloseControl()
    {
        return;
    }




    public void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }
}
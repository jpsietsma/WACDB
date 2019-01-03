using System;
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
using System.Collections;
using WAC_Containers;

public partial class WACPR_TaxParcelPicker : WACUserInputUtilityControl, IWACContainer, IWACIndependentControl
{

    public bool CommandButtonsVisible { get; set; }
    public bool NewTaxParcelsOnly { get; set; }
    public event EventHandler<UserControlResultEventArgs> TaxParcelPickerAdd;
    public event EventHandler<UserControlResultEventArgs> TaxParcelPickerNotify;

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
   
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Panel pnlTaxParcelPickerCommands = this.FindControl("pnlTaxParcelPickerCommands") as Panel;
        pnlTaxParcelPickerCommands.Visible = CommandButtonsVisible;
        
    }
    public void InitControl()
    {
        ResetControl();
        sReq.ParmList.Clear();  // County list doesn't need any parameters
        sReq.ParmList.Add(new WACParameter("ddlID", "ddlCounty"));        
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.BindDDL;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    public override void ReBindControl()
    {
        ServiceRequest sr = new ServiceRequest(this);
        sr.ServiceFor = this;
        sr.ServiceRequested = ServiceFactory.ServiceTypes.ReBindDDLs;
        ServiceFactory.Instance.ServiceRequest(sr);
        sr = null;
    }
    public override List<WACParameter> GetContents()
    {
        return getAllSelected();
    }

    public override void InitControl(List<WACParameter> parms)
    {
        InitControl(); 
    }
    public void InitControls(List<WACParameter> parms)
    {
        ddlJurisdiction.SelectedIndex = -1;
        ddlTaxParcelID.SelectedIndex = -1;
    }
    public override void ResetControl()
    {
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReSetDDLs;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    protected void TaxParcelPicker_Command(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Add":
                if (TaxParcelPickerAdd != null)
                {
                    List<WACParameter> eventParms = getAllSelected();
                    TaxParcelPickerAdd(this, new UserControlResultEventArgs(eventParms));
                    ResetControl();
                }
                break;
            case "Delete"://maybe for use later
                break;
            case "Cancel":
                ResetControl();
                break;
            default:             
                break;
        }
    }
    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJurisdiction.Items != null)
            ddlJurisdiction.Items.Clear();
        if (ddlTaxParcelID.Items != null)
            ddlTaxParcelID.Items.Clear();
        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
        {
            sReq.ServiceFor = this;
            sReq.ParmList.Clear();
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.SaveDDLSelectedValues;
            ServiceFactory.Instance.ServiceRequest(sReq);
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.BindControlDDLs;
            sReq.ParmList.Add(new WACParameter("pk_list_countyNY", ddlCounty.SelectedValue, WACParameter.ParameterType.SelectedKey));
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
    }
    protected void ddlJurisdiction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTaxParcelID.Items != null)
            ddlTaxParcelID.Items.Clear();
        if (!string.IsNullOrEmpty(ddlJurisdiction.SelectedValue))
        {
            sReq.ServiceFor = this;
            sReq.ParmList.Clear();
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.SaveDDLSelectedValues;
            ServiceFactory.Instance.ServiceRequest(sReq);
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.BindControlDDLs;
            sReq.ParmList.Add(new WACParameter("NewTaxParcelsOnly", NewTaxParcelsOnly, WACParameter.ParameterType.Property));
            sReq.ParmList.Add(new WACParameter("swis", ddlJurisdiction.SelectedValue, WACParameter.ParameterType.NotKey));
            ServiceFactory.Instance.ServiceRequest(sReq);
        }
    }
    protected void ddlTaxParcelID_SelectedIndexChanged(object sender, EventArgs e)
    {
        sReq.ServiceFor = this;
        sReq.ParmList.Clear();
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.SaveDDLSelectedValues;
        if (!string.IsNullOrEmpty(ddlTaxParcelID.SelectedValue))
        {
            if (TaxParcelPickerNotify != null)
            {
                List<WACParameter> eventParms = getAllSelected();
                TaxParcelPickerNotify(this, new UserControlResultEventArgs(eventParms));
            }
        }
    }
    private List<WACParameter> getAllSelected()
    {
        List<WACParameter> selected = new List<WACParameter>();
        try
        {
            selected.Add(new WACParameter("county", ddlCounty.SelectedValue, WACParameter.ParameterType.Property));
            selected.Add(new WACParameter("swis", ddlJurisdiction.SelectedValue, WACParameter.ParameterType.Property));
            if (ddlTaxParcelID.SelectedItem.Text.Contains("Select"))
            {
                selected.Add(new WACParameter("printKey", null, WACParameter.ParameterType.Property));
            }
            else
            {
                selected.Add(new WACParameter("printKey", ddlTaxParcelID.SelectedItem.Text, WACParameter.ParameterType.Property));
            }
            selected.Add(new WACParameter("sbl", ddlTaxParcelID.SelectedItem.Value, WACParameter.ParameterType.Property));
        }
        catch (Exception)
        {   // fail silently
            selected.Add(new WACParameter("county", string.Empty, WACParameter.ParameterType.Property));
            selected.Add(new WACParameter("swis", string.Empty, WACParameter.ParameterType.Property));
            selected.Add(new WACParameter("printKey", string.Empty, WACParameter.ParameterType.Property));
            selected.Add(new WACParameter("sbl", string.Empty, WACParameter.ParameterType.Property));
        }
        return selected;
    }   
    
    public override void UpdateControl(List<WACParameter> parms)
    {
        
    }

    public override void CloseControl()
    {
        ResetControl();
    }


    public void AdjustContentVisibility(List<WACControl> ContainedControls, WACFormControl form)
    {
        throw new NotImplementedException();
    }
}


using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Services;
using WAC_UserControls;


public partial class UC_ParticipantAlphaPicker : WACUserInputUtilityControl, IWACIndependentControl
{
    public ParticipantOrganizationDP.AlphaPickerListType ListType { get; set; }

    public event EventHandler<UserControlResultEventArgs> ParticipantAlphaPicker_Clicked;
    protected override void OnInit(EventArgs e)
    {
        sReq = new ServiceRequest(this);
        ReBindControl();
        base.OnInit(e);
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        base.RegisterAndConnect(this);
    }

    protected void lbStartsWith_Click(object sender, CommandEventArgs c)
    {
        ResetControl();
        sReq.ParmList.Add(new WACParameter(string.Empty, this.ListType, WACParameter.ParameterType.PickerListType));
        sReq.ParmList.Add(new WACParameter("NameStartsWith", c.CommandName, WACParameter.ParameterType.NotKey));
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.BindControlDDLs;
        ServiceFactory.Instance.ServiceRequest(sReq);

    }
    public override List<WACParameter> GetContents()
    {
        List<WACParameter> parms = new List<WACParameter>();
        try
        {
            parms.Add(new WACParameter("participant", ddlParticipant.SelectedItem.Text, WACParameter.ParameterType.Property));
            parms.Add(new WACParameter("pk_participant", ddlParticipant.SelectedItem.Value, WACParameter.ParameterType.Property));
        }
        catch (Exception)
        {   // fail silently
            parms.Add(new WACParameter("participant", string.Empty, WACParameter.ParameterType.Property));
            parms.Add(new WACParameter("pk_participant", string.Empty, WACParameter.ParameterType.Property));
        }
        return parms;
    }
    protected void ddlParticipant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlParticipant.SelectedValue))
        {
            sReq.ServiceFor = this;
            sReq.ServiceRequested = ServiceFactory.ServiceTypes.SaveDDLSelectedValues;
            ServiceFactory.Instance.ServiceRequest(sReq);
            if (ParticipantAlphaPicker_Clicked != null)
            {
                List<WACParameter> eventParms = new List<WACParameter>();
                eventParms.Add(new WACParameter("participant", ddlParticipant.SelectedItem.Text, WACParameter.ParameterType.Property));
                eventParms.Add(new WACParameter("pk_participant", ddlParticipant.SelectedItem.Value, WACParameter.ParameterType.Property));
                ParticipantAlphaPicker_Clicked(this, new UserControlResultEventArgs(eventParms));
            }
        }
    }
    public override void ReBindControl()
    {
        ddlParticipant.Items.Clear();
        sReq.ServiceFor = this;
        sReq.ServiceRequested = ServiceFactory.ServiceTypes.ReBindDDLs;
        ServiceFactory.Instance.ServiceRequest(sReq);
    }
    public override void ResetControl()
    {
        ddlParticipant.Items.Clear();
        ddlParticipant.SelectedIndex = -1;
    }

    public override void InitControl(List<WACParameter> parms)
    {
        // nothing to initialize in this control
        ResetControl();
    }

    public override void UpdateControl(List<WACParameter> parms)
    {

    }

    public override void CloseControl()
    {
        return;
    }


    public void InitContainer(List<WACParameter> parms)
    {
        return;
    }
}
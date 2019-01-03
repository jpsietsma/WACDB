using System;
using System.Collections.Generic;
using System.Collections;
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
using WAC_Extensions;

public partial class Participant_WACPT_ParticipantListSearch : WACUserInputUtilityControl, IWACIndependentControl
{
    public ParticipantOrganizationDP.AlphaPickerListType ListType { get; set; }
    protected override void OnInit(EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
        base.OnInit(e);
    }  
    public override List<WACParameter> GetContents()
    {
        List<WACParameter> parms = new List<WACParameter>();
        //parms.Add(new WACParameter("participant", ddlParticipant.SelectedItem.Text, WACParameter.ParameterType.Property));
        //parms.Add(new WACParameter("pk_participant", ddlParticipant.SelectedItem.Value, WACParameter.ParameterType.Property));
        return parms;
    }  
    public override void ReBindControl()
    {
        
    }
    public override void ResetControl()
    {
        
    }
    public override void InitControl(List<WACParameter> parms)
    {
        
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
    protected void tbParticipants_TextChanged(object sender, EventArgs e)
    {
        WACAlert.Show("changed...",0);
    }
}
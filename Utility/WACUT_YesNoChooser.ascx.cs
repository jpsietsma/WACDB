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

public partial class Utility_WACUT_YesNoChooser : WACUserInputUtilityControl, IWACDisconnectedControl, IWACContainer
{
    public string SelectedValue { get; set; }
    public string BoundPropertyName { get; set; }
    public string ChooserLabel
    {
        get { return ViewState[this.ClientID + "_Label"] == null ? string.Empty : ViewState[this.ClientID + "_Label"].ToString(); }
        set { ViewState[this.ClientID+"_Label"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblYesNoChooser.Text = ChooserLabel;
    }
   
    public override void InitControl(List<WACParameter> parms)
    {
        if (this.SelectedValue != null)
            rblYesNo.SelectedValue = SelectedValue;
    }

    public override void UpdateControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    public override void CloseControl()
    {
        throw new NotImplementedException();
    }

    public override void ResetControl()
    {
        throw new NotImplementedException();
    }


    public void InitControls(List<WACParameter> parms)
    {
        
    }

    public override List<WACParameter> GetContents()
    {    
        List<WACParameter> parms = new List<WACParameter>();
        parms.Add(new WACParameter(BoundPropertyName, rblYesNo.SelectedValue, WACParameter.ParameterType.Property));
        return parms;
    }

    public override void ReBindControl()
    {
        throw new NotImplementedException();
    }

    event EventHandler<UserControlResultEventArgs> IWACContainer.ContentStateChanged
    {
        add { throw new NotImplementedException(); }
        remove { throw new NotImplementedException(); }
    }

    void IWACContainer.InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    void IWACContainer.UpdatePanelUpdate()
    {
        throw new NotImplementedException();
    }
}
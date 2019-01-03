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

public partial class Utility_WACUT_AddNewItem : WACUtilityControl, IWACDisconnectedControl
{
    public event EventHandler<UserControlResultEventArgs> AddNewItem_Clicked;
    public string ButtonLabel
    {
        get { return ViewState[this.ID + "_Label"] == null ? "Add Item" : ViewState[this.ID + "_Label"].ToString(); }
        set { ViewState[this.ID + "_Label"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lb_AddNewItem.Text = ButtonLabel;
    }
    protected void lb_AddNewItem_Click(object sender, EventArgs e)
    {
        if (AddNewItem_Clicked != null)
        {
            List<WACParameter> eventParms = new List<WACParameter>();
            eventParms.Add(new WACParameter(string.Empty,WACFormControl.FormState.OpenInsert, WACParameter.ParameterType.FormState));
            AddNewItem_Clicked(this, new UserControlResultEventArgs(eventParms));
        }
    }

    public override void InitControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
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

    public override void ReBindControl()
    {
        
    }
}
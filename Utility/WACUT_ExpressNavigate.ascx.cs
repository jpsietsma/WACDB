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

public partial class Utility_WACUT_ExpressNavigate : WACUtilityControl, IWACIndependentControl
{
    public WACEnumerations.WACSector WACSector { get; set; }
    public string BindToProperty { get; set; }
    public string ButtonImageSize { get; set; }
    public Utility_WACUT_ExpressNavigate() { }
    protected void Page_Init(object sender, EventArgs e)
    {
        sReq = new ServiceRequest(this);
        RegisterAndConnect(this);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (ButtonImageSize)
        {
            case "24":
                ibExpressNav.ImageUrl = "~/images/arrow_orb_right_24.png";
                break;
            case "32":
                ibExpressNav.ImageUrl = "~/images/arrow_orb_right_32.png";
                break;
            default:
                ibExpressNav.ImageUrl = "~/images/arrow_orb_right_16.png";
                break;
        }
        ibExpressNav.ToolTip = "Click to Navigate to the Section Page for this Record";
    }
    protected void ibExpressNav_DataBinding(object sender, EventArgs e)
    {
        Control parent = null;
        ImageButton ib = (ImageButton)sender;
        Control binder = ib.BindingContainer;
        if (binder != null)
            parent = binder.BindingContainer;
        if (typeof(IDataBoundControl).IsAssignableFrom(parent.GetType()))
        {
            IDataItemContainer dItem = (IDataItemContainer)ib.BindingContainer;
            WACDataObject item = (WACDataObject)dItem.DataItem;
            ib.CommandArgument = item.GetPropertyAsString(BindToProperty);
        }
        else
            throw new Exception("WACUT_ExpressNavigate can only be used in a container that implements IDataBoundControl.");
    }
    protected void ibExpressNav_Click(object sender, EventArgs e)
    {
        ImageButton ib = (ImageButton)sender;
        string queryString = queryString = "pk=" + ib.CommandArgument;
        string url = null;
        switch (WACSector)
        {
            case WACEnumerations.WACSector.Communication:
                url = "~/WACCommunications.aspx?";
                break;
            case WACEnumerations.WACSector.FarmBusiness:
                url = "~/AG/WACAgriculture.aspx?";
                break;
            case WACEnumerations.WACSector.Organization:
                url = "~/Participant/WACPT_Organizations.aspx?";
                break;
            case WACEnumerations.WACSector.Participant:
                url = "~/Participant/WACPT_Participants.aspx?";
                break;
            case WACEnumerations.WACSector.Property:
                url = "~/Property/WACPR_Property.aspx?";
                break;
            case WACEnumerations.WACSector.Venue:
                url = "~/WACVenues.aspx?";
                break;
            default:
                break;
        }
        if (url != null)
            Response.Redirect(url + queryString);

    }

    public override void ResetControl()
    {
        throw new NotImplementedException();
    }

    public override void InitControl(List<WACParameter> parms)
    {
        return;
    }

    public override void UpdateControl(List<WACParameter> parms)
    {
        throw new NotImplementedException();
    }

    public override void CloseControl()
    {
        
    }


    public override void ReBindControl()
    {
        throw new NotImplementedException();
    }
}
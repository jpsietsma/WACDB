using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Containers;
using WAC_UserControls;
using WAC_DataObjects;
using WAC_Services;

public partial class Utility_WACUT_TestPageContents : WACContainer, IWACDisconnectedControl
{
    protected override void OnInit(EventArgs e)
    {
        sReq = new ServiceRequest(this);
        base.RegisterAndConnect(this);
        base.OnInit(e);
    }
    

    public override void InitControls(List<WACParameter> parms)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}
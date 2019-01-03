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

public partial class AG_WACAG_SupplementalAgreementPageContents : WACContainer, IWACContainer, IWACIndependentControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            sReq = new ServiceRequest(this);
            base.RegisterAndConnect(this);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            ServiceRequest sr = new ServiceRequest(this);
            sr.ServiceFor = WACAG_SupplementalAgreementForm;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContainerVisibility;
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
    }
    protected void WACUT_AddNewItem_AddNewItem_Clicked(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        FormView fv = WACGlobal_Methods.FindControl<FormView>(WACAG_SupplementalAgreementForm);
        if (fv != null)
            WACAG_SupplementalAgreementForm.OpenAdd(fv);
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

    event EventHandler<WAC_Event.UserControlResultEventArgs> IWACContainer.ContentStateChanged
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

    public override void ReBindControl()
    {
        throw new NotImplementedException();
    }
   
    protected void WACAG_SupplementalAgreementsForm_ContentStateChanged(object sender, WAC_Event.UserControlResultEventArgs e)
    {
        upSupplementalAgreement.Update();
    }
}
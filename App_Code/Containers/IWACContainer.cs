using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_DataObjects;
using System.Collections;
using System.Collections.Concurrent;

namespace WAC_Containers
{
    /// <summary>
    /// Summary description for IWACContainer
    /// </summary>
    public interface IWACContainer
    {
        event EventHandler<UserControlResultEventArgs> ContentStateChanged;
        void InitControls(List<WACParameter> parms);
        void UpdatePanelUpdate();
        //void AdjustContentVisibility(List<WACControl> ContainedControls, WACFormControl form);
        //void TurnThingsOff(List<WACParameter> parms);
        //void TurnThingsOn(List<WACParameter> parms);
        //List<WACParameter> GetContents();
    }
}
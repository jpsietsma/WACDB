using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACPT_ParticipantAlphaPickerConnector
    /// </summary>
    public class WACPT_ParticipantAlphaPickerConnector : UserControlConnector
    {
        public WACPT_ParticipantAlphaPickerConnector() { }
        
        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPT_ParticipantAlphaPickerVM pvm = (WACPT_ParticipantAlphaPickerVM)ViewModel;
            WACPT_ParticipantAlphaPickerDP pdp = (WACPT_ParticipantAlphaPickerDP)pvm.DataProvider;
            if (pvm.DDLBinders.Count < 1)
            {
                pvm.DDLBinders.TryAdd("ddlParticipant", new DDLDataObject(new ParticipantOrganizationDP(),false,null,false,null));
            }  
        }
    }
}
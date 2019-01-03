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
    /// Summary description for WACPT_ParticipantListSearchConnector
    /// </summary>
    public class WACPT_ParticipantListSearchConnector : UserControlConnector
    {
        public WACPT_ParticipantListSearchConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            WACPT_ParticipantListSearchVM pvm = (WACPT_ParticipantListSearchVM)ViewModel;
            WACPT_ParticipantListSearchDP pdp = (WACPT_ParticipantListSearchDP)pvm.DataProvider;
            if (pvm.DDLBinders.Count < 1)
            {
                pvm.DDLBinders.TryAdd("ddlParticipant", new DDLDataObject(new ParticipantOrganizationDP(), false, null, false, null));
            }
        }
    }
}
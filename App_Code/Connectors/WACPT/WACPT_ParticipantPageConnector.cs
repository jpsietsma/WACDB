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
using WAC_Connectors;

namespace WAC_Connectors
{
    /// <summary>
    /// Summary description for WACPT_ParticipantPageConnector
    /// </summary>
    public class WACPT_ParticipantPageConnector : UserControlConnector
    {
        public WACPT_ParticipantPageConnector() { }

        public override void ConnectControlSpecific(Control _page, ConnectorFactory _factory)
        {

        }
    }
}
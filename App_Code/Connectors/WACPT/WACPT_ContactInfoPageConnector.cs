﻿using System;
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
    /// Summary description for WACPT_ContactInfoPageConnector
    /// </summary>
    public class WACPT_ContactInfoPageConnector : UserControlConnector
    {
        public WACPT_ContactInfoPageConnector() { }

        public override void ConnectControlSpecific(Control _control, ConnectorFactory _factory)
        {
            
        }
    }
}
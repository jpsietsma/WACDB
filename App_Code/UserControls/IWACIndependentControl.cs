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
namespace WAC_UserControls
{
    /// <summary>
    /// Summary description for IWACIndependentControl
    /// </summary>
    public interface IWACIndependentControl
    {
        void RegisterAndConnect(Control c);
        void Connect(Control c);
    }
}
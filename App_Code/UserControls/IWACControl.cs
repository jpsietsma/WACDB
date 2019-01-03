using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WAC_Event;
using WAC_Connectors;
using WAC_ViewModels;

/// <summary>
/// Summary description for IWACControl
/// </summary>
namespace WAC_UserControls
{
    public interface IWACControl
    {
        void Register(Control c);     
    }
}
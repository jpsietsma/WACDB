using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Containers;
using WAC_Services;

/// <summary>
/// Summary description for AbstractControlConnector
/// </summary>
namespace WAC_Connectors
{
    public abstract class WACControlConnector
    {
        public List<WACControl> ContainedControls { get; set; }
        public string SessionID { get; set; }
        public WACViewModel ViewModel { get; set; }
        public abstract void ConnectControlSpecific(Control _control, ConnectorFactory _factory);
        
        public void Connect(Control _control, ConnectorFactory _factory)
        {
            WACControl myContainer = null;
            WACControlConnector myContainersConnector = null;
            try { myContainer = (WACControl)WACNamingContainer(_control); } catch (Exception) { }
            if (myContainer != null)
                myContainersConnector = getConnector(myContainer, _factory);
            if (myContainersConnector != null)
                myContainersConnector.TryAdd((WACControl)_control);
            if (ServiceFactory.IsDisconnectedControl(_control))
                return;
            if (ServiceFactory.IsIndependentControl(_control))
                _connect(_control, _factory, null);
            else
            {   // Dependent            
                if (myContainersConnector != null)
                {
                    if (myContainersConnector.ViewModel == null)
                        myContainersConnector.Connect(myContainer, _factory);
                    _connect(_control, _factory, myContainersConnector.ViewModel);    
                }
            }
            this.ContainedControls = getContents(_control, this);
        }
       
        private void _connect(Control _control, ConnectorFactory _factory, WACViewModel _containerViewModel)
        {
            if (ViewModel == null)
                ViewModel = GetViewModel(_control, _factory);
            if (String.IsNullOrEmpty(ViewModel.UserName))
            {
                ViewModel.UserName = _control.Page.Session["userName"] as string;
                ViewModel.UserID = WACGlobal_Methods.KeyAsInt(_control.Page.Session["userID"]);
            }
            if (_containerViewModel == null)
                ViewModel.DataProvider = GetDataProvider(_control, _factory);
            else
            {
                ViewModel.DataProvider = _containerViewModel.DataProvider;
                ViewModel.ListSource = _containerViewModel.ListSource;
            }
            if (SessionID == null)
                SessionID = _control.Page.Session.SessionID;
            else if (!_control.Page.Session.SessionID.Equals(SessionID, StringComparison.OrdinalIgnoreCase))
                throw new Exception("Connector / Control SessionID mismatch");
            ConnectControlSpecific(_control, _factory);
        }
        private Control WACNamingContainer(Control _control)
        {
            Control c = null;
            if (_control.NamingContainer != null)
            {
                if (!ServiceFactory.IsWACControl(_control.NamingContainer))
                    c = WACNamingContainer(_control.NamingContainer);
                else if (ServiceFactory.IsContainerControl(_control.NamingContainer))
                    c = _control.NamingContainer;
            }
            return c;
        }
        public string GetSessionID(Control _control)
        {
            if (SessionID == null)
                SessionID = _control.Page.Session.SessionID;
            else if (!_control.Page.Session.SessionID.Equals(SessionID, StringComparison.OrdinalIgnoreCase))
                throw new Exception("Connector / Control SessionID mismatch");
            return SessionID;
        }
        public WACViewModel GetViewModel(Control _control, ConnectorFactory _factory)
        {
            return _factory.CreateViewModel(_factory.ViewModelName(_control));
        }
        public WACDataProvider GetDataProvider(Control _control, ConnectorFactory _factory)
        {
            if (ServiceFactory.IsIndependentControl(_control))
                return _factory.CreateDataProvider(_factory.DataProviderName(_control));
            else
                return null;
        }
        public void TryAdd(WACControl wc)
        {
            if (ContainedControls == null)
                ContainedControls = new List<WACControl>();
            int _index = ContainedControls.IndexOf(wc);
            if (_index > -1)
                ContainedControls.RemoveAt(_index);
            ContainedControls.Add(wc);
        }

        private WACControlConnector getConnector(Control _control, ConnectorFactory _cfac)
        {
            WACControlConnector cCon = _cfac.GetConnectorForControl(_control);
            return cCon;
        }

        private IEnumerable<Control> EnumerateControlsRecursive(Control _parent)
        {
            List<Control> ret = new List<Control>();
            foreach (Control c in _parent.Controls)
            {
                ret.AddRange(EnumerateControlsRecursive(c));
                if (ServiceFactory.IsWACControl(c) || ServiceFactory.IsGenericContainer(c))
                    ret.Add((Control)c);
            }
            return (IEnumerable<Control>)ret;
        }

        private List<WACControl> getContents(Control _control, WACControlConnector _connector)
        {
            List<WACControl> ret = new List<WACControl>();
            List<Control> contents = EnumerateControlsRecursive(_control).ToList<Control>();
            contents.Add(_control);
            var a = contents.Where(w => WACNamingContainer(w) == _control).Select(s => s);
            foreach (Control wc in a)
            {
                if (ServiceFactory.IsWACControl(wc))
                    ret.Add((WACControl)wc);
            }
            return ret;
        }
        
    }
}
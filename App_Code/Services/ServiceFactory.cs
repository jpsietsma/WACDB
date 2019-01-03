using System;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_Connectors;
using WAC_ViewModels;
using WAC_DataProviders;
using WAC_DataObjects;
using WAC_UserControls;
using WAC_Exceptions;
using WAC_Validators;
using WAC_Containers;
using WAC_Event;

/// <summary>
/// Summary description for ServiceFactory
/// </summary>
namespace WAC_Services
{
    public class ServiceFactory
    {

        public enum ServiceTypes
        {
            RegisterConnectionComponents, MakeConnections, GetConnectorFactory, InsertItem, UpdateItem, DeleteItem,
            InitTabControl, ResetTabControls, SetMasterKeyInTabs, 
            BindControlDDLs, BindDDL, ReBindDDLs, ReSetDDLs, InitFilterDDLs, ResetReloadFilterDDLs,
            FilteredGridViewList, GridViewList, PageGridView, SortGridView, OpenGridView, CloseGridView,
            BindGridView, RefreshGridView, ClearGridView,  HandleGridViewSelection, ReBindGrid,
            FilteredListViewList, ListViewList, OpenListView, ClearListView, HandleListViewSelection,
            BindListView, ListRowCount, ReBindList,
            OpenFormViewInsert, OpenFormViewUpdate, OpenFormViewReadOnly, ClearFormView, CloseFormView, ReturnFormToViewMode, 
            ShowFormModal, HideFormModal, ReBindFormView,
            CustomString, CustomControlAction, InitCustomControls, InitCustomControl, CustomControlFilterAction, CustomControlReset,
            UpdatePanelUpdate, SaveDDLSelectedValues, ControlVisibility,
            InitControls, UpdateControls, UpdateControl, CloseControls, ResetControls, ContentStateChanged, ContainerDefaultDataView, 
            SetMasterKeyForContainer, ReBindControls, ContainerVisibility
        }
        
        public void ServiceRequested(object sender, ServiceRequestEventArgs s)
        {
            s.Sr.Requestor = (Control)sender;
            this.ProvideService(s.Sr);
        }
        private static ServiceFactory _instance = null;

        private readonly ConcurrentDictionary<string, ConnectorFactory> connectorFactories =
            new ConcurrentDictionary<string, ConnectorFactory>();
        private ServiceFactory() {}
        private void PersistValueToViewModel(ServiceRequest _request)
        {
            WACViewModel vm = GetViewModelForControl(_request.ServiceFor);

        }
        #region Utility
        public static ServiceFactory Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new ServiceFactory();
                return _instance;
            }
        }
        private List<WACControl> ControlList(Control _control)
        {
            WACControlConnector con = GetConnector(_control);
            if (con.ContainedControls == null)
                ConnectControl(_control);
            return con.ContainedControls ?? new List<WACControl>();
        }
        public static string ShortName(string _controlID)
        {
            if (!_controlID.Contains("WAC"))
                return _controlID;
            else
                return _controlID.Substring(_controlID.LastIndexOf("WAC"));
        }
        public static bool IsGenericContainer(Control _control)
        {
            bool result2 = false;
            bool result = _control.GetType().IsSubclassOf(typeof(System.Web.UI.WebControls.DataBoundControl));
            if (result)
            {
                result2 = typeof(System.Web.UI.WebControls.FormView).IsAssignableFrom(_control.GetType()) ||
                    typeof(System.Web.UI.WebControls.GridView).IsAssignableFrom(_control.GetType());
            }
            return result2;
        }
        public static bool IsPageContentContainer(Control _control)
        {
            return typeof(IWACPageContentContainer).IsAssignableFrom(_control.GetType());
        }
        public static bool IsTabControl(Control _control)
        {
            return _control.GetType().IsSubclassOf(typeof(WACTabControl));
        }
        public static bool IsPageControl(Control _control)
        {
            return _control.GetType().IsSubclassOf(typeof(WACPage));
        }
        public static bool IsFormControl(Control _control)
        {
            return _control.GetType().IsSubclassOf(typeof(WACFormControl));
        }
        public static bool IsGridControl(Control _control)
        {
            return _control.GetType().IsSubclassOf(typeof(WACGridControl));
        }
        public static bool IsListControl(Control _control)
        {
            return _control.GetType().IsSubclassOf(typeof(WACListControl));
        }
        public static bool IsFilterControl(Control _control)
        {
            return _control.GetType().IsSubclassOf(typeof(WACFilterControl));
        }
        public static bool IsUtilityControl(Control _control)
        {
            return _control.GetType().IsSubclassOf(typeof(WACUtilityControl));
        }
        public static bool IsContainerControl(Control _control)
        {
            return typeof(IWACContainer).IsAssignableFrom(_control.GetType());
        }
        public static bool IsWACControl(Control _control)
        {
            return typeof(IWACControl).IsAssignableFrom(_control.GetType());
        }
        public static bool IsDependentControl(Control _control)
        {
            return typeof(IWACDependentControl).IsAssignableFrom(_control.GetType());
        }
        public static bool IsIndependentControl(Control _control)
        {
            return typeof(IWACIndependentControl).IsAssignableFrom(_control.GetType());
        }
        public static bool IsDisconnectedControl(Control _control)
        {
            return typeof(IWACDisconnectedControl).IsAssignableFrom(_control.GetType());
        }
        public ConnectorFactory GetConnectorFactory(HttpSessionState _session)
        {
            ConnectorFactory cFac = null;
            var a = connectorFactories.Where(w => w.Key == _session.SessionID).Select(s => s);
            if (a.Any())
                cFac = a.Single().Value as ConnectorFactory;
            if (cFac == null)
            {
                cFac = ConnectorFactory.GetConnectorFactoryForSession(_session);
                connectorFactories.TryAdd(_session.SessionID, cFac);
            }
            return cFac;
        }
        private WACViewModel GetViewModelForControl(Control _control)
        {
            // Get ConnectorFactory for this session, create one if not there
            ConnectorFactory cFac = GetConnectorFactory(_control.Page.Session);            
            // Get Connector for this control, create one if not there
            WACControlConnector cCon = GetConnector(_control, cFac);
            // Get ViewModel for this control, create one if not there
            WACViewModel vMod = cCon.ViewModel;
            if (vMod == null)
                vMod = cCon.GetViewModel(_control, cFac) as WACViewModel;
            return vMod;
        }
       
        private WACViewModel GetViewModelForControl(ServiceRequest _request)
        {
            // Get ConnectorFactory for this session, create one if not there
            ConnectorFactory cFac = GetConnectorFactory(_request.Requestor.Page.Session);
            // Get Connector for this control, create one if not there
            WACControlConnector cCon = GetConnector(_request);
            // Get ViewModel for this control, create one if not there
            WACViewModel vMod = cCon.ViewModel;
            if (vMod == null)
                vMod = cCon.GetViewModel(_request.Requestor, cFac) as WACViewModel;
            return vMod;
        }
        private WACFilterControl getFilterFromRequest(ServiceRequest _request)
        {
            WACFilterControl filter = null;
            if (IsFilterControl(_request.Requestor))
                filter = (WACFilterControl)_request.Requestor;
            else
                filter = (WACFilterControl)_request.ServiceFor;
            return filter;
        }
        private WACFormControl getFormFromRequest(ServiceRequest _request)
        {
            WACFormControl form = null;
            if (IsFormControl(_request.Requestor))
                form = (WACFormControl)_request.Requestor;
            else
                form = (WACFormControl)_request.ServiceFor;
            return form;
        }
        private FormView getFormViewFromRequest(ServiceRequest _request)
        {
            WACFormControl form = getFormFromRequest(_request);
            FormView fv = null;
            if (form != null)
            {
                fv = _request.ServiceFor as FormView;
                if (fv == null)
                    fv = WACGlobal_Methods.FindControl<FormView>(form);
            }
            return fv;
        }
        private WACGridControl getGridFromRequest(ServiceRequest _request)
        {
            WACGridControl grid = null;
            if (IsGridControl(_request.Requestor))
                grid = (WACGridControl)_request.Requestor;
            else
                grid = (WACGridControl)_request.ServiceFor;
            return grid;
        }
        private GridView getGridViewFromRequest(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            GridView gv = null;
            if (grid != null)
            {
                gv = _request.ServiceFor as GridView;
                if (gv == null)
                    gv = WACGlobal_Methods.FindControl<GridView>(grid);               
            }
            return gv;
        }
        private WACListControl getListFromRequest(ServiceRequest _request)
        {
            WACListControl list = null;
            if (IsListControl(_request.Requestor))
                list = (WACListControl)_request.Requestor;
            else
                list = (WACListControl)_request.ServiceFor;
            return list;
        }
        private ListView getListViewFromRequest(ServiceRequest _request)
        {
            WACListControl list = getListFromRequest(_request);
            ListView lv = null;
            if (list != null)
            {
               lv = _request.ServiceFor as ListView;
                if (lv == null)
                    lv = WACGlobal_Methods.FindControl<ListView>(list);
            }
            return lv;
        }
        private WACControlConnector GetConnector(Control _control)
        {
            // Get ConnectorFactory for this session, create one if not there
            ConnectorFactory cFac = GetConnectorFactory(_control.Page.Session);

            WACControlConnector cCon;
            cCon = GetConnector(_control, cFac);
            //cCon.Connect(_control, cFac);
            return cCon;
        }
        private WACControlConnector GetConnector(ServiceRequest _request)
        {
            Control _control = _request.Requestor;
            return GetConnector(_control);
        }
        private WACControlConnector GetConnector(Control _control, ConnectorFactory _cfac)
        {
            WACControlConnector cCon = _cfac.GetConnectorForControl(_control);
            return cCon;
        }
        private WACControlConnector GetConnector(string _controlID, ConnectorFactory _cfac)
        {
            return _cfac.GetConnector(_controlID);
        }        
        private void RegisterConnectorComponents(ServiceRequest _request)
        {
            try
            {
                Control _control = _request.Requestor;
                ConnectorFactory _factory = GetConnectorFactory(_control.Page.Session);
                _factory.RegisterClassTypes();
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message, 0);
            }    
        }
        private void ConnectControl(ServiceRequest _request)
        {
            Control _control = _request.Requestor;
            ConnectControl(_control); 
        }
        private void ConnectControl(Control _control)
        {
            WACControlConnector _connector;
            ConnectorFactory _factory = GetConnectorFactory(_control.Page.Session);
            try
            {
                _connector = _factory.GetConnectorForControl(_control);
                _connector.Connect(_control, _factory);
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message + " In " + this.ToString() + ".ConnectControl", 0);
            }
        }
        private void UpdatePanelUpdate(ServiceRequest _request)
        {
            IWACContainer container = (IWACContainer)_request.Requestor;
            if (container != null)
                container.UpdatePanelUpdate();
        }
        private void ShowFormModal(ServiceRequest _request)
        {
            WACFormControl form = _request.Requestor as WACFormControl;
            WACFormViewModel fvvm = GetViewModelForControl(form) as WACFormViewModel;
            if (fvvm != null && fvvm.ModalDisplayed)
                fvvm.ShowModal(form, true);
        }
        private void HideFormModal(ServiceRequest _request)
        {
            WACFormControl form = _request.Requestor as WACFormControl;
            WACFormViewModel fvvm = GetViewModelForControl(form) as WACFormViewModel;
            fvvm.ShowModal(form, false);
        }
        private void GetCustomString(ServiceRequest _request)
        {
            //if (IsFormControl(_request.Requestor))
            //    WACFormViewModel vm = GetViewModelForControl(_request) as WACFormViewModel;
            //else if (IsUtilityControl(_request.Requestor))
            //    WACUtilityControlViewModel vm = GetViewModelForControl(_request) as WACUtilityControlViewModel;
            
        }
        #endregion
        #region Container Services
        private void InitControl(ServiceRequest _request)
        {
            ((WACControl)_request.ServiceFor).InitControl(_request.ParmList);
        }
        private void InitControls(ServiceRequest _request)
        {
            if (IsFormControl(_request.ServiceFor))
                InitFormChildControls((WACFormControl)_request.ServiceFor, _request.ParmList);
            else
                foreach (WACControl _control in ControlList(_request.ServiceFor))
                    _control.InitControl(_request.ParmList);
        }
        private void InitFormChildControls(WACFormControl form, List<WACParameter> parms)
        {
            IList<WACControl> _controls = null;
            switch (form.CurrentState)
            {
                case WACFormControl.FormState.OpenView:
                    _controls = ControlList(form).Where(w => w.IsActiveReadOnly == true).ToList<WACControl>();
                    break;
                case WACFormControl.FormState.OpenInsert:
                    _controls = ControlList(form).Where(w => w.IsActiveInsert == true).ToList<WACControl>();
                    break;
                case WACFormControl.FormState.OpenUpdate:
                    _controls = ControlList(form).Where(w => w.IsActiveUpdate == true).ToList<WACControl>();                          
                    break;
                default:
                    break;
            }
            if (_controls != null)
                foreach (WACControl _control in _controls)
                    _control.InitControl(parms);
        }
        private void UpdateControl(ServiceRequest _request)
        {
            ((WACControl)_request.ServiceFor).UpdateControl(_request.ParmList);
        }
        private void UpdateControls(ServiceRequest _request)
        {
            foreach (WACControl _control in ControlList(_request.ServiceFor))
                _control.UpdateControl(_request.ParmList);
        }         
        private void ResetControls(ServiceRequest _request)
        {
            foreach (WACControl _control in ControlList(_request.ServiceFor))
                _control.ResetControl();
        }
        private void ReBindControls(ServiceRequest _request)
        {
            foreach (WACControl _control in ControlList(_request.ServiceFor))
                _control.ReBindControl();
        }  
        private void CloseControls(ServiceRequest _request)
        {
            foreach (WACControl _control in ControlList(_request.ServiceFor))
                _control.CloseControl();
        }
        private void SetMasterKeyForContainer(ServiceRequest _request)
        { 
            //WACParameter wp = WACParameter.RemoveParameterType(_request.ParmList, WACParameter.ParameterType.MasterKey);
            foreach (WACControl _control in ControlList(_request.ServiceFor))
            {
                WACViewModel vm = GetViewModelForControl(_request.ServiceFor);
                if (vm.ListSource != null)
                    vm.ListSource.SetMasterKeyValue(WACParameter.GetParameterValue(_request.ParmList, WACParameter.ParameterType.MasterKey));
            }
        }
        private void ContentStateChanged(ServiceRequest _request)
        {
            ((WACControl)_request.Requestor).StateChanged(_request.ParmList);
        }
        private void ContainerVisibility(ServiceRequest _request)
        {           
            ((WACViewModel)GetViewModelForControl(_request.ServiceFor)).AdjustContentVisibility(ControlList(_request.Requestor),
                ((WACFormControl)getFormFromRequest(_request)));
        }

        #endregion
        #region Form Services
        
        private void OpenFormViewReadOnly(ServiceRequest _request)
        {
            WACFormControl form = getFormFromRequest(_request);
            WACFormViewModel fvvm = (WACFormViewModel)GetViewModelForControl(form);
            fvvm.OpenFormViewReadOnly(form, _request.ParmList);
        }
        private void OpenFormViewInsert(ServiceRequest _request)
        {
            WACFormControl form = getFormFromRequest(_request);
            WACFormViewModel fvvm = (WACFormViewModel)GetViewModelForControl(form);
            fvvm.OpenFormViewInsert(form, _request.ParmList);        
        }
        private void OpenFormViewUpdate(ServiceRequest _request)
        {
            WACFormControl form = getFormFromRequest(_request);
            WACFormViewModel fvvm = (WACFormViewModel)GetViewModelForControl(form);
            fvvm.OpenFormViewUpdate(form, _request.ParmList);
        }
        private void ClearFormView(ServiceRequest _request)
        {
            WACFormControl form = getFormFromRequest(_request);
            WACFormViewModel fvvm = (WACFormViewModel)GetViewModelForControl(form);
            CloseFormView(_request);
        }
        private void CloseFormView(ServiceRequest _request)
        {
            // should only be called from Form Control, all other controls should call ClearFormView
            WACFormControl form = getFormFromRequest(_request);
            FormView fv = getFormViewFromRequest(_request);
            WACFormViewModel fvvm = (WACFormViewModel)GetViewModelForControl(form);
            fvvm.CloseFormView(form, fv);
        }
        private void ReturnFormToReadOnly(ServiceRequest _request)
        {
            WACFormControl form = getFormFromRequest(_request);
            WACFormViewModel fvvm = (WACFormViewModel)GetViewModelForControl(form);
            fvvm.ReturnToViewMode(form);
        }
        private void ReBindFormView(ServiceRequest _request)
        {
            WACFormControl form = getFormFromRequest(_request);
            FormView fv = getFormViewFromRequest(_request);
            WACFormViewModel fvvm = (WACFormViewModel)GetViewModelForControl(form);
            fvvm.BindFormView(fv);
        }
       
        #endregion
        #region Tab Services
        private void SetMasterKeyInTabs(ServiceRequest _request)
        {
            // called from page level form 
            WACControlConnector topCon = GetConnector(_request);
            //var x = topCon.Connections.Values.Where(w => IsTabControl(w)).Select(s => s);
            //foreach (Control c in x)
            //{
            //    if (IsTabControl(c))
            //    {
            //        WACTabControlViewModel tcvm = GetViewModelForControl(c) as WACTabControlViewModel;
            //        if (tcvm != null)
            //        {
            //            tcvm.SetPrimaryKey(_request.ParmList);
            //            tcvm.MyTabState = WACTabControlViewModel.TabState.Initialized;
            //        }
            //    }
            //}
        }
        private void OpenTabControl(ServiceRequest _request)
        {
            // called from page level form 
            WACControlConnector topCon = GetConnector(_request);
            int tabIndex = Convert.ToInt32(WACParameter.GetParameterValue(_request.ParmList, "activeTabindex"));
            //var x = topCon.Connections.Values.Where(w => IsTabControl(w)).Select(s => s);
            //foreach (WACTabControl tc in x)
            //{
            //    if (tabIndex == tc.MyTabIndex)
            //    {
            //        WACTabControlViewModel tcvm = GetViewModelForControl(tc) as WACTabControlViewModel;
            //        if (tcvm != null && tcvm.MyTabState == WACTabControlViewModel.TabState.Initialized)
            //        {
            //            _request.ParmList.Add(tcvm.ListSource.MasterKey);
            //            tc.OpenTabControl(tc, _request.ParmList);
            //            tcvm.MyTabState = WACTabControlViewModel.TabState.Open;
            //        }
            //    }
            //}
        }
        private void ResetTabControls(ServiceRequest _request)
        {
            // called from page level form 
            WACControlConnector topCon = GetConnector(_request);
            //var x = topCon.Connections.Values.Where(w => IsTabControl(w)).Select(s => s);
            //foreach (Control tc in x)
            //{
            //    WACControlConnector con = GetConnector(tc);
            //    WACTabControlViewModel tcvm = GetViewModelForControl(tc) as WACTabControlViewModel;
            //    ((WACTabControl)tc).ResetTabControl(tc, _request.ParmList);
            //    tcvm.MyTabState = WACTabControlViewModel.TabState.Reset;
            //}
        }
        #endregion
        #region Filter Services
     
        private void InitFilterDDLs(ServiceRequest _request)
        {
            WACFilterViewModel fvm = null;
            WACFilterControl fc = getFilterFromRequest(_request);
            if (fc != null)
                fvm = GetViewModelForControl(fc) as WACFilterViewModel;
            if (fvm != null)
                fvm.InitializeFilterDropDownLists(fc, _request.ParmList);
        }
        private void ResetFilterDDLs(ServiceRequest _request)
        {
            WACFilterViewModel fvm = null;
            WACFilterControl fc = getFilterFromRequest(_request);
            if (fc != null)
                fvm = GetViewModelForControl(fc) as WACFilterViewModel;
            if (fvm != null)
                fvm.ResetReloadFilterDropDownLists(fc, _request.ParmList);
        }
        #endregion
        #region GridView Services
        private void OpenGridView(ServiceRequest _request)
        {
            // binds a gridview to a previously populated list
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);
            gvvm.OpenGridViewReadOnly(grid, gv);
        }
        private void GridViewList(ServiceRequest _request)
        {
            _request.ParmList.Clear();
            FilteredGridViewList(_request);
        }       
        private void FilteredGridViewList(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);
            gvvm.GetViewList(grid, gv, _request.ParmList);
        }       
        private void HandleGridViewSelection(ServiceRequest _request)
        {
            WACFormControl form = _request.ServiceFor as WACFormControl;
            if (form != null)
                form.OpenView(form, _request.ParmList);
        }      
        private void ContainedGridChanged(ServiceRequest _request)
        {
            WACGridControl grid = _request.Requestor as WACGridControl;
            grid.StateChanged(_request.ParmList);
        }
        private void SortGridView(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);
            gvvm.SortGridView(gv, _request.ParmList);
        }
        private void PageGridView(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);
            gvvm.PageGridView(gv,_request.ParmList);
        }
        private void ClearGridView(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);       
            if (grid != null && gv != null)
                gvvm.ClearGridView(grid,gv);
        }
        private void CloseGridView(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);       
            if (grid != null && gv != null)
                gvvm.CloseGridView(gv);
        }
        private void RefreshGridView(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);
            gvvm.GetViewList(grid, gv, _request.ParmList);
            gvvm.OpenGridViewReadOnly(grid, gv);
        }
        private void ReBindGrid(ServiceRequest _request)
        {
            WACGridControl grid = getGridFromRequest(_request);
            WACGridViewModel gvvm = GetViewModelForControl(grid) as WACGridViewModel;
            GridView gv = getGridViewFromRequest(_request);
            gvvm.BindGridView(gv);
        }
        #endregion
        #region List Services
        private void ListRowCount(ServiceRequest _request)
        {
            WACDataBoundListControl list = (WACDataBoundListControl)_request.ServiceFor;
            WACListControlViewModel lvvm = GetViewModelForControl(list) as WACListControlViewModel;
            lvvm.GetListRowCount(list);
        }
        #endregion
        #region ListView Services

        private void ListViewList(ServiceRequest _request)
        {
            _request.ParmList.Clear();
            FilteredListViewList(_request);
        }
        private void FilteredListViewList(ServiceRequest _request)
        {
            WACListControl list = getListFromRequest(_request);
            ListView lv = getListViewFromRequest(_request);
            WACListViewModel lvvm = GetViewModelForControl(list) as WACListViewModel;
            if (_request.ParmList.Count() < 1)
                lvvm.GetViewList(list, lv);
            else
                lvvm.GetViewList(list,lv, _request.ParmList);
        }        
        private void OpenListView(ServiceRequest _request)
        {
            // binds a listview to a previously populated list
            WACListControl list = getListFromRequest(_request);
            ListView lv = getListViewFromRequest(_request);
            WACListViewModel lvvm = GetViewModelForControl(list) as WACListViewModel;
            lvvm.OpenListViewReadOnly(list, lv);
        }
        private void ClearListView(ServiceRequest _request)
        {
            WACListControl list = getListFromRequest(_request);
            ListView lv = getListViewFromRequest(_request);
            WACListViewModel lvvm = GetViewModelForControl(list) as WACListViewModel;
            lvvm.ClearListView(list, lv);
        }
        private void HandleListViewSelection(ServiceRequest _request)
        {
            WACFormControl form = _request.ServiceFor as WACFormControl;
            if (form != null)
                form.OpenView(form, _request.ParmList);
        }
        private void ReBindList(ServiceRequest _request)
        {
            WACListControl list = getListFromRequest(_request);
            ListView lv = getListViewFromRequest(_request);
            WACListViewModel vm = GetViewModelForControl(list) as WACListViewModel;
            vm.BindListView(lv);
        }
        #endregion
        #region DropDownList Services
        private void ReSetControlDDLs(ServiceRequest _request)
        {
            WACViewModel vm = GetViewModelForControl(_request.Requestor);
            vm.ReSetDDLs(_request.ServiceFor);
        }
        private void BindControlDDLs(ServiceRequest _request)
        {
            WACViewModel vm = GetViewModelForControl(_request.Requestor);
            vm.BindControlDDLs(_request.ServiceFor, _request.ParmList);
        }
        private void BindDDL(ServiceRequest _request)
        {
            WACViewModel vm = GetViewModelForControl(_request.Requestor);
            vm.BindSingleDDL(_request.ServiceFor, _request.ParmList);
        }
        private void ReBindDDLs(ServiceRequest _request)
        {
            WACViewModel vm = GetViewModelForControl(_request) as WACViewModel;
            vm.BindControlDDLs(_request.ServiceFor);
        }
        private void SaveDDLSelectedValues(ServiceRequest _request)
        {
            WACViewModel vm = GetViewModelForControl(_request);
            vm.SaveDDLState(_request.ServiceFor);
        }
        #endregion       
        #region Custom Controls       
        
        private void InitCustomControls(ServiceRequest _request)
        {
            //foreach (Control c in WACGlobal_Methods.FindControls<WACUtilityControl>(_request.ServiceFor))
            //    ((WACUtilityControl)c).InitControls(_request.ParmList);
            throw new NotImplementedException();
        }
        private void InitCustomControl(ServiceRequest _request)
        {
            throw new NotImplementedException();
            //WACUtilityControl cc = _request.ServiceFor as WACUtilityControl;
            //if (cc != null)
            //    cc.InitControls(_request.ParmList);
        }
        //private void CustomControlFilterAction(ServiceRequest _request)
        //{
        //    // used by CustomControls contained in Filter Controls
        //    if (IsUtilityControl(_request.Requestor))
        //    {
        //        WACControlConnector con = GetConnector(_request);
        //        WACControlConnector pCon = con.ContainerConnector;
        //        Control c = pCon.ConnectorFor;
        //        WACFilterControl filter = null;
        //        //if (!IsFilterControl(c))
        //        //{
        //        //    filter = pCon.GetContainedFilterControl() as WACFilterControl;
        //        //    if (filter == null)
        //        //        filter = pCon.GetPeerFilterControl(c) as WACFilterControl;
        //        //}
        //        //else
        //        //    filter = (WACFilterControl)c;
        //        //if (filter != null)
        //        //{
        //        //    _request.ParmList.Add(new WACParameter("FilterControl", filter));
        //        //    WACUtilityControlViewModel ccvm = GetViewModelForControl(_request.Requestor) as WACUtilityControlViewModel;
        //        //    ccvm.CustomAction(_request.ParmList);
        //        //}
        //    }
        //}
        private void CustomControlAction(ServiceRequest _request)
        {         
            if (IsGridControl(_request.Requestor))
            {
                throw new NotImplementedException();
                //WACGridControl wgc = _request.Requestor as WACGridControl;
                //WACGridViewModel wgcvm = GetViewModelForControl(wgc) as WACGridViewModel;
                //WACUtilityControl wcc = WACParameter.GetParameterValue(_request.ParmList, "CommandSource") as WACUtilityControl;
                //if (wcc != null)
                //    wcc.CustomControlAction(_request.ParmList);
            }
            else if (IsUtilityControl(_request.Requestor))
            {
                WACUtilityControlViewModel ccvm = GetViewModelForControl(_request.Requestor) as WACUtilityControlViewModel;
                ccvm.CustomAction(_request.ParmList);
            }
        }
        private void CustomControlReset(ServiceRequest _request)
        {
            WACUtilityControl wcc = null;
            foreach (Control c in WACGlobal_Methods.FindControls<WACUtilityControl>(_request.Requestor))
            {
                wcc = c as WACUtilityControl;
                if (wcc != null)
                    wcc.ResetControl();
            }         
        }
        private void ContainedCustomControlChanged(ServiceRequest _request)
        {
            //WACContainer container = null;
            //WACControlConnector con = GetConnector(_request);
            //WACControlConnector containerCon = con.ContainerConnector;
            //container = containerCon.ConnectorFor as WACContainer;
            //if (container != null)
            //    container.StateChanged(_request.ParmList);
        }

        
        #endregion
        #region Data Services
        private void InsertItem(ServiceRequest _request)
        {
            if (IsFormControl(_request.Requestor))
            {
                ((WACFormControl)_request.Requestor).CurrentState = WACFormControl.FormState.ItemUpdated;
                WACFormViewModel vMod = GetViewModelForControl(_request) as WACFormViewModel;
                vMod.Insert(_request.Requestor as WACFormControl, _request.ServiceFor as FormView, _request.ParmList);
            }
            else if (IsTabControl(_request.Requestor))
            {
                WACTabControlViewModel vMod = GetViewModelForControl(_request) as WACTabControlViewModel;
                vMod.Insert(_request.Requestor, _request.ParmList);
            }
            
        }
        private void DeleteItem(ServiceRequest _request)
        {           
            //DeleteItem is a authorization Pass-thru to Delete()
            //Insert and Update authorization checked on form changemode
            if (IsFormControl(_request.Requestor))
            {
                ((WACFormControl)_request.Requestor).CurrentState = WACFormControl.FormState.ItemDeleted;
                WACFormViewModel vMod = GetViewModelForControl(_request) as WACFormViewModel;
                vMod.Delete(_request.Requestor as WACFormControl, _request.ServiceFor as FormView, _request.ParmList);
            }
            else if (IsGridControl(_request.Requestor))
            {

                ((WACGridControl)_request.Requestor).CurrentState = WACGridControl.ListState.ItemDeleted;
                WACGridViewModel vMod = GetViewModelForControl(_request) as WACGridViewModel;
                vMod.Delete(_request.Requestor as WACGridControl, _request.ServiceFor as GridView, _request.ParmList);
            }
        }
        private void UpdateItem(ServiceRequest _request)
        {
           
            if (IsFormControl(_request.Requestor))
            {
                ((WACFormControl)_request.Requestor).CurrentState = WACFormControl.FormState.ItemUpdated;
                WACFormViewModel vMod = GetViewModelForControl(_request) as WACFormViewModel;
                vMod.Update(_request.Requestor as WACFormControl, _request.ServiceFor as FormView, _request.ParmList);
            }
            else if (IsGridControl(_request.Requestor))
            {

                ((WACGridControl)_request.Requestor).CurrentState = WACGridControl.ListState.ItemDeleted;
                WACGridViewModel vMod = GetViewModelForControl(_request) as WACGridViewModel;
               // vMod.Update(_request.Requestor as WACGridControl, _request.ServiceFor as GridView, _request.ParmList);
            }
        }
        #endregion
        public void ServiceRequest(ServiceRequest _request)
        {
            ProvideService(_request);
        }
        private void ProvideService(ServiceRequest _request)
        {
            switch (_request.ServiceRequested)
            {
                case ServiceTypes.InitControls:
                    InitControls(_request);
                    break;
                case ServiceTypes.ResetControls:
                    ResetControls(_request);
                    break;
                case ServiceTypes.ReBindControls:
                    ReBindControls(_request);
                    break;
                case ServiceTypes.UpdateControl:
                    UpdateControl(_request);
                    break;
                case ServiceTypes.UpdateControls:
                    UpdateControls(_request);
                    break;
                case ServiceTypes.CloseControls:
                    CloseControls(_request);
                    break;
                case ServiceTypes.ContainerVisibility:
                    ContainerVisibility(_request);
                    break;
                case ServiceTypes.SetMasterKeyForContainer:
                    SetMasterKeyForContainer(_request);
                    break;
                case ServiceTypes.InitCustomControls:
                    InitCustomControls(_request);
                    break;
                case ServiceTypes.CustomControlAction:
                    CustomControlAction(_request);
                    break;
                case ServiceTypes.CustomControlReset:
                    CustomControlReset(_request);
                    break;
                case ServiceTypes.MakeConnections:
                    ConnectControl(_request);
                    break;
                case ServiceTypes.RegisterConnectionComponents:
                    RegisterConnectorComponents(_request);
                    break;
                case ServiceTypes.InitCustomControl:
                    InitCustomControl(_request);
                    break;
                case ServiceTypes.FilteredGridViewList:
                    FilteredGridViewList(_request);
                    break;
                case ServiceTypes.GridViewList:
                    GridViewList(_request);
                    break;
                case ServiceTypes.HandleGridViewSelection:
                    HandleGridViewSelection(_request);
                    break;
                case ServiceTypes.OpenGridView:
                    OpenGridView(_request);
                    break;
                case ServiceTypes.FilteredListViewList:
                    FilteredListViewList(_request);
                    break;
                case ServiceTypes.ListViewList:
                    ListViewList(_request);
                    break;
                case ServiceTypes.HandleListViewSelection:
                    HandleListViewSelection(_request);
                    break;
                case ServiceTypes.OpenListView:
                    OpenListView(_request);
                    break;
                case ServiceTypes.ClearListView:
                    ClearListView(_request);
                    break;
                case ServiceTypes.ListRowCount:
                    ListRowCount(_request);
                    break;
                case ServiceTypes.ContentStateChanged:
                    ContentStateChanged(_request);
                    break;
                case ServiceTypes.SetMasterKeyInTabs:
                    SetMasterKeyInTabs(_request);
                    break;
                case ServiceTypes.InitTabControl:
                    OpenTabControl(_request);
                    break;
                case ServiceTypes.ResetTabControls:
                    ResetTabControls(_request);
                    break;
                case ServiceTypes.InsertItem:
                    InsertItem(_request);
                    break;
                case ServiceTypes.UpdateItem:
                    UpdateItem(_request);
                    break;
                case ServiceTypes.DeleteItem:
                    DeleteItem(_request);
                    break;
                case ServiceTypes.UpdatePanelUpdate:
                    UpdatePanelUpdate(_request);
                    break;
                case ServiceTypes.InitFilterDDLs:
                    InitFilterDDLs(_request);
                    break;
                case ServiceTypes.ResetReloadFilterDDLs:
                    ResetFilterDDLs(_request);
                    break;
                case ServiceTypes.SaveDDLSelectedValues:
                    SaveDDLSelectedValues(_request);
                    break;
                case ServiceTypes.RefreshGridView:
                    RefreshGridView(_request);
                    break;
                case ServiceTypes.PageGridView:
                    PageGridView(_request);
                    break;
                case ServiceTypes.SortGridView:
                    SortGridView(_request);
                    break;
                case ServiceTypes.ClearGridView:
                    ClearGridView(_request);
                    break;
                case ServiceTypes.CloseGridView:
                    CloseGridView(_request);
                    break;
                case ServiceTypes.ReturnFormToViewMode:
                    ReturnFormToReadOnly(_request);
                    break;
                case ServiceTypes.ShowFormModal:
                    ShowFormModal(_request);
                    break;
                case ServiceTypes.HideFormModal:
                    HideFormModal(_request);
                    break;
                case ServiceTypes.ClearFormView:
                    ClearFormView(_request);
                    break;
                case ServiceTypes.CloseFormView:
                    CloseFormView(_request);
                    break;
                case ServiceTypes.OpenFormViewInsert:
                    OpenFormViewInsert(_request);
                    break;
                case ServiceTypes.OpenFormViewUpdate:
                    OpenFormViewUpdate(_request);
                    break;
                case ServiceTypes.OpenFormViewReadOnly:
                    OpenFormViewReadOnly(_request);
                    break;
                case ServiceTypes.ReBindFormView:
                    ReBindFormView(_request);
                    break;
                case ServiceTypes.ReBindGrid:
                    ReBindGrid(_request);
                    break;
                case ServiceTypes.ReBindList:
                    ReBindList(_request);
                    break;
                case ServiceTypes.BindControlDDLs:
                    BindControlDDLs(_request);
                    break;
                case ServiceTypes.ReSetDDLs:
                    ReSetControlDDLs(_request);
                    break;
                case ServiceTypes.ReBindDDLs:
                    ReBindDDLs(_request);
                    break;
                case ServiceTypes.BindDDL:
                    BindDDL(_request);
                    break;
                case ServiceTypes.CustomString:
                    GetCustomString(_request);
                    break;
               
                //case ServiceTypes.ControlVisibility:
                //    ControlVisibility(_request);
                //    break;
                //case ServiceTypes.ListVisibility:
                //    ListVisibility(_request);
                //    break;
                default:
                    break;
            }
            return;
        }
    }
}
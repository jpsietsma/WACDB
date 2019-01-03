using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Validators;
using AjaxControlToolkit;
using System.Reflection;


/// <summary>
/// Summary description for AbstractWACFormViewModel
/// </summary>
namespace WAC_ViewModels
{
    public abstract class WACFormViewModel : WACViewModel
    {

        public abstract void Insert(WACFormControl wfc, FormView fv, List<WACParameter> parms);
        public abstract void Update(WACFormControl wfc, FormView fv, List<WACParameter> parms);
        public abstract void Delete(WACFormControl wfc, FormView fv, List<WACParameter> parms);
        public abstract string CustomString(List<WACParameter> parms);

        public bool ShowModalReadOnly { get; set; }
        public bool ModalDisplayed { get; set; }
        public void ContentStateChanged(Control form)
        {
            ServiceRequest sr = new ServiceRequest(form);
            if (ListSource.SelectedKey != null)
                sr.ParmList.Add(ListSource.SelectedKey);
            if (ListSource.PrimaryKey != null)
                sr.ParmList.Add(ListSource.PrimaryKey);
            if (ListSource.MasterKey != null)
                sr.ParmList.Add(ListSource.MasterKey);
            sr.ParmList.Add(new WACParameter(null, ((WACFormControl)form).CurrentState, WACParameter.ParameterType.FormState));
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContentStateChanged;
            //OnServiceRequested(form, new ServiceRequestEventArgs(sr));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public override void ContentStateChanged(Control form, Enum state)
        {
            ServiceRequest sr = new ServiceRequest(form);
            if ((WAC_UserControls.WACFormControl.FormState)state != WAC_UserControls.WACFormControl.FormState.ItemDeleted)
            {
                if (ListSource.SelectedKey != null)
                    sr.ParmList.Add(ListSource.SelectedKey);
                if (ListSource.PrimaryKey != null)
                    sr.ParmList.Add(ListSource.PrimaryKey);
                if (ListSource.MasterKey != null)
                    sr.ParmList.Add(ListSource.MasterKey);
            }
            sr.ParmList.Add(new WACParameter(null, state, WACParameter.ParameterType.FormState));
            sr.ServiceRequested = ServiceFactory.ServiceTypes.ContentStateChanged;
            //OnServiceRequested(form, new ServiceRequestEventArgs(sr));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public void InitControls(WACFormControl form, List<WACParameter> parms)
        {
            ServiceRequest sr = new ServiceRequest(form);
            sr.ServiceFor = form;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.InitControls;
            parms.Add(new WACParameter(string.Empty, form.CurrentState, WACParameter.ParameterType.FormState));
            sr.ParmList = parms;
            //OnServiceRequested(form, new ServiceRequestEventArgs(sr));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public void CloseControls(WACFormControl form)
        {
            ServiceRequest sr = new ServiceRequest(form);
            sr.ServiceFor = form;
            sr.ServiceRequested = ServiceFactory.ServiceTypes.CloseControls;
           // OnServiceRequested(form, new ServiceRequestEventArgs(sr));
            ServiceFactory.Instance.ServiceRequest(sr);
            sr = null;
        }
        public void BindFormView(FormView fv)
        {
            try
            {
                if (ListSource.FList != null)
                {
                    fv.DataKeyNames = ListSource.DataKeyNames;
                    fv.DataSource = ListSource.FList;
                    fv.DataBind();
                }
            }
            catch (Exception ex) { WACAlert.Show(ex.Message + " In " + this.ToString() + ".BindFormView", 0); }
        }
        public void BindFormViewForInsert(FormView fv, List<WACParameter> parms, MasterDetailDataObject.EmptyItemGetterDelegate _getItem)
        {
            try
            {
                ListSource.FList = _getItem();
                fv.DataKeyNames = ListSource.DataKeyNames;
                fv.DataSource = ListSource.FList;
                fv.DataBind();
            }
            catch (Exception ex) { WACAlert.Show(ex.Message + " In " + this.ToString() + ".BindFormViewForInsert", 0); }
        }
        public void BindFormView(FormView fv, List<WACParameter> parms, MasterDetailDataObject.ItemGetterDelegate _getItem)
        {
            try
            {
                fv.DataKeyNames = ListSource.DataKeyNames;
                fv.DataSource = ListSource.GetSingleItemList(parms, _getItem);
                fv.DataBind();
            }
            catch (Exception ex) { WACAlert.Show(ex.Message + " In " + this.ToString() + ".BindFormView", 0); }
        }
        
        public virtual  void OpenFormViewReadOnly(WACFormControl form, List<WACParameter> parms)
        {
            try
            {
                FormView fv = WACGlobal_Methods.FindControl<FormView>(form);
                checkDependencies(fv, parms, DataProvider, ListSource);
                //ListSource.SelectedKey = WACParameter.GetSelectedKey(parms);
                fv.ChangeMode(FormViewMode.ReadOnly);
                BindFormView(fv, parms, DataProvider.GetItem);
                form.CurrentState = WACFormControl.FormState.OpenView;
                InitControls(form, parms);
                ShowModal(form, ShowModalReadOnly);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message + " In " + this.ToString() + ".OpenFormViewReadOnly", 0); }
            finally
            {
                ContentStateChanged(form);
            }
        }

        public void OpenFormViewInsert(WACFormControl form, List<WACParameter> parms)
        {
            if (UserAuthorized("Insert", AuthorizationArea))
            {
                FormView fv = WACGlobal_Methods.FindControl<FormView>(form);
                checkDependencies(fv, parms, DataProvider, ListSource);
                ListSource.PrimaryKey = new WACParameter(DataProvider.PrimaryKeyName(), -1, WACParameter.ParameterType.PrimaryKey);                
                parms.Add(ListSource.PrimaryKey);
                fv.ChangeMode(FormViewMode.Insert);        
                BindFormViewForInsert(fv, parms, DataProvider.GetNewItem);               
                BindControlDDLs(fv, parms);
                form.CurrentState = WACFormControl.FormState.OpenInsert;
                InitControls(form, parms);
                ShowModal(form, true);
                ContentStateChanged(form);
            }
        }
        public virtual void OpenFormViewUpdate(WACFormControl form, List<WACParameter> parms)
        {
            
            if (UserAuthorized("Update", AuthorizationArea))
            {
                FormView fv = WACGlobal_Methods.FindControl<FormView>(form);
                checkDependencies(fv, parms, DataProvider, ListSource);
                fv.ChangeMode(FormViewMode.Edit);               
                BindFormView(fv, parms, DataProvider.GetItem);                
                BindControlDDLs(fv, parms, ListSource.GetCurrentItem());
                form.CurrentState = WACFormControl.FormState.OpenUpdate;
                InitControls(form, parms);
                ShowModal(form, true);
                ContentStateChanged(form);
            }
        }
        private void checkDependencies(FormView fv, List<WACParameter> parms, WACDataProvider dp, MasterDetailDataObject ds)
        {
            ListSource = ds;
            DataProvider = dp;
            try
            {
                if (fv == null)
                    throw new Exception("Null FormView in FormViewModel");
                if (ListSource == null)
                    throw new Exception("Null ListSource in FormViewModel: " + fv.ID);
                if (parms == null)
                    throw new Exception("Null Key list in FormViewModel: " + fv.ID);
                if (DataProvider == null)
                    throw new Exception("Null DataProvider in FormViewModel: " + fv.ID);
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message, 0);
            }          
        }

        public void ShowModal(WACFormControl form, bool show)
        {
            ModalPopupExtender mpe = WACGlobal_Methods.FindControl<ModalPopupExtender>(form);
            if (mpe != null)
            {
                if (show)
                {
                    mpe.Show();
                    ModalDisplayed = true;
                }
                else
                {
                    mpe.Hide();
                    ModalDisplayed = false;
                }
            }
        }
        public void CloseFormView(WACFormControl form, FormView fv)
        {
            fv.ChangeMode(FormViewMode.ReadOnly);
            fv.DataSource = null;
            fv.DataBind();
            form.CurrentState = WACFormControl.FormState.Closed;
            ShowModal(form, false);
            CloseControls(form);
            ContentStateChanged(form);
        }

        public void ReturnToViewMode(WACFormControl form)
        {
            FormView fv = WACGlobal_Methods.FindControl<FormView>(form);
            fv.ChangeMode(FormViewMode.ReadOnly);
            fv.DataSource = ListSource.GetSingleItemList();
            fv.DataBind();            
            ShowModal(form, true);
            ContentStateChanged(form);
        }
       
    }
}
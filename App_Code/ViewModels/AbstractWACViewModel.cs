using System;
using System.Collections.Generic;
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
using WAC_Containers;
using System.Collections.Concurrent;



/// <summary>
/// Summary description  WACViewModel
/// </summary>
namespace WAC_ViewModels
{
    public abstract class WACViewModel
    {
        public WACDataProvider DataProvider { get; set; }
        public MasterDetailDataObject ListSource { get; set; }
        public ConcurrentDictionary<string, DDLDataObject> DDLBinders =
           new ConcurrentDictionary<string, DDLDataObject>();
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string AuthorizationArea { get; set; }

        public delegate void ServiceRequestHandler(object sender, ServiceRequestEventArgs s);
        public event ServiceRequestHandler ServiceRequestEvent;
        public void OnServiceRequested(object sender, ServiceRequestEventArgs s)
        {
            if (ServiceRequestEvent != null)
                ServiceRequestEvent(sender, s);
        }
       
        public abstract void ContentStateChanged(Control _control, Enum e);
        public void AdjustContentVisibility(List<WACControl> ContainedControls, WACFormControl form)
        {
            switch (form.CurrentState)
            {
                case WACFormControl.FormState.Closed:
                    foreach (WACControl c in ContainedControls)
                        c.Visible = c.DefaultVisibility;
                    break;
                case WACFormControl.FormState.UpdateCanceled:
                    goto case WACFormControl.FormState.OpenView;
                case WACFormControl.FormState.OpenView:
                    foreach (WACControl c in ContainedControls)
                        c.Visible = c.IsActiveReadOnly;
                    break;
                case WACFormControl.FormState.OpenInsert:
                    foreach (WACControl c in ContainedControls)
                        c.Visible = c.IsActiveInsert;
                    break;
                case WACFormControl.FormState.OpenUpdate:
                    foreach (WACControl c in ContainedControls)
                        c.Visible = c.IsActiveUpdate;
                    break;
                default:
                    break;
            }
        }
       
        public bool UserAuthorized(string _action, string _section)
        {
            return WACGlobal_Methods.Security_UserCanPerformAction(UserID, _action.Substring(0, 1),
                _section.Substring(0, 1), _section.ToLower(), "msg" + _action);
        }
        public void ReSetDDLs(Control c)
        {
            foreach (string ddlID in DDLBinders.Keys)
            {
                DDLDataObject ddlDO = DDLBinders[ddlID];
                DropDownList ddl = c.FindControl(ddlID) as DropDownList;
                ddlDO.ReSetDDL(ddl);
            }
        }
        public void BindSingleDDL(Control c, List<WACParameter> parms)
        {
            string ddlID = (string)WACParameter.GetParameterValue(parms, "ddlID");
            DDLDataObject ddlDO = DDLBinders[ddlID];
            if (ddlDO != null)
                bindControlDDL(c, parms, null, ddlDO, ddlID);
        }
        public void BindControlDDLs(Control c)
        {
            foreach (string ddlID in DDLBinders.Keys)
            {
                DDLDataObject ddlDO = DDLBinders[ddlID];
                if (ddlDO == null || ddlDO.IsEmptyList())
                    continue;
                if (ddlDO.IsADependent)
                {
                    DDLDataObject prereq = DDLBinders[ddlDO.DependsOn];
                    if (prereq == null || !prereq.Selected)
                        continue;
                }
                bindControlDDL(c, ddlDO, ddlID);

            }
        }
        public void BindControlDDLs(Control c, List<WACParameter> parms)
        {
            object item = null;
            BindControlDDLs(c, parms, item);
        }       
        public void BindControlDDLs(Control c, List<WACParameter> parms, object item)
        {
            foreach (string ddlID in DDLBinders.Keys)
            {
                DDLDataObject ddlDO = DDLBinders[ddlID];
                if (ddlDO == null)
                    continue;
                if (ddlDO.IsADependent)
                {
                    DDLDataObject prereq = DDLBinders[ddlDO.DependsOn];
                    if (prereq == null || !prereq.Selected)
                        continue;
                }
                bindControlDDL(c, parms, item, ddlDO, ddlID);
            }
        }
        private void bindControlDDL(Control c, List<WACParameter> parms, object item, DDLDataObject ddlDO, string ddlID)
        {
            DropDownList ddl = c.FindControl(ddlID) as DropDownList;
            if (ddl != null)
                ddlDO.DataBindDDL(ddl, parms, item);
        }
        private void bindControlDDL(Control c, DDLDataObject ddlDO, string ddlID)
        {
            DropDownList ddl = c.FindControl(ddlID) as DropDownList;
            if (ddl != null)
                ddlDO.DataBindDDL(ddl);
        }
        public void SaveDDLState(Control c)
        {
            foreach (string ddlID in DDLBinders.Keys)
            {
                DropDownList ddl = c.FindControl(ddlID) as DropDownList;
                DDLDataObject ddlDO = DDLBinders[ddlID];
                if (ddl != null && ddlDO != null)
                    ddlDO.SaveSelectedValue(ddl);
            }
        }
        public bool Insert<T>(List<WACParameter> parms, WACValidator.InsertValidateDelegate<T> vDel)
        {
            bool result = false;
            T item = default(T);
            parms.Add(new WACParameter("created_by", UserName, WACParameter.ParameterType.Property));
            try
            {
                item = vDel(parms);
                item = ((IWACDataProvider<T>)DataProvider).Insert(item);
                ListSource.SetCurrentItem<T>(item, ((IWACDataProvider<T>)DataProvider).GetPrimaryKey);
                ListSource.VList.Clear();
                result = true;              
            }
            catch (WACEX_ValidationException wex)
            {
                WACAlert.Show("Insert failed, validation error: " + wex.ErrorText + " In " + this.ToString() + ".Insert", 0);
            }
            catch (WACEX_GeneralDatabaseException gex)
            {
                WACAlert.Show("Insert failed: " + gex.ErrorText + " In " + this.ToString() + ".Insert", gex.ErrorCode);
            }
            catch (Exception ex)
            {
                WACAlert.Show("Insert failed: " + ex.Message + " In " + this.ToString() + ".Insert", 0);
            }
            return result;
        }        
        public bool Update<T>(List<WACParameter> parms, WACValidator.UpdateValidateDelegate<T> vDel)
        {
            bool result = false;
            T item = default(T);
            parms.Add(new WACParameter("modified_by", UserName));
            parms.Add(new WACParameter("modified", DateTime.Now));
            try
            {
                item = vDel(parms,(T)ListSource.GetCurrentItem());
                ListSource.RemoveItemFromViewlist<T>(item);
                ((IWACDataProvider<T>)DataProvider).Update(item);
                ListSource.SetCurrentItem<T>(item, ((IWACDataProvider<T>)DataProvider).GetPrimaryKey);
                result = true;
            }
            catch (WACEX_ValidationException wex)
            {
                WACAlert.Show("Update failed, validation error: " + wex.ErrorText + " In " + this.ToString() + ".Update", 0);
            }
            catch (WACEX_GeneralDatabaseException gex)
            {
                WACAlert.Show("Update failed: " + gex.ErrorText + " In " + this.ToString() + ".Update", gex.ErrorCode);
            }
            catch (Exception ex)
            {
                WACAlert.Show("Update failed: " + ex.Message + " In " + this.ToString() + ".Update", 0);
            }
            return result;
        }
        public bool Delete<T>(List<WACParameter> parms)
        {
            bool result = false;
            try
            {
                if (UserAuthorized("Delete", AuthorizationArea))
                {
                    IList li = ListSource.GetSingleItemList(parms, DataProvider.GetItem);
                    T item = (T)li[0];
                    ((IWACDataProvider<T>)DataProvider).Delete(item);                                     
                    ListSource.RemoveItemFromViewlist<T>(item);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                WACAlert.Show("Delete failed: " + ex.Message + " In " + this.ToString() + ".Delete", 0);
            }
            return result;
        }      
    }
}
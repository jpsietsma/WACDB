using System;
using System.Collections.Generic;
using WAC_Services;
using System.Collections;
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
using WAC_DataProviders;

/// <summary>
/// Summary description for DDLDataObject
/// </summary>
namespace WAC_DataObjects
{
    public class DDLDataObject
    {
        public delegate IList<DDLListItem> ListGetterDelegate(List<WACParameter> parms, object o);
        public delegate string GetSelectedDelegate(object o);
        public ListGetterDelegate GetListDel { get; set; }
        public GetSelectedDelegate GetSelectedDel { get; set; }
        public bool HasADependent { get; set; }
        public bool IsADependent { get; set; }
        public bool Selected { get; set; }
        public string DependedOnBy { get; set; }
        public string DependsOn { get; set; }
        public DropDownList DependentDDL { get; set; }
        IList<DDLListItem> Items = null;

        public DDLDataObject(DDLDataProvider ddlDP) : this(ddlDP.GetDDLList) { }
        public DDLDataObject(DDLDataProvider ddlDP, bool _hasDependent, string _dependedOnBy) : this(ddlDP.GetDDLList, _hasDependent, _dependedOnBy) { }
        public DDLDataObject(DDLDataProvider ddlDP, bool _hasDependent, string _dependedOnBy, bool _isDependent, string _dependsOn)
            : this(ddlDP.GetDDLList, _hasDependent, _dependedOnBy, _isDependent, _dependsOn) {}

        public DDLDataObject(ListGetterDelegate del) 
        {
            GetListDel = del;
            Selected = false;
            IsADependent = false;
            HasADependent = false;
        }
        public DDLDataObject(ListGetterDelegate del, bool _hasDependent, string _dependedOnBy)
        {
            GetListDel = del;
            Selected = false;
            DependedOnBy = _dependedOnBy;
            IsADependent = false;
            HasADependent = _hasDependent;
        }
        public DDLDataObject(ListGetterDelegate del, bool _hasDependent, string _dependedOnBy, bool _isDependent, string _dependsOn)
        {
            GetListDel = del;
            Selected = false;
            DependedOnBy = _dependedOnBy;
            DependsOn = _dependsOn;
            HasADependent = _hasDependent;
            IsADependent = _isDependent;
        }
        private void loadList(List<WACParameter> parms, object item)
        {
            Items = GetListDel(parms, item);
            DDLListItem select = new DDLListItem(string.Empty, "[Select]");
            if (Items != null)
                Items.Insert(0, select);
        }
        private void setSelectedValue(DropDownList ddl)
        {
            var a = Items.Where(w => w.SelectedValue == true);
            if (a.Any())
                ddl.SelectedValue = a.FirstOrDefault().DataValueField;
        }
        private void bind(DropDownList ddl)
        {
            ddl.DataSource = null;
            ddl.DataTextField = "DataTextField";
            ddl.DataValueField = "DataValueField";
            ddl.DataSource = Items;
            ddl.DataBind();
        }
        public void DataBindDDL(DropDownList ddl)
        {
            try
            {
                if (Items != null)
                {
                    bind(ddl);
                    setSelectedValue(ddl);
                }
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message + " In " + this.ToString(), -1);
            }
        }
        public void DataBindDDL(DropDownList ddl, List<WACParameter> parms, object item)
        {
            if (Items == null || Items.Count() < 2 || ddl.Items.Count < 2)
                loadList(parms, item);
            try
            {
                if (Items != null)
                {
                    bind(ddl);
                    setSelectedValue(ddl);
                }
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message + " In " + this.ToString(), -1);
            }
        }
        public void ReSetDDL(DropDownList ddl)
        {
            Selected = false;
            if (Items != null)
                Items.Clear();
            ddl.DataSource = null;
            ddl.DataTextField = null;
            ddl.DataValueField = null;
            ddl.DataBind();
        }
        public bool IsEmptyList()
        {
            return Items != null ? Items.Count < 2 : true;
        }
        public void SaveSelectedValue(DropDownList ddl)
        {
            if (Items != null)
            {
                ClearSelectedValue();
                if (!string.IsNullOrEmpty(ddl.SelectedValue))
                {
                    var a = Items.Where(w => w.DataValueField.CompareTo(ddl.SelectedValue) == 0).First();
                    a.SelectedValue = true;
                    Selected = true;
                }
            }
        }
       
        public void ClearSelectedValue()
        {
            Selected = false;
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    item.SelectedValue = false;
                }
            }
        }
        public bool IsDependedOn()
        {
            return HasADependent;
        }
        public bool IsDependent()
        {
            return IsADependent;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Text;
using System.Linq.Expressions;


public partial class WACHR_HRFilters : System.Web.UI.UserControl
{
    private Dictionary<string, string> filters = new Dictionary<string, string>();
    public Dictionary<string, string> Filters
    {
        get { return filters; }
    }

    const string selectString = "new(pk_participant,pk_participantWAC,Employee,location AS Location," +
                "SLT, fieldStaff AS FieldStaff,position AS Position,classification as EEOClass," +
                "classificationHR as HRClass, sector as HRSector)";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            resetAll();
        }
        else
        {
            reloadAll();
        }
    }

    private void resetAll()
    {
        try
        {
            Filters.Clear();
            clearDDLs();
            loadDDLs(new List<string>());
            upHR_WACEmployeeFilter.Update();
            // fire event to update employee table
            List<string> _filters = new List<string>();
            if (cbActiveEmployee.Checked)
                _filters.Add("ActiveEmployeeFilter");
            OnFilterChanged(this, new FilterChangedEventArgs(_filters));
        }
        catch { }
    }

    private void reloadAll()
    {
        rebuildFiltersFromDDls();
        loadDDLs(WhereClause());
    }

    #region Events and Handlers

    protected void lbResetFilters_Click(object sender, EventArgs e)
    {
        resetAll();
    }
    public void FilterReset(object sender, FilterChangedEventArgs fa)
    {
        resetAll();
    }
    public event InsertEmployeeEvent OnInsertEmployeeClicked;
    public delegate void InsertEmployeeEvent();
    protected void lbHR_WACEmployee_Insert_Click(object sender, EventArgs e)
    {
        try
        {
            if (OnInsertEmployeeClicked != null)
            {
                OnInsertEmployeeClicked();
            }
        }
        catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
    }

    public event FilterChangedEventHandler OnFilterChanged;
    public delegate void FilterChangedEventHandler(object sender, FilterChangedEventArgs e);
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (OnFilterChanged != null)
            {
                DropDownList ddl = (DropDownList)sender;
                setFilter(ddl);
                // fire event to update employee table
                OnFilterChanged(this, new FilterChangedEventArgs(WhereClause()));
            }
        }
        catch { }
        loadDDLs(WhereClause());
    }

   protected void cbActiveEmployee_CheckChanged(object sender, EventArgs e)
   {
       OnFilterChanged(this, new FilterChangedEventArgs(WhereClause()));
   }

    #endregion

   
    #region Filters

    private void rebuildFiltersFromDDls()
    {
        // recreate list of filters from selected valued of DDLs
        List<DropDownList> ddls = new List<DropDownList>();
        WACGlobal_Methods.GetControlList<DropDownList>(upHR_WACEmployeeFilter.Controls, ddls);
        foreach (DropDownList d in ddls)
        {
            setFilter(d);
        }
        
    }

    private void setFilter(DropDownList d)
    {
        try
        {
            if (!string.IsNullOrEmpty(d.SelectedValue))
            {
                if (d.SelectedValue.Contains("[SELECT ALL]"))
                {
                    removeFilter(ddlIDString(d));
                }
                else
                {
                    addFilter(ddlIDString(d), d.SelectedValue);
                }
            }
            else
                removeFilter(ddlIDString(d));
        }
        catch { }
    }
   
    private void addFilter(string key, string value)
    {
        if (!filters.ContainsKey(key))
        {
            filters.Add(key, value);
        }
    }

    private void removeFilter(string key)
    {
        if (filters.ContainsKey(key))
            filters.Remove(key);
    }

    #endregion 

    #region DDLs

    private void clearDDLs()
    {
        // Clear All Items and Selected Values
        List<DropDownList> ddls = new List<DropDownList>();
        WACGlobal_Methods.GetControlList<DropDownList>(upHR_WACEmployeeFilter.Controls, ddls);
        foreach (DropDownList d in ddls)
        {
            d.Items.Clear();
            d.SelectedValue = null;
        }
    }
    private void loadDDLs(List<string> whereClause)
    {
        string predicate;

        //bool activeEmployeeFilter = cbActiveEmployee.Checked;
        //if (whereClause.Contains("ActiveEmployeeFilter"))
        //{
        //    int i = whereClause.LastIndexOf("ActiveEmployeeFilter");
        //    activeEmployeeFilter = true;
        //    whereClause.RemoveAt(i);
        //}
            
        if (whereClause.Count < 1)
        {
            whereClause.Add("null");
            predicate = "Employee != @0";
        }
        else
        {
            predicate = whereClause[whereClause.Count - 1];
            whereClause.RemoveAt(whereClause.Count - 1);
        }
        
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            //IQueryable a;
            //if (activeEmployeeFilter)
            //    a = wac.vw_participantWACs.Where(w => w.start_date.HasValue && !w.finish_date.HasValue);
            //else
            //    a = wac.vw_participantWACs;
            IQueryable q;
            try
            {
                q = wac.vw_participantWACs.Select(selectString).Where(predicate, whereClause.ToArray<string>());
            }
            catch
            {
                q = wac.vw_participantWACs.Select(selectString).Where("Employee != null");
            }
            List<DropDownList> ddls = new List<DropDownList>();
            WACGlobal_Methods.GetControlListAll<DropDownList>(upHR_WACEmployeeFilter.Controls, ddls);
            foreach (DropDownList d in ddls)
            {
                populateDDL(d, q);
                setFilter(d);
            }
            //if (activeEmployeeFilter)
            //    filters.Add("ActiveEmployeeFilter",null);
        }
    }
    
    private void populateDDL(DropDownList ddl, IQueryable subTab)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            string ddlID = ddlIDString(ddl);
            string whereString = "ddlVal != null";
            string selectedValue = null;
            if (!string.IsNullOrEmpty(ddl.SelectedValue)) { selectedValue = ddl.SelectedValue; }

            var x = subTab.Select("new(" + ddlID + " as ddlVal)").Where(whereString).Distinct().OrderBy("ddlVal").Select("ddlVal");

            if (x.Any())
            {
                ddl.Items.Clear();
                ddl.DataSource = x;
                try
                {
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
                    if (!string.IsNullOrEmpty(selectedValue))
                    {
                        ddl.SelectedValue = selectedValue;
                    }
                    setFilter(ddl);
                }
                catch { }
            }
        }
    }

    private string ddlIDString(DropDownList d)
    {
        return d.ID.Substring(d.ID.LastIndexOf('_') + 1);
    }

    #endregion

    //
    // Build a dynamic LINQ where clause from the Filters list
    //
    private List<string> WhereClause()
    {

        List<string> values = new List<string>();
        if (filters.Count < 1)
        {
            if (cbActiveEmployee.Checked)
                values.Add("ActiveEmployeeFilter");
            return values;
        }
        StringBuilder fields = new StringBuilder();
        int tokenNum = 0;
        foreach (string key in filters.Keys)
        {
            string _value = filters[key];
            fields.Append(key);
            if (_value.StartsWith("!"))
            {
                fields.Append(" != @");
                _value = filters[key].TrimStart('!');
            }
            else
                fields.Append(" = @");
            fields.Append(tokenNum++);
            values.Add(_value);
            if (!key.Equals(filters.Keys.Last()))
            {
                fields.Append(" and ");
            }
        }
        values.Add(fields.ToString());
        if (cbActiveEmployee.Checked)
            values.Add("ActiveEmployeeFilter");
        return values;
    }

    

 
}
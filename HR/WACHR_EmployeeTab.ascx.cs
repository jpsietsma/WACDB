using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

public partial class WACHR_HREmployeeTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Populate_HR_WACEmployees_GridView(new List<string>());
        //}
    }

    public void gvLoad(object sender, FilterChangedEventArgs e)
    {
        List<string> filters = e.Filters;
        Session.Remove("Filters");
        Session["Filters"] = filters;
        Populate_HR_WACEmployees_GridView(filters);
    }

    //SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");

    #region GridView Events
    

    public event ViewEmployeeDetails_Click OnViewDetailsClicked;
    public delegate void ViewEmployeeDetails_Click(PrimaryKeyEventArgs e);

    protected void gvHR_WACEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHR_WACEmployees.PageIndex = e.NewPageIndex;
        applyFilters();
    }

    protected void gvHR_WACEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortOn = Session["SortExpression"] as string;
        string sortDir = Session["SortDirection"] as string;
        // if column clicked is column currently sorted, toggle sort direction
        if (sortOn == null)
        {
            sortOn = e.SortExpression;
            sortDir = "asc";
        }
        else if (sortOn != null && sortOn.Equals(e.SortExpression))
        {
            if (sortDir == null || sortDir.Contains("desc"))
                sortDir = "asc";
            else
                sortDir = "desc";
        }
        Session.Remove("SortExpression");
        Session.Remove("SortDirection");
        Session["SortExpression"] = e.SortExpression;
        Session["SortDirection"] = sortDir;
        applyFilters();
    }

    protected void gvHR_WACEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvHR_WACEmployees.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        PrimaryKeyEventArgs args = new PrimaryKeyEventArgs(Convert.ToInt32(gvHR_WACEmployees.SelectedDataKey.Value));
        OnViewDetailsClicked(args);

        if (gvHR_WACEmployees.SelectedIndex != -1) ViewState["SelectedValue"] = gvHR_WACEmployees.SelectedValue.ToString();       
    }

    private void applyFilters()
    {
        List<string> filters = (List<string>)Session["Filters"];
        if (filters == null)
        {
            filters = new List<string>();
        }
        Populate_HR_WACEmployees_GridView(filters);
    }

    private void Populate_HR_WACEmployees_GridView(List<string> filters)
    {
        string predicate;
        string orderBy;
        bool activeEmployeeFilter = filters.Contains("ActiveEmployeeFilter");
        if (activeEmployeeFilter)
            filters.RemoveAt(filters.Count - 1);

        if (filters.Count < 1)
        {
            filters.Add("null");
            predicate = "Employee != @0";
        }
        else
        {
            predicate = filters[filters.Count - 1];
            filters.RemoveAt(filters.Count - 1);
        }
        string sortOn = Session["SortExpression"] as string;
        string sortDir = Session["SortDirection"] as string;
        if (sortOn != null && sortDir != null)
        {
            orderBy = sortOn + " " + sortDir;
        }
        else
            orderBy = "Employee";

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            IQueryable a;
            if (activeEmployeeFilter)
                a = wac.vw_participantWACs.Where(w => w.start_date.HasValue && !w.finish_date.HasValue);
            else
                a = wac.vw_participantWACs;
            var x = a.Select("new(pk_participant,pk_participantWAC,Employee,location AS Location," +
                "SLT, fieldStaff AS FieldStaff,position AS Position,start_date AS StartDate,classification as EEOClass," +
                "classificationHR as HRClass, sector as HRSector, start_date, finish_date)").Where(predicate,
                filters.ToArray<string>()).OrderBy(orderBy);

            gvHR_WACEmployees.DataSource = x;
            gvHR_WACEmployees.DataKeyNames = new string[] { "pk_participantWAC" };
            gvHR_WACEmployees.DataBind();

            lblCount.Text = "Records: " + x.Count();

            if (gvHR_WACEmployees.Rows.Count == 1)
            {
                gvHR_WACEmployees.SelectedIndex = 0;
                gvHR_WACEmployees.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
            }
            else
            {
                gvHR_WACEmployees.SelectedRowStyle.BackColor = gvHR_WACEmployees.RowStyle.BackColor;
            }
        }
        if (filters.Contains("null"))
            filters.Clear();
        if (activeEmployeeFilter)
            filters.Add("ActiveEmployeeFilter");

    }


    #endregion
}
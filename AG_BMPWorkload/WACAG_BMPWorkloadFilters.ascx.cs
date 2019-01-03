using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Linq;

public partial class AG_BMPWorkload_WACAG_BMPWorkloadFilters : System.Web.UI.UserControl
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
        Filters.Clear();
        clearDDLs();
        loadDDLs(new List<string>());
        upWACAG_BMPWorkloadFilter.Update();
        // fire event to update workload table
        OnFilterChanged(this, new FilterChangedEventArgs(new List<string>()));
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



    #endregion


    #region Filters

    private void rebuildFiltersFromDDls()
    {
        // recreate list of filters from selected valued of DDLs
        List<DropDownList> ddls = new List<DropDownList>();
        WACGlobal_Methods.GetControlList<DropDownList>(upWACAG_BMPWorkloadFilter.Controls, ddls);
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
        WACGlobal_Methods.GetControlList<DropDownList>(upWACAG_BMPWorkloadFilter.Controls, ddls);
        foreach (DropDownList d in ddls)
        {
            d.Items.Clear();
            d.SelectedValue = null;
        }
    }
    private void loadDDLs(List<string> whereClause)
    {
        string predicate;
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
            //IQueryable q;
            //try
            //{
            //    q = wac.vw_participantWACs.Select(selectString).Where(predicate, whereClause.ToArray<string>());
            //}
            //catch
            //{
            //    q = wac.vw_participantWACs.Select(selectString).Where("Employee != null");
            //}
            List<DropDownList> ddls = new List<DropDownList>();
            //WACGlobal_Methods.GetControlList<DropDownList>(upHR_WACEmployeeFilter.Controls, ddls);
            //foreach (DropDownList d in ddls)
            //{
            //    populateDDL(d, q);
            //    setFilter(d);
            //}
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

            //var x = subTab.Select("new(" + ddlID + " as ddlVal)").Where(whereString).Distinct().OrderBy("ddlVal").Select("ddlVal");

            //if (x.Any())
            //{
            //    ddl.Items.Clear();
            //    ddl.DataSource = x;
            //    try
            //    {
            //        ddl.DataBind();
            //        ddl.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            //        if (!string.IsNullOrEmpty(selectedValue))
            //        {
            //            ddl.SelectedValue = selectedValue;
            //        }
            //        setFilter(ddl);
            //    }
            //    catch { }
            //}
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
            return values;
        StringBuilder fields = new StringBuilder();
        int tokenNum = 0;
        foreach (string key in filters.Keys)
        {
            fields.Append(key);
            fields.Append(" = @");
            fields.Append(tokenNum++);
            values.Add(filters[key]);
            if (!key.Equals(filters.Keys.Last()))
            {
                fields.Append(" and ");
            }
        }
        values.Add(fields.ToString());
        return values;
    }
    #region Filters

    private void PopulateFilters()
    {
        PopulateFilter_Workload();
        PopulateFilter_Owner();
        PopulateFilter_Group();
        PopulateFilter_Planner();
        PopulateFilter_Agency();
        PopulateFilter_Technician();
        PopulateFilter_Status();
        PopulateFilter_Funding();

        //Populate_BMP_Workload_GridView();
    }

    //protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ClearFormView();
    //    gv.EditIndex = -1;
    //    lv.DataSource = "";
    //    lv.DataBind();
    //    PopulateFilters();
    //}

    //protected void lbResetFilters_Click(object sender, EventArgs e)
    //{
    //    Session["orderAg_BMPWorkload"] = "";
    //    //ClearFormView();
    //    ddlFilter_Workload.Items.Clear();
    //    ddlFilter_Owner.Items.Clear();
    //    ddlFilter_Group.Items.Clear();
    //    ddlFilter_Planner.Items.Clear();
    //    ddlFilter_Agency.Items.Clear();
    //    ddlFilter_Technician.Items.Clear();
    //    ddlFilter_Status.Items.Clear();
    //    ddlFilter_Funding.Items.Clear();
    //    PopulateFilters();
    //}

    private void PopulateFilter_Workload()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sWorkload = null;
            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) sWorkload = ddlFilter_Workload.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.year != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T" && w.bmp_ag_workloadSupports.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            var b = a.GroupBy(g => g.year).OrderByDescending(o => o.Key).Select(s => s.Key);

            ddlFilter_Workload.Items.Clear();
            ddlFilter_Workload.DataSource = b;
            ddlFilter_Workload.DataBind();
            ddlFilter_Workload.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sWorkload))
            {
                try { ddlFilter_Workload.SelectedValue = sWorkload; }
                catch { }
            }
        }
    }
    private void PopulateFilter_Group()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sGroup = null;
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) sGroup = ddlFilter_Group.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T" && w.bmp_ag_workloadSupports.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            var b = a.GroupBy(g => g.bmp_ag.farmBusiness.fk_groupPI_code).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_Group.Items.Clear();
            ddlFilter_Group.DataSource = b;
            ddlFilter_Group.DataBind();
            ddlFilter_Group.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sGroup)) ddlFilter_Group.SelectedValue = sGroup;
        }
    }
    private void PopulateFilter_Status()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sStatus = null;
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) sStatus = ddlFilter_Status.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.fk_statusBMPWorkload_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T" && w.bmp_ag_workloadSupports.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            var b = a.GroupBy(g => g.list_statusBMPWorkload.status).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_Status.Items.Clear();
            ddlFilter_Status.DataSource = b;
            ddlFilter_Status.DataBind();
            ddlFilter_Status.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sStatus)) ddlFilter_Status.SelectedValue = sStatus;
        }
    }
    private void PopulateFilter_Funding()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sFunding = null;
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) sFunding = ddlFilter_Funding.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.fk_agWorkloadFunding_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T" && w.bmp_ag_workloadSupports.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);

            var b = a.GroupBy(g => g.list_agWorkloadFunding.source).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_Funding.Items.Clear();
            ddlFilter_Funding.DataSource = b;
            ddlFilter_Funding.DataBind();
            ddlFilter_Funding.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sFunding)) ddlFilter_Funding.SelectedValue = sFunding;
        }
    }
    private void PopulateFilter_Planner()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sPlanner = null;
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) sPlanner = ddlFilter_Planner.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T" && w.bmp_ag_workloadSupports.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            var b = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.groupLeader == "Y").groupLeader == "Y").GroupBy(g => g.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.groupLeader == "Y").list_designerEngineer.designerEngineer).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_Planner.Items.Clear();
            ddlFilter_Planner.DataSource = b;
            ddlFilter_Planner.DataBind();
            ddlFilter_Planner.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sPlanner)) ddlFilter_Planner.SelectedValue = sPlanner;
        }
    }
    private void PopulateFilter_Owner()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sOwner = null;
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) sOwner = ddlFilter_Owner.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd != "").Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T" && w.bmp_ag_workloadSupports.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            var b = a.GroupBy(g => g.bmp_ag.farmBusiness.ownerStr_dnd).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_Owner.Items.Clear();
            ddlFilter_Owner.DataSource = b;
            ddlFilter_Owner.DataBind();
            ddlFilter_Owner.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sOwner)) ddlFilter_Owner.SelectedValue = sOwner;
        }
    }
    private void PopulateFilter_Agency()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sAgency = null;
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) sAgency = ddlFilter_Agency.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.fk_agency_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T" && w.bmp_ag_workloadSupports.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            var b = a.GroupBy(g => g.list_agency.agency).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_Agency.Items.Clear();
            ddlFilter_Agency.DataSource = b;
            ddlFilter_Agency.DataBind();
            ddlFilter_Agency.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sAgency)) ddlFilter_Agency.SelectedValue = sAgency;
        }
    }
    private void PopulateFilter_Technician()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sTechnician = null;
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) sTechnician = ddlFilter_Technician.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").fk_BMPWorkloadSupport_code == "T").Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            var b = a.GroupBy(g => g.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T").list_designerEngineer.designerEngineer).OrderBy(o => o.Key).Select(s => s.Key);

            ddlFilter_Technician.Items.Clear();
            ddlFilter_Technician.DataSource = b;
            ddlFilter_Technician.DataBind();
            ddlFilter_Technician.Items.Insert(0, new ListItem("[SELECT ALL]", ""));
            if (!string.IsNullOrEmpty(sTechnician)) ddlFilter_Technician.SelectedValue = sTechnician;
        }
    }


    public class BMPWorkLoad
    {
        public int Year { get; set; }
        public string WorkGroup { get; set; }
        public string Status { get; set; }
        public string Funding { get; set; }
        public int Planner { get; set; }
        public string Owner { get; set; }
        public string Agency { get; set; }
        public int Technician { get; set; }
        public string BMPdescription { get; set; }
    }

    private IEnumerable<BMPWorkLoad> preLoadBMPWorkloads()
    {
        IEnumerable<BMPWorkLoad> b = null;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            // eager load workload data
            DataLoadOptions dlOpt = new DataLoadOptions();
            dlOpt.LoadWith<bmp_ag_workload>(c => c.bmp_ag);

            b = from a in wac.bmp_ag_workloads 
                select new BMPWorkLoad { Year = Convert.ToInt16(a.year), 
                    WorkGroup = a.bmp_ag.farmBusiness.fk_groupPI_code,    
                    Status = a.fk_statusBMPWorkload_code,
                    Funding = a.fk_agWorkloadFunding_code,
                    Planner = Convert.ToInt16(a.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers),
                    Owner = a.bmp_ag.farmBusiness.ownerStr_dnd,
                    Agency = a.fk_agency_code,
                    Technician = Convert.ToInt16(a.bmp_ag_workloadSupports),
                    BMPdescription = a.bmp_ag.bmp_nbr
                };

        }
        return b;
    }

    #endregion
}
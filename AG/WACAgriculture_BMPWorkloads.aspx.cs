using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_CustomControls;
public partial class WACAgriculture_BMPWorkloads : System.Web.UI.Page
{
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["orderAg_BMPWorkload"] = "";

            PopulateFilters();
        }
        lblMessage.Text = "";
    }

    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    #endregion

    #region Filters

    private void PopulateFilters()
    {
        PopulateFilter_Workload();
//        PopulateFilter_FarmID();
        PopulateFilter_Owner();
        PopulateFilter_Group();
        PopulateFilter_Planner();
//        PopulateFilter_Priority();
        PopulateFilter_Agency();
        PopulateFilter_Technician();
        PopulateFilter_Status();
        PopulateFilter_Funding();

        Populate_BMP_Workload_GridView();
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearFormView();
        gv.EditIndex = -1;
        lv.DataSource = "";
        lv.DataBind();
        PopulateFilters();
    }

    protected void lbResetFilters_Click(object sender, EventArgs e)
    {
        Session["orderAg_BMPWorkload"] = "";
        ClearFormView();
        ddlFilter_Workload.Items.Clear();
//        ddlFilter_FarmID.Items.Clear();
        ddlFilter_Owner.Items.Clear();
        ddlFilter_Group.Items.Clear();
        ddlFilter_Planner.Items.Clear();
//        ddlFilter_Priority.Items.Clear();
        ddlFilter_Agency.Items.Clear();
        ddlFilter_Technician.Items.Clear();
        ddlFilter_Status.Items.Clear();
        ddlFilter_Funding.Items.Clear();
        PopulateFilters();
    }

    private void PopulateFilter_Workload()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sWorkload = null;
            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) sWorkload = ddlFilter_Workload.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.year != null).Select(s => s);

//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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

  
    private void PopulateFilter_Owner()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sOwner = null;
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) sOwner = ddlFilter_Owner.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd != "").Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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

    private void PopulateFilter_Group()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sGroup = null;
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) sGroup = ddlFilter_Group.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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

    private void PopulateFilter_Planner()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sPlanner = null;
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) sPlanner = ddlFilter_Planner.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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

    private void PopulateFilter_Agency()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sAgency = null;
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) sAgency = ddlFilter_Agency.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.fk_agency_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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

    private void PopulateFilter_Status()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            string sStatus = null;
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) sStatus = ddlFilter_Status.SelectedValue;

            var a = wDataContext.bmp_ag_workloads.Where(w => w.fk_statusBMPWorkload_code != null).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
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

    #endregion

    #region GridView Events

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Populate_BMP_Workload_GridView();
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderAg_BMPWorkload"] = e.SortExpression;
        Populate_BMP_Workload_GridView();
    }

    protected void gv_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        fv.ChangeMode(FormViewMode.ReadOnly);
        if (WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_WL)) fv.ChangeMode(FormViewMode.Edit);
        Populate_BMP_Workload_FormView(Convert.ToInt32(gv.SelectedDataKey.Value));
        if (gv.SelectedIndex != -1) ViewState["SelectedValue"] = gv.SelectedValue.ToString();
    }

    protected void ddlWorkload_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_WL))
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvr = ddl.Parent.Parent as GridViewRow;
            int iGVR_Index = gvr.DataItemIndex;
            while (iGVR_Index >= 10) iGVR_Index = iGVR_Index - 10;
            int i = Convert.ToInt32(gv.DataKeys[iGVR_Index].Value);
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    var a = wac.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == i).Single();
                    a.year = Convert.ToInt16(ddl.SelectedValue);

                    wac.SubmitChanges();
                    PopulateFilters();
                    lblMessage.Text = "Workload Year Updated: " + ddl.SelectedValue;
                }
                catch { WACAlert.Show("Could not update workload year.", 0); }
            }
        }
        else
        {
            PopulateFilters();
            WACAlert.Show("You do not have permission to modify the BMP Workload data.", 0);
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_WL))
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvr = ddl.Parent.Parent as GridViewRow;
            int iGVR_Index = gvr.DataItemIndex;
            while (iGVR_Index >= 10) iGVR_Index = iGVR_Index - 10;
            int i = Convert.ToInt32(gv.DataKeys[iGVR_Index].Value);
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    var a = wac.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == i).Single();
                    if (!string.IsNullOrEmpty(ddl.SelectedValue)) a.fk_statusBMPWorkload_code = ddl.SelectedValue;
                    else a.fk_statusBMPWorkload_code = null;

                    wac.SubmitChanges();
                    PopulateFilters();
                    lblMessage.Text = "Status Updated: " + ddl.SelectedValue;
                }
                catch { WACAlert.Show("Could not update status.", 0); }
            }
        }
        else
        {
            PopulateFilters();
            WACAlert.Show("You do not have permission to modify the BMP Workload data.", 0);
        }
    }

  

    private void Populate_BMP_Workload_GridView()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.bmp_ag_workloads.OrderByDescending(o => o.year).ThenBy(o => o.bmp_ag.farmBusiness.farmID).Select(s => s);

            if (!string.IsNullOrEmpty(ddlFilter_Workload.SelectedValue)) a = a.Where(w => w.year == Convert.ToInt16(ddlFilter_Workload.SelectedValue));
//            if (!string.IsNullOrEmpty(ddlFilter_FarmID.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.farmID == ddlFilter_FarmID.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Owner.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.ownerStr_dnd == ddlFilter_Owner.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Group.SelectedValue)) a = a.Where(w => w.bmp_ag.farmBusiness.fk_groupPI_code == ddlFilter_Group.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Planner.SelectedValue)) 
                a = a.Where(w => w.bmp_ag.farmBusiness.list_groupPI.list_groupPIMembers.First(f => f.list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Planner.SelectedValue);
//            if (!string.IsNullOrEmpty(ddlFilter_Priority.SelectedValue)) a = a.Where(w => w.priority == Convert.ToByte(ddlFilter_Priority.SelectedValue));
            if (!string.IsNullOrEmpty(ddlFilter_Agency.SelectedValue)) a = a.Where(w => w.list_agency.agency == ddlFilter_Agency.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Technician.SelectedValue)) a = a.Where(w => w.bmp_ag_workloadSupports.First(f => f.fk_BMPWorkloadSupport_code == "T" && f.list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue).list_designerEngineer.designerEngineer == ddlFilter_Technician.SelectedValue);
            if (!string.IsNullOrEmpty(ddlFilter_Status.SelectedValue)) a = a.Where(w => w.list_statusBMPWorkload.status == ddlFilter_Status.SelectedValue);
            //if (!string.IsNullOrEmpty(ddlFilter_Funding.SelectedValue)) a = a.Where(w => w.list_agWorkloadFunding.source == ddlFilter_Funding.SelectedValue);

            if (!string.IsNullOrEmpty(Session["orderAg_BMPWorkload"].ToString())) a = a.OrderBy(Session["orderAg_BMPWorkload"].ToString(), null);

            gv.DataKeyNames = new string[] { "pk_bmp_ag_workload" };
            gv.DataSource = a;
            gv.DataBind();

            foreach (GridViewRow gvr in gv.Rows)
            {
                DropDownList ddlWorkload = gvr.FindControl("ddlWorkload") as DropDownList;
                DropDownList ddlStatus = gvr.FindControl("ddlStatus") as DropDownList;
                //DropDownList ddlPercentage = gvr.FindControl("ddlPercentage") as DropDownList;
                //DropDownList ddlFunding = gvr.FindControl("ddlFunding") as DropDownList;

                int iGVR_Index = gvr.DataItemIndex;
                while (iGVR_Index >= 10) iGVR_Index = iGVR_Index - 10;
                int iPK = Convert.ToInt32(gv.DataKeys[iGVR_Index].Value);
                
                int? i = wDataContext.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == iPK).Select(s => s.year).Single();
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(ddlWorkload, i);
                ddlWorkload.Items.RemoveAt(0);
                
                string sStatus = wDataContext.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == iPK).Select(s => s.fk_statusBMPWorkload_code).Single();
                WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMPWorkload_DDL(ddlStatus, sStatus);
                string sFunding = wDataContext.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == iPK).Select(s => s.fk_agWorkloadFunding_code).Single();
                //WACGlobal_Methods.PopulateControl_DatabaseLists_FundingBMPWorkload_DDL(ddlFunding, sFunding);
                //WACGlobal_Methods.PopulateControl_Generic_Percentages_DDL(ddlPercentage, WACGlobal_Methods.SpecialQuery_Agriculture_BMPWorkload_Percentage(iPK), false);
            }

            if (ViewState["SelectedValue"] != null)
            {
                string sSelectedValue = (string)ViewState["SelectedValue"];
                foreach (GridViewRow gvr in gv.Rows)
                {
                    string sKeyValue = gv.DataKeys[gvr.RowIndex].Value.ToString();
                    if (sKeyValue == sSelectedValue)
                    {
                        gv.SelectedIndex = gvr.RowIndex;
                        return;
                    }
                    else gv.SelectedIndex = -1;
                }
            }
            lblCount.Text = "Records: " + a.Count();

            if (gv.Rows.Count == 1)
            {
                gv.SelectedIndex = 0;
                gv.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
                fv.ChangeMode(FormViewMode.ReadOnly);
                if (WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_WL)) fv.ChangeMode(FormViewMode.Edit);
                Populate_BMP_Workload_FormView(Convert.ToInt32(gv.SelectedDataKey.Value));
                if (gv.SelectedIndex != -1) ViewState["SelectedValue"] = gv.SelectedValue.ToString();
            }
        }
    }

    #endregion

    #region FormView Events

    protected void lbFV_Close_Click(object sender, EventArgs e)
    {
        ClearFormView();
        gv.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        PopulateFilters();
        lv.DataSource = "";
        lv.DataBind();
    }

    protected void fv_ModeChanging(object sender, FormViewModeEventArgs e)
    {

        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_WL);
        if (b)
        {
            fv.ChangeMode(e.NewMode);
            Populate_BMP_Workload_FormView(Convert.ToInt32(fv.DataKey.Value));
        }
        else WACAlert.Show("You do not have permission to modify the BMP Workload data.", 0);
    }

    protected void fv_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        DropDownList ddlWorkload = fv.FindControl("ddlWorkload") as DropDownList;
        DropDownList ddlFundingSource = fv.FindControl("ddlFundingSource") as DropDownList;
        DropDownList ddlStatusBMPWorkload = fv.FindControl("ddlStatusBMPWorkload") as DropDownList;
        //DropDownList ddlRollover = fv.FindControl("ddlRollover") as DropDownList;
        //DropDownList ddlRevisionRequired = fv.FindControl("ddlRevisionRequired") as DropDownList;
        //DropDownList ddlSortGroupCode = fv.FindControl("ddlSortGroupCode") as DropDownList;
        //TextBox tbSWCD = fv.FindControl("tbSWCD") as TextBox;
        TextBox tbPriority = fv.FindControl("tbPriority") as TextBox;
        TextBox tbPriorityTech = fv.FindControl("tbPriorityTech") as TextBox;
        //CustomControls_AjaxCalendar tbCalTargetDesignDate = fv.FindControl("tbCalTargetDesignDate") as CustomControls_AjaxCalendar;
        //Calendar caltargetDesign_date = fv.FindControl("UC_EditCalendar_targetDesign_date").FindControl("cal") as Calendar;
        CustomControls_AjaxCalendar tbCalEngConsult = fv.FindControl("tbCalEngConsult") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalSurveyDate = fv.FindControl("tbCalSurveyDate") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProg30 = fv.FindControl("tbCalProg30") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProg60 = fv.FindControl("tbCalProg60") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProg90 = fv.FindControl("tbCalProg90") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalDesignPkg95 = fv.FindControl("tbCalDesignPkg95") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalEngConsultDone = fv.FindControl("tbCalEngConsultDone") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalSurveyDateDone = fv.FindControl("tbCalSurveyDateDone") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProg30Done = fv.FindControl("tbCalProg30Done") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProg60Done = fv.FindControl("tbCalProg60Done") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalProg90Done = fv.FindControl("tbCalProg90Done") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalDesignPkg95Done = fv.FindControl("tbCalDesignPkg95Done") as CustomControls_AjaxCalendar;
        CustomControls_AjaxCalendar tbCalReadyForBidDone = fv.FindControl("tbCalReadyForBidDone") as CustomControls_AjaxCalendar;

        DropDownList ddlAgency = fv.FindControl("ddlAgency") as DropDownList;

        TextBox tbNote = fv.FindControl("tbNote") as TextBox;


        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = wDataContext.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == Convert.ToInt32(fv.DataKey.Value)).Single();

                if (!string.IsNullOrEmpty(ddlWorkload.SelectedValue)) a.year = Convert.ToInt16(ddlWorkload.SelectedValue);
                else a.year = null;

                if (!string.IsNullOrEmpty(ddlFundingSource.SelectedValue)) a.fk_agWorkloadFunding_code = ddlFundingSource.SelectedValue;
                else a.fk_agWorkloadFunding_code = null;
                
                if (!string.IsNullOrEmpty(ddlStatusBMPWorkload.SelectedValue)) a.fk_statusBMPWorkload_code = ddlStatusBMPWorkload.SelectedValue;
                else a.fk_agWorkloadFunding_code = null;
                //if (!string.IsNullOrEmpty(ddlRollover.SelectedValue)) a.rollover = ddlRollover.SelectedValue;
                //else a.rollover = null;

                //if (!string.IsNullOrEmpty(ddlRevisionRequired.SelectedValue)) a.revisionReqd = ddlRevisionRequired.SelectedValue;
                //else a.revisionReqd = null;

                //if (!string.IsNullOrEmpty(ddlSortGroupCode.SelectedValue) && !ddlSortGroupCode.SelectedValue.ToLower().Contains("select")) 
                //    a.fk_BMPWorkloadSortGroup_code = ddlSortGroupCode.SelectedValue;
                //else a.fk_BMPWorkloadSortGroup_code = null;

                //if (!string.IsNullOrEmpty(tbSWCD.Text)) a.SWCD = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbSWCD.Text, 8);
                //else a.SWCD = null;

                if (!string.IsNullOrEmpty(tbPriority.Text))
                {
                    try { a.priority = Convert.ToDecimal(tbPriority.Text); }
                    catch { sb.Append("Could not update Priority. Must be a number (Decimal). "); }
                }
                else a.priority = null;

                if (!string.IsNullOrEmpty(tbPriorityTech.Text))
                {
                    try { a.priorityTech = Convert.ToInt32(tbPriorityTech.Text); }
                    catch { sb.Append("Could not update Priority Tech. Must be a number (Integer). "); }
                }
                else a.priorityTech = null;
                //a.targetDesign_date = tbCalTargetDesignDate.CalDateNullable;
                a.prog_engConsult = tbCalEngConsult.CalDateNullable;
                a.prog_survey = tbCalSurveyDate.CalDateNullable;
                a.prog_inProg30 = tbCalProg30.CalDateNullable;
                a.prog_inProg60 = tbCalProg60.CalDateNullable;
                a.prog_inProg90 = tbCalProg90.CalDateNullable;
                a.prog_design95 = tbCalDesignPkg95.CalDateNullable;
                a.prog_engConsult_done = tbCalEngConsultDone.CalDateNullable;
                a.prog_survey_done = tbCalSurveyDateDone.CalDateNullable;
                a.prog_inProg30_done = tbCalProg30Done.CalDateNullable;
                a.prog_inProg60_done = tbCalProg60Done.CalDateNullable;
                a.prog_inProg90_done = tbCalProg90Done.CalDateNullable;
                a.prog_design95_done = tbCalDesignPkg95Done.CalDateNullable;

                if (!string.IsNullOrEmpty(ddlAgency.SelectedValue)) a.fk_agency_code = ddlAgency.SelectedValue;
                else a.fk_agency_code = null;


                if (!string.IsNullOrEmpty(tbNote.Text)) a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);
                else a.note = null;

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();

                fv.ChangeMode(FormViewMode.ReadOnly);
                Populate_BMP_Workload_FormView(Convert.ToInt32(fv.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fv_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {

        bool b = WACGlobal_Methods.Security_UserObjectCustom(Session["userID"], WACGlobal_Methods.Enum_Security_UserObjectCustom.A_WL);
        if (b)
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.bmp_ag_workload_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbFV_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
        else WACAlert.Show("You do not have permission to delete the BMP Workload data.", 0);
    }

    private void Populate_BMP_Workload_FormView(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.bmp_ag_workloads.Where(w => w.pk_bmp_ag_workload == i).Select(s => s);
            var x = wDataContext.bmp_ag_workload_oneRecord(i);
            fv.DataKeyNames = new string[] { "pk_bmp_ag_workload" };
            fv.DataSource = a;
            fv.DataBind();
            lv.DataSource = x;
            lv.DataBind();

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_Year_DDL(fv.FindControl("ddlWorkload") as DropDownList, a.Single().year);
                WACGlobal_Methods.PopulateControl_DatabaseLists_AgWorkloadFunding_DDL(fv.FindControl("ddlFundingSource") as DropDownList, a.Single().fk_agWorkloadFunding_code, true);
          
                WACGlobal_Methods.PopulateControl_DatabaseLists_Agency_DDL(fv.FindControl("ddlAgency") as DropDownList, a.Single().fk_agency_code);

                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlTechnician_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlChecker_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlConstruction_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv.FindControl("ddlEngineer_Insert") as DropDownList, new string[] { "TECH" }, null, true, a.Single().bmp_ag.farmBusiness.fk_regionWAC_code);

                WACGlobal_Methods.PopulateControl_DatabaseLists_StatusBMPWorkload_DDL(fv.FindControl("ddlStatusBMPWorkload") as DropDownList, a.Single().fk_statusBMPWorkload_code);

                WACGlobal_Methods.PopulateControl_DatabaseLists_FundingBMPWorkload_DDL(fv.FindControl("ddlFundingBMPWorkload") as DropDownList, a.Single().fk_agWorkloadFunding_code);
             
                //DropDownList ddlSortGroupCode = fv.FindControl("ddlSortGroupCode") as DropDownList;
                //var b = wDataContext.list_BMPWorkloadSortGroups.Select(s => s).OrderBy(o => o.pk_BMPWorkloadSortGroup_code);
                //ddlSortGroupCode.DataTextField = "sortGroup";
                //ddlSortGroupCode.DataValueField = "pk_BMPWorkloadSortGroup_code";
                //ddlSortGroupCode.DataSource = b;
                //ddlSortGroupCode.DataBind();
                //WACGlobal_Methods.DdlAddSelect(ddlSortGroupCode);
                //if (a.Single().fk_BMPWorkloadSortGroup_code != null)
                //    ddlSortGroupCode.SelectedValue = a.Single().list_BMPWorkloadSortGroup.pk_BMPWorkloadSortGroup_code;

            }
        }
    }

    private void ClearFormView()
    {
        gv.SelectedRowStyle.BackColor = System.Drawing.Color.Empty;
        fv.DataSource = null;
        fv.DataBind();
    }

    #endregion

    #region BMP Workload Support

    protected void ddlTechnician_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "T");
    }

    protected void ddlChecker_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "C");
    }

    protected void ddlConstruction_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "O");
    }

    protected void ddlEngineer_Insert_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue)) DesignerEngineer_Insert(Convert.ToInt32(ddl.SelectedValue), "E");
    }

    private void DesignerEngineer_Insert(int? iDesignerEngineer, string sBMPWorkloadSupportCode)
    {
        int? i = null;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wDataContext.bmp_ag_workloadSupport_add(Convert.ToInt32(fv.DataKey.Value), sBMPWorkloadSupportCode, iDesignerEngineer, Session["userName"].ToString(), ref i);
                if (iCode == 0) Populate_BMP_Workload_FormView(Convert.ToInt32(fv.DataKey.Value));
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void DesignerEngineer_Delete_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        int iCode = 0;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                iCode = wDataContext.bmp_ag_workloadSupport_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
                if (iCode == 0) Populate_BMP_Workload_FormView(Convert.ToInt32(fv.DataKey.Value));
                else WACAlert.Show("Error Returned from Database.", iCode);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    #endregion

    //protected void BMP_AG_Workload_Progress_Delete_Click(object sender, EventArgs e)
    //{
    //    if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_workloadProgress", "msgDelete"))
    //    {
    //        LinkButton lb = (LinkButton)sender;
    //        int iCode = 0;
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            try
    //            {
    //                iCode = wDataContext.bmp_ag_workloadProgress_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
    //                if (iCode == 0)
    //                {
 
    //                    IQueryable<byte> by = wDataContext.bmp_ag_workloadProgresses.Where(w => w.fk_bmp_ag_workload == Convert.ToInt32(fv.DataKey.Value)).OrderByDescending(o => o.created).Select(s => s.progress_pct);
                     
    //                }
    //                else WACAlert.Show("Error Returned from Database.", iCode);
    //            }
    //            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
    //        }
    //    }
    //}
    public string Decode_ListEasementTypeCode(object code)
    {
        if (code == null)
            return "No";
        switch (code.ToString())
        {
            case "F":
                return "Yes";
            case "P":
                return "Partial";
            default:
                return "No";
        }
    }
}
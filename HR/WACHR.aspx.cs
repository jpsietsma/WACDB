using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_WACHR : System.Web.UI.Page
{
    public int PK_Participant_EmergencyContact
    {
        get { return Convert.ToInt32(Session["PK_Participant_EmergencyContact"]) == 0 ? -1 : Convert.ToInt32(Session["PK_Participant_EmergencyContact"]); }
        set { Session["PK_Participant_EmergencyContact"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // filter changed handler to re-populate gridview
        Filters.OnFilterChanged += new WACHR_HRFilters.FilterChangedEventHandler(EmployeeMaster.gvLoad);
        Filters.OnFilterChanged += new WACHR_HRFilters.FilterChangedEventHandler(EmployeeDetails.HRDetails_Close);
        // Employee Details Insert handler to open details form
        Filters.OnInsertEmployeeClicked += new WACHR_HRFilters.InsertEmployeeEvent(EmployeeDetails.WACEmployee_Insert_Click);
        //Employee Details View handler
        EmployeeMaster.OnViewDetailsClicked += new WACHR_HREmployeeTab.ViewEmployeeDetails_Click(EmployeeDetails.WACEmployee_View_Click);
        //Employee Details Closed handler
        EmployeeDetails.OnEmployeeDetailsClosed += new WACHR_HRFilters.FilterChangedEventHandler(Filters.FilterReset);        
    }

    public int Delegate_GetHRWACEmployeesPK()
    {
        return Convert.ToInt32(((FormView)EmployeeDetails.FindControl("fvHR_WACEmployee")).DataKey.Value);
    }
    public void InvokedMethod_DropDownListByAlphabet(object oType)
    {
          
    }

}
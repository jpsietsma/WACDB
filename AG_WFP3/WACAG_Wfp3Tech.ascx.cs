using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class AG_WACAG_Wfp3Tech : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int FK_Wfp3
    {
        get { return Convert.ToInt32(ViewState["FK_Wfp3"]) == 0 ? -1 : Convert.ToInt32(ViewState["FK_Wfp3"]); }
        set { ViewState["FK_Wfp3"] = value; }
    }
    public int PK_Wfp3Tech
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Tech"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Tech"]); }
        set { ViewState["PK_Wfp3Tech"] = value; }
    }

    
    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Tech = e.PrimaryKey;
        fvAg_WFP3_Technician.ChangeMode(e.ViewMode);
        BindAg_WFP3_Technician();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);


    #region Event Handling - Ag - WFP3 - Technicians

    protected void lbAg_WFP3_Technician_Close_Click(object sender, EventArgs e)
    {
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "Technician"));
        FormView fv = fvAg_WFP3_Technician;
        fv.DataSource = "";
        fv.DataBind();
        
    }

    protected void ddlAg_WFP3_Technician_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (!string.IsNullOrEmpty(ddl.SelectedValue))
        {
            DropDownList ddlTechnician = fvAg_WFP3_Technician.FindControl("ddlTechnician") as DropDownList;
            WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(ddlTechnician, new string[] { ddl.SelectedValue }, null, true);
        }
    }

    protected void fvAg_WFP3_Technician_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_Technician_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_modification", "msgInsert"))
                    fvAg_WFP3_Technician.ChangeMode(FormViewMode.Insert);
                else
                    fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                bind = true;
                break;
            case "InsertData":
                fvAg_WFP3_Technician_ItemInserting(sender, null);
                fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelInsert":
                fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_modification", "msgInsert"))
                {
                    fvAg_WFP3_Technician.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_Technician_ItemUpdating(sender, null);
                fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CancelUpdate":
                fvAg_WFP3_Technician.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_Technician_ItemDeleting(sender, null);
                lbAg_WFP3_Technician_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_Technician();
    }
    protected void fvAg_WFP3_Technician_ModeChanging(object sender, FormViewModeEventArgs e)
    {
    }

    protected void fvAg_WFP3_Technician_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        DropDownList ddlAg_WFP3_Technician_Type = fvAg_WFP3_Technician.FindControl("ddlAg_WFP3_Technician_Type") as DropDownList;
        DropDownList ddlTechnician = fvAg_WFP3_Technician.FindControl("ddlTechnician") as DropDownList;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_teches.Where(w => w.pk_form_wfp3_tech == PK_Wfp3Tech).Select(s => s).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlAg_WFP3_Technician_Type.SelectedValue) && !string.IsNullOrEmpty(ddlTechnician.SelectedValue))
                {
                    a.fk_designerEngineerType_code = ddlAg_WFP3_Technician_Type.SelectedValue;
                    a.fk_list_designerEngineer = Convert.ToInt32(ddlTechnician.SelectedValue);
                }
                else sb.Append("Type and Technician were not updated. Type and Technician are required. ");

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Technician_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        DropDownList ddlAg_WFP3_Technician_Type = fvAg_WFP3_Technician.FindControl("ddlAg_WFP3_Technician_Type") as DropDownList;
        DropDownList ddlTechnician = fvAg_WFP3_Technician.FindControl("ddlTechnician") as DropDownList;
        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sType = null;
                if (!string.IsNullOrEmpty(ddlAg_WFP3_Technician_Type.SelectedValue)) sType = ddlAg_WFP3_Technician_Type.SelectedValue;
                else sb.Append("Type is required. ");

                int? iTechnician = null;
                if (!string.IsNullOrEmpty(ddlTechnician.SelectedValue)) iTechnician = Convert.ToInt32(ddlTechnician.SelectedValue);
                else sb.Append("Technician is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_tech_add(FK_Wfp3, iTechnician, sType, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                    else PK_Wfp3Tech = Convert.ToInt32(i);
                }
                else WACAlert.Show(sb.ToString(), iCode);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Technician_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_tech", "msgDelete"))
        {
            
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_tech_delete(Convert.ToInt32(fvAg_WFP3_Technician.DataKey.Value), Session["userName"].ToString());
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    private void BindAg_WFP3_Technician()
    {
        FormView fv = fvAg_WFP3_Technician;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.form_wfp3_teches.Where(w => w.pk_form_wfp3_tech == PK_Wfp3Tech).Select(s => s);
            fv.DataKeyNames = new string[] { "pk_form_wfp3_tech" };
            fv.DataSource = a;
            fv.DataBind();

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineerType_DDL(fv.FindControl("ddlAg_WFP3_Technician_Type") as DropDownList, null, true);
                //WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fvAg_WFP3_Tech, "ddlTechnician", new string[] { a.Single().fk_designerEngineerType_code }, a.Single().fk_list_designerEngineer, true);
            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineerType_DDL(fv.FindControl("ddlAg_WFP3_Technician_Type") as DropDownList, a.Single().fk_designerEngineerType_code, true);
                WACGlobal_Methods.PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(fv, "ddlTechnician", new string[] { a.Single().fk_designerEngineerType_code }, a.Single().fk_list_designerEngineer, true);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Express_Organization : System.Web.UI.UserControl
{
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadFormView_Organization(int iPK_Organization)
    {
        try
        {
            fvOrganization.ChangeMode(FormViewMode.ReadOnly);
            if (iPK_Organization == -1) fvOrganization.ChangeMode(FormViewMode.Insert);
            BindOrganization(iPK_Organization);
            mpeExpress_Organization.Show();
            upExpress_Organization.Update();
        }
        catch { WACAlert.Show("The Organization is disassociated and the express Organization window is not available.", 0); }
    }

    #endregion

    #region Event Handling

    protected void lbExpress_Organization_Close_Click(object sender, EventArgs e)
    {
        hfOrganizationPK.Value = "";
        Page.GetType().InvokeMember("InvokedMethod_SectionPage_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
        mpeExpress_Organization.Hide();
    }

    protected void lbExpress_Organization_Add_Click(object sender, EventArgs e)
    {
        fvOrganization.ChangeMode(FormViewMode.Insert);
        BindOrganization(-1);
    }

    protected void fvOrganization_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        try
        {
            fvOrganization.ChangeMode(e.NewMode);
            if (!string.IsNullOrEmpty(hfOrganizationPK.Value)) BindOrganization(Convert.ToInt32(hfOrganizationPK.Value));
            else lbExpress_Organization_Close_Click(null, null);
        }
        catch { }
    }

    protected void fvOrganization_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbOrg = fvOrganization.FindControl("tbOrg") as TextBox;
        DropDownList ddlContact = fvOrganization.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("ddl") as DropDownList;
        HiddenField hfPropertyPK = fvOrganization.FindControl("UC_Property_EditInsert_Organization").FindControl("hfPropertyPK") as HiddenField;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.organizations.Where(w => w.pk_organization == Convert.ToInt32(fvOrganization.DataKey.Value)) select b).Single();

            a.org = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbOrg.Text, 96).Trim();

            if (ddlContact.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(ddlContact.SelectedValue)) a.fk_participant_contact = Convert.ToInt32(ddlContact.SelectedValue);
                else a.fk_participant_contact = null;
            }

            if (!string.IsNullOrEmpty(hfPropertyPK.Value)) a.fk_property = Convert.ToInt32(hfPropertyPK.Value);
            else a.fk_property = null;

            a.modified = DateTime.Now;
            a.modified_by = Session["userName"].ToString();

            try
            {
                wDataContext.SubmitChanges();
                fvOrganization.ChangeMode(FormViewMode.ReadOnly);
                BindOrganization(Convert.ToInt32(fvOrganization.DataKey.Value));
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvOrganization_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        TextBox tbOrg = fvOrganization.FindControl("tbOrg") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sOrg = null;
                if (!string.IsNullOrEmpty(tbOrg.Text)) sOrg = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbOrg.Text, 96).Trim();
                else sb.Append("Organization is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.organization_add(sOrg, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvOrganization.ChangeMode(FormViewMode.ReadOnly);
                        BindOrganization(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    #endregion

    #region Data Binding

    public void BindOrganization(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.organizations.Where(w => w.pk_organization == i).Select(s => s);
            fvOrganization.DataKeyNames = new string[] { "pk_organization" };
            fvOrganization.DataSource = a;
            fvOrganization.DataBind();

            if (fvOrganization.CurrentMode != FormViewMode.Insert)
            {
                hfOrganizationPK.Value = a.Single().pk_organization.ToString();
            }

            if (fvOrganization.CurrentMode == FormViewMode.Edit)
            {
                DropDownList ddl = fvOrganization.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("ddl") as DropDownList;
                Label lbl = fvOrganization.FindControl("UC_DropDownListByAlphabet_Organization").FindControl("lblLetter") as Label;
                string sLetter = null;
                try { sLetter = a.Single().participant.lname[0].ToString(); }
                catch { }
                WACGlobal_Methods.EventControl_Custom_DropDownListByAlphabet(ddl, lbl, sLetter, "PARTICIPANT", "ALL", a.Single().fk_participant_contact);

                WACGlobal_Methods.PopulateControl_Property_EditInsert_UserControl(fvOrganization.FindControl("UC_Property_EditInsert_Organization") as UserControl, a.Single().property);
            }
        }
    }

    #endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_ControlGroup_ParticipantCommunication : System.Web.UI.UserControl
{
    #region Initilization

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region Event Handling

    protected void lbParticipant_Communication_Close_Click(object sender, EventArgs e)
    {
        fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
        Participant_Communication_BindRecord(-1);

        object[] oArgs = new object[] { Convert.ToInt32(hfPK_Participant.Value) };
        Page.GetType().InvokeMember("InvokedMethod_ControlGroup_RebindRecord", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, oArgs);
    }

    protected void lbParticipant_Communication_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            fvParticipant_Communication.ChangeMode(FormViewMode.Insert);
            Participant_Communication_BindRecord(-1);
        }
    }

    protected void lbView_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
        Participant_Communication_BindRecord(Convert.ToInt32(lb.CommandArgument));
    }

    protected void ddlParticipant_Communication_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlType = (DropDownList)sender;
        DropDownList ddlUsage = fvParticipant_Communication.FindControl("ddlUsage") as DropDownList;
        ddlUsage.Items.Clear();
        if (!string.IsNullOrEmpty(ddlType.SelectedValue))
        {
            WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationUsage_DDL(fvParticipant_Communication, "ddlUsage", null, ddlType.SelectedValue);
        }
    }

    protected void fvParticipant_Communication_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvParticipant_Communication.ChangeMode(e.NewMode);
            Participant_Communication_BindRecord(Convert.ToInt32(fvParticipant_Communication.DataKey.Value));
        }
    }

    protected void fvParticipant_Communication_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlType = fvParticipant_Communication.FindControl("ddlType") as DropDownList;
        DropDownList ddlUsage = fvParticipant_Communication.FindControl("ddlUsage") as DropDownList;
        TextBox tbAreaCode = fvParticipant_Communication.FindControl("tbAreaCode") as TextBox;
        TextBox tbPhoneNumber = fvParticipant_Communication.FindControl("tbPhoneNumber") as TextBox;
        //DropDownList ddlPhone = fvParticipant_Communication.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlNumber") as DropDownList;
        TextBox tbExtension = fvParticipant_Communication.FindControl("tbExtension") as TextBox;
        TextBox tbNote = fvParticipant_Communication.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? i = null;
                int iCode = 0;
                iCode = wDataContext.communication_add(tbAreaCode.Text, tbPhoneNumber.Text, Session["userName"].ToString(), ref i);
                if (iCode != 0) i = null;

                var a = wDataContext.participantCommunications.Where(w => w.pk_participantCommunication == Convert.ToInt32(fvParticipant_Communication.DataKey.Value)).Select(s => s).Single();

                if (!string.IsNullOrEmpty(ddlType.SelectedValue)) a.fk_communicationType_code = ddlType.SelectedValue;
                else sb.Append("Type was not updated. Type is required. ");

                if (!string.IsNullOrEmpty(ddlUsage.SelectedValue)) a.fk_communicationUsage_code = ddlUsage.SelectedValue;
                else sb.Append("Usage was not updated. Usage is required. ");

                if (i != null) a.fk_communication = Convert.ToInt32(i);
                else sb.Append("Phone Number was not updated. Bad Phone Number. ");

                //if (!string.IsNullOrEmpty(ddlPhone.SelectedValue)) a.fk_communication = Convert.ToInt32(ddlPhone.SelectedValue);
                //else sb.Append("Phone Number was not updated. Phone Number is required. ");

                a.extension = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbExtension.Text, 8).Trim();

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 48).Trim();

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
                fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
                Participant_Communication_BindRecord(Convert.ToInt32(fvParticipant_Communication.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvParticipant_Communication_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        DropDownList ddlType = fvParticipant_Communication.FindControl("ddlType") as DropDownList;
        DropDownList ddlUsage = fvParticipant_Communication.FindControl("ddlUsage") as DropDownList;
        //DropDownList ddlPhone = fvParticipant_Communication.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlNumber") as DropDownList;
        TextBox tbAreaCode = fvParticipant_Communication.FindControl("tbAreaCode") as TextBox;
        TextBox tbPhoneNumber = fvParticipant_Communication.FindControl("tbPhoneNumber") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sType = null;
                if (!string.IsNullOrEmpty(ddlType.SelectedValue)) sType = ddlType.SelectedValue;
                else sb.Append("Type is required. ");

                string sUsage = null;
                if (!string.IsNullOrEmpty(ddlUsage.SelectedValue)) sUsage = ddlUsage.SelectedValue;
                else sb.Append("Usage is required. ");

                //int? iPhoneNumber = null;
                //if (!string.IsNullOrEmpty(ddlPhone.SelectedValue)) iPhoneNumber = Convert.ToInt32(ddlPhone.SelectedValue);
                //else sb.Append("Phone Number is required. ");

                string sAreaCode = null;
                if (!string.IsNullOrEmpty(tbAreaCode.Text)) sAreaCode = tbAreaCode.Text;
                else sb.Append("Area Code is required. ");

                string sPhoneNumber = null;
                if (!string.IsNullOrEmpty(tbPhoneNumber.Text)) sPhoneNumber = tbPhoneNumber.Text;
                else sb.Append("Phone Number is required. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.participantCommunication_add_express(Convert.ToInt32(hfPK_Participant.Value), sAreaCode, sPhoneNumber, sType, sUsage, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvParticipant_Communication.ChangeMode(FormViewMode.ReadOnly);
                        Participant_Communication_BindRecord(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvParticipant_Communication_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.participantCommunication_delete(Convert.ToInt32(fvParticipant_Communication.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbParticipant_Communication_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Data Binding

    private void Participant_Communication_BindRecord(int i)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.participantCommunications.Where(w => w.pk_participantCommunication == i).Select(s => s);
            fvParticipant_Communication.DataKeyNames = new string[] { "pk_participantCommunication" };
            fvParticipant_Communication.DataSource = a;
            fvParticipant_Communication.DataBind();

            if (fvParticipant_Communication.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationType_DDL(fvParticipant_Communication, "ddlType", null);
                //WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvParticipant_Communication.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlAreaCode") as DropDownList, null);
            }

            if (fvParticipant_Communication.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationType_DDL(fvParticipant_Communication, "ddlType", a.Single().fk_communicationType_code);
                WACGlobal_Methods.PopulateControl_DatabaseLists_CommunicationUsage_DDL(fvParticipant_Communication, "ddlUsage", a.Single().fk_communicationUsage_code, a.Single().fk_communicationType_code);

                //if (a.Single().communication != null)
                //{
                //    WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvParticipant_Communication.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlAreaCode") as DropDownList, a.Single().communication.areacode);
                //    WACGlobal_Methods.PopulateControl_Communication_PhoneNumbersByAreaCode_DDL(fvParticipant_Communication.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlNumber") as DropDownList, a.Single().fk_communication, a.Single().communication.areacode);
                //}
                //else WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(fvParticipant_Communication.FindControl("UC_Communication_EditInsert_Phone").FindControl("ddlAreaCode") as DropDownList, null);
            }
        }
    }

    #endregion

    //#region GridView

    //protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gv.EditIndex = e.NewEditIndex;
    //    gv.
    //}

    //#endregion
}
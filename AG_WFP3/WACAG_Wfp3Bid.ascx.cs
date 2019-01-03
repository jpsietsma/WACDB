using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WAC_CustomControls;
public partial class AG_WACAG_Wfp3Bid : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int FK_FarmBusiness
    {
        get { return Convert.ToInt32(Session["FK_FarmBusiness"]) == 0 ? -1 : Convert.ToInt32(Session["FK_FarmBusiness"]); }
        set { Session["FK_FarmBusiness"] = value; }
    }
    public int FK_Wfp3
    {
        get { return Convert.ToInt32(Session["FK_Wfp3"]) == 0 ? -1 : Convert.ToInt32(Session["FK_Wfp3"]); }
        set { Session["FK_Wfp3"] = value; }
    }
    public int PK_Wfp3Bid
    {
        get { return Convert.ToInt32(ViewState["PK_Wfp3Bid"]) == 0 ? -1 : Convert.ToInt32(ViewState["PK_Wfp3Bid"]); }
        set { ViewState["PK_Wfp3Bid"] = value; }
    }

    public void OpenFormView(object sender, FormViewEventArgs e)
    {
        FK_Wfp3 = e.ForeignKey;
        PK_Wfp3Bid = e.PrimaryKey;
        fvAg_WFP3_Bid.ChangeMode(e.ViewMode);
        BindAg_WFP3_Bid();
    }
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);

    #region Event Handling - Ag - WFP3 - Bid

    protected void lbAg_WFP3_Bid_Close_Click(object sender, EventArgs e)
    {
        OnFormActionCompleted(this, new FormViewEventArgs(FK_Wfp3, "Bid"));
        FormView fv = fvAg_WFP3_Bid;
        fv.DataSource = "";
        fv.DataBind();
        
    }

    protected void fvAg_WFP3_Bid_ItemCommand(object sender, EventArgs e)
    {
        FormViewCommandEventArgs f = e as FormViewCommandEventArgs;
        bool bind = true;
        switch (f.CommandName)
        {
            case "ViewMode":
                fvAg_WFP3_Bid.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "CloseForm":
                fvAg_WFP3_Bid.ChangeMode(FormViewMode.ReadOnly);
                lbAg_WFP3_Bid_Close_Click(sender, null);
                break;
            case "InsertMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3_bid", "msgInsert"))
                    fvAg_WFP3_Bid.ChangeMode(FormViewMode.Insert);
                else
                    fvAg_WFP3_Bid.ChangeMode(FormViewMode.ReadOnly);
                bind = false;
                break;
            case "InsertData":
                fvAg_WFP3_Bid_ItemInserting(sender, null);
                lbAg_WFP3_Bid_Close_Click(sender, null);
                break;
            case "CancelInsert":
                fvAg_WFP3_Bid.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateMode":
                if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "A", "form_wfp3_bid", "msgInsert"))
                {
                    fvAg_WFP3_Bid.ChangeMode(FormViewMode.Edit);
                    bind = true;
                }
                else
                    fvAg_WFP3_Bid.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "UpdateData":
                fvAg_WFP3_Bid_ItemUpdating(sender, null);
                lbAg_WFP3_Bid_Close_Click(sender, null);
                break;
            case "CancelUpdate":
                fvAg_WFP3_Bid.ChangeMode(FormViewMode.ReadOnly);
                break;
            case "DeleteData":
                fvAg_WFP3_Bid_ItemDeleting(sender, null);
                lbAg_WFP3_Bid_Close_Click(sender, null);
                break;
            default:
                bind = false;
                break;
        }
        if (bind) BindAg_WFP3_Bid();
    }

    protected void fvAg_WFP3_Bid_ModeChanging(object sender, FormViewModeEventArgs e)
    {
       
    }

    protected void fvAg_WFP3_Bid_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        StringBuilder sbErrorCollection = new StringBuilder();

        FormView fv = fvAg_WFP3_Bid;
        DropDownList ddlContractor = fv.FindControl("ddlContractor") as DropDownList;
        TextBox tbBidAmount = fv.FindControl("tbBidAmount") as TextBox;
        CustomControls_AjaxCalendar tbCalBidAwardDate = fv.FindControl("tbCalBidAwardDate") as CustomControls_AjaxCalendar;
        TextBox tbModificationAmount = fv.FindControl("tbModificationAmount") as TextBox;
        TextBox tbBidRank = fv.FindControl("tbBidRank") as TextBox;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = (from b in wDataContext.form_wfp3_bids.Where(w => w.pk_form_wfp3_bid == Convert.ToInt32(fv.DataKey.Value))
                     select b).Single();
            try
            {
                if (!string.IsNullOrEmpty(ddlContractor.SelectedValue)) a.fk_participant_contractor = Convert.ToInt32(ddlContractor.SelectedValue);
                else sbErrorCollection.Append("Contractor was not updated. This is a required field. ");

                try { if (!string.IsNullOrEmpty(tbBidAmount.Text)) a.bid_amt = Convert.ToDecimal(tbBidAmount.Text); }
                catch { sbErrorCollection.Append("Bid Amount was not updated. Must be a number (Decimal). "); }

                a.bid_awarded = tbCalBidAwardDate.CalDateNullable;

                a.modification_amt = 0;
                try { if (!string.IsNullOrEmpty(tbModificationAmount.Text)) a.modification_amt = Convert.ToDecimal(tbModificationAmount.Text); }
                catch { sbErrorCollection.Append("Modification Amount was not updated. Must be a number (Decimal). "); }

                try { if (!string.IsNullOrEmpty(tbBidRank.Text)) a.bid_rank = Convert.ToByte(tbBidRank.Text); }
                catch { sbErrorCollection.Append("Bid Rank was not updated. Must be a Number (Byte). "); }

                a.note = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                a.modified = DateTime.Now;
                a.modified_by = Session["userName"].ToString();

                wDataContext.SubmitChanges();
             
                if (!string.IsNullOrEmpty(sbErrorCollection.ToString())) WACAlert.Show(sbErrorCollection.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Bid_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;
        FormView fv = fvAg_WFP3_Bid;

        DropDownList ddlContractor = fv.FindControl("ddlContractor") as DropDownList;
        TextBox tbBidAmount = fv.FindControl("tbBidAmount") as TextBox;
        CustomControls_AjaxCalendar tbCalBidAwardDate = fv.FindControl("tbCalBidAwardDate") as CustomControls_AjaxCalendar;
        TextBox tbModificationAmount = fv.FindControl("tbModificationAmount") as TextBox;
        TextBox tbBidRank = fv.FindControl("tbBidRank") as TextBox;
        TextBox tbNote = fv.FindControl("tbNote") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int? iContractorFK = null;
                if (!string.IsNullOrEmpty(ddlContractor.SelectedValue)) iContractorFK = Convert.ToInt32(ddlContractor.SelectedValue);
                else sb.Append("Contractor is required. ");

                decimal? dBidAmount = null;
                try { if (!string.IsNullOrEmpty(tbBidAmount.Text)) dBidAmount = Convert.ToDecimal(tbBidAmount.Text); }
                catch { }
                if (dBidAmount == null || dBidAmount == 0) sb.Append("Bid Amount is required and must be greater than 0. ");

                decimal? dModificationAmount = null;
                try { if (!string.IsNullOrEmpty(tbModificationAmount.Text)) dModificationAmount = Convert.ToDecimal(tbModificationAmount.Text); }
                catch { }

                string sNote = WACGlobal_Methods.Format_Global_StringLengthRestriction(tbNote.Text, 255);

                byte? byBidRank = null;
                try { if (!string.IsNullOrEmpty(tbBidRank.Text)) byBidRank = Convert.ToByte(tbBidRank.Text); }
                catch { }

                DateTime? dtBidAwarded = tbCalBidAwardDate.CalDateNullable;

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.form_wfp3_bid_add(FK_Wfp3, iContractorFK, dBidAmount, dModificationAmount, sNote, byBidRank, dtBidAwarded, Session["userName"].ToString(), ref i);
                    if (iCode != 0) WACAlert.Show("Error Returned from Database.", iCode);
                    else PK_Wfp3Bid = Convert.ToInt32(i);
                }
                else WACAlert.Show(sb.ToString(), iCode);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvAg_WFP3_Bid_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "form_wfp3_bid", "msgDelete"))
        {
            FormView fv = fvAg_WFP3_Bid;
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.form_wfp3_bid_delete(Convert.ToInt32(fv.DataKey.Value), Session["userName"].ToString());
                    if (iCode != 0)  WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
            }
        }
    }

    #endregion
    private void BindAg_WFP3_Bid()
    {
        FormView fv = fvAg_WFP3_Bid;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.form_wfp3_bids.Where(w => w.pk_form_wfp3_bid == PK_Wfp3Bid) select b;
            fv.DataKeyNames = new string[] { "pk_form_wfp3_bid" };
            fv.DataSource = a;
            fv.DataBind();

            string sRegionWAC = WACGlobal_Methods.SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(FK_FarmBusiness);

            if (fv.CurrentMode == FormViewMode.Insert)
            {
                WACGlobal_Methods.PopulateControl_Participant_DBView_DDL(fv.FindControl("ddlContractor") as DropDownList, null, new string[] { "C" }, 
                    null, null, new string[] { sRegionWAC }, null, null, null, false, false, false, false, false, false, 
                    WACGlobal_Methods.Enum_Participant_Forms.ALL, true);

            }

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                WACGlobal_Methods.PopulateControl_Participant_DBView_DDL(fv.FindControl("ddlContractor") as DropDownList, a.Single().fk_participant_contractor, 
                    new string[] { "C" }, null, null, new string[] { sRegionWAC }, null, null, null, false, false, false, false, false, false, 
                    WACGlobal_Methods.Enum_Participant_Forms.ALL, true);
            }
        }
    }
}
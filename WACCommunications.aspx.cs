using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACCommunications : System.Web.UI.Page
{
    #region Page Load Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["searchTypeCommunications"] = "";
            Session["searchKeyCommunications"] = "";
            Session["resultsCommunications"] = "";
            Session["orderCommunications"] = "";
            Session["countCommunications"] = "";

            PopulateDLL4PhoneNumberAreaCode();

            hlCommunication_Help.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["DocsLink"] + "Help/FAME Global Data Communication.pdf";
            hlCommunication_Help.ImageUrl = "~/images/help_24.png";

            string sQS_PK = Request.QueryString["pk"];
            if (!string.IsNullOrEmpty(sQS_PK)) BindCommunication_PhoneNumber(Convert.ToInt32(sQS_PK));

            imgAdvisory1.ToolTip = WACGlobal_Methods.Specialtext_Global_Advisory(1);
        }
    }

    private void PopulateDLL4PhoneNumberAreaCode()
    {
        WACGlobal_Methods.PopulateControl_Communication_AreaCodes_DDL(ddlCommunication_Search_PhoneNumber_AreaCode, null);
    }

    #endregion

    #region Invoked Methods

    public void InvokedMethod_Insert_Global()
    {
        try { UC_Global_Insert1.ShowGlobal_Insert(); }
        catch { WACAlert.Show("Could not open Global Insert Express Window.", 0); }
    }

    public void Participant_ViewEditInsertWindow(object oPK_Participant)
    {
        try { UC_Express_Participant1.LoadFormView_Participant(Convert.ToInt32(oPK_Participant)); }
        catch { WACAlert.Show("Could not open Participant Window.", 0); }
    }

    public void InvokedMethod_SectionPage_RebindRecord()
    {
        BindCommunication_PhoneNumber(Convert.ToInt32(fvCommunication_PhoneNumber.DataKey.Value));
        upCommunication.Update();
        upCommunicationSearch.Update();
    }

    #endregion

    #region Event Handling - Search - Communication - Phone Number

    public void HandleQueryType()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (Session["searchTypeCommunications"].ToString())
            {
                case "A":
                    var aA = wDataContext.communications.OrderBy(o => o.areacode).ThenBy(o => o.number).Select(s => new { s.pk_communication, s.areacode, s.number });
                    Session["countCommunications"] = aA.Count();
                    Session["resultsCommunications"] = aA;
                    if (!string.IsNullOrEmpty(Session["orderCommunications"].ToString())) Session["resultsCommunications"] = aA.OrderBy(Session["orderCommunications"].ToString(), null);
                    break;
                case "B":
                    var aB = wDataContext.communications.Where(w => w.number.StartsWith(Session["searchKeyCommunications"].ToString())).OrderBy(o => o.areacode).ThenBy(o => o.number).Select(s => new { s.pk_communication, s.areacode, s.number });
                    Session["countCommunications"] = aB.Count();
                    Session["resultsCommunications"] = aB;
                    if (!string.IsNullOrEmpty(Session["orderCommunications"].ToString())) Session["resultsCommunications"] = aB.OrderBy(Session["orderCommunications"].ToString(), null);
                    break;
                case "C":
                    var aC = wDataContext.communications.Where(w => w.areacode == Session["searchKeyCommunications"].ToString()).OrderBy(o => o.areacode).ThenBy(o => o.number).Select(s => new { s.pk_communication, s.areacode, s.number });
                    Session["countCommunications"] = aC.Count();
                    Session["resultsCommunications"] = aC;
                    if (!string.IsNullOrEmpty(Session["orderCommunications"].ToString())) Session["resultsCommunications"] = aC.OrderBy(Session["orderCommunications"].ToString(), null);
                    break;
                default:
                    Session["countCommunications"] = 0;
                    Session["resultsCommunications"] = "";
                    Session["orderCommunications"] = "";
                    break;
            }
            BindCommunication_PhoneNumbers();
        }
    }

    public void ChangeIndex2Zero4SearchDDLs()
    {
        gvCommunication_PhoneNumbers.SelectedIndex = -1;
        ViewState["SelectedValueCommunications"] = null;
        ClearCommunication();
        try { ddlCommunication_Search_PhoneNumber_AreaCode.SelectedIndex = 0; }
        catch { }
    }

    protected void lbCommunication_Search_ReloadReset_Click(object sender, EventArgs e)
    {
        ChangeIndex2Zero4SearchDDLs();
        ClearCommunications();
    }

    protected void lbCommunication_Search_PhoneNumber_All_Click(object sender, EventArgs e)
    {
        Session["orderCommunications"] = "";
        Session["searchTypeCommunications"] = "A";
        Session["searchKeyCommunications"] = "";
        ChangeIndex2Zero4SearchDDLs();
        HandleQueryType();
    }

    protected void btnCommunication_Search_PhoneNumber_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbCommunication_Search_PhoneNumber.Text))
        {
            Session["orderCommunications"] = "";
            Session["searchTypeCommunications"] = "B";
            Session["searchKeyCommunications"] = tbCommunication_Search_PhoneNumber.Text;
            ChangeIndex2Zero4SearchDDLs();
            tbCommunication_Search_PhoneNumber.Text = Session["searchKeyCommunications"].ToString();
            HandleQueryType();
        }
        else WACAlert.Show("Please enter a full or partial phone number.", 0);
    }

    protected void ddlCommunication_Search_PhoneNumber_AreaCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlCommunication_Search_PhoneNumber_AreaCode.SelectedValue))
        {
            Session["orderCommunications"] = "";
            Session["searchTypeCommunications"] = "C";
            Session["searchKeyCommunications"] = ddlCommunication_Search_PhoneNumber_AreaCode.SelectedValue;
            ChangeIndex2Zero4SearchDDLs();
            ddlCommunication_Search_PhoneNumber_AreaCode.SelectedValue = Session["searchKeyCommunications"].ToString();
            HandleQueryType();
        }
    }

    #endregion

    #region Event Handling - Results - Communication - Phone Number

    protected void gvCommunication_PhoneNumbers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCommunication_PhoneNumbers.PageIndex = e.NewPageIndex;
        HandleQueryType();
    }

    protected void gvCommunication_PhoneNumbers_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderCommunications"] = e.SortExpression;
        HandleQueryType();
    }

    protected void gvCommunication_PhoneNumbers_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCommunication_PhoneNumbers.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFAA");
        fvCommunication_PhoneNumber.ChangeMode(FormViewMode.ReadOnly);
        BindCommunication_PhoneNumber(Convert.ToInt32(gvCommunication_PhoneNumbers.SelectedDataKey.Value));
        if (gvCommunication_PhoneNumbers.SelectedIndex != -1) ViewState["SelectedValueCommunications"] = gvCommunication_PhoneNumbers.SelectedValue.ToString();
    }

    #endregion

    #region Event Handling - Communication - Phone Number

    protected void lbCommunication_PhoneNumber_Close_Click(object sender, EventArgs e)
    {
        ClearCommunication();
        gvCommunication_PhoneNumbers.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFDDAA");
        HandleQueryType();
        upCommunication.Update();
        upCommunicationSearch.Update();
    }

    protected void lbCommunication_PhoneNumber_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "GlobalData", "GlobalData", "msgInsert"))
        {
            ChangeIndex2Zero4SearchDDLs();
            ClearCommunications();
            fvCommunication_PhoneNumber.ChangeMode(FormViewMode.Insert);
            Session["searchTypeCommunications"] = "";
            BindCommunication_PhoneNumber(-1);
        }
    }

    protected void fvCommunication_PhoneNumber_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        bool bChangeMode = true;
        if (e.NewMode == FormViewMode.Edit) bChangeMode = WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "U", "GlobalData", "GlobalData", "msgUpdate");
        if (bChangeMode)
        {
            fvCommunication_PhoneNumber.ChangeMode(e.NewMode);
            BindCommunication_PhoneNumber(Convert.ToInt32(fvCommunication_PhoneNumber.DataKey.Value));
        }
    }

    protected void fvCommunication_PhoneNumber_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbAreaCode = fvCommunication_PhoneNumber.FindControl("tbAreaCode") as TextBox;
        TextBox tbPhoneNumber = fvCommunication_PhoneNumber.FindControl("tbPhoneNumber") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.communications.Where(w => w.pk_communication == Convert.ToInt32(fvCommunication_PhoneNumber.DataKey.Value)).Select(s => s).Single();

            if (!string.IsNullOrEmpty(tbAreaCode.Text)) a.areacode = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbAreaCode.Text, WACGlobal_Methods.Enum_Number_Type.AREACODE);
            else sb.Append("Area Code was not updated. Area Code is required. ");

            if (!string.IsNullOrEmpty(tbPhoneNumber.Text)) a.number = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbPhoneNumber.Text, WACGlobal_Methods.Enum_Number_Type.PHONENUMBER);
            else sb.Append("Phone Number was not updated. Phone Number is required. ");

            a.modified = DateTime.Now;
            a.modified_by = Session["userName"].ToString();

            try
            {
                wDataContext.SubmitChanges();
                fvCommunication_PhoneNumber.ChangeMode(FormViewMode.ReadOnly);
                BindCommunication_PhoneNumber(Convert.ToInt32(fvCommunication_PhoneNumber.DataKey.Value));

                if (!string.IsNullOrEmpty(sb.ToString())) WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        }
    }

    protected void fvCommunication_PhoneNumber_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int? i = null;
        int iCode = 0;

        TextBox tbAreaCode = fvCommunication_PhoneNumber.FindControl("tbAreaCode") as TextBox;
        TextBox tbPhoneNumber = fvCommunication_PhoneNumber.FindControl("tbPhoneNumber") as TextBox;

        StringBuilder sb = new StringBuilder();

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sAreaCode = null;
                if (!string.IsNullOrEmpty(tbAreaCode.Text)) sAreaCode = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbAreaCode.Text, WACGlobal_Methods.Enum_Number_Type.AREACODE);
                else sb.Append("Area Code is required. ");
                if (string.IsNullOrEmpty(sAreaCode)) sb.Append("Area Code not in correct format. ");

                string sPhoneNumber = null;
                if (!string.IsNullOrEmpty(tbPhoneNumber.Text)) sPhoneNumber = WACGlobal_Methods.Format_Global_PhoneNumber_StripToNumbers(tbPhoneNumber.Text, WACGlobal_Methods.Enum_Number_Type.PHONENUMBER);
                else sb.Append("Phone Number is required. ");
                if (string.IsNullOrEmpty(sPhoneNumber)) sb.Append("Phone Number not in correct format. ");

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    iCode = wDataContext.communication_add(sAreaCode, sPhoneNumber, Session["userName"].ToString(), ref i);
                    if (iCode == 0)
                    {
                        fvCommunication_PhoneNumber.ChangeMode(FormViewMode.ReadOnly);
                        BindCommunication_PhoneNumber(Convert.ToInt32(i));
                    }
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                else WACAlert.Show(sb.ToString(), 0);
            }
            catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
        }
    }

    protected void fvCommunication_PhoneNumber_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "GlobalData", "GlobalData", "msgDelete"))
        {
            int iCode = 0;
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    iCode = wDataContext.communication_delete(Convert.ToInt32(fvCommunication_PhoneNumber.DataKey.Value), Session["userName"].ToString());
                    if (iCode == 0) lbCommunication_PhoneNumber_Close_Click(null, null);
                    else WACAlert.Show("Error Returned from Database.", iCode);
                }
                catch (Exception ex) { WACAlert.Show("Error: " + ex.Message, 0); }
            }
        }
    }

    #endregion

    #region Data Binding - Communication - Phone Number

    private void BindCommunication_PhoneNumbers()
    {
        try
        {
            gvCommunication_PhoneNumbers.DataKeyNames = new string[] { "pk_communication" };
            gvCommunication_PhoneNumbers.DataSource = Session["resultsCommunications"];
            gvCommunication_PhoneNumbers.DataBind();
        }
        catch { }
        if (ViewState["SelectedValueCommunications"] != null)
        {
            string sSelectedValue = (string)ViewState["SelectedValueCommunications"];
            foreach (GridViewRow gvr in gvCommunication_PhoneNumbers.Rows)
            {
                string sKeyValue = gvCommunication_PhoneNumbers.DataKeys[gvr.RowIndex].Value.ToString();
                if (sKeyValue == sSelectedValue)
                {
                    gvCommunication_PhoneNumbers.SelectedIndex = gvr.RowIndex;
                    return;
                }
                else gvCommunication_PhoneNumbers.SelectedIndex = -1;
            }
        }
        try
        {
            if (Convert.ToInt32(Session["countCommunications"]) > 0) lblCount.Text = "Records: " + Session["countCommunications"];
            else lblCount.Text = "Records: 0";
        }
        catch { lblCount.Text = ""; }
    }

    private void BindCommunication_PhoneNumber(int i)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.communications.Where(w => w.pk_communication == i).Select(s => s);
            fvCommunication_PhoneNumber.DataKeyNames = new string[] { "pk_communication" };
            fvCommunication_PhoneNumber.DataSource = a;
            fvCommunication_PhoneNumber.DataBind();
        }
    }

    private void ClearCommunications()
    {
        lblCount.Text = "";
        gvCommunication_PhoneNumbers.DataSource = null;
        gvCommunication_PhoneNumbers.DataBind();
    }

    private void ClearCommunication()
    {
        fvCommunication_PhoneNumber.ChangeMode(FormViewMode.ReadOnly);
        fvCommunication_PhoneNumber.DataSource = "";
        fvCommunication_PhoneNumber.DataBind();
    }

    #endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACDocumentUpload : System.Web.UI.Page
{
    private int iPK_Level_01;
    private int iPK_Level_02;
    private string StrArea;
    private string StrAreaSector;

    protected void Page_Load(object sender, EventArgs e)
    {
        iPK_Level_01 = Convert.ToInt32(Request.QueryString["pk1"]);
        iPK_Level_02 = Convert.ToInt32(Request.QueryString["pk2"]);
        StrArea = Request.QueryString["area"];
        StrAreaSector = Request.QueryString["areaSector"];

        if (!Page.IsPostBack)
        {
            
            //WACAlert.Show(iPK_Level_01.ToString() + "," + iPK_Level_02.ToString() + "," + StrArea + "," + StrAreaSector, 0);

            cal.SelectedDate = WACGlobal_Methods.SpecialDataType_DateTime_Today();
            cal.VisibleDate = WACGlobal_Methods.SpecialDataType_DateTime_Today();

            WACGlobal_Methods.PopulateControl_DatabaseLists_FormsWAC_DDL(ddlWACForm, "", StrArea);

            SetupUploader();
        }
    }

    private string GetTitleString()
    {
        switch (StrAreaSector)
        {
            case "A_ASR": return "Document Upload for Agriculture >> ASR";
            case "A_NMP": return "Document Upload for Agriculture >> NMP";
            case "A_OVER": return "Document Upload for Agriculture >> Overview";
            case "A_WFP2": return "Document Upload for Agriculture >> WFP2";
            case "A_WFP3": return "Document Upload for Agriculture >> WFP3";
            case "A_WFP3_BMP": return "Document Upload for Agriculture >> WFP3 BMP";
            case "F_BMP": return "Document Upload for Forestry >> BMP";
            case "F_FMP": return "Document Upload for Forestry >> FMP";
            case "F_MAP": return "Document Upload for Forestry >> MAP";
            case "M_OVEREVENT": return "Document Upload for Marketing >> Event";
            case "M_OVERPC": return "Document Upload for Marketing >> Pure Catskills";
            default: return "Document Upload";
        }
    }

    private void SetupUploader()
    {
        lblTitle.Text = GetTitleString();
        lblMessage.Text = "";
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (StrArea)
            {
                case "A":
                    //iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetFarmBusinessPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                    switch (StrAreaSector)
                    {
                        case "A_WFP3_BMP":
                            //iPK_Level_02 = (int)Page.GetType().InvokeMember("Delegate_GetFormWFP3PK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            ddlSpecialLevel3.DataSource = wDataContext.form_wfp3_bmps.Where(w => w.fk_form_wfp3 == iPK_Level_02).Select(s => new { s.fk_bmp_ag, s.bmp_ag.bmp_nbr });
                            ddlSpecialLevel3.DataTextField = "bmp_nbr";
                            ddlSpecialLevel3.DataValueField = "fk_bmp_ag";
                            ddlSpecialLevel3.DataBind();
                            ddlSpecialLevel3.Visible = true;
                            lblSpecialLevel3.Text = "BMP:";
                            lblSpecialLevel3.Visible = true;
                            break;
                    }
                    break;
            }
            ddlAreaSectorFolder.DataSource = wDataContext.list_participantTypeSectorFolders.Where(w => w.fk_participantTypeSector_code == StrAreaSector).OrderBy(o => o.folder).Select(s => new { s.pk_participantTypeSectorFolder_code, s.folder });
            ddlAreaSectorFolder.DataTextField = "folder";
            ddlAreaSectorFolder.DataValueField = "pk_participantTypeSectorFolder_code";
            ddlAreaSectorFolder.DataBind();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string sRoot = System.Configuration.ConfigurationManager.AppSettings["DocumentsRoot"];
        StringBuilder sb = new StringBuilder();
        if (fuFile.HasFile)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                string s = null;
                int? i = null;
                int iCode = 0;
                try
                {
                    int? iPK_Level_03 = null;
                    if (ddlSpecialLevel3.Visible == true) iPK_Level_03 = Convert.ToInt32(ddlSpecialLevel3.SelectedValue);

                    string sWACForm = null;
                    if (!string.IsNullOrEmpty(ddlWACForm.SelectedValue)) sWACForm = ddlWACForm.SelectedValue;

                    iCode = wDataContext.documentArchive_add(fuFile.FileName, ddlAreaSectorFolder.SelectedValue, iPK_Level_01, iPK_Level_02, iPK_Level_03, sWACForm, cal.SelectedDate, Session["userName"].ToString(), ref s, ref i);

                    if (iCode == 0)
                    {
                        fuFile.SaveAs(sRoot + StrArea + @"\" + s);

                        sb.Append("File Successfully Uploaded");
                        sb.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;File Name: " + fuFile.FileName + " >> " + s);
                        sb.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Content Type: " + fuFile.PostedFile.ContentType);
                        sb.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Content Length: " + fuFile.PostedFile.ContentLength);
                        lblMessage.Text = sb.ToString();

                        btnCloseWindow.Visible = true;
                    }
                    else WACAlert.Show("Error returned from database.", iCode);

                }
                catch (Exception ex)
                {
                    try
                    {
                        int iCode2 = 0;
                        iCode2 = wDataContext.documentArchive_delete(i, Session["userName"].ToString());
                        if (iCode2 != 0) WACAlert.Show("Error returned from database.", iCode2);
                        lblMessage.Text = "Error Uploading File: " + ex.Message;
                    }
                    catch (Exception ex2) { lblMessage.Text = "Error Uploading File: " + ex2.Message; }
                }
            }
        }
        else lblMessage.Text = "You must select a file to upload.";
    }
}
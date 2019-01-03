using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAC_DataObjects;

public partial class UC_DocumentArchive : System.Web.UI.UserControl
{
    public string StrArea = "A";
    public string StrAreaSector = "A_OVER";

    private int iPK_Level_01 = 0;
    private int? iPK_Level_02 = 0;

    public void SetupViewer()
    {
        pnl.Visible = false;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (StrArea)
            {
                case "A":
                    iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetFarmBusinessPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                    switch (StrAreaSector)
                    {
                        case "A_ASR":
                            iPK_Level_02 = (int)Page.GetType().InvokeMember("Delegate_GetASRPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, iPK_Level_02, "A_ASR");
                            break;
                        case "A_BMP":
                            iPK_Level_02 = (int)Page.GetType().InvokeMember("Delegate_GetBMPPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, iPK_Level_02, "A_BMP");
                            break;
                        case "A_NMP":
                            SetupViewer_Generic(iPK_Level_01, null, "A_NMP");
                            break;
                        case "A_OVER":
                            SetupViewer_Generic(iPK_Level_01, null, "A_OVER");
                            break;
                        case "A_TIER1":
                            SetupViewer_Generic(iPK_Level_01, null, "A_TIER1");
                            break;
                        case "A_WFP2":
                            iPK_Level_02 = (int)Page.GetType().InvokeMember("Delegate_GetFormWFP2PK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, iPK_Level_02, "A_WFP2");
                            break;
                        case "A_WFP3":
                            iPK_Level_02 = (int)Page.GetType().InvokeMember("Delegate_GetFormWFP3PK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, iPK_Level_02, "A_WFP3");
                            break;
                        case "A_WFP3_BMP":
                            iPK_Level_02 = (int)Page.GetType().InvokeMember("Delegate_GetFormWFP3PK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Ag_WFP3_BMP(iPK_Level_01, iPK_Level_02);
                            break;
                    }
                    break;
                case "E":
                    iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetEasementPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                    SetupViewer_Generic(iPK_Level_01, null, "E_OVER");
                    break;
                case "F":
                    switch (StrAreaSector)
                    {
                        case "F_BMP":
                            iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetForestry_BMP_PK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, null, "F_BMP");
                            break;
                        case "F_FMP":
                            iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetForestry_FMP_PK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, null, "F_FMP");
                            break;
                        case "F_MAP":
                            iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetForestry_MAP_PK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, null, "F_MAP");
                            break;
                    }
                    break;
                case "H":
                    SetupViewer_Home();
                    break;
                case "HR":
                    iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetHRWACEmployeesPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                    SetupViewer_Generic(iPK_Level_01, null, "HR_OVER");
                    break;
                case "M":
                    switch (StrAreaSector)
                    {
                        case "M_OVEREVENT":
                            iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetMarketingEventPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, null, "M_OVEREVENT");
                            break;
                        case "M_OVERPC":
                             iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetMarketingPureCatskillsPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                            SetupViewer_Generic(iPK_Level_01, null, "M_OVERPC");
                            break;
                    }
                    break;
                case "PART":
                    iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetParticipantPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                    SetupViewer_Generic(iPK_Level_01, null, "PART");
                    break;
                case "TP":
                    iPK_Level_01 = (int)Page.GetType().InvokeMember("Delegate_GetTaxParcelPK", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
                    SetupViewer_Generic(iPK_Level_01, null, "TP");
                    break;
            }
        }
    }

    private void DisplayErrorMessage(string sError)
    {
        lb.Text = "Database Error: Could not load documents.";
        lbl.Text = "<div style='margin:5px;'>" + sError + "</div>";
    }

    private string GetEncodedALink(string sFileName, string sFileNameDisplay)
    { 
        StringBuilder sb = new StringBuilder();
        sb.Append("<a href='" + ConfigurationManager.AppSettings["DocsLink"] + StrArea + "/" + sFileName + "'");
        if (!string.IsNullOrEmpty(sFileNameDisplay)) sb.Append("' target='_blank'>" + sFileNameDisplay + "</a>");
        else sb.Append("' target='_blank'>" + sFileName + "</a>");

        return sb.ToString();
    }
    
    private void BindDocumentList(List<AttachedDocument> docList)
    {
        StringBuilder sb = new StringBuilder();
        lvDocuments.DataSource = docList;
        lvDocuments.DataBind();
        sb.Append("(");
        sb.Append(docList.Count());
        sb.Append(")");
        lblDocumentCount.Text = sb.ToString();
        sb = null;
    }
    private void SetupViewer_Generic(int iPK1, int? iPK2, string sSectorCode)
    {
        lb.Visible = false;
        pnl.Visible = false;
        upDocuments.Visible = true;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.documentArchives.Where(w => w.PK_1 == iPK1 && w.list_participantTypeSectorFolder.fk_participantTypeSector_code == sSectorCode).OrderBy(o => o.fk_participantTypeSectorFolder_code).Select(s => s);
                if (iPK2 != null) a = wDataContext.documentArchives.Where(w => w.PK_1 == iPK1 && w.PK_2 == iPK2 && w.list_participantTypeSectorFolder.fk_participantTypeSector_code == sSectorCode).OrderBy(o => o.fk_participantTypeSectorFolder_code).Select(s => s);
                var c = a.Select(s => new AttachedDocument(s.list_participantTypeSectorFolder.folder, s.filename_actual, s.filename_display, sSectorCode));
                BindDocumentList(c.ToList<AttachedDocument>());
            }
        }
        catch (Exception ex) { DisplayErrorMessage(ex.Message); }
    }

    private void SetupViewer_Home()
    {
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                StringBuilder sb = new StringBuilder();
                var a = wDataContext.documentArchives.Where(w => w.list_participantTypeSectorFolder.pk_participantTypeSectorFolder_code == "H_OVER").
                    OrderBy(o => o.filename_display).Select(s => new AttachedDocument(string.Empty, s.filename_actual, s.filename_display,"H_OVER"));
                BindDocumentList(a.ToList<AttachedDocument>());
            }
        }
        catch (Exception ex) { DisplayErrorMessage(ex.Message); }
    }

    private void SetupViewer_Ag_WFP3_BMP(int iPK1, int? iPK2)
    {
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                //StringBuilder sb = new StringBuilder();
                var a = wDataContext.documentArchives.Where(w => w.PK_1 == iPK1 && w.PK_2 == iPK2 &&
                    w.list_participantTypeSectorFolder.fk_participantTypeSector_code == "A_WFP3_BMP").OrderBy(o => o.PK_3).
                    ThenBy(o => o.fk_participantTypeSectorFolder_code).Select(s => new AttachedDocument(s.list_participantTypeSectorFolder.folder,
                        s.filename_actual, s.filename_display,"A_WFP3_BMP"));
                BindDocumentList(a.ToList<AttachedDocument>());
                //sb.Append("<table cellpadding='5' rules='cols'>");
                //sb.Append("<tr valign='top'>");
                //sb.Append("<td class='B U'>BMP</td>");
                //sb.Append("<td class='B U'>File Type</td>");
                //sb.Append("<td class='B U'>File Link</td>");
                //sb.Append("</tr>");
                //int iCount = 0;
                //foreach (var c in a)
                //{
                //    var x = wDataContext.bmp_ags.Where(w => w.pk_bmp_ag == c.PK_3).Select(s => new { s.bmp_nbr, s.description });

                //    sb.Append("<tr valign='top'>");
                //    if (x.Count() == 1)
                //    {
                //        iCount += 1;
                //        sb.Append("<td>BMP " + x.Single().bmp_nbr + " " + x.Single().description + "</td>");
                //        sb.Append("<td>" + c.list_participantTypeSectorFolder.folder + "</td>");
                //        sb.Append("<td>" + GetEncodedALink(c.filename_actual, c.filename_display) + "</td>");
                //    }
                //    sb.Append("</tr>");
                //}
                //sb.Append("</table>");
                //lb.Text = "[+] Documents (" + iCount + ")";
                //lbl.Text = sb.ToString();
            }
        }
        catch (Exception ex) { DisplayErrorMessage(ex.Message); }
    }

    protected void lb_Click(object sender, EventArgs e)
    {
        if (pnl.Visible == true)
        {
            pnl.Visible = false;
            lb.Text = lb.Text.Replace('-', '+');
        }
        else
        {
            pnl.Visible = true;
            lb.Text = lb.Text.Replace('+', '-');
        }
    }
}

//public class AttachedDocument
//{
//    public AttachedDocument(string _folder, string _hlink)
//    {
//        Folder = _folder;
//        ALink = _hlink;
//    }
//    public string Folder { get; set; }
//    public string ALink { get; set; }
//}
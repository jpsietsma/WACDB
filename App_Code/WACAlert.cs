using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Web.UI;

/// <summary>
/// Summary description for WACAlert
/// </summary>
public static class WACAlert
{
    public enum AlertLevel { NONE = 0, INFORMATION = 1, WARNING = 2, ERROR = 3 }

    public static void Show(string message, int iCode)
    {
        //string cleanMessage = MessageBuilder(message, iCode);
        Page page = HttpContext.Current.CurrentHandler as Page;
        RadWindowManager wacRadWindowManager = null;
        if (page.Master == null)
            wacRadWindowManager = (RadWindowManager)page.FindControl("WacRadWindowManager");
        else
            wacRadWindowManager = (RadWindowManager)page.Master.FindControl("WacRadWindowManager");
        Show(wacRadWindowManager, message, iCode);
        //string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

        //if (page != null)
        //{
        //    ScriptManager.RegisterClientScriptBlock(page, typeof(WACAlert), "alert", script, false);
        //}               
    }

    public static void Show(RadWindowManager wacRadWindowManager, string message, int iCode, AlertLevel messageLevel)
    {
        string messageLevelUrl = string.Empty;
        string messageType = string.Empty;
        switch (messageLevel)
        {
            case AlertLevel.INFORMATION:
                messageLevelUrl = "/images/Information32.png";
                messageType = "Informational Message";
                break;
            case AlertLevel.WARNING:
                messageLevelUrl = "/images/Warning32.png";
                messageType = "Warning";
                break;
            case AlertLevel.ERROR:
                messageLevelUrl = "/images/ErrorExclaimination32.png";
                messageType = "Error";
                break;
            default:
                messageLevelUrl = null;
                break;
        }

        string cleanMessage = MessageBuilder(message, iCode);

        wacRadWindowManager.RadAlert(cleanMessage, 600, 150, messageType, null, messageLevelUrl);
    }

    public static void Show(RadWindowManager wacRadWindowManager, string message, int iCode)
    {
        WACAlert.Show(wacRadWindowManager, message, iCode, AlertLevel.WARNING);
    }

    public static string MessageBuilder(string message, int iCode)
    {
        string sErrorMsg = string.Empty;
        if (iCode != 0)
        {
            iCode = Math.Abs(iCode);
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    sErrorMsg = wDataContext.errorMsgs.Where(o => o.pk_errorMsg == iCode).Select(s => s.errorMsg1).Single();
                }
                catch { }
            }
        }
        return HttpUtility.JavaScriptStringEncode(message + " " + sErrorMsg);
    }
    public static void DisplayNotificationMessage(Control control, string errormessage, string title)
    {

        if (control.FindControl("editErrorMessageArea") != null)
        {
            control.FindControl("editErrorMessageArea").Visible = true;
        }

        //if (master.FindControl("divdimmer") != null)
        //{
        //    master.FindControl("divdimmer").Visible = true;
        //}


        Label titlelabel = (Label)control.FindControl("errorMessageAreaTitle");

        if (titlelabel != null)
        {
            titlelabel.Text = title;
        }

        TextBox thetxtbox = (TextBox)control.FindControl("editErrorMessageText");

        if (thetxtbox != null)
        {
            thetxtbox.Text = errormessage;

        }
    }
}

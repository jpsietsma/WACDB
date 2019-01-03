using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AG_WACAG_WFP3_Grid : System.Web.UI.UserControl
{
   // public event AG_WACAG_WFP3.OpenFormViewEventHandler OnWFP3Clicked;
    public event FormActionCompletedEventHandler OnFormActionCompleted;
    public delegate void FormActionCompletedEventHandler(object sender, FormViewEventArgs e);
    public delegate void OpenGridViewEventHandler(object sender,PrimaryKeyEventArgs e);

    protected void Page_Load(object sender, EventArgs e)
    {
        gvAg_WFP3_Bind();
    }

    public int PK_FarmBusiness
    {
        get { try { return Convert.ToInt32(Session["PK_FarmBusiness"]); } catch (Exception) { return -1; } }
        set { Session["PK_FarmBusiness"] = value; }
    }

    //public void OpenGirdView(object sender, PrimaryKeyEventArgs p)
    //{
    //    PK_FarmBusiness = p.PrimaryKey;    
    //    gvAg_WFP3_Bind();  
    //}
    public void OnWFP3CloseClick(object sender, FormViewEventArgs e)
    {
        PK_FarmBusiness = e.PrimaryKey;
        gvAg_WFP3_Bind();        
        upnl_WFP3.Update();
    }
    protected void lbAg_WFP3_Add_Click(object sender, EventArgs e)
    {
        if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "I", "A", "form_wfp3", "msgInsert"))
        {
            //OnWFP3Clicked(this, new FormViewEventArgs(-1, PK_FarmBusiness, FormViewMode.Insert));
            string queryString = "PK_FormWfp3=-1" + "&FK_FarmBusiness=" + PK_FarmBusiness + "&AddPackage=true";
            Response.Redirect("~/AG_WFP3/WACAG_WFP3.aspx?" + queryString);
        }
    }

    protected void lbAg_WFP3_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        //OnWFP3Clicked(this, new FormViewEventArgs(Convert.ToInt32(lb.CommandArgument), PK_FarmBusiness, FormViewMode.ReadOnly));
        string queryString = "PK_FormWfp3=" + Convert.ToInt32(lb.CommandArgument) + "&FK_FarmBusiness=" + PK_FarmBusiness + "&AddPackage=false";
        Response.Redirect("~/AG_WFP3/WACAG_WFP3.aspx?" + queryString);
    }

    private void gvAg_WFP3_Bind()
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            //gvAg_WFP3
            var v = wDataContext.form_wfp3_grid_PK(PK_FarmBusiness).OrderBy(o => o.packageName);
            gvAg_WFP3.DataSource = v.OrderBy(o => o.packageName);
            gvAg_WFP3.DataBind();
        }

    }
}
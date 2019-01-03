using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Map : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //lbl.Text = Request.QueryString["address"] + "," + Request.QueryString["city"] + "," + Request.QueryString["state"] + "," + Request.QueryString["zip"];

            //tb.Text = "380 New York St, Redlands, CA, 92373";
            //tb.Text = "4 Horseshoe Drive, Johnstown, NY, 12095";
            hf.Value = Request.QueryString["address"] + ", " + Request.QueryString["city"] + ", " + Request.QueryString["state"] + ", " + Request.QueryString["zip"];


            //string script = "<script type='text/javascript'> ";
            //script += "init();";
            //script += "locate('380 New York St', 'Redlands', 'CA', '92373');";
            //script += "</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", script);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //lbl.Text = Request.QueryString["address"];


        //string script = "<script type='text/javascript'> ";
        //script += "locate('380 New York St', 'Redlands', 'CA', '92373');";
        //script += "</script>";
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", script);

    }
}
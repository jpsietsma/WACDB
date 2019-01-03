using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string myConnect = ConfigurationManager.ConnectionStrings["wacConnectionString"].ConnectionString;
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (!Page.IsValid) return;

        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = from b in wDataContext.users.Where(w => w.username == tbUN.Text)
                        select b;
                if (a.Count() == 1)
                {
                    if (a.Single().password.ToUpper() == tbPW.Text.ToUpper())
                    {
                        Session["user"] = true;
                        Session["userID"] = a.Single().pk_user;
                        Session["userName"] = a.Single().fullname;
                        FormsAuthentication.RedirectFromLoginPage(tbUN.Text, false);
                    }
                    //else lbl.Text = "Incorrect password. Please try again.";
                }
                //else lbl.Text = "Could not find username. Please make sure it is spelled correctly";
            }
            catch (Exception ex) { }
            //{ lbl.Text = "Error: " + ex.Message; }

        }


        //if (FormsAuthentication.Authenticate(tbUN.Text, tbPW.Text))
        //{
        //    Session["user"] = true;
        //    FormsAuthentication.RedirectFromLoginPage(tbUN.Text, false);
        //}
        //else lbl.Text = "Incorrect Username or Password";
    }
}
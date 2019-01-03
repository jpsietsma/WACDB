using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WACTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnFarmBusinessActive_Click(object sender, EventArgs e)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinesses.Where(w => w.farmID != "").OrderBy(o => o.farmID).Select(s => new { s.farmID, s.farm_name, s.farmBusinessStatus, s.farmBusinessOwners });
            gvFarmBusiness.DataSource = a;
            gvFarmBusiness.DataBind();
            lblRecordCount.Text = "Records: " + a.Count();
        }
    }

    protected void btnBMPs_Click(object sender, EventArgs e)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            //var a = wDataContext.bmp_ags.Where(w => w.fk_statusBMP_code == "C" && w.completed_date < new DateTime(2009, 4, 29) && (w.fk_bmpPractice_code == 382.0m || w.fk_bmpPractice_code == 390.0m || w.fk_bmpPractice_code == 391.0m)).OrderBy(o => o.farmBusiness.farmID);
            //var a = wDataContext.bmp_ags.Where(w => w.fk_statusBMP_code == "C" && w.completed_date < new DateTime(2009, 4, 29) && (w.fk_bmpPractice_code == 707.0m || w.fk_bmpPractice_code == 3220.0m)).OrderBy(o => o.farmBusiness.farmID);
            var a = wDataContext.bmp_ags.Where(w => w.farmBusiness.farmID == "DEC-002" || w.farmBusiness.farmID == "DEC-035" || w.farmBusiness.farmID == "DEC-SF061" || w.farmBusiness.farmID == "DEC-SF081").OrderBy(o => o.farmBusiness.farmID).ThenBy(o => o.fk_bmpPractice_code);
            gvBMPs.DataSource = a;
            gvBMPs.DataBind();
            lblRecordCount.Text = "Records: " + a.Count();

            var b = a.GroupBy(g => g.farmBusiness.farmID).OrderBy(o => o.Key);

            string s = string.Empty;
            foreach (var c in b)
            {
                //ddl.Items.Add(new ListItem(c.Key));
                //lbox.Items.Add(c.Key);
                s += c.Key + "<br />";
            }
            lbl.Text = s;
            //gv2.DataSource = b;
            //gv2.DataBind();

            //gv.DataSource = wDataContext.list_bmpPractices.OrderBy(o => o.practice);
            //gv.DataBind();
        }
    }

    protected void btnNoBMPs_Click(object sender, EventArgs e)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.farmBusinesses.Where(w => w.farmID != "" && w.bmp_ags.Count() == 0 && w.farmBusinessStatus.First(f => f.fk_status_code == "A").fk_status_code == "A").OrderBy(o => o.farmID);
            //gv.DataSource = a;
            //gv.DataBind();
            //lblRecordCount.Text = "Records: " + a.Count();

            int i = 0;
            string s = string.Empty;
            foreach (var c in a)
            {
                if (c.farmID.Substring(0, 3) == "DEC" || c.farmID.Substring(0, 3) == "DEP")
                {
                    s += c.farmID + "<br />";
                    i += 1;
                }
            }
            lbl.Text = s;
            lblRecordCount.Text = "Records: " + i;
        }
    }

    protected void btnFarmLand_Click(object sender, EventArgs e)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            //var a = wDataContext.farmLandTractFields.Where(w => w.pk_farmLandTractField == 12376);
            //string s = string.Empty;
            //s += "FarmLandTractField_PK: 12376<br />";
            //s += "FarmLandTract_PK: " + a.Single().fk_farmLandTract.ToString() + "<br />";
            //s += "FarmLand_PK: " + a.Single().farmLandTract.fk_farmLand + "<br />";
            //try { s += "FarmBusiness_PK: " + a.Single().farmLandTract.farmLand.farmBusinesses.Where(w => w.fk_farmLand == a.Single().farmLandTract.fk_farmLand).Single().pk_farmBusiness + "<br />"; }
            //catch { s += "FarmBusiness_PK: NO MATCHING PK"; }

            string sTract = "T381";
            var a = wDataContext.farmBusinesses.Where(w => w.farmLand.farmLandTracts.First(f => f.tract == sTract).tract == sTract);
            try
            {
                string s = "Farms:<br />";
                foreach (var c in a)
                {
                    s += c.farmID + "<br />";
                }
                lbl.Text = s;
            }
            catch { lbl.Text = "No Match"; }
        }
    }
}
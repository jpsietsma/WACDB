using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class AG_BMP_WACAG_BMPSupplementalAgreement : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbAg_BMP_SA_View_Click(object sender, EventArgs e)
    {

    }

    protected void lbAg_BMP_SA_Delete_Click(object sender, EventArgs e)
    {
        //if (WACGlobal_Methods.Security_UserCanPerformAction(Session["userID"], "D", "A", "bmp_ag_SA", "msgDelete"))
        //{
        //    LinkButton lb = (LinkButton)sender;
        //    int iCode = 0;
        //    using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        //    {
        //        try
        //        {
        //            iCode = wDataContext.bmp_ag_SA_delete(Convert.ToInt32(lb.CommandArgument), Session["userName"].ToString());
        //            if (iCode == 0) BindAg_BMP(Convert.ToInt32(fvAg_BMP.DataKey.Value));
        //            else WACAlert.Show("Error Returned from Database.", iCode);
        //        }
        //        catch (Exception ex) { WACAlert.Show(ex.Message, 0); }
        //    }
        //}
    }
    //<td class="B U">SA #</td>
    //                    <td class="B U">Revision #</td>
    //                    <td class="B U">Tax Parcel</td>
    //                    <td class="B U">Active</td>
    //                    <td class="B U">Note</td>

    IEnumerable BmpSAs = null;
    public class SupplementalAgreement
    {
        public string Revision { get; set; }
        public string TaxParcel { get; set; }
        public string Active { get; set; }
        public string Note { get; set; }
    }

    private IEnumerable<SupplementalAgreement> LoadBPA_SA()
    {
        IEnumerable<SupplementalAgreement> b = null;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            //b = wac.bmp_ag_SAs.Where(
        }
        return b;
    }
}
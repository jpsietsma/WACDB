using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Services;
using WAC_DataObjects;
using WAC_Containers;
using System.Reflection;

public partial class WACAG_QMACheckbox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public ListItemCollection Items 
    {
        get { return cbAsrQma.Items; } 
    }
    public bool Enabled 
    { 
        get { return cbAsrQma.Enabled; }
        set { cbAsrQma.Enabled = (bool)value;  }
    }
    public void BindCheckList()
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var q = wac.list_QMATypes.OrderBy(o => o.type).Select(s =>
                new DDLListItem(s.pk_QMAType_code, s.type));
            cbAsrQma.DataSource = q;
            cbAsrQma.DataTextField = "DataTextField";
            cbAsrQma.DataValueField = "DataValueField";
            cbAsrQma.DataBind();         
        }
    }
    public void BindCheckList(string checkedList)
    {
        cbAsrQma.DataSource = null;
        cbAsrQma.DataBind();
        BindCheckList();
        if (!string.IsNullOrEmpty(checkedList))
        {
            ListItemCollection listItems = cbAsrQma.Items;
            foreach (string val in checkedList.Split(','))
            {
                ListItem li = listItems.FindByValue(val);
                if (li != null)
                    li.Selected = true;
            }
        }
    }
    public string GetCheckedList()
    {
        StringBuilder sb = new StringBuilder();
        foreach (ListItem li in cbAsrQma.Items)
        {
            if (li.Selected)
            {
                sb.Append(li.Value);
                sb.Append(",");
            }
        }
        if (sb.Length > 1)
            return sb.Remove(sb.Length-1, 1).ToString();
        else
            return null;
    }

   
    protected void cbAsrQma_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListControl lc = (ListControl)sender;

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for FilterChangedEventArgs
/// </summary>
public class FilterChangedEventArgs : EventArgs
{
    public FilterChangedEventArgs(List<string> f)
    {
        filters = f;
    }
    private List<string> filters;
    public List<string> Filters
    {
        get { return filters; }
        set { filters = value; }
    }
}
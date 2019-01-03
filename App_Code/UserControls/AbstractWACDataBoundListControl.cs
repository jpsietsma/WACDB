using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_DataObjects;
using WAC_Containers;
using System.Text;


namespace WAC_UserControls
{
    /// <summary>
    /// Summary description for WACDataBoundListControl
    /// </summary>
    public abstract class WACDataBoundListControl : WACControl
    {

        public enum ListState { ItemDeleted, ListEmpty, ListFull, ListSingle, OpenView, SelectionMade, Closed, MasterKeySet, RowCountSet, Stale }
        public ListState CurrentState
        {
            get { return ViewState[ClientID + "_ListState"] != null ? (ListState)ViewState[ClientID + "_ListState"] : ListState.ListEmpty; }
            set { ViewState[ClientID + "_ListState"] = value; }
        }
        public string RowCount { get; set; }

    }
}
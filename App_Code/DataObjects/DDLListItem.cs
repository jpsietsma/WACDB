using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI;

/// <summary>
/// Summary description for DDLListItem
/// </summary>
namespace WAC_DataObjects
{
    public class DDLListItem
    {
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public bool SelectedValue { get; set; }
        
        public DDLListItem(string _valueField,string _textField)
        {
            DataTextField = _textField;
            DataValueField = _valueField;
            SelectedValue = false;
        }
    }
    // Custom comparer for the DDLListItem class 
    public class DDLListItemComparer : IEqualityComparer<DDLListItem>
    {
        // Products are equal if their names and DDLListItem numbers are equal. 
        public bool Equals(DDLListItem x, DDLListItem y)
        {

            //Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal. 
            return x.DataTextField == y.DataTextField;
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 

        public int GetHashCode(DDLListItem DDLListItem)
        {
            //Check whether the object is null 
            if (Object.ReferenceEquals(DDLListItem, null)) return 0;

            //Get hash code for the DataTextField field if it is not null. 
            int hashDataTextField = DDLListItem.DataTextField == null ? 0 : DDLListItem.DataTextField.GetHashCode();

            //Calculate the hash code for the DDLListItem. 
            return hashDataTextField;
        }
    }
    
}
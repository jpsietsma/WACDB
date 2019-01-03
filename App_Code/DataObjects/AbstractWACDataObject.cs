using System;
using System.Reflection;

/// <summary>
/// Summary description for WACDataObject
/// </summary>
namespace WAC_DataObjects
{
    public abstract class WACDataObject
    {
        //public abstract WACParameter PrimaryKeyAsWACParameter();
        public abstract string PrimaryKeyAsString();
        public string created_by { get; set; }
        public DateTime? created { get; set; }
        public string modified_by { get; set; }
        public DateTime? modified { get; set; }

        public string GetPropertyAsString(string pName)
        {
            PropertyInfo p = this.GetType().GetProperty(pName);
            return p.GetValue(this, null).ToString();
        }
        
    }
}
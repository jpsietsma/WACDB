using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for swis
/// </summary>
namespace WAC_DataObjects
{
    public class SWIS : WACDataObject, IWACDataObject<SWIS>
    {
        public const string SWISTextField = "pk_list_swis";
        public const string CountyTextField = "county";
        public const string CountyValueField = "pk_county_code";
        public const string JurisdictionTextField = "jurisdiction";
        public const string ValueField = "pk_list_swis";
        public const string PrimaryKeyName = "pk_list_swis";
        public string pk_list_swis { get; set; }
        public string county { get; set; }
        public string muniname { get; set; }
        public string munitype { get; set; }
        public bool active { get; set; }
        public string jurisdiction { get; set; }

	    public SWIS()
	    {
		    //
		    // TODO: Add constructor logic here
		    //

	    }
        public SWIS(string _pk, string _county, string _muniName, string _muniType, string _active, string _jurisdiction)
        {
            pk_list_swis = _pk;
            county = _county;
            muniname = _muniName;
            munitype = _muniType;
            active = (string.IsNullOrEmpty(_active) || _active == "Y") ? true : false;
            jurisdiction = _jurisdiction;
        }
        public SWIS(string _pk, string _county, string _jurisdiction)
        {
            pk_list_swis = _pk;
            county = _county;
            jurisdiction = _jurisdiction;
        }
        public SWIS Clone()
        {
            return (SWIS)this.MemberwiseClone();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
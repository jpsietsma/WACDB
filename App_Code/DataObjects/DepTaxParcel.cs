using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;

/// <summary>
/// Summary description for DepTaxParcel
/// </summary>
namespace WAC_DataObjects
{
    public class DepTaxParcel : WACDataObject, IWACDataObject<DepTaxParcel>
    {
        public string SWIS { get; set; }
        public string SBL { get; set; }
        public string PrintKey { get; set; }
        public decimal? Acres { get; set; }
        public string County { get; set; }
        public string CityTown { get; set; }
        public string Village { get; set; }
        public string Owner1 { get; set; }
        public string Owner2 { get; set; }
        public string Street { get; set; }
        public string CityState { get; set; }
        public string ZipCode { get; set; }
        public string MailAddr1 { get; set; }
        public string MailAddr2 { get; set; }
        public int? TaxYear { get; set; }
        public string SBLSection { get; set; }

	    public DepTaxParcel()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public DepTaxParcel(string swis, string sbl, string printKey, decimal acres, string county, string cityTown, string village, string owner1,
            string owner2, string street, string cityState, string zipCode, string mailAddr1, string mailAddr2, int taxYear, string sblSection)
        {
            SWIS = swis;
            SBL = sbl;
            PrintKey = printKey;
            Acres = acres;
            County = county;
            CityTown = CityTown;
            Village = village;
            Owner1 = owner1;
            Owner2 = owner2;
            Street = street;
            CityState = cityState;
            ZipCode = zipCode;
            MailAddr1 = mailAddr1;
            MailAddr2 = mailAddr2;
            TaxYear = taxYear;
            SBLSection = sblSection;
        }
        public DepTaxParcel(string swis, string sbl, string printKey, string sblSection)
        {
            SWIS = swis;
            SBL = sbl;
            PrintKey = printKey;
            SBLSection = sblSection;
        }
        public static int CompareByField(DepTaxParcel e1, DepTaxParcel e2, string sortBy)
        {
            int result = 0;
            switch (sortBy)
            {
                case "PrintKey":
                    result = SortExtensions.Compare(e1.PrintKey, e2.PrintKey);
                    break;
                case "SBL":
                    result = SortExtensions.Compare(e1.SBL, e2.SBL);
                    break;
                case "SWIS":
                    result = SortExtensions.Compare(e1.SWIS, e2.SWIS);
                    break;
                case "County":
                    result = SortExtensions.Compare(e1.County, e2.County);
                    break;
                default:
                    break;
            }
            return result;
        }

        public DepTaxParcel Clone()
        {
            return (DepTaxParcel)this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(DepTaxParcel))
                return false;
            else
                return this.PrintKey == ((DepTaxParcel)obj).PrintKey;
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }

}
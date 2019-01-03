using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;

/// <summary>
/// Summary description for TaxParcel
/// </summary>
namespace WAC_DataObjects
{
    public class TaxParcel : WACDataObject, IWACDataObject<TaxParcel>
    {
        public int pk_taxParcel { get; set; }
        public string fk_list_swis { get; set; }
        public string taxParcelID { get; set; }
        public string note { get; set; }
        public string SBL { get; set; }
        public string ownerStr_dnd { get; set; }
        public decimal? acreage { get; set; }
        public SWIS Swis { get; set; }
        public string retired { get; set; }
        public const string PrimaryKeyName = "pk_taxParcel";
        public const string MasterKeyName = "pk_taxParcel";

        public TaxParcel()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        public TaxParcel(int pk, string fk_swis, string tpID, string _note, string sbl, string owner, decimal? acres, string _retired, DateTime? crDate, string crBy, DateTime? modDate,
            string modBy)
        {
            pk_taxParcel = pk;
            fk_list_swis = fk_swis;
            taxParcelID = tpID;
            note = _note;
            SBL = sbl;
            ownerStr_dnd = owner;
            acreage = acres;
            retired = _retired;
            created = crDate;
            created_by = crBy;
            modified = modDate;
            modified_by = modBy;
        }
        public TaxParcel(int pk, string fk_swis, string tpID, string _note, string sbl, string owner, decimal? acres, string _retired, string county, string jurisdiction,
            DateTime? crDate, string crBy, DateTime? modDate, string modBy)
        {
            pk_taxParcel = pk;
            fk_list_swis = fk_swis;
            taxParcelID = tpID;
            note = _note;
            SBL = sbl;
            ownerStr_dnd = owner;
            acreage = acres;
            retired = _retired;
            Swis = new SWIS(fk_list_swis, county, jurisdiction);
            created = crDate;
            created_by = crBy;
            modified = modDate;
            modified_by = modBy;
        }
        public TaxParcel(int pk, string fk_swis, string tpID, string county, string jurisdiction, string _retired)
        {
            pk_taxParcel = pk;
            fk_list_swis = fk_swis;
            taxParcelID = tpID;
            Swis = new SWIS(fk_list_swis, county, jurisdiction);
            retired = _retired;
    
        }

        public TaxParcel(int pk, string fk_swis, string tpID, string _note, string sbl, string owner, decimal? acres, string _retired, string county, string jurisdiction)
        {
            pk_taxParcel = pk;
            fk_list_swis = fk_swis;
            taxParcelID = tpID;
            SBL = sbl;
            ownerStr_dnd = owner;
            Swis = new SWIS(fk_list_swis, county, jurisdiction);
            acreage = acres;
            retired = _retired;
            
        }
        public TaxParcel(int pk, string fk_swis, string tpID, string _note, string sbl, string owner, decimal? acres, string _retired)
        {
            pk_taxParcel = pk;
            fk_list_swis = fk_swis;
            taxParcelID = tpID;
            SBL = sbl;
            ownerStr_dnd = owner;
            acreage = acres;    
            retired = _retired;
        }
        public static int CompareByField(TaxParcel e1, TaxParcel e2, string sortBy)
        {
            int result = 0;
            switch (sortBy)
            {
                case "taxParcelID":
                    result = SortExtensions.Compare(e1.taxParcelID, e2.taxParcelID);
                    break;
                case "SBL":
                    result = SortExtensions.Compare(e1.SBL, e2.SBL);
                    break;
                case "Swis":
                    result = SortExtensions.Compare(e1.Swis.pk_list_swis, e2.Swis.pk_list_swis);
                    break;
                case "County":
                    result = SortExtensions.Compare(e1.Swis.county, e2.Swis.county);
                    break;
                case "Jurisdiction":
                    result = SortExtensions.Compare(e1.Swis.jurisdiction, e2.Swis.jurisdiction);
                    break;
                case "ownerStr_dnd":
                    result = SortExtensions.Compare(e1.ownerStr_dnd, e2.ownerStr_dnd);
                    break;
                case "retired":
                    result = SortExtensions.Compare(e1.retired, e2.retired);
                    break;
                default:
                    break;
            }
            return result;
        }

        public TaxParcel Clone()
        {
            return (TaxParcel)this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(TaxParcel))
                return false;
            else
                return this.pk_taxParcel == ((TaxParcel)obj).pk_taxParcel;
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
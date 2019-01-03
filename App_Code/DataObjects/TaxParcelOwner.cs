using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;

namespace WAC_DataObjects
{

    /// <summary>
    /// Summary description for TaxParcelOwner
    /// </summary>
    public class TaxParcelOwner : WACDataObject, IWACDataObject<TaxParcelOwner>
    {
        public int? pk_taxParcelOwner { get; set; }
        public int fk_taxParcel { get; set; }
        public int pk_taxParcel { get; set; }
        public int fk_participant { get; set; }        
        public int pk_participant { get; set; }
        public string fullname_FL_dnd { get; set; }
        public string fullname_LF_dnd { get; set; }
        public string fk_list_swis { get; set; }
        public string taxParcelID { get; set; }
        public string SBL { get; set; }
        public decimal? acreage { get; set; }
        public string retired { get; set; }
        public string county { get; set; }
        public string note { get; set; }
        public string active { get; set; }
        public int? pk_list_countyNY { get; set; }
        public const string PrimaryKeyName = "pk_taxParcelOwner";
        public const string MasterKeyName = "fk_taxParcel";

        public TaxParcelOwner() { }
        public TaxParcelOwner(int? _pk_taxParcelOwner, int _fk_taxParcel, int _fk_participant, string _participant, string _note, string _active,
            string _crBy, DateTime? _crDate, string _modBy, DateTime? _modDate)
        {
            pk_taxParcelOwner = _pk_taxParcelOwner;
            fk_taxParcel = _fk_taxParcel;
            pk_taxParcel = _fk_taxParcel;
            fk_participant = _fk_participant;
            fullname_FL_dnd = _participant;
            note = _note;
            active = _active;
            created_by = _crBy;
            created = _crDate;
            modified_by = _modBy;
            modified = _modDate;
        }
        public TaxParcelOwner(int? _pk_taxParcelOwner,int _fk_participant, string _participant, string _note, string _active,
            string _crBy, DateTime? _crDate)
        {
            pk_taxParcelOwner = _pk_taxParcelOwner;
            fk_participant = _fk_participant;
            fullname_FL_dnd = _participant;
            note = _note;
            active = _active;
            created_by = _crBy;
            created = _crDate;
        }
        //Read from view
       public TaxParcelOwner(int? _pk_taxParcelOwner, int _pk_taxParcel, int _pk_participant, string _fullname_LF, string _fullname_FL,
            string _fk_list_swis,  string _taxParcelID, string _SBL, decimal? _acreage, string _retired, 
            string _note, string _active, string _county, int? _pk_list_countyNY, string _crBy, DateTime? _crDate)
        {
            pk_taxParcelOwner = _pk_taxParcelOwner;
            pk_taxParcel = _pk_taxParcel;
            fk_taxParcel = _pk_taxParcel;
            pk_participant = _pk_participant;
            fk_list_swis = _fk_list_swis;
            fullname_FL_dnd = _fullname_FL;
            fullname_LF_dnd = _fullname_LF;
            taxParcelID = _taxParcelID;
            SBL = _SBL;
            acreage = _acreage;
            retired = _retired;
            county = _county;
            note = _note;
            active = _active;
            pk_list_countyNY = _pk_list_countyNY;
            created_by = _crBy;
            created = _crDate;
        }
        public static int CompareByField(TaxParcelOwner e1, TaxParcelOwner e2, string sortBy)
        {
            int result = 0;
            switch (sortBy)
            {
                case "active":
                    result = SortExtensions.Compare(e1.active, e2.active);
                    break;
                default:
                    break;
            }
            return result;
        }
        public TaxParcelOwner Clone()
        {
            TaxParcelOwner tpo = (TaxParcelOwner)this.MemberwiseClone();
            tpo.fk_participant = tpo.pk_participant;
            tpo.fk_taxParcel = tpo.pk_taxParcel;
            return tpo;
        }
        
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(TaxParcelOwner))
                return false;
            else
                return this.pk_taxParcelOwner == ((TaxParcelOwner)obj).pk_taxParcelOwner;
        }

        public override string PrimaryKeyAsString()
        {
            if (pk_taxParcelOwner != null)
                return pk_taxParcelOwner.ToString();
            else
                return null;
        }
    }

    public class TaxParcelOwnerView : TaxParcelOwner
    {

        
        public TaxParcelOwnerView() { }
       
        public TaxParcelOwnerView(int? _pk_taxParcelOwner, int _pk_taxParcel, int _pk_participant, string _fullname_LF, string _fullname_FL,
            string _fk_list_swis,  string _taxParcelID, string _SBL, decimal? _acreage, string _retired, 
            string _note, string _active, string _county, int? _pk_list_countyNY, string _crBy, DateTime? _crDate)
        {
            pk_taxParcelOwner = _pk_taxParcelOwner;
            pk_taxParcel = _pk_taxParcel;
            pk_participant = _pk_participant;
            fk_list_swis = _fk_list_swis;
            fullname_FL_dnd = _fullname_FL;
            fullname_LF_dnd = _fullname_LF;
            taxParcelID = _taxParcelID;
            SBL = _SBL;
            acreage = _acreage;
            retired = _retired;
            county = _county;
            note = _note;
            active = _active;
            pk_list_countyNY = _pk_list_countyNY;
            created_by = _crBy;
            created = _crDate;
        }
        public static int CompareByField(TaxParcelOwnerView e1, TaxParcelOwnerView e2, string sortBy)
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
                case "fullname_LF_dnd":
                    result = SortExtensions.Compare(e1.fullname_LF_dnd, e2.fullname_LF_dnd);
                    break;
                
                default:
                    break;
            }
            return result;
        }
      
        public override string PrimaryKeyAsString()
        {
            if (pk_taxParcelOwner != null)
                return pk_taxParcelOwner.ToString();
            else
                return null;
        }
    }
}
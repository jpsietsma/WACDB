using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;

/// <summary>
/// Summary description for SupplementalAgreement
/// </summary>
namespace WAC_DataObjects
{
    public class SupplementalAgreement : WACDataObject, IWACDataObject<SupplementalAgreement>
    {
        public const string PrimaryKeyName = "pk_supplementalAgreement";
        public int pk_supplementalAgreement { get; set; }
        public DateTime? agreement_date { get; set; }
        public string agreement_nbr_ro { get; set; }
        public DateTime? inactive_date { get; set; }
        public string ownerStr_dnd { get; set; }
        public string taxParcelStr { get; set; }
        public string ownerStrFL_dnd { get; set; }
        public string swis { get; set; }
        public string printKey { get; set; }

        public SupplementalAgreement()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SupplementalAgreement(DateTime? agDate, DateTime? inActDate, string agNbr, string owner, string ownerFull, string taxParcel)
        {
            agreement_date = agDate;
            inactive_date = inActDate;
            agreement_nbr_ro = agNbr;
            ownerStr_dnd = owner;
            ownerStrFL_dnd = ownerFull;
            taxParcelStr = taxParcel;
        }
        public override string PrimaryKeyAsString()
        {
            return "pk_supplementalAgreement";
        }

        public SupplementalAgreement Clone()
        {
            return (SupplementalAgreement)this.MemberwiseClone();
        }

       
    }
}
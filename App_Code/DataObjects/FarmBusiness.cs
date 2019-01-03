using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;

namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for FarmBusiness
    /// </summary>
    public class FarmBusiness : WACDataObject, IWACDataObject<FarmBusiness>
    {
        public const string PrimaryKeyName = "pk_farmBusiness";
        public int pk_farmBusiness { get; set; }
        public int? fk_farmLand { get; set; }
        public string farmID { get; set; }
        public string prior_LF_FarmID { get; set; }
        public string fk_basin_code { get; set; }
        public string fk_programWAC_code { get; set; }
        public string fk_farmSize_code { get; set; }
        public string farm_name { get; set; }
        public string fk_ownership_code { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public Decimal form_wfp2_total_plan_approved { get; set; }
        public Decimal form_wfp2_total_crep_approved { get; set; }
        public string wac_program_fad { get; set; }
        public string wac_program_nonfad { get; set; }
        public int? sold_farm { get; set; }
        public int? farm_cnt { get; set; }
        public int? subfarm_cnt { get; set; }
        public short multiple_farm_equivalents { get; set; }
        public string team { get; set; }
        public string fk_environmentalImpact_code { get; set; }
        public string IA_prior_to_implementation { get; set; }
        public string prior_implementation_commenced { get; set; }
        public string implementation_substantially_complete { get; set; }
        public DateTime? implementation_substantially_complete_date { get; set; }
        public DateTime? implementation_fully_complete_date { get; set; }
        public DateTime? approved_date_legacy { get; set; }
        public string status_comment { get; set; }
        public string fk_mailingStatus_code { get; set; }
        public string ownerStr_dnd { get; set; }
        public string supplementalAttachmentWFP2 { get; set; }
        public string EIN { get; set; }
        public string W9_filed { get; set; }
        public string planned { get; set; }
        public string CREP_capExempt { get; set; }
        public Decimal? guideline_curr { get; set; }
        public string fk_groupPI_code { get; set; }
        public string fk_regionWAC_code { get; set; }
        public string FAD { get; set; }
        public string forestry { get; set; }
        public string easement { get; set; }
        public string ranking_ro { get; set; }
        public string fk_status_code_orig { get; set; }
        public string asr_reqd { get; set; }
        public string fk_status_code_curr { get; set; }
        public Decimal? gps_id_legacy { get; set; }
        public int bmp_cnt_ro { get; set; }
        public string wfp1_signed { get; set; }
        public DateTime? wfp0_signed { get; set; }
        public string wfp0 { get; set; }
        public string fk_dataReview_code { get; set; }
        public string dataReview_note { get; set; }
        public string WACSign { get; set; }
        public string taxParcelStr_dnd { get; set; }
        public int? fk_farmBusiness_transferTo { get; set; }
        public int? fk_farmBusiness_transferFrom { get; set; }
        public DateTime? wfp1_signed_date { get; set; }
        public string ownerStrFL_dnd { get; set; }
        public string ownerStrLF_dnd { get; set; }
        public string priorOwner { get; set; }
        public string printKey { get; set; }
        public string swis { get; set; }
        public int? fk_participantOperator { get; set; }
        public int? fk_participantOwner { get; set; }
        public string GenerateID { get; set; }

        public FarmBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public FarmBusiness(int _pkFB, string _swis, string _printKey, int _pkOwn, int _pkOp, string _basinCode, string _size, string _prog, int? _pkSold, string _genID, string _createdBy)
        {
            pk_farmBusiness = _pkFB;
            swis = _swis;
            printKey = _printKey;
            fk_participantOwner = _pkOwn;
            fk_participantOperator = _pkOp;
            fk_basin_code = _basinCode;
            fk_farmSize_code = _size;
            fk_programWAC_code = _prog;
            sold_farm = _pkSold;
            GenerateID = _genID;
            created_by = _createdBy;
        }
        public FarmBusiness Clone()
        {
            return (FarmBusiness)this.MemberwiseClone();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
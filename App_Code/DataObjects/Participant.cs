using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;

namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for Participant
    /// </summary>
    public class Participant : WACDataObject, IWACDataObject<Participant>
    {
        public const string PrimaryKeyName = "pk_participant";
        public int pk_participant { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string email { get; set; }
        public string fk_gender_code { get; set; }
        public string gender { get; set; }
        public string fk_ethnicity_code { get; set; }
        public string ethnicity { get; set; }
        public string fk_race_code { get; set; }
        public string race { get; set; }
        public string fk_diversityData_code { get; set; }
        public string dataSetVia { get; set; }
        public string fk_mailingStatus_code { get; set; }
        public string mailingStatus { get; set; }
        public int? fk_property { get; set; }
        public string org { get; set; }
        public int? fk_organization { get; set; }
        public string active { get; set; }
        public DateTime? form_W9_signed_date { get; set; }
        public string fk_regionWAC_code { get; set; }
        public string regionWAC { get; set; }
        public string fk_prefix_code { get; set; }
        public string prefix { get; set; }
        public string mname { get; set; }
        public string nickname { get; set; }
        public string fk_suffix_code { get; set; }
        public string suffix { get; set; }
        public string fullname_FL_dnd { get; set; }
        public string fullname_LF_dnd { get; set; }
        public string web { get; set; }
        public string fk_dataReview_code { get; set; }
        public string dataReview_note { get; set; }
        public int? fk_participant_split { get; set; }
        public string fk_participantType_code { get; set; }
        public string participantType { get; set; }

        public Participant() { }

        public Participant(int _pk_participant, string _lname, string _fname, string _email, string _fk_gender_code, string _fk_ethnicity_code,
            string _fk_race_code, string _fk_diversityData_code, string _fk_mailingStatus_code, int? _fk_property, int? _fk_organization,
            string _active, DateTime? _form_w9_signed, string _fk_regionWAC_code, string _fk_prefix_code, string _mname, string _nickname, 
            string _fk_suffix_code, string _fullname_LF_dnd, string _fullname_FL_dnd, string _web, string _fk_dataReview_note, 
            string _dataReview_note, int? _fk_participant_split)
        {
            pk_participant = _pk_participant;
            lname = _lname;
            fname = _fname;
            email = _email;
            fk_gender_code = _fk_gender_code;
            fk_ethnicity_code = _fk_ethnicity_code;
            fk_race_code = _fk_race_code;
            fk_diversityData_code = _fk_diversityData_code;
            fk_mailingStatus_code = _fk_mailingStatus_code;
            fk_property = _fk_property;
            fk_organization = _fk_organization;
            active = _active;
            form_W9_signed_date = _form_w9_signed;
            fk_regionWAC_code = _fk_regionWAC_code;
            fk_prefix_code = _fk_prefix_code;
            mname = _mname;
            nickname = _nickname;
            fk_suffix_code = _fk_suffix_code;
            fullname_FL_dnd = _fullname_FL_dnd;
            fullname_LF_dnd = _fullname_LF_dnd;
            web = _web;
            fk_dataReview_code = _fk_dataReview_note;
            dataReview_note = _dataReview_note;
            fk_participant_split = _fk_participant_split;
        }

        public Participant(int _pk_participant, string _lname, string _fname, string _email, string _fk_gender_code, string _gender, string _fk_ethnicity_code,
            string _ethnicity, string _fk_race_code, string _race, string _fk_diversityData_code, string _dataSetVia, string _fk_mailingStatus_code, 
            string _mailingStatus, int? _fk_property, int? _fk_organization, string _org, string _active, DateTime? _form_w9_signed, string _fk_regionWAC_code, 
            string _regionWAC, string _fk_prefix_code, string _prefix, string _mname, string _nickname, string _fk_suffix_code, string _suffix, 
            string _fullname_LF_dnd, string _fullname_FL_dnd, string _web, string _fk_dataReview_note, string _dataReview_note, int? _fk_participant_split)
        {
            pk_participant = _pk_participant;
            lname = _lname;
            fname = _fname;
            email = _email;
            fk_gender_code = _fk_gender_code;
            gender = _gender;
            fk_ethnicity_code = _fk_ethnicity_code;
            ethnicity = _ethnicity;
            fk_race_code = _fk_race_code;
            race = _race;
            fk_diversityData_code = _fk_diversityData_code;
            dataSetVia = _dataSetVia;
            fk_mailingStatus_code = _fk_mailingStatus_code;
            mailingStatus = _mailingStatus;
            fk_property = _fk_property;
            fk_organization = _fk_organization;
            org = _org;
            active = _active;
            form_W9_signed_date = _form_w9_signed;
            fk_regionWAC_code = _fk_regionWAC_code;
            regionWAC = _regionWAC;
            fk_prefix_code = _fk_prefix_code;
            prefix = _prefix;
            mname = _mname;
            nickname = _nickname;
            fk_suffix_code = _fk_suffix_code;
            suffix = _suffix;
            fullname_FL_dnd = _fullname_FL_dnd;
            fullname_LF_dnd = _fullname_LF_dnd;
            web = _web;
            fk_dataReview_code = _fk_dataReview_note;
            dataReview_note = _dataReview_note;
            fk_participant_split = _fk_participant_split;
        }
        public string GetSelectedDiversityDataCode(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_diversityData_code;
            else
                return string.Empty;
        }
        public string GetSelectedEthnicityCode(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_ethnicity_code;
            else
                return string.Empty;
        }
        public string GetSelectedGenderCode(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_gender_code;
            else
                return string.Empty;
        }
        public string GetSelectedMailingStatusCode(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_mailingStatus_code;
            else
                return string.Empty;
        }
        public string GetSelectedPrefixCode(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_prefix_code;
            else
                return string.Empty;
        }
        public string GetSelectedRaceCode(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_race_code;
            else
                return string.Empty;
        }
        public string GetSelectedRegionWACCode(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_regionWAC_code;
            else
                return string.Empty;
        }
        public string GetSelectedSuffix(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.fk_suffix_code;
            else
                return string.Empty;
        }
        public string GetSelectedActive(object item)
        {
            Participant p = item as Participant;
            if (p != null)
                return p.active;
            else
                return string.Empty;
        }
        public Participant Clone()
        {
            return (Participant)this.MemberwiseClone();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
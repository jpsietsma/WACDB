using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Extensions;
using WAC_Event;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for ParticipantOrganizationDP
    /// </summary>
    public class ParticipantOrganizationDP : DDLDataProvider
    {
        public enum AlphaPickerListType { Participant, Organization, ParticipantOrganization, TaxParcelOwner, Operator}

        public ParticipantOrganizationDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            AlphaPickerListType listType;
            try
            {
                listType = (AlphaPickerListType)WACParameter.GetParameterValue(parms, WACParameter.ParameterType.PickerListType);
                string startsWith = WACParameter.GetParameterValue(parms, "NameStartsWith") as string;
                if (string.IsNullOrEmpty(startsWith))
                    switch (listType)
                    {
                        case AlphaPickerListType.Participant:
                            return participantOnlyList();
                        case AlphaPickerListType.Organization:
                            return organizationOnlyList();
                        case AlphaPickerListType.ParticipantOrganization:
                            return participantAndOrganizationList();
                        case AlphaPickerListType.TaxParcelOwner:
                            return taxParcelOwner();
                        case AlphaPickerListType.Operator:
                            return operatorList(startsWith);
                        default:
                            return null;
                    }
                else
                    switch (listType)
                    {
                        case AlphaPickerListType.Participant:
                            return participantOnlyList(startsWith);
                        case AlphaPickerListType.Organization:
                            return organizationOnlyList(startsWith);
                        case AlphaPickerListType.ParticipantOrganization:
                            return participantAndOrganizationList(startsWith);
                        case AlphaPickerListType.TaxParcelOwner:
                            return taxParcelOwner(startsWith);
                        case AlphaPickerListType.Operator:
                            return operatorList(startsWith);
                        default:
                            return null;
                    }
            }
            catch (Exception ex)
            {
                WACAlert.Show("Invalid or Missing AlphaPickerListType In ParticipantOrganizationDP" + ex.Message, 0);
                return null;
            }            
        }

        public override string GetSelected(object item)
        {
            return string.Empty;
        }
        private IList<DDLListItem> operatorList(string startsWith)
        {

            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_farmBusiness_operators.Where(w => w.fullname_LF_dnd.StartsWith(startsWith)).
                    Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => new DDLListItem(s.fk_participant.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }
        private IList<DDLListItem> participantOnlyList(string startsWith)
        {
            
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_participantTypes.Where(w => w.IsOrg.Contains("N") && w.fullname_LF_dnd.StartsWith(startsWith)).
                    Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => new DDLListItem(s.pk_participant.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }
        private IList<DDLListItem> organizationOnlyList(string startsWith)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_participantTypes.Where(w => w.IsOrg.Contains("Y") && w.fullname_LF_dnd.StartsWith(startsWith)).
                    Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => new DDLListItem(s.pk_participant.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }
        private IList<DDLListItem> participantAndOrganizationList(string startsWith)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_participantTypes.Where(w => w.fullname_LF_dnd.StartsWith(startsWith)).
                    Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => new DDLListItem(s.pk_participant.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }

        private IList<DDLListItem> taxParcelOwner(string startsWith)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.taxParcels.Where(w => w.ownerStr_dnd.StartsWith(startsWith)).
                    Distinct((x, y) => x.ownerStr_dnd == y.ownerStr_dnd).OrderBy(o => o.ownerStr_dnd).
                    Select(s => new DDLListItem(s.pk_taxParcel.ToString(), s.ownerStr_dnd.TrimEnd()));               
                return a.ToList();
            }
        }
        private IList<DDLListItem> operatorList()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_farmBusiness_operators.Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                        Select(s => new DDLListItem(s.pk_farmBusinessOperator.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }
        private IList<DDLListItem> participantOnlyList()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_participantTypes.Where(w => w.IsOrg.Contains("N")).
                    Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => new DDLListItem(s.pk_participant.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }
        private IList<DDLListItem> organizationOnlyList()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_participantTypes.Where(w => w.IsOrg.Contains("Y")).
                    Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => new DDLListItem(s.pk_participant.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }
        private IList<DDLListItem> participantAndOrganizationList()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_participantTypes.Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => new DDLListItem(s.pk_participant.ToString(), s.fullname_LF_dnd.TrimEnd()));
                return a.ToList();
            }
        }

        private IList<DDLListItem> taxParcelOwner()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.taxParcels.Distinct((x, y) => x.ownerStr_dnd == y.ownerStr_dnd).OrderBy(o => o.ownerStr_dnd).
                    Select(s => new DDLListItem(s.pk_taxParcel.ToString(), s.ownerStr_dnd.TrimEnd()));
                return a.ToList();
            }
        }

        

    }

     
}
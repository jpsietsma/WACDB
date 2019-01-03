using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Event;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACPT_ExpressFormDP
    /// </summary>
    public class WACPT_ExpressFormDP : WACDataProvider, IWACDataProvider<Participant>
    {
       
        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            try
            {
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    int pk_participant = WACGlobal_Methods.KeyAsInt(WACParameter.GetPrimaryKey(parms).ParmValue);
                    var e = wac.participants.Where(w => w.pk_participant == pk_participant).Select(s =>
                        new Participant(s.pk_participant, s.lname, s.fname, s.email, s.fk_gender_code, s.list_gender.gender, s.fk_ethnicity_code, 
                            s.list_ethnicity.ethnicity, s.fk_race_code, s.list_race.race, s.fk_diversityData_code, s.list_diversityData.dataSetVia,
                            s.fk_mailingStatus_code, s.list_mailingStatus.status, s.fk_property, s.fk_organization, s.organization.org, s.active, 
                            s.form_W9_signed_date, s.fk_regionWAC_code, s.list_regionWAC.regionWAC, s.fk_prefix_code, s.list_prefix.prefix,s.mname, 
                            s.nickname, s.fk_suffix_code, s.list_suffix.suffix, s.fullname_LF_dnd, s.fullname_FL_dnd, s.web, s.fk_dataReview_code, 
                            s.dataReview_note,s.fk_participant_split));
                    if (e.Any())
                        return e.ToList<Participant>();
                    else
                        return new List<Participant>();
                }
            }
            catch (Exception ex)
            {
                throw new WACEX_GeneralDatabaseException("Error loading Participant. " + ex.Message, -1);
            }
        }

        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            List<Participant> ilist = list as List<Participant>;
            int key = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
            return ilist.Where(w => w.pk_participant == key).ToList();
        }

        public override object PrimaryKeyValue(IList list)
        {
            List<Participant> li = list as List<Participant>;
            return li.First().pk_participant;
        }
        public override string PrimaryKeyName()
        {
            return Participant.PrimaryKeyName;
        }
        Participant IWACDataProvider<Participant>.Insert(Participant _item)
        {
            throw new NotImplementedException();
        }

        void IWACDataProvider<Participant>.Update(Participant _item)
        {
            throw new NotImplementedException();
        }

        void IWACDataProvider<Participant>.Delete(Participant _item)
        {
            throw new NotImplementedException();
        }


        public override IList GetNewItem()
        {
            throw new NotImplementedException();
        }


        public WACParameter GetPrimaryKey(Participant _item)
        {
            throw new NotImplementedException();
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
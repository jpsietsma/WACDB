using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Connectors;
using WAC_Exceptions;
using System.Collections;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACPT_ParticipantPropertyDP
    /// </summary>
    public class WACPT_ParticipantPropertyDP : WACDataProvider
    {
        public WACPT_ParticipantPropertyDP()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
                    int pk_participantProperty = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
                    var e = wac.participantProperties.Where(w => w.pk_participantProperty == pk_participantProperty).Select(s =>
                        new ParticipantProperty(s.pk_participantProperty, s.fk_participant, s.fk_participant_cc, s.fk_property,
                            s.master, s.created, s.created_by, s.modified, s.modified_by));
                    if (e.Any())
                       return e.ToList<ParticipantProperty>();
                    else
                        return new List<ParticipantProperty>();
                }
            }
            catch (Exception ex)
            {
                throw new WACEX_GeneralDatabaseException("Error loading Participant Properties. " + ex.Message, -1);
            }
        }
        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            List<ParticipantProperty> ilist = list as List<ParticipantProperty>;
            int key = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
            return ilist.Where(w => w.pk_participantProperty == key).ToList();
        }

        public override object PrimaryKeyValue(IList list)
        {
            List<ParticipantProperty> li = list as List<ParticipantProperty>;
            return li.First().pk_participantProperty;
        }

        public override string PrimaryKeyName()
        {
            return ParticipantProperty.PrimaryKeyName;
        }

        public override IList GetNewItem()
        {
            throw new NotImplementedException();
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
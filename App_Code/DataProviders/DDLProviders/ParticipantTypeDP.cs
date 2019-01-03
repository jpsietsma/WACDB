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
    /// Summary description for ParticipantTypeDP
    /// </summary>
    public class ParticipantTypeDP : DDLDataProvider
    {
        public ParticipantTypeDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_participantTypes.OrderBy(o => o.participantType).Select(s =>
                            new DDLListItem(s.pk_participantType_code, s.participantType));
                return x.ToList();
            }
            
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_participantType_code");
        }
    }
}
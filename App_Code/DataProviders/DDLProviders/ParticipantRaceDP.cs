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
    /// Summary description for ParticipantRaceDP
    /// </summary>
    public class ParticipantRaceDP : DDLDataProvider
    {
        public ParticipantRaceDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_races.OrderBy(o => o.race).Select(s =>
                            new DDLListItem(s.pk_race_code, s.race));
                return x.ToList();
            }
        }



        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_race_code");
        }
    }
}
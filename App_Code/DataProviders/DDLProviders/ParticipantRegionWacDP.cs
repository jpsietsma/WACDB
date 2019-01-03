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
    /// Summary description for ParticipantRegionWacDP
    /// </summary>
    public class ParticipantRegionWacDP : DDLDataProvider
    {
        public ParticipantRegionWacDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_regionWACs.OrderBy(o => o.regionWAC).Select(s =>
                            new DDLListItem(s.pk_regionWAC_code, s.regionWAC));
                return x.ToList();
            }
        }



        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_regionWAC_code");
        }
    }
}
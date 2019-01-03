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
    /// Summary description for ParticipantPrefixDP
    /// </summary>
    public class ParticipantPrefixDP : DDLDataProvider
    {
        public ParticipantPrefixDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_prefixes.OrderBy(o => o.prefix).Select(s =>
                            new DDLListItem(s.pk_prefix_code, s.prefix));
                return x.ToList();
            }
        }


        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_prefix_code");
        }
    }
}
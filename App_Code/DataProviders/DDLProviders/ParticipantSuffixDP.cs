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
    /// Summary description for ParticipantSuffixDP
    /// </summary>
    public class ParticipantSuffixDP : DDLDataProvider
    {
        public ParticipantSuffixDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            //ddl.ListSource = wac.list_suffixes.OrderBy(o => o.suffix).Select(s => new { s.pk_suffix_code, s.suffix });
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_suffixes.OrderBy(o => o.suffix).Select(s =>
                            new DDLListItem(s.pk_suffix_code, s.suffix));
                return x.ToList();
            }
        }



        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_suffix_code");
        }
    }
}
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
    /// Summary description for ParticipantEthnicityDP
    /// </summary>
    public class ParticipantEthnicityDP : DDLDataProvider
    {
        public ParticipantEthnicityDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_ethnicities.OrderBy(o => o.ethnicity).Select(s =>
                            new DDLListItem(s.pk_ethnicity_code, s.ethnicity));
                return x.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_ethnicity_code");
        }
    }
}
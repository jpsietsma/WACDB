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
    /// Summary description for ParticipantGenderDP
    /// </summary>
    public class ParticipantGenderDP : DDLDataProvider
    {
        public ParticipantGenderDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_genders.OrderBy(o => o.gender).Select(s =>
                            new DDLListItem(s.pk_gender_code, s.gender));
                return x.ToList();
            }
        }



        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_gender_code");
        }
    }
}
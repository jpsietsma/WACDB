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
    /// Summary description for ParticipantMailingStatusDP
    /// </summary>
    public class ParticipantMailingStatusDP : DDLDataProvider
    {
        public ParticipantMailingStatusDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            //list_mailingStatus.OrderBy(o => o.status)
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_mailingStatus.OrderBy(o => o.status).Select(s =>
                            new DDLListItem(s.pk_mailingStatus_code, s.status));
                return x.ToList();
            }
        }



        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_mailingStatus_code");
        }
    }
}
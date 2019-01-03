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
    /// Summary description for ParticipantDiversityDataDP
    /// </summary>
    public class ParticipantDiversityDataDP : DDLDataProvider
    {
        public ParticipantDiversityDataDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_diversityDatas.OrderBy(o => o.dataSetVia).Select(s =>
                            new DDLListItem(s.pk_diversityData_code, s.dataSetVia));
                return x.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "fk_diversityData_code");
        }
    }
}
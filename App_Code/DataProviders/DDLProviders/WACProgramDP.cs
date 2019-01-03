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
    /// Summary description for WACProgramDP
    /// </summary>
    public class WACProgramDP : DDLDataProvider
    {
        public WACProgramDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_programWACs.OrderBy(o => o.program).Select(s =>
                            new DDLListItem(s.pk_programWAC_code, s.program));
                return x.ToList(); 
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<FarmBusiness>(item, "fk_programWAC_code");
        }
    }
}
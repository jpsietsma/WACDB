using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Event;
using WAC_Extensions;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACCountyDP
    /// </summary>
    public class WACCountyDP : DDLDataProvider
    {
        public WACCountyDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var c = wac.vw_taxParcel_jurisdictions.Distinct((x, y) => x.pk_list_countyNY == y.pk_list_countyNY).OrderBy(o => o.county).
                    Select(s => new DDLListItem(s.pk_list_countyNY.ToString(), s.county));
                return c.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<SWIS>(item, "county");
        }
    }
}
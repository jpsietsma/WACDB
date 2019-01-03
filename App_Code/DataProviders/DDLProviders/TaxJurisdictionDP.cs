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
    /// Summary description for TaxJurisdictionDP
    /// </summary>
    public class TaxJurisdictionDP : DDLDataProvider
    {
        public TaxJurisdictionDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            int _pkCounty = WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms, "pk_list_countyNY"));
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var c = wac.vw_taxParcel_jurisdictions.Where(w => w.pk_list_countyNY == _pkCounty).Distinct((x, y) => x.Jurisdiction == y.Jurisdiction).
                        OrderBy(o => o.Jurisdiction).Select(s => new DDLListItem(s.SWIS, s.Jurisdiction));
                return c.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<SWIS>(item, "Jurisdiction");
        }
    }
}
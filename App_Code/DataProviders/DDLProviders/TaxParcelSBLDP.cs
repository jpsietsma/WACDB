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
    /// Summary description for TaxParcelSBLDP
    /// </summary>
    public class TaxParcelSBLDP : DDLDataProvider
    {
        public TaxParcelSBLDP()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var c = wac.taxParcels.Where(w => w.SBL != null).Distinct((x, y) => x.SBL == y.SBL).OrderBy(o => o.SBL).
                    Select(s => new DDLListItem(s.pk_taxParcel.ToString(), s.SBL));
                return c.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<TaxParcel>(item, "SBL");
        }
    }
}
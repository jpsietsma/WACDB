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
    /// Summary description for TaxParcelIDDP
    /// </summary>
    public class TaxParcelIDDP : DDLDataProvider
    {
        public TaxParcelIDDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var c = wac.taxParcels.Where(w => w.taxParcelID != null).Distinct((x, y) => x.taxParcelID == y.taxParcelID).OrderBy(o => o.taxParcelID).
                    Select(s => new DDLListItem(s.pk_taxParcel.ToString(), s.taxParcelID));
                return c.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<TaxParcel>(item, "taxParcelID");
        }
    }
}
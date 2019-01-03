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
    /// Summary description for TaxParcelPickerIDDP
    /// </summary>
    public class TaxParcelPickerIDDP : DDLDataProvider
    {
        public TaxParcelPickerIDDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            string _swis = (string)WACParameter.GetParameterValue(parms, "swis");
            bool newTPOnly = (bool)WACParameter.GetParameterValue(parms, "NewTaxParcelsOnly");
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                //var c = wac.vw_taxParcel_DEP_alls.Where(w => w.swis == _swis).Distinct((x, y) => x.SBL == y.SBL).OrderBy(o => o.PRINT_KEY).
                //    Select(s => new DDLListItem(s.SBL, s.PRINT_KEY));
                //return c.ToList();
                var c = wac.vw_taxParcel_DEP_alls.Where(w => w.SWIS == _swis).Distinct((x, y) => x.SBL == y.SBL).OrderBy(o => o.PRINT_KEY).
                    Select(s => new DDLListItem(s.SBL, s.PRINT_KEY));
                var d = wac.taxParcels.Where(w => w.fk_list_swis == _swis).Distinct((x, y) => x.SBL == y.SBL).OrderBy(o => o.taxParcelID).
                    Select(s => new DDLListItem(s.SBL, s.taxParcelID));
                if (newTPOnly)
                {
                    var minus = c.ToList().Except(d.ToList(), new DDLListItemComparer());
                    return minus.OrderBy(o => o.DataTextField).ToList();
                }
                else
                {
                    var union = c.ToList().Union(d.ToList(), new DDLListItemComparer()).
                        Distinct((x, y) => x.DataValueField == y.DataValueField);
                    return union.OrderBy(o => o.DataTextField).ToList();
                }
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<TaxParcel>(item, "taxParcelID");
        }
    }
}
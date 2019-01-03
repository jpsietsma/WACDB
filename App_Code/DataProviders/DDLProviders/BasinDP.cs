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
    /// Summary description for BasinDP
    /// </summary>
    public class BasinDP : DDLDataProvider
    {
        public BasinDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            int fk_County = WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms, "pk_County"));
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                if (fk_County > 0)
                {
                    
                    var x = wac.list_basins.Where(w => w.list_countyNYBasins.Any(a => a.fk_list_countyNY == fk_County)).OrderBy(o => o.basin).Select(s =>
                                new DDLListItem(s.pk_basin_code, s.basin));
                    return x.ToList();
                }
                else 
                {
                    var x = wac.list_basins.OrderBy(o => o.basin).Select(s =>
                                new DDLListItem(s.pk_basin_code, s.basin));
                    return x.ToList();
                }
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<FarmBusiness>(item, "fk_basin_code");
        }
    }
}
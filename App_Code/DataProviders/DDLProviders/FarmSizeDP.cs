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
    /// Summary description for FarmSizeDP
    /// </summary>
    public class FarmSizeDP : DDLDataProvider
    {
        public FarmSizeDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_farmSizes.OrderBy(o => o.farmSize).Select(s =>
                            new DDLListItem(s.pk_farmSize_code, s.farmSize));
                return x.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<FarmBusiness>(item, "fk_farmSize_code");
        }
    }
}
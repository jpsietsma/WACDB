using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Event;
using System.Text;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for FarmIDFarmNameDP
    /// </summary>
    public class FarmIDFarmNameDP : DDLDataProvider
    {
        public FarmIDFarmNameDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.farmBusinesses.Where(w => w.farmID != null).OrderBy(o => o.farmID).Select(s =>
                                new DDLListItem(s.pk_farmBusiness.ToString(),
                                    FarmIDBuildUpMax75(s.farmID, s.farm_name, s.ownerStr_dnd, true, true)));                                
                return x.ToList();
            }
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<FarmBusiness>(item, "sold_farm");
        }
        public string FarmIDBuildUpMax75(object oFarmID, object oFarmName, object oFarmOwner, bool bShowFarmName, bool bShowFarmOwner)
        {
            StringBuilder sb = new StringBuilder();
            if (oFarmID != null)
            {
                sb.Append(oFarmID);
                sb.Append(" ");
            }
            if (oFarmName != null && bShowFarmName)
            {
                sb.Append(oFarmName);
                sb.Append(" ");
            }
            if (oFarmOwner != null && bShowFarmOwner)
            {
                sb.Append(oFarmOwner);
                sb.Append(" ");
            }
            if (sb.Length < 1)
                return "No ID, Name, or Owner";
            else if (sb.Length > 75)
                return sb.ToString(0, 74);
            else
                return sb.ToString();
        }
    }
}
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
    /// Summary description for YesNoDP
    /// </summary>
    public class YesNoDP : DDLDataProvider
    {
        public YesNoDP() { }

        public override IList<DDLListItem> GetList(List<WACParameter> parms)
        {
            List<DDLListItem> list = new List<DDLListItem>();
            list.Add(new DDLListItem("Yes", "Y"));
            list.Add(new DDLListItem("No", "N"));
            return list as IList<DDLListItem>;
        }

        public override string GetSelected(object item)
        {
            return base.PropertyValue<Participant>(item, "active");
        }
    }
}
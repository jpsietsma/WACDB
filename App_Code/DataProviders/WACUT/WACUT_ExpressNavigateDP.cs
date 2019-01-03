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
    public class WACUT_ExpressNavigateDP : WACDataProvider
    {
        public WACUT_ExpressNavigateDP()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            throw new NotImplementedException();
        }

        public override object PrimaryKeyValue(IList list)
        {
            throw new NotImplementedException();
        }

        public override string PrimaryKeyName()
        {
            throw new NotImplementedException();
        }

        public override IList GetNewItem()
        {
            throw new NotImplementedException();
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
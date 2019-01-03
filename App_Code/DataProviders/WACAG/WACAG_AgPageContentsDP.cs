using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Extensions;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACAG_AgPageContentsDP
    /// </summary>
    public class WACAG_AgPageContentsDP : WACDataProvider
    {
        public WACAG_AgPageContentsDP() { }

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

        public override IList GetNewItem()
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

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
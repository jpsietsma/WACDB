using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Extensions;
using WAC_Event;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACPT_ParticipantListSearchDP
    /// </summary>
    public class WACPT_ParticipantListSearchDP : WACDataProvider
    {
        public WACPT_ParticipantListSearchDP() { }

        public DDLDataProvider PartOrgDP { get; set; }

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
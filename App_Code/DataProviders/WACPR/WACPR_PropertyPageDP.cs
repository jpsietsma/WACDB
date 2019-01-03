using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Connectors;
using WAC_Exceptions;
using System.Collections;
/// <summary>
/// Summary description for WACPR_PropertyPageDP
/// </summary>
namespace WAC_DataProviders
{
    public class WACPR_PropertyPageDP : WACDataProvider
    {
        public WACPR_PropertyPageDP() { }
        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            throw new NotImplementedException();
        }
        public override IList GetFilteredList(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        //public override IList GetItem(object key)
        //{
        //    throw new NotImplementedException();
        //}
        //public override IList GetItem()
        //{
        //    throw new NotImplementedException();
        //}
        //public override void Load()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Load(object key)
        //{
        //    throw new NotImplementedException();
        //}


        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        //public override IList GetList(object[] keys)
        //{
        //    throw new NotImplementedException();
        //}

        //public override object GetMasterPrimaryKey(IList list, string property)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IList GetListWithParms(List<WACParameter> Parms)
        //{
        //    throw new NotImplementedException();
        //}

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
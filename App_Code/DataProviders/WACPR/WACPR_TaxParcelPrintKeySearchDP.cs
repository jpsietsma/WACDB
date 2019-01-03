using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Concurrent;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Extensions;
using WAC_Extensions;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelPrintKeySearchDP
    /// </summary>
    public class WACPR_TaxParcelPrintKeySearchDP : WACDataProvider, IWACDataProvider<TaxParcel>
    {
        public WACPR_TaxParcelPrintKeySearchDP() { }

        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                string _partialPrintKey = WACParameter.GetParameterValue(parms, "partialPrintKey") as string;
                try
                {
                    var a = wac.taxParcels.Where(w => w.taxParcelID.StartsWith(_partialPrintKey)).OrderBy(o => o.taxParcelID).Select(s =>
                        new WAC_DataObjects.TaxParcel(s.pk_taxParcel, s.fk_list_swis, s.taxParcelID, s.note, s.SBL, s.ownerStr_dnd, s.acreage, s.retired,
                             s.list_swi.county, s.list_swi.jurisdiction, s.created, s.created_by, s.modified, s.modified_by));
                    return a.ToList<WAC_DataObjects.TaxParcel>();
                }
                catch (Exception e) { WACAlert.Show(e.Message, 0); }
            }
            return null;
        }
     
        public override object PrimaryKeyValue(IList list)
        {
            List<TaxParcel> li = list as List<TaxParcel>;
            return li.First().pk_taxParcel;

        }
        public override string PrimaryKeyName()
        {
            return TaxParcel.PrimaryKeyName;
        }

        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            throw new NotImplementedException();
        }

        public override IList GetNewItem()
        {
            throw new NotImplementedException();
        }

        public TaxParcel Insert(TaxParcel _item)
        {
            throw new NotImplementedException();
        }

        public void Update(TaxParcel _item)
        {
            throw new NotImplementedException();
        }

        public void Delete(TaxParcel _item)
        {
            throw new NotImplementedException();
        }


        public WACParameter GetPrimaryKey(TaxParcel _item)
        {
            return new WACParameter(TaxParcel.PrimaryKeyName, _item.pk_taxParcel, WACParameter.ParameterType.PrimaryKey);
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
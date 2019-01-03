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
/// <summary>
/// Summary description for WACPR_TaxParcelPickerDP
/// </summary>
namespace WAC_DataProviders
{
    public class WACPR_TaxParcelPickerDP : WACDataProvider, IWACDataProvider<TaxParcel>
    {
        public WACPR_TaxParcelPickerDP() {}
        public override IList GetFilteredList(List<WACParameter> parms)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                string _county = WACParameter.GetParameterValue(parms, "county") as string;
                string _jurisdiction = WACParameter.GetParameterValue(parms, "jurisdiction") as string;
                try
                {
                    var a = wac.taxParcels.Where(w => w.list_swi.county == _county && w.list_swi.jurisdiction == _jurisdiction).OrderBy(o => o.taxParcelID).Select(s =>
                        new WAC_DataObjects.TaxParcel(s.pk_taxParcel, s.fk_list_swis, s.taxParcelID, s.list_swi.county, s.list_swi.jurisdiction, s.retired));
                    return a.ToList<WAC_DataObjects.TaxParcel>();
                }
                catch (Exception e) { WACAlert.Show(e.Message, 0); }
            }
            return null;
        }
        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            List<TaxParcel> ilist = list as List<TaxParcel>;
            int key = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
            return ilist.Where(w => w.pk_taxParcel == key).ToList();
        }

     
        public TaxParcel Insert(TaxParcel _tp)
        {
            throw new NotImplementedException();
        }
        public void Update(TaxParcel _tp)
        {
            throw new NotImplementedException();
        }
        public void Delete(TaxParcel _tp)
        {
            throw new NotImplementedException();
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
        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        public override IList GetNewItem()
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
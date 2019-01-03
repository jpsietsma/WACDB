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
    /// Summary description for WACPR_TaxParcelOwnerContainerDP
    /// </summary>
    public class WACPR_TaxParcelOwnerContainerDP : WACDataProvider, IWACDataProvider<TaxParcelOwner>
    {
        public WACPR_TaxParcelOwnerContainerDP() { }

        public override IList GetList()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    var a = wac.vw_taxParcel_owners.OrderBy(o => o.taxParcelID).Select(s =>
                        new WAC_DataObjects.TaxParcelOwner(s.pk_taxParcelOwner,s.pk_taxParcel,s.pk_participant,s.fullname_LF_dnd, s.fullname_FL_dnd,
                            s.fk_list_swis,s.taxParcelID,s.SBL,s.acreage,s.retired,s.note,s.active,s.county,
                            s.pk_list_countyNY,s.created_by,s.created));
                    return a.ToList<WAC_DataObjects.TaxParcelOwner>();
                }
                catch (Exception e) { WACAlert.Show(e.Message, 0); }
            }
            return null;
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            try
            {
                IList<TaxParcelOwner> x = GetList() as IList<TaxParcelOwner>;
                return x.Where<TaxParcelOwner>(FuncWhere<TaxParcelOwner>(parms)).ToList();
            }
            catch (Exception e) { WACAlert.Show(e.Message, 0); }
            return null;
        }

        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            if (list == null)
                list = GetFilteredList(parms);
            List<TaxParcelOwner> ilist = list as List<TaxParcelOwner>;
            return ilist.Where(w => w.pk_taxParcelOwner == WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms,
                TaxParcelOwner.PrimaryKeyName))).ToList();
        }

        public override IList GetNewItem()
        {
            TaxParcelOwner tp = new TaxParcelOwner();
            IList list = new List<TaxParcelOwner>();
            list.Add(tp);
            return list;
        }
        public override object PrimaryKeyValue(IList list)
        {
            List<TaxParcelOwner> li = list as List<TaxParcelOwner>;
            return li.First().pk_taxParcelOwner;
        }

        public override string PrimaryKeyName()
        {
            return TaxParcelOwner.PrimaryKeyName;
        }
        public WACParameter GetPrimaryKey(TaxParcelOwner _item)
        {
            return new WACParameter(TaxParcelOwner.PrimaryKeyName, _item.pk_taxParcelOwner, WACParameter.ParameterType.PrimaryKey);
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            IEnumerable<TaxParcelOwner> sorted = null;
            IList<TaxParcelOwner> iList = _iList as IList<TaxParcelOwner>;
            sorted = iList.OrderBy((a, b) => TaxParcelOwner.CompareByField(a, b, sortExpression));
            if (sortDirection.ToUpper().Contains("ASC"))
                return sorted.ToList<TaxParcelOwner>();
            else
                return sorted.Reverse().ToList();
        }

        public TaxParcelOwner Insert(TaxParcelOwner _item)
        {
            int iCode = 0;
            int? pk = null;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                iCode = wac.taxParcelOwner_add(_item.fk_taxParcel, _item.fk_participant, _item.note, _item.active, _item.created_by, ref pk);
                if (iCode == 0)
                    _item.pk_taxParcelOwner = pk.Value;
                else
                    throw new WACEX_GeneralDatabaseException("Insert TaxParcelOwner returned database error. ", iCode);
            }
            return _item;
        }

        public void Update(TaxParcelOwner _item)
        {
            int iCode = 0;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {

                iCode = wac.taxParcelOwner_update(_item.pk_taxParcelOwner, _item.fk_participant, _item.note, _item.active, _item.modified_by);
                if (iCode != 0)
                    throw new WACEX_GeneralDatabaseException("Update TaxParcelOwner returned database error. ", iCode);
            }
            _item = null;
        }

        public void Delete(TaxParcelOwner _item)
        {
            int iCode = 0;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                iCode = wac.taxParcelOwner_delete(_item.pk_taxParcelOwner, _item.modified_by);
                if (iCode != 0)
                    throw new WACEX_GeneralDatabaseException("Delete TaxParcelOwner returned an error. ", iCode);
            }
            _item = null;
        }
       
    }
}
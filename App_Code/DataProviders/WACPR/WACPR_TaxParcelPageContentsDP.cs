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
    /// Summary description for WACPR_TaxParcelPageContentsDP
    /// </summary>
    public class WACPR_TaxParcelPageContentsDP : WACDataProvider, IWACDataProvider<TaxParcel>
    {
        public WACPR_TaxParcelPageContentsDP() { }

        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            if (list == null)
                list = GetFilteredList(parms);
            List<TaxParcel> ilist = list as List<TaxParcel>;
            return ilist.Where(w => w.pk_taxParcel == WACGlobal_Methods.KeyAsInt(WACParameter.GetParameterValue(parms,
                WACParameter.ParameterType.SelectedKey))).ToList();
        }
        public override IList GetList()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    var a = wac.taxParcels.OrderBy(o => o.taxParcelID).Select(s =>
                        new WAC_DataObjects.TaxParcel(s.pk_taxParcel, s.fk_list_swis, s.taxParcelID, s.note, s.SBL, s.ownerStr_dnd, s.acreage, s.retired,
                             s.list_swi.county, s.list_swi.jurisdiction, s.created, s.created_by, s.modified, s.modified_by));
                    return a.ToList<WAC_DataObjects.TaxParcel>();
                }
                catch (Exception e) { WACAlert.Show(e.Message, 0); }
            }
            return null;
        }
        public override IList GetFilteredList(List<WACParameter> parms)
        {
            try
            {
                IList<TaxParcel> x = GetList() as IList<TaxParcel>;
                PrimaryKeyVerify(ref parms);
                string _partialPrintKey = WACParameter.GetParameterValue(parms, "partialPrintKey") as string;
                string _participant = WACParameter.GetParameterValue(parms, "participant") as string;
                if (!string.IsNullOrEmpty(_partialPrintKey))
                {
                    var a = x.Where(w => w.taxParcelID.StartsWith(_partialPrintKey)).OrderBy(o => o.taxParcelID).Select(s => s);
                    return a.ToList<WAC_DataObjects.TaxParcel>();
                }
                else if (!string.IsNullOrEmpty(_participant))
                {
                    var a = x.Where(w => !string.IsNullOrEmpty(w.ownerStr_dnd) && w.ownerStr_dnd.Contains(_participant)).OrderBy(o => o.taxParcelID).Select(s => s);
                    return a.ToList<WAC_DataObjects.TaxParcel>();
                }
                else
                    return x.Where<TaxParcel>(FuncWhere<TaxParcel>(parms)).ToList();
            }
            catch (Exception e) { WACAlert.Show(e.Message, 0); }
            return null;
        }

        
        public WACParameter GetPrimaryKey(TaxParcel _item)
        {
            return new WACParameter(TaxParcel.PrimaryKeyName, _item.pk_taxParcel, WACParameter.ParameterType.PrimaryKey);
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

        public override IList GetNewItem()
        {
            TaxParcel tp = new TaxParcel();
            IList list = new List<TaxParcel>();
            list.Add(tp);
            return list;
        }
        public TaxParcel Insert(TaxParcel e)
        {
            int iCode = 0;
            int? pk = null;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var v = wac.taxParcel_add(e.Swis.pk_list_swis, e.taxParcelID, e.note, e.created_by, ref pk);
                iCode = (int)v.ReturnValue;
                if (iCode == 0)
                    e.pk_taxParcel = pk.Value;
                else
                    throw new WACEX_GeneralDatabaseException("Insert TaxParcel returned database error. ", iCode);
            }
            return e;
        }

        public void Update(TaxParcel e)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                wac.taxParcel_update(e.pk_taxParcel, e.note, e.modified_by);
            }
            e = null;
        }

        public void Delete(TaxParcel e)
        {
            int iCode = 0;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                iCode = wac.taxParcel_delete(e.pk_taxParcel, e.modified_by);
                if (iCode != 0)
                    throw new WACEX_GeneralDatabaseException("Delete TaxParcel returned an error. ", iCode);
            }
            e = null;
        }
       

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            IEnumerable<TaxParcel> sorted = null;
            IList<TaxParcel> iList = _iList as IList<TaxParcel>;
            sorted = iList.OrderBy((a, b) => TaxParcel.CompareByField(a, b, sortExpression));
            if (sortDirection.ToUpper().Contains("ASC"))
                return sorted.ToList<TaxParcel>();
            else
                return sorted.Reverse().ToList();
        }

       
    }
}
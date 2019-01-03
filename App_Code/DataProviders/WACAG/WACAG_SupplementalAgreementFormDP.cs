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
    /// Summary description for WACAG_SupplementalAgreementFormDP
    /// </summary>
    public class WACAG_SupplementalAgreementFormDP : WACDataProvider, IWACDataProvider<SupplementalAgreement>
    {
        public WACAG_SupplementalAgreementFormDP() { }

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
            List<SupplementalAgreement> ilist = list as List<SupplementalAgreement>;
            int key = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
            return ilist.Where(w => w.pk_supplementalAgreement == key).ToList(); 
        }

        public override IList GetNewItem()
        {
            SupplementalAgreement sa = new SupplementalAgreement();
            IList list = new List<SupplementalAgreement>();
            list.Add(sa);
            return list;
        }

        public override object PrimaryKeyValue(IList list)
        {
            List<SupplementalAgreement> li = list as List<SupplementalAgreement>;
            return li.First().pk_supplementalAgreement;
        }

        public override string PrimaryKeyName()
        {
            return "pk_supplementalAgreement";
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }

        public SupplementalAgreement Insert(SupplementalAgreement _item)
        {
            int iCode = 0;
            int? pk = null;

            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {

                iCode = wac.supplementalAgreement_add(_item.agreement_date, _item.swis, _item.printKey, _item.created_by, ref pk);
                if (iCode == 0)
                    _item.pk_supplementalAgreement = pk.Value;
                else
                    throw new WACEX_GeneralDatabaseException("Insert Supplemental Agreement returned database error. ", iCode);
            }
            return _item;
        }

        public void Update(SupplementalAgreement _item)
        {
            throw new NotImplementedException();
        }

        public void Delete(SupplementalAgreement _item)
        {
            throw new NotImplementedException();
        }

        public WACParameter GetPrimaryKey(SupplementalAgreement _item)
        {
            return new WACParameter(PrimaryKeyName(), _item.pk_supplementalAgreement, WACParameter.ParameterType.PrimaryKey);
        }
    }
}
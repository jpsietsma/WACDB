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
    /// Summary description for WACAG_FarmBusinessFormDP
    /// </summary>
    public class WACAG_FarmBusinessFormDP : WACDataProvider, IWACDataProvider<FarmBusiness>
    {
        public WACAG_FarmBusinessFormDP() { }

        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }
        public override IList GetNewItem()
        {
            FarmBusiness fb = new FarmBusiness();
            IList list = new List<FarmBusiness>();
            list.Add(fb);
            return list;
        }
        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            List<FarmBusiness> ilist = list as List<FarmBusiness>;
            int key = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
            return ilist.Where(w => w.pk_farmBusiness == key).ToList();         
        }
        public override object PrimaryKeyValue(IList list)
        {
            List<FarmBusiness> li = list as List<FarmBusiness>;
            return li.First().pk_farmBusiness;
        }
        public override string PrimaryKeyName()
        {
            return FarmBusiness.PrimaryKeyName;
        }
        
        public FarmBusiness Insert(FarmBusiness _item)
        {
            int iCode = 0;
            int? pk = null;
        
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                iCode = wac.farm_add_express(_item.swis, _item.printKey, _item.fk_participantOwner, 
                    _item.fk_participantOperator, _item.fk_basin_code, _item.fk_farmSize_code,
                    _item.fk_programWAC_code, _item.farm_name, _item.sold_farm, _item.GenerateID, "Y", _item.created_by, ref pk);
         
                if (iCode == 0)
                    _item.pk_farmBusiness = pk.Value;
                else
                    throw new WACEX_GeneralDatabaseException("Insert Farm Business returned database error. ", iCode);
            }
            return _item;
        }

        public void Update(FarmBusiness _item)
        {
            throw new NotImplementedException();
        }

        public void Delete(FarmBusiness _item)
        {
            throw new NotImplementedException();
        }


        public WACParameter GetPrimaryKey(FarmBusiness _item)
        {
            return new WACParameter(FarmBusiness.PrimaryKeyName, _item.pk_farmBusiness, WACParameter.ParameterType.PrimaryKey);
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
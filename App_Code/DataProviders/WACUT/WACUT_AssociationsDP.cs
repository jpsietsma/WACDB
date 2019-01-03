using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Event;


/// <summary>
/// Summary description for WACUT_AssociationsDP
/// </summary>
namespace WAC_DataProviders
{
    public class WACUT_AssociationsDP : WACDataProvider
    {
        private IList associations = null;
        private int primaryKey;

        public WACUT_AssociationsDP() { }

        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        private IList GetList(object key, AssociationLoader _loader)
        {
            primaryKey = WACGlobal_Methods.KeyAsInt(key);
            _loader(primaryKey);
            return associations;
        }
        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            throw new NotImplementedException();
        }
       
        public delegate void AssociationLoader(int _pk_);
        public void LoadPartcipantAssociations(int _pk)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_associations.Where(w => w.PK == _pk).Select(s => s).
                    OrderBy(o => o.source).ThenBy(t => t.Label);
                associations = a.ToList();
            }
        }
        public void LoadPropertyAssociations(int _pk)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_property_associations.Where(w => w.PK == _pk).Select(s => s).
                    OrderBy(o => o.source).ThenBy(t => t.Label);
                associations = a.ToList();
            }
        }
        public void LoadOrganizationAssociations(int _pk)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_organization_associations.Where(w => w.PK == _pk).Select(s => s).
                    OrderBy(o => o.source).ThenBy(t => t.Label);
                associations = a.ToList();
            }
        }
        public void LoadTaxParcelAssociations(int _pk)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_taxParcel_associations.Where(w => w.PK == _pk).Select(s => s).
                    OrderBy(o => o.source).ThenBy(t => t.Label);
                associations = a.ToList();
            }
        }
        public override IList GetFilteredList(List<WACParameter> parms)
        {
            WACEnumerations.AssociationTypes assocType = (WACEnumerations.AssociationTypes)WACParameter.GetParameterValue(parms, WACParameter.ParameterType.AssociationType);
            switch (assocType)
            {
                case WACEnumerations.AssociationTypes.Participant:
                    return GetList(WACParameter.GetParameterValue(parms, WACParameter.ParameterType.SelectedKey), LoadPartcipantAssociations);
                case WACEnumerations.AssociationTypes.Property:
                    return GetList(WACParameter.GetParameterValue(parms, WACParameter.ParameterType.SelectedKey), LoadPropertyAssociations);
                case WACEnumerations.AssociationTypes.Organization:
                    return GetList(WACParameter.GetParameterValue(parms, WACParameter.ParameterType.SelectedKey), LoadOrganizationAssociations);
                case WACEnumerations.AssociationTypes.TaxParcel:
                    return GetList(WACParameter.GetParameterValue(parms, WACParameter.ParameterType.SelectedKey), LoadTaxParcelAssociations);
                default:
                    return null;
            }
        }

        public override object PrimaryKeyValue(IList list)
        {
            try
            {
                List<Association> li = list as List<Association>;
                return li.First().PK;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public override string PrimaryKeyName()
        {
            return Association.PrimaryKeyName;
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
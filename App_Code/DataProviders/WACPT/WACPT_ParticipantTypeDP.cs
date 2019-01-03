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
using WAC_Extensions;
using System.Collections;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACPT_ParticipantTypeDP
    /// </summary>
    public class WACPT_ParticipantTypeDP : WACDataProvider
    {
        public WACPT_ParticipantTypeDP()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public IList<DDLListItem> GetParticipantTypeList(List<WACParameter> parms, object item)
        {
            try
            {
                IList<DDLListItem> _items;
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    var c = wac.list_participantTypes.Select(s => new DDLListItem(s.pk_participantType_code, s.participantType));
                    _items = c.ToList();
                    string selectedValue = null;
                    if (item != null)
                        selectedValue = ((ParticipantType)item).fk_participantType_code;
                    if (!string.IsNullOrEmpty(selectedValue))
                    {
                        foreach (DDLListItem ddls in _items)
                        {
                            if (selectedValue.Contains(ddls.DataValueField))
                            {
                                ddls.SelectedValue = true;
                                break;
                            }
                        }
                    }
                    return _items;
                }
            }
            catch (Exception ex)
            {
                throw new WACEX_GeneralDatabaseException("Error loading Participant Type list. " + ex.Message, -1);
            }
        }
        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            try
            {
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    int fk_participant = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
                    var e = wac.participantTypes.Where(w => w.fk_participant == fk_participant).Select(s =>
                        new ParticipantType(s.pk_participantType, s.fk_participant, s.fk_participantType_code, s.created, s.created_by, 
                            s.modified, s.modified_by));
                    if (e.Any())
                        return e.ToList<ParticipantType>();
                    else
                        return new List<ParticipantType>();
                }
            }
            catch (Exception ex)
            {
                throw new WACEX_GeneralDatabaseException("Error loading Participant Types. " + ex.Message, -1);
            }
        }

        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            throw new NotImplementedException();
        }

        public override object PrimaryKeyValue(IList list)
        {
            List<ParticipantType> li = list as List<ParticipantType>;
            return li.First().pk_participantType;
        }

        public override string PrimaryKeyName()
        {
            return ParticipantType.PrimaryKeyName;
        }
        public ParticipantType Insert(ParticipantType _item)
        {
            int iCode = 0;
            int? pk = null;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
            //    iCode = wac.ParticipantType_add(_item.fk_easement, _item.fk_statusEasement_code, _item.date, _item.created_by, ref pk);
            //    if (iCode == 0)
            //        _item.pk_ParticipantType = pk.Value;
            //    else
            //        throw new WACEX_GeneralDatabaseException("Insert Easement status returned database error. ", iCode);
            }
            return _item;
        }
        public void Update(ParticipantType _item)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                //var x = wac.ParticipantType.Where(w => w.pk_ParticipantType == _item.pk_ParticipantType).Select(s => s).Single();
                //x.fk_statusEasement_code = _item.fk_statusEasement_code;
                //x.date = (DateTime)_item.date;
                //x.note = _item.note;
                //wac.SubmitChanges();
            }
            _item = null;
        }
        public void Delete(ParticipantType _item)
        {
            int iCode = 0;
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                //iCode = wac.ParticipantType_delete(_item.pk_ParticipantType, _item.modified_by);
                iCode = wac.participantType_delete(_item.pk_participantType, _item.modified_by);
                if (iCode != 0)
                    throw new WACEX_GeneralDatabaseException("Delete Participant Type returned an error. ", iCode);
            }
            _item = null;
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
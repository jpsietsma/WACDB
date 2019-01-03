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
/// Summary description for WACPR_TaxParcelPageContentsDP
/// </summary>
/// 
namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACPT_ParticipantCommunicationsDP
    /// </summary>
    public class WACPT_ParticipantCommunicationsDP : WACDataProvider
    {
        public WACPT_ParticipantCommunicationsDP()
        {
            //
            // TODO: Add constructor logic here
            //
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
                    int pk_participantCommunication = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
                    var e = wac.participantCommunications.Where(w => w.pk_participantCommunication == pk_participantCommunication).Select(s =>
                        new ParticipantCommunication(s.pk_participantCommunication, s.fk_participant, s.fk_communication, s.fk_communicationType_code,
                            s.fk_communicationUsage_code, s.fk_organization, s.extension, s.note, s.source, s.created, s.created_by, s.modified, s.modified_by));
                    if (e.Any())
                        return e.ToList<ParticipantCommunication>();
                    else
                        return new List<ParticipantCommunication>();
                }
            }
            catch (Exception ex)
            {
                throw new WACEX_GeneralDatabaseException("Error loading ParticipantCommunication. " + ex.Message, -1);
            }
        }


        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            List<ParticipantCommunication> ilist = list as List<ParticipantCommunication>;
            int key = WACGlobal_Methods.KeyAsInt(WACParameter.GetSelectedKey(parms).ParmValue);
            return ilist.Where(w => w.pk_participantCommunication == key).ToList();
        }

        public override object PrimaryKeyValue(IList list)
        {
            List<ParticipantCommunication> li = list as List<ParticipantCommunication>;
            return li.First().pk_participantCommunication;
        }

        public override string PrimaryKeyName()
        {
            return ParticipantCommunication.PrimaryKeyName;
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
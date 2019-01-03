using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;


namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for ParticipantCommunication
    /// </summary>
    public class ParticipantCommunication : WACDataObject, IWACDataObject<ParticipantCommunication>
    {
        public const string PrimaryKeyName = "pk_participantCommunication";
        public int pk_participantCommunication { get; set; }
        public int fk_participant { get; set; }
        public int fk_communication { get; set; }
        public string fk_communicationType_code { get; set; }
        public string fk_communicationUsage_code { get; set; }
        public int? fk_organization { get; set; }
        public string extension { get; set; }
        public string note { get; set; }
        public string source { get; set; }

        public ParticipantCommunication() { }
        public ParticipantCommunication(int _pk,int _fk_participant, int _fk_communication, string _fk_communicationType_code, string _fk_communicationUsage_code,
            int? _fk_organization, string _extension, string _note, string _source, DateTime? _created, string _created_by, DateTime? _modified, string _modified_by )
        {
            pk_participantCommunication = _pk;
            fk_participant = _fk_participant;
            fk_communication = _fk_communication;
            fk_communicationType_code = _fk_communicationType_code;
            fk_communicationUsage_code = _fk_communicationUsage_code;
            fk_organization = _fk_organization;
            extension = _extension;
            note = _note;
            source = _source;
            created = _created;
            created_by = _created_by;
            modified = _modified;
            modified_by = _modified_by;
        }
        public ParticipantCommunication Clone()
        {
            return (ParticipantCommunication)this.MemberwiseClone();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
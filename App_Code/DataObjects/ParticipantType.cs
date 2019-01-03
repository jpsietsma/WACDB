using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;


namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for ParticipantType
    /// </summary>
    public class ParticipantType: WACDataObject, IWACDataObject<ParticipantType>
    {
        public const string PrimaryKeyName = "pk_participantType";
        public int pk_participantType { get; set; }
        public int fk_participant { get; set; }
        public string fk_participantType_code { get; set; }

        public ParticipantType() { }

        public ParticipantType(int _pkPT, int _fkParticipant, string _fkParticipantTypeCode,
            DateTime? _created, string _created_by, DateTime? _modified, string _modified_by)
        {
            pk_participantType = _pkPT;
            fk_participant = _fkParticipant;
            fk_participantType_code = _fkParticipantTypeCode;
            created = _created;
            created_by = _created_by;
            modified = _modified;
            modified_by = _modified_by;
        }
        public ParticipantType Clone()
        {
            return (ParticipantType)this.MemberwiseClone();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;


namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for ParticipantProperty
    /// </summary>
    public class ParticipantProperty : WACDataObject, IWACDataObject<ParticipantProperty>
    {
        public const string PrimaryKeyName = "pk_participantProperty";
        public int pk_participantProperty { get; set; }
        public int fk_participant { get; set; }
        public int? fk_participant_cc { get; set; }
        public int fk_property { get; set; }
        public string master { get; set; }

        public ParticipantProperty() { }


        public ParticipantProperty(int _pk, int _fkParticipant, int? _fkParticipantCC, int _fkProperty, string _master, 
            DateTime? _created, string _created_by, DateTime? _modified, string _modified_by)
        {
            pk_participantProperty = _pk;
            fk_participant = _fkParticipant;
            fk_participant_cc = _fkParticipantCC;
            fk_property = _fkProperty;
            master = _master;
            created = _created;
            created_by = _created_by;
            modified = _modified;
            modified_by = _modified_by;
        }
        public ParticipantProperty Clone()
        {
            return (ParticipantProperty)this.MemberwiseClone();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
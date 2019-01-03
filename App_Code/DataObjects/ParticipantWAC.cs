using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;

namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for ParticipantWAC
    /// </summary>
    public class ParticipantWAC : WACDataObject, IWACDataObject<ParticipantWAC>
    {
        public const string PrimaryKeyName = "pk_participantWAC";
        public int pk_participantWAC { get; set; }
        public string email { get; set; }
        public string Phone { get; set; }
        public string phone_ext { get; set; }
        public string PhoneCellStr { get; set; }
        public string Employee { get; set; }
        public string position { get; set; }

        public ParticipantWAC() { }

        public ParticipantWAC(int _pk, string _email, string _Phone, string _phone_ext, string _PhoneCellStr, string _Employee, string _position)
        {
            pk_participantWAC = _pk;
            email = _email;
            Phone = _Phone;
            phone_ext = _phone_ext;
            PhoneCellStr = _PhoneCellStr;
            Employee = _Employee;
            position = _position;
        }
        public ParticipantWAC Clone()
        {
            return (ParticipantWAC)this.MemberwiseClone();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
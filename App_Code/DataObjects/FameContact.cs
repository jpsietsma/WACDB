using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;

namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for FameContact
    /// </summary>
    public class FameContact: WACDataObject, IWACDataObject<FameContact>
    {
        public const string PrimaryKeyName = "pk_participantWAC";
        public int pk_participantWAC { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Employee { get; set; }
        public string Position { get; set; }

        public FameContact() { }

        public FameContact(int _pk, string _email, string _Phone, string _Employee, string _position)
        {
            pk_participantWAC = _pk;
            Email = _email;
            Phone = _Phone;
            Employee = _Employee;
            Position = _position;
        }
        public FameContact Clone()
        {
            return (FameContact)this.MemberwiseClone();
        }


        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
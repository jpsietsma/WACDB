using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
/// <summary>
/// Summary description for UserControlResultEventArgs
/// </summary>
namespace WAC_Event
{
    
    public class UserControlResultEventArgs : EventArgs
    {
        public List<WACParameter> Parms { get; set; }

        public UserControlResultEventArgs()
        {
            Parms = new List<WACParameter>();
        }
        public UserControlResultEventArgs(List<WACParameter> _parms)
        {
            Parms = _parms;
        }
    }
}
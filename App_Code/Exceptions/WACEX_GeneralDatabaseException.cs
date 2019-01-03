using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WACEX_GeneralDatabaseException
/// </summary>
namespace WAC_Exceptions
{
    public class WACEX_GeneralDatabaseException : Exception
    {
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public WACEX_GeneralDatabaseException(string _message, int _eCode)
        {
           ErrorText = _message;
           ErrorCode = _eCode;
        }
    }
}
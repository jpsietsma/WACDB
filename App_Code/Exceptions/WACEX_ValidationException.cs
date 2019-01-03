using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WACEX_ValidationException
/// </summary>
namespace WAC_Exceptions
{
    public class WACEX_ValidationException : Exception
    {
        public string ErrorText { get; set; }
        public WACEX_ValidationException(string _message)
        {
            ErrorText = _message;
        }
    }
}
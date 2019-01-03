using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WACEX_BmpCloneException
/// </summary>
public class WACEX_BmpCloneException : Exception
{
   
    public string ErrorText { get; set; }
    public WACEX_BmpCloneException(string _message)
	{
        ErrorText = _message;
	}
}
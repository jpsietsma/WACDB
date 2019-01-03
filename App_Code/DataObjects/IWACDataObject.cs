using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;

/// <summary>
/// Summary description for IWACDataObject
/// </summary>
namespace WAC_DataObjects
{
    public interface IWACDataObject<T>
    {
        T Clone();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using System.Collections;

/// <summary>
/// Summary description for IWACDataProvider
/// </summary>
namespace WAC_DataProviders
{
    public interface IWACDataProvider<T> 
    {      
        T Insert(T _item);
        void Update(T _item);
        void Delete(T _item);
        WACParameter GetPrimaryKey(T _item);
    }
}
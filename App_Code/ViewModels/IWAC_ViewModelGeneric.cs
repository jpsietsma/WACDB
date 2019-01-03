using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace WAC_ViewModels
{
    public interface IWAC_ViewModel<T> 
    {
        // T will be of type IWAC_DataProvider
        
        void Insert(T _addItem);
        void Update(T _updateItem);
        void Delete(T _deleteItem);
        T GetItem(object key);
        IList GetList(object key);
        IList GetList(object[] key);
    }
}

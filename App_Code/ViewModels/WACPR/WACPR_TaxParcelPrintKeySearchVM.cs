using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WAC_DataProviders;
using WAC_DataObjects;
using WAC_Event;
using WAC_Services;
using System.Collections;
using System.Web.UI;
/// <summary>
/// Summary description for WACPR_TaxParcelPickerVM
/// </summary>

namespace WAC_ViewModels
{
    /// <summary>
    /// Summary description for WACPR_TaxParcelPrintKeySearchVM
    /// </summary>
    public class WACPR_TaxParcelPrintKeySearchVM : WACUtilityControlViewModel
    {
        public WACPR_TaxParcelPrintKeySearchVM() { }

        public override void CustomAction(List<WACParameter> parms)
        {
            if (DataProvider != null)
            {
                Control filter = WACParameter.GetParameterValue(parms, "FilterControl") as Control;
                ServiceRequest sr = new ServiceRequest(filter);
                sr.ParmList = parms;
                sr.ParmList.Add(new WACParameter("dataprovider", DataProvider, WACParameter.ParameterType.DataProvider));
                sr.ServiceRequested = ServiceFactory.ServiceTypes.FilteredGridViewList;
                ServiceFactory.Instance.ServiceRequest(sr);
                sr = null;
            }
            else
                WACAlert.Show("WACPR_TaxParcelPrintKeySearchVM.CustomAction: DataProvider is null", 0);
          
        }

        public override string CustomString(List<WACParameter> parms)
        {
            throw new NotImplementedException();
        }

        public override void ContentStateChanged(Control _control, Enum e)
        {
            throw new NotImplementedException();
        }
    }
}
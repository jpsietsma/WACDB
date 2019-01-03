using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WAC_Event;
using WAC_ViewModels;
using WAC_UserControls;
using WAC_Connectors;
using WAC_Exceptions;
using WAC_Services;
using WAC_DataObjects;


namespace WAC_Containers
{
    /// <summary>
    /// Summary description for WACContainer
    /// </summary>
    public abstract class WACContainer : WACControl, IWACContainer
    {
        public abstract void InitControls(List<WACParameter> parms);
        //public void AdjustContentVisibility(List<WACControl> ContainedControls, FormView fv)
        //{
        //    if (fv != null)
        //    {
        //        switch (fv.CurrentMode)
        //        {
        //            case FormViewMode.Edit:
        //                foreach (WACControl c in ContainedControls)
        //                    if (!ServiceFactory.IsFormControl(c))
        //                        c.Visible = c.IsActiveUpdate;
        //                break;
        //            case FormViewMode.Insert:
        //                foreach (WACControl c in ContainedControls)
        //                    if (!ServiceFactory.IsFormControl(c))
        //                        c.Visible = c.IsActiveInsert;
        //                break;
        //            case FormViewMode.ReadOnly:
        //                foreach (WACControl c in ContainedControls)
        //                    if (!ServiceFactory.IsFormControl(c))
        //                        c.Visible = c.IsActiveReadOnly;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        foreach (WACControl c in ContainedControls)
        //            c.Visible = c.DefaultVisibility;
        //    }
        //}
        //public void AdjustContentVisibility(List<WACControl> ContainedControls, WACFormControl form)
        //{
        //    switch (form.CurrentState)
        //    {
        //        case WACFormControl.FormState.Closed:
        //            foreach (WACControl c in ContainedControls)
        //                c.Visible = c.DefaultVisibility;
        //            break;
        //        case WACFormControl.FormState.OpenView:
        //            foreach (WACControl c in ContainedControls)
        //                c.Visible = c.IsActiveReadOnly;
        //            break;
        //        case WACFormControl.FormState.OpenInsert:
        //            foreach (WACControl c in ContainedControls)
        //                c.Visible = c.IsActiveInsert;
        //            break;
        //        case WACFormControl.FormState.OpenUpdate:
        //            foreach (WACControl c in ContainedControls)
        //                c.Visible = c.IsActiveUpdate;
        //            break;
        //        default:                   
        //            break;
        //    }
        //}
    }
}
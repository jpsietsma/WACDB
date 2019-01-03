using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_UserControls;
using WAC_Containers;
using WAC_Connectors;
using WAC_Services;
using System.Collections.Concurrent;
using WAC_Services;

namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for WACControlInfo
    /// </summary>
    public class WACControlInfo
    {
        public bool IsContainer { get; set; }
        public string ContainedByID { get; set; }
        public string ControlID { get; set; }
        public ConcurrentDictionary<string, WACControlInfo> ContainedControls { get; set; }
        public WACControlInfo(bool _isContainer, string _containedByID, string _controlID)
        {
            IsContainer = _isContainer;
            ContainedByID = _containedByID;
            ControlID = _controlID;
            if (IsContainer)
                ContainedControls = new ConcurrentDictionary<string, WACControlInfo>();
        }

    }
}
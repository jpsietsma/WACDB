using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;

namespace WAC_DataObjects
{

    /// <summary>
    /// Summary description for WACEnumerations
    /// </summary>
    public static class WACEnumerations
    {
        public enum AssociationTypes { Participant, Property, Organization, TaxParcel };
        public enum WACSector { Communication, FarmBusiness, Organization, Participant, Property, Venue }
        public enum ControlType { Undefined, Container, FormControl, GridControl, ListControl, FilterControl, UtilityControl }
        public enum ContainerState { NestedContainerActive, NestedContainerClosed }
        public enum WACSectorCodes { A_ASR, A_BMP, A_NMP, A_OVER, A_AEM, A_WFP2, A_WFP3, A_WFP3_BMP, E_OVER, F_BMP, F_MAP, F_FMP, HOME, HR_OVER, 
                                     M_OVEREVENT, M_OVERPC, PART, TP }

        public static Dictionary<WACSectorCodes, string> SectorCodes = new Dictionary<WACSectorCodes, string> ()
        {
           { WACSectorCodes.A_OVER,"A_OVER"},
           { WACSectorCodes.A_ASR,"A_ASR"},
           { WACSectorCodes.A_BMP,"A_BMP"},
           { WACSectorCodes.A_NMP,"A_NMP"},
           { WACSectorCodes.A_AEM,"A_TIER1"},
           { WACSectorCodes.A_WFP2,"A_WFP2"},
           { WACSectorCodes.A_WFP3,"A_WFP3"},
           { WACSectorCodes.A_WFP3_BMP,"A_WFP3_BMP"},
           { WACSectorCodes.E_OVER,"E_OVER"},
           { WACSectorCodes.F_BMP,"F_BMP"},
           { WACSectorCodes.F_MAP,"F_MAP"},
           { WACSectorCodes.F_FMP,"F_FMP"},
           { WACSectorCodes.HOME,"H"},
           { WACSectorCodes.HR_OVER,"HR"},
           { WACSectorCodes.M_OVEREVENT,"M_OVEREVENT"},
           { WACSectorCodes.M_OVERPC, "M_OVERPC"},
           { WACSectorCodes.PART, "PART"},
           { WACSectorCodes.TP,"TP"}
        };
    }
}
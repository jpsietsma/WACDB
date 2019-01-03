using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Connectors;
using WAC_Exceptions;
using System.Collections;
using System.Web.UI;

namespace WAC_Validators
{
    /// <summary>
    /// Summary description for WACPT_ParticipantValidator
    /// </summary>
    public class WACPT_ParticipantValidator : WACValidator
    {
        public WACPT_ParticipantValidator()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public Participant ValidateInsert(List<WACParameter> parms)
        {
            Participant p = new Participant();
            foreach (WACParameter parm in parms)
            {
                switch (parm.ParmName)
                {
                    default:
                        break;
                }
            }
            return p;
        }
        public Participant ValidateUpdate(List<WACParameter> parms)
        {
            Participant p = new Participant();
            foreach (WACParameter parm in parms)
            {
                switch (parm.ParmName)
                {
                    default:
                        break;
                }
            }
            return p;
        }
    }
}
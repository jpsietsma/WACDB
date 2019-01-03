using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_DataProviders;
using WAC_Event;
using WAC_UserControls;
using WAC_ViewModels;
using WAC_Connectors;
using WAC_Exceptions;
using System.Collections;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACPT_ContactInfoListDP
    /// </summary>
    public class WACPT_ContactInfoListDP : WACDataProvider
    {
        public WACPT_ContactInfoListDP() { }

        public override IList GetList()
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    var a = wac.vw_FAME_contacts.OrderBy(o => o.contactFAME).ThenBy(t => t.Employee).Select(s =>
                        new FameContact(s.pk_participantWAC, s.Email, s.Phone, s.Employee, s.Position));
                    return a.ToList<FameContact>();
                }
                catch (Exception e) { WACAlert.Show(e.Message, 0); }
            }
            return null;
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            return GetList();
        }

        public override IList GetItem(List<WACParameter> parms, IList list)
        {
            throw new NotImplementedException();
        }

        public override IList GetNewItem()
        {
            throw new NotImplementedException();
        }

        public override object PrimaryKeyValue(IList list)
        {
            throw new NotImplementedException();
        }

        public override string PrimaryKeyName()
        {
            throw new NotImplementedException();
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
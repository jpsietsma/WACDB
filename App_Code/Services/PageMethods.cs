using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;

/// <summary>
/// Summary description for PageMethods
/// </summary>
namespace WAC_Services
{
    public class PageMethods
    {
        public PageMethods() { }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetParticipantOrgList(string prefixText, int count, string contextKey)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.vw_participant_participantTypes.Where(w => w.fullname_LF_dnd.StartsWith(prefixText)).
                    Distinct((x, y) => x.fullname_LF_dnd == y.fullname_LF_dnd).OrderBy(o => o.fullname_LF_dnd).
                    Select(s => s.fullname_LF_dnd.TrimEnd());
                return a.ToArray<string>();
            }
        }
    }
}
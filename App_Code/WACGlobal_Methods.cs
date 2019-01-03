using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using WAC_Extensions;
using WAC_DataObjects;
/// <summary>
/// Summary description for WACGlobal_Methods
/// 
/// </summary>
public class WACGlobal_Methods
{
    #region Reflection
    
    public static object ContainingObject(Page sourcePage, string filter)
    {
        MethodInfo[] methods;
        methods = sourcePage.GetType().GetMethods();
        foreach (var m in methods)
        {
            if (m.Name.StartsWith(filter))
                return sourcePage;
        }
        List<Control> controlList = new List<Control>();
        GetControlList<Control>(sourcePage.Controls, controlList);
        foreach (Control c in controlList)
        {
            methods = c.GetType().GetMethods();
            if (methods.Any())
            {
                foreach (var m in methods)
                {
                    if (m.Name.StartsWith(filter))
                        return c;
                }
            }
        }
        return null;
    }
    #endregion

    #region Primary & Foreign Key Methods

    public static int KeyAsInt(object o)
    {
        try
        {
            return Convert.ToInt32(o);
        }
        catch (Exception ex)
        {
            WACAlert.Show("Error converting key: " + ex.Message, 0);
            return -1;
        }
    }
    public static string KeyAsString(object o)
    {
        try
        {
            return Convert.ToString(o);
        }
        catch (Exception ex)
        {
            WACAlert.Show("Error converting key: " + ex.Message, 0);
            return string.Empty;
        }
    }
    #endregion
    #region Text
   
    public static string Truncate(object o, int tWidth)
    { // truncate a string to a set number of characters and append "..." to indicate there is more to it
        try
        {
            string s = o as string;
            tWidth = tWidth > s.Length ? s.Length : tWidth;
            return s.Substring(0, tWidth) + "...";
        }
        catch { return null; }
    }
    #endregion

    #region Control Search

    public static void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection) where T : Control
    {
        foreach (Control control in controlCollection)
        {
            if (control is T) 
                resultCollection.Add((T)control);
        }
    }
    public static void GetControlListAll<T>(ControlCollection controlCollection, List<T> resultCollection) where T : Control
    {
        foreach (Control control in controlCollection)
        {
            if (control is T)
                resultCollection.Add((T)control);

            if (control.HasControls())
                GetControlListAll(control.Controls, resultCollection);
        }
    }
    public T FindControl<T>(Page _page, string id) where T : Control
    {
       return FindControl<T>(_page, id);
    }

    public static T FindControl<T>(Control startingControl, string id) where T : Control
    {
        //T found = null;
        //foreach (Control activeControl in startingControl.Controls)
        IEnumerable<Control> _controls = EnumerateControlsRecursive(startingControl);
        var c = _controls.Where(w => w.ClientID.CompareTo(id) == 0).Select(s => s);
        if (c.Any())
            return (T)c.First();
        else
            return null;
        //foreach (Control activeControl in _controls)
        //{
        //    found = WACGlobal_Methods.FindControl<T>(activeControl);
        //    if (found == null)
        //    {
        //        found = FindControl<T>(activeControl, id);
        //    }
        //    if (found != null && string.Compare(id, found.ClientID, true) != 0)
        //    {
        //        break;
        //    }
        //}
        //return found;
    }
   
    public static IEnumerable<Control> AllControls(Control startingControl)
    {
        var result = new List<Control>();
        foreach (Control activeControl in startingControl.Controls)
        {          
            result.Add(activeControl);          
            if (activeControl.HasControls())
            {
                result.AddRange(AllControls(activeControl));
            }
        }
        return result;
    }
    public static IEnumerable<Control> EnumerateControlsRecursive(Control parent)
    {
        List<Control> ret = new List<Control>();
        foreach (Control c in parent.Controls)
        {
            ret.Add(c);
            ret.AddRange(EnumerateControlsRecursive(c));
        }
        return (IEnumerable<Control>)ret;
    }
    public static IEnumerable<T> FindControlsFlat<T>(Control startingControl) where T : Control
    {
        // find all controls of Type T in the current control collection
        var result = new List<T>();
        foreach (Control activeControl in startingControl.Controls)
        {
            if (activeControl is T)
            {
                result.Add((T)activeControl);
            }
        }
        return result;
    }
    public static IEnumerable<T> FindControls<T>(Control startingControl) where T : Control
	{
        // find all controls of Type T, recursively starting from "startingControl"
        IEnumerable<Control> _controls = EnumerateControlsRecursive(startingControl);
	    var result = new List<T>();
        foreach (Control activeControl in _controls )
	    {
            if (activeControl is T)
	        {
                result.Add((T)activeControl);
	        }
	    }
	    return result;
	}
    public static T FindControl<T>(Control startingControl) where T : Control
    {   // find the first control of Type T, recursively starting from "startingControl"
        T found = null;
        foreach (Control activeControl in startingControl.Controls)
        {
            found = activeControl as T;
            if (found == null)
            {
                found = FindControl<T>(activeControl);
            }
            if (found != null)
            {
                break;
            }
        }
        return found;
    }

    public static Control FindControlRecursive(Control Root, string Id)
    {
        if (Root.ID == Id)
            return Root;
        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = FindControlRecursive(Ctl, Id);
            if (FoundCtl != null)
                return FoundCtl;
        }
        return null;
    }
    public static Control FindOuterControl(Control _top, Control _control)
    {
        if (_top.ID == _control.ID)
            return _control;
        foreach (Control ctl in _top.Controls)
        {
            Control FoundCtl = FindOuterControl(ctl, _control);
            if (FoundCtl != null)
                return ctl;
        }
        return null;
    }
    #endregion

    #region Control Events
    public static void EventControl_Custom_SetOnClientClick_LinkButton(LinkButton lb, int? iPK1, int? iPK2, string sArea, string sAreaSector)
    {
        //try
        //{
            lb.OnClientClick = "nonModalWinDocumentUpload('" + iPK1 + "','" + iPK2 + "','" + sArea + "','" + sAreaSector + "'); return false;";
        //}
        //catch { }
    }

    public static void EventControl_Custom_CalendarChangeYear(FormView fv, DropDownList ddl)
    {
        try
        {
            Calendar cal = fv.FindControl("cal" + ddl.ToolTip) as Calendar;
            DateTime dt = Convert.ToDateTime("1/1/" + ddl.SelectedValue);
            cal.SelectedDate = dt;
            cal.VisibleDate = dt;
        }
        catch { WACAlert.Show("Error setting new date.", 0); }
    }

    public static void EventControl_Custom_CalendarClear(FormView fv, LinkButton lb)
    {
        try
        {
            Calendar cal = fv.FindControl("cal" + lb.CommandArgument) as Calendar;
            cal.SelectedDates.Clear();
            cal.VisibleDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DropDownList ddl = fv.FindControl("ddlYear" + lb.CommandArgument) as DropDownList;
            ddl.SelectedIndex = 0;
        }
        catch { WACAlert.Show("Error clearing calendar.", 0); }
    }

    public static string EventControl_Custom_DropDownListByAlphabet_Header(string value)
    {
        string s = string.Empty;
        switch (value)
        {
            case "AGRICULTURE_FARMOWNER_SEARCH": s = "<b>Farm Owner:</b> Select first letter of last name or organization:"; break;
            case "AGRICULTURE_FARMOWNEROPERATOR_SEARCH": s = "<b>Farm Owner/Operator:</b> Select first letter of last name or organization:"; break;
            case "AGRICULTURE_FARMOWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_BMP_OWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_BMP_CONTACT_INSERT": s = "<b>Add a Contact:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_BMP_MAIL_INSERT": s = "<b>Add a Mail Participant:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_FMP_OWNER_SEARCH": s = "<b>Owner:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_FMP_OWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_FMP_CONTACT_INSERT": s = "<b>Add a Contact:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_FMP_MAIL_INSERT": s = "<b>Add a Mail Participant:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_MAP_OWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_MAP_CONTACT_INSERT": s = "<b>Add a Contact:</b> Select first letter of last name or organization:"; break;
            //case "FORESTRY_MAP_MAIL_INSERT": s = "<b>Add a Mail Participant:</b> Select first letter of last name or organization:"; break;
            case "ORGANIZATION": s = "Select first letter of organization:"; break;
            case "PARTICIPANT_WITH_ORGANIZATION": s = "Select first letter of last name or organization:"; break;
            case "ORGANIZATION_SEARCH":
            case "PARTICIPANT_ORGANIZATION_SEARCH": s = "<b>Organization:</b> Select first letter of organization name:"; break;
            case "PARTICIPANT_PERSON_SEARCH": s = "<b>Person:</b> Select first letter of last name:"; break;
            case "ORGANIZATION_SEARCH_MULTI": s = "<b>Organization:</b> Get All Organizations starting with selected letter:"; break;
            case "PARTICIPANT_ORGANIZATION_SEARCH_MULTI": s = "<b>Select the first letter of an organization's name:</b>"; break;
            case "PARTICIPANT_PERSON_SEARCH_MULTI": s = "<b>Select the first letter of a person's last name:</b>"; break;
            case "PROPERTY_PARTICIPANT": s = "<b>Participant:</b> Select first letter of last name or organization:"; break;
            case "TAXPARCEL_OWNER": s = "<b>Tax Parcel Owner:</b> Select first letter of last name or organization:"; break;
            case "VENUE": s = "<b>Select first letter of venue:</b>"; break;
            case "EASEMENT_CONTACT": s = "<b>Easement Contact:</b> Select first letter of last name or organization:"; break;
            default: s = "Select first letter of last name:"; break;
        }
        return s;
    }

    public static void EventControl_Custom_DropDownListByAlphabet(DropDownList ddl, Label lbl, string sLB_CommandArg, string sEntityType,
        string sParticipantType, int? iValue)
    {
        ddl.Items.Clear();
        if (!string.IsNullOrEmpty(sLB_CommandArg))
        {
            lbl.Text = sLB_CommandArg + ":";
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                switch (sEntityType)
                {
                    case "AGRICULTURE_FARMOWNER":
                        if (sLB_CommandArg == "ORG")
                        {
                            var xAGRICULTURE_FARMOWNER_ORG = wDataContext.farmBusinessOwners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
                            foreach (var x in xAGRICULTURE_FARMOWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
                        }
                        else
                        {
                            var xAGRICULTURE_FARMOWNER_PERSON = wDataContext.farmBusinessOwners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
                            foreach (var x in xAGRICULTURE_FARMOWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                        }
                        break;
                    case "AGRICULTURE_FARMOWNEROPERATOR":
                        //var yAgFarmOwnerOperatorPerson = wDataContext.vw_farmBusiness_ownerOperators.Where(w => w.fullname_LF_dnd != null &&
                        //    w.fullname_LF_dnd.StartsWith(sLB_CommandArg)).
                        //    Select(s => new { s.fullname_LF_dnd, s.pk_farmBusiness }).Distinct().OrderBy(o => o.fullname_LF_dnd);
                        //List<ListItem> OwnerOperator = new List<ListItem>();
                        //foreach (var y in yAgFarmOwnerOperatorPerson)
                        //{
                        //    ddl.Items.Add(new ListItem(y.fullname_LF_dnd, y.pk_farmBusiness.ToString()));
                        //}
                        // 2/11/2014 - Changed by Eric for Seth
                        var yAgFarmOwnerOperatorPerson = wDataContext.vw_farmBusiness_ownerOperators.Where(w => w.OwnerLF != null &&
                            w.OwnerLF.StartsWith(sLB_CommandArg)).
                            Select(s => new { s.OwnerLF, s.fk_participant }).Distinct().OrderBy(o => o.OwnerLF);
                        List<ListItem> OwnerOperator = new List<ListItem>();
                        foreach (var y in yAgFarmOwnerOperatorPerson)
                        {
                            ddl.Items.Add(new ListItem(y.OwnerLF, y.fk_participant.ToString()));
                        }
                        break;
                    // NOT USED YET
                    //case "FORESTRY_BMP_OWNER":
                    //    if (sLB_CommandArg == "ORG")
                    //    {
                    //        var xFORESTRY_BMP_OWNER_ORG = wDataContext.forestryBMP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_BMP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
                    //    }
                    //    else
                    //    {
                    //        var xFORESTRY_BMP_OWNER_PERSON = wDataContext.forestryBMP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_BMP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                    //    }
                    //    break;
                    //case "FORESTRY_FMP_OWNER":
                    //    if (sLB_CommandArg == "ORG")
                    //    {
                    //        var xFORESTRY_FMP_OWNER_ORG = wDataContext.forestryFMP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_FMP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
                    //    }
                    //    else
                    //    {
                    //        var xFORESTRY_FMP_OWNER_PERSON = wDataContext.forestryFMP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_FMP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                    //    }
                    //    break;
                    //case "FORESTRY_MAP_OWNER":
                    //    if (sLB_CommandArg == "ORG")
                    //    {
                    //        var xFORESTRY_MAP_OWNER_ORG = wDataContext.forestryMAP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_MAP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
                    //    }
                    //    else
                    //    {
                    //        var xFORESTRY_MAP_OWNER_PERSON = wDataContext.forestryMAP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_MAP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                    //    }
                    //    break;
                    //case "FORESTRY_MAP_OWNER_SPECIAL": // Restricted to owners with a WFMP and cancel date is null and completed date is not null
                    //    if (sLB_CommandArg == "ORG")
                    //    {
                    //        var xFORESTRY_MAP_OWNER_ORG = wDataContext.forestryFMP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null && w.forestryFMP.cancel_date == null && w.forestryFMP.completion_date != null).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_MAP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
                    //    }
                    //    else
                    //    {
                    //        var xFORESTRY_MAP_OWNER_PERSON = wDataContext.forestryFMP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg) && w.forestryFMP.cancel_date == null && w.forestryFMP.completion_date != null).Select(s => s.participant).Distinct();
                    //        foreach (var x in xFORESTRY_MAP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                    //    }
                    //    break;
                    case "ORGANIZATION":
                        if (sLB_CommandArg == "1")
                        {
                            var xORGANIZATION_1 = wDataContext.organizations.OrderBy(o => o.org).Select(s => new { s.pk_organization, s.org });
                            List<KeyValuePair<string, int>> l = new List<KeyValuePair<string, int>>();
                            l.AddRange(xORGANIZATION_1.Select(s => new KeyValuePair<string, int>(s.org, s.pk_organization)));
                            foreach (KeyValuePair<string, int> kvp in l.Where(w => !char.IsLetter(w.Key[0]))) { ddl.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString())); }
                        }
                        else
                        {
                            var xORGANIZATION = wDataContext.organizations.Where(w => w.org.StartsWith(sLB_CommandArg)).OrderBy(o => o.org).Select(s => new { s.pk_organization, s.org });
                            foreach (var c in xORGANIZATION) { ddl.Items.Add(new ListItem(c.org, c.pk_organization.ToString())); }
                        }
                        break;
                    case "PARTICIPANT":
                        if (iValue != null && iValue > -1)
                        {
                            var xPARTICIPANT = wDataContext.participants.Where(w => w.pk_participant == iValue).
                                    OrderBy(o => o.fullname_LF_dnd).Select(s => new {text = s.fullname_LF_dnd, value = s.pk_participant.ToString()} );
                            ddl.Items.Add(new ListItem(xPARTICIPANT.Single().text, xPARTICIPANT.Single().value));
                            break;
                        }
                        if (sLB_CommandArg == "ORG")
                        {
                            var xPARTICIPANT_ORG = wDataContext.participants.Where(w => w.lname == null && w.fk_organization != null).Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.participantTypes });
                            if (!string.IsNullOrEmpty(sParticipantType)) xPARTICIPANT_ORG = xPARTICIPANT_ORG.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType));
                            foreach (var x in xPARTICIPANT_ORG.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                        }
                        else
                        {
                            var dbas = from p in wDataContext.participants
                                       join d in wDataContext.vw_participant_LFDBAs on p.pk_participant equals d.PK
                                       where d.listing.StartsWith(sLB_CommandArg)
                                           select p;
                            var parts = wDataContext.participants.Where(w => w.fullname_LF_dnd.StartsWith(sLB_CommandArg)).
                                    OrderBy(o => o.fullname_LF_dnd).Select(s => s);
                            var dbaParts = parts.Union(dbas);
                            if (!string.IsNullOrEmpty(sParticipantType))
                            {
                                dbas = from pt in dbaParts
                                        join t in wDataContext.participantTypes on pt.pk_participant equals t.fk_participant
                                        where t.fk_participantType_code == sParticipantType
                                        select pt;
                                dbaParts = dbas.AsQueryable();
                            }
                            var dbaPartsOrdered = dbaParts.OrderBy(o => o.fullname_LF_dnd).ToList<participant>();
                            foreach (var z in dbaPartsOrdered)
                                    ddl.Items.Add(new ListItem(z.fullname_LF_dnd, z.pk_participant.ToString()));
                        }
                        break;
                    case "PARTICIPANT_ORG":
                        if (sLB_CommandArg == "1")
                        {
                            var xPARTICIPANT_ORG_1 = wDataContext.participants.Where(w => w.lname == null && w.fk_organization != null).Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.participantTypes });
                            if (!string.IsNullOrEmpty(sParticipantType)) xPARTICIPANT_ORG_1 = xPARTICIPANT_ORG_1.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType));
                            List<KeyValuePair<string, int>> l = new List<KeyValuePair<string, int>>();
                            l.AddRange(xPARTICIPANT_ORG_1.Select(s => new KeyValuePair<string, int>(s.fullname_LF_dnd, s.pk_participant)));
                            foreach (KeyValuePair<string, int> kvp in l.Where(w => !char.IsLetter(w.Key[0]))) { ddl.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString())); }
                        }
                        else
                        {
                            var xPARTICIPANT_ORG = wDataContext.participants.Where(w => w.lname == null && w.fk_organization != null && w.fullname_LF_dnd.StartsWith(sLB_CommandArg)).Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.participantTypes });
                            if (!string.IsNullOrEmpty(sParticipantType)) xPARTICIPANT_ORG = xPARTICIPANT_ORG.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType));
                            foreach (var x in xPARTICIPANT_ORG.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                        }
                        break;
                    case "PROPERTY_PARTICIPANT":
                        if (sLB_CommandArg == "ORG")
                        {
                            var xPROPERTY_PARTICIPANT_ORG = wDataContext.participantProperties.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
                            foreach (var x in xPROPERTY_PARTICIPANT_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
                        }
                        else
                        {
                            var xPROPERTY_PARTICIPANT_PERSON = wDataContext.participantProperties.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
                            foreach (var x in xPROPERTY_PARTICIPANT_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                        }
                        break;
                    case "TAXPARCEL_OWNER":
                        if (sLB_CommandArg == "ORG")
                        {
                            var xTAXPARCEL_OWNER_ORG = wDataContext.taxParcelOwners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
                            foreach (var x in xTAXPARCEL_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
                        }
                        else
                        {
                            var xTAXPARCEL_OWNER_PERSON = wDataContext.taxParcelOwners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
                            foreach (var x in xTAXPARCEL_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
                        }
                        break;
                    case "VENUE":
                        var xVENUE = wDataContext.eventVenues.Where(w => w.location.StartsWith(sLB_CommandArg)).OrderBy(o => o.location).Select(s => new { s.pk_eventVenue, s.location });
                        foreach (var c in xVENUE) { ddl.Items.Add(new ListItem(c.location, c.pk_eventVenue.ToString())); }
                        break;
                    //case "EASEMENTS_CONTACT":
                    //    var xEasementContact = wDataContext.vw_participant_participantTypes.Select(s => s);
                    //    foreach (var item in xEasementContact)
                    //    {
                    //        ddl.Items.Add(new ListItem(item.fullname_LF_dnd, item.pk_participant.ToString()));
                    //    }
                    //    break;
                    default:
                        var xPARTICIPANT_DEFAULT = wDataContext.vw_participant_participantTypes.Where(w => w.fullname_LF_dnd != null &&
                                w.IsOrg == "N" && w.fullname_LF_dnd.StartsWith(sLB_CommandArg)).Distinct((x,y) => x.fullname_LF_dnd == y.fullname_LF_dnd).
                                Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.fk_participantType_code }).OrderBy(o => o.fullname_LF_dnd);;
                            foreach (var x in xPARTICIPANT_DEFAULT) 
                            { 
                                ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); 
                            }
                            break;
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    //public static string EventControl_Custom_DropDownListByAlphabet_Header(string value)
    //{
    //    string s = string.Empty;
    //    switch (value)
    //    {
    //        case "AGRICULTURE_FARMOWNER_SEARCH": s = "<b>Farm Owner:</b> Select first letter of last name or organization:"; break;
    //        case "AGRICULTURE_FARMOWNEROPERATOR_SEARCH": s = "<b>Farm Owner/Operator:</b> Select first letter of last name or organization:"; break;
    //        case "AGRICULTURE_FARMOWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_BMP_OWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_BMP_CONTACT_INSERT": s = "<b>Add a Contact:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_BMP_MAIL_INSERT": s = "<b>Add a Mail Participant:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_FMP_OWNER_SEARCH": s = "<b>Owner:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_FMP_OWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_FMP_CONTACT_INSERT": s = "<b>Add a Contact:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_FMP_MAIL_INSERT": s = "<b>Add a Mail Participant:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_MAP_OWNER_INSERT": s = "<b>Add an Owner:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_MAP_CONTACT_INSERT": s = "<b>Add a Contact:</b> Select first letter of last name or organization:"; break;
    //        case "FORESTRY_MAP_MAIL_INSERT": s = "<b>Add a Mail Participant:</b> Select first letter of last name or organization:"; break;
    //        case "ORGANIZATION": s = "Select first letter of organization:"; break;
    //        case "PARTICIPANT_WITH_ORGANIZATION": s = "Select first letter of last name or organization:"; break;
    //        case "ORGANIZATION_SEARCH":
    //        case "PARTICIPANT_ORGANIZATION_SEARCH": s = "<b>Organization:</b> Select first letter of organization name:"; break;
    //        case "PARTICIPANT_PERSON_SEARCH": s = "<b>Person:</b> Select first letter of last name:"; break;
    //        case "ORGANIZATION_SEARCH_MULTI":
    //        case "PARTICIPANT_ORGANIZATION_SEARCH_MULTI": s = "<b>Select the first letter of an organization's name:</b>"; break;
    //        case "PARTICIPANT_PERSON_SEARCH_MULTI": s = "<b>Select the first letter of a person's last name:</b>"; break;
    //        case "PROPERTY_PARTICIPANT": s = "<b>Participant:</b> Select first letter of last name or organization:"; break;
    //        case "TAXPARCEL_OWNER": s = "<b>Tax Parcel Owner:</b> Select first letter of last name or organization:"; break;
    //        case "VENUE": s = "<b>Select first letter of venue:</b>"; break;
    //        case "EASEMENT_CONTACT": s = "<b>Easement Contact:</b> Select first letter of last name or organization:"; break;
    //        default: s = "Select first letter of last name:"; break;
    //    }
    //    return s;
    //}

    //public static void EventControl_Custom_DropDownListByAlphabet(DropDownList ddl, Label lbl, string sLB_CommandArg, string sEntityType, 
    //    string sParticipantType, int? iValue)
    //{
    //    ddl.Items.Clear();
    //    if (!string.IsNullOrEmpty(sLB_CommandArg))
    //    {
    //        lbl.Text = sLB_CommandArg + ":";
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            switch (sEntityType)
    //            {
    //                case "AGRICULTURE_FARMOWNER":
    //                    if (sLB_CommandArg == "ORG")
    //                    {
    //                        var xAGRICULTURE_FARMOWNER_ORG = wDataContext.farmBusinessOwners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
    //                        foreach (var x in xAGRICULTURE_FARMOWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xAGRICULTURE_FARMOWNER_PERSON = wDataContext.farmBusinessOwners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
    //                        foreach (var x in xAGRICULTURE_FARMOWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "AGRICULTURE_FARMOWNEROPERATOR":
    //                        var yAgFarmOwnerOperatorPerson = wDataContext.vw_farmBusiness_ownerOperators.Where(w => w.fullname_LF_dnd != null && 
    //                            w.fullname_LF_dnd.StartsWith(sLB_CommandArg) && w.active.Contains("Y")).
    //                            Select(s => new { s.fullname_LF_dnd, s.fk_participant }).Distinct().OrderBy(o => o.fullname_LF_dnd);
    //                        List<ListItem> OwnerOperator = new List<ListItem>();
    //                        foreach (var y in yAgFarmOwnerOperatorPerson)
    //                        {
    //                            ddl.Items.Add(new ListItem(y.fullname_LF_dnd, y.fk_participant.ToString()));
    //                        }
    //                    break;

    //                // NOT USED YET
    //                //case "FORESTRY_BMP_OWNER":
    //                //    if (sLB_CommandArg == "ORG")
    //                //    {
    //                //        var xFORESTRY_BMP_OWNER_ORG = wDataContext.forestryBMP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
    //                //        foreach (var x in xFORESTRY_BMP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
    //                //    }
    //                //    else
    //                //    {
    //                //        var xFORESTRY_BMP_OWNER_PERSON = wDataContext.forestryBMP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
    //                //        foreach (var x in xFORESTRY_BMP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                //    }
    //                //    break;
    //                case "FORESTRY_FMP_OWNER":
    //                    if (sLB_CommandArg == "ORG")
    //                    {
    //                        var xFORESTRY_FMP_OWNER_ORG = wDataContext.forestryFMP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
    //                        foreach (var x in xFORESTRY_FMP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xFORESTRY_FMP_OWNER_PERSON = wDataContext.forestryFMP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
    //                        foreach (var x in xFORESTRY_FMP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "FORESTRY_MAP_OWNER":
    //                    if (sLB_CommandArg == "ORG")
    //                    {
    //                        var xFORESTRY_MAP_OWNER_ORG = wDataContext.forestryMAP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
    //                        foreach (var x in xFORESTRY_MAP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xFORESTRY_MAP_OWNER_PERSON = wDataContext.forestryMAP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
    //                        foreach (var x in xFORESTRY_MAP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "FORESTRY_MAP_OWNER_SPECIAL": // Restricted to owners with a WFMP and cancel date is null and completed date is not null
    //                    if (sLB_CommandArg == "ORG")
    //                    {
    //                        var xFORESTRY_MAP_OWNER_ORG = wDataContext.forestryFMP_owners.Where(w => w.participant.lname == null && w.participant.fk_organization != null && w.forestryFMP.cancel_date == null && w.forestryFMP.completion_date != null).Select(s => s.participant).Distinct();
    //                        foreach (var x in xFORESTRY_MAP_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xFORESTRY_MAP_OWNER_PERSON = wDataContext.forestryFMP_owners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg) && w.forestryFMP.cancel_date == null && w.forestryFMP.completion_date != null).Select(s => s.participant).Distinct();
    //                        foreach (var x in xFORESTRY_MAP_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "ORGANIZATION":
    //                    if (sLB_CommandArg == "1")
    //                    {
    //                        var xORGANIZATION_1 = wDataContext.organizations.OrderBy(o => o.org).Select(s => new { s.pk_organization, s.org });
    //                        List<KeyValuePair<string, int>> l = new List<KeyValuePair<string, int>>();
    //                        l.AddRange(xORGANIZATION_1.Select(s => new KeyValuePair<string, int>(s.org, s.pk_organization)));
    //                        foreach (KeyValuePair<string, int> kvp in l.Where(w => !char.IsLetter(w.Key[0]))) { ddl.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xORGANIZATION = wDataContext.organizations.Where(w => w.org.StartsWith(sLB_CommandArg)).OrderBy(o => o.org).Select(s => new { s.pk_organization, s.org });
    //                        foreach (var c in xORGANIZATION) { ddl.Items.Add(new ListItem(c.org, c.pk_organization.ToString())); }
    //                    }
    //                    break;
    //                case "PARTICIPANT":
    //                    if (sLB_CommandArg == "ORG")
    //                    {
    //                        var xPARTICIPANT_ORG = wDataContext.participants.Where(w => w.lname == null && w.fk_organization != null).Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.participantTypes });
    //                        if (!string.IsNullOrEmpty(sParticipantType)) xPARTICIPANT_ORG = xPARTICIPANT_ORG.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType));
    //                        foreach (var x in xPARTICIPANT_ORG.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xPARTICIPANT_PERSON = wDataContext.participants.Where(w => w.lname != null && w.lname.StartsWith(sLB_CommandArg)).Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.participantTypes });
    //                        if (!string.IsNullOrEmpty(sParticipantType)) xPARTICIPANT_PERSON = xPARTICIPANT_PERSON.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType));
    //                        foreach (var x in xPARTICIPANT_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "PARTICIPANT_ORG":
    //                    if (sLB_CommandArg == "1")
    //                    {
    //                        var xPARTICIPANT_ORG_1 = wDataContext.participants.Where(w => w.lname == null && w.fk_organization != null).Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.participantTypes });
    //                        if (!string.IsNullOrEmpty(sParticipantType)) xPARTICIPANT_ORG_1 = xPARTICIPANT_ORG_1.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType));
    //                        List<KeyValuePair<string, int>> l = new List<KeyValuePair<string, int>>();
    //                        l.AddRange(xPARTICIPANT_ORG_1.Select(s => new KeyValuePair<string, int>(s.fullname_LF_dnd, s.pk_participant)));
    //                        foreach (KeyValuePair<string, int> kvp in l.Where(w => !char.IsLetter(w.Key[0]))) { ddl.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xPARTICIPANT_ORG = wDataContext.participants.Where(w => w.lname == null && w.fk_organization != null && w.fullname_LF_dnd.StartsWith(sLB_CommandArg)).Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.participantTypes });
    //                        if (!string.IsNullOrEmpty(sParticipantType)) xPARTICIPANT_ORG = xPARTICIPANT_ORG.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType));
    //                        foreach (var x in xPARTICIPANT_ORG.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "PROPERTY_PARTICIPANT":
    //                    if (sLB_CommandArg == "ORG")
    //                    {
    //                        var xPROPERTY_PARTICIPANT_ORG = wDataContext.participantProperties.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
    //                        foreach (var x in xPROPERTY_PARTICIPANT_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xPROPERTY_PARTICIPANT_PERSON = wDataContext.participantProperties.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
    //                        foreach (var x in xPROPERTY_PARTICIPANT_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "TAXPARCEL_OWNER":
    //                    if (sLB_CommandArg == "ORG")
    //                    {
    //                        var xTAXPARCEL_OWNER_ORG = wDataContext.taxParcelOwners.Where(w => w.participant.lname == null && w.participant.fk_organization != null).Select(s => s.participant).Distinct();
    //                        foreach (var x in xTAXPARCEL_OWNER_ORG.OrderBy(o => o.organization.org)) { ddl.Items.Add(new ListItem(x.organization.org, x.pk_participant.ToString())); }
    //                    }
    //                    else
    //                    {
    //                        var xTAXPARCEL_OWNER_PERSON = wDataContext.taxParcelOwners.Where(w => w.participant.lname != null && w.participant.lname.StartsWith(sLB_CommandArg)).Select(s => s.participant).Distinct();
    //                        foreach (var x in xTAXPARCEL_OWNER_PERSON.OrderBy(o => o.fullname_LF_dnd)) { ddl.Items.Add(new ListItem(x.fullname_LF_dnd, x.pk_participant.ToString())); }
    //                    }
    //                    break;
    //                case "VENUE":
    //                    var xVENUE = wDataContext.eventVenues.Where(w => w.location.StartsWith(sLB_CommandArg)).OrderBy(o => o.location).Select(s => new { s.pk_eventVenue, s.location });
    //                    foreach (var c in xVENUE) { ddl.Items.Add(new ListItem(c.location, c.pk_eventVenue.ToString())); }
    //                    break;
    //                //case "EASEMENTS_CONTACT":
    //                //    var xEasementContact = wDataContext.vw_participant_participantTypes.Select(s => s);
    //                //    foreach (var item in xEasementContact)
    //                //    {
    //                //        ddl.Items.Add(new ListItem(item.fullname_LF_dnd, item.pk_participant.ToString()));
    //                //    }
    //                //    break;
    //            }
    //            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
    //            if (iValue != null) ddl.SelectedValue = iValue.ToString();
    //        }
    //    }
    //}

    public static string EventControl_Custom_Hyperlink_NavigateURL(Enum_NavigateURL_Special enumNUS, object oPK)
    {
        string sURL = string.Empty;
        try
        {
            switch (enumNUS)
            {
                case Enum_NavigateURL_Special.AG_2_SA:
                    sURL = "~/AG/WACAgriculture_SupplementalAgreements.aspx?pk=" + oPK.ToString();
                    break;
                case Enum_NavigateURL_Special.SA_2_AG:
                    sURL = "~/AG/WACAgriculture.aspx?pk=" + oPK.ToString() + "&tc=BMP";
                    break;
            }
        }
        catch { }
        return sURL;
    }

    #endregion

    #region Control Population

    #region Custom - Agriculture

    public static void LoadAgBmpViabilityCode(DropDownList ddl, int selectedId)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "Viability";
                ddl.DataValueField = "Id";
                var c = wac.AgBmpViabilityCodes.Select(s => new { Id = s.AgBmpViabilityCodeId, Viability = s.Viability });
                ddl.DataSource = c;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (selectedId > 0)
                {
                    ddl.SelectedValue = selectedId.ToString();
                }
            }             
        }
    }
    public static void LoadCountyDDL(DropDownList ddl, string county)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "county";
                ddl.DataValueField = "pk_county_code";
                var x = wac.list_counties.Select(s => s);
                    //wac.list_countyNY.Select(s => s);
                ddl.DataSource = x.OrderBy(o => o.county);
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(county))
                    ddl.SelectedValue = county.Substring(0, 2).ToUpper();
            }
        }
    }

    public static void LoadJurisdictionDDL(DropDownList ddl, string county)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "jurisdiction";
                ddl.DataValueField = "pk_list_swis";
                var x = wac.list_swis.Where(w => w.county == county && w.active == "Y").Select(s => new { s.pk_list_swis, s.jurisdiction });
                ddl.DataSource = x.OrderBy(o => o.jurisdiction);
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));    
            }
        }
    }

    public static int? GetBmpAncestorPk(int? bmp)
    {

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int? x = wac.AgBmpAncestoryVws.Where(y => y.fk_bmp_ag == bmp).Select(z => z.ParentBmpId).FirstOrDefault();
                return x;
            }
            catch (Exception)
            {

                return null;
            }


        }
    }

    public static string GetBmpAncestorBmpNumber(int? bmp)
    {

        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                string x = wac.AgBmpAncestoryVws.Where(y => y.fk_bmp_ag == bmp).Select(z => z.ParentCompositID).FirstOrDefault();
                return x;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }

    public static void LoadAncestorBmpDDL(DropDownList ddl, int fk_farmID, int? childBmpId)
    {
        
        if(ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {

                ddl.DataTextField = "ParentCompositID";
                ddl.DataValueField = "ParentBmpId";
                var x = wac.AgBmpAncestoryVws.Where(w => w.fk_farmBusiness == fk_farmID).Select(s => new { s.ParentBmpId, s.ParentCompositID}).Distinct();
                if (childBmpId != null)
                {
                    ddl.DataSource = x.Where(w => w.ParentBmpId != childBmpId).OrderBy(o => o.ParentCompositID);
                }
                else
                {
                    ddl.DataSource = x.OrderBy(o => o.ParentCompositID);
                }
               
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));

                var ancestor = GetBmpAncestorPk(childBmpId);


                if (ancestor != null)
                {

                    ddl.SelectedValue = ancestor.ToString();                   
                }
            }
        }

    }


    public static void LoadSblSectionDDL(DropDownList ddl, IList<DepTaxParcel> parcels)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            ddl.DataTextField = "SBLSection";
            ddl.DataValueField = "SBLSection";
            ddl.DataSource = parcels.Distinct((x,y) => x.SBLSection == y.SBLSection).OrderBy(o => o.PrintKey).Select(s => s);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }
    public static void LoadTaxParcelDDL(DropDownList ddl, IList<DepTaxParcel> parcels)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            ddl.DataTextField = "PrintKey";
            ddl.DataValueField = "PrintKey";
            ddl.DataSource = parcels.Distinct((x, y) => x.SBL == y.SBL).OrderBy(o => o.PrintKey).Select(s => s);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }
    public static IList<DepTaxParcel> LoadDepTaxParcels(string swis)
    {
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.vw_taxParcel_DEP_alls.Where(w => w.SWIS == swis).Select(s => new DepTaxParcel(s.SWIS, s.SBL, s.PRINT_KEY, s.SBLSection));
                return x.ToList<DepTaxParcel>();
            }
        }
        catch (Exception)
        {

            return null;
        }
        
        
    }

    public static void PopulateControl_Custom_Agriculture_TaxParcelOwner_SupplementalAgreement_DDL(DropDownList ddl, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                var x = wac.vw_supplementalAgreements.Where(w => w.SA_TP_Cancelled == null && w.pk_farmBusiness != null).Join(wac.taxParcels, jV => jV.pk_taxParcel, jTP => jTP.pk_taxParcel, (jV, jTP) => jTP).Join(wac.taxParcelOwners, jTP => jTP.pk_taxParcel, jTPO => jTPO.fk_taxParcel, (jTP, jTPO) => jTPO).Join(wac.participants, jTPO => jTPO.fk_participant, jP => jP.pk_participant, (jTPO, jP) => jP).Select(s => new { PK = s.pk_participant, NAME = s.fullname_LF_dnd }).Distinct();
                ddl.DataSource = x.OrderBy(o => o.NAME);
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_Cropware_FilterByYear(DropDownList ddl, int iPK_FarmBusiness)
    {
        if (ddl != null)
        {
            if (ddl.Items.Count == 0)
            {
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    ddl.DataSource = wac.cropwares.Where(w => w.fk_farmBusiness == iPK_FarmBusiness).GroupBy(g => g.plan_year).OrderByDescending(o => o.Key).Select(s => s.Key);
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("[ALL]", ""));
                }
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_BMP_ByFarmBusiness_DDL(DropDownList ddl, int iFarmBusinessPK, int? iValue, int?[] iProgrammaticCodes, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                IEnumerable<bmp_ag> x = wDataContext.bmp_ags.Where(w => w.fk_farmBusiness == iFarmBusinessPK && w.fk_statusBMP_code != "D").OrderBy(o => o.CompositBmpNum);
                if (iProgrammaticCodes != null)
                {
                    x = x.Where(w => !iProgrammaticCodes.Contains(w.fk_programmaticRecord_code)).Select(s => s);
                }
                foreach (var c in x)
                {
                    string sBMP = string.Empty;
                    if (!string.IsNullOrEmpty(c.CompositBmpNum)) sBMP = c.CompositBmpNum;
                    if (!string.IsNullOrEmpty(c.description))
                    {
                        if (!string.IsNullOrEmpty(sBMP)) sBMP += " - " + c.description;
                        else sBMP = c.description;
                    }
                    ddl.Items.Add(new ListItem(sBMP, c.pk_bmp_ag.ToString()));
                }
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
                catch { }
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_BMP_ByWFP3BMP_DDL(FormView fv, string ddlID, int iFormWFP3PK, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.form_wfp3_bmps.Where(w => w.fk_form_wfp3 == iFormWFP3PK).Select(s => new { s.pk_form_wfp3_bmp, s.bmp_ag.CompositBmpNum, s.bmp_ag.description });
                foreach (var c in a)
                {
                    string sBMP = string.Empty;
                    if (!string.IsNullOrEmpty(c.CompositBmpNum)) sBMP = c.CompositBmpNum;
                    if (!string.IsNullOrEmpty(c.description))
                    {
                        if (!string.IsNullOrEmpty(sBMP)) sBMP += " - " + c.description;
                        else sBMP = c.description;
                    }
                    ddl.Items.Add(new ListItem(sBMP, c.pk_form_wfp3_bmp.ToString()));
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_WFP3PaymentBMP_ByWFP3Specification_DDL(DropDownList ddl, int iFormWFP3PK, decimal? dValue, bool bShowPracticeCodeInTextValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.form_wfp3_specifications.Where(w => w.fk_form_wfp3 == iFormWFP3PK).Select(s => new { s.fk_bmpPractice_code, s.list_bmpPractice }).OrderBy(o => o.fk_bmpPractice_code);
                foreach (var y in x)
                {
                    if (bShowPracticeCodeInTextValue) ddl.Items.Add(new ListItem(y.fk_bmpPractice_code + " " + y.list_bmpPractice.practice, y.fk_bmpPractice_code.ToString()));
                    else ddl.Items.Add(new ListItem(y.list_bmpPractice.practice, y.fk_bmpPractice_code.ToString()));
                }
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (dValue != null) ddl.SelectedValue = dValue.ToString();
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_BMP_Lifespan_DDL(DropDownList ddl, byte? byValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            WACDataClassesDataContext wac = new WACDataClassesDataContext();
            var a = wac.list_lifespans.OrderBy(o => o.pk_lifespan);
            foreach (var b in a)
                ddl.Items.Add(new ListItem(b.pk_lifespan.ToString(), b.pk_lifespan.ToString()));
            //string[] sCollection = new string[] { "1", "5", "10", "15", "20" };
            //ddl.DataSource = new List<string>(sCollection);
            //ddl.DataBind();
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (byValue != null) ddl.SelectedValue = byValue.ToString();
        }
    }

    public static void PopulateControl_Custom_Agriculture_BMP_Status_WFP2Revision(DropDownList ddl, int iPK_WFP2, int? iValue, bool bShowSelect)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "PK";
            ddl.DataSource = wac.form_wfp2_versions.Where(w => w.fk_form_wfp2 == iPK_WFP2).OrderByDescending(o => o.version).Select(s => new { PK = s.pk_form_wfp2_version, NAME = s.version });
            ddl.DataBind();
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    public static void PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(DropDownList ddl, int? iValue, bool bShowSelect)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.supplementalAgreementTaxParcels.Where(w => w.cancel_date == null).OrderBy(o => o.supplementalAgreement.agreement_nbr_ro).Select(s => new { s.pk_supplementalAgreementTaxParcel, s.supplementalAgreement.agreement_nbr_ro, s.taxParcel.taxParcelID, s.taxParcel.ownerStr_dnd });
            foreach (var b in a)
            {
                ddl.Items.Add(new ListItem(SpecialText_Agriculture_SA_TP_Owner(b.agreement_nbr_ro, b.taxParcelID, b.ownerStr_dnd), b.pk_supplementalAgreementTaxParcel.ToString()));
            }
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }

    public static void PopulateControl_Custom_Agriculture_BMP_SupplementalAgreementTaxParcel(DropDownList ddl, int iPK_FarmBusiness, int? iValue, bool bShowSelect)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.bmp_ag_SAs.Where(w => w.bmp_ag.fk_farmBusiness == iPK_FarmBusiness).OrderBy(o => o.supplementalAgreementTaxParcel.taxParcel.taxParcelID).Select(s => new { s.supplementalAgreementTaxParcel.pk_supplementalAgreementTaxParcel, s.supplementalAgreementTaxParcel.supplementalAgreement.agreement_nbr_ro, s.supplementalAgreementTaxParcel.taxParcel.taxParcelID, s.supplementalAgreementTaxParcel.taxParcel.ownerStr_dnd }).Distinct();
            foreach (var b in a)
            {
                ddl.Items.Add(new ListItem(SpecialText_Agriculture_SA_TP_Owner(b.agreement_nbr_ro, b.taxParcelID, b.ownerStr_dnd), b.pk_supplementalAgreementTaxParcel.ToString()));
            }
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }

    public static void PopulateControl_Custom_Agriculture_BMP_WLProject_DDL(DropDownList ddl, int iFarmBusinessPK, int? iValue, int?[] iProgrammaticCodes)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "PK";
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var aExclude = wac.farmBusinessWLProjectBMPs.Where(w => w.bmp_ag.farmBusiness.pk_farmBusiness == iFarmBusinessPK)
                        .Join(wac.farmBusinessWLProjects,
                        b => b.fk_farmBusinessWLProject,
                        p => p.pk_farmBusinessWLProject,
                        (b, p) => new { b = b, p = p })
                        .Where(w => w.p.ImplementationProject != 0)
                        .Select(s => s.b.fk_bmp_ag);
             
                var a = wac.bmp_ags.Where(w => w.fk_farmBusiness == iFarmBusinessPK && (w.fk_statusBMP_code == "A" || w.fk_BMPTypeCode == "IRC")
                    && !w.Bmp.Contains("NMCP") && !w.Bmp.Contains("PFM") && !w.Bmp.Contains("PH"))
                    .OrderBy(o => o.CompositBmpNum)
                    .Select(s => new { PK = s.pk_bmp_ag, NAME = s.CompositBmpNum, s.fk_programmaticRecord_code });
                foreach (int? i in iProgrammaticCodes)
                {
                    a = a.Where(w => w.fk_programmaticRecord_code == null || w.fk_programmaticRecord_code != i);
                }
                a = a.Where(w => !aExclude.Contains(w.PK));

                ddl.DataSource = a;
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    public static void PopulateControl_Custom_Agriculture_Farm_DDL(DropDownList ddl, int? iValue, bool bShowFarmName, bool bShowFarmOwner, bool bShowSelect)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "PK";
            ddl.DataSource = wac.farmBusinesses.Where(w => w.farmID != null).OrderBy(o => o.farmID).Select(s => new { s.pk_farmBusiness, s.farmID, s.farm_name, s.ownerStr_dnd }).ToList().Select(s => new { PK = s.pk_farmBusiness, NAME = SpecialText_Agriculture_FarmBusiness_ID_NAME_OWNER(s.farmID, s.farm_name, s.ownerStr_dnd, bShowFarmName, bShowFarmOwner) });
            ddl.DataBind();
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }

    public static void PopulateControl_Custom_Agriculture_FarmIds_DDL(DropDownList ddl, int? iValue, bool bActiveOnly, bool bShowSelect)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "PK";
            var x = wac.farmBusinesses.Where(w => w.farmID != null).OrderBy(o => o.farmID).Select(s => new { PK = s.pk_farmBusiness, NAME = s.farmID, STATUS = s.farmBusinessStatus.OrderByDescending(o => o.date) });
            if (bActiveOnly) x = x.Where(w => w.STATUS.FirstOrDefault(f => f.fk_status_code == "A").fk_status_code == "A");
            ddl.DataSource = x;
            ddl.DataBind();
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }
    public static void PopulateControl_Custom_Agriculture_FarmLandTractField_ByFarmBusiness_CBL(CheckBoxList cbl, int iFarmBusinessPK)
    {
        if (cbl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == iFarmBusinessPK) select b.farmLand.farmLandTracts;
                foreach (var c in a)
                {
                    foreach (farmLandTract flt in c)
                    {
                        foreach (farmLandTractField fltf in flt.farmLandTractFields)
                        {
                            string s = string.Empty;
                            if (!string.IsNullOrEmpty(flt.tract)) s = flt.tract;
                            if (!string.IsNullOrEmpty(fltf.field)) s += ": " + fltf.field;
                            if (!string.IsNullOrEmpty(s)) cbl.Items.Add(new ListItem(s, fltf.pk_farmLandTractField.ToString()));
                        }
                    }
                }
            }
        }
    }
    public static void PopulateControl_Custom_Agriculture_FarmLandTractField_ByFarmBusiness_DDL(FormView fv, string ddlID, int iFarmBusinessPK, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == iFarmBusinessPK) select b.farmLand.farmLandTracts;
                foreach (var c in a)
                {
                    foreach (farmLandTract flt in c)
                    {
                        foreach (farmLandTractField fltf in flt.farmLandTractFields)
                        {
                            string s = string.Empty;
                            if (!string.IsNullOrEmpty(flt.tract)) s = flt.tract;
                            if (!string.IsNullOrEmpty(fltf.field)) s += ": " + fltf.field;
                            if (!string.IsNullOrEmpty(s)) ddl.Items.Add(new ListItem(s, fltf.pk_farmLandTractField.ToString()));
                        }
                    }
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_SupplementalAgreement_Farms_ActiveWithBMPs(DropDownList ddl, int? iValue)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.farmBusinesses.Where(w => w.fk_status_code_curr == "A" && w.bmp_ags.Count() > 0 ).OrderBy(o => o.farmID).Select(s => new { s.pk_farmBusiness, s.farmID, s.farm_name }).ToList().Select(s => new { PK = s.pk_farmBusiness, NAME = SpecialText_Agriculture_RecordTitle(s.farmID, s.farm_name, null) });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_WFP2_And_SAs(DropDownList ddl, object oForm_WFP2s, bool bShowSelect)
    {
        if (ddl != null)
        {
            try
            {
                EntitySet<form_wfp2> x = (EntitySet<form_wfp2>)oForm_WFP2s;
                foreach (form_wfp2 y in x)
                {
                    if (y.fk_supplementalAgreement != null)
                    {
                        ddl.Items.Add(new ListItem("WFP2 for Supplemental Agreement: " + y.supplementalAgreement.agreement_nbr_ro + " (" + y.supplementalAgreement.ownerStr_dnd + ")", y.pk_form_wfp2.ToString()));
                    }
                    else ddl.Items.Insert(0, new ListItem("Main WFP2", y.pk_form_wfp2.ToString()));
                }
            }
            catch { }
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
        }
    }

    public static void PopulateControl_Custom_Agriculture_WFP2Version_ByFarmBusiness_DDL(FormView fv, string ddlID, int iFarmBusinessPK, form_wfp2_version f_wfp2_version)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == iFarmBusinessPK) select b.form_wfp2s;
                foreach (var c in a)
                {
                    foreach (form_wfp2 wfp2 in c.Where(w => w.fk_supplementalAgreement == null))
                    {
                        foreach (form_wfp2_version wfp2_version in wfp2.form_wfp2_versions)
                        {
                            ddl.Items.Add(new ListItem(wfp2_version.version.ToString(), wfp2_version.pk_form_wfp2_version.ToString()));
                        }
                    }
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && f_wfp2_version != null) ddl.SelectedValue = f_wfp2_version.pk_form_wfp2_version.ToString();
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_WFP2Revision_PastAndFutureRevisions_DDL(DropDownList ddl, int? iPK_WFP2, bool bShowSelect)
    {
        if (ddl != null && iPK_WFP2 != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.form_wfp2_versions.Where(w => w.fk_form_wfp2 == iPK_WFP2).OrderByDescending(o => o.version).Select(s => s.version).ToList();
                if (a.Count() > 0)
                {
                    int iMax = a[0];
                    for (int i = iMax + 1; i >= 0; i--)
                    {
                        bool bAddRevisionNumber = true;
                        foreach (byte by in a)
                        {
                            if (i == Convert.ToInt32(by)) bAddRevisionNumber = false;
                        }
                        if (bAddRevisionNumber) ddl.Items.Add(i.ToString());
                    }
                }
                else ddl.Items.Add("0");
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_WFP3_InvoiceByWFP3(DropDownList ddl, int iPK_FormWFP3, int? iValue)
    {
        if (ddl != null)
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "PK";
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.form_wfp3_payments.Where(w => w.fk_form_wfp3 == iPK_FormWFP3).Select(s => s.fk_form_wfp3_invoice);
                ddl.DataSource = wac.form_wfp3_invoices.Where(w => w.fk_form_wfp3 == iPK_FormWFP3 && !a.Contains(w.pk_form_wfp3_invoice)).OrderByDescending(o => o.date).Select(s => new { PK = s.pk_form_wfp3_invoice, NAME = SpecialText_Agriculture_WFP3_Invoice(s.pk_form_wfp3_invoice) });
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null)
            {
                ddl.Items.Insert(1, new ListItem(SpecialText_Agriculture_WFP3_Invoice(iValue), iValue.ToString()));
                ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    public static void PopulateControl_Custom_Agriculture_Units_By_BMPPractice(object oBMPPracticeCode, Label lbl)
    {
        if (lbl != null)
        {
            if (oBMPPracticeCode != null)
            {
                if (!string.IsNullOrEmpty(oBMPPracticeCode.ToString())) lbl.Text = WACGlobal_Methods.SpecialQuery_Agriculture_Unit_By_BMPPractice(oBMPPracticeCode);
                else lbl.Text = "";
            }
            else lbl.Text = "";
        }
    }

    #endregion

    #region Custom Easements

    public static void PopulateControl_Custom_Easements_FarmIds_DDL(DropDownList ddl, int? iValue, bool bShowSelect)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "PK";
            var x = wac.vw_wfp2_versions.Where(w => w.version == 0 && w.approved_date != null && w.fk_status_code_curr == "A" 
                && w.fk_supplementalAgreement == null).OrderBy(o => o.farmID).Select(s => new { PK = s.pk_farmBusiness, NAME = s.farmID });
            ddl.DataSource = x;
            ddl.DataBind();
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }

    public static void PopulateControl_Custom_Easements_WFP2Versions_DDL(DropDownList ddl, int? iPK_FarmBusiness, int? iValue, bool bShowSelect)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "PK";
            var x = wac.vw_wfp2_version_SAs.Where(w => w.pk_farmBusiness == iPK_FarmBusiness).OrderBy(o => o.Revision).Select(s => new { PK = s.pk_form_wfp2_version, NAME = s.Revision });
            ddl.DataSource = x;
            ddl.DataBind();
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }

    #endregion

    #region Custom - Farm To Market

    public static void PopulateControl_Custom_FarmToMarket_EventRegistrant_ByEvent_DDL(DropDownList ddl, int iPK_Event, int? iPK_EventRegistrant, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.events.Where(w => w.pk_event == iPK_Event).Select(s => s.eventRegistrants.OrderBy(o => o.participant.fullname_LF_dnd)).Single();
                foreach (var y in x)
                {
                    ddl.Items.Add(new ListItem(y.participant.fullname_LF_dnd, y.pk_eventRegistrant.ToString()));
                }
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iPK_EventRegistrant != null) ddl.SelectedValue = iPK_EventRegistrant.ToString(); }
                catch { }
            }
        }
    }

    public static void PopulateControl_Custom_FarmToMarket_EventName_Title_DDL(DropDownList ddl, int? iEventName, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "title";
                ddl.DataValueField = "pk_eventName";
                ddl.DataSource = wac.eventNames.OrderBy(o => o.title).Select(s => new { s.pk_eventName, s.title });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iEventName != null) ddl.SelectedValue = iEventName.ToString(); }
                catch { }
            }
        }
    }

    #endregion

    #region Custom - Tax Parcels

    public static void PopulateControl_Custom_TaxParcels_County_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataSource = wac.taxParcels.Where(w => w.list_swi.county != "" && w.list_swi.county != "unknown" && w.list_swi.county != "n/a").Select(s => s.list_swi.county).Distinct().OrderBy(o => o);
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    public static void PopulateControl_Custom_TaxParcels_Jurisdiction_DDL(DropDownList ddl, string sCounty, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.taxParcels.Where(w => w.list_swi.county == sCounty).Select(s => new { s.list_swi.pk_list_swis, s.list_swi.jurisdiction }).Distinct().OrderBy(o => o.jurisdiction);
                ddl.DataTextField = "jurisdiction";
                ddl.DataValueField = "pk_list_swis";
                ddl.DataSource = x;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    public static void PopulateControl_Custom_TaxParcels_TaxParcelID_DDL(DropDownList ddl, string sCounty, string sJurisdiction, int? iTaxParcelID, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var a = wac.taxParcels.OrderBy(o => o.taxParcelID).Select(s => new { s.pk_taxParcel, s.taxParcelID, s.list_swi });
                if (!string.IsNullOrEmpty(sCounty)) a = a.Where(w => w.list_swi.county == sCounty);
                if (!string.IsNullOrEmpty(sJurisdiction)) a = a.Where(w => w.list_swi.jurisdiction == sJurisdiction);
                ddl.DataTextField = "taxParcelID";
                ddl.DataValueField = "pk_taxParcel";
                ddl.DataSource = a;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iTaxParcelID != null) ddl.SelectedValue = iTaxParcelID.ToString(); }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists

    #region DatabaseLists - Address Type

    public static void PopulateControl_DatabaseLists_AddressType_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_addressType_code";
                ddl.DataSource = wac.list_addressTypes.OrderBy(o => o.type).Select(s => new { s.pk_addressType_code, s.type });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Address 2 Type

    public static void PopulateControl_DatabaseLists_Address2Type_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "longname";
                ddl.DataValueField = "pk_address2Type_code";
                ddl.DataSource = wDataContext.list_address2Types.OrderBy(o => o.longname).Select(s => new { s.pk_address2Type_code, s.longname });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Agency

    public static void PopulateControl_DatabaseLists_Agency_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_Agency_DDL_Internal(ddl, value);
    }

    public static void PopulateControl_DatabaseLists_Agency_DDL(DropDownList ddl, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_Agency_DDL_Internal(ddl, value);
    }

    private static void PopulateControl_DatabaseLists_Agency_DDL_Internal(DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "agency";
            ddl.DataValueField = "pk_agency_code";
            ddl.DataSource = wDataContext.list_agencies.Where(w => w.BMPWorkload == "Y").OrderBy(o => o.agency).Select(s => new { s.pk_agency_code, s.agency });
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Agency - Funding

    public static void PopulateControl_DatabaseLists_AgencyFunding_DDL(DropDownList ddl, string sFunding, string sPayment, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "agency";
                ddl.DataValueField = "pk_agencyFunding_code";
                IEnumerable<list_agencyFunding> x = wac.list_agencyFundings.OrderBy(o => o.agency);
                if (!string.IsNullOrEmpty(sFunding)) x = x.Where(w => w.funding == sFunding);
                if (!string.IsNullOrEmpty(sPayment)) x = x.Where(w => w.payment == sPayment);
                ddl.DataSource = x;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - AG BMP Parent

    public static void PopulateControl_DatabaseLists_AncestorBMP_DDL(FormViewMode fvm, DropDownList ddl, object value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.AgBmpAncestoryVws.ToList();
                ddl.DataTextField = "CompositBmpNumber";
                ddl.DataValueField = "fk_bmp_ag";
                foreach (var b in a)
                    ddl.Items.Add(new ListItem(b.CompositBmpNumber, b.fk_bmp_ag.ToString()));
                ddl.DataSource = null;
                if (fvm == FormViewMode.Edit && value != null)
                    ddl.SelectedValue = value.ToString();
                else
                    ddl.SelectedIndex = 0;
            }
        }
    }

    #endregion  

    #region DatabaseLists - Ag Workload Funding

    public static void PopulateControl_DatabaseLists_AgWorkloadFunding_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_agWorkloadFundings.OrderBy(o => o.source).Select(s => new { PK = s.pk_agWorkloadFunding_code, NAME = s.source });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Animal

    public static void PopulateControl_DatabaseLists_Animal_DDL(FormView fv, string ddlID, int? iValue, string sASR, string sTier1)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_animals.OrderBy(o => o.animal).Select(s => s);
                if (!string.IsNullOrEmpty(sASR)) a = a.Where(w => w.asr == sASR);
                if (!string.IsNullOrEmpty(sTier1)) a = a.Where(w => w.tier1 == sTier1);
                ddl.DataTextField = "animal";
                ddl.DataValueField = "pk_list_animal";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    public static void PopulateControl_DatabaseLists_AnimalAge_DDL(FormView fv, string ddlID, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_animalAges.OrderBy(o => o.ageBracket) select new { b.pk_list_animalAge, b.ageBracket };
                ddl.DataTextField = "ageBracket";
                ddl.DataValueField = "pk_list_animalAge";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    #endregion

    #region DatabaseLists - Applicant Source

    public static void PopulateControl_DatabaseLists_ApplicantSource_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_applicantSources.OrderBy(o => o.source).Select(s => new { PK = s.pk_applicantSource_code, NAME = s.source });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - ASR Type

    public static void PopulateControl_DatabaseLists_ASRType_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_asrTypes.OrderBy(o => o.asrType) select new { b.pk_asrType_code, b.asrType };
                ddl.DataTextField = "asrType";
                ddl.DataValueField = "pk_asrType_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Basin

    public static void PopulateControl_DatabaseLists_Basin_DDL(DropDownList ddl, string sYN_Priority, string sYN_WOH, string sYN_EOH, int? iCounty, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "basin";
                ddl.DataValueField = "pk_basin_code";
                IEnumerable<list_basin> x = wac.list_basins.OrderBy(o => o.basin);
                if (!string.IsNullOrEmpty(sYN_Priority)) x = x.Where(w => w.priority == sYN_Priority);
                if (!string.IsNullOrEmpty(sYN_WOH)) x = x.Where(w => w.WOH == sYN_WOH);
                if (!string.IsNullOrEmpty(sYN_EOH)) x = x.Where(w => w.EOH == sYN_EOH);
                if (iCounty != null)
                {
                    x = x.Where(w => w.list_countyNYBasins.Any(a => a.fk_list_countyNY == iCounty));
                }
                ddl.DataSource = x;
                try { ddl.DataBind(); }
                catch { }
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - BMPCode_code

    public static void PopulateControl_DatabaseLists_BMPCode_code_DDL(DropDownList ddl, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_BMPCodes.OrderBy(o => o.code) select new { b.pk_BMPCode_code, b.code };
                ddl.DataTextField = "code";
                ddl.DataValueField = "pk_BMPCode_code";
                foreach (var b in a)
                {
                    ddl.Items.Add(new ListItem(b.code, b.pk_BMPCode_code));
                }

                ddl.DataSource = null;
                //ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - BMP Practice

    public static void PopulateControl_DatabaseLists_BMPPractice_DDL(FormView fv, string ddlID, decimal? dValue, bool bActive)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_BMPPractice_DDL_Internal(ddl, dValue, bActive, true, false);
    }

    public static void PopulateControl_DatabaseLists_BMPPractice_DDL(DropDownList ddl, decimal? dValue, bool bActive, bool bShowSelect, bool bOnlyShowPracticeCode)
    {
        if (ddl != null) PopulateControl_DatabaseLists_BMPPractice_DDL_Internal(ddl, dValue, bActive, bShowSelect, bOnlyShowPracticeCode);
    }

    private static void PopulateControl_DatabaseLists_BMPPractice_DDL_Internal(DropDownList ddl, decimal? dValue, bool bActive, bool bShowSelect, bool bOnlyShowPracticeCode)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.list_bmpPractices.OrderBy(o => o.pk_bmpPractice_code).Select(s => new { s.pk_bmpPractice_code, s.practice, s.active });
            if (bActive) 
                a = a.Where(w => w.active == "Y");
            foreach (var b in a)
            {
                if (bOnlyShowPracticeCode) 
                    ddl.Items.Add(new ListItem(b.pk_bmpPractice_code.ToString(), b.pk_bmpPractice_code.ToString()));
                else 
                    ddl.Items.Add(new ListItem(b.pk_bmpPractice_code + " - " + b.practice, b.pk_bmpPractice_code.ToString()));
            }
            if (dValue != null)
                ddl.SelectedValue = dValue.ToString();
            //if (bActive) ddl.ListSource = wDataContext.list_bmpPractices.Where(w => w.active == "Y").OrderBy(o => o.pk_bmpPractice_code).Select(s => new { s.pk_bmpPractice_code, mypractice = s.pk_bmpPractice_code + " - " + s.practice });
            //else ddl.ListSource = wDataContext.list_bmpPractices.OrderBy(o => o.pk_bmpPractice_code).Select(s => new { s.pk_bmpPractice_code, mypractice = s.pk_bmpPractice_code + " - " + s.practice });
            //ddl.DataTextField = "mypractice";
            //ddl.DataValueField = "pk_bmpPractice_code";
            //ddl.DataBind();
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (dValue != null) ddl.SelectedValue = dValue.ToString(); }
            catch { }
        }
    }

    #endregion

    #region DatabaseLists - BMP Priority

    public static void PopulateControl_DatabaseLists_BMPPriority_DDL(FormView fv, string ddlID, byte? byteValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_bmpPriorities.OrderBy(o => o.priority_level) select new { b.pk_bmpPriority_code, b.priority_level };
                ddl.DataTextField = "priority_level";
                ddl.DataValueField = "pk_bmpPriority_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && byteValue != null) 
                    ddl.SelectedValue = byteValue.ToString();
            }
        }
    }

    #endregion

    #region DatabaseLists - BMPCREPH20

    public static void PopulateControl_DatabaseLists_BMPCREPH20_DDL(FormView fv, string ddlID, object value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_BMPCREPH20s.OrderBy(o => o.type).Select(s => new { s.type, s.pk_BMPCREPH20_code });
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_BMPCREPH20_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (fv.CurrentMode == FormViewMode.Edit && value != null) 
                    ddl.SelectedValue = value.ToString();
            }
        }
    }

    #endregion

    #region DatabaseLists - BMPCode

    public static void PopulateControl_DatabaseLists_BMPCode_DDL(FormView fv, string ddlID, object value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_BMPCodes.Where(w => w.IsDisabled.Value == false).OrderBy(o => o.code).Select(s => new { s.pk_BMPCode_code, s.code });
                ddl.DataTextField = "pk_bmpCode_code";
                ddl.DataValueField = "pk_bmpCode_code";
                foreach (var b in a)
                    ddl.Items.Add(new ListItem(b.code, b.pk_BMPCode_code));
                ddl.DataSource = null;
                if (fv.CurrentMode == FormViewMode.Edit && value != null) 
                    ddl.SelectedValue = value.ToString();
                else
                    ddl.SelectedValue = "UNC";
            }
        }
    }

    #endregion

    #region DatabaseLists - BMPTypeCode

    public static void PopulateControl_DatabaseLists_BMPTypeCode_DDL(FormViewMode fvm, DropDownList ddl, object value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_BMPTypeCodes.ToList();
                ddl.DataTextField = "pk_BMPTypeCode";
                ddl.DataValueField = "pk_BMPTypeCode";
                foreach (var b in a)
                    ddl.Items.Add(new ListItem(b.pk_BMPTypeCode, b.pk_BMPTypeCode));
                ddl.DataSource = null;
                if (fvm == FormViewMode.Edit && value != null)
                    ddl.SelectedValue = value.ToString();
                else
                    ddl.SelectedValue = "BMP";
            }
        }
    }

    #endregion

    #region DatabaseLists - Bmp Descriptor

    public static void PopulateControl_DatabaseLists_BMPDescriptor_DDL(FormView fv, string ddlID, object value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.AgBmpDescriptorCodes.Where(w => w.IsDisabled == false).OrderBy(o => o.DescriptorCode).Select(s => new { s.Descriptor, s.DescriptorCode });
                ddl.DataTextField = "Descriptor";
                ddl.DataValueField = "DescriptorCode";
                foreach (var b in a)
                    ddl.Items.Add(new ListItem(b.Descriptor, b.DescriptorCode));
                ddl.DataSource = null;
                
                if (fv.CurrentMode == FormViewMode.Edit && value != null)
                    ddl.SelectedValue = value.ToString();
                else
                    ddl.SelectedValue = "UNC";
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
        }
    }

    #endregion
    #region DatabaseLists - NMP Storage Code
    public static void PopulateControl_DatabaseLists_NMPStorage_code_DDL(DropDownList ddl, int pk, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_NMPStorages.
                    OrderBy(o => o.storage).Select(s => new ListItem(s.storage, s.pk_NMPStorage_code));
                ddl.DataTextField = "Text";
                ddl.DataValueField = "Value";
                ddl.DataSource = a.ToList();
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value))
                    ddl.SelectedValue = value;
            }
        }
    }
    #endregion
    

    #region DatabaseLists - NMP BMPAgNMP
    public static void PopulateControl_DatabaseLists_BMPAgNMP_DDL(DropDownList ddl, int pk, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.vw_farmBusinessBMP_NMPs.Where(w => w.pk_farmBusiness == pk && !(w.fk_status_code == "D" || w.fk_status_code == "T")).
                    OrderBy(o => o.bmp_descrip).Select(s => new ListItem(s.bmp_descrip, s.pk_bmp_ag.ToString()));
                ddl.DataTextField = "Text";
                ddl.DataValueField = "Value";
                ddl.DataSource = a.ToList();
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value))
                    ddl.SelectedValue = value;
            }
        }
    }
    #endregion

    #region DatabaseLists - BMPSortGroupCode
    //public static void PopulateControl_DatabaseLists_BMPSortGroup_code_DDL(FormView fv, string ddlID, object value)
    //{
    //    DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
    //    if (ddl != null)
    //        PopulateControl_DatabaseLists_BMPSortGroup_code_DDL(ddl, value != null ? value.ToString() : string.Empty);
    //}
    //public static void PopulateControl_DatabaseLists_BMPSortGroup_code_DDL(DropDownList ddl, string value)
    //{
    //    if (ddl != null)
    //    {
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            var a = from b in wDataContext.list_BMPSortGroups.OrderBy(o => o.sortGroup) select new { b.pk_BMPSortGroup_code, b.sortGroup };
    //            ddl.DataTextField = "sortGroup";
    //            ddl.DataValueField = "pk_BMPSortGroup_code";
    //            foreach (var b in a)
    //                ddl.Items.Add(new ListItem(b.sortGroup, b.pk_BMPSortGroup_code));
    //            ddl.DataSource = null;
    //            //ddl.DataBind();
    //            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
    //            if (!string.IsNullOrEmpty(value)) 
    //                ddl.SelectedValue = value;
    //        }
    //    }
    //}
    public static void PopulateControl_DatabaseLists_BMPWorkloadSortGroup_code_DDL(FormView fv, string ddlID, object value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
            PopulateControl_DatabaseLists_BMPWorkloadSortGroup_code_DDL(ddl, value != null ? value.ToString() : string.Empty);
    }
    public static void PopulateControl_DatabaseLists_BMPWorkloadSortGroup_code_DDL(DropDownList ddl, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_BMPWorkloadSortGroups.OrderBy(o => o.sortGroup) select new { b.pk_BMPWorkloadSortGroup_code, b.sortGroup };
                ddl.DataTextField = "sortGroup";
                ddl.DataValueField = "pk_BMPWorkloadSortGroup_code";
                foreach (var b in a)
                    ddl.Items.Add(new ListItem(b.sortGroup, b.pk_BMPWorkloadSortGroup_code));
                ddl.DataSource = null;
                //ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value))
                    ddl.SelectedValue = value;
            }
        }
    }
    #endregion

    #region DatabaseLists - BMP Source

    public static void PopulateControl_DatabaseLists_BMPSource_DDL(FormView fv, string ddlID, string value, bool bShowSelect)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "source";
                ddl.DataValueField = "pk_BMPSource_code";
                ddl.DataSource = wDataContext.list_BMPSources.OrderBy(o => o.source).Select(s => new { s.pk_BMPSource_code, s.source });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - BMP Status 2006

    public static void PopulateControl_DatabaseLists_BMPStatus2006_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_bmpStatus2006s.OrderBy(o => o.status) select new { b.pk_bmpStatus2006_code, b.status };
                ddl.DataTextField = "status";
                ddl.DataValueField = "pk_bmpStatus2006_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Communication Type

    public static void PopulateControl_DatabaseLists_CommunicationType_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_communicationType_code";
                ddl.DataSource = wDataContext.list_communicationTypes.OrderBy(o => o.type).Select(s => new { s.pk_communicationType_code, s.type });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value.Trim();
            }
        }
    }

    #endregion

    #region DatabaseLists - Communication Usage

    public static void PopulateControl_DatabaseLists_CommunicationUsage_DDL(FormView fv, string ddlID, string value, string sType)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "usage";
                ddl.DataValueField = "pk_communicationUsage_code";
                ddl.DataSource = wDataContext.list_communicationUsages.OrderBy(o => o.usage).Select(s => new { s.pk_communicationUsage_code, s.usage });
                if (!string.IsNullOrEmpty(sType)) ddl.DataSource = wDataContext.list_communicationUsages.Where(w => w.fk_communicationType_code == sType).OrderBy(o => o.usage).Select(s => new { s.pk_communicationUsage_code, s.usage });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value.Trim();
            }
        }
    }

    #endregion

    #region DatabaseLists - Contractor Type

    public static void PopulateControl_DatabaseLists_ContractorType_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_contractorType_code";
                ddl.DataSource = wDataContext.list_contractorTypes.OrderBy(o => o.type).Select(s => new { s.pk_contractorType_code, s.type });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value.Trim();
            }
        }
    }

    public static void PopulateControl_DatabaseLists_ContractorType_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_contractorTypes.OrderBy(o => o.type).Select(s => new { PK = s.pk_contractorType_code, NAME = s.type });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value.Trim();
            }
        }
    }

    #endregion

    #region DatabaseLists - County NY

    public static void PopulateControl_DatabaseLists_CountyNY_DDL(FormView fv, string ddlID, bool bWatershedOnly, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_CountyNY_DDL_Internal(ddl, bWatershedOnly, iValue);
    }

    public static void PopulateControl_DatabaseLists_CountyNY_DDL(DropDownList ddl, bool bWatershedOnly, int? iValue)
    {
        if (ddl != null) PopulateControl_DatabaseLists_CountyNY_DDL_Internal(ddl, bWatershedOnly, iValue);
    }

    private static void PopulateControl_DatabaseLists_CountyNY_DDL_Internal(DropDownList ddl, bool bWatershedOnly, int? iValue)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.list_countyNies.OrderBy(o => o.county).Select(s => new { s.pk_list_countyNY, s.county, s.watershed });
            if (bWatershedOnly) a = a.Where(w => w.watershed == "Y");
            ddl.DataTextField = "county";
            ddl.DataValueField = "pk_list_countyNY";
            ddl.DataSource = a;
            ddl.DataBind();
            if (ddl.Items.Count > 0) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    #endregion

    #region DatabaseLists - Data Review

    public static void PopulateControl_DatabaseLists_DataReview_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "dataReview_status";
                ddl.DataValueField = "pk_dataReview_code";
                ddl.DataSource = wDataContext.list_dataReviews.OrderBy(o => o.dataReview_status).Select(s => new { s.pk_dataReview_code, s.dataReview_status });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Designer Engineer
    public static void PopulateControl_DatabaseLists_ASRPlanner_DDL(FormView fv, string ddlID, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                List<DDLListItem> items = null;

                    items = wDataContext.vw_designerEngineer_usages.Where(w => w.fk_designerEngineerType_code == "PLAN" && w.active == "Y").
                    Distinct((x, y) => x.designerEngineer_title == y.designerEngineer_title).OrderBy(o => o.designerEngineer_title).
                    Select(s => new DDLListItem(s.pk_list_designerEngineer.ToString(), s.designerEngineer_title)).ToList<DDLListItem>();
                ddl.DataTextField = "DataTextField";
                ddl.DataValueField = "DataValueField";
                ddl.DataSource = items;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }
    public static void PopulateControl_DatabaseLists_ASRAssignedTo_DDL(FormView fv, string ddlID, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                List<DDLListItem> items = wDataContext.list_designerEngineers.Join(
                        wDataContext.list_designerEngineerUsages.Where(w => w.fk_designerEngineerType_code == "ASR" && w.list_designerEngineer.active == "Y"),
                        de => de.pk_list_designerEngineer,
                        du => du.fk_list_designerEngineer,
                        (de, du) => new DDLListItem(de.pk_list_designerEngineer.ToString(), de.designerEngineer)
                    ).ToList<DDLListItem>();

                ddl.DataTextField = "DataTextField";
                ddl.DataValueField = "DataValueField";
                ddl.DataSource = items;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }
    public static void PopulateControl_DatabaseLists_DesignerEngineer_DDL(FormView fv, string ddlID, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_designerEngineers.OrderBy(o => o.designerEngineer) select new { b.pk_list_designerEngineer, b.designerEngineer };
                ddl.DataTextField = "designerEngineer";
                ddl.DataValueField = "pk_list_designerEngineer";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    public static void PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(FormView fv, string ddlID, string[] sDesignerEngineerTypeCollection, int? iValue, bool bActiveOnly)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL_Internal(fv, ddl, sDesignerEngineerTypeCollection, iValue, bActiveOnly, null);
    }

    public static void PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(FormView fv, string ddlID, string[] sDesignerEngineerTypeCollection, int? iValue, bool bActiveOnly, string sWACRegion)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL_Internal(fv, ddl, sDesignerEngineerTypeCollection, iValue, bActiveOnly, sWACRegion);
    }

    public static void PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(DropDownList ddl, string[] sDesignerEngineerTypeCollection, int? iValue, bool bActiveOnly)
    {
        if (ddl != null) PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL_Internal(null, ddl, sDesignerEngineerTypeCollection, iValue, bActiveOnly, null);
    }

    public static void PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL(DropDownList ddl, string[] sDesignerEngineerTypeCollection, int? iValue, bool bActiveOnly, string sWACRegion)
    {
        if (ddl != null) PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL_Internal(null, ddl, sDesignerEngineerTypeCollection, iValue, bActiveOnly, sWACRegion);
    }

    private static void PopulateControl_DatabaseLists_DesignerEngineer_ByDesignerEngineerTypeCollection_DDL_Internal(FormView fv, DropDownList ddl, string[] sDesignerEngineerTypeCollection, int? iValue, bool bActiveOnly, string sWACRegion)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddl.Items.Clear();

            var z = wDataContext.vw_designerEngineer_usages.OrderBy(o => o.designerEngineer_title).Select(s => s);
            if (bActiveOnly) z = z.Where(w => w.active == "Y");
            if (!string.IsNullOrEmpty(sWACRegion)) z = z.Where(w => w.fk_regionWAC_code == sWACRegion || w.fk_regionWAC_code == "BOTH");
            if (sDesignerEngineerTypeCollection != null) z = z.Where(w => sDesignerEngineerTypeCollection.Contains(w.fk_designerEngineerType_code));
            foreach (var y in z)
            {
                ddl.Items.Add(new ListItem(y.designerEngineer_title, y.pk_list_designerEngineer.ToString()));
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }

    #endregion

    #region DatabaseLists - Designer Engineer Type

    public static void PopulateControl_DatabaseLists_DesignerEngineerType_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {

                var a = wac.list_designerEngineerTypes.Where(w => w.pk_designerEngineerType_code != null).Where(w => w.formWFP3Tech == "Y").OrderBy(o => o.grouping).Select(s => new { PK = s.pk_designerEngineerType_code, NAME = s.grouping });
                //if (sTypeCollection != null) a = a.Where(w => sTypeCollection.Contains(w.PK));
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = a;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value))
                {
                    try { ddl.SelectedValue = value; }
                    catch { }
                }
            }
        }
    }

    #endregion

    #region DatabaseLists - Diversity Data

    public static void PopulateControl_DatabaseLists_DiversityData_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_diversityDatas.OrderBy(o => o.dataSetVia) select new { b.pk_diversityData_code, b.dataSetVia };
                ddl.DataTextField = "dataSetVia";
                ddl.DataValueField = "pk_diversityData_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Employee Relationship

    public static void PopulateControl_DatabaseLists_EmployeeRelationship_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wDataContext.list_employeeRelationships.OrderBy(o => o.relationship).Select(s => new { PK = s.pk_employeeRelationship_code, NAME = s.relationship }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Encumbrance

    public static void PopulateControl_DatabaseLists_Encumbrance_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_Encumbrance_DDL_Internal(fv, ddl, value, false);
    }

    public static void PopulateControl_DatabaseLists_Encumbrance_DDL(FormView fv, string ddlID, string value, bool bActiveOnly)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_Encumbrance_DDL_Internal(fv, ddl, value, bActiveOnly);
    }

    private static void PopulateControl_DatabaseLists_Encumbrance_DDL_Internal(FormView fv, DropDownList ddl, string value, bool bActiveOnly)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            if (bActiveOnly)
            {
                var a = from b in wDataContext.list_encumbrances.Where(w => w.active.ToString() == "Y").OrderBy(o => o.encumbrance) select new { b.pk_encumbrance_code, b.encumbrance };
                ddl.DataSource = a;
            }
            else
            {
                var a = from b in wDataContext.list_encumbrances.OrderBy(o => o.encumbrance) select new { b.pk_encumbrance_code, b.encumbrance };
                ddl.DataSource = a;
            }
            ddl.DataTextField = "encumbrance";
            ddl.DataValueField = "pk_encumbrance_code";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Encumbrance Type

    public static void PopulateControl_DatabaseLists_EncumbranceType_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wDataContext.list_encumbranceTypes.OrderBy(o => o.type).Select(s => new { PK = s.pk_encumbranceType_code, NAME = s.type });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Environmental Impact

    public static void PopulateControl_DatabaseLists_EnvironmentalImpact_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_environmentalImpacts.OrderBy(o => o.environmentalImpact) select new { b.pk_environmentalImpact_code, b.environmentalImpact };
                ddl.DataTextField = "environmentalImpact";
                ddl.DataValueField = "pk_environmentalImpact_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Ethnicity

    public static void PopulateControl_DatabaseLists_Ethnicity_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_ethnicities.OrderBy(o => o.ethnicity) select new { b.pk_ethnicity_code, b.ethnicity };
                ddl.DataTextField = "ethnicity";
                ddl.DataValueField = "pk_ethnicity_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Event Registrant Type

    public static void PopulateControl_DatabaseLists_EventRegistrantType_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_eventRegistrantTypes.OrderBy(o => o.type) select new { b.pk_eventRegistrantType_code, b.type };
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_eventRegistrantType_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Event Sponsor

    public static void PopulateControl_DatabaseLists_EventSponsor_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "sponsor";
                ddl.DataValueField = "pk_eventSponsor_code";
                ddl.DataSource = wDataContext.list_eventSponsors.OrderBy(o => o.sponsor).Select(s => new { s.pk_eventSponsor_code, s.sponsor });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - FAD

    public static void PopulateControl_DatabaseLists_FAD_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_FADs.OrderBy(o => o.setting).Select(s => new { PK = s.pk_FAD_code, NAME = s.setting });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Farm Business Note Types

    public static void PopulateControl_DatabaseLists_FarmBusinessNoteTypes_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_FarmBusinessNoteTypes_DDL_Internal(fv, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_FarmBusinessNoteTypes_DDL(DropDownList ddl, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_FarmBusinessNoteTypes_DDL_Internal(null, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_FarmBusinessNoteTypes_DDL_Internal(FormView fv, DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.list_farmBusinessNoteTypes.OrderBy(o => o.type) select new { b.pk_farmBusinessNoteType_code, b.type };
            ddl.DataTextField = "type";
            ddl.DataValueField = "pk_farmBusinessNoteType_code";
            ddl.DataSource = a;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (fv != null) if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Farm Size

    public static void PopulateControl_DatabaseLists_FarmSize_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_FarmSize_DDL_Internal(fv, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_FarmSize_DDL(DropDownList ddl, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_FarmSize_DDL_Internal(null, ddl, value);
    }

    private static void PopulateControl_DatabaseLists_FarmSize_DDL_Internal(FormView fv, DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.list_farmSizes.OrderBy(o => o.farmSize) select b;
            ddl.DataTextField = "farmSize";
            ddl.DataValueField = "pk_farmSize_code";
            ddl.DataSource = a;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (fv != null) if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Farm To Market Note Types

    //public static void PopulateControl_DatabaseLists_FarmToMarketNoteTypes_DDL(FormView fv, string ddlID, string value)
    //{
    //    DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
    //    if (ddl != null)
    //    {
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            var a = from b in wDataContext.list_f2m_noteTypes.OrderBy(o => o.type) select new { b.pk_f2m_noteType_code, b.type };
    //            ddl.DataTextField = "type";
    //            ddl.DataValueField = "pk_f2m_noteType_code";
    //            ddl.DataSource = a;
    //            ddl.DataBind();
    //            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
    //            if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
    //        }
    //    }
    //}

    #endregion

    #region DatabaseLists - Farm Types

    public static void PopulateControl_DatabaseLists_FarmType_DDL(FormView fv, string ddlID, string sFarmSize, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_FarmType_DDL_Internal(ddl, sFarmSize, value);
    }

    public static void PopulateControl_DatabaseLists_FarmType_DDL(DropDownList ddl, string sFarmSize, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_FarmType_DDL_Internal(ddl, sFarmSize, value);
    }

    public static void PopulateControl_DatabaseLists_FarmType_DDL_Internal(DropDownList ddl, string sFarmSize, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            switch (sFarmSize)
            {
                case "LF":
                    ddl.DataSource = wDataContext.list_farmTypes.Where(w => w.LF == "Y").OrderBy(o => o.farmType).Select(s => new { s.pk_farmType_code, s.farmType });
                    break;
                case "SF":
                    ddl.DataSource = wDataContext.list_farmTypes.Where(w => w.SF == "Y").OrderBy(o => o.farmType).Select(s => new { s.pk_farmType_code, s.farmType });
                    break;
                case "EOH":
                    ddl.DataSource = wDataContext.list_farmTypes.Where(w => w.EOH == "Y").OrderBy(o => o.farmType).Select(s => new { s.pk_farmType_code, s.farmType });
                    break;
                default:
                    ddl.DataSource = wDataContext.list_farmTypes.OrderBy(o => o.farmType).Select(s => new { s.pk_farmType_code, s.farmType });
                    break;
            }
            ddl.DataTextField = "farmType";
            ddl.DataValueField = "pk_farmType_code";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseList - fk_bmp_ag_subIssueHeader

    public static void PopulateControl_DatabaseLists_fk_bmp_ag_subIssueHeader(DropDownList ddl, int PK_FarmBusiness, string pk_pollutant_category_code, int? iValue)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.farmBusiness_get_subIssueHeaderRecords(PK_FarmBusiness, pk_pollutant_category_code);
                ddl.DataTextField = "BMPDescrip";
                ddl.DataValueField = "pk_bmp_ag";
                foreach (var b in a)
                   ddl.Items.Add(new ListItem(b.BMPDescrip, b.pk_bmp_ag.ToString()));
                //ddl.DataSource = a;
                //ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    #endregion

    #region DatabaseLists - Fiscal Year

    public static void PopulateControl_DatabaseLists_FiscalYear_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_fiscalYears.OrderByDescending(o => o.active).Select(s => new { PK = s.pk_fiscalYear_code, NAME = s.fiscalYear }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Follow Up NMP

    public static void PopulateControl_DatabaseLists_FollowUpNMP_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_followupNMPs.OrderBy(o => o.description) select b;
                ddl.DataTextField = "description";
                ddl.DataValueField = "pk_followupNMP_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

  
    #region DatabaseLists - Form WFP3 Fixed Text

    public static void PopulateControl_DatabaseLists_FormWFP3FixedText_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_formWFP3_fixedTexts.OrderBy(o => o.displayText) select new { b.pk_formWFP3_fixedText_code, b.displayText };
                ddl.DataTextField = "displayText";
                ddl.DataValueField = "pk_formWFP3_fixedText_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Forms WAC

    public static void PopulateControl_DatabaseLists_FormsWAC_DDL(DropDownList ddl, string value, string sParticipantType)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_formsWACs.Where(w => w.fk_participantType_code == sParticipantType).OrderBy(o => o.form).Select(s => new { s.pk_formsWAC_code, s.form });
                ddl.DataTextField = "form";
                ddl.DataValueField = "pk_formsWAC_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Funding Purpose

    public static void PopulateControl_DatabaseLists_FundingPurpose_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "purpose";
                ddl.DataValueField = "pk_fundingPurpose_code";
                ddl.DataSource = wDataContext.list_fundingPurposes.OrderBy(o => o.purpose).Select(s => new { s.pk_fundingPurpose_code, s.purpose }); ;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Funding Source

    public static void PopulateControl_DatabaseLists_FundingSource_DDL(FormView fv, string ddlID, string value, bool bHideTransferFunds)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "source";
                ddl.DataValueField = "pk_fundingSource_code";
                if (bHideTransferFunds) ddl.DataSource = wDataContext.list_fundingSources.Where(w => w.pk_fundingSource_code != "T").OrderBy(o => o.source).Select(s => new { s.pk_fundingSource_code, s.source });
                else ddl.DataSource = wDataContext.list_fundingSources.OrderBy(o => o.source).Select(s => new { s.pk_fundingSource_code, s.source });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Funding Source Forestry

    //public static void PopulateControl_DatabaseLists_FundingSourceForestry_DDL(FormView fv, string ddlID, string value)
    //{
    //    DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
    //    if (ddl != null)
    //    {
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            var a = from b in wDataContext.list_fundingSourceForestries.OrderBy(o => o.source) select b;
    //            ddl.DataTextField = "source";
    //            ddl.DataValueField = "pk_fundingSourceForestry_code";
    //            ddl.DataSource = a;
    //            ddl.DataBind();
    //            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
    //            if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
    //        }
    //    }
    //}

    #endregion

    #region DatabaseLists - Gender

    public static void PopulateControl_DatabaseLists_Gender_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_genders.OrderBy(o => o.gender) select new { b.pk_gender_code, b.gender };
                ddl.DataTextField = "gender";
                ddl.DataValueField = "pk_gender_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Group PI

    public static void PopulateControl_DatabaseLists_GroupPI_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_groupPIs.OrderBy(o => o.name) select new { b.pk_groupPI_code, b.name };
                ddl.DataTextField = "name";
                ddl.DataValueField = "pk_groupPI_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Interest

    public static void PopulateControl_DatabaseLists_Interest_DDL(FormView fv, string ddlID, string value, bool bActive, EntitySet<participantType> esParticipantTypes)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                IQueryable<list_interestType> iqInterestType;

                if (esParticipantTypes != null)
                {
                    List<string> listParticipantTypes = new List<string>();
                    if (esParticipantTypes.Count() > 0)
                    {
                        foreach (participantType pt in esParticipantTypes)
                        {
                            listParticipantTypes.Add(pt.fk_participantType_code);
                        }
                    }
                    if (bActive) iqInterestType = wDataContext.list_interestTypes.Where(w => w.list_interest.active == "Y" && listParticipantTypes.Contains(w.list_participantType.pk_participantType_code)).OrderBy(o => o.list_participantType.participantType).ThenBy(o => o.list_interest.interest).Select(s => s);
                    else iqInterestType = wDataContext.list_interestTypes.Where(w => listParticipantTypes.Contains(w.list_participantType.pk_participantType_code)).OrderBy(o => o.list_participantType.participantType).ThenBy(o => o.list_interest.interest).Select(s => s);
                }
                else
                {
                    if (bActive) iqInterestType = wDataContext.list_interestTypes.Where(w => w.list_interest.active == "Y").OrderBy(o => o.list_participantType.participantType).ThenBy(o => o.list_interest.interest).Select(s => s);
                    else iqInterestType = wDataContext.list_interestTypes.OrderBy(o => o.list_participantType.participantType).ThenBy(o => o.list_interest.interest).Select(s => s);
                }

                foreach (var b in iqInterestType)
                {
                    string sText = b.list_participantType.participantType + " - " + b.list_interest.interest;
                    ddl.Items.Add(new ListItem(sText, b.pk_list_interestType.ToString()));
                }

                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Location Org

    public static void PopulateControl_DatabaseLists_LocationOrg_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_locationOrgs.OrderBy(o => o.location) select new { b.pk_locationOrg_code, b.location };
                ddl.DataTextField = "location";
                ddl.DataValueField = "pk_locationOrg_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Mailing

    public static void PopulateControl_DatabaseLists_Mailing_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "mailing1";
                ddl.DataValueField = "pk_mailing";
                ddl.DataSource = wDataContext.mailings.OrderBy(o => o.mailing1).Select(s => new { s.pk_mailing, s.mailing1 });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Mailing Status

    public static void PopulateControl_DatabaseLists_MailingStatus_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_mailingStatus.OrderBy(o => o.status) select b;
                ddl.DataTextField = "status";
                ddl.DataValueField = "pk_mailingStatus_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Marital Status

    public static void PopulateControl_DatabaseLists_MaritalStatus_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wDataContext.list_maritalStatus.OrderBy(o => o.status).Select(s => new { PK = s.pk_maritalStatus_code, NAME = s.status }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Meal Type

    public static void PopulateControl_DatabaseLists_MealType_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_mealTypes.OrderBy(o => o.type) select new { b.pk_mealType_code, b.type };
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_mealType_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - NMCP Type

    public static void PopulateControl_DatabaseLists_NMCPType_DDL(FormView fv, string ddlID, string value, bool bShowSelect)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_NMCPType_code";
                ddl.DataSource = wDataContext.list_NMCPTypes.OrderBy(o => o.type).Select(s => new { s.pk_NMCPType_code, s.type });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Ownership

    public static void PopulateControl_DatabaseLists_Ownership_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_ownerships.OrderBy(o => o.ownership) select b;
                ddl.DataTextField = "ownership";
                ddl.DataValueField = "pk_ownership_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Participant Type

    public static void PopulateControl_DatabaseLists_ParticipantDBA_DDL(DropDownList ddl, string value)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "listing";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.vw_participant_LFDBAs.OrderBy(o => o.listing).Select(s => new { s.PK, s.listing });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    public static void PopulateControl_DatabaseLists_ParticipantType_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "participantType";
                ddl.DataValueField = "pk_participantType_code";
                ddl.DataSource = wac.list_participantTypes.OrderBy(o => o.participantType).Select(s => new { s.pk_participantType_code, s.participantType });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    public static void PopulateControl_DatabaseLists_ParticipantType_ByParticipantTypeCollection_DDL(FormView fv, string ddlID, string[] sParticipantTypeCollection, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_participantTypes.OrderBy(o => o.participantType) select new { b.pk_participantType_code, b.participantType };

                foreach (var c in a)
                {
                    foreach (string sParticipantType in sParticipantTypeCollection)
                    {
                        if (c.pk_participantType_code == sParticipantType)
                        {
                            ddl.Items.Add(new ListItem(c.participantType, c.pk_participantType_code.ToString()));
                            break;
                        }
                    }
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    public static void PopulateControl_DatabaseLists_ParticipantType_ByParticipantTypeCollection_DDL(FormView fv, string ddlID, EntitySet<participantType> esParticipantType , string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_participantTypes.OrderBy(o => o.participantType).Select(s => new { s.pk_participantType_code, s.participantType });

                foreach (var c in a)
                {
                    foreach (participantType p in esParticipantType)
                    {
                        if (c.pk_participantType_code == p.list_participantType.pk_participantType_code)
                        {
                            ddl.Items.Add(new ListItem(c.participantType, c.pk_participantType_code.ToString()));
                            break;
                        }
                    }
                }

                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Payment Status

    public static void PopulateControl_DatabaseLists_PaymentStatus_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "status";
                ddl.DataValueField = "pk_paymentStatus_code";
                ddl.DataSource = wac.list_paymentStatus.OrderBy(o => o.status).Select(s => s);
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Payment Via

    public static void PopulateControl_DatabaseLists_PaymentVia_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_paymentVias.OrderBy(o => o.mode) select new { b.pk_paymentVia_code, b.mode };
                ddl.DataTextField = "mode";
                ddl.DataValueField = "pk_paymentVia_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Phosphorous Level

    public static void PopulateControl_DatabaseLists_PhosphorousLevel_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wDataContext.list_phosphorousLevels.OrderBy(o => o.sort).Select(s => new { PK = s.pk_phosphorousLevel_code, NAME = s.level });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Pollutant Category

    public static void PopulateControl_DatabaseLists_PollutantCategory_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_PollutantCategory_DDL_Internal(ddl, value, false, true);
    }

    public static void PopulateControl_DatabaseLists_PollutantCategory_DDL(DropDownList ddl, string value, bool bOnlyShowCatCode, bool bShowSelect)
    {
        if (ddl != null) PopulateControl_DatabaseLists_PollutantCategory_DDL_Internal(ddl, value, bOnlyShowCatCode, bShowSelect);
    }

    private static void PopulateControl_DatabaseLists_PollutantCategory_DDL_Internal(DropDownList ddl, string value, bool bOnlyShowCatCode, bool bShowSelect)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.list_pollutant_categories.OrderByDescending(o => o.sort_position).Select(s => new { s.pk_pollutant_category_code, s.descrip });
            foreach (var c in a)
            {
                if (bOnlyShowCatCode) ddl.Items.Insert(0, new ListItem(c.pk_pollutant_category_code, c.pk_pollutant_category_code.ToString()));
                else ddl.Items.Insert(0, new ListItem(c.pk_pollutant_category_code + ". " + c.descrip, c.pk_pollutant_category_code.ToString()));
            }
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value.Trim();
        }
    }

    #endregion

    #region DatabaseLists - Position WAC

    public static void PopulateControl_DatabaseLists_PositionWAC_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_positionWACs.OrderBy(o => o.position).Select(s => new { PK = s.pk_positionWAC_code, NAME = s.position }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Prefix

    public static void PopulateControl_DatabaseLists_Prefix_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "prefix";
                ddl.DataValueField = "pk_prefix_code";
                ddl.DataSource = wDataContext.list_prefixes.OrderBy(o => o.prefix).Select(s => new { s.pk_prefix_code, s.prefix });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Procurement Type

    public static void PopulateControl_DatabaseLists_ProcurementType_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_procurementTypes.OrderBy(o => o.type) select new { b.pk_procurementType_code, b.type };
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_procurementType_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Program WAC

    public static void PopulateControl_DatabaseLists_ProgramWAC_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_ProgramWAC_DDL_Internal(fv, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_ProgramWAC_DDL(DropDownList ddl, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_ProgramWAC_DDL_Internal(null, ddl, value);
    }

    private static void PopulateControl_DatabaseLists_ProgramWAC_DDL_Internal(FormView fv, DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.list_programWACs.OrderBy(o => o.program) select b;
            ddl.DataTextField = "program";
            ddl.DataValueField = "pk_programWAC_code";
            ddl.DataSource = a;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (fv != null) if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Race

    public static void PopulateControl_DatabaseLists_Race_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_races.OrderBy(o => o.race) select new { b.pk_race_code, b.race };
                ddl.DataTextField = "race";
                ddl.DataValueField = "pk_race_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Region DEC

    public static void PopulateControl_DatabaseLists_RegionDEC_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_regionDECs.OrderBy(o => o.region) select new { b.pk_regionDEC_code, b.region };
                ddl.DataTextField = "region";
                ddl.DataValueField = "pk_regionDEC_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (fv != null) if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Region WAC

    public static void PopulateControl_DatabaseLists_RegionWAC_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_RegionWAC_DDL_Internal(fv, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_RegionWAC_DDL(DropDownList ddl, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_RegionWAC_DDL_Internal(null, ddl, value);
    }

    private static void PopulateControl_DatabaseLists_RegionWAC_DDL_Internal(FormView fv, DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.list_regionWACs.OrderBy(o => o.regionWAC) select new { b.pk_regionWAC_code, b.regionWAC };
            ddl.DataTextField = "regionWAC";
            ddl.DataValueField = "pk_regionWAC_code";
            ddl.DataSource = a;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (fv != null) if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Revision Description

    public static void PopulateControl_DatabaseLists_RevisionDescription_DDL(FormView fv, string ddlID, string revDesc, string activeRevDesc)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        PopulateControl_DatabaseLists_RevisionDescription_DDL(ddl, revDesc, activeRevDesc);
    }

    public static void PopulateControl_DatabaseLists_RevisionDescription_DDL(DropDownList ddl, string revDesc, string activeRevDesc)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                List<ListItem> items = new List<ListItem>();
                if (!string.IsNullOrEmpty(activeRevDesc) && activeRevDesc.Contains("N"))
                {
                    items = wDataContext.list_revisionDescriptions.OrderBy(o => o.description).Select(s =>
                        new ListItem(s.description, s.pk_revisionDescription_code)).ToList<ListItem>();
                }   
                else
                {
                    items = wDataContext.list_revisionDescriptions.Where(w => w.active == "Y").OrderBy(o => o.description).Select(s =>
                        new ListItem(s.description, s.pk_revisionDescription_code)).ToList<ListItem>();
                }
                ddl.DataTextField = "Text";
                ddl.DataValueField = "revDesc";
                foreach (var b in items)
                    ddl.Items.Insert(0, new ListItem(b.Text, b.Value));
                ddl.DataSource = null;
                //ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(revDesc)) ddl.SelectedValue = revDesc;
            }
        }
    }

    #endregion

    #region DatabaseLists - School Level

    public static void PopulateControl_DatabaseLists_SchoolLevel_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_SchoolLevel_DDL_Internal(fv, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_SchoolLevel_DDL(DropDownList ddl, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_SchoolLevel_DDL_Internal(null, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_SchoolLevel_DDL_Internal(FormView fv, DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.list_schoolLevels.OrderBy(o => o.level) select new { b.pk_schoolLevel_code, b.level };
            ddl.DataTextField = "level";
            ddl.DataValueField = "pk_schoolLevel_code";
            ddl.DataSource = a;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (fv != null) if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Sector WAC

    public static void PopulateControl_DatabaseLists_SectorWAC_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_sectorWACs.OrderBy(o => o.sector) select new { b.pk_sectorWAC_code, b.sector };
                ddl.DataTextField = "sector";
                ddl.DataValueField = "pk_sectorWAC_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - States (US)

    public static void PopulateControl_DatabaseLists_StatesUS_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataSource = wDataContext.zipcodes.GroupBy(g => g.state).OrderBy(o => o.Key).Select(s => s.Key);
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    public static void PopulateControl_DatabaseLists_StatesUS_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataSource = wac.zipcodes.GroupBy(g => g.state).OrderBy(o => o.Key).Select(s => s.Key);
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Status

    public static void PopulateControl_DatabaseLists_Status_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_Status_DDL_Internal(fv, ddl, value);
    }

    public static void PopulateControl_DatabaseLists_Status_DDL(DropDownList ddl, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_Status_DDL_Internal(null, ddl, value);
    }

    private static void PopulateControl_DatabaseLists_Status_DDL_Internal(FormView fv, DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = from b in wDataContext.list_status.OrderBy(o => o.sort) select b;
            ddl.DataTextField = "status";
            ddl.DataValueField = "pk_status_code";
            ddl.DataSource = a;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (fv != null) if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Status BMP

    public static void PopulateControl_DatabaseLists_StatusBMP_DDL(FormView fv, string ddlID, string value, bool bPaymentYesOnly, bool bStatusTable, bool bmpForm)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_statusBMPs.OrderBy(o => o.status).Select(s => s);
                if (bPaymentYesOnly) a = a.Where(w => w.payment == "Y");
                if (bStatusTable) a = a.Where(w => w.statusTable == "Y");
                if (bmpForm) a = a.Where(w => w.pk_statusBMP_code == "DR" || w.pk_statusBMP_code == "A");
                ddl.DataTextField = "status";
                ddl.DataValueField = "pk_statusBMP_code";
                ddl.DataSource = a;
                ddl.DataBind();
                //ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { WACAlert.Show("The current status value for the BMP does not match any available status options. ", 0); }
                
            }
        }
    }

    #endregion

    #region DatabaseLists - Status BMP Workload

    public static void PopulateControl_DatabaseLists_StatusBMPWorkload_DDL(DropDownList ddl, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "status";
                ddl.DataValueField = "pk_statusBMPWorkload_code";
                ddl.DataSource = wDataContext.list_statusBMPWorkloads.OrderBy(o => o.status).Select(s => new { s.pk_statusBMPWorkload_code, s.status});
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Funding Source BMP Workload

    public static void PopulateControl_DatabaseLists_FundingBMPWorkload_DDL(DropDownList ddl, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "pk_agWorkloadFunding_code";
                ddl.DataValueField = "pk_agWorkloadFunding_code";
                ddl.DataSource = wDataContext.list_agWorkloadFundings.OrderBy(o => o.pk_agWorkloadFunding_code).Select(s => new { s.pk_agWorkloadFunding_code, s.source });
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Subbasin

    public static void PopulateControl_DatabaseLists_Subbasin_DDL(FormView fv, string ddlID, string sBasin, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_Subbasin_DDL_Internal(ddl, sBasin, value);
    }

    public static void PopulateControl_DatabaseLists_Subbasin_DDL(DropDownList ddl, string sBasin, string value)
    {
        if (ddl != null) PopulateControl_DatabaseLists_Subbasin_DDL_Internal(ddl, sBasin, value);
    }

    private static void PopulateControl_DatabaseLists_Subbasin_DDL_Internal(DropDownList ddl, string sBasin, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            if (!string.IsNullOrEmpty(sBasin))
            {
                ddl.DataSource = wDataContext.list_subbasins.Where(w => w.basin == sBasin).OrderBy(o => o.subbasin).Select(s => new { s.pk_subbasin_code, s.subbasin });
            }
            else
            {
                ddl.DataSource = wDataContext.list_subbasins.OrderBy(o => o.subbasin).Select(s => new { s.pk_subbasin_code, s.subbasin });
            }
            ddl.DataTextField = "subbasin";
            ddl.DataValueField = "pk_subbasin_code";
            ddl.DataBind();
            if (ddl.Items.Count > 0) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region DatabaseLists - Suffix

    public static void PopulateControl_DatabaseLists_Suffix_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "suffix";
                ddl.DataValueField = "pk_suffix_code";
                ddl.DataSource = wac.list_suffixes.OrderBy(o => o.suffix).Select(s => new { s.pk_suffix_code, s.suffix });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Supplemental Agreement Assignment

    public static void PopulateControl_DatabaseLists_SupplementalAgreementAssignment_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_SAAssignType_code";
                ddl.DataSource = wDataContext.list_SAAssignTypes.OrderBy(o => o.type).Select(s => new { s.pk_SAAssignType_code, s.type });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - swis

    public static void PopulateControl_DatabaseLists_SWIS_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.list_swis.OrderBy(o => o.pk_list_swis).Select(s => s);
                foreach (var c in a)
                {
                    string s = c.county + " - " + c.muniname + " (" + c.munitype + ") - " + c.pk_list_swis;
                    ddl.Items.Add(new ListItem(s, c.pk_list_swis.ToString()));
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - swis - County

    public static void PopulateControl_DatabaseLists_SWIS_County_DDL(DropDownList ddl, string value, bool bActive, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                string sActive = "N";
                if (bActive) sActive = "Y";
                ddl.DataSource = wac.list_swis.Where(w => w.county != "" && w.county != "unknown" && w.county != "n/a" && w.active == sActive).GroupBy(g => g.county).OrderBy(o => o.Key).Select(s => s.Key);
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - swis - TaxJurisdictionDP

    public static void PopulateControl_DatabaseLists_SWIS_Jurisdiction_DDL(DropDownList ddl, string sCounty, string value, bool bShowSelect)
    {
        //if (ddl != null)
        //{
        //    //using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        //    //{
        //    //    var c = wac.vw_taxParcel_jurisdictions.Where(w => w.pk_list_countyNY == _pkCounty).Distinct((x, y) => x.jurisdiction == y.jurisdiction).
        //    //            OrderBy(o => o.jurisdiction).Select(s => new DDLListItem(s.swis, s.jurisdiction));
        //    //    return c.ToList();
        //    //}
        //    using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        //    {
        //        var c = wac.vw_taxParcel_jurisdictions.Where(w => w.county.Contains(sCounty)).Distinct((x, y) => x.jurisdiction == y.jurisdiction).
        //                OrderBy(o => o.jurisdiction).Select(s => new ListItem(s.jurisdiction,s.SWIS));
        //        //var a = wac.list_swis.OrderBy(o => o.jurisdiction).Select(s => new { s.pk_list_swis, s.jurisdiction, s.county }).Distinct();
        //        //if (!string.IsNullOrEmpty(sCounty)) a = a.Where(w => w.county == sCounty);
        //        ddl.DataTextField = "jurisdiction";
        //        ddl.DataValueField = "swis";
        //        ddl.DataSource = c;
        //        ddl.DataBind();
        //        if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
        //        try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
        //        catch { }
        //    }
        //}
    }

    #endregion

    #region DatabaseLists - Tour Type

    public static void PopulateControl_DatabaseLists_TourType_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_tourTypes.OrderBy(o => o.type) select new { b.pk_tourType_code, b.type };
                ddl.DataTextField = "type";
                ddl.DataValueField = "pk_tourType_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Township NY

    public static void PopulateControl_DatabaseLists_TownshipNY_DDL(FormView fv, string ddlID, int? iCounty, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_TownshipNY_DDL_Internal(ddl, iCounty, iValue);
    }

    public static void PopulateControl_DatabaseLists_TownshipNY_DDL(DropDownList ddl, int? iCounty, int? iValue)
    {
        if (ddl != null) PopulateControl_DatabaseLists_TownshipNY_DDL_Internal(ddl, iCounty, iValue);
    }

    private static void PopulateControl_DatabaseLists_TownshipNY_DDL_Internal(DropDownList ddl, int? iCounty, int? iValue)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.list_townshipNies.OrderBy(o => o.township).Select(s => new { s.pk_list_townshipNY, s.township, s.fk_list_countyNY });
            if (iCounty != null) a = a.Where(w => w.fk_list_countyNY == iCounty);
            ddl.DataTextField = "township";
            ddl.DataValueField = "pk_list_townshipNY";
            ddl.DataSource = a;
            ddl.DataBind();
            if (ddl.Items.Count > 0) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    #endregion

    #region DatabaseLists - Training Cost

    public static void PopulateControl_DatabaseLists_TrainingCost_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_trainingCosts.OrderBy(o => o.item).Select(s => new { PK = s.pk_trainingCost_code, NAME = s.item }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - Training Requirement

    public static void PopulateControl_DatabaseLists_TrainingReqd_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.list_trainingReqds.OrderBy(o => o.pk_trainingReqd_code).Select(s => new { PK = s.pk_trainingReqd_code, NAME = s.pk_trainingReqd_code }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion
    #region DatabaseLists - Unit

    public static void PopulateControl_DatabaseLists_Unit_DDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.list_units.OrderBy(o => o.unit) select new { b.pk_unit_code, b.unit };
                ddl.DataTextField = "unit";
                ddl.DataValueField = "pk_unit_code";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - USPS ABBR

    public static void PopulateControl_DatabaseLists_USPS_Abbr_DDL(FormView fv, string ddlID, string value, bool bShowOnlySuffixes)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "longname";
                ddl.DataValueField = "pk_usps_abbr";
                if (bShowOnlySuffixes) ddl.DataSource = wDataContext.list_usps_abbrs.Where(w => w.suffix == "Y").OrderBy(o => o.longname);
                else ddl.DataSource = wDataContext.list_usps_abbrs.OrderBy(o => o.longname);
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    public static void PopulateControl_DatabaseLists_USPS_Abbr_DDL(DropDownList ddl, string value, bool bShowOnlySuffixes, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "longname";
                ddl.DataValueField = "pk_usps_abbr";
                var x = wac.list_usps_abbrs.OrderBy(o => o.longname);
                if (bShowOnlySuffixes) x = x.Where(w => w.suffix == "Y").OrderBy(o => o.longname);
                ddl.DataSource = x;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - WAC Location

    public static void PopulateControl_DatabaseLists_WACLocation_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wDataContext.list_wacLocations.OrderBy(o => o.location).Select(s => new { PK = s.pk_wacLocation_code, NAME = s.location }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #region DatabaseLists - WAC Practice Category

    public static void PopulateControl_DatabaseLists_WACPracticeCategory_DDL(DropDownList ddl, int? iValue)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wDataContext.list_wacPracticeCategories.OrderBy(o => o.category).Select(s => new { PK = s.pk_list_wacPracticeCategory, NAME = s.category }); ;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    #endregion

    #region DatabaseLists - WFP2 Approved By

    public static void PopulateControl_DatabaseLists_WFP2ApprovedBy_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wDataContext.list_WFP2ApprovedBies.OrderBy(o => o.approvedBy).Select(s => new { PK = s.pk_WFP2ApprovedBy_code, NAME = s.approvedBy }); ;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Workload Notation

    public static void PopulateControl_DatabaseLists_WorkloadNotation_DDL(DropDownList ddl, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "notation";
                ddl.DataValueField = "pk_workloadNotation_code";
                ddl.DataSource = wDataContext.list_workloadNotations.OrderBy(o => o.notation).Select(s => new { s.pk_workloadNotation_code, s.notation }); ;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region DatabaseLists - Year

    public static void PopulateControl_DatabaseLists_Year_DDL(FormView fv, string ddlID, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_DatabaseLists_Year_DDL_Internal(fv, ddl, iValue);
    }

    public static void PopulateControl_DatabaseLists_Year_DDL(DropDownList ddl, int? iValue)
    {
        if (ddl != null) PopulateControl_DatabaseLists_Year_DDL_Internal(null, ddl, iValue);
    }

    private static void PopulateControl_DatabaseLists_Year_DDL_Internal(FormView fv, DropDownList ddl, int? iValue)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "pk_year";
            ddl.DataValueField = "pk_year";
            ddl.DataSource = wDataContext.list_years.OrderBy(o => o.pk_year).Select(s => new { s.pk_year });
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null)
                //iValue = DateTime.Now.Year+1;
                ddl.SelectedValue = iValue.ToString();
            else
                ddl.SelectedIndex = 0;
        }
    }

    #endregion

    #region DatabaseLists - Zipcode

    public static void PopulateControl_DatabaseLists_Zipcode_DDL(FormView fv, string ddlID, string value, string sState)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "pk_zipcode";
                ddl.DataValueField = "pk_zipcode";
                if (string.IsNullOrEmpty(sState)) ddl.DataSource = wDataContext.zipcodes.OrderBy(o => o.pk_zipcode);
                else ddl.DataSource = wDataContext.zipcodes.Where(w => w.state == sState).OrderBy(o => o.pk_zipcode);
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    public static void PopulateControl_DatabaseLists_Zipcode_DDL(DropDownList ddl, string value, string sState, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "pk_zipcode";
                ddl.DataValueField = "pk_zipcode";
                var x = wac.zipcodes.OrderBy(o => o.pk_zipcode);
                if (!string.IsNullOrEmpty(sState)) x = x.Where(w => w.state == sState).OrderBy(o => o.pk_zipcode);
                ddl.DataSource = x;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                catch { }
            }
        }
    }

    #endregion

    #endregion

    #region Generic

    public static void DdlAddSelect(DropDownList ddl)
    {
        if (ddl.Items.FindByText("[Select]") == null)
            ddl.Items.Insert(0, new ListItem("[Select]","-1"));
    }
    #region Edit Calendar

    public static void PopulateControl_Generic_TextBoxCalendar(FormView fv, string sTBCalendar_ID, DateTime? dt, int? iStartYear)
    {
       // TextBox tB = (fv.FindControl(sTBCalendar_ID)).FindControl("tb") as TextBox;
        TextBox tB = fv.FindControl(sTBCalendar_ID) as TextBox;
        if (tB != null)
        {
            tB.Text = dt == null ? "" : ((DateTime)dt).ToShortDateString();
        }
    }

    public static void PopulateControl_Generic_CalendarAndDDL(UserControl uc, DateTime? dt, int? iStartYear)
    {
        Calendar cal = uc.FindControl("cal") as Calendar;
        DropDownList ddl = uc.FindControl("ddl") as DropDownList;
        if (cal != null) PopulateControl_Generic_CalendarAndDDL_Internal(cal, ddl, dt, iStartYear);
    }

    public static void PopulateControl_Generic_CalendarAndDDL(FormView fv, string sUCID, DateTime? dt, int? iStartYear)
    {
        UserControl uc = fv.FindControl(sUCID) as UserControl;
        Calendar cal = uc.FindControl("cal") as Calendar;
        DropDownList ddl = uc.FindControl("ddl") as DropDownList;
        if (cal != null) PopulateControl_Generic_CalendarAndDDL_Internal(cal, ddl, dt, iStartYear);
    }

    public static void PopulateControl_Generic_CalendarAndDDL(FormView fv, string sCalendar_ID, string ddlID, DateTime? dt, int? iStartYear)
    {
        Calendar cal = fv.FindControl(sCalendar_ID) as Calendar;
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (cal != null) PopulateControl_Generic_CalendarAndDDL_Internal(cal, ddl, dt, iStartYear);
    }

    private static void PopulateControl_Generic_CalendarAndDDL_Internal(Calendar cal, DropDownList ddl, DateTime? dt, int? iStartYear)
    {
        if (dt != null)
        {
            cal.SelectedDate = Convert.ToDateTime(dt);
            cal.VisibleDate = Convert.ToDateTime(dt);
        }
        if (ddl != null)
        {
            if (iStartYear != null)
            {
                ddl.Items.Clear();
                for (int i = Convert.ToInt32(iStartYear); i < DateTime.Now.Year + 1; i++)
                {
                    ddl.Items.Add(i.ToString());
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
            else
            {
                ddl.Items.Clear();
                for (int i = 1990; i < 2016; i++)
                {
                    ddl.Items.Add(i.ToString());
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
        }
    }

    #endregion

    public static void PopulateControl_Generic_Percentages_DDL(DropDownList ddl, byte? byValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            ddl.DataSource = SpecialDataType_StringCollection_PercentagesByFive();
            ddl.DataBind();
            if (bShowSelect) ddl.Items.Add(new ListItem("[SELECT]", ""));
            try { if (byValue != null) ddl.SelectedValue = byValue.ToString(); }
            catch { }
        }
    }

    public static void PopulateControl_Generic_YesNoCBX(FormView fv, string sCBX_ID, string value)
    {
        CheckBox cbx = fv.FindControl(sCBX_ID) as CheckBox;
        if (value == "Y") cbx.Checked = true;
    }

    public static void PopulateControl_Generic_YesNoDDL(FormView fv, string ddlID, string value)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_Generic_YesNoDDL_Internal(ddl, value, true);
    }

    public static void PopulateControl_Generic_YesNoDDL(FormView fv, string ddlID, string value, bool bShowSelectOption)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_Generic_YesNoDDL_Internal(ddl, value, bShowSelectOption);
    }

    public static void PopulateControl_Generic_YesNoDDL(DropDownList ddl, string value, bool bShowSelectOption)
    {
        if (ddl != null) PopulateControl_Generic_YesNoDDL_Internal(ddl, value, bShowSelectOption);
    }

    private static void PopulateControl_Generic_YesNoDDL_Internal(DropDownList ddl, string value, bool bShowSelectOption)
    {
        if (bShowSelectOption) ddl.Items.Add(new ListItem("[SELECT]", ""));
        ddl.Items.Add(new ListItem("Yes", "Y"));
        ddl.Items.Add(new ListItem("No", "N"));
        if (!string.IsNullOrEmpty(value))
            ddl.SelectedValue = value;
    }

    #endregion

    #region Communication

    public static void PopulateControl_Communication_PhoneNumbersByAreaCode_DDL(FormView fv, string ddlID, int? iValue, string sAreaCode)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_Communication_PhoneNumbersByAreaCode_DDL_Internal(ddl, iValue, sAreaCode);
    }

    public static void PopulateControl_Communication_PhoneNumbersByAreaCode_DDL(DropDownList ddl, int? iValue, string sAreaCode)
    {
        if (ddl != null) PopulateControl_Communication_PhoneNumbersByAreaCode_DDL_Internal(ddl, iValue, sAreaCode);
    }

    public static void PopulateControl_Communication_PhoneNumbersByAreaCode_DDL_Internal(DropDownList ddl, int? iValue, string sAreaCode)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.communications.Where(w => w.areacode == sAreaCode).OrderBy(o => o.number).Select(s => new { s.pk_communication, s.areacode, s.number });
            foreach (var c in a)
            {
                ddl.Items.Add(new ListItem(Format_Global_PhoneNumberSeparateAreaCode(c.areacode, c.number), c.pk_communication.ToString()));
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    public static void PopulateControl_Communication_AreaCodes_DDL(DropDownList ddl, string value)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                ddl.DataSource = wDataContext.communications.OrderBy(o => o.areacode).Select(s => s.areacode).Distinct();
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion

    #region Organization

    public static void PopulateControl_Organization_Org_DDL(FormView fv, string ddlID, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = from b in wDataContext.organizations.OrderBy(o => o.org) select new { b.pk_organization, b.org };
                ddl.DataTextField = "org";
                ddl.DataValueField = "pk_organization";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    #endregion

    #region Participant

    /* NEW PARTICIPANT METHOD */
    public static void PopulateControl_Participant_DBView_DDL(DropDownList ddl, int? iValue, string[] sParticipantTypeCollection, string[] sContractorTypeCollection, string[] sForestryTypeCollection, string[] sRegionWACCollection, string[] sBasinCollection, int?[] iCountyCollection, string[] sZipCodeCollection, bool bFarmBusiness, bool bForestryBMP, bool bForestryFMP, bool bForestryMAP, bool bFarmToMarket, bool bTaxParcel, Enum_Participant_Forms enumParticipantForms, bool bShowSelect)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            var a = wac.vw_participant_participantTypes.Select(s => new { s.pk_participant, s.fullname_LF_dnd, s.lname, s.fname, s.IsOrg, s.org, s.fk_participantType_code, s.fk_contractorType_code, s.fk_forestryCode_code, s.fk_regionWAC_code, s.fk_basin_code, s.fk_list_countyNY, s.fk_zipcode, s.fk_farmBusiness, s.fk_forestryBMP, s.fk_forestryFMP, s.fk_forestryMAP, s.fk_farmToMarket, s.fk_taxParcel });
            if (sParticipantTypeCollection != null) a = a.Where(w => sParticipantTypeCollection.Contains(w.fk_participantType_code));
            if (sContractorTypeCollection != null) a = a.Where(w => sContractorTypeCollection.Contains(w.fk_contractorType_code));
            if (sForestryTypeCollection != null) a = a.Where(w => sForestryTypeCollection.Contains(w.fk_forestryCode_code));
            if (sRegionWACCollection != null) a = a.Where(w => sRegionWACCollection.Contains(w.fk_regionWAC_code) || w.fk_regionWAC_code == "BOTH");
            if (sBasinCollection != null) a = a.Where(w => sBasinCollection.Contains(w.fk_basin_code));
            if (iCountyCollection != null) a = a.Where(w => iCountyCollection.Contains(w.fk_list_countyNY));
            if (sZipCodeCollection != null) a = a.Where(w => sZipCodeCollection.Contains(w.fk_zipcode));
            if (bFarmBusiness) a = a.Where(w => w.fk_farmBusiness != null);
            if (bForestryBMP) a = a.Where(w => w.fk_forestryBMP != null);
            if (bForestryFMP) a = a.Where(w => w.fk_forestryFMP != null);
            if (bForestryMAP) a = a.Where(w => w.fk_forestryMAP != null);
            if (bFarmToMarket) a = a.Where(w => w.fk_farmToMarket != null);
            if (bTaxParcel) a = a.Where(w => w.fk_taxParcel != null);
            switch (enumParticipantForms)
            {
                case Enum_Participant_Forms.PERSON: a = a.Where(w => w.IsOrg == "N"); break;
                case Enum_Participant_Forms.ORGANIZATION: a = a.Where(w => w.IsOrg == "Y"); break;
            }
            var x = a.Select(s => new { s.pk_participant, s.fullname_LF_dnd }).Distinct().OrderBy(o => o.fullname_LF_dnd);
            foreach (var b in x)
            {
                ddl.Items.Add(new ListItem(b.fullname_LF_dnd + " (PK: " + b.pk_participant + ")", b.pk_participant.ToString()));
            }
            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
            catch { }
        }
    }

    //public static void PopulateControl_Participant_Name_DDL(FormView fv, string ddlID, int? iValue, bool bHideNoLastName)
    //{
    //    DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
    //    if (ddl != null)
    //    {
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            if (bHideNoLastName)
    //            {
    //                var a = from b in wDataContext.participants.Where(w => w.lname != null).OrderBy(o => o.lname).ThenBy(o => o.fname)
    //                        select new { b.pk_participant, b.fullname_LF_dnd };
    //                foreach (var c in a)
    //                {
    //                    ddl.Items.Add(new ListItem(c.fullname_LF_dnd, c.pk_participant.ToString()));
    //                }
    //            }
    //            else
    //            {
    //                var a = from b in wDataContext.participants.Where(w => w.lname != null).OrderBy(o => o.lname).ThenBy(o => o.fname)
    //                        select new { b.pk_participant, b.fullname_LF_dnd };
    //                foreach (var c in a)
    //                {
    //                    ddl.Items.Add(new ListItem(c.fullname_LF_dnd, c.pk_participant.ToString()));
    //                }
    //            }
    //            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
    //            if (iValue != null) ddl.SelectedValue = iValue.ToString();
    //        }
    //    }
    //}

    public static void PopulateControl_Participant_Name_ByParticipantType_DDL(FormView fv, string ddlID, string sParticipantType, int? iValue, bool bHideNoLastName)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_Participant_Name_ByParticipantType_DDL_Internal(fv, ddl, sParticipantType, iValue, bHideNoLastName);
    }

    public static void PopulateControl_Participant_Name_ByParticipantType_DDL(DropDownList ddl, string sParticipantType, int? iValue, bool bHideNoLastName)
    {
        if (ddl != null) PopulateControl_Participant_Name_ByParticipantType_DDL_Internal(null, ddl, sParticipantType, iValue, bHideNoLastName);
    }

    private static void PopulateControl_Participant_Name_ByParticipantType_DDL_Internal(FormView fv, DropDownList ddl, string sParticipantType, int? iValue, bool bHideNoLastName)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            if (bHideNoLastName)
            {
                var x = wDataContext.participants.Where(w => w.lname != null && w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType)).OrderBy(o => o.fullname_LF_dnd).Select(s => new { s.pk_participant, s.fullname_LF_dnd });
                foreach (var c in x)
                {
                    ddl.Items.Add(new ListItem(c.fullname_LF_dnd, c.pk_participant.ToString()));
                }
            }
            else
            {
                var x = wDataContext.participants.Where(w => w.participantTypes.Any(a => a.fk_participantType_code == sParticipantType)).OrderBy(o => o.fullname_LF_dnd).Select(s => new { s.pk_participant, s.fullname_LF_dnd });
                foreach (var c in x)
                {
                    ddl.Items.Add(new ListItem(c.fullname_LF_dnd, c.pk_participant.ToString()));
                }
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    public static void PopulateControl_Participant_Org_DDL(FormView fv, string ddlID, Enum_Participant_Type Enum_Participant_Type, int? iValue)
    {
        DropDownList ddl = fv.FindControl("ddlID") as DropDownList;
        if (ddl != null) PopulateControl_Participant_Org_DDL_Internal(fv, ddl, Enum_Participant_Type, iValue);
    }

    public static void PopulateControl_Participant_Org_DDL(DropDownList ddl, Enum_Participant_Type Enum_Participant_Type, int? iValue)
    {
        if (ddl != null) PopulateControl_Participant_Org_DDL_Internal(null, ddl, Enum_Participant_Type, iValue);
    }

    public static void PopulateControl_Participant_Org_DDL_Internal(FormView fv, DropDownList ddl, Enum_Participant_Type enumParticipantType, int? iValue)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            if (enumParticipantType == Enum_Participant_Type.ALL)
            {
                var a = from b in wDataContext.participants.Where(w => w.fk_organization != null && w.lname == null).OrderBy(o => o.organization.org)
                        select new { b.pk_participant, b.organization.org };
                foreach (var c in a) { ddl.Items.Add(new ListItem(c.org, c.pk_participant.ToString())); }
            }
            else
            {
                var a = from b in wDataContext.participants.Where(w => w.fk_organization != null && w.lname == null && w.participantTypes.First(f => f.fk_participantType_code == enumParticipantType.ToString()).fk_participantType_code == enumParticipantType.ToString()).OrderBy(o => o.organization.org)
                        select new { b.pk_participant, b.organization.org };
                foreach (var c in a) { ddl.Items.Add(new ListItem(c.org, c.pk_participant.ToString())); }
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    public static void PopulateControl_Participant_Name_ByParticipantForestryType_DDL(FormView fv, string ddlID, string sParticipantForestryType, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_Participant_Name_ByParticipantForestryType_DDL_Internal(ddl, sParticipantForestryType, iValue);
    }

    public static void PopulateControl_Participant_Name_ByParticipantForestryType_DDL(DropDownList ddl, string sParticipantForestryType, int? iValue)
    {
        if (ddl != null) PopulateControl_Participant_Name_ByParticipantForestryType_DDL_Internal(ddl, sParticipantForestryType, iValue);
    }

    public static void PopulateControl_Participant_Name_ByParticipantForestryType_DDL_Internal(DropDownList ddl, string sParticipantForestryType, int? iValue)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.participants.Where(w => w.lname != null && w.participantForestryTypes.First(f => f.fk_forestryCode_code == sParticipantForestryType).fk_forestryCode_code == sParticipantForestryType).OrderBy(o => o.lname).ThenBy(o => o.fname).Select(s => new { s.pk_participant, s.fullname_LF_dnd });
            foreach (var c in a)
            {
                ddl.Items.Add(new ListItem(c.fullname_LF_dnd, c.pk_participant.ToString()));
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    #region Participant - Contractor

    public static void PopulateControl_ParticipantContractor_Name_ByParticipantType_DDL(FormView fv, string ddlID, string sParticipantType, int? iValue)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.participantContractors.Where(w => w.participant.lname != null && w.participant.participantTypes.First(f => f.fk_participantType_code == sParticipantType).fk_participantType_code == sParticipantType).OrderBy(o => o.participant.lname).ThenBy(o => o.participant.fname).Select(s => new { s.fk_participant, s.participant.fullname_LF_dnd });
                foreach (var c in a)
                {
                    ddl.Items.Add(new ListItem(c.fullname_LF_dnd, c.fk_participant.ToString()));
                }
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && iValue != null) ddl.SelectedValue = iValue.ToString();
            }
        }
    }

    #endregion

    #endregion

    #region Property

    // sPropertyType { ORGANIZATION, PARTICIPANT, PROPERTY, VENUE }
    // sPropertyLevel { STATE, CITY, ADDRESS, ADDRESSNUMBER }
    public static void PopulateControl_Property_AddressItems_DDL(string sPropertyType, string sPropertyLevel, DropDownList ddl, string sState, string sCity, string sAddress, string value, bool bBindPKToAddressNumber)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                switch (sPropertyType)
                {
                    case "ORGANIZATION":
                        switch (sPropertyLevel)
                        {
                            case "STATE":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.fk_property != null && w.property.state != null).GroupBy(g => g.property.state).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CITY":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.city != "" && w.property.state == sState).GroupBy(g => g.property.city).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSTYPE":
                                ddl.DataTextField = "NAME";
                                ddl.DataValueField = "PK";
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.fk_addressType_code != null && w.property.fk_addressType_code != "" && w.property.city == sCity && w.property.state == sState).Select(s => new { PK = s.property.list_addressType.pk_addressType_code, NAME = s.property.list_addressType.type }).Distinct();
                                break;
                            case "RD":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "RD").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "RR":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "RR").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "HC":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "HC").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "SHWY":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "SHWY").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CRD":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "CRD").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "POB":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.nbr != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "POB").GroupBy(g => g.property.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSNUMBER":
                                ddl.DataSource = wDataContext.organizations.Where(w => w.property.nbr != "" && w.property.state == sState && w.property.city == sCity && w.property.address_base == sAddress).GroupBy(g => g.property.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                        }
                        break;
                    case "PARTICIPANT":
                        switch (sPropertyLevel)
                        {
                            case "STATE":
                                ddl.DataSource = wDataContext.participants.Where(w => w.fk_property != null && w.property.state != null).GroupBy(g => g.property.state).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CITY":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.city != "" && w.property.state == sState).GroupBy(g => g.property.city).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSTYPE":
                                ddl.DataTextField = "NAME";
                                ddl.DataValueField = "PK";
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.fk_addressType_code != null && w.property.fk_addressType_code != "" && w.property.city == sCity && w.property.state == sState).Select(s => new { PK = s.property.list_addressType.pk_addressType_code, NAME = s.property.list_addressType.type }).Distinct();
                                break;
                            case "RD":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "RD").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "RR":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "RR").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "HC":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "HC").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "SHWY":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "SHWY").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CRD":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "CRD").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "POB":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.nbr != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "POB").GroupBy(g => g.property.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSNUMBER":
                                ddl.DataSource = wDataContext.participants.Where(w => w.property.nbr != "" && w.property.state == sState && w.property.city == sCity && w.property.address_base == sAddress).GroupBy(g => g.property.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                        }
                        break;
                    case "VENUE":
                        switch (sPropertyLevel)
                        {
                            case "STATE":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.fk_property != null && w.property.state != null).GroupBy(g => g.property.state).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CITY":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.city != "" && w.property.state == sState).GroupBy(g => g.property.city).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSTYPE":
                                ddl.DataTextField = "NAME";
                                ddl.DataValueField = "PK";
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.fk_addressType_code != null && w.property.fk_addressType_code != "" && w.property.city == sCity && w.property.state == sState).Select(s => new { PK = s.property.list_addressType.pk_addressType_code, NAME = s.property.list_addressType.type }).Distinct();
                                break;
                            case "RD":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "RD").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "RR":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "RR").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "HC":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "HC").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "SHWY":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "SHWY").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CRD":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.address_base != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "CRD").GroupBy(g => g.property.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "POB":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.nbr != "" && w.property.state == sState && w.property.city == sCity && w.property.fk_addressType_code == "POB").GroupBy(g => g.property.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSNUMBER":
                                ddl.DataSource = wDataContext.eventVenues.Where(w => w.property.nbr != "" && w.property.state == sState && w.property.city == sCity && w.property.address_base == sAddress).GroupBy(g => g.property.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                        }
                        break;
                    default:
                        switch (sPropertyLevel)
                        {
                            case "STATE":
                                ddl.DataSource = wDataContext.properties.Where(w => w.state != "" && w.state != null).GroupBy(g => g.state).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CITY":
                                ddl.DataSource = wDataContext.properties.Where(w => w.city != "" && w.city != null && w.state == sState).GroupBy(g => g.city).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSTYPE":
                                ddl.DataTextField = "NAME";
                                ddl.DataValueField = "PK";
                                ddl.DataSource = wDataContext.properties.Where(w => w.fk_addressType_code != null && w.fk_addressType_code != "" && w.city == sCity && w.state == sState).Select(s => new { PK = s.list_addressType.pk_addressType_code, NAME = s.list_addressType.type }).Distinct();
                                break;
                            case "RD":
                                ddl.DataSource = wDataContext.properties.Where(w => w.address_base != "" && w.state == sState && w.city == sCity && w.fk_addressType_code == "RD").GroupBy(g => g.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "RR":
                                ddl.DataSource = wDataContext.properties.Where(w => w.address_base != "" && w.state == sState && w.city == sCity && w.fk_addressType_code == "RR").GroupBy(g => g.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "HC":
                                ddl.DataSource = wDataContext.properties.Where(w => w.address_base != "" && w.state == sState && w.city == sCity && w.fk_addressType_code == "HC").GroupBy(g => g.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "SHWY":
                                ddl.DataSource = wDataContext.properties.Where(w => w.address_base != "" && w.state == sState && w.city == sCity && w.fk_addressType_code == "SHWY").GroupBy(g => g.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "CRD":
                                ddl.DataSource = wDataContext.properties.Where(w => w.address_base != "" && w.state == sState && w.city == sCity && w.fk_addressType_code == "CRD").GroupBy(g => g.address_base).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "POB":
                                if (bBindPKToAddressNumber)
                                {
                                    ddl.DataTextField = "nbr";
                                    ddl.DataValueField = "pk_property";
                                    ddl.DataSource = wDataContext.properties.Where(w => w.nbr != "" && w.state == sState && w.city == sCity && w.fk_addressType_code == "POB").OrderBy(o => o.nbr).Select(s => new { s.pk_property, s.nbr });
                                }
                                else ddl.DataSource = wDataContext.properties.Where(w => w.nbr != "" && w.state == sState && w.city == sCity && w.fk_addressType_code == "POB").GroupBy(g => g.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                            case "ADDRESSNUMBER":
                                if (bBindPKToAddressNumber)
                                {
                                    ddl.DataTextField = "nbr";
                                    ddl.DataValueField = "pk_property";
                                    ddl.DataSource = wDataContext.properties.Where(w => w.nbr != "" && w.state == sState && w.city == sCity && w.address_base == sAddress).OrderBy(o => o.nbr).Select(s => new { s.pk_property, s.nbr });
                                }
                                else ddl.DataSource = wDataContext.properties.Where(w => w.nbr != "" && w.state == sState && w.city == sCity && w.address_base == sAddress).GroupBy(g => g.nbr).OrderBy(o => o.Key).Select(s => s.Key);
                                break;
                        }
                        break;
                }
                try { ddl.DataBind(); } catch {}
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                foreach (ListItem item in ddl.Items)
                {
                   	// strings containing "<...>" will cause html parse errors        
                    if (!String.IsNullOrEmpty(item.Text) && item.Text.Contains("<") && item.Text.Contains(">"))
                        ddl.Items.Remove(item);
	            }
                
                if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
                
            }
        }
    }
    public static void ZipCodesFromExistingAddresses(DropDownList ddl)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var addr = wDataContext.properties.Distinct((x, y) => x.fk_zipcode == y.fk_zipcode).Select(s => new { s.fk_zipcode, s.pk_property }).
                OrderBy(o => o.fk_zipcode);
            if (addr.Any() && ddl.Items != null)
            {
                ddl.DataTextField = "fk_zipcode";
                ddl.DataValueField = "pk_property";
                ddl.DataSource = addr;
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", null));
        }
    }
    public static void AddressStartsWith(DropDownList ddl, string _zip, string start)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddl.Items.Clear();
            var addr = wDataContext.properties.Where(w => w.fk_zipcode == _zip).Select(s => s);
            if (addr.Any() && ddl.Items != null)
            {
                ddl.DataTextField = "addressFull";
                ddl.DataValueField = "pk_property";
                if (!string.IsNullOrEmpty(start))
                    ddl.DataSource = addr.Where(w => w.addressFull.StartsWith(start)).Select(s => new {s.pk_property, s.addressFull}).OrderBy(o => o.addressFull);
                else
                    ddl.DataSource = addr.Select(s => new { s.pk_property, s.addressFull }).OrderBy(o => o.addressFull);
                ddl.DataBind();
            }
            ddl.Items.Insert(0,new ListItem("[SELECT]",null));
        }
    }
    public static void PopulateControl_Property_EditInsert_UserControl(UserControl uc, object oProperty)
    {
        //DropDownList ddlState = uc.FindControl("ddlState") as DropDownList;
        //DropDownList ddlCity = uc.FindControl("ddlCity") as DropDownList;
        //DropDownList ddlAddressType = uc.FindControl("ddlAddressType") as DropDownList;
        //DropDownList ddlAddress = uc.FindControl("ddlAddress") as DropDownList;
        //DropDownList ddlAddressNumber = uc.FindControl("ddlAddressNumber") as DropDownList;
        //Label lblBase = uc.FindControl("lblBase") as Label;
        //try
        //{
        //    property p = (property)oProperty;
        //    if (p != null)
        //    {
        //        PopulateControl_Property_AddressItems_DDL("PROPERTY", "STATE", ddlState, null, null, null, p.state, false);
        //        PopulateControl_Property_AddressItems_DDL("PROPERTY", "CITY", ddlCity, p.state, null, null, p.city, false);
        //        PopulateControl_Property_AddressItems_DDL("PROPERTY", "ADDRESSTYPE", ddlAddressType, p.state, p.city, null, p.fk_addressType_code, false);
        //        if (p.fk_addressType_code != "POB")
        //        {
        //            lblBase.Text = ddlAddressType.SelectedItem.Text + ":";
        //            ddlAddress.Visible = true;
        //            PopulateControl_Property_AddressItems_DDL("PROPERTY", p.fk_addressType_code, ddlAddress, p.state, p.city, null, p.address_base, false);
        //            PopulateControl_Property_AddressItems_DDL("PROPERTY", "ADDRESSNUMBER", ddlAddressNumber, p.state, p.city, p.address_base, p.pk_property.ToString(), true);
        //        }
        //        else
        //        {
        //            lblBase.Text = "";
        //            ddlAddress.Items.Clear();
        //            ddlAddress.Visible = false;
        //            PopulateControl_Property_AddressItems_DDL("PROPERTY", "POB", ddlAddressNumber, p.state, p.city, null, p.pk_property.ToString(), true);
        //        }
        //    }
        //    else
        //    {
        //        PopulateControl_Property_AddressItems_DDL("PROPERTY", "STATE", ddlState, null, null, null, "NY", false);
        //        PopulateControl_Property_AddressItems_DDL("PROPERTY", "CITY", ddlCity, "NY", null, null, null, false);
        //    }
        //}
        //catch { }
    }

    public static void PopulateControl_Property_EditInsert_UserControl_Controls(ImageButton ib, Label lbl, HiddenField hf, int iValue)
    {
        //if (lbl != null && hf != null)
        //{
        //    using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        //    {
        //        var a = (from b in wDataContext.properties.Where(w => w.pk_property == iValue) select b).Single();
        //        ib.CommandArgument = a.pk_property.ToString();
        //        ib.Visible = true;
        //        object oAddress2TypeLongName = null;
        //        if (a.list_address2Type != null) oAddress2TypeLongName = a.list_address2Type.longname;
        //        lbl.Text = SpecialText_Global_Address(a.address, a.address2, oAddress2TypeLongName, a.city, a.state, a.fk_zipcode, a.zip4, false);
        //        hf.Value = a.pk_property.ToString();
        //    }
        //}
    }

    #endregion

    #region Tax Parcels

    public static void PopulateControl_TaxParcels_TaxParcel_DDL(FormView fv, string ddlID, int? iTaxParcel)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null) PopulateControl_TaxParcels_TaxParcel_DDL_Internal(ddl, iTaxParcel);
    }

    public static void PopulateControl_TaxParcels_TaxParcel_DDL(DropDownList ddl, int? iTaxParcel)
    {
        if (ddl != null) PopulateControl_TaxParcels_TaxParcel_DDL_Internal(ddl, iTaxParcel);
    }

    private static void PopulateControl_TaxParcels_TaxParcel_DDL_Internal(DropDownList ddl, int? iTaxParcel)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            ddl.DataTextField = "taxParcelID";
            ddl.DataValueField = "pk_taxParcel";
            ddl.DataSource = wDataContext.taxParcels.OrderBy(o => o.taxParcelID).Select(s => new { s.pk_taxParcel, s.taxParcelID });
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iTaxParcel != null) ddl.SelectedValue = iTaxParcel.ToString();
        }
    }

    public static void PopulateControl_TaxParcels_TaxParcelByFarmBusiness_DDL(DropDownList ddl, int iFarmBusinessPK, int? iTaxParcel)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.farmBusinessTaxParcels.Where(w => w.fk_farmBusiness == iFarmBusinessPK).OrderBy(o => o.taxParcel.taxParcelID).Select(s => new { s.pk_farmBusinessTaxParcel, s.taxParcel.taxParcelID });
                ddl.DataTextField = "taxParcelID";
                ddl.DataValueField = "pk_farmBusinessTaxParcel";
                ddl.DataSource = a;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if (iTaxParcel != null) ddl.SelectedValue = iTaxParcel.ToString();
            }
        }
    }

    #endregion

    #region Venue

    public static void PopulateControl_Venue_Location_DDL(DropDownList ddl, int? iVenue, Enum_Participant_Type Enum_Participant_Type, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "location";
                ddl.DataValueField = "pk_eventVenue";
                var a = wac.eventVenues.OrderBy(o => o.location).Select(s => new { s.pk_eventVenue, s.location, s.eventVenueTypes });
                string sEnum_Participant_Type = Enum_Participant_Type.ToString();
                a = a.Where(w => w.eventVenueTypes.First(f => f.fk_participantType_code == sEnum_Participant_Type).fk_participantType_code == sEnum_Participant_Type);
                ddl.DataSource = a;
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iVenue != null) ddl.SelectedValue = iVenue.ToString(); }
                catch { }
            }
        }
    }

    #endregion

    #endregion

    #region Database Functions

    #region Agriculture

    //public static IQueryable<form_wfp3_paymentBMP_pctCalcResult> DatabaseFunction_Agriculture_WFP3_PaymentBMP_PercentCalc(int? iPK_Form_WFP3_BMP, decimal? dPK_BMPPractice, decimal? dPercent, decimal? dRetainage)
    //{
    //    IQueryable<form_wfp3_paymentBMP_pctCalcResult> x;
    //    using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //    {
    //        x = wac.form_wfp3_paymentBMP_pctCalc(iPK_Form_WFP3_BMP, dPK_BMPPractice, dPercent, dRetainage);
    //    }
    //    return x;
    //}

    public static IQueryable<supplementalAgreement_get_activityResult> DatabaseFunction_Agriculture_SupplementalAgreementActivity(int? iPK_SupplementalAgreement)
    {
        IQueryable<supplementalAgreement_get_activityResult> x;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            x = wac.supplementalAgreement_get_activity(iPK_SupplementalAgreement);
        }
        return x;
    }

    public static string DatabaseFunction_Agriculture_BMP_GetBalance(object oPK_BMP, bool bFormatCurrency)
    {
        string s = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                decimal? d = wDataContext.bmp_ag_get_balance(Convert.ToInt32(oPK_BMP), "N");
                if (d != null)
                {
                    if (bFormatCurrency) s = Format_Global_Currency(d);
                    else s = d.ToString();
                }
            }
        }
        catch { }
        return s;
    }

    public static string DatabaseFunction_Agriculture_BMP_GetFunding(object oPK_BMP, string sAgencyCode)
    {
        string s = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                decimal? d = wDataContext.bmp_ag_get_funding(Convert.ToInt32(oPK_BMP), sAgencyCode);
                if (d != null) s = Format_Global_Currency(d);
            }
        }
        catch { }
        return s;
    }

    public static string DatabaseFunction_Agriculture_BMP_GetFunding_AgencySource(object oPK_BMP, string sAgencyCode, string sSourceCode)
    {
        string s = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                decimal? d = wDataContext.bmp_ag_get_funding_agencySource(Convert.ToInt32(oPK_BMP), sAgencyCode, sSourceCode);
                if (d != null) s = Format_Global_Currency(d);
            }
        }
        catch { }
        return s;
    }

    public static string DatabaseFunction_Agriculture_BMP_GetCompletedDate(object oPK_BMP_AG)
    {
        string s = string.Empty;
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                int? i = Convert.ToInt32(oPK_BMP_AG);
                var x = wac.bmp_ag_get_completedDate(i);
                if (x != null) s = Convert.ToDateTime(x).ToShortDateString();
            }
        }
        catch { }
        return s;
    }

    public static string DatabaseFunction_Agriculture_BMP_GetImplementation(object oBMPAgPK, object oYear, Enum_Ag_BMP_Implementation enumImplementation)
    {
        string s = string.Empty;
        
        int? iBMPAgPK = null;
        short? shYear = null;
        try
        {
            iBMPAgPK = Convert.ToInt32(oBMPAgPK);
            shYear = Convert.ToInt16(oYear);
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                IQueryable<bmp_ag_implementation_getResult> r = wDataContext.bmp_ag_implementation_get(iBMPAgPK, shYear);
                if (r.Count() == 1)
                {
                    switch (enumImplementation)
                    {
                        case Enum_Ag_BMP_Implementation.CompletedDate: s = Format_Global_Date(r.Single().completed_date_workload); break;
                        case Enum_Ag_BMP_Implementation.CompletedUnits: s = Format_Global_StringFromObject(r.Single().units_completed_workload) + " " + r.Single().fk_unit_code; break;
                    }
                }
            }
        }
        catch { }

        return s;
    }

    public static string DatabaseFunction_Agriculture_BMP_GetPayment(object oBMPAgPK)
    {
        string s = string.Empty;
        decimal? d = null;
        int? iBMPAgPK = null;
        try
        {
            iBMPAgPK = Convert.ToInt32(oBMPAgPK);
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                d = wDataContext.bmp_ag_get_payment(iBMPAgPK);
            }
        }
        catch { }

        return Format_Global_Currency(d);
    }
    
    public static string DatabaseFunction_Agriculture_BMP_GetPaymentByStatus(object oBMPAgPK, string sStatus)
    {
        string s = string.Empty;
        decimal? d = null;
        int? iBMPAgPK = null;
        try
        {
            iBMPAgPK = Convert.ToInt32(oBMPAgPK);
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                d = wDataContext.bmp_ag_get_payment_byStatus(iBMPAgPK, sStatus);
            }
        }
        catch { }

        return Format_Global_Currency(d);
    }

    public static string DatabaseFunction_Agriculture_BMP_GetPaymentDateLast(object oPK_BMP)
    {
        string s = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                DateTime? dt = wDataContext.bmp_ag_get_paymentDateLast(Convert.ToInt32(oPK_BMP));
                if (dt != null) s = Format_Global_Date(dt);
            }
        }
        catch { }
        return s;
    }

    public static string DatabaseFunction_BMP_getWorkloadProject(object fk_farmBusinessWLProject)
    {
        string s = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var p = wDataContext.farmBusinessWLProjects.Where(w => w.pk_farmBusinessWLProject == Convert.ToInt32(fk_farmBusinessWLProject));
                s = p.FirstOrDefault().ImplementationProject.ToString();
            }
        }
        catch { }
        return s;
    }
    public static IEnumerable<bmp_ag_grid_allResult> DataBaseFunction_Agriculture_BMP_Grid_All(object oPK_FarmBusiness, int?[] iProgrammaticCodes)
    {
        IEnumerable<bmp_ag_grid_allResult> r = null;
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                int i = Convert.ToInt32(oPK_FarmBusiness);
                r = wac.bmp_ag_grid_all(i).AsEnumerable()
                    .Where(w => !iProgrammaticCodes.Contains(w.fk_programmaticRecord_code))
                    .OrderBy(v => v.IsIRC)
                    .ThenBy(o => o.pollutant_category_sortPosition)
                    .ThenBy(o => o.Bmp)
                    .ThenBy(o => o.QualifierCode)
                    .ThenBy(o => o.QualifierVersion)
                    .ToList(); 
            }
            
        }
        catch { }
        return r;
    }

    public static decimal? DatabaseFunction_Agriculture_Tier1_Animal_GetAU(object oFarmBusinessPK)
    {
        decimal? d = 0;
        try
        {
            int i = Convert.ToInt32(oFarmBusinessPK);
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                d = wDataContext.farmBusinessTier1Animal_get_AU(i, null);
            }
        }
        catch { }
        return d;
    }

    public static string DatabaseFunction_Agriculture_WFP3_GetBalance(object oPK_WFP3)
    {
        string s = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int? i = Convert.ToInt32(oPK_WFP3);
                //var a = wDataContext.form_wfp3_grid_all().Where(w => w.pk_form_wfp3 == i);
                //decimal? d = null;
                //if (a.Count() == 1) d = a.Single().balance;

                decimal? d = wDataContext.form_wfp3_get_balance(Convert.ToInt32(oPK_WFP3));
                if (d != null) s = Format_Global_Currency(d);
            }
        }
        catch { }
        return s;
    }

    public static string DatabaseFunction_Agriculture_SupplementalAgreement_TaxParcel_GetFarmBMPs(object oPK_SATP)
    {
        string s = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            if (oPK_SATP != null)
            {
                int? iPK_SATP = Convert.ToInt32(oPK_SATP);
                s = wac.supplementalAgreementTaxParcel_get_FarmBMPs(iPK_SATP);
            }
        }
        return s;
    }

    public static void DatabaseFunction_Agriculture_View_BMPAG_Financial_Get_DDL(DropDownList ddl, int iPK_BMP_Ag, int iPK_Form_WFP3, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.vw_bmp_ag_financial_get(iPK_BMP_Ag, iPK_Form_WFP3);
                foreach (var c in x)
                {
                    string sBoundValue = c.pk_bmp_ag + "|" + c.balance;
                    string sBalance = c.bmp_nbr + ": " + Format_Global_Currency(c.balance);
                    ddl.Items.Add(new ListItem(sBalance, sBoundValue));
                }
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
        }
    }

    #endregion

    #region Forestry

    //public static DateTime? DatabaseFunction_Forestry_FMP_PaidDate(object oPK_ID)
    //{
    //    DateTime? dt = null;
    //    try
    //    {
    //        int iPK_ID = Convert.ToInt32(oPK_ID);
    //        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //        {
    //            var x = wac.forestryFMP_overview_grid(iPK_ID);
    //            if (x.Count() == 1) dt = x.Single().paid;
    //        }
    //    }
    //    catch { }
    //    return dt;
    //}

    //public static DateTime? DatabaseFunction_Forestry_FMP_GetPaymentsPK_PaidDate(object oForestryFMPPK)
    //{
    //    DateTime? dt = null;
    //    try
    //    {
    //        int iForestryFMPPK = Convert.ToInt32(oForestryFMPPK);
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            var a = wDataContext.forestryFMP_get_payments_PK(iForestryFMPPK);
    //            if (a.Count() == 1) dt = a.Single().date;
    //        }
    //    }
    //    catch { }
    //    return dt;
    //}

    //public static decimal? DatabaseFunction_Forestry_FMP_GetPaymentsPK_RiparianAcres(object oForestryFMPPK)
    //{
    //    decimal? d = null;
    //    try
    //    {
    //        int iForestryFMPPK = Convert.ToInt32(oForestryFMPPK);
    //        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
    //        {
    //            var a = wDataContext.forestryFMP_get_payments_PK(iForestryFMPPK);
    //            if (a.Count() == 1) d = a.Single().acre_riparian;
    //        }
    //    }
    //    catch { }
    //    return d;
    //}

    #endregion

    #region Global

    public static string DatabaseFunction_Global_CheckForeignKeyAssignment(int iPK, string sTableToCheck)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                switch (sTableToCheck)
                {
                    case "PROPERTY":
                        i = wDataContext.farmBusinesses.Where(w => w.farmLand.fk_property == iPK).Count();
                        if (i > 0) sb.Append("Number of farm business records tied to this property: " + i + ". ");
                        i = wDataContext.organizations.Where(w => w.fk_property == iPK).Count();
                        if (i > 0) sb.Append("Number of organization records tied to this property: " + i + ". ");
                        i = wDataContext.participantProperties.Where(w => w.fk_property == iPK).Count();
                        if (i > 0) sb.Append("Number of participant property records tied to this property: " + i + ". ");
                        i = wDataContext.eventVenues.Where(w => w.fk_property == iPK).Count();
                        if (i > 0) sb.Append("Number of venue records tied to this property: " + i + ". ");
                        break;
                }
            }
        }
        catch { }
        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            sb.Insert(0, "You cannot delete because of the following: ");
            sb.Append("See bottom of record for listing of what other section records are tied to this record. ");
        }
        return sb.ToString();
    }

    #endregion

    #region Tax Parcel

    public static string DatabaseFunction_TaxParcel_GetSBL_By_SWIS_PrintKey(object oPrintKey, object oSWIS)
    {
        string sSBL = null;
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                string sPrintKey = oPrintKey.ToString().Trim();
                string sSWIS = oSWIS.ToString().Trim();
                sSBL = wDataContext.taxParcelReferences.Where(w => w.printKey.Trim() == sPrintKey && w.fk_list_swis.Trim() == sSWIS).Select(s => s.SBL).Single();
            }
            catch { }
        }
        return sSBL;
    }

    public static List<string> DatabaseFunction_TaxParcel_DefineTaxParcel_By_SWIS_SBL_PrintKey(string sSWIS, string sSBL, string sPrintKey)
    {
        List<string> l = new List<string>();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {

            if (!string.IsNullOrEmpty(sSBL))
            {
                var aSBL = wDataContext.taxParcelReferences.Where(w => w.SBL.Trim() == sSBL && w.fk_list_swis.Trim() == sSWIS).Select(s => new { s.printKey });
                if (aSBL.Count() == 1) l = DatabaseFunction_TaxParcel_PopulateListString(sSWIS, sSBL, aSBL.Single().printKey);
                else l = DatabaseFunction_TaxParcel_PopulateListString(sSWIS, "", "");
            }
            else if (!string.IsNullOrEmpty(sPrintKey))
            {
                var aPrintKey = wDataContext.taxParcelReferences.Where(w => w.printKey.Trim() == sPrintKey && w.fk_list_swis.Trim() == sSWIS).Select(s => new { s.SBL });
                if (aPrintKey.Count() == 1) l = DatabaseFunction_TaxParcel_PopulateListString(sSWIS, aPrintKey.Single().SBL, sPrintKey);
                else l = DatabaseFunction_TaxParcel_PopulateListString(sSWIS, "", sPrintKey);
            }
            else l = DatabaseFunction_TaxParcel_PopulateListString(sSWIS, "", "");
        }
        return l;
    }

    private static List<string> DatabaseFunction_TaxParcel_PopulateListString(string sSWIS, string sSBL, string sPrintKey)
    {
        List<string> l = new List<string>();
        l.Add(sSWIS);
        l.Add(sSBL);
        l.Add(sPrintKey);
        return l;
    }

    #endregion

    #endregion

    #region WACEnumerations

    public enum Enum_Ag_BMP_Implementation { CompletedUnits, CompletedDate }

    public enum Enum_Agriculture_SupplementalAgreement_View_StringReturned { FarmID_BMPs, PK_FarmBusiness }

    public enum Enum_Color { BLUE, GREEN, PURPLE, RED, YELLOW, ORANGE }

    public enum Enum_FAME_Sections { AG, TP }

    public enum Enum_Forestry_BMPContractor { QF, TH }

    public enum Enum_Forestry_MaxAmount { BMP_ROAD_MAX, BMP_ROAD_MAX_TLC, BMP_STREAM_MAX, BMP_STREAM_MAX_TLC }

    public enum Enum_Forestry_Section { BMP, FMP, MAP }

    public enum Enum_Forestry_SectionType { ROAD, STREAM }

    public enum Enum_Number_Type { AREACODE, PHONENUMBER }

    public enum Enum_Participant_Forms { PERSON, ORGANIZATION, ALL }

    public enum Enum_Participant_Type { ALL, A, F, E, M }

    public enum Enum_NavigateURL_Special { AG_2_SA, SA_2_AG }

    public enum Enum_Security_UserObjectCustom { A_ABC, A_PLAN, A_WL, HR_WAC }

    #endregion

    #region Formatting

    #region Color

    public static string Format_Color_General_ExpiredDates(object oPassedDate, object oPlusYears)
    {
        string s = string.Empty;
        string sDate = Format_Global_Date(oPassedDate);
        DateTime? dtPassedDate = null;
        DateTime dtCheckDate = DateTime.Now;
        int iPlusYears = 0;
        try
        {
            iPlusYears = Convert.ToInt32(oPlusYears);
            dtPassedDate = Convert.ToDateTime(oPassedDate).AddYears(iPlusYears);
            if (!string.IsNullOrEmpty(sDate))
            {
                if (dtPassedDate < dtCheckDate) s = "<div style='color:red;'>" + sDate + " (expired)</div>";
                else s = sDate;
            }            
        }
        catch { s = sDate; }

        return s;
    }

    public static string Format_Color_Global_ColorText(object oText, Enum_Color eColor)
    {
        string s = string.Empty;
        if (oText != null)
        {
            try
            {
                switch (eColor)
                {
                    case Enum_Color.BLUE: s = "<font color='blue'>" + oText.ToString() + "</font>"; break;
                    case Enum_Color.GREEN: s = "<font color='green'>" + oText.ToString() + "</font>"; break;
                    case Enum_Color.PURPLE: s = "<font color='purple'>" + oText.ToString() + "</font>"; break;
                    case Enum_Color.RED: s = "<font color='red'>" + oText.ToString() + "</font>"; break;
                    case Enum_Color.YELLOW: s = "<font color='yellow'>" + oText.ToString() + "</font>"; break;
                    case Enum_Color.ORANGE: s = "<font color='Orange'>" + oText.ToString() + "</font>"; break;
                }
            }
            catch { }
        }
        return s;
    }

    public static string Format_Color_YesNo(object oText, object oYN, Enum_Color eColorYes, Enum_Color eColorNo, bool bColorYes, bool bColorNo)
    {
        string s = string.Empty;
        if (oText != null)
        {
            s = oText.ToString();
            if (oYN != null)
            {
                if (oYN.ToString() == "Y")
                {
                    if (bColorYes) s = Format_Color_Global_ColorText(s, eColorYes);
                }
                if (oYN.ToString() == "N")
                {
                    if (bColorNo) s = Format_Color_Global_ColorText(s, eColorNo);
                }
            }
        }
        return s;
    }

    public static string Format_Color_Agriculture_WFP0_NotApproved(string s)
    {
        if (string.IsNullOrEmpty(s)) s = Format_Color_Global_ColorText("Not Approved", Enum_Color.RED);
        return s;
    }

    // The following method is still used in ForestryBMP but is not shown because the tabs have been hidden. However, TLC was moved to the forestryBMP table 
    // so if this method is going to be used, it must be modifed to obtain the global TLC from the forestryBMP table.
    public static string Format_Color_Forestry_CurrencyLimits(object oAmount, Enum_Forestry_SectionType enumForestrySectionType)
    {
        string s = Format_Global_Currency(oAmount);
        //decimal? d = null;
        //decimal? dAmount = null;
        //string sTLC = string.Empty;
        //try 
        //{
        //    forestryBMP FORESTRYBMP = (forestryBMP)oForestryBMP;
        //    sTLC = FORESTRYBMP.TLC;
        //    switch (enumForestrySectionType)
        //    {
        //        case Enum_Forestry_SectionType.ROAD:
        //            if (sTLC == "Y") d = SpecialQuery_Variable_Forestry_MaxAmount(Enum_Forestry_MaxAmount.BMP_ROAD_MAX_TLC);
        //            else d = SpecialQuery_Variable_Forestry_MaxAmount(Enum_Forestry_MaxAmount.BMP_ROAD_MAX);
        //            break;
        //        case Enum_Forestry_SectionType.STREAM:
        //            if (sTLC == "Y") d = SpecialQuery_Variable_Forestry_MaxAmount(Enum_Forestry_MaxAmount.BMP_STREAM_MAX_TLC);
        //            else d = SpecialQuery_Variable_Forestry_MaxAmount(Enum_Forestry_MaxAmount.BMP_STREAM_MAX);
        //            break;
        //    }
        //    try { dAmount = Convert.ToDecimal(oAmount); }
        //    catch { }
        //    if (dAmount != null) if (dAmount > d) s = "<div style='color:red;'>" + Format_Global_Currency(oAmount) + "</div>";
        //}
        //catch { }
        return s;
    }

    public static string Format_Color_TaxParcel_SBL(object oSBL)
    {
        string s = string.Empty;
        try
        {
            string sSBL = oSBL.ToString();
            s = sSBL;
        }
        catch { s = Format_Color_Global_ColorText("Invalid TaxJurisdictionDP or Tax Parcel ID", Enum_Color.RED); }
        return s;
    }

    public static string Format_Color_AlternatingColors(ref int i, string sColor1, string sColor2)
    {
        if (i % 2 == 0) return sColor1;
        else return sColor2;
    }

    #endregion

    #region Global

    public static string Format_Global_Currency(object oCurrency)
    {
        // FORMAT: $1,000.00
        string s = string.Empty;
        try { s = oCurrency.ToString(); }
        catch { }
        double d = -1;
        try
        {
            d = Convert.ToDouble(oCurrency);
            s = String.Format("{0:C}", d);
        }
        catch { }
        return s;
    }

    public static string Format_Global_Date(object oDateTime)
    {
        // FORMAT: yyyy/mm/dd
        string s = string.Empty;
        
        try
        {
            DateTime dt = Convert.ToDateTime(oDateTime);
            if (dt.Year < 1990)
                throw new Exception();
            s = dt.ToString("MM/dd/yyyy");
            //s = Convert.ToDateTime(oDateTime).ToShortDateString();
        //    string sY = s.Substring(s.LastIndexOf("/") + 1);
        //    string sM = s.Substring(0, s.IndexOf("/"));
        //    if (sM.Length == 1) sM = "0" + sM;
        //    string sD = s.Substring(s.IndexOf("/") + 1);
        //    sD = sD.Substring(0, sD.IndexOf("/"));
        //    if (sD.Length == 1) sD = "0" + sD;
        //    s = String.Format("{0}/{1}/{2}", sY, sM, sD);

        //    try
        //    {
        //        int iY = Convert.ToInt32(sY);
        //        if (iY < 1900) s = string.Empty;
        //    }
        //    catch { }
        }
        catch { s = string.Empty; }
        return s;
    }

    public static string Format_Global_Email_MailTo(object oEmail)
    {
        string s = null;
        string sEmail = string.Empty;
        try
        {
            sEmail = oEmail.ToString();
            if (sEmail.Contains("@")) if (!string.IsNullOrEmpty(sEmail)) s = "<a href='mailto:" + sEmail + "'>" + sEmail + "</a>";
        }
        catch { }
        return s;
    }

    //public static string Format_Global_Participant(string sLastName, string sFirstName, string sOrganization)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    if (!string.IsNullOrEmpty(sLastName)) sb.Append(sLastName);
    //    if (!string.IsNullOrEmpty(sFirstName))
    //    {
    //        if (!string.IsNullOrEmpty(sb.ToString())) sb.Append(", " + sFirstName);
    //        else sb.Append(sFirstName);
    //    }
    //    if (!string.IsNullOrEmpty(sOrganization))
    //    {
    //        if (!string.IsNullOrEmpty(sb.ToString())) sb.Append(" [" + sOrganization + "]");
    //        else sb.Append("[" + sOrganization + "]");
    //    }
    //    return sb.ToString();
    //}

    //public static string Format_Global_Participant(object oLastName, object oFirstName, object oOrganizationObject, bool bShowFirstNameFirst)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    if (oLastName != null)
    //    {
    //        if (oFirstName != null)
    //        {
    //            if (bShowFirstNameFirst) sb.Append(oFirstName.ToString() + " " + oLastName.ToString());
    //            else sb.Append(oLastName.ToString() + ", " + oFirstName.ToString());
    //        }
    //        else sb.Append(oLastName.ToString());
    //    }
    //    else if (oFirstName != null) sb.Append(oFirstName.ToString());

    //    if (oOrganizationObject != null)
    //    {
    //        organization organizationObject = (organization)oOrganizationObject;
    //        if (!string.IsNullOrEmpty(organizationObject.org))
    //        {
    //            if (string.IsNullOrEmpty(sb.ToString())) sb.Append(organizationObject.org.ToString());
    //            else sb.Append(" [" + organizationObject.org.ToString() + "]");
    //        }
    //    }

    //    return sb.ToString();
    //}

    public static string Format_Global_PhoneNumber(object oPhone)
    {
        string sPhone = string.Empty;

        try { sPhone = oPhone.ToString(); }
        catch { }

        try { sPhone = String.Format("({0}) {1}-{2}", sPhone.Substring(0, 3), sPhone.Substring(3, 3), sPhone.Substring(6)); }
        catch { }

        return sPhone;
    }

    public static string Format_Global_PhoneNumberPlusExtension(object oPhone, object oExtension)
    {
        string sPhone = string.Empty;

        try { sPhone = oPhone.ToString(); }
        catch { }

        try { sPhone = String.Format("({0}) {1}-{2}", sPhone.Substring(0, 3), sPhone.Substring(3, 3), sPhone.Substring(6)); }
        catch { }

        try
        {
            string sPhoneExt = oExtension.ToString();
            if (!string.IsNullOrEmpty(sPhoneExt)) sPhone = sPhone + " Ext. " + sPhoneExt; 
        }
        catch { }

        return sPhone;
    }

    public static string Format_Global_PhoneNumberSeparateAreaCode(object oAreaCode, object oPhoneNumber)
    {
        string s = string.Empty;
        string sAreaCode = string.Empty;
        string sPhoneNumber = string.Empty;

        try { sAreaCode = oAreaCode.ToString().Trim(); }
        catch { }

        try { sPhoneNumber = oPhoneNumber.ToString().Trim(); }
        catch { }

        if (!string.IsNullOrEmpty(sAreaCode) && !string.IsNullOrEmpty(sPhoneNumber))
        {
            try { s = String.Format("({0}) {1}-{2}", sAreaCode, sPhoneNumber.Substring(0, 3), sPhoneNumber.Substring(3)); }
            catch { s = sAreaCode + sPhoneNumber; }
        }
        else if (!string.IsNullOrEmpty(sPhoneNumber))
        {
            try { s = String.Format("{1}-{2}", sPhoneNumber.Substring(0, 3), sPhoneNumber.Substring(3)); }
            catch { s = sPhoneNumber; }
        }

        if (sPhoneNumber.Length != 7) s += " (Invalid Phone Number)";

        return s;
    }

    public static string Format_Global_PhoneNumber_StripToNumbers(object oNumber, Enum_Number_Type Enum_Number_Type)
    {
        string s = null;

        switch (Enum_Number_Type)
        {
            case Enum_Number_Type.AREACODE:
                string sAreaCode = string.Empty;
                try
                {
                    sAreaCode = oNumber.ToString();
                    foreach (char c in sAreaCode) { if (Char.IsNumber(c)) s += c.ToString(); }
                    if (s.Length > 3) s = s.Substring(0, 3);
                    if (s.Length < 3)
                    {
                        do
                        {
                            s = s.Insert(0, "0");
                        }
                        while (s.Length != 3);
                    }
                }
                catch { }
                break;
            case Enum_Number_Type.PHONENUMBER:
                string sPhoneNumber = string.Empty;
                try
                {
                    sPhoneNumber = oNumber.ToString();
                    foreach (char c in sPhoneNumber) { if (Char.IsNumber(c)) s += c.ToString(); }
                    if (s.Length > 7) s = s.Substring(0, 7);
                    if (s.Length < 7)
                    {
                        do
                        {
                            s = s.Insert(0, "0");
                        }
                        while (s.Length != 7);
                    }
                }
                catch { }
                break;
        }
        return s;
    }

    public static string Format_Global_StringFromObject(object oObject)
    {
        string s = string.Empty;
        try { s = oObject.ToString(); }
        catch { }
        return s;
    }

    public static string Format_Global_StringLengthRestriction(string sText, int iCharLimit)
    {
        //string s = sText.Trim();
        //if (sText.Length > iCharLimit) s = sText.Substring(0, iCharLimit);
        //return s;
        return sText;
    }

    public static string Format_Global_URL(object oURL)
    {
        string sURL = string.Empty;
        if (oURL != null)
        {
            try
            {
                sURL = oURL.ToString();
                if (!string.IsNullOrEmpty(sURL))
                {
                    if (sURL.IndexOf("www") == -1) sURL = "www." + sURL;
                    if (sURL.IndexOf("http://") == -1 && sURL.IndexOf("https://") == -1) sURL = "http://" + sURL;
                    if (Validation_isValidUrl(ref sURL)) sURL = "<a href='" + sURL + "' target='_blank'>" + sURL + "</a>";
                    else sURL = oURL.ToString() + " (Not a valid URL)";
                }
            }
            catch { }
        }
        return sURL;
    }

    public static string Format_CurrentPosition(object o)
    {
        // used from HR module 
        // position is current if finish_date is null or finish_date > now
        string s = "Yes";
        try
        {
            DateTime finishDate = (DateTime)o;
            s = DateTime.Now.CompareTo(finishDate) < 0 ? "Yes" : "No";
        }
        catch { }
        return s;
    }

    public static string Format_Global_YesNo(object oYN)
    {
        // FORMAT: Yes, No
        string s = "Unknown";
        try
        {
            s = oYN.ToString();
            if (s.ToUpper() == "Y") s = "Yes";
            if (s.ToUpper() == "N") s = "No";
        }
        catch { }
        return s;
    }

    #endregion

    #region Venue

    public static string Format_Venue_EventVenueTypes(object oEventVenueTypes)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            EntitySet<eventVenueType> es_evt = (EntitySet<eventVenueType>)oEventVenueTypes;
            foreach (eventVenueType evt in es_evt.OrderBy(o => o.list_participantType.participantType))
            {
                if (!string.IsNullOrEmpty(sb.ToString())) sb.Append("; " + evt.list_participantType.participantType);
                else sb.Append(evt.list_participantType.participantType);
            }
        }
        catch { sb.Append("Error parsing Event Venue Types."); }
        
        return sb.ToString();
    }

    #endregion

    #endregion

    #region Math

    public static int Math_CountRecords(ref int i)
    {
        i += 1;
        return i;
    }

    public static decimal Math_Round(object oNumber, int iDecimalPlaces)
    {
        decimal d = 0;
        try
        {
            d = Convert.ToDecimal(oNumber);
            d = Math.Round(d, iDecimalPlaces);
        }
        catch { }
        return d;
    }

    #endregion

    #region Ordering

    #region Agriculture

    public static IOrderedEnumerable<asrAg> Order_Agriculture_ASR(object oASRAg)
    {
        EntitySet<asrAg> es = (EntitySet<asrAg>)oASRAg;
        var a = es.OrderByDescending(o => o.year);
        return a;
    }

    public static IOrderedEnumerable<bmp_ag> Order_Agriculture_BMP_AG_PollutantCategory_BMPNumber(object oBMPAg)
    {
        EntitySet<bmp_ag> es = (EntitySet<bmp_ag>)oBMPAg;
        var a = es.OrderBy(o => o.list_pollutant_category.sort_position).ThenBy(o => o.CompositBmpNum);
        return a;
    }

    public static IOrderedEnumerable<farmBusinessContact> Order_Agriculture_FarmBusinessContact(object oFarmBusinessContact)
    {
        EntitySet<farmBusinessContact> es = (EntitySet<farmBusinessContact>)oFarmBusinessContact;
        var a = es.OrderBy(o => o.participant.fullname_LF_dnd);
        return a;
    }

    public static IOrderedEnumerable<farmBusinessMail> Order_Agriculture_FarmBusinessMail(object oFarmBusinessMail)
    {
        EntitySet<farmBusinessMail> es = (EntitySet<farmBusinessMail>)oFarmBusinessMail;
        var a = es.OrderBy(o => o.participant.fullname_LF_dnd);
        return a;
    }

    public static IOrderedEnumerable<farmBusinessOperator> Order_Agriculture_FarmBusinessOperator(object oFarmBusinessOperator)
    {
        EntitySet<farmBusinessOperator> es = (EntitySet<farmBusinessOperator>)oFarmBusinessOperator;
        var a = es.OrderBy(o => o.participant.fullname_LF_dnd);
        return a;
    }

    public static IOrderedEnumerable<farmBusinessOwner> Order_Agriculture_FarmBusinessOwner(object oFarmBusinessOwner)
    {
        EntitySet<farmBusinessOwner> es = (EntitySet<farmBusinessOwner>)oFarmBusinessOwner;
        var a = es.OrderBy(o => o.participant.fullname_LF_dnd);
        return a;
    }

    public static IOrderedEnumerable<farmBusinessPlanner> Order_Agriculture_FarmBusinessPlanner_designerEngineer(object oFarmBusinessPlanner)
    {
        EntitySet<farmBusinessPlanner> es = (EntitySet<farmBusinessPlanner>)oFarmBusinessPlanner;
        var a = es.OrderBy(o => o.list_designerEngineer.designerEngineer);
        return a;
    }

    public static IOrderedEnumerable<farmBusinessStatus> Order_Agriculture_FarmBusinessStatus_Chronology(object oFarmBusinessStatus)
    {
        EntitySet<farmBusinessStatus> es = (EntitySet<farmBusinessStatus>)oFarmBusinessStatus;
        var a = es.OrderByDescending(o => o.date);
        return a;
    }

    public static IOrderedEnumerable<farmBusinessType> Order_Agriculture_FarmBusinessType_Type(object oFarmBusinessType)
    {
        EntitySet<farmBusinessType> es = (EntitySet<farmBusinessType>)oFarmBusinessType;
        var a = es.OrderBy(o => o.list_farmType.farmType);
        return a;
    }
    public static string BMPGrouping(object _grouping)
    {
        int grouping = -1;
        try
        {
            Convert.ToInt32(_grouping);
        }
        catch (Exception)
        { // just leave grouping set to -1;
        }
        
        if (grouping < 0 || grouping > 9)
            return string.Empty;
        else
            return grouping.ToString();
    }
    public static IOrderedEnumerable<farmBusinessWLProjectBMP> Order_Agriculture_farmBusinessWLProjectBMP_BMPNumber(object wlProjectBMPs)//, object wlProjects)
    {
        EntitySet<farmBusinessWLProjectBMP> bmp = (EntitySet<farmBusinessWLProjectBMP>)wlProjectBMPs;
      //  EntitySet<farmBusinessWLProject> wlp = (EntitySet<farmBusinessWLProject>)wlProjects;
        var a = bmp
            //bmp.Join(wlp,
            //b => b.fk_farmBusinessWLProject,
            //p => p.pk_farmBusinessWLProject,
            //(b, p) => new { b = b, p = p })
            //.Where(w => w.p.ImplementationProject > 0)
            //.Select(s => s.b)
            .OrderBy(o => o.bmp_ag.CompositBmpNum);
            

        return a;
    }
    //public static IDictionary<string,object> WorkloadGroupBmpList(int fk_farmBusiness)
    //{
    //    using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //    {
    //        //var a = wac.farmBusinessWLProjects.GroupJoin(wac.farmBusinessWLProjectBMPs,                   
    //        //        bmps => fk_farmBusinessWLProject,
    //        //        project => project.pk_farmBusinessWLProject,
    //        //        (project, bmps) => new
    //        //        {
    //        //            projectId = project.pk_farmBusinessWLProject,
    //        //            new => {
    //        //            projectNumber = project.ImplementationProject,
    //        //            bmp = bmps.bmp_ag.CompositBmpNum
    //        //            }
    //        //        }).ToDictionary<int,string)();

    //    }


    //    return null;
    //}
    //public static IOrderedEnumerable<bmp_ag_workloadProgress> Order_Agriculture_BMP_AG_Workload_Progress(object oBMP_AG_Workload_Progress)
    //{
    //    EntitySet<bmp_ag_workloadProgress> es = (EntitySet<bmp_ag_workloadProgress>)oBMP_AG_Workload_Progress;
    //    var a = es.OrderBy(o => o.created);
    //    return a;
    //}

    #endregion

    #region Farm To Market

    public static IOrderedEnumerable<eventDate> Order_FarmToMarket_Event_EventDate(object oEventDates)
    {
        EntitySet<eventDate> es = (EntitySet<eventDate>)oEventDates;
        var a = es.OrderByDescending(o => o.date);
        return a;
    }

    public static IOrderedEnumerable<eventDateRegistrant> Order_FarmToMarket_Event_EventDateRegistrant(object oEventDateRegistrant)
    {
        EntitySet<eventDateRegistrant> es = (EntitySet<eventDateRegistrant>)oEventDateRegistrant;
        var a = es.OrderBy(o => o.eventRegistrant.participant.lname).ThenBy(o => o.eventRegistrant.participant.fname);
        return a;
    }

    public static IOrderedEnumerable<eventDateAttendance> Order_FarmToMarket_Event_EventDateAttendance(object oEventDateAttendance)
    {
        EntitySet<eventDateAttendance> es = (EntitySet<eventDateAttendance>)oEventDateAttendance;
        var a = es.OrderBy(o => o.eventRegistrant.participant.lname).ThenBy(o => o.eventRegistrant.participant.fname);
        return a;
    }

    public static IOrderedEnumerable<eventRegistrant> Order_FarmToMarket_Event_EventRegistrant(object oEventRegistrants)
    {
        EntitySet<eventRegistrant> es = (EntitySet<eventRegistrant>)oEventRegistrants;
        var a = es.OrderBy(o => o.participant.lname).ThenBy(o => o.participant.fname);
        return a;
    }

    #endregion

  

    #region Participant

    public static IOrderedEnumerable<participant> Order_Participant_LastName(object oParticipant)
    {
        EntitySet<participant> es = (EntitySet<participant>)oParticipant;
        var a = es.OrderBy(o => o.lname).ThenBy(o => o.fname);
        return a;
    }

    public static IOrderedEnumerable<participantContractor_vendex> Order_Participant_ContractorVendexes(object oContractorVendexes)
    {
        EntitySet<participantContractor_vendex> es = (EntitySet<participantContractor_vendex>)oContractorVendexes;
        var a = es.OrderBy(o => o.affidavit_yr);
        return a;
    }

    #endregion

    #region Tax Parcel

    public static IOrderedEnumerable<taxParcelOwner> Order_TaxParcel_Owner(object oTaxParcelOwner)
    {
        EntitySet<taxParcelOwner> es = (EntitySet<taxParcelOwner>)oTaxParcelOwner;
        var a = es.OrderBy(o => o.participant.lname).ThenBy(o => o.participant.fname);
        return a;
    }

    #endregion

    #region Venue

    public static IOrderedEnumerable<eventVenueType> Order_Venue_EventVenueTypes(object oEventVenueTypes)
    {
        EntitySet<eventVenueType> es = (EntitySet<eventVenueType>)oEventVenueTypes;
        var a = es.OrderBy(o => o.list_participantType.participantType);
        return a;
    }

    #endregion

    #endregion

    #region Security

    public static bool Security_UserCanPerformAction(object oPK_User, string sAction, string sSection, string sEntity, string sConfigMessage)
    {
        bool bUserCanPerformAction = true;
        if (Convert.ToBoolean(WebConfigurationManager.AppSettings["useSecurityAuthorization"]) == true)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                try
                {
                    int iPK_User = Convert.ToInt32(oPK_User);
                    var a = from b in wDataContext.vw_userRoleObjects.Where(w => w.pk_user == iPK_User && 
                        w.PK_Code == sAction && 
                        (w.dataSector == sEntity || w.fk_sectorWAC_code == sSection || w.dataSector == "All")) select b;
                    if (a.Count() > 0)
                    {
                        int i01 = a.First().pk_user;
                        string s01 = a.First().PK_Code;
                        string s02 = a.First().dataSector;
                    }
                    else bUserCanPerformAction = false;
                }
                catch 
                { 
                    // TODO: SP for sending and email
                    bUserCanPerformAction = false; 
                }
            }
        }
        if (!bUserCanPerformAction) WACAlert.Show(WebConfigurationManager.AppSettings[sConfigMessage], 0);
        return bUserCanPerformAction;
    }

    public static bool Security_UserCanModifyDeleteNote(string Username, string CreatedBy)
    {
        if (Username.Trim().ToUpper() == CreatedBy.Trim().ToUpper())
            return true;
        else
        {
            WACAlert.Show("Only the creator can edit or delete the note. Creator: " + CreatedBy + ", You: " + Username, 0);
            return false;
        }
    }

    public static bool Security_UserCanModifyDeleteNote(object oUsername, string sTable, int iPK)
    {
        bool bUserCanModifyDeleteNote = false;
        string sUsername = string.Empty;
        string sCreatedBy = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                switch (sTable)
                {
                    case "bmp_ag_note":
                        var Vbmp_ag_note = wDataContext.bmp_ag_notes.Where(w => w.pk_bmp_ag_note == iPK).Select(s => s.created_by);
                        if (Vbmp_ag_note.Count() == 1) sCreatedBy = Vbmp_ag_note.Single();
                        break;
                    case "farmBusinessNote":
                        var VfarmBusinessNote = wDataContext.farmBusinessNotes.Where(w => w.pk_farmBusinessNote == iPK).Select(s => s.created_by);
                        if (VfarmBusinessNote.Count() == 1) sCreatedBy = VfarmBusinessNote.Single();
                        break;
                    //case "farmToMarketNote":
                    //    var VfarmToMarketNote = wDataContext.farmToMarketNotes.Where(w => w.pk_farmToMarketNote == iPK).Select(s => s.created_by);
                    //    if (VfarmToMarketNote.Count() == 1) sCreatedBy = VfarmToMarketNote.Single();
                    //    break;
                    //case "forestryBMP_note":
                    //    var VforestryBMP_note = wDataContext.forestryBMP_notes.Where(w => w.pk_forestryBMP_note == iPK).Select(s => s.created_by);
                    //    if (VforestryBMP_note.Count() == 1) sCreatedBy = VforestryBMP_note.Single();
                    //    break;
                    //case "forestryFMP_note":
                    //    var VforestryFMP_note = wDataContext.forestryFMP_notes.Where(w => w.pk_forestryFMP_note == iPK).Select(s => s.created_by);
                    //    if (VforestryFMP_note.Count() == 1) sCreatedBy = VforestryFMP_note.Single();
                    //    break;
                    case "organizationNote":
                        var VorganizationNote = wDataContext.organizationNotes.Where(w => w.pk_organizationNote == iPK).Select(s => s.created_by);
                        if (VorganizationNote.Count() == 1) sCreatedBy = VorganizationNote.Single();
                        break;
                    case "participantNote":
                        var VparticipantNote = wDataContext.participantNotes.Where(w => w.pk_participantNote == iPK).Select(s => s.created_by);
                        if (VparticipantNote.Count() == 1) sCreatedBy = VparticipantNote.Single();
                        break;
                    //case "easementNote":
                    //    var VeasementNote = wDataContext.easementNotes.Where(w => w.pk_easementNote == iPK).Select(s => s.created_by);
                    //    if (VeasementNote.Count() == 1) sCreatedBy = VeasementNote.Single();
                    //    break;
                }
                sUsername = oUsername.ToString();
                if (sUsername.Trim().ToUpper() == sCreatedBy.Trim().ToUpper()) bUserCanModifyDeleteNote = true;
            }
        }
        catch { }
        if (!bUserCanModifyDeleteNote) WACAlert.Show("Only the creator can edit or delete the note. Creator: " + sCreatedBy + ", You: " + sUsername, 0);
        return bUserCanModifyDeleteNote;
    }

    public static bool Security_UserObjectCustom(object oUserID, Enum_Security_UserObjectCustom enumUOC)
    {
        bool b = false;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            //switch (enumUOC)
            //{
                //case Enum_Security_UserObjectCustom.A_PLAN:
                    try
                    {
                        int iUser_PK = Convert.ToInt32(oUserID);
                        //int i = wac.users.Where(w => w.userObjectCustoms.First(f => f.fk_user == iUser_PK).fk_user == iUser_PK && w.userObjectCustoms.First(f => f.fk_objectCustom_code == "A_PLAN").fk_objectCustom_code == "A_PLAN").Count();
                        //int i = wac.users.Where(w => w.userObjectCustoms.First(f => f.fk_user == iUser_PK).fk_user == iUser_PK && w.userObjectCustoms.First(f => f.fk_objectCustom_code == enumUOC.ToString()).fk_objectCustom_code == enumUOC.ToString()).Count();
                        int i = wac.users.Where(w => w.userObjectCustoms.Any(a => a.fk_user == iUser_PK) && w.userObjectCustoms.Any(a => a.fk_objectCustom_code == enumUOC.ToString())).Count();
                        if (i > 0) b = true;
                    }
                    catch { }
                    //break;
            //}
        }
        return b;
    }

    public static user Security_WACObjects_GetUserByID(int i)
    {
        user u = new user();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                var a = from b in wDataContext.users.Where(w => w.pk_user == i) select b;
                if (a.Count() == 1)
                {
                    u.active = a.Single().active;
                    u.fullname = a.Single().fullname;
                    u.password = a.Single().password;
                    u.pk_user = a.Single().pk_user;
                    u.username = a.Single().username;
                    u.userRoles = a.Single().userRoles;
                }
                else u = null;
            }
            catch { u = null; }
        }
        return u;
    }

    #endregion

    #region Sorting

    public static int Sorting_Compare_KVP_int_string_ByValue(KeyValuePair<int, string> a, KeyValuePair<int, string> b)
    {
        return a.Value.CompareTo(b.Value);
    }



    

    #endregion
    #region Conversion
    public static decimal? DecimalFromString(string s)
    {
        try
        {
            if (!string.IsNullOrEmpty(s))
                return Convert.ToDecimal(s);
            else
                return null;
        }
        catch (Exception ex)
        {
            WACAlert.Show(ex.Message, 0);
            return null;
        }
        
    }
    #endregion


    #region Special Conversion

    public static bool SpecialConversion_BooleanString_From_YesNoSingleChars(object oYN, bool bDefaultBoolean)
    {
        bool b = bDefaultBoolean;
        try
        {
            switch (oYN.ToString())
            {
                case "Y": b = true; break;
                case "N": b = false; break;
            }
        }
        catch { }
        return b;
    }

    #endregion

    #region Special DataTypes

    public static DateTime SpecialDataType_DateTime_Today()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }

    public static string[] SpecialDataType_StringCollection_Alphabet()
    {
        string[] input = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        return input;
    }

    public static List<string> SpecialDataType_ListString_Alphabet()
    {
        string[] input = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        List<string> l = new List<string>(input);
        return l;
    }

    public static string[] SpecialDataType_StringCollection_PercentagesByFive()
    {
        string[] input = { "0", "5", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55", "60", "65", "70", "75", "80", "85", "90", "95", "100" };
        return input;
    }

    #endregion

    #region Special Methods

    #region Global

    public static bool SpecialMethod_Global_CheckIfAllCharactersAreNumbers(string s)
    {
        bool b = true;
        foreach (char c in s)
        {
            if (!Char.IsNumber(c)) b = false;
        }
        return b;
    }

    //public static void SpecialMethod_Global_FAMEMAP_Hyperlink(ref HyperLink hyp, string sSection, string sHyperlinkText, string sFeature)
    //{
    //    hyp.Text = sHyperlinkText;
    //    hyp.NavigateUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["MapLink"] + @"/Webmap_SilverlightAppTestPage.aspx?section=" + sSection + "&feature=" + sFeature;
    //}

    public static string SpecialMethod_Global_FAMEMAP_NavigateURL(string sSection, object oFeature)
    {
        string s = string.Empty;
        if (oFeature != null) s = System.Web.Configuration.WebConfigurationManager.AppSettings["MapLink"] + @"/Webmap_SilverlightAppTestPage.aspx?section=" + sSection + "&feature=" + oFeature.ToString();
        return s;
    }

    public static bool SpecialMethod_Global_FAMEMAP_HyperlinkVisible(object oFeature)
    {
        bool b = true;
        if (oFeature == null) b = false;
        return b;
    }

    #endregion

    #region Tax Parcel

    public static string SpecialMethod_TaxParcel_PrintKeyFromSBL(object oSBL)
    {
        /*
         * SBL: 11122233334445556666
         * 
         * 111 = Section
         * 222 = SubSection
         * 3333 = Block
         * 444 = Lot
         * 555 = SubLot
         * 6666 = Suffix
         * 
         * Print Key: 111.222-3333-444.555-6666
         * */
        string s = string.Empty;
        try
        {
            string sSBL = oSBL.ToString();
            string sSection = sSBL.Substring(0, 3);
            string sSubSection = sSBL.Substring(3, 3);
            string sBlock = sSBL.Substring(6, 4);
            string sLot = sSBL.Substring(10, 3);
            string sSubLot = sSBL.Substring(13, 3);
            string sSuffix = sSBL.Substring(16, 4);

            s += Convert.ToInt16(sSection);
            if (Convert.ToInt16(sSubSection) > 0) s += "." + Convert.ToInt16(sSubSection);
            s += "-" + Convert.ToInt16(sBlock);
            s += "-" + Convert.ToInt16(sLot);
            if (Convert.ToInt16(sSubLot) > 0) s += "." + Convert.ToInt16(sSubLot);
            if (Convert.ToInt16(sSuffix) > 0) s += "-" + Convert.ToInt16(sSuffix);
        }
        catch { }
        return s;
    }

    #endregion

    #endregion

    #region Special Queries

    #region Agriculture

    public static int? SpecialQuery_Agriculture_PK_BMP_By_PK_WFP3BMP(object oPK_WFP3BMP)
    {
        int? iPK_BMP = null;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK = Convert.ToInt32(oPK_WFP3BMP);
                iPK_BMP = wac.form_wfp3_bmps.Where(w => w.pk_form_wfp3_bmp == iPK).Select(s => s.fk_bmp_ag).Single();
            }
            catch { }
        }
        return iPK_BMP;
    }

    public static bool SpecialQuery_Agriculture_BMP_Workload_CanInsert(object oPK_BMP)
    {
        bool b = true;
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                int iPK = Convert.ToInt32(oPK_BMP);
                string sStatus = wac.bmp_ags.Where(w => w.pk_bmp_ag == iPK).Select(s => s.list_statusBMP.workload).Single();
                if (sStatus != "Y") b = false;
            }
        }
        catch { }
        return b;
    }

    public static string SpecialQuery_Agriculture_BMPStatus_By_BMP(object oPK_BMP)
    {
        string sStatusPK = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK = Convert.ToInt32(oPK_BMP);
                sStatusPK = wac.bmp_ags.Where(w => w.pk_bmp_ag == iPK).Select(s => s.list_statusBMP.pk_statusBMP_code).Single();
            }
            catch { }
        }
        return sStatusPK;
    }

    public static decimal? SpecialQuery_Agriculture_BMPPracticeCode_By_BMP(object oPK_BMP)
    {
        decimal? d = null;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK = Convert.ToInt32(oPK_BMP);
                d = wac.bmp_ags.Where(w => w.pk_bmp_ag == iPK).Select(s => s.fk_bmpPractice_code).Single();
            }
            catch { }
        }
        return d;
    }

    //public static byte? SpecialQuery_Agriculture_BMPWorkload_Percentage(object oPK_BMPWorkload)
    //{
    //    byte? byPercentage = null;
    //    using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //    {
    //        try
    //        {
    //            int iPK_BMPWorkload = Convert.ToInt32(oPK_BMPWorkload);
    //            var a = wac.bmp_ag_workloadProgresses.Where(w => w.fk_bmp_ag_workload == iPK_BMPWorkload).Select(s => s).OrderByDescending(o => o.created);
    //            if (a.Count() > 0) byPercentage = a.First().progress_pct;
    //        }
    //        catch { }
    //    }
    //    return byPercentage;
    //}

    public static int? SpecialQuery_Agriculture_BidWinningContractor_ByWFP3Package(object oPK_FormWFP3)
    {
        int? i = null;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try { i = wac.form_wfp3_bids.Where(w => w.fk_form_wfp3 == Convert.ToInt32(oPK_FormWFP3) && w.bid_winner == "Y").Select(s => s.fk_participant_contractor).Single(); }
            catch { }
        }
        return i;
    }

    public static IEnumerable<bmp_ag_workloadSupport> SpecialQuery_Agriculture_BMP_Workload_DesignerEngineer(object oES_bmp_ag_workloadSupport, string sBMPWorkloadSupport_Code)
    {
        EntitySet<bmp_ag_workloadSupport> es = (EntitySet<bmp_ag_workloadSupport>)oES_bmp_ag_workloadSupport;
        var a = es.Where(w => w.fk_BMPWorkloadSupport_code == sBMPWorkloadSupport_Code).OrderBy(o => o.list_designerEngineer.designerEngineer);
        return a;
    }

    public static string SpecialQuery_Agriculture_GetWACRegion_ByFarmBusinessPK(object oPK)
    {
        string sWACRegion = string.Empty;
        try
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iPK = Convert.ToInt32(oPK);
                var x = wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == iPK).Select(s => s.fk_regionWAC_code);
                if (x.Count() == 1) sWACRegion = x.Single();
            }
        }
        catch { }
        return sWACRegion;
    }

    public static IOrderedEnumerable<bmp_ag> SpecialQuery_Agriculture_BMP_HideProgrammatic(object oBMPAg, int?[] iProgrammaticCodes)
    {
        EntitySet<bmp_ag> es = (EntitySet<bmp_ag>)oBMPAg;
        try
        {
            IEnumerable<bmp_ag> x = es.AsEnumerable();
            x = x.Where(w => !iProgrammaticCodes.Contains(w.fk_programmaticRecord_code));
            //foreach (int? i in iProgrammaticCodes)
            //{
                //x = x.Where(w => w.fk_programmaticRecord_code == null || w.fk_programmaticRecord_code != i);
            //}
            return x.OrderBy(o => o.list_pollutant_category.sort_position).ThenBy(o => o.CompositBmpNum);
        }
        catch { return Order_Agriculture_BMP_AG_PollutantCategory_BMPNumber(es); }
    }

    public static string SpecialQuery_Agriculture_WFP3_WinningContractor(object oFormWFP3Bids)
    {
        string s = string.Empty;
        try
        {
            EntitySet<form_wfp3_bid> esFormWFP3Bid = (EntitySet<form_wfp3_bid>)oFormWFP3Bids;
            if (esFormWFP3Bid.Count() > 0)
            {
                foreach (form_wfp3_bid a in esFormWFP3Bid)
                {
                    if (Convert.ToDateTime(a.bid_awarded).Year > 1900)
                    {
                        s = a.participant.fullname_LF_dnd;
                        break;
                    }
                }
            }
        }
        catch { }
        return s;
    }

    public static bool SpecialQuery_Agriculture_HasPhotos(string sPhotoType, object oPK)
    {
        bool b = false;
        try
        {
            int iPK = Convert.ToInt32(oPK);
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.photos.Where(w => w.fk_photoType_code == sPhotoType && w.pk_item == iPK);
                if (a.Count() > 0) b = true;
            }
        }
        catch { }
        return b;
    }

    public static IQueryable<supplementalAgreement> SpecialQuery_Agriculture_SupplementalAgreementsByFarmBusiness(object oPK_FarmBusiness)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_FarmBusiness = Convert.ToInt32(oPK_FarmBusiness);
                //var a = wac.supplementalAgreements.Where(w => w.supplementalAgreementTaxParcels.First(f => f.supplementalAgreementTaxParcelBMPs.First(f2 => f2.fk_farmBusiness == iPK_FarmBusiness).fk_farmBusiness == iPK_FarmBusiness).supplementalAgreementTaxParcelBMPs.First(f => f.fk_farmBusiness == iPK_FarmBusiness).fk_farmBusiness == iPK_FarmBusiness).OrderBy(o => o.agreement_nbr_ro).Select(s => s);
                //return a;
                return null;
            }
            catch { return null; }
       }
    }

    public static string SpecialQuery_Agriculture_Unit_By_BMPPractice(object oPK_BMPPractice)
    {
        string sUnit = "Undefined Unit";
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                decimal d = Convert.ToDecimal(oPK_BMPPractice);
                sUnit = wac.list_bmpPractices.Where(w => w.pk_bmpPractice_code == d).Select(s => s.list_unit.unit).Single();
            }
            catch { sUnit = "Error obtaining Unit"; }
        }
        return sUnit;
    }

    #endregion

    #region DatabaseLists - Forestry Code

    public static void PopulateControl_DatabaseLists_ForestryCode_DDL(FormView fv, string ddlID, string value, bool bOnlyContractors)
    {
        DropDownList ddl = fv.FindControl(ddlID) as DropDownList;
        if (ddl != null)
        {
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                if (bOnlyContractors)
                {
                    var a = from b in wDataContext.list_forestryCodes.Where(w => w.contractor == "Y").OrderBy(o => o.description) select new { b.pk_forestryCode_code, b.description };
                    ddl.DataSource = a;
                }
                else
                {
                    var a = from b in wDataContext.list_forestryCodes.OrderBy(o => o.description) select new { b.pk_forestryCode_code, b.description };
                    ddl.DataSource = a;
                }
                ddl.DataTextField = "description";
                ddl.DataValueField = "pk_forestryCode_code";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                if ((fv.CurrentMode == FormViewMode.Edit || fv.CurrentMode == FormViewMode.Insert) && !string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
            }
        }
    }

    #endregion


    #endregion

    #region Special Text

    #region Agriculture

    public static string Specialtext_Agriculture_BMP_revisionDescription(object oPK_revisionDescription_code)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                string sPK_revisionDescription_code = Convert.ToString(oPK_revisionDescription_code);
                str = wac.list_revisionDescriptions.Where(w => w.pk_revisionDescription_code == sPK_revisionDescription_code).Select(s => s.description).Single();
            }
            catch { str = ""; }
        }
        return str;
    }

    public static string Specialtext_Agriculture_BMP_Pollutant_Category_Comments(object oPK_FarmBusiness, object oPK_pollutant_category_code)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_FarmBusiness = Convert.ToInt32(oPK_FarmBusiness);
                string iPK_pollutant_category_code = Convert.ToString(oPK_pollutant_category_code);
                switch (iPK_pollutant_category_code)
                {
                    case "I":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_i_descrip).First();
                        break;
                    case "II":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_ii_descrip).First();
                        break;
                    case "III":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_iii_descrip).First();
                        break;
                    case "IV":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_iv_descrip).First();
                        break;
                    case "V":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_v_descrip).First();
                        break;
                    case "VI":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_vi_descrip).First();
                        break;
                    case "VII":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_vii_descrip).First();
                        break;
                    case "VIII":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_viii_descrip).First();
                        break;
                    case "IX":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_ix_descrip).First();
                        break;
                    case "X":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_x_descrip).First();
                        break;
                    case "XI":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_xi_descrip).First();
                        break;
                    case "CLII":
                        str = "CLII";
                        break;
                    case "V.2":
                        str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness && w.fk_supplementalAgreement == null).Select(s => s.pollutant_v2_descrip_CREP).First();
                        break;
                    case "n/a":
                        //str = wac.form_wfp2s.Where(w => w.fk_farmBusiness == iPK_FarmBusiness).Select(s => s.pollutant_i_descrip).First();
                        break;
                    default:
                        str = "Unknown pollutant category code";
                        break;
                }
            }
            catch { str = "Error: Could not get pollutant category comments"; }
        }
        return str;
    }

    public static string Specialtext_Agriculture_BMP_Status_WFP2_Revision(object oPK_FarmBusiness, object oPK_WFP2_Version)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_FarmBusiness = Convert.ToInt32(oPK_FarmBusiness);
                int iPK_WFP2_Version = Convert.ToInt32(oPK_WFP2_Version);
                str = wac.vw_wfp2_version_SAs.Where(w => w.pk_farmBusiness == iPK_FarmBusiness && w.pk_form_wfp2_version == iPK_WFP2_Version).Select(s => s.Revision).Single();
            }
            catch { str = "Could not get Revision"; }
        }
        return str;
    }

    public static string SpecialText_Agriculture_SA_AgreementNumberFarmIDTaxParcelOwner(object oAN, object oFID, object oTPO)
    {
        string str = string.Empty;
        try { str = oAN.ToString(); }
        catch { str = "Agreement Number Not Found"; }
        try { str += " [" + oFID.ToString(); }
        catch { str += " [Farm ID Not Found"; }
        try { str += " - " + oTPO.ToString() + "]"; }
        catch { str += " - Owner Not Found]"; }
        return str;
    }


    public static bool BmpExistsForFarm(bmp_ag bmp)
    {
        return BmpExistsForFarm(bmp.pk_bmp_ag, bmp.fk_farmBusiness, bmp.Bmp, bmp.AgBmpDescriptorCode.DescriptorCode, bmp.fk_BMPCode_code, bmp.QualifierVersion.Value);
    }

    public static bool BmpExistsForFarm(int pk_bmp_ag, int fk_farmBusiness, string bmp, string descriptorCode, string qualifier, byte? qualifierVersion)
    {
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                var b = wac.bmp_ags.Where(w => w.fk_farmBusiness == fk_farmBusiness && string.Compare(w.Bmp, bmp, true) == 0
                    && w.AgBmpDescriptorCode != null && string.Compare(w.AgBmpDescriptorCode.DescriptorCode, descriptorCode, true) == 0
                    && w.fk_BMPCode_code != null && string.Compare(w.fk_BMPCode_code, qualifier, true) == 0
                    && w.QualifierVersion == qualifierVersion).ToList();

                if (pk_bmp_ag == 0)
                    return b.Any();
                else 
                {
                    var c = b.Where(w => w.pk_bmp_ag != pk_bmp_ag).ToList();
                    return c.Any();
                }
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message, 0);
                return true;
            }
        }
    }

    public static Dictionary<string, string> BmpNumberDeconstruct(bmp_ag bmp, List<string> qualifiers)
    {
        Dictionary<string, string> parts = new Dictionary<string, string>();
        string descriptionUpper = bmp.description.ToUpper();
    
        var m = Regex.Match(bmp.bmp_nbr, @"^(\d{1,3}(?:\.\-_)?[a-zSXT]?[a-zSXT]?\d?)([a-zA-Z]*)(\d?)?$");
        if (m.Success)
        {
            parts.Add("BMP", m.Groups[1].Value);
            string qualifier = string.IsNullOrEmpty(m.Groups[2].Value) ? "UNC" : m.Groups[2].Value;
            // if qualifier is a valid qualifier we are done
            if (qualifiers.BinarySearch(qualifier) > 0)
                parts.Add("QUALIFIER", qualifier);
            // if not we try some other ways to come up with a valid qualifier
            else if (qualifier.Contains("Rep"))
                parts.Add("QUALIFIER", "R");
            else if (qualifier.Contains("Flood"))
                parts.Add("QUALIFIER", "STR");
            else if (qualifier.Contains("Mod"))
                parts.Add("QUALIFIER", "M");
            else if (qualifier.Contains("ER"))
                parts.Add("QUALIFIER", "ER");
            else if (qualifier.Contains("RR"))
                parts.Add("QUALIFIER", "RR");
            else if (qualifier.Contains("EX"))
                parts.Add("QUALIFIER", "EX");
            else if (qualifier.Contains("WRE"))
                parts.Add("QUALIFIER", "WRE");
            else if (qualifier.Contains("RE"))
                parts.Add("QUALIFIER", "RE");
            else if (qualifier.Contains("BYI"))
                parts.Add("QUALIFIER", "BYI");
            else parts.Add("QUALIFIER", "UNC");
            if (!string.IsNullOrEmpty(descriptionUpper))
            {
                if (descriptionUpper.Contains("CREP") || descriptionUpper.Contains("CP-30"))
                    parts["QUALIFIER"] = "C";

                else if (descriptionUpper.Contains("Re-en"))
                    parts["QUALIFIER"] = "RE";
            }
            parts.TryGetValue("QUALIFIER", out qualifier);

            byte version = 0;
            if (!string.IsNullOrEmpty(qualifier))
            {
                if (!qualifier.In(new[] { "MTC", "ENMC", "UNC", "C","CP","BYI","BYIV" }))
                {
                    if (!Byte.TryParse(m.Groups[3].Value, out version))
                        version = 1;
                }
            }
            parts.Add("VERSION", version.ToString());
            return parts;
        }
        else if (bmp.bmp_nbr.StartsWith("IRC"))
        {
            parts.Add("BMP", bmp.bmp_nbr);
            parts.Add("QUALIFIER", "UNC");
            parts.Add("VERSION", "0");
            return parts;
        }
        return null;
    }

  

    public static string SpecialText_Agriculture_BMP(object oPK_BMP_AG)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_BMP_AG = Convert.ToInt32(oPK_BMP_AG);
                var a = wac.bmp_ags.Where(w => w.pk_bmp_ag == iPK_BMP_AG).Select(s => new { s.CompositBmpNum, s.description }).Single();
                str = a.CompositBmpNum + " " + a.description;
            }
            catch { str = "BMP not matched"; }
        }
        return str;
    }

    public static string SpecialText_Agriculture_FullBmpName(object oPK_BMP_AG)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_BMP_AG = Convert.ToInt32(oPK_BMP_AG);
                var bmp = wac.bmp_ags.Where(w => w.pk_bmp_ag == iPK_BMP_AG).Select(s => new
                {
                    bmp_nbr = s.bmp_nbr,
                    qualifier = s.fk_BMPCode_code,
                    version = s.QualifierVersion
                }).Single();

                StringBuilder sb = new StringBuilder(bmp.bmp_nbr);
                if (!string.IsNullOrEmpty(bmp.qualifier) && !bmp.qualifier.Contains("UNC"))
                    sb.Append(bmp.qualifier);
                if (!string.IsNullOrEmpty(bmp.version.ToString()))
                    sb.Append(bmp.version);
                str = sb.ToString();
            }
            catch
            {
                str = "BMP not matched";
            }
        }
        return str;
    }

    public static string SpecialText_Agriculture_FullBmpName(string bmp, string qualifier, byte? version)
    {
        string str = string.Empty;

            try
            {
                StringBuilder sb = new StringBuilder(bmp);
                if (!string.IsNullOrEmpty(qualifier) && !qualifier.Contains("UNC"))
                    sb.Append(qualifier);
                if (!string.IsNullOrEmpty(version.ToString()))
                    sb.Append(version.ToString());
                str = sb.ToString();
            }
            catch
            {
                str = "BMP full name failed";
            }

        return str;
    }

    public static string SpecialText_Agriculture_WorkloadBmp(object oPK_BMP_AG)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_BMP_AG = Convert.ToInt32(oPK_BMP_AG);
                var bmp = wac.bmp_ags.Where(w => w.pk_bmp_ag == iPK_BMP_AG).Select(s => new
                {
                    //bmp_nbr = s.bmp_nbr,
                    //qualifier = s.fk_BMPCode_code,
                    //version = s.QualifierVersion,
                    bmp = s.CompositBmpNum,
                    dscript = s.description
                }).Single();

                StringBuilder sb = new StringBuilder(bmp.bmp);
                //if (!string.IsNullOrEmpty(bmp.qualifier) && !bmp.qualifier.Contains("UNC"))
                //    sb.Append(bmp.qualifier);
                //if (!string.IsNullOrEmpty(bmp.version.ToString()))
                //    sb.Append(bmp.version);
                sb.Append(" ");
                if (!string.IsNullOrEmpty(bmp.dscript))
                    sb.Append(bmp.dscript);
                str = sb.ToString();
            }
            catch
            {
                str = "BMP not matched";
            }        
        }
        return str;
    }

    public static string SpecialText_Agriculture_BMP_SA(object oPK_SA)
    {
        string str = "No Supplemental Agreement Record";
        if (oPK_SA != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                try
                {
                    int i = Convert.ToInt32(oPK_SA);
                    var a = wac.supplementalAgreements.Where(w => w.pk_supplementalAgreement == i).Select(s => s).Single();
                    str = a.agreement_nbr_ro + " [Lessor: " + a.ownerStr_dnd + "]";
                } 
                catch { }
            }
        }
        return str;
    }

    public static string SpecialText_Agriculture_BMPPractice(object oPK, object oPractice, object oABC, bool bShowABC)
    {
        string s = string.Empty;
        try
        {
            s = oPK.ToString() + " " + oPractice.ToString();
            if (bShowABC)
            {
                if (oABC != null) s += " (" + Format_Global_Currency(oABC) + ")";
            }
        }
        catch { }
        return s;
    }
    public static string FormatImplementationProject(object status, object project)
    {
        if (((string)status).In(new string[] { "A", "DR" }) == true && project != null)
            return project.ToString();
        return null;
    }
    public static string BmpGridIrcTextColor(object isIrc, object textToColor)
    {
        try
        {
            string t = (string)textToColor;
            bool irc = Convert.ToInt32(isIrc) == 1;
            if (irc)
                return "<font color='purple'>" + t + "</font>"; 
        }
        catch (Exception)
        { }
        // if anything goes wrong return the original string
        return (string)textToColor;
    }
    public static string SpecialText_Agriculture_BMP_Status_ColorCoded(object oStatusCode, object oStatus)
    {
        string s = oStatus.ToString();
        try
        {
            switch (oStatusCode.ToString())
            {
                case "C": s = Format_Color_Global_ColorText(oStatus, Enum_Color.GREEN); break;
                case "I": s = Format_Color_Global_ColorText(oStatus, Enum_Color.RED); break;
            }
        }
        catch { }
        return s;
    }

    public static string SpecialText_Agriculture_BMP_Status_ColorCoded(object oStatusCode, object oStatus, object oCompletedDate)
    {
        string s = string.Empty;
        try
        {
            switch (oStatusCode.ToString())
            {
                case "C": s = Format_Color_Global_ColorText(oStatus, Enum_Color.GREEN) + Environment.NewLine + Format_Color_Global_ColorText(Format_Global_Date(oCompletedDate), Enum_Color.GREEN); break;
                case "I": s = Format_Color_Global_ColorText(oStatus, Enum_Color.RED); break;
                default: s = oStatus.ToString(); break;
            }
        }
        catch { }
        return s;
    }

    public static string SpecialText_Agriculture_BMP_Workload_Planner(object pkFarmBusiness)
    {
        string s = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            s = wac.WFPGetMasterPlanner(Convert.ToInt32(pkFarmBusiness)).ToString();
        }
        //try
        //{
        //    EntitySet<list_groupPIMember> groupPIMembers = (EntitySet<list_groupPIMember>)oList_groupPIMembers;
        //    if (bUseNickname) s = groupPIMembers.Where(w => w.groupLeader == "Y").First().list_designerEngineer.nickname;
        //    else s = groupPIMembers.Where(w => w.groupLeader == "Y").First().list_designerEngineer.designerEngineer;
        //}
        //catch { }
        return s;
    }

    public static string SpecialText_Agriculture_FormWFP3_FixedText(object oCode)
    {
        string str = string.Empty;
        try
        {
            string sCode = oCode.ToString();
            if (!string.IsNullOrEmpty(sCode))
            {
                using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
                {
                    try
                    {
                        var a = wDataContext.list_formWFP3_fixedTexts.Where(w => w.pk_formWFP3_fixedText_code == sCode).Select(s => new { s.displayText, s.message_fixedText }).Single();
                        str = a.displayText;
                    }
                    catch { }
                }
            }
        }
        catch { }
        return str;
    }

    public static string SpecialText_Agriculture_PopUpHeader(object oPK_FarmBusiness)
    {
        StringBuilder sb = new StringBuilder();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int iPK = Convert.ToInt32(oPK_FarmBusiness);
                var a = (from b in wDataContext.farmBusinesses.Where(w => w.pk_farmBusiness == iPK)
                        select new { b.farmID, b.farm_name, b.farmBusinessOwners, b.farmBusinessOperators, b.farmLand.property }).Single();

                sb.Append("<div class='NestedDivLevel00'>");
                sb.Append("<div style='float:left;'>");

                if (!string.IsNullOrEmpty(a.farmID)) sb.Append("<span class='B'>" + a.farmID + "</span>");
                if (!string.IsNullOrEmpty(a.farm_name)) sb.Append("<span class='B' style='margin-left:5px;'>" + a.farm_name + "</span>");

                if (a.property != null)
                {
                    string sAddress2Type = null;
                    if (!string.IsNullOrEmpty(a.property.fk_address2Type_code)) sAddress2Type = a.property.list_address2Type.longname;
                    sb.Append("<div>" + SpecialText_Global_Address(a.property.address, a.property.address2, sAddress2Type, a.property.city, a.property.state, a.property.fk_zipcode, a.property.zip4, true) + "</div>");
                }

                sb.Append("</div>");

                sb.Append("<div style='float:right;'>");
                if (a.farmBusinessOwners.Count() > 0)
                {
                    sb.Append("<div class='B U'>Operators</div>");
                    foreach (farmBusinessOperator fbo in Order_Agriculture_FarmBusinessOperator(a.farmBusinessOperators))
                    {
                        sb.Append("<div>" + fbo.participant.fullname_LF_dnd + "</div>");
                    }
                }
                sb.Append("</div>");

                sb.Append("<div style='float:right; margin-right:20px;'>");
                if (a.farmBusinessOwners.Count() > 0)
                {
                    sb.Append("<div class='B U'>Owners</div>");
                    foreach (farmBusinessOwner fbo in Order_Agriculture_FarmBusinessOwner(a.farmBusinessOwners))
                    {
                        sb.Append("<div>" + fbo.participant.fullname_LF_dnd + "</div>");
                    }
                }
                sb.Append("</div>");

                sb.Append("<div style='clear:both;'></div>");
                sb.Append("</div>");
            }
            catch { }
        }
        return sb.ToString();
    }

    public static string SpecialText_Agriculture_FarmBusiness_ID_NAME_OWNER(object oFarmID, object oFarmName, object oFarmOwner, bool bShowFarmName, bool bShowFarmOwner)
    {
        string s = string.Empty;
        try
        {
            if (oFarmID != null)
            {
                try { s = oFarmID.ToString(); }
                catch { }
            }
            if (oFarmName != null && bShowFarmName)
            {
                try 
                {
                    if (string.IsNullOrEmpty(s)) s = oFarmName.ToString();
                    else s += " " + oFarmName.ToString();
                }
                catch { }
            }
            if (oFarmOwner != null && bShowFarmOwner)
            {
                try
                {
                    if (string.IsNullOrEmpty(s)) s = oFarmOwner.ToString();
                    else s += " - " + oFarmOwner.ToString();
                }
                catch { }
            }
        }
        catch { s = "No ID, Name, or Owner"; }
        if (string.IsNullOrEmpty(s)) s = "Farm Record Could Not Be Constructed";
        return Truncate(s,75);
    }

    public static string SpecialText_Agriculture_RecordTitle(object oFarmID, object oFarmName, object oPK_FarmSize)
    {
        string s = string.Empty;
        try
        {
            if (oFarmID != null) s = oFarmID.ToString();

            if (oFarmName != null)
            {
                if (string.IsNullOrEmpty(s)) s = oFarmName.ToString();
                else s += " " + oFarmName.ToString();
            }

            if (oPK_FarmSize != null)
            {
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    string sPK_FarmSize = oPK_FarmSize.ToString();
                    var a = wac.list_farmSizes.Where(w => w.pk_farmSize_code == sPK_FarmSize).Select(se => se.farmSize);
                    if (a.Count() == 1)
                    {
                        if (string.IsNullOrEmpty(s)) s = a.Single();
                        else s += " (" + a.Single() + ")";
                    }
                }
            }
        }
        catch { s = "Record Title Display Error"; }
        if (string.IsNullOrEmpty(s)) s = "No Farm Business Record";
        return s;
    }

    public static string SpecialText_Agriculture_SA_TP_Owner(object oSupplementalAgreementTaxParcel)
    {
        string str = "No Supplemental Agreement Record";
        try
        {
            if (oSupplementalAgreementTaxParcel != null)
            {
                supplementalAgreementTaxParcel SATP = (supplementalAgreementTaxParcel)oSupplementalAgreementTaxParcel;
                str = SATP.supplementalAgreement.agreement_nbr_ro + " - " + SATP.taxParcel.taxParcelID + " [Lessor: " + SATP.taxParcel.ownerStr_dnd + "]";
            }
        }
        catch { str = "Error Obtaining Supplemental Agreement"; }
        return str;
    }

    public static string SpecialText_Agriculture_SA_TP_Owner(object oSA_Number, object oSA_TaxParcelID, object oSA_Owner)
    {
        string str = "No Supplemental Agreement Record";
        try
        {
            if (oSA_Number != null) str = oSA_Number.ToString();
            else str = "Undefined SA Number";

            if (oSA_TaxParcelID != null) str += " - " + oSA_TaxParcelID.ToString();
            else str += " - Undefined Tax Parcel ID";

            if (oSA_Owner != null) str += " [Lessor: " + oSA_Owner.ToString() + "]";
            else str += " [Lessor: Undefined Owner]";
        }
        catch { }
        return str;
    }

    public static string SpecialText_Agriculture_WFP3_Invoice(object oPK_Form_WFP3_Invoice)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_Form_WFP3_Invoice = Convert.ToInt32(oPK_Form_WFP3_Invoice);
                var a = wac.form_wfp3_invoices.Where(w => w.pk_form_wfp3_invoice == iPK_Form_WFP3_Invoice).Select(s => new { s.date, s.invoice_nbr, s.amt }).Single();
                str = "Invoice #: " + a.invoice_nbr + " " + Format_Global_Currency(a.amt) + " (" + Format_Global_Date(a.date) + ")";
            }
            catch { str = "Invoice not matched"; }
        }
        return str;
    }

    public static string SpecialText_FarmBusinessPlanner_DesignerEngineer_NameAgencyMaster(object oFarmBusinessPlanner)
    {
        string s = string.Empty;
        try
        {
            farmBusinessPlanner fbp = (farmBusinessPlanner)oFarmBusinessPlanner;
            s = fbp.list_designerEngineer.designerEngineer;
            if (!string.IsNullOrEmpty(fbp.list_designerEngineer.fk_agency_code)) s += ", " + fbp.list_designerEngineer.fk_agency_code;
            if (fbp.master.ToUpper() == "Y") s += " [Master]";
            s += "<br />";
        }
        catch { }
        return s;
    }

    #endregion

  
    #region Global

    public static string Specialtext_Global_Advisory(object oPK)
    {
        string str = "No Advisory Available";
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK = Convert.ToInt32(oPK);
                str = wac.advisoryMsgs.Where(w => w.pk_advisoryMsg == iPK).Select(s => s.advisory).Single();
            }
            catch { }
        }
        return str;
    }
    public static string SpecialText_Global_Address(object oAddressFull, object oCityStateZip)
    {
        StringBuilder sb2 = new StringBuilder();
        try
        {
            string sAddress = null;
            try { sAddress = oAddressFull.ToString(); }
            catch { }
            string sAddress2 = null;
            
            string sCity = null;
            try { sCity = oCityStateZip.ToString(); }
            catch { }
           
            if (!string.IsNullOrEmpty(sAddress)) sb2.Append(sAddress.Trim());  
            if (!string.IsNullOrEmpty(sb2.ToString())) sb2.Append(", ");
            if (!string.IsNullOrEmpty(sCity)) sb2.Append(sCity.Trim());

        }
        catch { sb2.Append("Error Constructing Address"); }
        if (string.IsNullOrEmpty(sb2.ToString())) sb2.Append("Address Not Available");
        return sb2.ToString();
    }


    public static string SpecialText_Global_Address(object oAddress, object oAddress2, object oAddress2Type, object oCity, object oState, object oZip, object oZip4, bool bSingleLine)
    {
        StringBuilder sb2 = new StringBuilder();
        try
        {
            string sAddress = null;
            try { sAddress = oAddress.ToString(); }
            catch { }
            string sAddress2 = null;
            if (oAddress2 != null)
            {
                try { sAddress2 = oAddress2.ToString(); }
                catch { }
            }
            string sAddress2Type = null;
            if (oAddress2Type != null)
            {
                try { sAddress2Type = oAddress2Type.ToString(); }
                catch { }
            }
            string sCity = null;
            try { sCity = oCity.ToString(); }
            catch { }
            string sState = null;
            try { sState = oState.ToString(); }
            catch { }
            string sZip = null;
            try { sZip = oZip.ToString(); }
            catch { }
            string sZip4 = null;
            try { sZip4 = oZip4.ToString(); }
            catch { }
            if (bSingleLine)
            {

                if (!string.IsNullOrEmpty(sAddress))
                {
                    sb2.Append(sAddress.Trim());
                    //sb2.Append(", ");
                }   

                //           if (!string.IsNullOrEmpty(sAddress2) && !string.IsNullOrEmpty(sAddress2Type))
                if (!string.IsNullOrEmpty(sAddress2))
                {
                    sb2.Append(", ");
                    sb2.Append(sAddress2.Trim());
                    
                }

                if (!string.IsNullOrEmpty(sCity))
                {
                    sb2.Append(", ");
                    sb2.Append(sCity.Trim());
                    sb2.Append("&nbsp;");

                }
                if (!string.IsNullOrEmpty(sState)) 
                    sb2.Append(sState.Trim() + "&nbsp;");
                if (!string.IsNullOrEmpty(sZip)) 
                    sb2.Append(sZip.Trim());
                if (!string.IsNullOrEmpty(sZip4)) 
                    sb2.Append("-" + sZip4.Trim());
            }
            else
            {
                if (!string.IsNullOrEmpty(sAddress))
                {
                    sb2.Append("<div>" + sAddress.Trim() + ",&nbsp</div>");

                }
                if (!string.IsNullOrEmpty(sAddress2))
                    sb2.Append("<div>" + sAddress2.Trim() + ",&nbsp</div>");
                sb2.Append("<div>");
                if (!string.IsNullOrEmpty(sCity)) sb2.Append(sCity.Trim() + "&nbsp;");
                if (!string.IsNullOrEmpty(sState)) sb2.Append(sState.Trim() + "&nbsp;");
                if (!string.IsNullOrEmpty(sZip)) sb2.Append(sZip.Trim());
                if (!string.IsNullOrEmpty(sZip4)) sb2.Append("-" + sZip4.Trim());
                sb2.Append("</div>");
            }
        }
        catch { sb2.Append("Error Constructing Address"); }
        if (string.IsNullOrEmpty(sb2.ToString())) sb2.Append("Address Not Available");
        return sb2.ToString();
    }

    public static string SpecialText_Global_Address2(object oAddress2, object oPK_Address2Type)
    {
        string str = string.Empty;
        if (oAddress2 != null)
        {
            try { str = oAddress2.ToString(); }
            catch { }
        }

        if (oPK_Address2Type != null)
        {
            try
            {
                using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
                {
                    string sPK_Address2Type = oPK_Address2Type.ToString();
                    var a = wac.list_address2Types.Where(w => w.pk_address2Type_code == sPK_Address2Type).Select(s => new { s.longname });
                    if (a.Count() == 1) str += " " + a.Single().longname;
                }
            }
            catch { }
        }
        return str;
    }
    public static string SpecialText_Global_BMPAgNMP(object pk)
    {
        string bmpNbr = string.Empty;
        try
        {
            int fk_bmp_ag_nmp = KeyAsInt(pk);
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var z = wac.vw_farmBusinessBMP_NMPs.Where(w => w.pk_bmp_ag == fk_bmp_ag_nmp).Select(s => new { s.bmp_nbr, s.bmp_descrip });
                if (!string.IsNullOrEmpty(z.Single().bmp_nbr))
                    bmpNbr = z.Single().bmp_nbr;
            }
        }
        catch (Exception) { }
        return bmpNbr;
    }

    public static string SpecialText_Global_Planner(object _pk)
    {
        int pk_farmBusiness = KeyAsInt(_pk);
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            int? planner = wac.farmBusiness_get_planner(pk_farmBusiness);
            StringBuilder sb = new StringBuilder();
            if (planner != null)
            {
                var e = wac.list_designerEngineers.Where(w => w.pk_list_designerEngineer == planner).Select(s => s);
                var x = e.FirstOrDefault();
                if (!string.IsNullOrEmpty(x.designerEngineer)) sb.Append(x.designerEngineer);
                if (!string.IsNullOrEmpty(x.title)) sb.Append(" - " + x.title);
                if (!string.IsNullOrEmpty(x.fk_agency_code)) sb.Append(" (" + x.list_agency.agency + ")");
                return sb.ToString();
            }
            else
                return string.Empty;
        }
    }
    //public static string SpecialText_Global_DesignerEngineerCCE(object oDesignerEngineer, bool bShowTitle, bool bShowAgency)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    try
    //    {
    //        list_designerEngineer x = (list_designerEngineer)oDesignerEngineer;
    //        if (x != null)
    //        {
    //            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //            {
    //                var z = wac.vw_designerEngineer_usages.Where(w => w.pk_list_designerEngineer == x.pk_list_designerEngineer &&
    //                    w.fk_designerEngineerType_code == "PLAN" && w.active == "Y").Select(s => s);

    //                if (!string.IsNullOrEmpty(z.First().designerEngineer)) sb.Append(z.First().designerEngineer);
    //                if (!string.IsNullOrEmpty(z.First().designerEngineer_title) && bShowTitle) sb.Append(" - " + z.First().designerEngineer_title);
    //                if (!string.IsNullOrEmpty(x.fk_agency_code) && bShowAgency) sb.Append(" (" + x.list_agency.agency + ")");
    //            }
    //        }
    //        else sb.Append("Undefined");
    //    }
    //    catch { sb.Append("Error Constructing Designer/Engineer"); }
    //    return sb.ToString();
    //}
    public static string SpecialText_Global_DesignerEngineer(object oDesignerEngineer, bool bShowTitle, bool bShowAgency)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            int pk_list_designerEngineer = Convert.ToInt32(oDesignerEngineer);
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                var x = wac.list_designerEngineers.Where(w => w.pk_list_designerEngineer == pk_list_designerEngineer).Select(s => s);
                if (!string.IsNullOrEmpty(x.Single().designerEngineer)) sb.Append(x.Single().designerEngineer);
                if (!string.IsNullOrEmpty(x.Single().title) && bShowTitle) sb.Append(" - " + x.Single().title);
                if (!string.IsNullOrEmpty(x.Single().fk_agency_code) && bShowAgency) sb.Append(" (" + x.Single().list_agency.agency + ")");
            }
        }
        catch (Exception)
        {
            try
            {
                list_designerEngineer x = (list_designerEngineer)oDesignerEngineer;
                if (x != null)
                {
                    if (!string.IsNullOrEmpty(x.designerEngineer)) sb.Append(x.designerEngineer);
                    if (!string.IsNullOrEmpty(x.title) && bShowTitle) sb.Append(" - " + x.title);
                    if (!string.IsNullOrEmpty(x.fk_agency_code) && bShowAgency) sb.Append(" (" + x.list_agency.agency + ")");
                }
                else sb.Append("Undefined");
            }
            catch { sb.Append("Error Constructing Designer/Engineer"); };
        }
        
        return sb.ToString();
    }

    public static string SpecialText_Global_Participant_Name_Org(object oLast, object oFirst, object oMiddle, object oSuffix, object oOrg)
    {
        string s = string.Empty;

        try
        {
            string sLast = oLast.ToString();
            if (!string.IsNullOrEmpty(sLast)) s = sLast;
        }
        catch { }
        try
        {
            string sSuffix = oSuffix.ToString();
            if (!string.IsNullOrEmpty(sSuffix)) s += " " + sSuffix;
        }
        catch { }
        try
        {
            string sFirst = oFirst.ToString();
            if (!string.IsNullOrEmpty(sFirst)) s += ", " + sFirst;
        }
        catch { }
        try
        {
            string sMiddle = oMiddle.ToString();
            if (!string.IsNullOrEmpty(sMiddle)) s += " " + sMiddle;
        }
        catch { }

        try
        {
            string sOrg = oOrg.ToString();
            if (string.IsNullOrEmpty(s)) s = sOrg;
            else if (!string.IsNullOrEmpty(sOrg)) s += " [" + sOrg + "]";
        }
        catch { }
        return s;
    }

    public static string SpecialText_Global_Participant(object oParticipant, bool bShowPhone, bool bShowCell, bool bShowEmail, bool bShowAddress)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            participant x = (participant)oParticipant;
            if (x != null)
            {
                sb.Append("<div>");
                sb.Append("<div>" + x.fullname_LF_dnd + "</div>");
                //if (bShowPhone) sb.Append("<div>Phone: " + Format_Global_PhoneNumberPlusExtension(x.phone, x.phone_ext) + "</div>");
                //if (bShowCell) sb.Append("<div>Mobile: " + Format_Global_PhoneNumber(x.cell) + "</div>");
                if (bShowEmail) sb.Append("<div>Email: <a href='mailto:" + x.email + "'>" + x.email + "</a></div>");
                if (bShowAddress && x.property != null)
                {
                    string sAddress = string.Empty;
                    try 
                    {
                        object oAddress2Type = null;
                        if (x.property.list_address2Type != null) oAddress2Type = x.property.list_address2Type.longname;
                        sAddress = SpecialText_Global_Address(x.property.address, x.property.address2, oAddress2Type, x.property.city, x.property.state, x.property.fk_zipcode, x.property.zip4, false); 
                    }
                    catch { }
                    if (!string.IsNullOrEmpty(sAddress)) sb.Append(sAddress);
                }
                sb.Append("</div>");
            }
        }
        catch { sb = new StringBuilder("Error Constructing Participant"); }
        if (string.IsNullOrEmpty(sb.ToString())) sb.Append("No Participant Record");
        return sb.ToString();
    }

    public static string SpecialText_Global_PK_Creator_Modifier(object oPK, object oCreated, object oCreatedBy, object oModified, object oModifiedBy)
    {
        string s = string.Empty;
        string sCreated = string.Empty;
        string sCreatedBy = string.Empty;
        string sModified = string.Empty;
        string sModifiedBy = string.Empty;
        string sPK = string.Empty;
        DateTime? dtCreated;
        DateTime? dtModified;
        try
        {
            dtCreated = Convert.ToDateTime(oCreated);
            if (dtCreated != null) sCreated = WACGlobal_Methods.Format_Global_Date(dtCreated);
            else sCreated = "unknown date";
        }
        catch { sCreated = "unknown date"; }

        try
        {
            dtModified = Convert.ToDateTime(oModified);
            sModified = WACGlobal_Methods.Format_Global_Date(dtModified);
        }
        catch { }

        try { sCreatedBy = oCreatedBy.ToString(); }
        catch { sCreatedBy = "unknown"; }

        try { sModifiedBy = oModifiedBy.ToString(); }
        catch { }

        try { sPK = oPK.ToString(); }
        catch { sPK = "No PK"; }

        if (!string.IsNullOrEmpty(sModified) && !string.IsNullOrEmpty(sModifiedBy)) s = "created on " + sCreated + " by " + sCreatedBy + ", modified on " + sModified + " by " + sModifiedBy + " (PK: " + oPK.ToString() + ")";
        else s = "created on " + sCreated + " by " + sCreatedBy + ", not yet modified" + " (PK: " + sPK + ")"; ;
        return s;
    }

    public static string SpecialText_Global_TaxParcel_ID_OwnerStr(object oTaxParcelID, object oOwnerStr, bool bShowTaxParcelIDText)
    {
        string s = string.Empty;
        try 
        {
            if (bShowTaxParcelIDText) s = "<i>Tax Parcel ID:</i> ";
            s += oTaxParcelID.ToString(); 
        }
        catch { }
        try
        {
            if (!string.IsNullOrEmpty(s)) s += " (" + oOwnerStr.ToString() + ")";
            else s = oOwnerStr.ToString();
        }
        catch { }
        return s;
    }

    public static string SpecialText_Global_TitleAndSubtitle(object oTitle, object oSubtitle)
    {
        string s = string.Empty;
        string sTitle = string.Empty;
        try
        {
            sTitle = oTitle.ToString();
            if (!string.IsNullOrEmpty(sTitle)) s = sTitle;
        }
        catch { }
        try
        {
            string sSubtitle = oSubtitle.ToString();
            if (!string.IsNullOrEmpty(sTitle) && !string.IsNullOrEmpty(sSubtitle)) s += ": " + sSubtitle;
            else if (!string.IsNullOrEmpty(sSubtitle)) s = sSubtitle;
        }
        catch { }
        return s;
    }

    #endregion

    #region Participant

    public static string SpecialText_Participant_RelatedAreas(object oPK_Participant)
    {
        StringBuilder sb = new StringBuilder();
        int? iPK_Participant = null;
        try 
        { 
            iPK_Participant = Convert.ToInt32(oPK_Participant);
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                int iAgriculture = wDataContext.farmBusinesses.Where(w => w.farmBusinessOwners.First(f => f.fk_participant == iPK_Participant).fk_participant == iPK_Participant).Count();
                //int iF2MEvents = wDataContext.events.Where(w => w.eventRegistrants.First(f => f.fk_participant == iPK_Participant).fk_participant == iPK_Participant).Count();
                //int iF2MPureCatskills = wDataContext.farmToMarkets.Where(w => w.fk_participant == iPK_Participant).Count();
                //int iForestryBMP = wDataContext.forestryBMPs.Where(w => w.forestryBMP_owners.First(f => f.fk_participant == iPK_Participant).fk_participant == iPK_Participant).Count();
                //int iForestryFMP = wDataContext.forestryFMPs.Where(w => w.forestryFMP_owners.First(f => f.fk_participant == iPK_Participant).fk_participant == iPK_Participant).Count();
                //int iForestryMAP = wDataContext.forestryMAPs.Where(w => w.forestryMAP_owners.First(f => f.fk_participant == iPK_Participant).fk_participant == iPK_Participant).Count();
                //int iForestryTours = wDataContext.forestryTours.Where(w => w.fk_participant_contact == iPK_Participant).Count();
                //int iForestryEvents = wDataContext.eventDateForestries.Where(w => w.eventAttendeeForestries.First(f => f.fk_participant == iPK_Participant).fk_participant == iPK_Participant).Count();

                sb.Append("<table style='margin-left:20px;' cellpadding='3'>");

                sb.Append("<tr valign='top'><td><table cellpadding='3'>");

                sb.Append("<tr><td class='B taR'>Agriculture:</td><td>" + iAgriculture + "</td></tr>");
                sb.Append("<tr><td class='B taR'>Easements:</td><td>[pending]</td></tr>");
                //sb.Append("<tr><td class='B taR'>Farm To Market Events:</td><td>" + iF2MEvents + "</td></tr>");
                //sb.Append("<tr><td class='B taR'>Farm To Market Pure Catskills:</td><td>" + iF2MPureCatskills + "</td></tr>");

                sb.Append("</table></td>");
                sb.Append("<td><table cellpadding='3'>");

                //sb.Append("<tr><td class='B taR'>Forestry BMP:</td><td>" + iForestryBMP + "</td></tr>");
                //sb.Append("<tr><td class='B taR'>Forestry FMP:</td><td>" + iForestryFMP + "</td></tr>");
                //sb.Append("<tr><td class='B taR'>Forestry MAP:</td><td>" + iForestryMAP + "</td></tr>");
                //sb.Append("<tr><td class='B taR'>Forestry Bus Tours:</td><td>" + iForestryTours + "</td></tr>");
                //sb.Append("<tr><td class='B taR'>Forestry Events:</td><td>" + iForestryEvents + "</td></tr>");

                sb.Append("</table></td></tr>");

                sb.Append("</table>");
            }
        }
        catch { sb.Append("Could not rectify participant."); }

        return sb.ToString();
    }

    #endregion

    #endregion

    #region Validation

    public static bool Validation_isValidUrl(ref string url)
    {
        string pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
        Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return reg.IsMatch(url);
    }
    public static bool IsValidSelectedValue(DropDownList ddl)
    {
        return !string.IsNullOrEmpty(ddl.SelectedValue) && !ddl.SelectedValue.ToLower().Contains("select");
    }
    #endregion

    #region Views

    // NO LONGER USED
    public static void View_Agriculture_BMP_Financial(DropDownList ddl, int iFarmBusinessPK, int iBMP_AgPK, int iFormWFP3PK, int? iValue)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.vw_bmp_ag_financials.Where(w => w.pk_bmp_ag != iBMP_AgPK && w.fk_farmBusiness == iFarmBusinessPK && w.pk_form_wfp3 == iFormWFP3PK && w.balance > 0 && w.fk_statusBMP_code == "C").OrderBy(o => o.bmp_nbr).Select(s => new { s.pk_bmp_ag, s.bmp_nbr, s.balance });
            foreach (var c in a)
            {
                string sBoundValue = c.pk_bmp_ag + "|" + c.balance;
                string sBalance = c.bmp_nbr + " (" + Format_Global_Currency(c.balance) + ")";
                ddl.Items.Add(new ListItem(sBalance, sBoundValue));
            }
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (iValue != null) ddl.SelectedValue = iValue.ToString();
        }
    }

    public static void View_Agriculture_FarmBusinessMail_Candidates_DDL(DropDownList ddl, int iPKFarmBusiness, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.vw_farmBusinessMail_candidates.Where(w => w.fk_farmBusiness == iPKFarmBusiness).Select(s => new { PK = s.fk_participant, NAME = s.Participant });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            }
        }
    }

    public static int? View_Agriculture_PlannerPK_By_FarmBusinessPK(int iPK_FarmBusiness)
    {
        int? i = null;
        try
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                i = wac.vw_farmBusiness_planners.Where(w => w.pk_farmBusiness == iPK_FarmBusiness).Select(s => s.fk_list_designerEngineer).Single();
            }
        }
        catch { }
        return i;
    }

    public static string View_Agriculture_SupplementalAgreement(object oPK_SATP, Enum_Agriculture_SupplementalAgreement_View_StringReturned enumStringReturned)
    {
        string str = string.Empty;
        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
        {
            try
            {
                int iPK_SATP = Convert.ToInt32(oPK_SATP);
                switch (enumStringReturned)
                {
                    case Enum_Agriculture_SupplementalAgreement_View_StringReturned.FarmID_BMPs:
                        var a = wac.vw_supplementalAgreements.Where(w => w.pk_supplementalAgreementTaxParcel == iPK_SATP).Select(s => s.Tax_Parcel_BMPs).Distinct();
                        if (a.Count() == 1) str = a.Single();
                        break;
                    case Enum_Agriculture_SupplementalAgreement_View_StringReturned.PK_FarmBusiness:
                        var b = wac.vw_supplementalAgreements.Where(w => w.pk_supplementalAgreementTaxParcel == iPK_SATP).Select(s => s.pk_farmBusiness).Distinct();
                        if (b.Count() == 1) str = b.Single().ToString();
                        break;
                }
            }
            catch { }
        }
        return str;
    }

    public static void View_Agriculture_SupplementalAgreement_TaxParcel_TaxParcelOwners_DDL(DropDownList ddl, int? iValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            ddl.Items.Clear();
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.vw_supplementalAgreement_TPOs.OrderBy(o => o.TP_Owner).Select(s => new { PK = s.pk_participant, NAME = s.TP_Owner });
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
                catch { }
            }
        }
    }

    public static void View_Agriculture_SupplementalAgreement_DDL(DropDownList ddl, int? iValue, bool bShowSelect)
    //public static void View_Agriculture_SupplementalAgreement_DDL(DropDownList ddl, string value, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.vw_supplementalAgreements.Where(w => w.pk_farmBusiness != null && w.agreement_nbr_ro != null).OrderBy(o => o.agreement_nbr_ro).Select(s => new { PK = s.pk_farmBusiness, NAME = SpecialText_Agriculture_SA_AgreementNumberFarmIDTaxParcelOwner(s.agreement_nbr_ro, s.farmID, s.Tax_Parcel_Owner) }).Distinct();
                //ddl.ListSource = wac.vw_supplementalAgreements.Where(w => w.pk_farmBusiness != null && w.agreement_nbr_ro != null).OrderBy(o => o.agreement_nbr_ro).Select(s => new { PK = s.pk_farmBusiness, NAME = s.agreement_nbr_ro + " [" + s.farmID + " - " + s.Tax_Parcel_Owner + "]" }).Distinct();
                //ddl.ListSource = wac.vw_supplementalAgreements.Where(w => w.SA_TP_Cancelled == null && w.Farm_Owner != null).OrderBy(o => o.Farm_Owner).Select(s => new { PK = s.pk_farmBusiness, NAME = s.Farm_Owner + " [" + s.agreement_nbr_ro + "]" }).Distinct();
                //ddl.ListSource = wac.vw_supplementalAgreements.Where(w => w.SA_TP_Cancelled == null && w.Farm_Owner != null).OrderBy(o => o.Farm_Owner).Select(s => s.Farm_Owner).Distinct();
                //ddl.ListSource = wac.vw_supplementalAgreements.Where(w => w.SA_TP_Cancelled == null && w.Farm_Owner != null).OrderBy(o => o.Farm_Owner).Select(s => new { PK = s.Farm_Owner).Distinct();
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                //try { if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value; }
                try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
                catch { }
            }
        }
    }

    public static void View_Agriculture_SupplementalAgreement_ByFarmBusiness_DDL(DropDownList ddl, int iPK_FarmBusiness, int? iValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.vw_supplementalAgreements.Where(w => w.pk_farmBusiness == iPK_FarmBusiness && w.agreement_nbr_ro != null).OrderBy(o => o.agreement_nbr_ro).Select(s => new { PK = s.pk_supplementalAgreement, NAME = SpecialText_Agriculture_BMP_SA(s.pk_supplementalAgreement) }).Distinct();
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
                catch { }
            }
        }
    }

    public static void View_Agriculture_WFP2VersionSA_ByFarmBusiness_DDL(DropDownList ddl, int iPK_FarmBusiness, int? iValue, bool bShowSelect)
    {
        if (ddl != null)
        {
            using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
            {
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "PK";
                ddl.DataSource = wac.vw_wfp2_version_SAs.Where(w => w.pk_farmBusiness == iPK_FarmBusiness).OrderBy(o => o.pk_form_wfp2).ThenBy(o => o.Revision).Select(s => new { PK = s.pk_form_wfp2_version, NAME = s.Revision }).Distinct();
                ddl.DataBind();
                if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
                try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
                catch { }
            }
        }
    }

    //public static void View_Agriculture_WFP2VersionSA_ByFarmBusinessAndWFP2_DDL(DropDownList ddl, int iPK_FarmBusiness, int iPK_WFP2, int? iValue, bool bShowSelect)
    //{
    //    if (ddl != null)
    //    {
    //        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //        {
    //            ddl.DataTextField = "NAME";
    //            ddl.DataValueField = "PK";
    //            ddl.ListSource = wac.vw_wfp2_version_SAs.Where(w => w.pk_farmBusiness == iPK_FarmBusiness && w.pk_form_wfp2 == iPK_WFP2).OrderBy(o => o.version).Select(s => new { PK = s.pk_form_wfp2_version, NAME = s.version }).Distinct();
    //            ddl.DataBind();
    //            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
    //            try { if (iValue != null) ddl.SelectedValue = iValue.ToString(); }
    //            catch { }
    //        }
    //    }
    //}

    public static string View_Agriculture_Tier1_Animal_AU(object oFarmBusinessTier1AnimalPK)
    {
        StringBuilder sb = new StringBuilder();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            try
            {
                int iFarmBusinessTier1AnimalPK = Convert.ToInt32(oFarmBusinessTier1AnimalPK);
                var a = wDataContext.vw_farmBusinessTier1Animal_AUs.Where(w => w.pk_farmBusinessTier1Animal == iFarmBusinessTier1AnimalPK).Select(s => s).Single();

                sb.Append("<table cellpadding='3'>");
                sb.Append("<tr valign='top'><td class='B taR'>Animal:</td><td>" + a.animal + "</td></tr>");
                sb.Append("<tr valign='top'><td class='B taR'>Count:</td><td>" + a.cnt + "</td></tr>");
                sb.Append("<tr valign='top'><td class='B taR'>Weight:</td><td>" + a.weight + "</td></tr>");
                sb.Append("<tr valign='top'><td class='B taR'>Weight Total:</td><td>" + a.weightTotal + "</td></tr>");
                sb.Append("<tr valign='top'><td class='B taR'>AU:</td><td>" + a.AU + "</td></tr>");
                sb.Append("</table>");          
            }
            catch { }          
        }
        return sb.ToString();
    }

    //public static void View_Forestry_Mail_Candidates_DDL(DropDownList ddl, int iPK_Forestry, Enum_Forestry_Section enumForestrySection, bool bShowSelect)
    //{
    //    if (ddl != null)
    //    {
    //        ddl.Items.Clear();
    //        using (WACDataClassesDataContext wac = new WACDataClassesDataContext())
    //        {
    //            ddl.DataTextField = "NAME";
    //            ddl.DataValueField = "PK";
    //            switch (enumForestrySection)
    //            {
    //                case Enum_Forestry_Section.BMP: ddl.DataSource = wac.vw_forestryBMP_mail_candidates.Where(w => w.fk_forestryBMP == iPK_Forestry).Select(s => new { PK = s.fk_participant, NAME = s.Participant }); break;
    //                case Enum_Forestry_Section.FMP: ddl.DataSource = wac.vw_forestryFMP_mail_candidates.Where(w => w.fk_forestryFMP == iPK_Forestry).Select(s => new { PK = s.fk_participant, NAME = s.Participant }); break;
    //                case Enum_Forestry_Section.MAP: ddl.DataSource = wac.vw_forestryMAP_mail_candidates.Where(w => w.fk_forestryMAP == iPK_Forestry).Select(s => new { PK = s.fk_participant, NAME = s.Participant }); break;
    //            }
    //            ddl.DataBind();
    //            if (bShowSelect) ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
    //        }
    //    }
    //}

    public static void View_TaxParcel_County(DropDownList ddl, string value)
    {
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            var a = wDataContext.vw_taxParcel_jurisdictions.GroupBy(g => g.county).OrderBy(o => o.Key).Select(s => s.Key);
            ddl.DataSource = a;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    public static void View_TaxParcel_Jurisdiction(DropDownList ddl, string value, string sCounty)
    {
        ddl.Items.Clear();
        using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
        {
            //var a = wDataContext.vw_taxParcel_jurisdictions.Where(w => w.county == sCounty).OrderBy(o => o.jurisdiction).Select(s => new { s.pk_list_swis, s.jurisdiction });
            //ddl.DataTextField = "jurisdiction";
            //ddl.DataValueField = "pk_list_swis";
            //ddl.ListSource = a;
            //ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("[SELECT]", ""));
            //if (!string.IsNullOrEmpty(value)) ddl.SelectedValue = value;
        }
    }

    #endregion

    #region Archived Methods

    //public static void RecordCountLabel(string sControlID, ControlCollection cc, int iRecordCount)
    //{
    //    Control control = FindControl(sControlID, cc);
    //    if (control != null)
    //    {
    //        Label lbl = (Label)control;
    //        lbl.Text = "Records: " + iRecordCount;
    //    }
    //}

    //public static Control FindControl(string controlID, ControlCollection controls)
    //{
    //    foreach (Control c in controls)
    //    {
    //        if (c.ID == controlID)
    //            return c;
    //        if (c.HasControls())
    //        {
    //            Control cTmp = FindControl(controlID, c.Controls);
    //            if (cTmp != null)
    //                return cTmp;
    //        }
    //    }
    //    retur

    #endregion

    
  
}
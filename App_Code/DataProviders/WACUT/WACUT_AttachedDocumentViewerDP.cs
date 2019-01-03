using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using WAC_DataObjects;
using WAC_Exceptions;
using WAC_Event;

namespace WAC_DataProviders
{
    /// <summary>
    /// Summary description for WACUT_AttachedDocumentViewerDP
    /// </summary>
    public class WACUT_AttachedDocumentViewerDP : WACDataProvider
    {
        public WACUT_AttachedDocumentViewerDP() { }
        private IList docLoader(WACEnumerations.WACSectorCodes sc)
        {
            string sSectorCode = WACEnumerations.SectorCodes[sc];
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.documentArchives.Where(w => w.list_participantTypeSectorFolder.fk_participantTypeSector_code == sSectorCode).Select(s => s);
                var c = a.OrderBy(o => o.filename_display).
                    Select(s => new AttachedDocument(s.list_participantTypeSectorFolder.folder, s.filename_actual, s.filename_display, sSectorCode));
                return c.ToList<AttachedDocument>();
            }
        }
        private IList docLoader(object pk, WACEnumerations.WACSectorCodes sc)
        {
            int iPK1 = Convert.ToInt32(pk);
            string sSectorCode = WACEnumerations.SectorCodes[sc];
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.documentArchives.Where(w => w.PK_1 == iPK1 &&
                w.list_participantTypeSectorFolder.fk_participantTypeSector_code == sSectorCode).Select(s => s);
                var c = a.OrderBy(o => o.filename_display).
                    Select(s => new AttachedDocument(s.list_participantTypeSectorFolder.folder, s.filename_actual, s.filename_display, sSectorCode));
                return c.ToList<AttachedDocument>();
            }            
        }
        private IList docLoader(object mk, object pk, WACEnumerations.WACSectorCodes sc)
        {
            int iPK1 = Convert.ToInt32(mk);
            int? iPK2 = Convert.ToInt32(pk);
            string sSectorCode = WACEnumerations.SectorCodes[sc];
            using (WACDataClassesDataContext wDataContext = new WACDataClassesDataContext())
            {
                var a = wDataContext.documentArchives.Where(w => w.PK_1 == iPK1 && w.PK_2 == iPK2 &&
                    w.list_participantTypeSectorFolder.fk_participantTypeSector_code == sSectorCode).Select(s => s);
                var c = a.OrderBy(o => o.filename_display).
                    Select(s => new AttachedDocument(s.list_participantTypeSectorFolder.folder, s.filename_actual, s.filename_display, sSectorCode));
                return c.ToList<AttachedDocument>();  
            }          
        }
        public override IList GetList()
        {
            throw new NotImplementedException();
        }

        public override IList GetFilteredList(List<WACParameter> parms)
        {
            WACEnumerations.WACSectorCodes sectorCode = (WACEnumerations.WACSectorCodes)WACParameter.GetParameterValue(parms,
                WACParameter.ParameterType.SectorCode);
            object pk = WACParameter.GetParameterValue(parms, WACParameter.ParameterType.PrimaryKey);
            object mk = WACParameter.GetParameterValue(parms, WACParameter.ParameterType.MasterKey);
            if (mk == null && pk == null)
                return docLoader(sectorCode);
            if (mk != null && pk == null)
                return docLoader(mk, sectorCode);
            if (mk == null && pk != null)
                return docLoader(pk, sectorCode);
            if (mk != null && pk != null)
                return docLoader(mk, pk, sectorCode);
            else
                return null;
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
            return null;
        }

        public override string PrimaryKeyName()
        {
            return AttachedDocument.PrimaryKeyName;
        }

        public override IList SortedList(IList _iList, string sortExpression, string sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
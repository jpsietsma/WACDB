using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;
using System.Text;
using System.Configuration;

namespace WAC_DataObjects
{
    /// <summary>
    /// Summary description for AttachedDocument
    /// </summary>
    public class AttachedDocument : WACDataObject
    {
        public const string MasterKeyName = "mk";
        public const string PrimaryKeyName = "pk";
        public object pk { get; set; }
        public object mk { get; set; }
        public string Folder { get; set; }
        public string ALink { get; set; }
        public string Area { get; set; }
        public string Sector { get; set; }
        public string PathName { get; set; }
        public string DisplayFileName { get; set; }

        public AttachedDocument(string _folder, string _hlink)
        {
            Folder = _folder;
            ALink = _hlink;
        }
        public AttachedDocument(string _folder, string _path, string _fileName, WACEnumerations.WACSectorCodes _code)
        {
            Folder = _folder;
            PathName = _path;
            DisplayFileName = _fileName;
            setAreaAndSector(_code);
            ALink = getEncodedALink();
        }
        public AttachedDocument(string _folder, string _path, string _fileName, string _code)
        {
            Folder = _folder;
            PathName = _path;
            DisplayFileName = _fileName;
            string[] sectorAndArea = null;
            try
            {
                sectorAndArea = _code.Split('_');
            }
            catch (Exception) { } // fail silently

            Area = sectorAndArea != null ? sectorAndArea[0] : string.Empty;
            Sector = sectorAndArea.Count() > 1 ? sectorAndArea[1] : string.Empty;
            ALink = getEncodedALink();
        }
        private void setAreaAndSector(WACEnumerations.WACSectorCodes sectorCode)
        {
            string[] sectorAndArea = null;
            try
            {
                sectorAndArea = WACEnumerations.SectorCodes[sectorCode].Split('_');
            }
            catch (Exception) { } // fail silently
            
            Area = sectorAndArea != null ? sectorAndArea[0] : string.Empty;
            Sector = sectorAndArea.Count() > 1 ? sectorAndArea[1] : string.Empty;
        }
        private string getEncodedALink()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href='" + ConfigurationManager.AppSettings["DocsLink"] + Area + "/" + PathName + "'");
            if (!string.IsNullOrEmpty(DisplayFileName)) sb.Append("' target='_blank'>" + DisplayFileName + "</a>");
            else sb.Append("' target='_blank'>" + PathName + "</a>");

            return sb.ToString();
        }

        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
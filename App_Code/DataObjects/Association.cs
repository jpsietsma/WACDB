using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAC_Extensions;
using System.Reflection;

/// <summary>
/// Summary description for Association
/// </summary>
namespace WAC_DataObjects
{
    public class Association : WACDataObject
    {
        public const string PrimaryKeyName = "PK";
        public string source { get; set; }
        public int PK { get; set; }
        public string Label { get; set; }
        public string TableGo { get; set; }
        public string TablePK { get; set; }

        public Association()
        {

        }

        public Association(string _source, int _pk, string _label, string _tableGo, string _tablePK)
        {
            source = _source;
            PK = _pk;
            Label = _label;
            TableGo = _tableGo;
            TablePK = _tablePK;

        }


        public override string PrimaryKeyAsString()
        {
            throw new NotImplementedException();
        }
    }
}
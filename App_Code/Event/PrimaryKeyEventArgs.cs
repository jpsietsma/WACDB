using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PrimaryKeyEventArgs
/// </summary>
public class PrimaryKeyEventArgs : EventArgs
{
    public int PrimaryKey { get; set; }
    public string PrimaryKeyString { get; set; }
    public PrimaryKeyEventArgs(Int32 pk)
    {
        PrimaryKey = pk;
    }
    public PrimaryKeyEventArgs(string pk)
    {
        PrimaryKeyString = pk;
    }
    
}
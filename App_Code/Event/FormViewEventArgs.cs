using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for FormViewEventArgs
/// </summary>
public class FormViewEventArgs : EventArgs
{
	public FormViewEventArgs(int pk, FormViewMode fvm)
    {
        primaryKey = pk;
        viewMode = fvm;
    }
    public FormViewEventArgs(int pk, int fk, FormViewMode fvm)
    {
        primaryKey = pk;
        foreignKey = fk;
        viewMode = fvm;
    }
    public FormViewEventArgs(int pk, string fType)
    {
        primaryKey = pk;
        formType = fType;
    }
    public FormViewEventArgs(int pk, int fk, string fType)
    {
        primaryKey = pk;
        foreignKey = fk;
        formType = fType;
    }
    public FormViewEventArgs(int pk, int fk, string fType, FormViewMode fvm)
    {
        primaryKey = pk;
        foreignKey = fk;
        formType = fType;
        viewMode = fvm;
    }

    private int foreignKey;
    public int ForeignKey
    {
        get { return foreignKey; }
        set { foreignKey = value; }
    }

    private int primaryKey;
    public int PrimaryKey
    {
        get { return primaryKey; }
        set { primaryKey = value; }
    }
    private FormViewMode viewMode;
    public FormViewMode ViewMode 
    {
        get { return viewMode;}
        set { viewMode = value; }
    }
    private string formType;
    public string FormType 
    {
        get { return formType;}
        set { formType = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;

public class SPGenerator {
    const int tabSize = 5;
    public static string getSprocText(ProjectClass pClass, bool copyResultsToClipboard, string creatorName) {
        StringBuilder strB = new StringBuilder();
        if (pClass.Name.Text.Length > 0) {
            strB.Append(getSummaryText(creatorName, "Add a new  " + pClass.Name.Capitalized + " to the database."));
            strB.Append(getAddSprocText(pClass));
            strB.Append(getGrantAccessScript("sproc_" + pClass.Name.Capitalized + "Add", pClass.DALClassVariable.EditOnlyConnectionstring.UserName));

            strB.Append(getSummaryText(creatorName, "Update " + pClass.Name.Capitalized + " in the database."));
            strB.Append(getUpdateSprocText(pClass));
            strB.Append(getGrantAccessScript("sproc_" + pClass.Name.Capitalized + "Update", pClass.DALClassVariable.EditOnlyConnectionstring.UserName));

            strB.Append(getSummaryText(creatorName, "Retrieve specific " + pClass.Name.Capitalized + " from the database."));
            strB.Append(getSingleItemSprocText(pClass));
            strB.Append(getGrantAccessScript("sproc" + pClass.Name.Capitalized + "Get", pClass.DALClassVariable.ReadOnlyConnectionString.UserName));

            strB.Append(getSummaryText(creatorName, "Retrieve all " + pClass.Name.PluralAndCapitalized + " from the database."));
            strB.Append(getAllItemsSprocText(pClass));
            strB.Append(getGrantAccessScript("sproc" + pClass.Name.PluralAndCapitalized + "GetAll", pClass.DALClassVariable.ReadOnlyConnectionString.UserName));

            strB.Append(getSummaryText(creatorName, "Remove specific " + pClass.Name.Capitalized + " from the database."));
            strB.Append(getRemoveItemSprocText(pClass));
            strB.Append(getGrantAccessScript("sproc_" + pClass.Name.Capitalized + "Remove", pClass.DALClassVariable.EditOnlyConnectionstring.UserName));

            // TODO: Add SPROCs for Foreign Key Relations.
            // For Each classVar As ClassVariable In pClass.ClassVariables
            // If classVar.IsForeignKey Then
            // 
            // strB.Append(getSummaryText(creatorName, "Retrieve all " & pClass.Name.PluralAndCapitalized & " from the database for a specific " _
            // & "TYPE OF OBJECT"))
            // strB.Append(getAllItemsSprocText(pClass))
            // strB.Append(getGrantAccessScript("sproc" & pClass.Name.PluralAndCapitalized & "GetAll", pClass.DALClassVariable.ReadOnlyConnectionString.UserName))
            // End If
            // Next

            if (copyResultsToClipboard) {
                Clipboard.Clear();
                Clipboard.SetText(strB.ToString());
            }
        } else if (pClass.Name.Text.Length == 0)
            MessageBox.Show("You must provide an object name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return strB.ToString();
    }
    private static string getSummaryText(string creator, string description) {
        return string.Format("-- ============================================={4}" + "-- Author:{3}{3}{0}{4}" + "-- Create date:{3}{1}{4}" + "-- Description:{3}{2}{4}" + "-- ============================================={4}", creator, DateTime.Now.ToString("d MMM yyyy"), description, Constants.vbTab, Constants.vbCrLf);
    }
    private static string getGrantAccessScript(string StoredProcedureName, string userName) {
        string retString = "";
        retString += "GRANT EXECUTE ON dbo." + StoredProcedureName + " TO " + userName + Constants.vbCrLf;
        retString += "GO" + Constants.vbCrLf;
        return retString;
    }
    public static string getUpdateSprocText(ProjectClass pClass) {
        string retString = "CREATE PROCEDURE dbo.sproc_" + pClass.Name.Capitalized + "Update" + Constants.vbCrLf;
        int count = 0;
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            count += 1;
            retString += "@" + classVar.DatabaseColumnName + " " + classVar.DatabaseTypeWithLength;
            if (count < pClass.ClassVariables.Count)
                retString += ",";
            retString += Constants.vbCrLf;
        }
        retString += "AS" + Constants.vbCrLf;
        retString += Strings.Strings.Space((int)tabSize) + "UPDATE " + pClass.DatabaseTableName + Constants.vbCrLf;
        retString += Strings.Strings.Space((int)tabSize * 2) + "SET" + Constants.vbCrLf;
        count = 0;

        foreach (ClassVariable classVar in pClass.ClassVariables) {
            count += 1;
            if (pClass.ValueVariable != classVar) {
                retString += Strings.Strings.Space((int)tabSize * 3) + classVar.DatabaseColumnName + " = @" + classVar.DatabaseColumnName;
                if (count < pClass.ClassVariables.Count)
                    retString += ",";
                retString += Constants.vbCrLf;
            }
        }

        retString += Strings.Strings.Space((int)tabSize * 2) + "WHERE " + pClass.ValueVariable.DatabaseColumnName + " = @" + pClass.ValueVariable.DatabaseColumnName + Constants.vbCrLf + "GO" + Constants.vbCrLf + Constants.vbCrLf;
        return retString;
    }
    public static string getAddSprocText(ProjectClass pClass) {
        string retString = "CREATE PROCEDURE dbo.sproc_" + pClass.Name.Capitalized + "Add" + Constants.vbCrLf;
        int count = 0;
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            count += 1;
            if (classVar == pClass.ValueVariable)
                retString += "@" + classVar.DatabaseColumnName + " " + classVar.DatabaseType + " OUTPUT";
            else
                retString += "@" + classVar.DatabaseColumnName + " " + classVar.DatabaseTypeWithLength;
            if (count < pClass.ClassVariables.Count)
                retString += ",";
            retString += Constants.vbCrLf;
        }
        retString += "AS" + Constants.vbCrLf;
        retString += Strings.Strings.Space((int)tabSize) + "INSERT INTO " + pClass.DatabaseTableName + "(";
        count = 0;

        foreach (ClassVariable classVar in pClass.ClassVariables) {
            count += 1;
            if (classVar != pClass.ValueVariable) {
                retString += classVar.DatabaseColumnName;
                if (count < pClass.ClassVariables.Count)
                    retString += ",";
                else
                    retString += ")";
            }
        }
        retString += Constants.vbCrLf + Strings.Strings.Space((int)tabSize * 3) + "VALUES(";
        count = 0;
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            count += 1;
            if (classVar != pClass.ValueVariable) {
                retString += "@" + classVar.DatabaseColumnName;
                if (count < pClass.ClassVariables.Count)
                    retString += ",";
                else
                    retString += ")";
                if (count % 5 == 0)
                    retString += Constants.vbCrLf + Strings.Strings.Space((int)tabSize * 3);
            }
        }
        retString += Constants.vbCrLf;
        retString += Strings.Strings.Space((int)tabSize) + "SET @" + pClass.ValueVariable.DatabaseColumnName + " = @@IDENTITY" + Constants.vbCrLf + "GO" + Constants.vbCrLf + Constants.vbCrLf;
        return retString;
    }
    public static string getAllItemsSprocText(ProjectClass pClass) {
        string retString = "CREATE PROCEDURE dbo.sproc" + pClass.Name.PluralAndCapitalized + "GetAll" + Constants.vbCrLf + "AS" + Constants.vbCrLf;
        retString += getSelectText("SELECT * FROM " + pClass.DatabaseTableName);
        return retString;
    }
    public static string getSingleItemSprocText(ProjectClass pClass) {
        // TODO: Fix this to handle multiple Primary Keys
        string retString = "CREATE PROCEDURE dbo.sproc" + pClass.Name.Capitalized + "Get" + Constants.vbCrLf;
        retString += "@" + pClass.ValueVariable.DatabaseColumnName + " " + pClass.ValueVariable.DatabaseType + Constants.vbCrLf + "AS" + Constants.vbCrLf;
        retString += getSelectText("SELECT * FROM " + pClass.DatabaseTableName + Constants.vbCrLf + Strings.Strings.Space((int)tabSize) + "WHERE " + pClass.ValueVariable.DatabaseColumnName + " = @" + pClass.ValueVariable.DatabaseColumnName);
        return retString;
    }
    public static string getRemoveItemSprocText(ProjectClass pClass) {
        // TODO: Fix this to handle multiple Primary Keys
        return string.Format("CREATE PROCEDURE dbo.sproc_{0}Remove{5}@{1} {2}{5}AS{5}BEGIN{5}"
                             + "{4}DELETE FROM {3}{5}{4}{4}WHERE {1} = @{1}{5}"
                             + "{5}{4}-- Return -1 if we had an error{5}"
                             + "{4}IF @@ERROR > 0{5}{4}BEGIN{5}{4}{4}RETURN -1{5}{4}END{5}{4}"
                             + "ELSE{5}{4}BEGIN{5}{4}{4}RETURN 1{5}{4}END{5}END{5}GO{5}{5}", pClass.Name.Capitalized, pClass.ValueVariable.DatabaseColumnName, pClass.ValueVariable.DatabaseType, pClass.DatabaseTableName, Strings.Strings.Space((int)tabSize), Constants.vbCrLf);
    }
    private static string getSelectText(string selectStatement) {
        string retstring = "BEGIN" + Constants.vbCrLf;
        retstring += Strings.Strings.Space((int)tabSize) + "-- SET NOCOUNT ON added to prevent extra result sets from" + Constants.vbCrLf;
        retstring += Strings.Strings.Space((int)tabSize) + "-- interfering with SELECT statements." + Constants.vbCrLf;
        retstring += Strings.Strings.Space((int)tabSize) + "SET NOCOUNT ON;" + Constants.vbCrLf + Constants.vbCrLf;
        retstring += Strings.Strings.Space((int)tabSize) + selectStatement + Constants.vbCrLf;
        retstring += "END" + Constants.vbCrLf;
        retstring += "GO" + Constants.vbCrLf + Constants.vbCrLf;
        return retstring;
    }
}

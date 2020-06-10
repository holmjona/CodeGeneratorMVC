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
using System.Drawing;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;

public class ClassGenerator {
    public static string getDataReaderText(ProjectClass pClass, bool copyToClipboard, bool overridesBase, language lang) {
        if (lang == language.VisualBasic)
            return getDataReaderTextInVB(pClass, copyToClipboard, overridesBase);
        else
            return getDataReaderTextInCSharp(pClass, copyToClipboard, overridesBase);
    }
    private static string getDataReaderTextInVB(ProjectClass pClass, bool copyToClipboard, bool overridesBase) {
        StringBuilder retStrB = new StringBuilder();
        if (pClass.Name.Text.Length > 0) {
            retStrB.Append(cg.getMetaDataText("Fills object from a SqlClient Data Reader", false, tab.XX, language.VisualBasic));
            retStrB.AppendLine(Space(tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() + "Sub Fill(ByVal dr As Data.SqlClient.SqlDataReader)");
            foreach (ClassVariable classVar in pClass.ClassVariables) {
                if (classVar.isAssociative)
                    continue;
                retStrB.Append(Space(tab.XXX));
                if (!classVar.IsDatabaseBound || classVar.ParameterType.IsImage)
                    continue;
                bool AttributeAndObjectHaveSameName = classVar.Name == classVar.ParameterType.Name(language.VisualBasic);
                string DatabaseParameterName = "db_" + classVar.Name;
                if (!cg.isRegularDataType(classVar.ParameterType.Name(language.VisualBasic)) && AttributeAndObjectHaveSameName)
                    DatabaseParameterName = classVar.ParameterType.Name(language.VisualBasic) + ".db_ID";
                if (classVar.ParameterType.IsNameAlias) {
                    retStrB.AppendLine("_" + classVar.Name + ".TextUnFormatted = dr(" + DatabaseParameterName + ").ToString()");
                    retStrB.Append(Space(tab.XXX));
                } else if (!cg.isRegularDataType(classVar.ParameterType.Name(language.VisualBasic))) {
                    if (AttributeAndObjectHaveSameName)
                        retStrB.AppendLine("_" + classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized + " = CInt(dr(" + DatabaseParameterName + "))");
                    else
                        retStrB.AppendLine("_" + classVar.Name + "ID = CInt(dr(" + DatabaseParameterName + "))");
                } else {
                    string toStringText = "";
                    if (classVar.ParameterType.Name.ToLower().CompareTo("datetime") == 0)
                        toStringText = ".ToString()";
                    retStrB.AppendLine("_" + classVar.Name + " = " + cg.getConvertFunction(classVar.ParameterType.Name, language.VisualBasic) + "(dr(" + DatabaseParameterName + ")" + toStringText + ")");
                }
            }
            retStrB.AppendLine(Space(tab.XX) + "End Sub");
            if (copyToClipboard) {
                Clipboard.Clear();
                Clipboard.SetText(retStrB.ToString());
            }
        }
        return retStrB.ToString();
    }
    private static string getDataReaderTextInCSharp(ProjectClass pClass, bool copyToClipboard, bool overridesBase) {
        StringBuilder retStrB = new StringBuilder();
        if (pClass.Name.Text.Length > 0) {
            retStrB.Append(cg.getMetaDataText("Fills object from a SqlClient Data Reader", false, tab.XX, language.CSharp));
            retStrB.AppendLine(Space(tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "void Fill(System.Data.SqlClient.SqlDataReader dr)");
            retStrB.AppendLine(Space(tab.XX) + "{");
            foreach (ClassVariable classVar in pClass.ClassVariables) {
                if (classVar.isAssociative)
                    continue;
                retStrB.Append(Space(tab.XXX));
                if (!classVar.IsDatabaseBound || classVar.ParameterType.IsImage)
                    continue;
                bool AttributeAndObjectHaveSameName = classVar.Name == classVar.ParameterType.Name;
                string DatabaseParameterName = "db_" + classVar.Name;
                if (!cg.isRegularDataType(classVar.ParameterType.Name) && AttributeAndObjectHaveSameName)
                    DatabaseParameterName = classVar.ParameterType.Name + ".db_ID";
                if (classVar.ParameterType.IsNameAlias) {
                    retStrB.AppendLine("_" + classVar.Name + ".TextUnFormatted = dr[" + DatabaseParameterName + "].ToString();");
                    retStrB.Append(Space(tab.XXX));
                } else if (!cg.isRegularDataType(classVar.ParameterType.Name)) {
                    if (AttributeAndObjectHaveSameName)
                        retStrB.AppendLine("_" + classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized + " = (int)dr[" + DatabaseParameterName + "];");
                    else
                        retStrB.AppendLine("_" + classVar.Name + "ID = (int)dr[" + DatabaseParameterName + "];");
                } else {
                    string toStringText = "";
                    // If classVar.ParameterType.Name.ToLower().CompareTo("datetime") = 0 Then
                    // toStringText = ".ToString()"
                    // End If
                    retStrB.AppendLine("_" + classVar.Name + " = (" + classVar.ParameterType.Name(language.CSharp) + ")dr[" + DatabaseParameterName + "]" + toStringText + ";");
                }
            }
            retStrB.AppendLine(Space(tab.XX) + "}");
            if (copyToClipboard) {
                Clipboard.Clear();
                Clipboard.SetText(retStrB.ToString());
            }
        }
        return retStrB.ToString();
    }
    private static string getAddUpdateFunctions(ProjectClass pClass, bool copyToClipboard, bool overridesBase, language lang) {
        StringBuilder retStrB = new StringBuilder();
        string DALClassName = pClass.DALClassVariable.Name;
        string objectName = pClass.Name.Capitalized;
        if (objectName.Length > 0 & DALClassName.Length > 0) {
            if (DALClassName.Length > 0 & objectName.Length > 0) {
                retStrB.Append(getAddClassFunction(DALClassName, objectName, overridesBase, lang));
                retStrB.Append(getUpdateClassFunction(DALClassName, objectName, overridesBase, lang));
                retStrB.Append(getRemoveClassFunction(DALClassName, objectName, overridesBase, lang));
            }
            if (copyToClipboard) {
                Clipboard.Clear();
                Clipboard.SetText(retStrB.ToString());
            }
        }
        return retStrB.ToString();
    }
    private static string getAddClassFunction(string dalName, string objectName, bool overridesBase, language lang) {
        StringBuilder retStrB = new StringBuilder(cg.getMetaDataText("Calls DAL function to add " + objectName + " to the database.", false, tab.XX, lang, "Integer value greater than 0 if successful."));

        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Space(tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() + "Function dbAdd() As Integer");
            retStrB.AppendLine(Space(4 + tab.XX) + "_ID = " + dalName + ".Add" + objectName + "(Me)");
            retStrB.AppendLine(Space(4 + tab.XX) + "Return ID");
            retStrB.AppendLine(Space(tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Space(tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "int dbAdd()");
            retStrB.AppendLine(Space(tab.XX) + "{");
            retStrB.AppendLine(Space(4 + tab.XX) + "_ID = " + dalName + ".Add" + objectName + "(this);");
            retStrB.AppendLine(Space(4 + tab.XX) + "return ID;");
            retStrB.AppendLine(Space(tab.XX) + "}");
        }
        retStrB.AppendLine("");
        return retStrB.ToString();
    }
    private static string getUpdateClassFunction(string dalName, string objectName, bool overridesBase, language lang) {
        StringBuilder retStrB = new StringBuilder(cg.getMetaDataText("Calls DAL function to update " + objectName + " to the database.", false, tab.XX, lang, "Integer value greater than 0 if successful."));
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Space(tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() + "Function dbUpdate() As Integer");
            retStrB.AppendLine(Space(tab.XXX) + "Return " + dalName + ".Update" + objectName + "(Me)");
            retStrB.AppendLine(Space(tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Space(tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "int dbUpdate()");
            retStrB.AppendLine(Space(tab.XX) + "{");
            retStrB.AppendLine(Space(4 + tab.XX) + "return " + dalName + ".Update" + objectName + "(this);");
            retStrB.AppendLine(Space(tab.XX) + "}");
        }
        retStrB.AppendLine("");
        return retStrB.ToString();
    }
    private static string getRemoveClassFunction(string dalName, string objectName, bool overridesBase, language lang) {
        overridesBase = false;
        StringBuilder retStrB = new StringBuilder(cg.getMetaDataText("Calls DAL function to remove " + objectName + " from the database.", false, tab.XX, lang, "Integer value greater than 0 if successful."));
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Space(tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() + "Function dbRemove() As Integer");
            retStrB.AppendLine(Space(tab.XXX) + "Return " + dalName + ".Remove" + objectName + "(Me)");
            retStrB.AppendLine(Space(tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Space(tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "int dbRemove()");
            retStrB.AppendLine(Space(tab.XX) + "{");
            retStrB.AppendLine(Space(4 + tab.XX) + "return " + dalName + ".Remove" + objectName + "(this);");
            retStrB.AppendLine(Space(tab.XX) + "}");
        }
        return retStrB.ToString();
    }
    private static string getToStringFunction(string objectName, language lang) {
        StringBuilder retStrB = new StringBuilder();
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Space(tab.XX) + "Public Overrides Function ToString() As String");
            retStrB.AppendLine(Space(tab.XXX) + "Return Me.GetType().ToString()");
            retStrB.AppendLine(Space(tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Space(tab.XX) + "public override string ToString()");
            retStrB.AppendLine(Space(tab.XX) + "{");
            retStrB.AppendLine(Space(4 + tab.XX) + "return this.GetType().ToString();");
            retStrB.AppendLine(Space(tab.XX) + "}");
        }
        return retStrB.ToString();
    }
    private static string getConstructors(string objName, language lang) {
        StringBuilder retStrB = new StringBuilder();
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Space(tab.XX) + "Public Sub New()");
            retStrB.AppendLine(Space(tab.XX) + "End Sub");
            retStrB.AppendLine(Space(tab.XX) + "Friend Sub New(ByVal dr As System.Data.SqlClient.SqlDataReader)");
            retStrB.AppendLine(Space(tab.XXX) + "Fill(dr)");
            retStrB.AppendLine(Space(tab.XX) + "End Sub");
        } else {
            retStrB.AppendLine(Space(tab.XX) + "public " + objName + "()");
            retStrB.AppendLine(Space(tab.XX) + "{");
            retStrB.AppendLine(Space(tab.XX) + "}");
            retStrB.AppendLine(Space(tab.XX) + "internal " + objName + "(System.Data.SqlClient.SqlDataReader dr)");
            retStrB.AppendLine(Space(tab.XX) + "{");
            retStrB.AppendLine(Space(tab.XXX) + "Fill(dr);");
            retStrB.AppendLine(Space(tab.XX) + "}");
        }
        return retStrB.ToString();
    }
    public static string getEntireClass(ProjectClass pClass, bool copyResultsToClipboard, string creator, language lang, CodeGeneration.Format codeFormat) {
        StringBuilder retStrB = new StringBuilder();
        string namSpace = "";
        if (pClass.NameSpaceVariable != null)
            namSpace = pClass.NameSpaceVariable.NameBasedOnID;
        string Comments = pClass.Summary;
        // Dim txtProperties As String = txtInVars.Text.Trim
        string dalClassName = pClass.DALClassVariable.Name;
        string objName = pClass.Name.Capitalized;
        if (objName.Length > 0 & dalClassName.Length > 0) {
            if (objName.Length > 0) {
                if (lang == language.VisualBasic)
                    retStrB.Append(getEntireClassInVB(pClass, objName, namSpace, Comments, copyResultsToClipboard, creator, codeFormat));
                else
                    retStrB.Append(getEntireClassInCSharp(pClass, objName, namSpace, Comments, copyResultsToClipboard, creator, codeFormat));
            }
            if (copyResultsToClipboard) {
                Clipboard.Clear();
                Clipboard.SetText(retStrB.ToString());
            }
        } else {
            if (pClass.Name.Text.Length == 0)
                MessageBox.Show("You must provide an Object/Class name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (dalClassName.Length == 0)
                MessageBox.Show("You must provide a Data Access Layer Class name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return retStrB.ToString();
    }
    private static string getEntireClassInVB(ProjectClass pClass, string objName, string namSpace, string Comments, bool copyResultsToClipboard, string creator, CodeGeneration.Format codeFormat) {
        StringBuilder retStrB = new StringBuilder();
        string properties = getProperties(pClass, copyResultsToClipboard, language.VisualBasic, codeFormat);
        retStrB.AppendLine("'Created By: " + creator + " (using Code generator)");
        retStrB.AppendLine("'Created On: " + DateTime.Now.ToString());
        retStrB.AppendLine("Option Strict On");
        retStrB.AppendLine("Imports Microsoft.VisualBasic");
        retStrB.AppendLine("Imports System.Collections.Generic");
        if (codeFormat == CodeGeneration.Format.MVC)
            retStrB.AppendLine("Imports System.ComponentModel.DataAnnotations");
        if (properties.ToLower().Contains("xmlignore"))
            retStrB.AppendLine("Imports System.Xml.Serialization");
        retStrB.AppendLine("Namespace " + namSpace);
        retStrB.AppendLine(cg.getMetaDataText(Comments, false, tab.XX, language.VisualBasic));
        retStrB.AppendLine(Space(tab.X) + "Public Class " + objName);
        bool overridesBase = false;
        if (pClass.BaseClass != null && !string.IsNullOrEmpty(pClass.BaseClass.Name)) {
            retStrB.AppendLine(Space(tab.X) + "Inherits " + pClass.BaseClass.Name);
            overridesBase = pClass.BaseClass.Name.ToLower().Contains("databaserecord");
        }
        retStrB.AppendLine("");
        retStrB.Append(getCodeBody(objName, pClass, properties, copyResultsToClipboard, overridesBase, language.VisualBasic));
        retStrB.AppendLine(Space(tab.X) + "End Class");
        retStrB.AppendLine("End Namespace");

        return retStrB.ToString();
    }
    private static string getEntireClassInCSharp(ProjectClass pClass, string objName, string namSpace, string Comments, bool copyResultsToClipboard, string creator, CodeGeneration.Format codeFormat) {
        StringBuilder retStrB = new StringBuilder();

        string properties = getProperties(pClass, copyResultsToClipboard, language.CSharp, codeFormat);
        retStrB.AppendLine("//Created By: " + creator + " (using Code generator)");
        retStrB.AppendLine("//Created On: " + DateTime.Now.ToString());
        retStrB.AppendLine("using System;");
        retStrB.AppendLine("using System.Net;");
        retStrB.AppendLine("using System.Linq;");
        retStrB.AppendLine("using System.Collections.Generic;");
        if (codeFormat == CodeGeneration.Format.MVC)
            retStrB.AppendLine("using System.ComponentModel.DataAnnotations;");
        if (properties.ToLower().Contains("xmlignore"))
            retStrB.AppendLine("using System.Xml.Serialization;");
        retStrB.AppendLine("namespace " + namSpace);
        retStrB.AppendLine("{");
        retStrB.AppendLine(cg.getMetaDataText(Comments, false, tab.XX, language.CSharp));
        retStrB.Append(Strings.Space(4) + "public class " + objName);
        bool overridesBase = false;
        if (pClass.BaseClass != null && !string.IsNullOrEmpty(pClass.BaseClass.Name)) {
            retStrB.Append(" : " + pClass.BaseClass.Name);
            overridesBase = pClass.BaseClass.Name.ToLower().Contains("databaserecord");
        }
        retStrB.AppendLine();
        retStrB.AppendLine("{");
        retStrB.Append(getCodeBody(objName, pClass, properties, copyResultsToClipboard, overridesBase, language.CSharp));
        retStrB.AppendLine(Space(tab.X) + "}");
        retStrB.AppendLine("}");

        return retStrB.ToString();
    }
    private static string getCodeBody(string objName, ProjectClass pClass, string properties, bool copyResultsToClipboard, bool overridesBaseObject, language lang) {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(cg.getRegionStart(lang, "Constructors"));
        strB.AppendLine(getConstructors(objName, lang));
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(cg.getRegionStart(lang, "Database String"));
        strB.AppendLine(getDatabaseStrings(pClass, lang));
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(cg.getRegionStart(lang, "Private Variables"));
        strB.AppendLine(getPrivateVariables(pClass, lang));
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(cg.getRegionStart(lang, "Public Properties"));
        strB.AppendLine(properties);
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(cg.getRegionStart(lang, "Public Functions"));
        strB.AppendLine(getAddUpdateFunctions(pClass, copyResultsToClipboard, overridesBaseObject, lang));
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(cg.getRegionStart(lang, "Public Subs"));
        strB.AppendLine(getDataReaderText(pClass, copyResultsToClipboard, overridesBaseObject, lang));
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(getToStringFunction(objName, lang));
        return strB.ToString();
    }
    private static string getProperties(ProjectClass pClass, bool copyResultsToClipboard, language lang, CodeGeneration.Format codeFormat) {
        StringBuilder retStrB = new StringBuilder();
        string namSpace = pClass.NameSpaceVariable.NameBasedOnID;
        foreach (ClassVariable cv in pClass.ClassVariables) {
            try {
                if (cv.IsPropertyInherited || cv.ParameterType.IsImage)
                    continue;
                retStrB.AppendLine(getPropertyText(pClass, cv, namSpace, lang, codeFormat, pClass.DALClassVariable.Name));
            } catch (Exception ex) {
                retStrB.AppendLine(string.Format("{3}ERROR: While Adding Variable({0}), the following error occured: {1}{3}MSG: {2}{1}", cv.Name, Constants.vbCrLf, ex.Message, cg.getCommentString(lang)));
            }
        }
        if (copyResultsToClipboard) {
            Clipboard.Clear();
            Clipboard.SetText(retStrB.ToString());
        }
        return retStrB.ToString();
    }

    private static string getPropertyText(ProjectClass pClass, ClassVariable classVar, string namSpace, language lang, CodeGeneration.Format codeFormat, string DAL = "") {
        DataType mDataType = classVar.ParameterType;
        string nameWithUnderscore = "_" + classVar.Name;
        string nameWithoutUnderScore = classVar.Name;
        StringBuilder retStrB = new StringBuilder();
        retStrB.Append(cg.getMetaDataText(string.Format("Gets or sets the {0} for this {1}.{2} object.", nameWithoutUnderScore, pClass.NameSpaceVariable.NameBasedOnID, pClass.Name.Text), false, tab.XX, lang, mDataType.Name, namSpace));
        string propertyAttribute = "";
        if (classVar.IsPropertyXMLIgnored) {
            if (lang == language.VisualBasic)
                propertyAttribute = "<XmlIgnore()> ";
            else
                propertyAttribute = "[XmlIgnore]";
        }

        if (cg.isRegularDataType(mDataType.Name) | DAL.Length == 0)
            retStrB.Append(getPropertyStringForRegularType(classVar, propertyAttribute, nameWithoutUnderScore, nameWithUnderscore, lang, codeFormat));
        else
            retStrB.Append(getPropertyStringForDerivedObject(pClass, classVar, propertyAttribute, nameWithoutUnderScore, nameWithUnderscore, mDataType, namSpace, DAL, lang, codeFormat));

        return retStrB.ToString();
    }
    private static string getPropertyStringForRegularType(ClassVariable clsVar, string propertyAttribute, string nameWithoutUnderScore, string nameWithUnderscore, language lang, CodeGeneration.Format codeFormat) {
        StringBuilder strB = new StringBuilder();
        if (codeFormat == CodeGeneration.Format.MVC)
            strB.Append(getMVCMetaData(Space(tab.XX), clsVar, lang));
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + propertyAttribute + "Public Property " + nameWithoutUnderScore + "() As " + clsVar.ParameterType.Name);
            strB.AppendLine(Space(tab.XXX) + "Get");
            strB.AppendLine(Space(tab.XXXX) + "Return " + nameWithUnderscore);
            strB.AppendLine(Space(tab.XXX) + "End Get");
            strB.AppendLine(Space(tab.XXX) + "Set(ByVal value As " + clsVar.ParameterType.Name + ")");
            strB.Append(Space(tab.XXXX));
            string trimValue = "";
            if (clsVar.ParameterType.Name.ToLower().CompareTo("string") == 0)
                trimValue = ".Trim()";
            strB.AppendLine(nameWithUnderscore + " = value" + trimValue);
            strB.AppendLine(Space(tab.XXX) + "End Set");
            strB.AppendLine(Space(tab.XX) + "End Property");
        } else {
            strB.AppendLine(Space(tab.XX) + propertyAttribute + "public " + clsVar.ParameterType.Name(language.CSharp) + " " + nameWithoutUnderScore);
            strB.AppendLine(Space(tab.XX) + "{");
            strB.AppendLine(Space(tab.XXX) + "get");
            strB.AppendLine(Space(tab.XXX) + "{");
            strB.AppendLine(Space(tab.XXXX) + "return " + nameWithUnderscore + ";");
            strB.AppendLine(Space(tab.XXX) + "}");
            strB.AppendLine(Space(tab.XXX) + "set");
            strB.AppendLine(Space(tab.XXX) + "{");
            strB.Append(Space(tab.XXXX));
            string trimValue = "";
            if (clsVar.ParameterType.Name.ToLower().CompareTo("string") == 0)
                trimValue = ".Trim()";
            strB.AppendLine(nameWithUnderscore + " = value" + trimValue + ";");
            strB.AppendLine(Space(tab.XXX) + "}");
            strB.AppendLine(Space(tab.XX) + "}");
        }
        return strB.ToString();
    }
    private static string getMVCMetaData(string spacer, ClassVariable clsVar, language lang) {
        StringBuilder strB = new StringBuilder();
        if (clsVar.ParameterType.Name.ToLower == "string" && clsVar.LengthOfDatabaseProperty > 0) {
            if (lang == language.VisualBasic) {
                strB.Append(spacer + "<StringLength(" + clsVar.LengthOfDatabaseProperty.ToString());
                strB.Append(", ErrorMessage:= \"The length of " + clsVar.Name + " can not exceed ");
                strB.Append(clsVar.LengthOfDatabaseProperty.ToString() + " characters.\")>");
                strB.AppendLine();
                strB.AppendLine(spacer + "<Display(Name:=\"" + cg.MakeHumanReadable(clsVar.ParentClass.DatabaseTableName) + " " + cg.MakeHumanReadable(clsVar.DatabaseColumnName) + "\")>");
                if (clsVar.IsRequired)
                    strB.AppendLine("<Required(ErrorMessage:=\"" + clsVar.DatabaseColumnName + " is required.\")>");
                if (clsVar.IsDouble)
                    strB.AppendLine("<Range(0.00,100.0,ErrorMessage:=\"" + clsVar.DatabaseColumnName + " is required.\")>");
                if (clsVar.IsDate)
                    strB.AppendLine("<DataType(DataType.DateTime)>");
                if (clsVar.DatabaseColumnName.ToLower.Contains("email"))
                    strB.AppendLine("<DataType(DataType.EmailAddress)>");
            } else {
                strB.Append(spacer + "[StringLength(" + clsVar.LengthOfDatabaseProperty.ToString());
                strB.Append(", ErrorMessage = \"The length of " + clsVar.Name + " can not exceed ");
                strB.Append(clsVar.LengthOfDatabaseProperty.ToString() + " characters.\")]");
                strB.AppendLine();
                strB.AppendLine(spacer + "[Display(Name =\"" + cg.MakeHumanReadable(clsVar.ParentClass.DatabaseTableName) + " " + cg.MakeHumanReadable(clsVar.DatabaseColumnName) + "\")]");
                if (clsVar.IsRequired)
                    strB.AppendLine(spacer + "[Required(ErrorMessage =\"" + clsVar.DatabaseColumnName + " is required.\")]");
                if (clsVar.IsDouble)
                    strB.AppendLine(spacer + "[Range(0.00,100.0,ErrorMessage =\"" + clsVar.DatabaseColumnName + " is required.\")]");
                if (clsVar.IsDate)
                    strB.AppendLine("[DataType(DataType.DateTime)]");
                if (clsVar.DatabaseColumnName.ToLower.Contains("email"))
                    strB.AppendLine("[DataType(DataType.EmailAddress)]");
            }
        }
        return strB.ToString();
    }
    private static string getPropertyStringForDerivedObject(ProjectClass pClass, ClassVariable classVar, string propertyAttribute, string nameWithoutUnderscore, string nameWithUnderscore, DataType mDataType, string namSpace, string DAL, language lang, CodeGeneration.Format codeFormat) {
        StringBuilder strB = new StringBuilder();
        var nameOfIDVariable = nameWithoutUnderscore + "ID";
        if (lang == language.VisualBasic)
            strB.Append(getPropertyStringForDerivedObjectInVB(nameOfIDVariable, classVar, propertyAttribute, nameWithoutUnderscore, nameWithUnderscore, mDataType));
        else
            strB.Append(getPropertyStringForDerivedObjectInCSharp(nameOfIDVariable, classVar, propertyAttribute, nameWithoutUnderscore, nameWithUnderscore, mDataType));
        int ID = 0;
        if (pClass.ClassVariables.Count > 0)
            ID = pClass.ClassVariables(pClass.ClassVariables.Count - 1).ID + 1;

        // TODO: Fix this
        if (!classVar.IsList)
            strB.Append(getPropertyText(pClass, new ClassVariable(pClass, nameOfIDVariable, StaticVariables.Instance.GetDataType("Integer"), false, false, false, true, classVar.IsPropertyInherited, classVar.DisplayOnEditPage, classVar.DisplayOnViewPage, ID, true, false, "Integer", -1, "NA"), namSpace, lang, codeFormat, DAL));
        return strB.ToString();
    }
    private static string getPropertyStringForDerivedObjectInVB(string nameOfIDVariable, ClassVariable classVar, string propertyAttribute, string nameWithoutUnderscore, string nameWithUnderscore, DataType mDataType) {
        StringBuilder strB = new StringBuilder();
        string ObjectWithNameSpace = IIf(classVar.ParameterType.AssociatedProjectClass != null
                                           && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != classVar.ParentClass.NameSpaceVariable, classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".", "").ToString() + mDataType.Name;

        strB.Append(Space(tab.XX) + propertyAttribute + "Public Property " + getSystemUniqueName(nameWithoutUnderscore) + "() As ");
        strB.AppendLine(IIf(classVar.IsList, "List(Of ", "").ToString()
         + ObjectWithNameSpace + IIf(classVar.IsList, ")", "").ToString());
        strB.AppendLine(Space(tab.XXX) + "Get");
        strB.AppendLine(Space(tab.XXXX) + "If " + nameWithUnderscore + " Is Nothing Then");
        string nameSpaceString = "";
        if (mDataType.AssociatedProjectClass.NameSpaceVariable != null)
            nameSpaceString = mDataType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".";

        string nameSpacer = IIf(mDataType.AssociatedProjectClass.NameSpaceVariable
                                       != classVar.ParentClass.NameSpaceVariable, nameSpaceString, "").ToString();
        strB.Append(Space(tab.XXX + tab.XX) + nameWithUnderscore + " = " + nameSpacer + mDataType.AssociatedProjectClass.DALClassVariable.Name + ".Get");
        if (classVar.IsList)
            strB.AppendLine(nameWithoutUnderscore + "(Me)");
        else
            strB.AppendLine(mDataType.Name + "(_" + nameOfIDVariable + ")");
        strB.AppendLine(Space(tab.XXXX) + "End If");
        strB.AppendLine(Space(tab.XXXX) + "Return " + nameWithUnderscore);
        strB.AppendLine(Space(tab.XXX) + "End Get");
        strB.Append(Space(tab.XXXX) + "Set(ByVal value As ");
        strB.Append(IIf(classVar.IsList, "() As List(Of ", "").ToString());
        strB.Append(ObjectWithNameSpace);
        strB.AppendLine(IIf(classVar.IsList, ")", "").ToString() + ")");
        strB.AppendLine(Space(tab.XXXX) + nameWithUnderscore + " = value");
        if (!classVar.IsList) {
            strB.AppendLine(Space(tab.XXXX) + "If value Is nothing Then");
            strB.AppendLine(Space(tab.XXXXX) + "_" + nameOfIDVariable + "=-1");
            strB.AppendLine(Space(tab.XXXX) + "Else");
            strB.AppendLine(Space(tab.XXXXX) + "_" + nameOfIDVariable + "=value.ID");
            strB.AppendLine(Space(tab.XXXX) + "End If");
        }
        strB.AppendLine(Space(tab.XXX) + "End Set");
        strB.AppendLine(Space(tab.XX) + "End Property");

        return strB.ToString();
    }
    private static string getPropertyStringForDerivedObjectInCSharp(string nameOfIDVariable, ClassVariable classVar, string propertyAttribute, string nameWithoutUnderscore, string nameWithUnderscore, DataType mDataType) {
        StringBuilder strB = new StringBuilder();
        string ObjectWithNameSpace = IIf(classVar.ParameterType.AssociatedProjectClass != null
                                           && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != classVar.ParentClass.NameSpaceVariable, classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".", "").ToString() + mDataType.Name;
        strB.AppendLine(Space(tab.XX) + propertyAttribute);
        strB.Append(Space(tab.XX) + "public ");
        strB.Append(IIf(classVar.IsList, "List<", "").ToString()
         + ObjectWithNameSpace + IIf(classVar.IsList, "> ", " ").ToString());
        strB.Append(getSystemUniqueName(nameWithoutUnderscore));
        strB.AppendLine("{");
        strB.AppendLine(Space(tab.XXX) + "get");
        strB.AppendLine(Space(tab.XXX) + "{");
        strB.AppendLine(Space(tab.XXXX) + "if(" + nameWithUnderscore + " == null)");
        strB.AppendLine(Space(tab.XXXX) + "{");
        string nameSpaceString = "";
        if (mDataType.AssociatedProjectClass.NameSpaceVariable != null)
            nameSpaceString = mDataType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".";

        string nameSpacer = IIf(mDataType.AssociatedProjectClass.NameSpaceVariable
                                       != classVar.ParentClass.NameSpaceVariable, nameSpaceString, "").ToString();
        strB.Append(Space(tab.XXX + tab.XX) + nameWithUnderscore + " = " + nameSpacer + mDataType.AssociatedProjectClass.DALClassVariable.Name + ".Get");
        if (classVar.IsList)
            strB.AppendLine(nameWithoutUnderscore + "(this);");
        else
            strB.AppendLine(mDataType.Name(language.CSharp) + "(_" + nameOfIDVariable + ");");
        strB.AppendLine(Space(tab.XXXX) + "}");
        strB.AppendLine(Space(tab.XXXX) + "return " + nameWithUnderscore + ";");
        strB.AppendLine(Space(tab.XXX) + "}");
        strB.AppendLine(Space(tab.XXXX) + "set");
        strB.AppendLine(Space(tab.XXXX) + "{");
        strB.AppendLine(Space(tab.XXXX) + nameWithUnderscore + " = value;");
        if (!classVar.IsList) {
            strB.AppendLine(Space(tab.XXXX) + "if (value == null)");
            strB.AppendLine(Space(tab.XXXX) + "{");
            strB.AppendLine(Space(tab.XXXXX) + "_" + nameOfIDVariable + "=-1;");
            strB.AppendLine(Space(tab.XXXX) + "}");
            strB.AppendLine(Space(tab.XXXX) + "else");
            strB.AppendLine(Space(tab.XXXX) + "{");
            strB.AppendLine(Space(tab.XXXXX) + "_" + nameOfIDVariable + "=value.ID;");
            strB.AppendLine(Space(tab.XXXX) + "}");
        }
        strB.AppendLine(Space(tab.XXX) + "}");
        strB.AppendLine(Space(tab.XX) + "}");
        return strB.ToString();
    }
    public static string getSystemUniqueName(string str) {
        switch (str.ToLower()) {
            case "class":
            case "view":
            case "property": {
                    return str + "Object";
                }

            default: {
                    return str;
                }
        }
    }
    public static string getDatabaseStrings(ProjectClass pClass, language lang) {
        StringBuilder retStrB = new StringBuilder();
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            if (classVar.IsDatabaseBound) {
                if (lang == language.VisualBasic)
                    retStrB.AppendLine(Space(tab.XX) + "Friend Const db_" + classVar.Name
+ " As String = \"" + classVar.DatabaseColumnName + "\"");
                else
                    retStrB.AppendLine(Space(tab.XX) + "internal const string db_" + classVar.Name
                                   + "= \"" + classVar.DatabaseColumnName + "\";");
            }
        }
        return retStrB.ToString();
    }
    public static string getPrivateVariables(ProjectClass pClass, language lang) {
        if (lang == language.VisualBasic)
            return getPrivateVariablesInVB(pClass);
        else
            return getPrivateVariablesInCSharp(pClass);
    }
    public static string getPrivateVariablesInVB(ProjectClass pClass) {
        StringBuilder retStrB = new StringBuilder();
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            if (classVar.IsPropertyInherited || classVar.ParameterType.IsImage)
                continue;
            retStrB.Append(Space(tab.XX) + "Private ");
            if (!classVar.Name.StartsWith("_"))
                retStrB.Append("_");
            if (classVar.ParameterType.IsNameAlias)
                retStrB.AppendLine(classVar.Name + " As New " + classVar.ParameterType.Name + "(\"" + classVar.Name + "\")");
            else if (classVar.ParameterType.IsPrimitive)
                retStrB.AppendLine(classVar.Name + " As " + classVar.ParameterType.Name);
            else {
                string nameSpaceString = "";
                if (classVar.ParameterType.AssociatedProjectClass != null && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != null && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != pClass.NameSpaceVariable)
                    nameSpaceString = classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".";
                if (!classVar.isAssociative) {
                    retStrB.AppendLine(classVar.Name + "ID As Integer");
                    retStrB.Append(Space(tab.XX) + "Private _");
                }

                retStrB.AppendLine(classVar.Name + " As "
                            + IIf(classVar.IsList, "() As List(Of ", "").ToString() + nameSpaceString + classVar.ParameterType.Name
                            + IIf(classVar.IsList, ") = Nothing", "").ToString());
            }
        }
        return retStrB.ToString();
    }
    public static string getPrivateVariablesInCSharp(ProjectClass pClass) {
        StringBuilder retStrB = new StringBuilder();
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            if (classVar.IsPropertyInherited || classVar.ParameterType.IsImage)
                continue;

            string varName = classVar.Name;
            string paraType = classVar.ParameterType.Name(language.CSharp);
            bool isAssocName = false;
            if (!varName.StartsWith("_"))
                varName = "_" + varName;
            string objName = "";

            if (classVar.ParameterType.IsPrimitive)
                objName = paraType;
            else if (!classVar.ParameterType.IsNameAlias) {
                string nameSpaceString = "";
                if (classVar.ParameterType.AssociatedProjectClass != null && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != null && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != pClass.NameSpaceVariable)
                    nameSpaceString = classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".";
                objName = IIf(classVar.IsList, "List<", "").ToString() + nameSpaceString + paraType
                                + IIf(classVar.IsList, ">", "").ToString();

                isAssocName = !classVar.isAssociative;
            }

            // ' Name aliases need a default declaration.
            string objDecl = "";
            if (classVar.ParameterType.IsNameAlias)
                objDecl = " = new " + classVar.ParameterType.Name + "(\"" + classVar.Name + "\")";

            // Write Variable Strings
            // make associative id reference if variable is part of an associative entity
            if (isAssocName)
                retStrB.AppendLine(Space(tab.XX) + string.Format("private int {0}ID;", varName));
            retStrB.AppendLine(Space(tab.XX) + string.Format("private {0} {1}{2};", objName, varName, objDecl));
        }
        return retStrB.ToString();
    }
}

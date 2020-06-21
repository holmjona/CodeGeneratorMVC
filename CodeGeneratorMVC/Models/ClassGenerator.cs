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
using System.Drawing;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;
using System.Net.NetworkInformation;

public class ClassGenerator {
    public static string getDataReaderText(ProjectClass pClass, bool overridesBase, language lang) {
        if (lang == language.VisualBasic)
            return getDataReaderTextInVB(pClass, overridesBase);
        else
            return getDataReaderTextInCSharp(pClass, overridesBase);
    }
    private static string getDataReaderTextInVB(ProjectClass pClass, bool overridesBase) {
        StringBuilder retStrB = new StringBuilder();
        if (pClass.Name.Text().Length > 0) {
            retStrB.Append(cg.getMetaDataText("Fills object from a SqlClient Data Reader", false, (int)tab.XX, language.VisualBasic));
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() + "Sub Fill(ByVal dr As Data.SqlClient.SqlDataReader)");
            foreach (ClassVariable classVar in pClass.ClassVariables) {
                if (classVar.IsAssociative)
                    continue;
                retStrB.Append(Strings.Space((int)tab.XXX));
                if (!classVar.IsDatabaseBound || classVar.ParameterType.IsImage)
                    continue;
                bool AttributeAndObjectHaveSameName = classVar.Name == classVar.ParameterType.Name(language.VisualBasic);
                string DatabaseParameterName = "db_" + classVar.Name;
                if (!cg.isRegularDataType(classVar.ParameterType.Name(language.VisualBasic)) && AttributeAndObjectHaveSameName)
                    DatabaseParameterName = classVar.ParameterType.Name(language.VisualBasic) + ".db_ID";
                if (classVar.ParameterType.IsNameAlias) {
                    retStrB.AppendLine("_" + classVar.Name + ".TextUnFormatted = dr(" + DatabaseParameterName + ").ToString()");
                    retStrB.Append(Strings.Space((int)tab.XXX));
                } else if (!cg.isRegularDataType(classVar.ParameterType.Name(language.VisualBasic))) {
                    if (AttributeAndObjectHaveSameName)
                        retStrB.AppendLine("_" + classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized() + " = CInt(dr(" + DatabaseParameterName + "))");
                    else
                        retStrB.AppendLine("_" + classVar.Name + "ID = CInt(dr(" + DatabaseParameterName + "))");
                } else {
                    string toStringText = "";
                    if (classVar.ParameterType.Name().ToLower().CompareTo("datetime") == 0)
                        toStringText = ".ToString()";
                    retStrB.AppendLine("_" + classVar.Name + " = " + cg.getConvertFunction(classVar.ParameterType.Name(), language.VisualBasic) + "(dr(" + DatabaseParameterName + ")" + toStringText + ")");
                }
            }
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "End Sub");
        }
        return retStrB.ToString();
    }
    private static string getDataReaderTextInCSharp(ProjectClass pClass, bool overridesBase) {
        StringBuilder retStrB = new StringBuilder();
        if (pClass.Name.Text().Length > 0) {
            retStrB.Append(cg.getMetaDataText("Fills object from a SqlClient Data Reader", false, (int)tab.XX, language.CSharp));
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "void Fill(System.Data.SqlClient.SqlDataReader dr)");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "{");
            foreach (ClassVariable classVar in pClass.ClassVariables) {
                if (classVar.IsAssociative)
                    continue;
                retStrB.Append(Strings.Space((int)tab.XXX));
                if (!classVar.IsDatabaseBound || classVar.ParameterType.IsImage)
                    continue;
                bool AttributeAndObjectHaveSameName = classVar.Name == classVar.ParameterType.Name();
                string DatabaseParameterName = "db_" + classVar.Name;
                if (!cg.isRegularDataType(classVar.ParameterType.Name()) && AttributeAndObjectHaveSameName)
                    DatabaseParameterName = classVar.ParameterType.Name() + ".db_ID";
                if (classVar.ParameterType.IsNameAlias) {
                    retStrB.AppendLine("_" + classVar.Name + ".TextUnFormatted = dr[" + DatabaseParameterName + "].ToString();");
                    retStrB.Append(Strings.Space((int)tab.XXX));
                } else if (!cg.isRegularDataType(classVar.ParameterType.Name())) {
                    if (AttributeAndObjectHaveSameName)
                        retStrB.AppendLine("_" + classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized() + " = (int)dr[" + DatabaseParameterName + "];");
                    else
                        retStrB.AppendLine("_" + classVar.Name + "ID = (int)dr[" + DatabaseParameterName + "];");
                } else {
                    string toStringText = "";
                    // If classVar.ParameterType.Name().ToLower().CompareTo("datetime") = 0 Then
                    // toStringText = ".ToString()"
                    // End If
                    retStrB.AppendLine("_" + classVar.Name + " = (" + classVar.ParameterType.Name(language.CSharp) + ")dr[" + DatabaseParameterName + "]" + toStringText + ";");
                }
            }
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return retStrB.ToString();
    }
    private static string getAddUpdateFunctions(ProjectClass pClass, bool overridesBase, language lang) {
        StringBuilder retStrB = new StringBuilder();
        string DALClassName = pClass.DALClassVariable.Name;
        string objectName = pClass.Name.Capitalized();
        if (objectName.Length > 0 & DALClassName.Length > 0) {
            if (DALClassName.Length > 0 & objectName.Length > 0) {
                retStrB.Append(getAddClassFunction(DALClassName, objectName, overridesBase, lang));
                retStrB.Append(getUpdateClassFunction(DALClassName, objectName, overridesBase, lang));
                retStrB.Append(getRemoveClassFunction(DALClassName, objectName, overridesBase, lang));
            }
        }
        return retStrB.ToString();
    }
    private static string getAddClassFunction(string dalName, string objectName, bool overridesBase, language lang) {
        StringBuilder retStrB = new StringBuilder(cg.getMetaDataText("Calls DAL function to add " + objectName + " to the database.",
            false, (int)tab.XX, lang, "Integer value greater than 0 if successful."));

        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() 
                + "Function dbAdd() As Integer");
            retStrB.AppendLine(Strings.Space((int)tab.X + (int)tab.XX) + "_ID = " + dalName + ".Add" + objectName + "(Me)");
            retStrB.AppendLine(Strings.Space((int)tab.X + (int)tab.XX) + "Return ID");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "int dbAdd()");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "{");
            retStrB.AppendLine(Strings.Space((int)tab.X + (int)tab.XX) + "_ID = " + dalName + ".Add" + objectName + "(this);");
            retStrB.AppendLine(Strings.Space((int)tab.X + (int)tab.XX) + "return ID;");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        retStrB.AppendLine("");
        return retStrB.ToString();
    }
    private static string getUpdateClassFunction(string dalName, string objectName, bool overridesBase, language lang) {
        StringBuilder retStrB = new StringBuilder(cg.getMetaDataText("Calls DAL function to update " + objectName + " to the database.", false, 
            (int)tab.XX, lang, "Integer value greater than 0 if successful."));
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() 
                + "Function dbUpdate() As Integer");
            retStrB.AppendLine(Strings.Space((int)tab.XXX) + "Return " + dalName + ".Update" + objectName + "(Me)");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "int dbUpdate()");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "{");
            retStrB.AppendLine(Strings.Space((int)tab.X + (int)tab.XX) + "return " + dalName + ".Update" + objectName + "(this);");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        retStrB.AppendLine("");
        return retStrB.ToString();
    }
    private static string getRemoveClassFunction(string dalName, string objectName, bool overridesBase, language lang) {
        overridesBase = false;
        StringBuilder retStrB = new StringBuilder(cg.getMetaDataText("Calls DAL function to remove " + objectName + " from the database.", false, 
            (int)tab.XX, lang, "Integer value greater than 0 if successful."));
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "Public " + Interaction.IIf(overridesBase, "Overrides ", "").ToString() 
                + "Function dbRemove() As Integer");
            retStrB.AppendLine(Strings.Space((int)tab.XXX) + "Return " + dalName + ".Remove" + objectName + "(Me)");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "public " + Interaction.IIf(overridesBase, "override ", "").ToString() + "int dbRemove()");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "{");
            retStrB.AppendLine(Strings.Space((int)tab.X + (int)tab.XX) + "return " + dalName + ".Remove" + objectName + "(this);");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return retStrB.ToString();
    }
    private static string getToStringFunction(string objectName, language lang) {
        StringBuilder retStrB = new StringBuilder();
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "Public Overrides Function ToString() As String");
            retStrB.AppendLine(Strings.Space((int)tab.XXX) + "Return Me.GetType().ToString()");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "public override string ToString()");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "{");
            retStrB.AppendLine(Strings.Space((int)tab.X + (int)tab.XX) + "return this.GetType().ToString();");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return retStrB.ToString();
    }
    private static string getConstructors(string objName, language lang) {
        StringBuilder retStrB = new StringBuilder();
        if (lang == language.VisualBasic) {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "Public Sub New()");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "End Sub");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "Friend Sub New(ByVal dr As System.Data.SqlClient.SqlDataReader)");
            retStrB.AppendLine(Strings.Space((int)tab.XXX) + "Fill(dr)");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "End Sub");
        } else {
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "public " + objName + "()");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "{");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "}");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "internal " + objName + "(System.Data.SqlClient.SqlDataReader dr)");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "{");
            retStrB.AppendLine(Strings.Space((int)tab.XXX) + "Fill(dr);");
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return retStrB.ToString();
    }
    public static string getEntireClass(ProjectClass pClass, string creator, language lang, CodeGeneration.Format codeFormat,ref List<string> warnings) {
        StringBuilder retStrB = new StringBuilder();
        string namSpace = "";
        if (pClass.NameSpaceVariable != null)
            namSpace = pClass.NameSpaceVariable.NameBasedOnID;
        string Comments = pClass.Summary;
        // Dim txtProperties As String = txtInVars.Text.Trim
        string dalClassName = pClass.DALClassVariable.Name;
        string objName = pClass.Name.Capitalized();
        if (objName.Length > 0 & dalClassName.Length > 0) {
            if (objName.Length > 0) {
                if (lang == language.VisualBasic)
                    retStrB.Append(getEntireClassInVB(pClass, objName, namSpace, Comments, creator, codeFormat));
                else
                    retStrB.Append(getEntireClassInCSharp(pClass, objName, namSpace, Comments, creator, codeFormat));
            }
        } else {
            if (pClass.Name.Text().Length == 0)
                warnings.Add("Invalid Input: You must provide an Object/Class name.");
            if (dalClassName.Length == 0)
                warnings.Add("Invalid Input: You must provide a Data Access Layer Class name");
        }
        return retStrB.ToString();
    }
    private static string getEntireClassInVB(ProjectClass pClass, string objName, string namSpace, string Comments, string creator, CodeGeneration.Format codeFormat) {
        StringBuilder retStrB = new StringBuilder();
        string properties = getProperties(pClass, language.VisualBasic, codeFormat);
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
        retStrB.AppendLine(cg.getMetaDataText(Comments, false, (int)tab.XX, language.VisualBasic));
        retStrB.AppendLine(Strings.Space((int)tab.X) + "Public Class " + objName);
        bool overridesBase = false;
        if (pClass.BaseClass != null && !string.IsNullOrEmpty(pClass.BaseClass.Name)) {
            retStrB.AppendLine(Strings.Space((int)tab.X) + "Inherits " + pClass.BaseClass.Name);
            overridesBase = pClass.BaseClass.Name.ToLower().Contains("databaserecord");
        }
        retStrB.AppendLine("");
        retStrB.Append(getCodeBody(objName, pClass, properties, overridesBase, language.VisualBasic));
        retStrB.AppendLine(Strings.Space((int)tab.X) + "End Class");
        retStrB.AppendLine("End Namespace");

        return retStrB.ToString();
    }
    private static string getEntireClassInCSharp(ProjectClass pClass, string objName, string namSpace, string Comments,
        string creator, CodeGeneration.Format codeFormat) {
        StringBuilder retStrB = new StringBuilder();

        string properties = getProperties(pClass, language.CSharp, codeFormat);
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
        retStrB.AppendLine(cg.getMetaDataText(Comments, false, (int)tab.XX, language.CSharp));
        retStrB.Append(Strings.Space(4) + "public class " + objName);
        bool overridesBase = false;
        if (pClass.BaseClass != null && !string.IsNullOrEmpty(pClass.BaseClass.Name)) {
            retStrB.Append(" : " + pClass.BaseClass.Name);
            overridesBase = pClass.BaseClass.Name.ToLower().Contains("databaserecord");
        }
        retStrB.AppendLine();
        retStrB.AppendLine("{");
        retStrB.Append(getCodeBody(objName, pClass, properties, overridesBase, language.CSharp));
        retStrB.AppendLine(Strings.Space((int)tab.X) + "}");
        retStrB.AppendLine("}");

        return retStrB.ToString();
    }
    private static string getCodeBody(string objName, ProjectClass pClass, string properties, bool overridesBaseObject, language lang) {
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
        strB.AppendLine(getAddUpdateFunctions(pClass, overridesBaseObject, lang));
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(cg.getRegionStart(lang, "Public Subs"));
        strB.AppendLine(getDataReaderText(pClass, overridesBaseObject, lang));
        strB.AppendLine(cg.getRegionEnd(lang));
        strB.AppendLine("");
        strB.AppendLine(getToStringFunction(objName, lang));
        return strB.ToString();
    }
    private static string getProperties(ProjectClass pClass, language lang, CodeGeneration.Format codeFormat) {
        StringBuilder retStrB = new StringBuilder();
        string namSpace = pClass.NameSpaceVariable.NameBasedOnID;
        foreach (ClassVariable cv in pClass.ClassVariables) {
            try {
                if (cv.IsPropertyInherited || cv.ParameterType.IsImage)
                    continue;
                retStrB.AppendLine(getPropertyText(pClass, cv, namSpace, lang, codeFormat, pClass.DALClassVariable.Name));
            } catch (Exception ex) {
                retStrB.AppendLine(string.Format("{3}ERROR: While Adding Variable({0}), the following error occured: " +
                    "{1}{3}MSG: {2}{1}", cv.Name, Constants.vbCrLf, ex.Message, cg.getCommentString(lang)));
            }
        }
        return retStrB.ToString();
    }

    private static string getPropertyText(ProjectClass pClass, ClassVariable classVar, string namSpace,
        language lang, CodeGeneration.Format codeFormat, string DAL = "") {
        DataType mDataType = classVar.ParameterType;
        string nameWithUnderscore = "_" + classVar.Name;
        string nameWithoutUnderScore = classVar.Name;
        StringBuilder retStrB = new StringBuilder();
        retStrB.Append(cg.getMetaDataText(string.Format("Gets or sets the {0} for this {1}.{2} object.",
            nameWithoutUnderScore, pClass.NameSpaceVariable.NameBasedOnID, pClass.Name.Text()),
            false, (int)tab.XX, lang, mDataType.Name(), namSpace));
        string propertyAttribute = "";
        if (classVar.IsPropertyXMLIgnored) {
            if (lang == language.VisualBasic)
                propertyAttribute = "<XmlIgnore()> ";
            else
                propertyAttribute = "[XmlIgnore]";
        }

        if (cg.isRegularDataType(mDataType.Name()) | DAL.Length == 0)
            retStrB.Append(getPropertyStringForRegularType(classVar, propertyAttribute,
                nameWithoutUnderScore, nameWithUnderscore, lang, codeFormat));
        else
            retStrB.Append(getPropertyStringForDerivedObject(pClass, classVar, propertyAttribute,
                nameWithoutUnderScore, nameWithUnderscore, mDataType, namSpace, DAL, lang, codeFormat));

        return retStrB.ToString();
    }
    private static string getPropertyStringForRegularType(ClassVariable clsVar, string propertyAttribute, string nameWithoutUnderScore, string nameWithUnderscore, language lang, CodeGeneration.Format codeFormat) {
        StringBuilder strB = new StringBuilder();
        if (codeFormat == CodeGeneration.Format.MVC)
            strB.Append(getMVCMetaData(Strings.Space((int)tab.XX), clsVar, lang));
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + propertyAttribute + "Public Property " + nameWithoutUnderScore + "() As " + clsVar.ParameterType.Name());
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Get");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return " + nameWithUnderscore);
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Get");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Set(ByVal value As " + clsVar.ParameterType.Name() + ")");
            strB.Append(Strings.Space((int)tab.XXXX));
            string trimValue = "";
            if (clsVar.ParameterType.Name().ToLower().CompareTo("string") == 0)
                trimValue = ".Trim()";
            strB.AppendLine(nameWithUnderscore + " = value" + trimValue);
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Set");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Property");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + propertyAttribute + "public " + clsVar.ParameterType.Name(language.CSharp) + " " + nameWithoutUnderScore);
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "get");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return " + nameWithUnderscore + ";");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "set");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.Append(Strings.Space((int)tab.XXXX));
            string trimValue = "";
            if (clsVar.ParameterType.Name().ToLower().CompareTo("string") == 0)
                trimValue = ".Trim()";
            strB.AppendLine(nameWithUnderscore + " = value" + trimValue + ";");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }
    private static string getMVCMetaData(string spacer, ClassVariable clsVar, language lang) {
        StringBuilder strB = new StringBuilder();
        if (clsVar.ParameterType.Name().ToLower() == "string" && clsVar.LengthOfDatabaseProperty > 0) {
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
                if (clsVar.DatabaseColumnName.ToLower().Contains("email"))
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
                if (clsVar.DatabaseColumnName.ToLower().Contains("email"))
                    strB.AppendLine("[DataType(DataType.EmailAddress)]");
            }
        }
        return strB.ToString();
    }
    private static string getPropertyStringForDerivedObject(ProjectClass pClass, ClassVariable classVar, string propertyAttribute,
        string nameWithoutUnderscore, string nameWithUnderscore, DataType mDataType, string namSpace, string DAL, language lang,
        CodeGeneration.Format codeFormat) {
        StringBuilder strB = new StringBuilder();
        var nameOfIDVariable = nameWithoutUnderscore + "ID";
        if (lang == language.VisualBasic)
            strB.Append(getPropertyStringForDerivedObjectInVB(nameOfIDVariable, classVar, propertyAttribute,
                nameWithoutUnderscore, nameWithUnderscore, mDataType));
        else
            strB.Append(getPropertyStringForDerivedObjectInCSharp(nameOfIDVariable, classVar, propertyAttribute,
                nameWithoutUnderscore, nameWithUnderscore, mDataType));
        int ID = 0;
        if (pClass.ClassVariables.Count > 0)
            ID = pClass.ClassVariables[pClass.ClassVariables.Count - 1].ID + 1;

        // TODO: Fix this
        if (!classVar.IsList)
            strB.Append(getPropertyText(pClass, new ClassVariable(pClass, nameOfIDVariable,
                StaticVariables.Instance.GetDataType("Integer"), false, false, false, true, classVar.IsPropertyInherited,
                classVar.DisplayOnEditPage, classVar.DisplayOnViewPage, ID, true, false, "Integer", -1, "NA"), namSpace, lang, codeFormat, DAL));
        return strB.ToString();
    }
    private static string getPropertyStringForDerivedObjectInVB(string nameOfIDVariable, ClassVariable classVar,
        string propertyAttribute, string nameWithoutUnderscore, string nameWithUnderscore, DataType mDataType) {
        StringBuilder strB = new StringBuilder();
        string ObjectWithNameSpace = (classVar.ParameterType.AssociatedProjectClass != null
                                           && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != classVar.ParentClass.NameSpaceVariable
                                           ? classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + "." : "")
                                           + mDataType.Name();

        strB.Append(Strings.Space((int)tab.XX) + propertyAttribute + "Public Property " + getSystemUniqueName(nameWithoutUnderscore) + "() As ");
        strB.AppendLine((classVar.IsList ? "List(Of " : "") + ObjectWithNameSpace + (classVar.IsList ? ")" : ""));
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Get");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "If " + nameWithUnderscore + " Is Nothing Then");
        string nameSpaceString = "";
        if (mDataType.AssociatedProjectClass.NameSpaceVariable != null)
            nameSpaceString = mDataType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".";

        string nameSpacer = (mDataType.AssociatedProjectClass.NameSpaceVariable != classVar.ParentClass.NameSpaceVariable ? nameSpaceString : "");
        strB.Append(Strings.Space((int)tab.XXX + (int)tab.XX) + nameWithUnderscore + " = "
            + nameSpacer + mDataType.AssociatedProjectClass.DALClassVariable.Name + ".Get");
        if (classVar.IsList)
            strB.AppendLine(nameWithoutUnderscore + "(Me)");
        else
            strB.AppendLine(mDataType.Name() + "(_" + nameOfIDVariable + ")");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return " + nameWithUnderscore);
        strB.AppendLine(Strings.Space((int)tab.XXX) + "End Get");
        strB.Append(Strings.Space((int)tab.XXXX) + "Set(ByVal value As ");
        strB.Append(classVar.IsList ? "() As List(Of " : "");
        strB.Append(ObjectWithNameSpace);
        strB.AppendLine((classVar.IsList ? ")" : "") + ")");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + nameWithUnderscore + " = value");
        if (!classVar.IsList) {
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If value Is nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "_" + nameOfIDVariable + "=-1");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "_" + nameOfIDVariable + "=value.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
        }
        strB.AppendLine(Strings.Space((int)tab.XXX) + "End Set");
        strB.AppendLine(Strings.Space((int)tab.XX) + "End Property");

        return strB.ToString();
    }
    private static string getPropertyStringForDerivedObjectInCSharp(string nameOfIDVariable, ClassVariable classVar,
        string propertyAttribute, string nameWithoutUnderscore, string nameWithUnderscore, DataType mDataType) {
        StringBuilder strB = new StringBuilder();
        string ObjectWithNameSpace = (classVar.ParameterType.AssociatedProjectClass != null
                                           && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != classVar.ParentClass.NameSpaceVariable
                                           ? classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + "." : "")
                                           + mDataType.Name();
        strB.AppendLine(Strings.Space((int)tab.XX) + propertyAttribute);
        strB.Append(Strings.Space((int)tab.XX) + "public ");
        strB.Append((classVar.IsList ? "List<" : "") + ObjectWithNameSpace + (classVar.IsList ? "> " : " "));
        strB.Append(getSystemUniqueName(nameWithoutUnderscore));
        strB.AppendLine("{");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "get");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "if(" + nameWithUnderscore + " == null)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
        string nameSpaceString = "";
        if (mDataType.AssociatedProjectClass.NameSpaceVariable != null)
            nameSpaceString = mDataType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".";

        string nameSpacer = (mDataType.AssociatedProjectClass.NameSpaceVariable
                                       != classVar.ParentClass.NameSpaceVariable ? nameSpaceString : "");
        strB.Append(Strings.Space((int)tab.XXX + (int)tab.XX) + nameWithUnderscore
            + " = " + nameSpacer + mDataType.AssociatedProjectClass.DALClassVariable.Name + ".Get");
        if (classVar.IsList)
            strB.AppendLine(nameWithoutUnderscore + "(this);");
        else
            strB.AppendLine(mDataType.Name(language.CSharp) + "(_" + nameOfIDVariable + ");");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "return " + nameWithUnderscore + ";");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "set");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + nameWithUnderscore + " = value;");
        if (!classVar.IsList) {
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (value == null)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "_" + nameOfIDVariable + "=-1;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "else");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "_" + nameOfIDVariable + "=value.ID;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
        }
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XX) + "}");
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
                    retStrB.AppendLine(Strings.Space((int)tab.XX) + "Friend Const db_" + classVar.Name
+ " As String = \"" + classVar.DatabaseColumnName + "\"");
                else
                    retStrB.AppendLine(Strings.Space((int)tab.XX) + "internal const string db_" + classVar.Name
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
            retStrB.Append(Strings.Space((int)tab.XX) + "Private ");
            if (!classVar.Name.StartsWith("_"))
                retStrB.Append("_");
            if (classVar.ParameterType.IsNameAlias)
                retStrB.AppendLine(classVar.Name + " As New " + classVar.ParameterType.Name() + "(\"" + classVar.Name + "\")");
            else if (classVar.ParameterType.IsPrimitive)
                retStrB.AppendLine(classVar.Name + " As " + classVar.ParameterType.Name());
            else {
                string nameSpaceString = "";
                if (classVar.ParameterType.AssociatedProjectClass != null && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != null && classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable != pClass.NameSpaceVariable)
                    nameSpaceString = classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID + ".";
                if (!classVar.IsAssociative) {
                    retStrB.AppendLine(classVar.Name + "ID As Integer");
                    retStrB.Append(Strings.Space((int)tab.XX) + "Private _");
                }

                retStrB.AppendLine(classVar.Name + " As "
                            + (classVar.IsList ? "() As List(Of " : "") + nameSpaceString + classVar.ParameterType.Name()
                            + (classVar.IsList ? ") = Nothing" : ""));
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
                objName = (classVar.IsList ? "List<" : "") + nameSpaceString + paraType + (classVar.IsList ? ">" : "");

                isAssocName = !classVar.IsAssociative;
            }

            // ' Name aliases need a default declaration.
            string objDecl = "";
            if (classVar.ParameterType.IsNameAlias)
                objDecl = " = new " + classVar.ParameterType.Name() + "(\"" + classVar.Name + "\")";

            // Write Variable Strings
            // make associative id reference if variable is part of an associative entity
            if (isAssocName)
                retStrB.AppendLine(Strings.Space((int)tab.XX) + string.Format("private int {0}ID;", varName));
            retStrB.AppendLine(Strings.Space((int)tab.XX) + string.Format("private {0} {1}{2};", objName, varName, objDecl));
        }
        return retStrB.ToString();
    }
}

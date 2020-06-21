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

public class CodeGeneration {
    public static bool isRegularDataType(string typestr) {
        typestr = typestr.ToLower();
        DataType associatedDataType = StaticVariables.Instance.GetDataType(typestr.ToLower());
        if (associatedDataType != null && associatedDataType.IsPrimitive)
            return true;
        else
            return false;
        return false;
    }

    public enum Language : int {
        CSharp,
        VisualBasic
    }
    public enum Format : int {
        ASPX,
        MVC
    }
    public enum Tabs : int {
        None = 0,
        X = 4,
        XX = 8,
        XXX = 12,
        XXXX = 16,
        XXXXX = 20,
        XXXXXX = 24,
        XXXXXXX = 28,
        XXXXXXXX = 32
    }
    public static string getCommentString(CodeGeneration.Language lang, bool isSummary = false) {
        if (lang == CodeGeneration.Language.VisualBasic) {
            if (isSummary)
                return "'''";
            return "'";
        }
        if (isSummary)
            return "///";
        return "//";
    }
    public static string getSqlDataTypeConversion(string datatype) {
        string retString = "";
        switch (datatype.ToLower()) {
            case "integer": {
                    retString = "int";
                    break;
                }

            case "string": {
                    retString = "nvarchar()";
                    break;
                }

            case "double": {
                    retString = "float";
                    break;
                }

            case "byte()": {
                    retString = "image";
                    break;
                }

            case "date": {
                    retString = "datetime";
                    break;
                }

            case "boolean": {
                    retString = "bit";
                    break;
                }

            case "datetime": {
                    retString = "datetime";
                    break;
                }

            case "image": {
                    retString = "byte()";
                    break;
                }

            default: {
                    retString = "UnknownDataType";
                    break;
                }
        }
        return retString;
    }
    private static Dictionary<string, string> _ConversionFunctions;
    public static Dictionary<string, string> ConversionFunctions {
        get {
            if (_ConversionFunctions == null) {
                _ConversionFunctions = new Dictionary<string, string>();
                _ConversionFunctions.Add("integer", "CInt");
                _ConversionFunctions.Add("short", "CShort");
                _ConversionFunctions.Add("string", "CStr");
                _ConversionFunctions.Add("date", "CDate");
                _ConversionFunctions.Add("boolean", "CBool");
                _ConversionFunctions.Add("single", "CSng");
                _ConversionFunctions.Add("double", "CDbl");
                _ConversionFunctions.Add("byte", "CByte");
                _ConversionFunctions.Add("decimal", "CDec");
                _ConversionFunctions.Add("datetime", "DateTime.Parse");
            }
            return _ConversionFunctions;
        }
    }

    public static string getConvertFunction(string dataTypeString, CodeGeneration.Language lang) {
        string retString = "";
        if (lang == Language.VisualBasic) {
            if (ConversionFunctions.ContainsKey(dataTypeString.ToLower()))
                return ConversionFunctions[dataTypeString.ToLower()];
            else
                return "UnknownDataType";
        } else
            return "(" + dataTypeString + ")";
        // Select Case dataTypeString.ToLower
        // Case "integer"
        // retString = "CInt"
        // Case "short"
        // retString = "CShort"
        // Case 
        // Case "string"
        // retString = "CStr"
        // Case "date"
        // retString = "CDate"
        // Case "boolean"
        // retString = "CBool"
        // Case "single"
        // retString = "CSng"
        // Case "double"
        // retString = "CDbl"
        // Case "short"
        // retString = "CShort"
        // Case "byte"
        // retString = "CByte"
        // Case "decimal"
        // retString = "CDec"
        // Case "datetime"
        // retString = "DateTime.Parse"
        // Case Else
        // retString = "UnknownDataType"
        // End Select
        return retString;
    }
    public static string getPageImports(Language lang, bool includeSQL = false, bool includeSystemConfig = false, bool includeWebUI = false) {
        StringBuilder strB = new StringBuilder();
        if (lang == Language.VisualBasic) {
            strB.AppendLine("Option Strict On");
            strB.AppendLine("Imports Microsoft.VisualBasic");
            strB.AppendLine("Imports IRICommonObjects.Tools");
            strB.AppendLine("Imports System.Collections.Generic");
            strB.AppendLine("Imports System.Linq");
            if (includeWebUI)
                strB.AppendLine("Imports System.Web.UI");
            if (includeSQL)
                strB.AppendLine("Imports System.Data.SqlClient");
            if (includeSystemConfig)
                strB.AppendLine("Imports System.Configuration");
            foreach (ProjectVariable nsp in StaticVariables.Instance.NameSpaceNames)
                strB.AppendLine(string.Format("Imports {0}", nsp.Name));
        } else {
            strB.AppendLine("using System;");
            strB.AppendLine("using System.Net;");
            strB.AppendLine("using System.Linq;");
            strB.AppendLine("using System.Collections.Generic;");
            if (includeWebUI)
                strB.AppendLine("using System.Web.UI;");
            strB.AppendLine("using IRICommonObjects.Tools;");
            if (includeSQL)
                strB.AppendLine("using System.Data.SqlClient;");
            if (includeSystemConfig)
                strB.AppendLine("using System.Configuration;");
            foreach (ProjectVariable nsp in StaticVariables.Instance.NameSpaceNames)
                strB.AppendLine(string.Format("using {0};", nsp.Name));
        }
        return strB.ToString();
    }

    public static string getMetaDataText(string comment, bool isProperty, int indentationOffset, Language lang, string returnType = "", string namSpace = "") {
        string commString = "'''";
        if (lang == Language.CSharp)
            commString = "///";
        StringBuilder strB = new StringBuilder();
        if (comment.Length == 0) {
            strB.AppendLine(Strings.Space(indentationOffset) + commString + " <summary>");
            strB.AppendLine(Strings.Space(indentationOffset) + commString + " TODO: Comment this");
            strB.AppendLine(Strings.Space(indentationOffset) + commString + " </summary>");
        } else
            strB.AppendLine(Strings.Space(indentationOffset) + commString + " <summary>" + Constants.vbCrLf + Strings.Space(indentationOffset) + commString + " " + comment + Constants.vbCrLf + Strings.Space(indentationOffset) + commString + " </summary>");
        if (comment.Length == 0 && isProperty) {
            if (isRegularDataType(returnType)) {
                strB.AppendLine(Strings.Space(indentationOffset) + commString + " <value></value>");
                strB.AppendLine(Strings.Space(indentationOffset) + commString + " <returns>" + returnType + "</returns>");
            } else {
                strB.AppendLine(Strings.Space(indentationOffset) + commString + " <value></value>");
                strB.AppendLine(Strings.Space(indentationOffset) + commString + " <returns>" + namSpace + "." + returnType + "</returns>");
            }
        }
        strB.AppendLine(Strings.Space(indentationOffset) + commString + " <remarks></remarks>");
        return strB.ToString();
    }

    public static string getRegionStart(Language lang, string name) {
        if (lang == Language.VisualBasic)
            return "#Region \"" + name + "\"";
        else
            return "#region " + name;
    }
    public static string getRegionEnd(Language lang) {
        if (lang == Language.VisualBasic)
            return "#End Region";
        else
            return "#endregion";
    }

    public static string getClassDeclaration(Language lang, string className, Tabs offset, string inheritedClassName = "") {
        StringBuilder strB = new StringBuilder();
        if (lang == Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)Tabs.X + (int)offset) + "Partial Class " + className);
            if (inheritedClassName != "")
                strB.AppendLine(Strings.Space((int)Tabs.XX + (int)offset) + "Inherits " + inheritedClassName);
        } else {
            strB.Append(Strings.Space((int)Tabs.X + (int)offset) + "partial class " + className);
            if (inheritedClassName != "")
                strB.AppendLine(" : " + inheritedClassName);
            strB.AppendLine(Strings.Space((int)Tabs.X + (int)offset) + "{");
        }
        return strB.ToString();
    }

    /// <summary>
    ///     ''' Make camelCasedString read normally by adding spaces infront of Capital letters.
    ///     ''' </summary>
    ///     ''' <param name="strToConvert"></param>
    ///     ''' <returns>String of camelCasedString into a readable form ex: ShowMeATest --> Show Me A Test</returns>
    ///     ''' <remarks></remarks>
    public static string MakeHumanReadable(string strToConvert) {
        string retStr = "";
        int upperCount = 0;
        int numberCount = 0;
        bool lastCharWasNumber = false;
        foreach (char c in strToConvert) {
            // Add space in front of upper characters -- FirstName --> First Name
            if (char.IsUpper(c)) {
                retStr += " ";
                upperCount += 1;
            }
            // Keep numbers together but add a space infront -- Field12 --> Field 12
            if (char.IsNumber(c)) {
                numberCount += 1;
                if (!lastCharWasNumber)
                    retStr += " ";
                lastCharWasNumber = true;
            } else
                lastCharWasNumber = false;
            retStr += c;
        }
        if (upperCount + numberCount == strToConvert.Length) {
            // No CamelCase was in ALLCAPS. 
            // So we will just return the original.
            if (numberCount > 0)
                // will resend through in lower so that it will space off any numbers
                return MakeHumanReadable(strToConvert.ToLower()).ToUpper();
            else
                return strToConvert;
        }
        return retStr.Trim();
    }
}

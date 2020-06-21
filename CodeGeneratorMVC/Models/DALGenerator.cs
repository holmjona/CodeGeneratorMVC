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
using cg = CodeGeneration;
using tab = CodeGeneration.Tabs;

public class DALGenerator {
    public static string getDALFunctions(ProjectClass pClass, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        string ObjectName = pClass.Name.Capitalized();
        string readConnection = pClass.DALClassVariable.ReadOnlyConnectionString.Name;
        string editConnection = pClass.DALClassVariable.EditOnlyConnectionstring.Name;
        if (readConnection.Length > 0 & editConnection.Length > 0 & ObjectName.Length > 0) {
            strB.AppendLine();
            strB.AppendLine(cg.getRegionStart(lang, ObjectName));
            strB.AppendLine(getObjectFromQueryString(pClass, lang));
            strB.AppendLine(getDALFunction_SelectSingle(pClass, readConnection, lang));
            strB.AppendLine(getDALFunction_SelectAll(pClass, readConnection, lang));
            strB.AppendLine(getDALFunction_SelectAssociative(pClass, readConnection, lang));
            strB.AppendLine(getDALFunction_Insert(pClass, editConnection, lang));
            strB.AppendLine(getDALFunction_Update(pClass, editConnection, lang));
            strB.AppendLine(getDALFunction_Delete(pClass, editConnection, lang));
            strB.AppendLine(cg.getRegionEnd(lang));
        }
        return strB.ToString();
    }
    public static string getDALFunction_Delete(ProjectClass pClass, string editconn, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        strB.Append(cg.getMetaDataText("Attempts to delete the database entry corresponding to the " + pClass.Name.Capitalized(), false,
            (int)tab.XX, lang, "Integer"));
        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.Append(Strings.Space((int)tab.XX) + "Friend Shared Function Remove" + pClass.Name.Capitalized());
            strB.AppendLine("(ByVal obj As " + pClass.Name.Capitalized() + ") As Integer");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If obj Is Nothing Then Return -1");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim comm As New SqlCommand");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "With comm");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "'.CommandText = 'Insert Sproc Name Here");
            // strB.AppendLine(Strings.Space((int)tab.XXXXX) & ".Parameters.AddWithValue(""@"" & " & pClass.Name.Capitalized() & ".db_ID, obj.ID)")
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + getParaString(lang, pClass, "ID", "obj.ID"));
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End With");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return UpdateObject(comm)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return -1");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.Append(Strings.Space((int)tab.XX) + "internal static int Remove" + pClass.Name.Capitalized());
            strB.AppendLine("(" + pClass.Name.Capitalized() + " obj)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (obj == null) return -1;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SqlCommand comm = new SqlCommand();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "//comm.CommandText = //Insert Sproc Name Here;");
            // strB.AppendLine(Strings.Space((int)tab.XXXX) & "comm.Parameters.AddWithValue(""@"" + " & pClass.Name.Capitalized() & ".db_ID, obj.ID);")
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + getParaString(lang, pClass, "ID", "obj.ID"));
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return UpdateObject(comm);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return -1;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    public static string getDALFunction_SelectAll(ProjectClass pClass, string readConnection, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        strB.Append(CodeGeneration.getMetaDataText("Gets a list of all " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized()
                                                    + " objects from the database.", false, (int)tab.XX, lang,
                                                    cg.GetByLanguage(lang, "List(Of ", "List<")
                                                   + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized()
                                                   + cg.GetByLanguage(lang, ")", ">")));
        string sprocName = "sproc" + pClass.Name.PluralAndCapitalized + "GetAll";

        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Shared Function Get" + pClass.Name.PluralAndCapitalized
                + "() As List(Of " + pClass.Name.Capitalized() + ")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim comm As New SqlCommand(\"" + sprocName + "\")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim retList As New List(Of " + pClass.Name.Capitalized() + ")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = System.Data.CommandType.StoredProcedure");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Dim dr As SqlDataReader = GetDataReader(comm)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "While dr.Read()");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retList.Add(New " + pClass.Name.Capitalized() + "(dr))");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End While");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return retList");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "public static List<" + pClass.Name.Capitalized() + "> Get" + pClass.Name.PluralAndCapitalized + "()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SqlCommand comm = new SqlCommand(\"" + sprocName + "\");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<" + pClass.Name.Capitalized() + "> retList = new List<" + pClass.Name.Capitalized() + ">();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = System.Data.CommandType.StoredProcedure;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "SqlDataReader dr = GetDataReader(comm);");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "while (dr.Read())");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retList.Add(new " + pClass.Name.Capitalized() + "(dr));");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return retList;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    public static string getDALFunction_SelectAssociative(ProjectClass pClass, string readConnection, cg.Language lang) {
        StringBuilder retStr = new StringBuilder();
        foreach (ProjectClass assProjClass in pClass.AssociatedClasses) {
            retStr.Append(CodeGeneration.getMetaDataText("Gets a list of all " + pClass.NameWithNameSpace + " objects from the database.", false,
                                                  (int)tab.XX, lang, cg.GetByLanguage(lang, "List(Of ", "List<")
                                                  + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized()
                                                  + cg.GetByLanguage(lang, ")", ">")));
            string sprocName = "sproc" + pClass.Name.PluralAndCapitalized + "GetFor" + assProjClass.Name.Capitalized();
            if (lang == CodeGeneration.Language.VisualBasic) {
                retStr.AppendLine(Strings.Space((int)tab.XX) + "Public Shared Function Get" + pClass.Name.PluralAndCapitalized + "(obj AS " + assProjClass.NameWithNameSpace + ")" + " As List(Of " + pClass.NameWithNameSpace + ")");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "Dim comm As New SqlCommand(\"" + sprocName + "\")");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "Dim retList As New List(Of " + pClass.Name.Capitalized() + ")");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "Try");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = Data.CommandType.StoredProcedure");
                // retStr.AppendLine(Strings.Space((int)tab.XXXX) & "comm.Parameters.AddWithValue(""@"" & " & assProjClass.Name.Capitalized() & ".db_ID, obj.ID)")
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + getParaString(lang, assProjClass, "ID", "obj.ID"));
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "Dim dr As SqlDataReader = GetDataReader(comm)");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "While dr.Read()");
                retStr.AppendLine(Strings.Space((int)tab.XXXXX) + "Dim newObj  as New " + pClass.Name.Capitalized() + "(dr))");
                retStr.AppendLine(Strings.Space((int)tab.XXXXX) + "newObj." + assProjClass.Name.Capitalized() + " = obj");
                retStr.AppendLine(Strings.Space((int)tab.XXXXX) + "retList.Add(newObj)");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "End While");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");

                retStr.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "Return retList");
                retStr.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            } else {
                retStr.AppendLine(Strings.Space((int)tab.XX) + "public static List<" + pClass.NameWithNameSpace + "> Get" + pClass.Name.PluralAndCapitalized + "(" + assProjClass.NameWithNameSpace + " obj)");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "Dim comm As New SqlCommand(\"" + sprocName + "\")");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "List<" + pClass.Name.Capitalized() + "> retList = new List<" + pClass.Name.Capitalized() + ">()");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "try");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "{");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = System.Data.CommandType.StoredProcedure;");
                // retStr.AppendLine(Strings.Space((int)tab.XXXX) & "comm.Parameters.AddWithValue(""@"" + " & assProjClass.Name.Capitalized() & ".db_ID, obj.ID);")
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + getParaString(lang, assProjClass, "ID", "obj.ID"));
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "SqlDataReader dr = GetDataReader(comm);");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "while (dr.Read())");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "{");
                retStr.AppendLine(Strings.Space((int)tab.XXXXX) + pClass.Name.Capitalized() + " newObj = new " + pClass.Name.Capitalized() + "(dr));");
                retStr.AppendLine(Strings.Space((int)tab.XXXXX) + "newObj." + assProjClass.Name.Capitalized() + " = obj;");
                retStr.AppendLine(Strings.Space((int)tab.XXXXX) + "retList.Add(newObj);");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "}");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "}");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "{");
                retStr.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close();");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "}");
                retStr.AppendLine(Strings.Space((int)tab.XXX) + "return retList;");
                retStr.AppendLine(Strings.Space((int)tab.XX) + "}");
            }
        }
        retStr.AppendLine();

        return retStr.ToString();
    }
    public static string getObjectFromQueryString(ProjectClass pClass, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(CodeGeneration.getMetaDataText("Gets the " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized()
                                                        + " correposponding with the given ID", false, (int)tab.XX, lang, pClass.Name.Capitalized()));
        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Shared Function Get" + pClass.Name.Capitalized() + "(ByVal idstring As String, ByVAl retNewObject As Boolean) As " + pClass.Name.Capitalized());
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim retObject As " + pClass.Name.Capitalized() + " = Nothing");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim ID As Integer ");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If Integer.TryParse(idstring, ID) Then ");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If ID = -1 AndAlso retNewObject Then ");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObject = New " + pClass.Name.Capitalized());
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObject.ID = -1");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ElseIf ID >= 0 Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObject = Get" + pClass.Name.Capitalized() + "(ID)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return retObject");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "public static " + pClass.Name.Capitalized() + " Get" + pClass.Name.Capitalized() + "(String idstring, Boolean retNewObject)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + " retObject = null;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "int ID;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (int.TryParse(idstring, out ID))");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (ID == -1 && retNewObject)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObject = new " + pClass.Name.Capitalized() + "();");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObject.ID = -1;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "else if (ID >= 0)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObject = Get" + pClass.Name.Capitalized() + "(ID);");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return retObject;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    public static string getDALFunction_SelectSingle(ProjectClass pClass, string readConnection, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(CodeGeneration.getMetaDataText("Gets the " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Text()
                                                    + "corresponding with the given ID", false, (int)tab.XX, lang, pClass.Name.Capitalized()));

        string sprocName = "sproc" + pClass.Name.Capitalized() + "Get";

        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Shared Function Get" + pClass.Name.Capitalized() + "(ByVal id As Integer) As " + pClass.Name.Capitalized());
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim comm As New SqlCommand(\"" + sprocName + "\")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim retObj As " + pClass.Name.Capitalized() + " = Nothing ");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "With comm");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + getParaString(lang, pClass, "ID", "id"));
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End With");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Dim dr As SqlDataReader = GetDataReader(comm)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "While dr.Read()");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObj = New " + pClass.Name.Capitalized() + "(dr)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End While");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return retObj");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "public static " + pClass.Name.Capitalized() + " Get" + pClass.Name.Capitalized() + "(int id)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SqlCommand comm = new SqlCommand(\"" + sprocName + "\");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + " retObj = null;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + getParaString(lang, pClass, "ID", "id"));

            strB.AppendLine(Strings.Space((int)tab.XXXX) + "SqlDataReader dr = GetDataReader(comm);");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "while (dr.Read())");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "retObj = new " + pClass.Name.Capitalized() + "(dr);");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return retObj;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private static string getParaString(cg.Language lang, ProjectClass projectClass, string strConst, string strValueVar, string conCat = null) {
        string strStart = "";
        string strEndLine = "";
        if (lang == CodeGeneration.Language.CSharp) {
            strStart = "comm";
            strEndLine = ";";
        }
        if (conCat == null)
            conCat = cg.GetByLanguage(lang, "&", "+");
        return string.Format("{0}.Parameters.AddWithValue(\"@\" {1} {2}.db_{3}, {4}){5}",
                                strStart, conCat, projectClass.Name.Capitalized(), strConst, strValueVar, strEndLine);
    }

    public static string getDALFunction_Insert(ProjectClass pClass, string editConnection, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        string dataParameter = "";
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            if (classVar.ParameterType.IsImage) {
                dataParameter = cg.GetByLanguage(lang, ", ByVal data As Byte()", ", Byte[] data");
            }
        }
        string sprocName = "sproc_" + pClass.Name.Capitalized() + "Add";
        strB.AppendLine(CodeGeneration.getMetaDataText("Attempts to add a database entry corresponding to the given "
                                                       + pClass.Name.Capitalized(), false, (int)tab.XX, lang, "Integer"));
        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Friend Shared Function Add" + pClass.Name.Capitalized()
                                        + "(ByVal obj As " + pClass.Name.Capitalized() + dataParameter + ") As Integer");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If obj Is Nothing Then Return -1");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim comm As New SqlCommand(\"" + sprocName + "\")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "With comm");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "internal static int Add" + pClass.Name.Capitalized()
                                        + "(" + pClass.Name.Capitalized() + " obj" + dataParameter + ")");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (obj == null) return -1;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SqlCommand comm = new SqlCommand(\"" + sprocName + "\");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        }
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            if (classVar.Name != "ID" && classVar.IsDatabaseBound && !classVar.IsAssociative) {
                // have to add the var name at start because c# can not use With
                string optStr = "";
                string objVar = "";
                // strB.Append(Strings.Space((int)tab.XXXX) & IIf(lang = CodeGeneration.Language.CSharp, "comm", Strings.Space((int)tab.X)).ToString())
                // strB.Append(".Parameters.AddWithValue(""@"" & " & pClass.Name.Capitalized() & ".db_" & classVar.Name & ", ")
                if (classVar.ParameterType.IsImage)
                    objVar = "data";
                else if (!CodeGeneration.isRegularDataType(classVar.ParameterType.Name()))
                    objVar = "obj." + classVar.getVariableName();
                else
                    objVar = "obj." + classVar.Name + (classVar.ParameterType.IsNameAlias ? ".TextUnFormatted" : "");
                // strB.AppendLine(")" & IIf(lang = CodeGeneration.Language.CSharp, ";", "").ToString())
                strB.AppendLine(Strings.Space((int)tab.XXXX) + getParaString(lang, pClass, classVar.Name, objVar));
            }
        }
        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End With");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return AddObject(comm, \"@\" & " + pClass.Name.Capitalized() + ".db_ID)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return -1");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return AddObject(comm, \"@\" + " + pClass.Name.Capitalized() + ".db_ID);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return -1;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    public static string getDALFunction_Update(ProjectClass pClass, string editConnection, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        string dataParameter = "";
        char conCat = '&';
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            if (classVar.ParameterType.IsImage)
                dataParameter = ", ByVal data As Byte()";
        }
        string sprocName = "sproc_" + pClass.Name.Capitalized() + "Update";

        strB.AppendLine(CodeGeneration.getMetaDataText("Attempts to the database entry corresponding to the given "
                                                       + pClass.Name.Capitalized(), false, (int)tab.XX, lang, "Integer"));
        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Friend Shared Function Update" + pClass.Name.Capitalized()
+ "(ByVal obj As " + pClass.Name.Capitalized() + dataParameter + ") As Integer");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If obj Is Nothing Then Return -1");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim comm As New SqlCommand(\"" + sprocName + "\")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "With comm");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "internal static int Update" + pClass.Name.Capitalized()
                      + "(" + pClass.Name.Capitalized() + dataParameter + " obj)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (obj == null) return -1;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SqlCommand comm = new SqlCommand(\"" + sprocName + "\");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            conCat = '+';
        }
        foreach (ClassVariable classVar in pClass.ClassVariables) {
            if (!classVar.IsDatabaseBound)
                continue;
            if (classVar.IsAssociative)
                continue;
            // have to add the var name at start because c# can not use With
            // strB.Append(Strings.Space((int)tab.XXXX) & IIf(lang = CodeGeneration.Language.CSharp, "comm", Strings.Space((int)tab.X)).ToString())
            // strB.Append(String.Format(".Parameters.AddWithValue(""@"" {0} {1}.db_{3}, ", _
            // conCat, pClass.Name.Capitalized(), classVar.Name))
            string objVar;
            if (classVar.ParameterType.IsImage)
                objVar = "data";
            else if (!CodeGeneration.isRegularDataType(classVar.ParameterType.Name()))
                objVar = "obj." + classVar.getVariableName();
            else if (classVar.ParameterType.IsNameAlias)
                objVar = "obj." + classVar.Name + ".TextUnFormatted";
            else
                objVar = "obj." + classVar.Name;
            // strB.AppendLine(")" & IIf(lang = CodeGeneration.Language.CSharp, ";", "").ToString())
            strB.AppendLine(Strings.Space((int)tab.XXXX) + getParaString(lang, pClass, classVar.Name, objVar));
        }
        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End With");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return UpdateObject(comm)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return -1");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return UpdateObject(comm);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return -1;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }

    public static string getDALSkeleton(string readOnlyString, string editOnlySTring, DALClass pVariable, cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        strB.Append(cg.getPageImports(lang, true, true));
        if (lang == CodeGeneration.Language.VisualBasic) {
            strB.AppendLine();
            strB.AppendLine("Namespace " + pVariable.NameSpaceName.Name);
            strB.AppendLine(Strings.Space((int)tab.X) + "Public Class " + pVariable.Name);
            strB.AppendLine(Strings.Space((int)tab.XX) + "Inherits IRICommonObjects.DALBase");
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Shared " + readOnlyString + " As String = ConfigurationManager.AppSettings(\"" + readOnlyString + "\")");
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Shared " + editOnlySTring + " As String = ConfigurationManager.AppSettings(\"" + editOnlySTring + "\")");
            strB.AppendLine();
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Sub New()");
            strB.AppendLine();
            strB.AppendLine(Strings.Space((int)tab.X) + "End Sub");
            strB.AppendLine(getEnums(lang));
            strB.AppendLine(getDBConnectFunctionsInVB());
            strB.AppendLine(Strings.Space((int)tab.X) + "End Class");
            strB.AppendLine("End NameSpace");
        } else {
            strB.AppendLine();
            strB.AppendLine("namespace " + pVariable.NameSpaceName.Name);
            strB.AppendLine("{");
            strB.AppendLine(Strings.Space((int)tab.X) + "public class " + pVariable.Name + " : IRICommonObjects.DALBase");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "private static string " + readOnlyString + " = ConfigurationManager.AppSettings[\"" + readOnlyString + "\"];");
            strB.AppendLine(Strings.Space((int)tab.XX) + "private static string " + editOnlySTring + " = ConfigurationManager.AppSettings[\"" + editOnlySTring + "\"];");
            strB.AppendLine(Strings.Space((int)tab.XX) + "private " + pVariable.Name + "()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(getEnums(lang));
            strB.AppendLine(getDBConnectFunctionsInCSharp());
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
            strB.AppendLine("}");
        }
        return strB.ToString();
    }
    // These next few functions need added into the system in both VB and C# 
    // These function are already in use in our code and they will help streamline code.
    private static string getEnums(cg.Language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == cg.Language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Friend Enum dbAction As Integer");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Read");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Edit");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Enum");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "internal enum dbAction ");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Read,");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Edit");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    private static string getDBConnectFunctionsInVB() {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(cg.getRegionStart(CodeGeneration.Language.VisualBasic, "Database Connections"));
        strB.AppendLine(Strings.Space((int)tab.XX) + "Friend Sub ConnectToDatabase(ByVal comm As SqlCommand, Optional ByVal action As dbAction = dbAction.Read)");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "'Dim conn As SqlConnection");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "If action = dbAction.Edit Then");
        strB.AppendLine(Strings.Space((int)tab.XXXXX) + "comm.Connection = New SqlConnection(EditOnlyConnectionString)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else");
        strB.AppendLine(Strings.Space((int)tab.XXXXX) + "comm.Connection = New SqlConnection(ReadOnlyConnectionString)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = Data.CommandType.StoredProcedure");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
        strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function GetDataReader(ByVal comm As SqlCommand) As SqlDataReader");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "ConnectToDatabase(comm)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Open()");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return comm.ExecuteReader()");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.Message)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.StackTrace)");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
        strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");

        strB.AppendLine(Strings.Space((int)tab.XX));
        strB.AppendLine(Strings.Space((int)tab.XX));


        strB.AppendLine(Strings.Space((int)tab.XX) + "Friend Function AddObject(ByVal comm As SqlCommand, ByVal parameterName As String) As Integer");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim retInt As Integer = 0");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection = New SqlConnection(EditOnlyConnectionString)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = System.Data.CommandType.StoredProcedure");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Open()");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "Dim retParameter As SqlParameter");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retParameter = comm.Parameters.Add(parameterName, Data.SqlDbType.Int)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retParameter.Direction = Data.ParameterDirection.Output");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.ExecuteNonQuery()");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = CInt(retParameter.Value)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "If comm.Connection IsNot Nothing Then _");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = -1");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.Message)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.StackTrace)");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Return retInt");
        strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");

        strB.AppendLine(Strings.Space((int)tab.XX));
        strB.AppendLine(Strings.Space((int)tab.XX));


        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.VisualBasic, true) + " <summary>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.VisualBasic, true) + " Sets Connection and Executes given comm on the database");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.VisualBasic, true) + " </summary>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.VisualBasic, true) + " <param name=\"comm\">SQLCommand to execute</param>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.VisualBasic, true) + " <returns>number of rows affected; -1 on failure, positive value on success.</returns>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.VisualBasic, true) + " <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.VisualBasic, true) + "       Edit Connection is set here.</remarks>");

        strB.AppendLine(Strings.Space((int)tab.XX) + "Friend Function UpdateObject(ByVal comm As SqlCommand) As Integer");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim retInt As Integer = 0");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection = New SqlConnection(EditOnlyConnectionString)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = Data.CommandType.StoredProcedure");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Open()");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = comm.ExecuteNonQuery()");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "If comm.Connection IsNot Nothing Then _");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close()");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = -1");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.Message)");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.StackTrace)");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "Return retInt");
        strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        strB.AppendLine(cg.getRegionEnd(CodeGeneration.Language.VisualBasic));
        return strB.ToString();
    }

    private static string getDBConnectFunctionsInCSharp() {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(cg.getRegionStart(CodeGeneration.Language.CSharp, "Database Connections"));
        strB.AppendLine(Strings.Space((int)tab.XX) + "internal static void ConnectToDatabase(SqlCommand comm, dbAction action = dbAction.Read)");
        strB.AppendLine(Strings.Space((int)tab.XX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (action == dbAction.Edit)");
        strB.AppendLine(Strings.Space((int)tab.XXXXX) + "comm.Connection = new SqlConnection(EditOnlyConnectionString);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "else");
        strB.AppendLine(Strings.Space((int)tab.XXXXX) + "comm.Connection = new SqlConnection(ReadOnlyConnectionString);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = System.Data.CommandType.StoredProcedure;");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}catch(Exception ex){}");
        strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XX) + "public static SqlDataReader GetDataReader(SqlCommand comm)");
        strB.AppendLine(Strings.Space((int)tab.XX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "ConnectToDatabase(comm);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Open();");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "return comm.ExecuteReader();");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.Message);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.StackTrace);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "return null;");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XX) + "}");

        strB.AppendLine(Strings.Space((int)tab.XX));
        strB.AppendLine(Strings.Space((int)tab.XX));

        strB.AppendLine(Strings.Space((int)tab.XX) + "internal static int AddObject(SqlCommand comm, string parameterName)");
        strB.AppendLine(Strings.Space((int)tab.XX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "int retInt = 0;");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection = new SqlConnection(EditOnlyConnectionString);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = System.Data.CommandType.StoredProcedure;");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Open();");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "SqlParameter retParameter;");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retParameter = comm.Parameters.Add(parameterName, System.Data.SqlDbType.Int);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retParameter.Direction = System.Data.ParameterDirection.Output;");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.ExecuteNonQuery();");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = (int)retParameter.Value;");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close();");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (comm.Connection != null)");
        strB.AppendLine(Strings.Space((int)tab.XXXXX) + "comm.Connection.Close();");
        strB.AppendLine(Strings.Space((int)tab.XXXX));
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = -1;");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.Message);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.StackTrace);");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "return retInt;");
        strB.AppendLine(Strings.Space((int)tab.XX) + "}");

        strB.AppendLine(Strings.Space((int)tab.XX));
        strB.AppendLine(Strings.Space((int)tab.XX));


        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.CSharp, true) + " <summary>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.CSharp, true) + " Sets Connection and Executes given comm on the database");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.CSharp, true) + " </summary>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.CSharp, true) + " <param name=\"comm\">SQLCommand to execute</param>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.CSharp, true) + " <returns>number of rows affected; -1 on failure, positive value on success.</returns>");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.CSharp, true) + " <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.");
        strB.AppendLine(Strings.Space((int)tab.XX) + cg.getCommentString(cg.Language.CSharp, true) + "       Edit Connection is set here.</remarks>");

        strB.AppendLine(Strings.Space((int)tab.XX) + "internal static int UpdateObject(SqlCommand comm)");
        strB.AppendLine(Strings.Space((int)tab.XX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "int retInt = 0;");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection = new SqlConnection(EditOnlyConnectionString);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.CommandType = System.Data.CommandType.StoredProcedure;");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Open();");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = comm.ExecuteNonQuery();");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "comm.Connection.Close();");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "catch(Exception ex)");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "if(comm.Connection != null)");
        strB.AppendLine(Strings.Space((int)tab.XXXXX) + "comm.Connection.Close();");
        strB.AppendLine(Strings.Space((int)tab.XXXX));
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "retInt = -1;");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.Message);");
        strB.AppendLine(Strings.Space((int)tab.XXXX) + "System.Diagnostics.Debug.WriteLine(ex.StackTrace);");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "return retInt;");
        strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        strB.AppendLine(cg.getRegionEnd(CodeGeneration.Language.CSharp));
        return strB.ToString();
    }
}

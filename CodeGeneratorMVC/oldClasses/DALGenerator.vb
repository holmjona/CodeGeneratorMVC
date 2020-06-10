Imports System.Windows.Forms
Imports System.Text
Imports System.Collections.Generic
Imports cg = CodeGeneratorAddIn.CodeGeneration
Imports tab = CodeGeneratorAddIn.CodeGeneration.Tabs

Public Class DALGenerator

    Public Shared Function getDALFunctions(ByVal pClass As ProjectClass, ByVal copyResultsToClipboard As Boolean, lang As cg.Language) As String
        Dim strB As New StringBuilder
        Dim ObjectName As String = pClass.Name.Capitalized
        Dim readConnection As String = pClass.DALClassVariable.ReadOnlyConnectionString.Name
        Dim editConnection As String = pClass.DALClassVariable.EditOnlyConnectionstring.Name
        If readConnection.Length > 0 And editConnection.Length > 0 And ObjectName.Length > 0 Then
            strB.AppendLine()
            strB.AppendLine(cg.getRegionStart(lang, ObjectName))
            strB.AppendLine(getObjectFromQueryString(pClass, lang))
            strB.AppendLine(getDALFunction_SelectSingle(pClass, readConnection, lang))
            strB.AppendLine(getDALFunction_SelectAll(pClass, readConnection, lang))
            strB.AppendLine(getDALFunction_SelectAssociative(pClass, readConnection, lang))
            strB.AppendLine(getDALFunction_Insert(pClass, editConnection, lang))
            strB.AppendLine(getDALFunction_Update(pClass, editConnection, lang))
            strB.AppendLine(getDALFunction_Delete(pClass, editConnection, lang))
            strB.AppendLine(cg.getRegionEnd(lang))
            If copyResultsToClipboard Then
                Clipboard.Clear()
                Clipboard.SetText(strB.ToString())
            End If
        End If
        Return strB.ToString()
    End Function
    Public Shared Function getDALFunction_Delete(ByVal pClass As ProjectClass, ByVal editconn As String, lang As cg.Language) As String
        Dim strB As New StringBuilder
        strB.Append(cg.getMetaDataText("Attempts to delete the database entry corresponding to the " & pClass.Name.Capitalized, False, tab.XX, lang, "Integer"))
        If lang = CodeGeneration.Language.VisualBasic Then
            strB.Append(Space(tab.XX) & "Friend Shared Function Remove" & pClass.Name.Capitalized)
            strB.AppendLine("(ByVal obj As " & pClass.Name.Capitalized & ") As Integer")
            strB.AppendLine(Space(tab.XXX) & "If obj Is Nothing Then Return -1")
            strB.AppendLine(Space(tab.XXX) & "Dim comm As New SqlCommand")
            strB.AppendLine(Space(tab.XXX) & "Try")
            strB.AppendLine(Space(tab.XXXX) & "With comm")
            strB.AppendLine(Space(tab.XXXXX) & "'.CommandText = 'Insert Sproc Name Here")
            'strB.AppendLine(Space(tab.XXXXX) & ".Parameters.AddWithValue(""@"" & " & pClass.Name.Capitalized & ".db_ID, obj.ID)")
            strB.AppendLine(Space(tab.XXXXX) & getParaString(lang, pClass, "ID", "obj.ID"))
            strB.AppendLine(Space(tab.XXXX) & "End With")
            strB.AppendLine(Space(tab.XXXX) & "Return UpdateObject(comm)")
            strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
            strB.AppendLine(Space(tab.XXX) & "End Try")
            strB.AppendLine(Space(tab.XXX) & "Return -1")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else
            strB.Append(Space(tab.XX) & "internal static int Remove" & pClass.Name.Capitalized)
            strB.AppendLine("(" & pClass.Name.Capitalized & " obj)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "if (obj == null) return -1;")
            strB.AppendLine(Space(tab.XXX) & "SqlCommand comm = new SqlCommand();")
            strB.AppendLine(Space(tab.XXX) & "try")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "//comm.CommandText = //Insert Sproc Name Here;")
            '            strB.AppendLine(Space(tab.XXXX) & "comm.Parameters.AddWithValue(""@"" + " & pClass.Name.Capitalized & ".db_ID, obj.ID);")
            strB.AppendLine(Space(tab.XXXXX) & getParaString(lang, pClass, "ID", "obj.ID"))
            strB.AppendLine(Space(tab.XXXX) & "return UpdateObject(comm);")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return -1;")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Public Shared Function getDALFunction_SelectAll(ByVal pClass As ProjectClass, ByVal readConnection As String, lang As cg.Language) As String
        Dim strB As New StringBuilder
        strB.Append(CodeGeneration.getMetaDataText("Gets a list of all " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & _
                                                   " objects from the database.", _
                                                   False, tab.XX, lang, _
                                                   IIf(lang = CodeGeneration.Language.VisualBasic, "List(Of ", "List<").ToString() _
                                                   & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & _
                                                   IIf(lang = CodeGeneration.Language.VisualBasic, ")", ">").ToString()))
        Dim sprocName As String = "sproc" & pClass.Name.PluralAndCapitalized & "GetAll"

        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Public Shared Function Get" & pClass.Name.PluralAndCapitalized & "() As List(Of " & pClass.Name.Capitalized & ")")
            strB.AppendLine(Space(tab.XXX) & "Dim comm As New SqlCommand(""" & sprocName & """)")
            strB.AppendLine(Space(tab.XXX) & "Dim retList As New List(Of " & pClass.Name.Capitalized & ")")
            strB.AppendLine(Space(tab.XXX) & "Try")
            strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = System.Data.CommandType.StoredProcedure")
            strB.AppendLine(Space(tab.XXXX) & "Dim dr As SqlDataReader = GetDataReader(comm)")
            strB.AppendLine(Space(tab.XXXX) & "While dr.Read()")
            strB.AppendLine(Space(tab.XXXXX) & "retList.Add(New " & pClass.Name.Capitalized & "(dr))")
            strB.AppendLine(Space(tab.XXXX) & "End While")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
            strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
            strB.AppendLine(Space(tab.XXX) & "End Try")
            strB.AppendLine(Space(tab.XXX) & "Return retList")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else
            strB.AppendLine(Space(tab.XX) & "public static List<" & pClass.Name.Capitalized & "> Get" & pClass.Name.PluralAndCapitalized & "()")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "SqlCommand comm = new SqlCommand(""" & sprocName & """);")
            strB.AppendLine(Space(tab.XXX) & "List<" & pClass.Name.Capitalized & "> retList = new List<" & pClass.Name.Capitalized & ">();")
            strB.AppendLine(Space(tab.XXX) & "try")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = System.Data.CommandType.StoredProcedure;")
            strB.AppendLine(Space(tab.XXXX) & "SqlDataReader dr = GetDataReader(comm);")
            strB.AppendLine(Space(tab.XXXX) & "while (dr.Read())")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "retList.Add(new " & pClass.Name.Capitalized & "(dr));")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close();")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close();")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return retList;")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Public Shared Function getDALFunction_SelectAssociative(ByVal pClass As ProjectClass, ByVal readConnection As String, lang As cg.Language) As String
        Dim retStr As New StringBuilder
        For Each assProjClass As ProjectClass In pClass.AssociatedClasses
            retStr.Append(CodeGeneration.getMetaDataText("Gets a list of all " & pClass.NameWithNameSpace & _
                                                             " objects from the database.", False, tab.XX, lang, _
                                                             IIf(lang = CodeGeneration.Language.VisualBasic, "List(Of ", "List<").ToString() _
                                                   & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & _
                                                   IIf(lang = CodeGeneration.Language.VisualBasic, ")", ">").ToString()))
            Dim sprocName As String = "sproc" & pClass.Name.PluralAndCapitalized & "GetFor" & assProjClass.Name.Capitalized
            If lang = CodeGeneration.Language.VisualBasic Then
                retStr.AppendLine(Space(tab.XX) & "Public Shared Function Get" & pClass.Name.PluralAndCapitalized & _
                                  "(obj AS " & assProjClass.NameWithNameSpace & ")" & _
                                  " As List(Of " & pClass.NameWithNameSpace & ")")
                retStr.AppendLine(Space(tab.XXX) & "Dim comm As New SqlCommand(""" & sprocName & """)")
                retStr.AppendLine(Space(tab.XXX) & "Dim retList As New List(Of " & pClass.Name.Capitalized & ")")
                retStr.AppendLine(Space(tab.XXX) & "Try")
                retStr.AppendLine(Space(tab.XXXX) & "comm.CommandType = Data.CommandType.StoredProcedure")
                'retStr.AppendLine(Space(tab.XXXX) & "comm.Parameters.AddWithValue(""@"" & " & assProjClass.Name.Capitalized & ".db_ID, obj.ID)")
                retStr.AppendLine(Space(tab.XXXX) & getParaString(lang, assProjClass, "ID", "obj.ID"))
                retStr.AppendLine(Space(tab.XXXX) & "Dim dr As SqlDataReader = GetDataReader(comm)")
                retStr.AppendLine(Space(tab.XXXX) & "While dr.Read()")
                retStr.AppendLine(Space(tab.XXXXX) & "Dim newObj  as New " & pClass.Name.Capitalized & "(dr))")
                retStr.AppendLine(Space(tab.XXXXX) & "newObj." & assProjClass.Name.Capitalized & " = obj")
                retStr.AppendLine(Space(tab.XXXXX) & "retList.Add(newObj)")
                retStr.AppendLine(Space(tab.XXXX) & "End While")
                retStr.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
                retStr.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
                retStr.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")

                retStr.AppendLine(Space(tab.XXX) & "End Try")
                retStr.AppendLine(Space(tab.XXX) & "Return retList")
                retStr.AppendLine(Space(tab.XX) & "End Function")
            Else
                retStr.AppendLine(Space(tab.XX) & "public static List<" & pClass.NameWithNameSpace & "> Get" & pClass.Name.PluralAndCapitalized & _
                                  "(" & assProjClass.NameWithNameSpace & " obj)")
                retStr.AppendLine(Space(tab.XXX) & "Dim comm As New SqlCommand(""" & sprocName & """)")
                retStr.AppendLine(Space(tab.XXX) & "List<" & pClass.Name.Capitalized & "> retList = new List<" & pClass.Name.Capitalized & ">()")
                retStr.AppendLine(Space(tab.XXX) & "try")
                retStr.AppendLine(Space(tab.XXX) & "{")
                retStr.AppendLine(Space(tab.XXXX) & "comm.CommandType = System.Data.CommandType.StoredProcedure;")
                '                retStr.AppendLine(Space(tab.XXXX) & "comm.Parameters.AddWithValue(""@"" + " & assProjClass.Name.Capitalized & ".db_ID, obj.ID);")
                retStr.AppendLine(Space(tab.XXXX) & getParaString(lang, assProjClass, "ID", "obj.ID"))
                retStr.AppendLine(Space(tab.XXXX) & "SqlDataReader dr = GetDataReader(comm);")
                retStr.AppendLine(Space(tab.XXXX) & "while (dr.Read())")
                retStr.AppendLine(Space(tab.XXXX) & "{")
                retStr.AppendLine(Space(tab.XXXXX) & pClass.Name.Capitalized & " newObj = new " & pClass.Name.Capitalized & "(dr));")
                retStr.AppendLine(Space(tab.XXXXX) & "newObj." & assProjClass.Name.Capitalized & " = obj;")
                retStr.AppendLine(Space(tab.XXXXX) & "retList.Add(newObj);")
                retStr.AppendLine(Space(tab.XXXX) & "}")
                retStr.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
                retStr.AppendLine(Space(tab.XXXX) & "}")
                retStr.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
                retStr.AppendLine(Space(tab.XXX) & "{")
                retStr.AppendLine(Space(tab.XXXX) & "comm.Connection.Close();")
                retStr.AppendLine(Space(tab.XXX) & "}")
                retStr.AppendLine(Space(tab.XXX) & "return retList;")
                retStr.AppendLine(Space(tab.XX) & "}")
            End If
        Next
        retStr.AppendLine()

        Return retStr.ToString()
    End Function
    Public Shared Function getObjectFromQueryString(ByVal pClass As ProjectClass, lang As cg.Language) As String
        Dim strB As New StringBuilder
        strB.AppendLine(CodeGeneration.getMetaDataText("Gets the " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
                                                        & " correposponding with the given ID", False, tab.XX, lang, pClass.Name.Capitalized))
        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Public Shared Function Get" & pClass.Name.Capitalized & _
                            "(ByVal idstring As String, ByVAl retNewObject As Boolean) As " & pClass.Name.Capitalized)
            strB.AppendLine(Space(tab.XXX) & "Dim retObject As " & pClass.Name.Capitalized & " = Nothing")
            strB.AppendLine(Space(tab.XXX) & "Dim ID As Integer ")
            strB.AppendLine(Space(tab.XXX) & "If Integer.TryParse(idstring, ID) Then ")
            strB.AppendLine(Space(tab.XXXX) & "If ID = -1 AndAlso retNewObject Then ")
            strB.AppendLine(Space(tab.XXXXX) & "retObject = New " & pClass.Name.Capitalized)
            strB.AppendLine(Space(tab.XXXXX) & "retObject.ID = -1")
            strB.AppendLine(Space(tab.XXXX) & "ElseIf ID >= 0 Then")
            strB.AppendLine(Space(tab.XXXXX) & "retObject = Get" & pClass.Name.Capitalized & "(ID)")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "Return retObject")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else
            strB.AppendLine(Space(tab.XX) & "public static " & pClass.Name.Capitalized & " Get" & pClass.Name.Capitalized & _
                            "(String idstring, Boolean retNewObject)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & " retObject = null;")
            strB.AppendLine(Space(tab.XXX) & "int ID;")
            strB.AppendLine(Space(tab.XXX) & "if (int.TryParse(idstring, out ID))")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (ID == -1 && retNewObject)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "retObject = new " & pClass.Name.Capitalized & "();")
            strB.AppendLine(Space(tab.XXXXX) & "retObject.ID = -1;")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "else if (ID >= 0)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "retObject = Get" & pClass.Name.Capitalized & "(ID);")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return retObject;")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()

    End Function
    Public Shared Function getDALFunction_SelectSingle(ByVal pClass As ProjectClass, ByVal readConnection As String, lang As cg.Language) As String
        Dim strB As New StringBuilder
        strB.AppendLine(CodeGeneration.getMetaDataText("Gets the " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Text & "corresponding with the given ID", _
                                                       False, tab.XX, lang, pClass.Name.Capitalized))

        Dim sprocName As String = "sproc" & pClass.Name.Capitalized & "Get"

        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Public Shared Function Get" & pClass.Name.Capitalized & "(ByVal id As Integer) As " & pClass.Name.Capitalized)
            strB.AppendLine(Space(tab.XXX) & "Dim comm As New SqlCommand(""" & sprocName & """)")
            strB.AppendLine(Space(tab.XXX) & "Dim retObj As " & pClass.Name.Capitalized & " = Nothing ")
            strB.AppendLine(Space(tab.XXX) & "Try")
            strB.AppendLine(Space(tab.XXXX) & "With comm")
            strB.AppendLine(Space(tab.XXXXX) & getParaString(lang, pClass, "ID", "id"))
            strB.AppendLine(Space(tab.XXXX) & "End With")
            strB.AppendLine(Space(tab.XXXX) & "Dim dr As SqlDataReader = GetDataReader(comm)")
            strB.AppendLine(Space(tab.XXXX) & "While dr.Read()")
            strB.AppendLine(Space(tab.XXXXX) & "retObj = New " & pClass.Name.Capitalized & "(dr)")
            strB.AppendLine(Space(tab.XXXX) & "End While")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
            strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
            strB.AppendLine(Space(tab.XXX) & "End Try")
            strB.AppendLine(Space(tab.XXX) & "Return retObj")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else
            strB.AppendLine(Space(tab.XX) & "public static " & pClass.Name.Capitalized & " Get" & pClass.Name.Capitalized & "(int id)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "SqlCommand comm = new SqlCommand(""" & sprocName & """);")
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & " retObj = null;")
            strB.AppendLine(Space(tab.XXX) & "try")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & getParaString(lang, pClass, "ID", "id"))

            strB.AppendLine(Space(tab.XXXX) & "SqlDataReader dr = GetDataReader(comm);")
            strB.AppendLine(Space(tab.XXXX) & "while (dr.Read())")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "retObj = new " & pClass.Name.Capitalized & "(dr);")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close();")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close();")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return retObj;")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Shared Function getParaString(lang As cg.Language, projectClass As ProjectClass, strConst As String, _
                                   strValueVar As String, Optional conCat As String = Nothing) As String
        Dim strStart As String = ""
        Dim strEndLine As String = ""
        If lang = CodeGeneration.Language.CSharp Then
            strStart = "comm"
            strEndLine = ";"
        End If
        If conCat Is Nothing Then
            conCat = IIf(lang = CodeGeneration.Language.VisualBasic, "&", "+").ToString
        End If
        Return String.Format("{0}.Parameters.AddWithValue(""@"" {1} {2}.db_{3}, {4}){5}", _
                             strStart, conCat, projectClass.Name.Capitalized, strConst, strValueVar, strEndLine)
    End Function

    Public Shared Function getDALFunction_Insert(ByVal pClass As ProjectClass, ByVal editConnection As String, lang As cg.Language) As String
        Dim strB As New StringBuilder
        Dim dataParameter As String = ""
        For Each classVar As ClassVariable In pClass.ClassVariables
            If classVar.ParameterType.IsImage Then
                If lang = CodeGeneration.Language.VisualBasic Then
                    dataParameter = ", ByVal data As Byte()"
                Else
                    dataParameter = ", Byte[] data"
                End If
            End If
        Next
        Dim sprocName As String = "sproc_" & pClass.Name.Capitalized & "Add"
        strB.AppendLine(CodeGeneration.getMetaDataText("Attempts to add a database entry corresponding to the given " _
                                                       & pClass.Name.Capitalized, False, tab.XX, lang, "Integer"))
        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Friend Shared Function Add" & pClass.Name.Capitalized & _
                            "(ByVal obj As " & pClass.Name.Capitalized & dataParameter & ") As Integer")
            strB.AppendLine(Space(tab.XXX) & "If obj Is Nothing Then Return -1")
            strB.AppendLine(Space(tab.XXX) & "Dim comm As New SqlCommand(""" & sprocName & """)")
            strB.AppendLine(Space(tab.XXX) & "Try")
            strB.AppendLine(Space(tab.XXXX) & "With comm")
        Else
            strB.AppendLine(Space(tab.XX) & "internal static int Add" & pClass.Name.Capitalized & _
                            "(" & pClass.Name.Capitalized & " obj" & dataParameter & ")")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "if (obj == null) return -1;")
            strB.AppendLine(Space(tab.XXX) & "SqlCommand comm = new SqlCommand(""" & sprocName & """);")
            strB.AppendLine(Space(tab.XXX) & "try")
            strB.AppendLine(Space(tab.XXX) & "{")
        End If
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.Name = "ID" AndAlso classVar.IsDatabaseBound AndAlso Not classVar.isAssociative Then
                ' have to add the var name at start because c# can not use With
                Dim optStr As String = ""
                Dim objVar As String = ""
                'strB.Append(Space(tab.XXXX) & IIf(lang = CodeGeneration.Language.CSharp, "comm", Space(tab.X)).ToString())
                'strB.Append(".Parameters.AddWithValue(""@"" & " & pClass.Name.Capitalized & ".db_" & classVar.Name & ", ")
                If classVar.ParameterType.IsImage Then
                    objVar = "data"
                ElseIf Not CodeGeneration.isRegularDataType(classVar.ParameterType.Name) Then
                    objVar = "obj." & classVar.getVariableName()
                Else
                    objVar = "obj." & classVar.Name & IIf(classVar.ParameterType.IsNameAlias, ".TextUnFormatted", "").ToString()
                End If
                'strB.AppendLine(")" & IIf(lang = CodeGeneration.Language.CSharp, ";", "").ToString())
                strB.AppendLine(Space(tab.XXXX) & getParaString(lang, pClass, classVar.Name, objVar))
            End If
        Next
        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XXXX) & "End With")
            strB.AppendLine(Space(tab.XXXX) & "Return AddObject(comm, ""@"" & " & pClass.Name.Capitalized & ".db_ID)")
            strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
            strB.AppendLine(Space(tab.XXX) & "End Try")
            strB.AppendLine(Space(tab.XXX) & "Return -1")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else
            strB.AppendLine(Space(tab.XXXX) & "return AddObject(comm, ""@"" + " & pClass.Name.Capitalized & ".db_ID);")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return -1;")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Public Shared Function getDALFunction_Update(ByVal pClass As ProjectClass, ByVal editConnection As String, lang As cg.Language) As String
        Dim strB As New StringBuilder
        Dim dataParameter As String = ""
        Dim conCat As Char = "&"c
        For Each classVar As ClassVariable In pClass.ClassVariables
            If classVar.ParameterType.IsImage Then
                dataParameter = ", ByVal data As Byte()"
            End If
        Next
        Dim sprocName As String = "sproc_" & pClass.Name.Capitalized & "Update"

        strB.AppendLine(CodeGeneration.getMetaDataText("Attempts to the database entry corresponding to the given " _
                                                       & pClass.Name.Capitalized, False, tab.XX, lang, "Integer"))
        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Friend Shared Function Update" & pClass.Name.Capitalized _
                            & "(ByVal obj As " & pClass.Name.Capitalized & dataParameter & ") As Integer")
            strB.AppendLine(Space(tab.XXX) & "If obj Is Nothing Then Return -1")
            strB.AppendLine(Space(tab.XXX) & "Dim comm As New SqlCommand(""" & sprocName & """)")
            strB.AppendLine(Space(tab.XXX) & "Try")
            strB.AppendLine(Space(tab.XXXX) & "With comm")
        Else
            strB.AppendLine(Space(tab.XX) & "internal static int Update" & pClass.Name.Capitalized _
                            & "(" & pClass.Name.Capitalized & dataParameter & " obj)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "if (obj == null) return -1;")
            strB.AppendLine(Space(tab.XXX) & "SqlCommand comm = new SqlCommand(""" & sprocName & """);")
            strB.AppendLine(Space(tab.XXX) & "try")
            strB.AppendLine(Space(tab.XXX) & "{")
            conCat = "+"c
        End If
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.IsDatabaseBound Then Continue For
            If classVar.isAssociative Then Continue For
            ' have to add the var name at start because c# can not use With
            'strB.Append(Space(tab.XXXX) & IIf(lang = CodeGeneration.Language.CSharp, "comm", Space(tab.X)).ToString())
            'strB.Append(String.Format(".Parameters.AddWithValue(""@"" {0} {1}.db_{3}, ", _
            '                           conCat, pClass.Name.Capitalized, classVar.Name))
            Dim objVar As String
            If classVar.ParameterType.IsImage Then
                objVar = "data"
            ElseIf Not CodeGeneration.isRegularDataType(classVar.ParameterType.Name) Then
                objVar = "obj." & classVar.getVariableName()
            ElseIf classVar.ParameterType.IsNameAlias Then
                objVar = "obj." & classVar.Name & ".TextUnFormatted"
            Else
                objVar = "obj." & classVar.Name
            End If
            'strB.AppendLine(")" & IIf(lang = CodeGeneration.Language.CSharp, ";", "").ToString())
            strB.AppendLine(Space(tab.XXXX) & getParaString(lang, pClass, classVar.Name, objVar))
        Next
        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XXXX) & "End With")
            strB.AppendLine(Space(tab.XXXX) & "Return UpdateObject(comm)")
            strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
            strB.AppendLine(Space(tab.XXX) & "End Try")
            strB.AppendLine(Space(tab.XXX) & "Return -1")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else
            strB.AppendLine(Space(tab.XXXX) & "return UpdateObject(comm);")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return -1;")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function

    Public Shared Function getDALSkeleton(ByVal readOnlyString As String, ByVal editOnlySTring As String, ByVal pVariable As DALClass, lang As cg.Language) As String
        Dim strB As New StringBuilder()
        strB.Append(cg.getPageImports(lang, True, True))
        If lang = CodeGeneration.Language.VisualBasic Then
            strB.AppendLine()
            strB.AppendLine("Namespace " & pVariable.NameSpaceName.Name)
            strB.AppendLine(Space(tab.X) & "Public Class " & pVariable.Name)
            strB.AppendLine(Space(tab.XX) & "Inherits IRICommonObjects.DALBase")
            strB.AppendLine(Space(tab.X) & "Private Shared " & readOnlyString & " As String = ConfigurationManager.AppSettings(""" & readOnlyString & """)")
            strB.AppendLine(Space(tab.X) & "Private Shared " & editOnlySTring & " As String = ConfigurationManager.AppSettings(""" & editOnlySTring & """)")
            strB.AppendLine()
            strB.AppendLine(Space(tab.X) & "Private Sub New()")
            strB.AppendLine()
            strB.AppendLine(Space(tab.X) & "End Sub")
            strB.AppendLine(getEnums(lang))
            strB.AppendLine(getDBConnectFunctionsInVB)
            strB.AppendLine(Space(tab.X) & "End Class")
            strB.AppendLine("End NameSpace")
        Else
            strB.AppendLine()
            strB.AppendLine("namespace " & pVariable.NameSpaceName.Name)
            strB.AppendLine("{")
            strB.AppendLine(Space(tab.X) & "public class " & pVariable.Name & " : IRICommonObjects.DALBase")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "private static string " & readOnlyString & " = ConfigurationManager.AppSettings[""" & readOnlyString & """];")
            strB.AppendLine(Space(tab.XX) & "private static string " & editOnlySTring & " = ConfigurationManager.AppSettings[""" & editOnlySTring & """];")
            strB.AppendLine(Space(tab.XX) & "private " & pVariable.Name & "()")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(getEnums(lang))
            strB.AppendLine(getDBConnectFunctionsInCSharp)
            strB.AppendLine(Space(tab.X) & "}")
            strB.AppendLine("}")
        End If
        Return strB.ToString()

    End Function
    ' These next few functions need added into the system in both VB and C# 
    ' These function are already in use in our code and they will help streamline code.
    Private Shared Function getEnums(lang As cg.Language) As String
        Dim strB As New StringBuilder
        If lang = cg.Language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Friend Enum dbAction As Integer")
            strB.AppendLine(Space(tab.XXX) & "Read")
            strB.AppendLine(Space(tab.XXX) & "Edit")
            strB.AppendLine(Space(tab.XX) & "End Enum")
        Else
            strB.AppendLine(Space(tab.XX) & "internal enum dbAction ")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "Read,")
            strB.AppendLine(Space(tab.XXX) & "Edit")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    Private Shared Function getDBConnectFunctionsInVB() As String
        Dim strB As New StringBuilder
        strB.AppendLine(cg.getRegionStart(CodeGeneration.Language.VisualBasic, "Database Connections"))
        strB.AppendLine(Space(tab.XX) & "Friend Sub ConnectToDatabase(ByVal comm As SqlCommand, Optional ByVal action As dbAction = dbAction.Read)")
        strB.AppendLine(Space(tab.XXX) & "'Dim conn As SqlConnection")
        strB.AppendLine(Space(tab.XXX) & "Try")
        strB.AppendLine(Space(tab.XXXX) & "If action = dbAction.Edit Then")
        strB.AppendLine(Space(tab.XXXXX) & "comm.Connection = New SqlConnection(EditOnlyConnectionString)")
        strB.AppendLine(Space(tab.XXXX) & "Else")
        strB.AppendLine(Space(tab.XXXXX) & "comm.Connection = New SqlConnection(ReadOnlyConnectionString)")
        strB.AppendLine(Space(tab.XXXX) & "End If")
        strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = Data.CommandType.StoredProcedure")
        strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
        strB.AppendLine(Space(tab.XXX) & "End Try")
        strB.AppendLine(Space(tab.XX) & "End Function")
        strB.AppendLine(Space(tab.XX) & "Public Function GetDataReader(ByVal comm As SqlCommand) As SqlDataReader")
        strB.AppendLine(Space(tab.XXX) & "Try")
        strB.AppendLine(Space(tab.XXXX) & "ConnectToDatabase(comm)")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Open()")
        strB.AppendLine(Space(tab.XXXX) & "Return comm.ExecuteReader()")
        strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.Message)")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.StackTrace)")
        strB.AppendLine(Space(tab.XXX) & "End Try")
        strB.AppendLine(Space(tab.XX) & "End Function")

        strB.AppendLine(Space(tab.XX))
        strB.AppendLine(Space(tab.XX))


        strB.AppendLine(Space(tab.XX) & "Friend Function AddObject(ByVal comm As SqlCommand, ByVal parameterName As String) As Integer")
        strB.AppendLine(Space(tab.XXX) & "Dim retInt As Integer = 0")
        strB.AppendLine(Space(tab.XXX) & "Try")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection = New SqlConnection(EditOnlyConnectionString)")
        strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = System.Data.CommandType.StoredProcedure")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Open()")
        strB.AppendLine(Space(tab.XXXX) & "Dim retParameter As SqlParameter")
        strB.AppendLine(Space(tab.XXXX) & "retParameter = comm.Parameters.Add(parameterName, Data.SqlDbType.Int)")
        strB.AppendLine(Space(tab.XXXX) & "retParameter.Direction = Data.ParameterDirection.Output")
        strB.AppendLine(Space(tab.XXXX) & "comm.ExecuteNonQuery()")
        strB.AppendLine(Space(tab.XXXX) & "retInt = CInt(retParameter.Value)")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
        strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
        strB.AppendLine(Space(tab.XXXX) & "If comm.Connection IsNot Nothing Then _")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
        strB.AppendLine(Space(tab.XXXX) & "retInt = -1")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.Message)")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.StackTrace)")
        strB.AppendLine(Space(tab.XXX) & "End Try")
        strB.AppendLine(Space(tab.XXX) & "Return retInt")
        strB.AppendLine(Space(tab.XX) & "End Function")

        strB.AppendLine(Space(tab.XX))
        strB.AppendLine(Space(tab.XX))


        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.VisualBasic, True) & _
                        " <summary>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.VisualBasic, True) & _
                        " Sets Connection and Executes given comm on the database")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.VisualBasic, True) & _
                        " </summary>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.VisualBasic, True) & _
                        " <param name=""comm"">SQLCommand to execute</param>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.VisualBasic, True) & _
                        " <returns>number of rows affected; -1 on failure, positive value on success.</returns>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.VisualBasic, True) & _
                        " <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.VisualBasic, True) & _
                        "       Edit Connection is set here.</remarks>")

        strB.AppendLine(Space(tab.XX) & "Friend Function UpdateObject(ByVal comm As SqlCommand) As Integer")
        strB.AppendLine(Space(tab.XXX) & "Dim retInt As Integer = 0")
        strB.AppendLine(Space(tab.XXX) & "Try")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection = New SqlConnection(EditOnlyConnectionString)")
        strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = Data.CommandType.StoredProcedure")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Open()")
        strB.AppendLine(Space(tab.XXXX) & "retInt = comm.ExecuteNonQuery()")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
        strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
        strB.AppendLine(Space(tab.XXXX) & "If comm.Connection IsNot Nothing Then _")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close()")
        strB.AppendLine(Space(tab.XXXX) & "retInt = -1")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.Message)")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.StackTrace)")
        strB.AppendLine(Space(tab.XXX) & "End Try")
        strB.AppendLine(Space(tab.XXX) & "Return retInt")
        strB.AppendLine(Space(tab.XX) & "End Function")
        strB.AppendLine(cg.getRegionEnd(CodeGeneration.Language.VisualBasic))
        Return strB.ToString()
    End Function

    Private Shared Function getDBConnectFunctionsInCSharp() As String
        Dim strB As New StringBuilder
        strB.AppendLine(cg.getRegionStart(CodeGeneration.Language.CSharp, "Database Connections"))
        strB.AppendLine(Space(tab.XX) & "internal static void ConnectToDatabase(SqlCommand comm, dbAction action = dbAction.Read)")
        strB.AppendLine(Space(tab.XX) & "{")
        strB.AppendLine(Space(tab.XXX) & "try")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "if (action == dbAction.Edit)")
        strB.AppendLine(Space(tab.XXXXX) & "comm.Connection = new SqlConnection(EditOnlyConnectionString);")
        strB.AppendLine(Space(tab.XXXX) & "else")
        strB.AppendLine(Space(tab.XXXXX) & "comm.Connection = new SqlConnection(ReadOnlyConnectionString);")
        strB.AppendLine(Space(tab.XXXX) & "")
        strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = System.Data.CommandType.StoredProcedure;")
        strB.AppendLine(Space(tab.XXX) & "}catch(Exception ex){}")
        strB.AppendLine(Space(tab.XX) & "}")
        strB.AppendLine(Space(tab.XX) & "public static SqlDataReader GetDataReader(SqlCommand comm)")
        strB.AppendLine(Space(tab.XX) & "{")
        strB.AppendLine(Space(tab.XXX) & "try")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "ConnectToDatabase(comm);")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Open();")
        strB.AppendLine(Space(tab.XXXX) & "return comm.ExecuteReader();")
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.Message);")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.StackTrace);")
        strB.AppendLine(Space(tab.XXXX) & "return null;")
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XX) & "}")

        strB.AppendLine(Space(tab.XX))
        strB.AppendLine(Space(tab.XX))

        strB.AppendLine(Space(tab.XX) & "internal static int AddObject(SqlCommand comm, string parameterName)")
        strB.AppendLine(Space(tab.XX) & "{")
        strB.AppendLine(Space(tab.XXX) & "int retInt = 0;")
        strB.AppendLine(Space(tab.XXX) & "try")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection = new SqlConnection(EditOnlyConnectionString);")
        strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = System.Data.CommandType.StoredProcedure;")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Open();")
        strB.AppendLine(Space(tab.XXXX) & "SqlParameter retParameter;")
        strB.AppendLine(Space(tab.XXXX) & "retParameter = comm.Parameters.Add(parameterName, System.Data.SqlDbType.Int);")
        strB.AppendLine(Space(tab.XXXX) & "retParameter.Direction = System.Data.ParameterDirection.Output;")
        strB.AppendLine(Space(tab.XXXX) & "comm.ExecuteNonQuery();")
        strB.AppendLine(Space(tab.XXXX) & "retInt = (int)retParameter.Value;")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close();")
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "if (comm.Connection != null)")
        strB.AppendLine(Space(tab.XXXXX) & "comm.Connection.Close();")
        strB.AppendLine(Space(tab.XXXX))
        strB.AppendLine(Space(tab.XXXX) & "retInt = -1;")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.Message);")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.StackTrace);")
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XXX) & "return retInt;")
        strB.AppendLine(Space(tab.XX) & "}")

        strB.AppendLine(Space(tab.XX))
        strB.AppendLine(Space(tab.XX))


        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.CSharp, True) & _
                       " <summary>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.CSharp, True) & _
                        " Sets Connection and Executes given comm on the database")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.CSharp, True) & _
                        " </summary>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.CSharp, True) & _
                        " <param name=""comm"">SQLCommand to execute</param>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.CSharp, True) & _
                        " <returns>number of rows affected; -1 on failure, positive value on success.</returns>")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.CSharp, True) & _
                        " <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.")
        strB.AppendLine(Space(tab.XX) & cg.getCommentString(cg.Language.CSharp, True) & _
                        "       Edit Connection is set here.</remarks>")

        strB.AppendLine(Space(tab.XX) & "internal static int UpdateObject(SqlCommand comm)")
        strB.AppendLine(Space(tab.XX) & "{")
        strB.AppendLine(Space(tab.XXX) & "int retInt = 0;")
        strB.AppendLine(Space(tab.XXX) & "try")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection = new SqlConnection(EditOnlyConnectionString);")
        strB.AppendLine(Space(tab.XXXX) & "comm.CommandType = System.Data.CommandType.StoredProcedure;")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Open();")
        strB.AppendLine(Space(tab.XXXX) & "retInt = comm.ExecuteNonQuery();")
        strB.AppendLine(Space(tab.XXXX) & "comm.Connection.Close();")
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XXX) & "catch(Exception ex)")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "if(comm.Connection != null)")
        strB.AppendLine(Space(tab.XXXXX) & "comm.Connection.Close();")
        strB.AppendLine(Space(tab.XXXX))
        strB.AppendLine(Space(tab.XXXX) & "retInt = -1;")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.Message);")
        strB.AppendLine(Space(tab.XXXX) & "System.Diagnostics.Debug.WriteLine(ex.StackTrace);")
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XXX) & "return retInt;")
        strB.AppendLine(Space(tab.XX) & "}")
        strB.AppendLine(cg.getRegionEnd(CodeGeneration.Language.CSharp))
        Return strB.ToString()
    End Function

End Class

Option Strict On

Imports EnvDTE
Imports System.Collections.Generic
Imports IRICommonObjects.Words
Imports System.Text
Imports cg = CodeGeneratorAddIn.CodeGeneration
Imports language = CodeGeneratorAddIn.CodeGeneration.Language
Imports tab = CodeGeneratorAddIn.CodeGeneration.Tabs

Public Class MVC_ViewModelGenerator
    Private Function getIndexGET(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then

        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.None) & "namespace CodeGeneratorAddin.ViewModels")
            strB.AppendLine(Space(tab.None) & "{")
            strB.AppendLine(Space(tab.X) & "public Class SpecimenViewModel")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "private " & pClass.Name.Capitalized & " _spec;")
            strB.AppendLine(Space(tab.None))
            strB.AppendLine(Space(tab.XX) & "public " & pClass.Name.Capitalized & " Obj { get { return _spec; } }")
            strB.AppendLine(Space(tab.XX) & "private Collection _Collection;")
            strB.AppendLine(Space(tab.XX) & "private Person _Scanner;")
            strB.AppendLine(Space(tab.None))
            strB.AppendLine(Space(tab.XX) & "public Person Scanner")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "get { return _Scanner; }")
            strB.AppendLine(Space(tab.XXX) & "set")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (value == null) value = Person.CreateEmpty();")
            strB.AppendLine(Space(tab.XXXX) & "_Scanner = value; _spec.ScannedByID = value.ID;")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "public Collection Collection")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "get { return _Collection; }")
            strB.AppendLine(Space(tab.XXX) & "set")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (value == null) value = Collection.CreateEmpty();")
            strB.AppendLine(Space(tab.XXXX) & "_Collection = value; _spec.CollectionID = value.ID;")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.None) & "")
            strB.AppendLine(Space(tab.XX) & "public SpecimenViewModel(Specimen sp)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "_Spec = sp;")
            strB.AppendLine(Space(tab.XXX) & "Collection = IVLDAL.GetCollection(sp.CollectionID);")
            strB.AppendLine(Space(tab.XXX) & "Scanner = IVLDAL.GetPerson(sp.ScannedByID);")
            strB.AppendLine(Space(tab.None) & "")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.X) & "}")
            strB.AppendLine(Space(tab.None) & "}")
        End If
        Return strB.ToString()
    End Function
End Class

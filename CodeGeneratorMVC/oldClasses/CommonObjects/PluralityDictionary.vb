Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Words
	Public Class PluralityDictionary
        Private Shared _ListOfExceptions As Dictionary(Of String, String)
        ''' <summary>
        ''' get the plural version of text.
        ''' </summary>
        ''' <param name="str"></param>
        ''' <returns>return the lowercase plural form of the given string.</returns>
        ''' <remarks>The string will always be converted to lowercase.</remarks>
        Public Shared Function getPlurality(ByVal str As String) As String
            '' We assume the string will aways come formatted, no carets or other formating symbols.
            '' therefore we need to keep track of the places that are capitalized.
            Dim listOCaps As New List(Of Integer)
            Dim lastCharWasCAPS As Boolean = False
            Dim chrInd As Integer = 0
            For Each ch As Char In str
                If Char.IsUpper(ch) Then
                    listOCaps.Add(chrInd)
                    lastCharWasCAPS = chrInd = str.Length - 1
                End If
                chrInd += 1
            Next
            'str = str.ToLower
            Dim lowStr As String
            'force string to lower cas
            lowStr = str.ToLower
            'see if contains breaking spaces
            Dim wBS As Boolean = lowStr.Contains("&nbsp;")
            Dim spStr(1) As String

            ' if contains breaking spaces then set chars to split on appropriately
            If wBS Then
                spStr(0) = "&nbsp;"
            Else
                spStr(0) = " "
            End If

            'get results from split
            Dim wordArr() As String
            wordArr = lowStr.Split(spStr, System.StringSplitOptions.RemoveEmptyEntries)

            'set word to check plurality on as last word in the list
            ' make no sence to do "twenties apples" ; should be "twenty apples".
            lowStr = wordArr(wordArr.Count - 1)
            Dim newString As String = ""
            Dim lastWordIndex As Integer = wordArr.Count - 1
            For i As Integer = 0 To lastWordIndex
                If i = lastWordIndex Then
                    'see if is in exeptions list
                    If ListOfExceptions.ContainsKey(lowStr) Then
                        'if so, get the exeption version
                        newString &= ListOfExceptions(lowStr)
                    Else
                        'if not, get the plural version
                        newString &= getTextPluralized(lowStr)
                    End If
                Else
                    newString &= wordArr(i) & spStr(0)
                End If
            Next

            '' convert capital letters back to from the original string
            chrInd = 0
            Dim retStr As String = ""
            For Each ch As Char In newString
                If listOCaps.Contains(chrInd) _
                    OrElse (lastCharWasCAPS AndAlso chrInd > listOCaps.Max()) Then
                    retStr &= Char.ToUpper(ch)
                Else
                    retStr &= ch
                End If
                chrInd += 1
            Next
            Return retStr
        End Function
        ''' <summary>
        ''' get the singular version of a plural text.
        ''' </summary>
        ''' <param name="str"></param>
        ''' <returns>return the lowercase plural form of the given string.</returns>
        ''' <remarks>The string will always be converted to lowercase.</remarks>
        Public Shared Function getPluralityInverse(ByVal str As String) As String
            'str = str.ToLower
            Dim lowStr As String
            'force string to lower cas
            lowStr = str.ToLower
            'see if contains breaking spaces
            Dim wBS As Boolean = lowStr.Contains("&nbsp;")
            Dim spStr As New List(Of String)

            ' if contains breaking spaces then set chars to split on appropriately
            If wBS Then
                spStr.Add("&nbsp;")
            Else
                spStr.Add(" ")
            End If

            'get results from split
            Dim stArr() As String
            stArr = lowStr.Split(spStr.ToArray, System.StringSplitOptions.RemoveEmptyEntries)

            'set word to check plurality on as last word in the list
            ' make no sence to do "twenties apples" ; should be "twenty apples".
            lowStr = stArr(stArr.Count - 1)
            Dim retStr As String = ""
            For i As Integer = 0 To stArr.Count - 1
                If i = stArr.Count - 1 Then
                    'see if is in exeptions list
                    Dim exceptionExists As Boolean = False
                    For Each pair As KeyValuePair(Of String, String) In ListOfExceptions
                        If pair.Value = lowStr Then
                            retStr &= pair.Key
                            exceptionExists = True
                        End If
                    Next
                    If exceptionExists Then Continue For
                    'if not, get the plural version
                    retStr &= getTextPluralizedInverse(lowStr)
                Else
                    retStr &= stArr(i) & " "
                End If
            Next
            Return retStr
        End Function
        ''' <summary>
        ''' pluralize text based on basic english rules
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function getTextPluralized(ByVal name As String) As String
            If name.Length > 2 Then
                Dim lastChar As Char
                lastChar = CChar(name.Substring(name.Length - 1))
                Select Case lastChar
                    Case Is = "y"c
                        If name.Substring(name.Length - 2) = "ey" Then
                            'Return Format(name) & "s"
                            Exit Select
                        End If
                        Return Format(name.Substring(0, name.Length - 1) & "ies")
                    Case Is = "s"c
                        If name.Substring(name.Length - 2) = "ss" Then
                            name &= "e"
                        End If
                    Case Is = "h"c
                        If name.Substring(name.Length - 2) = "ch" Or name.Substring(name.Length - 2) = "sh" Then
                            name &= "e"
                        End If
                    Case Is = "x"c
                        name &= "e"
                    Case Else
                End Select
            End If
            Return Format(name) & "s"
        End Function
        ''' <summary>
        ''' pluralize text based on basic english rules
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function getTextPluralizedInverse(ByVal name As String) As String

            If name.Length > 2 Then
                If name(name.Length - 1) = "s" Then
                    If name.Length > 4 AndAlso name.Substring(name.Length - 3) = "ies" Then
                        Dim retString As String = name.Substring(0, name.Length - 3)
                        Return retString.Insert(retString.Length, "y")
                    ElseIf name.Length > 5 AndAlso name.Substring(name.Length - 4) = "sses" Then
                        Return name.Substring(0, name.Length - 2)
                    ElseIf name.Length > 5 AndAlso name.Substring(name.Length - 4) = "ches" Then
                        Return name.Substring(0, name.Length - 2)
                    ElseIf name.Length > 5 AndAlso name.Substring(name.Length - 4) = "shes" Then
                        Return name.Substring(0, name.Length - 2)
                    ElseIf name.Length > 4 AndAlso name.Substring(name.Length - 3) = "xes" Then
                        Return name.Substring(0, name.Length - 2)
                    Else
                        Return name.Substring(0, name.Length - 1)
                    End If

                End If
            End If
            Return name
        End Function
        ''' <summary>
        ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property ListOfExceptions() As Dictionary(Of String, String)
            Get
                If _ListOfExceptions Is Nothing Then _ListOfExceptions = getPluralityExceptions()
                Return _ListOfExceptions
            End Get
        End Property
        ''' <summary>
        ''' A list of exceptions to basic rules of plurality in English.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ''' <remarks></remarks>
        Private Shared Function getPluralityExceptions() As Dictionary(Of String, String)
            Dim retDict As New Dictionary(Of String, String)
            retDict.Add("addendum", "addenda")
            retDict.Add("alga", "algae")
            retDict.Add("alumna", "alumnae")
            retDict.Add("alumnus", "alumni")
            retDict.Add("analysis", "analyses")
            retDict.Add("antenna", "antennae")
            retDict.Add("apparatus", "apparatuses")
            retDict.Add("appendix", "appendices")
            retDict.Add("axis", "axes")
            retDict.Add("bacillus", "bacilli")
            retDict.Add("bacterium", "bacteria")
            retDict.Add("basis", "bases")
            retDict.Add("beau", "beaux")
            retDict.Add("bison", "bison")
            retDict.Add("buffalo", "buffalos")
            retDict.Add("bureau", "bureaus")
            retDict.Add("bus", "buses")
            retDict.Add("cactus", "cacti")
            retDict.Add("calf", "calves")
            retDict.Add("child", "children")
            retDict.Add("corps", "corps")
            retDict.Add("corpus", "corpuses")
            retDict.Add("crisis", "crises")
            retDict.Add("criterion", "criteria")
            retDict.Add("curriculum", "curricula")
            retDict.Add("datum", "Data")
            retDict.Add("deer", "deer")
            retDict.Add("die", "dice")
            retDict.Add("dwarf", "dwarves")
            retDict.Add("diagnosis", "diagnoses")
            retDict.Add("echo", "echoes")
            retDict.Add("elf", "elves")
            retDict.Add("ellipsis", "ellipses")
            retDict.Add("embargo", "embargoes")
            retDict.Add("emphasis", "emphases")
            retDict.Add("erratum", "errata")
            retDict.Add("fireman", "firemen")
            retDict.Add("fish", "fishes")
            retDict.Add("focus", "focuses")
            retDict.Add("foot", "feet")
            retDict.Add("formula", "formulas")
            retDict.Add("fungus", "fungi")
            retDict.Add("genus", "genera")
            retDict.Add("goose", "geese")
            retDict.Add("half", "halves")
            retDict.Add("hero", "heroes")
            retDict.Add("hippopotamus", "hippopotami")
            retDict.Add("hoof", "hooves")
            retDict.Add("hypothesis", "hypotheses")
            retDict.Add("index", "indices")
            retDict.Add("knife", "knives")
            retDict.Add("leaf", "leaves")
            retDict.Add("life", "lives")
            retDict.Add("loaf", "loaves")
            retDict.Add("louse", "lice")
            retDict.Add("man", "men")
            retDict.Add("matrix", "matrices")
            retDict.Add("means", "means")
            retDict.Add("medium", "media")
            retDict.Add("media", "media")
            retDict.Add("memorandum", "memoranda")
            retDict.Add("millennium", "milennia")
            retDict.Add("moose", "moose")
            retDict.Add("mosquito", "mosquitoes")
            retDict.Add("mouse", "mice")
            retDict.Add("nebula", "nebulae")
            retDict.Add("neurosis", "neuroses")
            retDict.Add("nucleus", "nuclei")
            retDict.Add("oasis", "oases")
            retDict.Add("octopus", "octopi")
            retDict.Add("ovum", "ova")
            retDict.Add("ox", "oxen")
            retDict.Add("paralysis", "paralyses")
            retDict.Add("parenthesis", "parentheses")
            retDict.Add("person", "people")
            retDict.Add("phenomenon", "phenomena")
            retDict.Add("potato", "potatoes")
            retDict.Add("radius", "radii")
            retDict.Add("scarf", "scarves")
            retDict.Add("self", "selves")
            retDict.Add("series", "series")
            retDict.Add("sheep", "sheep")
            retDict.Add("shelf", "shelves")
            retDict.Add("scissors", "scissors")
            retDict.Add("software", "software")
            retDict.Add("species", "species")
            retDict.Add("stimulus", "stimuli")
            retDict.Add("stratum", "strata")
            retDict.Add("syllabus", "syllabi")
            retDict.Add("symposium", "symposia")
            retDict.Add("synthesis", "syntheses")
            retDict.Add("synopsis", "synopses")
            retDict.Add("tableau", "tableaux")
            retDict.Add("that", "those")
            retDict.Add("thesis", "theses")
            retDict.Add("thief", "thieves")
            retDict.Add("this", "these")
            retDict.Add("tomato", "tomatoes")
            retDict.Add("tooth", "teeth")
            retDict.Add("torpedo", "torpedoes")
            retDict.Add("vertebra", "vertebrae")
            retDict.Add("veto", "vetoes")
            retDict.Add("vita", "vitae")
            retDict.Add("watch", "watches")
            retDict.Add("wife", "wives")
            retDict.Add("wolf", "wolves")
            retDict.Add("woman", "women")
            retDict.Add("zero", "zeros")
            retDict.Add("status", "statuses")
            retDict.Add("patch", "patches")
            Return retDict
        End Function
	End Class
End Namespace

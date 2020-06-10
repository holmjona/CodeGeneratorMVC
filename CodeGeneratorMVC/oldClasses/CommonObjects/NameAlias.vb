Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Words
	Public Class NameAlias
#Region "Private Variables"
		Private _Alias As String
        Private _Vowels() As Char = {"a"c, "e"c, "i"c, "o"c, "u"c}
        Private _SoftWords() As String = {"hour", "honest", "honor"}
        Private _HardWords() As String = {"user", "unicorn", "uniform", "u.", "one", "union"}

        ' Keep local copies of text to save on processor time.
        Private _Text As String = Nothing
        Private _TextPlural As String = Nothing
        Private _TextPluralAndCapital As String = Nothing
        Private _TextCapital As String = Nothing

        Private _JText As String = Nothing
        Private _JTextPlural As String = Nothing
        Private _JTextPluralAndCapital As String = Nothing
        Private _JTextCapital As String = Nothing

#End Region
#Region "Public Properties"
		''' <summary>
		''' Get Text as it is stored in the Database.
		''' </summary>
		''' <value></value>
		''' <returns>Unformatted text exactly as it is stored in the database.</returns>
		''' <remarks></remarks>
		Public Property TextUnFormatted() As String
			Get
				Return _Alias
			End Get
			Set(ByVal value As String)
                _Alias = value.ToLower.Trim
                clearLocalVars()
			End Set
        End Property
        Private Sub clearLocalVars()
            _Text = Nothing
            _TextPlural = Nothing
            _TextPluralAndCapital = Nothing
            _TextCapital = Nothing

            _JText = Nothing
            _JTextPlural = Nothing
            _JTextPluralAndCapital = Nothing
            _JTextCapital = Nothing
        End Sub
		''' <summary>
		''' The singular lowercase text of the Alias with formatting applied.
		''' </summary>
		''' <value></value>
		''' <returns>The alias from the database with formatting applied.</returns>
		''' <remarks>This will apply basic formatting rules applied to what is exactly stored in the database.</remarks>
		Public ReadOnly Property Text(ByVal withJuncture_A As Boolean) As String
            Get
                If _Text Is Nothing Then
                    _Text = format(_Alias, False)
                    _JText = format(_Alias, True)
                End If
                If withJuncture_A Then
                    Return _JText
                Else
                    Return _Text
                End If
            End Get
		End Property
		Public ReadOnly Property Text() As String
			Get
                If _Text Is Nothing Then
                    _Text = format(_Alias, False)
                    _JText = format(_Alias, True)
                End If
                Return _Text
			End Get
		End Property
		''' <summary>
		''' The singular lowercase text of the alias formated and non breaking spaces removed.
		''' </summary>
		''' <value></value>
		''' <returns>The alias from the database with basic formatting applied and non breaking spaces removed.</returns>
		''' <remarks>This is intended for HTML representation.</remarks>
		Public ReadOnly Property WithNonBreakingSpaces(ByVal withJuncture_A As Boolean) As String
			Get
                Return WithNonBreakingSpaces(_Alias, withJuncture_A)
			End Get
		End Property
		Public ReadOnly Property WithNonBreakingSpaces() As String
			Get
                Return WithNonBreakingSpaces(_Alias)
			End Get
		End Property

		''' <summary>
		''' The given text will be returned with non breaking spaces removed.
		''' </summary>
		''' <param name="strToFormat">The string to remove non-breaking spaces from.</param>
		''' <returns>The text given with non breaking spaces removed.</returns>
		''' <remarks>This is intended for HTML representation.</remarks>
		Public ReadOnly Property WithNonBreakingSpaces(ByVal strToFormat As String, ByVal withJuncture_A As Boolean) As String
			Get
				Return format(strToFormat.Replace(" ", "&nbsp;"), withJuncture_A)
			End Get
		End Property
		Public ReadOnly Property WithNonBreakingSpaces(ByVal strToFormat As String) As String
			Get
				Return format(strToFormat.Replace(" ", "&nbsp;"), False)
			End Get
		End Property
		''' <summary>
		''' The Text with the first letter of each word Capitalized.
		''' </summary>
		''' <returns>The alias from the database with the first letter of each word capitalized.</returns>
		''' <remarks></remarks>
		Public ReadOnly Property Capitalized(ByVal withJuncture_A As Boolean) As String
            Get
                If _TextCapital Is Nothing Then
                    _TextCapital = format(_Alias, False)
                    _JText = format(_Alias, True)
                End If
                Return getTextCapitalized(_Alias, withJuncture_A)
            End Get
		End Property
		Public ReadOnly Property Capitalized() As String
			Get
				Return getTextCapitalized(_Alias, False)
			End Get
		End Property
		''' <summary>
		''' Capitalizes and removes any "nbsp;" from the alias name.
		''' </summary>
		''' <value></value>
		''' <returns>A string which has the first leter capitalized and does not contain non-breaking spaces (nbsp;) where spaces exist.</returns>
		''' <remarks></remarks>
		Public ReadOnly Property CapitalizedWithNonBreakingSpaces(ByVal withJuncture_A As Boolean) As String
			Get
				Return getTextCapitalized(WithNonBreakingSpaces(withJuncture_A))
			End Get
		End Property
		Public ReadOnly Property CapitalizedWithNonBreakingSpaces() As String
			Get
				Return getTextCapitalized(WithNonBreakingSpaces(False))
			End Get
		End Property
		''' <summary>
		''' Pluralizes and capitalizes the name of the alias. 
		''' </summary>
		''' <value></value>
		''' <returns>A string which has the first letter capitalized and the appropriate characters to make it plural.</returns>
		''' <remarks></remarks>
		Public ReadOnly Property PluralAndCapitalized() As String
			Get
				Return getTextCapitalized(Plural)
			End Get
		End Property
        ''' <summary>
        ''' Pluralizes the name of the alias.
        ''' </summary>
        ''' <value></value>
        ''' <returns>A string containing the name of the alias with the appropriate characters added on to make it plural.</returns>
        ''' <remarks></remarks>
		Public ReadOnly Property Plural() As String
			'Plurals never need Junctures; it makes no sence to say "an apples" or "a pears"
			Get
				Return PluralityDictionary.getPlurality(format(_Alias))
			End Get
		End Property
        ''' <summary>
        ''' Pluralizes the name of the alias and removes any non-breaking spaces from the alias name. 
        ''' </summary>
        ''' <value></value>
        ''' <returns>A string which has the appropriate characters to make the name plural
        ''' and does not contain "nbsp;" where a space is found.
        ''' </returns>
        ''' <remarks></remarks>
		Public ReadOnly Property PluralWithNonBreakingSpaces() As String
			Get
				Return PluralityDictionary.getPlurality(format(WithNonBreakingSpaces))
			End Get
		End Property
        ''' <summary>
        ''' Capitalizes, pluralizes and removes all non breaking spaces from the name of the alias.
        ''' </summary>
        ''' <value></value>
        ''' <returns>A string that is capitalized, plurized and contains no non-breaking spaces from the name of the alias.</returns>
        ''' <remarks></remarks>
		Public ReadOnly Property PluralAndCapitalizedWithNonBreakingSpaces() As String
			Get
				Return getTextCapitalized(format(PluralWithNonBreakingSpaces))
			End Get
		End Property
		Public ReadOnly Property PastTense() As String
			' verbs do not need Junctures; it would not make sence to say "I had a watched the game."
			Get
				Return VerbDictionary.getPastTense(Text)
			End Get
		End Property
		Public ReadOnly Property PastTenseAndCapitalized() As String
			Get
				Return getTextCapitalized(PastTense)
			End Get
		End Property
		Public ReadOnly Property Gerund(ByVal withJuncture_A As Boolean) As String
			' Gerunds do need Junctions "I went to a swimming meet."
			Get
				Return VerbDictionary.getGerund(Text(withJuncture_A))
			End Get
		End Property
		Public ReadOnly Property Gerund() As String
			' Gerunds do need Junctions "I went to a swimming meet."
			Get
				Return VerbDictionary.getGerund(Text(False))
			End Get
		End Property
		Public ReadOnly Property GerundAndCapitalized(ByVal withJuncture_A As Boolean) As String
			Get
				Return getTextCapitalized(Gerund(withJuncture_A))
			End Get
		End Property
		Public ReadOnly Property GerundAndCapitalized() As String
			Get
				Return getTextCapitalized(Gerund(False))
			End Get
		End Property
#End Region
#Region "Private Functions"
		''' <summary>
		''' Add the Juncture or "a" or "an" to the start of the alias.
		''' For example "an apple" or "a pear".
		''' </summary>
		''' <param name="text"></param>
		''' <returns></returns>
		''' <remarks></remarks>
        Private Function getWithJunctureA(ByVal text As String, ByVal withNonBreakingSpaces As Boolean) As String
            Dim textToCompare As String = text.ToLower
            Dim juncText As String = "a"
            Dim spaceText As String = " "
            If withNonBreakingSpaces Then spaceText = "&nbsp;"
            If text.Contains("^"c) Then textToCompare = text.Replace("^", "")
            'may need to create a dictionary for handling exceptions like hotel, one-armed, etc.
            Dim firstChar As Char ', secondChar, thirdChar As Char
            firstChar = textToCompare.Chars(0)
            If _Vowels.Contains(firstChar) Then
                Dim isHard As Boolean = False
                For Each hw As String In _HardWords
                    If textToCompare.StartsWith(hw) Then
                        isHard = True
                        Exit For
                    End If
                Next
                If Not isHard Then
                    juncText = "an"
                End If
            Else
                For Each sw As String In _SoftWords
                    If textToCompare.StartsWith(sw) Then
                        juncText = "an"
                        Exit For
                    End If
                Next
            End If
            Return juncText & spaceText & text
        End Function
		Private Function getWithJunctureA(ByVal text As String) As String
            Return getWithJunctureA(text, False)
		End Function
		''' <summary>
		''' Capitalizes the string passed to it.
		''' </summary>
		''' <param name="name"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function getTextCapitalized(ByVal name As String, ByVal withJuncture_A As Boolean) As String
			Dim myArr As Array
			Dim wBS As Boolean = name.Contains("&nbsp;")
			If withJuncture_A Then name = getWithJunctureA(name, wBS)
			If wBS Then
				Dim splitter() As String = {"&nbsp;"}
				myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries)
			Else
				Dim splitter() As Char = {" "c}
				myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries)
			End If
			name = ""
			For Each s As String In myArr
				If s.Length > 1 Then
					name &= s.Substring(0, 1).ToUpper & s.Substring(1)
				Else
					name &= s.ToUpper
				End If
				If wBS Then
					name &= "&nbsp;"
				Else
					name &= " "
				End If
			Next
			If name.Length > 5 AndAlso name.Substring(name.Length - 6) = "&nbsp;" Then
				name = name.Substring(0, name.Length - 6)
			End If
			Return format(name.Trim)
		End Function
		Private Function getTextCapitalized(ByVal name As String) As String
			Dim myArr As Array
			Dim wBS As Boolean = name.Contains("&nbsp;")
			If wBS Then
				Dim splitter() As String = {"&nbsp;"}
				myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries)
			Else
				Dim splitter() As Char = {" "c}
				myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries)
			End If
			name = ""
			For Each s As String In myArr
				If s.Length > 1 Then
					name &= s.Substring(0, 1).ToUpper & s.Substring(1)
				Else
					name &= s.ToUpper
				End If
				If wBS Then
					name &= "&nbsp;"
				Else
					name &= " "
				End If
			Next
			If name.Length > 5 AndAlso name.Substring(name.Length - 6) = "&nbsp;" Then
				name = name.Substring(0, name.Length - 6)
			End If
			Return format(name.Trim)
		End Function
		''' <summary>
		''' Capitalizes the next letter in the name of the alias if a carrot("^") is in front of it.
		''' </summary>
		''' <param name="retString"></param>
		''' <returns>A string which capitalizes letters if there is a "^" character in front of it.</returns>
		''' <remarks></remarks>
		Private Function format(ByVal retString As String, ByVal withJuncture_A As Boolean) As String
			If withJuncture_A Then retString = getWithJunctureA(retString)
			If retString.Contains("^") Then
				Dim carrotLocation As Integer
				carrotLocation = retString.IndexOf("^"c)
				If carrotLocation < retString.Length - 1 Then
					retString = retString.Substring(0, carrotLocation) & retString.Substring(carrotLocation + 1, 1).ToUpper _
					   & retString.Substring(carrotLocation + 2)
					Return format(retString)
				Else
					Return retString.Substring(0, retString.Length - 1)
				End If
			Else
				Return retString
			End If
		End Function
        Private Function format(ByVal retString As String) As String
            If retString.Contains("^") Then
                Dim carrotLocation As Integer
                carrotLocation = retString.IndexOf("^"c)
                If carrotLocation < retString.Length - 1 Then
                    retString = retString.Substring(0, carrotLocation) & retString.Substring(carrotLocation + 1, 1).ToUpper _
                       & retString.Substring(carrotLocation + 2)
                    Return format(retString)
                Else
                    Return retString.Substring(0, retString.Length - 1)
                End If
            Else
                Return retString
            End If
        End Function

#End Region
        Public Overrides Function ToString() As String
            Return Capitalized
        End Function
		''' <summary>
		''' Sets the default text if to "[insert_alias]" if not passed any string variable.
		''' </summary>
		''' <param name="startingText">Optional string that can be the default for unspecified aliases.</param>
		''' <remarks></remarks>
		Public Sub New(ByVal startingText As String)
            setText(startingText)
        End Sub
        Private Sub setText(myText As String)
            If myText = "" Then myText = "[insert_alias]"
            _Alias = myText
        End Sub
        ''' <summary>
        ''' Sets the default text if to "[insert_alias]" if not passed any string variable.
        ''' </summary>
        ''' <param name="startingText">Optional string that can be the default for unspecified aliases.</param>
        ''' <param name="applyFormatting">True if text needs formatting applied from string</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal startingText As String, applyFormatting As Boolean)
            If applyFormatting Then
                setText(getTextWithFormatting(startingText))
            Else
                setText(startingText)
            End If
            
        End Sub
        Public Shared Function getTextWithFormatting(myText As String) As String
            Dim newString As String = ""
            For Each myChar As Char In myText.ToCharArray()
                If Char.IsUpper(myChar) Then
                    newString &= "^"
                End If
                newString &= myChar
            Next
            Return newString
        End Function
		Public Sub New()
			_Alias = "[insert_alias]"
		End Sub
	End Class
End Namespace

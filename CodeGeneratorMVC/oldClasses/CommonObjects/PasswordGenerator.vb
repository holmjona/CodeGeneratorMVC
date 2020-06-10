Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text

Namespace Tools
	''' <summary>
	''' Generates Passwords. Default Level is strong. Default Length is 8.
	''' </summary>
	''' <remarks>Default Level is strong and Length is 8.</remarks>
	Public Class PasswordGenerator
		Private passwordLength As Integer = 8
		Private dict As Dictionary(Of Char, String) = getDict()
		Const lowerLetters As String = "abcdefghijkmnopqrstuvwxyz"
		Const upperLetters As String = "ABCDEFGHJKLMNPQRSTUVWXYZ"
		Const numberChars As String = "23456789"
		Const specialChars As String = "!@#$%^&*()_=+-/"
		''' <summary>
		''' Creates object and calls Randomize().
		''' </summary>
		''' <remarks>The Randomize call is to make sure that new passwords are created, making them more difficult to guess.</remarks>
		Public Sub New()
			MyBase.New()
			Randomize(Now.TimeOfDay.TotalSeconds)
		End Sub
		''' <summary>
		''' The complexity of a password.
		''' </summary>
		''' <remarks>
		''' Weak uses only lowercase letters.
		''' Semi uses upper and lower case letters.
		''' Strong uses upper and lower case letters and numbers.
		''' Very Strong uses upper and lower case letters and numbers and special characters.
		''' </remarks>
		Enum passwordComplexity As Integer
			''' <summary>
			''' use just lowercase letters
			''' </summary>
			''' <remarks></remarks>
			Level1 = 1
			''' <summary>
			''' use lowercase and UPPERCASE letters
			''' </summary>
			''' <remarks></remarks>
			Level2 = 2
			''' <summary>
			''' use lowercase, UPPERCASE, and numbers
			''' </summary>
			''' <remarks></remarks>
			Level3 = 3
			''' <summary>
			''' use lowercase, UPPERCASE, numbers, and special characters
			''' </summary>
			''' <remarks></remarks>
			Level4 = 4
		End Enum
		''' <summary>
		''' The level of complexity of a password.
		''' </summary>
		Enum passwordStrength
			Weak = 1
			Semi = 2
			Strong = 3
			VeryStrong = 4
		End Enum
		''' <summary>
		''' Get a Random number between 1 and the given number.
		''' </summary>
		''' <param name="Sides">The max random value to use.</param>
		''' <returns>A random integer between 1 and the given number of Sides.</returns>
		''' <remarks>Acts like a Sides die, you specify the number of Sides and it will return a random roll from that die.</remarks>
		Private Function rollDie(ByVal Sides As Integer) As Integer
			Return CInt(Math.Floor(Rnd() * Sides)) + 1
		End Function
		''' <summary>
		''' Generate Strong Password with 8 characters.
		''' </summary>
		''' <returns>Password string with upper and lower case letters and numbers. </returns>
		Public Function generatePassword() As String
			Return generatePassword(passwordLength, passwordComplexity.Level3)
		End Function
		''' <summary>
		''' Generate Password at the desired strength and 8 characters.
		''' </summary>
		''' <param name="strength">The strength of the password to generate.</param>
		''' <returns>Password String that with 8 characters of the strength you specified.</returns>
		Public Function generatePassword(ByVal strength As passwordComplexity) As String
			Return generatePassword(passwordLength, strength)
		End Function
		''' <summary>
		''' Generate Strong Password with the number of characters equalling the given amount.
		''' </summary>
		''' <param name="length">Integer length of the number of characters in the password.</param>
		''' <returns>Password string at length specified using upper and lower case letters and number.</returns>
		Public Function generatePassword(ByVal length As Integer) As String
			Return generatePassword(length, passwordComplexity.Level3)
		End Function
		''' <summary>
		''' Generate Password at desired strength and desired number of characters.
		''' </summary>
		''' <param name="length">Integer length of the password string.</param>
		''' <param name="complex">Password Strength that password needs to be.</param>
		''' <returns>Password String of given length and strength given.</returns>
		Public Function generatePassword(ByVal length As Integer, ByVal complex As passwordComplexity) As String
			Dim passArray As New System.Text.StringBuilder
			Dim newIndex As Integer
			For a As Integer = 1 To length
				newIndex = rollDie(100)
				Dim charType As Integer = rollDie(complex)
				Dim list As String
				Select Case charType
					Case Is = 1
						list = lowerLetters
					Case Is = 2
						list = upperLetters
					Case Is = 3
						list = numberChars
					Case Else
						list = specialChars
				End Select
				passArray.Append(getCharAtIndexWrapping(newIndex, list))
			Next
			If checkStrength(passArray.ToString, complex) Then
				Return passArray.ToString
			Else
				Return generatePassword(length, complex)
			End If
		End Function
		''' <summary>
		''' Get the character at the given index of a string.
		''' </summary>
		''' <param name="index">0 based index of the character desired.</param>
		''' <param name="list">String to retrieve character from.</param>
		''' <returns>Character at given index.</returns>
		Private Function getCharAtIndexWrapping(ByVal index As Integer, ByVal list As String) As Char
			If index > list.Length - 1 Then Return getCharAtIndexWrapping(index - list.Length, list)
			If index < 0 Then Return getCharAtIndexWrapping(index + list.Length, list)
			Return list.Chars(index)
		End Function
		''' <summary>
		''' Get if the password is equivelent to the desired strength.
		''' </summary>
		''' <param name="password"></param>
		''' <param name="strength"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function checkStrength(ByVal password As String, ByVal strength As passwordComplexity) As Boolean
			Return getPasswordComplexity(password) >= strength
		End Function
		''' <summary>
		''' Get the strength of a password.
		''' </summary>
		''' <param name="password">String representation of a password.</param>
		''' <returns>The Stength Enumerator relating to a given password.</returns>

		Public Function getPasswordStrength(ByVal password As String) As passwordStrength
			Dim stn As Integer
			stn = getPasswordComplexity(password)
			If password.Length < 8 Then stn -= (8 - password.Length)
			If stn < 1 Then stn = 1
			Return CType(stn, passwordStrength)
		End Function
		''' <summary>
		''' Returns the level of complexity a password passed to it is.
		''' </summary>
		''' <param name="password">The password to check.</param>
		Public Function getPasswordComplexity(ByVal password As String) As passwordComplexity
			Dim stG As Integer = 0
			If containsChar(password, lowerLetters) Then stG += 1
			If containsChar(password, upperLetters) Then stG += 1
			If containsChar(password, numberChars) Then stG += 1
			If containsChar(password, specialChars) Then stG += 1
			Return CType(stG, passwordComplexity)
		End Function
		''' <summary>
		''' Determines if any of the characters contained in the list are contained in the string.
		''' </summary>
		''' <param name="str">The string to check.</param>
		''' <param name="list">The list of strings to check for to see if they exist.</param>
		''' <returns>True if one of the characters contains a character in the list.</returns>
		Private Function containsChar(ByVal str As String, ByVal list As String) As Boolean
			For Each c As Char In str
				If list.Contains(c.ToString) Then Return True
			Next
			Return False
		End Function
		''' <summary>
		''' Creates a phonetic of the string passed to it using a dictionary that refers to each letter in alphabet.
		''' </summary>
		''' <param name="str">The string to make a phonetic of.</param>
		''' <returns>Sting of phonetics separated by a " - ". ex. [alpha - bravo - charley]</returns>
		Public Function getPhonetics(ByVal str As String) As String
			Dim retStr As New StringBuilder
			Dim count As Integer = 0
			Dim len As Integer = str.Length
			retStr.Append("[")
			For Each c As Char In str
				retStr.Append(dict(c))
				count += 1
				If count < len Then retStr.Append(" - ")
			Next
			retStr.Append("]")
			Return retStr.ToString
		End Function
		''' <summary>
		''' Creates a phonetic dictionary for each letter in alphabet as well as some numbers and special characters.
		''' </summary>
		Private Function getDict() As Dictionary(Of Char, String)
			Dim d As New Dictionary(Of Char, String)
			d.Add("a"c, "alpha")
			d.Add("b"c, "bravo")
			d.Add("c"c, "charley")
			d.Add("d"c, "delta")
			d.Add("e"c, "echo")
			d.Add("f"c, "foxtrot")
			d.Add("g"c, "golf")
			d.Add("h"c, "hotel")
			d.Add("i"c, "igloo")
			d.Add("j"c, "juliet")
			d.Add("k"c, "kilo")
			d.Add("l"c, "lima")
			d.Add("m"c, "mike")
			d.Add("n"c, "november")
			d.Add("o"c, "oscar")
			d.Add("p"c, "papa")
			d.Add("q"c, "quebec")
			d.Add("r"c, "romeo")
			d.Add("s"c, "sierra")
			d.Add("t"c, "tango")
			d.Add("u"c, "uniform")
			d.Add("v"c, "victor")
			d.Add("w"c, "whiskey")
			d.Add("x"c, "x~ray")
			d.Add("y"c, "yankee")
			d.Add("z"c, "zulu")
			d.Add("A"c, "ALPHA")
			d.Add("B"c, "BRAVO")
			d.Add("C"c, "CHARLEY")
			d.Add("D"c, "DELTA")
			d.Add("E"c, "ECHO")
			d.Add("F"c, "FOXTROT")
			d.Add("G"c, "GOLF")
			d.Add("H"c, "HOTEL")
			d.Add("I"c, "IGLOO")
			d.Add("J"c, "JULIET")
			d.Add("K"c, "KILO")
			d.Add("L"c, "LIMA")
			d.Add("M"c, "MIKE")
			d.Add("N"c, "NOVEMBER")
			d.Add("O"c, "OSCAR")
			d.Add("P"c, "PAPA")
			d.Add("Q"c, "QUEBEC")
			d.Add("R"c, "ROMEO")
			d.Add("S"c, "SIERRA")
			d.Add("T"c, "TANGO")
			d.Add("U"c, "UNIFORM")
			d.Add("V"c, "VICTOR")
			d.Add("W"c, "WHISKEY")
			d.Add("X"c, "X~RAY")
			d.Add("Y"c, "YANKEE")
			d.Add("Z"c, "ZULU")
			d.Add("1"c, "One")
			d.Add("2"c, "Two")
			d.Add("3"c, "Three")
			d.Add("4"c, "Four")
			d.Add("5"c, "Five")
			d.Add("6"c, "Six")
			d.Add("7"c, "Seven")
			d.Add("8"c, "Eight")
			d.Add("9"c, "Nine")
			d.Add("0"c, "Zero")
			d.Add("!"c, "{Exclamation}")
			d.Add("@"c, "{At}")
			d.Add("#"c, "{Pound}")
			d.Add("$"c, "{Dollar}")
			d.Add("%"c, "{Percent}")
			d.Add("^"c, "{Caret}")
			d.Add("&"c, "{Ampersand}")
			d.Add("*"c, "{Asterisk}")
			d.Add("("c, "{Left Parenthesis}")
			d.Add(")"c, "{Right Parenthesis}")
			d.Add("_"c, "{Underscore}")
			d.Add("="c, "{Equal}")
			d.Add("+"c, "{Plus}")
			d.Add("-"c, "{Hyphen}")
			d.Add("/"c, "{Slash}")
			d.Add("~"c, "{Tilde}")
			d.Add("{"c, "{Left Brace}")
			d.Add("}"c, "{Right Brace}")
			d.Add("["c, "{Left Box}")
			d.Add("]"c, "{Right Box}")
			d.Add("|"c, "{Pipe}")
			d.Add("."c, "{Period}")
			d.Add(","c, "{Comma}")
			d.Add("?"c, "{Question Mark}")
			d.Add(Chr(34), "{Quotation}")
			d.Add(Chr(39), "{Apostrophe}")
			d.Add("<"c, "{Greater Than}")
			d.Add(">"c, "{Less Than}")
			Return d
		End Function
	End Class
End Namespace



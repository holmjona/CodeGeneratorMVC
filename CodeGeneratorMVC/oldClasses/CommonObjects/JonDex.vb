
Imports Microsoft.VisualBasic
Namespace Tools.SoundEx
	''' <summary>
	''' A SoundEx Algorithm that I made to be able to handle some of our needs.
	''' #############################################################################
	''' #   This is basically the soundex algorithm, but I accounted
	''' #      for and handle plurality.
	''' # Ref: http://www.unixreview.com/documents/s=7458/uni1026336632258/0207e.htm
	''' #############################################################################
	''' </summary>
	''' <author>Jonathan D. Holmes</author>
	''' <orginialdate> 29 Aug 2006 </orginialdate>
	''' <remarks>Because I made this to handle plurality, which does not
	''' seem to be normal for SoundEx I named it JonDex.</remarks>
	Public Class JonDex
		''' <summary>
		''' Compare the similarity of two JonDex Strings. 
		''' Similar to Microsoft's DIFFERENCE function in SQL Server.
		''' </summary>
		''' <param name="str1">String to compare with the second</param>
		''' <param name="str2">String to compare with the first</param>
		''' <returns>Integer value of the similarity of two JonDex Strings.</returns>
		''' <remarks>The value returned is between 0 and 4. 4 means identical, 0 is 
		''' the most different.</remarks>
		Public Shared Function Difference(ByVal str1 As String, ByVal str2 As String, Optional ByVal compareFirstLetter As Boolean = True) As Integer
			' 
			Dim counter As Integer = 0
			Dim jDex1 As String = Dex(str1)
			Dim jDex2 As String = Dex(str2)
			For a As Integer = 0 To jDex1.Length - 1
				If a = 0 AndAlso compareFirstLetter Then
					'what if the first letters are comparable like c and k? this should handle that.
					If getCharKey(jDex1.Chars(0), "#"c) = getCharKey(jDex2(0), "#"c) Then _
					 counter += 1
				Else
					If jDex1.Chars(a) = jDex2.Chars(a) Then counter += 1 'chars match
				End If
			Next
			Return counter
		End Function
		''' <summary>
		''' Makes a SoundEx String that handles some if not most plurality. 
		''' </summary>
		''' <param name="str">String that JonDex is to derived from.</param>
		''' <returns>The SoundEx value for a String(JonDex)</returns>
		''' <remarks></remarks>
		Public Shared Function Dex(ByVal str As String) As String
			'#############################################################################
			'#   This is basically the soundex algorithm, but I accounted
			'#      for and handle plurality.
			'# Ref: http://www.unixreview.com/documents/s=7458/uni1026336632258/0207e.htm
			'#############################################################################
			If str.Length < 1 Then Return "0000"
			Dim strToReturn As String = ""
			str = str.ToLower
			Dim lastChar As Char = str.Chars(0)
			strToReturn = lastChar.ToString
			Do While strToReturn < "a" Or strToReturn > "z"
				If str.Length = 1 Then
					If strToReturn < "a" Or strToReturn > "z" Then
						Return "0000"
					Else
						Return str.Substring(0, 1) & "000"
					End If
				Else
					strToReturn = str.Substring(0, 1)
					str = str.Substring(1)
					lastChar = str.Chars(0)
				End If
			Loop

			If str.Chars(str.Length - 1) = "s"c Then
				str = str.Substring(0, str.Length - 1)
			End If
			For Each chStr As Char In str
				If strToReturn.Length < 4 Then
					Dim key As Integer = getCharKey(chStr, lastChar)
					Select Case key
						Case Is > 0
							strToReturn &= key.ToString
						Case Is = -2
							lastChar = chStr
						Case Else
					End Select
				Else
					Exit For
				End If
			Next
			If strToReturn.Length < 4 Then
				For a As Integer = strToReturn.Length To 3
					strToReturn &= "0"
				Next
			End If
			Return strToReturn
		End Function
		''' <summary>
		''' 
		''' </summary>
		''' <param name="chr"></param>
		''' <param name="lastchar"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Shared Function getCharKey(ByVal chr As Char, ByVal lastchar As Char) As Integer
			'TODO: does this Dex handle the 'sw' as in sword or answer?
			Select Case chr
				Case Is = lastchar 'ignore repeating letters, or is that leters
					Exit Select
				Case Is = "a"c, "e"c, "i"c, "o"c, "u"c, "y"c
					Exit Select
				Case Is = "b"c, "f"c, "p"c, "v"c
					Select Case lastchar
						Case Is = "b"c, "f"c, "p"c, "v"c
							Exit Select
						Case Else
							Return 1
					End Select
					Exit Select
				Case Is = "c"c, "g"c, "j"c, "k"c, "q"c, "s"c, "x"c, "z"c
					Select Case lastchar
						Case Is = "c"c, "g"c, "j"c, "k"c, "q"c, "s"c, "x"c, "z"c
							Exit Select
						Case Else
							Return 2
					End Select
					Exit Select
				Case Is = "d"c, "t"c
					Select Case lastchar
						Case Is = "d"c, "t"c
							Exit Select
						Case Else
							Return 3
					End Select
					Exit Select
				Case Is = "l"c
					Return 4
					Exit Select
				Case Is = "m"c, "n"c
					Select Case lastchar
						Case Is = "m"c, "n"c
							Exit Select
						Case Else
							Return 5
					End Select
					Exit Select
				Case Is = "r"c
					Return 6
					Exit Select
				Case Else

			End Select
			Select Case chr
				Case Is = "h"c, "w"c
					Exit Select
				Case Else
					'lastChar = chStr
					Return -2
			End Select
			Return -1
		End Function
	End Class
End Namespace

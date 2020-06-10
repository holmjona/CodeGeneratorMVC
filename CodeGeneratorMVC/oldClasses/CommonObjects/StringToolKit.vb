Option Strict On
Imports Microsoft.VisualBasic
Namespace Tools
	Public Class StringToolKit
		Private Sub New()

		End Sub
		Public Shared Function getDatabaseSuccessString(ByVal variableAlias As Words.NameAlias, ByVal verbAlias As Words.NameAlias) As String
			Return variableAlias.Capitalized(False) & " was successfully " & verbAlias.PastTense & "."
		End Function
		Public Shared Function getDatabaseErrorString(ByVal variableAlias As Words.NameAlias, ByVal verbAlias As Words.NameAlias) As String
			Return "Unable to " & verbAlias.Text(False) & " " & variableAlias.Text(False) & "."
		End Function
		Public Shared Function getDatabaseErrorAlreadyExistsString(ByVal variableAlias As Words.NameAlias, ByVal verbAlias As Words.NameAlias) As String
			Return getDatabaseErrorString(variableAlias, verbAlias) & _
			variableAlias.Capitalized(False) & " is already entered into the system. Duplicate Entry."
		End Function

		Public Shared Function ObjectNotFound(ByVal aliasString As Words.NameAlias) As String
			Return "Unable to retrieve " & aliasString.Text(False) & "."
		End Function
		'Public Shared Function getIPAddressFromString(ByVal myString As String) As System.Net.IPAddress
		'    Dim myStrs() As String
		'    myStrs = myString.Split("."c)
		'    Try
		'        Dim myBytes As New List(Of Byte)
		'        For Each Str As String In myStrs
		'            myBytes.Add(CByte(CInt(Str)))
		'        Next
		'        Dim ipAddr As New System.Net.IPAddress(myBytes.ToArray())
		'        Return ipAddr
		'    Catch ex As Exception
		'        Return Nothing
		'    End Try
		'End Function
		Public Shared Function ConvertHexStringToByteArray(ByVal hexString As String) As Byte()
			Try
				If hexString.Length = 0 Then Return Nothing
				Dim numberOfCharacters As Integer = hexString.Length
				Dim lengthOfBytes As Integer = CInt(numberOfCharacters / 2 - 1)
				Dim bytes(lengthOfBytes) As Byte
				For i As Integer = 0 To numberOfCharacters - 1 Step 2
					bytes(CInt(i / 2)) = Convert.ToByte(hexString.Substring(i, 2), 16)
				Next
				Return bytes
			Catch ex As Exception
				Return Nothing
			End Try
		End Function

		Public Shared Function RemoveHTMLFromString(ByVal stringToModify As String) As String
			Dim retStr As String = stringToModify
			If retStr.Contains("&nbsp;") Then
				retStr = retStr.Replace("&nbsp;", " ")
			End If
			If retStr.Contains("<br />") Then
				retStr = retStr.Replace("<br />", vbCrLf)
			End If

			If retStr.Contains("<br/>") Then
				retStr = retStr.Replace("<br/>", vbCrLf)
			End If

			If retStr.Contains("<br>") Then
				retStr = retStr.Replace("<br>", vbCrLf)
			End If
			' http://www.rezashirazi.com/post/2008/11/remove-tags-from-html-string-function.aspx
			' http://aliraza.wordpress.com/2007/07/05/how-to-remove-html-tags-from-string-in-c/
			Return System.Text.RegularExpressions.Regex.Replace(retStr, "<(.|\n)*?>", "")

		End Function
	End Class
End Namespace

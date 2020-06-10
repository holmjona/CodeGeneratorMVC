Imports Microsoft.VisualBasic
Imports System.Text

Namespace Tools
	Public Class Hasher
		''' <summary>
		''' create SHA1 Hash from given String
		''' </summary>
		''' <param name="hashString">String to Hash</param>
		''' <returns>40 Character Hex Hash for given string.</returns>
		''' <remarks></remarks>
		Public Shared Function CreateSHA1Hash(ByVal hashString As String) As String
			Dim data() As Byte = System.Text.Encoding.ASCII.GetBytes(hashString)
			Dim hasher As System.Security.Cryptography.SHA1
			hasher = System.Security.Cryptography.SHA1.Create()
			Dim hash() As Byte = hasher.ComputeHash(data)
			Dim stB As New StringBuilder
			For Each b As Byte In hash
				stB.Append(Hex(b).PadLeft(2, "0"c))
			Next b
			Return stB.ToString
		End Function
	End Class
End Namespace

Public Class ClassCreationFromVariables
    Private Function getDataType(ByVal privateStr As String) As String
        Dim retString As String = privateStr
        If retString.IndexOf("As") > 0 Then
            retString = privateStr.Substring(privateStr.LastIndexOf("As "))
        ElseIf retString.IndexOf("as") > 0 Then
            retString = privateStr.Substring(privateStr.LastIndexOf("as "))
        End If

        retString = retString.Remove(0, 3)
        retString = retString.Trim
        Return retString
    End Function
    Private Function getName(ByVal privateStr As String, ByVal withUnderScore As Boolean) As String
        Dim retStr As String = privateStr
        retStr = retStr.Replace("Private", "")
        retStr = retStr.Replace("private", "")
        retStr = retStr.Replace("Dim", "")
        If retStr.IndexOf("As") > 0 Then
            retStr = retStr.Remove(retStr.LastIndexOf("As "))
        ElseIf retStr.IndexOf("as") > 0 Then
            retStr = retStr.Remove(retStr.LastIndexOf("as "))
        End If

        If Not withUnderScore Then
            retStr = retStr.Replace("_", "")
        Else
            retStr = retStr.Insert(0, "_")
        End If

        retStr = retStr.Trim

        Return retStr
    End Function

End Class

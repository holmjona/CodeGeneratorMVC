Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls

Namespace Tools
    Public Class Link
        Public Enum TargetLocation As Integer
            none = 0
            blank = 1
            parent = 2
            self = 3
            top = 4
            frame = 5
        End Enum
        Public Target As TargetLocation
        Public Address As String
        Public Text As String
        Public CssClass As String
        Public TargetFrame As String
        Public ToolTip As String
        Public Function AsLink() As HyperLink
            Return getHyperlink(Text, Address, Target, TargetFrame, CssClass, ToolTip)
        End Function
        Public Shared Function getHyperlink(ByVal text As String, ByVal URL As String, _
             Optional ByVal target As TargetLocation = TargetLocation.none, Optional ByVal targetFrameName As String = "", _
             Optional ByVal cssClass As String = "", Optional ToolTip As String = "") As HyperLink
            Dim hyp As New HyperLink
            hyp.Text = text
            hyp.NavigateUrl = URL
            hyp.ToolTip = ToolTip
            Select Case target
                Case TargetLocation.blank
                    hyp.Target = "_blank"
                Case TargetLocation.parent
                    hyp.Target = "_parent"
                Case TargetLocation.self
                    hyp.Target = "_self"
                Case TargetLocation.top
                    hyp.Target = "_top"
                Case TargetLocation.frame
                    hyp.Target = targetFrameName
                Case Else ' _target.none

            End Select
            If cssClass <> "" Then
                hyp.CssClass = cssClass
            End If
            Return hyp
        End Function
        Public Shared Sub addLinkToList(ByVal text As String, ByVal address As String, ByVal id As Integer, ByVal lst As List(Of Tools.Link), _
                                        Optional ByVal target As Tools.Link.TargetLocation = TargetLocation.none)
            Dim tLink As Tools.Link = lst.FirstOrDefault(Function(l) l.Text = text)
            If tLink Is Nothing Then
                tLink = New Tools.Link()
                tLink.Text = text
                lst.Add(tLink)
            End If
            If address <> "" Then
                tLink.Address = address & id.ToString
            Else
                tLink.Address = address
            End If
            tLink.Target = target
        End Sub
    End Class
End Namespace
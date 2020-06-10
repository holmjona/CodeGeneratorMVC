' Created: 5 September 2007
' Creator: John Leith

Option Strict On

Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports IRICommonObjects.Words

Namespace Tools
    Public Class TableToolkit
        Private Sub New()

        End Sub
        Public Shared Function getActionCell(ByVal userName As String, ByVal datePerformed As Date, _
                                             ByVal dateFormat As String, _
                                             Optional ByVal startingText As String = "", _
                                             Optional ByVal columnSpan As Integer = 1, _
                                             Optional ByVal rowSpan As Integer = 1, _
                                             Optional ByVal IsHeaderCell As Boolean = False) As TableCell
            Return getTableCell(startingText & " " & datePerformed.ToString(dateFormat) & " by " & userName, _
                                columnSpan, rowSpan, IsHeaderCell)
        End Function
        Public Shared Function getTableCell(ByVal value As String, Optional ByVal columnSpan As Integer = 1, _
                                            Optional ByVal rowSpan As Integer = 1, _
                                            Optional ByVal IsHeaderCell As Boolean = False,
                                            Optional addControls As List(Of WebControl) = Nothing) As TableCell

            Dim tCell As TableCell = getTableCell(New LiteralControl(value), columnSpan, rowSpan, IsHeaderCell)

            If addControls IsNot Nothing Then
                For Each cnt As WebControl In addControls
                    tCell.Controls.Add(cnt)
                Next
            End If

            Return tCell
        End Function
        Public Shared Function getTableCell(ByVal obj As Control, Optional ByVal columnSpan As Integer = 1, _
                                            Optional ByVal rowSpan As Integer = 1, _
                                            Optional ByVal IsHeaderCell As Boolean = False) As TableCell
            Dim tCell As New TableCell
            If IsHeaderCell Then tCell = New TableHeaderCell
            tCell.Controls.Add(obj)
            setSpans(tCell, columnSpan, rowSpan)
            Return tCell
        End Function
        Private Shared Sub setSpans(ByRef tCell As TableCell, ByVal columnSpan As Integer, ByVal rowSpan As Integer)
            If columnSpan > 1 Then tCell.ColumnSpan = columnSpan
            If rowSpan > 1 Then tCell.RowSpan = rowSpan
        End Sub

        Public Shared Function getNoResultsFoundCell(ByVal aliasString As NameAlias, _
                                                     Optional ByVal columnSpan As Integer = 1) As TableCell
            Return getTableCell("No " & aliasString.Plural & " were returned.", columnSpan)
        End Function
        Public Shared Function getHyperlinkCell(ByVal text As String, ByVal URL As String, Optional ByVal columnSpan As Integer = 1, _
                                                 Optional ByVal rowSpan As Integer = 1, _
                                                 Optional ByVal target As Tools.Link.TargetLocation = Tools.Link.TargetLocation.none, _
                                                 Optional ByVal targetFrameName As String = "", _
                                                 Optional ByVal cssClass As String = "", _
                                                 Optional ByVal IsHeaderCell As Boolean = False) As TableCell
            'Dim tCell As New TableCell
            'If IsHeaderCell Then tCell = New TableHeaderCell
            'addHyperlinkToCell(tCell, text, URL, linkTarget.none, , cssClass)
            'setSpans(tCell, columnSpan, rowSpan)
            'Return tCell
            Return getTableCell(Tools.Link.getHyperlink(text, URL, target, targetFrameName, cssClass), columnSpan, rowSpan, IsHeaderCell)
        End Function
        Public Shared Sub addHyperlinkToCell(ByRef tCell As TableCell, ByVal text As String, ByVal URL As String, _
         Optional ByVal target As Tools.Link.TargetLocation = Tools.Link.TargetLocation.none, Optional ByVal targetFrameName As String = "", _
            Optional ByVal cssClass As String = "")
            '   Dim hyp As New HyperLink
            '   hyp.Text = text
            '   hyp.NavigateUrl = URL
            '   Select Case target
            '       Case linkTarget.blank
            '           hyp.Target = "_blank"
            '       Case linkTarget.parent
            '           hyp.Target = "_parent"
            '       Case linkTarget.self
            '           hyp.Target = "_self"
            '       Case linkTarget.top
            '           hyp.Target = "_top"
            '       Case linkTarget.frame
            '           hyp.Target = targetFrameName
            '       Case Else ' _target.none

            '   End Select
            '   If cssClass <> "" Then
            '       hyp.CssClass = cssClass
            '   End If
            tCell.Controls.Add(Tools.Link.getHyperlink(text, URL, target, targetFrameName, cssClass))
        End Sub
        Public Shared Sub addTextToTableCell(ByVal tcell As TableCell, ByVal value As String)
            Dim lbl As New Label
            lbl.Text = value
            tcell.Controls.Add(lbl)
        End Sub
        Public Shared Function getCheckBoxCell(ByVal checkBoxID As String, Optional ByVal isChecked As Boolean = False) As TableCell
            Dim tCell As New TableCell
            Dim chkBox As New CheckBox
            chkBox.ID = checkBoxID
            chkBox.Checked = isChecked
            tCell.Controls.Add(chkBox)
            Return tCell
        End Function
        Public Shared Function getImageCell(ByVal querystring As String) As TableCell
            Dim tCell As New TableCell
            Dim img As New Image
            img.ImageUrl = querystring
            tCell.Controls.Add(img)

            Return tCell
        End Function
        Public Shared Function getHeaderRow(ByVal columnTitles As String()) As TableHeaderRow
            Dim thr As New TableHeaderRow
            For Each sName As String In columnTitles
                thr.Controls.Add(getHeaderCell(sName))
            Next
            Return thr
        End Function
        ''' <summary>
        ''' If table Command is passed use the following format 
        ''' tableCommand:NameOfJSFunction|textValueOfLink
        ''' optional parameters are passed as follows:
        ''' tableCommand:NameOfJSFunction|textValueOfLink|param1|param2 ...etc...
        ''' </summary>
        ''' <param name="title"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getHeaderCell(ByVal title As String) As TableHeaderCell
            Dim thc As New TableHeaderCell
            If title.Contains("tableCommand:") Then
                Dim variables As String()
                variables = title.Substring(title.IndexOf(":") + 1).Split("|"c)
                Dim hypComm As New HyperLink
                hypComm.NavigateUrl = "#"
                hypComm.Attributes.Add("onclick", variables(0) & "(this" & getAdditionalParameters(variables.Skip(2)) & ");")
                hypComm.CssClass = "tableCommand"
                hypComm.Text = variables(1)
                thc.Controls.Add(hypComm)
            Else
                thc.Text = title
            End If
            Return thc
        End Function
        Private Shared Function getAdditionalParameters(ByVal vars As IEnumerable(Of String)) As String
            Dim retStr As String = ""
            For Each s As String In vars
                retStr &= "," & s
            Next
            Return retStr
        End Function

    End Class
End Namespace

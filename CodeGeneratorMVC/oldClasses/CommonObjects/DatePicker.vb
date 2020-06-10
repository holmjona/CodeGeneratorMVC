Option Strict On
' Created: 13 September 2007
' Creator: John Leith
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace Tools
	Public Class DatePicker
		Inherits System.Web.UI.UserControl

		Private ddlMonth As New DropDownList
		Private ddlDay As New DropDownList
		Private ddlYear As New DropDownList

		Private _yearsback As Integer = 1
		Private _yearsforward As Integer = 10

		Public Sub New()

			' fill months
			'ddlMonth.Items.Add("-month-")
			For month As Integer = 1 To 12
				Dim d As New Date(1, month, 1)
				ddlMonth.Items.Add(New ListItem(d.ToString("MMM (M)"), month.ToString))
			Next

			' fill days
			'ddlDay.Items.Add("-day-")
			For day As Integer = 1 To 31
				ddlDay.Items.Add(New ListItem(day.ToString))
			Next

			' fill years
			'ddlYear.Items.Add("-year-")
			For year As Integer = Now.Year - _yearsback To Now.Year + _yearsforward
				ddlYear.Items.Add(New ListItem(year.ToString))
			Next

			SelectedDate = Now


			Controls.Add(ddlMonth)
			Controls.Add(ddlDay)
			Controls.Add(ddlYear)
		End Sub

		Public Property SelectedDate() As Date
			Get
				Try
					Return New Date(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), CInt(ddlDay.SelectedValue))
				Catch ex As Exception
					Return Nothing
				End Try
			End Get
			Set(ByVal value As Date)
				If value.Year >= Now.Year - _yearsback AndAlso value.Year <= Now.Year + _yearsforward Then
					ddlYear.SelectedValue = value.Year.ToString
					ddlMonth.SelectedValue = value.Month.ToString
					ddlDay.SelectedValue = value.Day.ToString
				End If
			End Set
		End Property
		Public ReadOnly Property validateDate() As Boolean
			Get
				Try
					Dim tempDate As New Date(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), CInt(ddlDay.SelectedValue))
					Return True
				Catch ex As Exception
					Return False
				End Try
			End Get
		End Property
	End Class

End Namespace
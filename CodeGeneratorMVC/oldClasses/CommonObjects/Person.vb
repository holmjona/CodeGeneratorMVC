Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Security
	''' <summary>
	''' Base class for a person in the system.
	''' </summary>
	''' <remarks></remarks>

	Public MustInherit Class Person
		'Created By: Jon Holmes
		'Created On: 1/12/2009 11:07 AM
		Inherits DatabaseRecord
#Region "Private Variables"
		Protected _FirstName As String
		Protected _MiddleName As String
        Protected _LastName As String
        Protected _NickName As String
#End Region
		Public Sub New()

		End Sub
#Region "Public Properties"
        ''' <summary>
        ''' Gets or Sets a person's first or given name.
        ''' </summary>
        ''' <value></value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Overridable Property FirstName() As String
            Get
                If _FirstName Is Nothing Then _FirstName = ""
                Return _FirstName.Trim
            End Get
            Set(ByVal value As String)
                _FirstName = value
            End Set
        End Property
        ''' <summary>
        ''' Gets or Sets a person's middle or second given name.
        ''' </summary>
        ''' <value></value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Overridable Property MiddleName() As String
            Get
                If _MiddleName Is Nothing Then _MiddleName = ""
                Return _MiddleName.Trim
            End Get
            Set(ByVal value As String)
                _MiddleName = value
            End Set
        End Property
        ''' <summary>
        ''' User's Middle Initial
        ''' </summary>
        ''' <returns>The first letter of the user based on the recorded middle name followed by a period".".</returns>
        ''' <remarks>This returns an empty string if there is no middle name provided.</remarks>
        Public Overridable ReadOnly Property MiddleInitial() As String
            Get
                If MiddleName.Length > 1 Then
                    Return MiddleName.Substring(0, 1).ToUpper & "."
                Else 'If MiddleName.Trim.Length = 1 Then
                    '	Return MiddleName.ToUpper & ".".Trim
                    'Else
                    Return MiddleName
                End If
            End Get
        End Property
        ''' <summary>
        ''' Gets or Sets a person's nick or common name.
        ''' </summary>
        ''' <value></value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Overridable Property NickName() As String
            Get
                If _NickName = Nothing Then _NickName = ""
                Return _NickName
            End Get
            Set(ByVal value As String)
                _NickName = value
            End Set
        End Property
        ''' <summary>
        ''' Gets or Sets a person's last, sur, or family name.
        ''' </summary>
        ''' <value></value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Overridable Property LastName() As String
            Get
                If _LastName Is Nothing Then _LastName = ""
                Return _LastName.Trim
            End Get
            Set(ByVal value As String)
                _LastName = value
            End Set
        End Property
        ''' <summary>
        ''' User's Initials
        ''' </summary>
        ''' <returns>The first letter of each of the user's names.</returns>
        '''<param name="usePeriods" >Whether to include periods(".") in the return value. e.g. true: J.Q.D.; false: JQD</param>
        ''' <remarks>This returns an empty string if there is no name provided.</remarks>
        Public Overridable ReadOnly Property Initials(Optional ByVal usePeriods As Boolean = False) As String
            Get
                Dim fI As String = ""
                Dim mI As String = MiddleInitial
                Dim lI As String = ""
                If FirstName.Length > 0 Then
                    fI = FirstName.Substring(0, 1)
                End If
                If mI.Length > 1 Then
                    mI = mI.Substring(0, 1)
                End If
                If LastName.Length > 0 Then
                    lI = LastName.Substring(0, 1)
                End If
                If Not usePeriods Then
                    Return fI & mI & lI
                Else
                    Return fI & "." & mI & "." & lI & "."
                End If
            End Get
        End Property

        ''' <summary>
        ''' The User's Full Name in the folloing format: First M. Last
        ''' </summary>
        ''' <returns>The users full Name with the Middle Name abbreviated.</returns>
        ''' <remarks>This assumes that the user uses the first name and a middle initial. 
        ''' If there is no middle name it returns the following format: First Last</remarks>
        Public Overridable ReadOnly Property OfficialName() As String
            Get
                If MiddleInitial.Length > 0 Then
                    Return FirstName.Trim & " " & MiddleInitial & " " & LastName.Trim
                Else
                    Return FirstName.Trim & " " & LastName.Trim
                End If
            End Get
        End Property
        ''' <summary>
        ''' The User's Common Name in the folloing format: Nick Last
        ''' </summary>
        ''' <returns>The users full Name with the Middle Name abbreviated.</returns>
        ''' <remarks>This assumes that the user uses a common name format. 
        ''' If there is no nick name it returns the following format: First Last</remarks>
        Public Overridable ReadOnly Property CommonName() As String
            Get
                Dim fName As String = NickName.Trim
                If fName.Length < 1 Then
                    fName = FirstName.Trim
                End If

                Return String.Format("{0} {1}", fName, LastName.Trim)
            End Get
        End Property
        ''' <summary>
        ''' The User's Complete Name in the folloing format: First Middle "Nick" Last
        ''' </summary>
        ''' <returns>The users Full Name with the full Middle Name and nickname in quotes("").</returns>
        ''' <remarks>If there is no middle name it returns the following format: First "Nick" Last
        ''' If there is no nick name it returns the following format: First Middle Last
        ''' If there is no middle or nick name it returns the following format: First Last</remarks>
        Public Overridable ReadOnly Property CompleteName() As String
            Get
                Dim fStr As String = FirstName.Trim
                Dim mStr As String = MiddleName.Trim
                Dim nStr As String = ""
                Dim lStr As String = LastName.Trim

                If NickName.Trim.Length > 0 Then
                    nStr = """" & NickName & """ "
                End If
                If mStr.Length > 0 Then
                    mStr &= " "
                End If

                Return String.Format("{0} {1}{2}{3}", fStr, mStr, nStr, lStr)
            End Get
        End Property

#End Region

	End Class
End Namespace

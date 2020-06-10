Imports System.ComponentModel

<Serializable()> Public Class MasterPageContent
    Implements INotifyPropertyChanged
	'Private _name As String
	'   Private _ContainsLiteralText As Boolean
	'   Private _ContainsLabelText As Boolean
	'Private _IsBody As Boolean
	

	'   Public Property Name() As String
	'       Get
	'           Return _name
	'       End Get
	'       Set(ByVal value As String)
	'           _name = value
	'           NotifyPropertyChanged("Name")
	'       End Set
	'End Property
	'Public Property ContainsLiteralText() As Boolean
	'    Get
	'        Return _ContainsLiteralText
	'    End Get
	'    Set(ByVal value As Boolean)
	'        _ContainsLiteralText = value
	'        NotifyPropertyChanged("ContainsLiteralText")
	'    End Set
	'End Property
	'Public Property ContainsLabelText() As Boolean
	'    Get
	'        Return _ContainsLabelText
	'    End Get
	'    Set(ByVal value As Boolean)
	'        _ContainsLabelText = value
	'        NotifyPropertyChanged("ContainsLabelText")
	'    End Set
	'End Property
	'Public Property IsBody() As Boolean
	'    Get
	'        Return _IsBody
	'    End Get
	'    Set(ByVal value As Boolean)
	'        _IsBody = value
	'        NotifyPropertyChanged("IsBody")
	'    End Set
	'End Property

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

End Class

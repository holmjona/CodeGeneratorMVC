Imports System.Collections.Generic

Public Class ClassVariableCollection
    Inherits CollectionBase
    'Dim _objList As New List(Of ClassVariable)
    'Public Sub New()

    'End Sub
    'Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
    '    _objList.CopyTo(array, index)
    'End Sub

    'Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
    '    Get
    '        Return _objList.Count
    '    End Get
    'End Property

    'Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
    '    Get
    '        Return False
    '    End Get
    'End Property

    'Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
    '    Get
    '        Return Me
    '    End Get
    'End Property

    'Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '    Return New ClassVariableCollectionEnumerator(Me)
    'End Function
    'Class ClassVariableCollectionEnumerator
    '    Implements IEnumerator
    '    Dim _index As Integer = -1
    '    Dim _collection As ClassVariableCollection
    '    Friend Sub New()

    '    End Sub
    '    Friend Sub New(ByVal collectionClass As ClassVariableCollection)
    '        _collection = collectionClass
    '        If (_collection._objList.Count = 0) Then
    '            _index = -1
    '        End If
    '    End Sub
    '    Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
    '        Get
    '            If _index > -1 Then
    '                Return _collection._objList(_index)
    '            Else
    '                Throw New InvalidOperationException("Invalid operation: index < 0")
    '            End If
    '        End Get
    '    End Property

    '    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
    '        If _index < _collection._objList.Count - 1 Then
    '            _index += 1
    '            Return True
    '        End If
    '        Return False
    '    End Function

    '    Public Sub Reset() Implements System.Collections.IEnumerator.Reset
    '        _index = -1
    '    End Sub
    'End Class

    'Public Function Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
    '    _objList.Add(CType(value, ClassVariable))
    '    Return _objList.Count - 1
    'End Function

    'Public Sub Clear() Implements System.Collections.IList.Clear
    '    _objList.Clear()
    'End Sub

    'Public Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
    '    Return (_objList.Contains(CType(value, ClassVariable)))
    'End Function

    'Public Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
    '    Return _objList.IndexOf(CType(value, ClassVariable))
    'End Function

    'Public Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
    '    _objList.Insert(index, CType(value, ClassVariable))
    'End Sub

    'Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
    '    Get
    '        Return False
    '    End Get
    'End Property

    'Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
    '    Get
    '        Return False
    '    End Get
    'End Property

    'Default Public Property Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
    '    Get

    '    End Get
    '    Set(ByVal value As Object)

    '    End Set
    'End Property

    'Public Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
    '    _objList.Remove(CType(value, ClassVariable))
    'End Sub

    'Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
    '    _objList.RemoveAt(index)
    'End Sub



End Class

Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Configuration

	Public MustInherit Class DALBase
#Region "Database Connections"
	
	Protected Shared Function GetDataReader(ByVal comm As SqlCommand, ByVal connectionString As String, _
				 Optional ByVal commType As CommandType = CommandType.StoredProcedure) As SqlDataReader
		comm.Connection = New SqlConnection(connectionString)
		comm.CommandType = commType
		comm.Connection.Open()
		Return comm.ExecuteReader()
	End Function
	''' <summary>
	''' Sets Connection and Executes given comm on the database
	''' </summary>
	''' <param name="comm">SQLCommand to execute</param>
	''' <returns>number of rows affected; -1 on failure, positive value on success.</returns>
	''' <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.
	'''       Edit Connection is set here.</remarks>
	Protected Shared Function UpdateObject(ByVal comm As SqlCommand, ByVal connectionString As String, _
		Optional ByVal commType As CommandType = CommandType.StoredProcedure) As Integer
		Dim retInt As Integer = 0
		Try
			comm.Connection = New SqlConnection(connectionString)
			comm.CommandType = commType
			comm.Connection.Open()
			retInt = comm.ExecuteNonQuery()
			comm.Connection.Close()
		Catch ex As Exception
			comm.Connection.Close()
			retInt = -1
		End Try
		Return retInt
	End Function
	Protected Shared Function AddObject(ByVal comm As SqlCommand, ByVal connectionString As String, ByVal outputParameterName As String, _
  Optional ByVal commType As CommandType = CommandType.StoredProcedure) As Integer
		Dim retInt As Integer = 0
		Try
			comm.Connection = New SqlConnection(connectionString)
			comm.CommandType = commType
			comm.Connection.Open()
			Dim retParameter As SqlParameter
			retParameter = comm.Parameters.Add(outputParameterName, Data.SqlDbType.Int)
			retParameter.Direction = Data.ParameterDirection.Output
			comm.ExecuteNonQuery()
			retInt = CInt(retParameter.Value)
			comm.Connection.Close()
		Catch ex As Exception
			comm.Connection.Close()
			retInt = -1
		End Try
		Return retInt
	End Function

#End Region

End Class

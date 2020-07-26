using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

public abstract class DALBase {
    protected static SqlDataReader GetDataReader(SqlCommand comm, string connectionString, CommandType commType = CommandType.StoredProcedure) {
        comm.Connection = new SqlConnection(connectionString);
        comm.CommandType = commType;
        comm.Connection.Open();
        return comm.ExecuteReader();
    }
    /// <summary>
	/// 	''' Sets Connection and Executes given comm on the database
	/// 	''' </summary>
	/// 	''' <param name="comm">SQLCommand to execute</param>
	/// 	''' <returns>number of rows affected; -1 on failure, positive value on success.</returns>
	/// 	''' <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.
	/// 	'''       Edit Connection is set here.</remarks>
    protected static int UpdateObject(SqlCommand comm, string connectionString, CommandType commType = CommandType.StoredProcedure) {
        int retInt = 0;
        try {
            comm.Connection = new SqlConnection(connectionString);
            comm.CommandType = commType;
            comm.Connection.Open();
            retInt = comm.ExecuteNonQuery();
            comm.Connection.Close();
        } catch (Exception ex) {
            comm.Connection.Close();
            retInt = -1;
        }
        return retInt;
    }
    protected static int AddObject(SqlCommand comm, string connectionString, string outputParameterName, CommandType commType = CommandType.StoredProcedure) {
        int retInt = 0;
        try {
            comm.Connection = new SqlConnection(connectionString);
            comm.CommandType = commType;
            comm.Connection.Open();
            SqlParameter retParameter;
            retParameter = comm.Parameters.Add(outputParameterName, System.Data.SqlDbType.Int);
            retParameter.Direction = System.Data.ParameterDirection.Output;
            comm.ExecuteNonQuery();
            retInt = System.Convert.ToInt32(retParameter.Value);
            comm.Connection.Close();
        } catch (Exception ex) {
            comm.Connection.Close();
            retInt = -1;
        }
        return retInt;
    }
}

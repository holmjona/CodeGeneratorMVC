
/// <summary>

/// ''' when overriden in a child class

/// ''' represents a database record

/// ''' </summary>
public abstract class DatabaseRecord {
    protected int _ID = -1;

    /// <summary>
	/// 	''' The primary key of this record
	/// 	''' </summary>
    public int ID {
        get {
            return _ID;
        }
        set {
            _ID = value;
        }
    }

    // ''' <summary>
    // ''' Saves this record to the database (it handles add or update as needed)
    // ''' </summary>
    // Public Overridable Function dbSave() As Integer
    // Dim retval As Integer
    // If _ID = -1 Then ' The ID here needs to be -1
    // retval = dbAdd()
    // Else
    // retval = dbUpdate()
    // End If

    // Return retval
    // End Function

    /// <summary>
	/// 	''' Determines based on class and ID if the given object is
	/// 	''' the same as this object
	/// 	''' </summary>
    public override bool Equals(object obj) {
        bool retval = false;
        if (obj != null && obj is DatabaseRecord) {
            if (((DatabaseRecord)obj).ID == ID)
                retval = true;
        }
        return retval;
    }

    /// <summary>
	/// 	''' This function is called by the Security DAL to load this object from the database
	/// 	''' </summary>
    public abstract void Fill(System.Data.SqlClient.SqlDataReader dr);

    public static bool operator ==(DatabaseRecord r1, DatabaseRecord r2) {
        if (r1 == null && r2 == null)
            return true;
        else if (r1 == null || r2 == null)
            return false;
        else
            return r1.ID == r2.ID;
    }
    public static bool operator !=(DatabaseRecord r1, DatabaseRecord r2) {
        return r1 != r2;
    }


    /// <summary>
	/// 	''' When overridden in a child class this method 
	/// 	''' will insert a record into the database associated with this object
	/// 	''' </summary>
    public abstract int dbAdd();

    /// <summary>
	/// 	''' When overriden in a child class this method
	/// 	''' will update the assoicated record(s) in the database
	/// 	''' </summary>
    public abstract int dbUpdate();

    /// <summary>
	/// 	''' When overriden in a child class this method
	/// 	''' will return the most human-readable string representing the object
	/// 	''' </summary>
	/// 	''' <returns></returns>
	/// 	''' <remarks></remarks>
    public abstract override string ToString();
}

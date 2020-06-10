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

namespace Security {
    /// <summary>
	/// 	''' Base class for a person in the system.
	/// 	''' </summary>
	/// 	''' <remarks></remarks>

    public abstract class Person : DatabaseRecord {
        protected string _FirstName;
        protected string _MiddleName;
        protected string _LastName;
        protected string _NickName;
        public Person() {
        }
        /// <summary>
        ///         ''' Gets or Sets a person's first or given name.
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns>String</returns>
        ///         ''' <remarks></remarks>
        public virtual string FirstName {
            get {
                if (_FirstName == null)
                    _FirstName = "";
                return _FirstName.Trim();
            }
            set {
                _FirstName = value;
            }
        }
        /// <summary>
        ///         ''' Gets or Sets a person's middle or second given name.
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns>String</returns>
        ///         ''' <remarks></remarks>
        public virtual string MiddleName {
            get {
                if (_MiddleName == null)
                    _MiddleName = "";
                return _MiddleName.Trim();
            }
            set {
                _MiddleName = value;
            }
        }
        /// <summary>
        ///         ''' User's Middle Initial
        ///         ''' </summary>
        ///         ''' <returns>The first letter of the user based on the recorded middle name followed by a period".".</returns>
        ///         ''' <remarks>This returns an empty string if there is no middle name provided.</remarks>
        public virtual string MiddleInitial {
            get {
                if (MiddleName.Length > 1)
                    return MiddleName.Substring(0, 1).ToUpper() + ".";
                else
                    // Return MiddleName.ToUpper & ".".Trim
                    // Else
                    return MiddleName;
            }
        }
        /// <summary>
        ///         ''' Gets or Sets a person's nick or common name.
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns>String</returns>
        ///         ''' <remarks></remarks>
        public virtual string NickName {
            get {
                if (_NickName == null)
                    _NickName = "";
                return _NickName;
            }
            set {
                _NickName = value;
            }
        }
        /// <summary>
        ///         ''' Gets or Sets a person's last, sur, or family name.
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns>String</returns>
        ///         ''' <remarks></remarks>
        public virtual string LastName {
            get {
                if (_LastName == null)
                    _LastName = "";
                return _LastName.Trim();
            }
            set {
                _LastName = value;
            }
        }
        /// <summary>
        ///         ''' User's Initials
        ///         ''' </summary>
        ///         ''' <returns>The first letter of each of the user's names.</returns>
        ///         '''<param name="usePeriods" >Whether to include periods(".") in the return value. e.g. true: J.Q.D.; false: JQD</param>
        ///         ''' <remarks>This returns an empty string if there is no name provided.</remarks>
        public virtual string Initials {
            get {
                string fI = "";
                string mI = MiddleInitial;
                string lI = "";
                if (FirstName.Length > 0)
                    fI = FirstName.Substring(0, 1);
                if (mI.Length > 1)
                    mI = mI.Substring(0, 1);
                if (LastName.Length > 0)
                    lI = LastName.Substring(0, 1);
                return fI + mI + lI;
            }
        }
        /// <summary>
        ///         ''' User's Initials
        ///         ''' </summary>
        ///         ''' <returns>The first letter of each of the user's names.</returns>
        ///         '''<param name="usePeriods" >Whether to include periods(".") in the return value. e.g. true: J.Q.D.; false: JQD</param>
        ///         ''' <remarks>This returns an empty string if there is no name provided.</remarks>
        public virtual string InitialsWithPeriods {
            get {
                string fI = "";
                string mI = MiddleInitial;
                string lI = "";
                if (FirstName.Length > 0)
                    fI = FirstName.Substring(0, 1);
                if (mI.Length > 1)
                    mI = mI.Substring(0, 1);
                if (LastName.Length > 0)
                    lI = LastName.Substring(0, 1);
                return fI + "." + mI + "." + lI + ".";
            }
        }

        /// <summary>
        ///         ''' The User's Full Name in the folloing format: First M. Last
        ///         ''' </summary>
        ///         ''' <returns>The users full Name with the Middle Name abbreviated.</returns>
        ///         ''' <remarks>This assumes that the user uses the first name and a middle initial. 
        ///         ''' If there is no middle name it returns the following format: First Last</remarks>
        public virtual string OfficialName {
            get {
                if (MiddleInitial.Length > 0)
                    return FirstName.Trim() + " " + MiddleInitial + " " + LastName.Trim();
                else
                    return FirstName.Trim() + " " + LastName.Trim();
            }
        }
        /// <summary>
        ///         ''' The User's Common Name in the folloing format: Nick Last
        ///         ''' </summary>
        ///         ''' <returns>The users full Name with the Middle Name abbreviated.</returns>
        ///         ''' <remarks>This assumes that the user uses a common name format. 
        ///         ''' If there is no nick name it returns the following format: First Last</remarks>
        public virtual string CommonName {
            get {
                string fName = NickName.Trim();
                if (fName.Length < 1)
                    fName = FirstName.Trim();

                return string.Format("{0} {1}", fName, LastName.Trim());
            }
        }
        /// <summary>
        ///         ''' The User's Complete Name in the folloing format: First Middle "Nick" Last
        ///         ''' </summary>
        ///         ''' <returns>The users Full Name with the full Middle Name and nickname in quotes("").</returns>
        ///         ''' <remarks>If there is no middle name it returns the following format: First "Nick" Last
        ///         ''' If there is no nick name it returns the following format: First Middle Last
        ///         ''' If there is no middle or nick name it returns the following format: First Last</remarks>
        public virtual string CompleteName {
            get {
                string fStr = FirstName.Trim();
                string mStr = MiddleName.Trim();
                string nStr = "";
                string lStr = LastName.Trim();

                if (NickName.Trim().Length > 0)
                    nStr = "\"" + NickName + "\" ";
                if (mStr.Length > 0)
                    mStr += " ";

                return string.Format("{0} {1}{2}{3}", fStr, mStr, nStr, lStr);
            }
        }
    }
}

// Created By: Michael Smuin
// Created On: 1/8/2009 12:30:56 PM
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
	/// 	''' Categorization for users that also contains permission sets.
	/// 	''' </summary>
	/// 	''' <remarks></remarks>
    public abstract class RoleBase : DatabaseRecord {
        protected string _Name = "Anonymous";
        protected bool _IsAdmin = false;
        protected bool _IsAnonymous;
        public RoleBase(bool IsAnon = true) {
            _ID = -3;
            _IsAnonymous = true;
        }

        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Boolean</returns>
		/// 		''' <remarks></remarks>
        public bool IsAdmin {
            get {
                return _IsAdmin;
            }
            set {
                _IsAdmin = value;
            }
        }

        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Boolean</returns>
		/// 		''' <remarks></remarks>
        public bool IsAnonymous {
            get {
                return _IsAnonymous;
            }
            set {
                _IsAnonymous = value;
            }
        }

        public virtual void setAnonymous() {
            _Name = "Anonymous";
            // set all perms to false
            _IsAdmin = false;
            // flag as anonymous
            _IsAnonymous = true;
        }
    }
}

// Created By: Michael Smuin
// Created On: 1/8/2009 12:44:58 PM
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
	/// 	''' Object reference for any person who is accessing the system. Credentials differential all users. No credentials creates an anonymous user.
	/// 	''' </summary>
	/// 	''' <remarks></remarks>
    public abstract class UserBase : Person {
        protected string _Email;
        protected bool _IsActive;
        protected string _Password;
        protected string _UserName;
        public UserBase() {
        }
        public UserBase(bool isAnon) {
            if (isAnon) {
                _Email = "invalid";
                _IsActive = true;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string Email {
            get {
                return _Email;
            }
            set {
                _Email = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Boolean</returns>
		/// 		''' <remarks></remarks>
        public bool IsActive {
            get {
                return _IsActive;
            }
            set {
                _IsActive = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string Password {
            get {
                return _Password;
            }
            set {
                _Password = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string UserName {
            get {
                return _UserName;
            }
            set {
                _UserName = value;
            }
        }

        public string Hash {
            get {
                return Tools.Hasher.CreateSHA1Hash(_Email + _Password);
            }
        }
    }
}

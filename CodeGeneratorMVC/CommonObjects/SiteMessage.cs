// Created By: JDH
// Created On: 3/24/2009 2:19:09 PM
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
	/// 	''' Messages for Sites
	/// 	''' </summary>
	/// 	''' <remarks></remarks>
    public abstract class SiteMessageBase : DatabaseRecord {
        private DateTime _DateAdded;
        private UserBase _AddedBy;
        private int _AddedByID;
        private DateTime _DateShow;
        private DateTime _DateExpire;
        private bool _IsMaintenance;
        private bool _IsExpired;
        private SiteConfigBase _SiteConfig;
        private int _SiteConfigID;
        private string _Message;

        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Date</returns>
		/// 		''' <remarks></remarks>
        public DateTime DateAdded {
            get {
                return _DateAdded;
            }
            set {
                _DateAdded = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Integer</returns>
		/// 		''' <remarks></remarks>
        public abstract UserBase AddedBy { get; set; }

        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Integer</returns>
		/// 		''' <remarks></remarks>
        public int AddedByID {
            get {
                return _AddedByID;
            }
            set {
                _AddedByID = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Date</returns>
		/// 		''' <remarks></remarks>
        public DateTime DateShow {
            get {
                return _DateShow;
            }
            set {
                _DateShow = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Date</returns>
		/// 		''' <remarks></remarks>
        public DateTime DateExpire {
            get {
                return _DateExpire;
            }
            set {
                _DateExpire = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Boolean</returns>
		/// 		''' <remarks></remarks>
        public bool IsMaintenance {
            get {
                return _IsMaintenance;
            }
            set {
                _IsMaintenance = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Boolean</returns>
		/// 		''' <remarks></remarks>
        public bool IsExpired {
            get {
                return _IsExpired;
            }
            set {
                _IsExpired = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Integer</returns>
		/// 		''' <remarks></remarks>
        public abstract SiteConfigBase SiteConfig { get; set; }

        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Integer</returns>
		/// 		''' <remarks></remarks>
        public int SiteConfigID {
            get {
                return _SiteConfigID;
            }
            set {
                _SiteConfigID = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string Message {
            get {
                return _Message;
            }
            set {
                _Message = value;
            }
        }
    }
}

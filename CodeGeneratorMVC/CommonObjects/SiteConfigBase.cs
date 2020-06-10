// Created By: Michael Smuin
// Created On: 1/13/2009 2:26:22 PM
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
	/// 	''' Site Variables for the site that the user is at.
	/// 	''' </summary>
	/// 	''' <remarks></remarks>
    public abstract class SiteConfigBase {
        // ##########################################################
        // Static Variables   
        protected static int VersionReleaseNumber = 0;
        protected static int VersionMajorNumber = 0;
        protected static int VersionMinorNumber = 0;
        protected static DateTime VersionDate;

        public enum verClass : int {
            live    // Live Production 
,
            dev     // In Development
,
            test    // Testing
,
            alpha   // Alpha Stage
,
            beta    // Beta Stage
        }


        // the VersionClass given here will not only flag the code but place the correct banner on the site.
        // the VersionClass must match specifically to the list below.
        // ==============================
        // live = Live Production 
        // dev = In Development
        // test = Testing 
        // alpha = Alpha Stage
        // beta = Beta Stage
        // ==============================
        public static verClass VersionClass = verClass.dev;
        // if site is being tested and developed. 
        public static bool TestingSite = false;
        // ########################################################## 

        public static string Years = "2008 - 2009";
        public static string CompanyName = "Informatics Research Institute (IRI)";
        public static string CompanyAddress = "http://iri.isu.edu";
        // ########################################################## 		


        private string _doM = "IRI";
        private string _userN = "iriwebsites";
        private string _passW = "XubeTrAga32ba7uP-&3EqutAbruC55";
        private string _Name;
        private string _Title;
        private string _SubTitle;
        private string _Theme;
        private string _AdminEmailAddress;
        private int _ThumbnailMaxDimension = 100;
        private string _Image_Error_FullPath = "App_Pics/error.jpg";
        private string _Image_Error_Type = "jpg";
        private string _FullImagePath = "";
        private string _Image_WaterMark_FullPath = "";
        private string _RemotePath = @"\\IRISQL1\AnthroImages"; // dev = "\\RIO\Projects\Anthro"
        private string _SiteAddress;

        // ################### Email Settings ######################

        private string _EmailServerAddress;
        private string _EmailUserName;
        private string _EmailUserPassword;
        private int _EmailServerPort;



        public static string Version {
            get {
                string verStr;
                verStr = VersionReleaseNumber.ToString() + "." + VersionMajorNumber.ToString() + "." + VersionMinorNumber.ToString();
                // If VersionNumber Mod (CLng(VersionNumber) \ 1) > 0 Then
                // verStr = VersionNumber.ToString
                // Else
                // verStr = VersionNumber.ToString("F01")
                // End If
                return verStr + " | " + System.Enum.GetName(typeof(verClass), VersionClass) + " | " + VersionDate.ToString("d MMM yy");
            }
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
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string AdministratorEmailAddress {
            get {
                return _AdminEmailAddress;
            }
            set {
                _AdminEmailAddress = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string Title {
            get {
                return _Title;
            }
            set {
                _Title = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string SubTitle {
            get {
                return _SubTitle;
            }
            set {
                _SubTitle = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string Theme {
            get {
                return _Theme;
            }
            set {
                _Theme = value;
            }
        }
        /// <summary>
		/// 		''' TODO: Comment this
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>String</returns>
		/// 		''' <remarks></remarks>
        public string SiteAddress {
            get {
                return _SiteAddress;
            }
            set {
                _SiteAddress = value;
            }
        }
        public int ThumbnailMaxDimension {
            get {
                return _ThumbnailMaxDimension;
            }
            set {
                _ThumbnailMaxDimension = value;
            }
        }
        public string Image_Error_FullPath {
            get {
                return "themes/" + Theme + "/" + _Image_Error_FullPath;
            }
            set {
                _Image_Error_FullPath = value;
            }
        }
        public string Image_Error_Type {
            get {
                return _Image_Error_Type;
            }
            set {
                _Image_Error_Type = value;
            }
        }
        public string FullImagePath {
            get {
                return _FullImagePath;
            }
            set {
                _FullImagePath = value;
            }
        }
        public string Image_WaterMark_FullPath {
            get {
                return _Image_WaterMark_FullPath;
            }
            set {
                _Image_WaterMark_FullPath = value;
            }
        }
        public string RemotePath {
            get {
                if (VersionClass == verClass.dev)
                    return @"\\RIO\Projects\Anthro";
                return _RemotePath;
            }
            set {
                _RemotePath = value;
            }
        }
        public abstract List<SiteMessageBase> MessagesCurrent { get; }
        public abstract List<SiteMessageBase> Messages { get; }
        public string ImpDomain {
            get {
                return _doM;
            }
        }
        public string ImpUserName {
            get {
                return _userN;
            }
        }
        public string ImpPassword {
            get {
                return _passW;
            }
        }
        public static string DatabaseType {
            get {
                if (VersionClass == verClass.dev)
                    return "dev";
                return "";
            }
        }

        public string Email_ServerAddress {
            get {
                return _EmailServerAddress;
            }
            set {
                _EmailServerAddress = value.Trim();
            }
        }

        public string Email_Credentials_UserName {
            get {
                return _EmailUserName;
            }
            set {
                _EmailUserName = value.Trim();
            }
        }

        public string Email_Credientials_Password {
            get {
                return _EmailUserPassword;
            }
            set {
                _EmailUserPassword = value.Trim();
            }
        }

        public int Email_ServerPort {
            get {
                return _EmailServerPort;
            }
            set {
                _EmailServerPort = value;
            }
        }
    }
}

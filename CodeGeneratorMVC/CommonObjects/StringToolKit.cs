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

namespace Tools {
    public class StringToolKit {
        private StringToolKit() {
        }
        public static string getDatabaseSuccessString(Words.NameAlias variableAlias, Words.NameAlias verbAlias) {
            return variableAlias.Capitalized(false) + " was successfully " + verbAlias.PastTense + ".";
        }
        public static string getDatabaseErrorString(Words.NameAlias variableAlias, Words.NameAlias verbAlias) {
            return "Unable to " + verbAlias.Text(false) + " " + variableAlias.Text(false) + ".";
        }
        public static string getDatabaseErrorAlreadyExistsString(Words.NameAlias variableAlias, Words.NameAlias verbAlias) {
            return getDatabaseErrorString(variableAlias, verbAlias) + variableAlias.Capitalized(false) + " is already entered into the system. Duplicate Entry.";
        }

        public static string ObjectNotFound(Words.NameAlias aliasString) {
            return "Unable to retrieve " + aliasString.Text(false) + ".";
        }
        // Public Shared Function getIPAddressFromString(ByVal myString As String) As System.Net.IPAddress
        // Dim myStrs() As String
        // myStrs = myString.Split("."c)
        // Try
        // Dim myBytes As New List(Of Byte)
        // For Each Str As String In myStrs
        // myBytes.Add(CByte(CInt(Str)))
        // Next
        // Dim ipAddr As New System.Net.IPAddress(myBytes.ToArray())
        // Return ipAddr
        // Catch ex As Exception
        // Return Nothing
        // End Try
        // End Function
        public static byte[] ConvertHexStringToByteArray(string hexString) {
            try {
                if (hexString.Length == 0)
                    return null;
                int numberOfCharacters = hexString.Length;
                int lengthOfBytes = System.Convert.ToInt32(numberOfCharacters / (double)2 - 1);
                byte[] bytes = new byte[lengthOfBytes + 1];
                for (int i = 0; i <= numberOfCharacters - 1; i += 2)
                    bytes[System.Convert.ToInt32(i / (double)2)] = Convert.ToByte(hexString.Substring(i, 2), 16);
                return bytes;
            } catch (Exception ex) {
                return null;
            }
        }

        public static string RemoveHTMLFromString(string stringToModify) {
            string retStr = stringToModify;
            if (retStr.Contains("&nbsp;"))
                retStr = retStr.Replace("&nbsp;", " ");
            if (retStr.Contains("<br />"))
                retStr = retStr.Replace("<br />", Constants.vbCrLf);

            if (retStr.Contains("<br/>"))
                retStr = retStr.Replace("<br/>", Constants.vbCrLf);

            if (retStr.Contains("<br>"))
                retStr = retStr.Replace("<br>", Constants.vbCrLf);
            // http://www.rezashirazi.com/post/2008/11/remove-tags-from-html-string-function.aspx
            // http://aliraza.wordpress.com/2007/07/05/how-to-remove-html-tags-from-string-in-c/
            return System.Text.RegularExpressions.Regex.Replace(retStr, @"<(.|\n)*?>", "");
        }
    }
}

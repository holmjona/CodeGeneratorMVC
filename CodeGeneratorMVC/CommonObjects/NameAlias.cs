using System;
using System.Linq;

namespace Words {
    public class NameAlias {
        private string _Alias;
        private char[] _Vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
        private string[] _SoftWords = new[] { "hour", "honest", "honor" };
        private string[] _HardWords = new[] { "user", "unicorn", "uniform", "u.", "one", "union" };

        // Keep local copies of text to save on processor time.
        private string _Text = null;
        private string _TextPlural = null;
        private string _TextPluralAndCapital = null;
        private string _TextCapital = null;

        private string _JText = null;
        private string _JTextPlural = null;
        private string _JTextPluralAndCapital = null;
        private string _JTextCapital = null;

        /// <summary>
		/// 		''' Get Text as it is stored in the Database.
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>Unformatted text exactly as it is stored in the database.</returns>
		/// 		''' <remarks></remarks>
        public string TextUnFormatted {
            get {
                return _Alias;
            }
            set {
                _Alias = value.ToLower().Trim();
                clearLocalVars();
            }
        }
        private void clearLocalVars() {
            _Text = null;
            _TextPlural = null;
            _TextPluralAndCapital = null;
            _TextCapital = null;

            _JText = null;
            _JTextPlural = null;
            _JTextPluralAndCapital = null;
            _JTextCapital = null;
        }
        /// <summary>
		/// 		''' The singular lowercase text of the Alias with formatting applied.
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>The alias from the database with formatting applied.</returns>
		/// 		''' <remarks>This will apply basic formatting rules applied to what is exactly stored in the database.</remarks>
        public string Text(bool withJuncture_A = false) {
            if (_Text == null) {
                _Text = format(_Alias, false);
                _JText = format(_Alias, true);
            }
            if (withJuncture_A)
                return _JText;
            else
                return _Text;
        }
        /// <summary>
		/// 		''' The singular lowercase text of the alias formated and non breaking spaces removed.
		/// 		''' The given text will be returned with non breaking spaces removed.
		/// 		''' </summary>
		/// 		''' <param name="strToFormat">The string to remove non-breaking spaces from.</param>
		/// 		''' <returns>The text given with non breaking spaces removed.</returns>
		/// 		''' <remarks>This is intended for HTML representation.</remarks>
        public string WithNonBreakingSpaces(string strToFormat = null, bool withJuncture_A = false) {
            if (strToFormat != null)
                return format(strToFormat.Replace(" ", "&nbsp;"), withJuncture_A);
            else
                return format(_Alias.Replace(" ", "&nbsp;"), withJuncture_A);

        }

        /// <summary>
        /// 		''' The Text with the first letter of each word Capitalized.
        /// 		''' </summary>
        /// 		''' <returns>The alias from the database with the first letter of each word capitalized.</returns>
        /// 		''' <remarks></remarks>
        public string Capitalized(bool withJuncture_A = false) {
            if (_TextCapital == null) {
                _TextCapital = format(_Alias, false);
                _JText = format(_Alias, true);
            }
            return getTextCapitalized(_Alias, withJuncture_A);
        }
        /// <summary>
		/// 		''' Capitalizes and removes any "nbsp;" from the alias name.
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>A string which has the first leter capitalized and does not contain non-breaking spaces (nbsp;) where spaces exist.</returns>
		/// 		''' <remarks></remarks>
        public string CapitalizedWithNonBreakingSpaces(bool withJuncture_A = false) {

            return getTextCapitalized(WithNonBreakingSpaces(null, withJuncture_A));

        }
        /// <summary>
		/// 		''' Pluralizes and capitalizes the name of the alias. 
		/// 		''' </summary>
		/// 		''' <value></value>
		/// 		''' <returns>A string which has the first letter capitalized and the appropriate characters to make it plural.</returns>
		/// 		''' <remarks></remarks>
        public string PluralAndCapitalized {
            get {
                return getTextCapitalized(Plural);
            }
        }
        /// <summary>
        ///         ''' Pluralizes the name of the alias.
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns>A string containing the name of the alias with the appropriate characters added on to make it plural.</returns>
        ///         ''' <remarks></remarks>
		public string Plural {
            // Plurals never need Junctures; it makes no sence to say "an apples" or "a pears"
            get {
                return PluralityDictionary.getPlurality(format(_Alias));
            }
        }
        /// <summary>
        ///         ''' Pluralizes the name of the alias and removes any non-breaking spaces from the alias name. 
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns>A string which has the appropriate characters to make the name plural
        ///         ''' and does not contain "nbsp;" where a space is found.
        ///         ''' </returns>
        ///         ''' <remarks></remarks>
		public string PluralWithNonBreakingSpaces(bool withJuncture_A = false) {
            return PluralityDictionary.getPlurality(format(WithNonBreakingSpaces(null, withJuncture_A)));
        }
        /// <summary>
        ///         ''' Capitalizes, pluralizes and removes all non breaking spaces from the name of the alias.
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns>A string that is capitalized, plurized and contains no non-breaking spaces from the name of the alias.</returns>
        ///         ''' <remarks></remarks>
		public string PluralAndCapitalizedWithNonBreakingSpaces() {

            return getTextCapitalized(format(PluralWithNonBreakingSpaces()));

        }
        public string PastTense {
            // verbs do not need Junctures; it would not make sence to say "I had a watched the game."
            get {
                return VerbDictionary.getPastTense(Text());
            }
        }
        public string PastTenseAndCapitalized {
            get {
                return getTextCapitalized(PastTense);
            }
        }
        public string Gerund(bool withJuncture_A = false) {
            // Gerunds do need Junctions "I went to a swimming meet."

            return VerbDictionary.getGerund(Text(withJuncture_A));

        }
        public string GerundAndCapitalized(bool withJuncture_A = false) {

            return getTextCapitalized(Gerund(withJuncture_A));

        }
        /// <summary>
		/// 		''' Add the Juncture or "a" or "an" to the start of the alias.
		/// 		''' For example "an apple" or "a pear".
		/// 		''' </summary>
		/// 		''' <param name="text"></param>
		/// 		''' <returns></returns>
		/// 		''' <remarks></remarks>
        private string getWithJunctureA(string text, bool withNonBreakingSpaces) {
            string textToCompare = text.ToLower();
            string juncText = "a";
            string spaceText = " ";
            if (withNonBreakingSpaces)
                spaceText = "&nbsp;";
            if (text.Contains('^'))
                textToCompare = text.Replace("^", "");
            // may need to create a dictionary for handling exceptions like hotel, one-armed, etc.
            char firstChar; // , secondChar, thirdChar As Char
            firstChar = textToCompare[0];
            if (_Vowels.Contains(firstChar)) {
                bool isHard = false;
                foreach (string hw in _HardWords) {
                    if (textToCompare.StartsWith(hw)) {
                        isHard = true;
                        break;
                    }
                }
                if (!isHard)
                    juncText = "an";
            } else
                foreach (string sw in _SoftWords) {
                    if (textToCompare.StartsWith(sw)) {
                        juncText = "an";
                        break;
                    }
                }
            return juncText + spaceText + text;
        }
        private string getWithJunctureA(string text) {
            return getWithJunctureA(text, false);
        }
        /// <summary>
		/// 		''' Capitalizes the string passed to it.
		/// 		''' </summary>
		/// 		''' <param name="name"></param>
		/// 		''' <returns></returns>
		/// 		''' <remarks></remarks>
        private string getTextCapitalized(string name, bool withJuncture_A) {
            Array myArr;
            bool wBS = name.Contains("&nbsp;");
            if (withJuncture_A)
                name = getWithJunctureA(name, wBS);
            if (wBS) {
                string[] splitter = new[] { "&nbsp;" };
                myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            } else {
                char[] splitter = new[] { ' ' };
                myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            }
            name = "";
            foreach (string s in myArr) {
                if (s.Length > 1)
                    name += s.Substring(0, 1).ToUpper() + s.Substring(1);
                else
                    name += s.ToUpper();
                if (wBS)
                    name += "&nbsp;";
                else
                    name += " ";
            }
            if (name.Length > 5 && name.Substring(name.Length - 6) == "&nbsp;")
                name = name.Substring(0, name.Length - 6);
            return format(name.Trim());
        }
        private string getTextCapitalized(string name) {
            Array myArr;
            bool wBS = name.Contains("&nbsp;");
            if (wBS) {
                string[] splitter = new[] { "&nbsp;" };
                myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            } else {
                char[] splitter = new[] { ' ' };
                myArr = name.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            }
            name = "";
            foreach (string s in myArr) {
                if (s.Length > 1)
                    name += s.Substring(0, 1).ToUpper() + s.Substring(1);
                else
                    name += s.ToUpper();
                if (wBS)
                    name += "&nbsp;";
                else
                    name += " ";
            }
            if (name.Length > 5 && name.Substring(name.Length - 6) == "&nbsp;")
                name = name.Substring(0, name.Length - 6);
            return format(name.Trim());
        }
        /// <summary>
		/// 		''' Capitalizes the next letter in the name of the alias if a carrot("^") is in front of it.
		/// 		''' </summary>
		/// 		''' <param name="retString"></param>
		/// 		''' <returns>A string which capitalizes letters if there is a "^" character in front of it.</returns>
		/// 		''' <remarks></remarks>
        private string format(string retString, bool withJuncture_A) {
            if (withJuncture_A)
                retString = getWithJunctureA(retString);
            if (retString.Contains("^")) {
                int carrotLocation;
                carrotLocation = retString.IndexOf('^');
                if (carrotLocation < retString.Length - 1) {
                    retString = retString.Substring(0, carrotLocation) + retString.Substring(carrotLocation + 1, 1).ToUpper()
+ retString.Substring(carrotLocation + 2);
                    return format(retString);
                } else
                    return retString.Substring(0, retString.Length - 1);
            } else
                return retString;
        }
        private string format(string retString) {
            if (retString.Contains("^")) {
                int carrotLocation;
                carrotLocation = retString.IndexOf('^');
                if (carrotLocation < retString.Length - 1) {
                    retString = retString.Substring(0, carrotLocation) + retString.Substring(carrotLocation + 1, 1).ToUpper()
+ retString.Substring(carrotLocation + 2);
                    return format(retString);
                } else
                    return retString.Substring(0, retString.Length - 1);
            } else
                return retString;
        }

        public override string ToString() {
            return Capitalized();
        }
        /// <summary>
		/// 		''' Sets the default text if to "[insert_alias]" if not passed any string variable.
		/// 		''' </summary>
		/// 		''' <param name="startingText">Optional string that can be the default for unspecified aliases.</param>
		/// 		''' <remarks></remarks>
        public NameAlias(string startingText) {
            setText(startingText);
        }
        private void setText(string myText) {
            if (myText == "")
                myText = "[insert_alias]";
            _Alias = myText;
        }
        /// <summary>
        ///         ''' Sets the default text if to "[insert_alias]" if not passed any string variable.
        ///         ''' </summary>
        ///         ''' <param name="startingText">Optional string that can be the default for unspecified aliases.</param>
        ///         ''' <param name="applyFormatting">True if text needs formatting applied from string</param>
        ///         ''' <remarks></remarks>
        public NameAlias(string startingText, bool applyFormatting) {
            if (applyFormatting)
                setText(getTextWithFormatting(startingText));
            else
                setText(startingText);
        }
        public static string getTextWithFormatting(string myText) {
            string newString = "";
            foreach (char myChar in myText.ToCharArray()) {
                if (char.IsUpper(myChar))
                    newString += "^";
                newString += myChar;
            }
            return newString;
        }
        public NameAlias() {
            _Alias = "[insert_alias]";
        }
    }
}

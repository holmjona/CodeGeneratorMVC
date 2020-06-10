using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace Tools {
    /// <summary>
	/// 	''' Generates Passwords. Default Level is strong. Default Length is 8.
	/// 	''' </summary>
	/// 	''' <remarks>Default Level is strong and Length is 8.</remarks>
    public class PasswordGenerator {
        private int passwordLength = 8;
        private Dictionary<char, string> phoneticDict = new Dictionary<char, string>() {
            ['a'] = "alpha",
            ['b'] = "bravo",
            ['c'] = "charley",
            ['d'] = "delta",
            ['e'] = "echo",
            ['f'] = "foxtrot",
            ['g'] = "golf",
            ['h'] = "hotel",
            ['i'] = "igloo",
            ['j'] = "juliet",
            ['k'] = "kilo",
            ['l'] = "lima",
            ['m'] = "mike",
            ['n'] = "november",
            ['o'] = "oscar",
            ['p'] = "papa",
            ['q'] = "quebec",
            ['r'] = "romeo",
            ['s'] = "sierra",
            ['t'] = "tango",
            ['u'] = "uniform",
            ['v'] = "victor",
            ['w'] = "whiskey",
            ['x'] = "x~ray",
            ['y'] = "yankee",
            ['z'] = "zulu",
            ['A'] = "ALPHA",
            ['B'] = "BRAVO",
            ['C'] = "CHARLEY",
            ['D'] = "DELTA",
            ['E'] = "ECHO",
            ['F'] = "FOXTROT",
            ['G'] = "GOLF",
            ['H'] = "HOTEL",
            ['I'] = "IGLOO",
            ['J'] = "JULIET",
            ['K'] = "KILO",
            ['L'] = "LIMA",
            ['M'] = "MIKE",
            ['N'] = "NOVEMBER",
            ['O'] = "OSCAR",
            ['P'] = "PAPA",
            ['Q'] = "QUEBEC",
            ['R'] = "ROMEO",
            ['S'] = "SIERRA",
            ['T'] = "TANGO",
            ['U'] = "UNIFORM",
            ['V'] = "VICTOR",
            ['W'] = "WHISKEY",
            ['X'] = "X~RAY",
            ['Y'] = "YANKEE",
            ['Z'] = "ZULU",
            ['1'] = "One",
            ['2'] = "Two",
            ['3'] = "Three",
            ['4'] = "Four",
            ['5'] = "Five",
            ['6'] = "Six",
            ['7'] = "Seven",
            ['8'] = "Eight",
            ['9'] = "Nine",
            ['0'] = "Zero",
            ['!'] = "{Exclamation}",
            ['@'] = "{At}",
            ['#'] = "{Pound}",
            ['$'] = "{Dollar}",
            ['%'] = "{Percent}",
            ['^'] = "{Caret}",
            ['&'] = "{Ampersand}",
            ['*'] = "{Asterisk}",
            ['('] = "{Left Parenthesis}",
            [')'] = "{Right Parenthesis}",
            ['_'] = "{Underscore}",
            ['='] = "{Equal}",
            ['+'] = "{Plus}",
            ['-'] = "{Hyphen}",
            ['/'] = "{Slash}",
            ['~'] = "{Tilde}",
            ['{'] = "{Left Brace}",
            ['}'] = "{Right Brace}",
            ['['] = "{Left Box}",
            [']'] = "{Right Box}",
            ['|'] = "{Pipe}",
            ['.'] = "{Period}",
            [','] = "{Comma}",
            ['?'] = "{Question Mark}",
            [Strings.Chr(34)] = "{Quotation}",
            [Strings.Chr(39)] = "{Apostrophe}",
            ['<'] = "{Greater Than}",
            ['>'] = "{Less Than}",
        };
        const string lowerLetters = "abcdefghijkmnopqrstuvwxyz";
        const string upperLetters = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        const string numberChars = "23456789";
        const string specialChars = "!@#$%^&*()_=+-/";
        /// <summary>
		/// 		''' Creates object and calls Randomize().
		/// 		''' </summary>
		/// 		''' <remarks>The Randomize call is to make sure that new passwords are created, making them more difficult to guess.</remarks>
        public PasswordGenerator() : base() {
            VBMath.Randomize(DateTime.Now.TimeOfDay.TotalSeconds);
        }
        /// <summary>
		/// 		''' The complexity of a password.
		/// 		''' </summary>
		/// 		''' <remarks>
		/// 		''' Weak uses only lowercase letters.
		/// 		''' Semi uses upper and lower case letters.
		/// 		''' Strong uses upper and lower case letters and numbers.
		/// 		''' Very Strong uses upper and lower case letters and numbers and special characters.
		/// 		''' </remarks>
        public enum passwordComplexity : int {
            /// <summary>
			/// 			''' use just lowercase letters
			/// 			''' </summary>
			/// 			''' <remarks></remarks>
            Level1 = 1,
            /// <summary>
			/// 			''' use lowercase and UPPERCASE letters
			/// 			''' </summary>
			/// 			''' <remarks></remarks>
            Level2 = 2,
            /// <summary>
			/// 			''' use lowercase, UPPERCASE, and numbers
			/// 			''' </summary>
			/// 			''' <remarks></remarks>
            Level3 = 3,
            /// <summary>
			/// 			''' use lowercase, UPPERCASE, numbers, and special characters
			/// 			''' </summary>
			/// 			''' <remarks></remarks>
            Level4 = 4
        }
        /// <summary>
		/// 		''' The level of complexity of a password.
		/// 		''' </summary>
        public enum passwordStrength {
            Weak = 1,
            Semi = 2,
            Strong = 3,
            VeryStrong = 4
        }
        /// <summary>
		/// 		''' Get a Random number between 1 and the given number.
		/// 		''' </summary>
		/// 		''' <param name="Sides">The max random value to use.</param>
		/// 		''' <returns>A random integer between 1 and the given number of Sides.</returns>
		/// 		''' <remarks>Acts like a Sides die, you specify the number of Sides and it will return a random roll from that die.</remarks>
        private int rollDie(int Sides) {
            return System.Convert.ToInt32(Math.Floor(VBMath.Rnd() * Sides)) + 1;
        }
        /// <summary>
		/// 		''' Generate Strong Password with 8 characters.
		/// 		''' </summary>
		/// 		''' <returns>Password string with upper and lower case letters and numbers. </returns>
        public string generatePassword() {
            return generatePassword(passwordLength, passwordComplexity.Level3);
        }
        /// <summary>
		/// 		''' Generate Password at the desired strength and 8 characters.
		/// 		''' </summary>
		/// 		''' <param name="strength">The strength of the password to generate.</param>
		/// 		''' <returns>Password String that with 8 characters of the strength you specified.</returns>
        public string generatePassword(passwordComplexity strength) {
            return generatePassword(passwordLength, strength);
        }
        /// <summary>
		/// 		''' Generate Strong Password with the number of characters equalling the given amount.
		/// 		''' </summary>
		/// 		''' <param name="length">Integer length of the number of characters in the password.</param>
		/// 		''' <returns>Password string at length specified using upper and lower case letters and number.</returns>
        public string generatePassword(int length) {
            return generatePassword(length, passwordComplexity.Level3);
        }
        /// <summary>
		/// 		''' Generate Password at desired strength and desired number of characters.
		/// 		''' </summary>
		/// 		''' <param name="length">Integer length of the password string.</param>
		/// 		''' <param name="complex">Password Strength that password needs to be.</param>
		/// 		''' <returns>Password String of given length and strength given.</returns>
        public string generatePassword(int length, passwordComplexity complex) {
            System.Text.StringBuilder passArray = new System.Text.StringBuilder();
            int newIndex;
            for (int a = 1; a <= length; a++) {
                newIndex = rollDie(100);
                int charType = rollDie((int)complex);
                string list;
                switch (charType) {
                    case object _ when charType == 1: {
                            list = lowerLetters;
                            break;
                        }

                    case object _ when charType == 2: {
                            list = upperLetters;
                            break;
                        }

                    case object _ when charType == 3: {
                            list = numberChars;
                            break;
                        }

                    default: {
                            list = specialChars;
                            break;
                        }
                }
                passArray.Append(getCharAtIndexWrapping(newIndex, list));
            }
            if (checkStrength(passArray.ToString(), complex))
                return passArray.ToString();
            else
                return generatePassword(length, complex);
        }
        /// <summary>
		/// 		''' Get the character at the given index of a string.
		/// 		''' </summary>
		/// 		''' <param name="index">0 based index of the character desired.</param>
		/// 		''' <param name="list">String to retrieve character from.</param>
		/// 		''' <returns>Character at given index.</returns>
        private char getCharAtIndexWrapping(int index, string list) {
            if (index > list.Length - 1)
                return getCharAtIndexWrapping(index - list.Length, list);
            if (index < 0)
                return getCharAtIndexWrapping(index + list.Length, list);
            return list[index];
        }
        /// <summary>
		/// 		''' Get if the password is equivelent to the desired strength.
		/// 		''' </summary>
		/// 		''' <param name="password"></param>
		/// 		''' <param name="strength"></param>
		/// 		''' <returns></returns>
		/// 		''' <remarks></remarks>
        private bool checkStrength(string password, passwordComplexity strength) {
            return getPasswordComplexity(password) >= strength;
        }
        /// <summary>
		/// 		''' Get the strength of a password.
		/// 		''' </summary>
		/// 		''' <param name="password">String representation of a password.</param>
		/// 		''' <returns>The Stength Enumerator relating to a given password.</returns>

        public passwordStrength getPasswordStrength(string password) {
            int stn;
            stn = (int)getPasswordComplexity(password);
            if (password.Length < 8)
                stn -= (8 - password.Length);
            if (stn < 1)
                stn = 1;
            return (passwordStrength)stn;
        }
        /// <summary>
		/// 		''' Returns the level of complexity a password passed to it is.
		/// 		''' </summary>
		/// 		''' <param name="password">The password to check.</param>
        public passwordComplexity getPasswordComplexity(string password) {
            int stG = 0;
            if (containsChar(password, lowerLetters))
                stG += 1;
            if (containsChar(password, upperLetters))
                stG += 1;
            if (containsChar(password, numberChars))
                stG += 1;
            if (containsChar(password, specialChars))
                stG += 1;
            return (passwordComplexity)stG;
        }
        /// <summary>
		/// 		''' Determines if any of the characters contained in the list are contained in the string.
		/// 		''' </summary>
		/// 		''' <param name="str">The string to check.</param>
		/// 		''' <param name="list">The list of strings to check for to see if they exist.</param>
		/// 		''' <returns>True if one of the characters contains a character in the list.</returns>
        private bool containsChar(string str, string list) {
            foreach (char c in str) {
                if (list.Contains(c.ToString()))
                    return true;
            }
            return false;
        }
        /// <summary>
		/// 		''' Creates a phonetic of the string passed to it using a dictionary that refers to each letter in alphabet.
		/// 		''' </summary>
		/// 		''' <param name="str">The string to make a phonetic of.</param>
		/// 		''' <returns>Sting of phonetics separated by a " - ". ex. [alpha - bravo - charley]</returns>
        public string getPhonetics(string str) {
            StringBuilder retStr = new StringBuilder();
            int count = 0;
            int len = str.Length;
            retStr.Append("[");
            foreach (char c in str) {
                retStr.Append(phoneticDict[c]);
                count += 1;
                if (count < len)
                    retStr.Append(" - ");
            }
            retStr.Append("]");
            return retStr.ToString();
        }
        /// <summary>
		/// 		''' Creates a phonetic dictionary for each letter in alphabet as well as some numbers and special characters.
		/// 		''' </summary>
        private static Dictionary<char, string> getDict() {
            Dictionary<char, string> d = 
            return d;
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Tools.SoundEx {
    /// <summary>
	/// 	''' A SoundEx Algorithm that I made to be able to handle some of our needs.
	/// 	''' #############################################################################
	/// 	''' #   This is basically the soundex algorithm, but I accounted
	/// 	''' #      for and handle plurality.
	/// 	''' # Ref: http://www.unixreview.com/documents/s=7458/uni1026336632258/0207e.htm
	/// 	''' #############################################################################
	/// 	''' </summary>
	/// 	''' <author>Jonathan D. Holmes</author>
	/// 	''' <orginialdate> 29 Aug 2006 </orginialdate>
	/// 	''' <remarks>Because I made this to handle plurality, which does not
	/// 	''' seem to be normal for SoundEx I named it JonDex.</remarks>
    public class JonDex {
        /// <summary>
		/// 		''' Compare the similarity of two JonDex Strings. 
		/// 		''' Similar to Microsoft's DIFFERENCE function in SQL Server.
		/// 		''' </summary>
		/// 		''' <param name="str1">String to compare with the second</param>
		/// 		''' <param name="str2">String to compare with the first</param>
		/// 		''' <returns>Integer value of the similarity of two JonDex Strings.</returns>
		/// 		''' <remarks>The value returned is between 0 and 4. 4 means identical, 0 is 
		/// 		''' the most different.</remarks>
        public static int Difference(string str1, string str2, bool compareFirstLetter = true) {
            // 
            int counter = 0;
            string jDex1 = Dex(str1);
            string jDex2 = Dex(str2);
            for (int a = 0; a <= jDex1.Length - 1; a++) {
                if (a == 0 && compareFirstLetter) {
                    // what if the first letters are comparable like c and k? this should handle that.
                    if (getCharKey(jDex1[0], '#') == getCharKey(jDex2[0], '#'))
                        counter += 1;
                } else if (jDex1[a] == jDex2[a])
                    counter += 1;// chars match
            }
            return counter;
        }
        /// <summary>
		/// 		''' Makes a SoundEx String that handles some if not most plurality. 
		/// 		''' </summary>
		/// 		''' <param name="str">String that JonDex is to derived from.</param>
		/// 		''' <returns>The SoundEx value for a String(JonDex)</returns>
		/// 		''' <remarks></remarks>
        public static string Dex(string str) {
            // #############################################################################
            // #   This is basically the soundex algorithm, but I accounted
            // #      for and handle plurality.
            // # Ref: http://www.unixreview.com/documents/s=7458/uni1026336632258/0207e.htm
            // #############################################################################
            if (str.Length < 1)
                return "0000";
            string strToReturn = "";
            str = str.ToLower();
            char firstChar = str[0];
            // remove starting characters that are not a letter. 
            while (firstChar < 'a' || firstChar > 'z') {
                // char is not a letter.
                if (str.Length == 1) {
                    // no characters found in string 
                    return "0000";
                } else {
                    // omit first char and try next char.
                    str = str.Substring(1);
                    // get next first character.
                    firstChar = str[0];
                }
            }

            if (str[str.Length - 1] == 's')
                str = str.Substring(0, str.Length - 1);
            foreach (char chStr in str) {
                if (strToReturn.Length < 4) {
                    int key = getCharKey(chStr, firstChar);
                    if (key > 0) {
                        strToReturn += key.ToString();
                    } else if (key == -2) {
                        firstChar = chStr;
                    } else {

                    }
                } else
                    break;
            }
            if (strToReturn.Length < 4) {
                for (int a = strToReturn.Length; a <= 3; a++)
                    strToReturn += "0";
            }
            return strToReturn;
        }
        /// <summary>
		/// 		''' 
		/// 		''' </summary>
		/// 		''' <param name="chr"></param>
		/// 		''' <param name="lastchar"></param>
		/// 		''' <returns></returns>
		/// 		''' <remarks></remarks>
        private static int getCharKey(char chr, char lastchar) {
            // TODO: does this Dex handle the 'sw' as in sword or answer?
            if (chr == lastchar) {
                // ignore repeating letters, or is that leters
            } else if ("aeiouy".Contains(chr)) {
                // ignore vowels
            } else if ("bfpv".Contains(chr)) {
                if ("bfpv".Contains(lastchar)) {
                    // ignore duplicate
                } else {
                    return 1; // first sound
                }
            } else if ("cgjkqsxz".Contains(chr)) {
                if ("cgjkqsxz".Contains(lastchar)){
                    // ignore duplicate
                } else {
                    return 2; // second sound
                }
            } else if ("dt".Contains(chr)) {
                if ("dt".Contains(chr)) {
                    // ignore duplicate
                } else {
                    return 3; // third sound
                }
            } else if (chr == 'l') {
                return 4; // forth sound
            } else if ("mn".Contains(chr)){
                if ("mn".Contains(lastchar)) {
                    // ignore duplicate
                } else {
                    return 5; // fifth sound
                }
            } else if (chr == 'r') {
                return 6; // sixth sound
            } else if ("hw".Contains(chr)) {
                // ignore soft sounds
            } else {
                return -2;
            }
            return -1; // should never hit this.
        }
    }
}

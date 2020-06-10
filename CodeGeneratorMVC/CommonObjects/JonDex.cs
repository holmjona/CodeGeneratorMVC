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
                    if (getCharKey(jDex1.Chars[0], '#') == getCharKey(jDex2[0], '#'))
                        counter += 1;
                } else if (jDex1.Chars[a] == jDex2.Chars[a])
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
            char lastChar = str.Chars[0];
            strToReturn = lastChar.ToString();
            while (strToReturn < "a" | strToReturn > "z") {
                if (str.Length == 1) {
                    if (strToReturn < "a" | strToReturn > "z")
                        return "0000";
                    else
                        return str.Substring(0, 1) + "000";
                } else {
                    strToReturn = str.Substring(0, 1);
                    str = str.Substring(1);
                    lastChar = str.Chars[0];
                }
            }

            if (str.Chars[str.Length - 1] == 's')
                str = str.Substring(0, str.Length - 1);
            foreach (char chStr in str) {
                if (strToReturn.Length < 4) {
                    int key = getCharKey(chStr, lastChar);
                    switch (key) {
                        case object _ when key > 0: {
                                strToReturn += key.ToString();
                                break;
                            }

                        case object _ when key == -2: {
                                lastChar = chStr;
                                break;
                            }

                        default: {
                                break;
                            }
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
            switch (chr) {
                case object _ when chr == lastchar // ignore repeating letters, or is that leters
               : {
                        break;
                        break;
                    }

                case object _ when chr == 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                case 'y': {
                        break;
                        break;
                    }

                case object _ when chr == 'b':
                case 'f':
                case 'p':
                case 'v': {
                        switch (lastchar) {
                            case object _ when lastchar == 'b':
                            case 'f':
                            case 'p':
                            case 'v': {
                                    break;
                                    break;
                                }

                            default: {
                                    return 1;
                                }
                        }
                        break;
                        break;
                    }

                case object _ when chr == 'c':
                case 'g':
                case 'j':
                case 'k':
                case 'q':
                case 's':
                case 'x':
                case 'z': {
                        switch (lastchar) {
                            case object _ when lastchar == 'c':
                            case 'g':
                            case 'j':
                            case 'k':
                            case 'q':
                            case 's':
                            case 'x':
                            case 'z': {
                                    break;
                                    break;
                                }

                            default: {
                                    return 2;
                                }
                        }
                        break;
                        break;
                    }

                case object _ when chr == 'd':
                case 't': {
                        switch (lastchar) {
                            case object _ when lastchar == 'd':
                            case 't': {
                                    break;
                                    break;
                                }

                            default: {
                                    return 3;
                                }
                        }
                        break;
                        break;
                    }

                case object _ when chr == 'l': {
                        return 4;
                        break;
                        break;
                    }

                case object _ when chr == 'm':
                case 'n': {
                        switch (lastchar) {
                            case object _ when lastchar == 'm':
                            case 'n': {
                                    break;
                                    break;
                                }

                            default: {
                                    return 5;
                                }
                        }
                        break;
                        break;
                    }

                case object _ when chr == 'r': {
                        return 6;
                        break;
                        break;
                    }

                default: {
                        break;
                    }
            }
            switch (chr) {
                case object _ when chr == 'h':
                case 'w': {
                        break;
                        break;
                    }

                default: {
                        // lastChar = chStr
                        return -2;
                    }
            }
            return -1;
        }
    }
}

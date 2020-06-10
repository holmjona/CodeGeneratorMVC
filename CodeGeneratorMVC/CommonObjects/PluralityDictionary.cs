using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Words {
    public class PluralityDictionary {
        private static Dictionary<string, string> _ListOfExceptions;
        /// <summary>
        ///         ''' get the plural version of text.
        ///         ''' </summary>
        ///         ''' <param name="str"></param>
        ///         ''' <returns>return the lowercase plural form of the given string.</returns>
        ///         ''' <remarks>The string will always be converted to lowercase.</remarks>
        public static string getPlurality(string str) {
            // ' We assume the string will aways come formatted, no carets or other formating symbols.
            // ' therefore we need to keep track of the places that are capitalized.
            List<int> listOCaps = new List<int>();
            bool lastCharWasCAPS = false;
            int chrInd = 0;
            foreach (char ch in str) {
                if (char.IsUpper(ch)) {
                    listOCaps.Add(chrInd);
                    lastCharWasCAPS = chrInd == str.Length - 1;
                }
                chrInd += 1;
            }
            // str = str.ToLower
            string lowStr;
            // force string to lower cas
            lowStr = str.ToLower();
            // see if contains breaking spaces
            bool wBS = lowStr.Contains("&nbsp;");
            string[] spStr = new string[2];

            // if contains breaking spaces then set chars to split on appropriately
            if (wBS)
                spStr[0] = "&nbsp;";
            else
                spStr[0] = " ";

            // get results from split
            string[] wordArr;
            wordArr = lowStr.Split(spStr, System.StringSplitOptions.RemoveEmptyEntries);

            // set word to check plurality on as last word in the list
            // make no sence to do "twenties apples" ; should be "twenty apples".
            lowStr = wordArr[wordArr.Count() - 1];
            string newString = "";
            int lastWordIndex = wordArr.Count() - 1;
            for (int i = 0; i <= lastWordIndex; i++) {
                if (i == lastWordIndex) {
                    // see if is in exeptions list
                    if (ListOfExceptions.ContainsKey(lowStr))
                        // if so, get the exeption version
                        newString += ListOfExceptions[lowStr];
                    else
                        // if not, get the plural version
                        newString += getTextPluralized(lowStr);
                } else
                    newString += wordArr[i] + spStr[0];
            }

            // ' convert capital letters back to from the original string
            chrInd = 0;
            string retStr = "";
            foreach (char ch in newString) {
                if (listOCaps.Contains(chrInd) || (lastCharWasCAPS && chrInd > listOCaps.Max()))
                    retStr += char.ToUpper(ch);
                else
                    retStr += ch;
                chrInd += 1;
            }
            return retStr;
        }
        /// <summary>
        ///         ''' get the singular version of a plural text.
        ///         ''' </summary>
        ///         ''' <param name="str"></param>
        ///         ''' <returns>return the lowercase plural form of the given string.</returns>
        ///         ''' <remarks>The string will always be converted to lowercase.</remarks>
        public static string getPluralityInverse(string str) {
            // str = str.ToLower
            string lowStr;
            // force string to lower cas
            lowStr = str.ToLower();
            // see if contains breaking spaces
            bool wBS = lowStr.Contains("&nbsp;");
            List<string> spStr = new List<string>();

            // if contains breaking spaces then set chars to split on appropriately
            if (wBS)
                spStr.Add("&nbsp;");
            else
                spStr.Add(" ");

            // get results from split
            string[] stArr;
            stArr = lowStr.Split(spStr.ToArray(), System.StringSplitOptions.RemoveEmptyEntries);

            // set word to check plurality on as last word in the list
            // make no sence to do "twenties apples" ; should be "twenty apples".
            lowStr = stArr[stArr.Count() - 1];
            string retStr = "";
            for (int i = 0; i <= stArr.Count() - 1; i++) {
                if (i == stArr.Count() - 1) {
                    // see if is in exeptions list
                    bool exceptionExists = false;
                    foreach (KeyValuePair<string, string> pair in ListOfExceptions) {
                        if (pair.Value == lowStr) {
                            retStr += pair.Key;
                            exceptionExists = true;
                        }
                    }
                    if (exceptionExists)
                        continue;
                    // if not, get the plural version
                    retStr += getTextPluralizedInverse(lowStr);
                } else
                    retStr += stArr[i] + " ";
            }
            return retStr;
        }
        /// <summary>
        ///         ''' pluralize text based on basic english rules
        ///         ''' </summary>
        ///         ''' <param name="name"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        private static string getTextPluralized(string name) {
            if (name.Length > 2) {
                char lastChar;
                lastChar = System.Convert.ToChar(name.Substring(name.Length - 1));
                switch (lastChar) {
                    case object _ when lastChar == 'y': {
                            if (name.Substring(name.Length - 2) == "ey")
                                // Return Format(name) & "s"
                                break;
                            return Strings.Format(name.Substring(0, name.Length - 1) + "ies");
                        }

                    case object _ when lastChar == 's': {
                            if (name.Substring(name.Length - 2) == "ss")
                                name += "e";
                            break;
                        }

                    case object _ when lastChar == 'h': {
                            if (name.Substring(name.Length - 2) == "ch" | name.Substring(name.Length - 2) == "sh")
                                name += "e";
                            break;
                        }

                    case object _ when lastChar == 'x': {
                            name += "e";
                            break;
                        }

                    default: {
                            break;
                        }
                }
            }
            return Strings.Format(name) + "s";
        }
        /// <summary>
        ///         ''' pluralize text based on basic english rules
        ///         ''' </summary>
        ///         ''' <param name="name"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        private static string getTextPluralizedInverse(string name) {
            if (name.Length > 2) {
                if (name[name.Length - 1] == 's') {
                    if (name.Length > 4 && name.Substring(name.Length - 3) == "ies") {
                        string retString = name.Substring(0, name.Length - 3);
                        return retString.Insert(retString.Length, "y");
                    } else if (name.Length > 5 && name.Substring(name.Length - 4) == "sses")
                        return name.Substring(0, name.Length - 2);
                    else if (name.Length > 5 && name.Substring(name.Length - 4) == "ches")
                        return name.Substring(0, name.Length - 2);
                    else if (name.Length > 5 && name.Substring(name.Length - 4) == "shes")
                        return name.Substring(0, name.Length - 2);
                    else if (name.Length > 4 && name.Substring(name.Length - 3) == "xes")
                        return name.Substring(0, name.Length - 2);
                    else
                        return name.Substring(0, name.Length - 1);
                }
            }
            return name;
        }
        /// <summary>
        ///         ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ///         ''' <remarks></remarks>
        public static Dictionary<string, string> ListOfExceptions {
            get {
                if (_ListOfExceptions == null)
                    _ListOfExceptions = getPluralityExceptions();
                return _ListOfExceptions;
            }
        }
        /// <summary>
        ///         ''' A list of exceptions to basic rules of plurality in English.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ///         ''' <remarks></remarks>
        private static Dictionary<string, string> getPluralityExceptions() {
            Dictionary<string, string> retDict = new Dictionary<string, string>();
            retDict.Add("addendum", "addenda");
            retDict.Add("alga", "algae");
            retDict.Add("alumna", "alumnae");
            retDict.Add("alumnus", "alumni");
            retDict.Add("analysis", "analyses");
            retDict.Add("antenna", "antennae");
            retDict.Add("apparatus", "apparatuses");
            retDict.Add("appendix", "appendices");
            retDict.Add("axis", "axes");
            retDict.Add("bacillus", "bacilli");
            retDict.Add("bacterium", "bacteria");
            retDict.Add("basis", "bases");
            retDict.Add("beau", "beaux");
            retDict.Add("bison", "bison");
            retDict.Add("buffalo", "buffalos");
            retDict.Add("bureau", "bureaus");
            retDict.Add("bus", "buses");
            retDict.Add("cactus", "cacti");
            retDict.Add("calf", "calves");
            retDict.Add("child", "children");
            retDict.Add("corps", "corps");
            retDict.Add("corpus", "corpuses");
            retDict.Add("crisis", "crises");
            retDict.Add("criterion", "criteria");
            retDict.Add("curriculum", "curricula");
            retDict.Add("datum", "Data");
            retDict.Add("deer", "deer");
            retDict.Add("die", "dice");
            retDict.Add("dwarf", "dwarves");
            retDict.Add("diagnosis", "diagnoses");
            retDict.Add("echo", "echoes");
            retDict.Add("elf", "elves");
            retDict.Add("ellipsis", "ellipses");
            retDict.Add("embargo", "embargoes");
            retDict.Add("emphasis", "emphases");
            retDict.Add("erratum", "errata");
            retDict.Add("fireman", "firemen");
            retDict.Add("fish", "fishes");
            retDict.Add("focus", "focuses");
            retDict.Add("foot", "feet");
            retDict.Add("formula", "formulas");
            retDict.Add("fungus", "fungi");
            retDict.Add("genus", "genera");
            retDict.Add("goose", "geese");
            retDict.Add("half", "halves");
            retDict.Add("hero", "heroes");
            retDict.Add("hippopotamus", "hippopotami");
            retDict.Add("hoof", "hooves");
            retDict.Add("hypothesis", "hypotheses");
            retDict.Add("index", "indices");
            retDict.Add("knife", "knives");
            retDict.Add("leaf", "leaves");
            retDict.Add("life", "lives");
            retDict.Add("loaf", "loaves");
            retDict.Add("louse", "lice");
            retDict.Add("man", "men");
            retDict.Add("matrix", "matrices");
            retDict.Add("means", "means");
            retDict.Add("medium", "media");
            retDict.Add("media", "media");
            retDict.Add("memorandum", "memoranda");
            retDict.Add("millennium", "milennia");
            retDict.Add("moose", "moose");
            retDict.Add("mosquito", "mosquitoes");
            retDict.Add("mouse", "mice");
            retDict.Add("nebula", "nebulae");
            retDict.Add("neurosis", "neuroses");
            retDict.Add("nucleus", "nuclei");
            retDict.Add("oasis", "oases");
            retDict.Add("octopus", "octopi");
            retDict.Add("ovum", "ova");
            retDict.Add("ox", "oxen");
            retDict.Add("paralysis", "paralyses");
            retDict.Add("parenthesis", "parentheses");
            retDict.Add("person", "people");
            retDict.Add("phenomenon", "phenomena");
            retDict.Add("potato", "potatoes");
            retDict.Add("radius", "radii");
            retDict.Add("scarf", "scarves");
            retDict.Add("self", "selves");
            retDict.Add("series", "series");
            retDict.Add("sheep", "sheep");
            retDict.Add("shelf", "shelves");
            retDict.Add("scissors", "scissors");
            retDict.Add("software", "software");
            retDict.Add("species", "species");
            retDict.Add("stimulus", "stimuli");
            retDict.Add("stratum", "strata");
            retDict.Add("syllabus", "syllabi");
            retDict.Add("symposium", "symposia");
            retDict.Add("synthesis", "syntheses");
            retDict.Add("synopsis", "synopses");
            retDict.Add("tableau", "tableaux");
            retDict.Add("that", "those");
            retDict.Add("thesis", "theses");
            retDict.Add("thief", "thieves");
            retDict.Add("this", "these");
            retDict.Add("tomato", "tomatoes");
            retDict.Add("tooth", "teeth");
            retDict.Add("torpedo", "torpedoes");
            retDict.Add("vertebra", "vertebrae");
            retDict.Add("veto", "vetoes");
            retDict.Add("vita", "vitae");
            retDict.Add("watch", "watches");
            retDict.Add("wife", "wives");
            retDict.Add("wolf", "wolves");
            retDict.Add("woman", "women");
            retDict.Add("zero", "zeros");
            retDict.Add("status", "statuses");
            retDict.Add("patch", "patches");
            return retDict;
        }
    }
}

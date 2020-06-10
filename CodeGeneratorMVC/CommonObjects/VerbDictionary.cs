using System.Collections.Generic;
using System.Linq;

namespace Words {
    /// <summary>
    ///     ''' Class that contains exceptions to verb usage in the English language.
    ///     ''' </summary>
    ///     ''' <remarks>Use this for verbs and PluralityDictionary for Nouns</remarks>
    public class VerbDictionary {
        private static Dictionary<string, string> _List;
        private static List<string> _Const;
        private static List<string> _Vowels;
        private static Dictionary<string, string> _Past;

        private static void fillList() {
            if (_List == null)
                _List = getExcepts();
        }
        private static void fillConsonants() {
            if (_Const == null)
                _Const = getConsts();
        }
        private static void fillVowels() {
            if (_Vowels == null)
                _Vowels = getVowels();
        }
        private static void FillPastTenseExceptions() {
            if (_Past == null)
                _Past = getPastTenseExceptions();
        }
        /// <summary>
        ///         ''' get the plural version of text.
        ///         ''' </summary>
        ///         ''' <param name="str">present tense verb to make past tense.</param>
        ///         ''' <returns>return the lowercase plural form of the given string.</returns>
        ///         ''' <remarks>The string will always be converted to lowercase.</remarks>
        public static string getPastTense(string str) {
            string lowStr = str.ToLower();
            if (List.ContainsKey(lowStr))
                return List[lowStr].Replace(lowStr, str);
            else
                return getSimplePast(str);
        }
        /// <summary>
        ///         ''' Get the gerund value of a verb.
        ///         ''' </summary>
        ///         ''' <param name="str">present tense verb to make gerund.</param>
        ///         ''' <returns>String value of the gerund form of a verb.</returns>
        ///         ''' <remarks></remarks>
        public static string getGerund(string str) {
            // str = str.ToLower
            if (str.ToLower().Substring(str.Length - 1, 1) == "e")
                str = str.Substring(0, str.Length - 1);
            else if (str.Length > 2) {
                string e1, e2;
                // get last letter
                e1 = str.ToLower().Substring(str.Length - 1, 1);
                // get second to last letter
                e2 = str.ToLower().Substring(str.Length - 2, 1);
                // if last letter is a consonant and the second to last is a vowel
                if (Vowels.Contains(e2) && Consonants.Contains(e1))
                    // double last letter
                    str += e1;
            }
            return str + "ing";
        }
        /// <summary>
        ///         ''' pluralize text based on basic english rules
        ///         ''' </summary>
        ///         ''' <param name="name"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        private static string getSimplePast(string name) {
            int vowelCount = 0;
            fillVowels();
            fillConsonants();
            fillList();
            FillPastTenseExceptions();


            // count the amount of vowels in word
            foreach (char ch in name.ToCharArray()) {
                if (_Vowels.Contains(ch.ToString()))
                    vowelCount += 1;
            }



            // Check to see if word exists in past tense special case dictionary'
            if (_Past.ContainsKey(name))
                name = _Past[name];
            else if (name.ToLower().Substring(name.Length - 1, 1) == "e") {
                name = name.Substring(0, name.Length - 1);
                name = name.Insert(name.Length, "ed");
            } else if (name.ToLower().Substring(name.Length - 2, 2) == "ch")
                name = name.Insert(name.Length, "ed");
            else if (name[name.Length - 1] == 'x')
                name = name.Insert(name.Length, "ed");
            else if (name[name.Length - 1] == 'y' && _Const.Contains(name[name.Length - 2].ToString())) {
                name = name.Remove(name.Length - 1);
                name = name.Insert((name.Length), "ied");
            } else if (name[name.Length - 1] == 'y' && _Vowels.Contains(name[name.Length - 2].ToString()))
                name = name.Insert((name.Length), "ed");
            else if (vowelCount < 2 && _Const.Contains(name[name.Length - 1].ToString())
                             && _Vowels.Contains(name[name.Length - 2].ToString())
                             && _Const.Contains(name[name.Length - 3].ToString()))
                name = name.Insert(name.Length, name[name.Length - 1] + "ed");
            else
                // If word does not fall into any other category, add "ed"
                name = name.Insert(name.Length, "ed");

            return name; // & "ed"
        }
        /// <summary>
        ///         ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ///         ''' <remarks></remarks>
        public static Dictionary<string, string> List {
            get {
                fillList();
                return _List;
            }
        }
        /// <summary>
        ///         ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ///         ''' <remarks></remarks>
        public static List<string> Consonants {
            get {
                fillConsonants();
                return _Const;
            }
        }
        /// <summary>
        ///         ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ///         ''' <remarks></remarks>
        public static List<string> Vowels {
            get {
                fillVowels();
                return _Vowels;
            }
        }
        /// <summary>
        ///         ''' A list of exceptions to basic rules of plurality in English.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ///         ''' <remarks></remarks>
        private static Dictionary<string, string> getExcepts() {
            Dictionary<string, string> retHash = new Dictionary<string, string>();
            return retHash;
        }
        /// <summary>
        ///         ''' A list of exceptions to basic rules of plurality in English.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ///         ''' <remarks></remarks>
        private static List<string> getConsts() {
            List<string> retHash = new List<string>();
            retHash.Add("b");
            retHash.Add("c");
            retHash.Add("d");
            retHash.Add("f");
            retHash.Add("g");
            retHash.Add("h");
            retHash.Add("j");
            retHash.Add("k");
            retHash.Add("l");
            retHash.Add("m");
            retHash.Add("n");
            retHash.Add("p");
            retHash.Add("q");
            retHash.Add("r");
            retHash.Add("s");
            retHash.Add("t");
            retHash.Add("v");
            // retHash.Add("w") omitted because is not needed.
            // retHash.Add("x")	omitted because is not needed.
            // retHash.Add("y") omitted because is not needed.
            retHash.Add("z");

            return retHash;
        }
        /// <summary>
        ///         ''' A list of exceptions to basic rules of plurality in English.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ///         ''' <remarks></remarks>
        private static List<string> getVowels() {
            List<string> retHash = new List<string>();

            retHash.Add("a");
            retHash.Add("e");
            retHash.Add("i");
            retHash.Add("o");
            retHash.Add("u");

            return retHash;
        }

        /// <summary>
        ///         ''' A list of exceptions to basic rules of past tense in English.
        ///         ''' </summary>
        ///         ''' <returns>Dictionary of exceptions to the english rules of past tense.</returns>
        ///         ''' <remarks></remarks>
        private static Dictionary<string, string> getPastTenseExceptions() {
            // Dim dictionary As New Dictionary(Of String, String)
            // {{"become", "became"}, {"begin", "began"}, {"blow", "blew"}, {"break", "broke"}, {"bring", "brought"}, {"build", "built"}, {"burst", "burst"}, {"buy", "bought"}, {"catch", "caught"}, {"choose", "chose"}, {"come", "came"}, {"cut", "cut"}, {"deal", "dealt"}, {"do", "did"}, {"drink", "drank"}, {"drive", "drove"}, {"eat", "ate"}, {"fall", "fell"}, {"feed", "fed"}, {"feel", "felt"}, {"fight", "fought"}, {"find", "found"}, {"fly", "flew"}, {"forbid", "forbade"}, {"forget", "forgot"}, {"forgive", "forgave"}, {"freeze", "froze"}, {"get", "got"}, {"give", "gave"}, {"go", "went"}, {"grow", "grew"}, {"have", "had"}, {"hear", "heard"}, {"hide", "hid"}, {"hold", "held"}, {"hurt", "hurt"}, {"keep", "kept"}, {"know", "knew"}, {"lay", "laid"}, {"lead", "led"}, {"let", "let"}, {"leave", "left"}, {"lie", "lay"}, {"lose", "lost"}, {"make", "made"}, {"meet", "met"}, {"pay", "paid"}, {"quit", "quit"}, {"read", "read"}, {"ride", "rode"}, {"run", "ran"}, {"say", "said"}, {"see", "saw"}, {"seek", "sought"}, {"sell", "sold"}, {"send", "sent"}, {"shake", "shook"}, {"shine", "shone"}, {"sing", "sang"}, {"sit", "sat"}, {"sleep", "slept"}, {"speak", "spoke"}, {"spend", "spent"}, {"spring", "sprang"}, {"stand", "stood"}, {"steal", "stole"}, {"swim", "swam"}, {"swing", "swung"}, {"take", "took"}, {"teach", "taught"}, {"tear", "tore"}, {"tell", "told"}, {"think", "thought"}, {"throw", "threw"}, {"understand", "understood"}, {"wake", "woke (waked)"}, {"wear", "wore"}, {"win", "won"}, {"write", "wrote"}}
            var dictionary = new Dictionary<string, string>() { { "become", "became" }, { "begin", "began" }, { "blow", "blew" }, { "break", "broke" }, { "bring", "brought" }, { "build", "built" }, { "burst", "burst" }, { "buy", "bought" }, { "catch", "caught" }, { "choose", "chose" }, { "come", "came" }, { "cut", "cut" }, { "deal", "dealt" }, { "do", "did" }, { "drink", "drank" }, { "drive", "drove" }, { "eat", "ate" }, { "fall", "fell" }, { "feed", "fed" }, { "feel", "felt" }, { "fight", "fought" }, { "find", "found" }, { "fly", "flew" }, { "forbid", "forbade" }, { "forget", "forgot" }, { "forgive", "forgave" }, { "freeze", "froze" }, { "get", "got" }, { "give", "gave" }, { "go", "went" }, { "grow", "grew" }, { "have", "had" }, { "hear", "heard" }, { "hide", "hid" }, { "hold", "held" }, { "hurt", "hurt" }, { "keep", "kept" }, { "know", "knew" }, { "lay", "laid" }, { "lead", "led" }, { "leave", "left" }, { "let", "let" }, { "lie", "lay" }, { "lose", "lost" }, { "make", "made" }, { "meet", "met" }, { "pay", "paid" }, { "quit", "quit" }, { "read", "read" }, { "ride", "rode" }, { "run", "ran" }, { "say", "said" }, { "see", "saw" }, { "seek", "sought" }, { "sell", "sold" }, { "send", "sent" }, { "shake", "shook" }, { "shine", "shone" }, { "sing", "sang" }, { "sit", "sat" }, { "sleep", "slept" }, { "speak", "spoke" }, { "spend", "spent" }, { "stand", "stood" }, { "steal", "stole" }, { "swim", "swam" }, { "swing", "swung" }, { "take", "took" }, { "teach", "taught" }, { "tear", "tore" }, { "tell", "told" }, { "think", "thought" }, { "throw", "threw" }, { "understand", "understood" }, { "wake", "woke " }, { "wear", "wore" }, { "win", "won" }, { "write", "wrote" }, { "power", "powered" }, { "arise", "arose" }, { "babysit", "babysat" }, { "be", "was" }, { "beat", "beat" }, { "bend", "bent" }, { "bet", "bet" }, { "bind", "bound" }, { "bite", "bit" }, { "bleed", "bled" }, { "breed", "bred" }, { "cost", "cost" }, { "dig", "dug" }, { "draw", "drew" }, { "hang", "hung" }, { "hit", "hit" }, { "lend", "lent" }, { "light", "lit" }, { "mean", "meant" }, { "put", "put" }, { "ring", "rang" }, { "rise", "rose" }, { "set", "set" }, { "shoot", "shot" }, { "show", "showed" }, { "shut", "shut" }, { "slide", "slid" }, { "spin", "spun" }, { "spread", "spread" }, { "stick", "stuck" }, { "sting", "stung" }, { "strike", "struck" }, { "swear", "swore" }, { "sweep", "swept" }, { "withdraw", "withdrew" }, { "awake", "awoke" }, { "backslide", "backslid" }, { "bear", "bore" }, { "bid", "bid" }, { "broadcast", "broadcasted" }, { "browbeat", "browbeat" }, { "burn", "burnt" }, { "bust", "busted" }, { "cast", "cast" }, { "cling", "clung" }, { "clothe", "clothed" }, { "creep", "crept" }, { "crossbreed", "crossbred" }, { "daydream", "daydreamt" }, { "disprove", "disproved" }, { "dive", "dove" }, { "dream", "dreamt" }, { "dwell", "dwelt" }, { "fit", "fit" }, { "flee", "fled" }, { "fling", "flung" }, { "forecast", "forecast" }, { "forego", "forewent" }, { "foresee", "foresaw" }, { "foretell", "foretold" }, { "forsake", "forsook" }, { "frostbite", "frostbit" }, { "grind", "ground" }, { "hand-feed", "hand-fed" }, { "handwrite", "handwrote" }, { "hew", "hewed" }, { "inbreed", "inbred" }, { "inlay", "inlaid" }, { "input", "input" }, { "interbreed", "interbred" }, { "interweave", "interwove" }, { "interwind", "interwound" }, { "jerry-build", "jerry-built" }, { "kneel", "knelt" }, { "knit", "knit" }, { "lean", "leant" }, { "leap", "leapt" }, { "learn", "learnt" }, { "lip-read", "lip-read" }, { "miscast", "miscast" }, { "misdeal", "misdealt" }, { "misdo", "misdid" }, { "mishear", "misheard" }, { "mislay", "mislaid" }, { "mislead", "misled" }, { "mislearn", "mislearnt" }, { "misread", "misread" }, { "misset", "misset" }, { "misspeak", "misspoke" }, { "misspell", "misspelt" }, { "misspend", "misspent" }, { "mistake", "mistook" }, { "misteach", "mistaught" }, { "misunderstand", "misunderstood" }, { "miswrite", "miswrote" }, { "mow", "mowed" }, { "offset", "offset" }, { "outbid", "outbid" }, { "outbreed", "outbred" }, { "outdo", "outdid" }, { "outdraw", "outdrew" }, { "outdrink", "outdrank" }, { "outdrive", "outdrove" }, { "outfight", "outfought" }, { "outfly", "outflew" }, { "outgrow", "outgrew" }, { "outleap", "outleapt" }, { "outlie", "outlied" }, { "outride", "outrode" }, { "outrun", "outran" }, { "outsell", "outsold" }, { "outshine", "outshone" }, { "outshoot", "outshot" }, { "outsing", "outsang" }, { "outsit", "outsat" }, { "outsleep", "outslept" }, { "outsmell", "outsmelt" }, { "outspeak", "outspoke" }, { "outspeed", "outsped" }, { "outspend", "outspent" }, { "outswear", "outswore" }, { "outswim", "outswam" }, { "outthink", "outthought" }, { "outthrow", "outthrew" }, { "outwrite", "outwrote" }, { "overbid", "overbid" }, { "overbreed", "overbred" }, { "overbuild", "overbuilt" }, { "overbuy", "overbought" }, { "overcome", "overcame" }, { "overdo", "overdid" }, { "overdraw", "overdrew" }, { "overdrink", "overdrank" }, { "overeat", "overate" }, { "overfeed", "overfed" }, { "overhang", "overhung" }, { "overhear", "overheard" }, { "overlay", "overlaid" }, { "overpay", "overpaid" }, { "override", "overrode" }, { "overrun", "overran" }, { "oversee", "oversaw" }, { "oversell", "oversold" }, { "oversew", "oversewed" }, { "overshoot", "overshot" }, { "oversleep", "overslept" }, { "overspeak", "overspoke" }, { "overspend", "overspent" }, { "overspill", "overspilt" }, { "overtake", "overtook" }, { "overthink", "overthought" }, { "overthrow", "overthrew" }, { "overwind", "overwound" }, { "overwrite", "overwrote" }, { "partake", "partook" }, { "plead", "pled" }, { "prebuild", "prebuilt" }, { "predo", "predid" }, { "premake", "premade" }, { "prepay", "prepaid" }, { "presell", "presold" }, { "preset", "preset" }, { "preshrink", "preshrank" }, { "proofread", "proofread" }, { "prove", "proved" }, { "quick-freeze", "quick-froze" }, { "reawake", "reawoke" }, { "rebid", "rebid" }, { "rebind", "rebound" }, { "rebroadcast", "rebroadcast" }, { "rebuild", "rebuilt" }, { "recast", "recast" }, { "recut", "recut" }, { "redeal", "redealt" }, { "redo", "redid" }, { "redraw", "redrew" }, { "regrind", "reground" }, { "regrow", "regrew" }, { "rehang", "rehung" }, { "rehear", "reheard" }, { "reknit", "reknit" }, { "relay", "relayed" }, { "relearn", "relearnt" }, { "relight", "relit" }, { "remake", "remade" }, { "repay", "repaid" }, { "reread", "reread" }, { "rerun", "reran" }, { "resell", "resold" }, { "resend", "resent" }, { "reset", "reset" }, { "resew", "resewed" }, { "retake", "retook" }, { "reteach", "retaught" }, { "retear", "retore" }, { "retell", "retold" }, { "rethink", "rethought" }, { "retread", "retread" }, { "retrofit", "retrofit" }, { "rewake", "rewoke" }, { "rewear", "rewore" }, { "reweave", "rewove" }, { "rewed", "rewed" }, { "rewet", "rewet" }, { "rewin", "rewon" }, { "rewind", "rewound" }, { "rewrite", "rewrote" }, { "rid", "rid" }, { "roughcast", "roughcast" }, { "sand-cast", "sand-cast" }, { "saw", "sawed" }, { "sew", "sewed" }, { "shave", "shaved" }, { "shear", "sheared" }, { "shed", "shed" }, { "shrink", "shrunk" }, { "sight-read", "sight-read" }, { "sink", "sunk" }, { "slay", "slew" }, { "sling", "slung" }, { "slink", "slunk" }, { "slit", "slit" }, { "smell", "smelt" }, { "sneak", "snuck" }, { "sow", "sowed" }, { "speed", "sped" }, { "spell", "spelt" }, { "spill", "spilt" }, { "spit", "spat" }, { "split", "split" }, { "spoil", "spoilt" }, { "spoon-feed", "spoon-fed" }, { "spring", "sprung" }, { "stink", "stank" }, { "strew", "strewed" }, { "stride", "strode" }, { "string", "strung" }, { "strive", "strove" }, { "sublet", "sublet" }, { "sunburn", "sunburnt" }, { "sweat", "sweat" }, { "swell", "swelled" }, { "telecast", "telecast" }, { "test-drive", "test-drove" }, { "test-fly", "test-flew" }, { "thrust", "thrust" }, { "tread", "trod" }, { "typecast", "typecast" }, { "typeset", "typeset" }, { "typewrite", "typewrote" }, { "unbend", "unbent" }, { "unbind", "unbound" }, { "unclothe", "unclad" }, { "underbid", "underbid" }, { "undercut", "undercut" }, { "underfeed", "underfed" }, { "undergo", "underwent" }, { "underlie", "underlay" }, { "undersell", "undersold" }, { "underspend", "underspent" }, { "undertake", "undertook" }, { "underwrite", "underwrote" }, { "undo", "undid" }, { "unfreeze", "unfroze" }, { "unhang", "unhung" }, { "unhide", "unhid" }, { "unknit", "unknit" }, { "unlearn", "unlearnt" }, { "unsew", "unsewed" }, { "unsling", "unslung" }, { "unspin", "unspun" }, { "unstick", "unstuck" }, { "unstring", "unstrung" }, { "unweave", "unwove" }, { "unwind", "unwound" }, { "uphold", "upheld" }, { "upset", "upset" }, { "waylay", "waylaid" }, { "weave", "wove" }, { "wed", "wed" }, { "weep", "wept" }, { "wet", "wet" }, { "whet", "whetted" }, { "wind", "wound" }, { "withhold", "withheld" }, { "withstand", "withstood" }, { "wring", "wrung" } };

            return dictionary;
        }
    }
}

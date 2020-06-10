Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Words
    ''' <summary>
    ''' Class that contains exceptions to verb usage in the English language.
    ''' </summary>
    ''' <remarks>Use this for verbs and PluralityDictionary for Nouns</remarks>
    Public Class VerbDictionary
        Private Shared _List As Dictionary(Of String, String)
        Private Shared _Const As List(Of String)
        Private Shared _Vowels As List(Of String)
        Private Shared _Past As Dictionary(Of String, String)

        Private Shared Sub fillList()
            If _List Is Nothing Then _List = getExcepts()
        End Sub
        Private Shared Sub fillConsonants()
            If _Const Is Nothing Then _Const = getConsts()
        End Sub
        Private Shared Sub fillVowels()
            If _Vowels Is Nothing Then _Vowels = getVowels()
        End Sub
        Private Shared Sub FillPastTenseExceptions()
            If _Past Is Nothing Then _Past = getPastTenseExceptions()
        End Sub
        ''' <summary>
        ''' get the plural version of text.
        ''' </summary>
        ''' <param name="str">present tense verb to make past tense.</param>
        ''' <returns>return the lowercase plural form of the given string.</returns>
        ''' <remarks>The string will always be converted to lowercase.</remarks>
        Public Shared Function getPastTense(ByVal str As String) As String
            Dim lowStr As String = str.ToLower
            If List.ContainsKey(lowStr) Then
                Return List(lowStr).Replace(lowStr, str)
            Else
                Return getSimplePast(str)
            End If
        End Function
        ''' <summary>
        ''' Get the gerund value of a verb.
        ''' </summary>
        ''' <param name="str">present tense verb to make gerund.</param>
        ''' <returns>String value of the gerund form of a verb.</returns>
        ''' <remarks></remarks>
        Public Shared Function getGerund(ByVal str As String) As String
            'str = str.ToLower
            If str.ToLower.Substring(str.Length - 1, 1) = "e" Then
                str = str.Substring(0, str.Length - 1)
            ElseIf str.Length > 2 Then
                Dim e1, e2 As String
                'get last letter
                e1 = str.ToLower.Substring(str.Length - 1, 1)
                'get second to last letter
                e2 = str.ToLower.Substring(str.Length - 2, 1)
                'if last letter is a consonant and the second to last is a vowel
                If Vowels.Contains(e2) AndAlso Consonants.Contains(e1) Then
                    'double last letter
                    str &= e1
                End If
            End If
            Return str & "ing"
        End Function
        ''' <summary>
        ''' pluralize text based on basic english rules
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function getSimplePast(ByVal name As String) As String

            Dim vowelCount As Integer = 0
            fillVowels()
            fillConsonants()
            fillList()
            FillPastTenseExceptions()


            'count the amount of vowels in word
            For Each ch As Char In name.ToCharArray
                If _Vowels.Contains(ch) Then
                    vowelCount += 1
                End If
            Next



            'Check to see if word exists in past tense special case dictionary'
            If _Past.ContainsKey(name) Then
                name = _Past(name)

                'trim the 'e' off the end if it exists for past tense
            ElseIf name.ToLower.Substring(name.Length - 1, 1) = "e" Then
                name = name.Substring(0, name.Length - 1)
                name = name.Insert(name.Length, "ed")

                'add ed if the word ends in ch
            ElseIf name.ToLower.Substring(name.Length - 2, 2) = "ch" Then
                name = name.Insert(name.Length, "ed")

                'If Words ends in x, add "ed"
            ElseIf name.Chars(name.Length - 1) = "x" Then
                name = name.Insert(name.Length, "ed")

                'If Words ends in y and the second to last letter is a consonant, drop the "y" and add "ied"
            ElseIf name.Chars(name.Length - 1) = "y" AndAlso _Const.Contains(name.Chars(name.Length - 2)) Then
                name = name.Remove(name.Length - 1)
                name = name.Insert((name.Length), "ied")

                'If Words ends in y and the second to last letter is a vowel add "ed"
            ElseIf name.Chars(name.Length - 1) = "y" AndAlso _Vowels.Contains(name.Chars(name.Length - 2)) Then
                name = name.Insert((name.Length), "ed")


                'If Words ends in a vowel followed by a consonant, then double the consonant and add "ed"
            ElseIf vowelCount < 2 AndAlso _Const.Contains(name.Chars(name.Length - 1)) _
                AndAlso _Vowels.Contains(name.Chars(name.Length - 2)) _
                AndAlso _Const.Contains(name.Chars(name.Length - 3)) Then

                name = name.Insert(name.Length, name.Chars(name.Length - 1) + "ed")

            Else
                'If word does not fall into any other category, add "ed"
                name = name.Insert(name.Length, "ed")

            End If

                Return name '& "ed"
        End Function
        ''' <summary>
        ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property List() As Dictionary(Of String, String)
            Get
                fillList()
                Return _List
            End Get
        End Property
        ''' <summary>
        ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property Consonants() As List(Of String)
            Get
                fillConsonants()
                Return _Const
            End Get
        End Property
        ''' <summary>
        ''' Get a dictionary of exceptions to the basic rules of plurality in English. Key is the singular form, Value is the Plural Form.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality.</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property Vowels() As List(Of String)
            Get
                fillVowels()
                Return _Vowels
            End Get
        End Property
        ''' <summary>
        ''' A list of exceptions to basic rules of plurality in English.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ''' <remarks></remarks>
        Private Shared Function getExcepts() As Dictionary(Of String, String)
            Dim retHash As New Dictionary(Of String, String)
            Return retHash
        End Function
        ''' <summary>
        ''' A list of exceptions to basic rules of plurality in English.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ''' <remarks></remarks>
        Private Shared Function getConsts() As List(Of String)
            Dim retHash As New List(Of String)
            retHash.Add("b")
            retHash.Add("c")
            retHash.Add("d")
            retHash.Add("f")
            retHash.Add("g")
            retHash.Add("h")
            retHash.Add("j")
            retHash.Add("k")
            retHash.Add("l")
            retHash.Add("m")
            retHash.Add("n")
            retHash.Add("p")
            retHash.Add("q")
            retHash.Add("r")
            retHash.Add("s")
            retHash.Add("t")
            retHash.Add("v")
            'retHash.Add("w") omitted because is not needed.
            'retHash.Add("x")	omitted because is not needed.
            'retHash.Add("y") omitted because is not needed.
            retHash.Add("z")

            Return retHash
        End Function
        ''' <summary>
        ''' A list of exceptions to basic rules of plurality in English.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of plurality. Key is the singular form, Value is the Plural Form.</returns>
        ''' <remarks></remarks>
        Private Shared Function getVowels() As List(Of String)
            Dim retHash As New List(Of String)

            retHash.Add("a")
            retHash.Add("e")
            retHash.Add("i")
            retHash.Add("o")
            retHash.Add("u")

            Return retHash
        End Function

        ''' <summary>
        ''' A list of exceptions to basic rules of past tense in English.
        ''' </summary>
        ''' <returns>Dictionary of exceptions to the english rules of past tense.</returns>
        ''' <remarks></remarks>
        Private Shared Function getPastTenseExceptions() As Dictionary(Of String, String)
            'Dim dictionary As New Dictionary(Of String, String)
            '{{"become", "became"}, {"begin", "began"}, {"blow", "blew"}, {"break", "broke"}, {"bring", "brought"}, {"build", "built"}, {"burst", "burst"}, {"buy", "bought"}, {"catch", "caught"}, {"choose", "chose"}, {"come", "came"}, {"cut", "cut"}, {"deal", "dealt"}, {"do", "did"}, {"drink", "drank"}, {"drive", "drove"}, {"eat", "ate"}, {"fall", "fell"}, {"feed", "fed"}, {"feel", "felt"}, {"fight", "fought"}, {"find", "found"}, {"fly", "flew"}, {"forbid", "forbade"}, {"forget", "forgot"}, {"forgive", "forgave"}, {"freeze", "froze"}, {"get", "got"}, {"give", "gave"}, {"go", "went"}, {"grow", "grew"}, {"have", "had"}, {"hear", "heard"}, {"hide", "hid"}, {"hold", "held"}, {"hurt", "hurt"}, {"keep", "kept"}, {"know", "knew"}, {"lay", "laid"}, {"lead", "led"}, {"let", "let"}, {"leave", "left"}, {"lie", "lay"}, {"lose", "lost"}, {"make", "made"}, {"meet", "met"}, {"pay", "paid"}, {"quit", "quit"}, {"read", "read"}, {"ride", "rode"}, {"run", "ran"}, {"say", "said"}, {"see", "saw"}, {"seek", "sought"}, {"sell", "sold"}, {"send", "sent"}, {"shake", "shook"}, {"shine", "shone"}, {"sing", "sang"}, {"sit", "sat"}, {"sleep", "slept"}, {"speak", "spoke"}, {"spend", "spent"}, {"spring", "sprang"}, {"stand", "stood"}, {"steal", "stole"}, {"swim", "swam"}, {"swing", "swung"}, {"take", "took"}, {"teach", "taught"}, {"tear", "tore"}, {"tell", "told"}, {"think", "thought"}, {"throw", "threw"}, {"understand", "understood"}, {"wake", "woke (waked)"}, {"wear", "wore"}, {"win", "won"}, {"write", "wrote"}}
            Dim dictionary = New Dictionary(Of String, String) From {{"become", "became"}, _
                {"begin", "began"}, {"blow", "blew"}, {"break", "broke"}, {"bring", "brought"}, _
                {"build", "built"}, {"burst", "burst"}, {"buy", "bought"}, {"catch", "caught"}, _
                {"choose", "chose"}, {"come", "came"}, {"cut", "cut"}, {"deal", "dealt"}, _
                {"do", "did"}, {"drink", "drank"}, {"drive", "drove"}, {"eat", "ate"}, _
                {"fall", "fell"}, {"feed", "fed"}, {"feel", "felt"}, {"fight", "fought"}, _
                {"find", "found"}, {"fly", "flew"}, {"forbid", "forbade"}, {"forget", "forgot"}, _
                {"forgive", "forgave"}, {"freeze", "froze"}, {"get", "got"}, {"give", "gave"}, _
                {"go", "went"}, {"grow", "grew"}, {"have", "had"}, {"hear", "heard"}, _
                {"hide", "hid"}, {"hold", "held"}, {"hurt", "hurt"}, {"keep", "kept"}, _
                {"know", "knew"}, {"lay", "laid"}, {"lead", "led"}, {"leave", "left"}, _
                {"let", "let"}, {"lie", "lay"}, {"lose", "lost"}, {"make", "made"}, _
                {"meet", "met"}, {"pay", "paid"}, {"quit", "quit"}, {"read", "read"}, _
                {"ride", "rode"}, {"run", "ran"}, {"say", "said"}, {"see", "saw"}, _
                {"seek", "sought"}, {"sell", "sold"}, {"send", "sent"}, {"shake", "shook"}, _
                {"shine", "shone"}, {"sing", "sang"}, {"sit", "sat"}, {"sleep", "slept"}, _
                {"speak", "spoke"}, {"spend", "spent"}, _
                {"stand", "stood"}, {"steal", "stole"}, {"swim", "swam"}, _
                {"swing", "swung"}, {"take", "took"}, {"teach", "taught"}, _
                {"tear", "tore"}, {"tell", "told"}, {"think", "thought"}, _
                {"throw", "threw"}, {"understand", "understood"}, _
                {"wake", "woke "}, {"wear", "wore"}, {"win", "won"}, _
                {"write", "wrote"}, {"power", "powered"}, {"arise", "arose"}, _
                {"babysit", "babysat"}, {"be", "was"}, {"beat", "beat"}, _
                {"bend", "bent"}, {"bet", "bet"}, {"bind", "bound"}, {"bite", "bit"}, _
                {"bleed", "bled"}, {"breed", "bred"}, _
                {"cost", "cost"}, {"dig", "dug"}, {"draw", "drew"}, {"hang", "hung"}, _
                {"hit", "hit"}, {"lend", "lent"}, {"light", "lit"}, {"mean", "meant"}, _
                {"put", "put"}, {"ring", "rang"}, {"rise", "rose"}, {"set", "set"}, _
                {"shoot", "shot"}, {"show", "showed"}, {"shut", "shut"}, _
                {"slide", "slid"}, {"spin", "spun"}, _
                {"spread", "spread"}, {"stick", "stuck"}, {"sting", "stung"}, _
                {"strike", "struck"}, {"swear", "swore"}, {"sweep", "swept"}, _
                {"withdraw", "withdrew"}, {"awake", "awoke"}, _
                {"backslide", "backslid"}, {"bear", "bore"}, {"bid", "bid"}, _
                {"broadcast", "broadcasted"}, {"browbeat", "browbeat"}, {"burn", "burnt"}, _
                {"bust", "busted"}, {"cast", "cast"}, {"cling", "clung"}, {"clothe", "clothed"}, _
                {"creep", "crept"}, {"crossbreed", "crossbred"}, {"daydream", "daydreamt"}, _
                {"disprove", "disproved"}, {"dive", "dove"}, {"dream", "dreamt"}, {"dwell", "dwelt"}, _
                {"fit", "fit"}, {"flee", "fled"}, {"fling", "flung"}, {"forecast", "forecast"}, _
                {"forego", "forewent"}, {"foresee", "foresaw"}, {"foretell", "foretold"}, _
                {"forsake", "forsook"}, {"frostbite", "frostbit"}, {"grind", "ground"}, _
                {"hand-feed", "hand-fed"}, {"handwrite", "handwrote"}, {"hew", "hewed"}, _
                {"inbreed", "inbred"}, {"inlay", "inlaid"}, {"input", "input"}, {"interbreed", "interbred"}, _
                {"interweave", "interwove"}, {"interwind", "interwound"}, {"jerry-build", "jerry-built"}, _
                {"kneel", "knelt"}, {"knit", "knit"}, {"lean", "leant"}, {"leap", "leapt"}, _
                {"learn", "learnt"}, {"lip-read", "lip-read"}, {"miscast", "miscast"}, _
                {"misdeal", "misdealt"}, {"misdo", "misdid"}, {"mishear", "misheard"}, _
                {"mislay", "mislaid"}, {"mislead", "misled"}, {"mislearn", "mislearnt"}, _
                {"misread", "misread"}, {"misset", "misset"}, {"misspeak", "misspoke"}, _
                {"misspell", "misspelt"}, {"misspend", "misspent"}, {"mistake", "mistook"}, _
                {"misteach", "mistaught"}, {"misunderstand", "misunderstood"}, {"miswrite", "miswrote"}, _
                {"mow", "mowed"}, {"offset", "offset"}, {"outbid", "outbid"}, {"outbreed", "outbred"}, _
                {"outdo", "outdid"}, {"outdraw", "outdrew"}, {"outdrink", "outdrank"}, _
                {"outdrive", "outdrove"}, {"outfight", "outfought"}, {"outfly", "outflew"}, _
                {"outgrow", "outgrew"}, {"outleap", "outleapt"}, {"outlie", "outlied"}, _
                {"outride", "outrode"}, {"outrun", "outran"}, {"outsell", "outsold"}, _
                {"outshine", "outshone"}, {"outshoot", "outshot"}, {"outsing", "outsang"}, _
                {"outsit", "outsat"}, {"outsleep", "outslept"}, {"outsmell", "outsmelt"}, _
                {"outspeak", "outspoke"}, {"outspeed", "outsped"}, {"outspend", "outspent"}, _
                {"outswear", "outswore"}, {"outswim", "outswam"}, {"outthink", "outthought"}, _
                {"outthrow", "outthrew"}, {"outwrite", "outwrote"}, {"overbid", "overbid"}, _
                {"overbreed", "overbred"}, {"overbuild", "overbuilt"}, {"overbuy", "overbought"}, _
                {"overcome", "overcame"}, {"overdo", "overdid"}, {"overdraw", "overdrew"}, _
                {"overdrink", "overdrank"}, {"overeat", "overate"}, {"overfeed", "overfed"}, _
                {"overhang", "overhung"}, {"overhear", "overheard"}, {"overlay", "overlaid"}, _
                {"overpay", "overpaid"}, {"override", "overrode"}, {"overrun", "overran"}, _
                {"oversee", "oversaw"}, {"oversell", "oversold"}, {"oversew", "oversewed"}, _
                {"overshoot", "overshot"}, {"oversleep", "overslept"}, {"overspeak", "overspoke"}, _
                {"overspend", "overspent"}, {"overspill", "overspilt"}, {"overtake", "overtook"}, _
                {"overthink", "overthought"}, {"overthrow", "overthrew"}, {"overwind", "overwound"}, _
                {"overwrite", "overwrote"}, {"partake", "partook"}, {"plead", "pled"}, _
                {"prebuild", "prebuilt"}, {"predo", "predid"}, {"premake", "premade"}, _
                {"prepay", "prepaid"}, {"presell", "presold"}, {"preset", "preset"}, _
                {"preshrink", "preshrank"}, {"proofread", "proofread"}, {"prove", "proved"}, _
                {"quick-freeze", "quick-froze"}, {"reawake", "reawoke"}, {"rebid", "rebid"}, _
                {"rebind", "rebound"}, {"rebroadcast", "rebroadcast"}, {"rebuild", "rebuilt"}, _
                {"recast", "recast"}, {"recut", "recut"}, {"redeal", "redealt"}, {"redo", "redid"}, _
                {"redraw", "redrew"}, {"regrind", "reground"}, {"regrow", "regrew"}, {"rehang", "rehung"}, _
                {"rehear", "reheard"}, {"reknit", "reknit"}, {"relay", "relayed"}, {"relearn", "relearnt"}, _
                {"relight", "relit"}, {"remake", "remade"}, {"repay", "repaid"}, {"reread", "reread"}, _
                {"rerun", "reran"}, {"resell", "resold"}, {"resend", "resent"}, {"reset", "reset"}, _
                {"resew", "resewed"}, {"retake", "retook"}, {"reteach", "retaught"}, _
                {"retear", "retore"}, {"retell", "retold"}, {"rethink", "rethought"}, _
                {"retread", "retread"}, {"retrofit", "retrofit"}, {"rewake", "rewoke"}, _
                {"rewear", "rewore"}, {"reweave", "rewove"}, {"rewed", "rewed"}, _
                {"rewet", "rewet"}, {"rewin", "rewon"}, {"rewind", "rewound"}, _
                {"rewrite", "rewrote"}, {"rid", "rid"}, {"roughcast", "roughcast"}, _
                {"sand-cast", "sand-cast"}, {"saw", "sawed"}, {"sew", "sewed"}, {"shave", "shaved"}, _
                {"shear", "sheared"}, {"shed", "shed"}, {"shrink", "shrunk"}, _
                {"sight-read", "sight-read"}, {"sink", "sunk"}, {"slay", "slew"}, _
                {"sling", "slung"}, {"slink", "slunk"}, {"slit", "slit"}, _
                {"smell", "smelt"}, {"sneak", "snuck"}, {"sow", "sowed"}, {"speed", "sped"}, _
                {"spell", "spelt"}, {"spill", "spilt"}, {"spit", "spat"}, {"split", "split"}, _
                {"spoil", "spoilt"}, {"spoon-feed", "spoon-fed"}, {"spring", "sprung"}, _
                {"stink", "stank"}, {"strew", "strewed"}, _
                {"stride", "strode"}, {"string", "strung"}, {"strive", "strove"}, _
                {"sublet", "sublet"}, {"sunburn", "sunburnt"}, {"sweat", "sweat"}, _
                {"swell", "swelled"}, {"telecast", "telecast"}, {"test-drive", "test-drove"}, _
                {"test-fly", "test-flew"}, {"thrust", "thrust"}, {"tread", "trod"}, _
                {"typecast", "typecast"}, {"typeset", "typeset"}, {"typewrite", "typewrote"}, _
                {"unbend", "unbent"}, {"unbind", "unbound"}, {"unclothe", "unclad"}, _
                {"underbid", "underbid"}, {"undercut", "undercut"}, {"underfeed", "underfed"}, _
                {"undergo", "underwent"}, {"underlie", "underlay"}, {"undersell", "undersold"}, _
                {"underspend", "underspent"}, {"undertake", "undertook"}, {"underwrite", "underwrote"}, _
                {"undo", "undid"}, {"unfreeze", "unfroze"}, {"unhang", "unhung"}, _
                {"unhide", "unhid"}, {"unknit", "unknit"}, {"unlearn", "unlearnt"}, _
                {"unsew", "unsewed"}, {"unsling", "unslung"}, {"unspin", "unspun"}, _
                {"unstick", "unstuck"}, {"unstring", "unstrung"}, {"unweave", "unwove"}, _
                {"unwind", "unwound"}, {"uphold", "upheld"}, {"upset", "upset"}, _
                {"waylay", "waylaid"}, {"weave", "wove"}, {"wed", "wed"}, {"weep", "wept"}, _
                {"wet", "wet"}, {"whet", "whetted"}, {"wind", "wound"}, _
                {"withhold", "withheld"}, {"withstand", "withstood"}, {"wring", "wrung"}}

            Return dictionary
        End Function


    End Class
End Namespace
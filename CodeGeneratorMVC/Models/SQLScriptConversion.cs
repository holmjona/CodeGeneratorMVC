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
using Words;
using System.Windows;
using Microsoft.VisualBasic;
using language = CodeGeneration.Language;

public class SQLScriptConversion {
    public static Dictionary<string, string> _dictionaryOfSQLToVBType;
    public static Dictionary<string, string> DictionaryOfSQLToVBType {
        get {
            if (_dictionaryOfSQLToVBType == null) {
                _dictionaryOfSQLToVBType = new Dictionary<string, string>();

                _dictionaryOfSQLToVBType.Add("smallint", "Int16");
                _dictionaryOfSQLToVBType.Add("tinyint", "Byte");
                _dictionaryOfSQLToVBType.Add("int", "Integer");
                _dictionaryOfSQLToVBType.Add("bigint", "Int64");

                _dictionaryOfSQLToVBType.Add("money", "Decimal");
                _dictionaryOfSQLToVBType.Add("smallmoney", "Double");
                _dictionaryOfSQLToVBType.Add("decimal", "Double");
                _dictionaryOfSQLToVBType.Add("numeric", "Double");
                _dictionaryOfSQLToVBType.Add("float", "Double");
                _dictionaryOfSQLToVBType.Add("real", "Single");

                _dictionaryOfSQLToVBType.Add("smalldatetime", "DateTime");
                _dictionaryOfSQLToVBType.Add("datetime", "DateTime");
                _dictionaryOfSQLToVBType.Add("datetime2", "DateTime");
                _dictionaryOfSQLToVBType.Add("date", "DateTime");

                _dictionaryOfSQLToVBType.Add("uniqueidentifier", "String");
                _dictionaryOfSQLToVBType.Add("guid", "String");

                _dictionaryOfSQLToVBType.Add("nvarchar", "String");
                _dictionaryOfSQLToVBType.Add("varchar", "String");
                _dictionaryOfSQLToVBType.Add("nchar", "String");
                _dictionaryOfSQLToVBType.Add("char", "String");
                _dictionaryOfSQLToVBType.Add("ntext", "String");
                _dictionaryOfSQLToVBType.Add("text", "String");

                _dictionaryOfSQLToVBType.Add("bit", "Boolean");

                _dictionaryOfSQLToVBType.Add("byte", "Image");

                _dictionaryOfSQLToVBType.Add("binary", "Byte()");
                _dictionaryOfSQLToVBType.Add("varbinary", "Byte()");
                _dictionaryOfSQLToVBType.Add("image", "Byte()");
            }
            Dictionary<string, Type> tempDictionary = new Dictionary<string, Type>();
            tempDictionary.Add("test", typeof(int));
            foreach (KeyValuePair<string, Type> pair in tempDictionary) {
            }
            return _dictionaryOfSQLToVBType;
        }
    }


    public static List<ProjectClass> generateObjects(string fileName, ref List<string> messages) {
        System.IO.StreamReader myRead = System.IO.File.OpenText(fileName);

        string myString = myRead.ReadToEnd();
        // remove comments (--This is a sample comment)
        while (myString.IndexOf("--") > -1) {
            int count = myString.IndexOf(Constants.vbCrLf, myString.IndexOf("--")) - myString.IndexOf("--");
            if (count < 0)
                count = myString.Length - myString.IndexOf("--");
            myString = myString.Remove(myString.IndexOf("--"), count);
        }
        int ID = StaticVariables.Instance.HighestProjectClassID + 1;
        List<ProjectClass> retList = new List<ProjectClass>();
        ProjectClass pAliasGroupClass = null/* TODO Change to default(_) if this is not a reference type */;
        // remove escape characters [ and ]
        myString = myString.Replace("[", "");
        myString = myString.Replace("]", "");

        // remove issues that may arise from line breaks.
        myString = myString.Replace(Constants.vbCrLf, " ");
        myString = myString.Replace(Strings.Chr(10), ' ');
        myString = myString.Replace(Strings.Chr(13), ' ');

        while (myString.IndexOf("CREATE TABLE", StringComparison.OrdinalIgnoreCase) > -1) {
            // for each line remove create table statement.
            myString = myString.Remove(0, myString.IndexOf("CREATE TABLE", StringComparison.OrdinalIgnoreCase));
            myString = myString.Remove(0, 12);
            string extractedText = ExtractCreateTableStatement(myString);
            ProjectClass projClass = ExtractProjectClass(ref extractedText, ID);
            // If StaticVariables.Instance.DALs.Count > 0 Then
            // pClass.DALClassVariable = StaticVariables.Instance.DALs(0)
            // End If
            // If StaticVariables.Instance.MasterPages.Count > 0 Then
            // pClass.MasterPage = StaticVariables.Instance.MasterPages(0)
            // End If
            // pClass.BaseClass = StaticVariables.Instance.BaseClasses(0)
            // If StaticVariables.Instance.NameSpaceNames.Count > 0 Then
            // pClass.NameSpaceName = StaticVariables.Instance.NameSpaceNames(0)
            // End If

            // ' See if this is the Alias Group Table/Object
            projClass.IsAssociatedWithAliasGroup = projClass.Name.Capitalized().ToLower().Contains("alias");
            if (projClass.IsAssociatedWithAliasGroup)
                pAliasGroupClass = projClass;

            retList.Add(projClass);
            ID += 1;
        }

        // ' If we did not find a Alias Group Table, create one.
        if (pAliasGroupClass == null) {
            pAliasGroupClass = new ProjectClass();
            pAliasGroupClass.ID = ID;
            pAliasGroupClass.IsAssociatedWithAliasGroup = true;
            pAliasGroupClass.NameString = "AliasGroup";
            pAliasGroupClass.Summary = "This class contains the aliases that will be used in this site.";
            pAliasGroupClass.DatabaseTableName = "AliasGroup";
            StaticVariables.Instance.ListOfProjectClasses.Add(pAliasGroupClass);
        }

        bool doesVariableExist = false;
        ProjectClass pClass;

        for (int i = 0; i < retList.Count; i++) {
            pClass = retList[i];
            doesVariableExist = false;
            int _ID = 1;
                string[] textSplit = pClass.OriginalSQLText.Split(',');
            for(int n = 0; n < textSplit.Length; n++) {
            string variableSQLText = textSplit[n].Trim();
                try {
                    ClassVariable newVar = ExtractClassVariable(ref pClass, ref variableSQLText, _ID);
                    if (newVar != null)
                        pClass.ClassVariables.Add(newVar);
                } catch (Exception ex) {
                    messages.Add(ex.Message);
                }

                _ID += 1;
            }
            // create the alias group variable for this object.
            ClassVariable myVariable = new ClassVariable(pAliasGroupClass, pClass.Name.Capitalized(), StaticVariables.Instance.GetDataType("namealias"), 
                false, false, false, false, false, true, true, pAliasGroupClass.ClassVariables.Count, false, false, "String", -1, pClass.Name.Capitalized());
            // check to see if alias already exists
            foreach (ClassVariable cVariable in pAliasGroupClass.ClassVariables) {
                if (cVariable.Name.ToLower().CompareTo(myVariable.Name.ToLower()) == 0) {
                    doesVariableExist = true;
                    break;
                }
            }
            if (!doesVariableExist)
                // add alias for this object to the Alias Group
                pAliasGroupClass.ClassVariables.Add(myVariable);
        }

        // ' Check for associative classes. These are classes that 
        // '   are only an association between two other classes.
        bool IsAssociativeClass;
        string failedObjects = "";
        foreach (ProjectClass projClass in retList) {
            IsAssociativeClass = true;
            foreach (ClassVariable cVar in projClass.ClassVariables) {
                if (!cVar.IsIDField) {
                    IsAssociativeClass = IsAssociativeClass & cVar.IsForeignKey;
                    if (!IsAssociativeClass)
                        break;
                }
            }

            if (IsAssociativeClass) {
                try {
                    projClass.IsAssociateEntitiy = true;
                    projClass.ClassObjectIsNotNeeded = true;
                    // ' move classes to appropriate objects.
                    AddAssociationsToAppropriateClasses(projClass, ref retList);
                } catch {
                    failedObjects += projClass.Name.Capitalized() + Constants.vbCrLf;
                }
            }
        }
        if (failedObjects != "")
            messages.Add(failedObjects);

        // ' Make sure we have the default Add, Edit, and Delete Aliases
        string[] listOfDefaultAliases = new[] { "Add", "Edit", "Delete" }; // {"AddAlias", "EditAlias", "DeleteAlias"}
        foreach (string cVariableString in listOfDefaultAliases) {
            doesVariableExist = false;
            foreach (ClassVariable cVariable in pAliasGroupClass.ClassVariables) {
                if (cVariable.Name.ToLower().CompareTo(cVariableString.ToLower()) == 0) {
                    doesVariableExist = true;
                    break;
                }
            }
            // Create Aliases needed based off of classes
            if (!doesVariableExist)
                pAliasGroupClass.ClassVariables.Add(new ClassVariable(pAliasGroupClass, cVariableString, StaticVariables.Instance.GetDataType("namealias"), false, false, false, false, false, true, true, pAliasGroupClass.ClassVariables.Count, false, false, "String", -1, "NA"));
        }

        return retList;
    }
    private static void AddAssociationsToAppropriateClasses(ProjectClass assoEntityClass, ref List<ProjectClass> lst) {
        List<ClassVariable> varsCompleted = new List<ClassVariable>();
        foreach (ClassVariable cVar in assoEntityClass.ClassVariables) {
            foreach (ClassVariable cVarInner in assoEntityClass.ClassVariables) {
                if (cVar != cVarInner) {
                    ProjectClass pC = getClass(cVar.ParameterType);
                    pC.ClassVariables.Add(new ClassVariable(pC, new NameAlias(cVar.Name, true).PluralAndCapitalized, cVar.ParameterType, true, true, true, cVar.IsPropertyInherited, false, false, false, pC.ClassVariables.Count + 1, cVar.IsDatabaseBound, false, cVar.DatabaseType, cVar.LengthOfDatabaseProperty, cVar.DatabaseColumnName));
                }
            }
        }
    }
    private static ProjectClass getClass(ClassVariable cVar) {
        if (cVar.ParameterType.AssociatedProjectClass == null)
            cVar.ParameterType = StaticVariables.Instance.GetDataType(cVar.ParameterType.Name());
        return cVar.ParameterType.AssociatedProjectClass;
    }
    private static ProjectClass getClass(DataType pType) {
        if (pType.AssociatedProjectClass == null)
            pType = StaticVariables.Instance.GetDataType(pType.Name());
        return pType.AssociatedProjectClass;
    }
    private static string ExtractCreateTableStatement(string text) {
        int parenthesisLevel = 0;
        int indexOfOpeningParenthesis = -1;
        int indexOfClosingParenthesis = -1;
        string textForTable = "";
        do {
            indexOfOpeningParenthesis = text.IndexOf("(");
            indexOfClosingParenthesis = text.IndexOf(")");
            if (indexOfOpeningParenthesis < indexOfClosingParenthesis && indexOfOpeningParenthesis > -1) {
                parenthesisLevel += 1;
                textForTable += text.Substring(0, indexOfOpeningParenthesis + 1);
                text = text.Remove(0, indexOfOpeningParenthesis + 1);
            } else if (indexOfClosingParenthesis < indexOfOpeningParenthesis && indexOfClosingParenthesis > -1) {
                parenthesisLevel -= 1;
                textForTable += text.Substring(0, indexOfClosingParenthesis + 1);
                text = text.Remove(0, indexOfClosingParenthesis + 1);
            } else if (indexOfClosingParenthesis > -1) {
                parenthesisLevel -= 1;
                textForTable += text.Substring(0, indexOfClosingParenthesis + 1);
                text = text.Remove(0, indexOfClosingParenthesis + 1);
            } else
                parenthesisLevel -= 1;
        }
        while (parenthesisLevel > 0 && text.Length > 0);

        // While parenthesisLevel > 0 AndAlso text.Length > 0

        // indexOfOpeningParenthesis = text.IndexOf("(")
        // indexOfClosingParenthesis = text.IndexOf(")")
        // If indexOfOpeningParenthesis < indexOfClosingParenthesis AndAlso indexOfOpeningParenthesis > -1 Then
        // parenthesisLevel += 1
        // textForTable &= text.Substring(0, indexOfOpeningParenthesis + 1)
        // text = text.Remove(0, indexOfOpeningParenthesis + 1)
        // ElseIf indexOfClosingParenthesis < indexOfOpeningParenthesis AndAlso indexOfClosingParenthesis > -1 Then
        // parenthesisLevel -= 1
        // textForTable &= text.Substring(0, indexOfClosingParenthesis + 1)
        // text = text.Remove(0, indexOfClosingParenthesis + 1)
        // ElseIf indexOfClosingParenthesis > -1 Then
        // parenthesisLevel -= 1
        // textForTable &= text.Substring(0, indexOfClosingParenthesis + 1)
        // text = text.Remove(0, indexOfClosingParenthesis + 1)
        // Else
        // parenthesisLevel -= 1
        // End If

        // End While
        if (textForTable.Length > 0 && textForTable[textForTable.Length - 1] == ')')
            textForTable = textForTable.Remove(textForTable.Length - 1, 1);

        return textForTable;
    }
    public static string[] _VariablesWithCommaInParams = new[] { "IDENTITY", "PRIMARY", "DECIMAL" };
    private static ProjectClass ExtractProjectClass(ref string text, int ID) {
        ProjectClass retProjectClass = new ProjectClass();
        int parenthesisLevel = 1;


        string textName = text.Substring(0, text.IndexOf("(")).Trim();

        // look to see if owner.table syntax is used
        // get index of period.
        int indexOfPeriod = textName.IndexOf(".");
        // remove owner if used.
        if (indexOfPeriod >= 0)
            textName = textName.Remove(0, indexOfPeriod + 1);
        retProjectClass.DatabaseTableName = textName;
        string newString = NameAlias.getTextWithFormatting(textName);
        string singularVersion = Words.PluralityDictionary.getPluralityInverse(newString);
        retProjectClass.Name = new NameAlias(singularVersion);
        // Get rid of opening ( for Create table statement.)
        text = text.Remove(0, text.IndexOf("(") + 1);
        int indexOfOpeningParenthesis = -1;
        int indexOfClosingParenthesis = -1;
        int indexOFNextComma = -1;
        string textTilParen = "";
        // Dim indexOfComma As Integer = -1


        // Remove any instances of extra parenthesis that exist behind certain variables,
        // such as PRIMARY KEY or IDENTITY
        // Dim indexToStart As Integer = 0
        int indexOfVar = 0;
        foreach (string VarCheck in _VariablesWithCommaInParams) {
            indexOfVar = text.IndexOf(VarCheck, indexOfVar, StringComparison.OrdinalIgnoreCase);
            while (indexOfVar >= 0) {
                if (indexOfVar > -1) {
                    indexOfOpeningParenthesis = text.IndexOf("(", indexOfVar);
                    indexOFNextComma = text.IndexOf(",", indexOfVar);
                    indexOfClosingParenthesis = text.IndexOf(")", indexOfVar);
                    if (indexOFNextComma < indexOfOpeningParenthesis) {
                        indexOfOpeningParenthesis = indexOFNextComma;
                        indexOfClosingParenthesis = indexOFNextComma - 1;
                    }
                    int lengthOfText = indexOfOpeningParenthesis - indexOfVar;
                    int lengthOfTextTilEndParan = indexOfClosingParenthesis - indexOfVar - (VarCheck.Length - 1);

                    if (lengthOfText < 0) {
                        lengthOfText = VarCheck.Length;
                        lengthOfTextTilEndParan = 0;
                    }

                    textTilParen = text.Substring(indexOfVar, lengthOfText) + " ";

                    if (!textContainsKnownSQLType(textTilParen))
                        text = text.Remove(indexOfVar + VarCheck.Length, lengthOfTextTilEndParan);
                    int newStart = indexOfVar + lengthOfText;
                    if (newStart < text.Length)
                        indexOfVar = text.IndexOf(VarCheck, newStart, StringComparison.OrdinalIgnoreCase);
                    else
                        indexOfVar = -1;
                }
            }
            indexOfVar = 0;
        }

        int _ID = 0;
        StaticVariables.Instance.addDerivedTypeToSystem(retProjectClass);
        retProjectClass.OriginalSQLText = text;
        retProjectClass.ID = ID;
        return retProjectClass;
    }
    /// <summary>
    ///     ''' Check to see if text contains a key word. Such as int or nvarchar
    ///     ''' </summary>
    ///     ''' <param name="textToParse"></param>
    ///     ''' <returns>True if key word found in keys of dictionaryOfSQLToVBType is in the gevn text followed immediately by a space (" ")</returns>
    ///     ''' <remarks></remarks>
    public static bool textContainsKnownSQLType(string textToParse) {
        textToParse = textToParse.Replace("(", " (");
        textToParse = textToParse.PadLeft(1).PadRight(1);
        textToParse = textToParse.ToLower();
        foreach (string keyWord in DictionaryOfSQLToVBType.Keys) {
            if (textToParse.Contains(" " + keyWord + " "))
                return true;
        }
        return false;
    }
    private static ClassVariable ExtractClassVariable(ref ProjectClass retProjectClass, ref string text, int ID) {
        // exit function if we can not create a parameter from this entry.
        if (!textContainsKnownSQLType(text))
            return null/* TODO Change to default(_) if this is not a reference type */;
        // eliminate extra spaces 
        text = text.Replace(" (", "(");
        string[] parameters = text.Split(' ');

        // if parameters is not long enough, it must not be a valid entry
        if (parameters.Length <= 1)
            return null/* TODO Change to default(_) if this is not a reference type */;

        // strip empty (extra) spaces from each parameter
        for (int px = 0; px < parameters.Length; px++) {
            parameters[px] = parameters[px].Trim();
        }

        string parameterName = "";
        string fieldName = parameters[0];
        string sqlType = parameters[1];
        bool isRequired = false;
        if (containsValue(parameters, "not", Sensitivity.CaseInsensitive) && containsValue(parameters, "null", Sensitivity.CaseInsensitive))
            isRequired = true;
        DataType dataType = null/* TODO Change to default(_) if this is not a reference type */;
        bool isInHerited = false;
        // check to see if second parameter, which should be the type declaration, contains parenthesis.
        int indexOfFirstParen = sqlType.IndexOf("(");
        int indexOfSecondParen = sqlType.IndexOf(")");
        int length = -1;
        // if contains parenthesies
        if (indexOfFirstParen > 0 & indexOfFirstParen < indexOfSecondParen) {
            // get string including parenthesies
            string lengthString = sqlType.Substring(indexOfFirstParen, indexOfSecondParen - indexOfFirstParen + 1);
            // remove first parentheses
            lengthString = lengthString.Remove(0, 1);
            // remove second, ending parentheses
            lengthString = lengthString.Remove(lengthString.Length - 1, 1);
            // try to convert to integer.
            // replace paramenter without parenthetical text.
            sqlType = sqlType.Remove(indexOfFirstParen, indexOfSecondParen - indexOfFirstParen + 1);

            if (int.TryParse(lengthString, out length)) {
            } else if (lengthString.ToLower() == "max") {
                // technically max is only available for nvarchar, but will check just in case.
                if (sqlType.ToLower() == "nvarchar")
                    length = 4000;
                else
                    length = 8000;
            } else
                // if fails default to -1
                length = -1;
        }
        // MsgBox(parameters(0).Substring(parameters(0).Length - 2, 2))
        // see if text is primary key.

        bool isForeignKey = false;

        bool isID = false;
        bool fieldIdentified = false;
        if (fieldName.Substring(fieldName.Length - 2, 2).ToLower().CompareTo("id") == 0) {
            if (fieldName.ToLower().Substring(fieldName.Length - 2, 2) == "id")
                parameterName = fieldName.Remove(fieldName.Length - 2, 2);
            else if (fieldName.ToLower().Substring(fieldName.Length - 4, 4) == "idfk")
                parameterName = fieldName.Remove(fieldName.Length - 4, 4);
            else if (fieldName.ToLower().Substring(fieldName.Length - 2, 2) == "fk")
                parameterName = fieldName.Remove(fieldName.Length - 2, 2);
            else
                parameterName = fieldName;
            if ((text.ToLower().Contains("primary") || text.ToLower().Contains("identity")))
                // This is the primary key
                isID = true;
            else if (text.ToLower().Contains("foreign key")
                                 || fieldName.Substring(fieldName.Length - 2, 2).ToLower().CompareTo("fk") == 0)
                isForeignKey = true;
            else if (parameterName.ToLower() == retProjectClass.Name.Text().ToLower())
                // this is an ID that was not declared inline
                isID = true;
            else
                // this is a foreign key that was not declared inline
                isForeignKey = true;

            if (isID) {
                parameterName = "ID";
                isInHerited = true;
            } else if (isForeignKey) {
                int indexOfRef = getReferenceIndex(parameters);
                // Check to see of the row references another table with an inline declaration
                if (indexOfRef > -1) {
                    string refString = parameters[indexOfRef + 1];
                    string tblName = refString.Substring(0, refString.IndexOf('('));
                    string objName = Words.PluralityDictionary.getPluralityInverse(tblName);
                    dataType = StaticVariables.Instance.GetDataType(objName);
                } else {
                    // otherwise use the FieldName to try and guess the object
                    // if name ends in ID then remove the ID.

                    // set datatype based on name
                    dataType = StaticVariables.Instance.GetDataType(parameterName);
                    if (dataType == null) {
                        // Check to see if it can be mapped to a user.
                        // make sure that parameter is long enough to check for byid on the end.
                        if (fieldName.Length > 2 && fieldName.Length > 4) {
                            if (fieldName.Substring(fieldName.Length - 4, 4).ToLower().CompareTo("byid") == 0)
                                dataType = StaticVariables.Instance.GetDataType("user");
                        } else
                            dataType = StaticVariables.Instance.GetDataType(sqlType.ToLower());
                    }
                }
            } else
                // Should not hit this code
                throw new Exception("Field could not be identified.");
        } else
            parameterName = fieldName;
        if (dataType == null && DictionaryOfSQLToVBType.ContainsKey(sqlType.ToLower()))
            dataType = StaticVariables.Instance.GetDataType(DictionaryOfSQLToVBType[sqlType.ToLower()]);
        if (dataType == null)
            dataType = StaticVariables.Instance.GetDataType(sqlType.ToLower());
        return new ClassVariable(retProjectClass, parameterName, dataType, isForeignKey, false, false, isID, isInHerited, parameterName != "ID", parameterName != "ID", ID, true, isRequired, sqlType, length, fieldName);
    }

    private static int getReferenceIndex(string[] parameters) {
        int ind = 0;
        foreach (string p in parameters) {
            if (p.Equals("references", StringComparison.OrdinalIgnoreCase))
                return ind;
            ind += 1;
        }
        return -1;
    }
    private enum Sensitivity {
        CaseMatters,
        CaseInsensitive
    }
    private static bool containsValue(string[] arr, string value, Sensitivity sens) {
        foreach (string s in arr) {
            if (sens == Sensitivity.CaseInsensitive) {
                if (value.ToLower() == s.ToLower())
                    return true;
                else if (value == s)
                    return true;
            }
        }
        return false;
    }
}

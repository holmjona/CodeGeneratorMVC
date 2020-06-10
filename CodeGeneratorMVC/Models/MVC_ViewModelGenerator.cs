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
using EnvDTE;
using IRICommonObjects.Words;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;

public class MVC_ViewModelGenerator {
    private string getIndexGET(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
        } else {
            strB.AppendLine(Space(tab.None) + "namespace ViewModels");
            strB.AppendLine(Space(tab.None) + "{");
            strB.AppendLine(Space(tab.X) + "public Class SpecimenViewModel");
            strB.AppendLine(Space(tab.X) + "{");
            strB.AppendLine(Space(tab.XX) + "private " + pClass.Name.Capitalized + " _spec;");
            strB.AppendLine(Space(tab.None));
            strB.AppendLine(Space(tab.XX) + "public " + pClass.Name.Capitalized + " Obj { get { return _spec; } }");
            strB.AppendLine(Space(tab.XX) + "private Collection _Collection;");
            strB.AppendLine(Space(tab.XX) + "private Person _Scanner;");
            strB.AppendLine(Space(tab.None));
            strB.AppendLine(Space(tab.XX) + "public Person Scanner");
            strB.AppendLine(Space(tab.XX) + "{");
            strB.AppendLine(Space(tab.XXX) + "get { return _Scanner; }");
            strB.AppendLine(Space(tab.XXX) + "set");
            strB.AppendLine(Space(tab.XXX) + "{");
            strB.AppendLine(Space(tab.XXXX) + "if (value == null) value = Person.CreateEmpty();");
            strB.AppendLine(Space(tab.XXXX) + "_Scanner = value; _spec.ScannedByID = value.ID;");
            strB.AppendLine(Space(tab.XXX) + "}");
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.XX) + "public Collection Collection");
            strB.AppendLine(Space(tab.XX) + "{");
            strB.AppendLine(Space(tab.XXX) + "get { return _Collection; }");
            strB.AppendLine(Space(tab.XXX) + "set");
            strB.AppendLine(Space(tab.XXX) + "{");
            strB.AppendLine(Space(tab.XXXX) + "if (value == null) value = Collection.CreateEmpty();");
            strB.AppendLine(Space(tab.XXXX) + "_Collection = value; _spec.CollectionID = value.ID;");
            strB.AppendLine(Space(tab.XXX) + "}");
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.None) + "");
            strB.AppendLine(Space(tab.XX) + "public SpecimenViewModel(Specimen sp)");
            strB.AppendLine(Space(tab.XX) + "{");
            strB.AppendLine(Space(tab.XXX) + "_Spec = sp;");
            strB.AppendLine(Space(tab.XXX) + "Collection = IVLDAL.GetCollection(sp.CollectionID);");
            strB.AppendLine(Space(tab.XXX) + "Scanner = IVLDAL.GetPerson(sp.ScannedByID);");
            strB.AppendLine(Space(tab.None) + "");
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.X) + "}");
            strB.AppendLine(Space(tab.None) + "}");
        }
        return strB.ToString();
    }
}

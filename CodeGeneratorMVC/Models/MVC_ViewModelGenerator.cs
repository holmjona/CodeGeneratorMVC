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
using Words;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;

public class MVC_ViewModelGenerator {
    private string getIndexGET(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
        } else {
            strB.AppendLine(Strings.Space((int)tab.None) + "namespace ViewModels");
            strB.AppendLine(Strings.Space((int)tab.None) + "{");
            strB.AppendLine(Strings.Space((int)tab.X) + "public Class SpecimenViewModel");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "private " + pClass.Name.Capitalized() + " _spec;");
            strB.AppendLine(Strings.Space((int)tab.None));
            strB.AppendLine(Strings.Space((int)tab.XX) + "public " + pClass.Name.Capitalized() + " Obj { get { return _spec; } }");
            strB.AppendLine(Strings.Space((int)tab.XX) + "private Collection _Collection;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "private Person _Scanner;");
            strB.AppendLine(Strings.Space((int)tab.None));
            strB.AppendLine(Strings.Space((int)tab.XX) + "public Person Scanner");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "get { return _Scanner; }");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "set");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (value == null) value = Person.CreateEmpty();");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "_Scanner = value; _spec.ScannedByID = value.ID;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public Collection Collection");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "get { return _Collection; }");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "set");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (value == null) value = Collection.CreateEmpty();");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "_Collection = value; _spec.CollectionID = value.ID;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.None) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public SpecimenViewModel(Specimen sp)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "_Spec = sp;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Collection = IVLDAL.GetCollection(sp.CollectionID);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Scanner = IVLDAL.GetPerson(sp.ScannedByID);");
            strB.AppendLine(Strings.Space((int)tab.None) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
            strB.AppendLine(Strings.Space((int)tab.None) + "}");
        }
        return strB.ToString();
    }
}

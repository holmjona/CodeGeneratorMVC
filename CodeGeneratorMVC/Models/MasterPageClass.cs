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
using System.ComponentModel;

[Serializable()]
public class MasterPageClass {
    private string _fileName;
    private BindingList<MasterPageContent> _MasterPageContents = new BindingList<MasterPageContent>();
    private string _Name;
    private string _TitleName;
    private string _SubTitleName;
    private string _PageInstructionsName;
    private string _BodyName;
    public string TitleName {
        get {
            return _TitleName;
        }
        set {
            _TitleName = value;
        }
    }
    public string SubTitleName {
        get {
            return _SubTitleName;
        }
        set {
            _SubTitleName = value;
        }
    }
    public string PageInstructionsName {
        get {
            return _PageInstructionsName;
        }
        set {
            _PageInstructionsName = value;
        }
    }
    public string BodyName {
        get {
            return _BodyName;
        }
        set {
            _BodyName = value;
        }
    }
    public string FileName {
        get {
            return _fileName;
        }
        set {
            string tempName = value;
            // Dim perLoc As Integer = value.LastIndexOf(".")
            string ext = System.IO.Path.GetExtension(tempName);
            if (ext != ".master")
                tempName += ".master";
            _fileName = tempName;
        }
    }
    public string Name {
        get {
            return _Name;
        }
        set {
            _Name = value;
        }
    }
    public BindingList<MasterPageContent> MasterPageContents {
        get {
            return _MasterPageContents;
        }
        set {
            _MasterPageContents = value;
        }
    }

    //public static List<MasterPageClass> getMasterPagesForProject(DTE2 _applicationObject) {
    //    List<MasterPageClass> retList = new List<MasterPageClass>();
    //    if (_applicationObject != null && _applicationObject.Solution != null) {
    //        if (_applicationObject.Solution.Projects.Count > 0) {
    //            Project myProject = _applicationObject.Solution.Projects.Item(1);
    //            if (myProject != null) {
    //                StaticVariables.Instance.IsProjectOpen = true;
    //                foreach (ProjectItem projItem in myProject.ProjectItems)
    //                    fillMasterPages(projItem, ref retList);
    //            }
    //        }
    //    }
    //    return retList;
    //}
    //private static void fillMasterPages(ProjectItem myItem, ref List<MasterPageClass> listOfMasterPageClasses) {
    //    try {
    //        if (myItem.Name.ToLower().Contains(".master")) {
    //            if (!myItem.Name.Contains(".master.vb")) {
    //                MasterPageClass newMasterPage = new MasterPageClass();
    //                newMasterPage.FileName = myItem.Name;
    //                newMasterPage.Name = System.IO.Path.GetFileNameWithoutExtension(myItem.Name);
    //                // MsgBox(myItem.Document.FullName)
    //                string myValue = myItem.ContainingProject.FullName;
    //                newMasterPage.MasterPageContents = new BindingList<MasterPageContent>();
    //                FillMasterPageContent(ref newMasterPage, myValue + myItem.Name);
    //                listOfMasterPageClasses.Add(newMasterPage);
    //            }

    //            if (myItem.Document != null) {
    //                if (myItem.Document.FullName.ToLower().Contains(".master") && !myItem.Document.FullName.ToLower().Contains(".master.vb")) {
    //                    MasterPageClass newMasterPage = new MasterPageClass();
    //                    newMasterPage.FileName = myItem.Document.FullName;
    //                    // MsgBox(myItem.Document.FullName)
    //                    FillMasterPageContent(ref newMasterPage, myItem.Document.FullName);
    //                    listOfMasterPageClasses.Add(newMasterPage);
    //                }
    //            }
    //        }
    //    } catch (Exception ex) {
    //    }
    //    if (myItem.ProjectItems != null && myItem.ProjectItems.Count > 0) {
    //        foreach (ProjectItem projItem in myItem.ProjectItems)
    //            fillMasterPages(projItem, ref listOfMasterPageClasses);
    //    }
    //}
    private static void FillMasterPageContent(ref MasterPageClass newMasterPage, string FileName) {
        if (System.IO.File.Exists(FileName)) {
            System.IO.StreamReader myRead = System.IO.File.OpenText(FileName);
            string myString = myRead.ReadToEnd();
            myString = myString.ToLower();
            int indexOfCPH = myString.IndexOf("contentplaceholder");
            int contentPlaceHolderIndex = 0;
            while (indexOfCPH > -1) {
                // indexOfCPH = myString.IndexOf("contentplaceholder")
                myString = myString.Remove(0, indexOfCPH);
                myString = myString.Remove(0, myString.IndexOf("id="));
                // Dim newMasterPageContent As New MasterPageContent
                // newMasterPageContent.Name = myString.Substring(0, myString.IndexOf(" "))
                string strID = myString.Substring(0, myString.IndexOf(" "));
                strID = strID.Remove(0, 7);
                strID = strID.Remove(strID.Length - 1, 1);
                contentPlaceHolderIndex += 1;
                if (contentPlaceHolderIndex == 1)
                    newMasterPage.SubTitleName = strID;
                else if (contentPlaceHolderIndex == 2)
                    newMasterPage.TitleName = strID;
                else if (contentPlaceHolderIndex == 3)
                    newMasterPage.PageInstructionsName = strID;
                else if (contentPlaceHolderIndex == 4)
                    newMasterPage.BodyName = strID;
                // If strID.ToLower.Contains("subtitle") Then
                // newMasterPage.SubTitleName = strID
                // ElseIf strID.ToLower.Contains("title") Then
                // newMasterPage.TitleName = strID
                // ElseIf strID.ToLower.Contains("body") Then
                // newMasterPage.BodyName = strID
                // End If
                // newMasterPageContent.Name = myString.Substring(0, myString.IndexOf(" "))
                indexOfCPH = myString.IndexOf("contentplaceholder");
            }
        }
    }
    public MasterPageClass Self {
        get {
            return this;
        }
    }
    public override string ToString() {
        return _Name;
    }
}

Imports EnvDTE
Imports System.Xml.Linq
Imports System.Reflection
Imports System.IO
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Linq
Imports System.Windows.Forms
Imports System.Drawing

Public Class GenerateCodeForm
    Private _langToUse As CodeGeneration.Language = CodeGeneration.Language.VisualBasic
    Private _codeFormat As CodeGeneration.Format = CodeGeneration.Format.ASPX
    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'updateCodeToggles()

    End Sub
    Dim myParent As CodeGeneratorForm
    Private _SelectedOption As GenerationOptions = GenerationOptions.CreateFiles
    Private _ClassesToBeGenerated As New BindingList(Of ProjectClass)
    Private _ClassesNotToBeGenerated As New BindingList(Of ProjectClass)
    Private _Pages As New List(Of String)
    Private _CancelSave As Boolean = False
    Private _numberOfLinesGenerated As Integer = 0
    Private _numberOfFilesGenerated As Integer = 0
    Private _OverWriteAll As Boolean = False
    Enum GenerationOptions
        CreateFiles = 1
        GenerateToClipboard = 2
        InsertCode = 3
        CreateSQLFile = 4
    End Enum
    Public Sub New(ByVal SelectedOption As GenerationOptions, ByVal parentForm As CodeGeneratorForm)
        Me.New()
        _SelectedOption = SelectedOption
        myParent = parentForm
        _Pages = getPages()
        cbPages.DataSource = _Pages
        If SelectedOption <> GenerationOptions.InsertCode Then
            gbInsertOptions.Visible = False
            cbPages.Visible = False
        Else
            SetGroupBoxVisibilities(".aspx")
        End If
        grpFormsUse.Enabled = chkEditPageHTML.Checked
        If SelectedOption <> GenerationOptions.CreateFiles Then
            'DisableParentNodes()
        End If
        pnlInsert.Visible = SelectedOption = GenerationOptions.InsertCode
        pnlCreateFiles.Visible = SelectedOption <> GenerationOptions.InsertCode
        cbPages.Enabled = False
        For Each pClass As ProjectClass In StaticVariables.Instance.ListOfProjectClasses
            If pClass.IsSelected Then
                _ClassesToBeGenerated.Add(pClass)
            Else
                _ClassesNotToBeGenerated.Add(pClass)
            End If
        Next
        If SelectedOption <> GenerationOptions.InsertCode Then
            dgvClassesToGenerate.DataSource = _ClassesToBeGenerated
        End If
        dgbClassesNotToBeGenerated.DataSource = _ClassesNotToBeGenerated
        setDataGridColors()
        If SelectedOption = GenerationOptions.CreateSQLFile Then
            pnlCreateFiles.Visible = False
            pnlInsert.Visible = False
        End If
    End Sub
    Private Sub checkWeb_Application(ByVal markChecked As Boolean)
        checkApp_Code(markChecked)
        checkMaster_Pages(markChecked)
        checkClassWebPages(markChecked)
    End Sub
    Private Sub checkApp_Code(ByVal markChecked As Boolean)
        chkApp_Code.Checked = markChecked
        chkClasses.Checked = markChecked
        chkDAL.Checked = markChecked
    End Sub
    Private Sub checkMaster_Pages(ByVal markChecked As Boolean)
        chkMaster.Checked = markChecked
        chkMaster_aspx.Checked = markChecked
        chkMasterCodeBehind.Checked = markChecked
    End Sub
    Private Sub checkClassWebPages(ByVal markChecked As Boolean)
        chkClassWebPages.Checked = markChecked
        checkEditPages(markChecked)
        checkViewPages(markChecked)
    End Sub
    Private Sub checkEditPages(ByVal markChecked As Boolean)
        chkEditPage.Checked = markChecked
        chkEditPageHTML.Checked = markChecked
        chkEditPageCodeBehind.Checked = markChecked
    End Sub
    Private Sub checkViewPages(ByVal markChecked As Boolean)
        chkViewAll.Checked = markChecked
        chkViewAllHTML.Checked = markChecked
        chkViewAllCodeBehind.Checked = markChecked
    End Sub
    Private Sub checkThemes(ByVal markChecked As Boolean)
        chkThemes.Checked = markChecked
        chkDefaultStyle.Checked = markChecked
        chkDefaultLayout.Checked = markChecked
    End Sub
    Private Sub UnCheckAllItems()
        chkWebApplication.Checked = False
        chkApp_Code.Checked = False
        chkClasses.Checked = False
        chkDAL.Checked = False
        chkMaster.Checked = False
        chkMaster_aspx.Checked = False
        chkMasterCodeBehind.Checked = False
        chkClassWebPages.Checked = False
        chkEditPage.Checked = False
        chkEditPageCodeBehind.Checked = False
        chkEditPageHTML.Checked = False
        chkViewAll.Checked = False
        chkViewAllCodeBehind.Checked = False
        chkViewAllHTML.Checked = False
        chkThemes.Checked = False
        chkDefaultStyle.Checked = False
        chkDefaultLayout.Checked = False
    End Sub
    Private Sub DisableParentNodes()
        chkWebApplication.Enabled = False
        chkApp_Code.Enabled = False
        chkMaster.Enabled = False
        chkClassWebPages.Enabled = False
        chkEditPage.Enabled = False
        chkViewAll.Enabled = False
        chkThemes.Enabled = False
        chkDefaultStyle.Checked = False
        chkDefaultLayout.Checked = False
    End Sub
    Private Sub chkWebApplication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWebApplication.CheckedChanged
        checkWeb_Application(chkWebApplication.Checked)
    End Sub

    Private Sub chkApp_Code_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkApp_Code.CheckedChanged
        checkApp_Code(chkApp_Code.Checked)
    End Sub

    Private Sub chkMaster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMaster.CheckedChanged
        checkMaster_Pages(chkMaster.Checked)
    End Sub

    Private Sub chkClassWebPages_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkClassWebPages.CheckedChanged
        checkClassWebPages(chkClassWebPages.Checked)
    End Sub

    Private Sub chkEditPage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditPage.CheckedChanged
        checkEditPages(chkEditPage.Checked)
    End Sub

    Private Sub chkViewAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkViewAll.CheckedChanged
        checkViewPages(chkViewAll.Checked)
    End Sub
    Private Sub chkThemes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkThemes.CheckedChanged
        checkThemes(chkThemes.Checked)
    End Sub


    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        _CancelSave = False
        Dim folderPath As String = ""
        Dim sqlSaveFileNameAndPath As String = ""

        ' had to set the string to vbasic and csharp. 
        '       Apparently, if you have .cs on a file it will not add it as a resource.
        Dim codeStr As String = "vbasic"
        If _langToUse = CodeGeneration.Language.CSharp Then codeStr = "csharp"
        Dim codeExt As String = codeStr.Substring(0, 2) ' cs or vb

        Dim fileName_BasePage As String = "BasePage." & codeStr & ".txt"
        Dim fileName_SessionVariables As String = "SessionVariables." & codeStr & ".txt"
        Dim fileName_DefaultASPX As String = "DefaultMaster." & codeStr & ".txt"
        Dim fileName_DefaultCodeBehind As String = "DefaultMasterCodeBehind." & codeStr & ".txt"

        'btnGenerate.Visible = True
        Dim copyToClipboardText As New Dictionary(Of String, Dictionary(Of String, String))
        pbGeneratingFiles.Visible = True
        If _SelectedOption = GenerationOptions.CreateFiles Then
            Dim folderDialog As New Forms.FolderBrowserDialog
            folderDialog.Description = "Select the folder where you would like the generated files to be saved."
            If folderDialog.ShowDialog.Equals(Forms.DialogResult.OK) Then
                folderPath = folderDialog.SelectedPath
                frmSavingFiles.Show()
            Else
                Exit Sub
            End If
            If chkDAL.Checked Then
                For Each dal As DALClass In StaticVariables.Instance.DALs
                    createDALFile(dal, folderPath, _langToUse)
                Next
            End If

            If chkBasePage.Checked Then
                CopyFile(fileName_BasePage, "BasePage." & codeExt, folderPath, "App_Code")
            End If
            If chkSessionVariables.Checked Then
                CopyFile(fileName_SessionVariables, "SessionVariables." & codeExt, folderPath, "App_Code")
            End If

            If chkMaster_aspx.Checked Then
                CopyFile(fileName_DefaultASPX, "DefaultMaster.master", folderPath)
            End If
            If chkMasterCodeBehind.Checked Then
                CopyFile(fileName_DefaultCodeBehind, "DefaultMaster.master." & codeExt, folderPath)
            End If
            If chkDefaultStyle.Checked Then
                CopyFile("style.css", "themes\default\style.css", folderPath)
            End If
            If chkDefaultLayout.Checked Then
                CopyFile("layout.css", "themes\default\layout.css", folderPath)
            End If
            If chkStoredProcedures.Checked Then
                sqlSaveFileNameAndPath = folderPath & "\SQL_Scripts\SPROCS\GeneratedSprocs.sql"
            End If
        ElseIf _SelectedOption = GenerationOptions.GenerateToClipboard Then
            Dim myDictionary As New Dictionary(Of String, String)
            If chkBasePage.Checked Then
                myDictionary = New Dictionary(Of String, String)
                myDictionary.Add("BasePage", getTextFromFile(fileName_BasePage))
                copyToClipboardText.Add(fileName_BasePage, myDictionary)

            End If
            If chkSessionVariables.Checked Then
                myDictionary = New Dictionary(Of String, String)
                myDictionary.Add("SessionVariables", getTextFromFile(fileName_SessionVariables))
                copyToClipboardText.Add(fileName_SessionVariables, myDictionary)

            End If
            If chkMaster_aspx.Checked Then
                myDictionary = New Dictionary(Of String, String)
                myDictionary.Add("DefaultMaster", getTextFromFile(fileName_DefaultASPX))
                copyToClipboardText.Add(fileName_DefaultASPX, myDictionary)
            End If
            If chkMasterCodeBehind.Checked Then
                myDictionary = New Dictionary(Of String, String)
                myDictionary.Add(fileName_DefaultCodeBehind, getTextFromFile("DefaultMasterCodeBehind.txt"))
                copyToClipboardText.Add(fileName_DefaultCodeBehind, myDictionary)
            End If
            If chkDefaultStyle.Checked Then
                myDictionary = New Dictionary(Of String, String)
                myDictionary.Add("style.css", getTextFromFile("style.css"))
                copyToClipboardText.Add("style.css", myDictionary)
            End If
            If chkDefaultLayout.Checked Then
                myDictionary = New Dictionary(Of String, String)
                myDictionary.Add("layout.css", getTextFromFile("layout.css"))
                copyToClipboardText.Add("layout.css", myDictionary)
            End If
        ElseIf _SelectedOption = GenerationOptions.CreateSQLFile Then
            Dim sfd As New Forms.SaveFileDialog
            sfd.Filter = "sql file|*.sql"
            If sfd.ShowDialog().Equals(Forms.DialogResult.OK) Then
                sqlSaveFileNameAndPath = sfd.FileName
            End If
        End If
        Dim sqlFileStream As FileStream = Nothing

        If _SelectedOption = GenerationOptions.CreateSQLFile _
                OrElse chkStoredProcedures.Checked Then
            If Not File.Exists(sqlSaveFileNameAndPath) Then
                createDirectoriesForPath(sqlSaveFileNameAndPath)
                sqlFileStream = File.Create(sqlSaveFileNameAndPath)
            Else
                sqlFileStream = New FileStream(sqlSaveFileNameAndPath, FileMode.Create)
            End If
        End If
        For Each pclass As ProjectClass In _ClassesToBeGenerated
            If _CancelSave Then Exit Sub
            If _SelectedOption = GenerationOptions.CreateFiles Then
                CreateFilesFor(pclass, folderPath, _langToUse)
            ElseIf _SelectedOption = GenerationOptions.GenerateToClipboard Then
                copyFiles(pclass, _langToUse, copyToClipboardText)
            End If
            If _SelectedOption = GenerationOptions.CreateSQLFile _
                OrElse chkStoredProcedures.Checked Then
                writeSQLStringForClass(sqlFileStream, pclass)
            End If

        Next
        If sqlFileStream IsNot Nothing Then
            sqlFileStream.Close()
            sqlFileStream.Dispose()
        End If
        If _SelectedOption = GenerationOptions.GenerateToClipboard Then
            Dim resultForm As New Results(copyToClipboardText)
            resultForm.Show()
        End If
        pbGeneratingFiles.Visible = False
        MsgBox(String.Format("Generated {0} Lines of Code in {1} Files.", _numberOfLinesGenerated, _numberOfFilesGenerated))
        frmSavingFiles.Hide()
    End Sub
    Private Sub createDirectoriesForPath(filePath As String)
        Dim parentDir As String = Path.GetDirectoryName(filePath)
        If Not Directory.Exists(parentDir) Then
            createDirectoriesForPath(parentDir)
            Directory.CreateDirectory(parentDir)
        End If
    End Sub
    Private Function getCreatorName() As String
        Return myParent.txtCreator.Text.Trim()
    End Function
    Private Sub writeSQLStringForClass(ByRef fStream As FileStream, pClass As ProjectClass)
        Dim spText As String = "-------------------" & pClass.Name.Capitalized & "-----------------" & vbCrLf
        spText &= SPGenerator.getSprocText(pClass, False, getCreatorName())
        Dim byteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(spText)
        fStream.Write(byteArray, 0, byteArray.Length)
    End Sub
    Private Sub copyFiles(ByVal pClass As ProjectClass, lang As CodeGeneration.Language, ByRef copyToClipboardText As Dictionary(Of String, Dictionary(Of String, String)))
        If _CancelSave Then Exit Sub
        If Not copyToClipboardText.ContainsKey(pClass.Name.Capitalized) Then
            copyToClipboardText.Add(pClass.Name.Capitalized, New Dictionary(Of String, String))
        End If
        Dim codeExt As String = ".vb"
        If lang = CodeGeneration.Language.CSharp Then codeExt = ".cs"
        Dim myDictionary As Dictionary(Of String, String) = copyToClipboardText(pClass.Name.Capitalized)
        If chkClasses.Checked Then
            myDictionary.Add(pClass.Name.Capitalized & codeExt, ClassGenerator.getEntireClass(pClass, False, getCreatorName(), lang, _codeFormat))
        End If
        If chkDAL.Checked Then
            myDictionary.Add(pClass.Name.Capitalized & " DAL", DALGenerator.getDALFunctions(pClass, False, lang))
        End If
        If chkStoredProcedures.Checked Then
            myDictionary.Add(pClass.Name.Capitalized & "SP", SPGenerator.getSprocText(pClass, False, getCreatorName()))
        End If
        If chkEditPageHTML.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getEditForm(pClass, grpFormsUseLists.Checked, lang)
            myDictionary.Add(pClass.Name.Capitalized & "Edit.aspx", generatedText)
        End If
        If chkEditPageCodeBehind.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getEditCodeBehind(pClass, lang)
            myDictionary.Add(pClass.Name.Capitalized & "Edit.aspx" & codeExt, generatedText)
        End If
        If chkViewAllHTML.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getViewForm(pClass, grpFormsUseLists.Checked, lang)
            myDictionary.Add(pClass.Name.Capitalized & "View.aspx", generatedText)
        End If
        If chkViewAllCodeBehind.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getViewCodeBehind(pClass, lang)
            myDictionary.Add(pClass.Name.Capitalized & "View.aspx" & codeExt, generatedText)
        End If

    End Sub

    Private Sub CreateFilesFor(ByVal pClass As ProjectClass, ByVal folderPath As String, lang As CodeGeneration.Language)
        If _CancelSave Then Exit Sub
        Dim codeExt As String = ".vb"
        If lang = CodeGeneration.Language.CSharp Then codeExt = ".cs"
        If chkClasses.Checked Then
            Dim generatedText As String = ClassGenerator.getEntireClass(pClass, False, getCreatorName(), lang, _codeFormat)
            CreateFileWithName(pClass.Name.Capitalized & codeExt, generatedText, folderPath, "App_Code")
        End If
        If chkDAL.Checked Then
            'Dim doc As Document
            'doc = StaticVariables.Instance.ApplicationObject.Documents.Item(pClass.DALClassVariable.Name & ".vb")
            'doc.Activate()
            'If doc IsNot Nothing Then
            '    Dim objTextDoc As TextDocument = CType(doc.Object("TextDocument"), TextDocument)
            '    Dim objEditPoint As EditPoint = CType(objTextDoc.StartPoint.CreateEditPoint(), EditPoint)
            '    If Not objTextDoc.MarkText("End Class") Then
            '        objEditPoint.Insert(DALGenerator.getDALSkeleton(pClass.DALClassVariable.ReadOnlyConnectionString.Name, pClass.DALClassVariable.EditOnlyConnectionstring.Name, pClass.DALClassVariable))
            '    End If
            '    insertTextIntoDocument(objTextDoc, DALGenerator.getDALFunctions(pClass, False), "End Class")
            '    'objEditPoint = CType(objTextDoc.EndPoint().CreateEditPoint(), EditPoint)
            '    'objEditPoint.Insert(DALGenerator.getDALFunctions(pClass, False))
            'End If
            createDAL(pClass, folderPath, lang)
        End If
        If chkEditPageHTML.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getEditForm(pClass, grpFormsUseLists.Checked, lang)
            CreateFileWithName("Edit" & pClass.Name.Capitalized & ".aspx", generatedText, folderPath)
        End If
        If chkEditPageCodeBehind.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getEditCodeBehind(pClass, lang)
            CreateFileWithName("Edit" & pClass.Name.Capitalized & ".aspx" & codeExt, generatedText, folderPath)
        End If
        If chkViewAllHTML.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getViewForm(pClass, grpFormsUseLists.Checked, lang)
            CreateFileWithName(pClass.Name.PluralAndCapitalized & ".aspx", generatedText, folderPath)
        End If
        If chkViewAllCodeBehind.Checked Then
            Dim generatedText As String
            Dim wb As New WebFormGenerator()
            generatedText = wb.getViewCodeBehind(pClass, lang)
            CreateFileWithName(pClass.Name.PluralAndCapitalized & ".aspx" & codeExt, generatedText, folderPath)
        End If
    End Sub
    Private Shared Function endOfVbFile(ByVal s As String) As Boolean
        Return s.Trim.Equals("End Class", StringComparison.InvariantCultureIgnoreCase)
    End Function
    Private Shared Function endOfCSFile(ByVal s As String) As Boolean
        Return s.Trim.Equals("}", StringComparison.InvariantCultureIgnoreCase)
    End Function
    Private Function createDAL(ByVal pClass As ProjectClass, ByVal folderPath As String, lang As CodeGeneration.Language) As Integer
        Dim codeExt As String = ".vb"
        If lang = CodeGeneration.Language.CSharp Then codeExt = ".cs"
        Dim DALname As String = pClass.DALClassVariable.Name & codeExt
        Dim DALPath = folderPath & "\App_Code\" & DALname
        Dim DALFileInfo As New FileInfo(DALPath)
        Dim lines() As String = IO.File.ReadAllLines(DALFileInfo.FullName)
        If lines.Count < 1 Then
            lines = DALGenerator.getDALSkeleton(pClass.DALClassVariable.ReadOnlyConnectionString.Name, _
                                                pClass.DALClassVariable.EditOnlyConnectionstring.Name, _
                                                pClass.DALClassVariable, lang).Split(Chr(13))
        End If
        Dim endIndex As Integer
        If lang = CodeGeneration.Language.VisualBasic Then
            endIndex = Array.FindLastIndex(Of String)(lines, AddressOf endOfVbFile)
        Else
            Dim lastIndex As Integer = Array.FindLastIndex(Of String)(lines, AddressOf endOfCSFile)
            endIndex = Array.FindLastIndex(Of String)(lines.Take(lastIndex).ToArray(), AddressOf endOfCSFile)
        End If
        Dim newLines As New List(Of String)
        For lineIndex As Integer = 0 To endIndex - 1
            newLines.Add(lines(lineIndex))
        Next
        newLines.Add(DALGenerator.getDALFunctions(pClass, False, lang))
        If endIndex = -1 Then endIndex = 0
        For lineIndex As Integer = endIndex To lines.Length - 1
            newLines.Add(lines(lineIndex))
        Next
        For Each line As String In newLines
            _numberOfLinesGenerated += line.Count(Chr(13))
        Next
        _numberOfFilesGenerated += 1
        IO.File.WriteAllLines(DALFileInfo.FullName, newLines.ToArray)

    End Function
    Private Sub createDALFile(ByVal dal As DALClass, ByVal folderPath As String, lang As CodeGeneration.Language)
        Dim codeExt As String = ".vb"
        If lang = CodeGeneration.Language.CSharp Then codeExt = ".cs"
        Dim DALname As String = dal.Name & codeExt
        Dim dirI As New DirectoryInfo(folderPath & "\App_Code")
        If Not dirI.Exists Then
            Dim dirPath As String = folderPath & "\App_Code"
            IO.Directory.CreateDirectory(dirPath)
            dirI = New DirectoryInfo(dirPath)
        End If
        Dim DALFileInfo As FileInfo = Nothing
        For Each fileI As FileInfo In dirI.GetFiles
            If fileI.Name = DALname Then
                DALFileInfo = fileI
            End If
        Next
        If DALFileInfo Is Nothing Then
            Dim DALPath = dirI.FullName & "\" & DALname
            Dim wri As IO.StreamWriter = IO.File.CreateText(DALPath)
            wri.Write(DALGenerator.getDALSkeleton(dal.ReadOnlyConnectionString.Name, dal.EditOnlyConnectionstring.Name, dal, lang))
            wri.Close()
            DALFileInfo = New FileInfo(DALPath)
        End If
    End Sub
    Private Sub CreateFileWithName(ByVal fileName As String, ByVal text As String, ByVal folderPath As String, Optional ByVal folderToAddTo As String = "")
        Try
            Dim overWriteFile As Boolean = True
            Dim pathToSave As String = folderPath & "\"
            If folderToAddTo <> "" Then
                pathToSave &= folderToAddTo & "\"
            End If
            pathToSave &= fileName
            If IO.File.Exists(pathToSave) Then
                Dim over As MsgBoxResult
                over = MsgBox("File (" & fileName & ") already exists. Do you wish to overwrite the file?", MsgBoxStyle.YesNoCancel)
                overWriteFile = over = MsgBoxResult.Yes
                If over = MsgBoxResult.Cancel Then
                    _CancelSave = True
                End If
            End If
            If overWriteFile Then
                createDirectoriesForPath(pathToSave)
                IO.File.WriteAllText(pathToSave, text)
            End If
            _numberOfLinesGenerated += text.Count(Chr(13))
            _numberOfFilesGenerated += 1

            'Dim myProject As Project = StaticVariables.Instance.ApplicationObject.Solution.Projects.Item(1)
            'If myProject IsNot Nothing Then
            '    Dim vsProject As VsWebSite.VSWebSite = CType(myProject.Object, VsWebSite.VSWebSite)
            '    Dim templateName As String = "TextFile.vstemplate"
            '    Dim address As String = System.IO.Path.Combine(vsProject.TemplatePath, "VisualBasic\1033\TextFile.zip")
            '    address = System.IO.Path.Combine(address, templateName)
            '    If folderToAddTo.Length = 0 Then
            '        myProject.ProjectItems.AddFromTemplate(address, fileName)
            '    Else
            '        myProject.ProjectItems.Item(folderToAddTo).ProjectItems.AddFromTemplate(address, fileName)
            '    End If


            'End If
            'Dim objTextDoc As TextDocument = CType(StaticVariables.Instance.ApplicationObject.ActiveDocument.Object("TextDocument"), TextDocument)
            'Dim objEditPoint As EditPoint = CType(objTextDoc.StartPoint.CreateEditPoint(), EditPoint)

            'objEditPoint.Insert(text)

        Catch ex As Exception
        End Try
    End Sub
    Private Sub insertTextIntoDocument(ByVal myDocument As TextDocument, ByVal text As String, ByVal patternToMatch As String)
        Dim objEditPoint As EditPoint = CType(myDocument.StartPoint.CreateEditPoint(), EditPoint)
        objEditPoint.FindPattern(patternToMatch, vsFindOptions.vsFindOptionsNone, objEditPoint)
        objEditPoint.LineUp()
        'Clipboard.Clear()
        'Clipboard.SetText(text)
        'objEditPoint.Paste()
        objEditPoint.Insert(text)

    End Sub


    Private Sub CopyFile(ByVal fileToCopy As String, ByVal fileToCopyTo As String, ByVal folderPath As String, Optional ByVal folderToAddTo As String = "")
        'Dim directory As String = System.Environment.CurrentDirectory
        'Dim fileAddress As String = System.IO.Path.Combine(directory, fileToCopy)
        'fileText = My.Computer.FileSystem.ReadAllText(fileToCopy)
        CreateFileWithName(fileToCopyTo, getTextFromFile(fileToCopy), folderPath, folderToAddTo)
    End Sub
    Private Function getTextFromFile(ByVal fileToFetch As String) As String
        Dim myAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim textStreamReader As StreamReader
        Dim resourceName As String = "CodeGeneratorAddIn." & fileToFetch
        If myAssembly.GetManifestResourceNames.Contains(resourceName) Then
            textStreamReader = New StreamReader(myAssembly.GetManifestResourceStream(resourceName))
        Else
            Return "Could not find file: " & fileToFetch
        End If
        Return textStreamReader.ReadToEnd()
    End Function
    Private Function getPages() As List(Of String)
        Dim retList As New List(Of String)
        If StaticVariables.Instance.ApplicationObject IsNot Nothing AndAlso StaticVariables.Instance.ApplicationObject.Solution IsNot Nothing Then
            If StaticVariables.Instance.ApplicationObject.Solution.Projects.Count > 0 Then
                Dim myProject As Project = StaticVariables.Instance.ApplicationObject.Solution.Projects.Item(1)
                fillPages(myProject.FullName, retList)
                'For Each Directory As String In System.IO.Directory.GetDirectories(myProject.FullName)
                '    fillPages(Directory, retList)
                'Next
            End If
        End If
        Return retList
    End Function
    Private Sub setTextDocument(ByRef myDocument As TextDocument, ByVal fileName As String)
        Dim retList As New List(Of String)
        If StaticVariables.Instance.ApplicationObject IsNot Nothing AndAlso StaticVariables.Instance.ApplicationObject.Solution IsNot Nothing Then
            If StaticVariables.Instance.ApplicationObject.Solution.Projects.Count > 0 Then
                Dim myProject As Project = StaticVariables.Instance.ApplicationObject.Solution.Projects.Item(1)
                fillPages(myProject.FullName, retList)
                'For Each Directory As String In System.IO.Directory.GetDirectories(myProject.FullName)
                '    fillPages(Directory, retList)
                'Next
            End If
        End If
    End Sub
    Private Sub fillPages(ByVal myDirectory As String, ByRef listOfPages As List(Of String))
        For Each Directory As String In System.IO.Directory.GetDirectories(myDirectory)
            fillPages(Directory, listOfPages)
        Next
        For Each myFile As String In System.IO.Directory.GetFiles(myDirectory)
            Dim extension As String = System.IO.Path.GetExtension(myFile).ToLower()
            If Not InvalidFileExtensions.Contains(System.IO.Path.GetExtension(myFile).ToLower()) Then
                listOfPages.Add(System.IO.Path.GetFileName(myFile))
            End If
        Next

    End Sub
    Private _invalidFileExtensions As New List(Of String)
    Public ReadOnly Property InvalidFileExtensions() As List(Of String)
        Get
            If _invalidFileExtensions.Count = 0 Then
                _invalidFileExtensions.Add(".pdb")
                _invalidFileExtensions.Add(".xml")
                _invalidFileExtensions.Add(".scc")
                _invalidFileExtensions.Add(".dll")
                _invalidFileExtensions.Add(".refresh")
                _invalidFileExtensions.Add(".jpg")
                _invalidFileExtensions.Add(".config")
                _invalidFileExtensions.Add(".css")


            End If
            Return _invalidFileExtensions
        End Get
    End Property
    Public Sub SetGroupBoxVisibilities(ByVal fileName As String)
        gbMasterPages.Visible = False
        gbApp_Code.Visible = False
        gbClassWebPages.Visible = False
        gbScripts.Visible = False
        If fileName.ToLower().Contains(".master") Then
            gbMasterPages.Visible = True
        ElseIf fileName.ToLower().Contains(".aspx") Then
            gbClassWebPages.Visible = True
        ElseIf fileName.ToLower().Contains(".vb") Then
            gbApp_Code.Visible = True
        ElseIf fileName.ToLower().Contains(".sql") Then
            gbScripts.Visible = True
        End If

    End Sub


    Private Sub rbActiveDocument_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbActiveDocument.CheckedChanged
        If StaticVariables.Instance.ApplicationObject IsNot Nothing Then
            If StaticVariables.Instance.ApplicationObject.ActiveDocument IsNot Nothing Then
                SetGroupBoxVisibilities(StaticVariables.Instance.ApplicationObject.ActiveDocument.Name)
                cbPages.Enabled = False
            End If
        End If
    End Sub

    Private Sub rbPickDocument_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPickDocument.CheckedChanged
        If cbPages.SelectedValue IsNot Nothing Then
            Dim fileString As String = cbPages.SelectedValue.ToString()
            SetGroupBoxVisibilities(fileString)
        End If
        cbPages.Enabled = True

    End Sub

    Private Sub cbPages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPages.SelectedIndexChanged
        Dim fileString As String = cbPages.SelectedValue.ToString()
        SetGroupBoxVisibilities(fileString)
    End Sub

    Private Sub dgbClassesNotToBeGenerated_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgbClassesNotToBeGenerated.CellContentClick
        If dgbClassesNotToBeGenerated.Columns(e.ColumnIndex).Name = "btnAddProjectClass" Then
            _ClassesToBeGenerated.Add(_ClassesNotToBeGenerated(e.RowIndex))
            _ClassesNotToBeGenerated.Remove(_ClassesNotToBeGenerated(e.RowIndex))
        End If

    End Sub

    Private Sub dgvClassesToGenerate_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClassesToGenerate.CellContentClick
        If dgvClassesToGenerate.Columns(e.ColumnIndex).Name = "btnRemoveClass" Then
            _ClassesNotToBeGenerated.Add(_ClassesToBeGenerated(e.RowIndex))
            _ClassesToBeGenerated.Remove(_ClassesToBeGenerated(e.RowIndex))
        End If

    End Sub

    Private Sub btnAddAll_Click(sender As System.Object, e As System.EventArgs) Handles btnAddAll.Click
        For Each pc As ProjectClass In _ClassesNotToBeGenerated
            _ClassesToBeGenerated.Add(pc)
        Next
        _ClassesNotToBeGenerated.Clear()
    End Sub

    Private Sub btnRemoveAll_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveAll.Click
        For Each pc As ProjectClass In _ClassesToBeGenerated
            _ClassesNotToBeGenerated.Add(pc)
        Next
        _ClassesToBeGenerated.Clear()
    End Sub

    Private Sub btnAddAssocEntities_Click(sender As System.Object, e As System.EventArgs) Handles btnAddAssocEntities.Click
        Dim listToMove As New List(Of ProjectClass)
        For Each pc As ProjectClass In _ClassesNotToBeGenerated.Where(Function(p) p.IsAssociateEntitiy)
            listToMove.Add(pc)
        Next
        For Each pc As ProjectClass In listToMove
            _ClassesToBeGenerated.Add(pc)
            _ClassesNotToBeGenerated.Remove(pc)
        Next
    End Sub

    Private Sub btnRemoveAssocEntities_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveAssocEntities.Click
        Dim listToMove As New List(Of ProjectClass)
        For Each pc As ProjectClass In _ClassesToBeGenerated.Where(Function(p) p.IsAssociateEntitiy)
            listToMove.Add(pc)
        Next
        For Each pc As ProjectClass In listToMove
            _ClassesToBeGenerated.Remove(pc)
            _ClassesNotToBeGenerated.Add(pc)
        Next
    End Sub

    Private Sub test(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgbClassesNotToBeGenerated.RowPrePaint, dgvClassesToGenerate.RowPrePaint
        Dim dgv As DataGridView = CType(sender, DataGridView)
        For Each r As DataGridViewRow In dgv.Rows
            SetColorBasedOnAssociatedRowObject(r)
        Next

    End Sub
    'Private Sub dgvClassesToGenerate_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvClassesToGenerate.RowsAdded, dgbClassesNotToBeGenerated.RowsAdded

    '    Dim dgv As DataGridView = CType(sender, DataGridView)
    '    Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
    '    SetColorBasedOnAssociatedRowObject(row)
    'End Sub
    Private Sub SetColorBasedOnAssociatedRowObject(rw As DataGridViewRow)
        Dim correspondingObject As ProjectClass = CType(rw.DataBoundItem, ProjectClass)

        Dim clrFore, clrBack As Color
        If correspondingObject.IsAssociateEntitiy Then
            ThemeEngine.SetRowWarningColors(clrBack, clrFore)
        Else
            ThemeEngine.SetRowGoodColors(clrBack, clrFore)
        End If
        For Each c As DataGridViewCell In rw.Cells
            c.Style.BackColor = clrBack
            c.Style.ForeColor = clrFore
        Next
    End Sub

    Private Sub setDataGridColors()
        Dim clrAssocFore, clrAssocBack As Color

        ThemeEngine.SetRowWarningColors(clrAssocBack, clrAssocFore)

        btnAddAssocEntities.ForeColor = clrAssocFore
        btnAddAssocEntities.BackColor = clrAssocBack

        btnRemoveAssocEntities.ForeColor = clrAssocFore
        btnRemoveAssocEntities.BackColor = clrAssocBack

        'ThemeEngine.SetRowGoodColors(clrBack, clrFore)

    End Sub

    Private Sub chkEditPageHTML_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEditPageHTML.CheckedChanged
        grpFormsUse.Enabled = chkEditPageHTML.Checked
    End Sub

    'Private Sub btnUseVisualBasicCode_Click(sender As System.Object, e As System.EventArgs) Handles btnUseVisualBasicCode.Click, btnUseCSharpCode.Click

    '    Dim btn As Button = CType(sender, Button)

    '    If btn.Name.Contains("CSharp") Then
    '        _langToUse = CodeGeneration.Language.CSharp
    '    Else
    '        _langToUse = CodeGeneration.Language.VisualBasic
    '    End If
    '    updateCodeToggles()
    'End Sub

    'Private Sub updateCodeToggles()
    '    Dim notSelected_BackColor As Color = Color.Gainsboro
    '    Dim notSelected_ForeColor As Color = Color.Gray
    '    Dim notSelected_BorderColor As Color = Color.Gray
    '    Dim selected_BackColor As Color = Color.RoyalBlue
    '    Dim selected_ForeColor As Color = Color.White
    '    Dim selected_BorderColor As Color = Color.DarkBlue


    '    Dim btnNotSelected As Button
    '    Dim btnSelected As Button

    '    Dim codeExt As String = ".vb"

    '    If _langToUse = CodeGeneration.Language.CSharp Then
    '        btnNotSelected = btnUseVisualBasicCode
    '        btnSelected = btnUseCSharpCode
    '        codeExt = ".cs"
    '    Else
    '        btnNotSelected = btnUseCSharpCode
    '        btnSelected = btnUseVisualBasicCode
    '    End If

    '    UpdateCodeText()

    '    chkMasterCodeBehind.Text = ".master" & codeExt
    '    chkEditPageCodeBehind.Text = ".aspx" & codeExt
    '    chkViewAllCodeBehind.Text = ".asxp" & codeExt

    '    btnNotSelected.BackColor = notSelected_BackColor
    '    btnNotSelected.ForeColor = notSelected_ForeColor
    '    btnNotSelected.Font = New Font("Arial", 12.0, Drawing.FontStyle.Regular)
    '    btnNotSelected.FlatAppearance.BorderSize = 1
    '    btnNotSelected.FlatAppearance.BorderColor = notSelected_BorderColor

    '    btnSelected.BackColor = selected_BackColor
    '    btnSelected.ForeColor = selected_ForeColor
    '    btnSelected.Font = New Font("Arial", 12.0, Drawing.FontStyle.Bold)
    '    btnSelected.FlatAppearance.BorderSize = 2
    '    btnSelected.FlatAppearance.BorderColor = selected_BorderColor

    'End Sub
    Private Sub rbCodeFormat_Update(sender As Object, e As EventArgs) Handles rbCodeVersionASPX.CheckedChanged, rbCodeVersionMVC.CheckedChanged

        If rbCodeVersionASPX.Checked Then
            _codeFormat = CodeGeneration.Format.ASPX
        Else
            _codeFormat = CodeGeneration.Format.MVC
        End If
        UpdateCodeText()
    End Sub
    Private Sub rbLanguage_Update(sender As System.Object, e As System.EventArgs) Handles rbLanguageCSharp.CheckedChanged, rbLanguateVBasic.CheckedChanged

        If rbLanguageCSharp.Checked Then
            _langToUse = CodeGeneration.Language.CSharp
        Else
            _langToUse = CodeGeneration.Language.VisualBasic
        End If
        UpdateCodeText()
    End Sub

    Private Sub UpdateCodeText()
        Dim strCode, strFormat As String

        If _langToUse = CodeGeneration.Language.CSharp Then
            strCode = rbLanguageCSharp.Text
        Else ' _langToUse = CodeGeneration.Language.VisualBasic
            strCode = rbLanguateVBasic.Text
        End If

        If _codeFormat = CodeGeneration.Format.ASPX Then
            strFormat = rbCodeVersionASPX.Text
        Else ' _codeFormat = CodeGeneration.Format.MVC
            strFormat = rbCodeVersionMVC.Text
        End If

        lblCodeToUse.Text = String.Format("{0} in {1}", strFormat, strCode)

    End Sub

End Class
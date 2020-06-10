Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing
Imports EnvDTE
Imports EnvDTE80
Imports System.Xml.Linq
Imports System.Xml
Imports System.IO
Imports System.ComponentModel
Imports System.Reflection

Public Class CodeGeneratorForm
    Private ctrolKeyPressed As Boolean
    Private Const readConDefault As String = "ReadOnlyConnectionString"
    Private Const editConDefault As String = "EditOnlyConnectionString"
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal applicationObject As DTE2)
        Try
            InitializeComponent()
            StaticVariables.Instance.ApplicationObject = applicationObject
            setDataGrids()
        Catch ex As Exception
            Dim inhere As Boolean = True
        End Try

    End Sub

    Private Sub CodeGeneratorForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim msgAnswer As MsgBoxResult
        msgAnswer = MsgBox("Would you like to save your current project?", MsgBoxStyle.YesNoCancel, "")
        If msgAnswer = MsgBoxResult.Yes Then
            handleSaveAsDialog()
        End If
        If msgAnswer = MsgBoxResult.Cancel Then
            e.Cancel = True
        End If
    End Sub


    Protected Sub PropertyCreator_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCreator.Text = System.Security.Principal.WindowsIdentity.GetCurrent.Name
    End Sub
    Protected Sub txtResults_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.ControlKey Then
            ctrolKeyPressed = True
        End If
        If e.KeyCode = Keys.A And ctrolKeyPressed Then
            Dim txtBox As TextBox = CType(sender, TextBox)
            txtBox.SelectAll()
        End If
    End Sub
    Protected Sub txtResults_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.ControlKey Then
            ctrolKeyPressed = False
        End If
    End Sub

#Region "Copy Variables"
    Private Sub txtInVars_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        txtInVars.Text = ""
        txtInVars.Text = e.Data.GetData(DataFormats.Text).ToString
    End Sub

    Private Sub txtInVars_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.Text) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
#End Region

#Region "CodeGeneratorForm Items"
    Public Sub handleSaveAsDialog()
        sfdSaveProject.Filter = "Code Generator File(*.cgf)|*.cgf"
        Dim buttonClicked As DialogResult = sfdSaveProject.ShowDialog()
        If buttonClicked.Equals(DialogResult.OK) Then
            Try
                StaticVariables.Instance.Save(sfdSaveProject.FileName)
                Me.Text = "Project " & StaticVariables.Instance.FileSaveAddress
                Me.miSaveProject.Enabled = True
                Application.DoEvents()

            Catch ex As Exception
                MessageBox.Show("Unable to save project.")
            End Try
        End If
    End Sub
    Private Sub handleOpenDialog()
        ofdOpenProject.Filter = "Code Generator File(*.cgf)|*.cgf"
        Dim btnClicked As DialogResult = ofdOpenProject.ShowDialog()

        If btnClicked.Equals(DialogResult.OK) Then
            Dim instance As AppDomain = AppDomain.CurrentDomain
            ' we were loading the file twice. I am not sure why we were doing this.
            ' It did not make sense.
            'LoadFile(ofdOpenProject.FileName, instance)
            AddHandler instance.AssemblyResolve, AddressOf handler
            LoadFile(ofdOpenProject.FileName, instance)
            StaticVariables.Instance.FileSaveAddress = ofdOpenProject.FileName
            Me.Text = "Project " & StaticVariables.Instance.FileSaveAddress
            Me.Update()
            Me.miSaveProject.Enabled = True
            Application.DoEvents()
        End If
    End Sub

    Private Sub CodeGeneratorForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then
            ctrolKeyPressed = True
        End If
        If e.KeyCode = Keys.A And ctrolKeyPressed Then
            handleSaveAsDialog()
        ElseIf e.KeyCode = Keys.S And ctrolKeyPressed Then
            Dim overWriteFile As Boolean = False
            Dim over As MsgBoxResult
            over = MsgBox("File (" & StaticVariables.Instance.FileSaveAddress & ") already exists. Do you wish to overwrite the file?", MsgBoxStyle.YesNo)
            overWriteFile = over = MsgBoxResult.Yes
            If overWriteFile Then
                saveFile(StaticVariables.Instance.FileSaveAddress)
            End If
            ElseIf e.KeyCode = Keys.O And ctrolKeyPressed Then
                handleOpenDialog()
            End If
    End Sub
    Private Sub CodeGeneratorForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then
            ctrolKeyPressed = False
        End If
    End Sub
    Private Sub saveFile(ByVal fileName As String)
        Try
            StaticVariables.Instance.Save(fileName)
            Me.miSaveProject.Enabled = True
            Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show("Unable to save project to " & sfdSaveProject.FileName & ".")
        End Try

    End Sub


    Private Sub HandleDeletingRow(ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        Dim msgAnswer As MsgBoxResult
        msgAnswer = MsgBox("Are you sure", MsgBoxStyle.YesNo, "Deleting Row")
        e.Cancel = msgAnswer = MsgBoxResult.No
    End Sub

    Private Sub clearAllVariables()
        txtInVars.Text = ""
        txtCreator.Text = ""
        'dgvClassVariables.DataSource = Nothing
        StaticVariables.Instance.SelectedProjectClasses.Clear()
        StaticVariables.Instance.ListOfProjectClasses.Clear()
        StaticVariables.Instance.DataTypes.Clear()
        StaticVariables.Instance.NameSpaceNames.Clear()
        StaticVariables.Instance.BaseClasses.Clear()
        StaticVariables.Instance.ConnectionStrings.Clear()
        StaticVariables.Instance.DALs.Clear()
        StaticVariables.Instance.MasterPages.Clear()
        setDataGrids()
    End Sub

    Private Sub GenerateCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCodeToClipBoard.Click
        Dim newForm As New GenerateCodeForm(GenerateCodeForm.GenerationOptions.GenerateToClipboard, Me)
        newForm.Show()
    End Sub

    Private Sub InsertCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miIntoCurrentDocument.Click
        Dim newForm As New GenerateCodeForm(GenerateCodeForm.GenerationOptions.InsertCode, Me)
        newForm.Show()
    End Sub
    Private Sub CreateFilesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCreateAndAddFiles.Click
        Dim newForm As New GenerateCodeForm(GenerateCodeForm.GenerationOptions.CreateFiles, Me)
        newForm.Show()
    End Sub
    Private Sub ImportFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miImportSQLFile.Click
        If variablesNotSet() Then
            ' give error to set variables.
            MsgBox("Error: Variables missing that are needed to continue.")
            tbProjectVariables.Show()
            Exit Sub
        End If
        Dim myStream As Stream = Nothing
        OpenFileDialog1.Filter = "SQL Files (*.sql) |*.sql"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim prog As New splProgress
            prog.Show("SQL Script is being imported.")
            Try
                Dim listOfClasses As List(Of ProjectClass) = SQLScriptConversion.generateObjects(OpenFileDialog1.FileName)
                For Each pClass As ProjectClass In listOfClasses
                    fillClassVariablesOfSingleInstances(pClass)
                    StaticVariables.Instance.ListOfProjectClasses.Add(pClass)
                Next
                tbTabs.SelectTab(tbProjectClasses)
            Catch ex As Exception
                MsgBox("Error Importing SQL File: " & ex.Message, MsgBoxStyle.Exclamation)
            End Try
            prog.Hide()
        End If

    End Sub
    Private Function variablesNotSet() As Boolean
        Dim retBool As Boolean = False
        If StaticVariables.Instance.MasterPages.Count < 1 Then
            ProjectVariables1.lblMasterPages.ForeColor = Color.Red
            retBool = True
        End If
        If StaticVariables.Instance.DALs.Count < 1 Then
            ProjectVariables1.lblDataAccessLayers.ForeColor = Color.Red
            retBool = True
        End If
        Return retBool
    End Function
    Private Sub fillClassVariablesOfSingleInstances(ByVal projClass As ProjectClass)
        If StaticVariables.Instance.MasterPages.Count = 1 Then
            projClass.MasterPage = StaticVariables.Instance.MasterPages(0)
        End If
        If StaticVariables.Instance.NameSpaceNames.Count = 2 Then
            projClass.NameSpaceVariable = StaticVariables.Instance.NameSpaceNames(1)
        ElseIf StaticVariables.Instance.NameSpaceNames.Count = 1 Then
            projClass.NameSpaceVariable = StaticVariables.Instance.NameSpaceNames(0)
        End If
        If StaticVariables.Instance.DALs.Count = 1 Then
            projClass.DALClassVariable = StaticVariables.Instance.DALs(0)
        End If
        Dim databaseClass As ProjectVariable = Nothing
        For Each pVar As ProjectVariable In StaticVariables.Instance.BaseClasses
            If pVar.Name.ToLower.Contains("database") Then
                databaseClass = pVar
                Exit For
            End If
        Next
        projClass.BaseClass = databaseClass
    End Sub
    Private Sub ClearAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miClearAllVariables.Click
        clearAllVariables()
    End Sub
    Private Sub ResetMasterPagesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miResetMasterPages.Click
        'StaticVariables.Instance.MasterPages = New BindingList(Of MasterPageClass)

        For Each mP As MasterPageClass In MasterPageClass.getMasterPagesForProject(StaticVariables.Instance.ApplicationObject)
            StaticVariables.Instance.MasterPages.Add(mP)
        Next
        Dim masterPageToAssign As MasterPageClass = Nothing
        If StaticVariables.Instance.MasterPages.Count > 0 Then
            masterPageToAssign = StaticVariables.Instance.MasterPages(0)
        End If
        For Each pClass As ProjectClass In StaticVariables.Instance.ListOfProjectClasses
            pClass.MasterPage = masterPageToAssign
        Next
    End Sub

    Private Function handler(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly
        Console.WriteLine("Resolving ....")
        Return GetType(StaticVariables).Assembly
    End Function
    Private Sub miOpenProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miOpenProject.Click
        handleOpenDialog()
    End Sub
    Private Sub LoadFile(ByVal fileName As String, ByVal instance As AppDomain)
        Try
            'instance.CreateInstance("Project1, Version=1.0.3513.14171, Culture=neutral, PublicKeyToken=null", "StaticVariables")
            Dim tempObject As DTE2 = StaticVariables.Instance.ApplicationObject
            StaticVariables.Instance = StaticVariables.Load(ofdOpenProject.FileName)
            StaticVariables.CleanUpObject()
            StaticVariables.Instance.ApplicationObject = tempObject
            setDataGrids()
        Catch ex As Exception
            MsgBox("Oops we had an error loading the file: " & ex.Message)
        End Try
    End Sub
    Private Sub miSaveProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miSaveProject.Click
        saveFile(StaticVariables.Instance.FileSaveAddress)
    End Sub
    Private Sub miSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miSaveAs.Click
        handleSaveAsDialog()
    End Sub

#End Region

    Private Sub setDataGrids()
        'dgvName1.DataSource = StaticVariables.Instance.NameSpaceNames
        'dgvClassVariables.DataSource = StaticVariables.Instance.VBDataTypes
        'ParameterTypeDataGridViewTextBoxColumn.DataSource = StaticVariables.Instance.DataTypes
        ProjectVariables1.SetDataGrids()
        ProjectClasses1.setDataGrids()
        DataTypes1.setDataGrids()

    End Sub

    Private Sub miExitApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miExitApp.Click
        Me.Close()
    End Sub

    Private Sub miCreateSQLFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCreateSQLFile.Click
        Dim newForm As New GenerateCodeForm(GenerateCodeForm.GenerationOptions.CreateSQLFile, Me)
        newForm.Show()
    End Sub
   

    
    Private Sub tsmiTesting_Click(sender As System.Object, e As System.EventArgs) Handles tsmiTesting.Click
        Dim tst As New Testing
        tst.Show()

    End Sub
End Class

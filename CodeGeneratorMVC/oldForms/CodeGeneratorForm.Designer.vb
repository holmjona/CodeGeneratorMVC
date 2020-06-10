<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CodeGeneratorForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CodeGeneratorForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.miImportFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.miImportSQLFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.miClearAllVariables = New System.Windows.Forms.ToolStripMenuItem()
        Me.miResetMasterPages = New System.Windows.Forms.ToolStripMenuItem()
        Me.miOpenProject = New System.Windows.Forms.ToolStripMenuItem()
        Me.miSaveProject = New System.Windows.Forms.ToolStripMenuItem()
        Me.miSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.miExitApp = New System.Windows.Forms.ToolStripMenuItem()
        Me.CodeGenerationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.miCodeToClipBoard = New System.Windows.Forms.ToolStripMenuItem()
        Me.miInsertCode = New System.Windows.Forms.ToolStripMenuItem()
        Me.miIntoCurrentDocument = New System.Windows.Forms.ToolStripMenuItem()
        Me.IntoSpecificFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.miCreateAndAddFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.miCreateSQLFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.miTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.miOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTesting = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.tbTabs = New System.Windows.Forms.TabControl()
        Me.tbProjectVariables = New System.Windows.Forms.TabPage()
        Me.chkAddResults = New System.Windows.Forms.CheckBox()
        Me.tbProjectClasses = New System.Windows.Forms.TabPage()
        Me.tbInsertVBCode = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtInVars = New System.Windows.Forms.TextBox()
        Me.tbDataTypes = New System.Windows.Forms.TabPage()
        Me.TextPropertyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ofdOpenProject = New System.Windows.Forms.OpenFileDialog()
        Me.sfdSaveProject = New System.Windows.Forms.SaveFileDialog()
        Me.DataGridViewButtonColumn1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn3 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn4 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn5 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn6 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn7 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn8 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn9 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ClassVariablesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridViewComboBoxColumn10 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn11 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn12 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn13 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn14 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn15 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn16 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn17 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn18 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.lblCreator = New System.Windows.Forms.Label()
        Me.txtCreator = New System.Windows.Forms.TextBox()
        Me.DataGridViewComboBoxColumn19 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn20 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn21 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewComboBoxColumn22 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ProjectVariables1 = New CodeGeneratorAddIn.ProjectVariables()
        Me.ProjectClasses1 = New CodeGeneratorAddIn.ProjectClasses()
        Me.DataTypes1 = New CodeGeneratorAddIn.DataTypes()
        Me.ProjectClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ProjectVariableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataTypeBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DALClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MasterPageClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MasterPageClassBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ClassVariableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ProjectClassBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MasterPageContentBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.tbTabs.SuspendLayout()
        Me.tbProjectVariables.SuspendLayout()
        Me.tbProjectClasses.SuspendLayout()
        Me.tbInsertVBCode.SuspendLayout()
        Me.tbDataTypes.SuspendLayout()
        CType(Me.TextPropertyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassVariablesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProjectVariableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTypeBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DALClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterPageClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterPageClassBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassVariableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProjectClassBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterPageContentBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.CodeGenerationToolStripMenuItem, Me.miTools})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1029, 24)
        Me.MenuStrip1.TabIndex = 26
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miImportFile, Me.miClearAllVariables, Me.miResetMasterPages, Me.miOpenProject, Me.miSaveProject, Me.miSaveAs, Me.miExitApp})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'miImportFile
        '
        Me.miImportFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miImportSQLFile})
        Me.miImportFile.Name = "miImportFile"
        Me.miImportFile.Size = New System.Drawing.Size(172, 22)
        Me.miImportFile.Text = "&ImportFile"
        '
        'miImportSQLFile
        '
        Me.miImportSQLFile.Name = "miImportSQLFile"
        Me.miImportSQLFile.Size = New System.Drawing.Size(116, 22)
        Me.miImportSQLFile.Text = "S&QL File"
        '
        'miClearAllVariables
        '
        Me.miClearAllVariables.Name = "miClearAllVariables"
        Me.miClearAllVariables.Size = New System.Drawing.Size(172, 22)
        Me.miClearAllVariables.Text = "&Clear All Variables"
        '
        'miResetMasterPages
        '
        Me.miResetMasterPages.Name = "miResetMasterPages"
        Me.miResetMasterPages.Size = New System.Drawing.Size(172, 22)
        Me.miResetMasterPages.Text = "&Reset MasterPages"
        '
        'miOpenProject
        '
        Me.miOpenProject.Name = "miOpenProject"
        Me.miOpenProject.Size = New System.Drawing.Size(172, 22)
        Me.miOpenProject.Text = "&Open"
        '
        'miSaveProject
        '
        Me.miSaveProject.Enabled = False
        Me.miSaveProject.Name = "miSaveProject"
        Me.miSaveProject.Size = New System.Drawing.Size(172, 22)
        Me.miSaveProject.Text = "&Save"
        '
        'miSaveAs
        '
        Me.miSaveAs.Name = "miSaveAs"
        Me.miSaveAs.Size = New System.Drawing.Size(172, 22)
        Me.miSaveAs.Text = "S&ave As"
        '
        'miExitApp
        '
        Me.miExitApp.Name = "miExitApp"
        Me.miExitApp.Size = New System.Drawing.Size(172, 22)
        Me.miExitApp.Text = "E&xit"
        '
        'CodeGenerationToolStripMenuItem
        '
        Me.CodeGenerationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miCodeToClipBoard, Me.miInsertCode, Me.miCreateAndAddFiles, Me.miCreateSQLFile})
        Me.CodeGenerationToolStripMenuItem.Name = "CodeGenerationToolStripMenuItem"
        Me.CodeGenerationToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.CodeGenerationToolStripMenuItem.Text = "&Generate"
        '
        'miCodeToClipBoard
        '
        Me.miCodeToClipBoard.Name = "miCodeToClipBoard"
        Me.miCodeToClipBoard.Size = New System.Drawing.Size(239, 22)
        Me.miCodeToClipBoard.Text = "Copy Code To Cli&pboard"
        '
        'miInsertCode
        '
        Me.miInsertCode.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miIntoCurrentDocument, Me.IntoSpecificFileToolStripMenuItem})
        Me.miInsertCode.Name = "miInsertCode"
        Me.miInsertCode.Size = New System.Drawing.Size(239, 22)
        Me.miInsertCode.Text = "&Insert Code"
        Me.miInsertCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'miIntoCurrentDocument
        '
        Me.miIntoCurrentDocument.Name = "miIntoCurrentDocument"
        Me.miIntoCurrentDocument.Size = New System.Drawing.Size(197, 22)
        Me.miIntoCurrentDocument.Text = "Into Current Document"
        '
        'IntoSpecificFileToolStripMenuItem
        '
        Me.IntoSpecificFileToolStripMenuItem.Enabled = False
        Me.IntoSpecificFileToolStripMenuItem.Name = "IntoSpecificFileToolStripMenuItem"
        Me.IntoSpecificFileToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.IntoSpecificFileToolStripMenuItem.Text = "Into Specific File"
        '
        'miCreateAndAddFiles
        '
        Me.miCreateAndAddFiles.Name = "miCreateAndAddFiles"
        Me.miCreateAndAddFiles.Size = New System.Drawing.Size(239, 22)
        Me.miCreateAndAddFiles.Text = "&Create and Add  Files to Folder"
        '
        'miCreateSQLFile
        '
        Me.miCreateSQLFile.Name = "miCreateSQLFile"
        Me.miCreateSQLFile.Size = New System.Drawing.Size(239, 22)
        Me.miCreateSQLFile.Text = "Create SQL File"
        '
        'miTools
        '
        Me.miTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miOptions, Me.tsmiTesting})
        Me.miTools.Name = "miTools"
        Me.miTools.Size = New System.Drawing.Size(48, 20)
        Me.miTools.Text = "&Tools"
        '
        'miOptions
        '
        Me.miOptions.Enabled = False
        Me.miOptions.Name = "miOptions"
        Me.miOptions.Size = New System.Drawing.Size(152, 22)
        Me.miOptions.Text = "&Options"
        '
        'tsmiTesting
        '
        Me.tsmiTesting.Name = "tsmiTesting"
        Me.tsmiTesting.Size = New System.Drawing.Size(152, 22)
        Me.tsmiTesting.Text = "Testing"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'tbTabs
        '
        Me.tbTabs.Controls.Add(Me.tbProjectVariables)
        Me.tbTabs.Controls.Add(Me.tbProjectClasses)
        Me.tbTabs.Controls.Add(Me.tbInsertVBCode)
        Me.tbTabs.Controls.Add(Me.tbDataTypes)
        Me.tbTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbTabs.Location = New System.Drawing.Point(0, 24)
        Me.tbTabs.Name = "tbTabs"
        Me.tbTabs.SelectedIndex = 0
        Me.tbTabs.Size = New System.Drawing.Size(1029, 714)
        Me.tbTabs.TabIndex = 32
        '
        'tbProjectVariables
        '
        Me.tbProjectVariables.Controls.Add(Me.ProjectVariables1)
        Me.tbProjectVariables.Controls.Add(Me.chkAddResults)
        Me.tbProjectVariables.Location = New System.Drawing.Point(4, 22)
        Me.tbProjectVariables.Name = "tbProjectVariables"
        Me.tbProjectVariables.Padding = New System.Windows.Forms.Padding(3)
        Me.tbProjectVariables.Size = New System.Drawing.Size(1021, 688)
        Me.tbProjectVariables.TabIndex = 2
        Me.tbProjectVariables.Text = "Step 1: Project Variables"
        Me.tbProjectVariables.ToolTipText = "Variables that apply to the whole project"
        Me.tbProjectVariables.UseVisualStyleBackColor = True
        '
        'chkAddResults
        '
        Me.chkAddResults.AutoSize = True
        Me.chkAddResults.Location = New System.Drawing.Point(808, 9)
        Me.chkAddResults.Name = "chkAddResults"
        Me.chkAddResults.Size = New System.Drawing.Size(83, 17)
        Me.chkAddResults.TabIndex = 2
        Me.chkAddResults.Text = "Add Results"
        Me.chkAddResults.UseVisualStyleBackColor = True
        Me.chkAddResults.Visible = False
        '
        'tbProjectClasses
        '
        Me.tbProjectClasses.Controls.Add(Me.ProjectClasses1)
        Me.tbProjectClasses.Location = New System.Drawing.Point(4, 22)
        Me.tbProjectClasses.Name = "tbProjectClasses"
        Me.tbProjectClasses.Padding = New System.Windows.Forms.Padding(3)
        Me.tbProjectClasses.Size = New System.Drawing.Size(1021, 688)
        Me.tbProjectClasses.TabIndex = 0
        Me.tbProjectClasses.Text = "Step 2: Project Classes"
        Me.tbProjectClasses.UseVisualStyleBackColor = True
        '
        'tbInsertVBCode
        '
        Me.tbInsertVBCode.Controls.Add(Me.Label13)
        Me.tbInsertVBCode.Controls.Add(Me.txtInVars)
        Me.tbInsertVBCode.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.tbInsertVBCode.Location = New System.Drawing.Point(4, 22)
        Me.tbInsertVBCode.Name = "tbInsertVBCode"
        Me.tbInsertVBCode.Padding = New System.Windows.Forms.Padding(3)
        Me.tbInsertVBCode.Size = New System.Drawing.Size(1021, 688)
        Me.tbInsertVBCode.TabIndex = 4
        Me.tbInsertVBCode.Text = "Insert VB Class"
        Me.tbInsertVBCode.ToolTipText = "Not Yet Fully Functional"
        Me.tbInsertVBCode.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(15, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 23
        Me.Label13.Text = "VB Class Text"
        '
        'txtInVars
        '
        Me.txtInVars.AllowDrop = True
        Me.txtInVars.Location = New System.Drawing.Point(18, 26)
        Me.txtInVars.Multiline = True
        Me.txtInVars.Name = "txtInVars"
        Me.txtInVars.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInVars.Size = New System.Drawing.Size(346, 226)
        Me.txtInVars.TabIndex = 22
        Me.txtInVars.WordWrap = False
        '
        'tbDataTypes
        '
        Me.tbDataTypes.Controls.Add(Me.DataTypes1)
        Me.tbDataTypes.Location = New System.Drawing.Point(4, 22)
        Me.tbDataTypes.Name = "tbDataTypes"
        Me.tbDataTypes.Padding = New System.Windows.Forms.Padding(3)
        Me.tbDataTypes.Size = New System.Drawing.Size(1021, 688)
        Me.tbDataTypes.TabIndex = 5
        Me.tbDataTypes.Text = "DataTypes"
        Me.tbDataTypes.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'ofdOpenProject
        '
        Me.ofdOpenProject.FileName = "OpenFileDialog2"
        '
        'DataGridViewButtonColumn1
        '
        Me.DataGridViewButtonColumn1.DataPropertyName = "Self"
        Me.DataGridViewButtonColumn1.HeaderText = "Self"
        Me.DataGridViewButtonColumn1.Name = "DataGridViewButtonColumn1"
        Me.DataGridViewButtonColumn1.ReadOnly = True
        Me.DataGridViewButtonColumn1.Text = "View Contents"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "NameSpaceName"
        Me.DataGridViewTextBoxColumn4.HeaderText = "NameSpaceName"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewComboBoxColumn1
        '
        Me.DataGridViewComboBoxColumn1.DataPropertyName = "DALClassVariable"
        Me.DataGridViewComboBoxColumn1.HeaderText = "DALClassVariable"
        Me.DataGridViewComboBoxColumn1.Name = "DataGridViewComboBoxColumn1"
        Me.DataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn2
        '
        Me.DataGridViewComboBoxColumn2.DataPropertyName = "NameSpaceVariable"
        Me.DataGridViewComboBoxColumn2.HeaderText = "NameSpace"
        Me.DataGridViewComboBoxColumn2.Name = "DataGridViewComboBoxColumn2"
        Me.DataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "NameSpaceName"
        Me.DataGridViewTextBoxColumn5.HeaderText = "NameSpaceName"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewComboBoxColumn3
        '
        Me.DataGridViewComboBoxColumn3.DataPropertyName = "NameSpaceName"
        Me.DataGridViewComboBoxColumn3.HeaderText = "NameSpaceName"
        Me.DataGridViewComboBoxColumn3.Name = "DataGridViewComboBoxColumn3"
        Me.DataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn4
        '
        Me.DataGridViewComboBoxColumn4.DataPropertyName = "DALClassVariable"
        Me.DataGridViewComboBoxColumn4.HeaderText = "DALClassVariable"
        Me.DataGridViewComboBoxColumn4.Name = "DataGridViewComboBoxColumn4"
        Me.DataGridViewComboBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn5
        '
        Me.DataGridViewComboBoxColumn5.DataPropertyName = "NameSpaceVariable"
        Me.DataGridViewComboBoxColumn5.HeaderText = "NameSpace"
        Me.DataGridViewComboBoxColumn5.Name = "DataGridViewComboBoxColumn5"
        Me.DataGridViewComboBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn6
        '
        Me.DataGridViewComboBoxColumn6.DataPropertyName = "DALClassVariable"
        Me.DataGridViewComboBoxColumn6.HeaderText = "DALClassVariable"
        Me.DataGridViewComboBoxColumn6.Name = "DataGridViewComboBoxColumn6"
        Me.DataGridViewComboBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn7
        '
        Me.DataGridViewComboBoxColumn7.DataPropertyName = "NameSpaceVariable"
        Me.DataGridViewComboBoxColumn7.HeaderText = "NameSpace"
        Me.DataGridViewComboBoxColumn7.Name = "DataGridViewComboBoxColumn7"
        Me.DataGridViewComboBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn8
        '
        Me.DataGridViewComboBoxColumn8.DataPropertyName = "DALClassVariable"
        Me.DataGridViewComboBoxColumn8.HeaderText = "DALClassVariable"
        Me.DataGridViewComboBoxColumn8.Name = "DataGridViewComboBoxColumn8"
        Me.DataGridViewComboBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn9
        '
        Me.DataGridViewComboBoxColumn9.DataPropertyName = "NameSpaceVariable"
        Me.DataGridViewComboBoxColumn9.HeaderText = "NameSpace"
        Me.DataGridViewComboBoxColumn9.Name = "DataGridViewComboBoxColumn9"
        Me.DataGridViewComboBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ClassVariablesBindingSource
        '
        Me.ClassVariablesBindingSource.DataMember = "ClassVariables"
        Me.ClassVariablesBindingSource.DataSource = Me.ProjectClassBindingSource
        '
        'DataGridViewComboBoxColumn10
        '
        Me.DataGridViewComboBoxColumn10.DataPropertyName = "DALClassVariable"
        Me.DataGridViewComboBoxColumn10.HeaderText = "DALClassVariable"
        Me.DataGridViewComboBoxColumn10.Name = "DataGridViewComboBoxColumn10"
        Me.DataGridViewComboBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn11
        '
        Me.DataGridViewComboBoxColumn11.DataPropertyName = "NameSpaceVariable"
        Me.DataGridViewComboBoxColumn11.HeaderText = "NameSpace"
        Me.DataGridViewComboBoxColumn11.Name = "DataGridViewComboBoxColumn11"
        Me.DataGridViewComboBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn12
        '
        Me.DataGridViewComboBoxColumn12.DataPropertyName = "TextVariable"
        Me.DataGridViewComboBoxColumn12.HeaderText = "Text(For Lists)"
        Me.DataGridViewComboBoxColumn12.Name = "DataGridViewComboBoxColumn12"
        Me.DataGridViewComboBoxColumn12.ToolTipText = "Property to Use for Text in Lists"
        '
        'DataGridViewComboBoxColumn13
        '
        Me.DataGridViewComboBoxColumn13.DataPropertyName = "ValueVariable"
        Me.DataGridViewComboBoxColumn13.HeaderText = "Value(For Lists)"
        Me.DataGridViewComboBoxColumn13.Name = "DataGridViewComboBoxColumn13"
        Me.DataGridViewComboBoxColumn13.ToolTipText = "Property to Use for Value in Lists"
        '
        'DataGridViewComboBoxColumn14
        '
        Me.DataGridViewComboBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn14.DataPropertyName = "DALClassVariable"
        Me.DataGridViewComboBoxColumn14.HeaderText = "DALClassVariable"
        Me.DataGridViewComboBoxColumn14.Name = "DataGridViewComboBoxColumn14"
        Me.DataGridViewComboBoxColumn14.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn15
        '
        Me.DataGridViewComboBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn15.DataPropertyName = "NameSpaceVariable"
        Me.DataGridViewComboBoxColumn15.HeaderText = "NameSpace"
        Me.DataGridViewComboBoxColumn15.Name = "DataGridViewComboBoxColumn15"
        Me.DataGridViewComboBoxColumn15.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn16
        '
        Me.DataGridViewComboBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn16.DataPropertyName = "TextVariable"
        Me.DataGridViewComboBoxColumn16.HeaderText = "Text(For Lists)"
        Me.DataGridViewComboBoxColumn16.Name = "DataGridViewComboBoxColumn16"
        Me.DataGridViewComboBoxColumn16.ToolTipText = "Property to Use for Text in Lists"
        '
        'DataGridViewComboBoxColumn17
        '
        Me.DataGridViewComboBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn17.DataPropertyName = "ValueVariable"
        Me.DataGridViewComboBoxColumn17.HeaderText = "Value(For Lists)"
        Me.DataGridViewComboBoxColumn17.Name = "DataGridViewComboBoxColumn17"
        Me.DataGridViewComboBoxColumn17.ToolTipText = "Property to Use for Value in Lists"
        '
        'DataGridViewComboBoxColumn18
        '
        Me.DataGridViewComboBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn18.DataPropertyName = "NameSpaceName"
        Me.DataGridViewComboBoxColumn18.HeaderText = "NameSpaceName"
        Me.DataGridViewComboBoxColumn18.Name = "DataGridViewComboBoxColumn18"
        Me.DataGridViewComboBoxColumn18.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'lblCreator
        '
        Me.lblCreator.AutoSize = True
        Me.lblCreator.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblCreator.Location = New System.Drawing.Point(878, 5)
        Me.lblCreator.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCreator.Name = "lblCreator"
        Me.lblCreator.Padding = New System.Windows.Forms.Padding(2)
        Me.lblCreator.Size = New System.Drawing.Size(48, 17)
        Me.lblCreator.TabIndex = 18
        Me.lblCreator.Text = "Creator:"
        '
        'txtCreator
        '
        Me.txtCreator.Location = New System.Drawing.Point(927, 4)
        Me.txtCreator.Name = "txtCreator"
        Me.txtCreator.Size = New System.Drawing.Size(100, 20)
        Me.txtCreator.TabIndex = 19
        Me.txtCreator.Text = "[Enter Name]"
        '
        'DataGridViewComboBoxColumn19
        '
        Me.DataGridViewComboBoxColumn19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn19.DataPropertyName = "DALClassVariable"
        Me.DataGridViewComboBoxColumn19.HeaderText = "Data Access Layer"
        Me.DataGridViewComboBoxColumn19.Name = "DataGridViewComboBoxColumn19"
        Me.DataGridViewComboBoxColumn19.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn20
        '
        Me.DataGridViewComboBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn20.DataPropertyName = "NameSpaceVariable"
        Me.DataGridViewComboBoxColumn20.HeaderText = "NameSpace"
        Me.DataGridViewComboBoxColumn20.Name = "DataGridViewComboBoxColumn20"
        Me.DataGridViewComboBoxColumn20.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewComboBoxColumn21
        '
        Me.DataGridViewComboBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn21.DataPropertyName = "TextVariable"
        Me.DataGridViewComboBoxColumn21.HeaderText = "Text (used for Lists)"
        Me.DataGridViewComboBoxColumn21.Name = "DataGridViewComboBoxColumn21"
        Me.DataGridViewComboBoxColumn21.ToolTipText = "Property to Use for Text in Lists"
        '
        'DataGridViewComboBoxColumn22
        '
        Me.DataGridViewComboBoxColumn22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn22.DataPropertyName = "ValueVariable"
        Me.DataGridViewComboBoxColumn22.HeaderText = "Value (used for Lists)"
        Me.DataGridViewComboBoxColumn22.Name = "DataGridViewComboBoxColumn22"
        Me.DataGridViewComboBoxColumn22.ToolTipText = "Property to Use for Value in Lists"
        '
        'ProjectVariables1
        '
        Me.ProjectVariables1.Location = New System.Drawing.Point(0, 0)
        Me.ProjectVariables1.Name = "ProjectVariables1"
        Me.ProjectVariables1.Size = New System.Drawing.Size(1021, 688)
        Me.ProjectVariables1.TabIndex = 3
        '
        'ProjectClasses1
        '
        Me.ProjectClasses1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProjectClasses1.Location = New System.Drawing.Point(3, 3)
        Me.ProjectClasses1.Name = "ProjectClasses1"
        Me.ProjectClasses1.Size = New System.Drawing.Size(1015, 682)
        Me.ProjectClasses1.TabIndex = 0
        '
        'DataTypes1
        '
        Me.DataTypes1.Location = New System.Drawing.Point(-3, 0)
        Me.DataTypes1.Name = "DataTypes1"
        Me.DataTypes1.Size = New System.Drawing.Size(1021, 688)
        Me.DataTypes1.TabIndex = 0
        '
        'ProjectClassBindingSource
        '
        Me.ProjectClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.ProjectClass)
        '
        'ProjectVariableBindingSource
        '
        Me.ProjectVariableBindingSource.DataSource = GetType(CodeGeneratorAddIn.ProjectVariable)
        '
        'DataTypeBindingSource1
        '
        Me.DataTypeBindingSource1.DataSource = GetType(CodeGeneratorAddIn.DataType)
        '
        'DALClassBindingSource
        '
        Me.DALClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.DALClass)
        '
        'MasterPageClassBindingSource
        '
        Me.MasterPageClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.MasterPageClass)
        '
        'MasterPageClassBindingSource1
        '
        Me.MasterPageClassBindingSource1.DataSource = GetType(CodeGeneratorAddIn.MasterPageClass)
        '
        'ClassVariableBindingSource
        '
        Me.ClassVariableBindingSource.DataSource = GetType(CodeGeneratorAddIn.ClassVariable)
        '
        'ProjectClassBindingSource2
        '
        Me.ProjectClassBindingSource2.DataSource = GetType(CodeGeneratorAddIn.ProjectClass)
        '
        'DataTypeBindingSource
        '
        Me.DataTypeBindingSource.DataSource = GetType(CodeGeneratorAddIn.DataType)
        '
        'MasterPageContentBindingSource
        '
        Me.MasterPageContentBindingSource.DataSource = GetType(CodeGeneratorAddIn.MasterPageContent)
        '
        'CodeGeneratorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 738)
        Me.Controls.Add(Me.lblCreator)
        Me.Controls.Add(Me.tbTabs)
        Me.Controls.Add(Me.txtCreator)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "CodeGeneratorForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CodeGeneratorForm"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.tbTabs.ResumeLayout(False)
        Me.tbProjectVariables.ResumeLayout(False)
        Me.tbProjectVariables.PerformLayout()
        Me.tbProjectClasses.ResumeLayout(False)
        Me.tbInsertVBCode.ResumeLayout(False)
        Me.tbInsertVBCode.PerformLayout()
        Me.tbDataTypes.ResumeLayout(False)
        CType(Me.TextPropertyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassVariablesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProjectVariableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTypeBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DALClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterPageClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterPageClassBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassVariableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProjectClassBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterPageContentBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ClassVariableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CodeGenerationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miCodeToClipBoard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miInsertCode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miCreateAndAddFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MasterPageClassBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents tbTabs As System.Windows.Forms.TabControl
    Friend WithEvents tbProjectClasses As System.Windows.Forms.TabPage
    Friend WithEvents ProjectClassBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tbProjectVariables As System.Windows.Forms.TabPage
    Friend WithEvents MasterPageClassBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents tbInsertVBCode As System.Windows.Forms.TabPage
    Friend WithEvents txtInVars As System.Windows.Forms.TextBox
    Friend WithEvents chkAddResults As System.Windows.Forms.CheckBox
    Friend WithEvents ProjectVariableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dgvName1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miImportFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miImportSQLFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miClearAllVariables As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miExitApp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miResetMasterPages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miIntoCurrentDocument As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miOpenProject As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miSaveProject As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ofdOpenProject As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sfdSaveProject As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MasterPageContentBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewButtonColumn1 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents IntoSpecificFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DALClassBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn1 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn2 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn3 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn4 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn5 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataTypeBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents tbDataTypes As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewComboBoxColumn6 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn7 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn8 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn9 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents TextPropertyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ClassVariablesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ProjectClassBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewComboBoxColumn10 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn11 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn12 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn13 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn14 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn15 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn16 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn17 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn18 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblCreator As System.Windows.Forms.Label
    Friend WithEvents txtCreator As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewComboBoxColumn19 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn20 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn21 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn22 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ProjectVariables1 As CodeGeneratorAddIn.ProjectVariables
    Friend WithEvents ProjectClasses1 As CodeGeneratorAddIn.ProjectClasses
    Friend WithEvents DataTypes1 As CodeGeneratorAddIn.DataTypes
    Friend WithEvents miSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miCreateSQLFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiTesting As System.Windows.Forms.ToolStripMenuItem

End Class



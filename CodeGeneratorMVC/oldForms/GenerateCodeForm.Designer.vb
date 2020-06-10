<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GenerateCodeForm
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
        Me.chkClasses = New System.Windows.Forms.CheckBox()
        Me.chkEditPage = New System.Windows.Forms.CheckBox()
        Me.chkEditPageCodeBehind = New System.Windows.Forms.CheckBox()
        Me.chkEditPageHTML = New System.Windows.Forms.CheckBox()
        Me.chkViewAll = New System.Windows.Forms.CheckBox()
        Me.chkViewAllCodeBehind = New System.Windows.Forms.CheckBox()
        Me.chkViewAllHTML = New System.Windows.Forms.CheckBox()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkWebApplication = New System.Windows.Forms.CheckBox()
        Me.pnlCreateFiles = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbCodeVersionMVC = New System.Windows.Forms.RadioButton()
        Me.rbCodeVersionASPX = New System.Windows.Forms.RadioButton()
        Me.chkSessionVariables = New System.Windows.Forms.CheckBox()
        Me.chkBasePage = New System.Windows.Forms.CheckBox()
        Me.chkStoredProcedures = New System.Windows.Forms.CheckBox()
        Me.chkOther = New System.Windows.Forms.CheckBox()
        Me.chkDefaultLayout = New System.Windows.Forms.CheckBox()
        Me.chkDefaultStyle = New System.Windows.Forms.CheckBox()
        Me.chkThemes = New System.Windows.Forms.CheckBox()
        Me.chkClassWebPages = New System.Windows.Forms.CheckBox()
        Me.chkMasterCodeBehind = New System.Windows.Forms.CheckBox()
        Me.chkMaster_aspx = New System.Windows.Forms.CheckBox()
        Me.chkMaster = New System.Windows.Forms.CheckBox()
        Me.chkDAL = New System.Windows.Forms.CheckBox()
        Me.chkApp_Code = New System.Windows.Forms.CheckBox()
        Me.grpFormsUse = New System.Windows.Forms.GroupBox()
        Me.grpFormsUseDivs = New System.Windows.Forms.RadioButton()
        Me.grpFormsUseLists = New System.Windows.Forms.RadioButton()
        Me.pnlInsert = New System.Windows.Forms.Panel()
        Me.gbMasterPages = New System.Windows.Forms.GroupBox()
        Me.gbScripts = New System.Windows.Forms.GroupBox()
        Me.rbStoredProcedures = New System.Windows.Forms.RadioButton()
        Me.rbMasterVb = New System.Windows.Forms.RadioButton()
        Me.rbMaster = New System.Windows.Forms.RadioButton()
        Me.gbClassWebPages = New System.Windows.Forms.GroupBox()
        Me.rbView = New System.Windows.Forms.RadioButton()
        Me.rbEdit = New System.Windows.Forms.RadioButton()
        Me.gbApp_Code = New System.Windows.Forms.GroupBox()
        Me.rbSessionVariables = New System.Windows.Forms.RadioButton()
        Me.rbBasePage = New System.Windows.Forms.RadioButton()
        Me.rbDAL = New System.Windows.Forms.RadioButton()
        Me.rbClass = New System.Windows.Forms.RadioButton()
        Me.rbActiveDocument = New System.Windows.Forms.RadioButton()
        Me.rbPickDocument = New System.Windows.Forms.RadioButton()
        Me.gbInsertOptions = New System.Windows.Forms.GroupBox()
        Me.cbPages = New System.Windows.Forms.ComboBox()
        Me.dgvClassesToGenerate = New System.Windows.Forms.DataGridView()
        Me.NameStringDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SummaryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnRemoveClass = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ProjectClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dgbClassesNotToBeGenerated = New System.Windows.Forms.DataGridView()
        Me.NameStringDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SummaryDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnAddProjectClass = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pbGeneratingFiles = New System.Windows.Forms.ProgressBar()
        Me.btnAddAll = New System.Windows.Forms.Button()
        Me.btnRemoveAll = New System.Windows.Forms.Button()
        Me.btnAddAssocEntities = New System.Windows.Forms.Button()
        Me.btnRemoveAssocEntities = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCodeToUse = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbLanguageCSharp = New System.Windows.Forms.RadioButton()
        Me.rbLanguateVBasic = New System.Windows.Forms.RadioButton()
        Me.pnlCreateFiles.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpFormsUse.SuspendLayout()
        Me.pnlInsert.SuspendLayout()
        Me.gbMasterPages.SuspendLayout()
        Me.gbScripts.SuspendLayout()
        Me.gbClassWebPages.SuspendLayout()
        Me.gbApp_Code.SuspendLayout()
        Me.gbInsertOptions.SuspendLayout()
        CType(Me.dgvClassesToGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgbClassesNotToBeGenerated, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkClasses
        '
        Me.chkClasses.AutoSize = True
        Me.chkClasses.Location = New System.Drawing.Point(67, 72)
        Me.chkClasses.Name = "chkClasses"
        Me.chkClasses.Size = New System.Drawing.Size(62, 17)
        Me.chkClasses.TabIndex = 0
        Me.chkClasses.Text = "Classes"
        Me.chkClasses.UseVisualStyleBackColor = True
        '
        'chkEditPage
        '
        Me.chkEditPage.AutoSize = True
        Me.chkEditPage.Location = New System.Drawing.Point(65, 261)
        Me.chkEditPage.Name = "chkEditPage"
        Me.chkEditPage.Size = New System.Drawing.Size(77, 17)
        Me.chkEditPage.TabIndex = 6
        Me.chkEditPage.Text = "Edit Pages"
        Me.chkEditPage.UseVisualStyleBackColor = True
        '
        'chkEditPageCodeBehind
        '
        Me.chkEditPageCodeBehind.AutoSize = True
        Me.chkEditPageCodeBehind.Location = New System.Drawing.Point(86, 343)
        Me.chkEditPageCodeBehind.Name = "chkEditPageCodeBehind"
        Me.chkEditPageCodeBehind.Size = New System.Drawing.Size(66, 17)
        Me.chkEditPageCodeBehind.TabIndex = 7
        Me.chkEditPageCodeBehind.Text = ".aspx.vb"
        Me.chkEditPageCodeBehind.UseVisualStyleBackColor = True
        '
        'chkEditPageHTML
        '
        Me.chkEditPageHTML.AutoSize = True
        Me.chkEditPageHTML.Location = New System.Drawing.Point(86, 280)
        Me.chkEditPageHTML.Name = "chkEditPageHTML"
        Me.chkEditPageHTML.Size = New System.Drawing.Size(51, 17)
        Me.chkEditPageHTML.TabIndex = 8
        Me.chkEditPageHTML.Text = ".aspx"
        Me.chkEditPageHTML.UseVisualStyleBackColor = True
        '
        'chkViewAll
        '
        Me.chkViewAll.AutoSize = True
        Me.chkViewAll.Location = New System.Drawing.Point(66, 368)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.Size = New System.Drawing.Size(82, 17)
        Me.chkViewAll.TabIndex = 9
        Me.chkViewAll.Text = "View Pages"
        Me.chkViewAll.UseVisualStyleBackColor = True
        '
        'chkViewAllCodeBehind
        '
        Me.chkViewAllCodeBehind.AutoSize = True
        Me.chkViewAllCodeBehind.Location = New System.Drawing.Point(87, 414)
        Me.chkViewAllCodeBehind.Name = "chkViewAllCodeBehind"
        Me.chkViewAllCodeBehind.Size = New System.Drawing.Size(66, 17)
        Me.chkViewAllCodeBehind.TabIndex = 10
        Me.chkViewAllCodeBehind.Text = ".aspx.vb"
        Me.chkViewAllCodeBehind.UseVisualStyleBackColor = True
        '
        'chkViewAllHTML
        '
        Me.chkViewAllHTML.AutoSize = True
        Me.chkViewAllHTML.Location = New System.Drawing.Point(87, 391)
        Me.chkViewAllHTML.Name = "chkViewAllHTML"
        Me.chkViewAllHTML.Size = New System.Drawing.Size(51, 17)
        Me.chkViewAllHTML.TabIndex = 11
        Me.chkViewAllHTML.Text = ".aspx"
        Me.chkViewAllHTML.UseVisualStyleBackColor = True
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.Green
        Me.btnGenerate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.ForeColor = System.Drawing.Color.White
        Me.btnGenerate.Location = New System.Drawing.Point(630, 572)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(93, 34)
        Me.btnGenerate.TabIndex = 12
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(729, 572)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 34)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'chkWebApplication
        '
        Me.chkWebApplication.AutoSize = True
        Me.chkWebApplication.Location = New System.Drawing.Point(31, 15)
        Me.chkWebApplication.Name = "chkWebApplication"
        Me.chkWebApplication.Size = New System.Drawing.Size(104, 17)
        Me.chkWebApplication.TabIndex = 14
        Me.chkWebApplication.Text = "Web Application"
        Me.chkWebApplication.UseVisualStyleBackColor = True
        '
        'pnlCreateFiles
        '
        Me.pnlCreateFiles.Controls.Add(Me.GroupBox1)
        Me.pnlCreateFiles.Controls.Add(Me.chkSessionVariables)
        Me.pnlCreateFiles.Controls.Add(Me.chkBasePage)
        Me.pnlCreateFiles.Controls.Add(Me.chkStoredProcedures)
        Me.pnlCreateFiles.Controls.Add(Me.chkOther)
        Me.pnlCreateFiles.Controls.Add(Me.chkDefaultLayout)
        Me.pnlCreateFiles.Controls.Add(Me.chkDefaultStyle)
        Me.pnlCreateFiles.Controls.Add(Me.chkThemes)
        Me.pnlCreateFiles.Controls.Add(Me.chkClassWebPages)
        Me.pnlCreateFiles.Controls.Add(Me.chkMasterCodeBehind)
        Me.pnlCreateFiles.Controls.Add(Me.chkMaster_aspx)
        Me.pnlCreateFiles.Controls.Add(Me.chkMaster)
        Me.pnlCreateFiles.Controls.Add(Me.chkDAL)
        Me.pnlCreateFiles.Controls.Add(Me.chkApp_Code)
        Me.pnlCreateFiles.Controls.Add(Me.chkClasses)
        Me.pnlCreateFiles.Controls.Add(Me.chkWebApplication)
        Me.pnlCreateFiles.Controls.Add(Me.chkViewAllHTML)
        Me.pnlCreateFiles.Controls.Add(Me.chkViewAllCodeBehind)
        Me.pnlCreateFiles.Controls.Add(Me.chkEditPage)
        Me.pnlCreateFiles.Controls.Add(Me.chkViewAll)
        Me.pnlCreateFiles.Controls.Add(Me.chkEditPageCodeBehind)
        Me.pnlCreateFiles.Controls.Add(Me.chkEditPageHTML)
        Me.pnlCreateFiles.Controls.Add(Me.grpFormsUse)
        Me.pnlCreateFiles.Location = New System.Drawing.Point(9, 43)
        Me.pnlCreateFiles.Name = "pnlCreateFiles"
        Me.pnlCreateFiles.Size = New System.Drawing.Size(308, 585)
        Me.pnlCreateFiles.TabIndex = 16
        Me.pnlCreateFiles.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.GroupBox1.Controls.Add(Me.rbCodeVersionMVC)
        Me.GroupBox1.Controls.Add(Me.rbCodeVersionASPX)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(199, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(106, 58)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Code Format"
        '
        'rbCodeVersionMVC
        '
        Me.rbCodeVersionMVC.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbCodeVersionMVC.AutoSize = True
        Me.rbCodeVersionMVC.BackColor = System.Drawing.Color.LightGray
        Me.rbCodeVersionMVC.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbCodeVersionMVC.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.rbCodeVersionMVC.FlatAppearance.BorderSize = 2
        Me.rbCodeVersionMVC.FlatAppearance.CheckedBackColor = System.Drawing.Color.RoyalBlue
        Me.rbCodeVersionMVC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue
        Me.rbCodeVersionMVC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue
        Me.rbCodeVersionMVC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbCodeVersionMVC.Location = New System.Drawing.Point(57, 19)
        Me.rbCodeVersionMVC.Name = "rbCodeVersionMVC"
        Me.rbCodeVersionMVC.Size = New System.Drawing.Size(44, 27)
        Me.rbCodeVersionMVC.TabIndex = 1
        Me.rbCodeVersionMVC.Text = "MVC"
        Me.rbCodeVersionMVC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbCodeVersionMVC.UseVisualStyleBackColor = False
        '
        'rbCodeVersionASPX
        '
        Me.rbCodeVersionASPX.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbCodeVersionASPX.AutoSize = True
        Me.rbCodeVersionASPX.BackColor = System.Drawing.Color.LightGray
        Me.rbCodeVersionASPX.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbCodeVersionASPX.Checked = True
        Me.rbCodeVersionASPX.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.rbCodeVersionASPX.FlatAppearance.BorderSize = 2
        Me.rbCodeVersionASPX.FlatAppearance.CheckedBackColor = System.Drawing.Color.RoyalBlue
        Me.rbCodeVersionASPX.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue
        Me.rbCodeVersionASPX.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue
        Me.rbCodeVersionASPX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbCodeVersionASPX.Location = New System.Drawing.Point(6, 19)
        Me.rbCodeVersionASPX.Name = "rbCodeVersionASPX"
        Me.rbCodeVersionASPX.Size = New System.Drawing.Size(49, 27)
        Me.rbCodeVersionASPX.TabIndex = 0
        Me.rbCodeVersionASPX.TabStop = True
        Me.rbCodeVersionASPX.Text = "ASPX"
        Me.rbCodeVersionASPX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbCodeVersionASPX.UseVisualStyleBackColor = False
        '
        'chkSessionVariables
        '
        Me.chkSessionVariables.AutoSize = True
        Me.chkSessionVariables.Location = New System.Drawing.Point(67, 145)
        Me.chkSessionVariables.Name = "chkSessionVariables"
        Me.chkSessionVariables.Size = New System.Drawing.Size(137, 17)
        Me.chkSessionVariables.TabIndex = 28
        Me.chkSessionVariables.Text = "Session Variables Class"
        Me.chkSessionVariables.UseVisualStyleBackColor = True
        '
        'chkBasePage
        '
        Me.chkBasePage.AutoSize = True
        Me.chkBasePage.Location = New System.Drawing.Point(67, 119)
        Me.chkBasePage.Name = "chkBasePage"
        Me.chkBasePage.Size = New System.Drawing.Size(103, 17)
        Me.chkBasePage.TabIndex = 27
        Me.chkBasePage.Text = "BasePage Class"
        Me.chkBasePage.UseVisualStyleBackColor = True
        '
        'chkStoredProcedures
        '
        Me.chkStoredProcedures.AutoSize = True
        Me.chkStoredProcedures.Location = New System.Drawing.Point(53, 529)
        Me.chkStoredProcedures.Name = "chkStoredProcedures"
        Me.chkStoredProcedures.Size = New System.Drawing.Size(129, 17)
        Me.chkStoredProcedures.TabIndex = 26
        Me.chkStoredProcedures.Text = "chkStoredProcedures"
        Me.chkStoredProcedures.UseVisualStyleBackColor = True
        '
        'chkOther
        '
        Me.chkOther.AutoSize = True
        Me.chkOther.Location = New System.Drawing.Point(32, 506)
        Me.chkOther.Name = "chkOther"
        Me.chkOther.Size = New System.Drawing.Size(52, 17)
        Me.chkOther.TabIndex = 25
        Me.chkOther.Text = "Other"
        Me.chkOther.UseVisualStyleBackColor = True
        '
        'chkDefaultLayout
        '
        Me.chkDefaultLayout.AutoSize = True
        Me.chkDefaultLayout.Location = New System.Drawing.Point(66, 483)
        Me.chkDefaultLayout.Name = "chkDefaultLayout"
        Me.chkDefaultLayout.Size = New System.Drawing.Size(95, 17)
        Me.chkDefaultLayout.TabIndex = 24
        Me.chkDefaultLayout.Text = "Default Layout"
        Me.chkDefaultLayout.UseVisualStyleBackColor = True
        '
        'chkDefaultStyle
        '
        Me.chkDefaultStyle.AutoSize = True
        Me.chkDefaultStyle.Location = New System.Drawing.Point(66, 460)
        Me.chkDefaultStyle.Name = "chkDefaultStyle"
        Me.chkDefaultStyle.Size = New System.Drawing.Size(86, 17)
        Me.chkDefaultStyle.TabIndex = 23
        Me.chkDefaultStyle.Text = "Default Style"
        Me.chkDefaultStyle.UseVisualStyleBackColor = True
        '
        'chkThemes
        '
        Me.chkThemes.AutoSize = True
        Me.chkThemes.Location = New System.Drawing.Point(51, 437)
        Me.chkThemes.Name = "chkThemes"
        Me.chkThemes.Size = New System.Drawing.Size(64, 17)
        Me.chkThemes.TabIndex = 22
        Me.chkThemes.Text = "Themes"
        Me.chkThemes.UseVisualStyleBackColor = True
        '
        'chkClassWebPages
        '
        Me.chkClassWebPages.AutoSize = True
        Me.chkClassWebPages.Location = New System.Drawing.Point(50, 238)
        Me.chkClassWebPages.Name = "chkClassWebPages"
        Me.chkClassWebPages.Size = New System.Drawing.Size(110, 17)
        Me.chkClassWebPages.TabIndex = 21
        Me.chkClassWebPages.Text = "Class Web Pages"
        Me.chkClassWebPages.UseVisualStyleBackColor = True
        '
        'chkMasterCodeBehind
        '
        Me.chkMasterCodeBehind.AutoSize = True
        Me.chkMasterCodeBehind.Location = New System.Drawing.Point(65, 214)
        Me.chkMasterCodeBehind.Name = "chkMasterCodeBehind"
        Me.chkMasterCodeBehind.Size = New System.Drawing.Size(75, 17)
        Me.chkMasterCodeBehind.TabIndex = 20
        Me.chkMasterCodeBehind.Text = ".master.vb"
        Me.chkMasterCodeBehind.UseVisualStyleBackColor = True
        '
        'chkMaster_aspx
        '
        Me.chkMaster_aspx.AutoSize = True
        Me.chkMaster_aspx.Location = New System.Drawing.Point(65, 191)
        Me.chkMaster_aspx.Name = "chkMaster_aspx"
        Me.chkMaster_aspx.Size = New System.Drawing.Size(60, 17)
        Me.chkMaster_aspx.TabIndex = 19
        Me.chkMaster_aspx.Text = ".master"
        Me.chkMaster_aspx.UseVisualStyleBackColor = True
        '
        'chkMaster
        '
        Me.chkMaster.AutoSize = True
        Me.chkMaster.Location = New System.Drawing.Point(50, 168)
        Me.chkMaster.Name = "chkMaster"
        Me.chkMaster.Size = New System.Drawing.Size(91, 17)
        Me.chkMaster.TabIndex = 18
        Me.chkMaster.Text = "Master Pages"
        Me.chkMaster.UseVisualStyleBackColor = True
        '
        'chkDAL
        '
        Me.chkDAL.AutoSize = True
        Me.chkDAL.Location = New System.Drawing.Point(67, 96)
        Me.chkDAL.Name = "chkDAL"
        Me.chkDAL.Size = New System.Drawing.Size(47, 17)
        Me.chkDAL.TabIndex = 17
        Me.chkDAL.Text = "DAL"
        Me.chkDAL.UseVisualStyleBackColor = True
        '
        'chkApp_Code
        '
        Me.chkApp_Code.AutoSize = True
        Me.chkApp_Code.Location = New System.Drawing.Point(52, 39)
        Me.chkApp_Code.Name = "chkApp_Code"
        Me.chkApp_Code.Size = New System.Drawing.Size(76, 17)
        Me.chkApp_Code.TabIndex = 16
        Me.chkApp_Code.Text = "App_Code"
        Me.chkApp_Code.UseVisualStyleBackColor = True
        '
        'grpFormsUse
        '
        Me.grpFormsUse.Controls.Add(Me.grpFormsUseDivs)
        Me.grpFormsUse.Controls.Add(Me.grpFormsUseLists)
        Me.grpFormsUse.Location = New System.Drawing.Point(65, 303)
        Me.grpFormsUse.Name = "grpFormsUse"
        Me.grpFormsUse.Size = New System.Drawing.Size(212, 34)
        Me.grpFormsUse.TabIndex = 31
        Me.grpFormsUse.TabStop = False
        Me.grpFormsUse.Text = "Forms Use:"
        '
        'grpFormsUseDivs
        '
        Me.grpFormsUseDivs.AutoSize = True
        Me.grpFormsUseDivs.Location = New System.Drawing.Point(111, 17)
        Me.grpFormsUseDivs.Name = "grpFormsUseDivs"
        Me.grpFormsUseDivs.Size = New System.Drawing.Size(46, 17)
        Me.grpFormsUseDivs.TabIndex = 30
        Me.grpFormsUseDivs.Text = "Divs"
        Me.grpFormsUseDivs.UseVisualStyleBackColor = True
        '
        'grpFormsUseLists
        '
        Me.grpFormsUseLists.AutoSize = True
        Me.grpFormsUseLists.Checked = True
        Me.grpFormsUseLists.Location = New System.Drawing.Point(59, 17)
        Me.grpFormsUseLists.Name = "grpFormsUseLists"
        Me.grpFormsUseLists.Size = New System.Drawing.Size(46, 17)
        Me.grpFormsUseLists.TabIndex = 29
        Me.grpFormsUseLists.TabStop = True
        Me.grpFormsUseLists.Text = "Lists"
        Me.grpFormsUseLists.UseVisualStyleBackColor = True
        '
        'pnlInsert
        '
        Me.pnlInsert.Controls.Add(Me.gbMasterPages)
        Me.pnlInsert.Controls.Add(Me.gbClassWebPages)
        Me.pnlInsert.Controls.Add(Me.gbApp_Code)
        Me.pnlInsert.Location = New System.Drawing.Point(9, 43)
        Me.pnlInsert.Name = "pnlInsert"
        Me.pnlInsert.Size = New System.Drawing.Size(308, 585)
        Me.pnlInsert.TabIndex = 28
        Me.pnlInsert.Visible = False
        '
        'gbMasterPages
        '
        Me.gbMasterPages.Controls.Add(Me.gbScripts)
        Me.gbMasterPages.Controls.Add(Me.rbMasterVb)
        Me.gbMasterPages.Controls.Add(Me.rbMaster)
        Me.gbMasterPages.Location = New System.Drawing.Point(0, 58)
        Me.gbMasterPages.Name = "gbMasterPages"
        Me.gbMasterPages.Size = New System.Drawing.Size(200, 70)
        Me.gbMasterPages.TabIndex = 25
        Me.gbMasterPages.TabStop = False
        Me.gbMasterPages.Text = "Master Pages"
        '
        'gbScripts
        '
        Me.gbScripts.Controls.Add(Me.rbStoredProcedures)
        Me.gbScripts.Location = New System.Drawing.Point(0, 2)
        Me.gbScripts.Name = "gbScripts"
        Me.gbScripts.Size = New System.Drawing.Size(200, 100)
        Me.gbScripts.TabIndex = 27
        Me.gbScripts.TabStop = False
        Me.gbScripts.Text = "Scripts"
        '
        'rbStoredProcedures
        '
        Me.rbStoredProcedures.AutoSize = True
        Me.rbStoredProcedures.Location = New System.Drawing.Point(19, 22)
        Me.rbStoredProcedures.Name = "rbStoredProcedures"
        Me.rbStoredProcedures.Size = New System.Drawing.Size(113, 17)
        Me.rbStoredProcedures.TabIndex = 0
        Me.rbStoredProcedures.TabStop = True
        Me.rbStoredProcedures.Text = "Stored Procedures"
        Me.rbStoredProcedures.UseVisualStyleBackColor = True
        '
        'rbMasterVb
        '
        Me.rbMasterVb.AutoSize = True
        Me.rbMasterVb.Location = New System.Drawing.Point(19, 47)
        Me.rbMasterVb.Name = "rbMasterVb"
        Me.rbMasterVb.Size = New System.Drawing.Size(74, 17)
        Me.rbMasterVb.TabIndex = 1
        Me.rbMasterVb.TabStop = True
        Me.rbMasterVb.Text = ".master.vb"
        Me.rbMasterVb.UseVisualStyleBackColor = True
        '
        'rbMaster
        '
        Me.rbMaster.AutoSize = True
        Me.rbMaster.Location = New System.Drawing.Point(19, 23)
        Me.rbMaster.Name = "rbMaster"
        Me.rbMaster.Size = New System.Drawing.Size(59, 17)
        Me.rbMaster.TabIndex = 0
        Me.rbMaster.TabStop = True
        Me.rbMaster.Text = ".master"
        Me.rbMaster.UseVisualStyleBackColor = True
        '
        'gbClassWebPages
        '
        Me.gbClassWebPages.Controls.Add(Me.rbView)
        Me.gbClassWebPages.Controls.Add(Me.rbEdit)
        Me.gbClassWebPages.Location = New System.Drawing.Point(3, 57)
        Me.gbClassWebPages.Name = "gbClassWebPages"
        Me.gbClassWebPages.Size = New System.Drawing.Size(200, 70)
        Me.gbClassWebPages.TabIndex = 26
        Me.gbClassWebPages.TabStop = False
        Me.gbClassWebPages.Text = "Class WebPages"
        '
        'rbView
        '
        Me.rbView.AutoSize = True
        Me.rbView.Location = New System.Drawing.Point(19, 44)
        Me.rbView.Name = "rbView"
        Me.rbView.Size = New System.Drawing.Size(76, 17)
        Me.rbView.TabIndex = 1
        Me.rbView.TabStop = True
        Me.rbView.Text = "View Page"
        Me.rbView.UseVisualStyleBackColor = True
        '
        'rbEdit
        '
        Me.rbEdit.AutoSize = True
        Me.rbEdit.Location = New System.Drawing.Point(19, 20)
        Me.rbEdit.Name = "rbEdit"
        Me.rbEdit.Size = New System.Drawing.Size(71, 17)
        Me.rbEdit.TabIndex = 0
        Me.rbEdit.TabStop = True
        Me.rbEdit.Text = "Edit Page"
        Me.rbEdit.UseVisualStyleBackColor = True
        '
        'gbApp_Code
        '
        Me.gbApp_Code.Controls.Add(Me.rbSessionVariables)
        Me.gbApp_Code.Controls.Add(Me.rbBasePage)
        Me.gbApp_Code.Controls.Add(Me.rbDAL)
        Me.gbApp_Code.Controls.Add(Me.rbClass)
        Me.gbApp_Code.Location = New System.Drawing.Point(3, 58)
        Me.gbApp_Code.Name = "gbApp_Code"
        Me.gbApp_Code.Size = New System.Drawing.Size(200, 134)
        Me.gbApp_Code.TabIndex = 24
        Me.gbApp_Code.TabStop = False
        Me.gbApp_Code.Text = "App_Code"
        '
        'rbSessionVariables
        '
        Me.rbSessionVariables.AutoSize = True
        Me.rbSessionVariables.Location = New System.Drawing.Point(19, 96)
        Me.rbSessionVariables.Name = "rbSessionVariables"
        Me.rbSessionVariables.Size = New System.Drawing.Size(133, 17)
        Me.rbSessionVariables.TabIndex = 26
        Me.rbSessionVariables.TabStop = True
        Me.rbSessionVariables.Text = "SessionVariables Class"
        Me.rbSessionVariables.UseVisualStyleBackColor = True
        '
        'rbBasePage
        '
        Me.rbBasePage.AutoSize = True
        Me.rbBasePage.Location = New System.Drawing.Point(19, 72)
        Me.rbBasePage.Name = "rbBasePage"
        Me.rbBasePage.Size = New System.Drawing.Size(102, 17)
        Me.rbBasePage.TabIndex = 25
        Me.rbBasePage.TabStop = True
        Me.rbBasePage.Text = "BasePage Class"
        Me.rbBasePage.UseVisualStyleBackColor = True
        '
        'rbDAL
        '
        Me.rbDAL.AutoSize = True
        Me.rbDAL.Location = New System.Drawing.Point(19, 48)
        Me.rbDAL.Name = "rbDAL"
        Me.rbDAL.Size = New System.Drawing.Size(46, 17)
        Me.rbDAL.TabIndex = 24
        Me.rbDAL.TabStop = True
        Me.rbDAL.Text = "DAL"
        Me.rbDAL.UseVisualStyleBackColor = True
        '
        'rbClass
        '
        Me.rbClass.AutoSize = True
        Me.rbClass.Location = New System.Drawing.Point(19, 24)
        Me.rbClass.Name = "rbClass"
        Me.rbClass.Size = New System.Drawing.Size(50, 17)
        Me.rbClass.TabIndex = 23
        Me.rbClass.TabStop = True
        Me.rbClass.Text = "Class"
        Me.rbClass.UseVisualStyleBackColor = True
        '
        'rbActiveDocument
        '
        Me.rbActiveDocument.AutoSize = True
        Me.rbActiveDocument.Checked = True
        Me.rbActiveDocument.Location = New System.Drawing.Point(19, 19)
        Me.rbActiveDocument.Name = "rbActiveDocument"
        Me.rbActiveDocument.Size = New System.Drawing.Size(107, 17)
        Me.rbActiveDocument.TabIndex = 19
        Me.rbActiveDocument.TabStop = True
        Me.rbActiveDocument.Text = "Active Document"
        Me.rbActiveDocument.UseVisualStyleBackColor = True
        '
        'rbPickDocument
        '
        Me.rbPickDocument.AutoSize = True
        Me.rbPickDocument.Location = New System.Drawing.Point(132, 19)
        Me.rbPickDocument.Name = "rbPickDocument"
        Me.rbPickDocument.Size = New System.Drawing.Size(107, 17)
        Me.rbPickDocument.TabIndex = 20
        Me.rbPickDocument.Text = "Pick a Document"
        Me.rbPickDocument.UseVisualStyleBackColor = True
        '
        'gbInsertOptions
        '
        Me.gbInsertOptions.Controls.Add(Me.cbPages)
        Me.gbInsertOptions.Controls.Add(Me.rbPickDocument)
        Me.gbInsertOptions.Controls.Add(Me.rbActiveDocument)
        Me.gbInsertOptions.Location = New System.Drawing.Point(27, -2)
        Me.gbInsertOptions.Name = "gbInsertOptions"
        Me.gbInsertOptions.Size = New System.Drawing.Size(530, 43)
        Me.gbInsertOptions.TabIndex = 21
        Me.gbInsertOptions.TabStop = False
        Me.gbInsertOptions.Text = "Insert Options"
        '
        'cbPages
        '
        Me.cbPages.FormattingEnabled = True
        Me.cbPages.Location = New System.Drawing.Point(245, 16)
        Me.cbPages.Name = "cbPages"
        Me.cbPages.Size = New System.Drawing.Size(182, 21)
        Me.cbPages.TabIndex = 22
        '
        'dgvClassesToGenerate
        '
        Me.dgvClassesToGenerate.AllowUserToAddRows = False
        Me.dgvClassesToGenerate.AllowUserToDeleteRows = False
        Me.dgvClassesToGenerate.AutoGenerateColumns = False
        Me.dgvClassesToGenerate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClassesToGenerate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameStringDataGridViewTextBoxColumn, Me.SummaryDataGridViewTextBoxColumn, Me.btnRemoveClass})
        Me.dgvClassesToGenerate.DataSource = Me.ProjectClassBindingSource
        Me.dgvClassesToGenerate.Location = New System.Drawing.Point(338, 41)
        Me.dgvClassesToGenerate.Name = "dgvClassesToGenerate"
        Me.dgvClassesToGenerate.ReadOnly = True
        Me.dgvClassesToGenerate.Size = New System.Drawing.Size(426, 233)
        Me.dgvClassesToGenerate.TabIndex = 29
        '
        'NameStringDataGridViewTextBoxColumn
        '
        Me.NameStringDataGridViewTextBoxColumn.DataPropertyName = "NameString"
        Me.NameStringDataGridViewTextBoxColumn.HeaderText = "NameString"
        Me.NameStringDataGridViewTextBoxColumn.Name = "NameStringDataGridViewTextBoxColumn"
        Me.NameStringDataGridViewTextBoxColumn.ReadOnly = True
        Me.NameStringDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'SummaryDataGridViewTextBoxColumn
        '
        Me.SummaryDataGridViewTextBoxColumn.DataPropertyName = "Summary"
        Me.SummaryDataGridViewTextBoxColumn.HeaderText = "Summary"
        Me.SummaryDataGridViewTextBoxColumn.Name = "SummaryDataGridViewTextBoxColumn"
        Me.SummaryDataGridViewTextBoxColumn.ReadOnly = True
        '
        'btnRemoveClass
        '
        Me.btnRemoveClass.HeaderText = "Remove"
        Me.btnRemoveClass.Name = "btnRemoveClass"
        Me.btnRemoveClass.ReadOnly = True
        Me.btnRemoveClass.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.btnRemoveClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ProjectClassBindingSource
        '
        Me.ProjectClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.ProjectClass)
        '
        'dgbClassesNotToBeGenerated
        '
        Me.dgbClassesNotToBeGenerated.AllowUserToAddRows = False
        Me.dgbClassesNotToBeGenerated.AllowUserToDeleteRows = False
        Me.dgbClassesNotToBeGenerated.AutoGenerateColumns = False
        Me.dgbClassesNotToBeGenerated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgbClassesNotToBeGenerated.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameStringDataGridViewTextBoxColumn1, Me.SummaryDataGridViewTextBoxColumn1, Me.btnAddProjectClass})
        Me.dgbClassesNotToBeGenerated.DataSource = Me.ProjectClassBindingSource
        Me.dgbClassesNotToBeGenerated.Location = New System.Drawing.Point(338, 348)
        Me.dgbClassesNotToBeGenerated.Name = "dgbClassesNotToBeGenerated"
        Me.dgbClassesNotToBeGenerated.ReadOnly = True
        Me.dgbClassesNotToBeGenerated.RowHeadersWidth = 20
        Me.dgbClassesNotToBeGenerated.Size = New System.Drawing.Size(426, 206)
        Me.dgbClassesNotToBeGenerated.TabIndex = 30
        '
        'NameStringDataGridViewTextBoxColumn1
        '
        Me.NameStringDataGridViewTextBoxColumn1.DataPropertyName = "NameString"
        Me.NameStringDataGridViewTextBoxColumn1.HeaderText = "NameString"
        Me.NameStringDataGridViewTextBoxColumn1.Name = "NameStringDataGridViewTextBoxColumn1"
        Me.NameStringDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'SummaryDataGridViewTextBoxColumn1
        '
        Me.SummaryDataGridViewTextBoxColumn1.DataPropertyName = "Summary"
        Me.SummaryDataGridViewTextBoxColumn1.HeaderText = "Summary"
        Me.SummaryDataGridViewTextBoxColumn1.Name = "SummaryDataGridViewTextBoxColumn1"
        Me.SummaryDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'btnAddProjectClass
        '
        Me.btnAddProjectClass.HeaderText = "Add"
        Me.btnAddProjectClass.Name = "btnAddProjectClass"
        Me.btnAddProjectClass.ReadOnly = True
        Me.btnAddProjectClass.Text = "Add"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(344, 332)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Available"
        '
        'pbGeneratingFiles
        '
        Me.pbGeneratingFiles.Location = New System.Drawing.Point(400, 322)
        Me.pbGeneratingFiles.Name = "pbGeneratingFiles"
        Me.pbGeneratingFiles.Size = New System.Drawing.Size(364, 23)
        Me.pbGeneratingFiles.TabIndex = 32
        '
        'btnAddAll
        '
        Me.btnAddAll.Location = New System.Drawing.Point(376, 297)
        Me.btnAddAll.Name = "btnAddAll"
        Me.btnAddAll.Size = New System.Drawing.Size(34, 23)
        Me.btnAddAll.TabIndex = 33
        Me.btnAddAll.Text = " All"
        Me.btnAddAll.UseVisualStyleBackColor = True
        '
        'btnRemoveAll
        '
        Me.btnRemoveAll.Location = New System.Drawing.Point(612, 297)
        Me.btnRemoveAll.Name = "btnRemoveAll"
        Me.btnRemoveAll.Size = New System.Drawing.Size(29, 23)
        Me.btnRemoveAll.TabIndex = 34
        Me.btnRemoveAll.Text = "All"
        Me.btnRemoveAll.UseVisualStyleBackColor = True
        '
        'btnAddAssocEntities
        '
        Me.btnAddAssocEntities.Location = New System.Drawing.Point(416, 297)
        Me.btnAddAssocEntities.Name = "btnAddAssocEntities"
        Me.btnAddAssocEntities.Size = New System.Drawing.Size(86, 23)
        Me.btnAddAssocEntities.TabIndex = 35
        Me.btnAddAssocEntities.Text = "Assoc Entities"
        Me.btnAddAssocEntities.UseVisualStyleBackColor = True
        '
        'btnRemoveAssocEntities
        '
        Me.btnRemoveAssocEntities.Location = New System.Drawing.Point(647, 297)
        Me.btnRemoveAssocEntities.Name = "btnRemoveAssocEntities"
        Me.btnRemoveAssocEntities.Size = New System.Drawing.Size(93, 23)
        Me.btnRemoveAssocEntities.TabIndex = 36
        Me.btnRemoveAssocEntities.Text = "Assoc Entities"
        Me.btnRemoveAssocEntities.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(341, 302)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Add:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(556, 302)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Remove:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(635, 608)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Using: "
        '
        'lblCodeToUse
        '
        Me.lblCodeToUse.AutoSize = True
        Me.lblCodeToUse.Location = New System.Drawing.Point(678, 608)
        Me.lblCodeToUse.Name = "lblCodeToUse"
        Me.lblCodeToUse.Size = New System.Drawing.Size(53, 13)
        Me.lblCodeToUse.TabIndex = 42
        Me.lblCodeToUse.Text = "Unknown"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.GroupBox2.Controls.Add(Me.rbLanguageCSharp)
        Me.GroupBox2.Controls.Add(Me.rbLanguateVBasic)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(338, 563)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox2.Size = New System.Drawing.Size(277, 65)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Language"
        '
        'rbLanguageCSharp
        '
        Me.rbLanguageCSharp.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbLanguageCSharp.AutoSize = True
        Me.rbLanguageCSharp.BackColor = System.Drawing.Color.LightGray
        Me.rbLanguageCSharp.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbLanguageCSharp.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.rbLanguageCSharp.FlatAppearance.BorderSize = 2
        Me.rbLanguageCSharp.FlatAppearance.CheckedBackColor = System.Drawing.Color.RoyalBlue
        Me.rbLanguageCSharp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue
        Me.rbLanguageCSharp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue
        Me.rbLanguageCSharp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbLanguageCSharp.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbLanguageCSharp.Location = New System.Drawing.Point(137, 19)
        Me.rbLanguageCSharp.Name = "rbLanguageCSharp"
        Me.rbLanguageCSharp.Size = New System.Drawing.Size(127, 38)
        Me.rbLanguageCSharp.TabIndex = 1
        Me.rbLanguageCSharp.Text = "CSharp (C#)"
        Me.rbLanguageCSharp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbLanguageCSharp.UseVisualStyleBackColor = False
        '
        'rbLanguateVBasic
        '
        Me.rbLanguateVBasic.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbLanguateVBasic.AutoSize = True
        Me.rbLanguateVBasic.BackColor = System.Drawing.Color.LightGray
        Me.rbLanguateVBasic.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbLanguateVBasic.Checked = True
        Me.rbLanguateVBasic.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.rbLanguateVBasic.FlatAppearance.BorderSize = 2
        Me.rbLanguateVBasic.FlatAppearance.CheckedBackColor = System.Drawing.Color.RoyalBlue
        Me.rbLanguateVBasic.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue
        Me.rbLanguateVBasic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue
        Me.rbLanguateVBasic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbLanguateVBasic.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbLanguateVBasic.Location = New System.Drawing.Point(6, 19)
        Me.rbLanguateVBasic.Name = "rbLanguateVBasic"
        Me.rbLanguateVBasic.Size = New System.Drawing.Size(125, 38)
        Me.rbLanguateVBasic.TabIndex = 0
        Me.rbLanguateVBasic.TabStop = True
        Me.rbLanguateVBasic.Text = "Visual Basic"
        Me.rbLanguateVBasic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbLanguateVBasic.UseVisualStyleBackColor = False
        '
        'GenerateCodeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(816, 627)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblCodeToUse)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pnlInsert)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnRemoveAssocEntities)
        Me.Controls.Add(Me.btnAddAssocEntities)
        Me.Controls.Add(Me.btnRemoveAll)
        Me.Controls.Add(Me.btnAddAll)
        Me.Controls.Add(Me.pbGeneratingFiles)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgbClassesNotToBeGenerated)
        Me.Controls.Add(Me.dgvClassesToGenerate)
        Me.Controls.Add(Me.gbInsertOptions)
        Me.Controls.Add(Me.pnlCreateFiles)
        Me.Name = "GenerateCodeForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "GenerateCodeForm"
        Me.pnlCreateFiles.ResumeLayout(False)
        Me.pnlCreateFiles.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpFormsUse.ResumeLayout(False)
        Me.grpFormsUse.PerformLayout()
        Me.pnlInsert.ResumeLayout(False)
        Me.gbMasterPages.ResumeLayout(False)
        Me.gbMasterPages.PerformLayout()
        Me.gbScripts.ResumeLayout(False)
        Me.gbScripts.PerformLayout()
        Me.gbClassWebPages.ResumeLayout(False)
        Me.gbClassWebPages.PerformLayout()
        Me.gbApp_Code.ResumeLayout(False)
        Me.gbApp_Code.PerformLayout()
        Me.gbInsertOptions.ResumeLayout(False)
        Me.gbInsertOptions.PerformLayout()
        CType(Me.dgvClassesToGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgbClassesNotToBeGenerated, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkClasses As System.Windows.Forms.CheckBox
    Friend WithEvents chkEditPage As System.Windows.Forms.CheckBox
    Friend WithEvents chkEditPageCodeBehind As System.Windows.Forms.CheckBox
    Friend WithEvents chkEditPageHTML As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewAllCodeBehind As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewAllHTML As System.Windows.Forms.CheckBox
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkWebApplication As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCreateFiles As System.Windows.Forms.Panel
    Friend WithEvents ProjectClassBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents chkApp_Code As System.Windows.Forms.CheckBox
    Friend WithEvents chkMasterCodeBehind As System.Windows.Forms.CheckBox
    Friend WithEvents chkMaster_aspx As System.Windows.Forms.CheckBox
    Friend WithEvents chkMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chkDAL As System.Windows.Forms.CheckBox
    Friend WithEvents chkClassWebPages As System.Windows.Forms.CheckBox
    Friend WithEvents chkDefaultLayout As System.Windows.Forms.CheckBox
    Friend WithEvents chkDefaultStyle As System.Windows.Forms.CheckBox
    Friend WithEvents chkThemes As System.Windows.Forms.CheckBox
    Friend WithEvents chkOther As System.Windows.Forms.CheckBox
    Friend WithEvents chkStoredProcedures As System.Windows.Forms.CheckBox
    Friend WithEvents rbActiveDocument As System.Windows.Forms.RadioButton
    Friend WithEvents rbPickDocument As System.Windows.Forms.RadioButton
    Friend WithEvents gbInsertOptions As System.Windows.Forms.GroupBox
    Friend WithEvents cbPages As System.Windows.Forms.ComboBox
    Friend WithEvents chkBasePage As System.Windows.Forms.CheckBox
    Friend WithEvents chkSessionVariables As System.Windows.Forms.CheckBox
    Friend WithEvents rbClass As System.Windows.Forms.RadioButton
    Friend WithEvents gbApp_Code As System.Windows.Forms.GroupBox
    Friend WithEvents rbSessionVariables As System.Windows.Forms.RadioButton
    Friend WithEvents rbBasePage As System.Windows.Forms.RadioButton
    Friend WithEvents rbDAL As System.Windows.Forms.RadioButton
    Friend WithEvents gbMasterPages As System.Windows.Forms.GroupBox
    Friend WithEvents rbMasterVb As System.Windows.Forms.RadioButton
    Friend WithEvents rbMaster As System.Windows.Forms.RadioButton
    Friend WithEvents gbClassWebPages As System.Windows.Forms.GroupBox
    Friend WithEvents rbView As System.Windows.Forms.RadioButton
    Friend WithEvents rbEdit As System.Windows.Forms.RadioButton
    Friend WithEvents gbScripts As System.Windows.Forms.GroupBox
    Friend WithEvents rbStoredProcedures As System.Windows.Forms.RadioButton
    Friend WithEvents pnlInsert As System.Windows.Forms.Panel
    Friend WithEvents dgvClassesToGenerate As System.Windows.Forms.DataGridView
    Friend WithEvents dgbClassesNotToBeGenerated As System.Windows.Forms.DataGridView
    Friend WithEvents NameStringDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SummaryDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAddProjectClass As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents NameStringDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SummaryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRemoveClass As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pbGeneratingFiles As System.Windows.Forms.ProgressBar
    Friend WithEvents btnAddAll As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAll As System.Windows.Forms.Button
    Friend WithEvents btnAddAssocEntities As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAssocEntities As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grpFormsUseLists As System.Windows.Forms.RadioButton
    Friend WithEvents grpFormsUse As System.Windows.Forms.GroupBox
    Friend WithEvents grpFormsUseDivs As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCodeToUse As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbCodeVersionMVC As System.Windows.Forms.RadioButton
    Friend WithEvents rbCodeVersionASPX As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbLanguageCSharp As System.Windows.Forms.RadioButton
    Friend WithEvents rbLanguateVBasic As System.Windows.Forms.RadioButton
End Class

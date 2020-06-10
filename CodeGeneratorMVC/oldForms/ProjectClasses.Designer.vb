<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProjectClasses
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnEditSelected = New System.Windows.Forms.Button()
        Me.btnRemoveSelected = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.dgvProjectClasses = New System.Windows.Forms.DataGridView()
        Me.cbxclmIsSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.txtclmName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnclmShowVariables = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.cbclmDAL = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DALClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbclmNameSpace = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ProjectVariableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbclmMasterPages = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.MasterPageClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbClmText = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.cbClmValue = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.cbclmBaseClasse = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.txtclmSummary = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProjectClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ClassVariableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblGood = New System.Windows.Forms.Label()
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.lblBad = New System.Windows.Forms.Label()
        CType(Me.dgvProjectClasses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DALClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProjectVariableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterPageClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.ClassVariableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(28, 16)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectAll.TabIndex = 1
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnEditSelected
        '
        Me.btnEditSelected.Location = New System.Drawing.Point(754, 16)
        Me.btnEditSelected.Name = "btnEditSelected"
        Me.btnEditSelected.Size = New System.Drawing.Size(98, 23)
        Me.btnEditSelected.TabIndex = 3
        Me.btnEditSelected.Text = "Edit Selected"
        Me.btnEditSelected.UseVisualStyleBackColor = True
        '
        'btnRemoveSelected
        '
        Me.btnRemoveSelected.Location = New System.Drawing.Point(880, 16)
        Me.btnRemoveSelected.Name = "btnRemoveSelected"
        Me.btnRemoveSelected.Size = New System.Drawing.Size(126, 23)
        Me.btnRemoveSelected.TabIndex = 5
        Me.btnRemoveSelected.Text = "Remove Selected"
        Me.btnRemoveSelected.UseVisualStyleBackColor = True
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.Location = New System.Drawing.Point(131, 16)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnDeselectAll.TabIndex = 7
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'dgvProjectClasses
        '
        Me.dgvProjectClasses.AutoGenerateColumns = False
        Me.dgvProjectClasses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvProjectClasses.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvProjectClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProjectClasses.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cbxclmIsSelected, Me.txtclmName, Me.btnclmShowVariables, Me.cbclmDAL, Me.cbclmNameSpace, Me.cbclmMasterPages, Me.cbClmText, Me.cbClmValue, Me.cbclmBaseClasse, Me.txtclmSummary})
        Me.dgvProjectClasses.DataSource = Me.ProjectClassBindingSource
        Me.dgvProjectClasses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProjectClasses.Location = New System.Drawing.Point(0, 0)
        Me.dgvProjectClasses.Name = "dgvProjectClasses"
        Me.dgvProjectClasses.Size = New System.Drawing.Size(1021, 639)
        Me.dgvProjectClasses.TabIndex = 8
        '
        'cbxclmIsSelected
        '
        Me.cbxclmIsSelected.DataPropertyName = "IsSelected"
        Me.cbxclmIsSelected.HeaderText = ""
        Me.cbxclmIsSelected.Name = "cbxclmIsSelected"
        Me.cbxclmIsSelected.ToolTipText = "Select (unselect) this row"
        Me.cbxclmIsSelected.Width = 21
        '
        'txtclmName
        '
        Me.txtclmName.DataPropertyName = "NameString"
        Me.txtclmName.HeaderText = "Name"
        Me.txtclmName.Name = "txtclmName"
        Me.txtclmName.Width = 60
        '
        'btnclmShowVariables
        '
        Me.btnclmShowVariables.HeaderText = "Variables"
        Me.btnclmShowVariables.Name = "btnclmShowVariables"
        Me.btnclmShowVariables.Text = "View"
        Me.btnclmShowVariables.UseColumnTextForButtonValue = True
        Me.btnclmShowVariables.Width = 56
        '
        'cbclmDAL
        '
        Me.cbclmDAL.DataPropertyName = "DALClassVariable"
        Me.cbclmDAL.DataSource = Me.DALClassBindingSource
        Me.cbclmDAL.DisplayMember = "Name"
        Me.cbclmDAL.HeaderText = "Data Access Layer"
        Me.cbclmDAL.Name = "cbclmDAL"
        Me.cbclmDAL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cbclmDAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cbclmDAL.ValueMember = "Self"
        Me.cbclmDAL.Width = 112
        '
        'DALClassBindingSource
        '
        Me.DALClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.DALClass)
        '
        'cbclmNameSpace
        '
        Me.cbclmNameSpace.DataPropertyName = "NameSpaceVariable"
        Me.cbclmNameSpace.DataSource = Me.ProjectVariableBindingSource
        Me.cbclmNameSpace.DisplayMember = "Name"
        Me.cbclmNameSpace.HeaderText = "NameSpaceVariable"
        Me.cbclmNameSpace.Name = "cbclmNameSpace"
        Me.cbclmNameSpace.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cbclmNameSpace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cbclmNameSpace.ValueMember = "Self"
        Me.cbclmNameSpace.Width = 129
        '
        'ProjectVariableBindingSource
        '
        Me.ProjectVariableBindingSource.DataSource = GetType(CodeGeneratorAddIn.ProjectVariable)
        '
        'cbclmMasterPages
        '
        Me.cbclmMasterPages.DataPropertyName = "MasterPage"
        Me.cbclmMasterPages.DataSource = Me.MasterPageClassBindingSource
        Me.cbclmMasterPages.DisplayMember = "Name"
        Me.cbclmMasterPages.HeaderText = "MasterPage"
        Me.cbclmMasterPages.Name = "cbclmMasterPages"
        Me.cbclmMasterPages.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cbclmMasterPages.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cbclmMasterPages.ValueMember = "Self"
        Me.cbclmMasterPages.Width = 89
        '
        'MasterPageClassBindingSource
        '
        Me.MasterPageClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.MasterPageClass)
        '
        'cbClmText
        '
        Me.cbClmText.DataPropertyName = "TextVariable"
        Me.cbClmText.HeaderText = "Text (used for lists)"
        Me.cbClmText.Name = "cbClmText"
        Me.cbClmText.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cbClmText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cbClmText.Width = 92
        '
        'cbClmValue
        '
        Me.cbClmValue.DataPropertyName = "ValueVariable"
        Me.cbClmValue.HeaderText = "Value (used for lists)"
        Me.cbClmValue.Name = "cbClmValue"
        Me.cbClmValue.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cbClmValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cbClmValue.Width = 97
        '
        'cbclmBaseClasse
        '
        Me.cbclmBaseClasse.DataPropertyName = "BaseClass"
        Me.cbclmBaseClasse.DataSource = Me.ProjectVariableBindingSource
        Me.cbclmBaseClasse.DisplayMember = "Name"
        Me.cbclmBaseClasse.HeaderText = "BaseClass"
        Me.cbclmBaseClasse.Name = "cbclmBaseClasse"
        Me.cbclmBaseClasse.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cbclmBaseClasse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cbclmBaseClasse.ValueMember = "Self"
        Me.cbclmBaseClasse.Width = 81
        '
        'txtclmSummary
        '
        Me.txtclmSummary.DataPropertyName = "Summary"
        Me.txtclmSummary.HeaderText = "Summary"
        Me.txtclmSummary.Name = "txtclmSummary"
        Me.txtclmSummary.Width = 75
        '
        'ProjectClassBindingSource
        '
        Me.ProjectClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.ProjectClass)
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.lblBad)
        Me.Panel1.Controls.Add(Me.lblWarning)
        Me.Panel1.Controls.Add(Me.lblGood)
        Me.Panel1.Controls.Add(Me.btnSelectAll)
        Me.Panel1.Controls.Add(Me.btnDeselectAll)
        Me.Panel1.Controls.Add(Me.btnRemoveSelected)
        Me.Panel1.Controls.Add(Me.btnEditSelected)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.MaximumSize = New System.Drawing.Size(1021, 55)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1021, 42)
        Me.Panel1.TabIndex = 9
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvProjectClasses)
        Me.SplitContainer1.Size = New System.Drawing.Size(1021, 688)
        Me.SplitContainer1.SplitterDistance = 45
        Me.SplitContainer1.TabIndex = 10
        '
        'ClassVariableBindingSource
        '
        Me.ClassVariableBindingSource.DataSource = GetType(CodeGeneratorAddIn.ClassVariable)
        '
        'lblGood
        '
        Me.lblGood.AutoSize = True
        Me.lblGood.Location = New System.Drawing.Point(274, 2)
        Me.lblGood.Name = "lblGood"
        Me.lblGood.Size = New System.Drawing.Size(60, 13)
        Me.lblGood.TabIndex = 8
        Me.lblGood.Text = "Record OK"
        '
        'lblWarning
        '
        Me.lblWarning.AutoSize = True
        Me.lblWarning.Location = New System.Drawing.Point(274, 15)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(186, 13)
        Me.lblWarning.TabIndex = 9
        Me.lblWarning.Text = "May have issues or AssociativeObject"
        '
        'lblBad
        '
        Me.lblBad.AutoSize = True
        Me.lblBad.Location = New System.Drawing.Point(274, 29)
        Me.lblBad.Name = "lblBad"
        Me.lblBad.Size = New System.Drawing.Size(141, 13)
        Me.lblBad.TabIndex = 10
        Me.lblBad.Text = "Record has Missing Settings"
        '
        'ProjectClasses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ProjectClasses"
        Me.Size = New System.Drawing.Size(1021, 688)
        CType(Me.dgvProjectClasses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DALClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProjectVariableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterPageClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.ClassVariableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnEditSelected As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelected As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents dgvProjectClasses As System.Windows.Forms.DataGridView
    Friend WithEvents ProjectClassBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DALClassBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ProjectVariableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MasterPageClassBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ClassVariableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cbxclmIsSelected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents txtclmName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnclmShowVariables As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents cbclmDAL As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents cbclmNameSpace As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents cbclmMasterPages As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents cbClmText As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents cbClmValue As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents cbclmBaseClasse As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents txtclmSummary As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblBad As System.Windows.Forms.Label
    Friend WithEvents lblWarning As System.Windows.Forms.Label
    Friend WithEvents lblGood As System.Windows.Forms.Label

End Class

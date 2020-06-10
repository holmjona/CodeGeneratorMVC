<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditClassVariables
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
        Me.dgvClassVariables = New System.Windows.Forms.DataGridView()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatabaseColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatabaseType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LengthOfDatabaseProperty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParameterTypeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DisplayOnViewPageDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DisplayOnEditPageDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IsPropertyInheritedDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.EditColumn = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.RemoveColumn = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ClassVariableBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblSelectedClassTitle = New System.Windows.Forms.Label()
        Me.btnAddClassVariable = New System.Windows.Forms.Button()
        CType(Me.dgvClassVariables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassVariableBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvClassVariables
        '
        Me.dgvClassVariables.AllowUserToOrderColumns = True
        Me.dgvClassVariables.AutoGenerateColumns = False
        Me.dgvClassVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClassVariables.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameDataGridViewTextBoxColumn, Me.DatabaseColumnName, Me.DatabaseType, Me.LengthOfDatabaseProperty, Me.ParameterTypeDataGridViewTextBoxColumn, Me.DisplayOnViewPageDataGridViewCheckBoxColumn, Me.DisplayOnEditPageDataGridViewCheckBoxColumn, Me.IsPropertyInheritedDataGridViewCheckBoxColumn, Me.EditColumn, Me.RemoveColumn})
        Me.dgvClassVariables.DataSource = Me.ClassVariableBindingSource1
        Me.dgvClassVariables.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.dgvClassVariables.Location = New System.Drawing.Point(65, 58)
        Me.dgvClassVariables.MultiSelect = False
        Me.dgvClassVariables.Name = "dgvClassVariables"
        Me.dgvClassVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvClassVariables.Size = New System.Drawing.Size(766, 331)
        Me.dgvClassVariables.TabIndex = 12
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        '
        'DatabaseColumnName
        '
        Me.DatabaseColumnName.DataPropertyName = "DatabaseColumnName"
        Me.DatabaseColumnName.HeaderText = "DatabaseColumnName"
        Me.DatabaseColumnName.Name = "DatabaseColumnName"
        '
        'DatabaseType
        '
        Me.DatabaseType.DataPropertyName = "DatabaseType"
        Me.DatabaseType.HeaderText = "DatabaseType"
        Me.DatabaseType.Name = "DatabaseType"
        '
        'LengthOfDatabaseProperty
        '
        Me.LengthOfDatabaseProperty.DataPropertyName = "LengthOfDatabaseProperty"
        Me.LengthOfDatabaseProperty.HeaderText = "LengthOfDatabaseProperty"
        Me.LengthOfDatabaseProperty.Name = "LengthOfDatabaseProperty"
        '
        'ParameterTypeDataGridViewTextBoxColumn
        '
        Me.ParameterTypeDataGridViewTextBoxColumn.DataPropertyName = "ParameterType"
        Me.ParameterTypeDataGridViewTextBoxColumn.DataSource = Me.DataTypeBindingSource
        Me.ParameterTypeDataGridViewTextBoxColumn.HeaderText = "ParameterType"
        Me.ParameterTypeDataGridViewTextBoxColumn.Name = "ParameterTypeDataGridViewTextBoxColumn"
        Me.ParameterTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ParameterTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ParameterTypeDataGridViewTextBoxColumn.ValueMember = "Self"
        '
        'DataTypeBindingSource
        '
        Me.DataTypeBindingSource.DataSource = GetType(CodeGeneratorAddIn.DataType)
        '
        'DisplayOnViewPageDataGridViewCheckBoxColumn
        '
        Me.DisplayOnViewPageDataGridViewCheckBoxColumn.DataPropertyName = "DisplayOnViewPage"
        Me.DisplayOnViewPageDataGridViewCheckBoxColumn.HeaderText = "ViewPage"
        Me.DisplayOnViewPageDataGridViewCheckBoxColumn.Name = "DisplayOnViewPageDataGridViewCheckBoxColumn"
        '
        'DisplayOnEditPageDataGridViewCheckBoxColumn
        '
        Me.DisplayOnEditPageDataGridViewCheckBoxColumn.DataPropertyName = "DisplayOnEditPage"
        Me.DisplayOnEditPageDataGridViewCheckBoxColumn.HeaderText = "EditPage"
        Me.DisplayOnEditPageDataGridViewCheckBoxColumn.Name = "DisplayOnEditPageDataGridViewCheckBoxColumn"
        '
        'IsPropertyInheritedDataGridViewCheckBoxColumn
        '
        Me.IsPropertyInheritedDataGridViewCheckBoxColumn.DataPropertyName = "IsPropertyInherited"
        Me.IsPropertyInheritedDataGridViewCheckBoxColumn.HeaderText = "IsInherited"
        Me.IsPropertyInheritedDataGridViewCheckBoxColumn.Name = "IsPropertyInheritedDataGridViewCheckBoxColumn"
        '
        'EditColumn
        '
        Me.EditColumn.DataPropertyName = "ID"
        Me.EditColumn.HeaderText = "Edit"
        Me.EditColumn.Name = "EditColumn"
        Me.EditColumn.ReadOnly = True
        Me.EditColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EditColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.EditColumn.Text = "Edit"
        Me.EditColumn.ToolTipText = "{ID}"
        Me.EditColumn.UseColumnTextForButtonValue = True
        Me.EditColumn.Width = 50
        '
        'RemoveColumn
        '
        Me.RemoveColumn.DataPropertyName = "ID"
        Me.RemoveColumn.HeaderText = "Remove"
        Me.RemoveColumn.Name = "RemoveColumn"
        Me.RemoveColumn.ReadOnly = True
        Me.RemoveColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RemoveColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.RemoveColumn.Text = "Remove"
        Me.RemoveColumn.UseColumnTextForButtonValue = True
        Me.RemoveColumn.Width = 50
        '
        'ClassVariableBindingSource1
        '
        Me.ClassVariableBindingSource1.DataSource = GetType(CodeGeneratorAddIn.ClassVariable)
        '
        'lblSelectedClassTitle
        '
        Me.lblSelectedClassTitle.AutoSize = True
        Me.lblSelectedClassTitle.Location = New System.Drawing.Point(306, 34)
        Me.lblSelectedClassTitle.Name = "lblSelectedClassTitle"
        Me.lblSelectedClassTitle.Size = New System.Drawing.Size(94, 13)
        Me.lblSelectedClassTitle.TabIndex = 34
        Me.lblSelectedClassTitle.Text = "No Class Selected"
        '
        'btnAddClassVariable
        '
        Me.btnAddClassVariable.Enabled = False
        Me.btnAddClassVariable.Location = New System.Drawing.Point(65, 29)
        Me.btnAddClassVariable.Name = "btnAddClassVariable"
        Me.btnAddClassVariable.Size = New System.Drawing.Size(75, 23)
        Me.btnAddClassVariable.TabIndex = 33
        Me.btnAddClassVariable.Text = "Add Variable"
        Me.btnAddClassVariable.UseVisualStyleBackColor = True
        '
        'frmEditClassVariables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 447)
        Me.Controls.Add(Me.lblSelectedClassTitle)
        Me.Controls.Add(Me.dgvClassVariables)
        Me.Controls.Add(Me.btnAddClassVariable)
        Me.Name = "frmEditClassVariables"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EditClassVariables"
        CType(Me.dgvClassVariables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassVariableBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvClassVariables As System.Windows.Forms.DataGridView
    Friend WithEvents lblSelectedClassTitle As System.Windows.Forms.Label
    Friend WithEvents btnAddClassVariable As System.Windows.Forms.Button
    Friend WithEvents ClassVariableBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DatabaseColumnName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DatabaseType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LengthOfDatabaseProperty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParameterTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DisplayOnViewPageDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DisplayOnEditPageDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents IsPropertyInheritedDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents EditColumn As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents RemoveColumn As System.Windows.Forms.DataGridViewButtonColumn
End Class

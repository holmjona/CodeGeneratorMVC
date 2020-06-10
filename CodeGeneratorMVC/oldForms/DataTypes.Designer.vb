<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataTypes
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
        Me.components = New System.ComponentModel.Container
        Me.dgvDataTypes = New System.Windows.Forms.DataGridView
        Me.DataTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataTypeBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvDataTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTypeBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDataTypes
        '
        Me.dgvDataTypes.AutoGenerateColumns = False
        Me.dgvDataTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDataTypes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameDataGridViewTextBoxColumn})
        Me.dgvDataTypes.DataSource = Me.DataTypeBindingSource1
        Me.dgvDataTypes.Location = New System.Drawing.Point(44, 103)
        Me.dgvDataTypes.Name = "dgvDataTypes"
        Me.dgvDataTypes.Size = New System.Drawing.Size(505, 557)
        Me.dgvDataTypes.TabIndex = 0
        '
        'DataTypeBindingSource
        '
        Me.DataTypeBindingSource.DataSource = GetType(CodeGeneratorAddIn.DataType)
        '
        'DataTypeBindingSource1
        '
        Me.DataTypeBindingSource1.DataSource = GetType(CodeGeneratorAddIn.DataType)
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        '
        'DataTypes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgvDataTypes)
        Me.Name = "DataTypes"
        Me.Size = New System.Drawing.Size(1021, 688)
        CType(Me.dgvDataTypes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTypeBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvDataTypes As System.Windows.Forms.DataGridView
    Friend WithEvents DataTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataTypeBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class

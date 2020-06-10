<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditProjectClass
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
        Me.components = New System.ComponentModel.Container
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.cbMasterPages = New System.Windows.Forms.ComboBox
        Me.cbBaseClasses = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbNameSpaces = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbDALs = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnAddMasterPage = New System.Windows.Forms.Button
        Me.btnAddBaseClass = New System.Windows.Forms.Button
        Me.btnAddNameSpace = New System.Windows.Forms.Button
        Me.btnAddDAL = New System.Windows.Forms.Button
        Me.ProjectClassBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(516, 288)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'cbMasterPages
        '
        Me.cbMasterPages.DropDownWidth = 250
        Me.cbMasterPages.FormattingEnabled = True
        Me.cbMasterPages.Location = New System.Drawing.Point(247, 149)
        Me.cbMasterPages.Name = "cbMasterPages"
        Me.cbMasterPages.Size = New System.Drawing.Size(252, 21)
        Me.cbMasterPages.TabIndex = 5
        '
        'cbBaseClasses
        '
        Me.cbBaseClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBaseClasses.DropDownWidth = 250
        Me.cbBaseClasses.FormattingEnabled = True
        Me.cbBaseClasses.Location = New System.Drawing.Point(247, 177)
        Me.cbBaseClasses.Name = "cbBaseClasses"
        Me.cbBaseClasses.Size = New System.Drawing.Size(252, 21)
        Me.cbBaseClasses.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(177, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "MasterPage"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(182, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Base Class"
        '
        'cbNameSpaces
        '
        Me.cbNameSpaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbNameSpaces.DropDownWidth = 250
        Me.cbNameSpaces.FormattingEnabled = True
        Me.cbNameSpaces.Location = New System.Drawing.Point(247, 204)
        Me.cbNameSpaces.Name = "cbNameSpaces"
        Me.cbNameSpaces.Size = New System.Drawing.Size(252, 21)
        Me.cbNameSpaces.TabIndex = 17
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(177, 207)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Namespace"
        '
        'cbDALs
        '
        Me.cbDALs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDALs.DropDownWidth = 250
        Me.cbDALs.FormattingEnabled = True
        Me.cbDALs.Location = New System.Drawing.Point(247, 231)
        Me.cbDALs.Name = "cbDALs"
        Me.cbDALs.Size = New System.Drawing.Size(252, 21)
        Me.cbDALs.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(182, 234)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "DAL Name"
        '
        'btnAddMasterPage
        '
        Me.btnAddMasterPage.Location = New System.Drawing.Point(516, 149)
        Me.btnAddMasterPage.Name = "btnAddMasterPage"
        Me.btnAddMasterPage.Size = New System.Drawing.Size(130, 23)
        Me.btnAddMasterPage.TabIndex = 25
        Me.btnAddMasterPage.Text = "Add New MasterPage"
        Me.btnAddMasterPage.UseVisualStyleBackColor = True
        '
        'btnAddBaseClass
        '
        Me.btnAddBaseClass.Location = New System.Drawing.Point(516, 177)
        Me.btnAddBaseClass.Name = "btnAddBaseClass"
        Me.btnAddBaseClass.Size = New System.Drawing.Size(130, 23)
        Me.btnAddBaseClass.TabIndex = 26
        Me.btnAddBaseClass.Text = "Add New BaseClass"
        Me.btnAddBaseClass.UseVisualStyleBackColor = True
        '
        'btnAddNameSpace
        '
        Me.btnAddNameSpace.Location = New System.Drawing.Point(516, 204)
        Me.btnAddNameSpace.Name = "btnAddNameSpace"
        Me.btnAddNameSpace.Size = New System.Drawing.Size(130, 23)
        Me.btnAddNameSpace.TabIndex = 27
        Me.btnAddNameSpace.Text = "Add New NameSpace"
        Me.btnAddNameSpace.UseVisualStyleBackColor = True
        '
        'btnAddDAL
        '
        Me.btnAddDAL.Location = New System.Drawing.Point(516, 231)
        Me.btnAddDAL.Name = "btnAddDAL"
        Me.btnAddDAL.Size = New System.Drawing.Size(130, 23)
        Me.btnAddDAL.TabIndex = 28
        Me.btnAddDAL.Text = "Add New DAL"
        Me.btnAddDAL.UseVisualStyleBackColor = True
        '
        'ProjectClassBindingSource
        '
        Me.ProjectClassBindingSource.DataSource = GetType(CodeGeneratorAddIn.ProjectClass)
        '
        'EditProjectClass
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(674, 329)
        Me.Controls.Add(Me.btnAddDAL)
        Me.Controls.Add(Me.btnAddNameSpace)
        Me.Controls.Add(Me.btnAddBaseClass)
        Me.Controls.Add(Me.btnAddMasterPage)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cbDALs)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cbNameSpaces)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cbBaseClasses)
        Me.Controls.Add(Me.cbMasterPages)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditProjectClass"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EditProjectClass"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.ProjectClassBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents cbMasterPages As System.Windows.Forms.ComboBox
    Friend WithEvents cbBaseClasses As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbNameSpaces As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbDALs As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnAddMasterPage As System.Windows.Forms.Button
    Friend WithEvents btnAddBaseClass As System.Windows.Forms.Button
    Friend WithEvents btnAddNameSpace As System.Windows.Forms.Button
    Friend WithEvents btnAddDAL As System.Windows.Forms.Button
    Friend WithEvents ProjectClassBindingSource As System.Windows.Forms.BindingSource

End Class

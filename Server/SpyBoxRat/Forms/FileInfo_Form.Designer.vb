<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileInfo_Form
    Inherits Custom_Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FileInfo_Form))
        Me.UserControl13 = New AnonMon.UserControl1()
        Me.UserControl12 = New AnonMon.UserControl1()
        Me.UserControl11 = New AnonMon.UserControl1()
        Me.RichTextBox3 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'UserControl13
        '
        Me.UserControl13.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.UserControl13.ImageDescriptor = Global.AnonMon.My.Resources.Resources.icons8_processor_321
        Me.UserControl13.ImageDescriptorBackColor = System.Drawing.Color.White
        Me.UserControl13.Location = New System.Drawing.Point(12, 164)
        Me.UserControl13.Name = "UserControl13"
        Me.UserControl13.Size = New System.Drawing.Size(139, 33)
        Me.UserControl13.TabIndex = 34
        Me.UserControl13.TextEx = "FileSize : "
        Me.UserControl13.TextExColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(225, Byte), Integer))
        '
        'UserControl12
        '
        Me.UserControl12.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.UserControl12.ImageDescriptor = Global.AnonMon.My.Resources.Resources.icons8_File_qsqsqxplorer_322
        Me.UserControl12.ImageDescriptorBackColor = System.Drawing.Color.White
        Me.UserControl12.Location = New System.Drawing.Point(12, 110)
        Me.UserControl12.Name = "UserControl12"
        Me.UserControl12.Size = New System.Drawing.Size(139, 33)
        Me.UserControl12.TabIndex = 33
        Me.UserControl12.TextEx = "Last Write Time : "
        Me.UserControl12.TextExColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(225, Byte), Integer))
        '
        'UserControl11
        '
        Me.UserControl11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.UserControl11.ImageDescriptor = Global.AnonMon.My.Resources.Resources.icons8_processor_321
        Me.UserControl11.ImageDescriptorBackColor = System.Drawing.Color.White
        Me.UserControl11.Location = New System.Drawing.Point(12, 61)
        Me.UserControl11.Name = "UserControl11"
        Me.UserControl11.Size = New System.Drawing.Size(139, 33)
        Me.UserControl11.TabIndex = 32
        Me.UserControl11.TextEx = "Creation Time : "
        Me.UserControl11.TextExColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(225, Byte), Integer))
        '
        'RichTextBox3
        '
        Me.RichTextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.RichTextBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.RichTextBox3.Location = New System.Drawing.Point(165, 174)
        Me.RichTextBox3.Name = "RichTextBox3"
        Me.RichTextBox3.Size = New System.Drawing.Size(228, 23)
        Me.RichTextBox3.TabIndex = 31
        Me.RichTextBox3.Text = "1"
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.RichTextBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.RichTextBox2.Location = New System.Drawing.Point(165, 120)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(228, 23)
        Me.RichTextBox2.TabIndex = 30
        Me.RichTextBox2.Text = "1"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.RichTextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.RichTextBox1.Location = New System.Drawing.Point(165, 71)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(228, 23)
        Me.RichTextBox1.TabIndex = 29
        Me.RichTextBox1.Text = "1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Label1"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(365, 1)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(28, 28)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "X"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'FileInfo_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(405, 230)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.UserControl13)
        Me.Controls.Add(Me.UserControl12)
        Me.Controls.Add(Me.UserControl11)
        Me.Controls.Add(Me.RichTextBox3)
        Me.Controls.Add(Me.RichTextBox2)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FileInfo_Form"
        Me.Text = "File Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UserControl13 As UserControl1
    Friend WithEvents UserControl12 As UserControl1
    Friend WithEvents UserControl11 As UserControl1
    Friend WithEvents RichTextBox3 As RichTextBox
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
End Class

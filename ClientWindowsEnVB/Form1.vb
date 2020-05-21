Imports Params = OlymarsDemo.DataClasses.Parameters
Imports SPs = OlymarsDemo.DataClasses.StoredProcedures
Imports OlymarsDemo.Windows

Public Class Form1
    Inherits System.Windows.Forms.Form
    Private NoOfProductsPerPage As Integer = 3
    Private CurrentPage As Integer = 1

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Personalize_WinDataGrid_tblProduct1()
        Personnaliser_WinDataGridCustom_spS_xReadOrderItems1()
        TableTreeNodeFactory.SetUpConnection("")
        StoredProcedureTreeNodeFactory.SetUpConnection("")

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents WinComboBox_tblCategory1 As OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory
    Friend WithEvents WinListBox_tblProduct1 As OlymarsDemo.Windows.ListBoxes.WinListBox_tblProduct
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents WinDataGrid_tblProduct1 As OlymarsDemo.Windows.DataGrids.WinDataGrid_tblProduct
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents WinCheckedListBox_tblProduct1 As OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblProduct
    Friend WithEvents WinComboBox_tblCategory2 As OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents WinComboBox_tblSupplier1 As OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblSupplier
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents WinComboBoxCustom_spS_xSearchClient1 As OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_xSearchCustomer
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents WinListBox_tblOrder1 As OlymarsDemo.Windows.ListBoxes.WinListBox_tblOrder
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents WinDataGridCustom_spS_xReadOrderItems1 As OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_xReadOrderItems
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.WinListBox_tblProduct1 = New OlymarsDemo.Windows.ListBoxes.WinListBox_tblProduct
        Me.WinComboBox_tblCategory1 = New OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory
        Me.Button3 = New System.Windows.Forms.Button
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.WinComboBox_tblSupplier1 = New OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblSupplier
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button8 = New System.Windows.Forms.Button
        Me.WinComboBox_tblCategory2 = New OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory
        Me.WinCheckedListBox_tblProduct1 = New OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblProduct
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.WinDataGridCustom_spS_xReadOrderItems1 = New OlymarsDemo.Windows.DataGrids.WinDataGridCustom_spS_xReadOrderItems
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.WinListBox_tblOrder1 = New OlymarsDemo.Windows.ListBoxes.WinListBox_tblOrder
        Me.Button11 = New System.Windows.Forms.Button
        Me.Button10 = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.WinComboBoxCustom_spS_xSearchClient1 = New OlymarsDemo.Windows.ComboBoxes.WinComboBoxCustom_spS_xSearchCustomer
        Me.Button9 = New System.Windows.Forms.Button
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.Button12 = New System.Windows.Forms.Button
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.WinDataGrid_tblProduct1 = New OlymarsDemo.Windows.DataGrids.WinDataGrid_tblProduct
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.WinDataGridCustom_spS_xReadOrderItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.WinDataGrid_tblProduct1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(792, 573)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 33)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(784, 536)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Re-use of DataClasses"
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(0, 144)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.Size = New System.Drawing.Size(784, 392)
        Me.DataGrid1.TabIndex = 7
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(280, 56)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(144, 64)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Create the category"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Name of the category:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(8, 48)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(256, 29)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.Text = ""
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(424, 56)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 64)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Complete"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Button5)
        Me.TabPage2.Controls.Add(Me.Button4)
        Me.TabPage2.Controls.Add(Me.WinListBox_tblProduct1)
        Me.TabPage2.Controls.Add(Me.WinComboBox_tblCategory1)
        Me.TabPage2.Controls.Add(Me.Button3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 33)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(784, 536)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Re-use of standard controls"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 352)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(264, 25)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "The price of the product is: ?€"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(288, 296)
        Me.Label2.Name = "Label2"
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Page 1/?"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(560, 296)
        Me.Button5.Name = "Button5"
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Next"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(656, 296)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(104, 23)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "Previous"
        '
        'WinListBox_tblProduct1
        '
        Me.WinListBox_tblProduct1.ItemHeight = 24
        Me.WinListBox_tblProduct1.Location = New System.Drawing.Point(24, 136)
        Me.WinListBox_tblProduct1.Name = "WinListBox_tblProduct1"
        Me.WinListBox_tblProduct1.Size = New System.Drawing.Size(728, 148)
        Me.WinListBox_tblProduct1.TabIndex = 2
        '
        'WinComboBox_tblCategory1
        '
        Me.WinComboBox_tblCategory1.Location = New System.Drawing.Point(24, 80)
        Me.WinComboBox_tblCategory1.Name = "WinComboBox_tblCategory1"
        Me.WinComboBox_tblCategory1.Size = New System.Drawing.Size(736, 32)
        Me.WinComboBox_tblCategory1.TabIndex = 1
        Me.WinComboBox_tblCategory1.Text = "WinComboBox_tblCategory1"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(24, 16)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 40)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "Complete"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.WinComboBox_tblSupplier1)
        Me.TabPage4.Controls.Add(Me.Label6)
        Me.TabPage4.Controls.Add(Me.Label5)
        Me.TabPage4.Controls.Add(Me.Label4)
        Me.TabPage4.Controls.Add(Me.Button8)
        Me.TabPage4.Controls.Add(Me.WinComboBox_tblCategory2)
        Me.TabPage4.Controls.Add(Me.WinCheckedListBox_tblProduct1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 33)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(784, 536)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Re-use of CheckedListBox"
        '
        'WinComboBox_tblSupplier1
        '
        Me.WinComboBox_tblSupplier1.Location = New System.Drawing.Point(0, 72)
        Me.WinComboBox_tblSupplier1.Name = "WinComboBox_tblSupplier1"
        Me.WinComboBox_tblSupplier1.Size = New System.Drawing.Size(760, 32)
        Me.WinComboBox_tblSupplier1.TabIndex = 6
        Me.WinComboBox_tblSupplier1.Text = "WinComboBox_tblSupplier1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(0, 248)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(538, 25)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "List of products (those supplied by the supplier are selected): "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(0, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(160, 25)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "List of categories:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(148, 25)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "List of suppliers:"
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(648, 16)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(96, 40)
        Me.Button8.TabIndex = 2
        Me.Button8.Text = "Refresh"
        '
        'WinComboBox_tblCategory2
        '
        Me.WinComboBox_tblCategory2.Location = New System.Drawing.Point(0, 184)
        Me.WinComboBox_tblCategory2.Name = "WinComboBox_tblCategory2"
        Me.WinComboBox_tblCategory2.Size = New System.Drawing.Size(760, 32)
        Me.WinComboBox_tblCategory2.TabIndex = 1
        Me.WinComboBox_tblCategory2.Text = "WinComboBox_tblCategory2"
        '
        'WinCheckedListBox_tblProduct1
        '
        Me.WinCheckedListBox_tblProduct1.CheckOnClick = True
        Me.WinCheckedListBox_tblProduct1.Location = New System.Drawing.Point(0, 280)
        Me.WinCheckedListBox_tblProduct1.Name = "WinCheckedListBox_tblProduct1"
        Me.WinCheckedListBox_tblProduct1.Size = New System.Drawing.Size(784, 316)
        Me.WinCheckedListBox_tblProduct1.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.WinDataGridCustom_spS_xReadOrderItems1)
        Me.TabPage5.Controls.Add(Me.Label13)
        Me.TabPage5.Controls.Add(Me.Label12)
        Me.TabPage5.Controls.Add(Me.WinListBox_tblOrder1)
        Me.TabPage5.Controls.Add(Me.Button11)
        Me.TabPage5.Controls.Add(Me.Button10)
        Me.TabPage5.Controls.Add(Me.Label11)
        Me.TabPage5.Controls.Add(Me.Label10)
        Me.TabPage5.Controls.Add(Me.WinComboBoxCustom_spS_xSearchClient1)
        Me.TabPage5.Controls.Add(Me.Button9)
        Me.TabPage5.Controls.Add(Me.TextBox3)
        Me.TabPage5.Controls.Add(Me.TextBox2)
        Me.TabPage5.Controls.Add(Me.Label9)
        Me.TabPage5.Controls.Add(Me.Label8)
        Me.TabPage5.Controls.Add(Me.Label7)
        Me.TabPage5.Location = New System.Drawing.Point(4, 33)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(784, 536)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Re-use of personalized controls "
        '
        'WinDataGridCustom_spS_xReadOrderItems1
        '
        Me.WinDataGridCustom_spS_xReadOrderItems1.CommandTimeOut = 30
        Me.WinDataGridCustom_spS_xReadOrderItems1.DataMember = ""
        Me.WinDataGridCustom_spS_xReadOrderItems1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.WinDataGridCustom_spS_xReadOrderItems1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.WinDataGridCustom_spS_xReadOrderItems1.Location = New System.Drawing.Point(0, 416)
        Me.WinDataGridCustom_spS_xReadOrderItems1.Name = "WinDataGridCustom_spS_xReadOrderItems1"
        Me.WinDataGridCustom_spS_xReadOrderItems1.Size = New System.Drawing.Size(784, 120)
        Me.WinDataGridCustom_spS_xReadOrderItems1.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 384)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(108, 25)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Order lines:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(152, 384)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(233, 25)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Total sum of this order: ?€"
        '
        'WinListBox_tblOrder1
        '
        Me.WinListBox_tblOrder1.ItemHeight = 24
        Me.WinListBox_tblOrder1.Location = New System.Drawing.Point(8, 192)
        Me.WinListBox_tblOrder1.Name = "WinListBox_tblOrder1"
        Me.WinListBox_tblOrder1.Size = New System.Drawing.Size(728, 100)
        Me.WinListBox_tblOrder1.TabIndex = 11
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(648, 304)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(112, 32)
        Me.Button11.TabIndex = 10
        Me.Button11.Text = "Next Page"
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(8, 304)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(144, 32)
        Me.Button10.TabIndex = 9
        Me.Button10.Text = "Previous page"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(320, 312)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(108, 25)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Page 1 of X"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 152)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 25)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Order list:"
        '
        'WinComboBoxCustom_spS_xSearchClient1
        '
        Me.WinComboBoxCustom_spS_xSearchClient1.CommandTimeOut = 30
        Me.WinComboBoxCustom_spS_xSearchClient1.Location = New System.Drawing.Point(8, 112)
        Me.WinComboBoxCustom_spS_xSearchClient1.Name = "WinComboBoxCustom_spS_xSearchClient1"
        Me.WinComboBoxCustom_spS_xSearchClient1.Size = New System.Drawing.Size(736, 32)
        Me.WinComboBoxCustom_spS_xSearchClient1.TabIndex = 6
        Me.WinComboBoxCustom_spS_xSearchClient1.Text = "WinComboBoxCustom_spS_xSearchCustomer1"
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(632, 24)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(104, 23)
        Me.Button9.TabIndex = 5
        Me.Button9.Text = "Search... "
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(312, 40)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(272, 29)
        Me.TextBox3.TabIndex = 4
        Me.TextBox3.Text = ""
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(0, 40)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(256, 29)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 25)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Customer list: "
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(312, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(212, 25)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "First name of customer:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(151, 25)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Customer name:"
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.TreeView1)
        Me.TabPage6.Controls.Add(Me.Button12)
        Me.TabPage6.Location = New System.Drawing.Point(4, 33)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(784, 536)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Re-use of the TreeNodeFactory"
        '
        'TreeView1
        '
        Me.TreeView1.ImageIndex = -1
        Me.TreeView1.Location = New System.Drawing.Point(16, 64)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = -1
        Me.TreeView1.Size = New System.Drawing.Size(752, 432)
        Me.TreeView1.TabIndex = 1
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(16, 16)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(128, 32)
        Me.Button12.TabIndex = 0
        Me.Button12.Text = "Refresh"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.WinDataGrid_tblProduct1)
        Me.TabPage3.Controls.Add(Me.Button7)
        Me.TabPage3.Controls.Add(Me.Button6)
        Me.TabPage3.Location = New System.Drawing.Point(4, 33)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(784, 536)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Re-use of the standard DataGrid "
        '
        'WinDataGrid_tblProduct1
        '
        Me.WinDataGrid_tblProduct1.DataMember = ""
        Me.WinDataGrid_tblProduct1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.WinDataGrid_tblProduct1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.WinDataGrid_tblProduct1.Location = New System.Drawing.Point(0, 64)
        Me.WinDataGrid_tblProduct1.Name = "WinDataGrid_tblProduct1"
        Me.WinDataGrid_tblProduct1.Size = New System.Drawing.Size(784, 472)
        Me.WinDataGrid_tblProduct1.TabIndex = 3
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(624, 16)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(88, 32)
        Me.Button7.TabIndex = 2
        Me.Button7.Text = "Update"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(16, 16)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(112, 32)
        Me.Button6.TabIndex = 1
        Me.Button6.Text = "Complete"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(9, 22)
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Form1"
        Me.Text = "Windows Client Application"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        CType(Me.WinDataGridCustom_spS_xReadOrderItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.WinDataGrid_tblProduct1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text.Trim() = String.Empty Then
            MessageBox.Show(Me, "Aucune catégorie n'a été saisie!", "Erreur")
            TextBox1.Select()
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        ' On définit un objet de type Parameter pour prendre en charge les paramètres
        ' de la procédure stockée spI_tblCategory
        Dim MonParam As New OlymarsDemo.DataClasses.Parameters.spI_tblCategory(True)

        ' On affecte la chaîne de connexion à la base de données
        MonParam.SetUpConnection(OlymarsDemo.DataClasses.Information.BuildConnectionString("MAINSERVER\MAINSERVER", "OlymarsDemo"))

        ' On affecte les paramètres de la procédure stockée et notament le nom de la catégorie
        MonParam.Param_Cat_StrName = New System.Data.SqlTypes.SqlString(TextBox1.Text)

        ' On définit maintenant un objet de type StoredProcedure pour exécuter la procédure stockée
        Dim MaSp As New OlymarsDemo.DataClasses.StoredProcedures.spI_tblCategory(True)

        Try
            ' On exécute la procédure stockée avec les paramètres disponibles dans l'objet MonParam
            MaSp.Execute(MonParam)

            ' On affiche la clé primaire affectée à cette nouvelle catégorie
            MessageBox.Show(Me, String.Format("La nouvelle clé primaire est {0}", MonParam.Param_Cat_LngID.Value), "Succès")

        Catch sqlException As System.Data.SqlClient.SqlException
            ' On affiche l'erreur SQL
            MessageBox.Show(Me, String.Format("SqlException: {0}", sqlException.Message), "Erreur")

        Catch exception As Exception
            ' On affiche l'erreur générique
            MessageBox.Show(Me, String.Format("Exception: {0}", exception.Message), "Erreur")

        End Try

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Cursor = Cursors.WaitCursor

        Dim mesCategoriesParam As New Params.spS_tblCategory(True)

        mesCategoriesParam.SetUpConnection("")

        Dim mesCategoriesSp As New SPs.spS_tblCategory(True)
        Dim monDataSet As DataSet

        Try
            ' On exécute la procédure ramenant toutes les catégories
            ' On fournit un DataSet pour récupérer ces données qui
            ' seront stockées dans la table "Catégories"
            mesCategoriesSp.Execute(mesCategoriesParam, monDataSet, "Catégories")

        Catch sqlException As System.Data.SqlClient.SqlException
            ' On affiche l'erreur SQL
            MessageBox.Show(Me, String.Format("SqlException: {0}", sqlException.Message), "Erreur")
            Me.Cursor = Cursors.Default
            Exit Sub

        Catch exception As Exception
            ' On affiche l'erreur générique
            MessageBox.Show(Me, String.Format("Exception: {0}", exception.Message), "Erreur")
            Me.Cursor = Cursors.Default
            Exit Sub

        End Try

        Dim mesProduitsParam As New Params.spS_tblProduct(True)

        mesProduitsParam.SetUpConnection("")

        Dim mesProduitsSp As New SPs.spS_tblProduct(True)

        Try
            mesProduitsSp.Execute(mesProduitsParam, monDataSet, "Produits")

        Catch sqlException As System.Data.SqlClient.SqlException
            ' On affiche l'erreur SQL
            MessageBox.Show(Me, String.Format("SqlException: {0}", sqlException.Message), "Erreur")
            Me.Cursor = Cursors.Default
            Exit Sub

        Catch exception As Exception
            ' On affiche l'erreur générique
            MessageBox.Show(Me, String.Format("Exception: {0}", exception.Message), "Erreur")
            Me.Cursor = Cursors.Default
            Exit Sub

        End Try

        Dim maRelation As New DataRelation("Lis of products", monDataSet.Tables("Catégories").Columns("Cat_LngID"), monDataSet.Tables("Produits").Columns("Pro_LngCategoryID"))

        monDataSet.Relations.Add(maRelation)


        Dim monDataGridTableStyle As DataGridTableStyle
        Dim monDataGridColumnStyle As DataGridColumnStyle

        DataGrid1.TableStyles.Clear()

        ' On redéfinit l'affichage des catégories
        monDataGridTableStyle = New DataGridTableStyle(False)
        monDataGridTableStyle.MappingName = "Catégories"

        ' On définit l'affichage de la colonne 'Cat_StrNom'
        monDataGridColumnStyle = New DataGridTextBoxColumn
        monDataGridColumnStyle.MappingName = "Cat_StrName"
        monDataGridColumnStyle.HeaderText = monDataSet.Tables("Catégories").Columns("Cat_StrName").Caption
        monDataGridColumnStyle.Width = 200
        monDataGridTableStyle.GridColumnStyles.Add(monDataGridColumnStyle)

        DataGrid1.TableStyles.Add(monDataGridTableStyle)



        DataGrid1.DataSource = monDataSet.Tables("Catégories")

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Cursor = Cursors.WaitCursor

        ' We indicate the parameters for connection to the database
        WinComboBox_tblCategory1.Initialize("")

        ' We refresh the content of the ComboBox by pre-selecting the first item
        WinComboBox_tblCategory1.RefreshData()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub WinComboBox_tblCategory1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WinComboBox_tblCategory1.SelectedIndexChanged
        ' If one is called in this event while
        ' the DataBinding operation is underway, one exits
        If WinComboBox_tblCategory1.BindingInProgress Then Exit Sub

        ' If no selection has been made, you exit
        If WinComboBox_tblCategory1.SelectedIndex = -1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        CurrentPage = 1
        RefreshProducts()

        Me.Cursor = Cursors.Default



    End Sub

    Private Sub RefreshProducts()

        ' We indicate the parameters for connection to the database
        ' and we specify that we only want products linked
        ' to the category selected by the user
        WinListBox_tblProduct1.Initialize("", WinComboBox_tblCategory1.SelectedRecordID)

        ' We refresh the content of the ComboBox by pre-selecting the first item
        ' and we expressly request the display of only those products corresponding to the
        ' current page CurrentPage
        WinListBox_tblProduct1.RefreshData((CurrentPage - 1) * NoOfProductsPerPage, NoOfProductsPerPage)

        ' To calculate the total number of pages, we need to know
        ' the total number of records. The stored procedure returns
        ' this information via its return value.
        Dim TotalNoOfPages As Integer = WinListBox_tblProduct1.SP_Parameter.Param_RETURN_VALUE.Value

        ' We calculate the total number of pages
        If TotalNoOfPages Mod NoOfProductsPerPage = 0 Then
            TotalNoOfPages = TotalNoOfPages / NoOfProductsPerPage
        Else
            TotalNoOfPages = System.Math.Floor(Convert.ToDouble(TotalNoOfPages) / Convert.ToDouble(NoOfProductsPerPage)) + 1
        End If

        ' All that is left is to update the label
        Label2.Text = String.Format("Page {0}/{1}", CurrentPage, TotalNoOfPages)

    End Sub



    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If CurrentPage = 1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        CurrentPage -= 1
        RefreshProducts()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        Me.Cursor = Cursors.WaitCursor
        CurrentPage += 1
        RefreshProducts()

        ' If there is no record, we return to the previous page
        If WinListBox_tblProduct1.Items.Count = 0 Then
            CurrentPage -= 1
            RefreshProducts()
        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub WinListBox_tblProduct1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WinListBox_tblProduct1.SelectedIndexChanged
        If WinListBox_tblProduct1.BindingInProgress Then Exit Sub
        If WinListBox_tblProduct1.SelectedIndex = -1 Then Exit Sub

        ' We still don't specify a connection string, since the
        ' configuration file will be used.
        Dim myAbstractClass As OlymarsDemo.AbstractClasses.Abstract_tblProduct


        If WinListBox_tblProduct1.BindingInProgress Then Exit Sub
        If WinListBox_tblProduct1.SelectedIndex = -1 Then Exit Sub

        ' We refresh the current record
        If WinListBox_tblProduct1.RefreshCurrentRecord() Then
            myAbstractClass = WinListBox_tblProduct1.Abstract_tblProduct
        End If

        ' All that is left to do is display its price
        Label3.Text = String.Format("The price of the product is: {0:c}", myAbstractClass.Col_Pro_CurPrice.Value)

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.Cursor = Cursors.WaitCursor

        ' In this particular case, ALL the products are loaded
        WinDataGrid_tblProduct1.Initialize("", Nothing)
        WinDataGrid_tblProduct1.RefreshData()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        Me.Cursor = Cursors.WaitCursor

        ' We confirm all the changes made by the user
        WinDataGrid_tblProduct1.CommitChanges()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Personalize_WinDataGrid_tblProduct1()

        Dim myDataGridTableStyle As DataGridTableStyle
        Dim myDataGridColumnStyle As DataGridColumnStyle

        WinDataGrid_tblProduct1.TableStyles.Clear()

        ' We redefine the display of the products
        myDataGridTableStyle = New DataGridTableStyle(False)
        myDataGridTableStyle.MappingName = "tblProduct"

        ' We define the display of the column 'Pro_StrName'
        myDataGridColumnStyle = New DataGridTextBoxColumn
        myDataGridColumnStyle.MappingName = "Pro_StrName"
        myDataGridColumnStyle.HeaderText = "Name"
        myDataGridColumnStyle.Width = 200
        myDataGridTableStyle.GridColumnStyles.Add(myDataGridColumnStyle)

        ' We define the display of the column 'Pro_CurPrice'
        myDataGridColumnStyle = New DataGridTextBoxColumn
        myDataGridColumnStyle.MappingName = "Pro_CurPrice"
        myDataGridColumnStyle.HeaderText = "Price in €"
        myDataGridColumnStyle.Width = 200
        myDataGridTableStyle.GridColumnStyles.Add(myDataGridColumnStyle)

        WinDataGrid_tblProduct1.TableStyles.Add(myDataGridTableStyle)

    End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Cursor = Cursors.WaitCursor

        ' We initialize the supplier ComboBox
        WinComboBox_tblSupplier1.Initialize("")
        WinComboBox_tblSupplier1.RefreshData()

        ' We initialize the categories ComboBox
        WinComboBox_tblCategory2.Initialize("")
        WinComboBox_tblCategory2.RefreshData()

        Me.Cursor = Cursors.Default
    End Sub

    Private Function BuildTable() As System.Collections.ArrayList

        ' In this procedure, we use an ArrayList to return those products
        ' supplied by the supplier that is currently selected.

        Dim myParam As New Params.spS_tblSupplierProduct(True)
        myParam.SetUpConnection("")
        myParam.Param_Spr_GuidSupplierID = WinComboBox_tblSupplier1.SelectedRecordID

        Dim mySqlDataReader As SqlClient.SqlDataReader
        Dim myTable As New System.Collections.ArrayList

        Dim mySP As New SPs.spS_tblSupplierProduct(True)

        ' we ask to retrieve an SqlDataReader
        mySP.Execute(myParam, mySqlDataReader)

        While (mySqlDataReader.Read())

            ' 0=index of the product's primary key
            myTable.Add(mySqlDataReader.GetSqlGuid(0))

        End While

        mySqlDataReader.Close()

        mySP.Dispose()
        myParam.Dispose()

        Return (myTable)

    End Function

    Private Sub WinComboBox_tblCategory2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WinComboBox_tblCategory2.SelectedIndexChanged
        If WinComboBox_tblCategory2.BindingInProgress Then Exit Sub
        If WinComboBox_tblCategory2.SelectedIndex = -1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        WinCheckedListBox_tblProduct1.Initialize("", WinComboBox_tblCategory2.SelectedRecordID)
        WinCheckedListBox_tblProduct1.RefreshData(BuildTable())

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub WinComboBox_tblSupplier1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WinComboBox_tblSupplier1.SelectedIndexChanged
        If WinComboBox_tblSupplier1.BindingInProgress Then Exit Sub
        If WinComboBox_tblSupplier1.SelectedIndex = -1 Then Exit Sub

        ' It is not worth refreshing the list of products here
        ' It is enough to update the status of the products

        ' We return all the products to the unselected state
        WinCheckedListBox_tblProduct1.SetAllRecordsCheckState(CheckState.Unchecked)

        ' We reconstruct the new table and refresh the status of the items
        WinCheckedListBox_tblProduct1.SetRecordsCheckState(BuildTable(), CheckState.Checked)


    End Sub



    Private Sub WinCheckedListBox_tblProduct1_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles WinCheckedListBox_tblProduct1.ItemCheck

        ' Was it the user who provoked this event? If not, we exit
        If WinCheckedListBox_tblProduct1.InternalItemCheckStateUpdateInProgress Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        ' We recuperate the identifier of the product currently being processed
        Dim myDataRowView As System.Data.DataRowView
        myDataRowView = CType(WinCheckedListBox_tblProduct1.Items(e.Index), System.Data.DataRowView)
        Dim productID = New SqlTypes.SqlGuid(CType(myDataRowView(0), Guid))

        Select Case e.NewValue

            Case CheckState.Unchecked

                ' In this case, we are in the process of deleting a Supplier-Product relationship
                ' we call the stored procedure for deletion: spD_tblSupplierProduct
                Dim myParam As New Params.spD_tblSupplierProduct(True)
                myParam.SetUpConnection("")

                myParam.Param_Spr_GuidSupplierID = WinComboBox_tblSupplier1.SelectedRecordID
                myParam.Param_Spr_GuidProductID = productID

                Dim mySP As New SPs.spD_tblSupplierProduct(True)
                mySP.Execute(myParam)

                mySP.Dispose()
                myParam.Dispose()

            Case CheckState.Checked

                ' In this case, we are in the process of creating a Supplier-Product relationship
                ' We call the stored procedure for insertion: spI_tblSupplierProduct
                Dim myParam As New Params.spI_tblSupplierProduct(True)
                myParam.SetUpConnection("")

                myParam.Param_Spr_GuidSupplierID = WinComboBox_tblSupplier1.SelectedRecordID
                myParam.Param_Spr_GuidProductID = productID

                Dim mySP As New SPs.spI_tblSupplierProduct(True)
                mySP.Execute(myParam)

                mySP.Dispose()
                myParam.Dispose()

        End Select

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If TextBox2.Text.Trim().Length = 0 And TextBox3.Text.Trim().Length = 0 Then

            MessageBox.Show(Me, "Vous devez choisir au moins un des deux critères", "Erreur")
            TextBox2.Select()
            Exit Sub

        End If

        Me.Cursor = Cursors.WaitCursor

        Dim nomChoisi As SqlTypes.SqlString = SqlTypes.SqlString.Null
        Dim prenomChoisi As SqlTypes.SqlString = SqlTypes.SqlString.Null

        If TextBox2.Text.Trim().Length <> 0 Then nomChoisi = New SqlTypes.SqlString(TextBox2.Text)
        If TextBox3.Text.Trim().Length <> 0 Then prenomChoisi = New SqlTypes.SqlString(TextBox3.Text)

        ' On indique maintenant quel champ doit être la clé primaire et quel champ
        ' doit être affiché dans la ComboBox. On passe ensuite les paramètres attendus
        ' par la procédure stockée, à savoir les masques du nom et du prénom
        Me.WinComboBoxCustom_spS_xSearchClient1.Initialize("", "ID1", "Display", nomChoisi, prenomChoisi)

        WinComboBoxCustom_spS_xSearchClient1.RefreshData()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub WinComboBoxCustom_spS_xSearchClient1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WinComboBoxCustom_spS_xSearchClient1.SelectedIndexChanged
        If Me.WinComboBoxCustom_spS_xSearchClient1.SelectedIndex = -1 Then Exit Sub
        If WinComboBoxCustom_spS_xSearchClient1.BindingInProgress Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        PageCommandesCourante = 1
        RafraichirLesCommandes()

        Me.Cursor = Cursors.Default

    End Sub

    Private PageCommandesCourante As Integer = 1
    Private NbCommandesParPage As Integer = 3

    Private Sub RafraichirLesCommandes()

        Dim premierRecord As Integer
        premierRecord = (PageCommandesCourante - 1) * NbCommandesParPage

        WinListBox_tblOrder1.Initialize("", New SqlTypes.SqlInt32( _
       WinComboBoxCustom_spS_xSearchClient1.SelectedRecordID))
        WinListBox_tblOrder1.RefreshData(premierRecord, NbCommandesParPage)

        Dim nbTotalDePages As Integer
        nbTotalDePages = WinListBox_tblOrder1.SP_Parameter.Param_RETURN_VALUE.Value

        If nbTotalDePages Mod NbCommandesParPage = 0 Then
            nbTotalDePages = nbTotalDePages / NbCommandesParPage
        Else
            nbTotalDePages = System.Math.Floor(Convert.ToDouble(nbTotalDePages) / _
               Convert.ToDouble(NbCommandesParPage)) + 1
        End If

        Label11.Text = String.Format("Page {0} sur {1}", PageCommandesCourante, nbTotalDePages)

    End Sub


    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If PageCommandesCourante = 1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        PageCommandesCourante -= 1
        RafraichirLesCommandes()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Me.Cursor = Cursors.WaitCursor

        PageCommandesCourante += 1
        RafraichirLesCommandes()

        If WinListBox_tblOrder1.Items.Count = 0 Then

            PageCommandesCourante -= 1
            RafraichirLesCommandes()

        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Personnaliser_WinDataGridCustom_spS_xReadOrderItems1()

        Dim monDataGridTableStyle As DataGridTableStyle
        Dim monDataGridColumnStyle As DataGridColumnStyle

        Me.WinDataGridCustom_spS_xReadOrderItems1.TableStyles.Clear()

        ' On redéfinit l'affichage des produits
        monDataGridTableStyle = New DataGridTableStyle(False)
        monDataGridTableStyle.MappingName = "spS_xReadOrderItems"

        ' On définit l'affichage de la colonne 'NomProduit'
        monDataGridColumnStyle = New DataGridTextBoxColumn
        monDataGridColumnStyle.MappingName = "NomProduct"
        monDataGridColumnStyle.HeaderText = "Product"
        monDataGridColumnStyle.Width = 400
        monDataGridTableStyle.GridColumnStyles.Add(monDataGridColumnStyle)

        ' On définit l'affichage de la colonne 'Quantite'
        monDataGridColumnStyle = New DataGridTextBoxColumn
        monDataGridColumnStyle.MappingName = "Quantite"
        monDataGridColumnStyle.HeaderText = "Quantity"
        monDataGridColumnStyle.Width = 100
        monDataGridTableStyle.GridColumnStyles.Add(monDataGridColumnStyle)

        WinDataGridCustom_spS_xReadOrderItems1.TableStyles.Add(monDataGridTableStyle)

    End Sub



    Private Sub WinListBox_tblOrder1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WinListBox_tblOrder1.SelectedIndexChanged

        If WinListBox_tblOrder1.SelectedIndex = -1 Then Exit Sub
        If WinListBox_tblOrder1.BindingInProgress = -1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        WinListBox_tblOrder1.RefreshCurrentRecord()
        Label12.Text = String.Format("Montant total de cette commande: {0:c}", _
           WinListBox_tblOrder1.Abstract_tblOrder.Col_Ord_CurTotal.Value)

        WinDataGridCustom_spS_xReadOrderItems1.Initialize("", WinListBox_tblOrder1.SelectedRecordID)
        WinDataGridCustom_spS_xReadOrderItems1.RefreshData()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click

        Me.Cursor = Cursors.WaitCursor

        ' We empty the content of the TreeView
        TreeView1.Nodes.Clear()

        ' We add the parent TreeNode that will contain our categories
        Dim categories As New TreeNode("Categories")
        TreeView1.Nodes.Add(categories)

        ' The first True indicates that we want to purge the TreeNode provided
        ' The second True indicates that we want to insert a child TreeNode under the categories nodes
        TableTreeNodeFactory.Fill_tblCategory(categories.Nodes, "Categories", True, True)

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub TreeView1_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeExpand
        Me.Cursor = Cursors.WaitCursor

        ' What type of node are we on right now
        If TypeOf e.Node Is TableCustomTreeNode Then

            TableProcessing(CType(e.Node, TableCustomTreeNode))

        End If

        Me.Cursor = Cursors.Default
       
    End Sub

    Private Sub TableProcessing(ByVal currentNode As TableCustomTreeNode)

        ' We do not refresh the node if this has already been done
        If currentNode.IsUpToDate Then Exit Sub

        Select Case currentNode.ContextOfUse

            Case "Categories"
                TableTreeNodeFactory.Fill_tblProduct(currentNode.Nodes, "Products", True, _
                   True, CType(currentNode.Id, SqlTypes.SqlInt32))

            Case "Products"
                StoredProcedureTreeNodeFactory.Fill_spS_xQuantityOrderedPerProduct( _
                   currentNode.Nodes, "CustomersForAProduct", True, False, 0, 1, _
                   CType(currentNode.Id, SqlTypes.SqlGuid))

        End Select

        ' We indicate that the node was then refreshed
        currentNode.IsUpToDate = True

    End Sub



    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub
End Class

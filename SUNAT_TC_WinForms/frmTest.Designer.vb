<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTest
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
        Me.DataSet1 = New System.Data.DataSet()
        Me.DataTable1 = New System.Data.DataTable()
        Me.DataColumn1 = New System.Data.DataColumn()
        Me.DataColumn2 = New System.Data.DataColumn()
        Me.DataColumn3 = New System.Data.DataColumn()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.FechaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CompraDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VentaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnCargar = New System.Windows.Forms.Button()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        Me.DataSet1.Tables.AddRange(New System.Data.DataTable() {Me.DataTable1})
        '
        'DataTable1
        '
        Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3})
        Me.DataTable1.TableName = "Table1"
        '
        'DataColumn1
        '
        Me.DataColumn1.Caption = "Fecha"
        Me.DataColumn1.ColumnName = "Fecha"
        Me.DataColumn1.DataType = GetType(Date)
        '
        'DataColumn2
        '
        Me.DataColumn2.Caption = "Compra"
        Me.DataColumn2.ColumnName = "Compra"
        Me.DataColumn2.DataType = GetType(Double)
        '
        'DataColumn3
        '
        Me.DataColumn3.Caption = "Venta"
        Me.DataColumn3.ColumnName = "Venta"
        Me.DataColumn3.DataType = GetType(Double)
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FechaDataGridViewTextBoxColumn, Me.CompraDataGridViewTextBoxColumn, Me.VentaDataGridViewTextBoxColumn})
        Me.DataGridView1.DataMember = "Table1"
        Me.DataGridView1.DataSource = Me.DataSet1
        Me.DataGridView1.Location = New System.Drawing.Point(11, 62)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(616, 446)
        Me.DataGridView1.TabIndex = 0
        '
        'FechaDataGridViewTextBoxColumn
        '
        Me.FechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha"
        Me.FechaDataGridViewTextBoxColumn.HeaderText = "Fecha"
        Me.FechaDataGridViewTextBoxColumn.Name = "FechaDataGridViewTextBoxColumn"
        Me.FechaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CompraDataGridViewTextBoxColumn
        '
        Me.CompraDataGridViewTextBoxColumn.DataPropertyName = "Compra"
        Me.CompraDataGridViewTextBoxColumn.HeaderText = "Compra"
        Me.CompraDataGridViewTextBoxColumn.Name = "CompraDataGridViewTextBoxColumn"
        Me.CompraDataGridViewTextBoxColumn.ReadOnly = True
        '
        'VentaDataGridViewTextBoxColumn
        '
        Me.VentaDataGridViewTextBoxColumn.DataPropertyName = "Venta"
        Me.VentaDataGridViewTextBoxColumn.HeaderText = "Venta"
        Me.VentaDataGridViewTextBoxColumn.Name = "VentaDataGridViewTextBoxColumn"
        Me.VentaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'BtnCargar
        '
        Me.BtnCargar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnCargar.Location = New System.Drawing.Point(19, 13)
        Me.BtnCargar.Name = "BtnCargar"
        Me.BtnCargar.Size = New System.Drawing.Size(117, 43)
        Me.BtnCargar.TabIndex = 0
        Me.BtnCargar.Text = "Cargar"
        Me.BtnCargar.UseVisualStyleBackColor = True
        '
        'frmTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 514)
        Me.Controls.Add(Me.BtnCargar)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmTest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test SUNAT TC"
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataSet1 As DataSet
    Friend WithEvents DataTable1 As DataTable
    Friend WithEvents DataColumn1 As DataColumn
    Friend WithEvents DataColumn2 As DataColumn
    Friend WithEvents DataColumn3 As DataColumn
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents FechaDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents CompraDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents VentaDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents BtnCargar As Button
End Class

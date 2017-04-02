namespace SUNAT_TC_Test_CS
{
    partial class frmTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataSet1 = new System.Data.DataSet();
            this.DataTable1 = new System.Data.DataTable();
            this.DataColumn1 = new System.Data.DataColumn();
            this.DataColumn2 = new System.Data.DataColumn();
            this.DataColumn3 = new System.Data.DataColumn();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.FechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompraDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.CmbMes = new System.Windows.Forms.ComboBox();
            this.CmbAño = new System.Windows.Forms.ComboBox();
            this.BtnCargar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "NewDataSet";
            this.DataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.DataTable1});
            // 
            // DataTable1
            // 
            this.DataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.DataColumn1,
            this.DataColumn2,
            this.DataColumn3});
            this.DataTable1.TableName = "Table1";
            // 
            // DataColumn1
            // 
            this.DataColumn1.Caption = "Fecha";
            this.DataColumn1.ColumnName = "Fecha";
            this.DataColumn1.DataType = typeof(System.DateTime);
            // 
            // DataColumn2
            // 
            this.DataColumn2.Caption = "Compra";
            this.DataColumn2.ColumnName = "Compra";
            this.DataColumn2.DataType = typeof(double);
            // 
            // DataColumn3
            // 
            this.DataColumn3.Caption = "Venta";
            this.DataColumn3.ColumnName = "Venta";
            this.DataColumn3.DataType = typeof(double);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.AutoGenerateColumns = false;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaDataGridViewTextBoxColumn,
            this.CompraDataGridViewTextBoxColumn,
            this.VentaDataGridViewTextBoxColumn});
            this.DataGridView1.DataMember = "Table1";
            this.DataGridView1.DataSource = this.DataSet1;
            this.DataGridView1.Location = new System.Drawing.Point(1, 66);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            this.DataGridView1.Size = new System.Drawing.Size(436, 498);
            this.DataGridView1.TabIndex = 2;
            // 
            // FechaDataGridViewTextBoxColumn
            // 
            this.FechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            this.FechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.FechaDataGridViewTextBoxColumn.Name = "FechaDataGridViewTextBoxColumn";
            this.FechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // CompraDataGridViewTextBoxColumn
            // 
            this.CompraDataGridViewTextBoxColumn.DataPropertyName = "Compra";
            this.CompraDataGridViewTextBoxColumn.HeaderText = "Compra";
            this.CompraDataGridViewTextBoxColumn.Name = "CompraDataGridViewTextBoxColumn";
            this.CompraDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // VentaDataGridViewTextBoxColumn
            // 
            this.VentaDataGridViewTextBoxColumn.DataPropertyName = "Venta";
            this.VentaDataGridViewTextBoxColumn.HeaderText = "Venta";
            this.VentaDataGridViewTextBoxColumn.Name = "VentaDataGridViewTextBoxColumn";
            this.VentaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 39);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(30, 13);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Mes:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(29, 13);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Año:";
            // 
            // CmbMes
            // 
            this.CmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMes.FormattingEnabled = true;
            this.CmbMes.Location = new System.Drawing.Point(41, 39);
            this.CmbMes.Name = "CmbMes";
            this.CmbMes.Size = new System.Drawing.Size(121, 21);
            this.CmbMes.Sorted = true;
            this.CmbMes.TabIndex = 8;
            // 
            // CmbAño
            // 
            this.CmbAño.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAño.FormattingEnabled = true;
            this.CmbAño.Location = new System.Drawing.Point(41, 12);
            this.CmbAño.Name = "CmbAño";
            this.CmbAño.Size = new System.Drawing.Size(121, 21);
            this.CmbAño.TabIndex = 7;
            // 
            // BtnCargar
            // 
            this.BtnCargar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnCargar.Location = new System.Drawing.Point(182, 15);
            this.BtnCargar.Name = "BtnCargar";
            this.BtnCargar.Size = new System.Drawing.Size(117, 43);
            this.BtnCargar.TabIndex = 6;
            this.BtnCargar.Text = "Cargar";
            this.BtnCargar.UseVisualStyleBackColor = true;
            this.BtnCargar.Click += new System.EventHandler(this.BtnCargar_Click);
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 567);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.CmbMes);
            this.Controls.Add(this.CmbAño);
            this.Controls.Add(this.BtnCargar);
            this.Controls.Add(this.DataGridView1);
            this.Name = "frmTest";
            this.Text = "Test SUNAT TC (C#)";
            this.Load += new System.EventHandler(this.frmTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Data.DataSet DataSet1;
        internal System.Data.DataTable DataTable1;
        internal System.Data.DataColumn DataColumn1;
        internal System.Data.DataColumn DataColumn2;
        internal System.Data.DataColumn DataColumn3;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn FechaDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn CompraDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn VentaDataGridViewTextBoxColumn;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ComboBox CmbMes;
        private System.Windows.Forms.ComboBox CmbAño;
        internal System.Windows.Forms.Button BtnCargar;
    }
}


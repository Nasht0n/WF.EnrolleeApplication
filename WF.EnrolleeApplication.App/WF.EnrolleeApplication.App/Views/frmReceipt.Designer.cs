namespace WF.EnrolleeApplication.App.Views
{
    partial class frmReceipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceipt));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btPrint = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbDocumentOfStudy = new System.Windows.Forms.TextBox();
            this.tbDocumentForDiscount = new System.Windows.Forms.TextBox();
            this.tbOtherDocument = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.btPrint);
            this.flowLayoutPanel.Controls.Add(this.btCancel);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel.Location = new System.Drawing.Point(5, 384);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 7, 2, 0);
            this.flowLayoutPanel.Size = new System.Drawing.Size(614, 52);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(509, 10);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(100, 29);
            this.btPrint.TabIndex = 0;
            this.btPrint.Text = "Печать";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(403, 10);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 29);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(614, 379);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tbDocumentOfStudy);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Документ об образовании (с приложением)";
            // 
            // groupBox2
            // 
            this.tableLayoutPanel.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.tbDocumentForDiscount);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Документы (копии), подтверждающие право на льготы";
            // 
            // groupBox3
            // 
            this.tableLayoutPanel.SetColumnSpan(this.groupBox3, 2);
            this.groupBox3.Controls.Add(this.tbOtherDocument);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(608, 223);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Иные документы";
            // 
            // tbDocumentOfStudy
            // 
            this.tbDocumentOfStudy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDocumentOfStudy.Location = new System.Drawing.Point(3, 19);
            this.tbDocumentOfStudy.Name = "tbDocumentOfStudy";
            this.tbDocumentOfStudy.Size = new System.Drawing.Size(602, 23);
            this.tbDocumentOfStudy.TabIndex = 0;
            // 
            // tbDocumentForDiscount
            // 
            this.tbDocumentForDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDocumentForDiscount.Location = new System.Drawing.Point(3, 19);
            this.tbDocumentForDiscount.Multiline = true;
            this.tbDocumentForDiscount.Name = "tbDocumentForDiscount";
            this.tbDocumentForDiscount.Size = new System.Drawing.Size(602, 66);
            this.tbDocumentForDiscount.TabIndex = 1;
            // 
            // tbOtherDocument
            // 
            this.tbOtherDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOtherDocument.Location = new System.Drawing.Point(3, 19);
            this.tbOtherDocument.Multiline = true;
            this.tbOtherDocument.Name = "tbOtherDocument";
            this.tbOtherDocument.Size = new System.Drawing.Size(602, 201);
            this.tbOtherDocument.TabIndex = 1;
            // 
            // frmReceipt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.flowLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReceipt";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подготовка отчета — Расписка";
            this.flowLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbOtherDocument;
        private System.Windows.Forms.TextBox tbDocumentForDiscount;
        private System.Windows.Forms.TextBox tbDocumentOfStudy;
    }
}
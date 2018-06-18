namespace WF.EnrolleeApplication.App.Views
{
    partial class frmChooseSpeciality
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChooseSpeciality));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btPrint = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbFormOfStudy = new System.Windows.Forms.GroupBox();
            this.cbFormOfStudy = new System.Windows.Forms.ComboBox();
            this.gbFaculty = new System.Windows.Forms.GroupBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.cbSpeciality = new System.Windows.Forms.ComboBox();
            this.gbSpeciality = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbFormOfStudy.SuspendLayout();
            this.gbFaculty.SuspendLayout();
            this.gbSpeciality.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.btPrint);
            this.flowLayoutPanel.Controls.Add(this.btCancel);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 166);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(464, 35);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(361, 3);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(100, 29);
            this.btPrint.TabIndex = 0;
            this.btPrint.Text = "Печать";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(255, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 29);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.gbSpeciality, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gbFormOfStudy, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbFaculty, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 160);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // gbFormOfStudy
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbFormOfStudy, 2);
            this.gbFormOfStudy.Controls.Add(this.cbFormOfStudy);
            this.gbFormOfStudy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFormOfStudy.Location = new System.Drawing.Point(3, 56);
            this.gbFormOfStudy.Name = "gbFormOfStudy";
            this.gbFormOfStudy.Size = new System.Drawing.Size(458, 47);
            this.gbFormOfStudy.TabIndex = 3;
            this.gbFormOfStudy.TabStop = false;
            this.gbFormOfStudy.Text = "Выберите форму обучения";
            // 
            // cbFormOfStudy
            // 
            this.cbFormOfStudy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFormOfStudy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormOfStudy.FormattingEnabled = true;
            this.cbFormOfStudy.Location = new System.Drawing.Point(3, 19);
            this.cbFormOfStudy.Name = "cbFormOfStudy";
            this.cbFormOfStudy.Size = new System.Drawing.Size(452, 23);
            this.cbFormOfStudy.TabIndex = 0;
            this.cbFormOfStudy.SelectedValueChanged += new System.EventHandler(this.cbFormOfStudy_SelectedValueChanged);
            // 
            // gbFaculty
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbFaculty, 2);
            this.gbFaculty.Controls.Add(this.cbFaculty);
            this.gbFaculty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFaculty.Location = new System.Drawing.Point(3, 3);
            this.gbFaculty.Name = "gbFaculty";
            this.gbFaculty.Size = new System.Drawing.Size(458, 47);
            this.gbFaculty.TabIndex = 1;
            this.gbFaculty.TabStop = false;
            this.gbFaculty.Text = "Выберите факультет";
            // 
            // cbFaculty
            // 
            this.cbFaculty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(3, 19);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(452, 23);
            this.cbFaculty.TabIndex = 0;
            this.cbFaculty.SelectedValueChanged += new System.EventHandler(this.cbFaculty_SelectedValueChanged);
            // 
            // cbSpeciality
            // 
            this.cbSpeciality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSpeciality.FormattingEnabled = true;
            this.cbSpeciality.Location = new System.Drawing.Point(3, 19);
            this.cbSpeciality.Name = "cbSpeciality";
            this.cbSpeciality.Size = new System.Drawing.Size(452, 23);
            this.cbSpeciality.TabIndex = 0;
            this.cbSpeciality.SelectedValueChanged += new System.EventHandler(this.cbSpeciality_SelectedValueChanged);
            // 
            // gbSpeciality
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbSpeciality, 2);
            this.gbSpeciality.Controls.Add(this.cbSpeciality);
            this.gbSpeciality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSpeciality.Location = new System.Drawing.Point(3, 109);
            this.gbSpeciality.Name = "gbSpeciality";
            this.gbSpeciality.Size = new System.Drawing.Size(458, 48);
            this.gbSpeciality.TabIndex = 4;
            this.gbSpeciality.TabStop = false;
            this.gbSpeciality.Text = "Выберите специальность";
            // 
            // frmSummaryExaminationPrint
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 201);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSummaryExaminationPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подготовка отчёта \"Сводная экзаменнационная ведомость\"";
            this.flowLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbFormOfStudy.ResumeLayout(false);
            this.gbFaculty.ResumeLayout(false);
            this.gbSpeciality.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbFormOfStudy;
        private System.Windows.Forms.ComboBox cbFormOfStudy;
        private System.Windows.Forms.GroupBox gbFaculty;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.GroupBox gbSpeciality;
        private System.Windows.Forms.ComboBox cbSpeciality;
    }
}
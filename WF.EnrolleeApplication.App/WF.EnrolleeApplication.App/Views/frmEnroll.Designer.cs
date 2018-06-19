namespace WF.EnrolleeApplication.App.Views
{
    partial class frmEnroll
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnroll));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbSpeciality = new System.Windows.Forms.GroupBox();
            this.cbSpeciality = new System.Windows.Forms.ComboBox();
            this.gbFormOfStudy = new System.Windows.Forms.GroupBox();
            this.cbFormOfStudy = new System.Windows.Forms.ComboBox();
            this.gbFaculty = new System.Windows.Forms.GroupBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btClear = new System.Windows.Forms.Button();
            this.btReport = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.gbEnrollee = new System.Windows.Forms.GroupBox();
            this.EnrolleeGrid = new System.Windows.Forms.DataGridView();
            this.gbEnrollPanel = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbPriority = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btCancelEnrollCurrentEnrollee = new System.Windows.Forms.Button();
            this.btEnrollCurrentEnrollee = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDecree = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbSpeciality.SuspendLayout();
            this.gbFormOfStudy.SuspendLayout();
            this.gbFaculty.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbEnrollee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnrolleeGrid)).BeginInit();
            this.gbEnrollPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.gbSpeciality, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbFormOfStudy, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbFaculty, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 107);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gbSpeciality
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbSpeciality, 2);
            this.gbSpeciality.Controls.Add(this.cbSpeciality);
            this.gbSpeciality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSpeciality.Location = new System.Drawing.Point(3, 56);
            this.gbSpeciality.Name = "gbSpeciality";
            this.gbSpeciality.Size = new System.Drawing.Size(978, 48);
            this.gbSpeciality.TabIndex = 4;
            this.gbSpeciality.TabStop = false;
            this.gbSpeciality.Text = "Выберите специальность";
            // 
            // cbSpeciality
            // 
            this.cbSpeciality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSpeciality.FormattingEnabled = true;
            this.cbSpeciality.Location = new System.Drawing.Point(3, 19);
            this.cbSpeciality.Name = "cbSpeciality";
            this.cbSpeciality.Size = new System.Drawing.Size(972, 23);
            this.cbSpeciality.TabIndex = 0;
            this.cbSpeciality.SelectedValueChanged += new System.EventHandler(this.cbSpeciality_SelectedValueChanged);
            // 
            // gbFormOfStudy
            // 
            this.gbFormOfStudy.Controls.Add(this.cbFormOfStudy);
            this.gbFormOfStudy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFormOfStudy.Location = new System.Drawing.Point(347, 3);
            this.gbFormOfStudy.Name = "gbFormOfStudy";
            this.gbFormOfStudy.Size = new System.Drawing.Size(634, 47);
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
            this.cbFormOfStudy.Size = new System.Drawing.Size(628, 23);
            this.cbFormOfStudy.TabIndex = 0;
            this.cbFormOfStudy.SelectedValueChanged += new System.EventHandler(this.cbFormOfStudy_SelectedValueChanged);
            // 
            // gbFaculty
            // 
            this.gbFaculty.Controls.Add(this.cbFaculty);
            this.gbFaculty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFaculty.Location = new System.Drawing.Point(3, 3);
            this.gbFaculty.Name = "gbFaculty";
            this.gbFaculty.Size = new System.Drawing.Size(338, 47);
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
            this.cbFaculty.Size = new System.Drawing.Size(332, 23);
            this.cbFaculty.TabIndex = 0;
            this.cbFaculty.SelectedValueChanged += new System.EventHandler(this.cbFaculty_SelectedValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btClear);
            this.flowLayoutPanel1.Controls.Add(this.btReport);
            this.flowLayoutPanel1.Controls.Add(this.btCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 525);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(984, 36);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(781, 3);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(200, 28);
            this.btClear.TabIndex = 1;
            this.btClear.Text = "Отменить зачисление";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btReport
            // 
            this.btReport.Location = new System.Drawing.Point(625, 3);
            this.btReport.Name = "btReport";
            this.btReport.Size = new System.Drawing.Size(150, 28);
            this.btReport.TabIndex = 3;
            this.btReport.Text = "Печать выписки";
            this.btReport.UseVisualStyleBackColor = true;
            this.btReport.Click += new System.EventHandler(this.btReport_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(519, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 28);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gbEnrollee
            // 
            this.gbEnrollee.Controls.Add(this.EnrolleeGrid);
            this.gbEnrollee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEnrollee.Location = new System.Drawing.Point(0, 107);
            this.gbEnrollee.Name = "gbEnrollee";
            this.gbEnrollee.Size = new System.Drawing.Size(984, 344);
            this.gbEnrollee.TabIndex = 3;
            this.gbEnrollee.TabStop = false;
            this.gbEnrollee.Text = "Список абитуриентов";
            // 
            // EnrolleeGrid
            // 
            this.EnrolleeGrid.AllowUserToAddRows = false;
            this.EnrolleeGrid.AllowUserToDeleteRows = false;
            this.EnrolleeGrid.AllowUserToResizeColumns = false;
            this.EnrolleeGrid.AllowUserToResizeRows = false;
            this.EnrolleeGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EnrolleeGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.EnrolleeGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EnrolleeGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.EnrolleeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EnrolleeGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.EnrolleeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnrolleeGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.EnrolleeGrid.GridColor = System.Drawing.SystemColors.ControlLight;
            this.EnrolleeGrid.Location = new System.Drawing.Point(3, 19);
            this.EnrolleeGrid.MultiSelect = false;
            this.EnrolleeGrid.Name = "EnrolleeGrid";
            this.EnrolleeGrid.ReadOnly = true;
            this.EnrolleeGrid.RowHeadersVisible = false;
            this.EnrolleeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EnrolleeGrid.Size = new System.Drawing.Size(978, 322);
            this.EnrolleeGrid.TabIndex = 0;
            this.EnrolleeGrid.SelectionChanged += new System.EventHandler(this.EnrolleeGrid_SelectionChanged);
            // 
            // gbEnrollPanel
            // 
            this.gbEnrollPanel.Controls.Add(this.groupBox3);
            this.gbEnrollPanel.Controls.Add(this.flowLayoutPanel2);
            this.gbEnrollPanel.Controls.Add(this.groupBox2);
            this.gbEnrollPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbEnrollPanel.Location = new System.Drawing.Point(0, 451);
            this.gbEnrollPanel.Name = "gbEnrollPanel";
            this.gbEnrollPanel.Size = new System.Drawing.Size(984, 74);
            this.gbEnrollPanel.TabIndex = 4;
            this.gbEnrollPanel.TabStop = false;
            this.gbEnrollPanel.Text = "Информация об абитуриенте";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbPriority);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(203, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 52);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Укажите приоритетную специальность ";
            // 
            // cbPriority
            // 
            this.cbPriority.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPriority.FormattingEnabled = true;
            this.cbPriority.Location = new System.Drawing.Point(3, 19);
            this.cbPriority.Name = "cbPriority";
            this.cbPriority.Size = new System.Drawing.Size(511, 23);
            this.cbPriority.TabIndex = 1;
            this.cbPriority.SelectedValueChanged += new System.EventHandler(this.cbPriority_SelectedValueChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btCancelEnrollCurrentEnrollee);
            this.flowLayoutPanel2.Controls.Add(this.btEnrollCurrentEnrollee);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(720, 19);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(261, 52);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // btCancelEnrollCurrentEnrollee
            // 
            this.btCancelEnrollCurrentEnrollee.Location = new System.Drawing.Point(6, 6);
            this.btCancelEnrollCurrentEnrollee.Name = "btCancelEnrollCurrentEnrollee";
            this.btCancelEnrollCurrentEnrollee.Size = new System.Drawing.Size(120, 43);
            this.btCancelEnrollCurrentEnrollee.TabIndex = 4;
            this.btCancelEnrollCurrentEnrollee.Text = "Отменить зачисление";
            this.btCancelEnrollCurrentEnrollee.UseVisualStyleBackColor = true;
            this.btCancelEnrollCurrentEnrollee.Click += new System.EventHandler(this.btCancelEnrollCurrentEnrollee_Click);
            // 
            // btEnrollCurrentEnrollee
            // 
            this.btEnrollCurrentEnrollee.Location = new System.Drawing.Point(132, 6);
            this.btEnrollCurrentEnrollee.Name = "btEnrollCurrentEnrollee";
            this.btEnrollCurrentEnrollee.Size = new System.Drawing.Size(120, 43);
            this.btEnrollCurrentEnrollee.TabIndex = 5;
            this.btEnrollCurrentEnrollee.Text = "Зачислить абитуриента";
            this.btEnrollCurrentEnrollee.UseVisualStyleBackColor = true;
            this.btEnrollCurrentEnrollee.Click += new System.EventHandler(this.btEnrollCurrentEnrollee_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDecree);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(3, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 52);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Зачислить по приказу";
            // 
            // cbDecree
            // 
            this.cbDecree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDecree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDecree.FormattingEnabled = true;
            this.cbDecree.Location = new System.Drawing.Point(3, 19);
            this.cbDecree.Name = "cbDecree";
            this.cbDecree.Size = new System.Drawing.Size(194, 23);
            this.cbDecree.TabIndex = 1;
            this.cbDecree.SelectedValueChanged += new System.EventHandler(this.cbDecree_SelectedValueChanged);
            // 
            // frmEnroll
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gbEnrollee);
            this.Controls.Add(this.gbEnrollPanel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEnroll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Зачисление абитуриентов";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbSpeciality.ResumeLayout(false);
            this.gbFormOfStudy.ResumeLayout(false);
            this.gbFaculty.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbEnrollee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EnrolleeGrid)).EndInit();
            this.gbEnrollPanel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbSpeciality;
        private System.Windows.Forms.ComboBox cbSpeciality;
        private System.Windows.Forms.GroupBox gbFormOfStudy;
        private System.Windows.Forms.ComboBox cbFormOfStudy;
        private System.Windows.Forms.GroupBox gbFaculty;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btReport;
        private System.Windows.Forms.GroupBox gbEnrollee;
        private System.Windows.Forms.DataGridView EnrolleeGrid;
        private System.Windows.Forms.GroupBox gbEnrollPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbPriority;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbDecree;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btCancelEnrollCurrentEnrollee;
        private System.Windows.Forms.Button btEnrollCurrentEnrollee;
    }
}
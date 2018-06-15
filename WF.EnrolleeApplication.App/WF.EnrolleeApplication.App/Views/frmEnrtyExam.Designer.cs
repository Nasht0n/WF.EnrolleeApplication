namespace WF.EnrolleeApplication.App.Views
{
    partial class frmEnrtyExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnrtyExam));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbDiscipline = new System.Windows.Forms.GroupBox();
            this.cbDiscipline = new System.Windows.Forms.ComboBox();
            this.gbSpeciality = new System.Windows.Forms.GroupBox();
            this.cbSpeciality = new System.Windows.Forms.ComboBox();
            this.gbFormOfStudy = new System.Windows.Forms.GroupBox();
            this.cbFormOfStudy = new System.Windows.Forms.ComboBox();
            this.gbFaculty = new System.Windows.Forms.GroupBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btSave = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.gbEnrollee = new System.Windows.Forms.GroupBox();
            this.EnrolleeGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbDiscipline.SuspendLayout();
            this.gbSpeciality.SuspendLayout();
            this.gbFormOfStudy.SuspendLayout();
            this.gbFaculty.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbEnrollee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnrolleeGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.gbDiscipline, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gbSpeciality, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbFormOfStudy, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbFaculty, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 147);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbDiscipline
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbDiscipline, 2);
            this.gbDiscipline.Controls.Add(this.cbDiscipline);
            this.gbDiscipline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDiscipline.Location = new System.Drawing.Point(3, 101);
            this.gbDiscipline.Name = "gbDiscipline";
            this.gbDiscipline.Size = new System.Drawing.Size(778, 43);
            this.gbDiscipline.TabIndex = 5;
            this.gbDiscipline.TabStop = false;
            this.gbDiscipline.Text = "Укажите дисциплину";
            // 
            // cbDiscipline
            // 
            this.cbDiscipline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiscipline.FormattingEnabled = true;
            this.cbDiscipline.Location = new System.Drawing.Point(3, 19);
            this.cbDiscipline.Name = "cbDiscipline";
            this.cbDiscipline.Size = new System.Drawing.Size(772, 23);
            this.cbDiscipline.TabIndex = 0;
            this.cbDiscipline.SelectedValueChanged += new System.EventHandler(this.cbDiscipline_SelectedValueChanged);
            // 
            // gbSpeciality
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbSpeciality, 2);
            this.gbSpeciality.Controls.Add(this.cbSpeciality);
            this.gbSpeciality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSpeciality.Location = new System.Drawing.Point(3, 52);
            this.gbSpeciality.Name = "gbSpeciality";
            this.gbSpeciality.Size = new System.Drawing.Size(778, 43);
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
            this.cbSpeciality.Size = new System.Drawing.Size(772, 23);
            this.cbSpeciality.TabIndex = 0;
            this.cbSpeciality.SelectedValueChanged += new System.EventHandler(this.cbSpeciality_SelectedValueChanged);
            // 
            // gbFormOfStudy
            // 
            this.gbFormOfStudy.Controls.Add(this.cbFormOfStudy);
            this.gbFormOfStudy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFormOfStudy.Location = new System.Drawing.Point(277, 3);
            this.gbFormOfStudy.Name = "gbFormOfStudy";
            this.gbFormOfStudy.Size = new System.Drawing.Size(504, 43);
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
            this.cbFormOfStudy.Size = new System.Drawing.Size(498, 23);
            this.cbFormOfStudy.TabIndex = 0;
            this.cbFormOfStudy.SelectedValueChanged += new System.EventHandler(this.cbFormOfStudy_SelectedValueChanged);
            // 
            // gbFaculty
            // 
            this.gbFaculty.Controls.Add(this.cbFaculty);
            this.gbFaculty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFaculty.Location = new System.Drawing.Point(3, 3);
            this.gbFaculty.Name = "gbFaculty";
            this.gbFaculty.Size = new System.Drawing.Size(268, 43);
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
            this.cbFaculty.Size = new System.Drawing.Size(262, 23);
            this.cbFaculty.TabIndex = 0;
            this.cbFaculty.SelectedValueChanged += new System.EventHandler(this.cbFaculty_SelectedValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btSave);
            this.flowLayoutPanel1.Controls.Add(this.btClear);
            this.flowLayoutPanel1.Controls.Add(this.btCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 525);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(784, 36);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(531, 3);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(250, 28);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "Сохранить изменения";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(325, 3);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(200, 28);
            this.btClear.TabIndex = 1;
            this.btClear.Text = "Очистить столбец оценок";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(169, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(150, 28);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gbEnrollee
            // 
            this.gbEnrollee.Controls.Add(this.EnrolleeGrid);
            this.gbEnrollee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEnrollee.Location = new System.Drawing.Point(0, 147);
            this.gbEnrollee.Name = "gbEnrollee";
            this.gbEnrollee.Size = new System.Drawing.Size(784, 378);
            this.gbEnrollee.TabIndex = 2;
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
            this.EnrolleeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EnrolleeGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.EnrolleeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnrolleeGrid.Location = new System.Drawing.Point(3, 19);
            this.EnrolleeGrid.MultiSelect = false;
            this.EnrolleeGrid.Name = "EnrolleeGrid";
            this.EnrolleeGrid.RowHeadersVisible = false;
            this.EnrolleeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.EnrolleeGrid.Size = new System.Drawing.Size(778, 356);
            this.EnrolleeGrid.TabIndex = 0;
            // 
            // frmEnrtyExam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.gbEnrollee);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEnrtyExam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вступительные испытания. Ввод оценок";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbDiscipline.ResumeLayout(false);
            this.gbSpeciality.ResumeLayout(false);
            this.gbFormOfStudy.ResumeLayout(false);
            this.gbFaculty.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbEnrollee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EnrolleeGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbFaculty;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.GroupBox gbSpeciality;
        private System.Windows.Forms.ComboBox cbSpeciality;
        private System.Windows.Forms.GroupBox gbFormOfStudy;
        private System.Windows.Forms.ComboBox cbFormOfStudy;
        private System.Windows.Forms.GroupBox gbDiscipline;
        private System.Windows.Forms.ComboBox cbDiscipline;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox gbEnrollee;
        private System.Windows.Forms.DataGridView EnrolleeGrid;
    }
}
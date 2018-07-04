namespace WF.EnrolleeApplication.App.Views
{
    partial class frmChangeDiscipline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeDiscipline));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDiscipline = new System.Windows.Forms.ComboBox();
            this.IsChange = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btSave);
            this.flowLayoutPanel1.Controls.Add(this.btCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 51);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(578, 32);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(375, 3);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(200, 27);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "Сохранить";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(269, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 27);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IsChange);
            this.groupBox1.Controls.Add(this.cbDiscipline);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите дисциплину";
            // 
            // cbDiscipline
            // 
            this.cbDiscipline.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiscipline.FormattingEnabled = true;
            this.cbDiscipline.Location = new System.Drawing.Point(161, 19);
            this.cbDiscipline.Name = "cbDiscipline";
            this.cbDiscipline.Size = new System.Drawing.Size(414, 23);
            this.cbDiscipline.TabIndex = 0;
            this.cbDiscipline.SelectedValueChanged += new System.EventHandler(this.cbDiscipline_SelectedValueChanged);
            // 
            // IsChange
            // 
            this.IsChange.AutoSize = true;
            this.IsChange.Location = new System.Drawing.Point(19, 21);
            this.IsChange.Name = "IsChange";
            this.IsChange.Size = new System.Drawing.Size(136, 19);
            this.IsChange.TabIndex = 1;
            this.IsChange.Text = "Выполнить замену?";
            this.IsChange.UseVisualStyleBackColor = true;
            this.IsChange.CheckedChanged += new System.EventHandler(this.IsChange_CheckedChanged);
            // 
            // frmChangeDiscipline
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 86);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangeDiscipline";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Замена дисциплины";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDiscipline;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.CheckBox IsChange;
    }
}
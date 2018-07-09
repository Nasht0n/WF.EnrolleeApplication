namespace WF.EnrolleeApplication.App.Views.DictionaryView
{
    partial class frmFaculty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFaculty));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.NewRecord = new System.Windows.Forms.ToolStripButton();
            this.EditRecord = new System.Windows.Forms.ToolStripButton();
            this.DeleteRecord = new System.Windows.Forms.ToolStripButton();
            this.FacultyGrid = new System.Windows.Forms.DataGridView();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FacultyGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewRecord,
            this.EditRecord,
            this.DeleteRecord});
            this.toolStrip.Location = new System.Drawing.Point(3, 3);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(778, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // NewRecord
            // 
            this.NewRecord.Image = ((System.Drawing.Image)(resources.GetObject("NewRecord.Image")));
            this.NewRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewRecord.Name = "NewRecord";
            this.NewRecord.Size = new System.Drawing.Size(101, 22);
            this.NewRecord.Text = "Новая запись";
            // 
            // EditRecord
            // 
            this.EditRecord.Image = ((System.Drawing.Image)(resources.GetObject("EditRecord.Image")));
            this.EditRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditRecord.Name = "EditRecord";
            this.EditRecord.Size = new System.Drawing.Size(147, 22);
            this.EditRecord.Text = "Редактировать запись";
            // 
            // DeleteRecord
            // 
            this.DeleteRecord.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRecord.Image")));
            this.DeleteRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRecord.Name = "DeleteRecord";
            this.DeleteRecord.Size = new System.Drawing.Size(111, 22);
            this.DeleteRecord.Text = "Удалить запись";
            // 
            // FacultyGrid
            // 
            this.FacultyGrid.AllowUserToAddRows = false;
            this.FacultyGrid.AllowUserToDeleteRows = false;
            this.FacultyGrid.AllowUserToResizeColumns = false;
            this.FacultyGrid.AllowUserToResizeRows = false;
            this.FacultyGrid.BackgroundColor = System.Drawing.Color.White;
            this.FacultyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FacultyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FacultyGrid.Location = new System.Drawing.Point(3, 28);
            this.FacultyGrid.Name = "FacultyGrid";
            this.FacultyGrid.ReadOnly = true;
            this.FacultyGrid.Size = new System.Drawing.Size(778, 530);
            this.FacultyGrid.TabIndex = 1;
            // 
            // frmFaculty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.FacultyGrid);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFaculty";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочники университета. Справочник \"Факультеты\"";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FacultyGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton NewRecord;
        private System.Windows.Forms.ToolStripButton EditRecord;
        private System.Windows.Forms.ToolStripButton DeleteRecord;
        private System.Windows.Forms.DataGridView FacultyGrid;
    }
}
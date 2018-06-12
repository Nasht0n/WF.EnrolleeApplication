using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.App.Views
{
    public partial class frmReceipt : Form
    {
        public string DocumentOfStudy;
        public string DocumentOfDiscount;
        public string DocumentOther;
        public frmReceipt(Enrollee enrollee)
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            DocumentOfStudy = tbDocumentOfStudy.Text;
            DocumentOfDiscount = tbDocumentForDiscount.Text;
            DocumentOther = tbOtherDocument.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

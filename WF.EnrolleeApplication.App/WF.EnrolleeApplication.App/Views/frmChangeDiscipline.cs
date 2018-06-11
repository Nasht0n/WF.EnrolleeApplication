using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Views
{
    public partial class frmChangeDiscipline : Form
    {
        public Discipline discipline;
        private DisciplineService disciplineService;
        private BasisForAssessing basisForAssessing;
        private BasisForAssessingService basisForAssessingService;

        public frmChangeDiscipline()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            disciplineService = new DisciplineService(connectionString);
            basisForAssessingService = new BasisForAssessingService(connectionString);
            InitializeDisciplineComboBox();
        }

        private void InitializeDisciplineComboBox()
        {
            cbDiscipline.SelectedValueChanged -= cbDiscipline_SelectedValueChanged;
            basisForAssessing = basisForAssessingService.GetBasisForAssessing(3); // Тестирование
            if (basisForAssessing != null)
            {
                var disciplines = disciplineService.GetDisciplines(basisForAssessing, false).OrderBy(d => d.Name).ToList();
                cbDiscipline.DataSource = disciplines;
                cbDiscipline.DisplayMember = "Name";
                cbDiscipline.ValueMember = "DisciplineId";
                if (disciplines.Count != 0) discipline = disciplines[0];
            }
            cbDiscipline.SelectedValueChanged += cbDiscipline_SelectedValueChanged;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cbDiscipline_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbDiscipline.SelectedValue != null)
            {
                int id = (int)cbDiscipline.SelectedValue;
                discipline = disciplineService.GetDiscipline(id);
            }
        }
    }
}

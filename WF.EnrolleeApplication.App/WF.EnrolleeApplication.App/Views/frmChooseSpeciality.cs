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
    public partial class frmChooseSpeciality : Form
    {
        private Faculty faculty;
        private FormOfStudy formOfStudy;
        public Speciality speciality;

        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;

        public frmChooseSpeciality()
        {
            InitializeComponent();
            InitializeDataAccessServices();
            InitializeComboBoxes();
        }

        private void InitializeDataAccessServices()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
        }

        private void InitializeComboBoxes()
        {
            InitializeFacultyComboBox();
            InitializeFormOfStudyComboBox();
        }

        private void InitializeFormOfStudyComboBox()
        {
            cbFormOfStudy.SelectedValueChanged -= cbFormOfStudy_SelectedValueChanged;
            var formsOfStudies = formOfStudyService.GetFormOfStudies();
            cbFormOfStudy.DataSource = formsOfStudies;
            cbFormOfStudy.DisplayMember = "Fullname";
            cbFormOfStudy.ValueMember = "FormOfStudyId";
            if (formsOfStudies.Count != 0)
            {
                formOfStudy = formsOfStudies[0];
                InitializeSpecialityComboBox();
            }
            cbFormOfStudy.SelectedValueChanged += cbFormOfStudy_SelectedValueChanged;
        }

        private void InitializeFacultyComboBox()
        {
            cbFaculty.SelectedValueChanged -= cbFaculty_SelectedValueChanged;
            var faculties = facultyService.GetFaculties();
            cbFaculty.DataSource = faculties;
            cbFaculty.DisplayMember = "Shortname";
            cbFaculty.ValueMember = "FacultyId";
            if (faculties.Count != 0)
            {
                faculty = faculties[0];
                InitializeSpecialityComboBox();
            }
            cbFaculty.SelectedValueChanged += cbFaculty_SelectedValueChanged;
        }

        private void InitializeSpecialityComboBox()
        {
            if (faculty != null && formOfStudy != null)
            {
                cbSpeciality.SelectedValueChanged -= cbSpeciality_SelectedValueChanged;
                var specialities = specialityService.GetSpecialities(faculty, formOfStudy);
                cbSpeciality.DataSource = specialities;
                cbSpeciality.DisplayMember = "Fullname";
                cbSpeciality.ValueMember = "SpecialityId";
                if (specialities.Count != 0) speciality = specialities[0];
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }

        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFaculty.SelectedValue != null)
            {
                int id = (int)cbFaculty.SelectedValue;
                faculty = facultyService.GetFaculty(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFormOfStudy.SelectedValue != null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSpeciality.SelectedValue != null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                speciality = specialityService.GetSpeciality(id);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if(speciality != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

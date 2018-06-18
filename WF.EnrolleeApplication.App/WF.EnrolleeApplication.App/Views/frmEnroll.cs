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
    public partial class frmEnroll : Form
    {
        private Faculty faculty;
        private FormOfStudy formOfStudy;
        private Speciality speciality;

        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private PriorityOfSpecialityService priorityOfSpecialityService;
        private DecreeService decreeService;

        public frmEnroll()
        {
            InitializeComponent();
            InitializeDataAccessServices();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            InitializeFacultyComboBox();
            InitializeFormOfStudyComboBox();
            // Grid ComboBox
            //InitializePriorityGridComboBox();
            InitializeDecreeGridComboBox();
        }

        private void InitializePriorityGridComboBox()
        {
            //var priorities = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee);
        }

        private void InitializeDecreeGridComboBox()
        {
            var decrees = decreeService.GetDecrees();
            ColumnDecree.DataSource = decrees;
            ColumnDecree.DisplayMember = "DecreeNumber";
            ColumnDecree.ValueMember = "DecreeId";
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
                if (specialities.Count != 0)
                {
                    speciality = specialities[0];

                }
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }

        private void InitializeDataAccessServices()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            priorityOfSpecialityService = new PriorityOfSpecialityService(connectionString);
            decreeService = new DecreeService(connectionString);
        }

        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFaculty.SelectedValue !=null)
            {
                int id = (int)cbFaculty.SelectedValue;
                faculty = facultyService.GetFaculty(id);
            }
        }

        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFormOfStudy.SelectedValue!=null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
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
    }
}

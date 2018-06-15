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
    public partial class frmEnrtyExam : Form
    {
        private DataTable enrolleeTable;

        private Faculty faculty;
        private FormOfStudy formOfStudy;
        private Speciality speciality;
        private Discipline discipline;

        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private DisciplineService disciplineService;
        private AssessmentService assessmentService;
        private ExamSchemaService examSchemaService;
        private ViewService viewService;

        public frmEnrtyExam()
        {
            InitializeComponent();
            enrolleeTable = CreateStructureTable();
            EnrolleeGrid.DataSource = enrolleeTable;
            InitializeDataAccessServices();
            InitializeComboBox();
            
            SetGridStyle();
        }

        private DataTable CreateStructureTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn("КодОценки",typeof(string)));
            result.Columns.Add(new DataColumn("№ Л/Д", typeof(string)));
            result.Columns.Add(new DataColumn("Фамилия", typeof(string)));
            result.Columns.Add(new DataColumn("Имя", typeof(string)));
            result.Columns.Add(new DataColumn("Оценка", typeof(string)));
            return result;
        }

        private void InitializeComboBox()
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
                if (specialities.Count != 0)
                {
                    speciality = specialities[0];
                    InitializeDisciplineComboBox();
                    
                }
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }

        private void InitializeEnrolleeGrid()
        {
            enrolleeTable.Clear();
            var assessments = viewService.GetAssessments(discipline);
            foreach (var assessment in assessments)
                if (string.IsNullOrWhiteSpace(assessment.FormOfStudy))
                    enrolleeTable.Rows.Add(assessment.AssessmentId, $"{assessment.Speciality.Trim()}-{assessment.NumberOfDeal}", assessment.RuSurname, assessment.RuName, assessment.Estimation);
                else enrolleeTable.Rows.Add(assessment.AssessmentId, $"{assessment.Speciality.Trim()}{assessment.FormOfStudy.Trim()}-{assessment.NumberOfDeal}", assessment.RuSurname, assessment.RuName, assessment.Estimation);

        }

        private void SetGridStyle()
        {
            EnrolleeGrid.Columns[0].Visible = false;
            EnrolleeGrid.Columns[1].FillWeight = 25;
            EnrolleeGrid.Columns[1].ReadOnly = true;
            EnrolleeGrid.Columns[2].FillWeight = 25;
            EnrolleeGrid.Columns[2].ReadOnly = true;
            EnrolleeGrid.Columns[3].FillWeight = 25;
            EnrolleeGrid.Columns[3].ReadOnly = true;
            EnrolleeGrid.Columns[4].FillWeight = 25;
        }

        private void InitializeDisciplineComboBox()
        {
            cbDiscipline.SelectedValueChanged -= cbDiscipline_SelectedValueChanged;
            var exams = examSchemaService.GetExamSchemas(speciality);
            List<Discipline> disciplines = new List<Discipline>();
            foreach (var exam in exams)
                if (exam.Discipline.BasisForAssessingId == 2) disciplines.Add(exam.Discipline);
            cbDiscipline.DataSource = disciplines;
            cbDiscipline.DisplayMember = "Name";
            cbDiscipline.ValueMember = "DisciplineId";
            if (disciplines.Count != 0)
            {
                discipline = disciplines[0];
                InitializeEnrolleeGrid();
            }
            else
            {
                discipline = null;
                enrolleeTable.Clear();
            }
            
            cbDiscipline.SelectedValueChanged += cbDiscipline_SelectedValueChanged;
        }

        private void InitializeDataAccessServices()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            assessmentService = new AssessmentService(connectionString);
            examSchemaService = new ExamSchemaService(connectionString);
            disciplineService = new DisciplineService(connectionString);
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            viewService = new ViewService(connectionString);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFaculty.SelectedValue != null)
            {
                int id = (int)cbFaculty.SelectedValue;
                faculty = facultyService.GetFaculty(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFormOfStudy.SelectedValue !=null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbSpeciality.SelectedValue!=null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                speciality = specialityService.GetSpeciality(id);
                InitializeDisciplineComboBox();
            }
        }

        private void cbDiscipline_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSpeciality.SelectedValue != null)
            {
                int id = (int)cbDiscipline.SelectedValue;
                discipline = disciplineService.GetDiscipline(id);
                InitializeEnrolleeGrid();
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in EnrolleeGrid.Rows)
            {
                row.Cells[4].Value = "0";
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Сохранить список оценок?", "Вступительные испытания", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in EnrolleeGrid.Rows)
                {
                    Assessment assessment = assessmentService.GetAssessment(Int32.Parse(row.Cells[0].Value.ToString()));
                    assessment.Estimation = Int32.Parse(row.Cells[4].Value.ToString());
                    assessmentService.UpdateAssessment(assessment);
                }
            }
        }
    }
}

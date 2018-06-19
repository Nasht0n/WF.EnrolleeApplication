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
using WF.EnrolleeApplication.App.Services;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Views
{
    public partial class frmEnroll : Form
    {
        private DataTable enrolleeTable;
        private string connectionString;
        private Faculty faculty;
        private FormOfStudy formOfStudy;
        private Speciality speciality;
        private Enrollee enrollee;
        private Decree decree;
        private Speciality priority;

        private EnrolleeService enrolleeService;
        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private PriorityOfSpecialityService priorityOfSpecialityService;
        private DecreeService decreeService;
        private ViewService viewService;

        public frmEnroll()
        {
            InitializeComponent();
            enrolleeTable = CreateStructure();
            EnrolleeGrid.DataSource = enrolleeTable;
            InitializeDataAccessServices();
            InitializeComboBox();
            SetGridStyle();
        }

        private void SetGridStyle()
        {
            EnrolleeGrid.Columns[0].Visible = false;
            EnrolleeGrid.Columns[1].FillWeight = 15;
            EnrolleeGrid.Columns[2].FillWeight = 15;
            EnrolleeGrid.Columns[3].FillWeight = 40;
            EnrolleeGrid.Columns[4].FillWeight = 15;
            EnrolleeGrid.Columns[5].FillWeight = 15;
        }

        private DataTable CreateStructure()
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn("КодАбитуриента",typeof(int)));
            result.Columns.Add(new DataColumn("№ личного дела",typeof(string)));
            result.Columns.Add(new DataColumn("Вид конкурса", typeof(string)));
            result.Columns.Add(new DataColumn("ФИО абитуриента", typeof(string)));
            result.Columns.Add(new DataColumn("Количество баллов", typeof(int)));
            result.Columns.Add(new DataColumn("Зачислен?",typeof(bool)));
            return result;
        }

        private void InitializeDataAccessServices()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            priorityOfSpecialityService = new PriorityOfSpecialityService(connectionString);
            decreeService = new DecreeService(connectionString);
            enrolleeService = new EnrolleeService(connectionString);
            viewService = new ViewService(connectionString);
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
                    InitializeEnrolleeDataGrid();
                }
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }

        private void InitializeEnrolleeDataGrid()
        {
          //  EnrolleeGrid.SelectionChanged -= EnrolleeGrid_SelectionChanged;
            enrolleeTable.Clear();
            var enrollees = enrolleeService.GetEnrollees(speciality).OrderByDescending(e => e.ReasonForAddmission.ContestId)
                    .ThenByDescending(e => e.Assessment.Sum(a => a.Estimation))
                    .ToList();
            foreach (var enrollee in enrollees)
            {
                string numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                string fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} {enrollee.RuPatronymic.Trim()}";
                int sum = enrollee.Assessment.Sum(a=>a.Estimation).Value;
                bool enroll = false;
                if (enrollee.DecreeId.HasValue) enroll = true;
                else enroll = false;
                enrolleeTable.Rows.Add(enrollee.EnrolleeId, numberOfDeal, enrollee.ReasonForAddmission.Contest.Name.Trim(), fullname, sum, enroll);
            }
          //  EnrolleeGrid.SelectionChanged += EnrolleeGrid_SelectionChanged;
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
                InitializeEnrolleeDataGrid();
            }
        }

        private void EnrolleeGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (EnrolleeGrid.SelectedCells.Count != 0)
            {
                int index = EnrolleeGrid.SelectedCells[0].RowIndex;
                DataGridViewRow row = EnrolleeGrid.Rows[index];
                int id = Int32.Parse(row.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                InitializeEnrollPanel();
            }
        }

        private void InitializeEnrollPanel()
        {
            if (enrollee != null)
            {
                string fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} {enrollee.RuPatronymic.Trim()}";
                gbEnrollPanel.Text = fullname;
                InitializeDecreeComboBox();
                InitializePriorityComboBox(enrollee);
            }
        }

        private void InitializePriorityComboBox(Enrollee enrollee)
        {
            cbPriority.SelectedValueChanged -= cbPriority_SelectedValueChanged;
            var priorities = viewService.GetPriorities(enrollee);
            cbPriority.DataSource = priorities;
            cbPriority.DisplayMember = "Fullname";
            cbPriority.ValueMember = "SpecialityId";
            if (priorities.Count != 0)
            {
                int id = (int)priorities[0].SpecialityId;
                priority = specialityService.GetSpeciality(id);
            }
            cbPriority.SelectedValueChanged += cbPriority_SelectedValueChanged;
        }

        private void InitializeDecreeComboBox()
        {
            cbDecree.SelectedValueChanged -= cbDecree_SelectedValueChanged;
            var decrees = decreeService.GetDecrees();
            cbDecree.DataSource = decrees;
            cbDecree.DisplayMember = "DecreeNumber";
            cbDecree.ValueMember = "DecreeId";
            if(decrees.Count!= 0)
            {
                decree = decrees[0];
            }
            cbDecree.SelectedValueChanged += cbDecree_SelectedValueChanged;
        }

        private void cbDecree_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbDecree.SelectedValue!=null)
            {
                int id = (int)cbDecree.SelectedValue;
                decree = decreeService.GetDecree(id);
            }
        }

        private void cbPriority_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbPriority.SelectedValue != null)
            {
                int id = (int)cbPriority.SelectedValue;
                priority = specialityService.GetSpeciality(id);
            }
        }

        private int GetNumberOfDeal(Speciality speciality)
        {
            int result = 0;
            var enrollees = enrolleeService.GetEnrollees(speciality).OrderBy(e => e.NumberOfDeal).ToList();
            if (enrollees.Count == 0)
            {
                result = 1;
            }
            else
            {
                result = enrollees[enrollees.Count - 1].NumberOfDeal + 1;
            }
            return result;
        }

        private void btEnrollCurrentEnrollee_Click(object sender, EventArgs e)
        {
            string fullname = $"";
            if(string.IsNullOrWhiteSpace(enrollee.RuPatronymic)) fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}.";
            else fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}. {enrollee.RuPatronymic[0]}.";
            if (MessageBox.Show(this,$"Произвести зачисление абитуриента: {fullname} в число студентов?","Зачисление абитуриентов",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                enrollee.DecreeId = decree.DecreeId;
                enrollee.BeforeEnrollNumberOfDeal = enrollee.NumberOfDeal;
                enrollee.BeforeEnrollSpecialityId = enrollee.SpecialityId;
                enrollee.StateTypeId = 3;
                enrollee.StateDateChange = DateTime.Now;
                enrollee.SpecialityId = priority.SpecialityId;
                enrollee.NumberOfDeal = GetNumberOfDeal(priority);
                enrolleeService.UpdateEnrollee(enrollee);
                InitializeEnrolleeDataGrid();
            }
        }

        private void btCancelEnrollCurrentEnrollee_Click(object sender, EventArgs e)
        {
            if (enrollee != null)
            {
                string fullname = $"";
                if (string.IsNullOrWhiteSpace(enrollee.RuPatronymic)) fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}.";
                else fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}. {enrollee.RuPatronymic[0]}.";
                if (MessageBox.Show(this, $"Отменить зачисление абитуриента: {fullname}?", "Зачисление абитуриентов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CancelEnroll(enrollee);
                    InitializeEnrolleeDataGrid();
                }               
            }
        }

        private void CancelEnroll(Enrollee enrollee)
        {
            if (enrollee.DecreeId.HasValue)
            {
                enrollee.DecreeId = null;
                if (enrollee.BeforeEnrollSpecialityId.HasValue) enrollee.SpecialityId = enrollee.BeforeEnrollSpecialityId.Value;
                if (enrollee.BeforeEnrollNumberOfDeal.HasValue) enrollee.NumberOfDeal = enrollee.BeforeEnrollNumberOfDeal.Value;
                enrollee.StateTypeId = 1;
                enrollee.StateDateChange = DateTime.Now;
                enrollee.BeforeEnrollNumberOfDeal = null;
                enrollee.BeforeEnrollSpecialityId = null;
                enrolleeService.UpdateEnrollee(enrollee);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btClear_Click(object sender, EventArgs ea)
        {
            var enrollees = enrolleeService.GetEnrollees(speciality).Where(e=>e.StateTypeId!=2).ToList();
            if(enrollees.Count!=0)
            {
                if (MessageBox.Show(this, $"Отменить зачисление абитуриентов специальности: {speciality.Fullname.Trim()}?", "Зачисление абитуриентов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var enrollee in enrollees)
                        CancelEnroll(enrollee);
                    InitializeEnrolleeDataGrid();
                }
            }
        }

        private void btReport_Click(object sender, EventArgs ea)
        {
            var enrollees = enrolleeService.GetEnrollees(speciality).Where(e => e.StateTypeId != 2).ToList();
            ReportManager.ConnectionString = connectionString;
            ReportManager.PrintExtract(enrollees);
        }
    }
}

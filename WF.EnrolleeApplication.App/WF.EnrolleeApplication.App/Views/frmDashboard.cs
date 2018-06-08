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
    public partial class frmDashboard : Form
    {
        private bool ToolBoxVisible;
        private bool SearchToolBoxVisible;
        private bool FilterToolBoxVisible;
        private bool StatusStripVisible;
        private bool SearchMode = false;

        private DataTable enrolleeTable;

        private Enrollee enrollee;
        private Employee activeEmployee;
        private Faculty currentFaculty;
        private FormOfStudy currentFormOfStudy;
        private Speciality currentSpeciality;

        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private EnrolleeService enrolleeService;
        private ViewService viewService;

        public frmDashboard(Employee employee)
        {
            InitializeComponent();
            this.activeEmployee = employee;
            // Инициализируем сервисы доступа к данным
            InitializeDataAccessServices();
            // Заполняем данными выпадающие списки для поиска 
            InitializeSearchComboBoxes();
            // Инициализируем строку состояния данными
            InitializeStatusStrip();
            // Параметры программы
            LoadUsersSettings();
            // Заполняем таблицу абитуриентов
            enrolleeTable = CreateStructureTable();
            EnrolleeGrid.DataSource = enrolleeTable;
            InitializeEnrolleeGrid(SearchMode);
            SetStyleEnrolleeGrid(EnrolleeGrid);
        }

        private void SetStyleEnrolleeGrid(DataGridView enrolleeGrid)
        {
            enrolleeGrid.Columns[0].Visible = false;
            enrolleeGrid.Columns[1].Visible = false;
            enrolleeGrid.Columns[2].Visible = false;
            enrolleeGrid.Columns[3].Visible = false;
            enrolleeGrid.Columns[4].Visible = false;
            enrolleeGrid.Columns[5].Visible = false;
            enrolleeGrid.Columns[6].Visible = false;
            enrolleeGrid.Columns[7].Visible = true;
            enrolleeGrid.Columns[8].Visible = true;
            enrolleeGrid.Columns[9].Visible = true;
            enrolleeGrid.Columns[10].Visible = true;
            enrolleeGrid.Columns[11].Visible = true;
            enrolleeGrid.Columns[12].Visible = false;
            enrolleeGrid.Columns[13].Visible = true;
            enrolleeGrid.Columns[14].Visible = true;
            enrolleeGrid.Columns[15].Visible = true;
        }

        private DataTable CreateStructureTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn("КодАбитуриента",typeof(int)));
            result.Columns.Add(new DataColumn("КодСпециальности", typeof(int)));
            result.Columns.Add(new DataColumn("КодКонкурса", typeof(int)));
            result.Columns.Add(new DataColumn("КодОснования", typeof(int)));
            result.Columns.Add(new DataColumn("КодФинансирования", typeof(int)));
            result.Columns.Add(new DataColumn("КодСтатуса", typeof(int)));
            result.Columns.Add(new DataColumn("КодОператора", typeof(int)));
            result.Columns.Add(new DataColumn("Форма обучения",typeof(string)));
            result.Columns.Add(new DataColumn("Специальность", typeof(string)));
            result.Columns.Add(new DataColumn("Фамилия", typeof(string)));
            result.Columns.Add(new DataColumn("Имя", typeof(string)));
            result.Columns.Add(new DataColumn("Конкурс", typeof(string)));
            result.Columns.Add(new DataColumn("Основание зачисления", typeof(string)));
            result.Columns.Add(new DataColumn("Тип финансирования", typeof(string)));
            result.Columns.Add(new DataColumn("№ Л/Д", typeof(string)));
            result.Columns.Add(new DataColumn("Статус", typeof(string)));
            return result;
        }

        private void InitializeEnrolleeGrid(bool searchMode)
        {
            List<EnrolleeView> enrollees;
            if(searchMode)
            {
                enrollees = viewService.GetEnrollees(currentSpeciality); 
            }
            else
            {
                enrollees = viewService.GetEnrollees(activeEmployee);
            }

            enrolleeTable.Clear();
            foreach (var enrollee in enrollees)
            {
                enrolleeTable.Rows.Add(enrollee.EnrolleeId, enrollee.SpecialityId, enrollee.ContestId, enrollee.ReasonForAddmissionId, enrollee.FinanceTypeId, enrollee.StateId, enrollee.EmployeeId, enrollee.FormOfStudy,enrollee.Speciality,enrollee.Surname,enrollee.Name,enrollee.Contest,enrollee.ReasonForAddmission,enrollee.Finance, enrollee.NumberOfDeal,enrollee.Status);
            }

        }

        private void LoadUsersSettings()
        {
            // Получаем значения сохраненные пользователем
            ToolBoxVisible = Properties.Settings.Default.ToolBoxVisible;
            SearchToolBoxVisible = Properties.Settings.Default.SearchToolBoxVisible;
            FilterToolBoxVisible = Properties.Settings.Default.FilterToolBoxVisible;
            StatusStripVisible = Properties.Settings.Default.StatusStripVisible;
            // Устанавливаем значения компонентам
            ToolBox.Visible = ToolBoxVisible;
            SearchToolBox.Visible = SearchToolBoxVisible;
            FilterToolBox.Visible = FilterToolBoxVisible;
            StatusStrip.Visible = StatusStripVisible;
        }

        private void InitializeSearchComboBoxes()
        {
            // Заполняем факультеты
            InitializeFacultyComboBox();
            // Заполняем формы обучения
            InitializeFormOfStudyComboBox();
            // Заполняем специальности
            InitializeSpecialityComboBox();            
        }

        private void InitializeSpecialityComboBox()
        {
            if (currentFaculty != null && currentFormOfStudy != null)
            {
                cbSpeciality.SelectedValueChanged -= cbSpeciality_SelectedValueChanged;
                var specialities = specialityService.GetSpecialities(currentFaculty,currentFormOfStudy);
                cbSpeciality.DataSource = specialities;
                cbSpeciality.DisplayMember = "Fullname";
                cbSpeciality.ValueMember = "SpecialityId";
                if (specialities.Count != 0) currentSpeciality = specialities[0];
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }

        private void InitializeFormOfStudyComboBox()
        {
            cbFormOfStudy.SelectedValueChanged -= cbFormOfStudy_SelectedValueChanged;
            var formsOfStudies = formOfStudyService.GetFormOfStudies();
            cbFormOfStudy.DataSource = formsOfStudies;
            cbFormOfStudy.DisplayMember = "Fullname";
            cbFormOfStudy.ValueMember = "FormOfStudyId";
            if (formsOfStudies.Count != 0) currentFormOfStudy = formsOfStudies[0];
            cbFormOfStudy.SelectedValueChanged += cbFormOfStudy_SelectedValueChanged;
        }

        private void InitializeFacultyComboBox()
        {
            cbFaculty.SelectedValueChanged -= cbFaculty_SelectedValueChanged;
            var faculties = facultyService.GetFaculties();
            cbFaculty.DataSource = faculties;
            cbFaculty.DisplayMember = "Shortname";
            cbFaculty.ValueMember = "FacultyId";
            if (faculties.Count != 0) currentFaculty = faculties[0];
            cbFaculty.SelectedValueChanged += cbFaculty_SelectedValueChanged;
        }

        private void InitializeDataAccessServices()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            enrolleeService = new EnrolleeService(connectionString);
            viewService = new ViewService(connectionString);
        }

        private void InitializeStatusStrip()
        {
            lbEmployee.Text = $"{activeEmployee.EmployeePost.Name}, {activeEmployee.Fullname}";
            var enrollees = enrolleeService.GetEnrollees();
            lbCountRecord.Text = enrollees.Count.ToString();
        }

        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFaculty.SelectedValue != null)
            {
                int id = (int)cbFaculty.SelectedValue;
                currentFaculty = facultyService.GetFaculty(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFormOfStudy.SelectedValue != null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                currentFormOfStudy = formOfStudyService.GetFormOfStudy(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbSpeciality.SelectedValue!=null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                currentSpeciality = specialityService.GetSpeciality(id);
            }
        }

        private void ToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ToolBoxVisible)
            {
                Properties.Settings.Default.ToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
            else
            {
                Properties.Settings.Default.ToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
        }

        private void SearchToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SearchToolBoxVisible)
            {
                Properties.Settings.Default.SearchToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, которые добавил оператор
                SearchMode = false;
            }
            else
            {
                Properties.Settings.Default.SearchToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, по выбранным параметрам специальности
                SearchMode = true;
            }
        }

        private void StatusStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StatusStripVisible)
            {
                Properties.Settings.Default.StatusStripVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
            else
            {
                Properties.Settings.Default.StatusStripVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
        }

        private void FilterToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilterToolBoxVisible)
            {
                Properties.Settings.Default.FilterToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
            else
            {
                Properties.Settings.Default.FilterToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
        }

        private void SearchToolBoxToolStripButton_Click(object sender, EventArgs e)
        {
            if (SearchToolBoxVisible)
            {
                Properties.Settings.Default.SearchToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, которые добавил оператор
                SearchMode = false;
            }
            else
            {
                Properties.Settings.Default.SearchToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, по выбранным параметрам специальности
                SearchMode = true;
            }
        }

        private void ShowNewEnrolleeCard(object sender, EventArgs e)
        {
            frmEnrolleeCard enrolleeCard = new frmEnrolleeCard();
            DialogResult enrolleeCardResult = enrolleeCard.ShowDialog();
            if(enrolleeCardResult == DialogResult.OK)
            {

            }
        }
    }
}

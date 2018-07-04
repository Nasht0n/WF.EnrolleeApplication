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
    public partial class frmDashboard : Form
    {
        private string connectionString;

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

        private AssessmentService assessmentService;
        private PriorityOfSpecialityService priorityOfSpecialityService;
        private AtributeForEnrolleeService atributeForEnrolleeService;
        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private EnrolleeService enrolleeService;
        private ViewService viewService;
        private ExamSchemaService examSchemaService;

        public frmDashboard(Employee employee)
        {
            InitializeComponent();
            this.activeEmployee = employee;
            enrolleeTable = CreateStructureTable();
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
            enrolleeGrid.Columns[12].Visible = true;
            enrolleeGrid.Columns[13].Visible = false;
            enrolleeGrid.Columns[14].Visible = true;
            enrolleeGrid.Columns[15].Visible = true;

            enrolleeGrid.Columns[0].FillWeight = 5;
            enrolleeGrid.Columns[1].FillWeight = 5;
            enrolleeGrid.Columns[2].FillWeight = 5;
            enrolleeGrid.Columns[3].FillWeight = 5;
            enrolleeGrid.Columns[4].FillWeight = 5;
            enrolleeGrid.Columns[5].FillWeight = 5;
            enrolleeGrid.Columns[6].FillWeight = 5;
            enrolleeGrid.Columns[7].FillWeight = 10;
            enrolleeGrid.Columns[8].FillWeight = 10;
            enrolleeGrid.Columns[9].FillWeight = 30;
            enrolleeGrid.Columns[10].FillWeight = 10;
            enrolleeGrid.Columns[11].FillWeight = 10;
            enrolleeGrid.Columns[12].FillWeight = 10;
            enrolleeGrid.Columns[13].FillWeight = 5;
            enrolleeGrid.Columns[14].FillWeight = 10;
            enrolleeGrid.Columns[15].FillWeight = 10;

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

            result.Columns.Add(new DataColumn("№ Л/Д", typeof(string)));
            result.Columns.Add(new DataColumn("Форма обучения",typeof(string)));
            result.Columns.Add(new DataColumn("Специальность", typeof(string)));
            result.Columns.Add(new DataColumn("Фамилия", typeof(string)));
            result.Columns.Add(new DataColumn("Имя", typeof(string)));
            result.Columns.Add(new DataColumn("Конкурс", typeof(string)));
            result.Columns.Add(new DataColumn("Основание зачисления", typeof(string)));
            result.Columns.Add(new DataColumn("Тип финансирования", typeof(string)));            
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
                string numberOfDeal = $"";
                if (string.IsNullOrWhiteSpace(enrollee.FormOfStudyShortname)) numberOfDeal = $"{enrollee.SpecialityShortname}-{enrollee.NumberOfDeal}";
                else numberOfDeal = $"{enrollee.SpecialityShortname}{enrollee.FormOfStudyShortname}-{enrollee.NumberOfDeal}";

                enrolleeTable.Rows.Add(enrollee.EnrolleeId, enrollee.SpecialityId, enrollee.ContestId, enrollee.ReasonForAddmissionId, enrollee.FinanceTypeId, enrollee.StateId, enrollee.EmployeeId, numberOfDeal, enrollee.FormOfStudy,enrollee.Speciality,enrollee.Surname,enrollee.Name,enrollee.Contest,enrollee.ReasonForAddmission,enrollee.Finance,enrollee.Status);
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
                if (specialities.Count != 0)
                {
                    currentSpeciality = specialities[0];
                    InitializeEnrolleeGrid(SearchMode);
                }
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
            connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            enrolleeService = new EnrolleeService(connectionString);
            viewService = new ViewService(connectionString);
            assessmentService = new AssessmentService(connectionString);
            priorityOfSpecialityService = new PriorityOfSpecialityService(connectionString);
            atributeForEnrolleeService = new AtributeForEnrolleeService(connectionString);
            examSchemaService = new ExamSchemaService(connectionString);
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
                InitializeEnrolleeGrid(SearchMode);
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
                InitializeEnrolleeGrid(SearchMode);
            }
            else
            {
                Properties.Settings.Default.SearchToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, по выбранным параметрам специальности
                SearchMode = true;
                InitializeEnrolleeGrid(SearchMode);
            }
        }

        private void ShowNewEnrolleeCard(object sender, EventArgs e)
        {
            frmEnrolleeCard enrolleeCard = new frmEnrolleeCard(activeEmployee);
            DialogResult enrolleeCardResult = enrolleeCard.ShowDialog();
            if(enrolleeCardResult == DialogResult.OK)
            {
                InitializeEnrolleeGrid(SearchMode);
            }
        }

        private void EditCurrentEnrolleeCard(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Редактировать профиль выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                frmEnrolleeCard enrolleeCard = new frmEnrolleeCard(activeEmployee, enrollee);
                DialogResult enrolleeCardResult = enrolleeCard.ShowDialog();
                if (enrolleeCardResult == DialogResult.OK)
                {
                    InitializeEnrolleeGrid(SearchMode);
                }
            }
        }

        private void DeleteCurrentEnrollee(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Удалить выбранного абитуриента?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                List<Assessment> assessments = assessmentService.GetAssessments(enrollee);
                foreach (var assessment in assessments)
                    assessmentService.DeleteAssessment(assessment);
                List<AtributeForEnrollee> atributes = atributeForEnrolleeService.GetAtributeForEnrollees(enrollee);
                foreach (var atribute in atributes)
                    atributeForEnrolleeService.DeleteAtributeForEnrollee(atribute);
                List<PriorityOfSpeciality> priorities = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee);
                foreach (var priority in priorities)
                    priorityOfSpecialityService.DeletePriorityOfSpeciality(priority);
                enrolleeService.DeleteEnrollee(enrollee);
                InitializeEnrolleeGrid(SearchMode);
            }
        }

        private void PrintStatement(object sender, EventArgs e)
        {
            ReportManager.ConnectionString = connectionString;
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            enrollee = enrolleeService.GetEnrollee(id);
            ReportManager.PrintStatement(enrollee);
        }

        private void PrintTitle(object sender, EventArgs e)
        {
            ReportManager.ConnectionString = connectionString;
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            enrollee = enrolleeService.GetEnrollee(id);
            ReportManager.PrintTitle(enrollee);
        }

        private void PrintExamSheet(object sender, EventArgs e)
        {
            ReportManager.ConnectionString = connectionString;
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            enrollee = enrolleeService.GetEnrollee(id);
            ReportManager.PrintExamSheet(enrollee);
        }

        private void PrintReceipt(object sender, EventArgs e)
        {
            ReportManager.ConnectionString = connectionString;
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            enrollee = enrolleeService.GetEnrollee(id);
            frmReceipt receiptCard = new frmReceipt(enrollee);
            DialogResult receiptCardResult = receiptCard.ShowDialog();
            if(receiptCardResult == DialogResult.OK)
            {
                ReportManager.PrintReceipt(enrollee, receiptCard.DocumentOfStudy, receiptCard.DocumentOfDiscount, receiptCard.DocumentOther);
            }
        }

        private void PrintNotice(object sender, EventArgs e)
        {
            ReportManager.ConnectionString = connectionString;
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            enrollee = enrolleeService.GetEnrollee(id);
            List<Assessment> assessments = assessmentService.GetAssessments(enrollee);
            bool flag = false;
            foreach(var assessment in assessments)
                if(assessment.Discipline.BasisForAssessingId == 2)
                {
                    flag = true;
                    break;
                }
            else
                {
                    flag = false;
                    break;                    
                }
            if (!flag) MessageBox.Show(this, "Абитуриент не сдаёт вступительные испытания в университете", "Печать извещения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else ReportManager.PrintNotice(enrollee);
        }

        private void Filter(object sender, EventArgs e)
        {
            // Фамилия абитуриента
            string query = $"Фамилия LIKE '%{tbSurname.Text}%'";
            // Тип финансирования
            if (BudgetFilter.CheckState == CheckState.Checked && FeeFilter.CheckState == CheckState.Unchecked) query += $" AND (КодФинансирования = 1 OR КодФинансирования = 3)";            
            else if (BudgetFilter.CheckState == CheckState.Unchecked && FeeFilter.CheckState == CheckState.Checked) query += $" AND (КодФинансирования = 2 OR КодФинансирования = 3)";
            else if (BudgetFilter.CheckState == CheckState.Checked && FeeFilter.CheckState == CheckState.Checked) query += $" AND КодФинансирования = 3";
            // Вид конкурса
            if (GeneralContestFilter.CheckState == CheckState.Checked && WithoutExamFilter.CheckState == CheckState.Unchecked && OutOfContestFilter.CheckState == CheckState.Unchecked)      query += $" AND КодКонкурса = 1";
            else if (GeneralContestFilter.CheckState == CheckState.Unchecked && WithoutExamFilter.CheckState == CheckState.Checked && OutOfContestFilter.CheckState == CheckState.Unchecked) query += $" AND КодКонкурса = 2";
            else if (GeneralContestFilter.CheckState == CheckState.Unchecked && WithoutExamFilter.CheckState == CheckState.Unchecked && OutOfContestFilter.CheckState == CheckState.Checked) query += $" AND КодКонкурса = 3";
            else if ((GeneralContestFilter.CheckState == CheckState.Checked && WithoutExamFilter.CheckState == CheckState.Unchecked && OutOfContestFilter.CheckState == CheckState.Checked)) query += $" AND (КодКонкурса = 1 OR КодКонкурса = 3)";
            else if ((GeneralContestFilter.CheckState == CheckState.Checked && WithoutExamFilter.CheckState == CheckState.Checked && OutOfContestFilter.CheckState == CheckState.Unchecked)) query += $" AND (КодКонкурса = 1 OR КодКонкурса = 2)";
            else if ((GeneralContestFilter.CheckState == CheckState.Unchecked && WithoutExamFilter.CheckState == CheckState.Checked && OutOfContestFilter.CheckState == CheckState.Checked)) query += $" AND (КодКонкурса = 2 OR КодКонкурса = 3)";
            // Статус абитуриента
            if (CandidateFilter.CheckState == CheckState.Checked && EnrollFilter.CheckState == CheckState.Unchecked && TookDocumentFilter.CheckState == CheckState.Unchecked) query += $" AND КодСтатуса = 1";
            else if (CandidateFilter.CheckState == CheckState.Unchecked && EnrollFilter.CheckState == CheckState.Checked && TookDocumentFilter.CheckState == CheckState.Unchecked) query += $" AND КодСтатуса = 3";
            else if (CandidateFilter.CheckState == CheckState.Unchecked && EnrollFilter.CheckState == CheckState.Unchecked && TookDocumentFilter.CheckState == CheckState.Checked) query += $" AND КодСтатуса = 2";
            else if (CandidateFilter.CheckState == CheckState.Checked && EnrollFilter.CheckState == CheckState.Checked && TookDocumentFilter.CheckState == CheckState.Unchecked) query += $" AND (КодСтатуса = 1 OR КодСтатуса = 3)";
            else if (CandidateFilter.CheckState == CheckState.Checked && EnrollFilter.CheckState == CheckState.Unchecked && TookDocumentFilter.CheckState == CheckState.Checked) query += $" AND (КодСтатуса = 1 OR КодСтатуса = 2)";
            else if (CandidateFilter.CheckState == CheckState.Unchecked && EnrollFilter.CheckState == CheckState.Checked && TookDocumentFilter.CheckState == CheckState.Checked) query += $" AND (КодСтатуса = 2 OR КодСтатуса = 3)";
            (EnrolleeGrid.DataSource as DataTable).DefaultView.RowFilter = query;
        }

        private void LogoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void EntryExamView(object sender, EventArgs e)
        {
            frmEnrtyExam entryExamView = new frmEnrtyExam();
            entryExamView.ShowDialog();
        }

        private void EnrollView(object sender, EventArgs e)
        {
            frmEnroll enrollView = new frmEnroll();
            enrollView.ShowDialog();
            InitializeEnrolleeGrid(SearchMode);
        }

        private void PrintSummaryExaminationReport(object sender, EventArgs ea)
        {
            frmChooseSpeciality chooseSpeciality = new frmChooseSpeciality();
            if (chooseSpeciality.ShowDialog() == DialogResult.OK)
            {
                ReportManager.ConnectionString = connectionString;
                List<Enrollee> enrollees = enrolleeService.GetEnrollees(chooseSpeciality.speciality)
                    .OrderByDescending(e => e.ReasonForAddmission.ContestId)
                    .ThenByDescending(e => e.Assessment.Sum(a => a.Estimation))
                    .ToList();
                ReportManager.PrintSummaryExaminationSheet(enrollees, chooseSpeciality.speciality);
            }
        }

        private void PrintExaminationSheet(object sender, EventArgs e)
        {
            frmChooseSpeciality chooseSpeciality = new frmChooseSpeciality();
            if (chooseSpeciality.ShowDialog() == DialogResult.OK)
            {
                ReportManager.ConnectionString = connectionString;
                List<ExamSchema> exams = examSchemaService.GetExamSchemas(chooseSpeciality.speciality);
                bool flag = false;
                foreach(var exam in exams)
                {
                    if (exam.Discipline.BasisForAssessingId == 2)
                    { flag = true; break; }
                }
                if (flag)
                {
                    List<Enrollee> enrollees = enrolleeService.GetEnrollees(chooseSpeciality.speciality);
                    ReportManager.PrintExaminationSheet(exams, enrollees);
                }
                else
                {
                    MessageBox.Show(this, "Абитуриенты данной специальности не сдают вступительные испытания в университете", "Печать экзаменнационного листа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void PrintMonitoringBudget(object sender, EventArgs ea)
        {
            ReportManager.ConnectionString = connectionString;
            List<Enrollee> enrollees = enrolleeService.GetEnrollees();
            ReportManager.PrintBudgetMonitoring(enrollees);
        }

        private void PrintMonitoringFee(object sender, EventArgs ea)
        {
            ReportManager.ConnectionString = connectionString;
            List<Enrollee> enrollees = enrolleeService.GetEnrollees();
            ReportManager.PrintFeeMonitoring(enrollees);
        }

        private void PrintInformationReport(object sender, EventArgs ea)
        {
            ReportManager.ConnectionString = connectionString;
            List<Enrollee> enrollees = enrolleeService.GetEnrollees();
            ReportManager.PrintInformationReport(enrollees);
        }

        private void EnrolleeGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Add this
                EnrolleeGrid.CurrentCell = EnrolleeGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                EnrolleeGrid.Rows[e.RowIndex].Selected = true;
                EnrolleeGrid.Focus();
            }
        }

        private void contextMenuEnrolleeGrid_Opening(object sender, CancelEventArgs e)
        {
            var cms = sender as ContextMenuStrip;
            var mousepos = MousePosition;
            if (cms != null)
            {
                var rel_mousePos = cms.PointToClient(mousepos);
                if (cms.ClientRectangle.Contains(rel_mousePos))
                {
                    //the mouse pos is on the menu ... 
                    //looks like the mouse was used to open it
                    var dgv_rel_mousePos = EnrolleeGrid.PointToClient(mousepos);
                    var hti = EnrolleeGrid.HitTest(dgv_rel_mousePos.X, dgv_rel_mousePos.Y);
                    if (hti.RowIndex == -1)
                    {
                        // no row ...
                        e.Cancel = true;
                    }
                    else
                    {
                        int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                        enrollee = enrolleeService.GetEnrollee(id);
                        SetStateContextMenu(enrollee);
                    }
                }
                else
                {
                    //looks like the menu was opened without the mouse ...
                    //we could cancel the menu, or perhaps use the currently selected cell as reference...
                    e.Cancel = true;
                }
            }
        }

        private void SetStateContextMenu(Enrollee enrollee)
        {
            // Получем тип финансирования
            switch (enrollee.FinanceTypeId)
            {
                case 1:
                    {
                        BudgetContextMenuItem.CheckState = CheckState.Checked;
                        FeeContextMenuItem.CheckState = CheckState.Unchecked;
                        BudgetAndFeeContextMenuItem.CheckState = CheckState.Unchecked;
                        break;
                    }
                case 2:
                    {
                        BudgetContextMenuItem.CheckState = CheckState.Unchecked;
                        FeeContextMenuItem.CheckState = CheckState.Checked;
                        BudgetAndFeeContextMenuItem.CheckState = CheckState.Unchecked;
                        break;
                    }
                case 3:
                    {
                        BudgetContextMenuItem.CheckState = CheckState.Unchecked;
                        FeeContextMenuItem.CheckState = CheckState.Unchecked;
                        BudgetAndFeeContextMenuItem.CheckState = CheckState.Checked;
                        break;
                    }
            }
            // Статус абитуриента
            switch (enrollee.StateTypeId)
            {
                case 1:
                    {
                        CandidateContextMenuItem.Enabled = true;
                        TookDocumentContextMenuItem.Enabled = true;
                        CandidateContextMenuItem.CheckState = CheckState.Checked;
                        TookDocumentContextMenuItem.CheckState = CheckState.Unchecked;
                        break;
                    }
                case 2:
                    {
                        CandidateContextMenuItem.Enabled = true;
                        TookDocumentContextMenuItem.Enabled = true;
                        CandidateContextMenuItem.CheckState = CheckState.Unchecked;
                        TookDocumentContextMenuItem.CheckState = CheckState.Checked;
                        break;
                    }
                case 3:
                    {
                        CandidateContextMenuItem.Enabled = false;
                        TookDocumentContextMenuItem.Enabled = false;
                        break;
                    }
            }
        }

        private void BudgetAndFeeContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.FinanceTypeId = 3;
                enrolleeService.UpdateEnrollee(enrollee);
                InitializeEnrolleeGrid(SearchMode);
            }
        }

        private void BudgetContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.FinanceTypeId = 1;
                enrolleeService.UpdateEnrollee(enrollee);
                InitializeEnrolleeGrid(SearchMode);
            }
        }

        private void FeeContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.FinanceTypeId = 2;
                enrolleeService.UpdateEnrollee(enrollee);
                InitializeEnrolleeGrid(SearchMode);
            }
        }

        private void CandidateContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.StateTypeId = 1;
                enrolleeService.UpdateEnrollee(enrollee);
                InitializeEnrolleeGrid(SearchMode);
            }
        }

        private void TookDocumentContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.StateTypeId = 2;
                enrollee.DecreeId = null;

                enrolleeService.UpdateEnrollee(enrollee);
                InitializeEnrolleeGrid(SearchMode);
            }
        }
    }
}

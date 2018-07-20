using NLog;
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
    /// <summary>
    /// Класс-форма вступительные испытания
    /// </summary>
    public partial class frmEnrtyExam : Form
    {
        // Логгер
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Таблица данных абитуриентов
        private DataTable enrolleeTable;
        // Выбранный факультет
        private Faculty faculty;
        // Выбранная форма обучения
        private FormOfStudy formOfStudy;
        // Выбранная специальность
        private Speciality speciality;
        // Выбранная дисциплина
        private Discipline discipline;
        // Текущий пользователь
        private Employee activeEmployee;
        // Сервисы доступа к данным
        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private DisciplineService disciplineService;
        private AssessmentService assessmentService;
        private EnrolleeService enrolleeService;
        private ExamSchemaService examSchemaService;
        private ViewService viewService;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public frmEnrtyExam(Employee employee)
        {
            InitializeComponent();
            // Запоминаем текущего пользователя
            this.activeEmployee = employee;
            // Создаем структуру таблицы абитуриентов
            enrolleeTable = CreateStructureTable();
            EnrolleeGrid.DataSource = enrolleeTable;
            // Инициализируем сервисы доступа к данным
            InitializeDataAccessServices();
            // Инициализация данными выпадающих списков
            InitializeComboBox();            
            // Установка стиля отображения таблицы абитуриентов
            SetGridStyle();
            logger.Info($"Пользователь: {activeEmployee.Fullname.Trim()} начал работу с вводом оценок вступительных испытаний.");
        }
        /// <summary>
        /// Метод создания структуры таблиы данных абитуриентов
        /// </summary>
        /// <returns>Таблица данных абитуриента</returns>
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
        /// <summary>
        /// Метод инициализации выпадающих списков данными
        /// </summary>
        private void InitializeComboBox()
        {
            InitializeFacultyComboBox();
            InitializeFormOfStudyComboBox();
        }
        /// <summary>
        /// Метод инициализации выпадающего списка форм обучения
        /// </summary>
        private void InitializeFormOfStudyComboBox()
        {
            // Отключаем отслеживание изменения
            cbFormOfStudy.SelectedValueChanged -= cbFormOfStudy_SelectedValueChanged;
            // Получаем список форм обучения
            var formsOfStudies = formOfStudyService.GetFormOfStudies();
            cbFormOfStudy.DataSource = formsOfStudies;
            cbFormOfStudy.DisplayMember = "Fullname";
            cbFormOfStudy.ValueMember = "FormOfStudyId";
            // Инициализируем форму обучения первым элементом списка
            // Инициализируем список специальностей
            if (formsOfStudies.Count != 0)
            {
                formOfStudy = formsOfStudies[0];
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения
            cbFormOfStudy.SelectedValueChanged += cbFormOfStudy_SelectedValueChanged;
        }
        /// <summary>
        /// Метод инициализации выпадающего списка факультетов
        /// </summary>
        private void InitializeFacultyComboBox()
        {
            // Отключаем отслеживание изменения
            cbFaculty.SelectedValueChanged -= cbFaculty_SelectedValueChanged;
            // Получаем список факультетов
            var faculties = facultyService.GetFaculties();
            cbFaculty.DataSource = faculties;
            cbFaculty.DisplayMember = "Shortname";
            cbFaculty.ValueMember = "FacultyId";
            // Инициализируем факультет первым элементом списка
            // Инициализируем список специальностей
            if (faculties.Count != 0)
            {
                faculty = faculties[0];
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения
            cbFaculty.SelectedValueChanged += cbFaculty_SelectedValueChanged;
        }
        /// <summary>
        /// Метод инициализации выпадающего списка специальностей первой ступени
        /// </summary>
        private void InitializeSpecialityComboBox()
        {
            if (faculty != null && formOfStudy != null)
            {
                // Отключаем отслеживание изменения
                cbSpeciality.SelectedValueChanged -= cbSpeciality_SelectedValueChanged;
                // Получаем список специальности выбранного факультета и формы обучения
                var specialities = specialityService.GetSpecialities(faculty, formOfStudy);
                cbSpeciality.DataSource = specialities;
                cbSpeciality.DisplayMember = "Fullname";
                cbSpeciality.ValueMember = "SpecialityId";
                // Инициализируем специальность первым элементом списка
                // Инициализируем список дисциплинами вступительного испытания
                if (specialities.Count != 0)
                {
                    speciality = specialities[0];
                    InitializeDisciplineComboBox();
                    
                }
                // Включаем отслеживание изменения
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }
        /// <summary>
        /// Метод инициализации таблицы абитуриентов
        /// </summary>
        private void InitializeEnrolleeGrid()
        {
            // Очистка таблицы
            enrolleeTable.Clear();
            // Получение списка оценок по дисциплине
            var assessments = viewService.GetAssessments(discipline);
            // Заполнение таблицы данными
            foreach (var assessment in assessments)
            {
                var enrollee = enrolleeService.GetEnrollee(assessment.EnrolleeId);
                if (enrollee.StateTypeId != 2 && enrollee.Speciality.FormOfStudyId == formOfStudy.FormOfStudyId && enrollee.ReasonForAddmission.ContestId != 2)
                {
                    if (string.IsNullOrWhiteSpace(assessment.FormOfStudy)) enrolleeTable.Rows.Add(assessment.AssessmentId, $"{assessment.Speciality.Trim()}-{assessment.NumberOfDeal}", assessment.RuSurname, assessment.RuName, assessment.Estimation);
                    else enrolleeTable.Rows.Add(assessment.AssessmentId, $"{assessment.Speciality.Trim()}{assessment.FormOfStudy.Trim()}-{assessment.NumberOfDeal}", assessment.RuSurname, assessment.RuName, assessment.Estimation);
                }
            }
        }
        /// <summary>
        /// Метод настройки отображения таблицы абитуриентов
        /// </summary>
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
        /// <summary>
        /// Метод инициализации выпадающего списка дисциплин вступительных испытаний
        /// </summary>
        private void InitializeDisciplineComboBox()
        {
            // Отключаем отслеживание изменения
            cbDiscipline.SelectedValueChanged -= cbDiscipline_SelectedValueChanged;
            // Получаем список экзаменнационной схемы специальности
            var exams = examSchemaService.GetExamSchemas(speciality);
            // Список дисциплин
            var disciplines = new List<Discipline>();
            // Если в экзаменнационной схеме дисциплина вступительного испытания добавляем в список
            foreach (var exam in exams)
                if (exam.Discipline.BasisForAssessingId == 2) disciplines.Add(exam.Discipline);
            cbDiscipline.DataSource = disciplines;
            cbDiscipline.DisplayMember = "Name";
            cbDiscipline.ValueMember = "DisciplineId";
            // Если список не пуст
            // Инициализируем дисциплину первой записью списка
            // Загружаем список абитуриентов сдающих вступительное испытание
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
            //Включаем отслеживание изменения
            cbDiscipline.SelectedValueChanged += cbDiscipline_SelectedValueChanged;
        }
        /// <summary>
        /// Инициализация сервисов доступа данных
        /// </summary>
        private void InitializeDataAccessServices()
        {
            // Инициализация строки подключения данных
            logger.Info($"Получение строки подключения к источнику данных");
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            // Инициализация сервисов доступа данных
            assessmentService = new AssessmentService(connectionString);
            enrolleeService = new EnrolleeService(connectionString);
            examSchemaService = new ExamSchemaService(connectionString);
            disciplineService = new DisciplineService(connectionString);
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            viewService = new ViewService(connectionString);
        }
        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        /// <param name="sender">Кнопка "Отмена"</param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} закончил работу с вводом оценок вступительного испытания.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Факультеты"</param>
        /// <param name="e"></param>
        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbFaculty.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbFaculty.SelectedValue;
                // По уникальному идентификатору ищем факультет
                faculty = facultyService.GetFaculty(id);
                // Инициализируем специальности
                InitializeSpecialityComboBox();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Формы обучения"</param>
        /// <param name="e"></param>
        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbFormOfStudy.SelectedValue !=null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbFormOfStudy.SelectedValue;
                // По уникальному идентификатору ищем форму обучения
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
                // Инициализируем специальности
                InitializeSpecialityComboBox();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Специальности"</param>
        /// <param name="e"></param>
        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbSpeciality.SelectedValue!=null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbSpeciality.SelectedValue;
                // По уникальному идентификатору ищем специальность
                speciality = specialityService.GetSpeciality(id);
                // Инициализируем дисциплины
                InitializeDisciplineComboBox();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Дисциплины"</param>
        /// <param name="e"></param>
        private void cbDiscipline_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbDiscipline.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbDiscipline.SelectedValue;
                // По уникальному идентификатору ищем специальность
                discipline = disciplineService.GetDiscipline(id);
                // Инициализируем таблицу абитуриентов
                InitializeEnrolleeGrid();
            }
        }
        /// <summary>
        /// Обработчик очистки оценок абитуриентов
        /// </summary>
        /// <param name="sender">Кнопка "Очистить столбец оценок"</param>
        /// <param name="e"></param>
        private void btClear_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in EnrolleeGrid.Rows)
            {
                row.Cells[4].Value = "0";
            }
        }
        /// <summary>
        /// Обработчик сохранения оценок
        /// </summary>
        /// <param name="sender">Кнопка "Сохранить оценки"</param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Сохранить список оценок?", "Вступительные испытания", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} выполняет сохранение оценок абитуриентов.");
                // Сохраняем оценки абитуриентов
                foreach (DataGridViewRow row in EnrolleeGrid.Rows)
                {
                    var assessment = assessmentService.GetAssessment(Int32.Parse(row.Cells[0].Value.ToString()));
                    assessment.Estimation = Int32.Parse(row.Cells[4].Value.ToString());
                    assessmentService.UpdateAssessment(assessment);                    
                }
            }
        }
    }
}

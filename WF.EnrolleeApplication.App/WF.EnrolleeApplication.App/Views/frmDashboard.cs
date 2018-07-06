using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WF.EnrolleeApplication.App.Services;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Views
{
    /// <summary>
    /// Класс-форма "Главная форма приложения"
    /// </summary>
    public partial class frmDashboard : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Строка подключения к источнику данных
        private string connectionString;
        // Вспомогательные переменные для скрытия/отображения панелей формы
        private bool ToolBoxVisible;
        private bool SearchToolBoxVisible;
        private bool FilterToolBoxVisible;
        private bool StatusStripVisible;
        // Режим поиска
        private bool SearchMode = false;
        // Таблица данных абитуриентов
        private DataTable enrolleeTable;
        // Выбранный абитуриент
        private Enrollee enrollee;
        // Текущий пользователь системы
        private Employee activeEmployee;
        // Выбранный факультет
        private Faculty currentFaculty;
        // Выбранная форма обучения
        private FormOfStudy currentFormOfStudy;
        // Выбранная специальность
        private Speciality currentSpeciality;
        // Сервисы получения данных
        private AssessmentService assessmentService;
        private PriorityOfSpecialityService priorityOfSpecialityService;
        private AtributeForEnrolleeService atributeForEnrolleeService;
        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private EnrolleeService enrolleeService;
        private ViewService viewService;
        private ExamSchemaService examSchemaService;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="employee">Текущий пользователь системы</param>
        public frmDashboard(Employee employee)
        {
            InitializeComponent();
            // Запоминаем текущего пользователя
            this.activeEmployee = employee;
            // Создаем структуру таблицы абитуриентов
            enrolleeTable = CreateStructureTable();
            // Инициализируем сервисы доступа к данным
            InitializeDataAccessServices();
            // Заполняем данными выпадающие списки для поиска 
            InitializeSearchComboBoxes();
            // Инициализируем строку состояния данными
            InitializeStatusStrip();
            // Параметры программы
            LoadUsersSettings();
            // Устанавливаем таблицу абитуриентов как источник данных компонента
            EnrolleeGrid.DataSource = enrolleeTable;
            // Инициализируем данными таблицу абитуриентов
            InitializeEnrolleeGrid(SearchMode);
            // Устанавливаем стиль отображения
            SetStyleEnrolleeGrid(EnrolleeGrid);
            logger.Info("Выполнен вход в АИС Абитуриент.");
            logger.Info($"Работает пользователь:\n {employee.ToString()}.");
        }
        /// <summary>
        /// Метод установка стиля отображения таблицы данных абитуриентов
        /// </summary>
        /// <param name="enrolleeGrid">Компонент расположения таблицы абитуриентов</param>
        private void SetStyleEnrolleeGrid(DataGridView enrolleeGrid)
        {
            // Установка видимости столбцов
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
            // Установка весов (ширины) столбцов
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
        /// <summary>
        /// Создание структуры таблицы данных
        /// </summary>
        /// <returns>Таблица данных, для отображения списка абитуриентов</returns>
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
        /// <summary>
        /// Инициализация таблицы абитуриентов данными
        /// </summary>
        /// <param name="searchMode">Параметр, определяющий способ поиска абитуриента: по специальностям или в порядке добавления</param>
        private void InitializeEnrolleeGrid(bool searchMode)
        {
            // Список абитуриентов
            List<EnrolleeView> enrollees;
            // Если режим поиска — получаем список абитуриентов, по выбранной специальности
            // Иначе в порядке добавления текущего пользователя
            if(searchMode)
            {
                logger.Debug($"Обновление таблицы абитуриентов. Включен режим поиска.");
                enrollees = viewService.GetEnrollees(currentSpeciality); 
            }
            else
            {
                logger.Debug($"Обновление таблицы абитуриентов. Режим поиска выключен.");
                enrollees = viewService.GetEnrollees(activeEmployee);
            }
            // Очищаем таблицу данных
            enrolleeTable.Clear();
            logger.Debug($"Заполняем данными таблицу абитуриентов.");
            // Заполняем таблицу данных списком абитуриентов
            foreach (var enrollee in enrollees)
            {
                string numberOfDeal = $"";
                if (string.IsNullOrWhiteSpace(enrollee.FormOfStudyShortname)) numberOfDeal = $"{enrollee.SpecialityShortname}-{enrollee.NumberOfDeal}";
                else numberOfDeal = $"{enrollee.SpecialityShortname}{enrollee.FormOfStudyShortname}-{enrollee.NumberOfDeal}";
                enrolleeTable.Rows.Add(enrollee.EnrolleeId, enrollee.SpecialityId, enrollee.ContestId, enrollee.ReasonForAddmissionId, enrollee.FinanceTypeId, enrollee.StateId, enrollee.EmployeeId, numberOfDeal, enrollee.FormOfStudy,enrollee.Speciality,enrollee.Surname,enrollee.Name,enrollee.Contest,enrollee.ReasonForAddmission,enrollee.Finance,enrollee.Status);
            }
        }
        /// <summary>
        /// Метод загрузки сохраненных параметров приложения пользователем
        /// Используются параметры внешнего вида приложения:
        /// Панели инструментов, панели поиска, панели фильтров и строки состояния
        /// </summary>
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
        /// <summary>
        /// Инициализация выпадающих списков поиска по специальности
        /// </summary>
        private void InitializeSearchComboBoxes()
        {
            // Заполняем факультеты
            InitializeFacultyComboBox();
            // Заполняем формы обучения
            InitializeFormOfStudyComboBox();         
        }
        /// <summary>
        /// Инициализация выпадающего списка специальности
        /// </summary>
        private void InitializeSpecialityComboBox()
        {
            // Если выбраны текущие факультет и форма обучения
            if (currentFaculty != null && currentFormOfStudy != null)
            {
                // Отключаем отслеживание изменения специальности в списке специальностей
                cbSpeciality.SelectedValueChanged -= cbSpeciality_SelectedValueChanged;
                // Получаем список специальностей
                var specialities = specialityService.GetSpecialities(currentFaculty,currentFormOfStudy);
                // Инициализация комбо-бокса
                cbSpeciality.DataSource = specialities;
                cbSpeciality.DisplayMember = "Fullname";
                cbSpeciality.ValueMember = "SpecialityId";
                // Если список специальностей не пуст, инициализируем выбранную специальность - первой из списка
                // Инициализируем список абитуриентов
                if (specialities.Count != 0)
                {
                    currentSpeciality = specialities[0];
                    InitializeEnrolleeGrid(SearchMode);
                }
                // Включаем отслеживание изменения специальности в списке специальностей
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }
        /// <summary>
        /// Инициализация выпадающего списка форм обучения
        /// </summary>
        private void InitializeFormOfStudyComboBox()
        {
            // Отключаем отслеживание изменения формы обучения в списке форм обучения
            cbFormOfStudy.SelectedValueChanged -= cbFormOfStudy_SelectedValueChanged;
            // Получаем список форм обучения
            var formsOfStudies = formOfStudyService.GetFormOfStudies();
            // Инициализация комбо-бокса
            cbFormOfStudy.DataSource = formsOfStudies;
            cbFormOfStudy.DisplayMember = "Fullname";
            cbFormOfStudy.ValueMember = "FormOfStudyId";
            // Если список форм обучения не пуст, инициализируем выбранную форму обучения - первой из списка
            // Инициализируем список специальностей
            if (formsOfStudies.Count != 0)
            {
                currentFormOfStudy = formsOfStudies[0];
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения формы обучения в списке форм обучения
            cbFormOfStudy.SelectedValueChanged += cbFormOfStudy_SelectedValueChanged;
        }
        /// <summary>
        /// Инициализация выпадающего списка факультета
        /// </summary>
        private void InitializeFacultyComboBox()
        {
            // Отключаем отслеживание изменения факультета в списке факультетов
            cbFaculty.SelectedValueChanged -= cbFaculty_SelectedValueChanged;
            // Получаем список факультетов
            var faculties = facultyService.GetFaculties();
            // Инициализация комбо-бокса
            cbFaculty.DataSource = faculties;
            cbFaculty.DisplayMember = "Shortname";
            cbFaculty.ValueMember = "FacultyId";
            // Если список факультетов не пуст, инициализируем выбранный факультет - первой из списка
            // Инициализируем список специальностей
            if (faculties.Count != 0)
            {
                currentFaculty = faculties[0];
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения факультета в списке факультетов
            cbFaculty.SelectedValueChanged += cbFaculty_SelectedValueChanged;
        }
        /// <summary>
        /// Инициализация сервисов доступа к данным
        /// </summary>
        private void InitializeDataAccessServices()
        {
            // Получение строки подключения к источнику данных
            logger.Info($"Получение строки подключения к источнику данных.");
            connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            // Инициализация сервисов доступа к данным
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
        /// <summary>
        /// Инициализация строки состояния приложения
        /// </summary>
        private void InitializeStatusStrip()
        {
            // Вывод информации о текущем пользователе системы
            lbEmployee.Text = $"{activeEmployee.EmployeePost.Name}, {activeEmployee.Fullname}";
            // Получение списка абитуриентов
            var enrollees = enrolleeService.GetEnrollees();
            // Вывод информации о количество записей абитуриентов
            lbCountRecord.Text = enrollees.Count.ToString();
        }
        /// <summary>
        /// Обработчик выбора факультета
        /// </summary>
        /// <param name="sender">ComboBox факультетов</param>
        /// <param name="e"></param>
        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список факультетов не пуст
            // Получаем уникальный идентификатор выбранного факультета
            // Получаем факультет по уникальному идентификатору
            // Инициализация списка специальностей
            if(cbFaculty.SelectedValue != null)
            {
                int id = (int)cbFaculty.SelectedValue;
                currentFaculty = facultyService.GetFaculty(id);
                InitializeSpecialityComboBox();
            }
        }
        /// <summary>
        /// Обработчик выбора форм обучения
        /// </summary>
        /// <param name="sender">ComboBox формы обучения</param>
        /// <param name="e"></param>
        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список форм обучения не пуст
            // Получаем уникальный идентификатор выбранной формы обучения
            // Получаем форму обучения по уникальному идентификатору
            // Инициализация списка специальностей
            if (cbFormOfStudy.SelectedValue != null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                currentFormOfStudy = formOfStudyService.GetFormOfStudy(id);
                InitializeSpecialityComboBox();
            }
        }
        /// <summary>
        /// Обработчик выбора специальностей
        /// </summary>
        /// <param name="sender">ComboBox специальности</param>
        /// <param name="e"></param>
        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список специальностей не пуст
            // Получаем уникальный идентификатор выбранной специальности
            // Получаем специальность по уникальному идентификатору
            // Инициализация списка абитуриентов
            if (cbSpeciality.SelectedValue!=null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                currentSpeciality = specialityService.GetSpeciality(id);
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки меню "Вид — Панель инструментов"
        /// </summary>
        /// <param name="sender">Кнопка меню "Вид — Панель инструментов"</param>
        /// <param name="e"></param>
        private void ToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если панель инструментов видима
            if(ToolBoxVisible)
            {
                // Скрываем панель инструментов
                Properties.Settings.Default.ToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
            // Иначе
            else 
            {
                // Показываем панель инструментов
                Properties.Settings.Default.ToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки меню "Вид — Панель поиска"
        /// </summary>
        /// <param name="sender">Кнопка меню "Вид — Панель поиска"</param>
        /// <param name="e"></param>
        private void SearchToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если панель поиска видима
            if (SearchToolBoxVisible)
            {
                // Скрываем панель поиска
                Properties.Settings.Default.SearchToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, которые добавил оператор
                SearchMode = false;
            }
            else
            {
                // Показываем панель поиска
                Properties.Settings.Default.SearchToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, по выбранным параметрам специальности
                SearchMode = true;
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки меню "Вид — Строка состояния"
        /// </summary>
        /// <param name="sender">Кнопка меню "Вид — Строка состояния"</param>
        /// <param name="e"></param>
        private void StatusStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если строка состояния видима
            if (StatusStripVisible)
            {
                // Скрываем строку состояния
                Properties.Settings.Default.StatusStripVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
            else
            {
                // Показываем строку состояния
                Properties.Settings.Default.StatusStripVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки меню "Вид — Панель фильтра"
        /// </summary>
        /// <param name="sender">Кнопка меню "Вид — Панель фильтра"</param>
        /// <param name="e"></param>
        private void FilterToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если панель фильтра видима
            if (FilterToolBoxVisible)
            {
                // Скрываем панель фильтра
                Properties.Settings.Default.FilterToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
            else
            {
                // Показываем панель фильтра
                Properties.Settings.Default.FilterToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Панель поиска" на панели инструментов
        /// </summary>
        /// <param name="sender">Кнопка "Панель поиска"</param>
        /// <param name="e"></param>
        private void SearchToolBoxToolStripButton_Click(object sender, EventArgs e)
        {
            // Если панель поиска видима
            if (SearchToolBoxVisible)
            {
                // Скрываем панель поиска
                Properties.Settings.Default.SearchToolBoxVisible = false;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, которые добавил оператор
                SearchMode = false;
                InitializeEnrolleeGrid(SearchMode);
            }
            else
            {
                // Показываем панель поиска
                Properties.Settings.Default.SearchToolBoxVisible = true;
                Properties.Settings.Default.Save();
                LoadUsersSettings();
                // Режим поиска : подгрузка пользователей, по выбранным параметрам специальности
                SearchMode = true;
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Вызов формы регистрации/изменения абитуриента для добавления абитуриента
        /// </summary>
        /// <param name="sender">Кнопка вызова регистрации/изменения абитуриента</param>
        /// <param name="e"></param>
        private void ShowNewEnrolleeCard(object sender, EventArgs e)
        {
            // Форма регистрации абитуриента
            // activeEmployee — текущий оператор
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} выполняет регистрацию абитуриента.");
            frmEnrolleeCard enrolleeCard = new frmEnrolleeCard(activeEmployee);
            DialogResult enrolleeCardResult = enrolleeCard.ShowDialog();
            if(enrolleeCardResult == DialogResult.OK)
            {
                // Если пользователь успешно добавил абитуриента
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} " +
                    $"выполнил регистрацию абитуриента: {enrolleeCard.enrollee.RuSurname} {enrolleeCard.enrollee.RuName}");
                // Обновляем список абитуриентов
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Вызов формы регистрации/изменения абитуриента для редактирования абитуриента
        /// </summary>
        /// <param name="sender">Кнопка вызова регистрации/изменения абитуриента</param>
        /// <param name="e"></param>
        private void EditCurrentEnrolleeCard(object sender, EventArgs e)
        {
            // Диалоговое окно о серьезности намерения (:
            DialogResult youSure = MessageBox.Show(this, "Редактировать профиль выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {           
                // Получаем уникальный идентификатор выбранного абитуриента
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                // Поиск абитуриента
                enrollee = enrolleeService.GetEnrollee(id);
                // Вызов формы регистрации
                // activeEmployee — текущий оператор
                // enrollee — выбранный абитуриент для редактирования
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} выполняет редактирование профиля абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                frmEnrolleeCard enrolleeCard = new frmEnrolleeCard(activeEmployee, enrollee);
                DialogResult enrolleeCardResult = enrolleeCard.ShowDialog();
                if (enrolleeCardResult == DialogResult.OK)
                {
                    // Если пользователь успешно редактировал запись абитуриента
                    logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} " +
                    $"выполнил редактирование профиля абитуриента: {enrolleeCard.enrollee.RuSurname} {enrolleeCard.enrollee.RuName}.");
                    // Обновляем список абитуриентов
                    InitializeEnrolleeGrid(SearchMode);
                }
            }
        }
        /// <summary>
        /// Удаление выбранного абитуриента
        /// </summary>
        /// <param name="sender">Кнопка вызова удаления абитуриента</param>
        /// <param name="e"></param>
        private void DeleteCurrentEnrollee(object sender, EventArgs e)
        {
            // Диалоговое окно о серьезности намерения (:
            DialogResult youSure = MessageBox.Show(this, "Удалить выбранного абитуриента?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                // Получаем уникальный идентификатор выбранного абитуриента
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                // Поиск абитуриента
                enrollee = enrolleeService.GetEnrollee(id);
                // Получаем список оценок абитуриента
                var assessments = assessmentService.GetAssessments(enrollee);
                // Удаляем оценки
                foreach (var assessment in assessments)
                    assessmentService.DeleteAssessment(assessment);
                // Получаем список атрибутов (льгот) абитуриента
                var atributes = atributeForEnrolleeService.GetAtributeForEnrollees(enrollee);
                // Удаляем атрибуты абитуриента
                foreach (var atribute in atributes)
                    atributeForEnrolleeService.DeleteAtributeForEnrollee(atribute);
                // Получаем список приоритетов
                var priorities = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee);
                // Удаляем список приоритетов абитуриента
                foreach (var priority in priorities)
                    priorityOfSpecialityService.DeletePriorityOfSpeciality(priority);
                // Удаляем абитуриента
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} выполняет удаление профиля абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                enrolleeService.DeleteEnrollee(enrollee);
                logger.Info($"Профиль абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} удален пользователем {activeEmployee.Fullname.Trim()}.");
                // Обновляем список абитуриентов
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Печать заявления абитуриента
        /// </summary>
        /// <param name="sender">Кнопка печать заявления</param>
        /// <param name="e"></param>
        private void PrintStatement(object sender, EventArgs e)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем уникальный идентификатор выбранного абитуриента
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            // Поиск абитуриента
            enrollee = enrolleeService.GetEnrollee(id);
            // Подготавливаем заявление абитуриента
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает заявление для абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
            ReportManager.PrintStatement(enrollee);
        }
        /// <summary>
        /// Печать титульного листа абитуриента
        /// </summary>
        /// <param name="sender">Кнопка печать титульного листа</param>
        /// <param name="e"></param>
        private void PrintTitle(object sender, EventArgs e)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем уникальный идентификатор выбранного абитуриента
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            // Поиск абитуриента
            enrollee = enrolleeService.GetEnrollee(id);
            // Подготавливаем титульный лист абитуриента
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает титульный лист для абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
            ReportManager.PrintTitle(enrollee);
        }
        /// <summary>
        /// Печать экзаменнационного листа абитуриента
        /// </summary>
        /// <param name="sender">Кнопка печать экзаменнационного листа</param>
        /// <param name="e"></param>
        private void PrintExamSheet(object sender, EventArgs e)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем уникальный идентификатор выбранного абитуриента
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            // Поиск абитуриента
            enrollee = enrolleeService.GetEnrollee(id);
            // Подготавливаем экзаменнационный лист абитуриента
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает экзаменнационный лист для абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
            ReportManager.PrintExamSheet(enrollee);
        }
        /// <summary>
        /// Печать расписки абитуриента
        /// </summary>
        /// <param name="sender">Кнопка печать расписки</param>
        /// <param name="e"></param>
        private void PrintReceipt(object sender, EventArgs e)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем уникальный идентификатор выбранного абитуриента
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            // Поиск абитуриента
            enrollee = enrolleeService.GetEnrollee(id);
            // Форма подготовка отчёта расписки
            // enrollee — абитуриент
            frmReceipt receiptCard = new frmReceipt();
            DialogResult receiptCardResult = receiptCard.ShowDialog();
            if(receiptCardResult == DialogResult.OK)
            {
                // Подготовка расписки к печати
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает расписку для абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                ReportManager.PrintReceipt(enrollee, receiptCard.DocumentOfStudy, receiptCard.DocumentOfDiscount, receiptCard.DocumentOther);
            }
        }
        /// <summary>
        /// Печать извещения абитуриента
        /// </summary>
        /// <param name="sender">Кнопка печать извещения</param>
        /// <param name="e"></param>
        private void PrintNotice(object sender, EventArgs e)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем уникальный идентификатор выбранного абитуриента
            int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
            // Поиск абитуриента
            enrollee = enrolleeService.GetEnrollee(id);
            // Список оценок
            var assessments = assessmentService.GetAssessments(enrollee);
            // Проверяем наличие внутреннего испытания у абитуриента
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
            // Если абитуриент сдаёт вступительное испытание в вузе 
            // Подготовка извещения к печати
            if (!flag) MessageBox.Show(this, "Абитуриент не сдаёт вступительные испытания в университете", "Печать извещения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает извещение для абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                ReportManager.PrintNotice(enrollee);
            }
        }
        /// <summary>
        /// Метод фильтрации в списке абитуриентов
        /// </summary>
        /// <param name="sender">Обработчик фильтра</param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Обработчик выхода из системы пользователя
        /// </summary>
        /// <param name="sender">Кнопка "Выход из системы"</param>
        /// <param name="e"></param>
        private void LogoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} выполнил выход из системы.");
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
        /// <summary>
        /// Вызов формы ввода оценок вступительных испытаний
        /// </summary>
        /// <param name="sender">Кнопка "Вступительные испытания"</param>
        /// <param name="e"></param>
        private void EntryExamView(object sender, EventArgs e)
        {
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} выполнил вход в управление вступительными испытаниями.");
            frmEnrtyExam entryExamView = new frmEnrtyExam(activeEmployee);
            entryExamView.ShowDialog();
        }
        /// <summary>
        /// Вызов формы зачисления абитуриентов
        /// </summary>
        /// <param name="sender">Кнопка "Зачисление абитуриентов"</param>
        /// <param name="e"></param>
        private void EnrollView(object sender, EventArgs e)
        {
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} выполнил вход в управление зачислением абитуриентов.");
            frmEnroll enrollView = new frmEnroll(activeEmployee);
            enrollView.ShowDialog();
            InitializeEnrolleeGrid(SearchMode);
        }
        /// <summary>
        /// Печать "Сводной экзаменнационной ведомости"
        /// </summary>
        /// <param name="sender">Кнопка "Сводная экзаменнационная ведомость"</param>
        /// <param name="ea"></param>
        private void PrintSummaryExaminationReport(object sender, EventArgs ea)
        {
            // Вызов формы выбора специальности
            frmChooseSpeciality chooseSpeciality = new frmChooseSpeciality();
            if (chooseSpeciality.ShowDialog() == DialogResult.OK)
            {
                // Передаем строку подключения к источнику данных
                ReportManager.ConnectionString = connectionString;
                // Получение списка абитуриентов
                var enrollees = enrolleeService.GetEnrollees(chooseSpeciality.speciality)
                    .OrderByDescending(e => e.ReasonForAddmission.ContestId)
                    .ThenByDescending(e => e.Assessment.Sum(a => a.Estimation))
                    .ToList();
                // Подготовка отчёта сводной экзаменнационной ведомости
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает сводную экзаменнационную ведомость специальности {chooseSpeciality.speciality.Fullname.Trim()}.");
                ReportManager.PrintSummaryExaminationSheet(enrollees, chooseSpeciality.speciality);
            }
        }
        /// <summary>
        /// Печать "Экзаменнационная ведомость"
        /// </summary>
        /// <param name="sender">Кнопка "Экзаменнационная ведомость"</param>
        /// <param name="ea"></param>
        private void PrintExaminationSheet(object sender, EventArgs e)
        {
            // Вызов формы выбора специальности
            frmChooseSpeciality chooseSpeciality = new frmChooseSpeciality();
            if (chooseSpeciality.ShowDialog() == DialogResult.OK)
            {
                // Передаем строку подключения к источнику данных
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
                    // Получаем список абитуриентов сдающих вступительные испытания
                    var enrollees = enrolleeService.GetEnrollees(chooseSpeciality.speciality);
                    // Подготовка отчёта экзаменнационной ведомости
                    logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает экзаменнационную ведомость специальности {chooseSpeciality.speciality.Fullname.Trim()}.");
                    ReportManager.PrintExaminationSheet(exams, enrollees);
                }
                else
                {
                    MessageBox.Show(this, "Абитуриенты данной специальности не сдают вступительные испытания в университете", "Печать экзаменнационного листа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        /// <summary>
        /// Печать мониторинга (Бюджет)
        /// </summary>
        /// <param name="sender">Кнопка печати мониторинга</param>
        /// <param name="ea"></param>
        private void PrintMonitoringBudget(object sender, EventArgs ea)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем список абитуриентов
            var enrollees = enrolleeService.GetEnrollees();
            // Подготовка отчёта
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает информацию о ходе приема на бюджет.");
            ReportManager.PrintBudgetMonitoring(enrollees);
        }
        /// <summary>
        /// Печать мониторинга (Платное)
        /// </summary>
        /// <param name="sender">Кнопка печати мониторинга</param>
        /// <param name="ea"></param>
        private void PrintMonitoringFee(object sender, EventArgs ea)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем список абитуриентов
            var enrollees = enrolleeService.GetEnrollees();
            // Подготовка отчёта
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает информацию о ходе приема на платной основе.");
            ReportManager.PrintFeeMonitoring(enrollees);
        }
        /// <summary>
        /// Печать информации о ходе приема документов
        /// </summary>
        /// <param name="sender">Кнопка печати информации о ходе приема</param>
        /// <param name="ea"></param>
        private void PrintInformationReport(object sender, EventArgs ea)
        {
            // Передаем строку подключения к источнику данных
            ReportManager.ConnectionString = connectionString;
            // Получаем список абитуриентов
            var enrollees = enrolleeService.GetEnrollees();
            // Подготовка отчёта
            logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} печатает информацию о ходе приема.");
            ReportManager.PrintInformationReport(enrollees);
        }
        /// <summary>
        /// Вызов контекстного меню на таблице данных абитуриентов
        /// </summary>
        /// <param name="sender">Таблица данных</param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Обработчик открытия контекстного меню
        /// </summary>
        /// <param name="sender">Контекстное меню</param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Установка состояния контекстного меню таблицы абитуриентов
        /// </summary>
        /// <param name="enrollee"></param>
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
        /// <summary>
        /// Изменение типа финансирования "Бюджет/Платно" абитуриента
        /// </summary>
        /// <param name="sender">Кнопка типа финансирования "Бюджет/Платно"</param>
        /// <param name="e"></param>
        private void BudgetAndFeeContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.FinanceTypeId = 3;
                enrolleeService.UpdateEnrollee(enrollee);
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} изменил тип финансирования абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} на Бюджет/Платно.");
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Изменение типа финансирования "Бюджет" абитуриента
        /// </summary>
        /// <param name="sender">Кнопка типа финансирования "Бюджет"</param>
        /// <param name="e"></param>
        private void BudgetContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.FinanceTypeId = 1;
                enrolleeService.UpdateEnrollee(enrollee);
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} изменил тип финансирования абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} на бюджет.");
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Изменение типа финансирования "Платно" абитуриента
        /// </summary>
        /// <param name="sender">Кнопка типа финансирования "Платно"</param>
        /// <param name="e"></param>
        private void FeeContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип финансирования выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.FinanceTypeId = 2;
                enrolleeService.UpdateEnrollee(enrollee);
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} изменил тип финансирования абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} на платно.");
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Изменение статуса абитурента на "Кандидат"
        /// </summary>
        /// <param name="sender">Кнопка статуса абитурента "Кандидат"</param>
        /// <param name="e"></param>
        private void CandidateContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип состояния выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.StateTypeId = 1;
                enrolleeService.UpdateEnrollee(enrollee);
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} изменил тип состояния абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} на кандидат.");
                InitializeEnrolleeGrid(SearchMode);
            }
        }
        /// <summary>
        /// Изменение статуса абитурента на "Забрал документы"
        /// </summary>
        /// <param name="sender">Кнопка статуса абитурента "Забрал документы"</param>
        /// <param name="e"></param>
        private void TookDocumentContextMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show(this, "Изменить тип состояния выбранного абитуриента?", "Редактирование записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (youSure == DialogResult.Yes)
            {
                int id = Int32.Parse(EnrolleeGrid.CurrentRow.Cells[0].Value.ToString());
                enrollee = enrolleeService.GetEnrollee(id);
                enrollee.StateTypeId = 2;
                enrolleeService.UpdateEnrollee(enrollee);
                logger.Info($"Пользователь {activeEmployee.Fullname.Trim()} изменил тип состояния абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} на забрал документы.");
                InitializeEnrolleeGrid(SearchMode);
            }
        }
    }
}

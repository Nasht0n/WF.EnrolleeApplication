using NLog;
using System;
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
    /// Класс-форма "Зачисление абитуриентов" 
    /// </summary>
    public partial class frmEnroll : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Таблица данных абитуриентов
        private DataTable enrolleeTable;
        // Строка подключения к источнику данных
        private string connectionString;
        // Пользователь системы
        private Employee employee;
        // Выбранный факультет
        private Faculty faculty;
        // Выбранная форма обучения
        private FormOfStudy formOfStudy;
        // Выбранная специальность
        private Speciality speciality;
        // Текущий абитуриент
        private Enrollee enrollee;
        // Выбранный приказ о зачислении
        private Decree decree;
        // Выбранная специальность из списка приоритетов
        private Speciality priority;
        // Сервис доступа к данным абитуриентов
        private EnrolleeService enrolleeService;
        // Сервис доступа к данным факультетов
        private FacultyService facultyService;
        // Сервис доступа к данным форм обучения
        private FormOfStudyService formOfStudyService;
        // Сервис доступа к данным специальностей
        private SpecialityService specialityService;
        // Сервис доступа к данным специальностей списка приоритетов
        private PriorityOfSpecialityService priorityOfSpecialityService;
        // Сервис доступа к данным приказов
        private DecreeService decreeService;
        // Сервис доступа к данным представлений
        private ViewService viewService;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public frmEnroll(Employee activeEmployee)
        {
            InitializeComponent();
            this.employee = activeEmployee;
            // Создаем структуру таблицы данных
            enrolleeTable = CreateStructure();
            // Задаем таблицу данных как источник в компоненте
            EnrolleeGrid.DataSource = enrolleeTable;
            // Инициализируем сервисы доступа к данным
            InitializeDataAccessServices();
            // Инициализируем выпадающие списки данными
            InitializeComboBox();
            // Устанавливаем стиль отображения таблицы абитуриентов
            SetGridStyle();
        }
        /// <summary>
        /// Метод установки стиля отображения таблицы абитуриентов
        /// </summary>
        private void SetGridStyle()
        {
            // Устанавливаем видимость
            EnrolleeGrid.Columns[0].Visible = false;
            // Устанавливаем ширину (веса) столбцов
            EnrolleeGrid.Columns[1].FillWeight = 15;
            EnrolleeGrid.Columns[2].FillWeight = 15;
            EnrolleeGrid.Columns[3].FillWeight = 40;
            EnrolleeGrid.Columns[4].FillWeight = 15;
            EnrolleeGrid.Columns[5].FillWeight = 15;
        }
        /// <summary>
        /// Метод создания структуры таблицы данных
        /// </summary>
        /// <returns>Таблица данных абитуриента</returns>
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
        /// <summary>
        /// Метод инициализации сервисов доступа к данным
        /// </summary>
        private void InitializeDataAccessServices()
        {
            // Получение строки подключения
            logger.Info($"Получаем строку подключения к источнику данных");
            connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            // Инициализация сервисов получения данных
            logger.Info($"Инициализация сервисов получения данных");
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            priorityOfSpecialityService = new PriorityOfSpecialityService(connectionString);
            decreeService = new DecreeService(connectionString);
            enrolleeService = new EnrolleeService(connectionString);
            viewService = new ViewService(connectionString);
        }
        /// <summary>
        /// Метод инициализации выпадающих списков формы
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
                formOfStudy = formsOfStudies[0];
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения формы обучения в списке форм обучения
            cbFormOfStudy.SelectedValueChanged += cbFormOfStudy_SelectedValueChanged;
        }
        /// <summary>
        /// Метод инициализации выпадающего списка факультетов
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
                faculty = faculties[0];
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения факультета в списке факультетов
            cbFaculty.SelectedValueChanged += cbFaculty_SelectedValueChanged;
        }
        /// <summary>
        /// Метод инициализации выпадающего списка специальностей
        /// </summary>
        private void InitializeSpecialityComboBox()
        {
            // Если выбраны текущие факультет и форма обучения
            if (faculty != null && formOfStudy != null)
            {
                // Отключаем отслеживание изменения специальности в списке специальностей
                cbSpeciality.SelectedValueChanged -= cbSpeciality_SelectedValueChanged;
                // Получаем список специальностей
                var specialities = specialityService.GetSpecialities(faculty, formOfStudy);
                // Инициализация комбо-бокса
                cbSpeciality.DataSource = specialities;
                cbSpeciality.DisplayMember = "Fullname";
                cbSpeciality.ValueMember = "SpecialityId";
                // Если список специальностей не пуст, инициализируем выбранную специальность - первой из списка
                // Инициализируем список абитуриентов
                if (specialities.Count != 0)
                {
                    speciality = specialities[0];
                    InitializeEnrolleeDataGrid();
                }
                // Включаем отслеживание изменения специальности в списке специальностей
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }
        /// <summary>
        /// Метод инициализации списка абитуриентов
        /// </summary>
        private void InitializeEnrolleeDataGrid()
        {
            enrolleeTable.Clear();
            // Получение списка абитуриентов
            var enrollees = enrolleeService.GetEnrollees(speciality).OrderByDescending(e => e.ReasonForAddmission.ContestId)
                    .ThenByDescending(e => e.Assessment.Sum(a => a.Estimation))
                    .ToList();
            // Заполнение таблицы абитуриентов
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
            if (cbFaculty.SelectedValue !=null)
            {
                int id = (int)cbFaculty.SelectedValue;
                faculty = facultyService.GetFaculty(id);
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
            if (cbFormOfStudy.SelectedValue!=null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
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
            if (cbSpeciality.SelectedValue != null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                speciality = specialityService.GetSpeciality(id);
                InitializeEnrolleeDataGrid();
            }
        }
        /// <summary>
        /// Обработчик выбора абитуриента из таблицы
        /// </summary>
        /// <param name="sender">Таблица данных абитуриентов</param>
        /// <param name="e"></param>
        private void EnrolleeGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Если выделена хоть одна строка
            if (EnrolleeGrid.SelectedCells.Count != 0)
            {
                int index = EnrolleeGrid.SelectedCells[0].RowIndex;
                DataGridViewRow row = EnrolleeGrid.Rows[index];
                // Получение уникального идентификатора
                int id = Int32.Parse(row.Cells[0].Value.ToString());
                // Поиск абитуриента по уникальному идентификатору
                enrollee = enrolleeService.GetEnrollee(id);
                // Инициализация панели зачисления абитуриента
                InitializeEnrollPanel();
            }
        }
        /// <summary>
        /// Метод инициализации панели зачисления абитуриента
        /// </summary>
        private void InitializeEnrollPanel()
        {
            // Если выбран абитуриент
            if (enrollee != null)
            {
                // Меняем заголовок Панели 
                string fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} {enrollee.RuPatronymic.Trim()}";
                gbEnrollPanel.Text = fullname;
                // Инициализируем выпадающий список приказов
                InitializeDecreeComboBox();
                // Инициализируем выпадающий список специальностей приоритета абитуриентов
                InitializePriorityComboBox(enrollee);
            }
        }
        /// <summary>
        /// Метод инициализации списка специальностей приоритетов абитуриента
        /// </summary>
        /// <param name="enrollee">Зачисляемый абитуриент</param>
        private void InitializePriorityComboBox(Enrollee enrollee)
        {
            // Отключаем отслеживание изменения специальности-приоритета в списке специальностей
            cbPriority.SelectedValueChanged -= cbPriority_SelectedValueChanged;
            // Получаем список приоритетов абитуриента
            var priorities = viewService.GetPriorities(enrollee);
            // Заполняем комбо-бокс
            cbPriority.DataSource = priorities;
            cbPriority.DisplayMember = "Fullname";
            cbPriority.ValueMember = "SpecialityId";
            // Если список приоритетов не пуст
            // Устанавливаем выбранный приоритет равный первому элементу списка
            if (priorities.Count != 0)
            {
                int id = priorities[0].SpecialityId;
                priority = specialityService.GetSpeciality(id);
            }
            // Включаем отслеживание изменения специальности-приоритета в списке специальностей
            cbPriority.SelectedValueChanged += cbPriority_SelectedValueChanged;
        }
        /// <summary>
        /// Метод инициализации списка приказов
        /// </summary>
        private void InitializeDecreeComboBox()
        {
            // Отключаем отслеживание изменения приказа
            cbDecree.SelectedValueChanged -= cbDecree_SelectedValueChanged;
            // Получаем список приказов
            var decrees = decreeService.GetDecrees();
            // Заполняем комбо-бокс
            cbDecree.DataSource = decrees;
            cbDecree.DisplayMember = "DecreeNumber";
            cbDecree.ValueMember = "DecreeId";
            // Если список приказов не пуст
            // Устанавливаем выбранный приказ первым элементом списка
            if(decrees.Count!= 0)decree = decrees[0];
            // Включаем отслеживание изменения приказа
            cbDecree.SelectedValueChanged += cbDecree_SelectedValueChanged;
        }
        /// <summary>
        /// Обработчик выбора приказа
        /// </summary>
        /// <param name="sender">Выпадающий список приказов</param>
        /// <param name="e"></param>
        private void cbDecree_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список приказов не пуст
            // Получаем уникальный идентификатор выбранного приказа
            // Получаем приказ по уникальному идентификатору
            if (cbDecree.SelectedValue!=null)
            {
                int id = (int)cbDecree.SelectedValue;
                decree = decreeService.GetDecree(id);
            }
        }
        /// <summary>
        /// Обработчик выбора специальности из списка приоритетов абитуриента
        /// </summary>
        /// <param name="sender">Выпадающий список специальностей</param>
        /// <param name="e"></param>
        private void cbPriority_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список приоритетов не пуст
            // Получаем уникальный идентификатор выбранной специальности из списка приоритетов
            // Получаем специальность по уникальному идентификатору
            if (cbPriority.SelectedValue != null)
            {
                int id = (int)cbPriority.SelectedValue;
                priority = specialityService.GetSpeciality(id);
            }
        }
        /// <summary>
        /// Метод получения последнего номера личного дела специальности
        /// </summary>
        /// <param name="speciality">Выбранная специальность</param>
        /// <returns></returns>
        private int GetNumberOfDeal(Speciality speciality)
        {
            int result = 0;
            // Получаем список абитуриентов специальности
            var enrollees = enrolleeService.GetEnrollees(speciality).OrderBy(e => e.NumberOfDeal).ToList();
            if (enrollees.Count == 0)
            {
                // Если абитуриентов в списке нет, то номер личного дела = 1
                result = 1;
            }
            else
            {
                // Делаем +1 к последнему
                result = enrollees[enrollees.Count - 1].NumberOfDeal + 1;
            }
            return result;
        }
        /// <summary>
        /// Обработчик нажатия кнопки зачислить абитуриента
        /// </summary>
        /// <param name="sender">Кнопка "Зачислить абитуриента"</param>
        /// <param name="e"></param>
        private void btEnrollCurrentEnrollee_Click(object sender, EventArgs e)
        {
            string fullname = $"";
            if(string.IsNullOrWhiteSpace(enrollee.RuPatronymic)) fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}.";
            else fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}. {enrollee.RuPatronymic[0]}.";
            // Диалоговое окно
            if (MessageBox.Show(this,$"Произвести зачисление абитуриента: {fullname} в число студентов?","Зачисление абитуриентов",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Указание приказа зачисления
                enrollee.DecreeId = decree.DecreeId;
                // Записываем данные до зачисления
                enrollee.BeforeEnrollNumberOfDeal = enrollee.NumberOfDeal;
                enrollee.BeforeEnrollSpecialityId = enrollee.SpecialityId;
                // Указываем состояние абитуриента
                enrollee.StateTypeId = 3;
                // Указываем дату измениния статуса
                enrollee.StateDateChange = DateTime.Now;
                // Устанавливаем специальность и номер личного дела
                enrollee.SpecialityId = priority.SpecialityId;
                enrollee.NumberOfDeal = GetNumberOfDeal(priority);
                // Зачисление абитуриента
                enrolleeService.UpdateEnrollee(enrollee);
                logger.Info($"Пользователь {employee.Fullname.Trim()} выполнил зачисление абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} по приказу {decree.DecreeNumber.Trim()}.");
                InitializeEnrolleeDataGrid();
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки отмена абитуриента
        /// </summary>
        /// <param name="sender">Кнопка "Отменить зачисления"</param>
        /// <param name="e"></param>
        private void btCancelEnrollCurrentEnrollee_Click(object sender, EventArgs e)
        {
            // Если абитуриент выбран
            if (enrollee != null)
            {
                string fullname = $"";
                if (string.IsNullOrWhiteSpace(enrollee.RuPatronymic)) fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}.";
                else fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}. {enrollee.RuPatronymic[0]}.";
                if (MessageBox.Show(this, $"Отменить зачисление абитуриента: {fullname}?", "Зачисление абитуриентов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    logger.Info($"Пользователь {employee.Fullname.Trim()} пытается отменить зачисление абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                    CancelEnroll(enrollee);                   
                    InitializeEnrolleeDataGrid();
                }               
            }
        }
        /// <summary>
        /// Отмена зачисления абитуриента
        /// </summary>
        /// <param name="enrollee">Текущий абитуринт</param>
        private void CancelEnroll(Enrollee enrollee)
        {
            // Если абитуриент зачислен
            if (enrollee.DecreeId.HasValue)
            {
                // Убираем приказ
                enrollee.DecreeId = null;
                if (enrollee.BeforeEnrollSpecialityId.HasValue) enrollee.SpecialityId = enrollee.BeforeEnrollSpecialityId.Value;
                if (enrollee.BeforeEnrollNumberOfDeal.HasValue) enrollee.NumberOfDeal = enrollee.BeforeEnrollNumberOfDeal.Value;
                // Состояние в кандидат
                enrollee.StateTypeId = 1;
                enrollee.StateDateChange = DateTime.Now;
                // Обнуляем данные зачисления
                enrollee.BeforeEnrollNumberOfDeal = null;
                enrollee.BeforeEnrollSpecialityId = null;
                // Отмена зачисления
                enrolleeService.UpdateEnrollee(enrollee);
                logger.Info($"Пользователь {employee.Fullname.Trim()} отменил зачисление абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки отмена
        /// </summary>
        /// <param name="sender">Кнопка "Отмена"</param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            logger.Info($"Пользователь {employee.Fullname.Trim()} вышел из управление зачислением.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Обработчик отмены зачисления абитуриентов специальности
        /// </summary>
        /// <param name="sender">Кнопка "Отменить зачисления"</param>
        /// <param name="ea"></param>
        private void btClear_Click(object sender, EventArgs ea)
        {
            // Получение списка абитуриентов
            var enrollees = enrolleeService.GetEnrollees(speciality).Where(e=>e.StateTypeId!=2).ToList();
            // Если список не пуст
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
        /// <summary>
        /// Подготовка печати выписки зачисленных абитуриентов
        /// </summary>
        /// <param name="sender">Кнопка "Печать"</param>
        /// <param name="ea"></param>
        private void btReport_Click(object sender, EventArgs ea)
        {
            // Список зачисленных абитуриентов специальности
            var enrollees = enrolleeService.GetEnrollees(speciality).Where(e => e.StateTypeId != 2).ToList();
            if (enrollees.Count != 0)
            {
                ReportManager.ConnectionString = connectionString;
                // Подготовка выписки
                logger.Info($"Пользователь {employee.Fullname.Trim()} выполняет печать выписки.");
                ReportManager.PrintExtract(enrollees);
            }
            else
            {
                MessageBox.Show($"Ошибка печати: Нет зачисленных абитуриентов.");
            }
        }

        private void cbPriority_Format(object sender, ListControlConvertEventArgs e)
        {
            PriorityView priority = (PriorityView)e.ListItem;
            var speciality = specialityService.GetSpeciality(priority.SpecialityId);
            string shortFormOfStudy = speciality.FormOfStudy.Shortname.Trim();
            if (string.IsNullOrWhiteSpace(shortFormOfStudy)) e.Value = $"{priority.Fullname.Trim()}";
            else e.Value = $"{priority.Fullname.Trim()} -{speciality.FormOfStudy.Shortname.Trim()}";
        }
    }
}

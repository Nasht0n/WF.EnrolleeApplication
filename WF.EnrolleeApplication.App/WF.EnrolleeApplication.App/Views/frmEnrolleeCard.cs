using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WF.EnrolleeApplication.App.Services;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Views
{
    /// <summary>
    /// Класс-формы "Регистрация/Редактирование абитуриента профиля"
    /// </summary>
    public partial class frmEnrolleeCard : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Выбранная область
        private Area area;
        // Объект атрибута (льготы)
        private Atribute atribute;
        // Выбранное гражданство
        private Citizenship citizenship;
        // Выбранный тип конкурса
        private Contest contest;
        // Выбранная страна
        private Country country;
        // Объект приказа о зачислении
        private Decree decree;
        // Выбранный район
        private District district;
        // Выбранный тип документа
        private Document document;
        // Текущий пользователь системы
        private Employee activeEmployee;
        // Добавляемая/Редактируемая запись абитуриента
        public Enrollee enrollee;
        // Выбранный факультет
        private Faculty faculty;
        // Выбранный иностранный язык
        private ForeignLanguage foreignLanguage;
        // Выбранная форма обучения
        private FormOfStudy formOfStudy;
        // Выбранное основание зачисления
        private ReasonForAddmission reasonForAddmission;
        // Выбранная специальность второй ступени
        private SecondarySpeciality secondarySpeciality;
        // Выбранная специальность
        private Speciality speciality;
        // Выбранное место работы по целевому направлению
        private TargetWorkPlace targetWorkPlace;
        // Выбранный тип финансирования
        private TypeOfFinance typeOfFinance;
        // Выбранный тип учебного заведения
        private TypeOfSchool typeOfSchool;
        // Выбранный тип населенного пункта
        private TypeOfSettlement typeOfSettlement;
        // Выбранный тип состояния абитуриента
        private TypeOfState typeOfState;
        // Выбранный тип улицы
        private TypeOfStreet typeOfStreet;
        // Сервис доступа к данным области
        private AreaService areaService;
        // Сервис доступа к оценкам абитуриента
        private AssessmentService assessmentService;
        // Сервис доступа к атрибутам (льготам)
        private AtributeService atributeService;
        // Сервис доступа к атрибутам (льготам) абитуриента
        private AtributeForEnrolleeService atributeForEnrolleeService;
        // Сервис доступа к видам гражданства
        private CitizenshipService citizenshipService;
        // Сервис доступа к типам конкурса
        private ContestService contestService;
        // Сервис доступа к системе конвертации оценок
        private ConversionSystemService conversionSystemService;
        // Сервис доступа к списку стран
        private CountryService countryService;
        // Сервис доступа к списку приказов
        private DecreeService decreeService;
        // Сервис доступа к списку дисциплин
        private DisciplineService disciplineService;
        // Сервис доступа к списку районов
        private DistrictService districtService;
        // Сервис доступа к списке типов документов
        private DocumentService documentService;
        // Сервис доступа к данным абитуриента
        private EnrolleeService enrolleeService;
        // Сервис доступа к данным экзаменнационных схем
        private ExamSchemaService examSchemaService;
        // Сервис доступа к данным о факультетах
        private FacultyService facultyService;
        // Сервис доступа к данным о иностранных языках
        private ForeignLanguageService foreignLanguageService;
        // Сервис доступа к данным о формах обучения
        private FormOfStudyService formOfStudyService;
        // Сервис доступа к данным об интеграции специальностей
        private IntegrationOfSpecialitiesService integrationOfSpecialitiesService;
        // Сервис доступа к данным об специальностях приоритета
        private PriorityOfSpecialityService priorityOfSpecialityService;
        // Сервис доступа к данным об основаниях зачисления
        private ReasonForAddmissionService reasonForAddmissionService;
        // Сервис доступа к данным об специальностях второй ступени
        private SecondarySpecialityService secondarySpecialityService;
        // Сервис доступа к данным об специальностях первой ступени
        private SpecialityService specialityService;
        // Сервис доступа к данным об местах работы по целевому направлению
        private TargetWorkPlaceService targetWorkPlaceService;
        // Сервис доступа к данным об типах финансирования
        private TypeOfFinanceService typeOfFinanceService;
        // Сервис доступа к данным об типах учебных заведений
        private TypeOfSchoolService typeOfSchoolService;
        // Сервис доступа к данным об типах населенных пунктов
        private TypeOfSettlementService typeOfSettlementService;
        // Сервис доступа к данным о типах состояния абитуриентов
        private TypeOfStateService typeOfStateService;
        // Сервис доступа к данным о типах улиц
        private TypeOfStreetService typeOfStreetService;
        // Таблицы данных "Приоритеты специальности" и "Оценки"          
        private DataTable priorityTable;
        private DataTable sertificateTable;
        // Режим редактирования
        private bool editMode;
        // Абитуриент заочник
        private bool IsWorker;
        // Абитуриент поступает на сокращенную форму обучения
        private bool HasSecondarySpeciality;
        // Абитуриент зачилен по приказу
        private bool HasEnroll;
        // Список абитуриентов
        private List<Enrollee> enrollees;
        /// <summary>
        /// Конструктор карты абитуриента. 
        /// Используется при добавлении (регистрации) нового абитуриента
        /// </summary>
        /// <param name="employee">Оператор, выполняющий регистрацию новой записи</param>
        public frmEnrolleeCard(Employee employee)
        {
            InitializeComponent();
            logger.Info($"Открыт сеанс добавления (регистрации) абитуриента. Оператор — {employee.Fullname.Trim()}.");
            // Запоминаем текущего пользователя
            this.activeEmployee = employee;
            // Создаем объект нового абитуриента
            this.enrollee = new Enrollee();
            // Отключаем режим редактирования
            this.editMode = false;
            // Создаем структуру таблиц
            priorityTable = CreateStructurePriorityTable();
            PriorityGrid.DataSource = priorityTable;
            sertificateTable = CreateStructureSertificateTable();
            SertificateGrid.DataSource = sertificateTable;
            // Инициализируем сервисы доступа к данным
            InitializeDataAccessServices();
            // Инициализируем выпадающие списки данными
            InitializeComboBoxes();
            // Инициализируем список льгот
            InitializeAtributeList();
            // Инициализируем автозаполнение полей
            InitializeAutoCompleteTextBoxes();
        }
        /// <summary>
        /// Конструктор карты абитуриента. 
        /// Используется при редактировании карты выбранного абитуриента
        /// </summary>
        /// <param name="employee">Оператор, выполняющий регистрацию новой записи</param>
        /// <param name="editEnrollee">Профиль редактируемого абитуриента</param>
        public frmEnrolleeCard(Employee employee, Enrollee editEnrollee)
        {
            InitializeComponent();
            logger.Info($"Открыт сеанс редактирования абитуриента. Оператор — {employee.Fullname.Trim()}.");
            // Запоминаем текущего пользователя
            this.activeEmployee = employee;
            // Запоминаем редактируемого абитуриента
            this.enrollee = editEnrollee;
            // Активируем режим редактирования
            this.editMode = true;
            // Создаем структуру таблиц
            priorityTable = CreateStructurePriorityTable();
            PriorityGrid.DataSource = priorityTable;
            sertificateTable = CreateStructureSertificateTable();
            SertificateGrid.DataSource = sertificateTable;
            // Инициализируем сервисы доступа к данным
            InitializeDataAccessServices();
            // Инициализируем выпадающие списки данными
            InitializeComboBoxes();
            // Инициализируем список льгот
            InitializeAtributeList();
            // Инициализируем автозаполнение полей
            InitializeAutoCompleteTextBoxes();
            // Заполняем поля данными редактируемого абитуриента
            FillEditData(editEnrollee);
        }
        /// <summary>
        /// Инициализация сервисов доступа к базе данных
        /// </summary>
        private void InitializeDataAccessServices()
        {
            // Получаем строку подключения к источнику данных
            logger.Debug("Получаем строку подключения к источнику данных");
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            // Инициализируем сервисы доступа к данным
            areaService = new AreaService(connectionString);
            assessmentService = new AssessmentService(connectionString);
            atributeService = new AtributeService(connectionString);
            atributeForEnrolleeService = new AtributeForEnrolleeService(connectionString);
            citizenshipService = new CitizenshipService(connectionString);
            contestService = new ContestService(connectionString);
            conversionSystemService = new ConversionSystemService(connectionString);
            countryService = new CountryService(connectionString);
            decreeService = new DecreeService(connectionString);
            disciplineService = new DisciplineService(connectionString);
            districtService = new DistrictService(connectionString);
            documentService = new DocumentService(connectionString);
            enrolleeService = new EnrolleeService(connectionString);
            examSchemaService = new ExamSchemaService(connectionString);
            facultyService = new FacultyService(connectionString);
            foreignLanguageService = new ForeignLanguageService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            integrationOfSpecialitiesService = new IntegrationOfSpecialitiesService(connectionString);
            priorityOfSpecialityService = new PriorityOfSpecialityService(connectionString);
            reasonForAddmissionService = new ReasonForAddmissionService(connectionString);
            secondarySpecialityService = new SecondarySpecialityService(connectionString);
            specialityService = new SpecialityService(connectionString);
            targetWorkPlaceService = new TargetWorkPlaceService(connectionString);
            typeOfFinanceService = new TypeOfFinanceService(connectionString);
            typeOfSchoolService = new TypeOfSchoolService(connectionString);
            typeOfSettlementService = new TypeOfSettlementService(connectionString);
            typeOfStateService = new TypeOfStateService(connectionString);
            typeOfStreetService = new TypeOfStreetService(connectionString);
        }
        /// <summary>
        /// Заполнение полей данными редактируемого абитуриента
        /// </summary>
        /// <param name="editEnrollee">Профиль редактируемого абитуриента</param>
        private void FillEditData(Enrollee editEnrollee)
        {
            // Заполняем поля формы, данными редактируемого абитуриента
            /* *********************************** *
             * Вкладка "Информация об абитуриенте" *
             * *********************************** */
            // Устанавливаем факультет абитуриента
            cbFaculty.SelectedValue = editEnrollee.Speciality.FacultyId;
            // Устанавливаем форму обучения абитуриента
            cbFormOfStudy.SelectedValue = editEnrollee.Speciality.FormOfStudyId;
            // Устанавливаем специальность абитуриента
            cbSpeciality.SelectedValue = editEnrollee.SpecialityId;
            // Заполняем поля ФИО на русском и белорусском языках
            tbRuSurname.Text = editEnrollee.RuSurname;
            tbRuName.Text = editEnrollee.RuName;
            tbRuPatronymic.Text = editEnrollee.RuPatronymic;
            tbBlrSurname.Text = editEnrollee.BlrSurname;
            tbBlrName.Text = editEnrollee.BlrName;
            tbBlrPatronymic.Text = editEnrollee.BlrPatronymic;
            // Устанавливаем дату рождения абитуриента
            dtBirthday.Value = editEnrollee.DateOfBirthday;
            // Устанавливаем пол абитуриента
            if (editEnrollee.Gender.Trim() == "М") rbMale.Checked = true;
            else rbFemale.Checked = true;
            // Устанавливаем гражданство абитуриента
            cbCitizenship.SelectedValue = editEnrollee.CitizenshipId;
            // Устанавливаем тип документа абитуриента
            cbDocument.SelectedValue = editEnrollee.DocumentId;
            // Заполняем поля данными предоставленного документа абитуриентом
            tbDocSeria.Text = editEnrollee.DocumentSeria;
            tbDocNumber.Text = editEnrollee.DocumentNumber;
            dtDocDate.Value = editEnrollee.DocumentDate;
            tbDocWhoGave.Text = editEnrollee.DocumentWhoGave;
            tbDocPersonalNumber.Text = editEnrollee.DocumentPersonalNumber;
            // Заполняем поля о родителях абитуриента
            tbFatherInfo.Text = editEnrollee.FatherFullname;
            tbFatherAdres.Text = editEnrollee.FatherAddress;
            tbMotherInfo.Text = editEnrollee.MotherFullname;
            tbMotherAdres.Text = editEnrollee.MotherAddress;
            /* **********************************  *
             * Вкладка "Дополнительная информация" *
             * **********************************  */
            // Устанавливаем страну абитуриента
            cbCountry.SelectedValue = editEnrollee.CountryId;
            // Устанавливаем область абитуриента
            cbArea.SelectedValue = editEnrollee.AreaId;
            // Устанавливаем район абитуриента
            cbDistrict.SelectedValue = editEnrollee.DistrictId;
            // Устанавливаем тип населенного пункта абитуриент
            cbTypeOfSettlement.SelectedValue = editEnrollee.SettlementTypeId;
            // Устанавливаем тип улицы абитуриента
            cbTypeOfStreet.SelectedValue = editEnrollee.StreetTypeId;
            // Заполняем поля данными определяющие местожительства и номера телефонов абитуриента
            tbSettlementName.Text = editEnrollee.SettlementName;
            tbSettlementIndex.Text = editEnrollee.SettlementIndex.ToString();
            tbStreetName.Text = editEnrollee.StreetName;
            tbNumberHouse.Text = editEnrollee.NumberHouse;
            tbNumberFlat.Text = editEnrollee.NumberFlat;
            tbMobilePhone.Text = editEnrollee.MobilePhone;
            tbHomePhone.Text = editEnrollee.HomePhone;
            // Устанавливаем тип последнего учебного заведения абитуриента
            cbTypeOfSchool.SelectedValue = editEnrollee.SchoolTypeId;
            // Заполняем поля данными последнего учебного заведения абитуриента
            tbSchoolYear.Text = editEnrollee.SchoolYear;
            tbSchoolAdres.Text = editEnrollee.SchoolAddress;
            tbSchoolName.Text = editEnrollee.SchoolName;
            // Если пользователь имеет запись о специальности второй ступени устанавливаем специальность второй ступени
            if (editEnrollee.SecondarySpecialityId.HasValue) cbSecondarySpeciality.SelectedValue = editEnrollee.SecondarySpecialityId;
            // В случае, когда абитуриент подает документы на второе высшее образование заполняем поля данными о текущем обучении
            if (!string.IsNullOrWhiteSpace(editEnrollee.CurrentNumberCurs) || !string.IsNullOrWhiteSpace(editEnrollee.CurrentSpeciality) || !string.IsNullOrWhiteSpace(editEnrollee.CurrentUniversity))
            {
                cbSecondEducation.Checked = true;
                tbCurrentNumberCurs.Text = editEnrollee.CurrentNumberCurs;
                tbCurrentSpeciality.Text = editEnrollee.CurrentSpeciality;
                tbCurrentUniversity.Text = editEnrollee.CurrentUniversity;
            }
            else cbSecondEducation.Checked = false;
            // Устанавливаем пункт о членстве в БРСМ
            cbBrsm.Checked = editEnrollee.IsBRSM;
            // Устанавливаем иностранный язык абитуриента
            cbForeignLanguage.SelectedValue = editEnrollee.ForeignLanguageId;
            // Если абитуриент зачислен, устанавливаем значение приказа о зачислении
            if (editEnrollee.DecreeId.HasValue) cbDecree.SelectedValue = editEnrollee.DecreeId;
            // Устанавливаем дату подачи документов
            dtDateDeal.Value = editEnrollee.DateDeal;
            // Если абитуриент, подающий документы на заочную форму обучения, работает заполняем сведения о рабочем месте и стаже работы
            if (IsWorker)
            {
                tbSeniority.Text = editEnrollee.Seniority.ToString();
                tbWorkPlace.Text = editEnrollee.WorkPlace;
                tbWorkPost.Text = editEnrollee.WorkPost;
            }
            /* ********************* *
             * Вкладка "Поступление" *
             * ********************* */
            // Устанавливаем тип финансирования
            cbTypeOfFinance.SelectedValue = editEnrollee.FinanceTypeId;
            // Устанавливаем тип конкурса 
            cbContest.SelectedValue = editEnrollee.ReasonForAddmission.ContestId;
            // Устанавливаем тип основания зачисления
            cbReasonForAddmission.SelectedValue = editEnrollee.ReasonForAddmissionId;
            // Заполняем поля оценками документа (-ов), предоставленного (-ыми) абитуриентом
            tbFirstAttestatString.Text = editEnrollee.AttestatEstimationString;
            tbSecondAttestatString.Text = editEnrollee.AttestatEstimationString;
            tbFirstDiplomPtuString.Text = editEnrollee.DiplomPtuEstimationString;
            tbSecondDiplomPtuString.Text = editEnrollee.DiplomPtuEstimationString;
            tbFirstDiplomSsuzString.Text = editEnrollee.DiplomSusEstimationString;
            tbSecondDiplomSsuzString.Text = editEnrollee.DiplomSusEstimationString;
            // Устанавливаем данные о целевом направлении
            if (editEnrollee.TargetWorkPlaceId.HasValue)
            {
                cbTarget.Checked = true;
                cbTargetWorkPlace.SelectedValue = editEnrollee.TargetWorkPlaceId;
            }
            else
            {
                cbTarget.Checked = false;
            }
            // Отображаем и заполняем поле номера личного дела абитуриента
            tbNumberOfDeal.Visible = true;
            tbNumberOfDeal.Text = editEnrollee.NumberOfDeal.ToString();
            // Устанавливаем тип состояния абитуриента
            cbTypeOfState.SelectedValue = editEnrollee.StateTypeId;
            // Заполняем поле лица ответственного за приём документов
            tbPersonInCharge.Text = editEnrollee.PersonInCharge;
            // Инициализируем таблицу оценок абитуриента
            InitializeSertificationGrid(editEnrollee);
            // Инициализируем таблицу специальностей согласно приоритетам абитуриента
            InitializePrioritySpecialityGrid(editEnrollee);
            // Инициализируем список атрибутов (льгот) абитуриента
            InitializeAtributeList(editEnrollee);
            // Убираем возможно редактировать специальность абитуриента
            cbFaculty.Enabled = false;
            cbFormOfStudy.Enabled = false;
            cbSpeciality.Enabled = false;
        }
        #region Создание, настройка и заполнение таблиц данных
        /// <summary>
        /// Установка стиля отображения таблицы сертификатов (дисциплин)
        /// </summary>
        private void SetSertificateTableStyle()
        {
            SertificateGrid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            SertificateGrid.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            SertificateGrid.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            SertificateGrid.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            SertificateGrid.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            SertificateGrid.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            SertificateGrid.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            // Установка ширины (весов) столбцов оценок
            SertificateGrid.Columns[0].FillWeight = 10;
            SertificateGrid.Columns[1].FillWeight = 10;
            SertificateGrid.Columns[2].FillWeight = 60;
            SertificateGrid.Columns[3].FillWeight = 20;
            SertificateGrid.Columns[4].FillWeight = 20;
            SertificateGrid.Columns[5].FillWeight = 10;
            SertificateGrid.Columns[6].FillWeight = 10;
            // Установка видимости столбцов оценок
            SertificateGrid.Columns[0].Visible = false;
            SertificateGrid.Columns[1].Visible = false;
            SertificateGrid.Columns[5].Visible = false;
            SertificateGrid.Columns[6].Visible = false;
            // Установка столбца только для чтения
            SertificateGrid.Columns["Дисциплина"].ReadOnly = true;
            // Строки дисциплин вступительных испытаний только для чтения
            foreach(DataGridViewRow row in SertificateGrid.Rows)
            {
                int disciplineId = Int32.Parse(row.Cells[0].Value.ToString());
                var discipline = disciplineService.GetDiscipline(disciplineId);
                if(discipline.BasisForAssessingId == 2)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.ReadOnly = true;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.ReadOnly = false;
                }
            }
        }
        /// <summary>
        /// Создаем струтуру таблицы данных сертификатов и предметов
        /// </summary>
        /// <returns>Таблица данных сертификтов</returns>
        private DataTable CreateStructureSertificateTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn("КодДисциплины", typeof(int)));
            result.Columns.Add(new DataColumn("КодОснования", typeof(int)));
            result.Columns.Add(new DataColumn("Дисциплина", typeof(string)));
            result.Columns.Add(new DataColumn("Сертификат", typeof(string)));
            result.Columns.Add(new DataColumn("Оценка", typeof(string)));
            result.Columns.Add(new DataColumn("Дата сертификата", typeof(string)));
            result.Columns.Add(new DataColumn("Замена", typeof(string)));
            return result;
        }
        /// <summary>
        /// Установка стиля отображения таблицы приоритета специальности
        /// </summary>
        private void SetPriorityTableStyle()
        {
            // Установка видимости столбцов приоритета специальности
            PriorityGrid.Columns[2].Visible = false;
            // Установка ширины (весов) столбцов приоритета специальности
            PriorityGrid.Columns[0].FillWeight = 5;
            PriorityGrid.Columns[1].FillWeight = 20;
            PriorityGrid.Columns[2].FillWeight = 5;
            PriorityGrid.Columns[3].FillWeight = 70;
        }
        /// <summary>
        /// Создаем структуру таблицы данных списка приоритетов специальностей
        /// </summary>
        /// <returns>Таблица данных приоритетов специальностей</returns>
        private DataTable CreateStructurePriorityTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn(" ", typeof(string)));
            result.Columns.Add(new DataColumn("Форма обучения", typeof(string)));
            result.Columns.Add(new DataColumn("Код", typeof(string)));
            result.Columns.Add(new DataColumn("Специальность", typeof(string)));
            return result;
        }
        /// <summary>
        /// Заполнение таблицы приоритетов специальностей при добавлении нового абитуриента
        /// </summary>
        private void InitializePrioritySpecialityGrid()
        {
            // Установка списка приоритетов
            if (!editMode) // Для режима добавления абитуриента
            {
                // Проверяем выбрана ли специальность
                if (speciality != null)
                {
                    // Очищаем таблицу данных приоритетов
                    priorityTable.Rows.Clear();
                    // Если выбранная специальность общего конкурса
                    if (speciality.IsGroup) 
                    {
                        // Получаем список специальностей входящих в группу общего конкурса
                        var specialities = specialityService.GetSpecialities(speciality);
                        // Если список не пустой
                        if (specialities.Count != 0)
                        {
                            // Индекс приоритета
                            int lvl_priority = 1;
                            // Проход по всем специальностям
                            foreach (var specialityGroup in specialities)
                            {
                                // Получаем список интегрированных специальностей с текущей специальностью
                                var listIntegration = integrationOfSpecialitiesService.GetIntegrationOfSpecialities(specialityGroup);
                                // Если список не пуст
                                if (listIntegration.Count != 0)
                                {
                                    // Проход по специальностям интеграции
                                    foreach (var itemIntegration in listIntegration)
                                    {
                                        // Если выбранная специальность интеграции - специальность второй ступени абитуриента добавляем в список приоритетов абитуриента
                                        if (itemIntegration.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId)
                                        {
                                            priorityTable.Rows.Add(lvl_priority, specialityGroup.FormOfStudy.Fullname, specialityGroup.SpecialityId, specialityGroup.Fullname);
                                            lvl_priority++;
                                        }
                                    }
                                }
                                else
                                {
                                    // Если список интеграционных специальностей пуст
                                    // Добавляем в приоритеты специальности входящие в группу общего конкурса
                                    priorityTable.Rows.Add(lvl_priority, specialityGroup.FormOfStudy.Fullname, specialityGroup.SpecialityId, specialityGroup.Fullname);
                                    lvl_priority++;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Если специальность не общего конкурса, добавляем её в список приоритетов
                        priorityTable.Rows.Add(1, speciality.FormOfStudy.Fullname, speciality.SpecialityId, speciality.Fullname);
                    }
                }
                // Задаем стиль таблицы приоритетов
                SetPriorityTableStyle();
            }
        }
        /// <summary>
        /// Заполнение таблицы приоритетов специальностей выбранного абитуриента (При редактировании профиля абитуриента)
        /// </summary>
        /// <param name="enrollee">Профиль редактируемого абитуриента</param>
        private void InitializePrioritySpecialityGrid(Enrollee enrollee)
        {
            // Получаем список приоритетов абитуриента
            var priorities = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee).OrderBy(p => p.PriorityLevel).ToList();
            // Очищаем таблицу данных
            priorityTable.Rows.Clear();
            // Заполняем список приоритетов
            foreach (var priority in priorities)
                priorityTable.Rows.Add(priority.PriorityLevel, priority.Speciality.FormOfStudy.Fullname, priority.SpecialityId, priority.Speciality.Fullname);
            // Задаем стиль отображения таблицы приоритетов
            SetPriorityTableStyle();
        }
        /// <summary>
        /// Заполнение таблицы сертификатов (дисциплин) согласно экзаменнационной схеме
        /// </summary>
        private void InitializeSertificationGrid()
        {
            // Режим редактирования отключен
            if (!editMode)
            {
                // Получение списка дисциплин экзаменнационной схемы
                var schemas = examSchemaService.GetExamSchemas(speciality).Where(ex => ex.Discipline.BasisForAssessingId != 1).ToList();
                // Очистка таблицы сертификатов
                sertificateTable.Rows.Clear();
                // Если список не пуст
                if (schemas.Count != 0)
                {
                    // Проход по элементам экзаменнационной схемы
                    foreach (var schema in schemas)
                    {
                        // Поиск дисциплины
                        var discipline = disciplineService.GetDiscipline(schema.DisciplineId);
                        // Если дисциплина найдена и является группой дисциплин
                        if (discipline != null && discipline.IsGroup)
                        {
                            // Поиск альтернатив
                            var alternatives = disciplineService.GetDisciplines(discipline);
                            // Запись в таблицу данных об оценках
                            foreach (var alternative in alternatives)
                                sertificateTable.Rows.Add(alternative.DisciplineId, alternative.BasisForAssessingId, alternative.Name, "", "", "", "");
                        }
                        else
                        {
                            // Запись дисциплины в таблицу данных об оценках
                            sertificateTable.Rows.Add(discipline.DisciplineId, discipline.BasisForAssessingId, discipline.Name, "", "", "", "");
                        }
                    }
                }
                // Задаем стиль отображения таблицы сертификатов
                SetSertificateTableStyle();
            }
        }
        /// <summary>
        /// Заполнение таблицы сертификатов (дисциплин) выбранного абитуриента
        /// </summary>
        /// <param name="enrollee">Профиль редактируемого абитуриента</param>
        private void InitializeSertificationGrid(Enrollee enrollee)
        {
            // Получаем список оценок редактируемого абитуриента
            var assessments = assessmentService.GetAssessments(enrollee).Where(a => a.Discipline.BasisForAssessingId != 1).ToList();
            // Очистка таблицы данных 
            sertificateTable.Rows.Clear();
            // Запись оценок в таблицу сертификатов (оценок)
            foreach (var assessment in assessments)
                sertificateTable.Rows.Add(assessment.DisciplineId, assessment.Discipline.BasisForAssessingId, assessment.Discipline.Name, assessment.SertCode, assessment.Estimation, assessment.SertDate, assessment.ChangeDiscipline);
            // Задаем стиль отображения таблицы сертификатов (оценок)
            SetSertificateTableStyle();
        }
        #endregion
        #region Установка списка льгот
        /// <summary>
        /// Загружаем полный список льгот
        /// </summary>
        private void InitializeAtributeList()
        {
            // Получаем список атрибутов (льгот)
            var atributes = atributeService.GetAtributes();
            // Заполняем CheckBoxList данными
            foreach (var atribute in atributes)
                chkAtributeList.Items.Add(atribute.Fullname);
        }
        /// <summary>
        /// Загружаем льготы выбранного абитуриента
        /// </summary>
        /// <param name="enrollee">Профиль редактируемого абитуриента</param>
        private void InitializeAtributeList(Enrollee enrollee)
        {
            // Получаем список атрибутов (льгот) редактируемого абитуриента
            var atributes = atributeForEnrolleeService.GetAtributeForEnrollees(enrollee);
            // Отмечаем полученные льготы в списке атрибутов
            foreach (var atribute in atributes)
                SetCheckedAtribute(atribute.Atribute.Fullname.Trim(), true);
        }
        #endregion
        #region Настройка полей автозаполнения
        /// <summary>
        /// Настройка полей автозаполнения данными
        /// </summary>
        private void InitializeAutoCompleteTextBoxes()
        {
            // Источники автозаполнения данных
            // Фамилии на русском языке
            AutoCompleteStringCollection sourceSurnameRus = new AutoCompleteStringCollection();
            // Имена на русском языке
            AutoCompleteStringCollection sourceNameRus = new AutoCompleteStringCollection();
            // Отчества на русском языке
            AutoCompleteStringCollection sourcePatronymicRus = new AutoCompleteStringCollection();
            // Фамилии на белорусском языке
            AutoCompleteStringCollection sourceSurnameBlr = new AutoCompleteStringCollection();
            // Имена на белорусском языке
            AutoCompleteStringCollection sourceNameBlr = new AutoCompleteStringCollection();
            // Отчества на белорусском языке
            AutoCompleteStringCollection sourcePatronymicBlr = new AutoCompleteStringCollection();
            // Кем выдан документ 
            AutoCompleteStringCollection sourceDocumentWhoGave = new AutoCompleteStringCollection();
            // Местожительство родителей
            AutoCompleteStringCollection sourceMotherAdress = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceFatherAdress = new AutoCompleteStringCollection();
            // Населенный пункт
            AutoCompleteStringCollection sourceSettlement = new AutoCompleteStringCollection();
            // Тип улицы
            AutoCompleteStringCollection sourceStreet = new AutoCompleteStringCollection();
            // Наименование типа учебного заведения
            AutoCompleteStringCollection sourceSchoolName = new AutoCompleteStringCollection();
            // Местоположение типа учебного заведения
            AutoCompleteStringCollection sourceSchoolAdress = new AutoCompleteStringCollection();
            // Информация об обучении на втором высшем
            AutoCompleteStringCollection sourceCurrentUniversity = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceCurrentSpeciality = new AutoCompleteStringCollection();
            // Информация о рабочем месте
            AutoCompleteStringCollection sourceWorkPlace = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceWorkPost = new AutoCompleteStringCollection();
            // Лицо ответственное за прием документов
            AutoCompleteStringCollection sourcePersonInCharge = new AutoCompleteStringCollection();
            // Получаем список абитуриентов
            enrollees = enrolleeService.GetEnrollees();
            // Заполняем источники данными
            sourceSurnameRus.AddRange(enrollees.Select(s => s.RuSurname).Distinct().ToArray());
            sourceNameRus.AddRange(enrollees.Select(n => n.RuName).Distinct().ToArray());
            sourcePatronymicRus.AddRange(enrollees.Where(p => p.RuPatronymic != null).Select(p => p.RuPatronymic).Distinct().ToArray());
            sourceSurnameBlr.AddRange(enrollees.Select(s => s.BlrSurname).Distinct().ToArray());
            sourceNameBlr.AddRange(enrollees.Select(n => n.BlrName).Distinct().ToArray());
            sourcePatronymicBlr.AddRange(enrollees.Where(p => p.BlrPatronymic != null).Select(p => p.BlrPatronymic).Distinct().ToArray());
            sourceDocumentWhoGave.AddRange(enrollees.Select(d => d.DocumentWhoGave).Distinct().ToArray());
            sourceMotherAdress.Add(tbFatherAdres.Text);
            sourceFatherAdress.Add(tbMotherAdres.Text);
            sourceSettlement.AddRange(enrollees.Where(p => p.SettlementName != null).Select(p => p.SettlementName).Distinct().ToArray());
            sourceStreet.AddRange(enrollees.Where(p => p.StreetName != null).Select(p => p.StreetName).Distinct().ToArray());
            sourceSchoolName.AddRange(enrollees.Where(p => p.SchoolName != null).Select(p => p.SchoolName).Distinct().ToArray());
            sourceSchoolAdress.AddRange(enrollees.Where(p => p.SchoolAddress != null).Select(p => p.SchoolAddress).Distinct().ToArray());
            sourceCurrentUniversity.AddRange(enrollees.Where(p => p.CurrentUniversity != null).Select(p => p.CurrentUniversity).Distinct().ToArray());
            sourceCurrentSpeciality.AddRange(enrollees.Where(p => p.CurrentSpeciality != null).Select(p => p.CurrentSpeciality).Distinct().ToArray());
            sourceWorkPlace.AddRange(enrollees.Where(p => p.WorkPlace != null).Select(p => p.WorkPlace).Distinct().ToArray());
            sourceWorkPost.AddRange(enrollees.Where(p => p.WorkPost != null).Select(p => p.WorkPost).Distinct().ToArray());
            sourcePersonInCharge.AddRange(enrollees.Where(p => p.PersonInCharge != null).Select(p => p.PersonInCharge).Distinct().ToArray());
            // Задаем текстовым полям источники данных
            tbRuSurname.AutoCompleteCustomSource = sourceSurnameRus;
            tbRuName.AutoCompleteCustomSource = sourceNameRus;
            tbRuPatronymic.AutoCompleteCustomSource = sourcePatronymicRus;
            tbBlrSurname.AutoCompleteCustomSource = sourceSurnameBlr;
            tbBlrName.AutoCompleteCustomSource = sourceNameBlr;
            tbBlrPatronymic.AutoCompleteCustomSource = sourcePatronymicBlr;
            tbDocWhoGave.AutoCompleteCustomSource = sourceDocumentWhoGave;
            tbMotherAdres.AutoCompleteCustomSource = sourceMotherAdress;
            tbFatherAdres.AutoCompleteCustomSource = sourceFatherAdress;
            tbSettlementName.AutoCompleteCustomSource = sourceSettlement;
            tbStreetName.AutoCompleteCustomSource = sourceStreet;
            tbSchoolName.AutoCompleteCustomSource = sourceSchoolName;
            tbSchoolAdres.AutoCompleteCustomSource = sourceSchoolAdress;
            tbCurrentUniversity.AutoCompleteCustomSource = sourceCurrentUniversity;
            tbCurrentSpeciality.AutoCompleteCustomSource = sourceCurrentSpeciality;
            tbWorkPlace.AutoCompleteCustomSource = sourceWorkPlace;
            tbWorkPost.AutoCompleteCustomSource = sourceWorkPost;
            tbPersonInCharge.AutoCompleteCustomSource = sourcePersonInCharge;
        }
        #endregion
        #region Initialization ComboBoxes
        /// <summary>
        /// Инициализация выпадающих списков данными
        /// </summary>
        private void InitializeComboBoxes()
        {
            // Инициализация списка факультетов
            InitializeFacultyComboBox();
            // Инициализация списка форм обучения
            InitializeFormOfStudyComboBox();
            // Инициализация списка видов гражданства
            InitializeCitizenshipComboBox();
            // Инициализация списка типов документов
            InitializeDocumentComboBox();
            // Инициализация списка стран
            InitializeCountryComboBox();
            // Инициализация списка областей
            InitializeAreaComboBox();
            // Инициализация списка районов
            InitializeDistrictComboBox();
            // Инициализация списка типов населенных пунктов
            InitializeTypeOfSettlementComboBox();
            // Инициализация списка типов улицы
            InitializeTypeOfStreetComboBox();
            // Инициализация списка типов учебных заведений
            InitializeTypeOfSchoolComboBox();
            // Инициализация списка иностранных языков
            InitializeForeignLanguageComboBox();
            // Инициализация списка типов финансирования
            InitializeTypeOfFinanceComboBox();
            // Инициализация списка видов конкурса
            InitializeContestComboBox();
            // Инициализация списка типов состояний абитуриента
            InitializeTypeOfStateComboBox();
            // Инициализация списка систем перевода
            InitializeTransferSystem();
            // Инициализация списка приказов
            InitializeDecreeComboBox();
            // Инициализация списка рабочих мест целевого направления
            InitializeTargetWorkPlace();
        }
        /// <summary>
        /// Загрузка данных в список "Целевые места"
        /// </summary>
        private void InitializeTargetWorkPlace()
        {
            // Отключаем отслеживание изменения 
            cbTargetWorkPlace.SelectedValueChanged -= cbTargetWorkPlace_SelectedValueChanged;
            // Получаем список рабочих мест по целевому направлению
            var targets = targetWorkPlaceService.GetTargetWorkPlaces();
            cbTargetWorkPlace.DataSource = targets;
            cbTargetWorkPlace.DisplayMember = "Name";
            cbTargetWorkPlace.ValueMember = "TargetId";
            // Инициализируем рабочее место по целевому первым элементом списка
            if (targets.Count != 0) targetWorkPlace = targets[0];
            // Включаем отслеживание изменения
            cbTargetWorkPlace.SelectedValueChanged += cbTargetWorkPlace_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Приказы"
        /// </summary>
        private void InitializeDecreeComboBox()
        {
            // Отключаем отслеживание изменения
            cbDecree.SelectedValueChanged -= cbDecree_SelectedValueChanged;
            // Получаем список приказов
            var decrees = decreeService.GetDecrees();
            cbDecree.DataSource = decrees;
            cbDecree.DisplayMember = "DecreeNumber";
            cbDecree.ValueMember = "DecreeId";
            // Инициализируем приказ первым элементом списка
            if (decrees.Count != 0) decree = decrees[0];
            // Включаем отслеживание изменения
            cbDecree.SelectedValueChanged += cbDecree_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Районы"
        /// </summary>
        private void InitializeDistrictComboBox()
        {
            // Отключаем отслеживание изменения
            cbDistrict.SelectedValueChanged -= cbDistrict_SelectedValueChanged;
            // Получаем список районов
            var districts = districtService.GetDistricts();
            cbDistrict.DataSource = districts;
            cbDistrict.DisplayMember = "Name";
            cbDistrict.ValueMember = "DistrictId";
            // Инициализируем район первым элементом списка
            if (districts.Count != 0) district = districts[0];
            // Включаем отслеживание изменения
            cbDistrict.SelectedValueChanged += cbDistrict_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Области"
        /// </summary>
        private void InitializeAreaComboBox()
        {
            // Отключаем отслеживание изменения
            cbArea.SelectedValueChanged -= cbArea_SelectedValueChanged;
            // Получаем список областей
            var areas = areaService.GetAreas();
            cbArea.DataSource = areas;
            cbArea.DisplayMember = "Name";
            cbArea.ValueMember = "AreaId";
            // Инициализируем область первым элементом списка
            if (areas.Count != 0) area = areas[0];
            // Включаем отслеживание изменения
            cbArea.SelectedValueChanged += cbArea_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Страны"
        /// </summary>
        private void InitializeCountryComboBox()
        {
            // Отключаем отслеживание изменения
            cbCountry.SelectedValueChanged -= cbCountry_SelectedValueChanged;
            // Получаем список стран
            var countries = countryService.GetCountries();
            cbCountry.DataSource = countries;
            cbCountry.DisplayMember = "Name";
            cbCountry.ValueMember = "CountryId";
            // Инициализируем страну первым элементом списка
            if (countries.Count != 0) country = countries[0];
            // Включаем отслеживание изменения
            cbCountry.SelectedValueChanged += cbCountry_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Системы перевода"
        /// </summary>
        private void InitializeTransferSystem()
        {
            List<string> listAttestat = new List<string>();
            List<string> listPtu = new List<string>();
            List<string> listSsuz = new List<string>();
            listAttestat.Add("10 в 100");
            listAttestat.Add("5 в 100");
            listPtu.Add("10 в 100");
            listPtu.Add("5 в 100");
            listSsuz.Add("10 в 100");
            listSsuz.Add("5 в 100");
            cbSystemAttestat.DataSource = listAttestat;
            cbSystemDiplomPtu.DataSource = listPtu;
            cbSystemDiplomSsuz.DataSource = listSsuz;
        }
        /// <summary>
        /// Загрузка данных в список "Статусы"
        /// </summary>
        private void InitializeTypeOfStateComboBox()
        {
            // Отключаем отслеживание изменения
            cbTypeOfState.SelectedValueChanged -= cbTypeOfState_SelectedValueChanged;
            // Получаем список состояний
            var states = typeOfStateService.GetTypeOfStates().OrderBy(s => s.StateId).ToList();
            cbTypeOfState.DataSource = states;
            cbTypeOfState.DisplayMember = "Name";
            cbTypeOfState.ValueMember = "StateId";
            // Инициализируем состояние абитуриента первым элементом списка
            if (states.Count != 0) typeOfState = states[0];
            // Включаем отслеживание изменения
            cbTypeOfState.SelectedValueChanged += cbTypeOfState_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Основания зачисления"
        /// </summary>
        private void InitializeReasonForAddmissionComboBox()
        {
            // Отключаем отслеживание изменения
            cbReasonForAddmission.SelectedValueChanged -= cbReasonForAddmission_SelectedValueChanged;
            // Получаем список оснований зачисления
            var reasons = reasonForAddmissionService.GetReasonForAddmissions(contest);
            cbReasonForAddmission.DataSource = reasons;
            cbReasonForAddmission.DisplayMember = "Fullname";
            cbReasonForAddmission.ValueMember = "ReasonForAddmissionId";
            // Инициализируем основание зачисления первым элементом списка
            if (reasons.Count != 0) reasonForAddmission = reasons[0];
            // Включаем отслеживание изменения
            cbReasonForAddmission.SelectedValueChanged += cbReasonForAddmission_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Виды конкурса"
        /// </summary>
        private void InitializeContestComboBox()
        {
            // Отключаем отслеживание изменения
            cbContest.SelectedValueChanged -= cbContest_SelectedValueChanged;
            // Получаем список конкурсов
            var contests = contestService.GetContests().OrderBy(c => c.ContestId).ToList();
            cbContest.DataSource = contests;
            cbContest.DisplayMember = "Name";
            cbContest.ValueMember = "ContestId";
            // Инициализируем конкурс первым элементом списка 
            // Инициализируем список оснований зачисления
            if (contests.Count != 0)
            {
                contest = contests[0];
                InitializeReasonForAddmissionComboBox();
            }
            // Включаем отслеживание изменения
            cbContest.SelectedValueChanged += cbContest_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип финансирования"
        /// </summary>
        private void InitializeTypeOfFinanceComboBox()
        {
            // Отключаем отслеживание изменения
            cbTypeOfFinance.SelectedValueChanged -= cbTypeOfFinance_SelectedValueChanged;
            // Получаем список типов финансирования
            var finances = typeOfFinanceService.GetTypeOfFinances();
            cbTypeOfFinance.DataSource = finances;
            cbTypeOfFinance.DisplayMember = "Fullname";
            cbTypeOfFinance.ValueMember = "FinanceTypeId";
            // Инициализируем тип финансирования первым элементом списка 
            if (finances.Count != 0) typeOfFinance = finances[0];
            // Включаем отслеживание изменения
            cbTypeOfFinance.SelectedValueChanged += cbTypeOfFinance_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Иностранные языки"
        /// </summary>
        private void InitializeForeignLanguageComboBox()
        {
            // Отключаем отслеживание изменения
            cbForeignLanguage.SelectedValueChanged -= cbForeignLanguage_SelectedValueChanged;
            // Получаем список иностранных языков
            var languages = foreignLanguageService.GetForeignLanguages();
            cbForeignLanguage.DataSource = languages;
            cbForeignLanguage.DisplayMember = "Name";
            cbForeignLanguage.ValueMember = "LanguageId";
            // Инициализируем иностранный язык первым элементом списка 
            if (languages.Count != 0) foreignLanguage = languages[0];
            // Включаем отслеживание изменения
            cbForeignLanguage.SelectedValueChanged += cbForeignLanguage_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Специальности второй ступени"
        /// </summary>
        private void InitializeSecondarySpecialityComboBox()
        {
            // Отключаем отслеживание изменения
            cbSecondarySpeciality.SelectedValueChanged -= cbSecondarySpeciality_SelectedValueChanged;
            // Получаем список интеграционных специальностей
            var integrationOfSpecialities = integrationOfSpecialitiesService.GetIntegrationOfSpecialities(speciality);
            // Заводим список специальностей второй ступени
            var secondarySpecialities = new List<SecondarySpeciality>();
            foreach (var integration in integrationOfSpecialities)
            {
                // Добавляем только те специальности, которые интегрированны с текущей специальностью первой ступени
                var ss = secondarySpecialityService.GetSecondarySpeciality(integration.SecondarySpecialityId);
                secondarySpecialities.Add(ss);
            }
            cbSecondarySpeciality.DataSource = secondarySpecialities;
            cbSecondarySpeciality.DisplayMember = "Fullname";
            cbSecondarySpeciality.ValueMember = "SecondarySpecialityId";
            // Инициализируем специальность второй ступени первым элементом списка
            // Инициализируем таблицу приоритетов
            if (secondarySpecialities.Count != 0)
            {
                secondarySpeciality = secondarySpecialities[0];
                InitializePrioritySpecialityGrid();
            }
            // Включаем отслеживание изменения
            cbSecondarySpeciality.SelectedValueChanged += cbSecondarySpeciality_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип прошлого учебного заведения"
        /// </summary>
        private void InitializeTypeOfSchoolComboBox()
        {
            // Отключаем отслеживание изменения
            cbTypeOfSchool.SelectedValueChanged -= cbTypeOfSchool_SelectedValueChanged;
            // Получаем список типов учебных заведений
            var schools = typeOfSchoolService.GetTypeOfSchools().OrderBy(ts => ts.SchoolTypeId).ToList();
            cbTypeOfSchool.DataSource = schools;
            cbTypeOfSchool.DisplayMember = "Name";
            cbTypeOfSchool.ValueMember = "SchoolTypeId";
            // Инициализируем тип учебного заведения первым элементом списка
            if (schools.Count != 0) typeOfSchool = schools[0];
            // Включаем отслеживание изменения
            cbTypeOfSchool.SelectedValueChanged += cbTypeOfSchool_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип улиц"
        /// </summary>
        private void InitializeTypeOfStreetComboBox()
        {
            // Отключаем отслеживание изменения
            cbTypeOfStreet.SelectedValueChanged -= cbTypeOfStreet_SelectedValueChanged;
            // Получаем список типов улицы
            var streets = typeOfStreetService.GetTypeOfStreets();
            cbTypeOfStreet.DataSource = streets;
            cbTypeOfStreet.DisplayMember = "Fullname";
            cbTypeOfStreet.ValueMember = "StreetTypeId";
            // Инициализируем тип улицы первым элементом списка
            if (streets.Count != 0) typeOfStreet = streets[0];
            // Включаем отслеживание изменения
            cbTypeOfStreet.SelectedValueChanged += cbTypeOfStreet_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип населенного пункта"
        /// </summary>
        private void InitializeTypeOfSettlementComboBox()
        {
            // Отключаем отслеживание изменения
            cbTypeOfSettlement.SelectedValueChanged -= cbTypeOfSettlement_SelectedValueChanged;
            // Получаем список типов населенного пункта
            var settlements = typeOfSettlementService.GetTypeOfSettlements();
            cbTypeOfSettlement.DataSource = settlements;
            cbTypeOfSettlement.DisplayMember = "Fullname";
            cbTypeOfSettlement.ValueMember = "SettlementTypeId";
            // Инициализируем тип населенного пункта первым элементом списка
            if (settlements.Count != 0) typeOfSettlement = settlements[0];
            // Включаем отслеживание изменения
            cbTypeOfSettlement.SelectedValueChanged += cbTypeOfSettlement_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Факультеты"
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
        /// Загрузка данных в список "Формы обучения"
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
        /// Загрузка данных в список "Специальности"
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
                // Инициализируем список специальностей второй ступени
                // Инициализируем таблицу сертификатов (оценок)
                // Инициализируем список приоритетов
                if (specialities.Count != 0)
                {
                    speciality = specialities[0];
                    InitializeSecondarySpecialityComboBox();
                    InitializeSertificationGrid();
                    InitializePrioritySpecialityGrid();
                }
                // Включаем отслеживание изменения
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }
        /// <summary>
        /// Загрузка данных в список "Вид гражданства"
        /// </summary>
        private void InitializeCitizenshipComboBox()
        {
            // Отключаем отслеживание изменения
            cbCitizenship.SelectedValueChanged -= cbCitizenship_SelectedValueChanged;
            // Получаем список гражданств
            var citizenships = citizenshipService.GetCitizenships();
            cbCitizenship.DataSource = citizenships;
            cbCitizenship.DisplayMember = "Fullname";
            cbCitizenship.ValueMember = "CitizenshipId";
            // Инициализируем гражданство первым элементом списка
            if (citizenships.Count != 0) citizenship = citizenships[0];
            // Включаем отслеживание изменения
            cbCitizenship.SelectedValueChanged += cbCitizenship_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Документы"
        /// </summary>
        private void InitializeDocumentComboBox()
        {
            // Отключаем отслеживание изменения
            cbDocument.SelectedValueChanged -= cbDocument_SelectedValueChanged;
            // Получаем список документов
            var documents = documentService.GetDocuments().OrderBy(d => d.DocumentId).ToList();
            cbDocument.DataSource = documents;
            cbDocument.DisplayMember = "Name";
            cbDocument.ValueMember = "DocumentId";
            // Инициализируем тип документа первым элементом списка
            if (documents.Count != 0) document = documents[0];
            // Включаем отслеживание изменения
            cbDocument.SelectedValueChanged += cbDocument_SelectedValueChanged;
        }
        #endregion
        #region Обработчики выбора объекта ComboBox
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Целевые рабочие места"</param>
        /// <param name="e"></param>
        private void cbTargetWorkPlace_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbTargetWorkPlace.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbTargetWorkPlace.SelectedValue;
                // По уникальному идентификатору ищем рабочее место по целевому направлению
                targetWorkPlace = targetWorkPlaceService.GetTargetWorkPlace(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Приказы"</param>
        /// <param name="e"></param>
        private void cbDecree_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbDecree.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbDecree.SelectedValue;
                // По уникальному идентификатору ищем приказ о зачислении
                decree = decreeService.GetDecree(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Страны"</param>
        /// <param name="e"></param>
        private void cbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbCountry.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbCountry.SelectedValue;
                // По уникальному идентификатору ищем страну
                country = countryService.GetCountry(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Области"</param>
        /// <param name="e"></param>
        private void cbArea_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbArea.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbArea.SelectedValue;
                // По уникальному идентификатору ищем область
                area = areaService.GetArea(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Районы"</param>
        /// <param name="e"></param>
        private void cbDistrict_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbDistrict.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbDistrict.SelectedValue;
                // По уникальному идентификатору ищем район
                district = districtService.GetDistrict(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// Загрузка данных в выпадающий список "Специальности"
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
                // Инициализируем специальность
                InitializeSpecialityComboBox();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// Загрузка данных в выпадающий список "Специальности"
        /// Установка видимости полей
        /// </summary>
        /// <param name="sender">Выпадающий список "Формы обучения"</param>
        /// <param name="e"></param>
        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbFormOfStudy.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbFormOfStudy.SelectedValue;
                // По уникальному идентификатору ищем форму обучения
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
                // Инициализируем специальности
                InitializeSpecialityComboBox();
                // Если сокращенная форма — делаем видимым поле специальности 2 ступени
                // Если заочная — информация о рабочем месте
                switch (formOfStudy.FormOfStudyId)
                {
                    case 1:
                        {
                            ShowComboBoxSecondarySpeciality(false);
                            ShowComboBoxesCurrentWorkPlace(false);
                            break;
                        }
                    case 2:
                        {
                            ShowComboBoxSecondarySpeciality(true);
                            ShowComboBoxesCurrentWorkPlace(false);
                            break;
                        }
                    case 3:
                        {
                            ShowComboBoxSecondarySpeciality(false);
                            ShowComboBoxesCurrentWorkPlace(true);
                            break;
                        }
                    case 4:
                        {
                            ShowComboBoxSecondarySpeciality(false);
                            ShowComboBoxesCurrentWorkPlace(true);
                            break;
                        }
                    case 5:
                        {
                            ShowComboBoxSecondarySpeciality(true);
                            ShowComboBoxesCurrentWorkPlace(true);
                            break;
                        }
                    case 6:
                        {
                            ShowComboBoxSecondarySpeciality(true);
                            ShowComboBoxesCurrentWorkPlace(true);
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// Загрузка данных в выпадающий список "Специальности второй ступени"
        /// Загрузка данных в таблицу "Сертификатов (дисциплин"
        /// </summary>
        /// <param name="sender">Выпадающий список "Формы обучения"</param>
        /// <param name="e"></param>
        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbSpeciality.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbSpeciality.SelectedValue;
                // По уникальному идентификатору ищем специальность первой ступени
                speciality = specialityService.GetSpeciality(id);
                // Заполняет таблицу сертификатов
                InitializeSertificationGrid();
                // Если абитуриент поступает на сокращенную форму инициализируем специальности второй ступени
                if (HasSecondarySpeciality) InitializeSecondarySpecialityComboBox();
                // Иначе инициализируем список приоритетов
                else InitializePrioritySpecialityGrid();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Виды гражданства"</param>
        /// <param name="e"></param>
        private void cbCitizenship_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbCitizenship.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbCitizenship.SelectedValue;
                // По уникальному идентификатору ищем гражданство
                citizenship = citizenshipService.GetCitizenship(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Документы"</param>
        /// <param name="e"></param>
        private void cbDocument_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbDocument.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbDocument.SelectedValue;
                // По уникальному идентификатору ищем тип документа
                document = documentService.GetDocument(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Тип населенного пункта"</param>
        /// <param name="e"></param>
        private void cbTypeOfSettlement_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbTypeOfSettlement.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbTypeOfSettlement.SelectedValue;
                // По уникальному идентификатору ищем тип населенного пункта
                typeOfSettlement = typeOfSettlementService.GetTypeOfSettlement(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Вид улиц"</param>
        /// <param name="e"></param>
        private void cbTypeOfStreet_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbTypeOfStreet.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbTypeOfStreet.SelectedValue;
                // По уникальному идентификатору ищем тип улицы
                typeOfStreet = typeOfStreetService.GetTypeOfStreet(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Вид учебного заведения"</param>
        /// <param name="e"></param>
        private void cbTypeOfSchool_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbTypeOfSchool.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbTypeOfSchool.SelectedValue;
                // По уникальному идентификатору ищем тип учебного заведения
                typeOfSchool = typeOfSchoolService.GetTypeOfSchool(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Специальности второй ступени"</param>
        /// <param name="e"></param>
        private void cbSecondarySpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbSecondarySpeciality.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbSecondarySpeciality.SelectedValue;
                // По уникальному идентификатору ищем специальность второй ступени
                secondarySpeciality = secondarySpecialityService.GetSecondarySpeciality(id);
                // Инициализируем список приоритетов
                InitializePrioritySpecialityGrid();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Иностранные языки"</param>
        /// <param name="e"></param>
        private void cbForeignLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbForeignLanguage.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbForeignLanguage.SelectedValue;
                // По уникальному идентификатору ищем иностранный язык
                foreignLanguage = foreignLanguageService.GetForeignLanguage(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Тип финансирования"</param>
        /// <param name="e"></param>
        private void cbTypeOfFinance_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbTypeOfFinance.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbTypeOfFinance.SelectedValue;
                // По уникальному идентификатору ищем тип финансирования
                typeOfFinance = typeOfFinanceService.GetTypeOfFinance(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Вид конкурса"</param>
        /// <param name="e"></param>
        private void cbContest_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbContest.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbContest.SelectedValue;
                // По уникальному идентификатору ищем вид конкурса
                contest = contestService.GetContest(id);
                // Инициализируем основания зачисления
                InitializeReasonForAddmissionComboBox();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Основание зачисления"</param>
        /// <param name="e"></param>
        private void cbReasonForAddmission_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbReasonForAddmission.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbReasonForAddmission.SelectedValue;
                // По уникальному идентификатору ищем основание зачисления
                reasonForAddmission = reasonForAddmissionService.GetReasonForAddmission(id);
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Статус абитуриента"</param>
        /// <param name="e"></param>
        private void cbTypeOfState_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список не пуст
            if (cbTypeOfState.SelectedValue != null)
            {
                // Получаем уникальный идентификатор
                int id = (int)cbTypeOfState.SelectedValue;
                // По уникальному идентификатору ищем тип состояния
                typeOfState = typeOfStateService.GetTypeOfState(id);
            }
        }
        #endregion
        #region Видимость полей
        /// <summary>
        /// Установка видимости полей второго высшего образования
        /// </summary>
        /// <param name="sender">Поле-флажок "Второе высшее"</param>
        /// <param name="e"></param>
        private void cbSecondEducation_CheckedChanged(object sender, EventArgs e)
        {
            // Если абитуриент поступает на второе высшее образование
            if (cbSecondEducation.Checked)
            {
                // Показываем поля ввода информации о текущем образовании
                ShowSecondEducationFields(true);
            }
            else
            {
                // Иначе скрываем поля ввода информации о текущем образовании
                ShowSecondEducationFields(false);
            }
        }
        /// <summary>
        /// Установка видимости выпадающего списка "Специальности второй ступени"
        /// </summary>
        /// <param name="flag"></param>
        private void ShowComboBoxSecondarySpeciality(bool flag)
        {
            // Управляем видимостью списка выбора специальности второй ступени
            // В зависимости от формы обучения
            HasSecondarySpeciality = flag;
            gbSecondarySpeciality.Visible = flag;
        }
        /// <summary>
        /// Установка видимости полей рабочего места, должности и стажа
        /// </summary>
        /// <param name="flag"></param>
        private void ShowComboBoxesCurrentWorkPlace(bool flag)
        {
            // В зависимости от формы обучения показываем поля для ввода информации о рабочем стаже, месте работы и должности
            IsWorker = flag;
            gbSeniority.Visible = flag;
            gbWorkPlace.Visible = flag;
            gbWorkPost.Visible = flag;
        }
        /// <summary>
        /// Установка видимости полей специальности второй ступени
        /// </summary>
        /// <param name="flag"></param>
        private void ShowSecondEducationFields(bool flag)
        {
            // Управление видимостью полей ввода о текущем образовании
            gbCurrentCurs.Visible = flag;
            gbCurrentUniversity.Visible = flag;
            gbCurrentSpeciality.Visible = flag;
        }
        #endregion
        #region Работа со списком атрибутов(льгот)
        /// <summary>
        /// Установка видимости выпадающего списка рабочих мест целевого направления
        /// Дублирование выбора атрибута "Целевое направление" в списке льгот
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTarget_CheckedChanged(object sender, EventArgs e)
        {
            // Если выбран пункт "Целевое направление"
            if (cbTarget.Checked)
            {
                // Показываем список рабочих мест
                cbTargetWorkPlace.Visible = true;
                // Выбираем "Целевое направление" в списке атрибутов (льгот) абитуриента
                SetCheckedAtribute(cbTarget.Text.Trim(), true);
            }
            else
            {
                // Скрываем список рабочих мест
                cbTargetWorkPlace.Visible = false;
                // Отменяем выбор "Целевое направление" в списке атрибутов (льгот) абитуриента
                SetCheckedAtribute(cbTarget.Text.Trim(), false);
            }
        }
        /// <summary>
        /// Дублирование выбора атрибута "БРСМ" в списке льгот
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBrsm_CheckedChanged(object sender, EventArgs e)
        {
            // Если абитуриент член БРСМ
            if (cbBrsm.Checked)
                // Выбираем "БРСМ" в списке атрибутов (льгот) абитуриента
                SetCheckedAtribute(cbBrsm.Text, true);
            else
                // Отменяем выбор "БРСМ" в списке атрибутов (льгот) абитуриента
                SetCheckedAtribute(cbBrsm.Text, false);
        }
        /// <summary>
        /// Выбор атрибута в списке атрибутов (льгот) 
        /// </summary>
        /// <param name="name">Наимемнование атрибута</param>
        /// <param name="flag">Значение true - выбор атрибута; false - отмена выбора</param>
        private void SetCheckedAtribute(string name, bool flag)
        {
            // Проход по списку атрибутов
            for (int i = 0; i < chkAtributeList.Items.Count; i++)
            {
                // Получаем наименование атрибута
                string nameInListBox = chkAtributeList.Items[i].ToString();
                // Если наименование атрибута в списке идентично искомому наименованию
                if (string.Equals(nameInListBox, name))
                {
                    // Получаем атрибут по наименованию
                    Atribute atribute = atributeService.GetAtribute(name);
                    // Если атрибут найден производим выбор атрибута
                    if (atribute.AtributeId != 0 && atribute.Fullname.Trim() == name) chkAtributeList.SetItemChecked(i, flag);
                }
            }
        }
        /// <summary>
        /// Дублирование выбора атрибута "БРСМ" и "Целевое направление" в списке льгот и полях выбора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAtributeList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Получаем наименование выбранного атрибута
            string name = chkAtributeList.Items[e.Index].ToString();
            // Получаем атрибута
            atribute = atributeService.GetAtribute(name);
            // Если атрибут "БРСМ"
            if (atribute.Fullname == cbBrsm.Text)
            {
                if (e.NewValue == CheckState.Checked) cbBrsm.Checked = true;
                else cbBrsm.Checked = false;
            }
            // Если атрибут "Целевое направление"
            if (atribute.Fullname == cbTarget.Text)
            {
                if (e.NewValue == CheckState.Checked) cbTarget.Checked = true;
                else cbTarget.Checked = false;
            }
        }
        #endregion
        #region Управление сертификатами
        /// <summary>
        /// Обработчик нажатия кнопки "Удаления"
        /// </summary>
        /// <param name="sender">Кнопка "Удалить дисциплину"</param>
        /// <param name="e"></param>
        private void btDeleteDiscipline_Click(object sender, EventArgs e)
        {
            // Получаем текущий индекс выбранной строки
            int rowIndex = SertificateGrid.CurrentRow.Index;
            // Удаляем строку
            sertificateTable.Rows[rowIndex].Delete();
        }
        /// <summary>
        /// Обработчик нажатия кнопки сброса экзаменнационной схемы
        /// </summary>
        /// <param name="sender">Кнопка "Экзаменнационная схема"</param>
        /// <param name="e"></param>
        private void btGetExamSchema_Click(object sender, EventArgs e)
        {
            // Если не режим редактирования
            if (!editMode)
                // Инициализация списка сертификатов (оценок)
                InitializeSertificationGrid();
            else
            {
                // Если режим редактирования
                // Диалоговое окно 
                DialogResult examSchemaClearResult = MessageBox.Show(this, "При загрузке экзаменнационной схемы, будет удалена информация о текущих оценках абитуриента. Продолжить?", "Экзаменнационная схема", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (examSchemaClearResult == DialogResult.Yes)
                {
                    // Получение список оценок абитуриента
                    var assessments = assessmentService.GetAssessments(enrollee);
                    // Удаляем оценки
                    foreach (var assessment in assessments)
                        assessmentService.DeleteAssessment(assessment);
                    // Инициализация списка сертификатов (оценок)
                    InitializeSertificationGrid();
                }
            }
        }
        /// <summary>
        /// Замена дисциплины в таблице сертификатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btChangeDiscipline_Click(object sender, EventArgs e)
        {
            // Вызов формы замены дисциплины
            frmChangeDiscipline changeDisciplineCard = new frmChangeDiscipline();
            DialogResult changeDisciplineCardResult = changeDisciplineCard.ShowDialog();
            if (changeDisciplineCardResult == DialogResult.OK)
            {
                // Получаем текущий индекс выбранной строки
                int rowIndex = SertificateGrid.CurrentRow.Index;
                // Добавляем информацию о заменяемой дисциплине
                SertificateGrid.Rows[rowIndex].Cells[6].Value = changeDisciplineCard.discipline.Name;
            }
        }
        #endregion
        #region Установка языков
        /// <summary>
        /// Установка белорусского языка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBelorussianLanguage(object sender, EventArgs e)
        {
            ChangeInputLanguage change = new ChangeInputLanguage();
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage il in coll)
            {
                if (il.Culture.Name == "be-BY") change.ChangeLanguage(il);
            }
        }
        /// <summary>
        /// Установка английского языка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetEnglishLanguage(object sender, EventArgs e)
        {
            ChangeInputLanguage change = new ChangeInputLanguage();
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage il in coll)
            {
                if (il.Culture.Name == "en-US") change.ChangeLanguage(il);
            }
        }
        /// <summary>
        /// Установка языка по умолчанию (русский)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetDefaultLanguage(object sender, EventArgs e)
        {
            ChangeInputLanguage change = new ChangeInputLanguage();
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            change.ChangeLanguage(InputLanguage.DefaultInputLanguage);
        }
        #endregion
        #region Ввод цифр
        /// <summary>
        /// Разрешаем ввод цифр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllowNumeric(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
        /// <summary>
        /// Разрешаем ввод цифр поля оценок аттестата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllowNumericAttestat(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (cbSystemAttestat.SelectedIndex)
            {
                case 0: //10
                    {
                        if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                            e.Handled = true;

                        break;
                    }
                case 1: //5
                    {
                        if ((e.KeyChar <= 50 || e.KeyChar >= 54) && e.KeyChar != 8)
                        {
                            e.Handled = true;
                            MessageBox.Show(this, "Только пятибалльная система", "Выбор системы перевода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBox.Focus();
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// Разрешаем ввод цифр поля оценок диплома ПТУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllowNumericDiplomPtu(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (cbSystemDiplomPtu.SelectedIndex)
            {
                case 0: //10
                    {
                        if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                            e.Handled = true;

                        break;
                    }
                case 1: //5
                    {
                        if ((e.KeyChar <= 50 || e.KeyChar >= 54) && e.KeyChar != 8)
                        {
                            e.Handled = true;
                            MessageBox.Show(this, "Только пятибалльная система", "Выбор системы перевода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBox.Focus();
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// Разрешаем ввод цифр поля оценок диплома ССУЗ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllowNumericDiplomSsuz(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (cbSystemDiplomSsuz.SelectedIndex)
            {
                case 0: //10
                    {
                        if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                            e.Handled = true;

                        break;
                    }
                case 1: //5
                    {
                        if ((e.KeyChar <= 50 || e.KeyChar >= 54) && e.KeyChar != 8)
                        {
                            e.Handled = true;
                            MessageBox.Show(this, "Только пятибалльная система", "Выбор системы перевода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBox.Focus();
                        }
                        break;
                    }
            }
        }
        #endregion
        #region Работа с оценками абитуриента
        /// <summary>
        /// Считаем средний балл оценок выбранного документа об образовании
        /// </summary>
        /// <param name="firstString">Первая строка оценок</param>
        /// <param name="secondString">Вторая строка оценок</param>
        /// <param name="transferSystem">Выпадающий список системы перевода</param>
        /// <param name="averageString">Поле среднего балла</param>
        private void CalculateEstimation(TextBox firstString, TextBox secondString, ComboBox transferSystem, TextBox averageString)
        {
            // Если строки оценок равны
            if (firstString.Text.Equals(secondString.Text))
            {             
                // Проверяем систему перевода
                bool isTen = isTenSystem(firstString.Text);
                switch (transferSystem.SelectedIndex)
                {
                    // Если строка содержит оценки десятибальной системы = true
                    case 0: // из 10 в 100
                        {
                            // Количество оценок
                            int count = firstString.Text.Length;
                            // Обнуляем сумму баллов
                            int sum = 0;
                            // Проходим список оценок
                            for (int i = 0; i < count; i++)
                            {
                                // Оценка
                                int mark = (int)Char.GetNumericValue(firstString.Text[i]);
                                // Если оценка равна 0 - это 10
                                if (mark == 0) mark = 10;
                                // Добавляем оценку умноженную на 10
                                sum += mark * 10;
                            }
                            // Считаем средний балл
                            double avr = Math.Round((double)sum / count, MidpointRounding.AwayFromZero);                            
                            if (!Double.IsNaN(avr))
                            {
                                // Если число вычисленно записываем в строку среднего балла
                                averageString.Text = avr.ToString();
                            }
                            else
                            {
                                // Иначе указываем 0
                                averageString.Text = "0";
                            }
                            break;
                        }
                    case 1:
                        {
                            // Если пятибалльная система
                            if (!isTen)
                            {
                                // Количество оценок
                                int count = firstString.Text.Length;
                                // Обнуляем сумму баллов
                                double sum = 0;
                                // Проходим список оценок
                                for (int i = 0; i < count; i++)
                                {
                                    // Оценка
                                    int mark = (int)Char.GetNumericValue(firstString.Text[i]);
                                    // Конвертируем оценку в десятибалльную системы
                                    double ten = conversionSystemService.ConversionToTen(mark);
                                    // Суммируем
                                    sum += ten;
                                }
                                try
                                {
                                    // Считаем средний балл
                                    double avr = Math.Round(sum / count, 1);
                                    double ball = avr * 10;
                                    averageString.Text = ball.ToString();
                                }
                                catch (Exception ex)
                                {
                                    averageString.Text = " ";
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "В строке присутствуют оценки из 10-ти балльной системы", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                transferSystem.SelectedIndex = 0;
                            }
                            break;
                        }
                }
            }
            else averageString.Text = " ";
        }
        /// <summary>
        /// Считаем средний балл оценок документов об образовании
        /// </summary>
        /// <param name="attestat">Средний балл аттестата</param>
        /// <param name="diplomSsuz">Средний балл диплома ССУЗа</param>
        /// <param name="diplomPtu">Средний балл диплома ПТУ</param>
        private void CalculateAverageEstimation(TextBox attestat, TextBox diplomSsuz, TextBox diplomPtu)
        {
            // Оценки среднего балла
            double avgAttestat = 0;
            double avgDiplomPTU = 0;
            double avgDiplomSSUZ = 0;
            // Получаем средний балл документов
            if (!string.IsNullOrWhiteSpace(attestat.Text)) avgAttestat = Double.Parse(attestat.Text);
            if (!string.IsNullOrWhiteSpace(diplomSsuz.Text)) avgDiplomSSUZ = Double.Parse(diplomSsuz.Text);
            if (!string.IsNullOrWhiteSpace(diplomPtu.Text)) avgDiplomPTU = Double.Parse(diplomPtu.Text);
            // Получаем количество предоставленных документов
            int count = 0;
            if (avgAttestat != 0) count++;
            if (avgDiplomPTU != 0) count++;
            if (avgDiplomSSUZ != 0) count++;
            // Сумма баллов
            double sum = avgAttestat + avgDiplomSSUZ + avgDiplomPTU;
            // Средний балл всех документов
            double avr = Math.Round((double)sum / count, MidpointRounding.AwayFromZero);
            if (Double.IsNaN(avr)) tbAverage.Text = String.Empty;
            else tbAverage.Text = avr.ToString();
        }
        /// <summary>
        /// Проверка системы перевода введеных оценок
        /// </summary>
        /// <param name="s">Строка оценок</param>
        /// <returns></returns>
        private bool isTenSystem(string s)
        {
            bool tenSystem = false; // 10-и балльная система
            int length = s.Length; // длина строки
            int count = 0;
            foreach (char c in s)
            {
                if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5')
                {
                    count++;
                }
            }
            if (count == length) tenSystem = false;
            else tenSystem = true;
            return tenSystem;
        }
        /// <summary>
        /// Изменение системы перевода Аттестата
        /// </summary>
        /// <param name="sender">Выпадающий список системы перевода Аттестата</param>
        /// <param name="e"></param>
        private void cbSystemAttestat_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstAttestatString, tbSecondAttestatString, cbSystemAttestat, tbAverageAttestat);
        }
        /// <summary>
        /// Изменение первой строки оценок Аттестата
        /// </summary>
        /// <param name="sender">Текстовое поле аттестата</param>
        /// <param name="e"></param>
        private void tbFirstAttestatString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstAttestatString, tbSecondAttestatString, cbSystemAttestat, tbAverageAttestat);
        }
        /// <summary>
        /// Изменение второй строки оценок Аттестата
        /// </summary>
        /// <param name="sender">Текстовое поле аттестата</param>
        /// <param name="e"></param>
        private void tbSecondAttestatString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstAttestatString, tbSecondAttestatString, cbSystemAttestat, tbAverageAttestat);
        }
        /// <summary>
        /// Изменение системы перевода диплома ПТУ
        /// </summary>
        /// <param name="sender">Выпадающий список системы перевода диплома ПТУ</param>
        /// <param name="e"></param>
        private void cbSystemDiplomPtu_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomPtuString, tbSecondDiplomPtuString, cbSystemDiplomPtu, tbAverageDiplomPtu);
        }
        /// <summary>
        /// Изменение первой строки оценок диплома ПТУ
        /// </summary>
        /// <param name="sender">Текстовое поле диплома ПТУ</param>
        /// <param name="e"></param>
        private void tbFirstDiplomPtuString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomPtuString, tbSecondDiplomPtuString, cbSystemDiplomPtu, tbAverageDiplomPtu);
        }
        /// <summary>
        /// Изменение второй строки оценок диплома ПТУ
        /// </summary>
        /// <param name="sender">Текстовое поле диплома ПТУ</param>
        /// <param name="e"></param>
        private void tbSecondDiplomPtuString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomPtuString, tbSecondDiplomPtuString, cbSystemDiplomPtu, tbAverageDiplomPtu);
        }
        /// <summary>
        /// Изменение системы перевода диплома ССУЗа
        /// </summary>
        /// <param name="sender">Выпадающий список системы перевода диплома ССУЗа</param>
        /// <param name="e"></param>
        private void cbSystemDiplomSsuz_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomSsuzString, tbSecondDiplomSsuzString, cbSystemDiplomSsuz, tbAverageDiplomSsuz);
        }
        /// <summary>
        /// Изменение первой строки оценок диплома ССУЗа
        /// </summary>
        /// <param name="sender">Текстовое поле диплома ССУЗа</param>
        /// <param name="e"></param>
        private void tbFirstDiplomSsuzString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomSsuzString, tbSecondDiplomSsuzString, cbSystemDiplomSsuz, tbAverageDiplomSsuz);
        }
        /// <summary>
        /// Изменение второй строки оценок диплома ССУЗа
        /// </summary>
        /// <param name="sender">Текстовое поле диплома ССУЗа</param>
        /// <param name="e"></param>
        private void tbSecondDiplomSsuzString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomSsuzString, tbSecondDiplomSsuzString, cbSystemDiplomSsuz, tbAverageDiplomSsuz);
        }
        /// <summary>
        /// Изменение строки среднего балла аттестата
        /// </summary>
        /// <param name="sender">Текстовое поле диплома аттестата</param>
        /// <param name="e"></param>
        private void tbAverageAttestat_TextChanged(object sender, EventArgs e)
        {
            CalculateAverageEstimation(tbAverageAttestat, tbAverageDiplomSsuz, tbAverageDiplomPtu);
        }
        /// <summary>
        /// Изменение строки среднего балла диплома ПТУ
        /// </summary>
        /// <param name="sender">Текстовое поле диплома диплома ПТУ</param>
        /// <param name="e"></param>
        private void tbAverageDiplomPtu_TextChanged(object sender, EventArgs e)
        {
            CalculateAverageEstimation(tbAverageAttestat, tbAverageDiplomSsuz, tbAverageDiplomPtu);
        }
        /// <summary>
        /// Изменение строки среднего балла диплома ССУЗа
        /// </summary>
        /// <param name="sender">Текстовое поле диплома диплома ССУЗа</param>
        /// <param name="e"></param>
        private void tbAverageDiplomSsuz_TextChanged(object sender, EventArgs e)
        {
            CalculateAverageEstimation(tbAverageAttestat, tbAverageDiplomSsuz, tbAverageDiplomPtu);
        }
        #endregion
        #region Изменение порядка(ранга) приоритета
        /// <summary>
        /// Изменение порядка (ранга) приоритета специальности в таблице приоритетов
        /// Управление клавишами стрелок на клавиатуре "Вверх" и "Вниз"
        /// </summary>
        /// <param name="sender">Таблица приоритетов</param>
        /// <param name="e"></param>
        private void PriorityGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) // стрелка вверх
            {
                if (PriorityGrid.CurrentRow.Index > 0)
                {
                    // индекс текущей позиции
                    int current = PriorityGrid.CurrentRow.Index;
                    // позиция перед текущей
                    int next = current - 1;
                    string formOfStudy = PriorityGrid.Rows[next].Cells[1].Value.ToString();
                    int specialityId = Int32.Parse(PriorityGrid.Rows[next].Cells[2].Value.ToString());
                    string speciality = PriorityGrid.Rows[next].Cells[3].Value.ToString();
                    // двигаем вверх
                    PriorityGrid.Rows[next].Cells[1].Value = PriorityGrid.CurrentRow.Cells[1].Value.ToString();
                    PriorityGrid.Rows[next].Cells[2].Value = PriorityGrid.CurrentRow.Cells[2].Value.ToString();
                    PriorityGrid.Rows[next].Cells[3].Value = PriorityGrid.CurrentRow.Cells[3].Value.ToString();
                    // опускаем вниз
                    PriorityGrid.CurrentRow.Cells[1].Value = formOfStudy;
                    PriorityGrid.CurrentRow.Cells[2].Value = specialityId;
                    PriorityGrid.CurrentRow.Cells[3].Value = speciality;
                    // текущая выделенная строка
                    PriorityGrid.Rows[current].Selected = true;
                    PriorityGrid.CurrentCell = PriorityGrid.Rows[current].Cells[1];
                }
            }

            if (e.KeyCode == Keys.Down) // стрелка вниз
            {
                if (PriorityGrid.CurrentCell.RowIndex < PriorityGrid.Rows.Count - 1)
                {
                    int current = PriorityGrid.CurrentCell.RowIndex;
                    // позиция после текущей
                    int previous = current + 1;
                    string formOfStudy = PriorityGrid.Rows[previous].Cells[1].Value.ToString();
                    int specialityId = Int32.Parse(PriorityGrid.Rows[previous].Cells[2].Value.ToString());
                    string speciality = PriorityGrid.Rows[previous].Cells[3].Value.ToString();
                    // двигаем вверх
                    PriorityGrid.Rows[previous].Cells[1].Value = PriorityGrid.CurrentRow.Cells[1].Value.ToString();
                    PriorityGrid.Rows[previous].Cells[2].Value = PriorityGrid.CurrentRow.Cells[2].Value.ToString();
                    PriorityGrid.Rows[previous].Cells[3].Value = PriorityGrid.CurrentRow.Cells[3].Value.ToString();
                    // опускаем вниз
                    PriorityGrid.CurrentRow.Cells[1].Value = formOfStudy;
                    PriorityGrid.CurrentRow.Cells[2].Value = specialityId;
                    PriorityGrid.CurrentRow.Cells[3].Value = speciality;
                    // текущая выделенная строка
                    PriorityGrid.Rows[current].Selected = true;
                    PriorityGrid.CurrentCell = PriorityGrid.Rows[current].Cells[1];
                }
            }
        }
        #endregion
        /// <summary>
        /// Очистка буфера обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearClipboard(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }
        /// <summary>
        /// Отмена сохранения профиля абитуриента
        /// </summary>
        /// <param name="sender">Кнопка "Отмена"</param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            // Отмена регистрации абитуриента
            // Закрытие формы регистрации
            if (!editMode)
                logger.Info($"Пользователь: {activeEmployee.Fullname.Trim()} отменил регистрацию абитуриента. Закрытие формы регистрации.");
            else           
                logger.Info($"Пользователь: {activeEmployee.Fullname.Trim()} отменил редактирование абитуриента ({enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}). Закрытие формы редактирования.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Сохранение профиля абитуриента
        /// </summary>
        /// <param name="sender">Кнопка "Сохранить"</param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            // Сохраняем абитуриент, и получаем запись с уникальным идентификатором
            var savedEnrollee = SaveCurrentEnrollee(enrollee);
            // Если сохранение прошло успешно
            if (savedEnrollee.EnrolleeId != 0)
            {
                // Получаем список оценок абитуриента
                var assessments = GetAssessments(savedEnrollee);
                // Сохраняем оценки
                SaveAssessments(assessments);
                // Получаем список атрибутов (льгот) абитуриента
                var atributes = GetAtributes(savedEnrollee);
                // Сохраняем льготы
                SaveAtributes(atributes);
                // Получаем список приоритетов
                var priorities = GetPriorityList(savedEnrollee);
                // Сохраняем приоритеты абитуриента
                SavePriorities(priorities);
                // Закрываем окно регистрации абитуриента
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// Сохранение списка приоритетов пользователя
        /// </summary>
        /// <param name="priorities">Список приоритетов</param>
        private void SavePriorities(List<PriorityOfSpeciality> priorities)
        {
            // Если не режим редактирования
            if (!editMode)
            {
                logger.Info($"Добавление приоритетов абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                foreach (var priority in priorities)
                    // Добавляем приоритеты
                    priorityOfSpecialityService.InsertPriorityOfSpeciality(priority);
            }
            // Если режим редактирования
            else 
            {
                logger.Info($"Обновление приоритетов абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                foreach (var priority in priorities)
                    // Обновляем приоритеты
                    priorityOfSpecialityService.UpdatePriorityOfSpeciality(priority);
            }
        }
        /// <summary>
        /// Сохранение списка льгот
        /// </summary>
        /// <param name="atributes">Список льгот</param>
        private void SaveAtributes(List<AtributeForEnrollee> atributes)
        {
            logger.Info($"Добавление атрибутов (льгот) абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
            // Добавление атрибутов (льгот)
            foreach (var atribute in atributes)
                atributeForEnrolleeService.InsertAtributeForEnrollee(atribute);
        }
        /// <summary>
        /// Получение списка льгот абитуриента
        /// </summary>
        /// <param name="savedEnrollee">Текущий профиль абитуриента</param>
        /// <returns></returns>
        private List<AtributeForEnrollee> GetAtributes(Enrollee savedEnrollee)
        {
            // Получаем список атрибутов (льгот) пользователя
            var atributes = atributeForEnrolleeService.GetAtributeForEnrollees(savedEnrollee);
            // Если список не пуст
            if (atributes.Count != 0)
            {
                // Удаляем все атрибуты
                foreach (var atribute in atributes)
                    atributeForEnrolleeService.DeleteAtributeForEnrollee(atribute);
            }
            // Очищаем список атрибутов (льгот)
            atributes = new List<AtributeForEnrollee>();
            // Формируем новый список
            foreach (int i in chkAtributeList.CheckedIndices)
            {
                string name = chkAtributeList.Items[i].ToString();
                Atribute currentAtribute = atributeService.GetAtribute(name);
                AtributeForEnrollee atribute = new AtributeForEnrollee();
                atribute.AtributeId = currentAtribute.AtributeId;
                atribute.EnrolleeId = savedEnrollee.EnrolleeId;
                atributes.Add(atribute);
            }
            return atributes;
        }
        /// <summary>
        /// Добавление оценок абитуриента
        /// </summary>
        /// <param name="assessments">Список оценок</param>
        private void SaveAssessments(List<Assessment> assessments)
        {
            // Если не режим редактирования
            if (!editMode)
            {
                logger.Info($"Добавление оценок абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                foreach (var assessment in assessments)
                    // Добавляем оценки
                    assessmentService.InsertAssessment(assessment);
            }
            // Если режим редактирования
            else
            {
                logger.Info($"Редактирование оценок абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}.");
                foreach (var assessment in assessments)
                    // Обновляем оценки
                    assessmentService.UpdateAssessment(assessment);
            }
        }
        /// <summary>
        /// Получение списка оценок абитуриента
        /// </summary>
        /// <param name="savedEnrollee">Текущий профиль абитуриента</param>
        /// <returns></returns>
        private List<Assessment> GetAssessments(Enrollee savedEnrollee)
        {
            // Список оценок
            var assessments = new List<Assessment>();
            // Если режим регистрации абитуриента
            if (!editMode)
            {
                // Оценка документа об образовании
                var docAssessment = new Assessment();
                docAssessment.DisciplineId = 5;
                docAssessment.EnrolleeId = savedEnrollee.EnrolleeId;
                docAssessment.Estimation = Int32.Parse(tbAverage.Text);
                assessments.Add(docAssessment);
                // Оценки таблицы сертификатов
                foreach (DataGridViewRow row in SertificateGrid.Rows)
                {
                    var assessment = new Assessment();
                    // Получаем дисциплину
                    assessment.DisciplineId = Int32.Parse(row.Cells[0].Value.ToString());
                    assessment.EnrolleeId = savedEnrollee.EnrolleeId;
                    // Получаем оценку
                    if (string.IsNullOrWhiteSpace(row.Cells[4].Value.ToString())) assessment.Estimation = 0;
                    else assessment.Estimation = Int32.Parse(row.Cells[4].Value.ToString());
                    // Номер сертификата
                    assessment.SertCode = row.Cells[3].Value.ToString();
                    // Дата выдачи сертификата
                    assessment.SertDate = row.Cells[5].Value.ToString();
                    // Изменение дисциплины
                    assessment.ChangeDiscipline = row.Cells[6].Value.ToString();
                    assessments.Add(assessment);
                }
            }
            else
            {
                // Список оценок абитуриента
                assessments = assessmentService.GetAssessments(savedEnrollee);
                foreach (var assessment in assessments)
                {
                    // Документ об образовании
                    if (assessment.DisciplineId == 5)
                    {
                        assessment.Estimation = Int32.Parse(tbAverage.Text);
                    }
                    else
                    {
                        // Оценки таблицы сертификатов
                        foreach (DataGridViewRow row in SertificateGrid.Rows)
                        {
                            int disciplineId = Int32.Parse(row.Cells[0].Value.ToString());
                            if (assessment.DisciplineId == disciplineId)
                            {
                                if (string.IsNullOrWhiteSpace(row.Cells[4].Value.ToString())) assessment.Estimation = 0;
                                else assessment.Estimation = Int32.Parse(row.Cells[4].Value.ToString());
                                assessment.SertCode = row.Cells[3].Value.ToString();
                                assessment.SertDate = row.Cells[5].Value.ToString();
                                assessment.ChangeDiscipline = row.Cells[6].Value.ToString();
                            }
                        }
                    }
                }
            }
            return assessments;
        }
        /// <summary>
        /// Получение списка приоритетов специальностей абитуриента
        /// </summary>
        /// <param name="savedEnrollee">Текущий профиль абитуриента</param>
        /// <returns></returns>
        private List<PriorityOfSpeciality> GetPriorityList(Enrollee savedEnrollee)
        {
            // Список специальностей приоритета
            var list = new List<PriorityOfSpeciality>();
            // Режим регистрации приоритета
            if (!editMode)
            {    
                // Добавление приоритеты в список
                foreach (DataGridViewRow row in PriorityGrid.Rows)
                {
                    var priority = new PriorityOfSpeciality();
                    priority.EnrolleeId = savedEnrollee.EnrolleeId;
                    priority.SpecialityId = Int32.Parse(row.Cells[2].Value.ToString());
                    priority.PriorityLevel = Int32.Parse(row.Cells[0].Value.ToString());
                    list.Add(priority);
                }
            }
            else
            {
                // Список специальностей приоритета абитуриента
                list = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee);
                // Обновляем уровень приоритета специальности
                foreach (var priority in list)
                {
                    foreach (DataGridViewRow row in PriorityGrid.Rows)
                    {
                        int specialityId = Int32.Parse(row.Cells[2].Value.ToString());
                        var tempSpeciality = specialityService.GetSpeciality(specialityId);
                        var tempPriority = priorityOfSpecialityService.GetPriorityOfSpeciality(savedEnrollee, tempSpeciality);
                        if (tempPriority.PriorityId == priority.PriorityId)
                        {
                            priority.PriorityLevel = Int32.Parse(row.Cells[0].Value.ToString());
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Сохранение данных абитуриента
        /// </summary>
        /// <param name="enrollee">Текущий профиль абитуриента</param>
        /// <returns></returns>
        private Enrollee SaveCurrentEnrollee(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(tbRuSurname.Text))
            {
                
                MessageBox.Show(this, "Введите фамилию абитуриента на русском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbRuSurname.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbRuName.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл имя абитуриента на русском языке.");
                MessageBox.Show(this, "Введите имя абитуриента на русском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbRuName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbBlrSurname.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл фамилию абитуриента на белорусском языке.");
                MessageBox.Show(this, "Введите фамилию абитуриента на белорусском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbBlrSurname.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbBlrName.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл имя абитуриента на белорусском языке.");
                MessageBox.Show(this, "Введите имя абитуриента на белорусском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbBlrName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocSeria.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл серию документа предоставленного абитуриентом.");
                MessageBox.Show(this, "Введите серию документа абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocSeria.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocNumber.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл номер документа предоставленного абитуриентом.");
                MessageBox.Show(this, "Введите номер документа абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocNumber.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocWhoGave.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл информацию кто произвел выдачу документа предоставленного абитуриентом.");
                MessageBox.Show(this, "Введите наименование органа выдавшего документ абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocWhoGave.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocPersonalNumber.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл персональный номер документа предоставленного абитуриентом.");
                MessageBox.Show(this, "Введите личный номер абитуриента, согласно предоставленного документа", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocPersonalNumber.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSettlementName.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл наименование населенного пункта абитуриента.");
                MessageBox.Show(this, "Введите наименование населенного пункта абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSettlementName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSettlementIndex.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл индекс населенного пункта абитуриента.");
                MessageBox.Show(this, "Введите индекс населенного пункта абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSettlementIndex.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbStreetName.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл наименование улицы проживания абитуриента.");
                MessageBox.Show(this, "Введите наименование улицы абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbStreetName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbNumberHouse.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл номер дома абитуриента.");
                MessageBox.Show(this, "Введите номер дома абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbNumberHouse.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbMobilePhone.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл мобильный номер телефона абитуриента.");
                MessageBox.Show(this, "Введите мобильный номер телефона абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbMobilePhone.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbHomePhone.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл домашний номер телефона абитуриента.");
                MessageBox.Show(this, "Введите домашний номер телефона абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbHomePhone.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSchoolYear.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл год окончания учебного заведения абитуриентом.");
                MessageBox.Show(this, "Введите год окончания учебного заведения абитуриентом", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSchoolYear.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSchoolName.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл наименование учебного заведения абитуриента.");
                MessageBox.Show(this, "Введите наименование учебного заведения абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSchoolName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSchoolAdres.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл адрес учебного заведения абитуриента.");
                MessageBox.Show(this, "Введите адрес учебного заведения абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSchoolName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbPersonInCharge.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл ФИО лица ответственного за приём документов.");
                MessageBox.Show(this, "Введите лицо отвественное за приём документов", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 2;
                tbPersonInCharge.Focus();
            }
            else if (string.IsNullOrEmpty(tbAverage.Text))
            {
                logger.Warn($"Пользователь: {activeEmployee.Fullname.Trim()} не ввёл оценки абитуриента.");
                MessageBox.Show(this, "Введите оценки абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 2;
            }
            else
            {
                // Заполняем поля регистрируемого абитуриента
                enrollee.SpecialityId = speciality.SpecialityId;
                enrollee.CitizenshipId = citizenship.CitizenshipId;
                enrollee.CountryId = country.CountryId;
                enrollee.AreaId = area.AreaId;
                enrollee.DistrictId = district.DistrictId;
                enrollee.SettlementTypeId = typeOfSettlement.SettlementTypeId;
                enrollee.StreetTypeId = typeOfStreet.StreetTypeId;
                enrollee.DocumentId = document.DocumentId;
                enrollee.SchoolTypeId = typeOfSchool.SchoolTypeId;
                enrollee.ForeignLanguageId = foreignLanguage.LanguageId;
                enrollee.ReasonForAddmissionId = reasonForAddmission.ReasonForAddmissionId;
                enrollee.StateTypeId = typeOfState.StateId;
                enrollee.EmployeeId = activeEmployee.EmployeeId;
                enrollee.FinanceTypeId = typeOfFinance.FinanceTypeId;
                if (HasEnroll) enrollee.DecreeId = decree.DecreeId;
                else enrollee.DecreeId = null;
                if (HasSecondarySpeciality) enrollee.SecondarySpecialityId = secondarySpeciality.SecondarySpecialityId;
                else enrollee.SecondarySpecialityId = null;
                if (cbTarget.Checked) enrollee.TargetWorkPlaceId = targetWorkPlace.TargetId;
                else enrollee.TargetWorkPlaceId = null;
                enrollee.RuSurname = tbRuSurname.Text;
                enrollee.RuName = tbRuName.Text;
                enrollee.RuPatronymic = tbRuPatronymic.Text;
                enrollee.BlrSurname = tbBlrSurname.Text;
                enrollee.BlrName = tbBlrName.Text;
                enrollee.BlrPatronymic = tbBlrPatronymic.Text;
                if (rbMale.Checked) enrollee.Gender = "М";
                else enrollee.Gender = "Ж";
                enrollee.DateOfBirthday = dtBirthday.Value;
                enrollee.FatherFullname = tbFatherInfo.Text;
                enrollee.FatherAddress = tbFatherAdres.Text;
                enrollee.MotherFullname = tbMotherInfo.Text;
                enrollee.MotherAddress = tbMotherAdres.Text;
                enrollee.SettlementName = tbSettlementName.Text;
                enrollee.SettlementIndex = Int32.Parse(tbSettlementIndex.Text);
                enrollee.StreetName = tbStreetName.Text;
                enrollee.NumberHouse = tbNumberHouse.Text;
                enrollee.NumberFlat = tbNumberFlat.Text;
                enrollee.MobilePhone = tbMobilePhone.Text;
                enrollee.HomePhone = tbHomePhone.Text;
                enrollee.DocumentSeria = tbDocSeria.Text;
                enrollee.DocumentNumber = tbDocNumber.Text;
                enrollee.DocumentDate = dtDocDate.Value;
                enrollee.DocumentWhoGave = tbDocWhoGave.Text;
                enrollee.DocumentPersonalNumber = tbDocPersonalNumber.Text;
                enrollee.SchoolName = tbSchoolName.Text;
                enrollee.SchoolAddress = tbSchoolAdres.Text;
                enrollee.SchoolYear = tbSchoolYear.Text;
                enrollee.IsBRSM = cbBrsm.Checked;
                enrollee.PersonInCharge = tbPersonInCharge.Text;
                enrollee.Seniority = tbSeniority.Text;
                enrollee.WorkPlace = tbWorkPlace.Text;
                enrollee.WorkPost = tbWorkPost.Text;
                enrollee.CurrentNumberCurs = tbCurrentNumberCurs.Text;
                enrollee.CurrentSpeciality = tbCurrentSpeciality.Text;
                enrollee.CurrentUniversity = tbCurrentUniversity.Text;
                enrollee.AttestatEstimationString = tbFirstAttestatString.Text;
                enrollee.DiplomPtuEstimationString = tbFirstDiplomPtuString.Text;
                enrollee.DiplomSusEstimationString = tbFirstDiplomSsuzString.Text;
                // Если режим редактирования
                if (editMode)
                {
                    enrollee.NumberOfDeal = Int32.Parse(tbNumberOfDeal.Text);
                    enrollee.DateDeal = dtDateDeal.Value;
                    enrollee.StateDateChange = DateTime.Now;
                    // Обновляем данные
                    logger.Info($"Редактирование абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} \n" +
                                $"Оператор {activeEmployee.Fullname.Trim()}");
                    enrolleeService.UpdateEnrollee(enrollee);
                }
                // Режим добавления и регистрации
                else
                {
                    enrollee.NumberOfDeal = GetNumberOfDeal();
                    enrollee.DateDeal = DateTime.Now;
                    enrollee.StateDateChange = DateTime.Now;
                    // Добавление данных
                    logger.Info($"Регистрация абитуриента {enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} \n" +
                                $"Оператор {activeEmployee.Fullname.Trim()}");
                    enrolleeService.InsertEnrollee(enrollee);
                }
            }
            return enrollee;
        }
        /// <summary>
        /// Получение следующего номера личного дела абитуриента
        /// </summary>
        /// <returns></returns>
        private int GetNumberOfDeal()
        {
            int result = 0;
            // Список абитуриентов
            var enrollees = enrolleeService.GetEnrollees(speciality).OrderBy(e => e.NumberOfDeal).ToList();
            if (enrollees.Count == 0)
            {
                // Если список абитуриентов специальности пустой, то номер личного дела = 1
                result = 1;
            }
            else
            {
                // Иначе следующий по порядку
                result = enrollees[enrollees.Count - 1].NumberOfDeal + 1;
            }
            return result;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabControl.TabPages["tabPage3"])//your specific tabname
            {
                // your stuff
                SetSertificateTableStyle();
            }
        }
    }
}

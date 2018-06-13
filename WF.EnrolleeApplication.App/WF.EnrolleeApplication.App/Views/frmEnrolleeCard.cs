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
    public partial class frmEnrolleeCard : Form
    {
        // Изменяемые объекты-сущности
        private Area area;
        private Atribute atribute;
        private Citizenship citizenship;
        private Contest contest;
        private ConversionSystem system;
        private Country country;
        private Decree decree;
        private District district;
        private Document document;
        private Employee employee;
        private Enrollee enrollee;
        private Faculty faculty;
        private ForeignLanguage foreignLanguage;
        private FormOfStudy formOfStudy;
        private ReasonForAddmission reasonForAddmission;
        private SecondarySpeciality secondarySpeciality;
        private Speciality speciality;
        private TargetWorkPlace targetWorkPlace;
        private TypeOfFinance typeOfFinance;
        private TypeOfSchool typeOfSchool;
        private TypeOfSettlement typeOfSettlement;
        private TypeOfState typeOfState;
        private TypeOfStreet typeOfStreet;
        // Сервисы для получения данных
        private AreaService areaService;
        private AssessmentService assessmentService;
        private AtributeService atributeService;
        private AtributeForEnrolleeService atributeForEnrolleeService;
        private CitizenshipService citizenshipService;
        private ContestService contestService;
        private ConversionSystemService conversionSystemService;
        private CountryService countryService;
        private DecreeService decreeService;
        private DisciplineService disciplineService;
        private DistrictService districtService;
        private DocumentService documentService;
        private EnrolleeService enrolleeService;
        private ExamSchemaService examSchemaService;
        private FacultyService facultyService;
        private ForeignLanguageService foreignLanguageService;
        private FormOfStudyService formOfStudyService;
        private IntegrationOfSpecialitiesService integrationOfSpecialitiesService;
        private PriorityOfSpecialityService priorityOfSpecialityService;
        private ReasonForAddmissionService reasonForAddmissionService;
        private SecondarySpecialityService secondarySpecialityService;
        private SpecialityService specialityService;
        private TargetWorkPlaceService targetWorkPlaceService;
        private TypeOfFinanceService typeOfFinanceService;
        private TypeOfSchoolService typeOfSchoolService;
        private TypeOfSettlementService typeOfSettlementService;
        private TypeOfStateService typeOfStateService;
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
            this.employee = employee;
            this.enrollee = new Enrollee();
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
            this.employee = employee;
            this.enrollee = editEnrollee;
            // активируем режим редактирования
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
            // Заполняем поля данными абитуриента
            FillEditData(editEnrollee);
        }
        /// <summary>
        /// Инициализация сервисов доступа к базе данных
        /// </summary>
        private void InitializeDataAccessServices()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
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
            // Информация об абитуриенте
            cbFaculty.SelectedValue = editEnrollee.Speciality.FacultyId;
            cbFormOfStudy.SelectedValue = editEnrollee.Speciality.FormOfStudyId;
            cbSpeciality.SelectedValue = editEnrollee.SpecialityId;
            tbRuSurname.Text = editEnrollee.RuSurname;
            tbRuName.Text = editEnrollee.RuName;
            tbRuPatronymic.Text = editEnrollee.RuPatronymic;
            tbBlrSurname.Text = editEnrollee.BlrSurname;
            tbBlrName.Text = editEnrollee.BlrName;
            tbBlrPatronymic.Text = editEnrollee.BlrPatronymic;
            dtBirthday.Value = editEnrollee.DateOfBirthday;
            if (editEnrollee.Gender == "М") rbMale.Checked = true;
            else rbFemale.Checked = true;
            cbCitizenship.SelectedValue = editEnrollee.CitizenshipId;
            cbDocument.SelectedValue = editEnrollee.DocumentId;
            tbDocSeria.Text = editEnrollee.DocumentSeria;
            tbDocNumber.Text = editEnrollee.DocumentNumber;
            dtDocDate.Value = editEnrollee.DocumentDate;
            tbDocWhoGave.Text = editEnrollee.DocumentWhoGave;
            tbDocPersonalNumber.Text = editEnrollee.DocumentPersonalNumber;
            tbFatherInfo.Text = editEnrollee.FatherFullname;
            tbFatherAdres.Text = editEnrollee.FatherAddress;
            tbMotherInfo.Text = editEnrollee.MotherFullname;
            tbMotherAdres.Text = editEnrollee.MotherAddress;
            // Дополнительная информация
            cbCountry.SelectedValue = editEnrollee.CountryId;
            cbArea.SelectedValue = editEnrollee.AreaId;
            cbDistrict.SelectedValue = editEnrollee.DistrictId;
            cbTypeOfSettlement.SelectedValue = editEnrollee.SettlementTypeId;
            cbTypeOfStreet.SelectedValue = editEnrollee.StreetTypeId;
            tbSettlementName.Text = editEnrollee.SettlementName;
            tbSettlementIndex.Text = editEnrollee.SettlementIndex.ToString();
            tbStreetName.Text = editEnrollee.StreetName;
            tbNumberHouse.Text = editEnrollee.NumberHouse;
            tbNumberFlat.Text = editEnrollee.NumberFlat;
            tbMobilePhone.Text = editEnrollee.MobilePhone;
            tbHomePhone.Text = editEnrollee.HomePhone;
            tbSchoolYear.Text = editEnrollee.SchoolYear;
            tbSchoolAdres.Text = editEnrollee.SchoolAddress;
            tbSchoolName.Text = editEnrollee.SchoolName;
            cbTypeOfSchool.SelectedValue = editEnrollee.SchoolTypeId;
            if (editEnrollee.SecondarySpecialityId.HasValue) cbSecondarySpeciality.SelectedValue = editEnrollee.SecondarySpecialityId;
            if (editEnrollee.CurrentNumberCurs != "-" || editEnrollee.CurrentSpeciality != "-" || editEnrollee.CurrentUniversity != "-")
            {
                cbSecondEducation.Checked = true;
                tbCurrentNumberCurs.Text = editEnrollee.CurrentNumberCurs;
                tbCurrentSpeciality.Text = editEnrollee.CurrentSpeciality;
                tbCurrentUniversity.Text = editEnrollee.CurrentUniversity;
            }
            else cbSecondEducation.Checked = false;
            cbBrsm.Checked = editEnrollee.IsBRSM;
            cbForeignLanguage.SelectedValue = editEnrollee.ForeignLanguageId;
            if (editEnrollee.DecreeId.HasValue) cbDecree.SelectedValue = editEnrollee.DecreeId;
            dtDateDeal.Value = editEnrollee.DateDeal;
            if (IsWorker)
            {
                tbSeniority.Text = editEnrollee.Seniority.ToString();
                tbWorkPlace.Text = editEnrollee.WorkPlace;
                tbWorkPost.Text = editEnrollee.WorkPost;
            }
            // Поступление
            cbTypeOfFinance.SelectedValue = editEnrollee.FinanceTypeId;
            cbContest.SelectedValue = editEnrollee.ReasonForAddmission.ContestId;
            cbReasonForAddmission.SelectedValue = editEnrollee.ReasonForAddmissionId;
            tbFirstAttestatString.Text = editEnrollee.AttestatEstimationString;
            tbSecondAttestatString.Text = editEnrollee.AttestatEstimationString;
            tbFirstDiplomPtuString.Text = editEnrollee.DiplomPtuEstimationString;
            tbSecondDiplomPtuString.Text = editEnrollee.DiplomPtuEstimationString;
            tbFirstDiplomSsuzString.Text = editEnrollee.DiplomSusEstimationString;
            tbSecondDiplomSsuzString.Text = editEnrollee.DiplomSusEstimationString;
            if (editEnrollee.TargetWorkPlaceId.HasValue)
            {
                cbTarget.Checked = true;
                cbTargetWorkPlace.SelectedValue = editEnrollee.TargetWorkPlaceId;
            }
            else
            {
                cbTarget.Checked = false;
            }
            tbNumberOfDeal.Visible = true;
            tbNumberOfDeal.Text = editEnrollee.NumberOfDeal.ToString();
            cbTypeOfState.SelectedValue = editEnrollee.StateTypeId;
            tbPersonInCharge.Text = editEnrollee.PersonInCharge;
            InitializeSertificationGrid(editEnrollee);
            InitializePrioritySpecialityGrid(editEnrollee);
            InitializeAtributeList(editEnrollee);



        }

        #region Создание, настройка и заполнение таблиц данных
        /// <summary>
        /// Установка стиля отображения таблицы сертификатов (дисциплин)
        /// </summary>
        private void SetSertificateTableStyle()
        {
            SertificateGrid.Columns[0].FillWeight = 10;
            SertificateGrid.Columns[1].FillWeight = 10;
            SertificateGrid.Columns[2].FillWeight = 60;
            SertificateGrid.Columns[3].FillWeight = 20;
            SertificateGrid.Columns[4].FillWeight = 20;
            SertificateGrid.Columns[5].FillWeight = 10;
            SertificateGrid.Columns[6].FillWeight = 10;
            SertificateGrid.Columns[0].Visible = false;
            SertificateGrid.Columns[1].Visible = false;
            SertificateGrid.Columns[5].Visible = false;
            SertificateGrid.Columns[6].Visible = false;
            SertificateGrid.Columns["Дисциплина"].ReadOnly = true;
        }
        /// <summary>
        /// Создаем струтуру таблицы данных сертификатов и предметов
        /// </summary>
        /// <returns></returns>
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
            PriorityGrid.Columns[2].Visible = false;
            PriorityGrid.Columns[0].FillWeight = 5;
            PriorityGrid.Columns[1].FillWeight = 20;
            PriorityGrid.Columns[2].FillWeight = 5;
            PriorityGrid.Columns[3].FillWeight = 70;
        }
        /// <summary>
        /// Создаем структуру таблицы данных списка приоритетов специальностей
        /// </summary>
        /// <returns></returns>
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
            if (!editMode)
            {
                if (speciality != null)
                {
                    priorityTable.Rows.Clear();
                    if (speciality.IsGroup) // если общий конкурс
                    {
                        var specialities = specialityService.GetSpecialities(speciality);
                        if (specialities.Count != 0)
                        {
                            int lvl_priority = 1;
                            foreach (var specialityGroup in specialities)
                            {
                                List<IntegrationOfSpecialities> listIntegration = integrationOfSpecialitiesService.GetIntegrationOfSpecialities(specialityGroup);
                                if (listIntegration.Count != 0)
                                {
                                    foreach (IntegrationOfSpecialities itemIntegration in listIntegration)
                                    {
                                        if (itemIntegration.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId)
                                        {
                                            priorityTable.Rows.Add(lvl_priority, specialityGroup.FormOfStudy.Fullname, specialityGroup.SpecialityId, specialityGroup.Fullname);
                                            lvl_priority++;
                                        }
                                    }
                                }
                                else
                                {
                                    priorityTable.Rows.Add(lvl_priority, specialityGroup.FormOfStudy.Fullname, specialityGroup.SpecialityId, specialityGroup.Fullname);
                                    lvl_priority++;
                                }
                            }
                        }
                    }
                    else
                    {
                        priorityTable.Rows.Add(1, speciality.FormOfStudy.Fullname, speciality.SpecialityId, speciality.Fullname);
                    }
                }
                SetPriorityTableStyle();
            }
        }
        /// <summary>
        /// Заполнение таблицы приоритетов специальностей выбранного абитуриента
        /// </summary>
        /// <param name="enrollee">Профиль редактируемого абитуриента</param>
        private void InitializePrioritySpecialityGrid(Enrollee enrollee)
        {
            List<PriorityOfSpeciality> priorities = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee);
            priorityTable.Rows.Clear();
            foreach (var priority in priorities)
                priorityTable.Rows.Add(priority.PriorityLevel, priority.Speciality.FormOfStudy.Fullname, priority.SpecialityId, priority.Speciality.Fullname);
            SetPriorityTableStyle();
        }
        /// <summary>
        /// Заполнение таблицы сертификатов (дисциплин) согласно экзаменнационной схеме
        /// </summary>
        private void InitializeSertificationGrid()
        {
            if (!editMode)
            {
                List<ExamSchema> schema = examSchemaService.GetExamSchemas(speciality).Where(ex => ex.Discipline.BasisForAssessingId != 1).ToList();
                sertificateTable.Rows.Clear();
                if (schema.Count != 0)
                {
                    foreach (ExamSchema item in schema)
                    {
                        Discipline discipline = disciplineService.GetDiscipline(item.DisciplineId);
                        if (discipline != null && discipline.IsGroup)
                        {
                            var alternatives = disciplineService.GetDisciplines(discipline);
                            foreach (var alternative in alternatives)
                                sertificateTable.Rows.Add(alternative.DisciplineId, alternative.BasisForAssessingId, alternative.Name, "", "", "", "");
                        }
                        else
                        {
                            sertificateTable.Rows.Add(discipline.DisciplineId, discipline.BasisForAssessingId, discipline.Name, "", "", "", "");
                        }
                    }
                }
                SetSertificateTableStyle();
            }
        }
        /// <summary>
        /// Заполнение таблицы сертификатов (дисциплин) выбранного абитуриента
        /// </summary>
        /// <param name="enrollee">Профиль редактируемого абитуриента</param>
        private void InitializeSertificationGrid(Enrollee enrollee)
        {
            List<Assessment> assessments = assessmentService.GetAssessments(enrollee).Where(a => a.Discipline.BasisForAssessingId != 1).ToList();
            sertificateTable.Rows.Clear();
            foreach (var assessment in assessments)
                sertificateTable.Rows.Add(assessment.DisciplineId, assessment.Discipline.BasisForAssessingId, assessment.Discipline.Name, assessment.SertCode, assessment.Estimation, assessment.SertDate, assessment.ChangeDiscipline);
            SetSertificateTableStyle();
        }
        #endregion
        #region Установка списка льгот
        /// <summary>
        /// Загружаем полный список льгот
        /// </summary>
        private void InitializeAtributeList()
        {
            var atributes = atributeService.GetAtributes();
            foreach (var atribute in atributes)
                chkAtributeList.Items.Add(atribute.Fullname);
        }
        /// <summary>
        /// Загружаем льготы выбранного абитуриента
        /// </summary>
        /// <param name="enrollee">Профиль редактируемого абитуриента</param>
        private void InitializeAtributeList(Enrollee enrollee)
        {
            var atributes = atributeForEnrolleeService.GetAtributeForEnrollees(enrollee);
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
            AutoCompleteStringCollection sourceSurnameRus = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceNameRus = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourcePatronymicRus = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceSurnameBlr = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceNameBlr = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourcePatronymicBlr = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceDocumentWhoGave = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceMotherAdress = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceFatherAdress = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceCountry = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceArea = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceDistrict = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceSettlement = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceStreet = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceSchoolName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceSchoolAdress = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceCurrentUniversity = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceCurrentSpeciality = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceWorkPlace = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceWorkPost = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceTarget = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourcePersonInCharge = new AutoCompleteStringCollection();
            enrollees = enrolleeService.GetEnrollees();

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
        /// Инициализация выдающих списков данными
        /// </summary>
        private void InitializeComboBoxes()
        {
            InitializeFacultyComboBox();
            InitializeFormOfStudyComboBox();
            InitializeCitizenshipComboBox();
            InitializeDocumentComboBox();
            InitializeCountryComboBox();
            InitializeAreaComboBox();
            InitializeDistrictComboBox();
            InitializeTypeOfSettlementComboBox();
            InitializeTypeOfStreetComboBox();
            InitializeTypeOfSchoolComboBox();
            InitializeForeignLanguageComboBox();
            InitializeTypeOfFinanceComboBox();
            InitializeContestComboBox();
            InitializeTypeOfStateComboBox();
            InitializeTransferSystem();
            InitializeDecreeComboBox();
            InitializeTargetWorkPlace();
        }
        /// <summary>
        /// Загрузка данных в список "Целевые места"
        /// </summary>
        private void InitializeTargetWorkPlace()
        {
            cbTargetWorkPlace.SelectedValueChanged -= cbTargetWorkPlace_SelectedValueChanged;
            var targets = targetWorkPlaceService.GetTargetWorkPlaces();
            cbTargetWorkPlace.DataSource = targets;
            cbTargetWorkPlace.DisplayMember = "Name";
            cbTargetWorkPlace.ValueMember = "TargetId";
            if (targets.Count != 0) targetWorkPlace = targets[0];
            cbTargetWorkPlace.SelectedValueChanged += cbTargetWorkPlace_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Приказы"
        /// </summary>
        private void InitializeDecreeComboBox()
        {
            cbDecree.SelectedValueChanged -= cbDecree_SelectedValueChanged;
            var decrees = decreeService.GetDecrees();
            cbDecree.DataSource = decrees;
            cbDecree.DisplayMember = "DecreeNumber";
            cbDecree.ValueMember = "DecreeId";
            if (decrees.Count != 0) decree = decrees[0];
            cbDecree.SelectedValueChanged += cbDecree_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Районы"
        /// </summary>
        private void InitializeDistrictComboBox()
        {
            cbDistrict.SelectedValueChanged -= cbDistrict_SelectedValueChanged;
            var districts = districtService.GetDistricts();
            cbDistrict.DataSource = districts;
            cbDistrict.DisplayMember = "Name";
            cbDistrict.ValueMember = "DistrictId";
            if (districts.Count != 0) district = districts[0];
            cbDistrict.SelectedValueChanged += cbDistrict_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Области"
        /// </summary>
        private void InitializeAreaComboBox()
        {
            cbArea.SelectedValueChanged -= cbArea_SelectedValueChanged;
            var areas = areaService.GetAreas();
            cbArea.DataSource = areas;
            cbArea.DisplayMember = "Name";
            cbArea.ValueMember = "AreaId";
            if (areas.Count != 0) area = areas[0];
            cbArea.SelectedValueChanged += cbArea_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Страны"
        /// </summary>
        private void InitializeCountryComboBox()
        {
            cbCountry.SelectedValueChanged -= cbCountry_SelectedValueChanged;
            var countries = countryService.GetCountries();
            cbCountry.DataSource = countries;
            cbCountry.DisplayMember = "Name";
            cbCountry.ValueMember = "CountryId";
            if (countries.Count != 0) country = countries[0];
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
            cbTypeOfState.SelectedValueChanged -= cbTypeOfState_SelectedValueChanged;
            var states = typeOfStateService.GetTypeOfStates().OrderBy(s => s.StateId).ToList();
            cbTypeOfState.DataSource = states;
            cbTypeOfState.DisplayMember = "Name";
            cbTypeOfState.ValueMember = "StateId";
            if (states.Count != 0) typeOfState = states[0];
            cbTypeOfState.SelectedValueChanged += cbTypeOfState_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Основания зачисления"
        /// </summary>
        private void InitializeReasonForAddmissionComboBox()
        {
            cbReasonForAddmission.SelectedValueChanged -= cbReasonForAddmission_SelectedValueChanged;
            var reasons = reasonForAddmissionService.GetReasonForAddmissions(contest);
            cbReasonForAddmission.DataSource = reasons;
            cbReasonForAddmission.DisplayMember = "Fullname";
            cbReasonForAddmission.ValueMember = "ReasonForAddmissionId";
            if (reasons.Count != 0) reasonForAddmission = reasons[0];
            cbReasonForAddmission.SelectedValueChanged += cbReasonForAddmission_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Виды конкурса"
        /// </summary>
        private void InitializeContestComboBox()
        {
            cbContest.SelectedValueChanged -= cbContest_SelectedValueChanged;
            var contests = contestService.GetContests().OrderBy(c => c.ContestId).ToList();
            cbContest.DataSource = contests;
            cbContest.DisplayMember = "Name";
            cbContest.ValueMember = "ContestId";
            if (contests.Count != 0)
            {
                contest = contests[0];
                InitializeReasonForAddmissionComboBox();
            }
            cbContest.SelectedValueChanged += cbContest_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип финансирования"
        /// </summary>
        private void InitializeTypeOfFinanceComboBox()
        {
            cbTypeOfFinance.SelectedValueChanged -= cbTypeOfFinance_SelectedValueChanged;
            var finances = typeOfFinanceService.GetTypeOfFinances();
            cbTypeOfFinance.DataSource = finances;
            cbTypeOfFinance.DisplayMember = "Fullname";
            cbTypeOfFinance.ValueMember = "FinanceTypeId";
            if (finances.Count != 0) typeOfFinance = finances[0];
            cbTypeOfFinance.SelectedValueChanged += cbTypeOfFinance_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Иностранные языки"
        /// </summary>
        private void InitializeForeignLanguageComboBox()
        {
            cbForeignLanguage.SelectedValueChanged -= cbForeignLanguage_SelectedValueChanged;
            var languages = foreignLanguageService.GetForeignLanguages();
            cbForeignLanguage.DataSource = languages;
            cbForeignLanguage.DisplayMember = "Name";
            cbForeignLanguage.ValueMember = "LanguageId";
            if (languages.Count != 0) foreignLanguage = languages[0];
            cbForeignLanguage.SelectedValueChanged += cbForeignLanguage_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Специальности второй ступени"
        /// </summary>
        private void InitializeSecondarySpecialityComboBox()
        {
            cbSecondarySpeciality.SelectedValueChanged -= cbSecondarySpeciality_SelectedValueChanged;
            List<IntegrationOfSpecialities> integrationOfSpecialities = integrationOfSpecialitiesService.GetIntegrationOfSpecialities(speciality);
            List<SecondarySpeciality> secondarySpecialities = new List<SecondarySpeciality>();
            foreach (var integration in integrationOfSpecialities)
            {
                var ss = secondarySpecialityService.GetSecondarySpeciality(integration.SecondarySpecialityId);
                secondarySpecialities.Add(ss);
            }
            cbSecondarySpeciality.DataSource = secondarySpecialities;
            cbSecondarySpeciality.DisplayMember = "Fullname";
            cbSecondarySpeciality.ValueMember = "SecondarySpecialityId";
            if (secondarySpecialities.Count != 0)
            {
                secondarySpeciality = secondarySpecialities[0];
                InitializePrioritySpecialityGrid();
            }
            cbSecondarySpeciality.SelectedValueChanged += cbSecondarySpeciality_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип прошлого учебного заведения"
        /// </summary>
        private void InitializeTypeOfSchoolComboBox()
        {
            cbTypeOfSchool.SelectedValueChanged -= cbTypeOfSchool_SelectedValueChanged;
            var schools = typeOfSchoolService.GetTypeOfSchools().OrderBy(ts => ts.SchoolTypeId).ToList();
            cbTypeOfSchool.DataSource = schools;
            cbTypeOfSchool.DisplayMember = "Name";
            cbTypeOfSchool.ValueMember = "SchoolTypeId";
            if (schools.Count != 0) typeOfSchool = schools[0];
            cbTypeOfSchool.SelectedValueChanged += cbTypeOfSchool_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип улиц"
        /// </summary>
        private void InitializeTypeOfStreetComboBox()
        {
            cbTypeOfStreet.SelectedValueChanged -= cbTypeOfStreet_SelectedValueChanged;
            var streets = typeOfStreetService.GetTypeOfStreets();
            cbTypeOfStreet.DataSource = streets;
            cbTypeOfStreet.DisplayMember = "Fullname";
            cbTypeOfStreet.ValueMember = "StreetTypeId";
            if (streets.Count != 0) typeOfStreet = streets[0];
            cbTypeOfStreet.SelectedValueChanged += cbTypeOfStreet_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Тип населенного пункта"
        /// </summary>
        private void InitializeTypeOfSettlementComboBox()
        {
            cbTypeOfSettlement.SelectedValueChanged -= cbTypeOfSettlement_SelectedValueChanged;
            var settlements = typeOfSettlementService.GetTypeOfSettlements();
            cbTypeOfSettlement.DataSource = settlements;
            cbTypeOfSettlement.DisplayMember = "Fullname";
            cbTypeOfSettlement.ValueMember = "SettlementTypeId";
            if (settlements.Count != 0) typeOfSettlement = settlements[0];
            cbTypeOfSettlement.SelectedValueChanged += cbTypeOfSettlement_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Факультеты"
        /// </summary>
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
        /// <summary>
        /// Загрузка данных в список "Формы обучения"
        /// </summary>
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
        /// <summary>
        /// Загрузка данных в список "Специальности"
        /// </summary>
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
                    InitializeSecondarySpecialityComboBox();
                    InitializeSertificationGrid();
                    InitializePrioritySpecialityGrid();
                }
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }
        /// <summary>
        /// Загрузка данных в список "Вид гражданства"
        /// </summary>
        private void InitializeCitizenshipComboBox()
        {
            cbCitizenship.SelectedValueChanged -= cbCitizenship_SelectedValueChanged;
            var citizenships = citizenshipService.GetCitizenships();
            cbCitizenship.DataSource = citizenships;
            cbCitizenship.DisplayMember = "Fullname";
            cbCitizenship.ValueMember = "CitizenshipId";
            if (citizenships.Count != 0) citizenship = citizenships[0];
            cbCitizenship.SelectedValueChanged += cbCitizenship_SelectedValueChanged;
        }
        /// <summary>
        /// Загрузка данных в список "Документы"
        /// </summary>
        private void InitializeDocumentComboBox()
        {
            cbDocument.SelectedValueChanged -= cbDocument_SelectedValueChanged;
            var documents = documentService.GetDocuments().OrderBy(d => d.DocumentId).ToList();
            cbDocument.DataSource = documents;
            cbDocument.DisplayMember = "Name";
            cbDocument.ValueMember = "DocumentId";
            if (documents.Count != 0) document = documents[0];
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
            if (cbTargetWorkPlace.SelectedValue != null)
            {
                int id = (int)cbTargetWorkPlace.SelectedValue;
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
            if (cbDecree.SelectedValue != null)
            {
                int id = (int)cbDecree.SelectedValue;
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
            if (cbCountry.SelectedValue != null)
            {
                int id = (int)cbCountry.SelectedValue;
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
            if (cbArea.SelectedValue != null)
            {
                int id = (int)cbArea.SelectedValue;
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
            if (cbDistrict.SelectedValue != null)
            {
                int id = (int)cbDistrict.SelectedValue;
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
            if (cbFaculty.SelectedValue != null)
            {
                int id = (int)cbFaculty.SelectedValue;
                faculty = facultyService.GetFaculty(id);
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
            if (cbFormOfStudy.SelectedValue != null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
                // Заполняем специальности
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
            if (cbSpeciality.SelectedValue != null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                speciality = specialityService.GetSpeciality(id);
                InitializeSertificationGrid();
                InitializeSecondarySpecialityComboBox();
            }
        }
        /// <summary>
        /// Получение объекта выпадающего списка
        /// </summary>
        /// <param name="sender">Выпадающий список "Виды гражданства"</param>
        /// <param name="e"></param>
        private void cbCitizenship_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCitizenship.SelectedValue != null)
            {
                int id = (int)cbCitizenship.SelectedValue;
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
            if (cbDocument.SelectedValue != null)
            {
                int id = (int)cbDocument.SelectedValue;
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
            if (cbTypeOfSettlement.SelectedValue != null)
            {
                int id = (int)cbTypeOfSettlement.SelectedValue;
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
            if (cbTypeOfStreet.SelectedValue != null)
            {
                int id = (int)cbTypeOfStreet.SelectedValue;
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
            if (cbTypeOfSchool.SelectedValue != null)
            {
                int id = (int)cbTypeOfSchool.SelectedValue;
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
            if (cbSecondarySpeciality.SelectedValue != null)
            {
                int id = (int)cbSecondarySpeciality.SelectedValue;
                secondarySpeciality = secondarySpecialityService.GetSecondarySpeciality(id);
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
            if (cbForeignLanguage.SelectedValue != null)
            {
                int id = (int)cbForeignLanguage.SelectedValue;
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
            if (cbTypeOfFinance.SelectedValue != null)
            {
                int id = (int)cbTypeOfFinance.SelectedValue;
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
            if (cbContest.SelectedValue != null)
            {
                int id = (int)cbContest.SelectedValue;
                contest = contestService.GetContest(id);
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
            if (cbReasonForAddmission.SelectedValue != null)
            {
                int id = (int)cbReasonForAddmission.SelectedValue;
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
            if (cbTypeOfState.SelectedValue != null)
            {
                int id = (int)cbTypeOfState.SelectedValue;
                typeOfState = typeOfStateService.GetTypeOfState(id);
            }
        }
        #endregion
        #region Видимость полей

        private void cbSecondEducation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSecondEducation.Checked)
            {
                ShowSecondEducationFields(true);
            }
            else
            {
                ShowSecondEducationFields(false);
            }
        }

        private void ShowComboBoxSecondarySpeciality(bool flag)
        {
            HasSecondarySpeciality = flag;
            gbSecondarySpeciality.Visible = flag;
        }

        private void ShowComboBoxesCurrentWorkPlace(bool flag)
        {
            IsWorker = flag;
            gbSeniority.Visible = flag;
            gbWorkPlace.Visible = flag;
            gbWorkPost.Visible = flag;
        }

        private void ShowSecondEducationFields(bool flag)
        {
            gbCurrentCurs.Visible = flag;
            gbCurrentUniversity.Visible = flag;
            gbCurrentSpeciality.Visible = flag;
        }
        #endregion
        #region Работа со списком атрибутов(льгот)
        private void cbTarget_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTarget.Checked)
            {
                cbTargetWorkPlace.Visible = true;
                SetCheckedAtribute(cbTarget.Text.Trim(), true);
            }
            else
            {
                cbTargetWorkPlace.Visible = false;
                SetCheckedAtribute(cbTarget.Text.Trim(), false);
            }
        }

        private void cbBrsm_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBrsm.Checked) SetCheckedAtribute(cbBrsm.Text, true);
            else SetCheckedAtribute(cbBrsm.Text, false);
        }

        private void SetCheckedAtribute(string name, bool flag)
        {
            // Дополнительные атрибуты
            for (int i = 0; i < chkAtributeList.Items.Count; i++)
            {
                string nameInListBox = chkAtributeList.Items[i].ToString();
                if (string.Equals(nameInListBox, name))
                {
                    Atribute atribute = atributeService.GetAtribute(name);
                    if (atribute.AtributeId != 0 && atribute.Fullname.Trim() == name) chkAtributeList.SetItemChecked(i, flag);
                }
            }
        }

        private void chkAtributeList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string name = chkAtributeList.Items[e.Index].ToString();
            atribute = atributeService.GetAtribute(name);
            // БРСМ
            if (atribute.Fullname == cbBrsm.Text)
            {
                if (e.NewValue == CheckState.Checked)
                    cbBrsm.Checked = true;
                else
                    cbBrsm.Checked = false;
            }

            // Целевое направление
            if (atribute.Fullname == cbTarget.Text)
            {
                if (e.NewValue == CheckState.Checked)
                    cbTarget.Checked = true;
                else
                    cbTarget.Checked = false;
            }
        }
        #endregion
        #region Управление сертификатами
        private void btDeleteDiscipline_Click(object sender, EventArgs e)
        {
            int rowIndex = SertificateGrid.CurrentRow.Index;
            sertificateTable.Rows[rowIndex].Delete();
        }

        private void btGetExamSchema_Click(object sender, EventArgs e)
        {
            InitializeSertificationGrid();
        }

        private void btChangeDiscipline_Click(object sender, EventArgs e)
        {
            frmChangeDiscipline changeDisciplineCard = new frmChangeDiscipline();
            DialogResult changeDisciplineCardResult = changeDisciplineCard.ShowDialog();
            if (changeDisciplineCardResult == DialogResult.OK)
            {
                int rowIndex = SertificateGrid.CurrentRow.Index;
                SertificateGrid.Rows[rowIndex].Cells[6].Value = changeDisciplineCard.discipline.Name;
            }
        }
        #endregion
        #region Установка языков
        private void SetBelorussianLanguage(object sender, EventArgs e)
        {
            ChangeInputLanguage change = new ChangeInputLanguage();
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage il in coll)
            {
                if (il.Culture.Name == "be-BY") change.ChangeLanguage(il);
            }
        }

        private void SetEnglishLanguage(object sender, EventArgs e)
        {
            ChangeInputLanguage change = new ChangeInputLanguage();
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage il in coll)
            {
                if (il.Culture.Name == "en-US") change.ChangeLanguage(il);
            }
        }

        private void SetDefaultLanguage(object sender, EventArgs e)
        {
            ChangeInputLanguage change = new ChangeInputLanguage();
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            change.ChangeLanguage(InputLanguage.DefaultInputLanguage);
        }
        #endregion
        #region Ввод цифр
        private void AllowNumeric(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

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
        // Считаем оценки
        private void CalculateEstimation(TextBox firstString, TextBox secondString, ComboBox transferSystem, TextBox averageString)
        {
            if (firstString.Text.Equals(secondString.Text))
            {
                bool isTen = isTenSystem(firstString.Text);
                switch (transferSystem.SelectedIndex)
                {
                    case 0: // из 10 в 100
                        {
                            int count = firstString.Text.Length;
                            int sum = 0;
                            for (int i = 0; i < count; i++)
                            {
                                int mark = (int)Char.GetNumericValue(firstString.Text[i]);
                                if (mark == 0) mark = 10;
                                sum += mark * 10;
                            }
                            double avr = Math.Round((double)sum / count, MidpointRounding.AwayFromZero);
                            if (!Double.IsNaN(avr))
                            {
                                averageString.Text = avr.ToString();
                            }
                            else
                            {
                                averageString.Text = "0";
                            }
                            break;
                        }
                    case 1:
                        {
                            if (!isTen)
                            {
                                int count = firstString.Text.Length;
                                double sum = 0;
                                for (int i = 0; i < count; i++)
                                {
                                    int mark = (int)Char.GetNumericValue(firstString.Text[i]);
                                    double ten = conversionSystemService.ConversionToTen(mark);
                                    sum += ten;
                                }
                                try
                                {
                                    double avr = Math.Round(sum / count, 1);
                                    double ball = avr * 10;
                                    averageString.Text = ball.ToString();
                                }
                                catch (Exception ex)
                                {
                                    averageString.Text = "";
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
        // Среднее со всех документов
        private void CalculateAverageEstimation(TextBox attestat, TextBox diplomSsuz, TextBox diplomPtu)
        {
            double avgAttestat = 0;
            double avgDiplomPTU = 0;
            double avgDiplomSSUZ = 0;

            if (!string.IsNullOrWhiteSpace(attestat.Text)) avgAttestat = Double.Parse(attestat.Text);
            if (!string.IsNullOrWhiteSpace(diplomSsuz.Text)) avgDiplomSSUZ = Double.Parse(diplomSsuz.Text);
            if (!string.IsNullOrWhiteSpace(diplomPtu.Text)) avgDiplomPTU = Double.Parse(diplomPtu.Text);

            int count = 0;
            if (avgAttestat != 0) count++;
            if (avgDiplomPTU != 0) count++;
            if (avgDiplomSSUZ != 0) count++;

            double sum = avgAttestat + avgDiplomSSUZ + avgDiplomPTU;
            double avr = Math.Round((double)sum / count, MidpointRounding.AwayFromZero);
            if (Double.IsNaN(avr)) tbAverage.Text = String.Empty;
            else tbAverage.Text = avr.ToString();
        }
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
        private void cbSystemAttestat_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstAttestatString, tbSecondAttestatString, cbSystemAttestat, tbAverageAttestat);
        }
        private void tbFirstAttestatString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstAttestatString, tbSecondAttestatString, cbSystemAttestat, tbAverageAttestat);
        }

        private void tbSecondAttestatString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstAttestatString, tbSecondAttestatString, cbSystemAttestat, tbAverageAttestat);
        }

        private void cbSystemDiplomPtu_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomPtuString, tbSecondDiplomPtuString, cbSystemDiplomPtu, tbAverageDiplomPtu);
        }

        private void tbFirstDiplomPtuString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomPtuString, tbSecondDiplomPtuString, cbSystemDiplomPtu, tbAverageDiplomPtu);
        }

        private void tbSecondDiplomPtuString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomPtuString, tbSecondDiplomPtuString, cbSystemDiplomPtu, tbAverageDiplomPtu);
        }
        private void cbSystemDiplomSsuz_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomSsuzString, tbSecondDiplomSsuzString, cbSystemDiplomSsuz, tbAverageDiplomSsuz);
        }
        private void tbFirstDiplomSsuzString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomSsuzString, tbSecondDiplomSsuzString, cbSystemDiplomSsuz, tbAverageDiplomSsuz);
        }
        private void tbSecondDiplomSsuzString_TextChanged(object sender, EventArgs e)
        {
            CalculateEstimation(tbFirstDiplomSsuzString, tbSecondDiplomSsuzString, cbSystemDiplomSsuz, tbAverageDiplomSsuz);
        }
        private void tbAverageAttestat_TextChanged(object sender, EventArgs e)
        {
            CalculateAverageEstimation(tbAverageAttestat, tbAverageDiplomSsuz, tbAverageDiplomPtu);
        }

        private void tbAverageDiplomPtu_TextChanged(object sender, EventArgs e)
        {
            CalculateAverageEstimation(tbAverageAttestat, tbAverageDiplomSsuz, tbAverageDiplomPtu);
        }

        private void tbAverageDiplomSsuz_TextChanged(object sender, EventArgs e)
        {
            CalculateAverageEstimation(tbAverageAttestat, tbAverageDiplomSsuz, tbAverageDiplomPtu);
        }
        #endregion
        #region Изменение порядка(ранга) приоритета
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

        private void ClearClipboard(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            Enrollee savedEnrollee = SaveCurrentEnrollee(enrollee);
            if (savedEnrollee != null)
            {
                List<Assessment> assessments = GetAssessments(savedEnrollee);
                SaveAssessments(assessments);
                List<AtributeForEnrollee> atributes = GetAtributes(savedEnrollee);
                SaveAtributes(atributes);
                List<PriorityOfSpeciality> priorities = GetPriorityList(savedEnrollee);
                SavePriorities(priorities);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void SavePriorities(List<PriorityOfSpeciality> priorities)
        {
            foreach (var priority in priorities)
                priorityOfSpecialityService.InsertPriorityOfSpeciality(priority);
        }
        private void SaveAtributes(List<AtributeForEnrollee> atributes)
        {
            foreach (var atribute in atributes)
                atributeForEnrolleeService.InsertAtributeForEnrollee(atribute);
        }
        private List<AtributeForEnrollee> GetAtributes(Enrollee savedEnrollee)
        {
            List<AtributeForEnrollee> list = new List<AtributeForEnrollee>();
            foreach (int i in chkAtributeList.CheckedIndices)
            {
                string name = chkAtributeList.Items[i].ToString();
                Atribute currentAtribute = atributeService.GetAtribute(name);
                AtributeForEnrollee atribute = new AtributeForEnrollee();
                atribute.AtributeId = currentAtribute.AtributeId;
                atribute.EnrolleeId = savedEnrollee.EnrolleeId;
                list.Add(atribute);
            }
            return list;
        }
        private void SaveAssessments(List<Assessment> assessments)
        {
            foreach (var assessment in assessments)
            {
                assessmentService.InsertAssessment(assessment);
            }
        }
        private List<Assessment> GetAssessments(Enrollee savedEnrollee)
        {
            List<Assessment> list = new List<Assessment>();
            // Оценка документа об образовании
            Assessment docAssessment = new Assessment();
            docAssessment.DisciplineId = 5;
            docAssessment.EnrolleeId = savedEnrollee.EnrolleeId;
            docAssessment.Estimation = Int32.Parse(tbAverage.Text);
            list.Add(docAssessment);
            // Оценки таблицы сертификатов
            foreach (DataGridViewRow row in SertificateGrid.Rows)
            {
                Assessment assessment = new Assessment();
                assessment.DisciplineId = Int32.Parse(row.Cells[0].Value.ToString());
                assessment.EnrolleeId = savedEnrollee.EnrolleeId;
                assessment.Estimation = Int32.Parse(row.Cells[4].Value.ToString());
                assessment.SertCode = row.Cells[3].Value.ToString();
                assessment.SertDate = row.Cells[5].Value.ToString();
                assessment.ChangeDiscipline = row.Cells[6].Value.ToString();
                list.Add(assessment);
            }
            return list;
        }
        private List<PriorityOfSpeciality> GetPriorityList(Enrollee savedEnrollee)
        {
            List<PriorityOfSpeciality> list = new List<PriorityOfSpeciality>();
            foreach (DataGridViewRow row in PriorityGrid.Rows)
            {
                PriorityOfSpeciality priority = new PriorityOfSpeciality();
                priority.EnrolleeId = savedEnrollee.EnrolleeId;
                priority.SpecialityId = Int32.Parse(row.Cells[2].Value.ToString());
                priority.PriorityLevel = Int32.Parse(row.Cells[0].Value.ToString());
                list.Add(priority);
            }
            return list;
        }
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
                MessageBox.Show(this, "Введите имя абитуриента на русском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbRuName.Focus();
            }
            else if (string.IsNullOrEmpty(tbRuPatronymic.Text))
            {
                MessageBox.Show(this, "Введите отчество абитуриента на русском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbRuPatronymic.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbBlrSurname.Text))
            {
                MessageBox.Show(this, "Введите фамилию абитуриента на белорусском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbBlrSurname.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbBlrName.Text))
            {
                MessageBox.Show(this, "Введите имя абитуриента на белорусском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbBlrName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbBlrPatronymic.Text))
            {
                MessageBox.Show(this, "Введите отчество абитуриента на белорусском языке", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbBlrPatronymic.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocSeria.Text))
            {
                MessageBox.Show(this, "Введите серию документа абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocSeria.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocNumber.Text))
            {
                MessageBox.Show(this, "Введите номер документа абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocNumber.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocWhoGave.Text))
            {
                MessageBox.Show(this, "Введите наименование органа выдавшего документ абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocWhoGave.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbDocPersonalNumber.Text))
            {
                MessageBox.Show(this, "Введите личный номер абитуриента, согласно предоставленного документа", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbDocPersonalNumber.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbFatherInfo.Text))
            {
                MessageBox.Show(this, "Введите ФИО отца абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbFatherInfo.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbFatherAdres.Text))
            {
                MessageBox.Show(this, "Введите адрес проживания отца абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbFatherAdres.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbMotherInfo.Text))
            {
                MessageBox.Show(this, "Введите ФИО матери абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbMotherInfo.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbMotherAdres.Text))
            {
                MessageBox.Show(this, "Введите адрес проживания матери абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                tbMotherAdres.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSettlementName.Text))
            {
                MessageBox.Show(this, "Введите наименование населенного пункта абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSettlementName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSettlementIndex.Text))
            {
                MessageBox.Show(this, "Введите индекс населенного пункта абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSettlementIndex.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbStreetName.Text))
            {
                MessageBox.Show(this, "Введите наименование улицы абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbStreetName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbNumberHouse.Text))
            {
                MessageBox.Show(this, "Введите номер дома абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbNumberHouse.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbNumberFlat.Text))
            {
                MessageBox.Show(this, "Введите номер квартиры абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbNumberHouse.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbMobilePhone.Text))
            {
                MessageBox.Show(this, "Введите мобильный номер телефона абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbMobilePhone.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbHomePhone.Text))
            {
                MessageBox.Show(this, "Введите домашний номер телефона абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbHomePhone.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSchoolYear.Text))
            {
                MessageBox.Show(this, "Введите год окончания учебного заведения абитуриентом", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSchoolYear.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSchoolName.Text))
            {
                MessageBox.Show(this, "Введите наименование учебного заведения абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSchoolName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbSchoolAdres.Text))
            {
                MessageBox.Show(this, "Введите адрес учебного заведения абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSchoolName.Focus();
            }
            else if (cbSecondEducation.Checked && string.IsNullOrWhiteSpace(tbCurrentNumberCurs.Text))
            {
                MessageBox.Show(this, "Введите текущий курс обучения абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbCurrentNumberCurs.Focus();
            }
            else if (cbSecondEducation.Checked && string.IsNullOrWhiteSpace(tbCurrentSpeciality.Text))
            {
                MessageBox.Show(this, "Введите текущую специальность обучения абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbCurrentSpeciality.Focus();
            }
            else if (cbSecondEducation.Checked && string.IsNullOrWhiteSpace(tbCurrentUniversity.Text))
            {
                MessageBox.Show(this, "Введите наименование университета абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbCurrentUniversity.Focus();
            }
            else if (IsWorker && string.IsNullOrWhiteSpace(tbSeniority.Text))
            {
                MessageBox.Show(this, "Введите стаж работы абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbSeniority.Focus();
            }
            else if (IsWorker && string.IsNullOrWhiteSpace(tbWorkPlace.Text))
            {
                MessageBox.Show(this, "Введите место работы абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbWorkPlace.Focus();
            }
            else if (IsWorker && string.IsNullOrWhiteSpace(tbWorkPost.Text))
            {
                MessageBox.Show(this, "Введите должность абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1;
                tbWorkPost.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbPersonInCharge.Text))
            {
                MessageBox.Show(this, "Введите лицо отвественное за приём документов", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 2;
                tbPersonInCharge.Focus();
            }
            else if (string.IsNullOrEmpty(tbAverage.Text))
            {
                MessageBox.Show(this, "Введите оценки абитуриента", "Сохранение абитуриента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 2;
            }
            else
            {
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
                enrollee.EmployeeId = employee.EmployeeId;
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
                if (rbFemale.Checked) enrollee.Gender = "Ж";
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

                if (editMode)
                {
                    enrollee.NumberOfDeal = Int32.Parse(tbNumberOfDeal.Text);
                    enrollee.DateDeal = dtDateDeal.Value;
                    enrollee.StateDateChange = DateTime.Now;

                    enrolleeService.UpdateEnrollee(enrollee);
                }
                else
                {
                    enrollee.NumberOfDeal = GetNumberOfDeal();
                    enrollee.DateDeal = DateTime.Now;
                    enrollee.StateDateChange = DateTime.Now;

                    enrolleeService.InsertEnrollee(enrollee);
                }
            }
            return enrollee;
        }

        private int GetNumberOfDeal()
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

    }
}

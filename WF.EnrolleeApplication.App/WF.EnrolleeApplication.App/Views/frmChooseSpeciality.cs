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
    /// Класс-форма выбора специальности. 
    /// Подготовка печати отчета.
    /// </summary>
    public partial class frmChooseSpeciality : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Выбранный факультет
        private Faculty faculty;
        // Выбранная форма обучения
        private FormOfStudy formOfStudy;
        // Выбранная специальность
        public Speciality speciality;
        // Сервис получения данных о факультетах системы
        private FacultyService facultyService;
        // Сервис получения данных о формах обучения системы
        private FormOfStudyService formOfStudyService;
        // Сервис получения данных о специальностях системы
        private SpecialityService specialityService;
        // Конструктор по умолчанию
        public frmChooseSpeciality()
        {
            InitializeComponent();
            InitializeDataAccessServices();
            InitializeComboBoxes();
        }
        /// <summary>
        /// Метод инициализации сервисов данных
        /// </summary>
        private void InitializeDataAccessServices()
        {
            // Получение строки подключения
            logger.Info("Получение строки подключения к источнику данных.");
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            // Инициализации сервисов данных
            logger.Info("Инициализация сервисов получения данных.");
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
        }
        /// <summary>
        /// Метод инициализации выпадающих списков формы
        /// </summary>
        private void InitializeComboBoxes()
        {
            logger.Info("Инициализация данных выпадающих списков.");
            // Инициализация комбобокса списком факультетов
            InitializeFacultyComboBox();
            // Инициализация комбобокса списком форм обучения
            InitializeFormOfStudyComboBox();
        }
        /// <summary>
        /// Метод инициализации списка форм обучения
        /// </summary>
        private void InitializeFormOfStudyComboBox()
        {
            logger.Info("Инициализация выпадающего списка форм обучения.");
            // Отключаем отслеживание изменения списка форм обучения
            cbFormOfStudy.SelectedValueChanged -= cbFormOfStudy_SelectedValueChanged;
            // Получение списка форм обучения
            var formsOfStudies = formOfStudyService.GetFormOfStudies();
            // Загружаем в комбо-бокс
            cbFormOfStudy.DataSource = formsOfStudies;
            cbFormOfStudy.DisplayMember = "Fullname";
            cbFormOfStudy.ValueMember = "FormOfStudyId";
            // Если список форм обучения не пуст, инициализируем "выбранную форму обучения" первой из списка
            if (formsOfStudies.Count != 0)
            {
                formOfStudy = formsOfStudies[0];
                // Инициализация комбобокса специальности
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения списка форм обучения
            cbFormOfStudy.SelectedValueChanged += cbFormOfStudy_SelectedValueChanged;
        }
        /// <summary>
        /// Метод инициализации списка факультетов
        /// </summary>
        private void InitializeFacultyComboBox()
        {
            logger.Info("Инициализация выпадающего списка факультетов.");
            // Отключаем отслеживание изменения списка факультов
            cbFaculty.SelectedValueChanged -= cbFaculty_SelectedValueChanged;
            // Получение списка факультетов
            var faculties = facultyService.GetFaculties();
            // Загружаем в комбо-бокс
            cbFaculty.DataSource = faculties;
            cbFaculty.DisplayMember = "Shortname";
            cbFaculty.ValueMember = "FacultyId";
            // Если список факультетов не пуст, инициализируем "выбранный факультет" первой из списка
            if (faculties.Count != 0)
            {
                faculty = faculties[0];
                InitializeSpecialityComboBox();
            }
            // Включаем отслеживание изменения списка факультов
            cbFaculty.SelectedValueChanged += cbFaculty_SelectedValueChanged;
        }
        /// <summary>
        /// Метод инициализации списка специальностей
        /// </summary>
        private void InitializeSpecialityComboBox()
        {
            logger.Info("Инициализация выпадающего списка специальностей.");
            // Если факультет и форма обучения выбраны инициализируем список специальностей
            if (faculty != null && formOfStudy != null)
            {
                // Отключаем отслеживание изменения списка специальностей
                cbSpeciality.SelectedValueChanged -= cbSpeciality_SelectedValueChanged;
                // Получение списка специальностей
                var specialities = specialityService.GetSpecialities(faculty, formOfStudy);
                // Загружаем в комбо-бокс
                cbSpeciality.DataSource = specialities;
                cbSpeciality.DisplayMember = "Fullname";
                cbSpeciality.ValueMember = "SpecialityId";
                // Если список специальностей не пуст, инициализируем "выбранную специальность" первой из списка
                if (specialities.Count != 0) speciality = specialities[0];
                // Включаем отслеживание изменения списка специальностей
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }
        /// <summary>
        /// Обработчик выбора факультета
        /// </summary>
        /// <param name="sender">Комбобокс список факультетов</param>
        /// <param name="e"></param>
        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список факультетов не пуст
            // Получаем уникальный идентификатор выбранного факультета
            // Получаем факультет по уникальному идентификатору
            // Инициализируем список специальностей
            if (cbFaculty.SelectedValue != null)
            {
                int id = (int)cbFaculty.SelectedValue;
                faculty = facultyService.GetFaculty(id);
                logger.Info($"Выбранный факультет: {faculty.ToString()}.");
                InitializeSpecialityComboBox();
            }
        }
        /// <summary>
        /// Обработчик выбора формы обучения
        /// </summary>
        /// <param name="sender">Комбокс список форм обучения</param>
        /// <param name="e"></param>
        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список форм обучения не пуст
            // Получаем уникальный идентификатор выбранной формы обучения
            // Получаем форму обучения по уникальному идентификатору
            // Инициализируем список специальностей
            if (cbFormOfStudy.SelectedValue != null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
                logger.Info($"Выбранная форма обучения: { formOfStudy.ToString()}.");
                InitializeSpecialityComboBox();
            }
        }
        /// <summary>
        /// Обработчик выбора специальности
        /// </summary>
        /// <param name="sender">Комбобокс список специальностей</param>
        /// <param name="e"></param>
        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список специальностей не пуст
            // Получаем уникальный идентификатор выбранной специальности
            // Получаем специальность по уникальному идентификатору
            if (cbSpeciality.SelectedValue != null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                speciality = specialityService.GetSpeciality(id);
                logger.Info($"Выбранная специальность: {speciality.ToString()}.");
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Отмена"
        /// </summary>
        /// <param name="sender">Кнопка "Отмена"</param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            logger.Info("Отмена генерации отчёта выбранной специальности.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Печать"
        /// </summary>
        /// <param name="sender">Кнопка "Печать"</param>
        /// <param name="e"></param>
        private void btPrint_Click(object sender, EventArgs e)
        {
            logger.Info($"Подготовка отчёта по выбранной специальности: {speciality.ToString()}.");
            if(speciality != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

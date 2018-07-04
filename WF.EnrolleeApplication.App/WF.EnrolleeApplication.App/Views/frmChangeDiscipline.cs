using NLog;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Views
{
    /// <summary>
    /// Класс-форма "Изменение дисциплины"
    /// </summary>
    public partial class frmChangeDiscipline : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Отметка выполнения замены дисциплины
        public bool IsChanged = false;
        // Дисциплина
        public Discipline discipline;
        // Сервис получения данных о дисциплинах системы
        private DisciplineService disciplineService;
        // Основание оценивания
        private BasisForAssessing basisForAssessing;
        // Сервис получения данных об основании оценивания
        private BasisForAssessingService basisForAssessingService;
        // Конструктор по умолчанию
        public frmChangeDiscipline()
        {
            InitializeComponent();
            // Получение строки подключения к источнику данных
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            // Инициализация сервисов
            disciplineService = new DisciplineService(connectionString);
            basisForAssessingService = new BasisForAssessingService(connectionString);
            // Инициализация списка дисциплин
            InitializeDisciplineComboBox();
            logger.Info($"Регистрация замены дисциплины абитуриента.");
        }
        /// <summary>
        /// Метод инициализации содержимого списка дисциплин
        /// Инициализируем список только предметами тестирования
        /// Сортировка по наименованию
        /// </summary>
        private void InitializeDisciplineComboBox()
        {
            // Отключаем отслеживание изменения дисциплины в списке дисциплин
            cbDiscipline.SelectedValueChanged -= cbDiscipline_SelectedValueChanged;
            // Получаем основание оценок равное значению "Тестирование"
            basisForAssessing = basisForAssessingService.GetBasisForAssessing(3);
            if (basisForAssessing != null)
            {
                // Получаем список дисциплин
                var disciplines = disciplineService.GetDisciplines(basisForAssessing, false).OrderBy(d => d.Name).ToList();
                // Загружаем в комбо-бокс
                cbDiscipline.DataSource = disciplines;
                cbDiscipline.DisplayMember = "Name";
                cbDiscipline.ValueMember = "DisciplineId";
                // Если список дисциплин не пуст, инициализируем "выбранную дисциплину" первой из списка
                if (disciplines.Count != 0) discipline = disciplines[0];
            }
            // Включаем отслеживание изменения дисциплины в списке дисциплин
            cbDiscipline.SelectedValueChanged += cbDiscipline_SelectedValueChanged;
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Отмена"
        /// </summary>
        /// <param name="sender">Кнопка "Отмена"</param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            logger.Info($"Отмена регистрации замены дисциплины абитуриента.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Сохранить"
        /// </summary>
        /// <param name="sender">Кнопка "Сохранить"</param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            logger.Info($"Сохранение изменения дисциплины абитуриента.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// Обработчик выбора дисциплины из списка комбобокса
        /// </summary>
        /// <param name="sender">Выпадающий список дисциплин</param>
        /// <param name="e"></param>
        private void cbDiscipline_SelectedValueChanged(object sender, EventArgs e)
        {
            // Если список дисциплин не пуст получаем уникальный идентификатор дисциплины
            // Получаем дисциплину по уникальному идентификатору
            if(cbDiscipline.SelectedValue != null)
            {
                int id = (int)cbDiscipline.SelectedValue;
                discipline = disciplineService.GetDiscipline(id);
                logger.Info($"Выбранная дисциплина: {discipline.ToString()}.");
            }
        }
        /// <summary>
        /// Обработчик нажатия (установки флажка) замены дисциплины
        /// </summary>
        /// <param name="sender">Чек-бокс замены дисциплины</param>
        /// <param name="e"></param>
        private void IsChange_CheckedChanged(object sender, EventArgs e)
        {
            if(IsChange.Checked)
            {
                IsChanged = true;
                cbDiscipline.Enabled = true;
            }
            else
            {
                IsChanged = false;
                cbDiscipline.Enabled = false;
            }
        }
    }
}

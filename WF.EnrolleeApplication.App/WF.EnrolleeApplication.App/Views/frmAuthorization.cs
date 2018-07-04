using NLog;
using System;
using System.Configuration;
using System.Windows.Forms;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Views
{
    /// <summary>
    /// Класс-форма "Авторизация пользователя"
    /// </summary>
    public partial class frmAuthorization : Form
    {
        /// <summary>
        /// Форма авторизации пользователя в системе
        /// </summary>        
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Текущий оператор
        private Employee employee;
        // Сервис получения данных о пользователях системы
        private EmployeeService employeeService;
        public frmAuthorization()
        {
            InitializeComponent();
            // Получение строки подключения к источнику данных
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            // Инициализация сервиса
            employeeService = new EmployeeService(connectionString);
            // При запуске поле ввода имени пользователя делаем активным
            tbUsername.Focus();
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            logger.Info($"Отмена авторизации. Завершение работы приложения.");
            // Нажатие кнопки "Отмена" — Завершение работы приложения
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Войти в систему"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEnter_Click(object sender, EventArgs e)
        {
            // Пытаемся войти в систему
            TryEnter();
        }
        /// <summary>
        /// Обработчик нажатия клавиши в поле "Имя входа"
        /// Ловим нажатие клавиши Enter, если поля пустые, устанавливаем фокус в пустое поле 
        /// или пытаемся пройти авторизацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tbUsername.Text) && !string.IsNullOrWhiteSpace(tbPassword.Text))
                    TryEnter();
                else if (string.IsNullOrWhiteSpace(tbPassword.Text))
                    tbPassword.Focus();
                else if (string.IsNullOrWhiteSpace(tbUsername.Text))
                    tbUsername.Focus();
            }
        }
        /// <summary>
        /// Обработчик нажатия клавиши в поле "Пароль"
        /// Ловим нажатие клавиши Enter, если поля пустые, устанавливаем фокус в пустое поле 
        /// или пытаемся пройти авторизацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tbUsername.Text) && !string.IsNullOrWhiteSpace(tbPassword.Text))
                    TryEnter();
                else if (string.IsNullOrWhiteSpace(tbPassword.Text))
                    tbPassword.Focus();
                else if (string.IsNullOrWhiteSpace(tbUsername.Text))
                    tbUsername.Focus();
            }
        }

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private void TryEnter()
        {
            logger.Info($"Попытка авторизации пользователя.");
            // Проверяем поле ввода имени пользователя
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                logger.Warn($"Поле ввода имени пользователя НЕ ЗАПОЛНЕНО.");
                MessageBox.Show(this, "Введите логин пользователя", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbUsername.Focus();
            }
            // Проверяем поле пароля пользователя
            else if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                logger.Warn($"Поле пароля пользователя НЕ ЗАПОЛНЕНО.");
                MessageBox.Show(this, "Введите пароль пользователя", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbPassword.Focus();
            }
            else
            {
                string username = tbUsername.Text;
                string password = tbPassword.Text;
                // Поиск пользователя по данным авторизации
                employee = employeeService.GetEmployee(username, password);
                logger.Info($"Авторизация пользователя. Поиск пользователя.\nИмя входа: {username.Trim()};\nПароль: {password.Trim()}.\n");
                if (employee == null)
                {
                    // Пользователь не найден
                    // Диалоговое окно (Повтор/Отмена)
                    // - Повтор — очищаем поля ввода, устанавливаем фокус на поле имени пользователя;
                    // - Отмена — завершение работы приложения
                    logger.Warn($"Пользователь не найден.");
                    DialogResult employeeNull = MessageBox.Show(this, "Пользователь не найден", "Вход в систему", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                    if (employeeNull == DialogResult.Retry)
                    {
                        tbUsername.Text = null;
                        tbPassword.Text = null;
                        tbUsername.Focus();
                    }
                    else Application.Exit();
                }
                else if (employee.Enabled == false)
                {
                    // Пользователь найден.
                    // Учётная запись заблокирована.
                    // Диалоговое окно (Повтор/Отмена)
                    // - Повтор — очищаем поля ввода, устанавливаем фокус на поле имени пользователя;
                    // - Отмена — завершение работы приложения
                    logger.Warn($"Учётная запись пользователя отключена.");
                    DialogResult employeeDisable = MessageBox.Show(this, "Учётная запись заблокирована", "Вход в систему", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                    if (employeeDisable == DialogResult.Retry)
                    {
                        tbUsername.Text = null;
                        tbPassword.Text = null;
                        tbUsername.Focus();
                    }
                    else Application.Exit();
                }
                else
                {
                    // Пользователь найден.
                    // Проверяем уровень доступа.
                    logger.Info($"Пользователь найден. Проверяем уровень доступа");
                    if (employee.EmployeePost.DictionaryAllow)
                    {
                        logger.Info($"Доступ получен. Авторизация пройдена успешно.");
                        this.Hide();
                        // Доступ получен, открываем главное окно приложения
                        frmDashboard dashboard = new frmDashboard(employee);
                        DialogResult dashboardResult = dashboard.ShowDialog();
                        if (dashboardResult == DialogResult.Abort)
                        {
                            // Завершение работы пользователя
                            logger.Info($"Пользователь {employee.ToString()} завершил работу в системе.");
                            this.Show();
                            tbUsername.Text = null;
                            tbPassword.Text = null;
                            employee = null;
                        }
                        else Application.Exit();
                    }
                    else
                    {
                        // Нет прав доступа пользователя в систему
                        logger.Info($"Нет прав доступа в систему.");
                        DialogResult accessResult = MessageBox.Show(this, "Нет прав доступа", "Вход в систему", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                        if (accessResult == DialogResult.Retry)
                        {
                            employee = null;
                            tbUsername.Text = null;
                            tbPassword.Text = null;
                            tbUsername.Focus();
                        }
                        else Application.Exit();
                    }
                }
            }
        }
    }
}

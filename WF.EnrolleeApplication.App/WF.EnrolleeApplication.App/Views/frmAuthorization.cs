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
    public partial class frmAuthorization : Form
    {
        /// <summary>
        /// Форма авторизации пользователя в системе
        /// </summary>
        private Employee employee;
        private EmployeeService employeeService;
        public frmAuthorization()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            employeeService = new EmployeeService(connectionString);
            tbUsername.Focus();
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
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
            // Проверяем заполненность полей
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                MessageBox.Show(this, "Введите логин пользователя", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbUsername.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show(this, "Введите пароль пользователя", "Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbPassword.Focus();
            }
            else
            {
                // Ищем пользователя по имени и паролю
                string username = tbUsername.Text;
                string password = tbPassword.Text;
                employee = employeeService.GetEmployee(username, password);
                if (employee == null)
                {
                    // Если пользователь не найден
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
                    // Если найден проверяем уровень доступа
                    if (employee.EmployeePost.DictionaryAllow)
                    {
                        this.Hide();
                        frmDashboard dashboard = new frmDashboard(employee);
                        DialogResult dashboardResult = dashboard.ShowDialog();
                        if (dashboardResult == DialogResult.Abort) this.Show();
                        else Application.Exit();
                    }
                    else
                    {
                        DialogResult accessResult = MessageBox.Show(this, "Нет прав доступа", "Вход в систему", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                        if (accessResult == DialogResult.Retry)
                        {
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

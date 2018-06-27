using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    /// <summary>
    /// Класс-репозиторий для управления учетных записей сотрудников
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Объект для работы с данными
        /// </summary>
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public EmployeeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление учетной записи сотрудника
        /// </summary>
        /// <param name="employee">Объект удаляемой записи</param>
        public void DeleteEmployee(Employee employee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению пользователя.");
            try
            {
                logger.Debug($"Поиск записи пользователя для удаления. Удаляемый объект : {employee.ToString()}.");
                Employee employeeToDelete = context.Employee.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
                if (employeeToDelete != null)
                {
                    context.Employee.Remove(employeeToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи пользователя успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи пользователя.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи пользователя.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение учетной записи по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор сотрудника</param>
        /// <returns>Возвращаем объект сотрудника</returns>
        public Employee GetEmployee(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску пользователя.");
            try
            {
                logger.Debug($"Поиск записи пользователя по уникальному идентификатору = {id}.");
                Employee employeeById = context.Employee.AsNoTracking().FirstOrDefault(e => e.EmployeeId == id);
                if (employeeById != null) logger.Debug($"Поиск окончен. Искомая запись: {employeeById.ToString()}.");
                return employeeById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи пользователя.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи пользователя.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение учетной записи по логину и паролю.
        /// Используется для авторизации пользователя в системе.
        /// </summary>
        /// <param name="username">Имя входа сотрудника</param>
        /// <param name="password">Пароль учетной записи</param>
        /// <returns>Возвращаем объект сотрудника</returns>
        public Employee GetEmployee(string username, string password)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску пользователя.");
            try
            {
                logger.Debug($"Поиск записи пользователя по параметрам. Имя входа = [{username}]; Пароль = [{password}].");
                Employee employeeForAuth = context.Employee.AsNoTracking().FirstOrDefault(e => e.Username == username && e.Password == password);
                if (employeeForAuth != null) logger.Debug($"Поиск окончен. Искомая запись: {employeeForAuth.ToString()}.");
                return employeeForAuth;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи пользователя.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи пользователя.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        public List<Employee> GetEmployees()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка пользователей.");
            try
            {
                logger.Debug($"Получение списка пользователей.");
                List<Employee> employees = context.Employee.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {employees.Count}.");
                return employees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка пользователей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка пользователей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка сотрудников, определенной должности
        /// </summary>
        /// <param name="post">Должность сотрудников</param>
        /// <returns>Список сотрудников</returns>
        public List<Employee> GetEmployees(EmployeePost post)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка пользователей.");
            try
            {
                logger.Debug($"Получение списка пользователей, определенной должности. Должность = [{post.ToString()}]");
                List<Employee> employees = context.Employee.AsNoTracking().Where(e => e.PostId == post.PostId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {employees.Count}.");
                return employees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка пользователей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка пользователей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление учётной записи сотрудника
        /// </summary>
        /// <param name="employee">Новая учетная запись</param>
        /// <returns>Созданная учетная запись сотрудника</returns>
        public Employee InsertEmployee(Employee employee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению пользователя");
            try
            {
                logger.Debug($"Добавляемая запись: {employee.ToString()}");
                context.Employee.Add(employee);
                context.SaveChanges();
                logger.Debug($"Пользователь успешно добавлена.");
                return employee;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления пользователя.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления пользователя.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Редактирование учётной записи сотрудника
        /// </summary>
        /// <param name="employee">Редактируемая учётная запись</param>
        /// <returns>Обновленная учётная запись сотрудника</returns>
        public Employee UpdateEmployee(Employee employee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению пользователя.");
            try
            {
                Employee employeeToUpdate = context.Employee.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
                logger.Debug($"Текущая запись: {employeeToUpdate.ToString()}");
                employeeToUpdate.PostId = employee.PostId;
                employeeToUpdate.Fullname = employee.Fullname;
                employeeToUpdate.Username = employee.Username;
                employeeToUpdate.Password = employee.Password;
                employeeToUpdate.CreateDate = employee.CreateDate;
                employeeToUpdate.Enabled = employee.Enabled;
                context.SaveChanges();
                logger.Debug($"Новая запись: {employeeToUpdate.ToString()}");
                return employeeToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования пользователя.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования пользователя.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

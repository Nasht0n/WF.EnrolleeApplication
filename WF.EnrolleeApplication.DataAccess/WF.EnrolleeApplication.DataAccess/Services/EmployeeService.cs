using System;
using System.Collections.Generic;
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
            Employee employeeToDelete = context.Employee.AsNoTracking().FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            context.Employee.Remove(employeeToDelete);
            context.SaveChanges();
        }
        /// <summary>
        /// Получение учетной записи по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор сотрудника</param>
        /// <returns>Возвращаем объект сотрудника</returns>
        public Employee GetEmployee(int id)
        {
            Employee employeeById = context.Employee.AsNoTracking().FirstOrDefault(e => e.EmployeeId == id);
            return employeeById;
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
            Employee employeeForAuth = context.Employee.AsNoTracking().FirstOrDefault(e => e.Username == username && e.Password == password);
            return employeeForAuth;
        }
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = context.Employee.AsNoTracking().ToList();
            return employees;
        }
        /// <summary>
        /// Получение списка сотрудников, определенной должности
        /// </summary>
        /// <param name="post">Должность сотрудников</param>
        /// <returns>Список сотрудников</returns>
        public List<Employee> GetEmployees(EmployeePost post)
        {
            List<Employee> employees = context.Employee.AsNoTracking().Where(e => e.PostId == post.PostId).ToList();
            return employees;
        }
        /// <summary>
        /// Добавление учётной записи сотрудника
        /// </summary>
        /// <param name="employee">Новая учетная запись</param>
        /// <returns>Созданная учетная запись сотрудника</returns>
        public Employee InsertEmployee(Employee employee)
        {
            context.Employee.Add(employee);
            context.SaveChanges();
            return employee;
        }
        /// <summary>
        /// Редактирование учётной записи сотрудника
        /// </summary>
        /// <param name="employee">Редактируемая учётная запись</param>
        /// <returns>Обновленная учётная запись сотрудника</returns>
        public Employee UpdateEmployee(Employee employee)
        {
            Employee employeeToUpdate = context.Employee.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            employeeToUpdate.PostId = employee.PostId;
            employeeToUpdate.Fullname = employee.Fullname;
            employeeToUpdate.Username = employee.Username;
            employeeToUpdate.Password = employee.Password;
            employeeToUpdate.CreateDate = employee.CreateDate;
            employeeToUpdate.Enabled = employee.Enabled;
            context.SaveChanges();
            return employeeToUpdate;
        }
    }
}

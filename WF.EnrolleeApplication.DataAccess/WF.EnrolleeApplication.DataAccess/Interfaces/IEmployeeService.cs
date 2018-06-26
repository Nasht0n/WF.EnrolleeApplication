using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Пользователи"
    /// </summary>
    interface IEmployeeService
    {
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="employee">Новый пользователь</param>
        /// <returns>Добавленная запись</returns>
        Employee InsertEmployee(Employee employee);
        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="employee">Редактирование пользователя</param>
        /// <returns>Отредактированная запись</returns>
        Employee UpdateEmployee(Employee employee);
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="employee">Удаляемый пользователь</param>
        void DeleteEmployee(Employee employee);
        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        List<Employee> GetEmployees();
        /// <summary>
        /// Получение списка пользователей, выбранной должности
        /// </summary>
        /// <param name="post">Должность</param>
        /// <returns>Список пользователей</returns>
        List<Employee> GetEmployees(EmployeePost post);
        /// <summary>
        /// Получение пользователя по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись пользователя</returns>
        Employee GetEmployee(int id);
        /// <summary>
        /// Получение пользователя по имени входа и пароля
        /// </summary>
        /// <param name="username">Имя входа</param>
        /// <param name="password">Пароль</param>
        /// <returns>Запись пользователя</returns>
        Employee GetEmployee(string username, string password);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Должности"
    /// </summary>
    interface IEmployeePostService
    {
        /// <summary>
        /// Добавление должности
        /// </summary>
        /// <param name="post">Новая должность</param>
        /// <returns>Добавленная запись</returns>
        EmployeePost InsertEmployeePost(EmployeePost post);
        /// <summary>
        /// Обновление должности
        /// </summary>
        /// <param name="post">Редактируемая должность</param>
        /// <returns>Отредактированная запись</returns>
        EmployeePost UpdateEmployeePost(EmployeePost post);
        /// <summary>
        /// Удаление должности
        /// </summary>
        /// <param name="post">Удаляемая должность</param>
        void DeleteEmployeePost(EmployeePost post);
        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <returns>Список должностей</returns>
        List<EmployeePost> GetEmployeePosts();
        /// <summary>
        /// Получение должности по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись должности</returns>
        EmployeePost GetEmployeePost(int id);
        /// <summary>
        /// Получение должности по наименованию
        /// </summary>
        /// <param name="name">Наименование должности</param>
        /// <returns>Запись должности</returns>
        EmployeePost GetEmployeePost(string name);
    }
}

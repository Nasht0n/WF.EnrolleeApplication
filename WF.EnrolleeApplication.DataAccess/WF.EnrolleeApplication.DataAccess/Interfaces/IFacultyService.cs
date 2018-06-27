using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Факультеты"
    /// </summary>
    interface IFacultyService
    {
        /// <summary>
        /// Новая запись факультета
        /// </summary>
        /// <param name="faculty">Новый факультет</param>
        /// <returns>Новая запись</returns>
        Faculty InsertFaculty(Faculty faculty);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="faculty">Редактируемый факультет</param>
        /// <returns>Отредактированная запись</returns>
        Faculty UpdateFaculty(Faculty faculty);
        /// <summary>
        /// Удаление факультета
        /// </summary>
        /// <param name="faculty">Удаляемый факультет</param>
        void DeleteFaculty(Faculty faculty);
        /// <summary>
        /// Получение списка факультетов
        /// </summary>
        /// <returns>Список факультетов</returns>
        List<Faculty> GetFaculties();
        /// <summary>
        /// Получение факультета по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись факультета</returns>
        Faculty GetFaculty(int id);
        /// <summary>
        /// Получение факультета по наименованию
        /// </summary>
        /// <param name="fullname">Наименование факультета</param>
        /// <returns>Запись факультета</returns>
        Faculty GetFaculty(string fullname);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Виды конкурса"
    /// </summary>
    interface IContestService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="contest">Новый вид конкурса</param>
        /// <returns>Добавленная запись</returns>
        Contest InsertContest(Contest contest);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="contest">Редактируемая запись вида конкурса</param>
        /// <returns>Отредактированная запись</returns>
        Contest UpdateContest(Contest contest);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="contest">Удаляемая запись</param>
        void DeleteContest(Contest contest);
        /// <summary>
        /// Получение списка видов конкурса
        /// </summary>
        /// <returns>Список видов конкурса</returns>
        List<Contest> GetContests();
        /// <summary>
        /// Получение вида конкурса по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Найденная запись вида конкурса</returns>
        Contest GetContest(int id);
        /// <summary>
        /// Получение вида конкурса по наименованию
        /// </summary>
        /// <param name="fullname">Наименование вида конкурса</param>
        /// <returns>Найденная запись вида конкурса</returns>
        Contest GetContest(string name);
    }
}

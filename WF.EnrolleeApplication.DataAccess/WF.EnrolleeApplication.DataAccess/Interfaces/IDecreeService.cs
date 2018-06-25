using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Приказы"
    /// </summary>
    interface IDecreeService
    {
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="decree">Новая запись приказа</param>
        /// <returns>Добавленная запись</returns>
        Decree InsertDecree(Decree decree);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="decree">Редактируемая запись приказа</param>
        /// <returns>Отредактированная запись</returns>
        Decree UpdateDecree(Decree decree);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="decree">Удаляемая запись</param>
        void DeleteDecree(Decree decree);
        /// <summary>
        /// Получение списка приказов
        /// </summary>
        /// <returns>Список приказов</returns>
        List<Decree> GetDecrees();
        /// <summary>
        /// Получение записи приказа по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись приказа</returns>
        Decree GetDecree(int id);
        /// <summary>
        /// Получение записи приказа по дате приказа
        /// </summary>
        /// <param name="decreeDate">Дата приказа</param>
        /// <returns>Запись приказа</returns>
        Decree GetDecree(DateTime decreeDate);
        /// <summary>
        /// Получение записи приказа по номеру приказа
        /// </summary>
        /// <param name="decreeNumber">Номер приказа</param>
        /// <returns>Запись приказа</returns>
        Decree GetDecree(string decreeNumber);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    ///  Интерфейс работы с данными таблицы "Виды гражданства"
    /// </summary>
    interface ICitizenshipService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="citizenship">Новый вид гражданства</param>
        /// <returns>Добавленная запись</returns>
        Citizenship InsertCitizenship(Citizenship citizenship);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="citizenship">Редактируемая запись вида гражданства</param>
        /// <returns>Отредактированная запись</returns>
        Citizenship UpdateCitizenship(Citizenship citizenship);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="citizenship">Удаляемая запись</param>
        void DeleteCitizenship(Citizenship citizenship);
        /// <summary>
        /// Получение списка видов гражданства
        /// </summary>
        /// <returns>Список видов гражданства</returns>
        List<Citizenship> GetCitizenships();
        /// <summary>
        /// Получение вида гражданства по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Найденная запись вида гражданства</returns>
        Citizenship GetCitizenship(int id);
        /// <summary>
        /// Получение вида гражданства по наименованию
        /// </summary>
        /// <param name="fullname">Наименование вида гражданства</param>
        /// <returns>Найденная запись вида гражданства</returns>
        Citizenship GetCitizenship(string fullname);
    }
}

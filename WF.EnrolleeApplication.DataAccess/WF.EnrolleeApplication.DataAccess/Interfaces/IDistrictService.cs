using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Районы"
    /// </summary>
    interface IDistrictService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="district">Новый район</param>
        /// <returns>Добавленная запись</returns>
        District InsertDistrict(District district);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="district">Редактируемый район</param>
        /// <returns>Отредактированная запись</returns>
        District UpdateDistrict(District district);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="district">Удаляемый район</param>
        void DeleteDistrict(District district);
        /// <summary>
        /// Получение списка районов
        /// </summary>
        /// <returns>Список районов</returns>
        List<District> GetDistricts();
        /// <summary>
        /// Получение района по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись района</returns>
        District GetDistrict(int id);
        /// <summary>
        /// Получение района по наименованию
        /// </summary>
        /// <param name="name">Наименование района</param>
        /// <returns>Запись района</returns>
        District GetDistrict(string name);
    }
}

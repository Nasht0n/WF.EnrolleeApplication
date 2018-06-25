using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Страны"
    /// </summary>
    interface ICountryService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="country">Новая запись страны</param>
        /// <returns>Добавленная запись</returns>
        Country InsertCountry(Country country);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="country">Редактируемая запись</param>
        /// <returns>Отредактированная запись</returns>
        Country UpdateCountry(Country country);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="country">Удаляемая запись</param>
        void DeleteCountry(Country country);
        /// <summary>
        /// Получение списка стран
        /// </summary>
        /// <returns>Список стран</returns>
        List<Country> GetCountries();
        /// <summary>
        /// Получение записи по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись страны</returns>
        Country GetCountry(int id);
        /// <summary>
        /// Получение записи по наименованию страны
        /// </summary>
        /// <param name="name">Наименование страны</param>
        /// <returns>Запись страны</returns>
        Country GetCountry(string name);
    }
}

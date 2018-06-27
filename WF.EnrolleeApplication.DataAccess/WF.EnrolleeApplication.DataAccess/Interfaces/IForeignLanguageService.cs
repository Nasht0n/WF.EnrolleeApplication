using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Иностранные языки"
    /// </summary>
    interface IForeignLanguageService
    {
        /// <summary>
        /// Новый иностранный язык
        /// </summary>
        /// <param name="foreignLanguage">Иностранный язык</param>
        /// <returns>Новая запись</returns>
        ForeignLanguage InsertForeignLanguage(ForeignLanguage foreignLanguage);
        /// <summary>
        /// Обновление иностранного языка
        /// </summary>
        /// <param name="foreignLanguage">Редактируемый иностранный язык</param>
        /// <returns>Отредактированная запись</returns>
        ForeignLanguage UpdateForeignLanguage(ForeignLanguage foreignLanguage);
        /// <summary>
        /// Удаление иностранного языка
        /// </summary>
        /// <param name="foreignLanguage">Удаляемый иностранный язык</param>
        void DeleteForeignLanguage(ForeignLanguage foreignLanguage);
        /// <summary>
        /// Получение списка иностранных языков
        /// </summary>
        /// <returns>Список иностранных языков</returns>
        List<ForeignLanguage> GetForeignLanguages();
        /// <summary>
        /// Получение иностранного языка по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись иностранного языка</returns>
        ForeignLanguage GetForeignLanguage(int id);
        /// <summary>
        /// Получение иностранного языка по наименованию
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns>Запись иностранного языка</returns>
        ForeignLanguage GetForeignLanguage(string name);
    }
}

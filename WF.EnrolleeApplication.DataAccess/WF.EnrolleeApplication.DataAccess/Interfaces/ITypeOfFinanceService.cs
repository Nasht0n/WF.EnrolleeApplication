using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Тип финансирования"
    /// </summary>
    interface ITypeOfFinanceService
    {
        /// <summary>
        /// Добавление нового типа финансирования
        /// </summary>
        /// <param name="typeOfFinance">Тип финансирования</param>
        /// <returns>Новая запись</returns>
        TypeOfFinance InsertTypeOfFinance(TypeOfFinance typeOfFinance);
        /// <summary>
        /// Обновление типа финансирования
        /// </summary>
        /// <param name="typeOfFinance">Редактируемый тип финансирования</param>
        /// <returns>Отредактированная запись</returns>
        TypeOfFinance UpdateTypeOfFinance(TypeOfFinance typeOfFinance);
        /// <summary>
        /// Удаление типа финансирования
        /// </summary>
        /// <param name="typeOfFinance">Удаление записи</param>
        void DeleteTypeOfFinance(TypeOfFinance typeOfFinance);
        /// <summary>
        /// Получение списка типов финансирования
        /// </summary>
        /// <returns>Список финансирования</returns>
        List<TypeOfFinance> GetTypeOfFinances();
        /// <summary>
        /// Получение записи типа финансирования по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификтор</param>
        /// <returns>Запись типа финансирования</returns>
        TypeOfFinance GetTypeOfFinance(int id);
        /// <summary>
        /// Получение записи типа финансирования по наименованию
        /// </summary>
        /// <param name="fullname">Наименование типа финансирования</param>
        /// <returns>Запись типа финансирования</returns>
        TypeOfFinance GetTypeOfFinance(string fullname);
    }
}

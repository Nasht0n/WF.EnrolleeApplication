using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Тип улицы"
    /// </summary>
    interface ITypeOfStreetService
    {
        /// <summary>
        /// Добавление типа улицы
        /// </summary>
        /// <param name="typeOfStreet">Новый тип улицы</param>
        /// <returns>Новая запись</returns>
        TypeOfStreet InsertTypeOfStreet(TypeOfStreet typeOfStreet);
        /// <summary>
        /// Обновление типа улицы
        /// </summary>
        /// <param name="typeOfStreet">Редактируемый тип улицы</param>
        /// <returns>Отредактированный тип улицы</returns>
        TypeOfStreet UpdateTypeOfStreet(TypeOfStreet typeOfStreet);
        /// <summary>
        /// Удаление типа улицы
        /// </summary>
        /// <param name="typeOfStreet">Удаляемый тип улицы</param>
        void DeleteTypeOfStreet(TypeOfStreet typeOfStreet);
        /// <summary>
        /// Получение списка типов улиц
        /// </summary>
        /// <returns>Список типов улиц</returns>
        List<TypeOfStreet> GetTypeOfStreets();
        /// <summary>
        /// Получение типа улицы по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа улицы</returns>
        TypeOfStreet GetTypeOfStreet(int id);
        /// <summary>
        /// Получение типа улицы по наименованию
        /// </summary>
        /// <param name="fullname">Наименование</param>
        /// <returns>Запись типа улицы</returns>
        TypeOfStreet GetTypeOfStreet(string fullname);
    }
}

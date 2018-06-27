using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Тип населенного заведения"
    /// </summary>
    interface ITypeOfSettlementService
    {
        /// <summary>
        /// Добавление типа населенного пункта
        /// </summary>
        /// <param name="typeOfSettlement">Новый тип населенного пункта</param>
        /// <returns>Новая запись</returns>
        TypeOfSettlement InsertTypeOfSettlement(TypeOfSettlement typeOfSettlement);
        /// <summary>
        /// Обновление типа населенного пункта
        /// </summary>
        /// <param name="typeOfSettlement">Редактируемый тип населенного пункта</param>
        /// <returns>Отредактированная запись</returns>
        TypeOfSettlement UpdateTypeOfSettlement(TypeOfSettlement typeOfSettlement);
        /// <summary>
        /// Удаление типа населенного пункта
        /// </summary>
        /// <param name="typeOfSettlement">Удаляемая запись</param>
        void DeleteTypeOfSettlement(TypeOfSettlement typeOfSettlement);
        /// <summary>
        /// Получение списка типов населенных пунктов
        /// </summary>
        /// <returns>Список типов населенных пунктов</returns>
        List<TypeOfSettlement> GetTypeOfSettlements();
        /// <summary>
        /// Получение записи типа населенного пункта по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа населенного пункта</returns>
        TypeOfSettlement GetTypeOfSettlement(int id);
        /// <summary>
        /// Получение записи типа населенного пункта по наименованию
        /// </summary>
        /// <param name="fullname">Наименование типа</param>
        /// <returns>Запись типа населенного пункта</returns>
        TypeOfSettlement GetTypeOfSettlement(string fullname);
    }
}

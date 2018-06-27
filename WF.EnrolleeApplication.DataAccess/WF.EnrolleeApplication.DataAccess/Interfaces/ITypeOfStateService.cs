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
    interface ITypeOfStateService
    {
        /// <summary>
        /// Добавление типа состояния
        /// </summary>
        /// <param name="typeOfState">Новый тип состояния абитуриента</param>
        /// <returns>Новая запись</returns>
        TypeOfState InsertTypeOfState(TypeOfState typeOfState);
        /// <summary>
        /// Обновление типа состояния
        /// </summary>
        /// <param name="typeOfState">Редактируемый тип состояния абитуриента</param>
        /// <returns>Отредактированная запись</returns>
        TypeOfState UpdateTypeOfState(TypeOfState typeOfState);
        /// <summary>
        /// Удаление типа состояния
        /// </summary>
        /// <param name="typeOfState">Удаляемая запись</param>
        void DeleteTypeOfState(TypeOfState typeOfState);
        /// <summary>
        /// Получение списка типов состояния абитуриента
        /// </summary>
        /// <returns>Список типов состояния абитуриента</returns>
        List<TypeOfState> GetTypeOfStates();
        /// <summary>
        /// Получение записи типа состояния по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа состояния</returns>
        TypeOfState GetTypeOfState(int id);
        /// <summary>
        /// Получение записи типа состояния по наименованию
        /// </summary>
        /// <param name="name">Наименование состояния</param>
        /// <returns>Запись типа состояния</returns>
        TypeOfState GetTypeOfState(string name);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Типы оценивания"
    /// </summary>
    interface IBasisForAssessingService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="basisForAssessing">Тип оценивания</param>
        /// <returns>Объект добавленной записи</returns>
        BasisForAssessing InsertBasisForAssessing(BasisForAssessing basisForAssessing);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="basisForAssessing">Тип оценивания</param>
        /// <returns>Объект редактированной записи</returns>
        BasisForAssessing UpdateBasisForAssessing(BasisForAssessing basisForAssessing);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="basisForAssessing">Удаляемая запись</param>
        void DeleteBasisForAssessing(BasisForAssessing basisForAssessing);
        /// <summary>
        /// Получение списка типов оценивания
        /// </summary>
        /// <returns>Список типов оценивания</returns>
        List<BasisForAssessing> GetBasisForAssessings();
        /// <summary>
        /// Получение типа оценки по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Объект типа оценивания</returns>
        BasisForAssessing GetBasisForAssessing(int id);
        /// <summary>
        /// Получение типа оценки по наименованию
        /// </summary>
        /// <param name="name">Наименование типа</param>
        /// <returns>Объект типа оценивания</returns>
        BasisForAssessing GetBasisForAssessing(string name);
    }
}

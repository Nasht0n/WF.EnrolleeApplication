using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Области"
    /// </summary>
    interface IAreaService
    {
        /// <summary>
        /// Добавление новой области
        /// </summary>
        /// <param name="area">Новый объект области</param>
        /// <returns>Добавленный объект в базу данных</returns>
        Area InsertArea(Area area);
        /// <summary>
        /// Обновление полей объекта
        /// </summary>
        /// <param name="area">Редактируемый объект</param>
        /// <returns>Обновленный объект области</returns>
        Area UpdateArea(Area area);
        /// <summary>
        /// Удаление текущей области
        /// </summary>
        /// <param name="area">Удаляемый объект области</param>
        void DeleteArea(Area area);
        /// <summary>
        /// Получение списка областей
        /// </summary>
        /// <returns>Список областей из базы данных</returns>
        List<Area> GetAreas();
        /// <summary>
        /// Получение объекта области по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Объект области</returns>
        Area GetArea(int id);
        /// <summary>
        /// Получение объекта области по наименованию
        /// </summary>
        /// <param name="name">Наименование области</param>
        /// <returns>Объект области</returns>
        Area GetArea(string name);
    }
}

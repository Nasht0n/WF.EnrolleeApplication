using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Атрибуты"
    /// </summary>
    interface IAtributeService
    {
        /// <summary>
        /// Добавление нового атрибута
        /// </summary>
        /// <param name="atribute">Добавляемый атрибут</param>
        /// <returns>Новая запись</returns>
        Atribute InsertAtribute(Atribute atribute);
        /// <summary>
        /// Обновление атрибута
        /// </summary>
        /// <param name="atribute">Редактируемый атритут</param>
        /// <returns>Обновленная запись</returns>
        Atribute UpdateAtribute(Atribute atribute);
        /// <summary>
        /// Удаление атрибута
        /// </summary>
        /// <param name="atribute">Удаляемая запись</param>
        void DeleteAtribute(Atribute atribute);
        /// <summary>
        /// Получение списка атрибутов
        /// </summary>
        /// <returns>Список атрибутов</returns>
        List<Atribute> GetAtributes();
        /// <summary>
        /// Получение списка атрибутов
        /// </summary>
        /// <param name="IsDiscount">Фильтр льгот</param>
        /// <returns>Отфильтрованный список</returns>
        List<Atribute> GetAtributes(bool IsDiscount);
        /// <summary>
        /// Получение атрибута по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Искомая запись атрибута</returns>
        Atribute GetAtribute(int id);
        /// <summary>
        /// Получение атрибута по наименованию
        /// </summary>
        /// <param name="fullname">Полное наименование атрибута</param>
        /// <returns>Искомая запись атрибута</returns>
        Atribute GetAtribute(string fullname);
    }
}

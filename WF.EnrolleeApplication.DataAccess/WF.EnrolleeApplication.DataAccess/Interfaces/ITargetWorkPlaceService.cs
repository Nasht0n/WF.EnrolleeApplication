using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Целевые рабочие места"
    /// </summary>
    interface ITargetWorkPlaceService
    {
        /// <summary>
        /// Добавление целевого рабочего места
        /// </summary>
        /// <param name="targetWorkPlace">Новое целевое место</param>
        /// <returns>Новая запись</returns>
        TargetWorkPlace InsertTargetWorkPlace(TargetWorkPlace targetWorkPlace);
        /// <summary>
        /// Обновление целевого рабочего места
        /// </summary>
        /// <param name="targetWorkPlace">Редактируемое целевое место</param>
        /// <returns>Отредактированная запись</returns>
        TargetWorkPlace UpdateTargetWorkPlace(TargetWorkPlace targetWorkPlace);
        /// <summary>
        /// Удаление целевого рабочего места
        /// </summary>
        /// <param name="targetWorkPlace">Удаляемая запись</param>
        void DeleteTargetWorkPlace(TargetWorkPlace targetWorkPlace);
        /// <summary>
        /// Получение списка целевых мест
        /// </summary>
        /// <returns>Список целевых мест</returns>
        List<TargetWorkPlace> GetTargetWorkPlaces();
        /// <summary>
        /// Получение целевого рабочего места по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись целевого места</returns>
        TargetWorkPlace GetTargetWorkPlace(int id);
        /// <summary>
        /// Получение целевого рабочего места по наименованию
        /// </summary>
        /// <param name="name">Наименование целевого места</param>
        /// <returns>Запись целевого места</returns>
        TargetWorkPlace GetTargetWorkPlace(string name);
    }
}

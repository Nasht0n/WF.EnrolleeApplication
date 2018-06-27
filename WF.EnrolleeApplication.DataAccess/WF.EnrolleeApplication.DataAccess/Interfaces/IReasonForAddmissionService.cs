using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Основание зачисления"
    /// </summary>
    interface IReasonForAddmissionService
    {
        /// <summary>
        /// Добавление основания зачисления
        /// </summary>
        /// <param name="reasonForAddmission">Новое основание зачисления</param>
        /// <returns>Новая запись</returns>
        ReasonForAddmission InsertReasonForAddmission(ReasonForAddmission reasonForAddmission);
        /// <summary>
        /// Обновление основания зачисления
        /// </summary>
        /// <param name="reasonForAddmission">Редактируемая запись</param>
        /// <returns>Отредактированная запись</returns>
        ReasonForAddmission UpdateReasonForAddmission(ReasonForAddmission reasonForAddmission);
        /// <summary>
        /// Удаление основания зачисления
        /// </summary>
        /// <param name="reasonForAddmission">Удаляемая запись</param>
        void DeleteReasonForAddmission(ReasonForAddmission reasonForAddmission);
        /// <summary>
        /// Получение списка оснований зачисления
        /// </summary>
        /// <returns>Список оснований зачисления</returns>
        List<ReasonForAddmission> GetReasonForAddmissions();
        /// <summary>
        /// Получение списка оснований зачисления
        /// </summary>
        /// <param name="contest">Фильтр по типу конкурса</param>
        /// <returns>Список оснований зачисления</returns>
        List<ReasonForAddmission> GetReasonForAddmissions(Contest contest);
        /// <summary>
        /// Получение записи основания зачисления по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись основания зачисления</returns>
        ReasonForAddmission GetReasonForAddmission(int id);
        /// <summary>
        /// Получение записи основания зачисления по наименованию
        /// </summary>
        /// <param name="fullname">Наименованию</param>
        /// <returns>Запись основания зачисления</returns>
        ReasonForAddmission GetReasonForAddmission(string fullname);
    }
}

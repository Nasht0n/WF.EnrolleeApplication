using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    ///  Интерфейс работы с данными таблицы "Приоритеты специальностей"
    /// </summary>
    interface IPriorityOfSpecialityService
    {
        /// <summary>
        /// Добавление нового приоритета специальности
        /// </summary>
        /// <param name="priorityOfSpeciality">Новый приоритет</param>
        /// <returns>Новая запись</returns>
        PriorityOfSpeciality InsertPriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality);
        /// <summary>
        /// Обновление приоритета специальности
        /// </summary>
        /// <param name="priorityOfSpeciality">Редактируемый приоритет</param>
        /// <returns>Отредактированная запись</returns>
        PriorityOfSpeciality UpdatePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality);
        /// <summary>
        /// Удаление приоритета специальности
        /// </summary>
        /// <param name="priorityOfSpeciality">Удаляемая запись</param>
        void DeletePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality);
        /// <summary>
        /// Получение списка приоритетов
        /// </summary>
        /// <returns>Список приоритетов</returns>
        List<PriorityOfSpeciality> GetPriorityOfSpecialities();
        /// <summary>
        /// Получение списка приоритетов абитуриента
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <returns>Список приоритетов</returns>
        List<PriorityOfSpeciality> GetPriorityOfSpecialities(Enrollee enrollee);
        /// <summary>
        /// Получение записи приоритета по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись приоритета</returns>
        PriorityOfSpeciality GetPriorityOfSpeciality(int id);
        /// <summary>
        /// Получение записи приоритета по параметрам
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <param name="speciality">Фильтр по специальности</param>
        /// <returns>Запись приоритета</returns>
        PriorityOfSpeciality GetPriorityOfSpeciality(Enrollee enrollee, Speciality speciality);
    }
}

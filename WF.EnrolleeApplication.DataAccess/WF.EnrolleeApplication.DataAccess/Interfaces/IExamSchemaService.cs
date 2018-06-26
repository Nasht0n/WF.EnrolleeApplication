using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Экзаменнационная схема"
    /// </summary>
    interface IExamSchemaService
    {
        /// <summary>
        /// Новая запись экзаменнационной схемы
        /// </summary>
        /// <param name="examSchema">Экзаменнационная схема</param>
        /// <returns>Новая запись</returns>
        ExamSchema InsertExamSchema(ExamSchema examSchema);
        /// <summary>
        /// Удаление записи экзаменнационной схемы
        /// </summary>
        /// <param name="examSchema">Удаляемая схема</param>
        void DeleteExamSchema(ExamSchema examSchema);
        /// <summary>
        /// Получение списка экзаменнационых схем
        /// </summary>
        /// <returns>Список экзаменнационных схем</returns>
        List<ExamSchema> GetExamSchemas();
        /// <summary>
        /// Получение списка экзаменнационной схемы специальности
        /// </summary>
        /// <param name="speciality">Специальность</param>
        /// <returns>Список экзаменнационных схем</returns>
        List<ExamSchema> GetExamSchemas(Speciality speciality);
        /// <summary>
        /// Получение записи экзаменнационной схемы
        /// </summary>
        /// <param name="speciality">Специальность</param>
        /// <param name="discipline">Дисциплина</param>
        /// <returns>Запись экзаменнационной схемы</returns>
        ExamSchema GetExamSchema(Speciality speciality, Discipline discipline);
    }
}

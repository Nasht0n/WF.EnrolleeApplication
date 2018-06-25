using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Оценки"
    /// </summary>
    interface IAssessmentService
    {
        /// <summary>
        /// Добавление новой оценки
        /// </summary>
        /// <param name="assessment">Данные о текущей оценке</param>
        /// <returns>Запись о добавленной оценке в БД</returns>
        Assessment InsertAssessment(Assessment assessment);
        /// <summary>
        /// Обновление данных текущей оценки
        /// </summary>
        /// <param name="assessment">Редактируемая запись об оценке</param>
        /// <returns>Отредактированный объект</returns>
        Assessment UpdateAssessment(Assessment assessment);
        /// <summary>
        /// Удаление записи об оценке
        /// </summary>
        /// <param name="assessment">Удаляемая запись</param>
        void DeleteAssessment(Assessment assessment);
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="discipline">Фильтр по дисциплине</param>
        /// <returns>Отфильтрованный список оценок</returns>
        List<Assessment> GetAssessments(Discipline discipline);
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="discipline">Фильтр по дисциплине</param>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Отфильтрованный список оценок</returns>
        List<Assessment> GetAssessments(Discipline discipline, BasisForAssessing basisForAssessing);
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <returns>Отфильтрованный список оценок</returns>
        List<Assessment> GetAssessments(Enrollee enrollee);
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Отфильтрованный список оценок</returns>
        List<Assessment> GetAssessments(Enrollee enrollee, BasisForAssessing basisForAssessing);
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Отфильтрованный список оценок</returns>
        List<Assessment> GetAssessments(BasisForAssessing basisForAssessing);
        /// <summary>
        /// Получение оценки по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Искомая запись об оценке</returns>
        Assessment GetAssessment(int id);
        /// <summary>
        /// Получение оценки по номеру сертификата
        /// </summary>
        /// <param name="sertcode">Номер сертификата</param>
        /// <returns>Искомая запись об оценке</returns>
        Assessment GetAssessment(string sertcode);
        /// <summary>
        /// Получение оценки по дисциплине и абитуриенту
        /// </summary>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="enrollee">Абитуриент</param>
        /// <returns>Искомая запись об оценке</returns>
        Assessment GetAssessment(Discipline discipline, Enrollee enrollee);
        /// <summary>
        /// Получение оценки по дисциплине, абитуриенту и типу оценивания
        /// </summary>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="enrollee">Абитуриент</param>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Искомая запись об оценке</returns>
        Assessment GetAssessment(Discipline discipline, Enrollee enrollee, BasisForAssessing basisForAssessing);
    }
}

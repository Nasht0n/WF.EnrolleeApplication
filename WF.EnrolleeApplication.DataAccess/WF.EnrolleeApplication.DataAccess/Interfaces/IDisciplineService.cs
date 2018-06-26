using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Дисциплины"
    /// </summary>
    interface IDisciplineService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="discipline">Новая дисциплина</param>
        /// <returns>Добавленная запись</returns>
        Discipline InsertDiscipline(Discipline discipline);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="discipline">Редактируема дисциплина</param>
        /// <returns>Отредактированная запись</returns>
        Discipline UpdateDiscipline(Discipline discipline);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="discipline">Удаляемая запись</param>
        void DeleteDiscipline(Discipline discipline);
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <returns>Список дисциплин</returns>
        List<Discipline> GetDisciplines();
        /// <summary>
        /// Получение списка групп дисциплин
        /// </summary>
        /// <param name="IsGroup">Группа дисциплин?</param>
        /// <returns>Список дисциплин</returns>
        List<Discipline> GetDisciplines(bool IsGroup);
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="basisForAssessing">Тип оценивания</param>
        /// <param name="IsGroup">Группа дисциплин?</param>
        /// <returns>Список дисциплин</returns>
        List<Discipline> GetDisciplines(BasisForAssessing basisForAssessing, bool IsGroup);
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="discipline">Дисциплина</param>
        /// <returns>Список дисциплин</returns>
        List<Discipline> GetDisciplines(Discipline discipline);
        /// <summary>
        /// Получение дисциплины по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись дисциплины</returns>
        Discipline GetDiscipline(int id);
        /// <summary>
        /// Получение дисциплины по наименованию
        /// </summary>
        /// <param name="name">Наименование дисциплины</param>
        /// <returns>Запись дисциплины</returns>
        Discipline GetDiscipline(string name);
    }
}

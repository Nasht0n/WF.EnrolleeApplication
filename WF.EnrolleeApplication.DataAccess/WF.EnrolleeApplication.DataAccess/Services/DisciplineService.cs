using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    /// <summary>
    /// Класс доступа к данным таблицы "Дисциплины"
    /// </summary>
    public class DisciplineService : IDisciplineService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DisciplineService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="discipline">Удаляемая запись</param>
        public void DeleteDiscipline(Discipline discipline)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению дисциплины.");
            try
            {
                var disciplineToDelete = context.Discipline.FirstOrDefault(d => d.DisciplineId == discipline.DisciplineId);
                if (disciplineToDelete != null)
                {
                    context.Discipline.Remove(disciplineToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи дисциплины.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи дисциплины.");
                logger.Error($"Ошибка — {ex.Message}.");
            }      
        }
        /// <summary>
        /// Получение дисциплины по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись дисциплины</returns>
        public Discipline GetDiscipline(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску дисциплины по уникальному идентификатору.");
            try
            {
                var disciplineById = context.Discipline.AsNoTracking().FirstOrDefault(d => d.DisciplineId == id);
                if (disciplineById != null) logger.Debug($"Поиск окончен. Запись найдена {disciplineById.ToString()}.");
                return disciplineById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи дисциплины.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи дисциплины.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение дисциплины по наименованию
        /// </summary>
        /// <param name="name">Наименование дисциплины</param>
        /// <returns>Запись дисциплины</returns>
        public Discipline GetDiscipline(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску дисциплины по наименованию.");
            try
            {
                var disciplineByName = context.Discipline.AsNoTracking().FirstOrDefault(d => d.Name == name);
                if (disciplineByName != null) logger.Debug($"Поиск окончен. Запись найдена {disciplineByName.ToString()}.");
                return disciplineByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи дисциплины.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи дисциплины.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <returns>Список дисциплин</returns>
        public List<Discipline> GetDisciplines()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка дисциплин.");
            try
            {
                var disciplines = context.Discipline.AsNoTracking().ToList();
                if (disciplines.Count!=0) logger.Debug($"Поиск окончен. Количество записей: {disciplines.Count}.");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return disciplines;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка дисциплин.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка дисциплин.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка групп дисциплин
        /// </summary>
        /// <param name="IsGroup">Группа дисциплин?</param>
        /// <returns>Список дисциплин</returns>
        public List<Discipline> GetDisciplines(bool IsGroup)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка дисциплин общей группы дисциплин.");
            try
            {
                var disciplines = context.Discipline.AsNoTracking().Where(d => d.IsGroup == IsGroup).ToList();
                if (disciplines.Count != 0) logger.Debug($"Поиск окончен. Количество записей: {disciplines.Count}.");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return disciplines;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка дисциплин общей группы дисциплин.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка дисциплин общей группы дисциплин.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="basisForAssessing">Тип оценивания</param>
        /// <param name="IsGroup">Группа дисциплин?</param>
        /// <returns>Список дисциплин</returns>
        public List<Discipline> GetDisciplines(BasisForAssessing basisForAssessing, bool IsGroup)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка дисциплин по параметрам.");
            try
            {
                var disciplines = context.Discipline.AsNoTracking().Where(d => d.IsGroup == IsGroup && d.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
                if (disciplines.Count != 0) logger.Debug($"Поиск окончен. Количество записей: {disciplines.Count}.");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return disciplines;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка дисциплин по параметрам.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка дисциплин по параметрам.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="disciplineGroup">Дисциплина</param>
        /// <returns>Список дисциплин</returns>
        public List<Discipline> GetDisciplines(Discipline disciplineGroup)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка дисциплин, общей группы.");
            try
            {
                var disciplines = context.Discipline.AsNoTracking().Where(d => d.DisciplineGroupId == disciplineGroup.DisciplineId).ToList();
                if (disciplines.Count != 0) logger.Debug($"Поиск окончен. Количество записей: {disciplines.Count}.");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return disciplines;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка дисциплин, общей группы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка дисциплин, общей группы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="discipline">Новая дисциплина</param>
        /// <returns>Добавленная запись</returns>
        public Discipline InsertDiscipline(Discipline discipline)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению дисцпиплины");
            try
            {
                logger.Debug($"Добавляемая запись {discipline.ToString()}");
                context.Discipline.Add(discipline);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return discipline;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления дисцпиплины.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления дисцпиплины.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="discipline">Редактируема дисциплина</param>
        /// <returns>Отредактированная запись</returns>
        public Discipline UpdateDiscipline(Discipline discipline)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению дисциплины.");
            try
            {
                var disciplineToUpdate = context.Discipline.FirstOrDefault(d => d.DisciplineId == discipline.DisciplineId);
                logger.Debug($"Текущая запись {disciplineToUpdate.ToString()}");
                disciplineToUpdate.BasisForAssessingId = discipline.BasisForAssessingId;
                disciplineToUpdate.Name = discipline.Name;
                disciplineToUpdate.IsGroup = discipline.IsGroup;
                disciplineToUpdate.IsAlternative = discipline.IsAlternative;
                disciplineToUpdate.DisciplineGroupId = discipline.DisciplineGroupId;
                disciplineToUpdate.ConsultDate = discipline.ConsultDate;
                disciplineToUpdate.EntryExamDate = discipline.EntryExamDate;
                disciplineToUpdate.StageCount = discipline.StageCount;
                context.SaveChanges();
                logger.Debug($"Новая запись {disciplineToUpdate.ToString()}");
                return disciplineToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования дисциплины.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования дисциплины.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

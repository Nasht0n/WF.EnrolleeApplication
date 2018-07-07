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
    /// Класс доступа к данным таблицы "Виды конкурса"
    /// </summary>
    public class ContestService : IContestService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ContestService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="contest">Удаляемая запись</param>
        public void DeleteContest(Contest contest)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению вида конкурса.");
            try
            {
                var contestToDelete = context.Contest.FirstOrDefault(c => c.ContestId == contest.ContestId);
                if (contestToDelete != null)
                {
                    context.Contest.Remove(contestToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления вида конкурса.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления вида конкурса.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение вида конкурса по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Найденная запись вида конкурса</returns>
        public Contest GetContest(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску вида конкурса по уникальному идентификатору.");
            try
            {
                var contestById = context.Contest.AsNoTracking().FirstOrDefault(c => c.ContestId == id);
                if (contestById != null) logger.Debug($"Поиск окончен. Искомая запись: {contestById.ToString()}.");
                return contestById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска вида конкурса.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска вида конкурса.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение вида конкурса по наименованию
        /// </summary>
        /// <param name="fullname">Наименование вида конкурса</param>
        /// <returns>Найденная запись вида конкурса</returns>
        public Contest GetContest(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску вида конкурса по наименованию.");
            try
            {
                var contestByName = context.Contest.AsNoTracking().FirstOrDefault(c => c.Name == name);
                if (contestByName != null) logger.Debug($"Поиск окончен. Искомая запись: {contestByName.ToString()}.");
                return contestByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска вида конкурса.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска вида конкурса.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка видов конкурса
        /// </summary>
        /// <returns>Список видов конкурса</returns>
        public List<Contest> GetContests()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка видов конкурса.");
            try
            {
                var contests = context.Contest.AsNoTracking().ToList();
                if (contests.Count!=0) logger.Debug($"Поиск окончен. Количество записей: {contests.Count}.");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return contests;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка видов конкурса.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка видов конкурса.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="contest">Новый вид конкурса</param>
        /// <returns>Добавленная запись</returns>
        public Contest InsertContest(Contest contest)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению вида конкурса.");
            try
            {
                logger.Debug($"Добавляемая запись: {contest.ToString()}");
                context.Contest.Add(contest);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return contest;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления вида конкурса.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления вида конкурса.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="contest">Редактируемая запись вида конкурса</param>
        /// <returns>Отредактированная запись</returns>
        public Contest UpdateContest(Contest contest)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению вида конкурса.");
            try
            {
                var contestToUpdate = context.Contest.FirstOrDefault(c => c.ContestId == contest.ContestId);
                logger.Debug($"Текущая запись: {contestToUpdate.ToString()}");
                contestToUpdate.Name = contest.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись: {contestToUpdate.ToString()}");
                return contestToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования вида конкурса.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования вида конкурса.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

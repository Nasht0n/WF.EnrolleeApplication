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
    /// Класс доступа к данным таблицы "Приоритет специальностей"
    /// </summary>
    public class PriorityOfSpecialityService : IPriorityOfSpecialityService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public PriorityOfSpecialityService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление приоритета специальности
        /// </summary>
        /// <param name="priorityOfSpeciality">Удаляемая запись</param>
        public void DeletePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению приоритета специальности.");
            try
            {
                logger.Debug($"Поиск записи приоритета специальности для удаления. Удаляемый объект : {priorityOfSpeciality.ToString()}.");
                var priorityOfSpecialityToDelete = context.PriorityOfSpeciality.FirstOrDefault(ps => ps.PriorityId == priorityOfSpeciality.PriorityId);
                if (priorityOfSpecialityToDelete != null)
                {
                    context.PriorityOfSpeciality.Remove(priorityOfSpecialityToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи приоритета специальности успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи приоритета специальности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи приоритета специальности.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение списка приоритетов
        /// </summary>
        /// <returns>Список приоритетов</returns>
        public List<PriorityOfSpeciality> GetPriorityOfSpecialities()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка приоритетов.");
            try
            {
                logger.Debug($"Получение списка приоритетов.");
                var priorityOfSpecialities = context.PriorityOfSpeciality.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {priorityOfSpecialities.Count}.");
                return priorityOfSpecialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка приоритетов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка приоритетов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка приоритетов абитуриента
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <returns>Список приоритетов</returns>
        public List<PriorityOfSpeciality> GetPriorityOfSpecialities(Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка приоритетов абитуриента.");
            try
            {
                logger.Debug($"Получение списка приоритетов. Абитуриент = [{enrollee.ToString()}]");
                var priorityOfSpecialities = context.PriorityOfSpeciality.AsNoTracking().Where(ps => ps.EnrolleeId == enrollee.EnrolleeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {priorityOfSpecialities.Count}.");
                return priorityOfSpecialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка приоритетов абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка приоритетов абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи приоритета по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись приоритета</returns>
        public PriorityOfSpeciality GetPriorityOfSpeciality(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску приоритета по уникальному идентификатору.");
            try
            {
                logger.Debug($"Поиск записи приоритета по уникальному идентификатору = {id}.");
                var priorityOfSpecialityById = context.PriorityOfSpeciality.AsNoTracking().FirstOrDefault(ps => ps.EnrolleeId == id);
                if (priorityOfSpecialityById != null) logger.Debug($"Поиск окончен. Искомая запись: {priorityOfSpecialityById.ToString()}.");
                return priorityOfSpecialityById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи приоритета по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи приоритета по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи приоритета по параметрам
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <param name="speciality">Фильтр по специальности</param>
        /// <returns>Запись приоритета</returns>
        public PriorityOfSpeciality GetPriorityOfSpeciality(Enrollee enrollee, Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску приоритета по параметрам.");
            try
            {
                logger.Debug($"Поиск записи приоритета по параметрам. Абитуриент = [{enrollee.ToString()}]; Специальность = [{speciality.ToString()}].");
                var priorityOfSpeciality = context.PriorityOfSpeciality.AsNoTracking().FirstOrDefault(ps => ps.EnrolleeId == enrollee.EnrolleeId && ps.SpecialityId == speciality.SpecialityId);
                if (priorityOfSpeciality != null) logger.Debug($"Поиск окончен. Искомая запись: {priorityOfSpeciality.ToString()}.");
                return priorityOfSpeciality;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи приоритета по параметрам.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи приоритета по параметрам.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление нового приоритета специальности
        /// </summary>
        /// <param name="priorityOfSpeciality">Новый приоритет</param>
        /// <returns>Новая запись</returns>
        public PriorityOfSpeciality InsertPriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению приоритета специальности");
            try
            {
                logger.Debug($"Добавляемая запись: {priorityOfSpeciality.ToString()}");
                context.PriorityOfSpeciality.Add(priorityOfSpeciality);
                context.SaveChanges();
                logger.Debug($"Приоритет специальности успешно добавлена.");
                return priorityOfSpeciality;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления приоритета специальности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления приоритета специальности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление приоритета специальности
        /// </summary>
        /// <param name="priorityOfSpeciality">Редактируемый приоритет</param>
        /// <returns>Отредактированная запись</returns>
        public PriorityOfSpeciality UpdatePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению приоритета специальности.");
            try
            {
                var priorityOfSpecialityToUpdate = context.PriorityOfSpeciality.FirstOrDefault(ps => ps.PriorityId == priorityOfSpeciality.PriorityId);
                logger.Debug($"Текущая запись: {priorityOfSpecialityToUpdate.ToString()}");
                priorityOfSpecialityToUpdate.EnrolleeId = priorityOfSpeciality.EnrolleeId;
                priorityOfSpecialityToUpdate.SpecialityId = priorityOfSpeciality.SpecialityId;
                priorityOfSpecialityToUpdate.PriorityLevel = priorityOfSpeciality.PriorityLevel;
                context.SaveChanges();
                logger.Debug($"Новая запись: {priorityOfSpecialityToUpdate.ToString()}");
                return priorityOfSpecialityToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования приоритета специальности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования приоритета специальности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

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
    /// Класс доступа к данным таблицы "Основание зачисления"
    /// </summary>
    public class ReasonForAddmissionService : IReasonForAddmissionService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ReasonForAddmissionService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление основания зачисления
        /// </summary>
        /// <param name="reasonForAddmission">Удаляемая запись</param>
        public void DeleteReasonForAddmission(ReasonForAddmission reasonForAddmission)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению основания зачисления.");
            try
            {
                logger.Debug($"Поиск записи основания зачисления для удаления. Удаляемый объект : {reasonForAddmission.ToString()}.");
                var reasonForAddmissionToDelete = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == reasonForAddmission.ReasonForAddmissionId);
                if (reasonForAddmissionToDelete != null)
                {
                    context.ReasonForAddmission.Remove(reasonForAddmissionToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи основания зачисления успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи основания зачисления.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи основания зачисления.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение записи основания зачисления по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись основания зачисления</returns>
        public ReasonForAddmission GetReasonForAddmission(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску основания зачисления по уникальному идентификатору.");
            try
            {
                logger.Debug($"Поиск записи основания зачисления по уникальному идентификатору = {id}.");
                var reasonForAddmissionById = context.ReasonForAddmission.AsNoTracking().FirstOrDefault(r => r.ReasonForAddmissionId == id);
                if (reasonForAddmissionById != null) logger.Debug($"Поиск окончен. Искомая запись: {reasonForAddmissionById.ToString()}.");
                return reasonForAddmissionById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи основания зачисления по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи основания зачисления по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи основания зачисления по наименованию
        /// </summary>
        /// <param name="fullname">Наименованию</param>
        /// <returns>Запись основания зачисления</returns>
        public ReasonForAddmission GetReasonForAddmission(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску основания зачисления по наименованию.");
            try
            {
                logger.Debug($"Поиск записи основания зачисления по наименованию = {fullname}.");
                var reasonForAddmissionByName = context.ReasonForAddmission.AsNoTracking().FirstOrDefault(r => r.Fullname == fullname);
                if (reasonForAddmissionByName != null) logger.Debug($"Поиск окончен. Искомая запись: {reasonForAddmissionByName.ToString()}.");
                return reasonForAddmissionByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи основания зачисления по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи основания зачисления по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оснований зачисления
        /// </summary>
        /// <returns>Список оснований зачисления</returns>
        public List<ReasonForAddmission> GetReasonForAddmissions()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оснований зачисления.");
            try
            {
                logger.Debug($"Получение списка оснований зачисления.");
                var reasonForAddmissions = context.ReasonForAddmission.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {reasonForAddmissions.Count}.");
                return reasonForAddmissions;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка оснований зачисления.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка оснований зачисления.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оснований зачисления
        /// </summary>
        /// <param name="contest">Фильтр по типу конкурса</param>
        /// <returns>Список оснований зачисления</returns>
        public List<ReasonForAddmission> GetReasonForAddmissions(Contest contest)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оснований зачисления конкурса.");
            try
            {
                logger.Debug($"Получение списка оснований зачисления. Конкурс = [{contest.ToString()}]");
                var reasonForAddmissions = context.ReasonForAddmission.AsNoTracking().Where(r => r.ContestId == contest.ContestId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {reasonForAddmissions.Count}.");
                return reasonForAddmissions;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка оснований зачисления конкурса.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка оснований зачисления конкурса.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление основания зачисления
        /// </summary>
        /// <param name="reasonForAddmission">Новое основание зачисления</param>
        /// <returns>Новая запись</returns>
        public ReasonForAddmission InsertReasonForAddmission(ReasonForAddmission reasonForAddmission)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению основания зачисления");
            try
            {
                logger.Debug($"Добавляемая запись: {reasonForAddmission.ToString()}");
                context.ReasonForAddmission.Add(reasonForAddmission);
                context.SaveChanges();
                logger.Debug($"Основание специальности успешно добавлена.");
                return reasonForAddmission;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления основания зачисления.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления основания зачисления.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление основания зачисления
        /// </summary>
        /// <param name="reasonForAddmission">Редактируемая запись</param>
        /// <returns>Отредактированная запись</returns>
        public ReasonForAddmission UpdateReasonForAddmission(ReasonForAddmission reasonForAddmission)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению основания зачисления.");
            try
            {
                var reasonForAddmissionToUpdate = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == reasonForAddmission.ReasonForAddmissionId);
                logger.Debug($"Текущая запись: {reasonForAddmissionToUpdate.ToString()}");
                reasonForAddmissionToUpdate.ContestId = reasonForAddmission.ContestId;
                reasonForAddmissionToUpdate.Fullname = reasonForAddmission.Fullname;
                reasonForAddmissionToUpdate.Shortname = reasonForAddmission.Shortname;
                context.SaveChanges();
                logger.Debug($"Новая запись: {reasonForAddmissionToUpdate.ToString()}");
                return reasonForAddmissionToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования основания зачисления.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования основания зачисления.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

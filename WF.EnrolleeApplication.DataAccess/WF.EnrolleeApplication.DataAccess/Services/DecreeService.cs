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
    /// Класс доступа к данным таблицы "Приказы"
    /// </summary>
    public class DecreeService : IDecreeService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DecreeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="decree">Удаляемая запись</param>
        public void DeleteDecree(Decree decree)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению приказа.");
            try
            {
                logger.Debug($"Поиск записи приказа для удаления. Удаляемый объект : {decree.ToString()}.");
                Decree decreeToDelete = context.Decree.FirstOrDefault(d => d.DecreeId == decree.DecreeId);
                if (decreeToDelete != null)
                {
                    context.Decree.Remove(decreeToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи приказа успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи приказа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи приказа.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение записи приказа по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись приказа</returns>
        public Decree GetDecree(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску приказа.");
            try
            {
                logger.Debug($"Поиск записи приказа по уникальному идентификатору = {id}.");
                Decree decreeById = context.Decree.AsNoTracking().FirstOrDefault(d => d.DecreeId == id);
                if (decreeById != null) logger.Debug($"Поиск окончен. Искомая запись: {decreeById.ToString()}.");
                return decreeById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи приказа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи приказа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи приказа по дате приказа
        /// </summary>
        /// <param name="decreeDate">Дата приказа</param>
        /// <returns>Запись приказа</returns>
        public Decree GetDecree(DateTime decreeDate)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску приказа.");
            try
            {
                logger.Debug($"Поиск записи приказа по дате приказа = {decreeDate.ToShortDateString().Trim()}.");
                Decree decreeByDate = context.Decree.AsNoTracking().FirstOrDefault(d => d.DecreeDate == decreeDate.Date);
                if (decreeByDate != null) logger.Debug($"Поиск окончен. Искомая запись: {decreeByDate.ToString()}.");
                return decreeByDate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи приказа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи приказа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи приказа по номеру приказа
        /// </summary>
        /// <param name="decreeNumber">Номер приказа</param>
        /// <returns>Запись приказа</returns>
        public Decree GetDecree(string decreeNumber)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску приказа.");
            try
            {
                logger.Debug($"Поиск записи приказа по номеру приказа = {decreeNumber.Trim()}.");
                Decree decreeByNumber = context.Decree.AsNoTracking().FirstOrDefault(d => d.DecreeNumber.Trim() == decreeNumber.Trim());
                if (decreeByNumber != null) logger.Debug($"Поиск окончен. Искомая запись: {decreeByNumber.ToString()}.");
                return decreeByNumber;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи приказа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи приказа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка приказов
        /// </summary>
        /// <returns>Список приказов</returns>
        public List<Decree> GetDecrees()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка приказов.");
            try
            {
                logger.Debug($"Получение списка приказов.");
                List<Decree> decrees = context.Decree.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {decrees.Count}.");
                return decrees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка приказов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка приказов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="decree">Новая запись приказа</param>
        /// <returns>Добавленная запись</returns>
        public Decree InsertDecree(Decree decree)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению приказа.");
            try
            {
                logger.Debug($"Добавляемая запись: {decree.ToString()}");
                context.Decree.Add(decree);
                context.SaveChanges();
                logger.Debug($"Приказ успешно добавлен.");
                return decree;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления приказа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления приказа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="decree">Редактируемая запись приказа</param>
        /// <returns>Отредактированная запись</returns>
        public Decree UpdateDecree(Decree decree)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению приказа.");
            try
            {
                Decree decreeToUpdate = context.Decree.FirstOrDefault(d => d.DecreeId == decree.DecreeId);
                logger.Debug($"Текущая запись: {decreeToUpdate.ToString()}");
                decreeToUpdate.DecreeNumber = decree.DecreeNumber;
                decreeToUpdate.DecreeDate = decree.DecreeDate;
                decreeToUpdate.Content = decree.Content;
                decreeToUpdate.ProtocolNumber = decree.ProtocolNumber;
                decreeToUpdate.ProtocolDate = decree.ProtocolDate;
                context.SaveChanges();
                logger.Debug($"Новая запись: {decreeToUpdate.ToString()}");
                return decreeToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования приказа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования приказа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

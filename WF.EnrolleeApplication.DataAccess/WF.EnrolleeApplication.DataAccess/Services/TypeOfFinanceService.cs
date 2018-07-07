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
    /// Класс доступа к данным таблицы "Типы финансирования"
    /// </summary>
    public class TypeOfFinanceService : ITypeOfFinanceService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public TypeOfFinanceService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление типа финансирования
        /// </summary>
        /// <param name="typeOfFinance">Удаление записи</param>
        public void DeleteTypeOfFinance(TypeOfFinance typeOfFinance)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению типа финансирования.");
            try
            {
                var typeOfFinanceToDelete = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == typeOfFinance.FinanceTypeId);
                if (typeOfFinanceToDelete != null)
                {
                    context.TypeOfFinance.Remove(typeOfFinanceToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи типа финансирования успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи типа финансирования.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи типа финансирования.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение записи типа финансирования по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификтор</param>
        /// <returns>Запись типа финансирования</returns>
        public TypeOfFinance GetTypeOfFinance(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа финансирования по уникальному идентификатору.");
            try
            {
                var typeOfFinanceById = context.TypeOfFinance.AsNoTracking().FirstOrDefault(tf => tf.FinanceTypeId == id);
                if (typeOfFinanceById != null) logger.Debug($"Поиск окончен. Запись найдена {typeOfFinanceById.ToString()}.");
                return typeOfFinanceById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа финансирования по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа финансирования по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи типа финансирования по наименованию
        /// </summary>
        /// <param name="fullname">Наименование типа финансирования</param>
        /// <returns>Запись типа финансирования</returns>
        public TypeOfFinance GetTypeOfFinance(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа финансирования по наименованию.");
            try
            {
                var typeOfFinanceByName = context.TypeOfFinance.AsNoTracking().FirstOrDefault(tf => tf.Fullname == fullname);
                if (typeOfFinanceByName != null) logger.Debug($"Поиск окончен. Запись найдена {typeOfFinanceByName.ToString()}.");
                return typeOfFinanceByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа финансирования по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа финансирования по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка типов финансирования
        /// </summary>
        /// <returns>Список финансирования</returns>
        public List<TypeOfFinance> GetTypeOfFinances()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка типов финансирования.");
            try
            {
                var typeOfFinances = context.TypeOfFinance.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {typeOfFinances.Count}.");
                return typeOfFinances;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка типов финансирования.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка типов финансирования.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление нового типа финансирования
        /// </summary>
        /// <param name="typeOfFinance">Тип финансирования</param>
        /// <returns>Новая запись</returns>
        public TypeOfFinance InsertTypeOfFinance(TypeOfFinance typeOfFinance)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению типа финансирования");
            try
            {
                logger.Debug($"Добавляемая запись {typeOfFinance.ToString()}");
                context.TypeOfFinance.Add(typeOfFinance);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return typeOfFinance;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления типа финансирования.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления типа финансирования.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление типа финансирования
        /// </summary>
        /// <param name="typeOfFinance">Редактируемый тип финансирования</param>
        /// <returns>Отредактированная запись</returns>
        public TypeOfFinance UpdateTypeOfFinance(TypeOfFinance typeOfFinance)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению типа финансирования.");
            try
            {
                var typeOfFinanceToUpdate = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == typeOfFinance.FinanceTypeId);
                logger.Debug($"Текущая запись {typeOfFinanceToUpdate.ToString()}");
                typeOfFinanceToUpdate.Fullname = typeOfFinance.Fullname;
                context.SaveChanges();
                logger.Debug($"Новая запись {typeOfFinanceToUpdate.ToString()}");
                return typeOfFinanceToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования типа финансирования.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования типа финансирования.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

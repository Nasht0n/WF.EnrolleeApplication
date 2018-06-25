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
    /// Класс доступа к данным таблицы "Системы перевода"
    /// </summary>
    public class ConversionSystemService : IConversionSystemService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ConversionSystemService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Получение оценки в пятибалльной системе
        /// </summary>
        /// <param name="ten">Оценка в десятибалльной системе</param>
        /// <returns>Оценка в пятибалльной системе</returns>
        public double ConversionToFive(double ten)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к переводу оценки из десятибалльной в пятибалльную систему оценивания.");
            try
            {
                logger.Debug($"Поиск записи системы оценивания. Оценка в десятибалльной системе : {ten}.");
                ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.Ten == ten);
                if (conversionSystem != null) logger.Debug($"Поиск окончен. Оценка в пятибалльной системе: {conversionSystem.Five}.");
                return conversionSystem.Five;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка перевода оценки из десятибалльной в пятибалльную систему оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return -1;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка перевода оценки из десятибалльной в пятибалльную систему оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
                return -1;
            }
        }
        /// <summary>
        /// Получение оценки в десятибалльной системе
        /// </summary>
        /// <param name="five">Оценка в пятибалльной системе</param>
        /// <returns>Оценка в десятибалльной системе</returns>
        public double ConversionToTen(double five)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к переводу оценки из пятибалльной в десятибалльную систему оценивания.");
            try
            {
                logger.Debug($"Поиск записи системы оценивания. Оценка в пятибалльной системе : {five}.");
                ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.Five == five);
                if (conversionSystem != null) logger.Debug($"Поиск окончен. Оценка в десятибалльной системе: {conversionSystem.Ten}.");
                return conversionSystem.Ten;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка перевода оценки из пятибалльной в десятибалльную систему оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return -1;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка перевода оценки из пятибалльной в десятибалльную систему оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
                return -1;
            } 
        }
        /// <summary>
        /// Удаление новой записи
        /// </summary>
        /// <param name="conversionSystem">Удаляемая запись системы перевода</param>
        public void DeleteConversionSystem(ConversionSystem conversionSystem)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению записи системы перевода оценок.");
            try
            {
                logger.Debug($"Поиск записи системы перевода оценок для удаления. Удаляемый объект : {conversionSystem.ToString()}.");
                ConversionSystem conversionSystemToDelete = context.ConversionSystem.FirstOrDefault(cs => cs.Five == conversionSystem.Five && cs.Ten == conversionSystem.Ten);
                if (conversionSystemToDelete != null)
                {
                    context.ConversionSystem.Remove(conversionSystemToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи системы перевода оценок успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи системы перевода оценок.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи системы перевода оценок.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение записи системы перевода по оценкам
        /// </summary>
        /// <param name="five">Оценка в пятибалльной системе</param>
        /// <param name="ten">Оценка в десятибалльной системе</param>
        /// <returns>Запись системы перевода</returns>
        public ConversionSystem GetConversion(double five, double ten)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску записи системы перевода оценок.");
            try
            {
                logger.Debug($"Поиск записи системы перевода оценок (Пятибалльная система = {five}; Десятибалльная система = {ten}).");
                ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.Five == five && cs.Ten == ten);
                if (conversionSystem != null) logger.Debug($"Поиск окончен. Искомая запись: {conversionSystem.ToString()}.");
                return conversionSystem;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи системы перевода оценок.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи системы перевода оценок.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи системы перевода по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный сертификат</param>
        /// <returns>Запись системы перевода</returns>
        public ConversionSystem GetConversion(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску записи системы перевода оценок.");
            try
            {
                logger.Debug($"Поиск записи системы перевода оценок по уникальному идентификатору = {id}.");
                ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.ConversionSystemId == id);
                if (conversionSystem != null) logger.Debug($"Поиск окончен. Искомая запись: {conversionSystem.ToString()}.");
                return conversionSystem;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи системы перевода оценок.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи системы перевода оценок.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оценок системы перевода
        /// </summary>
        /// <returns>Список системы перевода оценок</returns>
        public List<ConversionSystem> GetConversions()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка записей системы перевода оценок.");
            try
            {
                logger.Debug($"Получение списка записей системы перевода оценок.");
                List<ConversionSystem> conversions = context.ConversionSystem.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {conversions.Count}.");
                return conversions;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка записей системы перевода оценок.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка записей системы перевода оценок.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="conversionSystem">Новая запись системы перевода</param>
        /// <returns>Добавленная запись</returns>
        public ConversionSystem InsertConversionSystem(ConversionSystem conversionSystem)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению записи в систему перевода оценок.");
            try
            {
                logger.Debug($"Добавляемая запись: {conversionSystem.ToString()}");
                context.ConversionSystem.Add(conversionSystem);
                context.SaveChanges();
                logger.Debug($"Запись перевода оценки успешно добавлена.");
                return conversionSystem;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления записи в систему перевода оценок.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления записи в систему перевода оценок.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

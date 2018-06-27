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
    /// Класс доступа к данным таблицы "Представление оценок"
    /// </summary>
    public class EstimationStringService : IEstimationStringService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public EstimationStringService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление представление оценки
        /// </summary>
        /// <param name="estimationString">Удаляемая оценка</param>
        public void DeleteEstimationString(EstimationString estimationString)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению представления оценки.");
            try
            {
                logger.Debug($"Поиск записи представления оценки для удаления. Удаляемый объект : {estimationString.ToString()}.");
                EstimationString estimationStringToDelete = context.EstimationString.FirstOrDefault(es => es.EstimationNumber == estimationString.EstimationNumber && es.EstimationText == estimationString.EstimationText);
                if (estimationStringToDelete != null)
                {
                    context.EstimationString.Remove(estimationStringToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи представления оценки успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи представления оценки.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи представления оценки.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение цифрого представления оценки
        /// </summary>
        /// <param name="estimation">Строковое представление оценки</param>
        /// <returns>Оценка цифрой</returns>
        public int EstimationAsNumber(string estimation)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску цифрового представления оценки.");
            try
            {
                logger.Debug($"Поиск записи представления оценки по строковому представлению оценки = {estimation.Trim()}.");
                var estimationString = context.EstimationString.AsNoTracking().FirstOrDefault(es => es.EstimationText == estimation);
                if (estimationString != null) logger.Debug($"Поиск окончен. Искомая запись: {estimationString.ToString()}.");
                return estimationString.EstimationNumber;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи представления оценки по строковому представлению оценки.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return -1;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи представления оценки по строковому представлению оценки.");
                logger.Error($"Ошибка — {ex.Message}.");
                return -1;
            }
        }
        /// <summary>
        /// Получение строкового представление оценки
        /// </summary>
        /// <param name="number">Цифровое представление оценки</param>
        /// <returns>Оценка прописью</returns>
        public string EstimationAsText(int number)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску цифрового представления оценки.");
            try
            {
                logger.Debug($"Поиск записи представления оценки по цифровому представлению оценки = {number}.");
                var estimationString = context.EstimationString.AsNoTracking().FirstOrDefault(es => es.EstimationNumber == number);
                if (estimationString != null) logger.Debug($"Поиск окончен. Искомая запись: {estimationString.ToString()}.");
                return estimationString.EstimationText;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи представления оценки по цифровому представлению оценки.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи представления оценки по цифровому представлению оценки.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение представления оценки
        /// </summary>
        /// <param name="number">Оценка цифрой</param>
        /// <param name="text">Оценка прописью</param>
        /// <returns>Запись представления оценки</returns>
        public EstimationString GetEstimationString(int number, string text)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску представления оценки.");
            try
            {
                logger.Debug($"Поиск записи представления оценки. Цифровое представлению оценки = [{number}]; Строковое представление оценки = [{text.Trim()}].");
                var estimationString = context.EstimationString.AsNoTracking().FirstOrDefault(es => es.EstimationNumber == number && es.EstimationText == text);
                if (estimationString != null) logger.Debug($"Поиск окончен. Искомая запись: {estimationString.ToString()}.");
                return estimationString;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи представления оценки.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи представления оценки.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка представления оценок
        /// </summary>
        /// <returns>Список оценок</returns>
        public List<EstimationString> GetEstimationStrings()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка представления оценок.");
            try
            {
                logger.Debug($"Получение списка представления оценок.");
                var estimationStrings = context.EstimationString.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {estimationStrings.Count}.");
                return estimationStrings;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка представления оценок.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка представления оценок.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Новое представление оценок
        /// </summary>
        /// <param name="estimationString">Представление оценки</param>
        /// <returns>Новая запись</returns>
        public EstimationString InsertEstimationString(EstimationString estimationString)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению представления оценки");
            try
            {
                logger.Debug($"Добавляемая запись: {estimationString.ToString()}");
                context.EstimationString.Add(estimationString);
                context.SaveChanges();
                logger.Debug($"Представление оценки успешно добавлена.");
                return estimationString;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления представления оценки.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления представления оценки.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

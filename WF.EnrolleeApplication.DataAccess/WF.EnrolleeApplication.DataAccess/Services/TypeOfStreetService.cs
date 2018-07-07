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
    /// Класс доступа к данным таблицы "Тип улицы"
    /// </summary>
    public class TypeOfStreetService : ITypeOfStreetService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public TypeOfStreetService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление типа улицы
        /// </summary>
        /// <param name="typeOfStreet">Удаляемый тип улицы</param>
        public void DeleteTypeOfStreet(TypeOfStreet typeOfStreet)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению типа улицы.");
            try
            {
                var typeOfStreetToDelete = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == typeOfStreet.StreetTypeId);
                if (typeOfStreetToDelete != null)
                {
                    context.TypeOfStreet.Remove(typeOfStreet);
                    context.SaveChanges();
                    logger.Debug("Удаление записи типа улицы успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи типа улицы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи типа улицы.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение типа улицы по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа улицы</returns>
        public TypeOfStreet GetTypeOfStreet(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа улицы по уникальному идентификатору.");
            try
            {
                var typeOfStreetId = context.TypeOfStreet.AsNoTracking().FirstOrDefault(ts => ts.StreetTypeId == id);
                if (typeOfStreetId != null) logger.Debug($"Поиск окончен. Запись найдена {typeOfStreetId.ToString()}.");
                return typeOfStreetId;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа улицы по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа улицы по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение типа улицы по наименованию
        /// </summary>
        /// <param name="fullname">Наименование</param>
        /// <returns>Запись типа улицы</returns>
        public TypeOfStreet GetTypeOfStreet(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа улицы по наименованию.");
            try
            {
                var typeOfStreetName = context.TypeOfStreet.AsNoTracking().FirstOrDefault(ts => ts.Fullname == fullname);
                if (typeOfStreetName != null) logger.Debug($"Поиск окончен. Запись найдена {typeOfStreetName.ToString()}.");
                return typeOfStreetName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа улицы по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа улицы по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка типов улиц
        /// </summary>
        /// <returns>Список типов улиц</returns>
        public List<TypeOfStreet> GetTypeOfStreets()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка типов улиц.");
            try
            {
                var typeOfStreets = context.TypeOfStreet.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {typeOfStreets.Count}.");
                return typeOfStreets;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка типов улиц.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка типов улиц.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление типа улицы
        /// </summary>
        /// <param name="typeOfStreet">Новый тип улицы</param>
        /// <returns>Новая запись</returns>
        public TypeOfStreet InsertTypeOfStreet(TypeOfStreet typeOfStreet)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению типа улицы");
            try
            {
                logger.Debug($"Добавляемая запись {typeOfStreet.ToString()}");
                context.TypeOfStreet.Add(typeOfStreet);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return typeOfStreet;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления типа улицы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления типа улицы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление типа улицы
        /// </summary>
        /// <param name="typeOfStreet">Редактируемый тип улицы</param>
        /// <returns>Отредактированный тип улицы</returns>
        public TypeOfStreet UpdateTypeOfStreet(TypeOfStreet typeOfStreet)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению типа улицы.");
            try
            {
                var typeOfStreetToUpdate = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == typeOfStreet.StreetTypeId);
                logger.Debug($"Текущая запись {typeOfStreetToUpdate.ToString()}");
                typeOfStreetToUpdate.Fullname = typeOfStreet.Fullname;
                typeOfStreetToUpdate.Shortname = typeOfStreet.Shortname;
                context.SaveChanges();
                logger.Debug($"Новая запись {typeOfStreetToUpdate.ToString()}");
                return typeOfStreetToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования типа улицы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования типа улицы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

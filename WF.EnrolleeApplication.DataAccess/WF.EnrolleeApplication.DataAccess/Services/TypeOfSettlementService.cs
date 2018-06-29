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
    /// Класс доступа к данным таблицы "Тип населенного пункта"
    /// </summary>
    public class TypeOfSettlementService : ITypeOfSettlementService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public TypeOfSettlementService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление типа населенного пункта
        /// </summary>
        /// <param name="typeOfSettlement">Удаляемая запись</param>
        public void DeleteTypeOfSettlement(TypeOfSettlement typeOfSettlement)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению типа населенного пункта.");
            try
            {
                logger.Debug($"Поиск записи типа населенного пункта для удаления. Удаляемый объект : {typeOfSettlement.ToString()}.");
                var typeOfSettlementToDelete = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == typeOfSettlement.SettlementTypeId);
                if (typeOfSettlementToDelete != null)
                {
                    context.TypeOfSettlement.Remove(typeOfSettlementToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи типа населенного пункта успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи типа населенного пункта.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи типа населенного пункта.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение записи типа населенного пункта по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа населенного пункта</returns>
        public TypeOfSettlement GetTypeOfSettlement(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа населенного пункта по уникальному идентификатору.");
            try
            {
                logger.Debug($"Поиск записи типа населенного пункта по уникальному идентификатору = {id}.");
                var typeOfSettlementById = context.TypeOfSettlement.AsNoTracking().FirstOrDefault(ts => ts.SettlementTypeId == id);
                if (typeOfSettlementById != null) logger.Debug($"Поиск окончен. Искомая запись: {typeOfSettlementById.ToString()}.");
                return typeOfSettlementById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа населенного пункта по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа населенного пункта по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи типа населенного пункта по наименованию
        /// </summary>
        /// <param name="fullname">Наименование типа</param>
        /// <returns>Запись типа населенного пункта</returns>
        public TypeOfSettlement GetTypeOfSettlement(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа населенного пункта по наименованию.");
            try
            {
                logger.Debug($"Поиск записи типа населенного пункта по наименованию = {fullname}.");
                var typeOfSettlementByName = context.TypeOfSettlement.AsNoTracking().FirstOrDefault(ts => ts.Fullname == fullname);
                if (typeOfSettlementByName != null) logger.Debug($"Поиск окончен. Искомая запись: {typeOfSettlementByName.ToString()}.");
                return typeOfSettlementByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа населенного пункта по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа населенного пункта по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка типов населенных пунктов
        /// </summary>
        /// <returns>Список типов населенных пунктов</returns>
        public List<TypeOfSettlement> GetTypeOfSettlements()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка типов населенных пунктов.");
            try
            {
                logger.Debug($"Получение списка типов населенных пунктов.");
                var typeOfSettlements = context.TypeOfSettlement.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {typeOfSettlements.Count}.");
                return typeOfSettlements;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка типов населенных пунктов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка типов населенных пунктов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление типа населенного пункта
        /// </summary>
        /// <param name="typeOfSettlement">Новый тип населенного пункта</param>
        /// <returns>Новая запись</returns>
        public TypeOfSettlement InsertTypeOfSettlement(TypeOfSettlement typeOfSettlement)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению типа населенного пункта");
            try
            {
                logger.Debug($"Добавляемая запись: {typeOfSettlement.ToString()}");
                context.TypeOfSettlement.Add(typeOfSettlement);
                context.SaveChanges();
                logger.Debug($"Тип населенного пункта успешно добавлен.");
                return typeOfSettlement;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления типа населенного пункта.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления типа населенного пункта.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }


        }
        /// <summary>
        /// Обновление типа населенного пункта
        /// </summary>
        /// <param name="typeOfSettlement">Редактируемый тип населенного пункта</param>
        /// <returns>Отредактированная запись</returns>
        public TypeOfSettlement UpdateTypeOfSettlement(TypeOfSettlement typeOfSettlement)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению типа населенного пункта.");
            try
            {
                var typeOfSettlementToUpdate = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == typeOfSettlement.SettlementTypeId);
                logger.Debug($"Текущая запись: {typeOfSettlementToUpdate.ToString()}");
                typeOfSettlementToUpdate.Fullname = typeOfSettlement.Fullname;
                typeOfSettlementToUpdate.Shortname = typeOfSettlement.Shortname;
                typeOfSettlementToUpdate.IsTown = typeOfSettlement.IsTown;
                context.SaveChanges();
                logger.Debug($"Новая запись: {typeOfSettlementToUpdate.ToString()}");
                return typeOfSettlementToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования типа населенного пункта.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования типа населенного пункта.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

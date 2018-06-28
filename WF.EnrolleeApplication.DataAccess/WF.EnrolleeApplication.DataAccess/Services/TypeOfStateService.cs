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
    /// Класс доступа к данным таблицы "Тип состояния"
    /// </summary>
    public class TypeOfStateService : ITypeOfStateService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public TypeOfStateService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление типа состояния
        /// </summary>
        /// <param name="typeOfState">Удаляемая запись</param>
        public void DeleteTypeOfState(TypeOfState typeOfState)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению типа состояния.");
            try
            {
                logger.Debug($"Поиск записи типа состояния для удаления. Удаляемый объект : {typeOfState.ToString()}.");
                var typeOfStateToDelete = context.TypeOfState.FirstOrDefault(ts => ts.StateId == typeOfState.StateId);
                if (typeOfStateToDelete != null)
                {
                    context.TypeOfState.Remove(typeOfStateToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи типа состояния успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи типа состояния.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи типа состояния.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение записи типа состояния по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа состояния</returns>
        public TypeOfState GetTypeOfState(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа состояния по уникальному идентификатору.");
            try
            {
                logger.Debug($"Поиск записи типа состояния по уникальному идентификатору = {id}.");
                var typeOfStateById = context.TypeOfState.AsNoTracking().FirstOrDefault(ts => ts.StateId == id);
                if (typeOfStateById != null) logger.Debug($"Поиск окончен. Искомая запись: {typeOfStateById.ToString()}.");
                return typeOfStateById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа состояния по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа состояния по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи типа состояния по наименованию
        /// </summary>
        /// <param name="name">Наименование состояния</param>
        /// <returns>Запись типа состояния</returns>
        public TypeOfState GetTypeOfState(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа состояния по наименованию.");
            try
            {
                logger.Debug($"Поиск записи типа состояния по наименованию = {name}.");
                var typeOfStateByName = context.TypeOfState.AsNoTracking().FirstOrDefault(ts => ts.Name == name);
                if (typeOfStateByName != null) logger.Debug($"Поиск окончен. Искомая запись: {typeOfStateByName.ToString()}.");
                return typeOfStateByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа состояния по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа состояния по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка типов состояния абитуриента
        /// </summary>
        /// <returns>Список типов состояния абитуриента</returns>
        public List<TypeOfState> GetTypeOfStates()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка типов состояния абитуриента.");
            try
            {
                logger.Debug($"Получение списка типов состояния абитуриента.");
                var typeOfStates = context.TypeOfState.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {typeOfStates.Count}.");
                return typeOfStates;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка типов состояния абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка типов состояния абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление типа состояния
        /// </summary>
        /// <param name="typeOfState">Новый тип состояния абитуриента</param>
        /// <returns>Новая запись</returns>
        public TypeOfState InsertTypeOfState(TypeOfState typeOfState)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению типа состояния");
            try
            {
                logger.Debug($"Добавляемая запись: {typeOfState.ToString()}");
                context.TypeOfState.Add(typeOfState);
                context.SaveChanges();
                logger.Debug($"Тип состояния успешно добавлен.");
                return typeOfState;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления типа состояния.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления типа состояния.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление типа состояния
        /// </summary>
        /// <param name="typeOfState">Редактируемый тип состояния абитуриента</param>
        /// <returns>Отредактированная запись</returns>
        public TypeOfState UpdateTypeOfState(TypeOfState typeOfState)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению типа состояния.");
            try
            {
                var typeOfStateToUpdate = context.TypeOfState.FirstOrDefault(ts => ts.StateId == typeOfState.StateId);
                logger.Debug($"Текущая запись: {typeOfStateToUpdate.ToString()}");
                typeOfStateToUpdate.Name = typeOfState.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись: {typeOfStateToUpdate.ToString()}");
                return typeOfStateToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования типа состояния.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования типа состояния.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

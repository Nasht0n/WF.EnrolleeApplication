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
    /// Класс доступа к данным таблицы "Места целевого направления"
    /// </summary>
    public class TargetWorkPlaceService : ITargetWorkPlaceService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TargetWorkPlaceService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление целевого рабочего места
        /// </summary>
        /// <param name="targetWorkPlace">Удаляемая запись</param>
        public void DeleteTargetWorkPlace(TargetWorkPlace targetWorkPlace)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению целевого рабочего места.");
            try
            {
                var targetWorkPlaceToDelete = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == targetWorkPlace.TargetId);
                if (targetWorkPlaceToDelete != null)
                {
                    context.TargetWorkPlace.Remove(targetWorkPlaceToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи целевого рабочего места.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи целевого рабочего места.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение целевого рабочего места по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись целевого места</returns>
        public TargetWorkPlace GetTargetWorkPlace(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску целевого рабочего места по уникальному идентификатору.");
            try
            {
                var targetWorkPlaceById = context.TargetWorkPlace.AsNoTracking().FirstOrDefault(tw => tw.TargetId == id);
                if (targetWorkPlaceById != null) logger.Debug($"Поиск окончен. Запись найдена {targetWorkPlaceById.ToString()}.");
                return targetWorkPlaceById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи целевого рабочего места по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи целевого рабочего места по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение целевого рабочего места по наименованию
        /// </summary>
        /// <param name="name">Наименование целевого места</param>
        /// <returns>Запись целевого места</returns>
        public TargetWorkPlace GetTargetWorkPlace(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску целевого рабочего места по наименованию.");
            try
            {
                var targetWorkPlaceByName = context.TargetWorkPlace.AsNoTracking().FirstOrDefault(tw => tw.Name == name);
                if (targetWorkPlaceByName != null) logger.Debug($"Поиск окончен. Запись найдена {targetWorkPlaceByName.ToString()}.");
                return targetWorkPlaceByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи целевого рабочего места по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи целевого рабочего места по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка целевых мест
        /// </summary>
        /// <returns>Список целевых мест</returns>
        public List<TargetWorkPlace> GetTargetWorkPlaces()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка целевых мест.");
            try
            {
                var targetWorkPlaces = context.TargetWorkPlace.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {targetWorkPlaces.Count}.");
                return targetWorkPlaces;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка целевых мест.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка целевых мест.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление целевого рабочего места
        /// </summary>
        /// <param name="targetWorkPlace">Новое целевое место</param>
        /// <returns>Новая запись</returns>
        public TargetWorkPlace InsertTargetWorkPlace(TargetWorkPlace targetWorkPlace)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению целевого рабочего места");
            try
            {
                logger.Debug($"Добавляемая запись {targetWorkPlace.ToString()}");
                context.TargetWorkPlace.Add(targetWorkPlace);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return targetWorkPlace;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления целевого рабочего места.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления целевого рабочего места.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление целевого рабочего места
        /// </summary>
        /// <param name="targetWorkPlace">Редактируемое целевое место</param>
        /// <returns>Отредактированная запись</returns>
        public TargetWorkPlace UpdateTargetWorkPlace(TargetWorkPlace targetWorkPlace)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению целевого рабочего места.");
            try
            {
                var targetWorkPlaceToUpdate = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == targetWorkPlace.TargetId);
                logger.Debug($"Текущая запись {targetWorkPlaceToUpdate.ToString()}");
                targetWorkPlaceToUpdate.Name = targetWorkPlace.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись {targetWorkPlaceToUpdate.ToString()}");
                return targetWorkPlaceToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования целевого рабочего места.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования целевого рабочего места.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

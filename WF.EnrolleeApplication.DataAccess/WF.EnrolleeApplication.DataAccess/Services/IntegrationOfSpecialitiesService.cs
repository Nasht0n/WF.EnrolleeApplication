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
    /// Класс доступа к данным таблицы "Интеграция специальностей"
    /// </summary>
    public class IntegrationOfSpecialitiesService : IIntegrationOfSpecialitiesService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IntegrationOfSpecialitiesService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление интеграции специальностей
        /// </summary>
        /// <param name="integrationOfSpecialities">Удаляемая запись об интеграции специальностей</param>
        public void DeleteIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению интеграции специальностей.");
            try
            {
                var integrationOfSpecialitiesToDelete = context.IntegrationOfSpecialities.FirstOrDefault(ios => ios.IntegrationId == integrationOfSpecialities.IntegrationId);
                if (integrationOfSpecialitiesToDelete != null)
                {
                    context.IntegrationOfSpecialities.Remove(integrationOfSpecialitiesToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи интеграции специальностей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи интеграции специальностей.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение списка интеграций специальностей
        /// </summary>
        /// <returns>Список интеграций специальностей</returns>
        public List<IntegrationOfSpecialities> GetIntegrationOfSpecialities()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка интеграций специальностей.");
            try
            {
                var integrationOfSpecialities = context.IntegrationOfSpecialities.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {integrationOfSpecialities.Count}.");
                return integrationOfSpecialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка интеграций специальностей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка интеграций специальностей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        ///  Получение списка интеграций специальностей
        /// </summary>
        /// <param name="speciality">Фильтр по специальности первой ступени</param>
        /// <returns>Список интеграций специальностей</returns>
        public List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка интеграций специальностей. Фильтр — специальность первой ступени.");
            try
            {
                var integrationOfSpecialities = context.IntegrationOfSpecialities.AsNoTracking().Where(ios => ios.FirstSpecialityId == speciality.SpecialityId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {integrationOfSpecialities.Count}.");
                return integrationOfSpecialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка интеграций специальностей. Фильтр — специальность первой ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка интеграций специальностей. Фильтр — специальность первой ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка интеграций специальностей
        /// </summary>
        /// <param name="secondarySpeciality">Фильтр по специальности второй ступени</param>
        /// <returns>Список интеграций специальностей</returns>
        public List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(SecondarySpeciality secondarySpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка интеграций специальностей. Фильтр — специальность второй ступени.");
            try
            {
                var integrationOfSpecialities = context.IntegrationOfSpecialities.AsNoTracking().Where(ios => ios.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {integrationOfSpecialities.Count}.");
                return integrationOfSpecialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка интеграций специальностей. Фильтр — специальность второй ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка интеграций специальностей. Фильтр — специальность второй ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи об интеграции специальностей по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись интеграции</returns>
        public IntegrationOfSpecialities GetIntegrationOfSpecialities(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску интеграции специальностей по уникальному идентификатору.");
            try
            {
                var integrationOfSpecialityById = context.IntegrationOfSpecialities.AsNoTracking().FirstOrDefault(ios => ios.IntegrationId == id);
                if (integrationOfSpecialityById != null) logger.Debug($"Поиск окончен. Запись найдена {integrationOfSpecialityById.ToString()}.");
                return integrationOfSpecialityById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи интеграции специальностей по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи интеграции специальностей по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи об интеграции специальностей по параметрам
        /// </summary>
        /// <param name="speciality">Фильтр по специальности первой ступени</param>
        /// <param name="secondarySpeciality">Фильтр по специальности второй ступени</param>
        /// <returns>Запись интеграции</returns>
        public IntegrationOfSpecialities GetIntegrationOfSpecialities(Speciality speciality, SecondarySpeciality secondarySpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску интеграции специальностей по параметрам.");
            try
            {
                var integrationOfSpeciality = context.IntegrationOfSpecialities.AsNoTracking().FirstOrDefault(ios => ios.FirstSpecialityId == speciality.SpecialityId && ios.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId);
                if (integrationOfSpeciality != null) logger.Debug($"Поиск окончен. Запись найдена {integrationOfSpeciality.ToString()}.");
                return integrationOfSpeciality;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи интеграции специальностей по параметрам.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи интеграции специальностей по параметрам.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление интеграции специальностей
        /// </summary>
        /// <param name="integrationOfSpecialities">Новая запись об интеграции специальностей</param>
        /// <returns>Новая запись</returns>
        public IntegrationOfSpecialities InsertIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению интеграции специальностей");
            try
            {
                logger.Debug($"Добавляемая запись {integrationOfSpecialities.ToString()}");
                context.IntegrationOfSpecialities.Add(integrationOfSpecialities);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return integrationOfSpecialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления интеграции специальностей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления интеграции специальностей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление интеграции специальностей
        /// </summary>
        /// <param name="integrationOfSpecialities">Редактируемая запись об интеграции специальностей</param>
        /// <returns>Отредактированная запись</returns>
        public IntegrationOfSpecialities UpdateIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению интеграции специальностей.");
            try
            {
                var integrationOfSpecialitiesToUpdate = context.IntegrationOfSpecialities.FirstOrDefault(ios => ios.IntegrationId == integrationOfSpecialities.IntegrationId);
                logger.Debug($"Текущая запись {integrationOfSpecialitiesToUpdate.ToString()}");
                integrationOfSpecialitiesToUpdate.FirstSpecialityId = integrationOfSpecialities.FirstSpecialityId;
                integrationOfSpecialitiesToUpdate.SecondarySpecialityId = integrationOfSpecialities.SecondarySpecialityId;
                context.SaveChanges();
                logger.Debug($"Новая запись {integrationOfSpecialitiesToUpdate.ToString()}");
                return integrationOfSpecialitiesToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования интеграции специальностей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования интеграции специальностей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

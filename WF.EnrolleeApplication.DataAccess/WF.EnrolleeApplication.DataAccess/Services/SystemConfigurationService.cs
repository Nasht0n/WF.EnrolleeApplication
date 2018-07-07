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
    /// Класс доступа к данным таблицы "Настройки системы"
    /// </summary>
    public class SystemConfigurationService : ISystemConfigurationService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public SystemConfigurationService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление настройки системы
        /// </summary>
        /// <param name="systemConfiguration">Удаляемая настройка системы</param>
        public void DeleteSystemConfiguration(SystemConfiguration systemConfiguration)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению настройки системы.");
            try
            {
                var systemConfigurationToDelete = context.SystemConfiguration.FirstOrDefault(sc => sc.Name == systemConfiguration.Name);
                if (systemConfigurationToDelete != null)
                {
                    context.SystemConfiguration.Remove(systemConfigurationToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи настройки системы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи настройки системы.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение настройки системы по наименованию
        /// </summary>
        /// <param name="name">Наименование параметра</param>
        /// <returns>Запись настройки</returns>
        public SystemConfiguration GetSystemConfiguration(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску настройки системы по наименованию параметра.");
            try
            {
                var systemConfiguration = context.SystemConfiguration.AsNoTracking().FirstOrDefault(sc => sc.Name == name);
                if (systemConfiguration != null) logger.Debug($"Поиск окончен. Запись найдена {systemConfiguration.ToString()}.");
                return systemConfiguration;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи настройки системы по наименованию параметра.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи настройки системы по наименованию параметра.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка настроек системы
        /// </summary>
        /// <returns>Список настроек</returns>
        public List<SystemConfiguration> GetSystemConfigurations()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка настроек системы.");
            try
            {
                var systemConfigurations = context.SystemConfiguration.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {systemConfigurations.Count}.");
                return systemConfigurations;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка настроек системы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка настроек системы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление настройки системы
        /// </summary>
        /// <param name="systemConfiguration">Новая настройка системы</param>
        /// <returns>Новая запись</returns>
        public SystemConfiguration InsertSystemConfiguration(SystemConfiguration systemConfiguration)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению настройки системы");
            try
            {
                logger.Debug($"Добавляемая запись {systemConfiguration.ToString()}");
                context.SystemConfiguration.Add(systemConfiguration);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return systemConfiguration;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления настройки системы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления настройки системы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление настройки системы
        /// </summary>
        /// <param name="systemConfiguration">Редактируемая настройка системы</param>
        /// <returns>Отредактированная настройка системы</returns>
        public SystemConfiguration UpdateSystemConfiguration(SystemConfiguration systemConfiguration)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению настройки системы.");
            try
            {
                var systemConfigurationToUpdate = context.SystemConfiguration.FirstOrDefault(sc => sc.Name == systemConfiguration.Name);
                logger.Debug($"Текущая запись {systemConfigurationToUpdate.ToString()}");
                systemConfigurationToUpdate.Title = systemConfiguration.Title;
                systemConfigurationToUpdate.Value = systemConfiguration.Value;
                context.SaveChanges();
                logger.Debug($"Новая запись {systemConfigurationToUpdate.ToString()}");
                return systemConfigurationToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования настройки системы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования настройки системы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

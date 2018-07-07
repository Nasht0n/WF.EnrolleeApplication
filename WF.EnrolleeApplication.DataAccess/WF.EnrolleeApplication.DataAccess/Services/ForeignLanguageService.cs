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
    /// Класс доступа к данным таблицы "Иностранные языки"
    /// </summary>
    public class ForeignLanguageService : IForeignLanguageService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ForeignLanguageService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление иностранного языка
        /// </summary>
        /// <param name="foreignLanguage">Удаляемый иностранный язык</param>
        public void DeleteForeignLanguage(ForeignLanguage foreignLanguage)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению иностранного языка.");
            try
            {
                var foreignLanguageToDelete = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == foreignLanguage.LanguageId);
                if (foreignLanguageToDelete != null)
                {
                    context.ForeignLanguage.Remove(foreignLanguageToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи иностранного языка.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи иностранного языка.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение иностранного языка по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись иностранного языка</returns>
        public ForeignLanguage GetForeignLanguage(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску иностранного языка по уникальному идентификатору.");
            try
            {
                var foreignLanguageById = context.ForeignLanguage.AsNoTracking().FirstOrDefault(fl => fl.LanguageId == id);
                if (foreignLanguageById != null) logger.Debug($"Поиск окончен. Запись найдена {foreignLanguageById.ToString()}.");
                return foreignLanguageById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи иностранного языка по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи иностранного языка по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение иностранного языка по наименованию
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns>Запись иностранного языка</returns>
        public ForeignLanguage GetForeignLanguage(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску иностранного языка по наименованию.");
            try
            {
                var foreignLanguageByName = context.ForeignLanguage.AsNoTracking().FirstOrDefault(fl => fl.Name == name);
                if (foreignLanguageByName != null) logger.Debug($"Поиск окончен. Запись найдена {foreignLanguageByName.ToString()}.");
                return foreignLanguageByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи иностранного языка по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи иностранного языка по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка иностранных языков
        /// </summary>
        /// <returns>Список иностранных языков</returns>
        public List<ForeignLanguage> GetForeignLanguages()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка иностранных языков.");
            try
            {
                var foreignLanguages = context.ForeignLanguage.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {foreignLanguages.Count}.");
                return foreignLanguages;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка иностранных языков.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка иностранных языков.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Новый иностранный язык
        /// </summary>
        /// <param name="foreignLanguage">Иностранный язык</param>
        /// <returns>Новая запись</returns>
        public ForeignLanguage InsertForeignLanguage(ForeignLanguage foreignLanguage)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению иностранного языка");
            try
            {
                logger.Debug($"Добавляемая запись {foreignLanguage.ToString()}");
                context.ForeignLanguage.Add(foreignLanguage);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return foreignLanguage;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления иностранного языка.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления иностранного языка.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление иностранного языка
        /// </summary>
        /// <param name="foreignLanguage">Редактируемый иностранный язык</param>
        /// <returns>Отредактированная запись</returns>
        public ForeignLanguage UpdateForeignLanguage(ForeignLanguage foreignLanguage)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению иностранного языка.");
            try
            {
                var foreignLanguageToUpdate = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == foreignLanguage.LanguageId);
                logger.Debug($"Текущая запись {foreignLanguageToUpdate.ToString()}");
                foreignLanguageToUpdate.Name = foreignLanguage.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись {foreignLanguageToUpdate.ToString()}");
                return foreignLanguageToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования иностранного языка.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования иностранного языка.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

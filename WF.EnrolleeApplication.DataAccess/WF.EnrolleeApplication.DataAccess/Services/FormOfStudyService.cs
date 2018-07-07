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
    /// Класс доступа к данным таблицы "Формы обучения"
    /// </summary>
    public class FormOfStudyService : IFormOfStudyService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public FormOfStudyService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление формы обучения
        /// </summary>
        /// <param name="formOfStudy">Удаляемая форма обучения</param>
        public void DeleteFormOfStudy(FormOfStudy formOfStudy)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению формы обучения.");
            try
            {
                var formOfStudyToDelete = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == formOfStudy.FormOfStudyId);
                if (formOfStudyToDelete != null)
                {
                    context.FormOfStudy.Remove(formOfStudyToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи формы обучения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи формы обучения.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение списка форм обучения
        /// </summary>
        /// <returns>Список форм обучения</returns>
        public List<FormOfStudy> GetFormOfStudies()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка форм обучения.");
            try
            {
                var formOfStudies = context.FormOfStudy.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {formOfStudies.Count}.");
                return formOfStudies;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка форм обучения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка форм обучения.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение формы обучения по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись формы обучения</returns>
        public FormOfStudy GetFormOfStudy(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску формы обучения по уникальному идентификатору.");
            try
            {
                var formOfStudyById = context.FormOfStudy.AsNoTracking().FirstOrDefault(f => f.FormOfStudyId == id);
                if (formOfStudyById != null) logger.Debug($"Поиск окончен. Запись найдена {formOfStudyById.ToString()}.");
                return formOfStudyById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи формы обучения по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи формы обучения по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение формы обучения по наименованию
        /// </summary>
        /// <param name="fullname">Наименование формы обучения</param>
        /// <returns>Запись формы обучения</returns>
        public FormOfStudy GetFormOfStudy(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску формы обучения по наименованию.");
            try
            {
                var formOfStudyByName = context.FormOfStudy.AsNoTracking().FirstOrDefault(f => f.Fullname == fullname);
                if (formOfStudyByName != null) logger.Debug($"Поиск окончен. Запись найдена {formOfStudyByName.ToString()}.");
                return formOfStudyByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи формы обучения по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи формы обучения по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление формы обучения
        /// </summary>
        /// <param name="formOfStudy">Новая форма обучения</param>
        /// <returns>Новая запись</returns>
        public FormOfStudy InsertFormOfStudy(FormOfStudy formOfStudy)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению формы обучения");
            try
            {
                logger.Debug($"Добавляемая запись {formOfStudy.ToString()}");
                context.FormOfStudy.Add(formOfStudy);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return formOfStudy;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления формы обучения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления формы обучения.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление формы обучения
        /// </summary>
        /// <param name="formOfStudy">Редактируемая форма обучения</param>
        /// <returns>Отредактированная форма обучения</returns>
        public FormOfStudy UpdateFormOfStudy(FormOfStudy formOfStudy)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению формы обучения.");
            try
            {
                var formOfStudyToUpdate = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == formOfStudy.FormOfStudyId);
                logger.Debug($"Текущая запись {formOfStudyToUpdate.ToString()}");
                formOfStudyToUpdate.Fullname = formOfStudy.Fullname;
                formOfStudyToUpdate.Shortname = formOfStudy.Shortname;
                context.SaveChanges();
                logger.Debug($"Новая запись {formOfStudyToUpdate.ToString()}");
                return formOfStudyToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования формы обучения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования формы обучения.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

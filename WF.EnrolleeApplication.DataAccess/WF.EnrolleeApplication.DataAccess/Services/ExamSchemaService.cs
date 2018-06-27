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
    /// Класс доступа к данным таблицы "Экзаменнационные схемы"
    /// </summary>
    public class ExamSchemaService : IExamSchemaService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ExamSchemaService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи экзаменнационной схемы
        /// </summary>
        /// <param name="examSchema">Удаляемая схема</param>
        public void DeleteExamSchema(ExamSchema examSchema)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению экзаменнационной схемы.");
            try
            {
                logger.Debug($"Поиск записи экзаменнационной схемы оценки для удаления. Удаляемый объект : {examSchema.ToString()}.");
                var examSchemaToDelete = context.ExamSchema.FirstOrDefault(es => es.DisciplineId == examSchema.DisciplineId && es.SpecialityId == examSchema.SpecialityId);
                if (examSchemaToDelete != null)
                {
                    context.ExamSchema.Remove(examSchemaToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи экзаменнационной схемы успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи экзаменнационной схемы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи экзаменнационной схемы.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение записи экзаменнационной схемы
        /// </summary>
        /// <param name="speciality">Специальность</param>
        /// <param name="discipline">Дисциплина</param>
        /// <returns>Запись экзаменнационной схемы</returns>
        public ExamSchema GetExamSchema(Speciality speciality, Discipline discipline)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску экзаменнационной схемы.");
            try
            {
                logger.Debug($"Поиск записи экзаменнационной схемы. Специальность = [{speciality.ToString()}]; Дисциплина = [{discipline.ToString()}].");
                var examSchema = context.ExamSchema.AsNoTracking().FirstOrDefault(es => es.DisciplineId == discipline.DisciplineId && es.SpecialityId == speciality.SpecialityId);
                if (examSchema != null) logger.Debug($"Поиск окончен. Искомая запись: {examSchema.ToString()}.");
                return examSchema;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи экзаменнационной схемы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи экзаменнационной схемы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка экзаменнационых схем
        /// </summary>
        /// <returns>Список экзаменнационных схем</returns>
        public List<ExamSchema> GetExamSchemas()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению списка экзаменнационых схем.");
            try
            {
                logger.Debug($"Поиск списка экзаменнационых схема.");
                var examSchemas = context.ExamSchema.AsNoTracking().ToList();
                if (examSchemas != null) logger.Debug($"Поиск окончен. Искомая запись: {examSchemas.ToString()}.");
                return examSchemas;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиск списка экзаменнационых схема.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиск списка экзаменнационых схема.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка экзаменнационной схемы специальности
        /// </summary>
        /// <param name="speciality">Специальность</param>
        /// <returns>Список экзаменнационных схем</returns>
        public List<ExamSchema> GetExamSchemas(Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению списка экзаменнационых схем.");
            try
            {
                logger.Debug($"Поиск списка экзаменнационых схем. Специальность = [{speciality.ToString()}]");
                var examSchemas = context.ExamSchema.AsNoTracking().Where(es => es.SpecialityId == speciality.SpecialityId).ToList();
                if (examSchemas != null) logger.Debug($"Поиск окончен. Искомая запись: {examSchemas.ToString()}.");
                return examSchemas;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиск списка экзаменнационых схем, выбранной специальности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиск списка экзаменнационых схем, выбранной специальности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Новая запись экзаменнационной схемы
        /// </summary>
        /// <param name="examSchema">Экзаменнационная схема</param>
        /// <returns>Новая запись</returns>
        public ExamSchema InsertExamSchema(ExamSchema examSchema)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению экзаменнационной схемы");
            try
            {
                logger.Debug($"Добавляемая запись: {examSchema.ToString()}");
                context.ExamSchema.Add(examSchema);
                context.SaveChanges();
                logger.Debug($"Экзаменнационная схема успешно добавлена.");
                return examSchema;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления экзаменнационной схемы.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления экзаменнационной схемы.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}

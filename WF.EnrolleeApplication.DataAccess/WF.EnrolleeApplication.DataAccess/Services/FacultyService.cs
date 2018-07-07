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
    /// Класс доступа к данным таблицы "Факульты"
    /// </summary>
    public class FacultyService : IFacultyService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public FacultyService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление факультета
        /// </summary>
        /// <param name="faculty">Удаляемый факультет</param>
        public void DeleteFaculty(Faculty faculty)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению факультета.");
            try
            {
                var facultyToDelete = context.Faculty.FirstOrDefault(f => f.FacultyId == faculty.FacultyId);
                if (facultyToDelete != null)
                {
                    context.Faculty.Remove(facultyToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи факультета.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи факультета.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение списка факультетов
        /// </summary>
        /// <returns>Список факультетов</returns>
        public List<Faculty> GetFaculties()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка факультетов.");
            try
            {
                var faculties = context.Faculty.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {faculties.Count}.");
                return faculties;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка факультетов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка факультетов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение факультета по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись факультета</returns>
        public Faculty GetFaculty(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску факультета по уникальному идентификатору.");
            try
            {
                var facultyById = context.Faculty.AsNoTracking().FirstOrDefault(f => f.FacultyId == id);
                if (facultyById != null) logger.Debug($"Поиск окончен. Запись найдена {facultyById.ToString()}.");
                return facultyById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи факультета по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи факультета по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение факультета по наименованию
        /// </summary>
        /// <param name="fullname">Наименование факультета</param>
        /// <returns>Запись факультета</returns>
        public Faculty GetFaculty(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску факультета по наименованию.");
            try
            {
                var facultyByName = context.Faculty.AsNoTracking().FirstOrDefault(f => f.Fullname == fullname);
                if (facultyByName != null) logger.Debug($"Поиск окончен. Запись найдена {facultyByName.ToString()}.");
                return facultyByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи факультета по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи факультета по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Новая запись факультета
        /// </summary>
        /// <param name="faculty">Новый факультет</param>
        /// <returns>Новая запись</returns>
        public Faculty InsertFaculty(Faculty faculty)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению факультета");
            try
            {
                logger.Debug($"Добавляемая запись {faculty.ToString()}");
                context.Faculty.Add(faculty);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return faculty;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления факультета.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления факультета.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="faculty">Редактируемый факультет</param>
        /// <returns>Отредактированная запись</returns>
        public Faculty UpdateFaculty(Faculty faculty)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению факультета.");
            try
            {
                var facultyToUpdate = context.Faculty.FirstOrDefault(f => f.FacultyId == faculty.FacultyId);
                logger.Debug($"Текущая запись {facultyToUpdate.ToString()}");
                facultyToUpdate.Fullname = faculty.Fullname;
                facultyToUpdate.Shortname = faculty.Shortname;
                context.SaveChanges();
                logger.Debug($"Новая запись {facultyToUpdate.ToString()}");
                return facultyToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования факультета.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования факультета.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
